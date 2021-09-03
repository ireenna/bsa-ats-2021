import { Observable, Subject } from 'rxjs';
import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { map, takeUntil, mergeMap } from 'rxjs/operators';
import { Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Applicant } from 'src/app/shared/models/applicants/applicant';
import { ApplicantsService } from 'src/app/shared/services/applicants.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { MatPaginator } from '@angular/material/paginator';
import { StylePaginatorDirective } from 'src/app/shared/directives/style-paginator.directive';
import { Tag } from 'src/app/shared/models/tags/tag';
import { ViewableApplicant } from 'src/app/shared/models/applicants/viewable-applicant';
import { ActivatedRoute } from '@angular/router';
import { FollowedService } from 'src/app/shared/services/followedService';
import { EntityType } from 'src/app/shared/enums/entity-type.enum';
import {
  FilterDescription,
  FilterType,
  TableFilterComponent,
} from 'src/app/shared/components/table-filter/table-filter.component';
import { IOption } from 'src/app/shared/components/multiselect/multiselect.component';
import { ApplicantVacancyInfo } from 'src/app/shared/models/applicants/applicant-vacancy-info';

@Component({
  selector: 'app-applicants',
  templateUrl: 'applicants.component.html',
  styleUrls: ['applicants.component.scss'],
})
export class ApplicantsComponent implements OnInit, OnDestroy, AfterViewInit {
  public displayedColumns: string[] = [
    'position',
    'name',
    'email',
    'jobs_list',
    'tags',
    'creation_date',
    'control_buttons',
  ];

  public dataSource = new MatTableDataSource<ViewableApplicant>();
  public cashedData: ViewableApplicant[] = [];
  public filteredData: ViewableApplicant[] = [];
  public filterDescription: FilterDescription = [];
  public searchValue = '';
  public page:string = 'all';
  public loading: boolean = true;

  @ViewChild(MatPaginator) public paginator: MatPaginator | undefined =
  undefined;

  @ViewChild(StylePaginatorDirective) public directive:
  | StylePaginatorDirective
  | undefined = undefined;

  @ViewChild('filter') public filter!: TableFilterComponent;

  private followedSet: Set<string> = new Set();
  private readonly applicantPageToken: string = 'applicantPageToken';
  private readonly unsubscribe$: Subject<void> = new Subject<void>();

  constructor(
    private readonly notificationsService: NotificationService,
    private readonly applicantsService: ApplicantsService,
    private readonly route: ActivatedRoute,
    private followService: FollowedService,
  ) {}

  public ngOnInit(): void {
    this.followService
      .getFollowed(EntityType.Applicant)
      .pipe(
        takeUntil(this.unsubscribe$),
        mergeMap((data) => {
          data.forEach((item) => this.followedSet.add(item.entityId));
          return this.applicantsService.getApplicants();
        }),
        map((arr) =>
          arr.map((a) => {
            let viewableApplicant = a as any as ViewableApplicant;

            viewableApplicant.isFollowed = this.followedSet.has(a.id);
            viewableApplicant.isShowAllTags = false;

            if (!a.tags) {
              viewableApplicant.tags = {
                id: '',
                elasticType: 1,
                tagDtos: [],
              };
            }

            return viewableApplicant;
          }),
        ),
      )
      .subscribe(
        (result: ViewableApplicant[]) => {
          result.forEach((d, i) => {
            d.position = i + 1;
          });
  
          this.loading = false;
          this.dataSource.data = result;
  
          if (localStorage.getItem(this.applicantPageToken) == 'followed') {
            this.dataSource.data = this.dataSource.data.filter(a => a.isFollowed);
          }
      
          if (localStorage.getItem(this.applicantPageToken) == 'self-applied') {
            this.dataSource.data = this.dataSource.data.filter(a => a.isSelfApplied);
          }

          this.cashedData = result;
          this.renewFilterDescription();
          this.directive!.applyFilter$.emit();
        },
        (error: Error) => {
          this.loading = false;

          this.notificationsService.showErrorMessage(
            error.message,
            'Cannot download applicants from the host',
          );
        },
      );
    this.page = localStorage.getItem(this.applicantPageToken) ? 
      localStorage.getItem(this.applicantPageToken)! : 'all';
    this.getApplicants();
  }

  public getApplicants() {
    this.applicantsService
      .getApplicants()
      .pipe(
        takeUntil(this.unsubscribe$),
        map((arr) =>
          arr.map((a) => {
            let viewableApplicant = a as any as ViewableApplicant;

            viewableApplicant.isFollowed = this.followedSet.has(a.id);
            viewableApplicant.isShowAllTags = false;

            if (!a.tags) {
              viewableApplicant.tags = {
                id: '',
                elasticType: 1,
                tagDtos: [],
              };
            }

            return viewableApplicant;
          }),
        ),
      )
      .subscribe(
        (result: ViewableApplicant[]) => {
          result.forEach((d, i) => {
            d.position = i + 1;
          });

          this.loading = false;
          this.dataSource.data = result;
            
          this.cashedData = result;
          this.renewFilterDescription();
          this.directive!.applyFilter$.emit();
        },
        (error: Error) => {
          this.loading = false;

          this.notificationsService.showErrorMessage(
            error.message,
            'Cannot download applicants from the host',
          );
        },
      );
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onTagClick(tag: Tag): void {
    this.filter.extraAdd('tags', {
      id: tag.id,
      value: tag.id,
      label: tag.tagName,
    });
  }

  public applySearchValue(searchValue: string) {
    this.searchValue = searchValue;
    this.dataSource.filter = searchValue;

    if (this.dataSource.paginator) {
      this.directive?.applyFilter$.emit();
      this.dataSource.paginator.firstPage();
    }
  }

  public ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator!;
    this.dataSource.filter = this.searchValue.trim().toLowerCase();
  }

  public renewFilterDescription(): void {
    const detectedTagIds: string[] = [];
    const detectedVacancyNames: string[] = [];

    const tags: IOption[] = [];
    const vacancies: IOption[] = [];

    this.cashedData.forEach((a) => {
      a.tags.tagDtos.forEach((tag) => {
        if (!detectedTagIds.includes(tag.id)) {
          tags.push({
            id: tag.id,
            value: tag.id,
            label: tag.tagName,
          });

          detectedTagIds.push(tag.id);
        }
      });

      a.vacancies.forEach((vacancy) => {
        if (!detectedVacancyNames.includes(vacancy.title)) {
          vacancies.push({
            id: vacancy.title,
            value: vacancy.title,
            label: vacancy.title,
          });

          detectedVacancyNames.push(vacancy.title);
        }
      });
    });

    this.filterDescription = [
      {
        id: 'firstName',
        name: 'First name',
      },
      {
        id: 'lastName',
        name: 'Last name',
      },
      {
        id: 'email',
        name: 'Email',
      },
      {
        id: 'vacancies',
        name: 'Jobs',
        type: FilterType.Multiple,
        multipleSettings: {
          options: vacancies,
          valueSelector: (applicant) =>
            applicant.vacancies.map((vac: ApplicantVacancyInfo) => vac.title),
        },
      },
      {
        id: 'tags',
        name: 'Tags',
        type: FilterType.Multiple,
        multipleSettings: {
          options: tags,
          canBeExtraModified: true,
          valueSelector: (applicant) =>
            applicant.tags.tagDtos.map((tag: Tag) => tag.id),
        },
      },
      {
        id: 'creationDate',
        name: 'Creation date',
        type: FilterType.Date,
      },
    ];
  }

  public setFiltered(data: ViewableApplicant[]): void {
    this.filteredData = data;

    if (localStorage.getItem(this.applicantPageToken) == 'followed') {
      this.dataSource.data = this.filteredData.filter((item) =>
        this.followedSet.has(item.id),
      );
    } 
    else if (this.page == 'self-applied') {
      this.dataSource.data = this.dataSource.data.filter(a => a.isSelfApplied);
    } 
    else {
      this.dataSource.data = this.filteredData;
    }

    this.directive?.applyFilter$.emit();
    this.dataSource.paginator?.firstPage();
  }

  public createApplicant(createdApplicant: Observable<Applicant>): void {
    createdApplicant
      .pipe(
        map((a: Applicant) => {
          let viewableApplicant = a as any as ViewableApplicant;

          if (viewableApplicant) {
            viewableApplicant.isShowAllTags = false;
            viewableApplicant.isFollowed = false;
            viewableApplicant.position = this.cashedData.length + 1;
          }

          return viewableApplicant;
        }),
      )
      .subscribe((result: ViewableApplicant) => {
        if (result) {
          this.cashedData.unshift(result);
          this.dataSource.data = this.cashedData;

          if (this.page) {
            this.dataSource.data = this.dataSource.data.filter(a => a.isFollowed);
          }

          this.renewFilterDescription();
          this.directive?.applyFilter$.emit();

          this.notificationsService.showSuccessMessage(
            'An applicant was succesfully created',
            'Success!',
          );
        }
      });
  }

  public updateApplicant(applicant: ViewableApplicant): void {
    if (applicant) {
      let applicantIndex = this.cashedData.findIndex(
        (a) => a.id === applicant.id,
      );

      applicant.position = this.cashedData[applicantIndex].position;
      applicant.isFollowed = this.cashedData[applicantIndex].isFollowed;

      const newCachedData = [...this.cashedData];
      newCachedData[applicantIndex] = applicant;

      this.cashedData = [...newCachedData];
      this.dataSource.data = this.cashedData;

      if (this.page) {
        this.dataSource.data = this.dataSource.data.filter(a => a.isFollowed);
      }

      this.renewFilterDescription();
      this.directive?.applyFilter$.emit();

      this.notificationsService.showSuccessMessage(
        'An applicant was succesfully updated',
        'Success!',
      );
    }
  }

  public deleteApplicant(applicantId: string): void {
    let applicantIndex = this.dataSource.data.findIndex(
      (a) => a.id === applicantId,
    );

    const newCachedData = [...this.cashedData];
    newCachedData.splice(applicantIndex, 1);

    this.cashedData = [...newCachedData];
    this.dataSource.data = this.cashedData;

    if (this.page) {
      this.dataSource.data = this.dataSource.data.filter(a => a.isFollowed);
    }

    this.renewFilterDescription();
    this.directive?.applyFilter$.emit();

    this.notificationsService.showSuccessMessage(
      'The applicant was successfully deleted',
      'Success!',
    );
  }

  public toggleFollowedOrAll(page: string): void {
    this.page = page;

    this.dataSource.data = this.cashedData;

    if (page == 'followed') {
      this.dataSource.data = this.dataSource.data.filter(a => a.isFollowed);
    }

    if (page == 'self-applied') {
      this.dataSource.data = this.dataSource.data.filter(a => a.isSelfApplied);
    }

    this.followService.switchRefreshFollowedPageToken(page, this.applicantPageToken);
    this.directive!.applyFilter$.emit();
  }

  public onBookmark(applicantId: string) {
    const applicantIndex = this.cashedData.findIndex(
      (a) => a.id === applicantId,
    );
    this.cashedData[applicantIndex].isFollowed =
      !this.cashedData[applicantIndex].isFollowed;
    this.dataSource.data = this.cashedData;
    if (this.cashedData[applicantIndex].isFollowed) {
      this.followService
        .createFollowed({
          entityId: this.cashedData[applicantIndex].id,
          entityType: EntityType.Applicant,
        })
        .subscribe();
    } else {
      this.followService
        .deleteFollowed(
          EntityType.Applicant,
          this.cashedData[applicantIndex].id,
        )
        .subscribe();
    }
    if (this.page == 'followed') {
      this.dataSource.data = this.dataSource.data.filter(a => a.isFollowed);
    }

    if (this.page == 'self-applied') {
      this.dataSource.data = this.dataSource.data.filter(a => a.isSelfApplied);
    }

    this.directive!.applyFilter$.emit();
  }

  public getFirstTags(applicant: ViewableApplicant): Tag[] {
    if (!applicant.tags?.tagDtos) {
      return [];
    }

    return applicant.tags.tagDtos.length > 6
      ? applicant.tags.tagDtos.slice(0, 5)
      : applicant.tags.tagDtos;
  }

  public toggleTags(applicant: ViewableApplicant): void {
    applicant.isShowAllTags = applicant.isShowAllTags ? false : true;
  }

  public sortData(sort: Sort): void {
    this.dataSource.data = (this.dataSource.data as ViewableApplicant[]).sort(
      (a, b) => {
        const isAsc = sort.direction === 'asc';

        switch (sort.active) {
          case 'position':
            return this.compareRows(a.position, b.position, isAsc);
          case 'name':
            return this.compareRows(
              a.firstName + ' ' + a.lastName,
              b.firstName + ' ' + b.lastName,
              isAsc,
            );
          case 'email':
            return this.compareRows(a.email, b.email, isAsc);
          case 'tags':
            return this.compareRows(
              a.tags.tagDtos.length,
              b.tags.tagDtos.length,
              isAsc,
            );
          case 'creation_date':
            return this.compareDates(a.creationDate, b.creationDate, isAsc);
          default:
            return 0;
        }
      },
    );
  }

  private compareRows(
    a: number | string,
    b: number | string,
    isAsc: boolean,
  ): number {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  private compareDates(a: Date, b: Date, isAsc: boolean): number {
    return (a.getTime() < b.getTime() ? -1 : 1) * (isAsc ? 1 : -1);
  }
}

import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import moment from 'moment';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
// eslint-disable-next-line
import { AvatarModalComponent } from 'src/app/shared/components/avatar-modal/avatar-modal.component';
import { FileInputComponent } from 'src/app/shared/components/file-input/file-input.component';
import { SelectCvComponent } from 'src/app/shared/components/select-cv/select-cv.component';
import { FileType } from 'src/app/shared/enums/file-type.enum';
import { FullVacancyCandidate } from 'src/app/shared/models/vacancy-candidates/full';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { VacancyCandidateService } from 'src/app/shared/services/vacancy-candidate.service';
import { CvFileInfo } from 'src/app/shared/models/file/cvFileInfo';
import { CandidateCvService } from 'src/app/shared/services/candidate-cv.service';

@Component({
  selector: 'app-one-candidate',
  templateUrl: './one-candidate.component.html',
  styleUrls: ['./one-candidate.component.scss'],
})
export class OneCandidateComponent implements OnInit, OnDestroy {
  @Input() public id!: string;
  @Input() public vacancyId!: string;

  public data!: FullVacancyCandidate;
  public loading: boolean = true;

  private readonly unsubscribe$: Subject<void> = new Subject<void>();

  public constructor(
    private readonly candidateService: VacancyCandidateService,
    private readonly notificationService: NotificationService,
    private readonly candidateCvService: CandidateCvService,
    private readonly dialog: MatDialog,
  ) {}

  public ngOnInit(): void {
    this.loadData(this.id);
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public enlargeAvatar(): void {
    this.dialog.open(AvatarModalComponent, {
      data: { url: '../../../../assets/images/defaultAvatar.png' }, // TODO: Add real url
    });
  }

  private loadData(id: string): void {
    this.candidateService
      .getFull(id, this.vacancyId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        (data) => {
          console.log(data);
          this.loading = false;
          this.data = data;
        },
        () => {
          this.loading = false;
          this.notificationService.showErrorMessage('Failed to load', 'Error');
        },
      );
  }

  public openCvSelectDialog(): void {
    let dialogRef = this.dialog.open(SelectCvComponent, {
      width: '600px',
      data: this.id,
    });

    dialogRef.afterClosed()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((cvFileInfo: CvFileInfo) => {
        this.data.cvName = cvFileInfo.name;
        this.data.cvLink = cvFileInfo.url;
      });
  }

  public detachCv(): void {
    this.candidateCvService.detachCv(this.id)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        this.data.cvName = undefined;
        this.data.cvLink = undefined;
        this.notificationService.showSuccessMessage('The cv was successfully detached', 'Success');
      },
      (error: Error) => {
        this.notificationService.showErrorMessage(error.message,
          'An error occured while detaching the cv');
      });
  }
}

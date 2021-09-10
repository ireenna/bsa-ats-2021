import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { openFileFromUrl } from 'src/app/shared/helpers/openFileFromUrl';
import { CvFileInfo } from 'src/app/shared/models/file/cvFileInfo';
import { ApplicantsService } from 'src/app/shared/services/applicants.service';
import { CvService } from 'src/app/shared/services/cv.service';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  selector: 'app-cv-list',
  templateUrl: './cv-list.component.html',
  styleUrls: ['./cv-list.component.scss'],
})
export class CvListComponent implements OnInit, OnDestroy {
  public cvFiles!: CvFileInfo[];
  private unsubscribe$ = new Subject<void>();

  constructor(
    private readonly cvService: CvService,
    private readonly applicantsService: ApplicantsService,
    @Inject(MAT_DIALOG_DATA) public applicantId: string,
    private readonly notificationsService: NotificationService,
  ) { }

  ngOnInit(): void {
    this.applicantsService
      .getCv(this.applicantId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        (cvFiles: CvFileInfo[]) => {
          this.cvFiles = cvFiles;
        },
        (error: Error) => {
          this.notificationsService.showErrorMessage(
            error.message,
            'Cannot download cvs',
          );
        },
      );
  }

  public openCv(url: string): void {
    openFileFromUrl(url);
  }

  public deleteCv(cvFileId: string): void {
    console.log(cvFileId);
    this.cvService.deleteCv(this.applicantId, cvFileId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        const cvIndex = this.cvFiles.findIndex(f => f.id == cvFileId);
        this.cvFiles.splice(cvIndex, 1);

        this.notificationsService.showInfoMessage('The cv was successfully removed',
          'Success');
      },
      (error: Error) => {
        this.notificationsService.showErrorMessage(error.message,
          'An error occurred while deleting a cv');
      });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}

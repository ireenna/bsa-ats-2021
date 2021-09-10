import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CvFileInfo } from '../../models/file/cvFileInfo';
import { CandidateCvService } from '../../services/candidate-cv.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-select-cv',
  templateUrl: './select-cv.component.html',
  styleUrls: ['./select-cv.component.scss'],
})
export class SelectCvComponent implements OnInit, OnDestroy {
  public candidateId!: string;
  public cvFiles!: CvFileInfo[]

  private readonly unsubscribe$ = new Subject<void>();
  
  constructor(
  @Inject(MAT_DIALOG_DATA) candidateId: string,
    private readonly candidateCvService: CandidateCvService,
    private readonly dialogRef: MatDialogRef<SelectCvComponent>,
    private readonly notificationService: NotificationService) {
    this.candidateId = candidateId;
  }

  ngOnInit(): void {
    this.candidateCvService.getAllowedCvs(this.candidateId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((cvFiles: CvFileInfo[]) => {
        this.cvFiles = cvFiles;
        console.log(cvFiles);
      },
      (error: Error) => (this.notificationService.showErrorMessage(error.message,
        'An error occurred while uploading existing cvs')),
      );
  }

  public attachCv(cvFileId: string): void {
    this.candidateCvService
      .attachCvForCandidate(this.candidateId, cvFileId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data: CvFileInfo) => {
        this.dialogRef.close(data);
      },
      (error: Error) => {
        this.notificationService.showErrorMessage(error.message,
          'An error occurred while attaching a cv file');
        this.dialogRef.close();
      });
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}

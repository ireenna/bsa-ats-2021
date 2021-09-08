import { Component, Inject, Input, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FileType } from '../../enums/file-type.enum';
import { CvFileInfo } from '../../models/file/cvFileInfo';
import { CandidateCvService } from '../../services/candidate-cv.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-upload-cv',
  templateUrl: './upload-cv.component.html',
  styleUrls: ['./upload-cv.component.scss'],
})
export class UploadCvComponent implements OnDestroy {
  public cvFileType = FileType.Pdf;
  public cvFile!: File;
  public candidateId!: string;

  private readonly unsubscribe$ = new Subject<void>();
  
  constructor(
  @Inject(MAT_DIALOG_DATA) candidateId: string,
    private readonly candidateCvService: CandidateCvService,
    private readonly dialogRef: MatDialogRef<UploadCvComponent>,
    private readonly notificationService: NotificationService) {
    this.candidateId = candidateId;
  }

  public uploadCv(): void {
    this.candidateCvService
      .uploadCvForCandidate(this.candidateId, this.cvFile)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data: CvFileInfo) => {
        console.log(data);
        this.dialogRef.close(data);
      },
      (error: Error) => {
        this.notificationService.showErrorMessage(error.message,
          'An error occured while uploading a cv file');
        this.dialogRef.close();
      });
  }

  public addFile(files: File[]): void {
    this.cvFile = files[0];
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}

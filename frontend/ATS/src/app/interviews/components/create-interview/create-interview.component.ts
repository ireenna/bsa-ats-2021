import { InterviewsService } from './../../services/interviews.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ValidatorFn,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { Tag } from 'src/app/shared/models/tags/tag';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Interview } from '../../models/interview.model';
import { CreateInterview } from '../../models/create-interview.model';

@Component({
  selector: 'app-create-interview',
  templateUrl: './create-interview.component.html',
  styleUrls: ['./create-interview.component.scss'],
})
export class CreateInterviewComponent implements OnDestroy {
  technologies = new FormControl();

  interview: CreateInterview = new CreateInterview();

  urlRegEx: string = '(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';

  interviewCreateForm = new FormGroup({
    'title': new FormControl(this.interview.title,
      [Validators.required,
        Validators.minLength(3),
        Validators.maxLength(15)]),
    'meetingLink': new FormControl(this.interview.meetingLink,
      [Validators.required]),
    'note': new FormControl(this.interview.note,
      [Validators.required,
        Validators.minLength(10)]),
    'teamInfo': new FormControl(this.interview.interviewType,
      [Validators.required,
        Validators.minLength(10)]),
    'duration': new FormControl(this.interview.duration,
      [Validators.required,
        URLValidator()]),
    'userParticipants':new FormControl(this.interview.userParticipants,[]),
  });

  public loading: boolean = false;

  private readonly unsubscribe$: Subject<void> = new Subject<void>();

  constructor(
    private interviewService: InterviewsService,
    private notificationService: NotificationService,
    private dialogRef: MatDialogRef<CreateInterviewComponent>,
  ) {
    this.dialogRef.disableClose = true;
    this.dialogRef.backdropClick().subscribe((_) => this.onFormClose());
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onFormClose() {
    if (this.interviewCreateForm.dirty) {
      if (confirm('Make sure you are saved everything. Continue?')) {
        this.dialogRef.close();
      }
    } else {
      this.dialogRef.close();
    }
  }

  public onSubmited() {
    this.interview = this.interviewCreateForm.value;
    this.loading = true;

    this.interviewService
      .createInterview(this.interview)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        () => {
          this.loading = false;

          this.notificationService.showSuccessMessage(
            `Project ${this.interview.title} created!`,
          );
        },
        (error: Error) => {
          this.loading = false;
          this.notificationService.showErrorMessage(error.message);
        },
      );

    this.dialogRef.close();
  }
  
  public updateTags(tags: Tag[]): void {
    // this.interview.tags.tagDtos = tags;
  }
}

function URLValidator(): ValidatorFn {
  let emailRe: RegExp = new RegExp(
    '(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?',
  );
  return (control: AbstractControl): ValidationErrors | null => {
    const valid = emailRe.test(control.value);
    return valid ? null : { url: { value: control.value } };
  };
}

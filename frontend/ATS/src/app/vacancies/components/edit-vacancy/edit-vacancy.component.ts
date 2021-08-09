import { Component, Inject, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-edit-vacancy',
  templateUrl: './edit-vacancy.component.html',
  styleUrls: ['./edit-vacancy.component.scss'],
})
export class EditVacancyComponent {

  vacancyForm!: FormGroup;
  isOpenCreateStage : Boolean = false;
  submitted:Boolean = false;
  
  constructor(
    public dialogRef: MatDialogRef<EditVacancyComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {title: string, description:string},
    private fb: FormBuilder) {
    this.vacancyForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(20), Validators.minLength(2)]],
      description: [''],
      requirements: [''],
      projectId:[''],
      salaryRang:[''],
      tierRang:[''],
      link:[''],
      isHot:[''],
      isRemote:[''],
    },
    );
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  createVacancy(){
    console.log(this.vacancyForm);
    console.log(this.vacancyForm.value);
    this.submitted = true;
    // this.dialogRef.close();
  }

  get vacancyFormControl() {
    return this.vacancyForm.controls;
  }

  displayCreateStage(){
    this.isOpenCreateStage = !this.isOpenCreateStage;
  }

  get registerFormControl() {
    return this.vacancyForm.controls;
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
    if(changes.vacancy && this.vacancyForm){
      this.vacancyForm.get('name')?.setValue(this.data.title);
      this.vacancyForm.get('description')?.setValue(this.data.description);
    }
  }

}

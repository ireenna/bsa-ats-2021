import { Component, Inject, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-edit-vacancy',
  templateUrl: './edit-vacancy.component.html',
  styleUrls: ['./edit-vacancy.component.scss']
})
export class EditVacancyComponent {

  vacancyForm!: FormGroup;
  isOpenCreateStage : Boolean = false;
  
  constructor(
    public dialogRef: MatDialogRef<EditVacancyComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {title: string, description:string},
    private fb: FormBuilder) {
      this.vacancyForm = this.fb.group({
        name: ['', [Validators.required, Validators.maxLength(20), Validators.minLength(2)]],
        description: ['']
      }
      );
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  displayCreateStage(){
    this.isOpenCreateStage = !this.isOpenCreateStage;
  }

  get registerFormControl() {
    return this.vacancyForm.controls;
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes)
    if(changes.vacancy && this.vacancyForm){
        this.vacancyForm.get('name')?.setValue(this.data.title)
        this.vacancyForm.get('description')?.setValue(this.data.description)
    }
}

}

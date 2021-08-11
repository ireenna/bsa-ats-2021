import { Component, Inject, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelect } from '@angular/material/select';
import { ReplaySubject, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { Project } from 'src/app/shared/models/projects/project';

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
      title: ['', [Validators.required]],
      description: ['', [Validators.required]],
      requirements: ['',[Validators.required]],
      projectId:['', [Validators.required]],
      salaryFrom:['', [Validators.required]],
      salaryTo:['', [Validators.required]],
      tierFrom:['', [Validators.required]],
      tierTo:['', [Validators.required]],
      link:['', [Validators.required]],
      isHot:[''],
      isRemote:[''],
    }, {validator: this.customValidationFunction}
    );
  }
  customValidationFunction(formGroup: FormGroup): any {
    let salaryFrom = formGroup.controls['salaryFrom'].value;
    let salaryTo = formGroup.controls['salaryTo'].value;
    // console.log((parseInt(salaryFrom,10) > parseInt(salaryTo,10)) ? { salaryRangeIsWrong: true } : null); 
    // this.vacancyFormControl.salaryTo.errors.Set({ salaryRangeIsWrong: true });
    return (parseInt(salaryFrom,10) > parseInt(salaryTo,10) ? { salaryRangeIsWrong: true } : null);
 }

  private projects: Project[] = [
    {name: 'Bank A (Switzerland)', id: 'A'},
    {name: 'Bank B (Switzerland)', id: 'B'},
    {name: 'Bank C (France)', id: 'C'},
    {name: 'Bank D (France)', id: 'D'},
    {name: 'Bank E (France)', id: 'E'},
    {name: 'Bank F (Italy)', id: 'F'},
    {name: 'Bank G (Italy)', id: 'G'},
    {name: 'Bank H (Italy)', id: 'H'},
    {name: 'Bank I (Italy)', id: 'I'},
    {name: 'Bank J (Italy)', id: 'J'},
    {name: 'Bank K (Italy)', id: 'K'},
    {name: 'Bank L (Germany)', id: 'L'},
    {name: 'Bank M (Germany)', id: 'M'},
    {name: 'Bank N (Germany)', id: 'N'},
    {name: 'Bank O (Germany)', id: 'O'},
    {name: 'Bank P (Germany)', id: 'P'},
    {name: 'Bank Q (Germany)', id: 'Q'},
    {name: 'Bank R (Germany)', id: 'R'} 
  ]

  onNoClick(): void {
    this.dialogRef.close();
  }

  createVacancy(){
    console.log(this.vacancyForm);
    console.log(this.vacancyForm.value);
    console.log(this.vacancyForm.errors?.salaryRangeIsWrong);
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

// Initially fill the selectedStates so it can be used in the for loop** 
selectedProjects = this.projects; 

// Receive user input and send to search method**
onKey(event:Event) { 
this.selectedProjects = this.search((<HTMLInputElement>event.target).value);
}

// Filter the states list and send back to populate the selectedStates**
search(value: string) { 
  let filter = value.toLowerCase();
  return this.projects.filter(option => option.name.toLowerCase().startsWith(filter));
}
}
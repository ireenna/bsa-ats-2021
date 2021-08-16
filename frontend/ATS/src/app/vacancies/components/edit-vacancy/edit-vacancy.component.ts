import { Component, EventEmitter, Inject, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelect } from '@angular/material/select';
import { ReplaySubject, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { Project } from 'src/app/shared/models/projects/project';
import { Stage } from 'src/app/shared/models/stages/stage';
import { VacancyCreate } from 'src/app/shared/models/vacancy/vacancy-create';
import { VacancyFull } from 'src/app/shared/models/vacancy/vacancy-full';
import { ProjectService } from 'src/app/shared/services/project.service';
import { VacancyService } from 'src/app/shared/services/vacancy.service';

@Component({
  selector: 'app-edit-vacancy',
  templateUrl: './edit-vacancy.component.html',
  styleUrls: ['./edit-vacancy.component.scss'],
})
export class EditVacancyComponent implements OnInit {

  vacancyForm!: FormGroup;
  isOpenCreateStage : Boolean = false;
  submitted:Boolean = false;
  selectedProjects:Project[] = []; 
  vacancy:VacancyCreate = {} as VacancyCreate;
  @Output() vacancyChange = new EventEmitter<VacancyFull>();
  
  constructor(
    public dialogRef: MatDialogRef<EditVacancyComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {title: string, description:string},
    private fb: FormBuilder,
    public projectService: ProjectService,
    public vacancyService:VacancyService) {
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
      stages:this.stageList
    }, {validator: this.customValidationFunction}
    );
    
  }

  ngOnInit(){
    this.projectService.getByCompany().subscribe(
      response=>{
        console.log(response)
        this.projects = response;
        this.selectedProjects = this.projects; 
        console.log(this.projects)
      })
  }

  stageList:Stage[]=[
    {
      name: "Test",
      index:1,
      isReviewable:true,
      vacancyId:"1"
    },
    {
      name: "Interview",
      index:2,
      isReviewable:true,
      vacancyId:"2"
    },
    {
      name: "Technical test",
      index:3,
      isReviewable:true,
      vacancyId:"1"
    }
  ]
  customValidationFunction(formGroup: FormGroup): any {
    let salaryFrom = formGroup.controls['salaryFrom'].value;
    let salaryTo = formGroup.controls['salaryTo'].value;
    return (parseInt(salaryFrom,10) > parseInt(salaryTo,10) ? { salaryRangeIsWrong: true } : null);
 }

 isTierFromLessTierTo(tierTo:Number):Boolean{
    let tierFrom = parseInt(this.vacancyForm.controls['tierFrom'].value,10)
    if(tierFrom <= tierTo){
      return true;
    }
    this.vacancyForm.controls['tierTo'].reset;
    return false;
 }

  private projects: Project[] = [];

  onNoClick(): void {
    this.dialogRef.close();
  }

  createVacancy(){
    console.log(this.vacancyForm);
    console.log(this.vacancyForm.value);
    console.log(this.vacancyForm.errors?.salaryRangeIsWrong);
    this.submitted = true;
    this.vacancy = {
      title:this.vacancyForm.controls['title'].value,
      description:this.vacancyForm.controls['description'].value,
      requirements:this.vacancyForm.controls['requirements'].value,
      projectId:this.vacancyForm.controls['projectId'].value,
      salaryFrom:parseInt(this.vacancyForm.controls['salaryFrom'].value,10),
      salaryTo:parseInt(this.vacancyForm.controls['salaryTo'].value,10),
      tierFrom:parseInt(this.vacancyForm.controls['tierFrom'].value,10),
      tierTo:parseInt(this.vacancyForm.controls['tierTo'].value,10),
      sources:this.vacancyForm.controls['link'].value,
      isHot:this.vacancyForm.controls['isHot'].value ? true : false,
      isRemote:this.vacancyForm.controls['isRemote'].value ? true : false,
      stages: [],
      responsibleHrId: '0affa701-db72-4fa6-b644-ef17229d5579',
      companyId: '1'
    }
    console.log(this.vacancy)
    this.vacancyService.postVacancy(this.vacancy)
    .subscribe(
      response=> this.vacancyChange.emit(response)
    );
    // this.dialogRef.close();
  }

  get vacancyFormControl() {
    return this.vacancyForm.controls;
  }

  saveStage(newStage:Stage){
    newStage.index = this.stageList.length + 1;
    this.stageList.push(newStage);
    console.log(this.stageList);
    this.displayCreateStage();
  }

  saveStageAndAdd(newStage:Stage){
    newStage.index = this.stageList.length + 1;
    this.stageList.push(newStage);
    console.log(this.stageList)
  }

  onDeleteStage(selectedStage:Stage){
    let id  = this.stageList.findIndex((a)=>a.index == selectedStage.index)
    this.stageList.splice(id, 1);
    console.log(this.stageList)
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

onKey(event:Event) { 
  this.selectedProjects = this.search((<HTMLInputElement>event.target).value);
}

// Filter the states list and send back to populate the selectedStates**
search(value: string) { 
  let filter = value.toLowerCase();
  return this.projects.filter(option => option.name.toLowerCase().startsWith(filter));
}
}
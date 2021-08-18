import { Component, EventEmitter, Inject, Input, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { MatSelect } from '@angular/material/select';
import { ReplaySubject, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { Project } from 'src/app/shared/models/projects/project';
import { Stage } from 'src/app/shared/models/stages/stage';
import { VacancyCreate } from 'src/app/shared/models/vacancy/vacancy-create';
import { VacancyFull } from 'src/app/shared/models/vacancy/vacancy-full';
import { ProjectService } from 'src/app/shared/services/project.service';
import { VacancyService } from 'src/app/shared/services/vacancy.service';
import { Tag } from 'src/app/users/models/tag';
import { MatChipInputEvent } from '@angular/material/chips';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { StageComponent } from '../stages/stage/stage.component';
import { VacancyData } from 'src/app/shared/models/vacancy/vacancy-data';

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
  stageToEdit:Stage = {} as Stage;
  isEditStageMode:Boolean = false;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  tags: Tag[] = [
    {name: 'Devops'},
    {name: 'Ukraine'},
    {name: 'Job offer'},
  ];

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our fruit
    if (value) {
      this.tags.push({name: value});
    }

    // Clear the input value
    event.chipInput!.clear();
  }

  remove(tag: Tag): void {
    const index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
  }
  
  constructor(
    public dialogRef: MatDialogRef<EditVacancyComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {vacancyToEdit: VacancyData},
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
      tags:this.tags,
      stages:this.stageList
    }, {validator: this.customValidationFunction}
    );
    if(data.vacancyToEdit && this.vacancyForm){
      this.vacancyService.getById(data.vacancyToEdit.id).subscribe(
        response=>{
          let vacancyToEdit:VacancyFull = response;
          this.vacancyForm.get('title')?.setValue(vacancyToEdit.title);
      this.vacancyForm.get('description')?.setValue(vacancyToEdit.description);
      this.vacancyForm.get('requirements')?.setValue(vacancyToEdit.requirements);
      this.vacancyForm.get('projectId')?.setValue(vacancyToEdit.projectId);
      this.vacancyForm.get('salaryFrom')?.setValue(vacancyToEdit.salaryFrom);
      this.vacancyForm.get('salaryTo')?.setValue(vacancyToEdit.salaryTo);
      this.vacancyForm.get('tierFrom')?.setValue(vacancyToEdit.tierFrom);
      this.vacancyForm.get('tierTo')?.setValue(vacancyToEdit.tierTo);
      this.vacancyForm.get('link')?.setValue(vacancyToEdit.sources);
      this.vacancyForm.get('isHot')?.setValue(vacancyToEdit.isHot);
      this.vacancyForm.get('isRemote')?.setValue(vacancyToEdit.isRemote);
      this.tags = vacancyToEdit.tags;
      this.stageList = vacancyToEdit.stages;
          this.selectedProjects = this.projects;
        })
      
    }
  }
  // drop(event: CdkDragDrop<StageComponent[]>) {
  //   moveItemInArray(this.stageList, event.previousIndex, event.currentIndex);
  // }
  
  // tslint:enable:max-line-length

  drop(event: CdkDragDrop<Stage[]>) {
    moveItemInArray(this.stageList, event.previousIndex, event.currentIndex);
  }

  ngOnInit(){
    this.projectService.getByCompany().subscribe(
      response=>{
        this.projects = response;
        this.selectedProjects = this.projects;
      })
  }

  stageList:Stage[]=[
    {
      id:"aaaa",
      name: "Test",
      index:2,
      action:"Prepare questions for interview",
      rates:"English",
      isReviewRequired:true,
      vacancyId:"1"
    },
    {
      id:"bbbb",
      name: "Interview",
      index:1,
      action:"Prepare questions for interview",
      rates:"English",
      isReviewRequired:true,
      vacancyId:"2"
    },
    {id:"ccccc",
      name: "Technical test",
      index:3,
      action:"Prepare questions for interview",
      rates:"English",
      isReviewRequired:true,
      vacancyId:"1"
    },
    {
      id:"bbbb",
      name: "Interview",
      index:4,
      action:"Prepare questions for interview",
      rates:"English",
      isReviewRequired:true,
      vacancyId:"2"
    },
    {id:"ddddd",
      name: "5",
      index:5,
      action:"Prepare questions for interview",
      rates:"English",
      isReviewRequired:true,
      vacancyId:"1"
    },
    {
      id:"bbbb",
      name: "Interview",
      index:6,
      action:"Prepare questions for interview",
      rates:"English",
      isReviewRequired:true,
      vacancyId:"2"
    }
  ]

  onEditStage(stageToEdit: Stage){
    this.stageToEdit = stageToEdit;
    this.isOpenCreateStage = true;
    this.isEditStageMode = true;
  }

  sortStageList(){
    // this.stageList.sort((a,b)=>{
    //   if(a.index>b.index)
    //     return 1
    //   if(a.index<b.index)
    //     return -1
    //   return 0;
    // });
    let index = 1;
    this.stageList.forEach(x=>{
      x.index = index;
      index++;
    })
    console.log(this.stageList)
    return this.stageList;
  }

  customValidationFunction(formGroup: FormGroup): any {
    let salaryFrom = formGroup.controls['salaryFrom'].value;
    let salaryTo = formGroup.controls['salaryTo'].value;
    let error = (parseInt(salaryFrom,10) > parseInt(salaryTo,10));
    if(error){
      formGroup.controls['salaryTo'].setErrors({ salaryRangeIsWrong: true });
      console.log(this.vacancyForm);
    }
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

  toSave(newStage:Stage){
    if(this.isEditStageMode){
      let stageIndex = this.stageList.find(x=>x.index === newStage.index)?.index;
      if(stageIndex){
        this.stageList[stageIndex-1] = newStage;
      }
      this.isEditStageMode = false;
    }else{
      newStage.index = this.stageList.length + 1;
      this.stageList.push(newStage);
    }
    this.stageToEdit = {} as Stage;
  }

  saveStage(newStage:Stage){
    this.toSave(newStage);
      this.displayCreateStage();
  }

  cancelStageEdit(){
  this.stageToEdit = {} as Stage;
  this.displayCreateStage();
  }

  saveStageAndAdd(newStage:Stage){
   this.toSave(newStage)
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

  // ngOnChanges(changes: SimpleChanges): void {
  //   console.log(changes);
    
  //   }
  // }

  // onDragStart(event:Event) {
  //   event
  //     .dataTransfer
  //     .setData('text/plain', event.target.id);
   
  //   event
  //     .currentTarget
  //     .style
  //     .backgroundColor = 'yellow';
  // }

onKey(event:Event) { 
  this.selectedProjects = this.search((<HTMLInputElement>event.target).value);
}

// Filter the states list and send back to populate the selectedStates**
search(value: string) { 
  let filter = value.toLowerCase();
  return this.projects.filter(option => option.name.toLowerCase().startsWith(filter));
}

// items=[0,1,2,3,4,5,6,7,8,9,10,11]
  // option="25rem"
  dropp(event: CdkDragDrop<any>) {
    this.stageList[event.previousContainer.data.index]=event.container.data.item
    this.stageList[event.container.data.index]=event.previousContainer.data.item
  }
}


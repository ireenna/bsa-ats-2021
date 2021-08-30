import { Component, OnInit, ViewChild } from '@angular/core';
import { Task } from 'src/app/shared/models/task-management/task';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { MatDialog } from '@angular/material/dialog';
import { AllInOneComponent} from '../modal/all-in-one/all-in-one.component';

interface filterObject {
  userFilter:boolean;
  isDoneFilter: boolean;
}

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})

export class MainPageComponent implements OnInit{
  public doneFilter: boolean = false;
  public isDone : boolean = false;
  public tasks : Task[] = [];
  public filter : filterObject = {userFilter:false, isDoneFilter:false} as filterObject;
  public filterUser : string = 'All';
  public currentUserId : string = '';
  

  public allTasks : Task[] =  [
    { id: '1', name: 'Interview', dueDate : new Date(), isDone:true,
      createdDate: new Date('2021-08-24 09:05'), 
      createdBy : {id: '1', firstName: 'Lion', lastName: 'King' },
      teamMembers : [
        {id: '1', firstName: 'Gerry', lastName: 'Long' },
        {id: '2', firstName: 'Max', lastName: 'Shure' },
      ],
      applicant: {id:'1',firstName:'Sam', lastName:'White',image: ''},
    },
    {id:'2',name:'Review candidates', dueDate : new Date(), isDone: false, 
      createdDate: new Date('2021-08-01 20:15'), 
      createdBy : {id: '2', firstName: 'Miracle', lastName: 'Madson' },
      teamMembers : [
        {id: '1', firstName: 'Gerry', lastName: 'Long' },
        {id: '2', firstName: 'Max', lastName: 'Shure' },
        {id: '3', firstName: 'Joe', lastName: 'Silver' },
      ],
      applicant: {id:'1',firstName:'Jack', lastName:'Notch',image: ''},
    },
    {id:'2',name:'Test', dueDate : new Date(), isDone: true, 
      createdDate: new Date('2021-08-12 09:05'), 
      createdBy : {id: '2', firstName: 'Miracle', lastName: 'Madson' },
      teamMembers : [        
        {id: '3', firstName: 'Joe', lastName: 'Silver' },
      ],
      applicant: {id:'1',firstName:'Vail', lastName:'Blind',image: ''},
    },
    {id:'2',name:'Test', dueDate : new Date(), isDone: true, 
      createdDate: new Date('2021-08-17 19:10'), 
      createdBy : {id: '3', firstName: 'Stacy', lastName: 'Dru' },
      teamMembers : [        
        {id: '3', firstName: 'Joe', lastName: 'Silver' },
      ],
      applicant: {id:'1',firstName:'Vail', lastName:'Blind',image: ''},
    },
    {id:'2',name:'Technical interview',dueDate : new Date(), isDone: true, 
      createdDate: new Date('2021-07-29 15:01'), 
      createdBy : {id: '2', firstName: 'Miracle', lastName: 'Madson' },
      teamMembers : [        
        {id: '3', firstName: 'Joe', lastName: 'Silver' },
        {id: '5', firstName: 'Joe', lastName: 'Silver' },
        {id: '2', firstName: 'Joe', lastName: 'Silver' },
      ],
      applicant: {id:'1',firstName:'Vail', lastName:'Blind',image: ''},
    },
    {id:'2',name:'Review candidates', dueDate : new Date(), isDone: false, 
      createdDate: new Date('2021-08-22 10:35'),
      createdBy : {id: '1', firstName: 'Jephry', lastName: 'Hanna' },
      teamMembers : [        
        {id: '3', firstName: 'Joe', lastName: 'Silver' },
      ],
      applicant: {id:'1',firstName:'Vail', lastName:'Blind',image: ''},
    },
    {id:'2',name:'Phone call', dueDate : new Date(), isDone: false, 
      createdDate: new Date('2021-08-14 16:40'), 
      createdBy : {id: '5', firstName: 'Piter', lastName: 'Double' },
      teamMembers : [        
        {id: '3', firstName: 'Joe', lastName: 'Silver' },
      ],
      applicant: {id:'1',firstName:'Otto', lastName:'Shmith',image: ''},
    },
  ];

  constructor (
    private readonly dialogService: MatDialog,     
    private notificationService: NotificationService,
  ) { }

  ngOnInit() : void {
    console.log('tasks loaded');
    this.filterData();    
  }

  toggleDone(isDone: boolean) {
    this.filter.isDoneFilter = isDone;
    this.filterData();
  }

  filterData()
  {
    this.tasks = this.allTasks.filter(x=> 
      x.isDone == this.filter.isDoneFilter && 
      (this.currentUserId == '' || this.filter.userFilter == false ||
        (this.filter.userFilter && x.createdBy.id == this.currentUserId)
      ));    
  }

  toggleUser(value:string) {
    //binding doesn't work correctly on boolean (
    this.filterUser = value == 'Me' ? 'Me': 'All';
    this.filter.userFilter = value == 'Me' ? true: false;

    if(this.currentUserId =='') 
    {
      if(this.decodeJwt())
      {
        this.filterData();
      }           
    }
    else this.filterData();
  }

  decodeJwt ()  {    
    let result = false;
    const token = localStorage.getItem('accessToken');
    if(token) {
      try {
        let data = JSON.parse(atob(token.split('.')[1]));
        this.currentUserId = data.id;
        result = true;
      }
      catch {
      }      
    }
    return result;
  }

  createTask() {
    let createDialog = this.dialogService.open(AllInOneComponent, {
      width: '600px',      
    });

    createDialog.afterClosed().subscribe((result) => {});

    // const dialogSubmitSubscription = 
    // createDialog.componentInstance.submitClicked.subscribe(result => {
    //   this.createPool(result);      
    //   dialogSubmitSubscription.unsubscribe();
    // });
  }

  editTask(task: Task) {
    let editDialog = this.dialogService.open(AllInOneComponent, {
      width: '600px',
      data: task,
    });

    editDialog.afterClosed().subscribe((result) => {this.filterData();});

    // const dialogSubmitSubscription =
    //   editDialog.componentInstance.submitClicked.subscribe((result) => {
    //     this.updatePool(result);
    //     dialogSubmitSubscription.unsubscribe();
    //   });
  }
}
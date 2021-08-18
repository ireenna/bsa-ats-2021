import { AfterViewInit, Component,
  ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { StylePaginatorDirective } from 'src/app/shared/directives/style-paginator.directive';
import { VacancyStatus } from 'src/app/shared/models/vacancy/vacancy-status';
import { VacancyData } from 'src/app/shared/models/vacancy/vacancy-data';
import { VacancyDataService } from 'src/app/shared/services/vacancy-data.service';
import { Router } from '@angular/router';
import { EditVacancyComponent } from '../edit-vacancy/edit-vacancy.component';
import { User } from 'src/app/users/models/user';

​
const HRs: User[] = [
  {
  firstName: "L",
  lastName: "M",
  birthDate: new Date,
  creationDate: new Date,
  email: "mail@gmail.com",
  isEmailConfirmed: true
  },
  {
    firstName: "I",
    lastName: "K",
    birthDate: new Date,
    creationDate: new Date,
    email:"email@gmail.com",
    isEmailConfirmed: true
    }
];
const NAMES: string[] = [
  'Interface Designer', 'Software Enginner', 'Project Manager', 'Developer', 'QA',
];

const STATUES: VacancyStatus[] = [
  VacancyStatus.Active,
  VacancyStatus.Former,
  VacancyStatus.Invited,
  VacancyStatus.Vacation,
];
​

@Component({
  selector: 'app-vacancies-table',
  templateUrl: './vacancies-table.component.html',
  styleUrls: ['./vacancies-table.component.scss'],
})
​
export class VacanciesTableComponent implements AfterViewInit {
  displayedColumns: string[] =
  ['position', 'title', 'candidates', 'teamInfo',
    'responsible', 'creationDate', 'status', 'actions'];
  dataSource: MatTableDataSource<VacancyData>;
​
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(StylePaginatorDirective) directive!: StylePaginatorDirective;
  @ViewChild('input') serachField!: ElementRef;
  private randomRequiredCandidatesAmounts: number[] = [60, 30, 6, 29, 44, 34, 55, 30, 6, 32];
  private randomCurrentApplicantsAmounts: number[] = [130, 34, 56, 34];
  constructor(private router:Router, private cd: ChangeDetectorRef,
    private dialog: MatDialog, private service: VacancyDataService) {
    const data =  Array.from({ length: 99 }, (_, k) => createNewVacancy());
    // service.getList().subscribe(data=>{
    //   data.forEach(d=>{
    //     d.requiredCandidatesAmount = this.randomRequiredCandidatesAmounts[
    //       Math.round(Math.random() * (this.randomRequiredCandidatesAmounts.length - 1))
    //     ];
    //     d.currentApplicantsAmount = this.randomCurrentApplicantsAmounts[
    //       Math.round(Math.random() * (this.randomCurrentApplicantsAmounts.length - 1))
    //     ];
    //   });
    this.dataSource = new MatTableDataSource<VacancyData>();
      this.dataSource.data = data;
      this.directive.applyFilter$.emit();
    // },
    // );
    // Assign the data to the data source for the table to render
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(EditVacancyComponent, {
      width: '914px',
      height: 'auto',
      data: {title: 'nameee', description:'desc'},
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
  }

  public getStatus(index:number): string{
    return VacancyStatus[index];
  }
  public toStagedReRoute(id:string){
    this.router.navigateByUrl(`candidates/${id}`);
  }
  public clearSearchField(){
    this.serachField.nativeElement.value = '';
    this.dataSource.filter = '';
    if (this.dataSource.paginator) {
      this.directive.applyFilter$.emit();
      this.dataSource.paginator.firstPage();
    }
  }
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
   
  }
  ​
​
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.directive.applyFilter$.emit();
      this.dataSource.paginator.firstPage();
    }
  }
​
 
}
​
/** Builds and returns a new User. */
function createNewVacancy(): VacancyData {
  const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))];
​
  return {
    title: name,
    requiredCandidatesAmount: Math.round(Math.random()*4+1),
    currentApplicantsAmount: Math.round(Math.random()*10 +1),
    responsibleHr: HRs[Math.round(Math.random() * (HRs.length - 1))],
    department: 'Lorem ipsum dorot sit',
    creationDate: new Date(),
    status: STATUES[Math.round(Math.random()*(STATUES.length - 1))],
  };
}
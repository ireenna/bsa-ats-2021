import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditVacancyComponent } from '../edit-vacancy/edit-vacancy.component';

@Component({
  selector: 'app-vacancies-list',
  templateUrl: './vacancies-list.component.html',
  styleUrls: ['./vacancies-list.component.scss'],
})
export class VacanciesListComponent{

  constructor(public dialog: MatDialog) { }
  openDialog(): void {
    const dialogRef = this.dialog.open(EditVacancyComponent, {
      width: '914px',
      height: 'auto',
      data: {title: 'nameee', description:'desc'}
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
  }

}

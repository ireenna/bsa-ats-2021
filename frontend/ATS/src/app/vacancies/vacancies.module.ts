import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { RoutingModule } from '../routing/routing.module';
import { SharedModule } from '../shared/shared.module';
import { CandidateCardComponent } from './components/candidate-card/candidate-card.component';
import { CandidateColumnComponent } from './components/candidate-column/candidate-column.component';

// This line can't be shorter
// eslint-disable-next-line max-len
import { VacanciesStagesBoardComponent } from './components/vacancies-stages-board/vacancies-stages-board.component';

import { VacanciesListComponent } from './components/vacancies-list/vacancies-list.component';
import { EditVacancyComponent } from './components/edit-vacancy/edit-vacancy.component';
import { StageComponent } from './components/stages/stage/stage.component';
import { CreateStageComponent } from './components/stages/create-stage/create-stage.component';

@NgModule({
  declarations: [
    CandidateCardComponent,
    CandidateColumnComponent,
    VacanciesStagesBoardComponent,
    VacanciesListComponent,
    EditVacancyComponent,
    StageComponent,
    CreateStageComponent,
  ],
  imports: [
    CommonModule,
    RoutingModule,
    MatIconModule,
    MatButtonModule,
    MatTabsModule,
    DragDropModule,
    SharedModule,
  ],
  exports: [
    CandidateCardComponent,
    CandidateColumnComponent,
    VacanciesStagesBoardComponent,
    VacanciesListComponent,
  ],
})
export class VacanciesModule {}

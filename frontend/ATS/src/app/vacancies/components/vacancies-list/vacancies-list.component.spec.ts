import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacanciesListComponent } from './vacancies-list.component';

describe('VacanciesListComponent', () => {
  let component: VacanciesListComponent;
  let fixture: ComponentFixture<VacanciesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacanciesListComponent ],
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacanciesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

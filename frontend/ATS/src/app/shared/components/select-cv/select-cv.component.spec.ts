import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectCvComponent } from './select-cv.component';

describe('SelectCvComponent', () => {
  let component: SelectCvComponent;
  let fixture: ComponentFixture<SelectCvComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectCvComponent ],
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectCvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

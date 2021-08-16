import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Stage } from 'src/app/shared/models/stages/stage';

@Component({
  selector: 'app-create-stage',
  templateUrl: './create-stage.component.html',
  styleUrls: ['./create-stage.component.scss']
})
export class CreateStageComponent implements OnInit {

  submitted:Boolean = false;
  @Output() stageChange = new EventEmitter<Stage>();
  @Output() stageCreateAndAddChange = new EventEmitter<Stage>();
  @Output() isClosedChange = new EventEmitter<Boolean>();
  stage:Stage = {} as Stage;
  
  constructor(private fb: FormBuilder) {
    this.stageForm = this.fb.group({
      name: ['', [Validators.required]],
      action: ['', [Validators.required]],
      isReviewRequired: [''],
      rates: ['', [Validators.required]]
    }
    );
  }

  onStageSave(){
    this.submitted = true;
    // if(this.stageForm.valid){
      this.stage = this.stageForm.value;
      this.stageChange.emit(this.stage);
    // }
      
  }
  onSaveAndAdd(){
    this.stage = this.stageForm.value;
    this.stageCreateAndAddChange.emit(this.stage);
    this.stageForm.reset();
  }

  onStageClose(){
    this.submitted = false;
    this.isClosedChange.emit(true);
  }

  get stageFormControl() {
    return this.stageForm.controls;
  }

  stageForm!: FormGroup;
  ngOnInit(): void {
  }

}

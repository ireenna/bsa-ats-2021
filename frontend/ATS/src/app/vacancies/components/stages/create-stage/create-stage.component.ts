import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-stage',
  templateUrl: './create-stage.component.html',
  styleUrls: ['./create-stage.component.scss']
})
export class CreateStageComponent implements OnInit {

  constructor(private fb: FormBuilder) {
    this.stageForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(20), Validators.minLength(2)]],
      description: ['']
    }
    );
  }
  stageForm!: FormGroup;
  ngOnInit(): void {
  }

}

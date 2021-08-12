import { Component, Input, OnInit } from '@angular/core';
import { Stage } from 'src/app/shared/models/stages/stage';

@Component({
  selector: 'app-stage',
  templateUrl: './stage.component.html',
  styleUrls: ['./stage.component.scss']
})
export class StageComponent implements OnInit {

  constructor() { }
  @Input() stage: Stage = {} as Stage;

  ngOnInit(): void {
  }

}

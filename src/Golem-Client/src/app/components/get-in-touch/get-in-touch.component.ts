import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-get-in-touch',
  templateUrl: './get-in-touch.component.html',
  styleUrls: ['./get-in-touch.component.scss']
})
export class GetInTouchComponent implements OnInit {
  public getInTouchForm: FormGroup;

  constructor() {
    this.getInTouchForm = new FormGroup({
      message: new FormControl(''),
      email: new FormControl(''),
      fullName: new FormControl('')
    });
  }

  ngOnInit(): void {
  }

}

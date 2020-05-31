import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-countries-chart',
  templateUrl: './countries-chart.component.html',
  styleUrls: ['./countries-chart.component.scss'],
})
export class CountriesChartComponent implements OnInit {
  public piedata: Object[];
  public legendSettings: Object;
  public dataLabel: Object;
  constructor() {}

  ngOnInit(): void {
    this.piedata = [
      { x: 'Jan', y: 3, text: 'Jan: 3' },
      { x: 'Feb', y: 3.5, text: 'Feb: 3.5' },
      { x: 'Mar', y: 7, text: 'Mar: 7' },
      { x: 'Apr', y: 13.5, text: 'Apr: 13.5' },
      { x: 'May', y: 19, text: 'May: 19' },
      { x: 'Jun', y: 23.5, text: 'Jun: 23.5' },
      { x: 'Jul', y: 26, text: 'Jul: 26' },
      { x: 'Aug', y: 25, text: 'Aug: 25' },
      { x: 'Sep', y: 21, text: 'Sep: 21' },
      { x: 'Oct', y: 15, text: 'Oct: 15' },
      { x: 'Nov', y: 9, text: 'Nov: 9' },
      { x: 'Dec', y: 3.5, text: 'Dec: 3.5' },
    ];

    this.legendSettings = {
      visible: true,
      position: 'Bottom',
      alignment: 'Center',
    };

    this.dataLabel = {
      visible: false,
    };
  }
}
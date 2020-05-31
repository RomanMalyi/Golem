import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-requests-chart',
  templateUrl: './requests-chart.component.html',
  styleUrls: ['./requests-chart.component.scss']
})
export class RequestsChartComponent implements OnInit {
  public primaryXAxis: Object;
  public primaryYAxis: Object;
  public legendSettings: Object;
  public chartData: Object[];
  public marker: Object;
  constructor() { }

  ngOnInit(): void {
    this.chartData = [
      { x: 'Egg', y: 2.2 },
      { x: 'Fish', y: 2.4 },
      { x: 'Misc', y: 3 },
      { x: 'Tea', y: 3.1 },
    ];
    this.primaryYAxis = {};
    this.primaryXAxis = {
      valueType: 'Category',
    };
    this.legendSettings = {
      visible: true,
    };
    this.marker = { visible: true, width: 10, height: 10 };
  }

}

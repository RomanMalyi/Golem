import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users-chart',
  templateUrl: './users-chart.component.html',
  styleUrls: ['./users-chart.component.scss'],
})
export class UsersChartComponent implements OnInit {
  public primaryXAxis: Object;
  public primaryYAxis: Object;
  public legendSettings: Object;
  public chartData: Object[];
  constructor() {}

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
  }
}

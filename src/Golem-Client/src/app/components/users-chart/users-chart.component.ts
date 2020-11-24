import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { SpinnerService } from 'src/app/services/spinner.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-users-chart',
  templateUrl: './users-chart.component.html',
  styleUrls: ['./users-chart.component.scss'],
})
export class UsersChartComponent implements OnInit {
  public primaryXAxis: Object;
  public primaryYAxis: Object;
  public legendSettings: Object;
  public chartUsersData: Object[];
  public chartSessionsData: Object[];
  constructor(
    private httpService: HttpService,
    private spinnerService: SpinnerService,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.primaryYAxis = {};
    this.primaryXAxis = {
      valueType: 'Category',
    };
    this.legendSettings = {
      visible: true,
    };
    this.loadChartInfo();
  }

  private loadChartInfo() {
    this.spinnerService.showSpinner();
    this.httpService.getUsersChartInfo().subscribe(
      (res) => {
        this.chartUsersData = [];
        this.chartSessionsData = [];
        res.forEach((elem) => {
          this.chartUsersData.push({
            x: this.datePipe.transform(elem.date, 'MM/dd'),
            y: elem.usersNumber,
          });
          this.chartSessionsData.push({
            x: this.datePipe.transform(elem.date, 'MM/dd'),
            y: elem.sessionsNumber,
          });
        });
        this.spinnerService.hideSpinner();
      },
      (error) => {
        console.log(error);
        this.spinnerService.hideSpinner();
      }
    );
  }
}

import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { SpinnerService } from 'src/app/services/spinner.service';
import { DatePipe } from '@angular/common';

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
  constructor(
    private httpService: HttpService,
    private spinnerService: SpinnerService,
    private datePipe: DatePipe
  ) { }
  ngOnInit(): void {
    this.primaryYAxis = {};
    this.primaryXAxis = {
      valueType: 'Category',
    };
    this.legendSettings = {
      visible: true,
    };
    this.marker = { visible: true, width: 10, height: 10 };
    this.loadRequests();
  }
  private loadRequests() {
    this.spinnerService.showSpinner();
    this.httpService.getRequestsChartInfo().subscribe(
      (res) => {
        this.chartData = [];
        res.forEach((elem) =>
          this.chartData.push({ x: this.datePipe.transform(elem.date, 'MM/dd'), y: elem.requestsNumber})
        );
        this.spinnerService.hideSpinner();
      },
      (error) => {
        console.log(error);
        this.spinnerService.hideSpinner();
      }
    );
  }
}

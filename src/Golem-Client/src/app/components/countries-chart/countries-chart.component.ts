import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { SpinnerService } from 'src/app/services/spinner.service';

@Component({
  selector: 'app-countries-chart',
  templateUrl: './countries-chart.component.html',
  styleUrls: ['./countries-chart.component.scss'],
})
export class CountriesChartComponent implements OnInit {
  public piedata: Object[];
  public legendSettings: Object;
  public dataLabel: Object;
  constructor(
    private httpService: HttpService,
    private spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.loadCountries();

    this.legendSettings = {
      visible: true,
      position: 'Bottom',
      alignment: 'Center',
    };

    this.dataLabel = {
      visible: false,
    };
  }

  private loadCountries() {
    this.spinnerService.showSpinner();
    this.httpService.getCountriesChartInfo().subscribe(
      (res) => {
        this.piedata = [];
        res.forEach((elem) =>
          this.piedata.push({ x: elem.name, y: elem.number, text: elem.name })
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

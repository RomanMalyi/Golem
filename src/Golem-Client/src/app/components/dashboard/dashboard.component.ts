import { Component, OnInit } from "@angular/core";
import { DashboardOverview } from "src/app/models/dashboardOverview";
import { HttpService } from "src/app/services/http.service";
import { SpinnerService } from "src/app/services/spinner.service";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {
  public overviewData: DashboardOverview;
  constructor(
    private httpService: HttpService,
    private spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  private loadUsers() {
    this.spinnerService.showSpinner();
    this.httpService.getDashboardOverview().subscribe(
      (res) => {
        this.overviewData = res;
        this.spinnerService.hideSpinner();
      },
      (error) => {
        console.log(error);
        this.spinnerService.hideSpinner();
      }
    );
  }
}

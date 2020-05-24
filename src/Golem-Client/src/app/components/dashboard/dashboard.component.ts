import { Component, OnInit } from '@angular/core';
import { DashboardOverview } from 'src/app/models/dashboardOverview';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  public overviewData: DashboardOverview;
  constructor(private httpService: HttpService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  private loadUsers() {
    this.httpService.getDashboardOverview().subscribe(
      (res) => {
        this.overviewData = res;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}

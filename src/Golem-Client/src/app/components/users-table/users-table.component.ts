import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { UserModel } from 'src/app/models/userModel';
import { MatPaginator } from '@angular/material/paginator';
import { SpinnerService } from 'src/app/services/spinner.service';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.scss'],
})
export class UsersTableComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  public skip = 0;
  public paginatorPageSize = 5;
  public totalUsersCount = 0;
  public displayedColumns: string[] = [
    'position',
    'lastVisitTime',
    'country',
    'numberOfVisits',
    'numberOfRequests',
  ];
  public users: UserModel[];
  public currentUser: UserModel;
  public showQueries = false;
  public showSessions = false;

  public dateFrom: Date;
  public dateTo: Date;

  constructor(
    private httpService: HttpService,
    private spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  public showUser(user: UserModel) {
    this.currentUser = user;
    this.showQueries = false;
    this.showSessions = false;
  }

  public toggleQueries() {
    this.showQueries = !this.showQueries;
  }

  public toggleSessions() {
    this.showSessions = !this.showSessions;
  }

  public getPagingUserData(event) {
    this.paginatorPageSize = event.pageSize;
    this.skip = event.pageIndex * event.pageSize;

    this.loadUsers();
  }

  public dateChange() {
    this.loadUsers();
  }

  private loadUsers() {
    this.spinnerService.showSpinner();
    this.httpService
      .getUsers(this.skip, this.dateFrom, this.dateTo, this.paginatorPageSize)
      .subscribe(
        (res) => {
          this.users = res.users;
          this.totalUsersCount = res.totalCount;
          if (this.users) {
            this.currentUser = this.users[0];
            this.showQueries = false;
          }
          this.spinnerService.hideSpinner();
        },
        (error) => {
          this.spinnerService.hideSpinner();
          console.log(error);
        }
      );
  }
}

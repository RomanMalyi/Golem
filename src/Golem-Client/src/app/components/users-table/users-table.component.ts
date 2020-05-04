import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { UserModel } from 'src/app/models/userModel';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.scss']
})
export class UsersTableComponent implements OnInit {
  public displayedColumns: string[] = ['position', 'id', 'numberOfVisits', 'actions'];
  public users: UserModel[];
  public userId: string;

  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.getUsers(0, 8);
  }

  public showUserQueries(userId: string) {
    this.userId = userId;
  }

  private getUsers(skip: number, take: number) {
    this.httpService.getUsers(skip, take).subscribe(
      (res) => {
        this.users = res;
      },
      (error) => {
        console.log(error);
      }
    );
  }

}

import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { HttpService } from 'src/app/services/http.service';
import { SessionModel } from 'src/app/models/sessionModel';

@Component({
  selector: 'app-sessions-table',
  templateUrl: './sessions-table.component.html',
  styleUrls: ['./sessions-table.component.scss']
})
export class SessionsTableComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  public skip = 0;
  public paginatorPageSize = 5;
  public totalSessionsCount = 0;

  @Input() public userId: string;
  public displayedColumns: string[] = ['position', 'startTime', 'endTime'];
  public sessions: SessionModel[];

  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.loadSessions();
  }

  public getPagingSessionData(event) {
    this.paginatorPageSize = event.pageSize;
    this.skip = event.pageIndex * event.pageSize;

    this.loadSessions();
  }

  private loadSessions() {
    this.httpService.getSessions(this.userId, this.skip, this.paginatorPageSize).subscribe(
      (res) => {
        this.sessions = res.sessions;
        this.totalSessionsCount = res.totalCount;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}

import { Component, OnInit, Input } from '@angular/core';
import { QueryModel } from 'src/app/models/queryModel';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-queries-table',
  templateUrl: './queries-table.component.html',
  styleUrls: ['./queries-table.component.scss']
})
export class QueriesTableComponent implements OnInit {
  @Input() public userId: string;
  public displayedColumns: string[] = ['position', 'queryString', 'methodType', 'creationDate'];
  public queries: QueryModel[];

  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.getQueries(this.userId, 0, 8);
  }

  private getQueries(userId: string, skip: number, take: number) {
    this.httpService.getQueries(userId, skip, take).subscribe(
      (res) => {
        this.queries = res;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}

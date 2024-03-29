import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { QueryModel } from 'src/app/models/queryModel';
import { HttpService } from 'src/app/services/http.service';
import { MatPaginator } from '@angular/material/paginator';
import { SpinnerService } from 'src/app/services/spinner.service';

@Component({
  selector: 'app-queries-table',
  templateUrl: './queries-table.component.html',
  styleUrls: ['./queries-table.component.scss'],
})
export class QueriesTableComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  public skip = 0;
  public paginatorPageSize = 5;
  public totalQueriesCount = 0;

  @Input() public userId: string;
  public displayedColumns: string[] = [
    'position',
    'path',
    'queryString',
    'methodType',
    'creationDate',
  ];
  public queries: QueryModel[];

  public dateFrom: Date;
  public dateTo: Date;
  public showEmpty = true;

  constructor(
    private httpService: HttpService,
    private spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.loadQueries();
  }

  public getPagingQuerieData(event) {
    this.paginatorPageSize = event.pageSize;
    this.skip = event.pageIndex * event.pageSize;

    this.loadQueries();
  }

  public filterChange() {
    this.loadQueries();
  }

  private loadQueries() {
    this.spinnerService.showSpinner();
    this.httpService
      .getQueries(
        this.userId,
        this.dateFrom,
        this.dateTo,
        this.showEmpty,
        this.skip,
        this.paginatorPageSize
      )
      .subscribe(
        (res) => {
          this.queries = res.queries;
          this.totalQueriesCount = res.totalCount;
          this.spinnerService.hideSpinner();
        },
        (error) => {
          this.spinnerService.hideSpinner();
          console.log(error);
        }
      );
  }
}

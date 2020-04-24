import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/models/project';
import { ElasticsearchService } from 'src/app/services/elasticsearch.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(private searchService: ElasticsearchService) {}

  public products: Project[];

  ngOnInit(): void {
    this.getProjects('');
  }

  public onKeyUp(event: any) {
    this.getProjects(event.target.value);
  }

  private getProjects(searchTerm: string) {
    this.searchService.search(searchTerm).subscribe(
      (res) => {
        this.products = res;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}

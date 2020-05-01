import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/models/project';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(private httpService: HttpService) {}

  public products: Project[];

  ngOnInit(): void {
    this.getProjects('');
  }

  public onKeyUp(event: any) {
    this.getProjects(event.target.value);
  }

  private getProjects(searchTerm: string) {
    this.httpService.searchProject(searchTerm).subscribe(
      (res) => {
        this.products = res;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}

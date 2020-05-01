import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public products: Project[];

  constructor(private httpService: HttpService) { }

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

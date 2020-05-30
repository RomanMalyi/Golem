import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { ProjectModel } from 'src/app/models/projectModel';
import { SpinnerService } from 'src/app/services/spinner.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public products: ProjectModel[];

  constructor(
    private httpService: HttpService,
    private spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.getProjects('');
  }

  public onKeyUp(event: any) {
    this.getProjects(event.target.value);
  }

  private getProjects(searchTerm: string) {
    this.spinnerService.showSpinner();
    this.httpService.searchProject(searchTerm).subscribe(
      (res) => {
        this.products = res;
        this.spinnerService.hideSpinner();
      },
      (error) => {
        console.log(error);
        this.spinnerService.hideSpinner();
      }
    );
  }
}

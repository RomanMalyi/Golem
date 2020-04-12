import { Component, OnInit } from '@angular/core';
import { ElasticsearchService } from 'src/app/services/elasticsearch.service';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'app-product-gallery',
  templateUrl: './product-gallery.component.html',
  styleUrls: ['./product-gallery.component.scss']
})
export class ProductGalleryComponent implements OnInit {
  public products: Project[];

  constructor(private searchService: ElasticsearchService) {}

  ngOnInit(): void {
    this.getProjects('');
  }

  private getProjects(searchTerm: string) {
    this.searchService.search(searchTerm).subscribe(
      res => {
        this.products = res;
      },
      error => {
        console.log(error);
      }
    );
  }
}

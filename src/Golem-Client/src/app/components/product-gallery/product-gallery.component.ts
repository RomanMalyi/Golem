import { Component, OnInit, Input } from '@angular/core';
import { ElasticsearchService } from 'src/app/services/elasticsearch.service';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'app-product-gallery',
  templateUrl: './product-gallery.component.html',
  styleUrls: ['./product-gallery.component.scss']
})
export class ProductGalleryComponent {
  @Input() public products: Project[];
}

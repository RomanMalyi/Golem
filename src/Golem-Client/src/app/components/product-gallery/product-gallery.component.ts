import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ProjectModel } from 'src/app/models/projectModel';

@Component({
  selector: 'app-product-gallery',
  templateUrl: './product-gallery.component.html',
  styleUrls: ['./product-gallery.component.scss']
})
export class ProductGalleryComponent {
  @Input() public products: ProjectModel[];

  constructor(
    private sanitizer: DomSanitizer
  ) {}

  transform(base64Image: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(base64Image);
  }
}

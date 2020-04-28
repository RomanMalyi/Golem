import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'app-product-gallery',
  templateUrl: './product-gallery.component.html',
  styleUrls: ['./product-gallery.component.scss']
})
export class ProductGalleryComponent {
  @Input() public products: Project[];

  constructor(
    private sanitizer: DomSanitizer
  ) {}

  transform(base64Image: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(base64Image);
  }
}

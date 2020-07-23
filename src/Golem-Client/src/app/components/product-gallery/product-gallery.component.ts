import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ProjectModel } from 'src/app/models/projectModel';
import { MatDialog } from '@angular/material/dialog';
import { ProductInfoModalComponent } from '../modals/product-info-modal/product-info-modal.component';

@Component({
  selector: 'app-product-gallery',
  templateUrl: './product-gallery.component.html',
  styleUrls: ['./product-gallery.component.scss']
})
export class ProductGalleryComponent {
  @Input() public products: ProjectModel[];

  constructor(
    private sanitizer: DomSanitizer,
    public dialog: MatDialog
  ) {}

  transform(base64Image: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(base64Image);
  }

  openDialog(product: ProjectModel) {
    this.dialog.open(ProductInfoModalComponent, {data: product});
  }
}

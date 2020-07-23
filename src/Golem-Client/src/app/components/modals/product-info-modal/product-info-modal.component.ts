import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectModel } from 'src/app/models/projectModel';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-product-info-modal',
  templateUrl: './product-info-modal.component.html',
  styleUrls: ['./product-info-modal.component.scss'],
})
export class ProductInfoModalComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public product: ProjectModel,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {}

  transform(base64Image: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(base64Image);
  }
}

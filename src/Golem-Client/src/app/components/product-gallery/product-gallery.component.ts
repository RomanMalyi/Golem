import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-gallery',
  templateUrl: './product-gallery.component.html',
  styleUrls: ['./product-gallery.component.scss']
})
export class ProductGalleryComponent implements OnInit {
  public products  = [
    // tslint:disable-next-line: max-line-length
    {text: 'Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old.',
    photo: '../../../assets/product_1.jpg'},
    // tslint:disable-next-line: max-line-length
    {text: 'Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage.',
    photo: '../../../assets/Product_2.jpg'},
    // tslint:disable-next-line: max-line-length
    {text: 'Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC. ',
    photo: '../../../assets/Product_3.jpg'},
    // tslint:disable-next-line: max-line-length
    {text: 'There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don\'t look even slightly believable.',
    photo: '../../../assets/Product_4.jpg'}
  ];

  constructor() {}

  ngOnInit(): void {}
}

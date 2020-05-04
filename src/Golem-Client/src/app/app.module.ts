import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';

import {HttpClientModule} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatDividerModule} from '@angular/material/divider';
import { ReactiveFormsModule } from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';

import { AppComponent } from './components/app/app.component';
import { ProductGalleryComponent } from './components/product-gallery/product-gallery.component';
import { FeaturesComponent } from './components/features/features.component';
import { GetInTouchComponent } from './components/get-in-touch/get-in-touch.component';
import { HomeComponent } from './components/home/home.component';
import { AnalyticsComponent } from './components/analytics/analytics.component';
import { UsersTableComponent } from './components/users-table/users-table.component';
import { QueriesTableComponent } from './components/queries-table/queries-table.component';



@NgModule({
  declarations: [
    AppComponent,
    ProductGalleryComponent,
    FeaturesComponent,
    GetInTouchComponent,
    HomeComponent,
    AnalyticsComponent,
    UsersTableComponent,
    QueriesTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatSnackBarModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

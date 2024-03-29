import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {MatToolbarModule} from '@angular/material/toolbar';
import {
  AccumulationChartModule,
  PieSeriesService,
  AccumulationLegendService,
  AccumulationTooltipService,
  AccumulationDataLabelService,
  AccumulationAnnotationService,
  BarSeriesService,
  CategoryService,
  DateTimeService,
  ScrollBarService,
  LineSeriesService,
  ColumnSeriesService,
  ChartAnnotationService,
  RangeColumnSeriesService,
  StackingColumnSeriesService,
  LegendService,
  TooltipService,
  ChartModule,
  SplineAreaSeriesService,
} from '@syncfusion/ej2-angular-charts';

import { AppComponent } from './components/app/app.component';
import { ProductGalleryComponent } from './components/product-gallery/product-gallery.component';
import { FeaturesComponent } from './components/features/features.component';
import { GetInTouchComponent } from './components/get-in-touch/get-in-touch.component';
import { HomeComponent } from './components/home/home.component';
import { AnalyticsComponent } from './components/analytics/analytics.component';
import { UsersTableComponent } from './components/users-table/users-table.component';
import { QueriesTableComponent } from './components/queries-table/queries-table.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SessionsTableComponent } from './components/sessions-table/sessions-table.component';
import { MatNativeDateModule } from '@angular/material/core';
import { SpinnerWrapperComponent } from './components/spinner-wrapper/spinner-wrapper.component';
import { CountriesChartComponent } from './components/countries-chart/countries-chart.component';
import { UsersChartComponent } from './components/users-chart/users-chart.component';
import { RequestsChartComponent } from './components/requests-chart/requests-chart.component';
import { DatePipe } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    ProductGalleryComponent,
    FeaturesComponent,
    GetInTouchComponent,
    HomeComponent,
    AnalyticsComponent,
    UsersTableComponent,
    QueriesTableComponent,
    DashboardComponent,
    SessionsTableComponent,
    SpinnerWrapperComponent,
    CountriesChartComponent,
    UsersChartComponent,
    RequestsChartComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AccumulationChartModule,
    ChartModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatSnackBarModule,
    MatTableModule,
    MatTabsModule,
    MatCardModule,
    MatPaginatorModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    FormsModule,
    MatProgressSpinnerModule,
    MatToolbarModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    DatePipe,
    PieSeriesService,
    AccumulationLegendService,
    AccumulationTooltipService,
    AccumulationDataLabelService,
    AccumulationAnnotationService,
    BarSeriesService,
    CategoryService,
    DateTimeService,
    ScrollBarService,
    LineSeriesService,
    ColumnSeriesService,
    ChartAnnotationService,
    RangeColumnSeriesService,
    StackingColumnSeriesService,
    LegendService,
    TooltipService,
    SplineAreaSeriesService
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

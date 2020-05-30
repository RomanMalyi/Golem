import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from 'src/app/services/http.service';
import { GetInTouch } from 'src/app/models/getInTouchModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SpinnerService } from 'src/app/services/spinner.service';

@Component({
  selector: 'app-get-in-touch',
  templateUrl: './get-in-touch.component.html',
  styleUrls: ['./get-in-touch.component.scss'],
})
export class GetInTouchComponent {
  public getInTouchForm: FormGroup;

  constructor(
    private httpService: HttpService,
    private snackBar: MatSnackBar,
    private spinnerService: SpinnerService
  ) {
    this.getInTouchForm = new FormGroup({
      message: new FormControl('', Validators.required),
      senderEmail: new FormControl('', [Validators.required, Validators.email]),
      senderFullName: new FormControl('', Validators.required),
    });
  }

  public SendEmail() {
    if (this.getInTouchForm.valid) {
      const getIntouchModel: GetInTouch = {
        message: this.getInTouchForm.value.message,
        senderEmail: this.getInTouchForm.value.senderEmail,
        senderFullName: this.getInTouchForm.value.senderFullName,
      };
      this.spinnerService.showSpinner();
      this.httpService.sendEmail(getIntouchModel).subscribe(
        (result) => {
          this.openSnackBar('Message send!', 'Close');
          this.getInTouchForm.reset();
          // tslint:disable-next-line: forin
          for (const name in this.getInTouchForm.controls) {
            this.getInTouchForm.controls[name].setErrors(null);
          }
          this.spinnerService.hideSpinner();
        },
        (error) => {
          this.spinnerService.hideSpinner();
          this.openSnackBar('Oops! Something went wrong.', 'Close');
        }
      );
    }
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 3000,
    });
  }
}

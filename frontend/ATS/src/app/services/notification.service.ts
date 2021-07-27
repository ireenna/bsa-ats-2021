import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private _toastrTimeOutInMiliseconds : number = 5000;
  private _toastrPosition : string = "toast-bottom-right";
  private _successToastClass : string = "ngx-toastr toast-success toast-success-custom";
  private _errorToastClass : string = "ngx-toastr toast-error toast-error-custom";
  private _infoToastClass : string = "ngx-toastr toast-info toast-info-custom";


  constructor(private _toastr: ToastrService) {
    _toastr.toastrConfig.timeOut = this._toastrTimeOutInMiliseconds;
    _toastr.toastrConfig.positionClass = this._toastrPosition;
  }

  public showSuccessMessage(message: string, title: string) {
    this._toastr.success(message, title, { toastClass : this._successToastClass});
  }

  public showErrorMessage(message: string, title: string) {
    this._toastr.error(message, title, { toastClass : this._errorToastClass});
  }

  public showInfoMessage(message: string, title: string) {
    this._toastr.info(message, title, { toastClass : this._infoToastClass});
  }
  
  public showDefaultMessage(message: string, title: string) {
    this._toastr.show(message, title);
  }

  public showWarningMessage(message: string, title: string) {
    this._toastr.warning(message, title);
  }
}

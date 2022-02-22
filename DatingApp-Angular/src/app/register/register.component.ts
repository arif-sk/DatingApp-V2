import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { throwError } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegistration = new EventEmitter();
  constructor(private accountServicde: AccountService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountServicde.register(this.model).subscribe(resp => {
      console.log(resp);
      this.cancel();
    }, err => {
      throwError(err);
    });
  }

  cancel() {
    this.cancelRegistration.emit(false);
  }

}

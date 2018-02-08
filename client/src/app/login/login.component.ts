import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AlertService, AuthenticationService } from '../_services/index';

@Component({
  moduleId: module.id,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  // 1)
  model: any = {};
  loading = false;
  returnUrl: string;
  // 2)
  constructor(
      private route: ActivatedRoute,
      private router: Router,
      private authenticationService: AuthenticationService,
      private alertService: AlertService
     ) { }

  // 3)
  ngOnInit() {
    // reset login status
    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // 4)
  login() {
    this.loading = true;
    this.authenticationService.login(this.model.username, this.model.password)
      // .subscribe (object) method
      //   .subscribe({
      //     next:   data => {
      //             this.router.navigate([this.returnUrl]);
      //         },
      //     error:   error => {
      //             this.alertService.error('Username or password is incorrect');
      //             this.loading = false;
      //         }
      //   }
      // )
      .subscribe(
        data => {
                this.router.navigate([this.returnUrl]);
            },
        error => {
                this.alertService.error('Username or password is incorrect');
                this.loading = false;
            }
      )
      ;
  }
}

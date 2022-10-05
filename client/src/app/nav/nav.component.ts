import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  //loggedIn: boolean;
  currentUser$: Observable<User>;

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService ) { }

  ngOnInit(): void {
    //this.getCurrentUser();
    this.currentUser$ = this.accountService.currentUser$;
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next: response => { 
        this.router.navigateByUrl('/members');
        //this.loggedIn = true;
      }
    });
  }

  logout(){
    //this.loggedIn = false;
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  //getCurrentUser(){
  //  this.accountService.currentUser$.subscribe(user =>{
  //    this.loggedIn = !!user;
  //  }, error =>{
  //    console.log(error);  
  //  });    
  //}
}

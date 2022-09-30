import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  //loggedIn: boolean;
  currentUser$: Observable<User>;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    //this.getCurrentUser();
    this.currentUser$ = this.accountService.currentUser$;
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next: response => { 
        console.log(response);
        //this.loggedIn = true;
      },
      error: error => console.log(error)
    });
  }

  logout(){
    //this.loggedIn = false;
    this.accountService.logout();
  }

  //getCurrentUser(){
  //  this.accountService.currentUser$.subscribe(user =>{
  //    this.loggedIn = !!user;
  //  }, error =>{
  //    console.log(error);  
  //  });    
  //}
}

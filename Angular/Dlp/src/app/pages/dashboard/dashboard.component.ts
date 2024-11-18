import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  quote: string = ''; // Variable to store the quote
  author: string = '';
  firstassignment:any;
  user:any;
  count:number | undefined;
  ct:number=0;
  totalct: number=0;
  constructor(private dash:DashboardService){}
  ngOnInit(): void {
    // Call loadData when the component is initialized
    this.loadData();
    this.getpendindassignment();
    this.getassignmentcount();
    this.getusr();
    this.ct=0;
    // this.totalct;
  }
  

  loadData(){
    this.dash.getquote().subscribe(
      (response)=>{
        this.quote = response?.quote || '';  // Get the quote from the response
        this.author = response?.author || '';  // Get the author from the response
      }
    )
  }
  getassignmentcount(){
    this.dash.getAssignmentcount().subscribe(
      (response)=>
      {
        this.totalct=response.length;
      }
    )
  }

  getpendindassignment(){
    this.dash.getAssignment().subscribe(
      (response)=>{
      this.firstassignment=response[0];
      this.count=response.length-1;
      this.ct=response.length;
      }
    )
  }

  getusr(){
    this.dash.getuser().subscribe(
      response=>{
        this.user=response;
      }
    )
  }

}

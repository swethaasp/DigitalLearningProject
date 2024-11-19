import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { DashboardService } from '../../services/dashboard.service';
import { NoteService } from '../../services/notes.service';

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
  notecount: number=0;
  topnote: any;
  constructor(private dash:DashboardService,private noteservice:NoteService){}
  ngOnInit(): void {
    // Call loadData when the component is initialized
    this.loadData();
    this.getpendindassignment();
    this.getassignmentcount();
    this.getusr();
    this.getnote();
    this.ct=0;
    this.count=0;
    // this.totalct;
  }
  

  getnote(){
    this.noteservice.getNotesByUser().subscribe(
      (response)=>{
        this.notecount=response.length;
        this.topnote=response[0];
      }
    )
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

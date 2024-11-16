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
  constructor(private dash:DashboardService){}
  ngOnInit(): void {
    // Call loadData when the component is initialized
    this.loadData();
  }
  

  loadData(){
    this.dash.getquote().subscribe(
      (response)=>{
        this.quote = response?.quote || '';  // Get the quote from the response
        this.author = response?.author || '';  // Get the author from the response
      }
    )
  }

}

import { Routes } from '@angular/router';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { HomeComponent } from './pages/home/home.component';
import { Component } from '@angular/core';
import { LeaderboardComponent } from './pages/leaderboard/leaderboard.component';
import { AssignmentsComponent } from './pages/assignments/assignments.component';

export const routes: Routes = [
    {path:"",component:WelcomeComponent},
    {path:"login",component:LoginComponent},
    {path:"signup",component:RegisterComponent},
    {path:"home",component:HomeComponent ,children:[
        {path:"",component:DashboardComponent},
        {path:"leaderboard",component:LeaderboardComponent},
        {path:"assignments",component:AssignmentsComponent},

    ]},

];

import { Routes } from '@angular/router';
import { SideNavBarComponent } from './components/side-nav-bar/side-nav-bar.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent}, title: 'Home - Score Craft'},
    //Users
    { path: 'users', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent}, title: 'Home - Users'},
    { path: 'users/:refUser', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent}, },
    //Teams
    { path: 'teams', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent}, title: 'Home - Teams'},
    { path: 'teams/:refTeam', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent} },
    //Matches
    { path: 'matches', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent}, title: 'Home - Matches'},
    { path: 'matches/:refMatch', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent),data: {menu: SideNavBarComponent} },
    //MatchResults
    { path: 'matches/results/:refMatchResult', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent} },

    // WildCard Route 
    { path: '**', redirectTo: '/home', pathMatch: 'full' }
];

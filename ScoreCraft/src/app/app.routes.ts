import { Routes } from '@angular/router';
import { SideNavBarComponent } from './components/util/side-nav-bar/side-nav-bar.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent}, title: 'Home - Score Craft'},
    //Users
    { path: 'users', loadComponent: () => import('./pages/users/users-home/users.component').then(m => m.UsersComponent), data: {menu: SideNavBarComponent}, title: 'Home - Users'},
    //Teams
    { path: 'teams', loadComponent: () => import('./pages/teams/teams-home/teams.component').then(m => m.TeamsComponent), data: {menu: SideNavBarComponent}, title: 'Home - Teams'},

    //Matches
    { path: 'matches', loadComponent: () => import('./pages/matches/matches-home/matches-home.component').then(m => m.MatchesHomeComponent), data: {menu: SideNavBarComponent}, title: 'Home - Matches'},
    { path: 'matches/details/:refMatch', loadComponent: () => import('./pages/matches/matches-details/matches-details.component').then(m => m.MatchesDetailsComponent),data: {menu: SideNavBarComponent} },

    // Wildcard Route 
    { path: '**', redirectTo: '/home', pathMatch: 'full' }
];

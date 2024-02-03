import { Routes } from '@angular/router';
import { SideNavBarComponent } from './components/util/side-nav-bar/side-nav-bar.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent), data: {menu: SideNavBarComponent}, title: 'Home - Score Craft'},
    //Users
    { path: 'users', loadComponent: () => import('./pages/users/users-home/users.component').then(m => m.UsersComponent), data: {menu: SideNavBarComponent}, title: 'Home - Users'},
    { path: 'users/:refUser', loadComponent: () => import('./pages/users/user-details/user-details.component').then(m => m.UserDetailsComponent), data: {menu: SideNavBarComponent}, },
    //Teams
    { path: 'teams', loadComponent: () => import('./pages/teams/teams-home/teams.component').then(m => m.TeamsComponent), data: {menu: SideNavBarComponent}, title: 'Home - Teams'},
    { path: 'teams/:refTeam', loadComponent: () => import('./pages/teams/team-details/team-details.component').then(m => m.TeamDetailsComponent), data: {menu: SideNavBarComponent} },
    //Matches
    { path: 'matches', loadComponent: () => import('./pages/matches/matches-home/matches-home.component').then(m => m.MatchesHomeComponent), data: {menu: SideNavBarComponent}, title: 'Home - Matches'},
    { path: 'matches/details/:refMatch', loadComponent: () => import('./pages/matches/matches-details/matches-details.component').then(m => m.MatchesDetailsComponent),data: {menu: SideNavBarComponent} },
    //MatchResults
    { path: 'matches/results/:refMatchResult', loadComponent: () => import('./pages/matches/match-results/match-results.component').then(m => m.MatchResultsComponent), data: {menu: SideNavBarComponent} },

    // Wildcard Route 
    { path: '**', redirectTo: '/home', pathMatch: 'full' }
];

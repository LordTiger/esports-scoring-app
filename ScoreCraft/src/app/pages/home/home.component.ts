import { Component } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import { MatDivider } from '@angular/material/divider';
import { RouterModule } from '@angular/router';
import { IonCard, IonGrid, IonCol, IonRow,  IonCardContent, IonCardHeader, IonCardSubtitle, IonCardTitle } from '@ionic/angular/standalone';



type Card = { 
  title: string;
  subtitle: string;
  image?: string;
  desc?: string;
  routerLink: string | Array<string | number>;
};
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatCardModule, IonCard, IonGrid, IonCol, IonRow, IonCardContent, IonCardHeader, IonCardSubtitle, IonCardTitle, RouterModule, MatDivider],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  cards: Array<Card> = [
    {
      title: 'Users',
      subtitle: 'View and manage users',
      desc: 'View, add, edit, and delete user accounts effortlessly for streamlined user management.',
      routerLink: '/users'
    },
    {
      title: 'Teams',
      subtitle: 'View and manage teams',
      desc: 'Manage e-sports teams with ease - view, add, edit, or remove teams for optimal organization.',
      routerLink: '/teams'
    },
    {
      title: 'Matches',
      subtitle: 'View and manage matches',
      desc: 'Seamlessly handle match-related data - view, add, edit, or delete matches for efficient e-sports event management.',
      routerLink: '/matches'
    }
  ];
}

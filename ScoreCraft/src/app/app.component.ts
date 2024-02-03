import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { SideNavBarComponent } from './components/side-nav-bar/side-nav-bar.component';
import { IMenuItemModel } from './interfaces/i-menu-item-model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, SideNavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ScoreCraft';
  menuItems: Array<IMenuItemModel> = [
    {
      label: 'Home',
      href: '/',
      iconName: 'home'
    },
    {
      label: 'Users',
      href: '/Users',
      iconName: 'person'
    },
    {
      label: 'Teams',
      href: '/Teams',
      iconName: 'group'
    },
    {
      label: 'Matches',
      href: '/contact',
      iconName: 'sports_soccer'
    }
  ];

}

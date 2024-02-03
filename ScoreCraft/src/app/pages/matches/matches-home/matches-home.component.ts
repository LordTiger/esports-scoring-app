import { Component, OnInit, inject } from '@angular/core';
import { LoadingController, ToastController } from '@ionic/angular/standalone';
import { RibbonComponent } from '../../../components/util/ribbon/ribbon.component';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTableModule} from '@angular/material/table';
import { IMatchModel } from '../../../interfaces/i-match-model';
import { MatchesService } from '../../../services/matches.service';

@Component({
  selector: 'app-matches-home',
  standalone: true,
  imports: [RibbonComponent, MatToolbarModule, MatButtonModule, MatIconModule, MatTooltipModule, MatTableModule],
  templateUrl: './matches-home.component.html',
  styleUrl: './matches-home.component.scss'
})
export class MatchesHomeComponent {
  private matchService = inject(MatchesService);
  private toastController = inject(ToastController);
  private loadingController = inject(LoadingController);

  matches: Array<IMatchModel> = [];
  displayedColumns: string[] = ['matchDate', 'homeTeam', 'guestTeam',  'format', 'bestOf', 'winningTeam', 'actions'];

  ngOnInit(): void {
    this.fetchData();
  }

  async fetchData() {
    const loading = await this.loadingController.create({
      message: 'Loading matches...'
    });

    await loading.present();
    try {

      const result = await this.matchService.getCollection();

      if (result) {
        this.matches = result;
      }
      
    } catch (error) {
      const toast = await this.toastController.create({
        message: error as string,
        duration: 2000,
        color: 'danger'
      });
      await toast.present();
    } finally {
        await loading.dismiss();
    }
  }

}

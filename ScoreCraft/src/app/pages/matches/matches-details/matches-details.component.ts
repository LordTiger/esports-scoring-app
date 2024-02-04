import { Component, OnInit, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import {MatTabsModule} from '@angular/material/tabs';
import { AlertController, IonAvatar, IonText, LoadingController, ToastController } from '@ionic/angular/standalone';
import { MatchesService } from '../../../services/matches.service';
import { MatDialog } from '@angular/material/dialog';
import { IMatchModel } from '../../../interfaces/i-match-model';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';

@Component({
  selector: 'app-matches-details',
  standalone: true,
  imports: [MatCardModule, MatTabsModule, IonAvatar, DatePipe, IonText, MatButtonModule, MatIconModule, MatListModule],
  templateUrl: './matches-details.component.html',
  styleUrl: './matches-details.component.scss'
})
export class MatchesDetailsComponent implements OnInit {


  private matchService = inject(MatchesService);
  private toastController = inject(ToastController);
  private loadingController = inject(LoadingController);
  private alertController = inject(AlertController);
  private dialog = inject(MatDialog);
  private activatedRoute = inject(ActivatedRoute);

  model: IMatchModel = {
    refMatch: this.activatedRoute.snapshot.params['refMatch'],
  };



  ngOnInit(): void {
    this.fetchData();
  }


  async fetchData() {
    const loading = await this.loadingController.create({
      message: 'Loading matches...'
    });

    await loading.present();
    try {

      const result = await this.matchService.getMatch(this.model.refMatch!);

      if (result) {
       this.model = result;
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

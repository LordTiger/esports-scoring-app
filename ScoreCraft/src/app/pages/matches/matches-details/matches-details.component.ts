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
import { AddMatchResultDialogComponent } from '../../../components/matches/add-match-result-dialog/add-match-result-dialog.component';
import {matchResultDialogType}  from '../../../types/dialogTypes';

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


  /**
   * Fetches data for the matches-details component.
   * Displays a loading spinner while fetching data.
   * If successful, updates the model with the fetched data.
   * If an error occurs, displays an error toast message.
   */
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

  /**
   * Handles the click event when the "Add" button is clicked.
   * Opens a dialog to add match result and updates the data accordingly.
   */
  onAddClick() {
    const dialogData: matchResultDialogType = { 
      title: 'Add Match Result',
      data: {
        refMatch: this.model.refMatch
      }
    }

    const dialogRef = this.dialog.open(AddMatchResultDialogComponent, {
      width: '37.5rem',
      height: 'auto',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.fetchData();

        if(this.model.bestOf == this.model.matchResults?.length) { // Check if the match is over, set winning team based on match results
          if(this.model.homeTotalWon! > this.model.guestTotalWon!) this.model.refMatchWinner = this.model.refHomeTeam; else this.model.refMatchWinner = this.model.refGuestTeam;
          
          this.setWinningTeam();
          
        }
      }
    });
  }

  /**
   * Sets the winning team for the match.
   */
  private async setWinningTeam() {
    try {
      const result = await this.matchService.update(this.model);

      if (result) {
        const toast = await this.toastController.create({
          message: 'Match result updated!',
          duration: 2000,
          color: 'success'
        });
        await toast.present();
      }
    } catch (error) { 
      const toast = await this.toastController.create({
        message: error as string,
        duration: 2000,
        color: 'danger'
      });
      await toast.present();
    }
  }

}

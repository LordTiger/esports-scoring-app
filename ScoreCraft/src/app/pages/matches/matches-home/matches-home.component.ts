import { Component, OnInit, inject } from '@angular/core';
import { AlertController, LoadingController, ToastController } from '@ionic/angular/standalone';
import { RibbonComponent } from '../../../components/util/ribbon/ribbon.component';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTableModule} from '@angular/material/table';
import { IMatchModel } from '../../../interfaces/i-match-model';
import { MatchesService } from '../../../services/matches.service';
import { MatDialog } from '@angular/material/dialog';
import { matchDialogType } from '../../../types/dialogTypes';
import { UpsertMatchDialogComponent } from '../../../components/matches/upsert-match-dialog/upsert-match-dialog.component';
import { DatePipe, NgClass } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-matches-home',
  standalone: true,
  imports: [RibbonComponent, MatToolbarModule, MatButtonModule, MatIconModule, MatTooltipModule, MatTableModule, DatePipe, NgClass, RouterModule],
  templateUrl: './matches-home.component.html',
  styleUrl: './matches-home.component.scss'
})
export class MatchesHomeComponent implements OnInit {
  private matchService = inject(MatchesService);
  private toastController = inject(ToastController);
  private loadingController = inject(LoadingController);
  private alertController = inject(AlertController);
  private dialog = inject(MatDialog);

  matches: Array<IMatchModel> = [];
  displayedColumns: string[] = ['matchDate', 'homeTeam', 'guestTeam',  'format', 'bestOf', 'winningTeam', 'actions'];

  ngOnInit(): void {
    this.fetchData();
  }

  /**
   * Fetches data by calling the matchService's getCollection method.
   * If successful, assigns the fetched data to the 'matches' property.
   * Displays an error toast if an error occurs during the fetch operation.
   */
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

  /**
   * Handles the click event when the "Add" button is clicked.
   */
  onAddClick() {
    const dialogData: matchDialogType = { 
      title: 'Create Match',
      isEdit: false,
      data: {}
    }

    const dialogRef = this.dialog.open(UpsertMatchDialogComponent, {
      width: '37.5rem',
      height: 'auto',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.fetchData();
      }
    });
  }

  /**
   * Handles the click event when the edit button is clicked for a match.
   * @param match The match object to be edited.
   */
  onEditClick(match: IMatchModel) {
    const dialogData: matchDialogType = {
      title: 'Edit Match',
      isEdit: true,
      data: match
    }

    const dialogRef = this.dialog.open(UpsertMatchDialogComponent, {
      width: '37.5rem',
      height: 'auto',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.fetchData();
      }
    });
  }

  /**
   * Handles the click event for deleting a match.
   * @param refMatch The reference number of the match to be deleted.
   */
  async onDeleteClick(refMatch: number) {
    const alert = await this.alertController.create({
      header: 'Confirm Match Deletion',
      subHeader: 'This action cannot be undone',
      message: 'Are you sure you want to delete this item?',
      buttons: [ {
        text: 'Cancel',
        role: 'cancel'
      }, {
        text: 'Delete',
        handler: async () => {
          await this.doDelete(refMatch);
        }
      }],
    });

    await alert.present();
  }

  /**
   * Deletes a match based on the provided reference match number.
   * @param refMatch The reference match number of the match to be deleted.
   */
  private async doDelete(refMatch: number) {
    const loading = await this.loadingController.create({
      message: 'Removing Team...'
    });

    await loading.present();
    try {
      const result = await this.matchService.delete(refMatch);

      if(result) {
       this.matches = await this.matchService.getCollection();
      }
      
    } catch (error) {
      var errorMessage = 'Oops, something went wrong';

      if (error instanceof Error) {
        errorMessage = error.message;
      }
       
      if(typeof error === 'string') { 
        errorMessage = error;
      }

      const toast = await this.toastController.create({
        message: errorMessage,
        duration: 2000,
        color: 'danger'
      });
      await toast.present();
    } finally {
      await loading.dismiss();
    }
  } 


}

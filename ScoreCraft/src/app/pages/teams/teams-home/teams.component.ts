import { Component, OnInit, inject } from '@angular/core';
import { AlertController, LoadingController, ToastController } from '@ionic/angular/standalone';
import { RibbonComponent } from '../../../components/util/ribbon/ribbon.component';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTableModule} from '@angular/material/table';
import { TeamsService } from '../../../services/teams.service';
import { ITeamModel } from '../../../interfaces/i-team-model';
import { MatDialog } from '@angular/material/dialog';
import { UpsertTeamDialogComponent } from '../../../components/teams/upsert-team-dialog/upsert-team-dialog.component';
import { teamDialogType } from '../../../types/dialogTypes';
@Component({
  selector: 'app-teams',
  standalone: true,
  imports: [RibbonComponent, MatToolbarModule, MatButtonModule, MatIconModule, MatTooltipModule, MatTableModule],
  templateUrl: './teams.component.html',
  styleUrl: './teams.component.scss'
})
export class TeamsComponent implements OnInit {

  private teamsService = inject(TeamsService);
  private toastController = inject(ToastController);
  private loadingController = inject(LoadingController);
  private alertController = inject(AlertController);
  private dialog = inject(MatDialog);

  teams: Array<ITeamModel> = [];
  displayedColumns: string[] = ['teamName', 'ratio', 'totalMatches', 'actions'];

  ngOnInit(): void {
    this.fetchData();
  }

  /**
   * Fetches the teams data from the teams service.
   * Displays a loading spinner while fetching the data.
   * If successful, assigns the fetched data to the 'teams' property.
   * If an error occurs, displays an error toast message.
   */
  async fetchData() {
    const loading = await this.loadingController.create({
      message: 'Loading teams...'
    });

    await loading.present();
    try {

      const result = await this.teamsService.getCollection();

      if (result) {
        this.teams = result;
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
   * Opens a dialog to add a new team and fetches data if the dialog is closed with a result.
   */
  onAddClick() {
    const dialogData: teamDialogType = { 
      title: 'Add Team',
      isEdit: false,
      data: {}
    }

    const dialogRef = this.dialog.open(UpsertTeamDialogComponent, {
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
   * Handles the click event when the edit button is clicked for a team.
   * @param team - The team object to be edited.
   */
  onEditClick(team: ITeamModel) {
    const dialogData: teamDialogType = {
      title: 'Edit Team',
      isEdit: true,
      data: team
    }

    const dialogRef = this.dialog.open(UpsertTeamDialogComponent, {
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
   * Handles the click event when the delete button is clicked for a team.
   * Displays a confirmation alert and deletes the team if confirmed.
   * @param refTeam The reference number of the team to be deleted.
   */
  async onDeleteClick(refTeam: number) {
    const alert = await this.alertController.create({
      header: 'Confirm Team Deletion',
      subHeader: 'This action cannot be undone',
      message: 'Are you sure you want to delete this item?',
      buttons: [ {
        text: 'Cancel',
        role: 'cancel'
      }, {
        text: 'Delete',
        handler: async () => {
          await this.doDelete(refTeam);
        }
      }],
    });

    await alert.present();
  }

  /**
   * Deletes a team based on the provided reference number.
   * 
   * @param refTeam The reference number of the team to be deleted.
   */
  private async doDelete(refTeam: number) {
    const loading = await this.loadingController.create({
      message: 'Removing Team...'
    });

    await loading.present();
    try {
      const result = await this.teamsService.delete(refTeam);

      if(result) {
       this.teams = await this.teamsService.getCollection();
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

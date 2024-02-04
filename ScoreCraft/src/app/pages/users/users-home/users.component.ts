import { Component, OnInit, inject } from '@angular/core';
import { UsersService } from '../../../services/users.service';
import { IUserModel } from '../../../interfaces/i-user-model';
import { AlertController, LoadingController, ToastController } from '@ionic/angular/standalone';
import { RibbonComponent } from '../../../components/util/ribbon/ribbon.component';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTableModule} from '@angular/material/table';
import { UpsertUserDialogComponent } from '../../../components/users/upsert-user-dialog/upsert-user-dialog.component';
import { userDialogType } from '../../../types/dialogTypes';
import { MatDialog } from '@angular/material/dialog';
import { IUserTeamModel } from '../../../interfaces/iuser-team-model';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [RibbonComponent, MatToolbarModule, MatButtonModule, MatIconModule, MatTooltipModule, MatTableModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {

  private usersService = inject(UsersService);
  private toastController = inject(ToastController);
  private loadingController = inject(LoadingController);
  private alertController = inject(AlertController);
  private dialog = inject(MatDialog);

  users: Array<IUserModel> = [];
  displayedColumns: string[] = ['name', 'surname', 'team', 'email', 'actions'];

  ngOnInit(): void {
    this.fetchData();
  }


  /**
   * Fetches data from the users service and updates the users array.
   * Displays a loading spinner while fetching the data.
   * Displays an error toast if an error occurs during the fetch.
   */
  async fetchData() {
    const loading = await this.loadingController.create({
      message: 'Loading users...'
    });

    await loading.present();
    try {

      const result = await this.usersService.getCollection();

      if (result) {
        this.users = result;
        this.users.forEach(user => { 
          user.userAssignedTeams = this.getUserTeams(user.userTeams ?? []);
        });
      }
      
    } catch (error) {
      var errorMessage = '';

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

  /**
   * Retrieves the names of the teams associated with a user.
   * 
   * @param user - An array of IUserTeamModel representing the user's teams.
   * @returns A string containing the names of the teams, separated by commas.
   */
  getUserTeams(user: Array<IUserTeamModel>) { 
    if(user.length === 0) return 'No Teams Assigned';

    return user.map(ut => ut.team.teamName).join(', ');
  }


  /**
   * Handles the click event when the "Add" button is clicked.
   * Opens a dialog to add a new user and updates the data if the dialog is closed with a result.
   */
  onAddClick() {
    const dialogData: userDialogType = { 
      title: 'Add User',
      isEdit: false,
      data: {}
    }

    const dialogRef = this.dialog.open(UpsertUserDialogComponent, {
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
   * Handles the click event when the edit button is clicked.
   * Opens a dialog to edit the user details.
   * @param user - The user object to be edited.
   */
  onEditClick(user: IUserModel) {
    const dialogData: userDialogType = {
      title: 'Edit User',
      isEdit: true,
      data: user
    }

    const dialogRef = this.dialog.open(UpsertUserDialogComponent, {
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
   * Handles the click event for deleting a user.
   * @param refUser - The reference of the user to be deleted.
   */
  async onDeleteClick(refUser: string) {
    const alert = await this.alertController.create({
      header: 'Confirm User Deletion',
      subHeader: 'This action cannot be undone',
      message: 'Are you sure you want to delete this item?',
      buttons: [ {
        text: 'Cancel',
        role: 'cancel'
      }, {
        text: 'Delete',
        handler: async () => {
          await this.doDelete(refUser);
        }
      }],
    });

    await alert.present();
  }

  /**
   * Deletes a user with the specified reference.
   * 
   * @param refUser - The reference of the user to be deleted.
   */
  private async doDelete(refUser: string) {
    const loading = await this.loadingController.create({
      message: 'Removing User...'
    });

    await loading.present();
    try {
      const result = await this.usersService.deleteUser(refUser);

      if(result) {
       this.users = await this.usersService.getCollection();
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

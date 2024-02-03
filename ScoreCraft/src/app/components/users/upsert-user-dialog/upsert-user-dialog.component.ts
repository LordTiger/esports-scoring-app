import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef  } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { userDialogType } from '../../../types/dialogTypes';
import { ToastController } from '@ionic/angular';
import { UsersService } from '../../../services/users.service';
import { TeamsService } from '../../../services/teams.service';
import { ITeamModel } from '../../../interfaces/i-team-model';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { IUserModel } from '../../../interfaces/i-user-model';


@Component({
  selector: 'app-upsert-user-dialog',
  templateUrl: './upsert-user-dialog.component.html',
  styleUrls: ['./upsert-user-dialog.component.scss'],
  standalone: true,
  imports: [MatDialogModule, MatFormFieldModule, MatButtonModule, FormsModule, ReactiveFormsModule, MatSelectModule, MatInputModule, MatIconModule]
})
export class UpsertUserDialogComponent implements OnInit {

  dialogRef = inject(MatDialogRef<UpsertUserDialogComponent>);
  private dialogData: userDialogType = inject(MAT_DIALOG_DATA);
  private toastController = inject(ToastController);
  private userService = inject(UsersService);
  private teamsService = inject(TeamsService);

  dialogTitle: string = this.dialogData.title;

  dialogForm: FormGroup = new FormGroup({
    name: new FormControl(this.dialogData.data.name, [Validators.required]),
    surname: new FormControl(this.dialogData.data.surname, [Validators.required]),
    email: new FormControl(this.dialogData.data.email, [Validators.email, Validators.required]),
    team: new FormControl(this.dialogData.data.refTeam),
    isTeamCaptain: new FormControl(this.dialogData.data.isTeamCaptain ?? false)
  });

  isEdit: boolean = this.dialogData.isEdit;
  teams: Array<ITeamModel> = [];

  ngOnInit(): void {
    this.getTeams();
  }


  /**
   * Handles the form submission event.
   * If the form is valid, it creates or updates a user based on the edit mode.
   * Displays a success toast message if the operation is successful.
   * Displays an error toast message if an error occurs.
   */
  async onSubmit() {
    try {
      const model = this.getFormData();
      const result = (!this.isEdit)? await this.userService.createUser(model) : await this.userService.updateUser(model);
      if(result) {
        const toast = await this.toastController.create({
          message: 'User successfully ' + ((this.isEdit)? 'updated' : 'created'),
          duration: 2000,
          color: 'success'
        });
        await toast.present();

        this.dialogRef.close(true);
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
    }
  }

  /**
   * Retrieves the form data and returns it as an IUserModel object.
   * @returns {IUserModel} The form data as an IUserModel object.
   */
  getFormData(): IUserModel {
    return {
      name: this.dialogForm.controls['name'].value,
      surname: this.dialogForm.controls['surname'].value,
      email: this.dialogForm.controls['email'].value,
      refTeam: this.dialogForm.controls['team'].value,
      isTeamCaptain: this.dialogForm.controls['isTeamCaptain'].value,
      refUser: this.dialogData.data.refUser
    };
  }

  /**
   * Retrieves the teams collection and updates the component's teams property.
   * If the teams collection is empty, disables the team form control.
   * Displays an error toast if an error occurs during the retrieval process.
   */
  async getTeams() {
    try {
      this.teams = await this.teamsService.getCollection();

      if(this.teams.length === 0) this.dialogForm.controls['team'].disable();
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
    }
  }


}

import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef  } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { teamDialogType } from '../../../types/dialogTypes';
import { ToastController } from '@ionic/angular';
import { TeamsService } from '../../../services/teams.service';
import { ITeamModel } from '../../../interfaces/i-team-model';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-upsert-team-dialog',
  templateUrl: './upsert-team-dialog.component.html',
  styleUrls: ['./upsert-team-dialog.component.scss'],
  standalone: true,
  imports: [MatDialogModule, MatFormFieldModule, MatButtonModule, FormsModule, ReactiveFormsModule, MatSelectModule, MatInputModule, MatIconModule]
})
export class UpsertTeamDialogComponent  implements OnInit {
  dialogRef = inject(MatDialogRef<UpsertTeamDialogComponent>);
  private dialogData: teamDialogType= inject(MAT_DIALOG_DATA);
  private toastController = inject(ToastController);
  private teamsService = inject(TeamsService);

  dialogTitle: string = this.dialogData.title;

  dialogForm: FormGroup = new FormGroup({
    teamName: new FormControl(this.dialogData.data.teamName, [Validators.required]),
  });

  isEdit: boolean = this.dialogData.isEdit;

  ngOnInit() {}


  /**
   * Handles the form submission event.
   * If the form is valid, it inserts or updates the team data using the teamsService.
   * Displays a success toast message if the operation is successful.
   * Displays an error toast message if an error occurs.
   */
  async onSubmit() {
    try {
      const model = this.getFormData();
      const result = (!this.isEdit)? await this.teamsService.insert(model) : await this.teamsService.update(model);
      if(result) {
        const toast = await this.toastController.create({
          message: 'Team successfully ' + ((this.isEdit)? 'updated' : 'created'),
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
   * Retrieves the form data and returns it as an instance of ITeamModel.
   * @returns {ITeamModel} The form data as an instance of ITeamModel.
   */
  private getFormData(): ITeamModel {
    return {
      teamName: this.dialogForm.controls['teamName'].value,
      refTeam: this.dialogData.data.refTeam
    };
  }

}

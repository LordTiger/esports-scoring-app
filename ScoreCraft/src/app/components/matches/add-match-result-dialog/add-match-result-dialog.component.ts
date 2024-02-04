import { Component, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { matchResultDialogType } from '../../../types/dialogTypes';
import { ToastController } from '@ionic/angular';
import { MatchesService } from '../../../services/matches.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { IMatchResultModel } from '../../../interfaces/i-match-result-model';

@Component({
  selector: 'app-add-match-result-dialog',
  templateUrl: './add-match-result-dialog.component.html',
  styleUrls: ['./add-match-result-dialog.component.scss'],
  standalone: true,
  imports: [MatDialogModule, MatFormFieldModule, MatButtonModule, FormsModule, ReactiveFormsModule, MatInputModule, MatIconModule ]
})
export class AddMatchResultDialogComponent  implements OnInit {

  dialogRef = inject(MatDialogRef<AddMatchResultDialogComponent>);
  private dialogData: matchResultDialogType = inject(MAT_DIALOG_DATA);
  private toastController = inject(ToastController);
  private matchService = inject(MatchesService);

  dialogTitle: string = this.dialogData.title;


  dialogForm: FormGroup = new FormGroup({
    homeResult: new FormControl(this.dialogData.data.homeResult, [Validators.required,]),
    guestResult: new FormControl(this.dialogData.data.guestResult, [Validators.required]),
  });

  ngOnInit() {}

  /**
   * Handles the form submission for adding a match result.
   */
  async onSubmit() {
    try {
      const model = this.getFormData();
      const result = await this.matchService.addMatchResult(model);

      if(result) {
        const toast = await this.toastController.create({
          message: 'Match result successfully captured',
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
   * Retrieves the form data for the match result.
   * @returns {IMatchResultModel} The match result data.
   */
  getFormData(): IMatchResultModel {
    return {
      refMatch: this.dialogData.data.refMatch,
      homeResult: this.dialogForm.get('homeResult')?.value,
      guestResult: this.dialogForm.get('guestResult')?.value
    };
  }

}

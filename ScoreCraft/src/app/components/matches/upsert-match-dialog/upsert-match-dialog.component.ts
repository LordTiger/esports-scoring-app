import { AfterContentInit, Component, Inject, OnInit, WritableSignal, inject, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef  } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { ToastController } from '@ionic/angular';
import { TeamsService } from '../../../services/teams.service';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { ITeamModel } from '../../../interfaces/i-team-model';
import { matchDialogType } from '../../../types/dialogTypes';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MAT_DATE_LOCALE, MatNativeDateModule, provideNativeDateAdapter } from '@angular/material/core';
import {MatRadioModule} from '@angular/material/radio';
import { IMatchModel } from '../../../interfaces/i-match-model';
import { MatchesService } from '../../../services/matches.service';
import {MAT_MOMENT_DATE_ADAPTER_OPTIONS, MatMomentDateModule} from '@angular/material-moment-adapter'
@Component({
  selector: 'app-upsert-match-dialog',
  templateUrl: './upsert-match-dialog.component.html',
  styleUrls: ['./upsert-match-dialog.component.scss'],
  standalone: true,
  imports: [MatDialogModule, MatFormFieldModule, MatButtonModule, FormsModule, ReactiveFormsModule,
     MatSelectModule, MatInputModule, MatIconModule, MatDatepickerModule, MatNativeDateModule, MatRadioModule, MatMomentDateModule],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'en-ZA'},
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true }},
    provideNativeDateAdapter()
  ]
})
export class UpsertMatchDialogComponent implements OnInit {

  dialogRef = inject(MatDialogRef<UpsertMatchDialogComponent>);
  private dialogData: matchDialogType = inject(MAT_DIALOG_DATA);
  private toastController = inject(ToastController);
  private teamsService = inject(TeamsService);
  private matchService = inject(MatchesService);
 


  dialogTitle: string = this.dialogData.title;

  dialogForm: FormGroup = new FormGroup({
    matchDate: new FormControl(this.dialogData.data.matchDate, [Validators.required]),
    refHomeTeam: new FormControl(this.dialogData.data.refHomeTeam, [Validators.required]),
    refGuestTeam: new FormControl(this.dialogData.data.refGuestTeam, [Validators.required]),
    format: new FormControl(this.dialogData.data.format, [Validators.required]),
    bestOf: new FormControl(this.dialogData.data.bestOf, [Validators.required])
  });

  isEdit: boolean = this.dialogData.isEdit;

  teamsList: Array<ITeamModel> = [];
  bestOfOptions: Array<number> = [1, 3, 5];
  formatOptions: Array<string> = [];
  oppositeTeamList: WritableSignal<Array<ITeamModel>> = signal([]);

  ngOnInit() {
    this.getTeams();
  }

  /**
   * Handles the form submission.
   * If the form is valid, it inserts or updates a match using the matchService.
   * Displays a success toast message if the operation is successful.
   * Displays an error toast message if an error occurs.
   */
  async onSubmit() {
    try {
      const model = this.getFormData();
      const result = (!this.isEdit)? await this.matchService.insert(model) : await this.matchService.update(model);

      if(result) {
        const toast = await this.toastController.create({
          message: 'Match successfully ' + ((this.isEdit)? 'updated' : 'created'),
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

  getFormData(): IMatchModel {
    return {
      matchDate: this.dialogForm.controls['matchDate'].value,
      refHomeTeam: this.dialogForm.controls['refHomeTeam'].value,
      refGuestTeam: this.dialogForm.controls['refGuestTeam'].value,
      format: this.dialogForm.controls['format'].value,
      bestOf: this.dialogForm.controls['bestOf'].value,
      refMatch: this.dialogData.data.refMatch,
      refMatchWinner: this.dialogData.data.refMatchWinner
    };
  }



  /**
   * Filters the teams list based on the selected home team.
   */
  filterTeams() {
    const oppositeTeamList = this.teamsList.filter(team => team.refTeam !== this.dialogForm.controls['refHomeTeam'].value);
    this.oppositeTeamList.set(oppositeTeamList);
  }

  setFormatOptions() {
    
    const homeTeamSize = this.teamsList.find(team => team.refTeam === this.dialogForm.controls['refHomeTeam'].value)?.teamSize;
    const guestTeamSize = this.teamsList.find(team => team.refTeam === this.dialogForm.controls['refGuestTeam'].value)?.teamSize;

    this.formatOptions = [];

    if(homeTeamSize! > 0 && guestTeamSize! > 0) {
      this.formatOptions.push('1v1');
    }

    if(homeTeamSize! > 1 && guestTeamSize! > 1) {
      this.formatOptions.push('2v2');
    }

    if(homeTeamSize! > 2 && guestTeamSize! > 2) {
      this.formatOptions.push('3v3');
    }

    if(homeTeamSize! > 3 && guestTeamSize! > 3) {
      this.formatOptions.push('4v4');
    }

    if(homeTeamSize! > 4 && guestTeamSize! > 4) {
      this.formatOptions.push('5v5');
    }
    
  }

  /**
   * Retrieves the teams list from the teams service.
   * If an error occurs, displays an error message using a toast.
   */
  private async getTeams() { 
    try {
      this.teamsList =  await this.teamsService.getTeamsForLookup();

      if(this.isEdit) {
        this.filterTeams();
        this.setFormatOptions();
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

}

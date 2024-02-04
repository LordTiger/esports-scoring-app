import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { teamDialogType } from '../../../types/dialogTypes';
import { ITeamModel } from '../../../interfaces/i-team-model';
import { DatePipe, DecimalPipe } from '@angular/common';
import {MatChipsModule} from '@angular/material/chips';

@Component({
  selector: 'app-team-details',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatIconModule, DecimalPipe, MatChipsModule, DatePipe],
  templateUrl: './team-details.component.html',
  styleUrl: './team-details.component.scss'
})
export class TeamDetailsComponent {

  dialogRef = inject(MatDialogRef<TeamDetailsComponent>);
  private dialogData: teamDialogType = inject(MAT_DIALOG_DATA);

  dialogTitle: string = this.dialogData.title;
  model: ITeamModel = this.dialogData.data;

}

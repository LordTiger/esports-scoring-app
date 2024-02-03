import { Component, OnInit, inject } from '@angular/core';
import { LoadingController, ToastController } from '@ionic/angular/standalone';
import { RibbonComponent } from '../../../components/util/ribbon/ribbon.component';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTableModule} from '@angular/material/table';
import { TeamsService } from '../../../services/teams.service';
import { ITeamModel } from '../../../interfaces/i-team-model';
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

  teams: Array<ITeamModel> = [];
  displayedColumns: string[] = ['teamName', 'ratio', 'totalMatches', 'actions'];

  ngOnInit(): void {
    this.fetchData();
  }

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


}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { CardReportComponent } from './components/card-report/card-report.component';
import { TaskBoardComponent } from './components/task-board/task-board.component';


@NgModule({
  declarations: [DashboardComponent, CardReportComponent, TaskBoardComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }

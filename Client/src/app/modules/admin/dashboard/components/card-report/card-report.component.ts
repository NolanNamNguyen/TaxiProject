import { Component, OnInit, Input } from '@angular/core';


export declare interface cardReport{
  color: string;
  icon_content: string;
  category: string;
  description: string;
  footer_icon: string;
  footer_desc: string;
}

@Component({
  selector: 'app-card-report',
  templateUrl: './card-report.component.html',
  styleUrls: ['./card-report.component.scss']
})
export class CardReportComponent implements OnInit {
  
  colorMap = new Map();
  shadowMap = new Map();
  backgrounShadow: string;
  backgroundColor: string;
  @Input() inCardModel: cardReport;

  
  constructor() {

   }

  ngOnInit(): void {
    this.addColor();
    this.addShadowColor();
    this.backgrounShadow = this.shadowMap.get(this.inCardModel.color)
    this.backgroundColor = this.colorMap.get(this.inCardModel.color);
  }

  addShadowColor(): void{
    this.shadowMap.set('orange','0 4px 20px 0px rgba(0, 0, 0, 0.14), 0 7px 10px -5px rgba(255, 152, 0, 0.4)');
    this.shadowMap.set('green','0 4px 20px 0px rgba(0, 0, 0, 0.14), 0 7px 10px -5px rgba(76, 175, 80, 0.4)');
    this.shadowMap.set('red','0 4px 20px 0px rgba(0, 0, 0, 0.14), 0 7px 10px -5px rgba(244, 67, 54, 0.4)');
    this.shadowMap.set('blue','0 4px 20px 0px rgba(0, 0, 0, 0.14), 0 7px 10px -5px rgba(0, 188, 212, 0.4)');
  }

  addColor(): void{
    this.colorMap.set('orange','linear-gradient(60deg, #ffa726, #fb8c00)');
    this.colorMap.set('green','linear-gradient(60deg, #66bb6a, #43a047)');
    this.colorMap.set('red','linear-gradient(60deg, #ef5350, #e53935)');
    this.colorMap.set('blue','linear-gradient(60deg, #26c6da, #00acc1)');
  }

  styleObject(): Object{
    return {'background': this.backgroundColor,'box-shadow': this.backgrounShadow}
  }


}

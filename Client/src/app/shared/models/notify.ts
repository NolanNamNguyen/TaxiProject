import { Injectable } from '@angular/core';
import { Adapter } from './../services/Adapter';

export class Notify {
  constructor(
    public notifyId: number,
    public content: number,
    public isRead: boolean
  ) {}
}

@Injectable({
  providedIn: 'root',
})
export class NotifyAdapter implements Adapter<Notify> {
  adapt(item: any): Notify {
    return new Notify(
      item.notifyId,
      item.content,
      item.isRead
    );
  }
}

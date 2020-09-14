import { Injectable } from '@angular/core';
import { ClientService } from '../client/client.service';

@Injectable()
export class LoginService {
  constructor(private webApiService: ClientService) { }

}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VacancyCreate } from '../models/vacancy/vacancy-create';
import { VacancyFull } from '../models/vacancy/vacancy-full';
import { HttpClientService } from './http-client.service';

@Injectable({
  providedIn: 'root',
})
export class VacancyService {

  public constructor(private readonly http: HttpClientService) {}

  public postVacancy(vacancy:VacancyCreate): Observable<VacancyFull> {
    return this.http.postRequest<VacancyFull>(
      '/vacancies',
      vacancy,
    );
  }
}

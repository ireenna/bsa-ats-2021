import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClientService } from './http-client.service';

@Injectable({providedIn: 'root'})
export class CvService {
  public constructor(private readonly http: HttpClientService)
  { }

  public deleteCv(applicantId: string, cvFileId: string): Observable<void> {
    return this.http.deleteRequest<void>(`/Cv/${cvFileId}/applicant/${applicantId}`);
  }
}
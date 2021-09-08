import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CvFileInfo } from '../models/file/cvFileInfo';
import { HttpClientService } from './http-client.service';

@Injectable({ providedIn: 'root' })
export class CandidateCvService {
  public constructor(private readonly http: HttpClientService)
  { }

  public uploadCvForCandidate(candidateId: string, file: File): Observable<CvFileInfo> {
    const formData: FormData = new FormData();
    formData.append('cvFile', file);

    return this.http.postRequest<CvFileInfo>('/VacancyCandidates/' + candidateId +
            '/cv', formData);
  }

  public deleteCv(candidateId: string): Observable<void> {
    return this.http.deleteRequest<void>('/VacancyCandidates/' + candidateId + '/cv');
  }
}
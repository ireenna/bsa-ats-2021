import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CvFileInfo } from '../models/file/cvFileInfo';
import { HttpClientService } from './http-client.service';

@Injectable({ providedIn: 'root' })
export class CandidateCvService {
  public constructor(private readonly http: HttpClientService)
  { }

  public getAllowedCvs(candidateId: string): Observable<CvFileInfo[]> {
    return this.http.getRequest<CvFileInfo[]>('/VacancyCandidates/' + candidateId +
      '/cv');
  }

  public attachCvForCandidate(candidateId: string, cvFileId: string): Observable<CvFileInfo> {
    return this.http.postRequest<CvFileInfo>('/VacancyCandidates/' + candidateId +
      '/cv', { cvFileId: cvFileId });
  }

  public detachCv(candidateId: string): Observable<void> {
    return this.http.deleteRequest<void>('/VacancyCandidates/' + candidateId + '/cv');
  }
}
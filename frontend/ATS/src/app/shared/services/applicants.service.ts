import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateApplicant } from '../models/applicants/create-applicant';
import { UpdateApplicant } from '../models/applicants/update-applicant';
import { Applicant } from '../models/applicants/applicant';
import { HttpClientService } from './http-client.service';
import { MarkedApplicant } from 'src/app/shared/models/applicants/marked-applicant';
import { GetShortApplicant } from '../models/applicants/get-short-applicant';
import { CsvApplicant } from 'src/app/applicants/models/CsvApplicant';
import { VacancyWithRecentActivity }
  from '../models/candidate-to-stages/vacancy-with-recent-activity';
import { CvFileInfo } from '../models/file/cvFileInfo';

@Injectable({ providedIn: 'root' })
export class ApplicantsService {
  constructor(private readonly httpClient: HttpClientService) {}

  public getApplicants(): Observable<Applicant[]> {
    return this.httpClient.getRequest<Applicant[]>('/applicants');
  }

  public getApplicantByCompany(id: string): Observable<GetShortApplicant> {
    return this.httpClient.getRequest<GetShortApplicant>(`/applicants/company/${id}`);
  }

  public getMarkedApplicants(vacancyId: string): Observable<MarkedApplicant[]> {
    return this.httpClient.getRequest<MarkedApplicant[]>(`/applicants/marked/${vacancyId}`);
  }

  public addApplicant(createApplicant: CreateApplicant): Observable<Applicant> {
    const formData = new FormData();
    formData.append('body', JSON.stringify(createApplicant));
    if (createApplicant.cvs) {
      createApplicant.cvs.forEach((f) => formData.append('cvFiles', f));
    }
    return this.httpClient.postRequest<Applicant>('/applicants', formData);
  }

  public updateApplicant(updateApplicant: UpdateApplicant): Observable<Applicant> {
    const formData = new FormData();
    formData.append('body', JSON.stringify(updateApplicant));
    if (updateApplicant.cvs) {
      updateApplicant.cvs.forEach((f) => formData.append('cvFiles', f));
    }
    return this.httpClient.putRequest<Applicant>('/applicants', formData);
  }

  public deleteApplicant(applicantId: string): Observable<Applicant> {
    return this.httpClient.deleteRequest<Applicant>(
      '/applicants/' + applicantId,
    );
  }

  public getCv(applicantId: string): Observable<CvFileInfo[]> {
    return this.httpClient.getRequest<CvFileInfo[]>(`/applicants/${applicantId}/cv`);
  }

  public getApplicantByEmail(email: string): Observable<Applicant>{
    return this.httpClient.getRequest<Applicant>(`/applicants/property/email/${email}`);
  }

  public getRecentActivity(id: string): Observable<VacancyWithRecentActivity[]> {
    return this.httpClient.getRequest<VacancyWithRecentActivity[]>(
      `/recentActivity/for-applicant/${id}`,
    );
  }
}

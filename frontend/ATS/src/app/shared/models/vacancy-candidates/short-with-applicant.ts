import { Applicant } from '../applicant/applicant';
import { Model } from '../model';

export interface ShortVacancyCandidateWithApplicant extends Model {
  averageMark?: number;
  dateAdded: Date;
  applicant: Applicant;
}

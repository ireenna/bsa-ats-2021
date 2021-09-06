import { Applicant } from 'src/app/shared/models/applicants/applicant';
import { Vacancy } from 'src/app/applicants/models/Vacancy';
import { User } from 'src/app/users/models/user';
import { MeetingSource } from './enums/meeting-source.enum';
import { InterviewType } from './enums/interview-type.enum';

export class CreateInterview{
  public title: string| undefined;
  public meetingLink: string| undefined;
  public interviewType: InterviewType | undefined;
  public meetingSource: MeetingSource| undefined;
  public userParticipants: User[] = []
  public vacancyId: string| undefined;
  public scheduled: Date| undefined;
  public duration: number| undefined;
  public note: string | undefined;
  public candidate: Applicant| undefined;
}
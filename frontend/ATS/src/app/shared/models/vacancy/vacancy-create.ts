import { Tag } from 'src/app/users/models/tag';
import { Stage } from '../stages/stage';

export interface VacancyCreate{
  title:string
  description:string,
  requirements:string,
  projectId:string,
  salaryFrom:number,
  salaryTo:number,
  tierFrom:number,
  tierTo:number,
  sources:string,
  isHot:boolean,
  isRemote:boolean,
  tags: Tag[], //Elastic Entity???
  stages:Stage[]
}
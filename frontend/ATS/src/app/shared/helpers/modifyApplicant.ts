import { CreateApplicant } from '../models/applicants/create-applicant';
import { UpdateApplicant } from '../models/applicants/update-applicant';

export function getModifyApplicantBody(dto: CreateApplicant | UpdateApplicant): any {
  return {
    ...dto,
    cvLink: typeof dto.cvs === 'string' ? dto.cvs : null,
    photoLink: typeof dto.photo === 'string' ? dto.photo : null,
  };
}

export function getModifyApplicantFormData(dto: CreateApplicant | UpdateApplicant): FormData {
  const formData = new FormData();
  formData.append('body', JSON.stringify(getModifyApplicantBody(dto)));

  if (dto.cvs && typeof dto.cvs !== 'string') {
    dto.cvs.forEach(f => formData.append('cvFiles', f));
  }

  if (dto.photo && typeof dto.photo !== 'string') {
    formData.append('photoFile', dto.photo);
  }

  console.log(formData);
  return formData;
}

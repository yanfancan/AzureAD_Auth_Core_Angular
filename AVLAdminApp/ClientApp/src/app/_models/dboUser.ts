export class DboUser {
  id: number;
  firstName: string;
  lastName: string;
  department: string;
  email: string;
  position: string;
  accessLevel: number;
  accountLocked: boolean;
  accountDisabled: boolean;
  password: string;
  addedBy: string;
  modifiedDateTime: Date;
  modifiedBy: string;
}

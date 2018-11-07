import { DboService } from "./dboService";

export class DboServiceAccount {
  id: number;
  loginName: string;
  name: string;
  password: string;
  roles: string;
  status: string;
  TimeZone: string;
  AddedBy: string;
  modifiedBy: string;
  services: DboService[];
}

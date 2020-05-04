import { UserModel } from './userModel';

export class QueryModel {
  id: string;
  user: UserModel;
  path: string;
  methodType: string;
  queryString: string;
  userAgent: string;
  ipAddress: string;
  creationDate: Date;
}

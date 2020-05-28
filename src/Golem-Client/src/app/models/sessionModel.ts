import { UserModel } from './userModel';

export class SessionModel {
  id: string;
  user: UserModel;
  startTime: Date;
  endTime: Date;
}

export class UserModel {
  id: string;
  numberOfVisits: number;
  numberOfRequests: number;
  lastVisitTime: Date;
  firstVisitTime: Date;
  country: string;
  region: string;
  city: string;
  userAgent: string;
  operatingSystem: string;
  device: string;
}

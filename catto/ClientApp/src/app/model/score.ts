import { User } from '../model/user';

export interface Score {
  id: number;
  user: User;
  value: number;
}

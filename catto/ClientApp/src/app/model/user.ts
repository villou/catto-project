export interface User {
  id?: number;
  username: string;
  avatar?: string;
}

export const defaultUser: User = {
  username: '',
};

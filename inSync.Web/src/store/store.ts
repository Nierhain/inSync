import { atom, useAtom } from 'jotai';

export const adminKeyAtom = atom('adminKey');
export const userAtom = atom('username');

export const useStore = () => ({
    adminKey: useAtom(adminKeyAtom)[0],
    setAdminKey: useAtom(adminKeyAtom)[1],
    username: useAtom(userAtom)[0],
    setUsername: useAtom(userAtom)[1],
});

import create from 'zustand';
import { devtools } from 'zustand/middleware';
interface SyncStore {
    username: string;
    adminKey: string;
    isAdmin: boolean;
    setIsAdmin: (isAdmin: boolean) => void;
    setAdminKey: (key: string, save?: boolean) => void;
    setUsername: (name: string) => void;
    password: Record<string, string>;
    addPassword: (id: string, password: string) => void;
    currentId: string;
    currentTitle: string;
    setCurrentId: (id: string) => void;
    setCurrentTitle: (title: string) => void;
}

export const useStore = create(
    devtools<SyncStore>((set) => ({
        username: localStorage.getItem('username') ?? '',
        adminKey: localStorage.getItem('adminKey') ?? '',
        setAdminKey: (key, save) => {
            if (save) {
                localStorage.setItem('adminKey', key);
            }
            set(() => ({ adminKey: key }));
        },
        isAdmin: false,
        setIsAdmin: (isAdmin) => {
            set(() => ({ isAdmin: isAdmin }));
        },
        setUsername: (name) => {
            localStorage.setItem('username', name);
            set(() => ({ username: name }));
        },
        password: {},
        addPassword: (id, password) => {
            console.log(password)
            set((state) => ({ password: { ...state.password, [id]: password } }));
        },
        currentId: '',
        currentTitle: '',
        setCurrentId: (id) => {
            set(() => ({ currentId: id }));
        },
        setCurrentTitle: (title) => {
            set(() => ({ currentTitle: title }));
        },
    }))
);

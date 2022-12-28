import create from 'zustand';

interface SyncStore {
    username: string;
    adminKey: string;
    setAdminKey: (key: string) => void;
    setUsername: (name: string) => void;
    password: Record<string, string>;
    addPassword: (id: string, password: string) => void;
}
export const useStore = create<SyncStore>((set) => ({
    username: '',
    adminKey: '',
    setAdminKey: (key) => set((state) => ({ adminKey: key })),
    setUsername: (name) => set((state) => ({ username: name })),
    password: {},
    addPassword: (id, password) => set((state) => ({ password: { ...state.password, [id]: password } })),
}));

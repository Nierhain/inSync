import {atom, useAtom} from 'jotai'

const adminKeyAtom = atom('adminKey')
const userAtom = atom('username')

export const useStore = {
    adminKey: useAtom(adminKeyAtom),
    username: useAtom(userAtom)
}
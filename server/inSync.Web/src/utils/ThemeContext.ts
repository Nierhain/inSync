import { createContext } from 'react';

export type ThemeTypes = 'dark' | 'light';
interface Theme {
    theme: ThemeTypes;
    setTheme: (theme: ThemeTypes) => void;
}
export const ThemeContext = createContext<Theme>({ theme: 'light', setTheme: (theme: ThemeTypes) => {} });

import { ThemeTypes } from './ThemeContext';

export const toThemeType = (theme: string | null): ThemeTypes => {
    if ((theme !== 'dark' && theme !== 'light') || !theme) {
        return 'light';
    }
    return theme;
};

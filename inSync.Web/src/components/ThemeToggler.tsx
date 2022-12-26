import { Button } from 'antd';
import { useContext } from 'react';
import { ThemeContext } from '../utils/ThemeContext';
import { IconMoon, IconSunHigh } from '@tabler/icons';

export default function () {
    const { theme, setTheme } = useContext(ThemeContext);

    const isDark = theme === 'dark';
    return (
        <>
            <Button
                icon={isDark ? <IconMoon /> : <IconSunHigh />}
                onClick={() => setTheme(isDark ? 'light' : 'dark')}
            ></Button>
        </>
    );
}

import React, { useState } from 'react';
import { useQueryClient, QueryClientProvider, QueryClient } from '@tanstack/react-query';
import { ConfigProvider, theme } from 'antd';
import { ThemeContext, ThemeTypes } from '../utils/ThemeContext';
const queryClient = new QueryClient();
const { darkAlgorithm, defaultAlgorithm } = theme;
export default function Provider({ children }: { children: React.ReactNode }) {
    const [currentTheme, setTheme] = useState<ThemeTypes>('light');

    return (
        <>
            <QueryClientProvider client={queryClient}>
                <ThemeContext.Provider value={{ theme: currentTheme, setTheme: setTheme }}>
                    <ConfigProvider theme={{ algorithm: currentTheme === 'dark' ? darkAlgorithm : defaultAlgorithm }}>
                        {children}
                    </ConfigProvider>
                </ThemeContext.Provider>
            </QueryClientProvider>
        </>
    );
}

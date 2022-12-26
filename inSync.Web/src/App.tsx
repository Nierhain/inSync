import { createReactRouter, createRouteConfig, Outlet, RouterProvider } from '@tanstack/react-router';
import { useState } from 'react';
import { Layout, Menu, theme } from 'antd';
import Navigation from './components/Navigation';

const { Content, Header, Sider, Footer } = Layout;
const rootRoute = createRouteConfig({
    component: App,
});

const indexRoute = rootRoute.createRoute({ path: '/' });
const listRoute = rootRoute.createRoute({ path: 'lists' });
const singleListRoute = listRoute.createRoute({ path: '$id' });

const routeConfig = rootRoute.addChildren([indexRoute, listRoute.addChildren([singleListRoute])]);

export const router = createReactRouter({ routeConfig });
function App() {
    const [collapsed, setCollapsed] = useState<boolean>(false);
    const {
        token: { colorBgContainer },
    } = theme.useToken();
    return (
        <Layout style={{ minHeight: '100vh' }}>
            <Sider
                collapsible
                collapsed={collapsed}
                onCollapse={(value) => setCollapsed(value)}
            >
                <Navigation />
            </Sider>
            <Layout>
                <Header style={{ padding: 0, background: colorBgContainer }}></Header>
                <Content>
                    <Outlet />
                </Content>
            </Layout>
        </Layout>
    );
}

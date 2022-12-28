import { createReactRouter, createRouteConfig, Outlet, RouterProvider } from '@tanstack/react-router';
import { useState } from 'react';
import { Col, Input, Layout, Menu, Row, Space, theme } from 'antd';
import Navigation from './components/Navigation';
import Lists from './pages/Lists';
import ThemeToggler from './components/ThemeToggler';
import { useStore } from './store/store';
import { TanStackRouterDevtools } from '@tanstack/react-router-devtools';
import { router } from './pages/Routes';
const { Content, Header, Sider, Footer } = Layout;

export default function App() {
    const [collapsed, setCollapsed] = useState<boolean>(false);
    const { adminKey, username, setUsername, setAdminKey } = useStore();
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
                <Header style={{ padding: 0, background: colorBgContainer }}>
                    <Row style={{ padding: '8px' }}>
                        <Space direction="horizontal">
                            <ThemeToggler />
                            <Input
                                placeholder="Username"
                                onChange={(e) => setUsername(e.target.value)}
                                value={username}
                            />
                            <Input
                                placeholder="Admin key"
                                onChange={(e) => setAdminKey(e.target.value)}
                                value={adminKey}
                            />
                        </Space>
                    </Row>
                </Header>
                <Content style={{ padding: '16px 16px' }}>
                    <Outlet />
                    <TanStackRouterDevtools position="bottom-right" router={router}/>
                </Content>
            </Layout>
        </Layout>
    );
}

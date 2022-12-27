import { createReactRouter, createRouteConfig, Outlet, RouterProvider } from '@tanstack/react-router';
import { useState } from 'react';
import { Col, Layout, Menu, Row, Space, theme } from 'antd';
import Navigation from './components/Navigation';
import Lists from './pages/Lists';
import ThemeToggler from './components/ThemeToggler';

const { Content, Header, Sider, Footer } = Layout;

export default function App() {
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
                <Header style={{ padding: 0, background: colorBgContainer }}>
                    <Row style={{padding: "8px"}}>
                        <Col>
                        <ThemeToggler />
                        </Col>
                    </Row>
                </Header>
                <Content style={{ padding: '16px 16px' }}>
                    <Outlet />
                </Content>
            </Layout>
        </Layout>
    );
}

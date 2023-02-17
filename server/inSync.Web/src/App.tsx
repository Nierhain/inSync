import { createReactRouter, createRouteConfig, Outlet, RouterProvider } from '@tanstack/react-router';
import { useState } from 'react';
import { Avatar, Button, Card, Col, Collapse, Input, Layout, Menu, Row, Space, Tag, theme, Typography } from 'antd';
import Navigation from './components/Navigation';
import Lists from './pages/Lists';
import ThemeToggler from './components/ThemeToggler';
import { useStore } from './store/store';
import { TanStackRouterDevtools } from '@tanstack/react-router-devtools';
import { router } from './pages/Routes';
import { useEnvironment, useVersion } from './hooks';
import useInit from './hooks/useInit';
import { useQuery } from '@tanstack/react-query';
import { AdminApi } from './utils/Api';
const { Content, Header, Sider, Footer } = Layout;

const { Title, Text } = Typography;

const envToColor: Partial<Record<string, string>> = {
    Development: 'red',
    Staging: 'orange',
    Production: 'green',
    Loading: 'cyan',
};

export default function App() {
    const [collapsed, setCollapsed] = useState<boolean>(false);
    const { username, setUsername, setIsAdmin, isAdmin } = useStore();
    // const { data } = useVerifyAdmin();
    const [userInput, setUserInput] = useState<string>('');
    const { version } = useVersion();
    const { env } = useEnvironment();
    const {
        token: { colorBgContainer },
    } = theme.useToken();

    return (
        <Layout style={{ minHeight: '100vh' }}>
            <Sider
                collapsible
                collapsed={collapsed}
                onCollapse={(value) => setCollapsed(value)}
                style={{ padding: '64px 0px' }}
            >
                <Navigation />
            </Sider>
            <Layout>
                <Header style={{ padding: 0, background: colorBgContainer }}>
                    <Row
                        justify="space-between"
                        style={{ padding: '8px' }}
                    >
                        <Col>
                            <Space
                                direction="horizontal"
                                align="baseline"
                            >
                                <ThemeToggler />
                            </Space>
                        </Col>
                        {username && (
                            <Col>
                                <Space>
                                    <Avatar
                                        size={32}
                                        style={{ marginTop: '-16px' }}
                                    >
                                        {username[0]}
                                    </Avatar>
                                    <Title level={2}>{username}</Title>
                                </Space>
                            </Col>
                        )}
                    </Row>
                </Header>
                <Content style={{ padding: '16px 16px' }}>
                    {!username && (
                        <Row>
                            <Col span={4}>
                                <Card title="Input your username">
                                    <Input
                                        value={userInput}
                                        onChange={(e) => setUserInput(e.target.value)}
                                    />
                                    <Button onClick={(e) => setUsername(userInput)}>Confirm</Button>
                                </Card>
                            </Col>
                        </Row>
                    )}
                    {username && <Outlet />}
                    <TanStackRouterDevtools
                        position="bottom-right"
                        router={router}
                    />
                </Content>
                <Footer style={{ background: colorBgContainer }}>
                    <Row justify="center">
                        <Text>
                            API-Version: {version} <Tag color={envToColor[env] ?? '#feffe6'}>{env}</Tag>
                        </Text>
                    </Row>
                </Footer>
            </Layout>
        </Layout>
    );
}

import { Alert, Button, Checkbox, Col, Input, Row, Space, Typography } from 'antd';
import { uploadMinecraftItems } from '../hooks';
import { useStore } from '../store/store';
import testData from '../../sampleItems.json';
import { AdminApi } from '../utils/Api';
import React, { useState } from 'react';
import { Response } from '../models';
import { LoadingOutlined } from '@ant-design/icons';
const { Title, Text } = Typography;
export default function () {
    const { adminKey, setAdminKey, setIsAdmin, isAdmin } = useStore();
    const { upload } = uploadMinecraftItems();
    const [isFetching, setIsFetching] = useState<boolean>();
    const [hasFetched, setHasFetched] = useState<boolean>();
    const [verifyResponse, setVerifyResponse] = useState<Response<boolean>>();
    const [saveKey, setSaveKey] = useState<boolean>();
    const verifyKey = async () => {
        setIsFetching(true);
        const res = await AdminApi.verifyAdminKey(adminKey);
        setVerifyResponse(res);
        setIsFetching(false);
        setHasFetched(true);
        setAdminKey(adminKey, saveKey);
        setIsAdmin(res.data);
    };

    const isError = () => {
        return verifyResponse && verifyResponse.errorMessage.length > 0;
    };

    const onKeyChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setAdminKey(e.target.value);
        setIsFetching(false);
        setHasFetched(false);
    };
    return (
        <>
            <Space
                direction="vertical"
                size="large"
            >
                <Row>
                    <Col>
                        <Row>
                            <Col>
                                <Title>Admin key</Title>
                            </Col>
                        </Row>
                        <Row>
                            <Space direction="vertical">
                                <Text>Input your admin key</Text>
                                <Space>
                                    <Input
                                        value={adminKey}
                                        onChange={onKeyChange}
                                        placeholder="adminkey"
                                    />
                                    <Checkbox
                                        checked={saveKey}
                                        onChange={(e) => setSaveKey(e.target.checked)}
                                    >
                                        Save key
                                    </Checkbox>
                                    <Button
                                        type="primary"
                                        onClick={verifyKey}
                                        disabled={isFetching || hasFetched}
                                    >
                                        Verifiy key
                                    </Button>
                                    {isFetching && (
                                        <Alert
                                            type="info"
                                            showIcon
                                            icon={<LoadingOutlined />}
                                        />
                                    )}
                                    {hasFetched && (
                                        <Alert
                                            type={isError() ? 'error' : 'success'}
                                            message={isError() ? 'Error' : 'Successfully verified'}
                                            description={
                                                isError()
                                                    ? verifyResponse?.errorMessage
                                                    : 'Key is successfully verified, you are now authorized to access admin functions! :)'
                                            }
                                        />
                                    )}
                                </Space>
                            </Space>
                        </Row>
                    </Col>
                </Row>
                {isAdmin && (
                    <Row>
                        <Col>
                            <Row>
                                <Col>
                                    <Title>Upload Item names</Title>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Button onClick={() => upload(testData.items)}>Upload Test Items</Button>
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                )}
            </Space>
        </>
    );
}

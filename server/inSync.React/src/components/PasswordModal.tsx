import { useMatch } from '@tanstack/react-router';
import { Input, Modal, Space, Typography } from 'antd';
import { useState } from 'react';
import { useStore } from '../store/store';
const { Title } = Typography;

interface PasswordModalProps {
    open: boolean;
    onClose: () => void;
    onConfirm: () => void;
}
export default function ({ open, onClose, onConfirm }: PasswordModalProps) {
    const [password, setPassword] = useState<string>('');
    const { addPassword, currentId, currentTitle } = useStore();

    const onOk = () => {
        addPassword(currentId, password);
        onConfirm();
    };

    const onCancel = () => {
        setPassword('');
        onClose();
    };

    return (
        <>
            <Modal
                open={open}
                onOk={onOk}
                onCancel={onCancel}
                title="Password"
            >
                <Space direction="vertical">
                    <Title level={4}>
                        Input the password for list "{currentTitle}"({currentId})
                    </Title>
                    <Input
                        placeholder="SuperSecretPhrase"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </Space>
            </Modal>
        </>
    );
}

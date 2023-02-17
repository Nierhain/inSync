import { Alert } from 'antd';

interface ErrorDisplayProps {
    error: unknown;
}
export default function ({ error }: ErrorDisplayProps) {
    return (
        <Alert
            message="Error"
            description={'Error message: ' + error}
            type="error"
            showIcon
        />
    );
}

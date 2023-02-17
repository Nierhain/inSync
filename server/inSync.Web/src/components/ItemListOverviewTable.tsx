import { useMatch } from '@tanstack/react-router';
import { Space, Table, Tag, Typography } from 'antd';
import { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { useState } from 'react';
import { Item, ItemList } from '../models';
import { useStore } from '../store/store';
import PasswordModal from './PasswordModal';

interface ItemListTableProps {
    data: ItemList[];
    type: 'admin' | 'user';
}

const { Link } = Typography;

export default function ({ data, type }: ItemListTableProps) {
    const [open, setOpen] = useState<boolean>(false);
    const { setCurrentId, setCurrentTitle, currentId, currentTitle } = useStore();
    const match = useMatch(type === 'admin' ? '/users/' : '/lists/');
    const columns: ColumnsType<ItemList> = [
        { title: 'Title', dataIndex: 'title', key: 'title' },
        { title: 'Description', dataIndex: 'description', key: 'description' },
        { title: 'Username', dataIndex: 'username', key: 'username' },
        {
            title: 'Active',
            dataIndex: 'isActive',
            key: 'isActive',
            render: (value) => <Tag color={value ? 'success' : 'error'}>{value ? 'Active' : 'Inactive'}</Tag>,
        },
        {
            title: 'Created at',
            dataIndex: 'createdAt',
            key: 'createdAt',
            render: (value) => dayjs(value).format('DD-MM-YYYY'),
        },
        {
            title: 'Actions',
            dataIndex: 'id',
            key: 'actions',
            render: (value: string, record: ItemList) => (
                <Space>
                    <Link
                        onClick={() => {
                            setCurrentId(value);
                            setCurrentTitle(record.title);
                            setOpen(true);
                        }}
                    >
                        Open
                    </Link>
                </Space>
            ),
        },
    ];

    const onConfirm = () => {
        setOpen(false);
        match.navigate({ to: '/lists/$id', params: { id: currentId } });
    };

    const onCancel = () => {
        setOpen(false);
    };
    return (
        <>
            <PasswordModal
                open={open}
                onClose={onCancel}
                onConfirm={onConfirm}
            />
            <Table
                columns={columns}
                dataSource={data}
            ></Table>
        </>
    );
}

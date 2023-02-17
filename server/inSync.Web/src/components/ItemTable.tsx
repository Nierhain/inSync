import { Table } from 'antd';
import { ColumnsType } from 'antd/es/table';
import { MinecraftItem } from '../models';

const columns: ColumnsType<MinecraftItem> = [
    { key: 'resourceKey', title: 'Resource key', dataIndex: 'resourceKey' },
    {
        key: 'ModId',
        title: 'Mod / ModId',
        dataIndex: 'resourceKey',
        render: (value: string) => value.substring(0, value.indexOf(':')),
    },
    { key: 'displayName', dataIndex: 'displayName', title: 'Display name' },
];

interface ItemTableProps {
    data: MinecraftItem[];
}
export default function ({ data }: ItemTableProps) {
    return (
        <>
            <Table
                dataSource={data}
                columns={columns}
            />
        </>
    );
}

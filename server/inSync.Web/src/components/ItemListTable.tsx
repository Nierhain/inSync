import { Table } from 'antd';
import { ColumnsType } from 'antd/es/table';
import { useCallback } from 'react';
import { useMinecraftItems } from '../hooks';
import { Item, ItemList, MinecraftItem } from '../models';
import { useStore } from '../store/store';

interface ItemListTableProps {
    data: ItemList;
}
const prepareColumns = (itemNames: MinecraftItem[]): ColumnsType<Item> => {
    return [
        {
            key: 'name',
            title: 'Name',
            dataIndex: 'resourceKey',
            render: (value: string, record) => {
                return itemNames.find((x) => x.resourceKey == value)?.displayName ?? value;
            },
        },
        { key: 'amount', title: 'Amount', dataIndex: 'amount' },
    ];
};
export default function ({ data }: ItemListTableProps) {
    const { data: itemNames } = useMinecraftItems();
    const getColumns = useCallback(() => prepareColumns(itemNames), [itemNames]);

    return (
        <>
            <Table
                dataSource={data.items}
                columns={getColumns()}
            />
        </>
    );
}

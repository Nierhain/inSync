import { Menu } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { IconHome, IconList, IconSettings, IconUser } from '@tabler/icons';
import { Link } from '@tanstack/react-router';
import React from 'react';
import { useStore } from '../store/store';

const items: ItemType[] = [
    { key: 'home', label: <Link to="/">Home</Link>, icon: <IconHome /> },
    { key: 'myLists', label: <Link to="/lists">My Lists</Link>, icon: <IconList /> },
    { key: 'lists', label: <Link to="/users">User Lists</Link>, icon: <IconUser /> },
    { key: 'settings', label: <Link to="/settings">Settings</Link>, icon: <IconSettings /> },
];

const getMenuItems = (disabledKeys: React.Key[]) => {
    return items.filter((x) => !disabledKeys.includes(x?.key ?? ''));
};

export default function () {
    const {isAdmin} = useStore()
    const disabledKeys = () => {
        if(!isAdmin) {
            return  ['lists']
        }
        return [];
    };
    return (
        <Menu
            theme="dark"
            defaultSelectedKeys={['home']}
            mode="inline"
            items={getMenuItems(disabledKeys())}
        />
    );
}

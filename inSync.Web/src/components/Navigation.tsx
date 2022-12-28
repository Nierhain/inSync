import { Menu } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { IconHome, IconList, IconUser } from '@tabler/icons';
import { Link } from '@tanstack/react-router';

const items: ItemType[] = [
    { key: 'home', label: <Link to="/">Home</Link>, icon: <IconHome /> },
    { key: 'myLists', label: <Link to="/lists">My Lists</Link>, icon: <IconList /> },
    { key: 'lists', label: <Link to="/users">User Lists</Link>, icon: <IconUser />},
    
];

export default function () {
    return (
        <Menu
            theme="dark"
            defaultSelectedKeys={['home']}
            mode="inline"
            items={items}
        />
    );
}

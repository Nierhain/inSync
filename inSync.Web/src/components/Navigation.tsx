import { Menu } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { IconList } from '@tabler/icons';

const items: ItemType[] = [{ key: 'lists', label: 'Lists', icon: <IconList /> }];
export default function () {
    return (
        <Menu
            theme="dark"
            defaultSelectedKeys={['1']}
            mode="inline"
            items={items}
        />
    );
}

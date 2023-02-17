import { useMatch, useParams } from '@tanstack/react-router';
import { Alert } from 'antd';
import ItemListTable from '../components/ItemListTable';
import ItemTable from '../components/ItemTable';
import Spinner from '../components/Spinner';
import { useAdminList, useUserList } from '../hooks';
import { useStore } from '../store/store';
import { singleListRoute } from './Routes';

export default function () {
    const { id } = useParams();
    const { password, isAdmin } = useStore();
    const { data, isLoading, isError, error } = useUserList(id ?? '', password[id ?? '']);
    const { data: adminData, isLoading: adminIsLoading } = useAdminList(id ?? '');
    if (isLoading) return <Spinner />;
    return (
        <>
            <ItemListTable data={isAdmin ? adminData : data} />
        </>
    );
}

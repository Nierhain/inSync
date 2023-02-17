import { Link } from '@tanstack/react-router';
import { Button } from 'antd';
import { useState } from 'react';
import ErrorDisplay from '../components/ErrorDisplay';
import ItemListOverviewTable from '../components/ItemListOverviewTable';
import ItemListTable from '../components/ItemListTable';
import PasswordModal from '../components/PasswordModal';
import Spinner from '../components/Spinner';
import { useUserLists } from '../hooks';
import { useStore } from '../store/store';

export default function () {
    const { username } = useStore();
    const { data, error, isError, isLoading } = useUserLists(username);

    if (isError) return <ErrorDisplay error={error} />;
    if (isLoading) return <Spinner />;

    return (
        <>
            <ItemListOverviewTable
                data={data}
                type="user"
            />
        </>
    );
}

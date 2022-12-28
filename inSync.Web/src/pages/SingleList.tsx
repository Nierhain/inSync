import { useMatch, useParams } from '@tanstack/react-router';
import Spinner from '../components/Spinner';
import { useUserList } from '../hooks';
import { singleListRoute } from './Routes';

export default function () {
    const { id } = useParams();
    const { list, isLoading, isError, error } = useUserList(id ?? "");

    if(isLoading) return <Spinner />;
    return <>Single list</>;
}

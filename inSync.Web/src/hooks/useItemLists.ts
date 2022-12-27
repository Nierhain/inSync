import { QueryFunction, QueryKey, useQuery } from '@tanstack/react-query';
import { useStore } from '../store/store';
import { AdminApi, UserApi } from '../utils/Api';

const queryKeys = {
    itemLists: 'itemLists',
    userLists: 'userLists',
    singleList: 'singleList',
};

const queryBuilder = (queryKey: QueryKey, queryFn: QueryFunction) => {
    const { data, isLoading, isError, error } = useQuery(queryKey, queryFn);
    return { data, isLoading, isError, error };
};

export function useItemLists() {
    const { adminKey } = useStore();
    const query = queryBuilder([queryKeys.itemLists], () => AdminApi.loadLists(adminKey));

    return {
        lists: query.data,
        isLoading: query.isLoading,
        isError: query.isError,
        error: query.error,
    };
}

export function useUserLists(user: string) {
    const query = queryBuilder([queryKeys.userLists, user], () => UserApi.loadListsForUser(user));

    return {
        lists: query.data,
        isLoading: query.isLoading,
        isError: query.isError,
        error: query.error,
    };
}

export function useUserList(id: string) {
    const { username } = useStore();
    const query = queryBuilder([queryKeys.singleList, id], () => UserApi.loadList(id, username));
    return {
        list: query.data,
        isLoading: query.isLoading,
        isError: query.isError,
        error: query.error,
    };
}

export function useAdminList(id: string) {
    const { adminKey } = useStore();
    const query = queryBuilder([queryKeys.singleList, id], () => AdminApi.loadList(id, adminKey));

    return {
        list: query.data,
        isLoading: query.isLoading,
        isError: query.isError,
        error: query.error
    };
}

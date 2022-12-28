import { QueryFunction, QueryFunctionContext, QueryKey, useQuery } from '@tanstack/react-query';
import { useStore } from '../store/store';
import { AdminApi, UserApi } from './Api';

export const queryBuilder = (queryKey: QueryKey, queryFn: QueryFunction, enabled = true) => {
    const { data, isLoading, isError, error } = useQuery({ queryKey: queryKey, queryFn: queryFn, enabled: enabled });
    return { data, isLoading, isError, error };
};

export const getUserLists = ({ queryKey }: QueryFunctionContext) => {
    const [_type, user] = queryKey as string[];
    return UserApi.loadListsForUser(user);
};
export const getUserList = ({ queryKey }: QueryFunctionContext) => {
    let [_type, id, password] = queryKey as string[];
    UserApi.loadList(id, password ?? useStore.getState().password[id]);
};
export const getCurrentUserLists = () => UserApi.loadListsForUser(useStore.getState().username);

export const getAdminLists = () => AdminApi.loadLists(useStore.getState().adminKey);

export const getListForAdmin = ({ queryKey }: QueryFunctionContext) => {
    const [_type, id] = queryKey as string[];
    AdminApi.loadList(id, useStore.getState().adminKey);
};

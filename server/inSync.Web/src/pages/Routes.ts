import { createReactRouter, createRouteConfig } from '@tanstack/react-router';
import App from '../App';
import { queryClient } from '../components/Provider';
import Spinner from '../components/Spinner';
import { queryKeys } from '../hooks';
import { useStore } from '../store/store';
import { getUserList, getUserLists } from '../utils/QueryFunctions';
import Home from './Home';
import Items from './Items';
import Lists from './Lists';
import Settings from './Settings';
import SingleList from './SingleList';
import UserLists from './UserLists';

const rootRoute = createRouteConfig({ component: App });

const indexRoute = rootRoute.createRoute({ path: '/', component: Home, pendingComponent: Spinner });
const listRoute = rootRoute.createRoute({ path: 'lists', pendingComponent: Spinner });

export const listIndex = listRoute.createRoute({
    path: '/',
    component: Lists,
    loader: async () => {
        queryClient.getQueryData([queryKeys.userList]) ??
            (await queryClient.prefetchQuery([queryKeys.userList, useStore.getState().username], getUserLists));
    },
    pendingComponent: Spinner,
});

export const singleListRoute = listRoute.createRoute({
    path: '$id',
    parseParams: ({ id }) => ({ id }),
    stringifyParams: ({ id }) => ({ id: `${id}` }),
    component: SingleList,
    loader: async ({ params: { id } }) => {
        queryClient.getQueryData([queryKeys.userList, id]) ??
            (await queryClient.prefetchQuery([queryKeys.userList, id], getUserList));
        return { id };
    },
    pendingComponent: Spinner,
});

const userRoute = rootRoute.createRoute({ path: 'users', pendingComponent: Spinner });
const userIndex = userRoute.createRoute({ path: '/', pendingComponent: Spinner, component: UserLists });
const userListRoute = userRoute.createRoute({ path: '$id', pendingComponent: Spinner, component: SingleList });

const settingsRoute = rootRoute.createRoute({ path: 'settings', pendingComponent: Spinner });
const settingsIndex = settingsRoute.createRoute({ path: '/', component: Settings, pendingComponent: Spinner });

const itemsRoute = rootRoute.createRoute({ path: 'items', pendingComponent: Spinner });
const itemsIndex = itemsRoute.createRoute({ path: '/', component: Items, pendingComponent: Spinner });

const routeConfig = rootRoute.addChildren([
    indexRoute,
    listRoute.addChildren([listIndex, singleListRoute]),
    userRoute.addChildren([userIndex, userListRoute]),
    settingsRoute.addChildren([settingsIndex]),
    itemsRoute.addChildren([itemsIndex]),
]);
export const router = createReactRouter({ routeConfig });

declare module '@tanstack/react-router' {
    interface RegisterRouter {
        router: typeof router;
    }
}

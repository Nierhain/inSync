import { createReactRouter, createRouteConfig } from '@tanstack/react-router';
import App from '../App';
import { queryClient } from '../components/Provider';
import { queryKeys } from '../hooks';
import { useStore } from '../store/store';
import { getUserList } from '../utils/QueryFunctions';
import Home from './Home';
import Lists from './Lists';
import SingleList from './SingleList';

const rootRoute = createRouteConfig({ component: App });

const indexRoute = rootRoute.createRoute({ path: '/', component: Home });
const listRoute = rootRoute.createRoute({ path: 'lists' });
export const listIndexRoute = listRoute.createRoute({
    path: '/',
    component: Lists,
    loader: async () => {
        queryClient.getQueryData([queryKeys.userList]) ?? (await queryClient.prefetchQuery([queryKeys.userList, useStore.getState().username], ));
    },
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
});

const userRoute = rootRoute.createRoute({ path: 'users' });
const userIndexRoute = userRoute.createRoute({ path: '/' });
const userListRoute = userRoute.createRoute({ path: '$id' });

const settingsRoute = rootRoute.createRoute({ path: 'settings' });

const routeConfig = rootRoute.addChildren([
    indexRoute,
    listRoute.addChildren([listIndexRoute, singleListRoute]),
    userRoute.addChildren([userIndexRoute, userListRoute]),
    settingsRoute.addChildren([]),
]);
export const router = createReactRouter({ routeConfig });

declare module '@tanstack/react-router' {
    interface RegisterRouter {
      router: typeof router
    }
  }
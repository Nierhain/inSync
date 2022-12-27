import { createReactRouter, createRouteConfig } from "@tanstack/react-router";
import App from "../App";
import Lists from "./Lists";

const rootRoute = createRouteConfig({component: App});

const indexRoute = rootRoute.createRoute({ path: '/', component: Lists });
const listRoute = rootRoute.createRoute({ path: '/lists', component: Lists });
const singleListRoute = listRoute.createRoute({ path: '$id' });

const routeConfig = rootRoute.addChildren([indexRoute, listRoute.addChildren([singleListRoute])]);
export const router = createReactRouter({ routeConfig });
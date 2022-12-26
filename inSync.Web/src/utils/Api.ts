const baseUrl = '/api';
const userUrl = baseUrl + '/lists';
const adminUrl = baseUrl + '/admin';

export const UserApi = {
    loadLists: () => fetch(baseUrl + '/lists').then(),
    loadListsForUser: (username: string) => fetch(baseUrl + '/lists?username=' + username).then(),
};

export const AdminApi = {
    loadLists: (adminKey: string) => fetch(adminUrl + "/lists?adminKey=" + adminKey).then(),
    loadListsForUser: (username: string) => fetch(adminUrl + "/lists/" + username).then()
};



import { useQuery } from '@tanstack/react-query';
import { useStore } from '../store/store';
import { AdminApi } from '../utils/Api';

export default function () {
    const { setIsAdmin, isAdmin } = useStore();
    const verifyAdmin = () => {

    };
    //load any important data

    return {verifyAdmin};
}

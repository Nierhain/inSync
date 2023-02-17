import { useQuery } from '@tanstack/react-query';
import { MetaApi } from '../utils/Api';

export function useVersion() {
    const { data, isLoading } = useQuery(['version'], MetaApi.getVersion);
    return {
        version: data ? data : '0.0.0-loading',
        isLoading: isLoading,
    };
}

export function useEnvironment() {
    const { data, isLoading } = useQuery(['environment'], MetaApi.getEnvironment);
    return {
        env: data ? data : 'Loading',
    };
}

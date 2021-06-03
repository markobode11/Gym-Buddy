import Axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IFetchResponse } from '../types/IFetchResponse';
import { IMessage } from '../types/IMessage';

export abstract class BaseService {
    protected static axios = Axios.create({
        baseURL: ApiBaseUrl,
        headers: {
            'Content-Type': 'application/json'
        }
    });

    protected static getAxiosConfiguration(token: string | undefined): AxiosRequestConfig | undefined {
        if (!token) return undefined;
        const config: AxiosRequestConfig = {
            headers: {
                Authorization: 'Bearer ' + token
            }
        };
        return config;
    }

    static async getAll<TEntity>(apiEndpoint: string, token?: string): Promise<IFetchResponse<TEntity[]>> {
        try {
            const response = await this.axios.get<TEntity[]>(apiEndpoint, BaseService.getAxiosConfiguration(token));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data
            };
        }
        catch (err) {
            const error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 0,
                message: error.response?.data.messages,
            }
        }
    }

    static async get<TEntity>(apiEndpoint: string, token?: string): Promise<IFetchResponse<TEntity>> {
        try {
            const response = await this.axios.get<TEntity>(apiEndpoint, BaseService.getAxiosConfiguration(token));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data
            };
        }
        catch (err) {
            const error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 0,
                message: error.response?.data.messages,
            }
        }
    }

    static async create<TReturnEntity, TBodyEntity = TReturnEntity>(body: TBodyEntity, apiEndpoint: string, token?: string): Promise<IFetchResponse<TReturnEntity>> {
        try {
            const response = await this.axios.post<TReturnEntity>(
                apiEndpoint,
                JSON.stringify(body),
                BaseService.getAxiosConfiguration(token));

            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data
            };
        } catch (err) {
            const error = err as AxiosError;
            return {
                ok: false,
                statusCode: 0,
                message: error.response?.data.messages,
            };
        }
    }

    static async edit<TEntity>(body: TEntity, apiEndpoint: string, token?: string): Promise<IFetchResponse<TEntity>> {
        try {
            const response = await this.axios.put<TEntity>(
                apiEndpoint,
                JSON.stringify(body),
                BaseService.getAxiosConfiguration(token));

            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data
            };
        } catch (err) {
            const error = err as AxiosError;
            return {
                ok: false,
                statusCode: 0,
                message: error.response?.data.messages,
            };
        }
    }

    static async delete(apiEndpoint: string, token?: string): Promise<IFetchResponse<IMessage>> {
        try {
            const response = await this.axios.delete(apiEndpoint, BaseService.getAxiosConfiguration(token))

            return {
                ok: response.status <= 299,
                statusCode: response.status,
                message: response.statusText
            };
        } catch (reason) {
            return {
                ok: false,
                statusCode: 0,
                message: reason.response?.data.messages,
            };
        }
    }
}

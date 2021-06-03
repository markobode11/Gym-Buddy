import { ILoginResponse } from './../types/ILoginResponse';
import Axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IFetchResponse } from '../types/IFetchResponse';
import { IMessage } from '../types/IMessage';
import { IUpdateUser } from '../domain/IUser';

export abstract class IdentityService {
    protected static axios = Axios.create({
        baseURL: ApiBaseUrl,
        headers: {
            'Content-Type': 'application/json'
        }
    });

    static async Login(apiEndpoint: string, loginData: { email: string, password: string }): Promise<IFetchResponse<ILoginResponse>> {
        let loginDataJson = JSON.stringify(loginData);
        try {
            const response = await this.axios.post<ILoginResponse>(apiEndpoint, loginDataJson);
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data,
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

    static async Register(apiEndpoint: string, registerData:
        { email: string, password: string, firstname: string, lastname: string }):
        Promise<IFetchResponse<ILoginResponse>> {
        const registerDataJson = JSON.stringify(registerData);
        try {
            const response = await this.axios.post<ILoginResponse>(apiEndpoint, registerDataJson);
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
                message: error.response?.data.detail,
            }
        }

    }

    static async Update(apiEndpoint: string, updateData: IUpdateUser, token: string): Promise<IFetchResponse<IMessage>> {
        let updateDataJson = JSON.stringify(updateData);
        const config: AxiosRequestConfig = {
            headers: {
                Authorization: 'Bearer ' + token
            }
        };
        try {
            const response = await this.axios.post<IUpdateUser>(apiEndpoint, updateDataJson, config);

            return {
                ok: response.status <= 299,
                statusCode: response.status,
            };
        }
        catch (err) {
            const error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 0,
                message: error.response?.data.detail,
            }
        }

    }

    static async RemoveRole(apiEndpoint: string, token: string): Promise<IFetchResponse<IMessage>> {
        const config: AxiosRequestConfig = {
            headers: {
                Authorization: 'Bearer ' + token
            }
        };
        try {
            const response = await this.axios.delete(apiEndpoint, config);

            return {
                ok: response.status <= 299,
                statusCode: response.status,
            };
        }
        catch (err) {
            const error = err as AxiosError;
            console.log(error)
            return {
                ok: false,
                statusCode: error.response?.status ?? 0,
                message: error.response?.data.detail,
            }
        }
    }

}

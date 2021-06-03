import { EGender } from "./Enums/EGender";

export interface IUser {
    id: number,
    firstname: string,
    lastname: string,
    userSince: Date,
    heightInCm: number,
    weightInKg: number,
    gender: EGender,
    email: string
}

export interface IUpdateUser {
    id: number,
    firstname: string,
    lastname: string,
    email: string,
    oldPassword?: string,
    newPassWord?: string
}

export interface IUserWithRoleAndLockOut {
    id: number,
    firstname: string,
    lastname: string,
    email: string,
    roleName: string | null,
    isLockedOut: boolean,
    lockoutEndDateTime: Date | null
}

export interface IUserWithRole {
    id: number,
    email: string,
    roleName: string | null
}
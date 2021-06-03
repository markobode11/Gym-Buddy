import { IUser } from "./IUser";

export interface IMentor {
    id: number
    fullName: string
    specialty: string,
    description: string,
    since: Date,
    email: string,
    mentorUsers: IUser[]
}

export interface ICreateMentor {
    fullName: string
    specialty: string,
    description: string,
    email: string,
    firstname: string,
    lastname: string,
    password: string,
    since: Date
}

export const InitialMentor: IMentor = {
    id: 0,
    fullName: '',
    description: '',
    specialty: '',
    since: new Date() ,
    email: '',
    mentorUsers: []
}
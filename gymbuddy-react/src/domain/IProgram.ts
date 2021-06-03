import { ISplit } from "./ISplit";

export interface IProgram {
    id: number
    name: string
    description: string,
    goal: string,
    splitsInProgram: ISplit[]
}

export const InitialProgram: IProgram = {
    id: 0,
    name: '',
    description: '',
    goal: '',
    splitsInProgram: []
}
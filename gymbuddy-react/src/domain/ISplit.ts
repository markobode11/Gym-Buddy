import { IWorkout } from "./IWorkout";

export interface ISplit {
    id: number
    name: string
    description: string,
    workoutsInSplit: IWorkout[]
}

export const InitialSplit: ISplit = {
    id: 0,
    name: '',
    description: '',
    workoutsInSplit: []
}
import { IDifficulty } from "./IDifficulty";
import { IExercise } from "./IExercise";

export interface IWorkout {
    id: number
    name: string
    description: string,
    duration: string,
    difficultyId: number,
    difficulty: IDifficulty | null,
    exercisesInWorkout: IExercise[]
}

export const InitialWorkout: IWorkout = {
    id: 0,
    name: '',
    description: '',
    duration: '',
    difficulty: null,
    difficultyId: 0,
    exercisesInWorkout: []
}
import { IDifficulty } from "./IDifficulty";
import { IMuscle } from "./IMuscle";

export interface IExercise {
    id: number
    name: string
    description: string
    difficultyId: number,
    difficulty: IDifficulty | null,
    musclesTrainedInExercise: IMuscle[]
}

export const InitialExercise: IExercise = {
    id: 0,
    name: '',
    description: '',
    difficulty: null,
    difficultyId: 0,
    musclesTrainedInExercise: []
}
import React from 'react'

export interface IAppState {
    token: string | null
    firstname: string
    lastname: string
    isAdmin: boolean
    isMentor: boolean

    setAuthInfo: (token: string, firstname: string, lastname: string) => void
    logOut: () => void
}

export const initialAppState: IAppState = {
    token: null,
    firstname: '',
    lastname: '',
    isAdmin: false,
    isMentor: false,
    setAuthInfo: (): void => {},
    logOut: (): void => {}
}

export const AppContext = React.createContext<IAppState>(initialAppState);
export const AppContextProvider = AppContext.Provider
export const AppContextConsumer = AppContext.Consumer

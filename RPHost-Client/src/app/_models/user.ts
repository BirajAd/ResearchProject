import { Photo } from './Photo';

export interface User {
    id: number;
    username: string;
    firstName: string; //
    lastName: string; //
    gender: string;
    age: number;
    created: Date;
    lastActive: Date;
    country: string; 
    photoPath: string; //
    city?: string; //
    bio?: string; //
    fieldOfInterest?: string; //
    photos?: Photo[]; //
}

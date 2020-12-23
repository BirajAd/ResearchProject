import { Photo } from './photo';

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
    fieldOfInterests?: string; //
    institute?: string;
    photos?: Photo[]; //
}

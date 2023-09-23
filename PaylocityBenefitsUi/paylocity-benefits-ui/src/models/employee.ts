export interface Person {
    id: number;
    firstName: string;
    lastName: string;
    dateOfBirth: Date;
    profileUrl?: string;
}

export interface Dependent extends Person {
    relationship: number;
}

export interface Employee extends Person {
    salary: number;
    dependents: Dependent[];
}
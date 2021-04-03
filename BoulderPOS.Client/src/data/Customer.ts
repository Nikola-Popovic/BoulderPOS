import { CustomerEntries } from "./CustomerEntries";
import { CustomerSubscription } from "./CustomerSubscription";

export interface Customer {
    id : number,
    firstName : string,
    lastName : string,
    email : string,
    phoneNumber : string,
    birthDate: Date,
    picturePath : string,
    entries : CustomerEntries | null,
    subscription : CustomerSubscription | null
}
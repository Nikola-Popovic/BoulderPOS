import { AxiosResponse } from 'axios';
import { get, put } from '../../helper/axios';
import { Customer } from '../../data';

export const CustomerService = {
    getCustomers: (phoneNumber : string) => get<string, AxiosResponse<Customer[]>>(`/customers/search?phoneNumber=${phoneNumber}`),
    getCustomer: (clientId : string) => get<string, AxiosResponse<Customer>>(`/customers/${clientId}`),
    checkinCustomer: (clientId : string) => put<string, AxiosResponse<boolean>>(`/customers/${clientId}/checkin`, ''),
};
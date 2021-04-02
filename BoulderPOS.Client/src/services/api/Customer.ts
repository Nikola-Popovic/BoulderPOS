import { AxiosResponse } from 'axios';
import { get, put } from '../../helper/axios';
import { Customer } from '../../data';

export const CustomerService = {
    getCustomers: (phoneNumber : String) => get<string, AxiosResponse<Customer[]>>(`/customers/search?phoneNumber=${phoneNumber}`),
    getCustomer: (clientId : String) => get<string, AxiosResponse<Customer>>(`/customers/${clientId}`),
    checkinCustomer: (clientId : String) => put<string, AxiosResponse<boolean>>(`/customers/${clientId}/checkin`, ''),
};
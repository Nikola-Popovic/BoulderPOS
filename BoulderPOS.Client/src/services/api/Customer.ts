import { AxiosResponse } from 'axios';
import { get } from '../../helper/axios';
import { Customer } from '../../data';

export const CustomerService = {
    getCustomers: (phoneNumber : String) => get<string, AxiosResponse<Customer[]>>(`/customers/search?phoneNumber=${phoneNumber}`),
    getCustomer: (clientId : String) => get<string, AxiosResponse<Customer>>(`/customers/${clientId}`)
};
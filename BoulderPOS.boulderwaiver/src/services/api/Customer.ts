import { AxiosResponse } from 'axios';
import { post, get } from '../../helper/axios';
import { NewCustomer, CreatedCustomer } from '../../payload';

export const CustomerService = {
    postNewCustomer: (params : NewCustomer) => post<NewCustomer, AxiosResponse<CreatedCustomer>>('/customers', params),
    getCustomer: (phoneNumber : String) => get<string, AxiosResponse<CreatedCustomer>>(`/customers/customer?phoneNumber${phoneNumber}`)
};
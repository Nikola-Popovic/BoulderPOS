import { AxiosResponse } from 'axios';
import { post } from '../../helper/axios';
import { NewCustomer, CreatedCustomer } from '../../payload';

export const CustomerService = {
    postNewCustomer: (params : NewCustomer) => post<NewCustomer, AxiosResponse<CreatedCustomer>>('/customers', params),
};
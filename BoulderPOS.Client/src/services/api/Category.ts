import { AxiosResponse } from 'axios';
import { get, put, post, destroy } from '../../helper/axios';
import { ProductCategory, CategoryToCreate } from '../../data';

export const CategoryService = {
    getCategories: () => get<string, AxiosResponse<ProductCategory[]>>('/categories'),
    getCategory: (categoryId : String) => get<string, AxiosResponse<ProductCategory>>(`/categories/${categoryId}`),
    updateCategory: (productCategory : ProductCategory) => put<string, AxiosResponse<boolean>>(`/categories/${productCategory.id}`, productCategory),
    postCategory: (toCreate : CategoryToCreate) => post<string, AxiosResponse<ProductCategory>>('/categories', toCreate),
    deleteCategory: (categoryId : number) => destroy<string, AxiosResponse>(`/categories/${categoryId}`),
};
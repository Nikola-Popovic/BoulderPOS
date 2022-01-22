import { AxiosResponse } from 'axios';
import { get, put, post, destroy } from '../../helper/axios';
import { ProductCategory, CategoryToCreate, Product } from '../../data';

export const CategoryService = {
    getCategories: () => get<string, AxiosResponse<ProductCategory[]>>('/categories'),
    getCategory: (categoryId : string) => get<string, AxiosResponse<ProductCategory>>(`/categories/${categoryId}`),
    getProductsByCategory: (categoryId : number) => get<string, AxiosResponse<Product[]>>(`/categories/${categoryId}/products`),
    updateCategory: (productCategory : ProductCategory) => put<string, AxiosResponse<boolean>>(`/categories/${productCategory.id}`, productCategory),
    updateCategories: (productCategories : ProductCategory[]) => put<string, AxiosResponse>(`/categories/updateOrder`, productCategories),
    postCategory: (toCreate : CategoryToCreate) => post<string, AxiosResponse<ProductCategory>>('/categories', toCreate),
    deleteCategory: (categoryId : number) => destroy<string, AxiosResponse>(`/categories/${categoryId}`),
};
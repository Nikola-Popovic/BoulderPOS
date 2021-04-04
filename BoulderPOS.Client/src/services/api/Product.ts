import { AxiosResponse } from 'axios';
import { get, put, post, destroy } from '../../helper/axios';
import { Product, ProductToCreate } from '../../data';

export const ProductService = {
    getProducts: () => get<string, AxiosResponse<Product[]>>('/products'),
    getProduct: (productId : String) => get<string, AxiosResponse<Product>>(`/products/${productId}`),
    updateProduct: (product : Product) => put<string, AxiosResponse<boolean>>(`/products/${product.id}`, product),
    postProduct: (toCreate : ProductToCreate) => post<string, AxiosResponse<Product>>('/products', toCreate),
    deleteProduct: (productId : number) => destroy<string, AxiosResponse>(`/products/${productId}`),
};
import api from "./api";
import type { Product } from "../interfaces/Product";
import type { GenerateProductsResponse } from "../interfaces/GenerateProductsResponse";
import type { PagedResponse } from "../interfaces/PagedResponse";

export async function getProducts(
    page: number = 1,
    pageSize: number = 10,
    search?: string,
    categoryID?: number
): Promise<PagedResponse<Product>> {

    const params = new URLSearchParams();

    params.append("Page", page.toString());
    params.append("PageSize", pageSize.toString());

    if (search) {
        params.append("Search", search);
    }

    if (categoryID) {
        params.append("CategoryID", categoryID.toString());
    }

    const response = await api.get<PagedResponse<Product>>(
        `/Product?${params.toString()}`
    );

    return response.data;
}

export async function generateProducts(
    quantity: number
): Promise<GenerateProductsResponse> {

    const response = await api.post<GenerateProductsResponse>(
        `/Product/generate?quantity=${quantity}`
    );

    return response.data;
}

export async function deleteProduct(productID: number): Promise<void> {

    await api.delete(`/Product/${productID}`);

}

export async function updateProduct(
    product: Product
): Promise<void> {

    await api.put(
        `/Product/${product.productID}`,
        {
            productName: product.productName,
            unitPrice: product.unitPrice,
            unitsInStock: product.unitsInStock,
            discontinued: product.discontinued,
            categoryID: product.categoryID
        }
    );

}
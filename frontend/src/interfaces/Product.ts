export interface Product {
    productID: number;
    productName: string;
    unitPrice: number;
    unitsInStock: number;
    discontinued: boolean;
    categoryID: number;
    categoryName: string;
    categoryPicture: string;
}
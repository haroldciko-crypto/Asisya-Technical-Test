import api from "./api";
import type { Category } from "../interfaces/Category";

export async function getCategories(): Promise<Category[]> {

    const response = await api.get<Category[]>("/Category");

    return response.data;

}
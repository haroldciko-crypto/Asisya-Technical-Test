import api from "./api";
import type { LoginRequest } from "../interfaces/LoginRequest";
import type { LoginResponse } from "../interfaces/LoginResponse";

export async function login(
    request: LoginRequest
): Promise<LoginResponse> {

    const response = await api.post<LoginResponse>(
        "/Auth/login",
        request
    );

    return response.data;
}
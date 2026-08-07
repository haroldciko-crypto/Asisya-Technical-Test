import { useState } from "react";
import { login } from "../../services/authService";
import { useNavigate } from "react-router-dom";

function Login() {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();

    const handleLogin = async () => {

        try {

            setLoading(true);

            const response = await login({
                username,
                password
            });

            localStorage.setItem("token", response.token);
            localStorage.setItem("username", response.username);
            localStorage.setItem("role", response.role);

            navigate("/dashboard");

        } catch (error) {

            console.error(error);

            alert("Usuario o contraseña incorrectos");

        } finally {

            setLoading(false);

        }

    };

    return (

        <div className="min-h-screen bg-gradient-to-br from-emerald-50 to-white flex items-center justify-center px-4">

            <div className="bg-white shadow-2xl rounded-3xl w-full max-w-md p-10">

                <div className="text-center">

                    <h1 className="text-4xl font-bold text-emerald-600">

                        Asisya Products

                    </h1>

                    <p className="text-gray-500 mt-3">

                        Inicia sesión para continuar

                    </p>

                </div>

                <div className="mt-8 space-y-5">

                    <div>

                        <label className="block mb-2 font-medium text-gray-700">

                            Usuario

                        </label>

                        <input
                            type="text"
                            placeholder="Ingrese su usuario"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            className="w-full border border-gray-300 rounded-xl px-4 py-3 focus:outline-none focus:ring-2 focus:ring-emerald-500"
                        />

                    </div>

                    <div>

                        <label className="block mb-2 font-medium text-gray-700">

                            Contraseña

                        </label>

                        <input
                            type="password"
                            placeholder="Ingrese su contraseña"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className="w-full border border-gray-300 rounded-xl px-4 py-3 focus:outline-none focus:ring-2 focus:ring-emerald-500"
                        />

                    </div>

                    <button
                        onClick={handleLogin}
                        disabled={loading}
                        className="w-full bg-emerald-600 hover:bg-emerald-700 text-white font-semibold py-3 rounded-xl transition disabled:opacity-50 disabled:cursor-not-allowed"
                    >

                        {
                            loading
                                ? "Ingresando..."
                                : "Iniciar Sesión"
                        }

                    </button>

                </div>

                <div className="mt-8 text-center text-sm text-gray-400">

                    <p>.NET 8 • React • PostgreSQL • JWT</p>

                </div>

            </div>

        </div>

    );

}

export default Login;
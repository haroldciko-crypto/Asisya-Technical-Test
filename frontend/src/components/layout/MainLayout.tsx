import type { ReactNode } from "react";
import { useNavigate } from "react-router-dom";

interface MainLayoutProps {
    children: ReactNode;
}

function MainLayout({ children }: MainLayoutProps) {

    const navigate = useNavigate();

    const username = localStorage.getItem("username");

    function logout() {

        localStorage.clear();

        navigate("/login");
    }

    return (

        <div className="min-h-screen bg-slate-100">

            <header className="bg-emerald-700 text-white shadow">

                <div className="max-w-7xl mx-auto flex justify-between items-center px-8 py-4">

                    <div>

                        <h1 className="text-2xl font-bold">
                            Inventory Management
                        </h1>

                        <p className="text-sm text-emerald-100">
                            Product Management System
                        </p>

                    </div>

                    <div className="flex items-center gap-5">

                        <span className="font-medium">
                            👤 {username}
                        </span>

                        <button
                            onClick={logout}
                            className="bg-white text-emerald-700 px-4 py-2 rounded-lg hover:bg-gray-100 transition"
                        >
                            Logout
                        </button>

                    </div>

                </div>

            </header>

            <main className="max-w-7xl mx-auto p-8">

                {children}

            </main>

        </div>

    );

}

export default MainLayout;
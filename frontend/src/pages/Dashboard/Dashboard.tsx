import { useEffect, useState } from "react";
import { getProducts, generateProducts } from "../../services/productService";
import type { Product } from "../../interfaces/Product";
import type { GenerateProductsResponse } from "../../interfaces/GenerateProductsResponse";
import MainLayout from "../../components/layout/MainLayout";
//import StatCard from "../../components/dashboard/StatCard";
import ProductTable from "../../components/dashboard/ProductTable";
import type { Category } from "../../interfaces/Category";
import { getCategories } from "../../services/categoryService";
import { deleteProduct } from "../../services/productService";
import { updateProduct } from "../../services/productService";



function Dashboard() {

    const [products, setProducts] = useState<Product[]>([]);
    const [quantity, setQuantity] = useState(10);
    const [page, setPage] = useState(1);
    const [result, setResult] = useState<GenerateProductsResponse | null>(null);
    const [search, setSearch] = useState("");
    const [totalPages, setTotalPages] = useState(1);
    const [loadingGenerate, setLoadingGenerate] = useState(false);
    const [categories, setCategories] = useState<Category[]>([]);
    const [categoryID, setCategoryID] = useState<number | undefined>();
    const [editingProduct, setEditingProduct] = useState<Product | null>(null);
    const [editName, setEditName] = useState("");
    const [editPrice, setEditPrice] = useState(0);
    const [editStock, setEditStock] = useState(0);

    useEffect(() => {
        loadProducts();
    }, [page, search, categoryID]);

    useEffect(() => {
        loadCategories();
    }, []);

    async function loadProducts() {

        try {

            const data = await getProducts(page, 10, search, categoryID);

            setProducts(data.items);

            setTotalPages(data.totalPages);

        } catch (error) {

            console.error(error);

        } 

    }

    async function loadCategories() {

        try {

            const data = await getCategories();

            setCategories(data);

        } catch (error) {

            console.error(error);

        }

    }

    async function handleGenerate() {

        try {

            setLoadingGenerate(true);

            const response = await generateProducts(quantity);

            setResult(response);

            setPage(1);

            await loadProducts();

        } catch (error) {

            console.error(error);

        } finally {

            setLoadingGenerate(false);

        }

    }

    async function handleDelete(id: number) {

        const confirmed = confirm(
            "¿Desea eliminar este producto?"
        );

        if (!confirmed)
            return;

        try {

            await deleteProduct(id);

            await loadProducts();

        } catch (error) {

            console.error(error);

            alert("No fue posible eliminar el producto.");

        }

    }

    function handleEdit(product: Product) {

        setEditingProduct(product);

        setEditName(product.productName);

        setEditPrice(product.unitPrice);

        setEditStock(product.unitsInStock);

    }

    async function handleSaveEdit() {

        if (!editingProduct)
            return;

        try {

            await updateProduct({

                ...editingProduct,

                productName: editName,

                unitPrice: editPrice,

                unitsInStock: editStock

            });

            setEditingProduct(null);

            await loadProducts();

        } catch (error) {

            console.error(error);

            alert("No fue posible actualizar el producto.");

        }

    }

    return (
        <MainLayout>

            <div className="max-w-7xl mx-auto space-y-8">

                <div className="bg-white rounded-2xl shadow-md p-8">

                    <h1 className="text-4xl font-bold text-emerald-600">
                        Dashboard
                    </h1>

                    <p className="text-gray-500 mt-2">
                        Bienvenido, <strong>{localStorage.getItem("username")}</strong>.
                    </p>

                    <p className="text-gray-500">
                        Gestiona y genera productos desde este panel.
                    </p>

                </div>

                <div className="bg-white rounded-xl shadow-md p-6">

                    <h2 className="text-xl font-semibold mb-5">
                        Generar Productos
                    </h2>

                    <div className="flex flex-col md:flex-row gap-4">

                        <input
                            type="number"
                            value={quantity}
                            onChange={(e) => setQuantity(Number(e.target.value))}
                            className="border rounded-lg px-4 py-2 md:w-48 focus:outline-none focus:ring-2 focus:ring-emerald-500"
                        />

                        <button
                            onClick={handleGenerate}
                            disabled={loadingGenerate}
                            className="bg-emerald-600 hover:bg-emerald-700 text-white px-6 py-2 rounded-lg transition disabled:opacity-50 disabled:cursor-not-allowed"
                        >

                            {
                                loadingGenerate ? (

                                    <span className="flex items-center gap-2">

                                        <span className="w-5 h-5 border-2 border-white border-t-transparent rounded-full animate-spin"></span>

                                        Generando...

                                    </span>

                                ) : (

                                    "Generar Productos"

                                )
                            }

                        </button>

                    </div>

                    {
                        result && (

                            <div className="mt-6 bg-emerald-50 border border-emerald-300 rounded-xl p-5">

                                <h3 className="font-semibold text-emerald-700 mb-3">
                                    Última generación
                                </h3>

                                <p>
                                    ✅ {result.message}
                                </p>

                                <p className="mt-2">
                                    <strong>Productos insertados:</strong> {result.inserted}
                                </p>

                                <p>
                                    <strong>Tiempo:</strong> {result.elapsedSeconds} segundos
                                </p>

                            </div>

                        )
                    }

                </div>

                <div className="bg-white rounded-xl shadow-md p-6">

                    <h2 className="text-xl font-semibold mb-4">
                        Buscar productos
                    </h2>

                    <input
                        type="text"
                        placeholder="Buscar por nombre..."
                        value={search}
                        onChange={(e) => {
                            setPage(1);
                            setSearch(e.target.value);
                        }}
                        className="w-full border rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-emerald-500"
                    />

                    <select
                        value={categoryID ?? ""}
                        onChange={(e) => {

                            setPage(1);

                            setCategoryID(
                                e.target.value === ""
                                    ? undefined
                                    : Number(e.target.value)
                            );

                        }}
                        className="w-full mt-4 border rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-emerald-500"
                    >

                        <option value="">
                            Todas las categorías
                        </option>

                        {
                            categories.map(category => (

                                <option
                                    key={category.categoryID}
                                    value={category.categoryID}
                                >
                                    {category.categoryName}
                                </option>

                            ))
                        }

                    </select>

                </div>

                <ProductTable products={products} onDelete={handleDelete} onEdit={handleEdit} />

                {
                    editingProduct && (

                        <div className="bg-white rounded-xl shadow-md p-6 mt-8">

                            <h2 className="text-xl font-semibold mb-5">

                                Editar producto

                            </h2>

                            <div className="grid md:grid-cols-3 gap-4">

                                <div>

                                    <label className="block text-sm font-medium text-gray-700 mb-2">
                                        Producto
                                    </label>

                                    <input
                                        value={editName}
                                        onChange={(e) => setEditName(e.target.value)}
                                        className="w-full border rounded-lg px-4 py-2"
                                    />

                                </div>

                                <div>

                                    <label className="block text-sm font-medium text-gray-700 mb-2">
                                        Precio
                                    </label>

                                    <input
                                        type="number"
                                        value={editPrice}
                                        onChange={(e) => setEditPrice(Number(e.target.value))}
                                        className="w-full border rounded-lg px-4 py-2"
                                    />

                                </div>

                                <div>

                                    <label className="block text-sm font-medium text-gray-700 mb-2">
                                        Stock
                                    </label>

                                    <input
                                        type="number"
                                        value={editStock}
                                        onChange={(e) => setEditStock(Number(e.target.value))}
                                        className="w-full border rounded-lg px-4 py-2"
                                    />

                                </div>

                            </div>

                            <div className="flex gap-3 mt-6">

                                <button
                                    onClick={handleSaveEdit}
                                    className="bg-emerald-600 text-white px-6 py-2 rounded-lg hover:bg-emerald-700"
                                >

                                    Guardar

                                </button>

                                <button
                                    onClick={() => setEditingProduct(null)}
                                    className="bg-gray-300 px-6 py-2 rounded-lg hover:bg-gray-400"
                                >

                                    Cancelar

                                </button>

                            </div>

                        </div>

                    )
                }



                <div className="flex justify-between items-center mt-6">

                    <button
                        disabled={page === 1}
                        onClick={() => setPage(page - 1)}
                        className="px-4 py-2 rounded-lg bg-gray-200 hover:bg-gray-300 disabled:opacity-40 disabled:cursor-not-allowed"
                    >
                        ← Anterior
                    </button>

                    <span className="font-medium text-gray-700">
                        Página {page} de {totalPages}
                    </span>

                    <button
                        disabled={page === totalPages}
                        onClick={() => setPage(page + 1)}
                        className="px-4 py-2 rounded-lg bg-emerald-600 text-white hover:bg-emerald-700 disabled:opacity-40 disabled:cursor-not-allowed"
                    >
                        Siguiente →
                    </button>

                </div>

            </div>

        </MainLayout>
    );
}

export default Dashboard;
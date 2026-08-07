import type { Product } from "../../interfaces/Product";
import { Pencil, Trash2 } from "lucide-react";

interface ProductTableProps {

    products: Product[];

    onDelete: (id: number) => void;

    onEdit: (product: Product) => void;

}

function ProductTable({ products, onDelete, onEdit }: ProductTableProps) {

    return (

        <div className="bg-white rounded-xl shadow-md overflow-hidden mt-8">

            <div className="px-6 py-4 border-b">

                <h2 className="text-xl font-semibold">
                    Productos
                </h2>

            </div>

            <table className="w-full">

                <thead className="bg-emerald-600 text-white">

                    <tr>

                        {/*<th className="px-4 py-3 text-left">ID</th>*/}

                        <th className="px-4 py-3 text-left">Producto</th>

                        <th className="px-4 py-3 text-left">Categoría</th>

                        <th className="px-4 py-3 text-right">Precio</th>

                        <th className="px-4 py-3 text-center">Stock</th>

                        <th className="px-4 py-3 text-center">Estado</th>

                        <th className="px-4 py-3 text-center">Acciones</th>

                    </tr>

                </thead>

                <tbody>

                    {products.map(product => (

                        <tr
                            key={product.productID}
                            className="border-b hover:bg-gray-50 transition"
                        >

                            {/*<td className="px-4 py-3">

                                {product.productID}

                            </td>}*/}

                            <td className="px-4 py-3 font-medium">

                                {product.productName}

                            </td>

                            <td className="px-4 py-3">

                                {product.categoryName}

                            </td>

                            <td className="px-4 py-3 text-right">

                                {new Intl.NumberFormat("es-CO", {
                                    style: "currency",
                                    currency: "COP",
                                    maximumFractionDigits: 0
                                }).format(product.unitPrice)}

                            </td>

                            <td className="px-4 py-3 text-center">

                                {product.unitsInStock}

                            </td>

                            <td className="px-4 py-3 text-center">

                                {product.unitsInStock > 0 ? (
                                    <span className="text-green-600 font-semibold">
                                        Disponible
                                    </span>
                                ) : (
                                    <span className="text-red-600 font-semibold">
                                        Sin stock
                                    </span>
                                )}

                            </td>

                            <td className="px-4 py-3">

                                <div className="flex justify-center gap-4">

                                    <button
                                        onClick={() => onEdit(product)}
                                        className="text-blue-600 hover:text-blue-800 transition"
                                        title="Editar"
                                    >

                                        <Pencil size={18} />

                                    </button>

                                    <button
                                        onClick={() => onDelete(product.productID)}
                                        className="text-red-600 hover:text-red-800 transition"
                                        title="Eliminar"
                                    >

                                        <Trash2 size={18} />

                                    </button>

                                </div>

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>

    );

}

export default ProductTable;
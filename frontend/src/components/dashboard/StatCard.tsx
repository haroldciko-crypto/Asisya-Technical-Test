interface StatCardProps {

    title: string;

    value: string | number;

}

function StatCard({ title, value }: StatCardProps) {

    return (

        <div className="bg-white rounded-xl shadow-md p-6">

            <p className="text-gray-500 text-sm">

                {title}

            </p>

            <h2 className="text-3xl font-bold text-emerald-700 mt-2">

                {value}

            </h2>

        </div>

    );

}

export default StatCard;
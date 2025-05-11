import { useLocation, useNavigate } from "react-router-dom";
import { ArrowLeft } from "lucide-react";
import "./Banner.css";

const Banner = () => {
	const location = useLocation();
	const navigate = useNavigate();

	const isDetailsPage = location.pathname.startsWith("/country/");

	return (
		<div className="banner">
			{isDetailsPage && (
				<button
					type="button"
					onClick={() => navigate(-1)}
					className="text-white hover:text-gray-200 transition"
				>
					<ArrowLeft className="w-5 h-5" />
				</button>
			)}
			<h1>Flag Explorer App</h1>
		</div>
	);
};

export default Banner;

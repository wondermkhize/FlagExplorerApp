import { useParams } from "react-router-dom";
import { useCountryDetails } from "../features/countries/countryApi";
import "./Details.css";

const Details = () => {
	const { name } = useParams<{ name: string }>();
	const { data, isLoading, isError } = useCountryDetails(name || "");

	if (isLoading)
		return (
			<p className="p-4" role="status" aria-live="polite">
				Loading country details...
			</p>
		);
	if (isError || !data)
		return (
			<p className="p-4 text-red-600" role="alert">
				Failed to load details.
			</p>
		);

	return (
		<div className="details-container">
			<div className="details-card">
				<img src={data.flag} alt={data.name} className="details-flag" />
				<h2 className="details-title">{data.name}</h2>
				<p className="details-info">
					<strong>Capital:</strong> {data.capital}
				</p>
				<p className="details-info">
					<strong>Population:</strong> {data.population.toLocaleString()}
				</p>
			</div>
		</div>
	);
};

export default Details;

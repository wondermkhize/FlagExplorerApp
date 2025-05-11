import { useCountries } from "../features/countries/countryApi";
import { Link } from "react-router-dom";
import "./Home.css";

const Home = () => {
	const { data, isLoading, isError } = useCountries();

	if (isLoading) return <p className="p-4">Loading...</p>;
	if (isError || !data)
		return <p className="p-4 text-red-600">Failed to load countries.</p>;

	const sortedCountries = [...data].sort((a, b) =>
		a.name.localeCompare(b.name, undefined, { sensitivity: "base" }),
	);

	return (
		<div className="country-grid">
			{sortedCountries.map((country) => (
				<Link to={`/country/${country.name}`} key={country.name}>
					<div className="country-card">
						<img
							src={country.flag}
							alt={country.name}
							className="country-flag"
						/>
						<div className="country-name">{country.name}</div>
					</div>
				</Link>
			))}
		</div>
	);
};

export default Home;

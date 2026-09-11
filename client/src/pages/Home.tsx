import { useMemo, useState } from "react";
import { useCountries } from "../features/countries/countryApi";
import { Link } from "react-router-dom";
import "./Home.css";

const Home = () => {
	const { data, isLoading, isError, refetch } = useCountries();
	const [searchTerm, setSearchTerm] = useState("");

	const filteredCountries = useMemo(() => {
		if (!data) return [];

		const normalizedSearch = searchTerm.trim().toLowerCase();

		return [...data]
			.filter((country) =>
				country.name.toLowerCase().includes(normalizedSearch),
			)
			.sort((a, b) =>
				a.name.localeCompare(b.name, undefined, { sensitivity: "base" }),
			);
	}, [data, searchTerm]);

	if (isLoading)
		return (
			<p className="p-4" role="status" aria-live="polite">
				Loading countries...
			</p>
		);
	if (isError || !data)
		return (
			<div className="state-container error-state" role="alert">
				<p>Failed to load countries.</p>
				<button type="button" onClick={() => refetch()}>
					Retry
				</button>
			</div>
		);

	return (
		<div className="home-page">
			<div className="home-toolbar">
				<label className="search-field" htmlFor="country-search">
					<span className="sr-only">Search countries</span>
					<input
						id="country-search"
						type="text"
						placeholder="Search countries"
						value={searchTerm}
						onChange={(event) => setSearchTerm(event.target.value)}
					/>
				</label>
			</div>

			{filteredCountries.length === 0 ? (
				<div className="state-container empty-state">
					<p>No countries found.</p>
				</div>
			) : (
				<div className="country-grid">
					{filteredCountries.map((country) => (
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
			)}
		</div>
	);
};

export default Home;

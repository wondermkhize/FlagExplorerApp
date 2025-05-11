import { useQuery } from "@tanstack/react-query";
import apiClient from "../../services/apiClient";
import type { Country, CountryDetails } from "../../types/country";

export const useCountries = () =>
	useQuery<Country[]>({
		queryKey: ["countries"],
		queryFn: async () => {
			const res = await apiClient.get("/countries");
			return res.data;
		},
	});

export const useCountryDetails = (name: string) =>
	useQuery<CountryDetails>({
		queryKey: ["country", name],
		queryFn: async () => {
			const res = await apiClient.get(`/countries/${name}`);
			return res.data;
		},
		enabled: !!name,
	});

export interface Country {
	name: string;
	flag: string;
}

export interface CountryDetails extends Country {
	capital: string;
	population: number;
}

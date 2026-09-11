import { render, screen, waitFor } from "@testing-library/react";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { describe, expect, it, vi } from "vitest";
import Details from "../Details";

vi.mock("../../services/apiClient", () => ({
	default: {
		get: vi.fn((url: string) =>
			url.includes("/countries/South Africa")
				? Promise.resolve({
						data: {
							name: "South Africa",
							flag: "za.svg",
							capital: "Pretoria",
							population: 60000000,
						},
					})
				: Promise.reject(),
		),
	},
}));

const renderWithRouter = () => {
	render(
		<QueryClientProvider client={new QueryClient()}>
			<MemoryRouter initialEntries={["/country/South Africa"]}>
				<Routes>
					<Route path="/country/:name" element={<Details />} />
				</Routes>
			</MemoryRouter>
		</QueryClientProvider>,
	);
};

describe("Details Page", () => {
	it("shows country details", async () => {
		renderWithRouter();

		await waitFor(() => {
			expect(screen.getByText(/South Africa/i)).toBeInTheDocument();
			expect(screen.getByText(/Pretoria/i)).toBeInTheDocument();
			expect(screen.getByText(/Population:/i).parentElement).toHaveTextContent(/60/);
		});
	});
});

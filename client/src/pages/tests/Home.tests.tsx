import { render, screen, waitFor } from "@testing-library/react";
import Home from "../Home";
import { BrowserRouter } from "react-router-dom";
import { QueryClientProvider, QueryClient } from "@tanstack/react-query";

jest.mock("../../services/apiClient", () => ({
	get: jest.fn((url: string) =>
		url === "/countries"
			? Promise.resolve({ data: [{ name: "South Africa", flag: "za.svg" }] })
			: Promise.reject(),
	),
}));

const renderWithProviders = (ui: React.ReactElement) => {
	return render(
		<QueryClientProvider client={new QueryClient()}>
			<BrowserRouter>{ui}</BrowserRouter>
		</QueryClientProvider>,
	);
};

describe("Home Page", () => {
	it("displays countries from API", async () => {
		renderWithProviders(<Home />);

		await waitFor(() => {
			expect(screen.getByText(/South Africa/i)).toBeInTheDocument();
			expect(screen.getByRole("img")).toHaveAttribute("src", "za.svg");
		});
	});
});

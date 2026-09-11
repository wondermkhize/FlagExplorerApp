import { fireEvent, render, screen, waitFor } from "@testing-library/react";
import Home from "../Home";
import { BrowserRouter } from "react-router-dom";
import { QueryClientProvider, QueryClient } from "@tanstack/react-query";
import { describe, expect, it, vi } from "vitest";

vi.mock("../../services/apiClient", () => ({
	default: {
		get: vi.fn((url: string) =>
			url === "/countries"
				? Promise.resolve({ data: [{ name: "South Africa", flag: "za.svg" }] })
				: Promise.reject(),
		),
	},
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

	it("filters countries by search text", async () => {
		renderWithProviders(<Home />);

		await waitFor(() => {
			expect(screen.getByText(/South Africa/i)).toBeInTheDocument();
		});

		fireEvent.change(screen.getByPlaceholderText(/search countries/i), {
			target: { value: "Botswana" },
		});

		expect(screen.queryByText(/South Africa/i)).not.toBeInTheDocument();
		expect(screen.getByText(/no countries found/i)).toBeInTheDocument();
	});
});

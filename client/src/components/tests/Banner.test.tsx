import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import Banner from '../Banner';
import { expect, test } from 'vitest';

test('renders Banner text', () => {
	render(
		<MemoryRouter>
			<Banner />
		</MemoryRouter>,
	);
	expect(screen.getByText(/flag explorer/i)).toBeInTheDocument();
});
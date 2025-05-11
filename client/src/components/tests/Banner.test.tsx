import { render, screen } from '@testing-library/react';
import Banner from '../Banner';

test('renders Banner text', () => {
	render(<Banner />);
	expect(screen.getByText(/flag explorer/i)).toBeInTheDocument();
});
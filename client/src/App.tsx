import type React from "react";
import AppRouter from "./routes/AppRouter";
import Banner from "./components/Banner";

const App: React.FC = () => (
	<div className="min-h-screen bg-gray-50">
		<Banner />
		<AppRouter />
	</div>
);

export default App;

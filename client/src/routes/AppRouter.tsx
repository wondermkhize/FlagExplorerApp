import { Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Details from "../pages/Details";

const AppRouter = () => (
	<Routes>
		<Route path="/" element={<Home />} />
		<Route path="/country/:name" element={<Details />} />
	</Routes>
);

export default AppRouter;

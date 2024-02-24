import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "./component/Home";
import CreateEmployye from "./component/Employee/CreateEmployee";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/create" element={<CreateEmployye />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

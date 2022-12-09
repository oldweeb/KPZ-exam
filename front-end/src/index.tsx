import ReactDOM from 'react-dom';
import App from './App';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import AddAnimal from './components/AddAnimal';
import AddVisit from './components/AddVisit';
import Animals from './components/Animals';
import Visits from './components/Visits';

ReactDOM.render(
  <BrowserRouter>
    <Routes>
      <Route path='/' element={<App />} />
      <Route path='/add-animal' element={<AddAnimal />} />
      <Route path='/add-visit' element={<AddVisit />} />
      <Route path='/animals' element={<Animals />} />
      <Route path='/visits' element={<Visits />} />
    </Routes>
  </BrowserRouter>,
  document.getElementById('root')
);
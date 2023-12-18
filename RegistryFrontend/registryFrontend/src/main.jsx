import React from 'react'
import ReactDOM from 'react-dom/client'
import {createBrowserRouter,createRoutesFromElements, RouterProvider, Route, Routes} from "react-router-dom";
import MainLayout from "./layouts/MainLayout";
import ListPage from "./pages/ListPage";
import CartPage from "./pages/CartPage.jsx"
import { CartProvider } from './provider/CartProvider.jsx';
import "./main.css";
import CheckoutPage from './pages/CheckoutPage.jsx';

const router = createBrowserRouter(
    createRoutesFromElements(
      <Route element={<MainLayout />}>
        <Route path="/" element={<ListPage />} />
        <Route path="/cart" element={<CartPage />} />
        <Route path="/checkout" element={<CheckoutPage />} />
      </Route>
    )
  )

ReactDOM.createRoot(document.getElementById('root')).render(
    <CartProvider>
      <RouterProvider router={router}/>
    </CartProvider>
)

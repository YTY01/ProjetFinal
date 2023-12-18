import { useContext, createContext, useState, useMemo, useEffect } from "react";
import useAPI from "../hooks/useAPI";

const CartContext = createContext();

export const CartProvider = (props) => {
    const [cart, setCart] = useState({});

    useEffect(() => {
        LoadCart();
    }, []);

    const LoadCart = async () => {
        let cart = JSON.parse(window.sessionStorage.getItem("cart"));
        if(cart == undefined){
            cart = await useAPI("/Cart/Create", "GET");
            window.sessionStorage.setItem("cart", JSON.stringify(cart));
        } 
        await setCart(cart);
        console.log(cart);
    }

    const AddItem = async (item) => {
        setCart((previousState) => {
            const newCart = {...previousState};
            newCart.products = [...previousState.products, item];
            window.sessionStorage.setItem("cart", JSON.stringify(newCart))
            return newCart;
        });
        await useAPI("/Cart/AddItem", "POST", {SessionGuid : cart.sessionGuid, Id: item.id});
    }

    const RemoveItem = async (item) =>{
        setCart((previousState) => {
            const newCart = {...previousState};
            console.log(newCart);
            //newCart.products = previousState.filter(p => p.);
            window.sessionStorage.setItem("cart", JSON.stringify(newCart))
            return newCart;
        });
    }

    const Clear = async () => {
        setCart((previousState) => {
            const newCart = {...previousState};
            newCart.products = [];
            window.sessionStorage.setItem("cart", JSON.stringify(newCart))
            return newCart;
        });
        const url = `/Cart/Clear/${cart.sessionGuid}`;
        await useAPI(url, "DELETE");
    }

    const Pay = async (member) => {
        const guid = member ? member.uuid : null;
        await window.sessionStorage.removeItem("cart");
        const url = `/Checkout/Pay/${cart.sessionGuid}${guid ? `&${guid}` : ""}`;
        console.log(url);
        await useAPI(url, "POST");
        LoadCart();
    }

    const value = useMemo(() => ({
        cart, 
        setCart,
        AddItem,
        RemoveItem,
        Clear,
        LoadCart,
        Pay
    }), [cart]);

    return <CartContext.Provider value={value}>{props.children}</CartContext.Provider>
}

export const useCart = () => {
    return useContext(CartContext);
}
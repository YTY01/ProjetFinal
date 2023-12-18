import useAPI from "../hooks/useAPI";
import { useCart } from "../provider/CartProvider";
import { useState, useEffect } from "react";
import "./CheckoutPage.css";

const montantFinauxInitial = {
    total: 0,
    sousTotal: 0,
    tps: 0,
    tvq: 0,
    rabaisMembre: 0
}

const CheckoutPage = () => {
    const CartContext = useCart();
    const [montantsFinaux, setMontantsFinaux] = useState();
    const [noMembre, setNoMembre] = useState();
    const [input, setInput] = useState("");

    useEffect(() => {
        if(CartContext.cart.products){
            console.log(noMembre)
            loadReceipt();
        }
    }, [CartContext.cart, noMembre])

    const loadReceipt = async () => {
        const url = noMembre ? `/Checkout/${CartContext.cart.sessionGuid}&${noMembre.email}` : `/Checkout/${CartContext.cart.sessionGuid}`;
        const receipt = await useAPI(url, "GET");
        await setMontantsFinaux(receipt);
    }

    const onClickHandler = async () => {
        const email = input;
        const url = `/Member/${email}`;
        const member = await useAPI(url, "GET");

        if(member.length){
            await setNoMembre(member[0]);
        }
    }

    const onChangeHandler = (event) => {
        setInput(event.target.value);
    }

    const paymentHandler = () => {
        alert(`${montantsFinaux.total}$ à été retiré de votre compte!`);
        CartContext.Pay(noMembre);
    }

    return (
        <div className="checkout-body">
            {montantsFinaux != undefined ? 
            <>
                <div className="checkout-items">
                    {!!montantsFinaux.cart.products.length && montantsFinaux.cart.products.map(product => {
                        return (
                        <div className="checkout-items-single">
                            <p>{product.name}, {product.quantity}x {product.salePrice ? product.salePrice: product.price}$ :</p> 
                            <p>{product.salePrice ? (product.quantity*product.salePrice).toFixed(2): (product.quantity*product.price).toFixed(2)}$</p>
                        </div>)
                    })} 
                </div>
                <div className="checkout-member">
                    <div>
                        <label htmlFor="member">Courriel de membre: </label>
                        <input type="text" id="member" onChange={onChangeHandler} value={input}></input> 
                    </div>

                    <button onClick={onClickHandler} className="checkout-button">Appliquer</button>
                </div>
                <div className="checkout-price">
                    <p>Sous-total: {montantsFinaux.sousTotal}$</p>
                    <p>Tps (5%): {montantsFinaux.tps}$</p>
                    <p>Tvq (9.975%): {montantsFinaux.tvq}$</p>
                    {montantsFinaux.member ? <p>Rabais membre: -{montantsFinaux.memberDiscount}$</p>:<></>}
                    <p>Total: {montantsFinaux.total}$</p>
                    <button onClick={paymentHandler} className="checkout-button">Payer</button>
                </div>
            </>
            :<></>}
        </div>
    )
}

export default CheckoutPage;
import CartItem from "../components/Cart/CartItem";
import { useMemo, useState } from "react";
import { useCart } from "../provider/CartProvider";
import "./CartPage.css";
import { Link } from "react-router-dom";

const CartPage = () => {
    const cartContext = useCart();
    const [sousTotal, setSousTotal] = useState();
    
    const computeCartItems = () => {
        const computedItems = []

        if(cartContext.cart.products)
        {
            let sousTotal = 0;
            const unique = [...new Set(cartContext.cart.products.map(product => product.id))];
            unique.forEach(value => {
                const allItems = cartContext.cart.products.filter(product => product.id == value);
                const item = {
                    item: allItems[0],
                    qty: allItems.length
                }

                computedItems.push(item)

                if(item.item.salePrice){
                    sousTotal += item.item.salePrice*item.qty;
                } else {
                    sousTotal += item.item.price*item.qty;
                }
                
            });
            (Math.round(sousTotal * 100) / 100).toFixed(2);
            setSousTotal(sousTotal);
        }

        return computedItems;
    }

    const onClickHandler = () => {
        if(confirm("Êtes-vous certain de vouloir vider votre panier?")){
            cartContext.Clear();
        }
    }

    const computedItems = useMemo(() => computeCartItems(), [cartContext.cart]);
    return (
        <div className="cart-page">
            <div className="cart-page-body">
                <table className="cart-page-table">
                    <thead>
                        <tr>
                            <th className="cart-page-col-lg">Produit</th>
                            <th className="cart-page-col-sm">Quantité</th>
                            <th className="cart-page-col-sm">Prix</th>
                        </tr>
                    </thead>
                    <tbody>
                    {!!computedItems.length && 
                    computedItems.map(item => <CartItem key={item.item.id} product={item} />)}
                    </tbody>
                    <tfoot>
                        <tr>
                            <td className="cart-page-col-lg"></td>
                            <td className="cart-page-col-sm"><strong>Sous-total:</strong></td>
                            <td className="cart-page-col-sm">{sousTotal}$</td>
                        </tr>
                    </tfoot>
                </table>
                <div className="cart-page_checkout-btn">
                    <Link to="/checkout" className="button cart-button">Checkout</Link>
                    <Link className="button cart-button" onClick={onClickHandler}>Clear Cart</Link>
                </div>
            </div>
        </div>
    )
}

export default CartPage;
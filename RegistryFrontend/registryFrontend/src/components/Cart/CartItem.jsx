import "./CartItem.css"

const CartItem = (props) => {
    const priceQuantity = props.product.qty*props.product.item.price;
    const taxable = props.product.item.departement.taxable;

    let priceDisplay;
    if(props.product.item.saleId != null){
        priceDisplay = (
            <>
                <p className="cart-item_price-onsale">{priceQuantity}$</p><p>{(props.product.qty*(props.product.item.price-(props.product.item.price*props.product.item.sale.percentageOff))).toFixed(2)}${taxable ? "+tx":""}</p>
            </>
        )
    } else {
        priceDisplay = (
            <>
                <p>{priceQuantity}${taxable ? "+tx":""}</p>
            </>
        )
    }

    return (
        <tr className="cart-item">
            <td className="cart-page-col-lg cart-item_produit">
                <div className="cart-item_img">
                    <img src={props.product.item.imageURL} height="75" width="75"/>
                </div>
                <div className="cart-item_nom">{props.product.item.name}</div>
            </td>
            <td className="cart-page-col-sm">
                <div className="cart-item_qty">{props.product.qty}</div>
            </td>
            <td className="cart-page-col-sm">
                <div className="cart-item_price">{priceDisplay}</div>
            </td>
        </tr>
    )
}

export default CartItem;
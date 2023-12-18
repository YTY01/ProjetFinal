import { useCart } from "../provider/CartProvider";
import "./ProductCard.css"

const ProductCard = (props) => {
    const CartContext = useCart();

    const onClickHandler = () => {
        CartContext.AddItem(props.product);
    }

    let priceDisplay;

    if(props.product.saleId != null){
        priceDisplay = (
            <div className="product-card-price_tag">
                <p className="product-card-price-onsale">{props.product.price}$</p> <p className="product-card-price-onsale-newprice">{props.product.salePrice}$!!</p>
            </div>
        )
    } else {
        priceDisplay = (
            <div className="product-card-price_tag">
                <p>{props.product.price}$</p>
            </div>
        )
    }

    return (
        <div className="product-card">
            <div className="product-card-header">
                <h3>{props.product.name}</h3>
            </div>
            <div className="product-card-image">
                <img src={props.product.imageURL} height="250" width="250"/>
            </div>
            <div className="product-card-body">
                {priceDisplay}
                <button className="product-card-add" onClick={onClickHandler}>Ajouter au panier</button>
            </div>
        </div>
    )
}

export default ProductCard;
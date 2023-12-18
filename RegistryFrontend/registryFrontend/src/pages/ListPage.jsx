import { useEffect, useState } from "react"
import useAPI from "../hooks/useAPI";
import ProductCard from "../components/ProductCard";
import "./ListPage.css"

const ListPage = () => {
    const [products, setProducts] = useState([]);


    useEffect(() => {
        const call = async () => {
            const data = await useAPI("/Product", "GET");
            console.log(data);
            await setProducts(data);
        }

        call();        
    }, [])

    console.log(products);
    return (
        <div className="product-list">
            {!!products.length && products.map(product => <ProductCard key={product.id} product={product} />)}
        </div>
    )
}

export default ListPage;
const urlAPI = "https://localhost:7168/api";

const useAPI = async (url, method, payload = "") => {
    const callURL = urlAPI+url;
    const options = {
        method: method,
        mode: "cors",
        headers: {
            "Content-Type": "application/json"
        }
    };

    if(method != "GET"){
        const body = {
            value: payload
        }
        options.body = JSON.stringify(payload);
    }
    console.log(options);
    let response;
    try {
        response = await fetch(callURL, options);
        console.log(response)
    } catch (e) {
        console.log(e);
    }

    return response.json();
}

export default useAPI
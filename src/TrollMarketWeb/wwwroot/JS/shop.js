(()=>{
    const api = 'http://localhost:8081/api/v1/TrollMarket/shop';
    const api2 = 'http://localhost:8081/api/v1/trollmarket';
    let params = 'productId';
    let buttondetail = document.querySelectorAll('a.detail');
    let buttonadd = document.querySelectorAll('a.add');
    let addtocart = document.querySelector('button.insert');
    let Token = localStorage.getItem('token')
    console.log(Token);
    // console.log(document.querySelectorAll('a.detail'));

    for(let index=0; index < buttondetail.length; index++){
        buttondetail[index].addEventListener('click', (event)=>{
            event.preventDefault();
            let productId = event.target.getAttribute('product-id');
            getproduct(productId);
            
        })
    }

    let getproduct = (productId)=>{
        let request = new XMLHttpRequest();
        request.open('GET', `${api}?${params}=${productId}`);
        request.setRequestHeader('Content-type', 'application/json');
        request.setRequestHeader('Authorization', 'Bearer ' + Token);
        request.send();
        request.onload = () =>{
            const response = JSON.parse(request.response);
            console.log(response);
            getproductbyid(response);
        }
    }

    let getproductbyid = (product) =>{
        document.querySelector('.modal-layer').style.display="flex";
        let form = document.querySelector('.detail-data');
        form.querySelector('.nameproduct').textContent = product.productName
        form.querySelector('.categoryname').textContent = product.catgoryName;
        form.querySelector('.descrriptionproduct').textContent = product.descriptionProduct;
        form.querySelector('.price').textContent = product.price;
        form.querySelector('.sellername').textContent = product.sellerUsername;
    }

    document.querySelector('.detail-data button').addEventListener('click',(event)=>{
        document.querySelector('.modal-layer').style.display="none"

    })



    // console.log(document.querySelectorAll('a.add'));
    for(let index=0; index < buttonadd.length; index++){
        buttonadd[index].addEventListener('click', (event)=>{
            event.preventDefault();
            let productId = event.target.getAttribute('product-id');
            console.log(productId);
            getaddtocart(productId);
            // document.querySelector('.modal-layer.add-to-cart').style.display="flex";
        })
    }

    let getaddtocart = (productId)=>{
        let request = new XMLHttpRequest();
        request.open('GET', `${api2}?${params}=${productId}`);
        request.setRequestHeader('Authorization', 'Bearer ' + Token);
        request.send();
        request.onload = () =>{
            const response = JSON.parse(request.response);
            console.log(response);
            getproducttocart(response)
        }
    }

    let getproducttocart = (respon) =>{
        // console.log(respon.shippers[0].shipperName);
        document.querySelector('.modal-layer.add-to-cart').style.display="flex";
        let form = document.querySelector('.form-add-tocart');
        console.log(form.querySelector('input[name="idproduct"]'))
        form.querySelector('input[name="idproduct"]').value =respon.products[0].productid
        let select = form.querySelector('select');
        let optionselected = document.createElement('option');
        optionselected.setAttribute('value',0);
        optionselected.textContent="Choose Shipper"
        select.append(optionselected);
        console.log(select);
        for(const shipper of respon.shippers){
            let selectoption = document.createElement('option');
            selectoption.setAttribute('value',shipper.shipperId);
            selectoption.textContent = shipper.shipperName
            select.append(selectoption);
        }
    }

    addtocart.addEventListener('click', event =>{
        event.preventDefault();
        cartproduct();
    })

    let cartproduct = () =>{
        let form = document.querySelector('.form-add-tocart');
        let idproduct = form.querySelector('input[name="idproduct"]');
        console.log(idproduct.value);
        let quantityproduct = form.querySelector('input[name="quantity"]');
        console.log(quantityproduct.value);
        let shipper = form.querySelector('select');
        console.log(shipper.value)
        console.log({productid : idproduct.value, quantity: quantityproduct.value, shipperId: shipper.value});
        cartaction({productid : idproduct.value, quantity: quantityproduct.value, shipperId: shipper.value});
    }

    let cartaction =(body)=>{
        console.log(body);
        let request = new XMLHttpRequest();
        request.open('POST', api2);
        request.setRequestHeader('Content-type', 'application/json', 'charset-UTF-8');
        request.setRequestHeader('Authorization', 'Bearer ' + Token);
        request.send(JSON.stringify(body));
        request.onload = () =>{
            const cartproduct = JSON.parse(request.response);
            console.log(cartproduct);
            if(request.status===200){
                alert('Success add to cart');
                document.querySelector('.modal-layer.add-to-cart').style.display="none";
            }else{
                alert("Failed add to cart");
                document.querySelector('.modal-layer.add-to-cart').style.display="none";
            }
        }
    }
})()
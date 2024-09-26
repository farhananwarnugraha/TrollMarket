(()=>{
    const api = 'http://localhost:8081/api/v1/merchandise'
    let params = 'productId';
    let Token = localStorage.getItem('token')

    let buttondetail = document.querySelectorAll('a.info');


    for(let index=0; index < buttondetail.length; index++){
        buttondetail[index].addEventListener('click', (event)=>{
            event.preventDefault();
            let productId = event.target.getAttribute('product-id');
            console.log(productId);
            getproduct(productId);
            
        })
    }

    let getproduct = (productId)=>{
        let request = new XMLHttpRequest();
        request.open('GET', `${api}?${params}=${productId}`);
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
        form.querySelector('.discontinue').textContent = product.discontiue;
    }

    document.querySelector('.detail-data button').addEventListener('click',(event)=>{
        document.querySelector('.modal-layer').style.display="none"

    })
})();
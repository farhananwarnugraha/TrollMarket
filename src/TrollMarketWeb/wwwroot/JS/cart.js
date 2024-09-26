(() => {
    console.log("test");
    const apiurl = 'http://localhost:8081/api/v1/trollmarket';
    let buttonPurcess = document.querySelector('a.purcessAll');
    let buttonDelete = document.querySelectorAll('a.delete');
    // console.log(buttonPurcess);
    let token = localStorage.getItem('token');
    console.log(token);
    
    buttonPurcess.addEventListener('click', () => {
       let approve = confirm("Apakah anda yakin ingin melanjutkan transaksi pada semua barang di keranjang ini?");
       console.log(approve);
        if(approve){
            getUsername();
        }
        else{
            return;
        }
        
    });

    let getUsername = () => {
        let username = localStorage.getItem('username');
        console.log(username);
        purcessAll({usernameBuyer: username});
    };
    
    let purcessAll = (body) => {
        console.log(body);
        let request = new XMLHttpRequest();
        request.open('POST', apiurl+"/purcessAll");
        request.setRequestHeader('Content-type', 'application/json', 'charset-UTF-8');
        request.setRequestHeader('Authorization', 'Bearer ' + token);
        request.send(JSON.stringify(body));
        request.onload = () => {
            const purcess = JSON.parse(request.response);
            console.log(purcess);
            if(request.status === 200 && purcess.status === 200){ 
                alert("Success");
                location.reload();
            }else{
                alert("Failed to purcess, because youre balance is not enough");
            }
        }
    };
    //delete cart 
    console.log(buttonDelete);
    for(let index=0; index < buttonDelete.length; index++){
        buttonDelete[index].addEventListener('click', (event)=>{
            event.preventDefault();
            let carttId = event.target.getAttribute('cart-id');
            document.querySelector('.delete.modal-layer').style.display="flex";
            document.querySelector('.upsert-form .yes').addEventListener('click',(event)=>{
                event.preventDefault();
                console.log(carttId);
                deletedCart(carttId);
            })
            console.log(document.querySelector('.upsert-form .no'));
            document.querySelector('.upsert-form .no').addEventListener('click',(event)=>{
                event.preventDefault();
                document.querySelector('.modal-layer').style.display="none";
            })
        })
    }

    let deletedCart = (cartId) => {
        let request = new XMLHttpRequest();
        request.open('DELETE', apiurl+'?orderProductId='+cartId);
        request.setRequestHeader('Authorization', 'Bearer ' + token);
        request.send();
        request.onload = () =>{
            console.log(request.response);
        }
        request.onloadend = () =>{
           if(request.status===200){
               alert("Delete successfully");
               document.querySelector('.delete.modal-layer').style.display="none";
           }
           else{
               alert("Deleted Failed");
                document.querySelector('.delete.modal-layer').style.display="none";
           }
        }
    }
})();
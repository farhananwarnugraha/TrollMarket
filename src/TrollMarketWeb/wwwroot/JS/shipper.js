(()=>{
    const url = 'http://localhost:8081/api/v1/trollmarket/shipment';
    let buttonAdd = document.querySelector('.shipper a.shipper');
    let buttonEdit = document.querySelectorAll('.edit');
    let buttonDelete = document.querySelectorAll('.delete');
    let params = 'shipperId';
    let Token = localStorage.getItem('token')
    console.log("this is Js Shiiper")
    // console.log(document.querySelector('.shipper a.shipper'));
    buttonAdd.addEventListener('click', (event)=>{
        event.preventDefault();
        document.querySelector('.modal-layer').style.display="flex";
        document.querySelector('.modal-layer button.update').style.display="none";
    });

    let newShipper = ()=>{
        let form = document.querySelector('.upsert-form');
        let name = form.querySelector('[name="shippername"]').value;
        let price = form.querySelector('[name="price"]').value;
        let service = form.querySelector('[name="service"]').checked;
        console.log(`Name : ${name}, price: ${price}, service: ${service}`)
        return ({shipperName:name, price: price, isService: service});
    }

    let sendnewShipper = () =>{
        console.log(newShipper());
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader('Content-type', 'application/json');
        request.setRequestHeader('Authorization', 'Bearer ' + Token);
        request.send(JSON.stringify(newShipper()));
        request.onload = () =>{
            console.log(request.response);
        }
        request.onloadend =()=>{
            document.querySelector('.modal-layer').style.display="none";
            alert("Success inputed data")
            location.reload();
        }
    }

    document.querySelector('.modal-layer button.insert').addEventListener('click',(event)=>{
        event.preventDefault();
        sendnewShipper();
    })
    console.log(buttonEdit);
    for(let index=0; index < buttonEdit.length; index++){
        buttonEdit[index].addEventListener('click', (event)=>{
            event.preventDefault();
            let shipperId = event.target.getAttribute('shipper-id');
            getshipperbyid(shipperId);
        })
    }

    let getshipperbyid = (shipId)=>{
        let request = new XMLHttpRequest();
        request.open('GET', `${url}?${params}=${shipId}`);
        request.setRequestHeader('Authorization', 'Bearer ' + Token);
        request.send();
        request.onload = () =>{
            const response = JSON.parse(request.response);
            console.log(response);
            geteditshipper(response);
        }
    }

    let geteditshipper = (shipper)=>{
        document.querySelector('.modal-layer').style.display="flex";
        document.querySelector('.insert').style.display="none";
        let form = document.querySelector('.upsert-form');
        form.querySelector('[name="shippername"]').value = shipper.shipperName;
        form.querySelector('[name="price"]').value = shipper.price;
        form.querySelector('[name="service"]').checked = shipper.isService;
    }

    let processedit = ()=>{
        console.log(newEditShipper());
        let request = new XMLHttpRequest();
        request.open('PUT', url);
        request.setRequestHeader('Content-type', 'application/json')
        request.setRequestHeader('Authorization', 'Bearer ' + Token);
        request.send(JSON.stringify(newEditShipper()));
        request.onload = ()=>{
            console.log(request.response);
        }
        request.onloadend = ()=>{
            document.querySelector('.modal-layer').style.display="none";
            alert("Changes Data Succesed");
        }
    }

    document.querySelector('.modal-layer button.update').addEventListener('click',(event)=>{
        processedit();
    })

    let newEditShipper = ()=>{
        let form = document.querySelector('.upsert-form');
        let name = form.querySelector('[name="shippername"]').value;
        let price = form.querySelector('[name="price"]').value;
        let service = form.querySelector('[name="service"]').checked;
        console.log(`Name : ${name}, price: ${price}, service: ${service}`)
        return ({shipperName:name, price: price, isService: service});
    }


    console.log(buttonDelete);
    for(let index=0; index < buttonDelete.length; index++){
        buttonDelete[index].addEventListener('click', (event)=>{
            event.preventDefault();
            let shipperId = event.target.getAttribute('shipper-id');
            document.querySelector('.delete.modal-layer').style.display="flex";
            document.querySelector('.upsert-form .yes').addEventListener('click',(event)=>{
                event.preventDefault();
                console.log(shipperId);
                deleteCategory(shipperId)
            })
            document.querySelector('.upsert-form .no').addEventListener('click',(event)=>{
                event.preventDefault();
                document.querySelector('.modal-layer').style.display="none";
            })
        })
    }

    let deleteCategory= (shipperId) =>{
        let request = new XMLHttpRequest();
        request.open('DELETE', `${url}?${params}=${shipperId}`);
        request.setRequestHeader('Authorization', 'Bearer ' + Token);
        request.send();
        request.onload = () =>{
            console.log(request.response);
        }
        request.onloadend = () =>{
           if(request.status===200){
               alert("Delete successfully");
               document.querySelector('.delete.modal-layer').style.display="none";
           }
           else if(request.status===400){
               alert("Shipper Cannot be deleted, because there are transactions that use this shipping service")
                document.querySelector('.delete.modal-layer').style.display="none";
           }
        }
    }


})()
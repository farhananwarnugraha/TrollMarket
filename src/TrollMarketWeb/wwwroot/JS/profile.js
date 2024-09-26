(()=>{
    const api = 'http://localhost:8081/api/v1/addbalance';
    let addbalance = document.querySelector('.add-balance')
    console.log(addbalance);
    let Token = localStorage.getItem('token')
    console.log(Token);
    let buttonadd = document.querySelector('button.insert');
    console.log(buttonadd);
    
    addbalance.addEventListener('click', () =>{
        document.querySelector('.modal-layer').style.display="flex";
    })

    // document.querySelector('.balance').addEventListener('click', (event)=>{
        
    // })

    buttonadd.addEventListener('click', () =>{
        // event.preventDefault();
        // console.log("test");
        let balance = document.querySelector('input[type="number"]').value;
        console.log(balance);
        addbalanceaction();
    })
    
    let addbalanceaction =() => {
        let balance = document.querySelector('input[type="number"]').value;
        let request = new XMLHttpRequest();
        console.log(balance);
        request.open('PUT', api);
        request.setRequestHeader('Content-type', 'application/json');
        request.setRequestHeader('Authorization', 'Bearer ' + Token);
        request.send(JSON.stringify({balance}))
        request.onload = () =>{
            console.log(request.response);
            if(request.status === 200){
                alert("Dana telah ditambahkan");
                location.reload();
            }else{
                alert("Gagal Menambahkan dana!")
            }
        }
    }
})();
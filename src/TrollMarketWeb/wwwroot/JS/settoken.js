(()=>{
    const api = 'http://localhost:8081/api/v1/auth';

    let username = document.querySelector('input.username');
    let password = document.querySelector('input.password');
    let role = document.querySelector('select.role');

    let buttonlogin= document.querySelector('button[type="submit"]')
    buttonlogin.addEventListener('click', event=>{
        // event.preventDefault();
        username.value
        console.log(username.value);
        password.value
        console.log(password.value);
        role.value
        console.log(role.value);
        GetToken({username: username.value, password:password.value,role:role.value})
    })

    let GetToken = (body)=>{
        let request = new XMLHttpRequest();
        request.open('POST', api);
        request.setRequestHeader('Content-type', 'application/json', 'charset-UTF-8');
        request.send(JSON.stringify(body));
        request.onload = () =>{
            const token = JSON.parse(request.response);
            console.log(token);
            localStorage.setItem('token', token.token)
            localStorage.setItem('username', token.username);
        }
    }
})();
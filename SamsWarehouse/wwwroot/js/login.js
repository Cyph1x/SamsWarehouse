var XHR = null;

const LOGINBTN = document.getElementById('loginBtn');

document.querySelectorAll('[data-val]')
    .forEach(field =>{
        field.addEventListener('keyup', () =>{
            let fields = $(document.getElementById("fields"));

            if (!fields.valid()){
                // Disable the login button.
                LOGINBTN.disabled = "disabled";
            } else{
                LOGINBTN.removeAttribute("disabled");
            }
        })
    })

async function login(){
    let email = document.getElementById("Email").value;
    let password = document.getElementById("Password").value;
    let validationToken = document.getElementsByName("__RequestVerificationToken")[0].value;
    let form = new FormData();
    form.append("Email", email);
    form.append("Password", password);
    form.append("__RequestVerificationToken", validationToken);

    try{
        await fetch("/Auth/Login",{
            method: "POST",
            body: form,
        }).then(async function (response){
            if (response.ok){
                window.location.href = "/";
            } else{
                let err = await response.text();
                alert(err);
            }
        });
    } catch (error){
    }
}
$('#fields').submit(function (event){
    event.preventDefault();
    login();
});
var settings ={
    validClass: "is-valid",
    errorClass: "is-invalid"
};
$.validator.setDefaults(settings);
$.validator.unobtrusive.options = settings;
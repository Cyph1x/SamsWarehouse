var XHR = null;

const SIGNUPBTN = document.getElementById('signupBtn');

document.querySelectorAll('[data-val]')
    .forEach(field => {
        field.addEventListener('keyup', () => {
            let fields = $(document.getElementById("fields"));

            if (!fields.valid()) {
                // Disable the login button.
                SIGNUPBTN.disabled = "disabled";
            } else {
                SIGNUPBTN.removeAttribute("disabled");
            }
        })
    });

async function signup(){
    let email = document.getElementById("Email").value;
    let password = document.getElementById("Password").value;
    let validationToken = document.getElementsByName("__RequestVerificationToken")[0].value;
    let form = new FormData();
    form.append("Email", email);
    form.append("Password", password);
    form.append("__RequestVerificationToken", validationToken);

    try{
        await fetch("/Auth/Signup",{
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
    signup();
});
var settings ={
    validClass: "is-valid",
    errorClass: "is-invalid"
};
$.validator.setDefaults(settings);
$.validator.unobtrusive.options = settings;

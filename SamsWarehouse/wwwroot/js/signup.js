var xhr = null;


const passwordField = document.getElementById('Password');
const emailField = document.getElementById('Email');
const signupBtn = document.getElementById('signupBtn');
document.querySelectorAll('[data-val]')
    .forEach(field => {
        field.addEventListener('keyup', () => {
            var fields = $(document.getElementById("fields"));

            if (!fields.valid()) {
                //disable the login button
                signupBtn.disabled = "disabled";
            } else {
                signupBtn.removeAttribute("disabled");
            }
        })
    })
var settings = {
    validClass: "is-valid",
    errorClass: "is-invalid"

};
$.validator.setDefaults(settings);
$.validator.unobtrusive.options = settings;
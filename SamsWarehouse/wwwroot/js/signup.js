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
                loginBtn.disabled = "disabled";
            } else {
                loginBtn.removeAttribute("disabled");
            }
        })
    })

var xhr = null;


const passwordField = document.getElementById('Password');
const emailField = document.getElementById('Email');
const loginBtn = document.getElementById('loginBtn');
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

async function login() {
    var email = document.getElementById("Email").value;
    var password = document.getElementById("Password").value;
    var validationToken = document.getElementsByName("__RequestVerificationToken")[0].value;
    var form = new FormData();
    form.append("Email", email);
    form.append("Password", password);
    form.append("__RequestVerificationToken",validationToken)

    try {
        const response = await fetch("/Auth/login", {
            method: "POST",
            body: form,
        });

        if (response.ok) {
            window.location.href = "/";
        } else {
            alert("Invalid Login")
        }
    } catch (error) {
    }
}
$('#fields').submit(function (event) {
    event.preventDefault();
    login();
});

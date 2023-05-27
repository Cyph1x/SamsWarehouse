
const CART = document.getElementById('cartContents');

function addListeners(){
    // New cart.
    $('#createCart').submit(function (event){
        event.preventDefault();
        var form = document.getElementById('createCart')
        if (!form.checkValidity()){
            event.stopPropagation();
        } else{
            createCart(document.getElementById('newCartName').value);
        }

        form.classList.add('was-validated');
    });
    // Remove cart.
    document.querySelectorAll('[data-remove-cart-id]')
        .forEach(button =>{
            button.addEventListener('click', () =>{
                var id = button.getAttribute('data-remove-cart-id');
                removeCart(id);
            })
        })
    // Switch cart.
    document.querySelectorAll('[data-switch-cart-id]')
        .forEach(button =>{
            button.addEventListener('click', () =>{
                var id = button.getAttribute('data-switch-cart-id');
                switchCart(id);
            })
        })
    // Remove from cart.
    document.querySelectorAll('[data-remove-from-cart-id]')
        .forEach(button =>{
            button.addEventListener('click', () =>{
                var id = button.getAttribute('data-remove-from-cart-id');
                removeFromCart(id);
            })
        })
    // Set item quantity.
    document.querySelectorAll('[data-set-item-quantity-id]')
        .forEach(field =>{
            field.addEventListener('change', () =>{
                var id = field.getAttribute('data-set-item-quantity-id');
                var quantity = field.value;
                setQuantityCart(id, quantity);
            })
        })
}

var CARTXHR = null;
// Refresh the cart view.
function updateCart(){
    if (CARTXHR !== null){
        CARTXHR.abort();
        CARTXHR = null;
    }
    CARTXHR = new XMLHttpRequest();
    CARTXHR.onreadystatechange = function (){
        if (this.readyState === 4 && this.status === 200) {
            // Update the cart views html.
            CART.innerHTML = this.responseText;
            // Get the cart price and update the cart total badge to match it.
            let price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            // Add listeners to the new cart view.
            addListeners();
        }
    };
    CARTXHR.open("GET", "/Cart/CartModal", true);
    CARTXHR.send();
};

var SWITCHCARTXHR = null;
// Select a different cart.
function switchCart(id){
    if (SWITCHCARTXHR !== null){
        SWITCHCARTXHR.abort();
        SWITCHCARTXHR = null;
    }
    SWITCHCARTXHR = new XMLHttpRequest();
    SWITCHCARTXHR.onreadystatechange = function (){
        if (this.readyState === 4 && this.status === 200){
            // Update the cart views html.
            CART.innerHTML = this.responseText;
            // Get the cart price and update the cart total badge to match it.
            let price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            // Add listeners to the new cart view.
            addListeners();
        }
    };
    SWITCHCARTXHR.open("GET", "/Cart/" + id, true);
    SWITCHCARTXHR.send();
};


var ADDTOCARTXHR = null;
// Add product to cart.
function addToCart(id, quantity){
    if (ADDTOCARTXHR !== null){
        ADDTOCARTXHR.abort();
        ADDTOCARTXHR = null;
    }
    ADDTOCARTXHR = new XMLHttpRequest();
    ADDTOCARTXHR.onreadystatechange = function (){
        if (this.readyState === 4 && this.status === 200){
            // Update the cart views html.
            CART.innerHTML = this.responseText;
            // Get the cart price and update the cart total badge to match it.
            let price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            // Add listeners to the new cart view.
            addListeners();
        }
    };
    ADDTOCARTXHR.open("POST", "/Cart/Items", true);
    ADDTOCARTXHR.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    ADDTOCARTXHR.send(JSON.stringify({ "ProductId": id, "Quantity": quantity }));
}

var REMOVEFROMCARTXHR = null;
// Remove product from cart.
function removeFromCart(id){
    if (REMOVEFROMCARTXHR !== null){
        REMOVEFROMCARTXHR.abort();
        REMOVEFROMCARTXHR = null;
    }
    REMOVEFROMCARTXHR = new XMLHttpRequest();
    REMOVEFROMCARTXHR.onreadystatechange = function (){
        if (this.readyState === 4 && this.status === 200){
            // Update the cart views html.
            CART.innerHTML = this.responseText;
            // Get the cart price and update the cart total badge to match it.
            let price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            // Add listeners to the new cart view.
            addListeners();
        }
    };
    REMOVEFROMCARTXHR.open("DELETE", "/Cart/Items/" + id, true);
    REMOVEFROMCARTXHR.send();
}

var SETQUANTITYCARTXHR = null;
// Set product quantity.
function setQuantityCart(id, quantity){
    if (SETQUANTITYCARTXHR !== null){
        SETQUANTITYCARTXHR.abort();
        SETQUANTITYCARTXHR = null;
    }
    SETQUANTITYCARTXHR = new XMLHttpRequest();
    SETQUANTITYCARTXHR.onreadystatechange = function (){
        if (this.readyState === 4 && this.status === 200){
            // Update the cart views html.
            CART.innerHTML = this.responseText;
            // Get the cart price and update the cart total badge to match it.
            let price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            // Add listeners to the new cart view.
            addListeners();
        }
    };
    SETQUANTITYCARTXHR.open("PUT", "/Cart/Items", true);
    SETQUANTITYCARTXHR.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    SETQUANTITYCARTXHR.send(JSON.stringify({ "ProductId": id, "Quantity": quantity }));
}

var CREATECARTXHR = null;
// Create cart.
function createCart(name){
    if (CREATECARTXHR !== null){
        CREATECARTXHR.abort();
        CREATECARTXHR = null;
    }
    CREATECARTXHR = new XMLHttpRequest();
    CREATECARTXHR.onreadystatechange = function (){
        if (this.readyState === 4 && this.status === 200){
            // Update the cart views html.
            CART.innerHTML = this.responseText;
            // Get the cart price and update the cart total badge to match it.
            let price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            // Add listeners to the new cart view.
            addListeners();
        }
    };
    CREATECARTXHR.open("POST", "/Cart", true);
    CREATECARTXHR.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    CREATECARTXHR.send(JSON.stringify({ "Name": name }));
}
var DELETECARTXHR = null;
// Delete cart.
function removeCart(id){
    if (DELETECARTXHR !== null){
        DELETECARTXHR.abort();
        DELETECARTXHR = null;
    }
    DELETECARTXHR = new XMLHttpRequest();
    DELETECARTXHR.onreadystatechange = function (){
        if (this.readyState === 4 && this.status === 200){
            // Update the cart views html.
            CART.innerHTML = this.responseText;
            // Get the cart price and update the cart total badge to match it.
            let price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            // Add listeners to the new cart view.
            addListeners();
        }
    };
    DELETECARTXHR.open("DELETE", "/Cart/" + id, true);
    DELETECARTXHR.send();
}
// Page may contain "add to cart" buttons.
// Add event listeners to cart buttons.
document.querySelectorAll('[data-add-to-cart-id]')
    .forEach(button =>{
        button.addEventListener('click', () => {
            // Get the product id for the button.
            var id = button.getAttribute('data-add-to-cart-id');
            addToCart(id, 1);
        })
    })
updateCart();
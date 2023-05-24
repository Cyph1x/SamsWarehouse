//create the cart


const cart = document.getElementById('cartContents');


function addListeners() {
    //new cart
    $('#createCart').submit(function (event) {
        event.preventDefault();
        createCart(document.getElementById('newCartName').value);
    });
    //remove cart
    document.querySelectorAll('[data-remove-cart-id]')
        .forEach(button => {
            button.addEventListener('click', () => {
                var id = button.getAttribute('data-remove-cart-id');
                removeCart(id);
            })
        })
    //switch cart
    document.querySelectorAll('[data-switch-cart-id]')
        .forEach(button => {
            button.addEventListener('click', () => {
                var id = button.getAttribute('data-switch-cart-id');
                switchCart(id);
            })
        })
    //remove from cart
    document.querySelectorAll('[data-remove-from-cart-id]')
        .forEach(button => {
            button.addEventListener('click', () => {
                var id = button.getAttribute('data-remove-from-cart-id');
                removeFromCart(id);
            })
        })
    //set item quantity
    document.querySelectorAll('[data-set-item-quantity-id]')
        .forEach(field => {
            field.addEventListener('change', () => {
                var id = field.getAttribute('data-set-item-quantity-id');
                var quantity = field.value;
                setQuantityCart(id, quantity);
            })
        })

}

var cartxhr = null;

function updateCart(){
    if (cartxhr !== null) {
        cartxhr.abort();
        cartxhr = null;
    }
    cartxhr = new XMLHttpRequest();
    cartxhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cart.innerHTML = this.responseText;
            var price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            addListeners();
        }
    };
    cartxhr.open("GET", "/Cart/CartModal", true);
    cartxhr.send();

};

var switchCartxhr = null;

function switchCart(id) {
    if (switchCartxhr !== null) {
        switchCartxhr.abort();
        switchCartxhr = null;
    }
    switchCartxhr = new XMLHttpRequest();
    switchCartxhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cart.innerHTML = this.responseText;
            var price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            addListeners();
        }
    };
    switchCartxhr.open("GET", "/Cart/" + id, true);
    switchCartxhr.send();

};

updateCart();
var addToCartxhr = null;
//add product to cart
function addToCart(id, quantity) {
    if (addToCartxhr !== null) {
        addToCartxhr.abort();
        addToCartxhr = null;
    }
    addToCartxhr = new XMLHttpRequest();
    addToCartxhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cart.innerHTML = this.responseText;
            var price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            addListeners();
        }
    };
    addToCartxhr.open("POST", "/Cart/Items", true);
    addToCartxhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    addToCartxhr.send(JSON.stringify({ "ProductId": id, "Quantity": quantity }));
}

var deleteFromCartxhr = null;
//remove product from cart
function removeFromCart(id) {
    if (deleteFromCartxhr !== null) {
        deleteFromCartxhr.abort();
        deleteFromCartxhr = null;
    }
    deleteFromCartxhr = new XMLHttpRequest();
    deleteFromCartxhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cart.innerHTML = this.responseText;
            var price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            addListeners();

        }
    };
    deleteFromCartxhr.open("DELETE", "/Cart/Items/" + id, true);
    deleteFromCartxhr.send();
}

var setQuantityCartxhr = null;
//add product to cart
function setQuantityCart(id, quantity) {
    if (setQuantityCartxhr !== null) {
        setQuantityCartxhr.abort();
        setQuantityCartxhr = null;
    }
    setQuantityCartxhr = new XMLHttpRequest();
    setQuantityCartxhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cart.innerHTML = this.responseText;
            var price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            addListeners();

        }
    };
    setQuantityCartxhr.open("PUT", "/Cart/Items", true);
    setQuantityCartxhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    setQuantityCartxhr.send(JSON.stringify({ "ProductId": id, "Quantity": quantity }));
}

var createCartxhr = null;
//create cart
function createCart(name) {
    if (createCartxhr !== null) {
        createCartxhr.abort();
        createCartxhr = null;
    }
    createCartxhr = new XMLHttpRequest();
    createCartxhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cart.innerHTML = this.responseText;
            var price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            addListeners();
        }
    };
    createCartxhr.open("POST", "/Cart", true);
    createCartxhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    createCartxhr.send(JSON.stringify({ "Name": name }));
}
var deleteCartxhr = null;
//delete cart
function removeCart(id) {
    if (deleteCartxhr !== null) {
        deleteCartxhr.abort();
        deleteCartxhr = null;
    }
    deleteCartxhr = new XMLHttpRequest();
    deleteCartxhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cart.innerHTML = this.responseText;
            var price = document.getElementById("total").innerHTML;
            document.getElementById("price").innerHTML = price;
            addListeners();
        }
    };
    deleteCartxhr.open("DELETE", "/Cart/" + id, true);
    deleteCartxhr.send();
}
//page will contain "add to cart buttons"
//add to cart buttons
document.querySelectorAll('[data-add-to-cart-id]')
    .forEach(button => {
        button.addEventListener('click', () => {
            var id = button.getAttribute('data-add-to-cart-id');
            addToCart(id, 1);
        })
    })
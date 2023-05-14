// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*!
 * Color mode toggler for Bootstrap's docs (https://getbootstrap.com/)
 * Copyright 2011-2023 The Bootstrap Authors
 * Licensed under the Creative Commons Attribution 3.0 Unported License.
 */




const storedTheme = localStorage.getItem('theme')
const themeToggleBtn = document.getElementById('themeToggleBtn');
    const getPreferredTheme = () => {
        if (storedTheme) {
            return storedTheme
        }

        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
    }

const setTheme = function (theme) {
    localStorage.setItem('theme', theme)
    
        if (theme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches) {
            document.documentElement.setAttribute('data-bs-theme', 'dark')
        } else {
            document.documentElement.setAttribute('data-bs-theme', theme)
    }
    themeToggleBtn.innerHTML = theme === 'dark' ? '☀️' : '🌕';
    }

setTheme(getPreferredTheme())
const toggleTheme = () => {
    const currentTheme = localStorage.getItem('theme');
    const newTheme = currentTheme === 'light' ? 'dark' : 'light';
    setTheme(newTheme);
    showActiveTheme(newTheme, true);
};
// Add event listener to the toggle button

themeToggleBtn.addEventListener('click', toggleTheme);

    const showActiveTheme = (theme, focus = false) => {
        const themeSwitcher = document.querySelector('#bd-theme')

        if (!themeSwitcher) {
            return
        }

        const themeSwitcherText = document.querySelector('#bd-theme-text')
        const activeThemeIcon = document.querySelector('.theme-icon-active use')
        const btnToActive = document.querySelector(`[data-bs-theme-value="${theme}"]`)
        const svgOfActiveBtn = btnToActive.querySelector('svg use').getAttribute('href')

        document.querySelectorAll('[data-bs-theme-value]').forEach(element => {
            element.classList.remove('active')
            element.setAttribute('aria-pressed', 'false')
        })

        btnToActive.classList.add('active')
        btnToActive.setAttribute('aria-pressed', 'true')
        activeThemeIcon.setAttribute('href', svgOfActiveBtn)
        const themeSwitcherLabel = `${themeSwitcherText.textContent} (${btnToActive.dataset.bsThemeValue})`
        themeSwitcher.setAttribute('aria-label', themeSwitcherLabel)

        if (focus) {
            themeSwitcher.focus()
        }
    }

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
        if (storedTheme !== 'light' || storedTheme !== 'dark') {
            setTheme(getPreferredTheme())
        }
    })

    window.addEventListener('DOMContentLoaded', () => {
        showActiveTheme(getPreferredTheme())

        document.querySelectorAll('[data-bs-theme-value]')
            .forEach(toggle => {
                toggle.addEventListener('click', () => {
                    const theme = toggle.getAttribute('data-bs-theme-value')
                    localStorage.setItem('theme', theme)
                    setTheme(theme)
                    showActiveTheme(theme, true)
                })
            })
    })


//search bar
const searchBar = document.getElementById('searchBar');
const searchResult = document.getElementById('searchResults');


var xhr = null;

const startSearch = () => {
    if (xhr !== null) {
        xhr.abort();
        xhr = null;
    }
    if (searchBar.value.length > 0) {
xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function() {
            if (this.readyState === 4 && this.status === 200) {
                searchResult.innerHTML = this.responseText;
            }
        };
        xhr.open("GET", "/Product/Search?q=" + searchBar.value, true);
        xhr.send();
    }
};

searchBar.addEventListener('keyup', startSearch);


//create the cart


const cart = document.getElementById('cartContents');


var cartxhr = null;

const updateCart = () => {
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
        }
    };
    cartxhr.open("GET", "/Cart/CartModal", true);
    cartxhr.send();

};

var switchCartxhr = null;

function switchCart(id){
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
        }
    };
    switchCartxhr.open("GET", "/Cart/"+id, true);
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

        }
    };
    addToCartxhr.open("POST", "/Cart/Items", true);
    addToCartxhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    addToCartxhr.send(JSON.stringify({ "ProductId": id, "Quantity": quantity }));
}

var deleteFromCartxhr = null;
//add product to cart
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

        }
    };
    deleteFromCartxhr.open("DELETE", "/Cart/Items/"+id, true);
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

        }
    };
    setQuantityCartxhr.open("PUT", "/Cart/Items", true);
    setQuantityCartxhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    setQuantityCartxhr.send(JSON.stringify({ "ProductId": id, "Quantity": quantity }));
}

var createCartxhr = null;
//add product to cart
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

        }
    };
    createCartxhr.open("POST", "/Cart", true);
    createCartxhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    createCartxhr.send(JSON.stringify({ "Name": name}));
}
var deleteCartxhr = null;
//add product to cart
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

        }
    };
    deleteCartxhr.open("DELETE", "/Cart/" + id, true);
    deleteCartxhr.send();
}
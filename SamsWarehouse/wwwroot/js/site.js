/*!
 * Color mode toggler for Bootstrap's docs (https://getbootstrap.com/)
 * Copyright 2011-2023 The Bootstrap Authors
 * Licensed under the Creative Commons Attribution 3.0 Unported License.
 */
const STOREDTHEME = localStorage.getItem('theme')
const THEMETOGGLEBTN = document.getElementById('themeToggleBtn');
function getPreferredTheme(){
    if (STOREDTHEME){
        return STOREDTHEME;
    }

    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

function setTheme(theme){
    localStorage.setItem('theme', theme);

        if (theme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches){
            document.documentElement.setAttribute('data-bs-theme', 'dark');
        } else{
            document.documentElement.setAttribute('data-bs-theme', theme);
        }
    if (THEMETOGGLEBTN) {
        THEMETOGGLEBTN.innerHTML = theme === 'dark' ? '☀️' : '🌕';
    }
};

setTheme(getPreferredTheme());
function toggleTheme(){
    let currentTheme = localStorage.getItem('theme');
    let newTheme = currentTheme === 'light' ? 'dark' : 'light';
    setTheme(newTheme);
    showActiveTheme(newTheme, true);
};
// Add event listener to the toggle button.
if (THEMETOGGLEBTN) {
    THEMETOGGLEBTN.addEventListener('click', toggleTheme);
}
function showActiveTheme(theme, focus = false){
    let themeSwitcher = document.querySelector('#bd-theme');

    if (!themeSwitcher){
        return;
    }

    let themeSwitcherText = document.querySelector('#bd-theme-text');
    let activeThemeIcon = document.querySelector('.theme-icon-active use');
    let btnToActive = document.querySelector(`[data-bs-theme-value="${theme}"]`);
    let svgOfActiveBtn = btnToActive.querySelector('svg use').getAttribute('href');

    document.querySelectorAll('[data-bs-theme-value]').forEach(element =>{
        element.classList.remove('active');
        element.setAttribute('aria-pressed', 'false');
    })

    btnToActive.classList.add('active');
    btnToActive.setAttribute('aria-pressed', 'true');
    activeThemeIcon.setAttribute('href', svgOfActiveBtn);
    let themeSwitcherLabel = `${themeSwitcherText.textContent} (${btnToActive.dataset.bsThemeValue})`;
    themeSwitcher.setAttribute('aria-label', themeSwitcherLabel);

    if (focus){
        themeSwitcher.focus();
    }
}

window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () =>{
    if (STOREDTHEME !== 'light' || STOREDTHEME !== 'dark'){
        setTheme(getPreferredTheme());
    }
});

window.addEventListener('DOMContentLoaded', () =>{
    showActiveTheme(getPreferredTheme());

    document.querySelectorAll('[data-bs-theme-value]')
        .forEach(toggle =>{
            toggle.addEventListener('click', () =>{
                let theme = toggle.getAttribute('data-bs-theme-value');
                localStorage.setItem('theme', theme);
                setTheme(theme);
                showActiveTheme(theme, true);
            })
        })
});



// Search bar functionality.
const SEARCHBAR = document.getElementById('searchBar');
const SEARCHRESULT = document.getElementById('searchResults');

var XHR = null;

function startSearch(){
    if (XHR !== null){
        XHR.abort();
        XHR = null;
    }
    if (SEARCHBAR.value.length > 0 && SEARCHBAR.value.length < 255){
        XHR = new XMLHttpRequest();
        XHR.onreadystatechange = function(){
            if (this.readyState === 4 && this.status === 200){
                SEARCHRESULT.innerHTML = this.responseText;
            }
        };
        XHR.open("GET", "/Product/Search?q=" + SEARCHBAR.value, true);
        XHR.send();
    }
};
if (SEARCHBAR) {
    SEARCHBAR.addEventListener('keyup', startSearch);
}
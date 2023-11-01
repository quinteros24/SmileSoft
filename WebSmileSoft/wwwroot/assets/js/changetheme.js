
//(function () {

//    // JavaScript snippet handling Dark/Light mode switching

//    const getStoredTheme = () => localStorage.getItem('theme');
//    const setStoredTheme = theme => localStorage.setItem('theme', theme);
//    const forcedTheme = document.documentElement.getAttribute('data-bss-forced-theme');

//    const getPreferredTheme = () => {

//        if (forcedTheme) return forcedTheme;

//        const storedTheme = getStoredTheme();
//        if (storedTheme) {
//            return storedTheme;
//        }

//        const pageTheme = document.documentElement.getAttribute('data-bs-theme');

//        if (pageTheme) {
//            return pageTheme;
//        }

//        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
//    }

//    const setTheme = theme => {
//        if (theme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches) {
//            document.documentElement.setAttribute('data-bs-theme', 'dark');
//        } else {
//            document.documentElement.setAttribute('data-bs-theme', theme);
//        }
//    }

//    setTheme(getPreferredTheme());

//    const showActiveTheme = (theme, focus = false) => {
//        const themeSwitchers = [].slice.call(document.querySelectorAll('.theme-switcher'));

//        if (!themeSwitchers.length) return;

//        document.querySelectorAll('[data-bs-theme-value]').forEach(element => {
//            element.classList.remove('active');
//            element.setAttribute('aria-pressed', 'false');
//        });

//        for (const themeSwitcher of themeSwitchers) {

//            const btnToActivate = themeSwitcher.querySelector('[data-bs-theme-value="' + theme + '"]');

//            if (btnToActivate) {
//                btnToActivate.classList.add('active');
//                btnToActivate.setAttribute('aria-pressed', 'true');
//            }
//        }
//    }

//    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
//        const storedTheme = getStoredTheme();
//        if (storedTheme !== 'light' && storedTheme !== 'dark') {
//            setTheme(getPreferredTheme());
//        }
//    });

//    window.addEventListener('DOMContentLoaded', () => {
//        showActiveTheme(getPreferredTheme());

//        document.querySelectorAll('[data-bs-theme-value]')
//            .forEach(toggle => {
//                toggle.addEventListener('click', (e) => {
//                    e.preventDefault();
//                    const theme = toggle.getAttribute('data-bs-theme-value');
//                    setStoredTheme(theme);
//                    setTheme(theme);
//                    showActiveTheme(theme);
//                })
//            })
//    });
//})();

//document.addEventListener("DOMContentLoaded", function () {
//    // Tu código aquí
//    if (document.getElementById("remember_me") !== null) {
//        var rememberMeCheckbox = document.getElementById("remember_me");
//        // Verifica si el usuario ya había seleccionado "Recordar usuario" anteriormente
//        if (localStorage.getItem("rememberUser") === "true") {
//            rememberMeCheckbox.checked = true;
//        }

//        // Agrega un controlador de eventos para guardar el estado del checkbox cuando cambie
//        rememberMeCheckbox.addEventListener("change", function () {
//            if (rememberMeCheckbox.checked) {
//                // Si el usuario selecciona "Recordar usuario", guarda la elección en el almacenamiento local
//                localStorage.setItem("rememberUser", "true");
//            } else {
//                // Si el usuario desmarca "Recordar usuario", elimina la elección del almacenamiento local
//                localStorage.removeItem("rememberUser");
//            }
//        });
//    }
//});

//(function (global) {

//    if (typeof (global) === "undefined") {
//        throw new Error("window is undefined");
//    }

//    var _hash = "!";
//    var noBackPlease = function () {
//        global.location.href += "#";

//        // making sure we have the fruit available for juice (^__^)
//        global.setTimeout(function () {
//            global.location.href += "!";
//        }, 50);
//    };

//    global.onhashchange = function () {
//        if (global.location.hash !== _hash) {
//            global.location.hash = _hash;
//        }
//    };

//    global.onload = function () {
//        noBackPlease();

//        // disables backspace on page except on input fields and textarea..
//        document.body.onkeydown = function (e) {
//            var elm = e.target.nodeName.toLowerCase();
//            if (e.which === 8 && (elm !== 'input' && elm !== 'textarea')) {
//                e.preventDefault();
//            }
//            // stopping event bubbling up the DOM tree..
//            e.stopPropagation();
//        };
//    }

//})(window);
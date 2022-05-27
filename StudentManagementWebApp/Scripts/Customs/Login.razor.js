﻿var pwd = document.getElementById('pwd');
var eye = document.getElementById('eye');
var bd = document.querySelector("body");
var errModal = document.querySelector(".alertModal");
var closeBtn = document.getElementById("btnClose-js");
var tryAgainBtn = document.getElementById("btnTryAgain-js");
var modalBackground = document.getElementById("modalBackground");


eye.addEventListener('click', togglePass);

function togglePass() {

    eye.classList.toggle('active');

    (pwd.type == 'password') ? pwd.type = 'text' : pwd.type = 'password';
}

// Form Validation

function checkStuff() {
    var email = document.form1.email;
    var password = document.form1.password;
    var msg = document.getElementById('msg');

    if (email.value == "") {
        msg.style.display = 'block';
        msg.innerHTML = "Please enter your email";
        email.focus();
        return false;
    } else {
        msg.innerHTML = "";
    }

    if (password.value == "") {
        msg.innerHTML = "Please enter your password";
        password.focus();
        return false;
    } else {
        msg.innerHTML = "";
    }
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!re.test(email.value)) {
        msg.innerHTML = "Please enter a valid email";
        email.focus();
        return false;
    } else {
        msg.innerHTML = "";
    }
}

// ParticlesJS
//Ver 2.0
window.addEventListener('DOMContentLoaded', (event) => {
    /* ---- particles.js config ---- */

    particlesJS("particles-js", {
        "particles": {
            "number": {
                "value": 60,
                "density": {
                    "enable": true,
                    "value_area": 800
                }
            },
            "color": {
                "value": "#ffffff"
            },
            "shape": {
                "type": "circle",
                "stroke": {
                    "width": 0,
                    "color": "#000000"
                },
                "polygon": {
                    "nb_sides": 5
                },
                "image": {
                    "src": "img/github.svg",
                    "width": 100,
                    "height": 100
                }
            },
            "opacity": {
                "value": 0.1,
                "random": false,
                "anim": {
                    "enable": false,
                    "speed": 1,
                    "opacity_min": 0.1,
                    "sync": false
                }
            },
            "size": {
                "value": 6,
                "random": false,
                "anim": {
                    "enable": false,
                    "speed": 40,
                    "size_min": 0.1,
                    "sync": false
                }
            },
            "line_linked": {
                "enable": true,
                "distance": 150,
                "color": "#ffffff",
                "opacity": 0.1,
                "width": 2
            },
            "move": {
                "enable": true,
                "speed": 1.5,
                "direction": "top",
                "random": false,
                "straight": false,
                "out_mode": "out",
                "bounce": false,
                "attract": {
                    "enable": false,
                    "rotateX": 600,
                    "rotateY": 1200
                }
            }
        },
        "interactivity": {
            "detect_on": "canvas",
            "events": {
                "onhover": {
                    "enable": false,
                    "mode": "repulse"
                },
                "onclick": {
                    "enable": false,
                    "mode": "push"
                },
                "resize": true
            },
            "modes": {
                "grab": {
                    "distance": 400,
                    "line_linked": {
                        "opacity": 1
                    }
                },
                "bubble": {
                    "distance": 400,
                    "size": 40,
                    "duration": 2,
                    "opacity": 8,
                    "speed": 3
                },
                "repulse": {
                    "distance": 200,
                    "duration": 0.4
                },
                "push": {
                    "particles_nb": 4
                },
                "remove": {
                    "particles_nb": 2
                }
            }
        },
        "retina_detect": true
    });
});
/* Modals */
closeBtn.addEventListener("click", () => {
    errModal.classList.remove("show");
    modalBackground.style.display = "none";
});
tryAgainBtn.addEventListener("click", () => {
    errModal.classList.remove("show");
    modalBackground.style.display = "none";
});
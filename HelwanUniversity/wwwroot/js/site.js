        document.addEventListener('DOMContentLoaded', function () {
            const loader = document.querySelector('.loader-container');
            const header = document.querySelector('header');
            const footer = document.querySelector('footer');

            setTimeout(function () {
                loader.style.display = 'none';
                header.style.display = 'block';
                footer.style.display = 'block';
            }, 3000); 
        });
        document.addEventListener('DOMContentLoaded', function () {
            const navbar = document.querySelector('.navbar');

            function handleScroll() {
                if (window.scrollY > 50) {
                    navbar.classList.add('scrolled');
                } else {
                    navbar.classList.remove('scrolled');
                }
            }

            window.addEventListener('scroll', handleScroll);

            handleScroll();
        });
        document.addEventListener("DOMContentLoaded", function () {
            var img = new Image();
            img.src = '/img9.png';
            img.onload = function () {
                document.querySelector('.header').classList.add('loaded');
            };
        });

        // script.js

        document.addEventListener('DOMContentLoaded', () => {
            const toggle = document.getElementById('theme-toggle');
            const body = document.body;

            if (localStorage.getItem('theme') === 'dark') {
                body.classList.add('dark-theme');
                toggle.classList.replace('fa-sun', 'fa-moon');
            }

            toggle.addEventListener('click', () => {
                body.classList.toggle('dark-theme');

                if (body.classList.contains('dark-theme')) {
                    localStorage.setItem('theme', 'dark');
                    toggle.classList.replace('fa-sun', 'fa-moon');
                } else {
                    localStorage.setItem('theme', 'light');
                    toggle.classList.replace('fa-moon', 'fa-sun');
                }
            });
        });


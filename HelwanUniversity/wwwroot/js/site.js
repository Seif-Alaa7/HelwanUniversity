        document.addEventListener('DOMContentLoaded', function () {
            const loader = document.querySelector('.loader-container');
            const header = document.querySelector('header');
            const main = document.querySelector('main');
            const footer = document.querySelector('footer');


            setTimeout(function () {
                loader.style.display = 'none';
                header.style.display = 'block';
                main.style.display = 'block';
                footer.style.display = 'block';
            }, 2000); 
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


        document.addEventListener('DOMContentLoaded', () => {
            const toggle = document.getElementById('theme-toggle');
            const body = document.body;

            if (localStorage.getItem('theme') === 'dark') {
                body.classList.add('dark-theme');
                toggle.classList.replace('fa-sun', 'fa-moon');
            }

            document.addEventListener("DOMContentLoaded", function () {
                const fadeElements = document.querySelectorAll('.fade-in');

                function checkVisibility() {
                    fadeElements.forEach(element => {
                        const rect = element.getBoundingClientRect();
                        if (rect.top < window.innerHeight && rect.bottom > 0) {
                            element.classList.add('visible');
                        }
                    });
                }

                window.addEventListener('scroll', checkVisibility);
                checkVisibility();
            }); 

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




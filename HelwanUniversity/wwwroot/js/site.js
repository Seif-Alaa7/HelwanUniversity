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
    document.addEventListener('DOMContentLoaded', function() {
        document.getElementById('delete-forever').addEventListener('click', function (event) {
            event.preventDefault(); // Prevent the default link behavior
            Swal.fire({
                title: 'Are you sure?',
                text: 'You want to delete this subject forever!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'Cancel'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = this.href;
                }
            });
        });

    document.getElementById('delete-from-department').addEventListener('click', function(event) {
        event.preventDefault(); 
    Swal.fire({
        title: 'Are you sure?',
    text: 'You want to delete this subject from this department only!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#dc3545',
    cancelButtonColor: '#6c757d',
    confirmButtonText: 'Yes, delete it!',
    cancelButtonText: 'Cancel'
            }).then((result) => {
                if (result.isConfirmed) {
        window.location.href = this.href;
                }
            });
        });
    });

document.getElementById('delete-department').addEventListener('click', function (event) {
    event.preventDefault();
    Swal.fire({
        title: 'Are you sure?',
        text: 'You want to delete this subject from this department only!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = this.href;
        }
    });

    document.addEventListener('DOMContentLoaded', function () {
        document.getElementById('delete-department').addEventListener('click', function (event) {
            event.preventDefault(); // Prevent the default link behavior
            var url = this.href; // Store the URL

            Swal.fire({
                title: 'Are you sure?',
                text: 'You want to delete this department permanently!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545', // Red color for confirm button
                cancelButtonColor: '#6c757d', // Gray color for cancel button
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'Cancel',
                // Optional: Customizing the content of the dialog
                customClass: {
                    title: 'text-xl font-bold', // Custom class for title
                    confirmButton: 'bg-red-600 text-white font-bold px-4 py-2 rounded', // Custom class for confirm button
                    cancelButton: 'bg-gray-400 text-white font-bold px-4 py-2 rounded' // Custom class for cancel button
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = url; // Redirect to the URL if confirmed
                }
            });
        });
    });
});
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('delete-button').addEventListener('click', function (event) {
        Swal.fire({
            title: 'هل أنت متأكد؟',
            text: 'هل تريد حذف هذه الكلية نهائيًا؟',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#dc3545', // اللون الأحمر لزر التأكيد
            cancelButtonColor: '#6c757d', // اللون الرمادي لزر الإلغاء
            confirmButtonText: 'نعم، احذفها!',
            cancelButtonText: 'إلغاء',
            customClass: {
                title: 'text-xl font-bold', // تخصيص تصميم العنوان
                confirmButton: 'bg-red-600 text-white font-bold px-4 py-2 rounded', // تخصيص تصميم زر التأكيد
                cancelButton: 'bg-gray-400 text-white font-bold px-4 py-2 rounded' // تخصيص تصميم زر الإلغاء
            }
        }).then((result) => {
            if (result.isConfirmed) {
                document.getElementById('delete-form').submit(); // إرسال النموذج إذا تم التأكيد
            }
        });
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const navBar = document.getElementById('nav-bar');
    const toggleLabel = document.getElementById('nav-toggle-label');

    toggleLabel.addEventListener('click', function () {
        navBar.classList.toggle('open');
    });

    // Close sidebar when clicking outside
    document.addEventListener('click', function (event) {
        const isClickInside = navBar.contains(event.target) || toggleLabel.contains(event.target);
        if (!isClickInside && navBar.classList.contains('open')) {
            navBar.classList.remove('open');
        }
    });

    // Prevent closing when clicking inside the sidebar
    navBar.addEventListener('click', function (event) {
        event.stopPropagation();
    });
});

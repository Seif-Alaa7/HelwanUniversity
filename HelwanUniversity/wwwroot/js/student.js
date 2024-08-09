function applyFilters() {
    var idFilter = document.getElementById('idFilter').value.toLowerCase();
    var nameFilter = document.getElementById('nameFilter').value.toLowerCase();
    var levelFilter = document.getElementById('levelFilter').value;
    var semesterFilter = document.getElementById('semesterFilter').value;
    var facultyFilter = document.getElementById('facultyFilter').value;
    var departmentFilter = document.getElementById('departmentFilter').value;

    var table = document.getElementById('studentsTable');
    var rows = table.querySelectorAll('tbody tr');

    rows.forEach(function (row) {
        var rowId = row.getAttribute('data-id').toLowerCase();
        var rowName = row.getAttribute('data-name').toLowerCase();
        var rowLevel = row.getAttribute('data-level');
        var rowSemester = row.getAttribute('data-semester');
        var rowFaculty = row.getAttribute('data-faculty');
        var rowDepartment = row.getAttribute('data-department');

        var matchId = idFilter === "" || rowId.includes(idFilter);
        var matchName = nameFilter === "" || rowName.includes(nameFilter);
        var matchLevel = levelFilter === "" || rowLevel === levelFilter;
        var matchSemester = semesterFilter === "" || rowSemester === semesterFilter;
        var matchFaculty = facultyFilter === "" || rowFaculty === facultyFilter;
        var matchDepartment = departmentFilter === "" || rowDepartment === departmentFilter;

        if (matchId && matchName && matchLevel && matchSemester && matchFaculty && matchDepartment) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}
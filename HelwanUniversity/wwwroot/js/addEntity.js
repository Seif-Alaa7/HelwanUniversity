    document.addEventListener("DOMContentLoaded", function () {
        const entityTypeRadios = document.querySelectorAll('input[name="EntityType"]');
    const departmentFields = document.getElementById('departmentFields');
    const facultyFields = document.getElementById('facultyFields');
    const subjectFields = document.getElementById('subjectFields');

    function updateFields() {
            const selectedType = document.querySelector('input[name="EntityType"]:checked').value;
    departmentFields.style.display = selectedType === 'Department' ? 'block' : 'none';
    facultyFields.style.display = selectedType === 'Faculty' ? 'block' : 'none';
    subjectFields.style.display = selectedType === 'Subject' ? 'block' : 'none';
        }

        entityTypeRadios.forEach(radio => {
        radio.addEventListener('change', updateFields);
        });

    updateFields();
    });
document.addEventListener('DOMContentLoaded', function () {
    var fileInput = document.getElementById('profilePicture');
    var preview = document.getElementById('picturePreview');

    fileInput.addEventListener('change', function () {
        var file = this.files[0];
        if (file) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var img = document.createElement('img');
                img.src = e.target.result;

                preview.innerHTML = '';
                preview.appendChild(img);
            }
            reader.readAsDataURL(file);
        }
    });

    document.querySelector('.custom-file-upload').addEventListener('click', function (e) {
        e.preventDefault(); 
        fileInput.click();
    });
});




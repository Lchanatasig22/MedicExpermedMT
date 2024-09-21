//FUNCION BUSQUESDA PACIENTE
document.addEventListener('DOMContentLoaded', function () {
    const opcionSelect = document.getElementById('opcion');
    const buscarFields = document.getElementById('buscarFields');
    const buscarCriterio = document.getElementById('buscarCriterio');
    const noResults = document.getElementById('noResults');

    window.onSearchChanged = function () {
        if (opcionSelect.value === '-2') {
            buscarFields.classList.add('d-none');
            showAllRowsAndCards();
            noResults.classList.add('d-none');
        } else {
            buscarFields.classList.remove('d-none');
        }
    };

    window.onSearch = function () {
        const criterio = buscarCriterio.value.toLowerCase();
        const optionValue = opcionSelect.value;
        const rows = document.querySelectorAll('tbody tr');
        const cards = document.querySelectorAll('.card-body');
        let found = false;

        rows.forEach(row => {
            const cellValue = getCellValue(row, optionValue).toLowerCase();
            if (cellValue.includes(criterio)) {
                row.style.display = '';
                found = true;
            } else {
                row.style.display = 'none';
            }
        });

        cards.forEach(card => {
            const cellValue = getCardValue(card, optionValue).toLowerCase();
            if (cellValue.includes(criterio)) {
                card.parentElement.style.display = '';
                found = true;
            } else {
                card.parentElement.style.display = 'none';
            }
        });

        noResults.classList.toggle('d-none', found);
    };

    function getCellValue(row, optionValue) {
        return row.querySelector(`[data-search-type="${optionValue}"]`).innerText || '';
    }

    function getCardValue(card, optionValue) {
        return card.querySelector(`[data-search-type="${optionValue}"]`).innerText || '';
    }

    function showAllRowsAndCards() {
        document.querySelectorAll('tbody tr, .card-body').forEach(el => el.style.display = '');
    }

    
 

   
});

//Busquedas dinamicas

$(document).ready(function () {
    $('#buscarNacionalidad').on('keyup', function () {
        let query = $(this).val();

        if (query.length > 2) {
            $.ajax({
                url: urlBuscarPais,  // Asegúrate de que la URL es correcta
                method: 'GET',
                data: { term: query },
                success: function (data) {
                    $('#nacionalidadResults').empty();
                    data.forEach(function (item) {
                        $('#nacionalidadResults').append('<a href="#" class="list-group-item list-group-item-action" onclick="seleccionarNacionalidad(\'' + item.id + '\', \'' + item.nombre + '\')">' + item.nombre + '</a>');
                    });
                },
                error: function () {
                    $('#nacionalidadResults').html('<div class="text-danger">Error al buscar nacionalidades.</div>');
                }
            });
        } else {
            $('#nacionalidadResults').empty();  // Limpiar resultados si el campo está vacío
        }
    });
});

function seleccionarNacionalidad(id, nombre) {
    // Establecemos el nombre en el campo visible
    $('#buscarNacionalidad').val(nombre);

    // Establecemos el ID en el campo oculto
    $('#NacionalidadPacientesPa').val(id);

    // Limpiamos los resultados
    $('#nacionalidadResults').empty();
}

function confirmDelete(idPaciente) {
    // Usar SweetAlert para confirmar la eliminación
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'No podrás revertir esta acción!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            // Enviar la solicitud de eliminación al servidor
            fetch(`${urlDelete}?idPaciente=${idPaciente}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: new URLSearchParams({ idPaciente: idPaciente })
            })
                .then(response => response.json())
                .then(data => {
                    // Manejar la respuesta del servidor
                    Swal.fire(
                        'Eliminado!',
                        data.resultMessage,
                        'success'
                    );
                    // Opcionalmente, recargar la página o actualizar la vista
                    location.reload();
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        }
    });
}
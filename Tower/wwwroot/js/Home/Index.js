new DataTable('#tableClientes');
var Home = {
    RegistraEntrada: function () {
        let pessoaID = $(this).attr("pessoaID");
        Swal.fire({
            title: 'Entrada?',
            text: "Você deseja registrar a entrada!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sim, Registre!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Access/RegistrarEntrada",
                    data: {
                        id: pessoaID
                    },
                    type: "POST",
                    success: function () {
                        Swal.fire(
                            'Sucesso!',
                            'Entrada do usuário registrada.',
                            'success'
                        ).then((result) => {
                            window.location.reload();
                        });
                    },
                })
            }
        })
    },
    RegistraSaida: function () {
        let pessoaID = $(this).attr("pessoaID");
        Swal.fire({
            title: 'Saida?',
            text: "Você deseja registrar a saída!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sim, Registre!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Access/RegistrarSaida",
                    data: {
                        id: pessoaID
                    },
                    type: "POST",
                    success: function () {
                        Swal.fire(
                            'Sucesso!',
                            'Saída do usuário registrada.',
                            'success'
                        ).then((result) => {
                            window.location.reload();
                        });
                    }
                })
            }
        })
    },
    ready: function () {
        $("[btnEntrada]").click(Home.RegistraEntrada);
        $("[btnSaida]").click(Home.RegistraSaida);
    }
}
$(document).ready(Home.ready)
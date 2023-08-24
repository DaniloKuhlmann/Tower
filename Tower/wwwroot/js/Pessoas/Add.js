$.validator.addMethod(
    "regex",
    function (value, element, regexp) {
        var re = new RegExp(regexp);
        return this.optional(element) || re.test(value);
    },
    "Formato incorreto."
);
$("#form").validate({
    errorClass: "error",
    errorElement: "span",
    rules: {
        "Nome": {required: true},
        "CPF": {required: true, regex: "[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}"},
    },
    messages: {
        "Nome": {required: "Insira o Nome"},
        "Tipo": {required: "Campo Obrigatório"},
        "CPF": {required: "Campo Obrigatório"},

    },
})
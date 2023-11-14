$(document).ready(function () {
    $("#Input_CRM").attr('type', 'hidden');
    $("#label_CRM").attr('hidden', 'hidden');
    $("#Input_CPF").attr('type', 'hidden');
    $("#label_CPF").attr('hidden', 'hidden');
    $("#Input_ADMINCODE").attr('type', 'hidden');
    $("#label_ADMINCODE").attr('hidden', 'hidden');

    $("#Input_UserType").on("change", function () {
        var UserTypeVal = $("#Input_UserType option:selected").text();

        if (UserTypeVal == 'Admin') {
            $("#Input_ADMINCODE").removeAttr('type');
            $("#label_ADMINCODE").removeAttr('hidden');

            $("#Input_CRM").attr('type', 'hidden');
            $("#label_CRM").attr('hidden', 'hidden');
            $("#Input_CPF").attr('type', 'hidden');
            $("#label_CPF").attr('hidden', 'hidden');
        }

        if (UserTypeVal == 'Doctor') {
            $("#Input_CRM").removeAttr('type');
            $("#label_CRM").removeAttr('hidden');

            $("#Input_CPF").attr('type', 'hidden');
            $("#label_CPF").attr('hidden', 'hidden');
            $("#Input_ADMINCODE").attr('type', 'hidden');
            $("#label_ADMINCODE").attr('hidden', 'hidden');
        }

        if (UserTypeVal == 'Pacient') {
            $("#Input_CPF").removeAttr('type');
            $("#label_CPF").removeAttr('hidden');

            $("#Input_CRM").attr('type', 'hidden');
            $("#label_CRM").attr('hidden', 'hidden');
            $("#Input_ADMINCODE").attr('type', 'hidden');
            $("#label_ADMINCODE").attr('hidden', 'hidden');
        }
    })
});

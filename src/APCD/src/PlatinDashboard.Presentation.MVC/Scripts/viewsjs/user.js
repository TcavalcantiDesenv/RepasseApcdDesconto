function changeFormStructure() {
    if ($('#UserType').val() == 'Manager') {
        $('#StoreAccess').hide('slow');
    }
    else {
        $('#StoreAccess').show('slow');
    }
}

function bindUserTypeChange() {
    $('#UserType').bind('change', function () {
        changeFormStructure();
    })
}

function bindDocumentReady() {
    $(document).ready(function () {
        $('.select2-element').select2(
            {
                placeholder: "Lojas",
                allowClear: true,
                templateSelection: function (tag, container) {
                    // here we are finding option element of tag and
                    // if it has property 'locked' we will add class 'locked-tag' 
                    // to be able to style element in select
                    var $option = $('.select2-element option[value="' + tag.id + '"]');
                    if ($option.attr('locked')) {
                        $(container).addClass('locked-tag');
                        tag.locked = true;
                    }
                    return tag.text;
                }
            });
        changeFormStructure();
    });
}

function disableSomeOptions(arrayIds) {
    if (arrayIds[0] != undefined) {
        $('#UserStoresIds > option').each(function (index, element) {
            if (!($.inArray(parseInt($(element).val()), arrayIds) > -1)) {
                console.log($(element).val());
                $(element).prop('disabled', true);
                $(element).attr('locked', 'locked');
            }
        });
    }    
}
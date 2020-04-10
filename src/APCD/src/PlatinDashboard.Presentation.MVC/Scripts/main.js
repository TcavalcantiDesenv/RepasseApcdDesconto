function initOpenAndCloseDetails() {
    $('#grid1 div').click(function () {
        if ($(this).attr('class') === 'collapsed') {
            $(this).attr('class', 'expanded');
            var ul = $(this).parent().parent().closest('tr').next('tr').find('.details-opened').removeClass('details-opened');
            $(ul).addClass('details-closed');

        } else {
            $(this).attr('class', 'collapsed');
            var ul = $(this).parent().parent().closest('tr').next('tr').find('.details-closed').removeClass('details-closed');
            $(ul).addClass('details-opened');
        }
    });
}

function initGetData() {

    $('#btnGetData').click(function () {
        $("#grid1").find("tr:gt(0)").remove();
        var request = new XMLHttpRequest();
        request.open('GET', '../LeisParametros/GetCompaniesData', true);
        request.onload = function () {
            if (request.status === 200) {
                var myStuff = JSON.parse(request.responseText);
                console.log(myStuff);
                var i = 0;
                var grid1 = $('#grid1');

                while (i <= myStuff.length - 1) {
                    var obj = myStuff[i];
                    var addresslist = '';
                    var li = '';

                    var j = 0;
                    while (j <= obj.details.length - 1) {
                        li = li + "<li>" + obj.details[j] + "</li>";
                        j++;
                    }
                    addresslist = "<tr><td colspan=4><ul class='details-closed'>" + li + "</ul></td></tr>";
                    grid1.append('<tr><td><div class=expanded id=' + i + '></div></td><td>' + obj.name + '</td><td>' + obj.bio + '</td><td>' + obj.headquarters + '</td></tr>' + addresslist);
                    i++;
                }
                initOpenAndCloseDetails();
            }
        }

        request.send();
    });
}

initGetData();
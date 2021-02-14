$(document).ready(function () {
    let regionArray = '';

    fetch('/Home/Regions')
        .then(response => response.json())
        .then(regionResponse => {
            let data = $.map(regionResponse.data, function (obj) {
                obj.id = obj.id || obj.iso;
                obj.text = obj.text || obj.name;

                return obj;
            })
            $('#regionSelect').select2({
                data: data,
                allowClear: true,
                placeholder: {
                    id: '-1',
                    text: 'Select a region'
                },
            });

            $('#regionSelect').val(null).trigger('change');
        });

    $('#table_id').DataTable({
        'ajax': '/Home/RegionStatistics',
        'columns': [
            { "data": "region" },
            {
                data: "cases",
                render: $.fn.dataTable.render.number(',', '.')
            },
            {
                data: "deaths",
                render: $.fn.dataTable.render.number(',', '.')
            },
        ]
    });
    //fetch('/Home/RegionStatistics')
    //    .then(response => response.json())
    //    .then(data => console.log(data));

});
let $regionSelect;
let $dataTable;

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

            $regionSelect = $('#regionSelect').val(null).trigger('change');
            getReport();
        });
});

function getReport() {

    if (typeof $dataTable !== 'undefined') {
        $dataTable.destroy();
    }

    let selectedCountry = $regionSelect.select2('data');
    if (selectedCountry.length > 0) {
        $dataTable = $('#covidInfoTable').DataTable({
            'ajax': '/Home/RegionStatistics/' + selectedCountry[0].id,
            'columns': [
                {
                    data: "region"
                },
                {
                    data: "cases",
                    render: $.fn.dataTable.render.number(',', '.')
                },
                {
                    data: "deaths",
                    render: $.fn.dataTable.render.number(',', '.')
                },
            ],
            "order": [[1, "desc"]]
        });
    }
    else {
        $dataTable = $('#covidInfoTable').DataTable({
            'ajax': '/Home/RegionStatistics',
            'columns': [
                {
                    data: "region"
                },
                {
                    data: "cases",
                    render: $.fn.dataTable.render.number(',', '.')
                },
                {
                    data: "deaths",
                    render: $.fn.dataTable.render.number(',', '.')
                },
            ],
            "order": [[1, "desc"]]
        });
    }

}

function getJSONFile() {
    let endpoint = "/Home/JSONExport/"

    let selectedCountry = $regionSelect.select2('data');
    if (selectedCountry.length > 0) {
        endpoint += selectedCountry[0].id;
    }
    window.location.href = endpoint;
}

function getXMLFile() {
    let endpoint = "/Home/XMLExport/"

    let selectedCountry = $regionSelect.select2('data');
    if (selectedCountry.length > 0) {
        endpoint += selectedCountry[0].id;
    }
    window.location.href = endpoint;
}

function getCSVFile() {
    let endpoint = "/Home/CSVExport/"

    let selectedCountry = $regionSelect.select2('data');
    if (selectedCountry.length > 0) {
        endpoint += selectedCountry[0].id;
    }
    window.location.href = endpoint;
}


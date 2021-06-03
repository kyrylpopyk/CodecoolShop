let types_app_data;

function index_start() {
    collect_data();
    set_events();
}

function collect_data() {
    types_app_data = document.querySelectorAll(".select-type-main");
}

function set_events() {
    set_event_for_types_app_data();
}

function set_event_for_types_app_data() {
    for (element in types_app_data) {
        element.onclick = function () {
            type_data = {
                type: element.getAttribute("main-type"),
                id: element.id
            };
            api_post(`${window.location.origin}/Product/Index`, type_data);
        };
    }
}

function api_post(url, data, callback= ()=>{ }) {

    fetch(url, {
        method: 'POST',
        headers: new Headers({
            'content-type': 'application/json'
        }),
        credentials: 'same-origin',
        body: JSON.stringify(data)
    })
    .then((response) => {
        return response.json()
    })
    .then(response => {
        callback(response);
    })


}

index_start();
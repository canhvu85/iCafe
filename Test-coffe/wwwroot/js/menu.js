let user2 = JSON.parse(sessionStorage.getItem('user'));
$(function () {
    var str = '';

    axios({
        url: "/api/MenusAPI",
        method: "GET",
        headers: {
            'content-type': 'application/json',
            'Authorization': user2.remember_token
        }
    }).then(function (response) {
        var items = response.data.filter(function (rs) {
            return rs.parentId == 0;
        });
        if (items.length > 0) {
            var child;
            for (var i = 0; i < items.length; i++) {

                child = response.data.filter(function (rs) {
                    return rs.parentId == items[i].id;
                });
                if (child.length > 0) {
                    str += `<li class="nav-item has-treeview">
                                                        <a href="#" class="nav-link">
                                                            <i class="${items[i].icon}"></i>
                                                            <p>
                                                                ${items[i].name}
                                                                <i class="right fas fa-angle-left"></i>
                                                            </p>
                                                        </a>`;
                    var child2;
                    for (var j = 0; j < child.length; j++) {
                        child2 = response.data.filter(function (rs) {
                            return rs.parentId == child[j].id;
                        });
                        str += `<ul class="nav nav-treeview treeview-menu">`;
                        if (child2.length > 0) {
                            str += `<li class="nav-item has-treeview treeview">
                                                        <a href="#" class="nav-link">
                                                            <i class="${child[j].icon}"></i>
                                                            <p>
                                                                ${child[j].name}
                                                                <i class="right fas fa-angle-left"></i>
                                                            </p>
                                                        </a>`;
                            str += `<ul class="nav nav-treeview treeview-menu">
            <li class="nav-item treeview">`;
                            for (var k = 0; k < child2.length; k++) {
                                str += `<a href="${child2[k].url}" class="nav-link">
                                                                    <i class="${child2[k].icon}"></i>
                                                                    <p>${child2[k].name}</p>
                                                                </a>`
                            }
                            str += `</li> </ul></li>`;
                        } else {
                            str += `<li class="nav-item has-treeview treeview">
                                                        <a href="${child[j].url}" class="nav-link">
                                                            <i class="${child[j].icon}"></i>
                                                            <p>${child[j].name}</p>
                                                        </a>
                                                    </li>`;
                        }
                        str += `</ul>`;
                    }
                    str += `</li > `;
                } else {
                    str += `< li class="nav-item has-treeview" >
    <a href="${items[i].url}" class="nav-link">
        <i class="${items[i].icon}"></i>
        <p>${items[i].name}</p>
    </a></li > `;
                }
            }
        }
        $("#sideBar").html(str);
    })
});

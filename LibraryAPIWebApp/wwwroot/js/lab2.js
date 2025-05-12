const uri = 'api/Categories';
let categories = [];

function getCategories() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayCategories(data))
        .catch(error => console.error('Unable to get categories.', error));
}

function addCategory() {
    const addNameTextbox = document.getElementById('add-name');
    const addDescriptionTextbox = document.getElementById('add-description');

    const category = {
        name: addNameTextbox.value.trim(),
        description: addDescriptionTextbox.value.trim()
    };


    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
        .then(response => response.json())
        .then(() => {
            getCategories();
            addNameTextbox.value = '';
            addDescriptionTextbox.value = '';
        })
        .catch(error => console.error('Unable to add category.', error));
}

function deleteCategory(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getCategories())
        .catch(error => console.error('Unable to delete category.', error));
}

function displayEditForm(id) {
    const category = categories.find(category => category.id === id);

    document.getElementById('edit-id').value = category.id;
    document.getElementById('edit-name').value = category.name;
    document.getElementById('edit-description').value = category.description;
    document.getElementById('editForm').style.display = 'block';
}

function updateCategory() {
    const categoryId = document.getElementById('edit-id').value;
    const category = {
        id: parseInt(categoryId, 10),
        name: document.getElementById('edit-name').value.trim(),
        description: document.getElementById('edit-description').value.trim()
    };


    fetch(`${uri}/${categoryId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
        .then(() => getCategories())
        .catch(error => console.error('Unable to update category.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayCategories(data) {
    const tBody = document.getElementById('categories');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(category => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${category.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteCategory(${category.id})`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(category.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(category.description);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    categories = data;
}

//const uri = 'api/Categories';
//let categories = [];

//// Функция для получения категорий с сервера
//function getCategories() {
//    fetch(uri)
//        .then(response => response.json())
//        .then(data => _displayCategories(data))
//        .catch(error => console.error('Не удалось получить категории.', error));
//}

//// Получаем правильные ID полей из вашего HTML
//// Функция для добавления новой категории
//function addCategory() {
//    console.log('Функция addCategory вызвана');

//    // Используем правильные ID из вашего HTML
//    const addNameTextbox = document.querySelector('input[id^="add-"]');
//    console.log('input element for name:', addNameTextbox);

//    const addDescriptionTextbox = document.querySelectorAll('input[id^="add-"]')[1];
//    console.log('input element for description:', addDescriptionTextbox);

//    // Проверяем, что элементы найдены, прежде чем использовать их
//    if (!addNameTextbox || !addDescriptionTextbox) {
//        console.error('Не удалось найти поля формы для добавления категории');
//        // Попробуем альтернативный способ получения элементов
//        const inputs = document.querySelectorAll('form[action*="addCategory"] input[type="text"]');
//        if (inputs.length >= 2) {
//            addNameTextbox = inputs[0];
//            addDescriptionTextbox = inputs[1];
//            console.log('Найдены альтернативные поля:', inputs);
//        } else {
//            return;
//        }
//    }

//    // Используем заглавные буквы для свойств, чтобы соответствовать модели C#
//    const category = {
//        Name: addNameTextbox.value.trim(),
//        Description: addDescriptionTextbox.value.trim(),
//        // Убедимся, что BookCategories инициализирован пустым массивом
//        BookCategories: []
//    };

//    console.log('Категория для отправки:', category);

//    fetch(uri, {
//        method: 'POST',
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(category)
//    })
//        .then(response => {
//            console.log('Ответ сервера на POST:', response);
//            if (!response.ok) {
//                return response.text().then(text => {
//                    throw new Error(`Сервер вернул ошибку: ${response.status}. Подробности: ${text}`);
//                });
//            }
//            return response.json();
//        })
//        .then(data => {
//            console.log('Полученные данные после добавления:', data);
//            getCategories();
//            if (addNameTextbox) addNameTextbox.value = '';
//            if (addDescriptionTextbox) addDescriptionTextbox.value = '';
//        })
//        .catch(error => {
//            console.error('Не удалось добавить категорию.', error);
//            alert('Ошибка при добавлении категории: ' + error.message);
//        });
//}

//fetch(uri, {
//    method: 'POST',
//    headers: {
//        'Accept': 'application/json',
//        'Content-Type': 'application/json'
//    },
//    body: JSON.stringify(category)
//})
//    .then(response => response.json())
//    .then(() => {
//        getCategories();
//        addNameTextbox.value = '';
//        addDescriptionTextbox.value = '';
//    })
//    .catch(error => console.error('Не удалось добавить категорию.', error));

//// Функция для удаления категории
//function deleteCategory(id) {
//    console.log(`Удаление категории с id=${id}`);

//    // Проверяем, что id является числом и не undefined
//    if (id === undefined || isNaN(parseInt(id))) {
//        console.error('Некорректный ID для удаления:', id);
//        return;
//    }

//    fetch(`${uri}/${id}`, {
//        method: 'DELETE',
//        headers: {
//            'Accept': 'application/json'
//        }
//    })
//        .then(response => {
//            console.log('Ответ на DELETE:', response);
//            if (!response.ok) {
//                return response.text().then(text => {
//                    throw new Error(`Сервер вернул ошибку: ${response.status}. Подробности: ${text}`);
//                });
//            }
//            return response;
//        })
//        .then(() => getCategories())
//        .catch(error => console.error('Не удалось удалить категорию.', error));
//}

//// Функция для отображения формы редактирования категории
//function displayEditForm(id) {
//    console.log('Отображение формы редактирования для id:', id);

//    const category = categories.find(item => item.Id === id);
//    if (!category) {
//        console.error(`Категория с id ${id} не найдена`);
//        return;
//    }

//    const editIdField = document.getElementById('edit-id');
//    const editNameField = document.getElementById('edit-name');
//    const editDescriptionField = document.getElementById('edit-description'); // Исправлена опечатка
//    const editCategoryDiv = document.getElementById('editCategory');

//    if (!editIdField || !editNameField || !editDescriptionField || !editCategoryDiv) {
//        console.error('Не удалось найти один или несколько элементов формы редактирования');
//        console.log('edit-id:', editIdField);
//        console.log('edit-name:', editNameField);
//        console.log('edit-description:', editDescriptionField);
//        console.log('editCategory:', editCategoryDiv);
//        return;
//    }

//    editIdField.value = category.Id;
//    editNameField.value = category.Name || '';
//    editDescriptionField.value = category.Description || '';

//    editCategoryDiv.style.display = 'block';
//}

//// Функция для обновления категории
//function updateCategory() {
//    console.log('Функция updateCategory вызвана');

//    const editIdField = document.getElementById('edit-id');
//    const editNameField = document.getElementById('edit-name');
//    const editDescriptionField = document.getElementById('edit-description');

//    if (!editIdField || !editNameField || !editDescriptionField) {
//        console.error('Не удалось найти один или несколько элементов формы редактирования');
//        // Попробуем альтернативный способ найти поля
//        const editForm = document.querySelector('form[onsubmit*="updateCategory"]');
//        if (editForm) {
//            const inputs = editForm.querySelectorAll('input');
//            if (inputs.length >= 3) {
//                editIdField = inputs[0];
//                editNameField = inputs[1];
//                editDescriptionField = inputs[2];
//                console.log('Найдены альтернативные поля для редактирования:', inputs);
//            } else {
//                return;
//            }
//        } else {
//            return;
//        }
//    }

//    const categoryId = editIdField.value;
//    if (!categoryId) {
//        console.error('ID категории отсутствует или некорректный');
//        return;
//    }

//    // Используем заглавные буквы для свойств, чтобы соответствовать модели C#
//    const category = {
//        Id: parseInt(categoryId, 10),
//        Name: editNameField.value.trim(),
//        Description: editDescriptionField.value.trim(),
//        BookCategories: [] // Убедимся, что BookCategories инициализирован
//    };

//    console.log('Обновляемая категория:', category);

//    fetch(`${uri}/${categoryId}`, {
//        method: 'PUT',
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(category)
//    })
//        .then(response => {
//            console.log('Ответ сервера на PUT:', response);
//            if (!response.ok) {
//                return response.text().then(text => {
//                    throw new Error(`Сервер вернул ошибку: ${response.status}. Подробности: ${text}`);
//                });
//            }
//            return response;
//        })
//        .then(() => {
//            getCategories();
//            closeInput();
//        })
//        .catch(error => {
//            console.error('Не удалось обновить категорию.', error);
//            alert('Ошибка при обновлении категории: ' + error.message);
//        });
//}

//// Функция для закрытия формы редактирования
//function closeInput() {
//    document.getElementById('editCategory').style.display = 'none';
//}

//// Функция для отображения списка категорий
//function _displayCategories(data) {
//    console.log('Полученные данные:', data);

//    const tBody = document.getElementById('categories');
//    if (!tBody) {
//        console.error('Элемент с id "categories" не найден');
//        return;
//    }

//    tBody.innerHTML = '';

//    // Проверка, что данные существуют и это массив
//    if (!Array.isArray(data)) {
//        console.error('Данные не являются массивом:', data);
//        return;
//    }

//    data.forEach(category => {
//        console.log('Обработка категории:', category);

//        // Проверяем, что у категории есть Id
//        if (category.Id === undefined || category.Id === null) {
//            console.error('У категории отсутствует Id:', category);
//            return;
//        }

//        let editButton = document.createElement('button');
//        editButton.innerText = 'Редактировать';
//        editButton.setAttribute('onclick', `displayEditForm(${category.Id})`);

//        let deleteButton = document.createElement('button');
//        deleteButton.innerText = 'Удалить';
//        deleteButton.setAttribute('onclick', `deleteCategory(${category.Id})`);

//        let tr = tBody.insertRow();

//        let td1 = tr.insertCell(0);
//        let textNode = document.createTextNode(category.Name || '');
//        td1.appendChild(textNode);

//        let td2 = tr.insertCell(1);
//        let textNodeDesc = document.createTextNode(category.Description || '');
//        td2.appendChild(textNodeDesc);

//        let td3 = tr.insertCell(2);
//        td3.appendChild(editButton);

//        let td4 = tr.insertCell(3);
//        td4.appendChild(deleteButton);
//    });

//    categories = data;

//    // Обновляем счетчик категорий
//    const counter = document.getElementById('counter');
//    if (counter) {
//        counter.innerText = `Всего категорий: ${data.length}`;
//    } else {
//        console.error('Элемент с id "counter" не найден');
//    }
//}
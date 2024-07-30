﻿function deleteTodo(i, name)
 {
    const confirmDelete = confirm('Are you sure you want to delete the todo item with ID # ' + i + '?');

     if (confirmDelete) {
         $.ajax({
             url: 'Home/DeleteTodo',
             type: 'POST',
             data: {
                 id: i,
                 name: name
             },
             success: function (response) {
                 if (response.success) {
                     window.location.reload();
                     alert(`Todo item "${name}" deleted successfully.`);
                 } else {
                     alert(response.message);
                 }
             }
         });
     }
}

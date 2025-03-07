AZ.Documents = (function (ko) {
	"use strict";

	var docViewModel = {
		documents: ko.observableArray(),
	};


	// Do this on start
	$(document).ready(function () {

		// Set the URL based in whether we are running locally or not
		// The URL locations are set in vars.js
		// This is a hack and is better solved via a build process
		// Now get the data
		// getApiData();
	});

	return {
		model: docViewModel,

		GetApiData: function() {
			// Get list of Documents for the current user (as determined by the API)
			AZ.Ajax.MakeAjaxCall("GET",
				apiUrl,
				null,
				function (data) {
					docViewModel.documents = ko.observableArray(data);
					ko.applyBindings(docViewModel);
				});
		},

		AddEdit: function() {
			var newTodo = prompt("Enter new todo item:");
			if (newTodo) {
				var newTodoItem = { ItemName: newTodo }
				AZ.Ajax.MakeAjaxCall("POST",
					apiUrl,
					JSON.stringify(newTodoItem),
					function (data) {
						docViewModel.documents.push(data);
					});				
			}
		},

		Remove: function(item) {
			if (confirm("Mark item '" + item.itemName + "' as completed?")) {
				AZ.Ajax.MakeAjaxCall("DELETE",
					apiUrl + "/" + item.id,
					null,
					function (data) {
						docViewModel.documents.remove(item);
					});								
			}
		}
	}
}(ko));
/// <reference path="knockout.js" />
/// <reference path="knockout-validation.js" />

$(document).ready(function () {
    viewModel = new EditViewModel();
    ko.applyBindings(viewModel);
});

function EditViewModel() {
    var self = this;
    self.result = ko.observableArray();
    self.isError = ko.observable();
    self.question = ko.observable(null);
    self.clear = function () {
        self.result([]);
    }
    self.updateQuiz = function (response) {
        self.response = ko.mapping.fromJS(self.response, {}, response);
        console.log(self.response);
        var responseResult = JSON.parse(response.responseText);
        self.isError(responseResult.Success);
        console.log(self.isError());
        self.result.push(responseResult.Message);
    }
    self.addQuestion = function () {
        self.question(new question());
    }
    console.log(self.result());
}


var question = function () {
    var self = this;

    var answer = function () {
        var self = this;
        self.title = ko.observable().extend({ required: true, maxLength: 100 });
        self.isTrue = ko.observable(false);
    }

    self.title = ko.observable().extend({ required: true, maxLength: 100 });
    self.options = ko.observableArray();
    self.optionsCount = ko.observable().extend({ numeric: 10 });

    self.setOptionsCount = function () {
        if (self.optionsCount() > self.options().length) {
            for (var i = self.options().length; i < self.optionsCount() ; i++) {
                self.options.push(new answer());
            }
        } else {
            for (var i = self.options().length; i > self.optionsCount() ; i--) {
                self.options.pop();
            }
        }
    }
}
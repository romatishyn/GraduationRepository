/// <reference path="jquery-2.2.1.min.js" />
/// <reference path="knockout.js" />

function QuizListViewModel() {
    var self = this;

    self.tests = ko.observableArray();

    //self.test = ko.observable();

    self.newTest = ko.observable(null);


    var test = function () {
        var self = this;
        self.title = ko.observable();
        self.questions = ko.observableArray();
        self.questionsCount = ko.observable();

        self.setQuestionsCount = function () {
            if (self.questionsCount() > self.questions().length) {
                for (var i = self.questions().length; i < self.questionsCount() ; i++) {
                    self.questions.push(new question());
                }
            } else {
                for (var i = self.questions().length; i > self.questionsCount() ; i--) {
                    self.questions.pop();
                }
            }
        }
    };

    var question = function () {
        var self = this;

        var answer = function () {
            var self = this;
            self.title = ko.observable();
            self.isTrue = ko.observable(false);
        }

        self.title = ko.observable();
        self.options = ko.observableArray();
        self.optionsCount = ko.observable();

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


    self.loadTests = function () {

        var url = "/Home/GetTestsList";
        $.ajax({
            url: url,
            method: 'POST',
            dataType: 'json',
            contentType: "application/json",
            success: function (tests) {
                self.tests(tests);
            }
        });
    }

    self.uploadTest = function (test) {

        var url = "/Home/AddNewTest";
        $.ajax({
            url: url,
            method: 'POST',
            dataType: 'json',
            data: JSON.stringify(test),
            contentType: "application/json",
            success: function () {
            }
        });
    }

    self.addNewTest = function () {
        //self.newTest(new Object());
        self.newTest(new test());
    }



    self.save = function () {

        var test = new TestModel(self.newTest());
        console.log(test);
        self.uploadTest(test);

    }
}



$(document).ready(function () {
    var viewModel = new QuizListViewModel();
    viewModel.loadTests();
    ko.applyBindings(viewModel);
})


function TestModel(test) {
    var self = this;
    self.Title = test.title();
    self.Questions = [];

    var constructOptions = function (optionsList) {
        var options = [];
        for (var i = 0; i < optionsList.length; i++) {
            var option = optionsList[i];
            options.push({ Title: option.title(), IsCorrect: option.isTrue() });
        }
        return options;
    }

    for (var i = 0; i < test.questions().length; i++) {
        var question = test.questions()[i];
        self.Questions.push({ Title: question.title(), Options: constructOptions(question.options()) });
    }
}
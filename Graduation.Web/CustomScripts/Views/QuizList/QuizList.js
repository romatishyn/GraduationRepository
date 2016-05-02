/// <reference path="knockout.js" />
/// <reference path="knockout-validation.js" />

$(document).ready(function () {
    var viewModel = new QuizListViewModel();
    viewModel.loadTests();
    ko.applyBindings(viewModel);
});

ko.validation.init({
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
});

ko.extenders.numeric = function (target, maxValue) {
    //create a writable computed observable to intercept writes to our observable
    var result = ko.pureComputed({
        read: target,  //always return the original observables value
        write: function (newValue) {
            var current = target();
            if (newValue)
                var valueToWrite = newValue.trim().replace(/\D/g, '');
            if (valueToWrite > maxValue) {
                valueToWrite = maxValue;
            }
            //only write if it changed
            if (valueToWrite != current) {
                target(valueToWrite);
            } else {
                //if the rounded value is the same, but a different value was written, force a notification for the current field
                if (newValue !== current) {
                    target.notifySubscribers(valueToWrite);
                }
            }
        }
    }).extend({ notify: 'always' });

    //initialize with current value to make sure it is rounded appropriately
    result(target());

    //return the new computed observable
    return result;
};



function QuizListViewModel() {
    var self = this;
    self.url = ko.observable("/Home/Quiz/");
    self.tests = ko.observableArray();
    self.pickTest = function (test) {
        location.href = self.url() + test.Id;
    }
    self.newTest = ko.validatedObservable(null);
    self.errors = ko.observableArray();

    var test = function () {
        var self = this;
        self.title = ko.observable().extend({ required: true, maxLength: 100 });
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
        self.newTest(new test());
    }


    self.save = function () {
        self.errors([]);
        if (self.hasTestContent()) {
            var validProps = [];
            validProps.push(self.newTest().title);
            for (var i = 0; i < self.newTest().questions().length; i++) {
                validProps.push(self.newTest().questions()[i].title);
                for (var j = 0; j < self.newTest().questions()[i].options().length; j++) {
                    validProps.push(self.newTest().questions()[i].options()[j].title);
                }
            }

            var errors = ko.validation.group(validProps);
            console.log(self.errors());
            console.log("errors count: " + self.errors().length);
            if (errors().length == 0) {
                //if true
                //var test = new TestModel(self.newTest());
                //console.log(test);
                //self.uploadTest(test);
            } else {
                //eat it
                errors.showAllMessages();
                self.errors.push("The content does not meet the validation rules");
            }
        } else {
            console.log(self.hasTestContent());
            self.errors.push("You should create Test with proper fields");
        }
        console.log(self.errors());
    }

    self.hasTestContent = ko.computed(function () {
        if (self.newTest() && self.newTest().questions().length > 0) {
            return true;
            //for (var i = 0; i < self.newTest().questions().length; i++) {
            //return self.newTest().questions()[i].options().length > 0;
            //} 
        } else {
            return false;
        }
    });
}

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
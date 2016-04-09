/// <reference path="jquery-2.2.1.min.js" />
/// <reference path="knockout.js" />

function QuizViewModel() {
    var self = this;
    self.title = ko.observable("loading question...");
    self.options = ko.observableArray([]);
    self.correctAnswer = ko.observable(false);
    self.working = ko.observable(true);
    self.answered = ko.observable(false);
    self.isLast = ko.observable(false);
    self.TestId = ko.observable(Model.TestId);
    self.CurrentQuestion = ko.observable(0);
    self.answer = ko.computed(function () {
        return self.correctAnswer() ? 'correct' : 'incorrect';
    });

    self.nextQuestion = function () {
        self.title("loading question...");
        self.isLast(false);
        var url = "/api/trivia/" + self.TestId() + "/" + self.CurrentQuestion();
        $.ajax({
            url: url,
            method: 'GET',
            contentType: "application/json",
            success: function (data) {
                self.options(data.options);
                self.title(data.title);
                self.isLast(data.isLast);
                self.answered(false);
                self.working(true);
                self.CurrentQuestion(self.CurrentQuestion() + 1);
            },
            error: function () {
                self.title("Oops... something went wrong");
                self.working(false);
            }
        });
    }

    self.sendAnswer = function (option) {

        var url = "/api/trivia";
        var answer = { 'questionId': option.questionId, 'optionId': option.id }
        $.ajax({
            url: url,
            method: 'POST',
            dataType: 'json',
            data: answer,
            success: function (data) {
                self.correctAnswer(data === true);
                self.working(false);
                self.answered(true);
            },
            error: function () {
                self.title("Oops... something went wrong");
                self.working(false);
            }
        });
    }
}

$(document).ready(function () {
    var viewModel = new QuizViewModel();
    viewModel.nextQuestion();
    ko.applyBindings(viewModel);
});



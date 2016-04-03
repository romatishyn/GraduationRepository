
QuizApi = angular.module('QuizListApp', [])
    .controller('QuizListCtrl', function ($scope, $http) {

        $scope.title = "Available tests";
        $scope.listOfTests = [];
        $scope.testCreateForm = null;
        $scope.optionsCount = 0;
        $scope.loadTests = function () {
            $http.post("/Home/GetTestsList").then(function (data, status, headers, config) {
                $scope.listOfTests = data.data;
            }, function (data, status, headers, config) {
                $scope.title = "Oops... something went wrong";
            });
        }

        $scope.question = {};

        $scope.answer = {};

        $scope.getNumber = function (num) {
            return new Array(num);
        }

        $scope.goToLink = function (testId) {
            location.href = "/Home/Quiz/" + testId;
        }

        $scope.addNewAnswer = function () {
            console.log($scope.testCreateForm);
        }

        $scope.addNewTest = function () {
            $scope.testCreateForm = {};
        }

        $scope.update = function (question) {
            $scope.question = angular.copy(question);
            var x = $scope.question;
        };

        $scope.reset = function () {
            $scope.user = angular.copy($scope.master);
        };
        $scope.log = function () {
            console.log($scope.testCreateForm.questionTitle);
        }

    });


QuizApi = angular.module('QuizListApp', [])
    .controller('QuizListCtrl', function ($scope, $http) {

        $scope.tests = [];
        $scope.test = {};
        $scope.questions = [];

        $scope.title = {};
        $scope.questionsCount = 0;

        $scope.question = {};

        $scope.newTest = null;

        var question = function () {
            var self = this;

            var answer = function () {
                var self = this;
                self.title = ko.observable();
                self.isTrue = ko.observable();
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

        $scope.loadTests = function (num) {
            $http.post("/Home/GetTestsList").then(function (data, status, headers, config) {
                $scope.tests = data.data;
            }, function (data, status, headers, config) {
                $scope.tests = null;
            });
        }

        $scope.addNewTest = function (testId) {
            $scope.newTest = true;
        }

        $scope.setQuestionsCount = function () {
            if ($scope.questionsCount() > $scope.questions().length) {
                for (var i = $scope.questions().length; i < $scope.questionsCount() ; i++) {
                    $scope.questions.push(new question());
                }
            } else {
                for (var i = $scope.questions().length; i > $scope.questionsCount() ; i--) {
                    $scope.questions.pop();
                }
            }
        }

    });

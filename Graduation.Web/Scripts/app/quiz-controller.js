var QuizApi = angular.module('QuizApp', ['ngRoute']);
    //.config(function($routeProvider, $locationProvider) {
    //    $routeProvider.when('/', {
    //        templateUrl: '/Home/Index/',
    //        controller: 'QuizCtrl'
    //    });
    //});

QuizApi.controller('QuizCtrl', function ($scope, $http) {
    $scope.answered = false;
    $scope.title = "loading question...";
    $scope.options = [];
    $scope.correctAnswer = false;
    $scope.working = false;

    $scope.answer = function () {
        return $scope.correctAnswer ? 'correct' : 'incorrect';
    };

    $scope.nextQuestion = function () {
        $scope.working = true;
        $scope.answered = false;
        $scope.title = "loading question...";
        $scope.options = [];
        $scope.isLast = false;

        $http.get("/api/trivia").success(function (data, status, headers, config) {
            $scope.options = data.options;
            $scope.title = data.title;
            $scope.isLast = data.isLast;
            $scope.answered = false;
            $scope.working = false;
        }).error(function (data, status, headers, config) {
            $scope.title = "Oops... something went wrong";
            $scope.working = false;
        });
    }

    $scope.sendAnswer = function (option) {
        $scope.working = true;
        $scope.answered = true;

        $http.post('/api/trivia', { 'questionId': option.questionId, 'optionId': option.id }).success(function (data, status, headers, config) {
            $scope.correctAnswer = (data === true);
            $scope.working = false;
        }).error(function (data, status, headers, config) {
            $scope.title = "Oops... something went wrong";
            $scope.working = false;
        });
    };
});

var configFunction = function ($routeProvider) {
    $routeProvider.
        when('/routeOne', {
            templateUrl: 'home/quiz'
        });
}
configFunction.$inject = ['$routeProvider'];

QuizApi.config(configFunction);
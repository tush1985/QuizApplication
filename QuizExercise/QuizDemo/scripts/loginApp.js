
// create our angular app and inject ngAnimate and ui-router 
// =============================================================================
angular.module('formApp', ['ngAnimate', 'ui.router'])

// configuring our routes 
// =============================================================================
.config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider

    // route to show our basic form (/form)
        .state('form', {
            url: '/form',
            templateUrl: 'form.html',
            controller: 'formController'
        })

    // nested states 
    // each of these sections will have their own view
    // url will be nested (/form/quiz1)
        .state('form.quiz1', {
            url: '/quiz1',
            templateUrl: 'form-quiz1.html'
        })

    // url will be /form/quiz2
        .state('form.quiz2', {
            url: '/quiz2',
            templateUrl: 'form-quiz2.html'
        })

    // url will be /form/quiz3
        .state('form.quiz3', {
            url: '/quiz3',
            templateUrl: 'form-quiz3.html'
        });

    // catch all route
    // send users to the form page 
    $urlRouterProvider.otherwise('/form/quiz1');
})

// our controller for the form
// =============================================================================
.controller('formController', function ($scope, quizService) {

    // we will store all of our form data in this object
    $scope.formData = {};
    $scope.errorCode = "";
    //Assign a UserID after Authentication    
    //1 for admin user
    //2 for client user
    $scope.userId = 1;
    $scope.errorMessage = "";
    //set the admin user
    $scope.changeToAdmin = function () {
        $scope.userId = "1";
    };
    //set the client user
    $scope.changeToClient = function () {
        $scope.userId = "2";
    };

    // function to get the quiz qestion as init
    $scope.init = function () {
        quizService.getQuestion($scope.userId)
        .then(userComplete, errorResponse);
    };
    //promise back from init call
    var userComplete = function (response) {

        $scope.formData = response;
        //ErrorCode>0 then error from API
        if (response.ErrorCode > 0) {
            $scope.errorMessage = response.ErrorMessage;
            $scope.errorCode = response.ErrorCode;
            console.log($scope.errorCode);
        }
        //no more questions left on quiz
        else if (response.ErrorCode < 0) {
            $scope.formData = {};
            $scope.errorMessage = response.ErrorMessage;
            $scope.errorCode = response.ErrorCode;
            console.log($scope.errorCode);
        } //if no error code then populate data
        else {
            $scope.formData = {};
            $scope.formData.question = response.question.question;
            $scope.formData.ID = response.question.ID;
            $scope.formData.AnswersOfQuestion = response.answers;
            $scope.errorMessage = response.ErrorMessage;
            $scope.errorCode = response.ErrorCode;
            console.log($scope.errorCode);
        }

      };
    //if the error response back from promise
    var errorResponse = function (response) {

        $scope.errorMessage = response.ErrorMessages;
        $scope.formData = {};
        $scope.errorCode = response.ErrorCode;
        console.log($scope.errorCode);
    };

    //submitting an answer
    $scope.nextQuestion = function () {
        //call the service
        $scope.formData.userId = $scope.userId;
        quizService.submitAnswer($scope.formData)
        .then(userSubmitComplete, errorSubmitResponse);

    };
    //When the promise back
    var userSubmitComplete = function (response) {
        //ErrorCode>0 then error from API
        if (response.ErrorCode > 0) {
            $scope.formData = null;
            $scope.errorMessage = response.ErrorMessage;
            $scope.errorCode = response.ErrorCode;
            console.log($scope.errorCode);

        }
        //no more questions left on quiz        
        else {
            $scope.formData = {};
            $scope.errorMessage = response.ErrorMessage;
            $scope.errorCode = response.ErrorCode;
            console.log($scope.errorCode);
        }
    };
    //when we receive any error
    var errorSubmitResponse = function (response) {
        $scope.errorMessage = response.ErrorMessages;
        $scope.formData = {};
        $scope.errorCode = response.ErrorCode;
        console.log($scope.errorCode);
    };

});


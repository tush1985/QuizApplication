(function () {

    //define the service
      var quizService = function ($http) {
          //Method for getting a question
        var getQuestion = function (id) {

            return $http({
                url: 'http://localhost:50175/api/quiz',
                method: 'GET',                
                contentType: 'application/json',
                params: { id: id }
            }) //return question and its multiple choice           
           .then(function (response) {
               return response.data;
           });
        };
        //Method for submit the user answer
        var submitAnswer = function (data) {
            return $http({
                url: 'http://localhost:50175/api/quiz',                
                method: 'POST',                                 
               data:data
               
            }) //return message           
           .then(function (response) {
               return response.data;
           });
        };
        return {
            //define the service name
            getQuestion: getQuestion,
            submitAnswer:submitAnswer
        };
        
      };
      //inject the service to module
    var app = angular.module("formApp");
    app.factory("quizService", quizService);

}());
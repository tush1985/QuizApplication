describe('quizService Test', function (){
  var quizService;  
  // excuted before each "it" is run.
  beforeEach(function (){    
    /* load the module.*/
    module('formApp');    
    // inject your service for TESTING.
    
    inject(function(_quizService_) {
      quizService = _quizService_;
    });
  });
     
  // CHECK to see if it has the expected function
  it('should have an GetQuestion function', function () { 
    expect(angular.isFunction(quizService.getQuestion)).toBe(true);
  });

  // CHECK to see if it has the expected function
  it('should have an submitAnswer function', function () { 
    expect(angular.isFunction(quizService.submitAnswer)).toBe(true);
  });
  
  // check to see if it does what it's supposed to do. 
});



/*describe('quizService', function () {
    beforeEach(module('formApp'));
    it('should return 3', inject(function (quizService) {
        expect(quizService.getQuestion("1").length).toBe("3");
    }));
});*/



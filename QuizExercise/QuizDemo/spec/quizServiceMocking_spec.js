
describe('quizService Mocking', function (){
  var quizService,
      quizMock;
      
  beforeEach(function (){
    // load our module
    // BUT also provide a mock quiz!
    module('formApp', function($provide) {
      
      // spy to a property, or simply spying on an existing property.
      quizMock = jasmine.createSpyObj('quizService', ['getQuestion']);
      
      // provide the mock!
      $provide.value('quizService', quizMock);
    });
    
    // now we inject the service we're testing.
    inject(function(_quizService_) {
      quizService = _quizService_;
    });
  });
  

  it('should call getQuestion on quizService TEST passing through parameters.', function (){
    // make the call.
    quizService.getQuestion('1');
    
    // CHECK our spy to see if bar was called properly.
    expect(quizMock.getQuestion).toHaveBeenCalledWith('1');
  });
});



describe('$httpBackend', function () {  

    it("expects POST http calls and returns mock data", inject(function ($http, $httpBackend) {
        var url = 'http://localhost:50175/api/quiz',
            data = 'mock data',
            header = {'LWSSO': 'token value'},
            successCallback = jasmine.createSpy('success'),
            errorCallback = jasmine.createSpy('error');
        
        // Create expectation
        // headers is a unction that receives http header object and returns true
        // if the headers match the current expectation.
        $httpBackend.expectPOST(url, data, function(headers) {
            // check if the header was send, if it wasn't the expectation won't
            // match the request and the test will fail
            return headers['LWSSO'] === 'token value';
          }).respond(500, 'Oh no!');
                 
        // Call http service
        $http({
            method: 'POST',
            url: url,
            data: data,
            headers: header
        }).success(successCallback).error(errorCallback);
                
        // flush response
        $httpBackend.flush();
        
        // Verify expectations
        expect(successCallback).not.toHaveBeenCalled();
        expect(errorCallback.calls.mostRecent().args).toContain('Oh no!');
        expect(errorCallback.calls.argsFor(0)).toContain(500);
    }));

});

// --- Runner -------------------------
(function () {
    var jasmineEnv = jasmine.getEnv();
    jasmineEnv.updateInterval = 1000;

    var htmlReporter = new jasmine.HtmlReporter();

    jasmineEnv.addReporter(htmlReporter);

    jasmineEnv.specFilter = function (spec) {
        return htmlReporter.specFilter(spec);
    };

    var currentWindowOnload = window.onload;

    window.onload = function () {
        if (currentWindowOnload) {
            currentWindowOnload();
        }
        execJasmine();
    };

    function execJasmine() {
        jasmineEnv.execute();
    }

})();

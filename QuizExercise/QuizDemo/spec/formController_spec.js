/* global describe, it, express  */
describe('formController', function () {
    var $scope, $controller, formController;

    beforeEach(module('formApp'));
    beforeEach(inject(function ($injector) {
        $scope = $injector.get('$rootScope');
        $controller = $injector.get('$controller');
        formController = $controller('formController', { $scope: $scope });

         }));

        describe('calling change To Admin', function () {

            beforeEach(function () {
                $scope.changeToAdmin();
            });

            it('logged in as Admin User', function () {
                expect($scope.userId).toEqual("1");
            });

            it('logged in as Client User', function () {
                $scope.changeToClient();
                expect($scope.userId).toEqual("2");
            });
        });  

})







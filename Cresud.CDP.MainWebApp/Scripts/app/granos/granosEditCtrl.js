angular.module('cresud.cdp.granos.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'granosService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, granosService, baseNavigationService, editBootstraperService) {
               $scope.onInitEnd = function () {
                   $scope.operation = $routeParams.id ? 'Nuevo grano' : 'Edición de grano';                   
               };

               editBootstraperService.init($scope, $routeParams,  {
                   service: granosService,
                   navigation: baseNavigationService
               });
              
               $scope.isValid = function() {
                   $scope.result = { hasErrors: false, messages: [] };
                  
                   if (!$scope.entity.nombre) {
                       $scope.result.messages.push($scope.entity.esChoferTransportista ? 'Ingrese la descripción' : 'Ingrese el nombre');
                   }                            

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };           
           }]);
angular.module('cresud.cdp.reservas.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'reservasService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, reservasService, baseNavigationService, editBootstraperService) {
               $scope.onInitEnd = function () {
                   $scope.operation = !$routeParams.id ? 'Nuevo chofer' : 'Edición de chofer';                
               };

               editBootstraperService.init($scope, $routeParams,  {
                   service: reservasService,
                   navigation: baseNavigationService
               });
              
               $scope.isValid = function() {
                   $scope.result = { hasErrors: false, messages: [] };
                   
                   if (!$scope.entity.esChoferTransportista ) {                   
                        $scope.result.messages.push('"Formato de patente de acoplado inválido. Formato corrercto ej: AAA111 o AA111AA');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };             
           }]);
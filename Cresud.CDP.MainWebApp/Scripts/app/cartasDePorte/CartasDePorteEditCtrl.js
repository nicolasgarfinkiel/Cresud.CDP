angular.module('cresud.cdp.cartasDePorte.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'cartasDePorteService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, cartasDePorteService, baseNavigationService, editBootstraperService) {
               $scope.onInitEnd = function () {
                   $scope.operation = 'Alta de Rango de Cartas de Porte solicitadas a la AFIP';
               };

               editBootstraperService.init($scope, $routeParams,  {
                   service: cartasDePorteService,
                   navigation: baseNavigationService
               });
              
               $scope.isValid = function() {
                   $scope.result = { hasErrors: false, messages: [] };
                  
                   if (!$scope.entity.esChoferTransportista && $scope.esGrupoCresud && !$scope.entity.camion) {
                       $scope.result.messages.push('Ingrese la patente del camión');
                   }                  

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };
          
           }]);
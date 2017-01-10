angular.module('cresud.cdp.gruposEmpresa.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'gruposEmpresaService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, gruposEmpresaService, baseNavigationService, editBootstraperService) {

               $scope.onInitEnd = function () {
                   $scope.operation = !$routeParams.id ? 'Nuevo  grupo empresa' : 'Edición de grupo empresa';                   
               };

               editBootstraperService.init($scope, $routeParams,  {
                   service: gruposEmpresaService,
                   navigation: baseNavigationService
               });

               $scope.isValid = function () {
                   $scope.result = { hasErrors: false, messages: [] };

                   if (!$scope.entity.descripcion) {
                       $scope.result.messages.push('Ingrese la descripción');
                   }

                   if (!$scope.entity.paisId) {
                       $scope.result.messages.push('Seleccione un país');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };                          
           }]);
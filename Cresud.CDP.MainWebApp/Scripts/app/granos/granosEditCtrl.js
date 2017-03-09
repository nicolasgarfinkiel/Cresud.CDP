angular.module('cresud.cdp.granos.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'granosService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, granosService, baseNavigationService, editBootstraperService) {
               $scope.onInitEnd = function () {
                   $scope.operation = !$routeParams.id ? 'Nuevo grano' : 'Edición de grano';   
                   $scope.esGrupoCresud = $scope.usuario.currentEmpresa.grupoEmpresa.id == 1;
               };

               editBootstraperService.init($scope, $routeParams,  {
                   service: granosService,
                   navigation: baseNavigationService
               });
              
               $scope.isValid = function() {
                   $scope.result = { hasErrors: false, messages: [] };

                   if (!$scope.entity.descripcion) {
                       $scope.result.messages.push('Ingrese la descripción');
                   }

                   if (!$scope.entity.idMaterialSap) {
                       $scope.result.messages.push('Ingrese el material SAP');
                   }

                   if ($scope.esGrupoCresud  && !$scope.entity.especieAfipId) {
                       $scope.result.messages.push('Seleccione una especie');
                   }

                   if (!$scope.entity.cosechaAfipId) {
                       $scope.result.messages.push('Seleccione la cosecha');
                   }

                   if ($scope.esGrupoCresud &&  !$scope.entity.tipoGranoAfipId) {
                       $scope.result.messages.push('Seleccione un tipo');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };           
           }]);
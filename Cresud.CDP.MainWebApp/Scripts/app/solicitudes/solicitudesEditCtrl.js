angular.module('cresud.cdp.solicitudes.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'solicitudesService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, solicitudesService, baseNavigationService, editBootstraperService) {
               //#region Base
               $scope.loading = true;

               $scope.onInitEnd = function () {
                   //$scope.operation = !$routeParams.id ? 'Nuevo chofer' : 'Edición de chofer';
                   $scope.esParaguay = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toUpperCase() == 'PARAGUAY';
                   $scope.esArgentina = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toUpperCase() == 'ARGENTINA';
                   $scope.esGrupoCresud = $scope.usuario.currentEmpresa.grupoEmpresa.id == 1;
                   $scope.loading = false;
               };

               editBootstraperService.init($scope, $routeParams,  {
                   service: solicitudesService,
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
          
               //#endregion

               $scope.resultAfip = { message: null, hasErros: false };

               $scope.tipoDeCartaControls = function() {
                   $scope.manual = $scope.entity.tipoDeCartaId == 2 ||
                                   $scope.entity.tipoDeCartaId == 7 ||
                                   $scope.entity.tipoDeCartaId == 4;
               };

               //#region Watches

               $scope.$watch('entity.tipoDeCartaId', function(newValue, oldValue) {
                   if ($scope.loading) return;
                   $scope.tipoDeCartaControls();
               });

               //#endregion
           }]);
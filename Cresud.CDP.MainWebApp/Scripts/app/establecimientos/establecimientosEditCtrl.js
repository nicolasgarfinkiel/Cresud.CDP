angular.module('cresud.cdp.establecimientos.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           '$timeout',
           'establecimientosService',
           'generalService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, $timeout,  establecimientosService, generalService, baseNavigationService, editBootstraperService) {
               $scope.loading = true;

               //#region Base

               $scope.onInitEnd = function () {
                   $scope.operation = !$routeParams.id ? 'Nuevo establecimiento' : 'Edición de establecimiento';
                   $scope.filterClientes.empresaId = $scope.usuario.currentEmpresa.id;
                   $scope.esArgentina = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toUpperCase() == 'ARGENTINA';

                   $timeout(function () {
                       $scope.loading = false;
                   }, 300);
               };
               
               editBootstraperService.init($scope, $routeParams,  {
                   service: establecimientosService,
                   navigation: baseNavigationService
               });
              
               $scope.isValid = function() {
                   $scope.result = { hasErrors: false, messages: [] };

                   if (!$scope.entity.descripcion) {
                       $scope.result.messages.push('Ingrese la descripción');
                   }

                   if (!$scope.entity.direccion) {
                       $scope.result.messages.push('Ingrese la dirección');
                   }

                   if ($scope.entity.provinciaId == null || $scope.entity.provinciaId === '' || $scope.entity.provinciaId == 'undefined' ) {
                       $scope.result.messages.push('Seleccione una provincia');
                   }

                   if (!$scope.entity.localidadId) {
                       $scope.result.messages.push('Seleccione una localidad');
                   }

                   if (!$scope.entity.interlocutorDestinatarioId) {
                       $scope.result.messages.push('Seleccione un locutor destinatario');
                   }

                   if (!$scope.entity.idExpedicion) {
                       $scope.result.messages.push('Ingrese el id expedición');
                   }
                  
                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };

               //#endregion

               $scope.localidadesFiltered = [];
           
               $scope.$watch('entity.provinciaId', function (newValue, oldValue) {
                   $scope.localidadesFiltered = [];

                   if (isNaN(newValue)) return;

                   for (var i = 0; i < $scope.data.localidades.length; i++) {
                       if ($scope.data.localidades[i].provinciaId == newValue) {
                           $scope.localidadesFiltered.push($scope.data.localidades[i]);
                       }
                   }
               });

               //#region Select UI

               $scope.selectList = [];
               $scope.currentPage = 0;
               $scope.pageCount = 0;               
               $scope.filterClientes = { pageSize: 20 };

               $scope.getSelectSource = function ($select, $event) {
                   if ($scope.loading) return;

                   if (!$event) {
                       $scope.currentPage = 1;
                       $scope.pageCount = 0;
                       $scope.selectList = [];
                   } else {
                       $event.stopPropagation();
                       $event.preventDefault();
                       $scope.currentPage++;
                   }

                   $scope.filterClientes.currentPage = $scope.currentPage;
                   $scope.filterClientes.multiColumnSearchText = $select.search;

                   generalService.getClientesByFilter($scope.filterClientes).then(function (response) {
                       $scope.selectList = $scope.selectList.concat(response.data.data);
                       $scope.pageCount = Math.ceil(response.data.count / 20);
                   }, function () { throw 'Error on getByFilter'; });
               };

               $scope.$watch('entity.interlocutorDestinatario', function (newValue, oldValue) {
                   if ($scope.loading) return;

                   $scope.entity.interlocutorDestinatarioId = newValue.id;
               });

               //#endregion                
           }]);
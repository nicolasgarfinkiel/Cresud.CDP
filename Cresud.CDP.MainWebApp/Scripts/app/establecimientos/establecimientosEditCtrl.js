angular.module('cresud.cdp.establecimientos.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'establecimientosService',
           'generalService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, establecimientosService, generalService, baseNavigationService, editBootstraperService) {
               //#region Base

               $scope.onInitEnd = function () {
                   $scope.operation = !$routeParams.id ? 'Nuevo establecimiento' : 'Edición de establecimiento';
                   $scope.filterClientes.empresaId = $scope.usuario.currentEmpresa.id;
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

                   if (!$scope.entity.provinciaId) {
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

                   if (!newValue) return;

                   for (var i = 0; i < $scope.data.localidades.length; i++) {
                       if ($scope.data.localidades[i].provinciaId == newValue) {
                           $scope.localidadesFiltered.push($scope.data.localidades[i]);
                       }
                   }
               });

               //#region Clientes

               $scope.clientes = [];
               $scope.clientesCount = 0;
               $scope.filterClientes = {};

               $scope.setCliente = function(cliente) {
                   $scope.entity.interlocutorDestinatarioId = cliente.id;
                   $('#clientesModal').modal('hide');
               };

               $scope.gridClientes = {
                   data: 'clientes',
                   columnDefs: [
                                { field: 'razonSocial', displayName: 'Razón Social' },
                                { field: 'cuit', displayName: 'Cuit' },
                                { field: 'id', displayName: 'Id Cliente', width: 100 },
                                { field: 'cuit', displayName: 'Seleccionar', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="setCliente(row.entity)"><i class="fa fa-thumbs-o-up"></i></a></div>' }
                                
                   ],
                   showFooter: true,
                   enablePaging: true,
                   multiSelect: false,                  
                   totalServerItems: 'clientesCount',
                   pagingOptions: {
                       pageSizes: [10],
                       pageSize: 10,
                       currentPage: 1
                   },
                   filterOptions: { useExternalFilter: true }
               };

               $scope.findClientes = function () {                   
                   $scope.clientes = [];

                   if (!$scope.filterClientes.empresaId) return;

                   $scope.filterClientes.currentPage = $scope.gridClientes.pagingOptions.currentPage;
                   $scope.filterClientes.pageSize = $scope.gridClientes.pagingOptions.pageSize;

                   generalService.getClientesByFilter($scope.filterClientes).then(function (response) {
                       $scope.clientes = response.data.data;
                       $scope.clientesCount = response.data.count;                     
                   }, function () { throw 'Error on getClientesByFilter'; });
               };

               $scope.$watch('gridClientes.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.findClientes();
               }, true);

               $scope.$watch('filterClientes.multiColumnSearchText', function () {
                   $scope.gridClientes.pagingOptions.currentPage = 1;
                   $scope.findClientes();
               });

               //#endregion
           }]);
angular.module('cresud.cdp.empresas.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'empresasService',
           'generalService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, empresasService, generalService, baseNavigationService, editBootstraperService) {
               //#region Base
               $scope.loading = true;

               $scope.onInitEnd = function () {
                   $scope.operation = !$routeParams.id ? 'Nueva empresa' : 'Edición de empresa';
                   $scope.filterClientes.empresaId = $scope.usuario.currentEmpresa.id;
                   $scope.loading = false;
               };
               
               editBootstraperService.init($scope, $routeParams,  {
                   service: empresasService,
                   navigation: baseNavigationService
               });
              
               $scope.isValid = function() {
                   $scope.result = { hasErrors: false, messages: [] };

                   if (!$scope.entity.idSapOrganizacionDeVenta) {
                       $scope.result.messages.push('Seleccione una organización');
                   }

                   if (!$scope.entity.idCliente) {
                       $scope.result.messages.push('Seleccione un cliente');
                   }
                   
                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };

               //#endregion                                     

               $scope.getGrupoEmpresaById = function(id) {
                   var result = null;

                   for (var i = 0; i < $scope.data.grupoEmpresaList.length; i++) {
                       if ($scope.data.grupoEmpresaList[i].id == id) {
                           result = $scope.data.grupoEmpresaList[i];
                           break;
                       }
                   }

                   return result;
               };

               $scope.$watch('entity.grupoEmpresaId', function (newVal, oldVal) {
                   if (!$scope.entity) return;

                   $scope.entity.grupoEmpresa = null;

                   if (!newVal) return;

                   $scope.entity.grupoEmpresa = $scope.getGrupoEmpresaById(newVal);

               }, true);

               $scope.$watch('entity.idSapOrganizacionDeVenta', function (newValue) {
                   $scope.filterClientes.idSapOrganizacionDeVenta = idSapOrganizacionDeVenta;

                   if (!$scope.loading) {
                       $scope.entity.idCliente = null;
                   }
               });

               //#region Clientes

               $scope.clientes = [];
               $scope.clientesCount = 0;
               $scope.filterClientes = {};

               $scope.setCliente = function(cliente) {
                   $scope.entity.idCliente = cliente.id;
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

                   generalService.getClientesConProveedorByFilter($scope.filterClientes).then(function (response) {
                       $scope.clientes = response.data.data;
                       $scope.clientesCount = response.data.count;                     
                   }, function () { throw 'Error on getClientesConProveedorByFilter'; });
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
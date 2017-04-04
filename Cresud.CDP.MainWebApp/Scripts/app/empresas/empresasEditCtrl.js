angular.module('cresud.cdp.empresas.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'empresasService',
           'generalService',
           'baseNavigationService',
           'editBootstraperService',
           '$timeout',
           function ($scope, $routeParams, empresasService, generalService, baseNavigationService, editBootstraperService, $timeout) {
               //#region Base
               $scope.loading = true;

               $scope.onInitEnd = function () {
                   $scope.operation = !$routeParams.id ? 'Nueva empresa' : 'Edición de empresa';
                   $scope.filterClientes.empresaId = $scope.usuario.currentEmpresa.id;

                   if ($scope.entity.idCliente) {
                       $scope.filterClientes.idSapOrganizacionDeVenta = $scope.entity.idSapOrganizacionDeVenta;
                       $scope.entity.cliente = { id: $scope.entity.idCliente, razonSocial: $scope.entity.descripcion};                                              
                   }

                   $timeout(function() {
                       $scope.loading = false;
                   }, 100);               
               };

               editBootstraperService.init($scope, $routeParams, {
                   service: empresasService,
                   navigation: baseNavigationService
               });

               $scope.isValid = function () {
                   $scope.result = { hasErrors: false, messages: [] };

                   if (!$scope.entity.idSapOrganizacionDeVenta) {
                       $scope.result.messages.push('Seleccione una organización');
                   }

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

               $scope.moneda = {
                   1: 'ARS',
                   2: 'BOB',
                   3: 'PYG'
               };

               $scope.getGrupoEmpresaById = function (id) {
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
                   $scope.entity.grupoEmpresaId = newVal;
                   $scope.entity.idSapMoneda = $scope.moneda[newVal];

               }, true);

               $scope.$watch('entity.idSapOrganizacionDeVenta', function (newValue, oldValue) {
                   if (!$scope.loading) {
                       $scope.filterClientes.idSapOrganizacionDeVenta = newValue;
                       $scope.entity.idCliente = null;
                       $scope.clientes = [];
                   }
               });

               //#region Select UI

               $scope.selectList = [];
               $scope.currentPage = 0;
               $scope.pageCount = 0;
               $scope.filterClientes = {pageSize: 20};

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

                   generalService.getClientesConProveedorByFilter($scope.filterClientes).then(function (response) {
                       $scope.selectList = $scope.selectList.concat(response.data.data);
                       $scope.pageCount = Math.ceil(response.data.count / 20);
                   }, function () { throw 'Error on getByFilter'; });
               };

               $scope.$watch('entity.cliente', function (newValue, oldValue) {
                   if ($scope.loading) return;

                   $scope.entity.idCliente = newValue.id;
               });            

               //#endregion                          
           
           }]);
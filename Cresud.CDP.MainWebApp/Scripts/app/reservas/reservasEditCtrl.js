angular.module('cresud.cdp.reservas.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'reservasService',
           'establecimientosService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, reservasService, establecimientosService, baseNavigationService, editBootstraperService) {
               //#region base

               $scope.onInitEnd = function () {
                   $scope.operation = 'Nueva reserva';
                   $scope.filterEstablecimientos.empresaId = $scope.usuario.currentEmpresa.id;
               };

               editBootstraperService.init($scope, $routeParams,  {
                   service: reservasService,
                   navigation: baseNavigationService
               });
              
               $scope.isValid = function() {
                   $scope.result = { hasErrors: false, messages: [] };
                   
                   if (!$scope.entity.tipoDeCartaId) {
                        $scope.result.messages.push('Seleccione el tipo de carta');
                   }

                   if (!$scope.entity.establecimientoProcedenciaId) {
                       $scope.result.messages.push('Seleccione el establecimiento');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };

               //#endregion

               //#region Establecimientos

               $scope.establecimientos = [];
               $scope.establecimientosCount = 0;
               $scope.filterEstablecimientos = { origen: true };

               $scope.setEstablecimiento = function (establecimiento) {
                   $scope.entity.establecimientoProcedenciaId = establecimiento.id;
                   $scope.entity.establecimientoProcedenciaDescripcion = establecimiento.descripcion;
                   $('#establecimientosModal').modal('hide');
               };

               $scope.gridEstablecimientos = {
                   data: 'establecimientos',
                   columnDefs: [
                                { field: 'descripcion', displayName: 'Descripción' },
                                { field: 'direccion', displayName: 'Dirección' },
                                { field: 'cuit', displayName: 'Seleccionar', width: 120, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="setEstablecimiento(row.entity)"><i class="fa fa-thumbs-o-up"></i></a></div>' }

                   ],
                   showFooter: true,
                   enablePaging: true,
                   multiSelect: false,
                   totalServerItems: 'establecimientosCount',
                   pagingOptions: {
                       pageSizes: [10],
                       pageSize: 10,
                       currentPage: 1
                   },
                   filterOptions: { useExternalFilter: true }
               };

               $scope.findEstablecimientos = function () {
                   $scope.establecimientos = [];

                   if (!$scope.filterEstablecimientos.empresaId) return;

                   $scope.filterEstablecimientos.currentPage = $scope.gridEstablecimientos.pagingOptions.currentPage;
                   $scope.filterEstablecimientos.pageSize = $scope.gridEstablecimientos.pagingOptions.pageSize;

                   establecimientosService.getByFilter($scope.filterEstablecimientos).then(function (response) {
                       $scope.establecimientos = response.data.data;
                       $scope.establecimientosCount = response.data.count;
                   }, function () { throw 'Error on getByFilter'; });
               };

               $scope.$watch('gridEstablecimientos.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.findEstablecimientos();
               }, true);

               $scope.$watch('filterEstablecimientos.multiColumnSearchText', function () {
                   $scope.gridEstablecimientos.pagingOptions.currentPage = 1;
                   $scope.findEstablecimientos();
               });

               //#endregion


           }]);
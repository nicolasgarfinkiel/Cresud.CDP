angular.module('cresud.cdp.bandejaDeSalida.ctrl.solicitadas', [])
       .controller('solicitadasCtrl', [
           '$scope',
           'bandejaDeSalidaService',
           function ($scope, bandejaDeSalidaService) {
               $scope.filter = {};
               $scope.columns = [];
               $scope.columnsArg = [
                    { field: 'id', displayName: 'Id', width: 50 },
                    { field: 'numeroCartaDePorte', displayName: 'Nro.Carta Porte' },
                    { field: 'ctg', displayName: 'Ctg', width: 80 },
                    { field: 'tipoCarta', displayName: 'Tipo' },
                    { field: 'titularCDP', displayName: 'Titular' },
                    { field: 'estProcedencia', displayName: 'Est.Procedencia' },
                    { field: 'asd', displayName: 'Afip', cellTemplate: '<div>{{getEstadoAfipDescripcion(row.entity.estadoEnAFIP)}}</div>' },
                    { field: 'asd', displayName: 'SAP', cellTemplate: '<div>{{getEstadoSapDescripcion(row.entity.estadoEnSAP)}}</div>' },
                    { field: 'observacionAfip', displayName: 'Observaciones AFIP' },
                    { field: 'fechaDeEmision', displayName: 'Fecha' },
                    { field: 'createdBy', displayName: 'Usuario Creación' },
                    { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a></div>' }
               ];

               $scope.columnsOtros = [
                   { field: 'id', displayName: 'Id', width: 50 },
                   { field: 'numeroCartaDePorte', displayName: 'Nro.Carta Porte' },
                   { field: 'tipoCarta', displayName: 'Tipo' },
                   { field: 'titularCDP', displayName: 'Titular' },
                   { field: 'estProcedencia', displayName: 'Est.Procedencia' },
                   { field: 'estadoEnSAP', displayName: 'SAP' },
                   { field: 'fechaDeEmision', displayName: 'Fecha' },
                   { field: 'createdBy', displayName: 'Usuario Creación' },
                   { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a></div>' }
               ];

               bandejaDeSalidaService.getDataListSolicitadas().then(function (response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;
                   $scope.esArgentina = $scope.data.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toLowerCase() == 'argentina';

                   $scope.columns = $scope.esArgentina ? $scope.columnsArg : $scope.columnsOtros;
               });

               $scope.getEstadoAfipDescripcion = function(id) {
                   var result = null;

                   for (var i = 0; i < $scope.data.estadosAfip.length; i++) {
                       if ($scope.data.estadosAfip[i].key == id) {
                           result = $scope.data.estadosAfip[i].value;
                           break;
                       }
                   }

                   return result;
               };

               $scope.getEstadoSapDescripcion = function (id) {
                   var result = null;

                   for (var i = 0; i < $scope.data.estadosSap.length; i++) {
                       if ($scope.data.estadosSap[i].key == id) {
                           result = $scope.data.estadosSap[i].value;
                           break;
                       }
                   }

                   return result;
               };

               $scope.solicitudes = [];
               $scope.dataCount = 0;

               $scope.gridOptions = {
                   data: 'solicitudes',
                   columnDefs: 'columns',
                   showFooter: true,
                   enablePaging: true,
                   multiSelect: false,
                   totalServerItems: 'dataCount',
                   pagingOptions: {
                       pageSizes: [10],
                       pageSize: 10,
                       currentPage: 1
                   },
                   filterOptions: { useExternalFilter: true }
               };

               $scope.find = function () {
                   $scope.solicitudes = [];

                   if (!$scope.filter.empresaId) return;

                   $scope.filter.currentPage = $scope.gridOptions.pagingOptions.currentPage;
                   $scope.filter.pageSize = $scope.gridOptions.pagingOptions.pageSize;

                   bandejaDeSalidaService.getSolicitadasByFilter($scope.filter).then(function (response) {
                       $scope.solicitudes = response.data.data;
                       $scope.dataCount = response.data.count;
                   }, function () { throw 'Error on getSolicitadasByFilter'; });
               };

               $scope.$watch('gridOptions.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.find();
               }, true);              
           }]);
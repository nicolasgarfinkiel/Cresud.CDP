angular.module('cresud.cdp.bandejaDeSalida.ctrl.confirmacionArribo', [])
       .controller('confirmacionArriboCtrl', [
           '$scope',
           'bandejaDeSalidaService',
           function ($scope, bandejaDeSalidaService) {
               $scope.filter = {};

               //#region Init

               bandejaDeSalidaService.getDataListConfirmacionArribo().then(function (response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;                   
               });

               //#endregion             

               $scope.solicitudes = [];
               $scope.dataCount = 0;
           
               $scope.gridOptions = {
                   data: 'solicitudes',
                   columnDefs: [
                        { field: 'id', displayName: 'Id', width: 60 },
                        { field: 'numeroCartaDePort', displayName: 'Nro.Carta Porte', width: 140 },
                        { field: 'tipoCarta', displayName: 'Tipo Carta', width: 140 },
                        { field: 'fecha', displayName: 'Fecha', width: 80 },
                        { field: 'fecha', displayName: 'Establecimiento Procedencia' },
                        { field: 'fecha', displayName: 'Establecimiento Destino' },
                        { field: 'fecha', displayName: 'Peso', width: 60 },
                        { field: 'fecha', displayName: 'Usuario', width: 140 },
                        { field: 'fecha', displayName: 'Ver', width: 60 }
                   ],
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

                   bandejaDeSalidaService.getConfirmacionesArriboByFilter($scope.filter).then(function (response) {
                       $scope.solicitudes = response.data.data;
                       $scope.dataCount = response.data.count;
                       $scope.result = response.data.result;
                   }, function () { throw 'Error on getConfirmacionesArriboByFilter'; });
               };

               $scope.$watch('gridOptions.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.find();
               }, true);

               $scope.$watch('filter.multiColumnSearchText', function () {
                   $scope.gridOptions.pagingOptions.currentPage = 1;
                   $scope.find();
               });

           }]);
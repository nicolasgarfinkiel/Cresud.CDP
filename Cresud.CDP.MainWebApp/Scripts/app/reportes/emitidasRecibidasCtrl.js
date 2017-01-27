angular.module('cresud.cdp.reportes.ctrl.emitidasRecibidas', [])
       .controller('emitidasRecibidasCtrl', [
           '$scope',
           'reportesService',
           'reportesNavigationService',       
           function ($scope, reportesService, reportesNavigationService) {
               $scope.filter = {};
               $scope.columns = [];

               reportesService.getDataListCdp().then(function(response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;
                   $scope.reportName = location.hash.replace("#/", '');
                   $scope.filter.emitidas = $scope.reportName == 'emitidas';
               });              

               $scope.$watch('filter.fechaDesde', function (newValue) {
                   $scope.fechaDesde = moment($scope.filter.fechaDesde).add(1, 'days').format('YYYY/MM/DD');
               });

               $scope.$watch('filter.fechaHasta', function (newValue) {
                   $scope.fechaHasta = moment($scope.filter.fechaHasta).add(1, 'days').format('YYYY/MM/DD');
               });
               
               $scope.data = [];
               $scope.dataCount = 0;
              
               $scope.gridOptions = {
                   data: 'proveedores',
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
                   $scope.data = [];

                   if (!$scope.filter.empresaId) return;

                   $scope.filter.currentPage = $scope.gridOptions.pagingOptions.currentPage;
                   $scope.filter.pageSize = $scope.gridOptions.pagingOptions.pageSize;

                   reportesService.getCdpsByFilter($scope.filter).then(function (response) {
                       $scope.data = response.data.data;
                       $scope.dataCount = response.data.count;
                   }, function () { throw 'Error on getCdpsByFilter'; });
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
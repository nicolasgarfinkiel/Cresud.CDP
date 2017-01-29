angular.module('cresud.cdp.reportes.ctrl.emitidasRecibidas', [])
       .controller('emitidasRecibidasCtrl', [
           '$scope',
           'reportesService',
           'reportesNavigationService',       
           function ($scope, reportesService, reportesNavigationService) {
               $scope.filter = {};
               $scope.columns = [];
               $scope.columnsEmitidas = [                    
                    { field: 'tipoCarta', displayName: 'Tipo' },
                    { field: 'numeroCartaDePorte', displayName: 'Nro.Carta Porte' },
                    { field: 'cee', displayName: 'Cee' },
                    { field: 'ctg', displayName: 'Ctg', width: 80 },
                    { field: 'fechaDeCarga', displayName: 'Fecha Carga' },
                    { field: 'choferCuit', displayName: 'Chofer/Conductor' },
                    { field: 'cosechaDescripcion', displayName: 'Cosecha', width: 80 },
                    { field: 'especieCodigo', displayName: 'Cod.Especie' },
                    { field: 'idTipoGrano', displayName: 'Tipo Grano', width: 80 },
                    { field: 'pesoNeto', displayName: 'Peso Neto', width: 80 },
                    { field: 'kmRecorridos', displayName: 'Km Recorrer' },
                    { field: 'patenteCamion', displayName: 'Patente camión' },
                    { field: 'patenteAcoplado', displayName: 'Patente acoplado' }                    
               ];
                                                                                                                                                                                                                                                                                                    
               $scope.columnsRecibidas = [
                   { field: 'tipoCartaId', displayName: 'Tipo' },
                    { field: 'numeroCartaDePorte', displayName: 'Nro.Carta Porte' },
                    { field: 'cee', displayName: 'Cee' },
                    { field: 'ctg', displayName: 'Ctg' },
                    { field: 'fechaDeCarga', displayName: 'Fecha Carga' },
                    { field: 'choferCuit', displayName: 'Chofer/Conductor' },
                    { field: 'grano.cosechaAfipDescripcion', displayName: 'Cosecha' },
                    { field: 'grano.especieAfipId', displayName: 'Cod.Especie' },
                    { field: 'grano.tipoGranoAfipId', displayName: 'Tipo Grano' },
                    { field: 'pesoNeto', displayName: 'Peso Neto' },
                    { field: 'kmRecorridos', displayName: 'Km Recorrer' },
                    { field: 'patenteCamion', displayName: 'Patente camión' },
                    { field: 'patenteAcoplado', displayName: 'Patente acoplado' },
                    { field: 'fchaDescarga', displayName: 'Fecha Descarga' }                      
               ];

               reportesService.getDataListCdp().then(function(response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;
                   $scope.reportName = location.hash.replace("#/", '');
                   $scope.filter.emitidas = $scope.reportName == 'emitidas';
                   $scope.columns = $scope.filter.emitidas ? $scope.columnsEmitidas : $scope.columnsRecibidas;
                   $scope.findMethod = $scope.filter.emitidas ? reportesService.getCdpsEmitidasByFilter : reportesService.getCdpsRecibidasByFilter;
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
                   data: 'data',
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

                   $scope.findMethod($scope.filter).then(function (response) {
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
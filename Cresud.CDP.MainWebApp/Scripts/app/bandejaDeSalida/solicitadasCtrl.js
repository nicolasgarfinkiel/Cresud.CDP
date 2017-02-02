angular.module('cresud.cdp.bandejaDeSalida.ctrl.solicitadas', [])
       .controller('solicitadasCtrl', [
           '$scope',
           'bandejaDeSalidaService',           
           function ($scope, bandejaDeSalidaService) {
               $scope.filter = {};
               $scope.columns = [];
               $scope.columnsArg = [                    
                    { field: 'id', displayName: 'Id', width: 80 },
                    { field: 'numeroCartaDePorte', displayName: 'Nro.Carta Porte' },
                    { field: 'ctg', displayName: 'Ctg' },
                    { field: 'tipoCartaId', displayName: 'Tipo' },
                    { field: 'titularCDP', displayName: 'Titular' },
                    { field: 'estProcedencia', displayName: 'Est.Procedencia' },
                    { field: 'estadoEnAFIP', displayName: 'Afip' },
                    { field: 'estadoEnSAP', displayName: 'SAP' },
                    { field: 'observacionAfip', displayName: 'Observaciones AFIP' },
                    { field: 'fechaDeEmision', displayName: 'Fecha' },
                    { field: 'createdBy', displayName: 'Usuario Creación' },
                    { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a></div>' }
                               
               ];
                                                                                                         
               
               
               row.Cells.Add(AddTitleCell("Re AFIP", 5, "Acción de Reenvio de la solicitud a AFIP"));
               row.Cells.Add(AddTitleCell("Re SAP", 5, "Acción de Reenvio de la solicitud a SAP"));
               row.Cells.Add(AddTitleCell("Log Sap", 5, "Log de respuestas de SAP"));
               
                                                                                                                                                                                                                                                                                                    
               $scope.columnsOtros = [
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

               bandejaDeSalidaService.GetDataListSolicitadas().then(function (response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;
                   $scope.esArgentina = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toLowerCase() == 'argentina';

                   $scope.columns = $scope.esArgentina ? $scope.columnsArg : $scope.columnsOtros;
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

                   bandejaDeSalidaService.getSolicitadasByFilter($scope.filter).then(function (response) {
                       $scope.data = response.data.data;
                       $scope.dataCount = response.data.count;
                   }, function () { throw 'Error on getSolicitadasByFilter'; });
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
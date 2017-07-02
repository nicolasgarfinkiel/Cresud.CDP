angular.module('cresud.cdp.bandejaDeSalida.ctrl.confirmacionArribo', [])
       .controller('confirmacionArriboCtrl', [
           '$scope',
           'bandejaDeSalidaService',
           'solicitudesService',
           'establecimientosService',
           function ($scope, bandejaDeSalidaService, solicitudesService, establecimientosService) {
               $scope.filter = {};
               $scope.resultModal = { hasErrors: false, messages: [] };
               $scope.filterEstablecimientos = { destino: true, pageSize: 20, enabled: true };

               //#region Init

               bandejaDeSalidaService.getDataListConfirmacionArribo().then(function (response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;
                   $scope.filterEstablecimientos.empresaId = $scope.data.usuario.currentEmpresa.id;
                   $scope.find();
               });

               //#endregion             

               $scope.solicitudes = [];
               $scope.dataCount = 0;
           
               $scope.gridOptions = {
                   data: 'solicitudes',
                   columnDefs: [
                        { field: 'id', displayName: 'Id', width: 60 },
                        { field: 'numeroCartaDePorte', displayName: 'Nro.Carta Porte', width: 140 },
                        { field: 'tipoCarta', displayName: 'Tipo Carta', width: 140 },
                        { field: 'createDate', displayName: 'Fecha', width: 80 },
                        { field: 'estProcedencia', displayName: 'Establecimiento Procedencia' },
                        { field: 'estDestino', displayName: 'Establecimiento Destino' },
                        { field: 'pesoNeto', displayName: 'Peso', width: 60 },
                        { field: 'createdBy', displayName: 'Usuario', width: 140 },
                        { field: 'fecha', displayName: 'Confirmar', width: 100, cellTemplate: '<div class="ng-grid-icon-container"><a title="Confirmar Arribo" ng-click="confirmarArribo(row.entity)" href="javascript:void(0)"><img style="width: 15px;" src="content/images/icon_select.gif" /></a></div>' },
                        { field: 'fecha', displayName: 'Ver', width: 60, cellTemplate: '<div class="ng-grid-icon-container"><a title="Abrir Solicitud" href="/solicitudes#/edit/{{row.entity.id}}"><img style="width: 15px;" src="content/images/magnify.gif" /></a></div>' }
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

               $scope.confirmarArribo = function (entity) {                   
                   solicitudesService.getById(entity.id).then(function(response) {
                       $scope.resultModal = { hasErrors: false, messages: [] };
                       $scope.selectedEntity = response.data.data;
                       $('#confirmacionModal').modal('show');
                       $scope.selectedEntity.establecimientoProcedenciaAux = null;
                   });                   
               };

               $scope.setArribo = function () {
                   if (!$scope.isValidConfirmacion()) return;

                   var estId = $scope.selectedEntity.establecimientoProcedenciaAux ? $scope.selectedEntity.establecimientoProcedenciaAux.establecimientoAfip : null;

                   bandejaDeSalidaService.confirmarArribo($scope.selectedEntity.id, $scope.selectedEntity.consumoPropio, estId).then(function (response) {
                       $scope.resultModal = response.data;

                       if ($scope.resultModal.hasErrors) return;

                       $scope.find();
                       $('#confirmacionModal').modal('hide');
                   });                   
               };

               $scope.isValidConfirmacion = function() {
                   $scope.resultModal = { hasErrors: false, messages: [] };

                   if (typeof $scope.selectedEntity.consumoPropio == 'undefined') {
                       $scope.resultModal.messages.push('Indique si es consumo propio');
                   }

                   if ($scope.selectedEntity.consumoPropio == 'N' && !$scope.selectedEntity.establecimientoProcedenciaAux) {
                       $scope.resultModal.messages.push('Selecione el establecimiento');
                   }

                   if ($scope.selectedEntity.establecimientoProcedenciaAux && !$scope.selectedEntity.establecimientoProcedenciaAux.establecimientoAfip) {
                       $scope.resultModal.messages.push('El establecimiento seleccionado no posee establecimientoAfip asignado');
                   }               

                   $scope.resultModal.hasErrors = $scope.resultModal.messages.length;
                   return !$scope.resultModal.hasErrors;
               };

               $scope.$watch('gridOptions.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.find();
               }, true);

               $scope.$watch('filter.multiColumnSearchText', function () {
                   $scope.gridOptions.pagingOptions.currentPage = 1;
                   $scope.find();
               });

               $scope.$watch('selectedEntity.consumoPropio', function (newValue) {
                   if (!$scope.selectedEntity) return;
                   $scope.filterEstablecimientos.consumoPropio = newValue == 'S';
                   $scope.selectedEntity.establecimientoProcedenciaAux = null;
                 //  $scope.getSelectSource();
               });

               //#region Select UI

               $scope.selectList = [];
               $scope.currentPage = 0;
               $scope.pageCount = 0;                              

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

                   $scope.filterEstablecimientos.currentPage = $scope.currentPage;
                   $scope.filterEstablecimientos.multiColumnSearchText = $select ? $select.search : null;

                   establecimientosService.getByFilter($scope.filterEstablecimientos).then(function (response) {
                       $scope.selectList = $scope.selectList.concat(response.data.data);
                       $scope.pageCount = Math.ceil(response.data.count / 20);
                   }, function () { throw 'Error on getByFilter'; });
               };
            
               //#endregion                

           }]);
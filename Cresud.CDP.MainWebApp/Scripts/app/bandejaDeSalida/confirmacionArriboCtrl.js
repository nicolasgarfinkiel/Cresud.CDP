angular.module('cresud.cdp.bandejaDeSalida.ctrl.confirmacionArribo', [])
       .controller('confirmacionArriboCtrl', [
           '$scope',
           'bandejaDeSalidaService',
           'solicitudesService',
           function ($scope, bandejaDeSalidaService, solicitudesService) {
               $scope.filter = {};
               $scope.resultModal = {hasErrors: false, messages: []};

               //#region Init

               bandejaDeSalidaService.getDataListConfirmacionArribo().then(function (response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;
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
                   });                   
               };

               $scope.setArribo = function () {
                   if (!$scope.isValidConfirmacion()) return;

                   bandejaDeSalidaService.confirmarArribo($scope.selectedEntity.id, $scope.selectedEntity.consumoPropio).then(function (response) {
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

                   //if (!$scope.entity.establecimiento) {
                   //    $scope.result.messages.push('Selecione el establecimiento');
                   //}

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

           }]);
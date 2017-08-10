angular.module('cresud.cdp.bandejaDeSalida.ctrl.trasladosRechazados', [])
       .controller('trasladosRechazadosCtrl', [
           '$scope',
           'bandejaDeSalidaService',
           'solicitudesService',
           'establecimientosService',
           'generalService',
           '$sce',
           function ($scope, bandejaDeSalidaService, solicitudesService, establecimientosService, generalService, $sce) {
               $scope.filter = {};
               $scope.imageSrc = '../content/images/';
               $scope.resultModal = { hasErrors: false, messages: [] };

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
                        { field: 'numeroCartaDePorte', displayName: 'Nro.Carta Porte', width: 120 },
                        { field: 'ctg', displayName: 'CTG', width: 100 },
                        { field: 'createDate', displayName: 'Fecha', width: 80 },               
                        { field: 'estProcedencia', displayName: 'Establecimiento Procedencia' },
                        { field: 'estDestino', displayName: 'Establecimiento Destino' },
                        { field: 'destinatario', displayName: 'Destinatario' },
                        { field: 'pesoNeto', displayName: 'Peso', width: 90 },
                        { field: 'estadoEnAFIP', displayName: 'AFIP', width: 60, cellTemplate: '<div style="text-align: center; position: relative;top: 2px;" ng-bind-html="getAfipImg(row.entity)"></div>' },
                        { field: 'CDD', displayName: 'CDD', width: 50, cellTemplate: '<div class="ng-grid-icon-container"><a title="Cambio Destino y Destinatario" href="javascript:void(0)" ng-click="setCdd(row.entity)"><img style="width: 15px;" src="../content/images/pencil2.png" /></a></div>' },
                        { field: 'RAO', displayName: 'RaO', width: 50, cellTemplate: '<div class="ng-grid-icon-container"><a title="Regresar a Origen" href="javascript:void(0)" ng-click="setRegresoOrigen(row.entity)"><img style="width: 15px;" src="../content/images/pencil2.png" /></a></div>' },
                        { field: 'fecha', displayName: 'Ver', width: 50, cellTemplate: '<div class="ng-grid-icon-container"><a title="Abrir Solicitud" href="/solicitudes#/edit/{{row.entity.id}}"><img style="width: 15px;" src="../content/images/magnify.gif" /></a></div>' }
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

               $scope.getAfipImg = function (item) {                   
                   var icons = {
                       '0': { icon: 'circle_yellow.png', title: $scope.getEstadoAfipDescripcion(0) },
                       '1': { icon: 'circle_green.png', title: $scope.getEstadoAfipDescripcion(1) },
                       '2': { icon: 'circle_red.png', title: $scope.getEstadoAfipDescripcion(2) },
                       '3': { icon: 'icon_Delete.png', title: $scope.getEstadoAfipDescripcion(3) + "\n\rCodigo Anulación AFIP: " + item.codigoAnulacionAfip },
                       '4': { icon: 'circle_greenManual.png', title: $scope.getEstadoAfipDescripcion(4) },
                       '5': { icon: 'iconArribo.png', title: $scope.getEstadoAfipDescripcion(5) },
                       '6': { icon: 'iconArriboDefinitivo.png', title: $scope.getEstadoAfipDescripcion(6) },
                       '7': { icon: 'iconRechazado.png', title: $scope.getEstadoAfipDescripcion(7) },
                       '8': { icon: 'cambiodestino.png', title: $scope.getEstadoAfipDescripcion(8) },
                       '9': { icon: 'vueltaorigen.png', title: $scope.getEstadoAfipDescripcion(9) }
                   };

                   var icon = icons[item.estadoEnAFIP] || {};
                   var result = '<img style="width: 15px;" src="' + $scope.imageSrc + icon.icon + '" title="' + icon.title + '" />';

                   return $sce.trustAsHtml(icon.icon ? result : '');
               };

               $scope.getEstadoAfipDescripcion = function (id) {
                   var result = null;

                   for (var i = 0; i < $scope.data.estadosAfip.length; i++) {
                       if ($scope.data.estadosAfip[i].key == id) {
                           result = $scope.data.estadosAfip[i].value;
                           break;
                       }
                   }

                   return result;
               };

               $scope.find = function () {
                   $scope.solicitudes = [];

                   if (!$scope.filter.empresaId) return;

                   $scope.filter.currentPage = $scope.gridOptions.pagingOptions.currentPage;
                   $scope.filter.pageSize = $scope.gridOptions.pagingOptions.pageSize;

                   bandejaDeSalidaService.getTrasladosRechazados($scope.filter).then(function (response) {
                       $scope.solicitudes = response.data.data;
                       $scope.dataCount = response.data.count;
                       $scope.result = response.data.result;
                   }, function () { throw 'Error on getTrasladosRechazados'; });
               };

               $scope.setRegresoOrigen = function (entity) {
                   $scope.selectedEntity = entity;
                   $('#regresoOrigenModal').modal('show');
               };

               $scope.confirmRegresoOrigen = function () {
                   solicitudesService.regresarOrigen($scope.selectedEntity.id).then(function (response) {
                       $scope.resultModal = response.data;
                       if ($scope.resultModal.hasErrors) return;

                       $('#regresoOrigenModal').modal('hide');
                       location.reload();
                   });                   
               };

               $scope.setCdd = function (entity) {
                   $scope.resultModal = { hasErrors: false, messages: [] };
                   $scope.selectedEntity = entity;
                   $('#cddModal').modal('show');
               };

               $scope.confirmCdd = function () {
                   if (!$scope.isValidCdd()) return;
                   
                   $scope.selectedEntity.establecimientoDestinoCambioId = $scope.selectedEntity.establecimientoDestinoCambio.id;
                   $scope.selectedEntity.clienteDestinatarioCambioId = $scope.selectedEntity.clienteDestinatarioCambio.id;

                   solicitudesService.cambioDestinoDestinatario($scope.selectedEntity).then(function (response) {
                       $scope.resultModal = response.data.result;
                       if ($scope.resultModal.hasErrors) return;

                       $('#cddModal').modal('hide');
                       location.reload();
                   });
               };

               $scope.isValidCdd = function() {
                   $scope.resultModal = { hasErrors: false, messages: [] };                   

                   if (!$scope.selectedEntity.establecimientoDestinoCambio) {
                       $scope.resultModal.messages.push('Seleccione un destino');
                   }

                   if (!$scope.selectedEntity.clienteDestinatarioCambio) {
                       $scope.resultModal.messages.push('Seleccione un destinatario');
                   }

                   $scope.resultModal.hasErrors = $scope.resultModal.messages.length;
                   return !$scope.resultModal.hasErrors;
               };
               
               $scope.$watch('gridOptions.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.find();
               }, true);

               //#region Select UI

               $scope.sources = {                  
                   'establecimientoDestino': {
                       service: establecimientosService,
                       method: 'getByFilter',
                       filter: { empresaId: $scope.filter.empresaId, destino: true, pageSize: 20, enabled: true },
                   },
                   'cliente': {
                       service: generalService,
                       method: 'getClientesByFilter',
                       filter: { empresaId: $scope.filter.empresaId, filterCresud: $scope.esGrupoCresud, pageSize: 20 },
                   }
               };

               $scope.selectList = [];
               $scope.currentPage = 0;
               $scope.pageCount = 0;

               $scope.getSelectSource = function ($select, $event) {
                   if ($scope.loading) return;

                   var source = $scope.sources[$select.$element.attr('name')];

                   if (!$event) {
                       $scope.currentPage = 1;
                       $scope.pageCount = 0;
                       $scope.selectList = [];
                   } else {
                       $event.stopPropagation();
                       $event.preventDefault();
                       $scope.currentPage++;
                   }

                   source.filter.currentPage = $scope.currentPage;
                   source.filter.empresaId = $scope.filter.empresaId;
                   source.filter.multiColumnSearchText = $select.search;

                   source.service[source.method](source.filter).then(function (response) {
                       $scope.selectList = $scope.selectList.concat(response.data.data);
                       $scope.pageCount = Math.ceil(response.data.count / 20);
                   }, function () { throw 'Error on getSelectByFilter'; });
               };

               //#endregion
              
           }]);
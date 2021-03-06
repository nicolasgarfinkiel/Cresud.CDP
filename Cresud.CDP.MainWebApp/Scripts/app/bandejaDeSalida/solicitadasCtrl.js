﻿angular.module('cresud.cdp.bandejaDeSalida.ctrl.solicitadas', [])
       .controller('solicitadasCtrl', [
           '$scope',
           '$timeout',
           'bandejaDeSalidaService',
           'solicitudesService',
           '$sce',
           function ($scope, $timeout, bandejaDeSalidaService, solicitudesService,  $sce) {
               $scope.filter = {};
               $scope.columns = [];
               $scope.imageSrc = '../content/images/';
               $scope.result = {hasErrors: false, messages: []};

               //#region Init

               bandejaDeSalidaService.getDataListSolicitadas().then(function (response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;
                   $scope.esArgentina = $scope.data.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toLowerCase() == 'argentina';
                   $scope.esParaguay = $scope.data.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toLowerCase() == 'paraguay';

                   $scope.columns = $scope.esArgentina ? $scope.columnsArg : $scope.columnsOtros;

                   $timeout(function() {
                       $scope.find();
                   }, 300);
               });

               //#endregion

               //#region Columns

               $scope.columnsArg = [                   
                    { field: 'id', displayName: 'Id', width: 40 },
                    { field: 'numeroCartaDePorte', displayName: 'Nro.Carta', width: 70 },
                    { field: 'ctg', displayName: 'Ctg', width: 65 },
                    { field: 'tipoCarta', displayName: 'Tipo', width: 140 },
                    { field: 'titularCDP', displayName: 'Titular' },
                    { field: 'estProcedencia', displayName: 'Procedencia' },
                    { field: 'estDestino', displayName: 'Destino' },
                    { field: 'asd', displayName: 'Afip', cellTemplate: '<div style="text-align: center; position: relative;top: 2px;" ng-bind-html="getAfipImg(row.entity)"></div>', width: 38 },
                    { field: 'asd', displayName: 'SAP', cellTemplate: '<div  style="text-align: center; position: relative;top: 2px;" ng-bind-html="getSapImg(row.entity)"></div>', width: 38 },
                    { field: 'observacionAfip', displayName: 'Observaciones AFIP', width: 120 },
                    { field: 'fechaDeEmision', displayName: 'Fecha', width: 70 },
                    { field: 'createdBy', displayName: 'Usuario Creación', width: 110 },
                    { field: 'cuit', displayName: '', width: 25, cellTemplate:
                             '<div class="ng-grid-icon-container"><span compile="getMantenimientoImg(row.entity)"></span></div>'
                    },
                    { field: 'cuit', displayName: '', width: 25, cellTemplate:
                              '<div class="ng-grid-icon-container"><span compile="getReenvioAfipImg(row.entity)"></span></div>'
                    },
                    { field: 'cuit', displayName: '', width: 25, cellTemplate:
                             '<div class="ng-grid-icon-container"><span compile="getReenvioSapImg(row.entity)"></span></div>'
                    },
                    { field: 'cuit', displayName: '', width: 25, cellTemplate:
                             '<div class="ng-grid-icon-container"><span compile="getLogSapImg(row.entity)"></span></div>'
                    },
                    { field: 'cuit', displayName: '', width: 25, cellTemplate:
                             '<div class="ng-grid-icon-container"><span compile="getReporteImg(row.entity)"></span></div>'
                    },
                    {
                      field: 'cuit', displayName: '', width: 25, cellTemplate:
                               '<div class="ng-grid-icon-container"><span compile="getReporteSimpleImg(row.entity)"></span></div>'
                    },
                    { field: 'cuit', displayName: '', width: 25, cellTemplate:
                             '<div class="ng-grid-icon-container"><span compile="getSolicitudImg(row.entity)"></span></div>'
                    }
               ];               

               $scope.columnsOtros = [                  
                   { field: 'id', displayName: 'Id', width: 50 },
                   { field: 'numeroCartaDePorte', displayName: 'Nro.Carta Porte' },
                   { field: 'tipoCarta', displayName: 'Tipo' },
                   { field: 'titularCDP', displayName: 'Titular' },
                   { field: 'estProcedencia', displayName: 'Est.Procedencia' },
                   { field: 'asd', displayName: 'SAP', cellTemplate: '<div  style="text-align: center; position: relative;top: 2px;" ng-bind-html="getSapImg(row.entity)"></div>', width: 38 },
                   { field: 'fechaDeEmision', displayName: 'Fecha' },
                   { field: 'createdBy', displayName: 'Usuario Creación' },
                   { field: 'cuit', displayName: '', width: 25, cellTemplate:
                            '<div class="ng-grid-icon-container"><span compile="getMantenimientoImg(row.entity)"></span></div>'
                   },
                   { field: 'cuit', displayName: '', width: 25, cellTemplate:
                            '<div class="ng-grid-icon-container"><span compile="getReenvioSapImg(row.entity)"></span></div>'
                   },
                   { field: 'cuit', displayName: '', width: 25, cellTemplate:
                            '<div class="ng-grid-icon-container"><span compile="getLogSapImg(row.entity)"></span></div>'
                   },
                   { field: 'cuit', displayName: '', width: 25, cellTemplate:
                            '<div class="ng-grid-icon-container"><span compile="getReporteImg(row.entity)"></span></div>'
                   },
                   { field: 'cuit', displayName: '', width: 25, cellTemplate:
                            '<div class="ng-grid-icon-container"><span compile="getSolicitudImg(row.entity)"></span></div>'
                   }
               ];

               //#endregion
               
               //#region Icons

               $scope.getAfipImg = function (item) {
                   if (!item.tipoCarta || item.tipoCarta == 'Compra de granos') return '';

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

               $scope.getSapImg = function (item) {
                   if (!item.tipoCarta) return '';
                   
                   var icons = {
                       '0': { icon: function () { return item.estadoEnAFIP == 2 ? 'circle_red.png' : 'circle_yellow.png'; }, title: $scope.getEstadoSapDescripcion(0) },
                       '1': { icon: 'circle_Orange.png', title: $scope.getEstadoSapDescripcion(1) },
                       '2': { icon: 'circle_green.png', title: $scope.getEstadoSapDescripcion(2)  + "\n\rCodigo Envio SAP:" + item.codigoRespuestaEnvioSAP },
                       '3': { icon: 'circle_red.png', title: $scope.getEstadoSapDescripcion(3) },
                       '4': { icon: 'icon_Delete.png', title: $scope.getEstadoSapDescripcion(4) },
                       '5': { icon: 'iconespera.png', title: $scope.getEstadoSapDescripcion(5) },
                       '6': { icon: 'vueltaorigen.png', title: $scope.getEstadoSapDescripcion(6) },
                       '8': { icon: 'datosPropios.png', title: $scope.getEstadoSapDescripcion(8) },
                       '9': { icon: 'busquedaEmpleados.png', title: $scope.getEstadoSapDescripcion(9) }
                   };

                   var icon = icons[item.estadoEnSAP] || {};
                   var result = '<img style="width: 15px;" src="' + $scope.imageSrc + (typeof icon.icon == 'function' ? icon.icon() : icon.icon) + '" title="' + icon.title + '" />';

                   return $sce.trustAsHtml(icon.icon ? result : '');
               };

               $scope.getReenvioAfipImg = function (item) {
                   var reenviosAfip = $scope.data.usuario.currentEmpresa.roles.indexOf('ReenviosAFIP') != -1;
                   if (!reenviosAfip || (item.estadoEnAFIP != 0 && item.estadoEnAFIP != 2) || !item.tipoCarta) return '';

                   var result = item.tipoCarta == 'Compra de granos' ?
                       '<img style="width: 15px;" src="' + $scope.imageSrc + 'icon_Delete.png" />' :
                       '<a title="Reenviar afip" href="javascript:void(0)" ng-click="confirmReenviarAfip(' + item.id + ')"><img style="width: 15px;" src="' + $scope.imageSrc + 'icon_select.gif" /></a>';
                                                                     
                   return result;
               };

               $scope.getReenvioSapImg = function (item) {
                   var reenviosSap = $scope.data.usuario.currentEmpresa.roles.indexOf('ReenviosSAP') != -1;
                   if (!reenviosSap || !item.tipoCarta || item.estadoEnSAP == 8 || item.tipoCarta == 'Compra de granos que transportamos') return '';

                   var result = item.tipoCarta == 'Compra de granos que transportamos' || item.tipoCarta == 'Compra de granos' ?
                       '<img style="width: 15px;" src="' + $scope.imageSrc + 'icon_Delete.png" />' :
                       '<a title="Reenviar Sap" href="javascript:void(0)" ng-click="confirmReenviarSap(' + item.id + ')"><img style="width: 15px;" src="' + $scope.imageSrc + 'icon_select.gif" /></a>';

                   return result;
               };

               $scope.getLogSapImg = function (item) {
                   var log = $scope.data.usuario.currentEmpresa.roles.indexOf('Visualizacion Log SAP') != -1;
                   if (!log || !item.mensajeRespuestaEnvioSap) return '';

                   var result = '<a title="Ver detalle respuesta SAP" href="javascript:void(0)" ng-click="showLogSap(' + item.id + ')"><img style="width: 15px;" src="' + $scope.imageSrc + 'logsap.png" /></a>';

                   return result;
               };

               $scope.getReporteImg = function (item) {
                   var imprimir = $scope.data.usuario.currentEmpresa.roles.indexOf('Imprimir Solicitud') != -1;
                   if (!imprimir || ($scope.esArgentina && item.estadoEnAFIP == 3)) return '';

                   var result = '<form title="Imprimir Carta de Porte" action="/BandejaDeSalida/ReportePdf" style="display: inline;"><input type="hidden" name="solicitudId" value="' + item.id + '" /><input type="hidden" name="numeroCartaDePorte" value="' + item.numeroCartaDePorte + '" /><button type="submit" style="border: 0; background: 0;"><img style="width: 15px;" src="' + $scope.imageSrc + 'folder-print.png" /></button></form>';
              
                   return result;
               };

               $scope.getReporteSimpleImg = function (item) {
                   var imprimir = $scope.data.usuario.currentEmpresa.roles.indexOf('Imprimir Solicitud') != -1;
                   if (!imprimir || ($scope.esArgentina && item.estadoEnAFIP == 3)) return '';

                   var result = '<form title="Imprimir Carta de Porte Simple" action="/BandejaDeSalida/ReporteSimplePdf" style="display: inline;"><input type="hidden" name="solicitudId" value="' + item.id + '" /><input type="hidden" name="numeroCartaDePorte" value="' + item.numeroCartaDePorte + '" /><button type="submit" style="border: 0; background: 0;"><img style="width: 15px;" src="' + $scope.imageSrc + 'folder-print-simple.png" /></button></form>';

                   return result;
               };

               $scope.getSolicitudImg = function (item) {
                   var visualizar = $scope.data.usuario.currentEmpresa.roles.indexOf('Visualizacion Solicitud') != -1;
                   if (!visualizar || (item.estadoEnAFIP == 3 && item.estadoEnSAP == 4)) return '';

                   var icon = item.observacionAfip && item.observacionAfip.indexOf('Reserva') == -1 ? 'cargaCartaDePorteReservada.png' : 'magnify.gif';
                   var result = '<a title="Abrir Solicitud"  href="/solicitudes/Index#/edit/' + item.id + '" ><img style="width: 15px;" src="' + $scope.imageSrc + icon + '" /></a>';

                   return result;
               };

               $scope.getMantenimientoImg = function (item) {
                   var bd = $scope.data.usuario.currentEmpresa.roles.indexOf('BaseDeDatos') != -1;
                   var seguimientos = $scope.data.usuario.currentEmpresa.roles.indexOf('SeguimientoEstados') != -1;
                   if (!bd || !seguimientos) return '';
                   
                   var result = '<a href="javascript:void(0)" ng-click="setMantenimiento(' + item.id +')"><img style="width: 15px;" src="' + $scope.imageSrc + 'mantenimiento.png" /></a>';

                   return result;
               };               

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

               //#endregion

               //#region ReenvioSap
             
               $scope.confirmReenviarSap = function () {
                   $scope.result.hasErrors = false;
                   $('#confirmReenviarSapModal').modal('show');
               };
             
               $scope.reenviarSap = function () {
                   solicitudesService.reenviarSap($scope.selectedEntity.id).then(function (response) {
                       $scope.result = response.data;
                       if ($scope.result.hasErrors) return;

                       $('#confirmReenviarSapModal').modal('hide');
                       $scope.find();
                   }, function () { throw 'Error on reenviarSap'; });
               };

               //#endregion

               //#region ReenvioAfip

               $scope.confirmReenviarAfip = function () {
                   $scope.result.hasErrors = false;
                   $('#confirmReenviarAfipModal').modal('show');
               };

               $scope.reenviarAfip = function () {
                   solicitudesService.reenviarAfip($scope.selectedEntity.id).then(function (response) {
                       $scope.result = response.data;
                       if ($scope.result.hasErrors) return;

                       $('#confirmReenviarAfipModal').modal('hide');
                       $scope.find();
                   }, function () { throw 'Error on reenviarAfip'; });
               };

               //#endregion

               //#region LogSap

               $scope.showLogSap = function (id) {
                   $scope.selectedEntity = $scope.getSolicitudById(id);
                   $scope.findLogSap();
                   $('#logSapModal').modal('show');
               };

               $scope.logSapList = [];
               $scope.logSapListCount = 0;
               $scope.filterlogSap = {};            
                           
               $scope.gridLogSap = {
                   data: 'logSapList',
                   columnDefs: [
                                { field: 'nroEnvio', displayName: '#Envío', width: 60 },
                                { field: 'fechaCreacion', displayName: 'Fecha', width: 80 },
                                { field: 'origen', displayName: 'Origen', width: 100 },
                                { field: 'nroDocumentoRE', displayName: 'Carta de Porte', width: 100 },
                                { field: 'nroDocumentoSap', displayName: 'Nro.Doc.Sap', width: 100 },
                                { field: 'tipoMensaje', displayName: 'Tipo Mensaje', width: 100 },
                                { field: 'textoMensaje', displayName: 'Texto Mensaje' }                                
                   ],
                   showFooter: true,
                   enablePaging: true,
                   multiSelect: false,
                   totalServerItems: 'logSapListCount',
                   pagingOptions: {
                       pageSizes: [10],
                       pageSize: 10,
                       currentPage: 1
                   },
                   filterOptions: { useExternalFilter: true }
               };

               $scope.findLogSap = function () {
                   $scope.logSapList = [];                   

                   $scope.filterlogSap.currentPage = $scope.gridLogSap.pagingOptions.currentPage;
                   $scope.filterlogSap.pageSize = $scope.gridLogSap.pagingOptions.pageSize;
                   $scope.filterlogSap.nroDocumentoRE = $scope.esParaguay ?
                       $scope.selectedEntity.numeroCartaDePorte + '|' + $scope.data.usuario.currentEmpresa.idSapOrganizacionDeVenta :
                       $scope.selectedEntity.numeroCartaDePorte;

                   bandejaDeSalidaService.getLogSapByFilter($scope.filterlogSap).then(function (response) {
                       $scope.logSapList = response.data.data;
                       $scope.logSapListCount = response.data.count;
                   }, function () { throw 'Error on getLogSapByFilter'; });
               };

               $scope.$watch('gridLogSap.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.findLogSap();
               }, true);

               //$scope.$watch('filterClientes.multiColumnSearchText', function () {
               //    $scope.gridClientes.pagingOptions.currentPage = 1;
               //    $scope.findClientes();
               //});

               //#endregion

               //#region Mantenimiento

               $scope.setMantenimiento = function () {
                   $scope.result.hasErros = false;
                   $('#mantenimientoModal').modal('show');
               };

               $scope.saveMantenimiento = function() {
                   solicitudesService.updateSimple($scope.selectedEntity).then(function (response) {
                       $('#mantenimientoModal').modal('hide');
                       $scope.find();
                   }, function () { throw 'Error on updateSimple'; });
               };

               //#endregion

               $scope.getSolicitudById = function(id) {
                   var result = null;

                   for (var i = 0; i < $scope.solicitudes.length; i++) {
                       if ($scope.solicitudes[i].id == id) {
                           result = $scope.solicitudes[i];
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
                   filterOptions: { useExternalFilter: true },
                   afterSelectionChange: function (row) {
                       if (row.selected) {
                           $scope.selectedEntity = row.entity;
                       }
                   },
               };

               $scope.find = function (clear) {
                   $scope.solicitudes = [];

                   if (!$scope.filter.empresaId) return;

                   if (clear) {
                       $scope.gridOptions.pagingOptions.currentPage = 1;
                   }

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
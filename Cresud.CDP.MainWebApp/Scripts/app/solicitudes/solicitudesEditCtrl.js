angular.module('cresud.cdp.solicitudes.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'solicitudesService',
           'baseNavigationService',
           'editBootstraperService',
           'establecimientosService',
           function ($scope, $routeParams, solicitudesService, baseNavigationService, editBootstraperService, establecimientosService) {
               $scope.loading = true;
               $scope.resultAfip = { message: null, hasErros: false };
             
               //#region Base

               $scope.onInitEnd = function () {
                   $scope.empresaId = $scope.usuario.currentEmpresa.id;
                   $scope.esParaguay = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toUpperCase() == 'PARAGUAY';
                   $scope.esArgentina = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toUpperCase() == 'ARGENTINA';
                   $scope.esGrupoCresud = $scope.usuario.currentEmpresa.grupoEmpresa.id == 1;
                   $scope.activarModelo = $routeParams.activarModelo;
                   $scope.rolAltaSolicitud = $scope.usuario.currentEmpresa.roles.indexOf('Alta Solicitud') >= 0;

                   $scope.sources = {
                       'establecimientoOrigen': {
                           service: establecimientosService,
                           filter: { empresaId: $scope.empresaId, origen: true, pageSize: 20 },
                       },
                       'establecimientoDestino': {
                           service: establecimientosService,
                           filter: { empresaId: $scope.empresaId, destino: true, pageSize: 20 },
                       }
                   };

                   $scope.loading = false;

                   $scope.setControls();
                   $scope.setDefaultValues();
               };

               editBootstraperService.init($scope, $routeParams, {
                   service: solicitudesService,
                   navigation: baseNavigationService
               });

               $scope.isValid = function () {
                   $scope.result = { hasErrors: false, messages: [] };

                   if (!$scope.entity.nombre) {
                       $scope.result.messages.push($scope.entity.esChoferTransportista ? 'Ingrese la descripción' : 'Ingrese el nombre');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };

               //#endregion               

               $scope.setControls = function () {
                   $scope.mensajeAfipReserva = $scope.resultAfip.message && $scope.resultAfip.message.indexOf('Reserva') >= 0;

                   $scope.manual = $scope.entity.tipoDeCartaId == 2 ||
                                   $scope.entity.tipoDeCartaId == 7 ||
                                   $scope.entity.tipoDeCartaId == 4;

                   $scope.controlsVisibility = {};
                   $scope.controlsVisibility.fechaDeEmision =
                   $scope.controlsVisibility.fechaDeVencimiento =
                   $scope.controlsVisibility.ctg = $scope.mensajeAfipReserva || ($scope.manual && !$scope.entity.id);
                   $scope.controlsVisibility.numeroCartaDePorte =
                   $scope.controlsVisibility.cee = $scope.manual && !$scope.entity.id;


               };

               $scope.setDefaultValues = function () {

               };

               //#region Select UI
                              
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
                   source.filter.multiColumnSearchText = $select.search;

                   source.service.getByFilter(source.filter).then(function (response) {
                       $scope.selectList = $scope.selectList.concat(response.data.data);
                       $scope.pageCount = Math.ceil(response.data.count / 20);
                   }, function () { throw 'Error on getSelectByFilter'; });
               }

               //#endregion

               //#region Watches

               $scope.$watch('entity.tipoDeCartaId', function (newValue, oldValue) {
                   if ($scope.loading) return;
                   $scope.setControls();
               });

               $scope.$watch('entity.cargaPesadaDestino', function (newValue, oldValue) {
                   if ($scope.loading) return;

                   if (!newValue) {
                       $scope.entity.pesoTara = null;
                       $scope.entity.pesoBruto = null;
                       $scope.entity.kilogramosEstimados = null;
                   }
               });

               //#endregion
           }]);
﻿-angular.module('cresud.cdp.lotes.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'lotesService',
           'establecimientosService',
           'baseNavigationService',
           'editBootstraperService',
           'singleFileService',
           function ($scope, $routeParams, lotesService, establecimientosService, baseNavigationService, editBootstraperService, singleFileService) {
               //#region Base

               $scope.onInitEnd = function () {
                   $scope.filterEstablecimientos.empresaId = $scope.usuario.currentEmpresa.id;
                   $scope.filterLotes.idGrupoEmpresa = $scope.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.esParaguay = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toLowerCase() == 'paraguay';
                   $scope.operation = $scope.esParaguay ? 'Alta de lote' : 'Alta de Rango de Cartas de Porte solicitadas a la AFIP';
                   $scope.findLotes();

                   singleFileService.setUploader({
                       onError: $scope.onErrorFileCallBack,
                       onSuccess: $scope.onSuccessFileCallBack,
                       controlId: 'fileupload',
                       urlFile: 'Lotes/UploadPdf'
                   });
               };

               editBootstraperService.init($scope, $routeParams, {
                   service: lotesService,
                   navigation: baseNavigationService
               });

               $scope.isValid = function () {
                   $scope.result = { hasErrors: false, messages: [] };

                   if (!$scope.entity.desde) {
                       $scope.result.messages.push('Ingrese el rango desde');
                   }

                   if (!$scope.entity.hasta) {
                       $scope.result.messages.push('Ingrese el rango hasta');
                   }

                   if ($scope.entity.desde && $scope.entity.hasta && $scope.entity.hasta <= $scope.entity.desde) {
                       $scope.result.messages.push('El rango hasta debe ser mayor al rango desde');
                   }

                   if (!$scope.entity.cee) {
                       $scope.result.messages.push('Ingrese el número de CEE');
                   }

                   if (!$scope.entity.fechaVencimiento) {
                       $scope.result.messages.push('Ingrese la fecha de vencimiento');
                   } else {
                       var fechaActual = moment(moment().format('DD/MM/YYYY'), 'DD/MM/YYYY');
                       var fecha = moment(moment($scope.entity.fechaVencimiento).format('DD/MM/YYYY'), 'DD/MM/YYYY').add(1, 'days');

                       if (fechaActual >= fecha) {
                           $scope.result.messages.push('La fecha de vencimiento debe ser mayor a la fecha actual');
                       }
                   }

                   if ($scope.esParaguay && !$scope.entity.sucursal) {
                       $scope.result.messages.push('Ingrese el nro. de sucursal');
                   }

                   if ($scope.esParaguay && !$scope.entity.puntoEmision) {
                       $scope.result.messages.push('Ingrese el punto de emisión');
                   }

                   if ($scope.esParaguay && !$scope.entity.habilitacionNumero) {
                       $scope.result.messages.push('Ingrese el nro. timbrado habilitación');
                   }

                   if ($scope.esParaguay && !$scope.entity.fechaDesde) {
                       $scope.result.messages.push('Ingrese la fecha desde');
                   }

                   if (!$scope.fileLoaded) {
                       $scope.result.messages.push('Seleccione un archivo de tipo pdf');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };

               //#endregion

               $scope.onErrorFileCallBack = function (message) {
                   $scope.$apply(function () {
                       $scope.fileLoaded = false;
                       $scope.result.hasErrors = true;
                       $scope.result.messages = [message || 'Se produjo un error al intentar subir el archivo'];
                   });
               };

               $scope.onSuccessFileCallBack = function () {
                   $scope.$apply(function () {
                       $scope.fileLoaded = true;
                       $scope.result.hasErrors = false;
                   });
               };

               //#region Establecimientos

               $scope.establecimientos = [];
               $scope.establecimientosCount = 0;
               $scope.filterEstablecimientos = { origen: true };

               $scope.setEstablecimiento = function (establecimiento) {
                   $scope.entity.establecimientoOrigenId = establecimiento.id;
                   $scope.entity.establecimientoOrigenDescripcion = establecimiento.descripcion;
                   $('#establecimientosModal').modal('hide');
               };

               $scope.gridEstablecimientos = {
                   data: 'establecimientos',
                   columnDefs: [
                                { field: 'descripcion', displayName: 'Descripción' },
                                { field: 'direccion', displayName: 'Dirección' },
                                { field: 'cuit', displayName: 'Seleccionar', width: 120, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="setEstablecimiento(row.entity)"><i class="fa fa-thumbs-o-up"></i></a></div>' }

                   ],
                   showFooter: true,
                   enablePaging: true,
                   multiSelect: false,
                   totalServerItems: 'establecimientosCount',
                   pagingOptions: {
                       pageSizes: [10],
                       pageSize: 10,
                       currentPage: 1
                   },
                   filterOptions: { useExternalFilter: true }
               };

               $scope.findEstablecimientos = function () {
                   $scope.establecimientos = [];

                   if (!$scope.filterEstablecimientos.empresaId) return;

                   $scope.filterEstablecimientos.currentPage = $scope.gridEstablecimientos.pagingOptions.currentPage;
                   $scope.filterEstablecimientos.pageSize = $scope.gridEstablecimientos.pagingOptions.pageSize;

                   establecimientosService.getByFilter($scope.filterEstablecimientos).then(function (response) {
                       $scope.establecimientos = response.data.data;
                       $scope.establecimientosCount = response.data.count;
                   }, function () { throw 'Error on getByFilter'; });
               };

               $scope.$watch('gridEstablecimientos.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.findEstablecimientos();
               }, true);

               $scope.$watch('filterEstablecimientos.multiColumnSearchText', function () {
                   $scope.gridEstablecimientos.pagingOptions.currentPage = 1;
                   $scope.findEstablecimientos();
               });

               //#endregion

               //#region Lotes

               $scope.lotes = [];
               $scope.lotesCount = 0;
               $scope.filterLotes = { vigente: true };
               $scope.lotesColumns = $scope.esParaguay ?
                   [
                       { field: 'sucursal', displayName: 'Sucursal', width: 60 },
                       { field: 'puntoEmision', displayName: 'Punto de Venta', width: 60 },
                       { field: 'id', displayName: 'Lote', width: 60 },
                       { field: 'desde', displayName: 'Desde', width: 100 },
                       { field: 'hasta', displayName: 'Hasta', width: 100 },
                       { field: 'cee', displayName: 'Timbrado', width: 120 },
                       { field: 'establecimientoOrigen', displayName: 'Establecimiento Origen' },
                       { field: 'fechaDesde', displayName: 'Fecha Desde', width: 100 },
                       { field: 'fechaVencimiento', displayName: 'Fecha Vencimiento', width: 100 },
                       { field: 'disponibles', displayName: 'Cantidad Disponible', width: 100 },
                       { field: 'createdBy', displayName: 'Usuario Creación' }
                   ]
                   :
                   [
                       { field: 'id', displayName: 'Lote', width: 60 },
                       { field: 'desde', displayName: 'Desde', width: 100 },
                       { field: 'hasta', displayName: 'Hasta', width: 100 },
                       { field: 'cee', displayName: 'Cee', width: 120 },
                       { field: 'establecimientoOrigen', displayName: 'Establecimiento Origen' },
                       { field: 'fechaVencimiento', displayName: 'Fecha Vencimiento' },
                       { field: 'disponibles', displayName: 'Cantidad Disponible' },
                       { field: 'createdBy', displayName: 'Usuario Creación' }
                   ];

               $scope.gridLotes = {
                   data: 'lotes',
                   columnDefs: 'lotesColumns',
                   showFooter: true,
                   enablePaging: true,
                   multiSelect: false,
                   totalServerItems: 'lotesCount',
                   pagingOptions: {
                       pageSizes: [10],
                       pageSize: 10,
                       currentPage: 1
                   },
                   filterOptions: { useExternalFilter: true }
               };

               $scope.findLotes = function () {
                   if (!$scope.filterLotes.idGrupoEmpresa) return;

                   $scope.filterLotes.currentPage = $scope.gridLotes.pagingOptions.currentPage;
                   $scope.filterLotes.pageSize = $scope.gridLotes.pagingOptions.pageSize;

                   lotesService.getByFilter($scope.filterLotes).then(function (response) {
                       $scope.lotes = response.data.data;
                       $scope.lotesCount = response.data.count;
                   }, function () { throw 'Error on getByFilter'; });
               };

               $scope.$watch('gridLotes.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.findLotes();
               }, true);

               //#endregion               
           }]);
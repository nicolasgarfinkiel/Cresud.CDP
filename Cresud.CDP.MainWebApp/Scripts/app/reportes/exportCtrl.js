angular.module('cresud.cdp.reportes.ctrl.export', [])
       .controller('exportCtrl', [
           '$scope',
           '$timeout',
           'reportesService',
           'reportesNavigationService',
           'generalService',
           'choferesService',
           'establecimientosService',
           function ($scope, $timeout, reportesService, reportesNavigationService, generalService, choferesService, establecimientosService) {
               $scope.loading = true;
               $scope.filter = {};
               $scope.filterProveedores = {};
               $scope.filterChoferes = {};
               $scope.filterEstablecimientos = {};

               reportesService.getDataListExport().then(function(response) {
                   $scope.data = response.data.data;
              
                   $scope.sources = {
                       'proveedorTitular': {
                           service: generalService,
                           method: 'getProveedoresByFilter',
                           filter: { empresaId: $scope.data.usuario.currentEmpresa.id, idGrupoEmpresa: $scope.data.usuario.currentEmpresa.grupoEmpresa.id, pageSize: 20, enabled: true },
                       },
                       'proveedorTransportista': {
                           service: generalService,
                           method: 'getProveedoresByFilter',
                           filter: { empresaId: $scope.data.usuario.currentEmpresa.id, idGrupoEmpresa: $scope.data.usuario.currentEmpresa.grupoEmpresa.id, pageSize: 20, enabled: true },
                       },
                       'chofer': {
                           service: choferesService,
                           method: 'getByFilter',
                           filter: { empresaId: $scope.data.usuario.currentEmpresa.id, idGrupoEmpresa: $scope.data.usuario.currentEmpresa.grupoEmpresa.id, pageSize: 20, enabled: true },
                       },
                       'establecimientoOrigen': {
                           service: establecimientosService,
                           method: 'getByFilter',
                           filter: { empresaId: $scope.data.usuario.currentEmpresa.id, origen: true, pageSize: 20, enabled: true },
                       },
                       'establecimientoDestino': {
                           service: establecimientosService,
                           method: 'getByFilter',
                           filter: { empresaId: $scope.data.usuario.currentEmpresa.id, destino: true, pageSize: 20, enabled: true },
                       }
                   };

                   $timeout(function () {
                       $scope.loading = false;
                   }, 300);
               });

               $scope.clearFilter = function() {
                   $scope.filter = {};
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;
               };

               $scope.$watch('filter.fechaDesde', function (newValue) {
                   $scope.fechaDesde = moment($scope.filter.fechaDesde).add(1, 'days').format('YYYY/MM/DD');
               });

               $scope.$watch('filter.fechaHasta', function (newValue) {
                   $scope.fechaHasta = moment($scope.filter.fechaHasta).add(1, 'days').format('YYYY/MM/DD');
               });

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

                   source.service[source.method](source.filter).then(function (response) {
                       $scope.selectList = $scope.selectList.concat(response.data.data);
                       $scope.pageCount = Math.ceil(response.data.count / 20);
                   }, function () { throw 'Error on getSelectByFilter'; });
               };

               $scope.$watch('filter.proveedorTitularCartaDePorte', function (newValue, oldValue) {
                   if ($scope.loading || !newValue) return;

                   $scope.filter.proveedorTitularCartaDePorteId = newValue.id;
               });

               $scope.$watch('filter.proveedorTransportista', function (newValue, oldValue) {
                   if ($scope.loading || !newValue) return;

                   $scope.filter.proveedorTransportistaId = newValue.id;
               });

               $scope.$watch('filter.chofer', function (newValue, oldValue) {
                   if ($scope.loading || !newValue) return;

                   $scope.filter.choferId = newValue.id;
               });

               $scope.$watch('filter.establecimientoProcedencia', function (newValue, oldValue) {
                   if ($scope.loading || !newValue) return;

                   $scope.filter.establecimientoProcedenciaId = newValue.id;
               });

               $scope.$watch('filter.establecimientoDestino', function (newValue, oldValue) {
                   if ($scope.loading || !newValue) return;

                   $scope.filter.establecimientoDestinoId = newValue.id;
               });

               //#endregion

               //#region Proveedores

               $scope.proveedores = [];
               $scope.proveedoresCount = 0;
               

               $scope.setProveedor = function (proveedor) {
                   if ($scope.filter.type == 'Titular') {
                       $scope.filter.proveedorTitularCartaDePorteId = proveedor.id;
                       $scope.filter.proveedorTitularCartaDePorteDescripcion = proveedor.nombre;
                   } else {
                       $scope.filter.proveedorTransportistaId = proveedor.id;
                       $scope.filter.proveedorTransportistaDescripcion = proveedor.nombre;                       
                   }
                   
                   $('#proveedoresModal').modal('hide');
               };

               $scope.gridProveedores = {
                   data: 'proveedores',
                   columnDefs: [
                                { field: 'nombre', displayName: 'Descripción' },
                                { field: 'numeroDocumento', displayName: 'Documento' },
                                { field: 'cuit', displayName: 'Seleccionar', width: 120, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="setProveedor(row.entity)"><i class="fa fa-thumbs-o-up"></i></a></div>' }

                   ],
                   showFooter: true,
                   enablePaging: true,
                   multiSelect: false,
                   totalServerItems: 'proveedoresCount',
                   pagingOptions: {
                       pageSizes: [10],
                       pageSize: 10,
                       currentPage: 1
                   },
                   filterOptions: { useExternalFilter: true }
               };

               $scope.findProveedores = function () {
                   $scope.proveedores = [];

                   if (!$scope.filterProveedores.empresaId) return;

                   $scope.filterProveedores.currentPage = $scope.gridProveedores.pagingOptions.currentPage;
                   $scope.filterProveedores.pageSize = $scope.gridProveedores.pagingOptions.pageSize;

                   generalService.getProveedoresByFilter($scope.filterProveedores).then(function (response) {
                       $scope.proveedores = response.data.data;
                       $scope.proveedoresCount = response.data.count;
                   }, function () { throw 'Error on getProveedoresByFilter'; });
               };

               $scope.$watch('gridProveedores.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.findProveedores();
               }, true);

               $scope.$watch('filterProveedores.multiColumnSearchText', function () {
                   $scope.gridProveedores.pagingOptions.currentPage = 1;
                   $scope.findProveedores();
               });

               //#endregion

               //#region Choferes

               $scope.choferes = [];
               $scope.choferesCount = 0;

               $scope.setChofer = function (chofer) {
                   $scope.filter.choferId = chofer.id;
                   $scope.filter.choferDescripcion = chofer.nombre + ' ' + chofer.apellido;

                   $('#choferesModal').modal('hide');
               };

               $scope.gridChoferes = {
                   data: 'choferes',
                   columnDefs: [
                            { field: 'nombre', displayName: 'Nombre' },
                            { field: 'apellido', displayName: 'Apellido' },
                            { field: 'cuit', displayName: 'Cuit', width: 100 },
                            { field: 'esChoferTransportista ? "Si" : "No" ', displayName: 'Transportista', width: 110 },                            
                            { field: 'cuit', displayName: 'Seleccionar', width: 120, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="setChofer(row.entity)"><i class="fa fa-thumbs-o-up"></i></a></div>' }

                   ],
                   showFooter: true,
                   enablePaging: true,
                   multiSelect: false,
                   totalServerItems: 'choferesCount',
                   pagingOptions: {
                       pageSizes: [10],
                       pageSize: 10,
                       currentPage: 1
                   },
                   filterOptions: { useExternalFilter: true }
               };

               $scope.findChoferes = function () {
                   $scope.choferes = [];

                   if (!$scope.filterChoferes.empresaId) return;

                   $scope.filterProveedores.currentPage = $scope.gridChoferes.pagingOptions.currentPage;
                   $scope.filterProveedores.pageSize = $scope.gridChoferes.pagingOptions.pageSize;

                   choferesService.getByFilter($scope.filterProveedores).then(function (response) {
                       $scope.choferes = response.data.data;
                       $scope.choferesCount = response.data.count;
                   }, function () { throw 'Error on getByFilter'; });
               };

               $scope.$watch('gridChoferes.pagingOptions', function (newVal, oldVal) {
                   if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                   $scope.findChoferes();
               }, true);

               $scope.$watch('filterChoferes.multiColumnSearchText', function () {
                   $scope.gridChoferes.pagingOptions.currentPage = 1;
                   $scope.findChoferes();
               });

               //#endregion

               //#region Establecimientos

               $scope.establecimientos = [];
               $scope.establecimientosCount = 0;              

               $scope.openEstablecimientosModal = function (origen) {
                   $scope.establecimientos = [];
                   $scope.filterEstablecimientos.origen = origen;
                   $scope.filterEstablecimientos.destino = !origen;
                   $('#establecimientosModal').modal('show');
               }

               $scope.setEstablecimiento = function (establecimiento) {
                   if ($scope.filterEstablecimientos.origen) {
                       $scope.filter.establecimientoProcedenciaId = establecimiento.id;
                       $scope.filter.establecimientoProcedenciaDescripcion = establecimiento.descripcion;
                   } else {
                       $scope.filter.establecimientoDestinoId = establecimiento.id;
                       $scope.filter.establecimientoDestinoDescripcion = establecimiento.descripcion;
                   }
                  
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
           }]);
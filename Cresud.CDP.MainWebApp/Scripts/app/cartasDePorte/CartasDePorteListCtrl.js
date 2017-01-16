angular.module('cresud.cdp.cartasDePorte.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'cartasDePorteService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, cartasDePorteService, baseNavigationService, listBootstraperService) {
               $scope.onInitEnd = function () {
                   $scope.esArgentina = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toLowerCase() == 'argentina';
                   $scope.columns[3].displayName = $scope.esArgentina ? 'Cee' : 'Timbrado';
               };

               $scope.deleteLote = function (lote) {
                   $scope.currentLote = lote;
                   $scope.confirmDeleteMessage = lote.cantidad == lote.disponibles ?
                       'El lote ' + lote.id + ' no fue utilizado por lo tanto sera eliminado. Items no utilizados desde el número ' + lote.desde + ' hasta el número ' + lote.hasta + '.' :
                       'Se eliminarán ' + lote.disponibles + ' items no utilizados del lote ' + lote.id;

                   $('#deleteModal').modal('show');
               };

               $scope.deleteLoteConfirm = function () {
                   cartasDePorteService.deleteEntity($scope.currentLote.id).then(function (response) {
                       $('#deleteModal').modal('hide');
                       $scope.currentLote = null;
                       $scope.search();
                   }, function () { throw 'Error on getByFilter'; });
               };             

               $scope.$watch('filter.fechaDesde', function(newValue) {
                   $scope.fechaDesde = moment($scope.filter.fechaDesde).add(1, 'days').format('YYYY/MM/DD');
               });

               $scope.$watch('filter.fechaHasta', function (newValue) {
                   $scope.fechaHasta = moment($scope.filter.fechaHasta).add(1, 'days').format('YYYY/MM/DD');
               });

               listBootstraperService.init($scope, {
                   service: cartasDePorteService,
                   navigation: baseNavigationService,
                   columns: [
                       { field: 'id', displayName: 'Lote', width: 60 },
                       { field: 'desde', displayName: 'Desde', width: 100 },
                       { field: 'hasta', displayName: 'Hasta', width: 100 },
                       { field: 'cee', displayName: 'Cee', width: 120 },
                       { field: 'establecimientoOrigen', displayName: 'Establecimiento Origen' },
                       { field: 'fechaVencimiento', displayName: 'Fecha Vencimiento' },
                       { field: 'disponibles', displayName: 'Cantidad Disponible' },
                       { field: 'createdBy', displayName: 'Usuario Creación' },
                       { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-if="row.entity.disponibles" ng-click="deleteLote(row.entity)"><i class="fa fa-remove"></i></a></div>' }
                   ]
               });
           }]);




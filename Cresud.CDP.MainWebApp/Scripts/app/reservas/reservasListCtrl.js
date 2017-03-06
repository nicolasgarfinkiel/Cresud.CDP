angular.module('cresud.cdp.reservas.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'reservasService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, reservasService, baseNavigationService, listBootstraperService) {
               //#region base

               $scope.onInitEnd = function () {
                                
               };
             
               listBootstraperService.init($scope, {
                   service: reservasService,
                   navigation: baseNavigationService,
                   columns: [
                       { field: 'numeroCartaDePorte', displayName: 'Número CDP' },
                       { field: 'cee', displayName: 'Cee' },                                              
                       { field: 'createDate', displayName: 'Fecha reserva', width: 120 },
                       { field: 'createdBy', displayName: 'Usuario reserva' },
                       { field: 'cuit', displayName: 'Acciones', width: 150,
                         cellTemplate:
                             '<div class="ng-grid-icon-container">' +
                                 '<a href="javascript:void(0)" title="Cancelar" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="cancelarConfirm(row.entity)"><i class="fa fa-times-circle"></i></a>' +
                                 '<a href="javascript:void(0)" title="Anular" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="anularConfirm(row.entity)"><i class="fa fa-times-circle-o"></i></a>' +
                                 '<a href="/solicitudes#/edit/{{row.entity.id}}" title="Cargar" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="carga(row.entity)"><i class="fa fa-plus"></i></a>' +
                             '</div>'
                       }
                   ]
               });

               //#endregion

               $scope.cancelarConfirm = function(solicitud) {
                   $scope.currentSolicitud = solicitud;
                   $scope.title = 'Cancelar carta de porte';
                   $scope.confirmMessage = '¿Desea Cancelar la carta de porte ' + solicitud.numeroCartaDePorte + ' previamente reservada?';

                   $('#confirmModal').modal('show');
               };

               $scope.anularConfirm = function(solicitud) {
                   $scope.currentSolicitud = solicitud;
                   $scope.title = 'Anular carta de porte';
                   $scope.confirmMessage = '¿Desea Anular la carta de porte ' + solicitud.numeroCartaDePorte + 'previamente reservada?. Recuerde que la carta de porte se debe ANULAR solamente si fue utilizada y Anulada fuera del sistema.';

                   $('#confirmModal').modal('show');
               };

               $scope.confirm = function() {
                   if ($scope.title == 'Cancelar carta de porte') {
                       reservasService.cancelar($scope.currentSolicitud.id).then(function (response) {
                           $('#confirmModal').modal('hide');
                           $scope.currentLote = null;
                           $scope.search();
                       }, function () { throw 'Error on cancelar'; });
                   } else {
                       reservasService.anular($scope.currentSolicitud.id).then(function (response) {
                           $('#confirmModal').modal('hide');
                           $scope.currentSolicitud = null;
                           $scope.search();
                       }, function () { throw 'Error on anular'; });
                   }
               };

               $scope.$watch('filter.asignadasAMi', function (newValue) {
                   if (typeof newValue == 'undefined') return;

                   $scope.gridOptions.pagingOptions.currentPage = 1;
                   $scope.search();
               });
           }]);
angular.module('cresud.cdp.reservas.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'reservasService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, reservasService, baseNavigationService, listBootstraperService) {
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
                       { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a></div>' }
                   ]
               });
           }]);
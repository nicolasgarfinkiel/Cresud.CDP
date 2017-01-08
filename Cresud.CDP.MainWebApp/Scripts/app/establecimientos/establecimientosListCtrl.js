angular.module('cresud.cdp.establecimientos.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'establecimientosService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, establecimientosService, baseNavigationService, listBootstraperService) {
               $scope.onInitEnd = function () {
                
               };

               listBootstraperService.init($scope, {
                   service: establecimientosService,
                   navigation: baseNavigationService,
                   columns: [
                       { field: 'descripcion', displayName: 'Descripción' },
                       { field: 'direccion', displayName: 'Dirección' },                       
                       { field: 'provinciaDescripcion', displayName: 'Provincia' },
                       { field: 'createDate', displayName: 'Fecha creación', width: 120 },
                       { field: 'createdBy', displayName: 'Usuario creación' },
                       { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a></div>' }
                   ]
               });
           }]);
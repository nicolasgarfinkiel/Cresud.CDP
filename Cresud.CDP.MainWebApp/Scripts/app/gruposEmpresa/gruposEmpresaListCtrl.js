angular.module('cresud.cdp.gruposEmpresa.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'gruposEmpresaService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, gruposEmpresaService, baseNavigationService, listBootstraperService) {
               $scope.onInitEnd = function () {                   
               };
              

               listBootstraperService.init($scope, {
                   service: gruposEmpresaService,
                   navigation: baseNavigationService,
                   columns: [
                       { field: 'descripcion', displayName: 'Descripción' },
                       { field: 'paisDescripcion', displayName: 'País' },                       
                       { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a></div>' }
                   ]
               });
           }]);
angular.module('cresud.cdp.cartasDePorte.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'cartasDePorteService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, cartasDePorteService, baseNavigationService, listBootstraperService) {
               $scope.onInitEnd = function () {
                   $scope.columns;
               };

               listBootstraperService.init($scope, {
                   service: cartasDePorteService,
                   navigation: baseNavigationService,
                   columns: [
                       { field: 'nombre', displayName: 'Nombre' },
                       { field: 'apellido', displayName: 'Apellido' },
                       { field: 'cuit', displayName: 'Cuit', width: 100 },
                       { field: 'esChoferTransportista ? "Si" : "No" ', displayName: 'Transportista', width: 110 },
                       { field: 'createDate', displayName: 'Fecha creación', width: 120 },
                       { field: 'createdBy', displayName: 'Usuario creación' },
                       { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a></div>' }
                   ]
               });
           }]);
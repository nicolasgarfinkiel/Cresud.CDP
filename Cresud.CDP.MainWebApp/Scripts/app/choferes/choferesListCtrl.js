angular.module('cresud.cdp.choferes.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'choferesService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, choferesService, baseNavigationService, listBootstraperService) {
               listBootstraperService.init($scope, {
                   service: choferesService,
                   navigation: baseNavigationService,
                   columns: [
                       { field: 'nombre', displayName: 'Nombre' },
                       { field: 'apellido', displayName: 'Apellido' },
                       { field: 'cuit', displayName: 'Cuit', width: 170 },
                       { field: 'camion', displayName: 'Camión' },
                       { field: 'acoplado', displayName: 'Acoplado' },
                       { field: 'cuit', displayName: 'Acciones', width: 170, cellTemplate: '<button type="button" class="btn btn-default" ng-click="edit(row.entity.id)" >Editar</button>' }
                   ]
               });
           }]);
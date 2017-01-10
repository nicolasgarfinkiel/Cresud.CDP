angular.module('cresud.cdp.empresas.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'empresasService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, empresasService, baseNavigationService, listBootstraperService) {
               $scope.onInitEnd = function () {
                
               };

               listBootstraperService.init($scope, {
                   service: empresasService,
                   navigation: baseNavigationService,
                   columns: [
                       { field: 'descripcion', displayName: 'Empresa' },
                       { field: 'grupoEmpresa.descripcion', displayName: 'Grupo' },                       
                       { field: 'grupoEmpresa.paisDescripcion', displayName: 'País' },
                       { field: 'idSapMoneda', displayName: 'Moneda', width: 80 },
                       { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a ng-if="!row.entity.solicitudesAsociadas" href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a><button ng-if="row.entity.solicitudesAsociadas" class="btn btn-rounded btn-xs btn-icon btn-default" data-toggle="tooltip" data-placement="top" title="No puede editar la empresa ya que posee solicitudes asociadas" ><i class="fa fa-warning"></i></button></div>' }
                   ]
               });
           }]);
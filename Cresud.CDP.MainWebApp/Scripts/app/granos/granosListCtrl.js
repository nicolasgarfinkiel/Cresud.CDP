angular.module('cresud.cdp.granos.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'granosService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, granosService, baseNavigationService, listBootstraperService) {
               $scope.onInitEnd = function () {
                   $scope.esGrupoCresud = $scope.usuario.currentEmpresa.grupoEmpresa.id == 1;

                   if ($scope.esGrupoCresud) {
                       $scope.columns[1].visible = false;
                       $scope.columns[2].visible = false;
                       $scope.columns[3].visible = false;                       
                   }
               };

               listBootstraperService.init($scope, {
                   service: granosService,
                   navigation: baseNavigationService,
                   columns: [
                       { field: 'descripcion', displayName: 'Descripción' },
                       { field: 'especieAfipDescripcion', displayName: 'Especie' },
                       { field: 'cosechaAfipDescripcion', displayName: 'Cosecha' },
                       { field: 'tipoGranoAfipDescripcion', displayName: 'Tipo' },
                       { field: 'sujetoALote', displayName: 'Sujeto a Lote' },
                       { field: 'createDate', displayName: 'Fecha creación', width: 120 },
                       { field: 'createdBy', displayName: 'Usuario creación' },
                       { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a></div>' }
                   ]
               });                                                         
           }]);
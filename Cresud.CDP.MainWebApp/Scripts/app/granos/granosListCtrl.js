angular.module('cresud.cdp.granos.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           'granosService',
           'baseNavigationService',
           'listBootstraperService',
           function ($scope, granosService, baseNavigationService, listBootstraperService) {
               $scope.onInitEnd = function () {
                   $scope.esGrupoCresud = $scope.usuario.currentEmpresa.grupoEmpresa.id == 1;

                   $scope.columns[1].visible = $scope.esGrupoCresud;
                   $scope.columns[2].visible = $scope.esGrupoCresud;
                   $scope.columns[3].visible = $scope.esGrupoCresud;
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
                       { field: 'cuit', displayName: 'Acciones', width: 80, cellTemplate: '<div class="ng-grid-icon-container"><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="edit(row.entity.id)"><i class="fa fa-pencil"></i></a><a href="javascript:void(0)" class="btn btn-rounded btn-xs btn-icon btn-default" ng-click="deleteEntityConfirm(row.entity.id)"><i class="fa fa-times"></i></a></div>' }
                   ]
               });

               $scope.deleteEntityConfirm = function (id) {
                   $scope.entityId = id;
                   $('#modalConfirm').modal('show');
               };

               $scope.deleteEntity = function () {
                   granosService.disableEntity($scope.entityId).then(function (response) {
                       $scope.resultModal = response.data.result;
                       if ($scope.resultModal.hasErrors) return;

                       $('#modalConfirm').modal('hide');
                       $scope.entityId = null;
                       $scope.search();
                   }, function () { throw 'Error on deleteEntity'; });
               };
           }]);
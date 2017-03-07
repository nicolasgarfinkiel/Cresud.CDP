angular.module('cresud.cdp.reservas.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           '$timeout',
           'reservasService',
           'establecimientosService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, $timeout, reservasService, establecimientosService, baseNavigationService, editBootstraperService) {
               $scope.loading = true;

               //#region base

               $scope.onInitEnd = function () {
                   $scope.operation = 'Nueva reserva';
                   $scope.filterEstablecimientos.empresaId = $scope.usuario.currentEmpresa.id;
                   
                   $timeout(function () {
                       $scope.loading = false;
                   }, 300);
               };

               editBootstraperService.init($scope, $routeParams,  {
                   service: reservasService,
                   navigation: baseNavigationService
               });
              
               $scope.isValid = function() {
                   $scope.result = { hasErrors: false, messages: [] };
                   
                   if (!$scope.entity.tipoDeCartaId) {
                        $scope.result.messages.push('Seleccione el tipo de carta');
                   }

                   if (!$scope.entity.establecimientoProcedenciaId) {
                       $scope.result.messages.push('Seleccione el establecimiento');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };

               //#endregion

               //#region Select UI

               $scope.selectList = [];
               $scope.currentPage = 0;
               $scope.pageCount = 0;
               $scope.filterEstablecimientos = { origen: true, pageSize: 20 };

               $scope.getSelectSource = function ($select, $event) {
                   if ($scope.loading) return;
                   
                   if (!$event) {
                       $scope.currentPage = 1;
                       $scope.pageCount = 0;
                       $scope.selectList = [];
                   } else {
                       $event.stopPropagation();
                       $event.preventDefault();
                       $scope.currentPage++;
                   }

                   $scope.filterEstablecimientos.currentPage = $scope.currentPage;
                   $scope.filterEstablecimientos.multiColumnSearchText = $select.search;

                   establecimientosService.getByFilter($scope.filterEstablecimientos).then(function (response) {
                       $scope.selectList = $scope.selectList.concat(response.data.data);
                       $scope.pageCount = Math.ceil(response.data.count / 20);
                   }, function () { throw 'Error on getByFilter'; });
               };

               $scope.$watch('entity.establecimientoProcedencia', function (newValue, oldValue) {
                   if ($scope.loading) return;
                  
                   $scope.entity.establecimientoProcedenciaId = newValue.id;
               });

               //#endregion         

           }]);
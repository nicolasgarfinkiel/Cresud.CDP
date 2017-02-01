angular.module('cresud.cdp.reportes.ctrl.actividad', [])
       .controller('actividadCtrl', [
           '$scope',
           'reportesService',
           'reportesNavigationService',       
           function ($scope, reportesService, reportesNavigationService) {
               $scope.filter = {};
              
               reportesService.getDataListCdp().then(function(response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;                                                                           
               });

               $scope.find = function() {
                   
               };

           }]);
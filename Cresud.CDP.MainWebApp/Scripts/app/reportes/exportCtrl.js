angular.module('cresud.cdp.reportes.ctrl.export', [])
       .controller('exportCtrl', [
           '$scope',
           'reportesService',
           'reportesNavigationService',
           function ($scope, reportesService, reportesNavigationService) {
               reportesService.getDataListExport().then(function(response) {
                   $scope.data = response.data.data;
               });
           }]);
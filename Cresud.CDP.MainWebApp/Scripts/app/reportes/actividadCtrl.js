angular.module('cresud.cdp.reportes.ctrl.actividad', [])
       .controller('actividadCtrl', [
           '$scope',
           'reportesService',
           'reportesNavigationService',
           function ($scope, reportesService, reportesNavigationService) {
               $scope.filter = {};
               $scope.values = [];

               reportesService.getDataListCdp().then(function (response) {
                   $scope.data = response.data.data;
                   $scope.filter.idGrupoEmpresa = $scope.data.usuario.currentEmpresa.grupoEmpresa.id;
                   $scope.filter.empresaId = $scope.data.usuario.currentEmpresa.id;                  
               });

               $scope.find = function () {
                   reportesService.getReporteActividad($scope.filter).then(function (response) {
                       $scope.values = [];
                       $scope.values.push(['Fecha', 'Aprobadas Afip', 'Aprobadas Sap']);

                       response.data.data.forEach(function(item) {
                           $scope.values.push([item.fecha, item.cantidadAfip, item.cantidadSap]);
                       });

                       if ($scope.values.length == 1) return;

                       google.charts.load('visualization', '1', { packages: ['corechart'] });
                       google.charts.setOnLoadCallback($scope.drawVisualization);
                   });
               };

               $scope.drawVisualization = function () {
                   var options = {
                       width: $('#visualization').width(),
                       height: $('#visualization').height(),
                       colors: ['#9db1c5', '#4cc0c1'],
                   };

                   var data = google.visualization.arrayToDataTable($scope.values);
                   var visualization = new google.visualization.BarChart(document.getElementById('visualization'));
                   visualization.draw(data, options);
               };

           }]);
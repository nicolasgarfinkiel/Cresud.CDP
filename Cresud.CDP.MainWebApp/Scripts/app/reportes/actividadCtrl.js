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
                       $scope.values.push(['Fecha', 'CantidadAfip', 'CantidadSap']);

                       response.data.data.forEach(function(item) {
                           $scope.values.push([item.fecha, item.cantidadAfip, item.cantidadSap]);
                       });

                       google.charts.load('visualization', '1', { packages: ['corechart'] });
                       google.charts.setOnLoadCallback($scope.drawVisualization);
                   });
               };

               $scope.drawVisualization = function () {
                   var options = {
                       annotations: {
                           boxStyle: {
                               stroke: '#888',
                               strokeWidth: 1,
                               rx: 10,
                               ry: 10,
                               gradient: {
                                   color1: '#fbf6a7',
                                   color2: '#33b679',
                                   x1: '0%', y1: '0%',
                                   x2: '100%', y2: '100%',
                                   useObjectBoundingBoxUnits: true
                               }
                           }
                       }
                   };

                   var data = google.visualization.arrayToDataTable($scope.values);
                   var visualization = new google.visualization.BarChart(document.getElementById('visualization'));
                   visualization.draw(data, options);
               };

           }]);
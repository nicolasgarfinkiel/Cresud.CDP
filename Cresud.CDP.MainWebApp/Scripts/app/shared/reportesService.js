angular.module('cresud.cdp.service.reportes', [])
       .factory('reportesService', [
           '$http',
           function ($http ) {
               return {
                   getDataListExport: function () {
                       return $http({
                           method: 'POST',
                           url: '/Reportes/GetDataListExport'
                       });
                   },
               };
           }]);
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
                   getDataListCdp: function () {
                       return $http({
                           method: 'POST',
                           url: '/Reportes/GetDataListCdp'
                       });
                   },
                   getCdpsEmitidasByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/Reportes/GetCdpsEmitidasByFilter',
                           data: { filter: filter }
                       });
                   },
                   getCdpsRecibidasByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/Reportes/getCdpsRecibidasByFilter',
                           data: { filter: filter }
                       });
                   },
                   getReporteActividad: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/Reportes/GetReporteActividad',
                           data: { filter: filter }
                       });
                   },
               };
           }]);
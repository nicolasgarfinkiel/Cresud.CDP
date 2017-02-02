angular.module('cresud.cdp.service.bandejaDeSalida', [])
       .factory('bandejaDeSalidaService', [
           '$http',
           function ($http ) {
               return {                 
                   getSolicitadasByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/BandejaDeSalida/GetSolicitadasByFilter',
                           data: { filter: filter }
                       });
                   },                  
               };
           }]);
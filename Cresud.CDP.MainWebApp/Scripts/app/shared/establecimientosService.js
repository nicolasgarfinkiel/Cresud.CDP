angular.module('cresud.cdp.service.establecimientos', [])
       .factory('establecimientosService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({}, baseService);
               result.controller = 'Establecimientos';

               return result;
           }]);
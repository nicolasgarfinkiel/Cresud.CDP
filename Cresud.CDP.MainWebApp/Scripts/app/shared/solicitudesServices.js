angular.module('cresud.cdp.service.solicitudes', [])
       .factory('solicitudesService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({}, baseService);
               result.controller = 'Solicitudes';

               return result;
           }]);
angular.module('cresud.cdp.service.reservas', [])
       .factory('reservasService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({}, baseService);
               result.controller = 'Reservas';

               return result;
           }]);
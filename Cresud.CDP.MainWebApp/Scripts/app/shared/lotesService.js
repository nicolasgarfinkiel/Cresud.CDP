angular.module('cresud.cdp.service.lotes', [])
       .factory('lotesService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({}, baseService);
               result.controller = 'Lotes';

               return result;
           }]);
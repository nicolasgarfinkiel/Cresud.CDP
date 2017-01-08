angular.module('cresud.cdp.service.granos', [])
       .factory('granosService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({}, baseService);
               result.controller = 'Granos';

               return result;
           }]);
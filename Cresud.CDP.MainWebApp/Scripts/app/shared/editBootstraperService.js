angular.module('contabilidad.service.casos', ['cresud.cdp.service.base'])
       .factory('casosService', [
           '$http',
           'baseService',
           function ($http, baseService) {

               var result = angular.extend(baseService, {});
               result.controller = '';

               return result;
           }]);
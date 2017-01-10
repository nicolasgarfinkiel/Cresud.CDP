angular.module('cresud.cdp.service.gruposEmpresa', [])
       .factory('gruposEmpresaService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({}, baseService);
               result.controller = 'GruposEmpresa';

               return result;
           }]);
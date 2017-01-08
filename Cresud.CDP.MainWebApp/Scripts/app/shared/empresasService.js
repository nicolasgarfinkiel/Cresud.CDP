angular.module('cresud.cdp.service.empresas', [])
       .factory('empresasService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({}, baseService);
               result.controller = 'Empresas';

               return result;
           }]);
angular.module('cresud.cdp.service.cartasDePorte', [])
       .factory('cartasDePorteService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({}, baseService);
               result.controller = 'CartasDePorte';

               return result;
           }]);
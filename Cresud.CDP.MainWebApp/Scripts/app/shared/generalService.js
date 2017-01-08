angular.module('cresud.cdp.service.general', [])
       .factory('generalService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({
                   getClientesByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/General/GetClientesByFilter',
                           data: { filter: filter }
                       });
                   },
               }, baseService);
               result.controller = 'General';

               return result;
           }]);
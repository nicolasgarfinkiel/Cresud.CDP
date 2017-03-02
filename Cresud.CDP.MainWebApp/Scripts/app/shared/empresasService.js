angular.module('cresud.cdp.service.empresas', [])
       .factory('empresasService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({
                   getByClienteId: function (clienteId) {
                       return $http({
                           method: 'POST',
                           url: '/Empresas/GetByClienteId',
                           data: { clienteId: clienteId }
                       });
                   },
                   getBySapId: function (sapId) {
                       return $http({
                           method: 'POST',
                           url: '/Empresas/GetBySapId',
                           data: { clienteId: sapId }
                       });
                   },
               }, baseService);
               result.controller = 'Empresas';

               return result;
           }]);
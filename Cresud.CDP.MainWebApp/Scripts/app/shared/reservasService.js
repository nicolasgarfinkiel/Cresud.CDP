angular.module('cresud.cdp.service.reservas', [])
       .factory('reservasService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({
                   cancelar: function (solicitudId) {
                       return $http({
                           method: 'POST',
                           url: '/Reservas/Cancelar',
                           data: { solicitudId: solicitudId }
                       });
                   },
                   anular: function (solicitudId) {
                       return $http({
                           method: 'POST',
                           url: '/Reservas/Anular',
                           data: { solicitudId: solicitudId }
                       });
                   }
               }, baseService);
               result.controller = 'Reservas';

               return result;
           }]);
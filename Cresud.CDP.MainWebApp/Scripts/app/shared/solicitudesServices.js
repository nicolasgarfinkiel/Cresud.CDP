angular.module('cresud.cdp.service.solicitudes', [])
       .factory('solicitudesService', [
           '$http',
           'baseService',
           function ($http, baseService) {
               var result = angular.extend({
                   updateSimple: function (solicitud) {
                       return $http({
                           method: 'POST',
                           url: '/Solicitudes/UpdateSimple',
                           data: { solicitud: solicitud }
                       });
                   }
               }, baseService);

               result.controller = 'Solicitudes';

               return result;
           }]);
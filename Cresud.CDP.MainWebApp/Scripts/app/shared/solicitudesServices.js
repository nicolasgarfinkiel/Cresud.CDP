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
                   },
                   reenviarSap: function (id) {
                       return $http({
                           method: 'POST',
                           url: '/Solicitudes/ReenviarSap',
                           data: { id: id }
                       });
                   },
                   reenviarAfip: function (id) {
                       return $http({
                           method: 'POST',
                           url: '/Solicitudes/ReenviarAfip',
                           data: { id: id }
                       });
                   },
                   anular: function (id) {
                       return $http({
                           method: 'POST',
                           url: '/Solicitudes/Anular',
                           data: { id: id }
                       });
                   },
                   regresarOrigen: function (id) {
                       return $http({
                           method: 'POST',
                           url: '/Solicitudes/RegresarOrigen',
                           data: { id: id }
                       });
                   },
                   cambioDestinoDestinatario: function (solicitud) {
                       return $http({
                           method: 'POST',
                           url: '/Solicitudes/CambioDestinoDestinatario',
                           data: { solicitud: solicitud }
                       });
                   }
               }, baseService);

               result.controller = 'Solicitudes';

               return result;
           }]);
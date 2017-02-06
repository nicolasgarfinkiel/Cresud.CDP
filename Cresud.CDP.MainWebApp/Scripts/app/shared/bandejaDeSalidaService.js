﻿angular.module('cresud.cdp.service.bandejaDeSalida', [])
       .factory('bandejaDeSalidaService', [
           '$http',
           function ($http ) {
               return {
                   getDataListSolicitadas: function () {
                       return $http({
                           method: 'POST',
                           url: '/BandejaDeSalida/GetDataListSolicitadas'
                       });
                   },
                   getSolicitadasByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/BandejaDeSalida/GetSolicitadasByFilter',
                           data: { filter: filter }
                       });
                   },
                   getLogSapByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/BandejaDeSalida/GetLogSapByFilter',
                           data: { filter: filter }
                       });
                   },
                   getDataListConfirmacionArribo: function () {
                       return $http({
                           method: 'POST',
                           url: '/BandejaDeSalida/GetDataListConfirmacionArribo'
                       });
                   },
                   getConfirmacionesArriboByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/BandejaDeSalida/GetConfirmacionesArriboByFilter',
                           data: { filter: filter }
                       });
                   },
               };
           }]);
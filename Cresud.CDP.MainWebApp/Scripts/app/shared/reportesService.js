angular.module('cresud.cdp.service.reportes', [])
       .factory('reportesService', [
           '$http',
           function ($http ) {
               return {
                   getDataListInit: function () {
                       return $http({
                           method: 'POST',
                           url: '/' + this.controller + '/GetDataListInit'
                       });
                   },
               };
           }]);
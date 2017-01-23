angular.module('cresud.cdp.navigation.reportes', [])
       .factory('reportesNavigationService', [
          '$location',
           function ($location) {
               return {
                   goToExport: function () {
                       $location.path('/');
                   }                  
               };
           }]);
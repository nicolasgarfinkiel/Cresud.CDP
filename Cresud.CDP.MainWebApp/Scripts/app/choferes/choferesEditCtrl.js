angular.module('cresud.cdp.choferes.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           'choferesService',
           'baseNavigationService',
           'editBootstraperService',
           function ($scope, $routeParams, choferesService, baseNavigationService, editBootstraperService) {
               editBootstraperService.init($scope, $routeParams,  {
                   service: choferesService,
                   navigation: baseNavigationService
               });
           }]);
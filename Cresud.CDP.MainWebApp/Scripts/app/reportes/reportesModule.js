﻿angular.module('cresud.cdp.reportes', [
    'cresud.cdp.reportes.ctrl.export',
    'cresud.cdp.reportes.ctrl.emitidasRecibidas',
    'cresud.cdp.reportes.ctrl.actividad',
    'cresud.cdp.service.reportes',
    'cresud.cdp.service.base',
    'cresud.cdp.service.general',
    'cresud.cdp.service.choferes',
    'cresud.cdp.service.establecimientos',
    'cresud.cdp.navigation.reportes',         
    'ngRoute',
    'ngGrid',
    '$strap.directives',
    'cresud.cdp.directive.loading',    
    'cresud.cdp.directive.debounce',
    'ui.select',
    'ngSanitize'
]).config([
    '$routeProvider',
    '$locationProvider',
    "$httpProvider",
    function ($routeProvider, $locationProvider, $httpProvider) {

        $routeProvider.when('/', {
            templateUrl: 'reportes/export',
            controller: 'exportCtrl'
        });

        $routeProvider.when('/emitidas', {
            templateUrl: 'reportes/emitidasRecibidas',
            controller: 'emitidasRecibidasCtrl'
        });

        $routeProvider.when('/recibidas', {
            templateUrl: 'reportes/emitidasRecibidas',
            controller: 'emitidasRecibidasCtrl'
        });

        $routeProvider.when('/actividad', {
            templateUrl: 'reportes/actividad',
            controller: 'actividadCtrl'
        });

        $routeProvider.otherwise({
            redirectTo: '/'
        });

        $httpProvider.interceptors.push(function ($q, $rootScope) {
            if ($rootScope.activeCalls == undefined) {
                $rootScope.activeCalls = 0;
            }

            return {
                request: function (config) {
                    $rootScope.activeCalls += 1;
                    return config;
                },
                requestError: function (rejection) {
                    $rootScope.activeCalls -= 1;
                    return rejection;
                },
                response: function (response) {
                    $rootScope.activeCalls -= 1;
                    return response;
                },
                responseError: function (rejection) {
                    $rootScope.activeCalls -= 1;
                    return rejection;
                }
            };
        });
               

        var regexIso8601 = /^\d{4}(-\d\d(-\d\d(T\d\d:\d\d(:\d\d)?(\.\d+)?(([+-]\d\d:\d\d)|Z)?)?)?)?$/i;
        
        $httpProvider.defaults.transformResponse.push(function (responseData) {
            convertDateStringsToDates(responseData);
            return responseData;
        });
        
        function convertDateStringsToDates(input) {
            // Ignore things that aren't objects.
            if (typeof input !== "object") return input;
                       

            for (var key in input) {
                if (!input.hasOwnProperty(key)) continue;

                var value = input[key];
                var match;                
                if (typeof value === "string" && value.length > 5 && (match = value.match(regexIso8601))) {
                    input[key] = moment(value).format('DD/MM/YYYY');
                } else if (typeof value === "object") {
                    // Recurse into object
                    convertDateStringsToDates(value);
                }
            }
        };

    }
]).run(['$rootScope', function ($rootScope) {
    $rootScope.$on('$routeChangeSuccess', function () {
        setTimeout(function() {            
        }, 200);
    });
}]);

      
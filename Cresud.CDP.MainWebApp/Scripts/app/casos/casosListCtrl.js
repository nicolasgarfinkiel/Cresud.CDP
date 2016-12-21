angular.module('contabilidad.casos.ctrl.list', [])
       .controller('listCtrl', [
           '$scope',
           '$rootScope',
           'casosService',
           'navigationService',
           function ($scope, $rootScope, casosService, navigationService) {
               $rootScope.navigationService = navigationService;
               $rootScope.showCreateButton = true;
               $scope.casosToDelete = [];

               $rootScope.setCurrentUser = function () {                   
                   $rootScope.filter.currentUser = true;
                   $rootScope.filter.user = null;
                   $rootScope.filter.categoria = null;
                   $rootScope.filter.tema = null;
                   $rootScope.filter.multicolumnSearchText = null;
                   $rootScope.filter.status = null;

                   if ($scope.find) {
                       $scope.find();
                   }                   
               };

               $rootScope.clearFilter = function () {                   
                   $rootScope.filter.currentUser = false;
                   $rootScope.filter.user = null;
                   $rootScope.filter.categoria = null;
                   $rootScope.filter.tema = null;
                   $rootScope.filter.multicolumnSearchText = null;
                   $rootScope.filter.status = null;
                   
                   if ($scope.find) {
                       $scope.find();
                   }                   
               };

               $rootScope.setCategoria = function (categoria) {                   
                   $rootScope.filter.categoria = categoria;
                   $rootScope.filter.multicolumnSearchText = null;
                   $rootScope.filter.status = null;

                   if ($scope.find) {
                       $scope.find();
                   }
               };

               $rootScope.setStatus = function (status) {
                   $rootScope.filter.status = status;
                   $rootScope.filter.multicolumnSearchText = null;

                   if ($scope.find) {
                       $scope.find();
                   }
               };

               $rootScope.setTema = function (tema) {
                   $rootScope.filter.tema = tema;
                   $rootScope.filter.multicolumnSearchText = null;
                   $rootScope.filter.status = null;

                   if ($scope.find) {
                       $scope.find();
                   }
               };

               $rootScope.setUsuario = function (user) {
                   $rootScope.filter.user = user;
                   $rootScope.filter.multicolumnSearchText = null;
                   $rootScope.filter.currentUser = false;
                   $rootScope.filter.categoria = null;
                   $rootScope.filter.tema = null;
                   $rootScope.filter.status = null;

                   if ($scope.find) {
                       $scope.find();
                   }
               };

               $scope.delete = function () {
                   if (!$scope.casosToDelete.length) return;
                   if (!confirm('Desea continuar con la operación?')) return;

                   casosService.deleteCasos($scope.casosToDelete).then(function (response) {
                       $scope.find();
                   }, function () { throw 'Error on deleteCasos'; });
               };

               $scope.find = function () {
                   $scope.casosToDelete = [];
                   $scope.setFilterDatail();
                  
                   casosService.getCasosByFilter($rootScope.filter).then(function (response) {                       
                       $rootScope.casos = response.data.data;
                   }, function () { throw 'Error on getCasosByFilter'; });
               };

               $scope.toggleCheck = function (caso) {
                   if ($scope.casosToDelete.indexOf(caso.id) === -1) {
                       $scope.casosToDelete.push(caso.id);
                   } else {
                       $scope.casosToDelete.splice($scope.casosToDelete.indexOf(caso.id), 1);
                   }
               };             

               $scope.setFilterDatail = function() {
                   $rootScope.filterDetail = [];

                   if ($rootScope.filter.currentUser) {
                       $rootScope.filterDetail.push('Asignados a mi');
                   }

                   if ($rootScope.filter.user) {
                       $rootScope.filterDetail.push($rootScope.filter.user);
                   }

                   if ($rootScope.filter.categoria) {
                       $rootScope.filterDetail.push($rootScope.filter.categoria);
                   }

                   if ($rootScope.filter.tema) {
                       $rootScope.filterDetail.push($rootScope.filter.tema);
                   }

                   if ($rootScope.filter.status) {
                       $rootScope.filterDetail.push($rootScope.filter.status);
                   }
                   
               };

               $rootScope.$watch('filter.multicolumnSearchText', function (newValue, oldValue) {
                   if (!$rootScope.loaded || !$scope.find ) return;

                   $scope.find();
               });

               if (!$rootScope.loaded) {
                   casosService.getDataInit().then(function (response) {
                       $rootScope.categorias = response.data.data.categorias;
                       $rootScope.prioridades = response.data.data.prioridades;
                       $rootScope.statusList = response.data.data.statusList;
                       $rootScope.temas = response.data.data.temas;
                       $rootScope.usuarios = response.data.data.usuarios;                       
                       $rootScope.loaded = true;
                       $rootScope.filter = { currentUser: true, empresaId: location.search.split("=")[1], multicolumnSearchText: null };
                       $rootScope.filterDetail = [];
                       $rootScope.casos = [];                       
                   }, function () { throw 'Error on getDataInit'; });
               }
           }]);
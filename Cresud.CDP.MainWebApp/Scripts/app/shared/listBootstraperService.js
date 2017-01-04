angular.module('cresud.cdp.service.bootstraper.list', [])
       .factory('listBootstraperService', [
           function () {
               var scope = {};
               var data = {};

               var bootstrapper = {
                   list: [],
                   columns: [],
                   filter: {},
                   result: { data: null, hasErrors: false, messages: [] },
                   create: function () {
                       data.navigation.goToCreate();
                   },
                   edit: function (id) {
                       data.navigation.goToEdit(id);
                   },
                   totalItems: 0,
                   gridOptions: {
                       data: 'list',
                       columnDefs: 'columns',
                       showFooter: true,
                       useExternalSorting: true,
                       useExternalPagination: true,
                       enablePaging: true,
                       enableRowSelection: false,
                       totalServerItems: 'totalItems',
                       pagingOptions: {
                           pageSizes: [10],
                           currentPage: 1
                       }
                   },
                   search: function () {

                       data.service.getByFilter(scope.filter)
                           .then(function (response) {
                               scope.list = response.data.list;
                               scope.totalItems = response.data.count;
                           }, function () { throw 'Error on getByFilter'; });
                   },
                   init: function () {
                       scope.columns = data.columns;

                       scope.$watch('gridOptions.pagingOptions', function (newVal, oldVal) {
                           if (newVal == oldVal || newVal.currentPage == oldVal.currentPage) return;
                           scope.search();
                       }, true);
                   }
               };

               return {
                   init: function (s, d) {
                       scope = s;
                       data = d;

                       for (var prop in bootstrapper) {
                           scope[prop] = scope[prop] || bootstrapper[prop];
                       }

                       scope.init();
                   }
               };
           }]);
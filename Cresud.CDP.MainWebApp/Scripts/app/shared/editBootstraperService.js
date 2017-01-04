angular.module('cresud.cdp.service.bootstraper.edit',
    [        
    ]).factory('editBootstraperService', [
           function () {
               var scope = {};
               var data = {};
               var $routeParams = {};

               var bootstrapper = {
                   entity: {},
                   operation: null,
                   result: { hasErrors: false, messages: [] },                   
                   list: function () {
                       data.navigation.goToList();
                   },
                   isValid: function() {
                       return true;
                   },
                   save: function () {
                       if (!scope.isValid()) return;

                       if (scope.entity.id) {
                           data.service.updateEntity(scope.entity).then(function (response) {
                               if (!response.data.hasErrors)
                                   data.navigation.goToList();

                               scope.result.hasErrors = true;
                               scope.result.messages = response.data.messages;

                           }, function () { throw 'Error on update'; });
                       } else {
                           data.service.createEntity(scope.entity).then(function (response) {
                               if (!response.data.hasErrors)
                                   data.navigation.goToList();

                               scope.result.hasErrors = true;
                               scope.result.messages = response.data.messages;

                           }, function () { throw 'Error on create'; });
                       }
                   },

                   //#region Init
                   getDataEditInit: function (entity) {
                       data.service.getDataEditInit().then(function (response) {
                           scope.result.hasErrors = response.data.hasErrors;
                           scope.result.messages = response.data.messages;
                           scope.entity = entity;
                       }, function () { throw 'Error on getDataEditInit'; });
                   },

                   init: function () {
                       scope.entity = data.entity;                       

                       if (angular.isUndefined($routeParams.id)) {
                           scope.getDataEditInit(this.entityInit);
                           scope.operation = 'Alta';
                       } else {
                           scope.operation = 'Edición';
                           data.service.getById($routeParams.id).then(function (response) {
                               scope.getDataEditInit(response.data);
                           }, function () { throw 'Error on get'; });
                       }

                       data.validation.setValidator(scope.save);
                   }
               };

               return {
                   init: function (s, rp, d) {
                       scope = s;
                       data = d;
                       $routeParams = rp;

                       for (var prop in bootstrapper) {
                           scope[prop] = scope[prop] || bootstrapper[prop];
                       }

                       scope.init();
                   }
               };
           }]);
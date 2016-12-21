angular.module('contabilidad.casos.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$rootScope',
           '$routeParams',
           'casosService',
           'navigationService',
           'markDownImageService',
           function ($scope, $rootScope, $routeParams, casosService, navigationService, markDownImageService) {
               $rootScope.navigationService = navigationService;
               $rootScope.showCreateButton = false;
               $scope.caso = { empresaId: location.search.split("=")[1], descripcionFormatted: null };
               $scope.result = { hasErrors: false, messages: [] };               
               $scope.markDownImageService = markDownImageService;
               $scope.casoEdit = {};

               $scope.save = function() {
                   if (!$scope.isValid()) return;

                   if (!$scope.caso.id) {
                       casosService.createCaso($scope.caso).then(function (response) {
                           if (!response.data.result.hasErrors)
                               navigationService.goToList();

                           $scope.result.hasErrors = true;
                           $scope.result.messages = response.data.result.messages;
                       }, function () { throw 'Error on createCaso'; });
                   } else {
                       casosService.updateCaso($scope.casoEdit).then(function (response) {
                           if (!response.data.result.hasErrors) {
                               $scope.caso = response.data.data;
                               $('#editModal').modal('hide');
                               return;
                           }
                               
                           $scope.result.hasErrors = true;
                           $scope.result.messages = response.data.result.messages;
                       }, function () { throw 'Error on updateCaso'; });
                   }                  
               };

               $scope.cerrar = function () {
                   var newStatus = 'Cerrado';

                   casosService.changeSatus($scope.caso.id, newStatus).then(function (response) {
                       if (!response.data.result.hasErrors) {
                           $scope.caso.status = newStatus;
                           return;
                       }

                       $scope.result.hasErrors = true;
                       $scope.result.messages = response.data.result.messages;
                   }, function () { throw 'Error on changeSatus'; });
               };

               $scope.comentar = function () {
                   if (!$scope.isValidComentario()) return;

                   var comentario = { casoId: $scope.caso.id, comentario: $scope.comentario, comentarioFormatted: $scope.comentarioFormatted };
                   casosService.createComentario(comentario).then(function (response) {
                       if (!response.data.result.hasErrors) {
                           response.data.data.createDate = moment(response.data.data.createDate).format('DD/MM/YYYY hh:mm:ss');
                           $scope.caso.comentarios.push(response.data.data);
                           
                           $scope.comentario = null;
                           $scope.comentarioFormatted = null;
                           return;
                       }

                       $scope.result.hasErrors = true;
                       $scope.result.messages = response.data.result.messages;
                   }, function () { throw 'Error on comentar'; });
               };

               $scope.changeStatus = function (status) {
                   casosService.changeStatus($scope.caso.id, status).then(function (response) {
                       $scope.caso = response.data.data;

                       switch (status) {
                           case "Abierto":
                               $scope.caso.cssClass = "label-info";
                               break;
                           case "Cerrado":
                               $scope.caso.cssClass = "label-danger";
                               break;
                       }

                   }, function () { throw 'Error on changeStatus'; });
               };

               $scope.setEdit = function () {
                   $scope.casoEdit = angular.copy($scope.caso);
                   $('#editModal').modal('show');
               };

               $scope.isValid = function () {
                   $scope.result.hasErrors = false;
                   $scope.result.messages = [];
                   var caso = $scope.caso.id ? $scope.casoEdit : $scope.caso;

                   if (!caso.titulo) {
                       $scope.result.messages.push('Ingrese el título');
                   }

                   if (!caso.usuarioAsignadoNick) {
                       $scope.result.messages.push('Seleccione el usuario');
                   }

                   if (!caso.categoria) {
                       $scope.result.messages.push('Seleccione la categoría');
                   }

                   if (!caso.tema) {
                       $scope.result.messages.push('Seleccione el tema');
                   }

                   if (!caso.prioridad) {
                       $scope.result.messages.push('Seleccione la prioridad');
                   }

                   if (!caso.fechaLimite) {
                       $scope.result.messages.push('Seleccione la fecha límite');
                   }

                   if (!caso.descripcion && caso.id) {
                       $scope.result.messages.push('Ingrese el comentario');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;

                   return !$scope.result.hasErrors;
               };

               $scope.isValidComentario = function () {
                   $scope.result.hasErrors = false;
                   $scope.result.messages = [];
                  
                   if (!$scope.comentario) {
                       $scope.result.messages.push('Ingrese el comentario');
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;

                   return !$scope.result.hasErrors;
               };

               //#region Image Markdown
               $scope.pendingFile = '[---Loading file {0}---]';

               $scope.onErrorImageCallBack = function (message, id) {
                   var pending = $scope.pendingFile.replace('{0}', id);

                   $scope.$apply(function () {
                       if (angular.isUndefined($routeParams.id)) {
                           $scope.caso.descripcion = $scope.caso.descripcion.replace(pending, "");
                           $scope.pendingImages = $scope.caso.descripcion.indexOf('[---Loading file') >= 0;
                       } else {
                           $scope.comentario = $scope.comentario.replace(pending, "");
                           $scope.pendingImages = $scope.comentario.indexOf('[---Loading file') >= 0;
                       }

                       $scope.result.hasErrors = true;
                       $scope.result.messages = [message || 'Se produjo un error al intentar subir la imágen'];
                   });
               };

               $scope.onSuccessFileCallBack = function (imageUrl, id) {
                   var pending = $scope.pendingFile.replace('{0}', id);
                   var markDownImage = '![image](' + imageUrl + ')';

                   $scope.$apply(function () {
                       $scope.result.hasErrors = false;
                       
                       if (angular.isUndefined($routeParams.id)) {
                           $scope.caso.descripcion = $scope.caso.descripcion.replace(pending, markDownImage);
                           $scope.pendingImages = $scope.caso.descripcion.indexOf('[---Loading file') >= 0;
                       } else {
                           $scope.comentario = $scope.comentario.replace(pending, markDownImage);
                           $scope.pendingImages = $scope.comentario.indexOf('[---Loading file') >= 0;
                       }
                   });
               };

               $scope.onUploadingCallBack = function (id) {
                   var pending = $scope.pendingFile.replace('{0}', id);
                   $scope.pendingImages = true;

                   $scope.$apply(function () {
                       $scope.result.hasErrors = false;

                       if (angular.isUndefined($routeParams.id)) {
                           $scope.caso.descripcion = $scope.caso.descripcion || '';
                           $scope.caso.descripcion += '\n' + pending;
                       } else {
                           $scope.comentario = $scope.comentario || '';
                           $scope.comentario += '\n' + pending;
                       }
                   });
               };

               markDownImageService.setUploader({
                   onError: $scope.onErrorImageCallBack,
                   onSuccess: $scope.onSuccessFileCallBack,
                   onUploading: $scope.onUploadingCallBack,
                   controlPasteId: 'comentario',
                   urlFromClipboard: 'Casos/Casos/UploadImage'
               });

               //#endregion

               //#region Link Markdown
               $scope.sharp = '#';               
               $scope.sharpIndex = null;
               $scope.references = [];

               $scope.setComentarioFormatted = function (comentario, target) {               
                   if (!target) return;

                   if (!comentario) {
                       target(null);
                       return;                       
                   }

                   var references = [];
                   var index = -1;

                   while (true) {
                       index = comentario.indexOf($scope.sharp, ++index);
                       if (index == -1) break;
                                                                     
                       var reference = comentario.substring(index + 1).match(/^prestamo[0-9]+|relacionado[0-9]+|solicitud[0-9]+$/);

                       if (!reference || reference.length != 1) break;

                       var tag = reference[0].match(/^prestamo|relacionado|solicitud|$/)[0];
                       var referenceId = reference[0].match(/[0-9]+/)[0];

                       references.push({ index: index, tag: tag, referenceId: referenceId });                       
                   }

                   var result = comentario;

                   if (!references.length) {
                       target(result);
                       return;
                   };
                   
                   casosService.checkReferences(references).then(function(response) {
                       response.data.data.forEach(function(r) {
                           var link = '<a class="' + r.tag + '|' + r.referenceId + '">#' + r.tag + r.referenceId + '</a>';
                           var regEx = new RegExp('#' + r.tag + r.referenceId, 'g');

                           result = result.replace(regEx, link);
                           target(result);
                       });
                   }, function () { console.log('Error on checkReferences'); });
               };

               $scope.setDescriptionFormatted = function(value) {
                   $scope.caso.descripcionFormatted = value;
               };

               $scope.setNewComentarioFormatted = function (value) {
                   $scope.comentarioFormatted = value;
               };

               $scope.setReferenceFilter = function (comentario, selectionStart) {
                   if ($scope.referenceSetted) {
                       $scope.referenceSetted = false;
                       return;
                   }

                   $scope.references = [];

                   if (!comentario) {
                       $scope.hideReferenceFilter();
                       return;
                   }

                   $scope.sharpIndex = comentario.substr(0, selectionStart).lastIndexOf($scope.sharp);

                   if ($scope.sharpIndex == -1) {
                       $scope.hideReferenceFilter();
                       return;
                   }
                   
                   var reference = comentario.substr($scope.sharpIndex + 1, selectionStart - $scope.sharpIndex).match(/^prestamo[0-9]*$|^relacionado[0-9]*$|^solicitud[0-9]*$/);

                   if (!reference || reference.length != 1) {                     
                       $scope.hideReferenceFilter();
                       return;
                   };
                   
                   var tag = reference[0].match(/^prestamo|relacionado|solicitud|$/)[0];
                   var referenceId = reference[0].match(/[0-9]+/);
                   referenceId = referenceId ? referenceId[0] : null;
                   var referenceFilter = { tag: tag, referenceId: referenceId, empresaId: $scope.caso.empresaId};
                   var pos = $('textarea').getCaretPosition();                   
                   var left = pos.left + 50;
                   $("#referenceFilter").css({ top: pos.top, left: left });

                   casosService.getReferencesByFilter(referenceFilter).then(function (response) {
                       $scope.references = response.data.data;
                       $("#referenceFilter").show();
                   }, function () { console.log('Error on getReferencesByFilter'); });
                 
               };

               $scope.setReference = function (entity, comentario, target) {
                   var selectionStart = document.getElementById("comentario").selectionStart;
                   var reference = comentario.substr($scope.sharpIndex + 1, selectionStart - $scope.sharpIndex).match(/^prestamo[0-9]*$|^relacionado[0-9]*$|^solicitud[0-9]*$/);
                   var tag = reference[0].match(/^prestamo|relacionado|solicitud|$/)[0];
                   var referenceId = reference[0].match(/[0-9]+/);
                   referenceId = referenceId ? referenceId[0] : '';

                   var newComentario = comentario.substr(0, $scope.sharpIndex) + '#' + tag + entity.id + comentario.substr($scope.sharpIndex + 1 + reference[0].length, comentario.length);

                   if ($scope.caso.id) {
                       $scope.comentario = newComentario;
                   } else {
                       $scope.caso.descripcion = newComentario;
                   }

                   $scope.referenceSetted = true;
                   $scope.setComentarioFormatted(newComentario, target);
                   $scope.hideReferenceFilter();
               };

               $scope.hideReferenceFilter = function () {                   
                   $("#referenceFilter").hide();
               };

               $scope.$watch('caso.descripcion', function (newValue, oldValue) {
                   var selectionStart = document.getElementById("comentario").selectionStart;
                   $scope.setReferenceFilter(newValue, selectionStart-1);
               });

               $scope.$watch('comentario', function (newValue, oldValue) {
                   var selectionStart = document.getElementById("comentario").selectionStart;
                   $scope.setReferenceFilter(newValue, selectionStart - 1);
               });

               //#endregion

               //#region ReferenceDetail

               $('body').on('click', '.markdown a', function (e) {
                   e.preventDefault(e);                   
                   var element = e.currentTarget;

                   var parentOffset = $(".chat-discussion").offset();
                   var top = $(element).offset().top - parentOffset.top + 10;
                   var left = $(element).offset().left - parentOffset.left + 100;

                   $("#referenceDetail").css({ top: top, left: left });
                   var detail = $(element).attr('class').split('|');
                   var reference = { referenceId: detail[1], tag: detail[0] };

                   casosService.getReferenceDetail(reference).then(function (response) {
                       $scope.referenceDetail = response.data.data;
                       $("#referenceDetail").show();
                   }, function () { console.log('Error on checkReferences'); });                   
               });            

               $scope.hideReferenceDetail = function () {
                   $("#referenceDetail").hide();
               };

               //#endregion

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

               if (!angular.isUndefined($routeParams.id)) {                  
                   casosService.getCaso($routeParams.id).then(function (response) {
                       $scope.caso = response.data.data;                       
                   }, function () { throw 'Error on getCaso'; });
               }
              
           }]);
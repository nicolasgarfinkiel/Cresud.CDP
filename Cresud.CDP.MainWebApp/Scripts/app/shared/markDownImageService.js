angular.module('contabilidad.service.markDownImage', [])
       .factory('markDownImageService', [
           '$http',
           function ($http) {
               var pending = 0;

               return {
                   hasPending: function() {
                       return pending;
                   },
                   setUploader: function (data) {                       
                       document.getElementById(data.controlPasteId).onpaste = function (event) {
                           var items = (event.clipboardData || event.originalEvent.clipboardData).items;
                           var blob = null;

                           for (var i = 0; i < items.length; i++) {
                               if (items[i].type.indexOf("image") === 0) {
                                   blob = items[i].getAsFile();
                               }
                           }

                           if (blob == null) {                               
                               return;
                           }

                           var reader = new FileReader();
                           //reader.onload = function (event) {                                   
                           //    document.getElementById(data.imageDest).src = event.target.result;
                           //};
                           reader.readAsDataURL(blob);
                           var xhr = new XMLHttpRequest();

                           xhr.onload = function (r) {                               
                               if (xhr.status != 200) {
                                   r.onError('Se produjo una excepción al tratar de subir la imágen', pending);
                                   return;
                               }

                               var response = JSON.parse(r.currentTarget.response);

                               if (response.result.hasErrors) {
                                   data.onError(response.result.messages[0], pending);
                               } else {
                                   data.onSuccess(response.data, pending);
                               }

                               pending--;
                           };

                           xhr.onerror = function () {
                               pending--;
                               data.onError('Se produjo una excepción al tratar de subir la imágen');
                           };

                           xhr.open("POST", data.urlFromClipboard, true);
                           xhr.setRequestHeader("Content-Type", blob.type);
                           pending++;
                           data.onUploading(pending);
                           xhr.send(blob);
                       };
                   }
               };
           }]);
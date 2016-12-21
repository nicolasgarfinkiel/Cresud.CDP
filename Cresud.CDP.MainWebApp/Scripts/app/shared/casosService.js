angular.module('contabilidad.service.casos', [])
       .factory('casosService', [
           '$http',           
           function ($http) {
               return {                                      
                   getDataInit: function () {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/GetDataInit'
                       });                       
                   },
                   getCasosByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/GetCasosByFilter',
                           data: { filter: filter }
                       });
                   },
                   getCaso: function (id) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/GetCaso',
                           data: { id: id }
                       });
                   },
                   createCaso: function (caso) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/CreateCaso',
                           data: { caso: caso }
                       });
                   },
                   updateCaso: function (caso) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/updateCaso',
                           data: { caso: caso }
                       });
                   },
                   createComentario: function (comentario) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/CreateComentario',
                           data: { comentario: comentario }
                       });
                   },
                   changeStatus: function (casoId, status) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/ChangeStatus',
                           data: { casoId: casoId, status: status }
                       });
                   },
                   checkReferences: function (references) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/CheckReferences',
                           data: { references: references }
                       });
                   },
                   getReferenceDetail: function (reference) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/GetReferenceDetail',
                           data: { reference: reference }
                       });
                   },
                   getReferencesByFilter: function (filter) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/GetReferencesByFilter',
                           data: { filter: filter }
                       });
                   },
                   deleteCasos: function (casos) {
                       return $http({
                           method: 'POST',
                           url: '/Casos/Casos/DeleteCasos',
                           data: { casos: casos }
                       });
                   },                 
               };                                                        
       }]);
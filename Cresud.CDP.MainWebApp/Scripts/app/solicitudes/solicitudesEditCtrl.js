angular.module('cresud.cdp.solicitudes.ctrl.edit', [])
       .controller('editCtrl', [
           '$scope',
           '$routeParams',
           '$timeout',
           'solicitudesService',
           'baseNavigationService',
           'editBootstraperService',
           'establecimientosService',
           'generalService',
           'choferesService',
           'empresasService',
           'granosService',           
           function ($scope, $routeParams, $timeout, solicitudesService, baseNavigationService, editBootstraperService, establecimientosService, generalService, choferesService, empresasService, granosService) {
               $scope.loading = true;
               $scope.resultAfip = { message: null, hasErros: false };
             
               //#region Base

               $scope.onInitEnd = function () {
                   $scope.empresaId = $scope.usuario.currentEmpresa.id;
                   $scope.esParaguay = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toUpperCase() == 'PARAGUAY';
                   $scope.esArgentina = $scope.usuario.currentEmpresa.grupoEmpresa.paisDescripcion.toUpperCase() == 'ARGENTINA';
                   $scope.esGrupoCresud = $scope.usuario.currentEmpresa.grupoEmpresa.id == 1;
                   $scope.guardarLabel = $scope.esGrupoCresud ? 'Guardar y enviar' : 'Guardar';
                   $scope.activarModelo =  false;
                   $scope.rolAltaSolicitud = $scope.usuario.currentEmpresa.roles.indexOf('Alta Solicitud') >= 0 ;
                   $scope.rolImprimirSolicitud = $scope.usuario.currentEmpresa.roles.indexOf('Imprimir Solicitud') >= 0;
                   $scope.rolConfirmarArribo = $scope.usuario.currentEmpresa.roles.indexOf('Confirmar Arribo') >= 0;
                   $scope.rolAnularSolicitud = $scope.usuario.currentEmpresa.roles.indexOf('Anular Solicitud') >= 0;

                   $scope.sources = {
                       'establecimientoOrigen': {
                           service: establecimientosService,
                           method: 'getByFilter',
                           filter: { empresaId: $scope.empresaId, origen: true, pageSize: 20, enabled: true },
                       },
                       'establecimientoDestino': {
                           service: establecimientosService,
                           method: 'getByFilter',
                           filter: { empresaId: $scope.empresaId, destino: true, pageSize: 20, enabled: true },
                       },
                       'cliente': {
                           service: generalService,
                           method: 'getClientesByFilter',
                           filter: { empresaId: $scope.empresaId, filterCresud: $scope.esGrupoCresud, pageSize: 20 },
                       },
                       'proveedor': {
                           service: generalService,
                           method: 'getProveedoresByFilter',
                           filter: { empresaId: $scope.empresaId, pageSize: 20 },
                       },
                       'chofer': {
                           service: choferesService,
                           method: 'getByFilter',
                           filter: { idGrupoEmpresa: $scope.usuario.currentEmpresa.grupoEmpresa.id, esChoferTransportista: false, pageSize: 20 },
                       },
                       'choferTransportista': {
                           service: choferesService,
                           method: 'getByFilter',
                           filter: { grupoEmpesaId: $scope.usuario.currentEmpresa.grupoEmpresa.id, esChoferTransportista: true, pageSize: 20 },
                       },
                       'grano': {
                           service: granosService,
                           method: 'getByFilter',
                           filter: { idGrupoEmpresa: $scope.usuario.currentEmpresa.grupoEmpresa.id, pageSize: 20 },
                       },
                   };

                   $timeout(function() {
                       $scope.loading = false;
                   }, 300);                   

                   $scope.setControls();
                   $scope.setDefaultValues();                   
               };

               $scope.onEntitySaved = function(entity) {
                   window.location = window.location.origin + '/solicitudes#/edit/' + entity.id;
               };

               editBootstraperService.init($scope, $routeParams, {
                   service: solicitudesService,
                   navigation: baseNavigationService
               });

               $scope.onlySave = function () {
                   $scope.entity.enviar = false;
                   if (!$scope.isValid()) return;

                   $scope.setEstados();
                   $scope.save();
               };

               $scope.saveAndSend = function () {
                   $scope.entity.enviar = true;
                   if (!$scope.isValid()) return;                   

                   $scope.setEstados();
                   $scope.save();
               };

               $scope.isValid = function () {
                   $scope.result = { hasErrors: false, messages: [] };

                   if (!$scope.entity.tipoDeCartaId) {
                       $scope.result.messages.push('Seleccione un tipo de carta');
                   }

                   if (!$scope.entity.proveedorTitularCartaDePorte) {
                       $scope.result.messages.push('Seleccione un titular de carta de porte');
                   }

                   if (!$scope.entity.grano) {
                       $scope.result.messages.push('Seleccione un grano');
                   }

                   if (!$scope.entity.establecimientoProcedencia) {
                       $scope.result.messages.push('Seleccione la procedencia de la mercadería');
                   }
                  
                   if ($scope.entity.cargaPesadaDestino && $scope.entity.kilogramosEstimados && isNaN($scope.entity.kilogramosEstimados)) {
                       $scope.result.messages.push('Ingrese un valor numérico para los kilogramos estimados');
                   }
                  
                   if (!$scope.entity.cargaPesadaDestino && $scope.entity.pesoBruto && isNaN($scope.entity.pesoBruto)) {
                       $scope.result.messages.push('El peso bruto debe ser numérico');
                   }
                 
                   if (!$scope.entity.cargaPesadaDestino && $scope.entity.pesoTara && isNaN($scope.entity.pesoTara)) {
                       $scope.result.messages.push('El peso tara debe ser numérico');
                   }
                                     
                   if ($scope.controlsVisibility.tarifaReferencia && $scope.entity.tarifaReferencia && isNaN($scope.entity.tarifaReferencia)) {
                       $scope.result.messages.push('La tarifa de referencia debe ser numérica');
                   }

                   if ($scope.entity.tarifaReal && isNaN($scope.entity.tarifaReal)) {
                       $scope.result.messages.push('La tarifa debe ser numérica');
                   }

                   if ($scope.entity.kmRecorridos && isNaN($scope.entity.kmRecorridos)) {
                       $scope.result.messages.push('Los km. a reccorer deben ser numérico');
                   }                 

                   var patenteOldRegex = /^[A-ZÑ]{3}\d{3}$/;
                   var patenteNewRegex = /^[A-ZÑ]{2}\d{3}[A-ZÑ]{2}$/;

                   if ($scope.esGrupoCresud &&                       
                       $scope.entity.patenteCamion &&
                       !($scope.entity.patenteCamion.match(patenteOldRegex) || $scope.entity.patenteCamion.match(patenteNewRegex))) {
                       $scope.result.messages.push('Formato de patente camión inválido. Formato corrercto ej: AAA111 o AA111AA');
                   }

                   if ($scope.esGrupoCresud &&
                       $scope.entity.patenteAcoplado &&
                       !($scope.entity.patenteAcoplado.match(patenteOldRegex) || $scope.entity.patenteAcoplado.match(patenteNewRegex))) {
                       $scope.result.messages.push('Formato de patente acomplado inválido. Formato corrercto ej: AAA111 o AA111AA');
                   }

                   if ($scope.entity.enviar) {

                       if ($scope.controlsVisibility.numeroCartaDePorte && !$scope.entity.numeroCartaDePorte) {
                           $scope.result.messages.push('Ingrese el número de carta de porte');
                       }

                       if (!$scope.entity.clienteDestinatario) {
                           $scope.result.messages.push('Seleccione un cliente destinatario');
                       }

                       if (!$scope.entity.clienteDestino) {
                           $scope.result.messages.push('Seleccione un cliente destino');
                       }

                       if (!$scope.entity.conformeCondicionalId) {
                           $scope.result.messages.push('Seleccione conforme o condicional');
                       }

                       if (!$scope.entity.estadoFlete) {
                           $scope.result.messages.push('Seleccione el estado del pago del flete');
                       }

                       if (!$scope.entity.establecimientoDestino) {
                           $scope.result.messages.push('Seleccione el destino de la mercadería');
                       }

                       if (!$scope.entity.clienteRemitenteComercial && (($scope.entity.tipoDeCartaId == 6 ||
                           ($scope.entity.tipoDeCartaId == 2 &&
                            $scope.entity.clienteDestino &&
                            $scope.entity.clienteDestinatario &&
                            $scope.entity.proveedorTitularCartaDePorte &&
                            $scope.entity.clienteDestinatario.cuit != $scope.entity.proveedorTitularCartaDePorte.cuit)))) {
                           $scope.result.messages.push('Seleccione un cliente remitente comercial');
                       }

                       if ($scope.entity.cargaPesadaDestino && !$scope.entity.kilogramosEstimados) {
                           $scope.result.messages.push('Ingrese los kilogramos estimados');
                       }


                       if (!$scope.entity.cargaPesadaDestino && !$scope.entity.pesoBruto) {
                           $scope.result.messages.push('Ingrese el peso bruto');
                       }

                       if (!$scope.entity.cargaPesadaDestino && !$scope.entity.pesoTara) {
                           $scope.result.messages.push('Ingrese el peso tara');
                       }

                       if ($scope.controlsVisibility.tarifaReferencia && !$scope.entity.tarifaReferencia) {
                           $scope.result.messages.push('Ingrese la tarifa de referencia enviada por AFIP');
                       }





                       if ($scope.entity.tipoDeCartaId == 4 || $scope.entity.tipoDeCartaId == 2 || $scope.entity.tipoDeCartaId == 7) {

                           if (!$scope.entity.clientePagadorDelFlete) {
                               $scope.result.messages.push('Seleccione el pagador del flete');
                           }
                         
                           if ($scope.entity.empresaClientePagadorFlete && !$scope.entity.patenteCamion) {
                               $scope.result.messages.push('Ingrese la patente del camión');
                           }

                           if ($scope.entity.empresaClientePagadorFlete && !$scope.entity.patenteAcoplado) {
                               $scope.result.messages.push('Ingrese la patente del acoplado');
                           }                          
                         
                       } else if ((($scope.entity.tipoDeCartaId == 4 || $scope.entity.tipoDeCartaId == 2 || $scope.entity.tipoDeCartaId == 7) && $scope.entity.empresaClientePagadorFlete) ||
                           ($scope.entity.tipoDeCartaId != 4 && $scope.entity.tipoDeCartaId != 2 && $scope.entity.tipoDeCartaId != 7)) {

                           if (!$scope.entity.proveedorTransportista) {
                               $scope.result.messages.push('Seleccione un proveedor transportista');
                           }

                           if (!$scope.entity.chofer) {
                               $scope.result.messages.push('Seleccione un chofer');
                           }

                           if (!$scope.entity.kmRecorridos) {
                               $scope.result.messages.push('Ingrese los km. a reccorer');
                           }

                           if ($scope.esGrupoCresud && !$scope.entity.cantHoras) {
                               $scope.result.messages.push('Ingrese la cantidad de horas');
                           }

                           if (!$scope.entity.tarifaReal) {
                               $scope.result.messages.push('Ingrese la tarifa');
                           }
                       }

                       //if ($scope.entity.tipoDeCartaId == 1 && !$scope.entity.empresaProveedorTitularCartaDePorte) {
                       //    $scope.result.messages.push('El proveedor titular de carta de porte debe ser una empresa');
                       //}

                       if (($scope.entity.tipoDeCartaId == 2 || $scope.entity.tipoDeCartaId == 7) && !$scope.entity.clienteRemitenteComercial) {
                           $scope.result.messages.push('Seleccione un cliente remitente comercial');
                       }

                       if (($scope.entity.tipoDeCartaId == 2 || $scope.entity.tipoDeCartaId == 7) && $scope.entity.clienteRemitenteComercial && !$scope.entity.empresaClienteRemitenteComercial) {
                           $scope.result.messages.push('El cliente remitente comercial debe ser una empresa');
                       }
                   }

                   $scope.result.hasErrors = $scope.result.messages.length;
                   return !$scope.result.hasErrors;
               };

               //#endregion   

               $scope.setEstados = function() {
                   if ($scope.entity.enviar) {
                       $scope.entity.observacionAfip = !$scope.esArgentina ? 'Solicitud SIN proceso AFIP' : null;

                       $scope.entity.estadoEnSAP =
                           !$scope.esArgentina ? 0 :
                           $scope.esProspecto() ? 9 :
                           $scope.entity.tipoDeCartaId == 4 ? 7 : 0;

                       $scope.entity.estadoEnAFIP = 2;
                   } else {
                       $scope.entity.observacionAfip = $scope.controlsVisibility.ctg ? 'Solicitud Manual' : 'Solicitud guardada.';                       

                       $scope.entity.estadoEnSAP =                           
                           $scope.esProspecto() ? 9 :
                           $scope.entity.tipoDeCartaId == 4 ? 7 : 0;

                       $scope.entity.estadoEnAFIP = $scope.controlsVisibility.ctg ? 4 : 2;
                   }                   
               };

               $scope.esProspecto = function() {
                   return ($scope.entity.clienteIntermediario != null && $scope.entity.clienteIntermediario.esProspecto) ||
                   ($scope.entity.clienteRemitenteComercial != null && $scope.entity.clienteRemitenteComercial.esProspecto) ||
                   ($scope.entity.clienteCorredor != null && $scope.entity.clienteCorredor.esProspecto) ||
                   ($scope.entity.clienteEntregador != null && $scope.entity.clienteEntregador.esProspecto) ||
                   ($scope.entity.clienteDestinatario != null && $scope.entity.clienteDestinatario.esProspecto) ||
                   ($scope.entity.clienteDestino != null && $scope.entity.clienteDestino.esProspecto) ||
                   ($scope.entity.clientePagadorDelFlete != null && $scope.entity.clientePagadorDelFlete.esProspecto) ||
                   ($scope.entity.clienteDestinatarioCambio != null && $scope.entity.clienteDestinatarioCambio.esProspecto) ||
                   ($scope.entity.proveedorTitularCartaDePorte != null && $scope.entity.proveedorTitularCartaDePorte.esProspecto) ||
                   ($scope.entity.proveedorTransportista != null && $scope.entity.proveedorTransportista.esProspecto);
               };

               $scope.setControls = function () {
                   $scope.mensajeAfipReserva = ($scope.resultAfip.message && $scope.resultAfip.message.indexOf('Reserva') >= 0) ||
                                               ($scope.entity.id && $scope.entity.observacionAfip && $scope.entity.observacionAfip.indexOf('Reserva') >= 0);

                   $scope.entity.manual =
                   $scope.manual = $scope.entity.tipoDeCartaId == 2 ||
                                   $scope.entity.tipoDeCartaId == 7 ||
                                   $scope.entity.tipoDeCartaId == 4;

                   $scope.controlsVisibility = {};
                   $scope.controlsVisibility.fechaDeEmision =
                   $scope.controlsVisibility.fechaDeVencimiento =
                   $scope.controlsVisibility.tarifaReferencia =
                   $scope.controlsVisibility.ctg = $scope.mensajeAfipReserva || ($scope.manual && !$scope.entity.id);

                   $scope.controlsVisibility.numeroCartaDePorte = $scope.controlsVisibility.cee = $scope.manual && !$scope.entity.id;
                   $scope.controlsVisibility.btnDesvio = $routeParams.btnDesvio && $scope.rolAltaSolicitud;
                   $scope.controlsVisibility.btnModelo = $scope.entity.id && !$scope.mensajeAfipReserva;
                   $scope.controlsVisibility.btnImprimir = $scope.entity.id && $scope.rolImprimirSolicitud;
                   

                   $scope.controlsVisibility.btnSoloGuardar = $scope.esGrupoCresud &&
                                                             (!$scope.entity.id ||
                                                               (($scope.mensajeAfipReserva && $scope.entity.createdBy == $scope.usuario.nombre) ||
                                                               (!$scope.mensajeAfipReserva && !$scope.activarModelo && !$scope.entity.ctg))
                                                             );

                   $scope.controlsVisibility.btnGuardar = !$scope.mensajeAfipReserva && 
                                                           $scope.rolAltaSolicitud &&
                                                           (
                                                              !$scope.entity.id ||
                                                              ($scope.entity.id && ((!$scope.activarModelo || $scope.entity.estadoEnAFIP == 2) && $scope.esGrupoCresud)) ||
                                                              (!$scope.esGrupoCresud && !$scope.entity.estadoEnSAP)
                                                           );


                   $scope.controlsVisibility.btnArribo = $scope.entity.id && !$scope.activarModelo && $scope.rolConfirmarArribo &&
                                                         ($scope.entity.estadoEnAFIP == 9 || 
                                                          ($scope.entity.estadoEnAFIP == 1 && $scope.entity.empresaDestino )
                                                         );


                   $scope.controlsVisibility.btnAnular = $scope.entity.id &&
                                                         (
                                                          (!$scope.esArgentina && $scope.entity.estadoEnSAP != 5) ||
                                                          ($scope.esArgentina && !$scope.mensajeAfipReserva && $scope.rolAnularSolicitud && $scope.entity.estadoEnAFIP == 1 && !$scope.activarModelo && $scope.entity.ctg && $scope.entity.numeroCartaDePorte)
                                                         );                   
               };

               $scope.setDefaultValues = function () {
                   if (!$scope.entity.id) {
                       $scope.entity.fechaDeEmision = moment().format('DD/MM/YYYY');
                       $scope.entity.fechaDeVencimiento = moment().format('DD/MM/YYYY');
                   } else if ($scope.mensajeAfipReserva) {
                       $scope.entity.fechaDeEmision = $scope.entity.createDate;
                   }
               };

               $scope.createModel = function () {
                   $scope.activarModelo = true;

                   $scope.entity.id = null;
                   $scope.entity.clientePagadorDelFlete = null;
                   $scope.entity.patenteCamion = null;
                   $scope.entity.patenteAcoplado = null;
                   $scope.entity.kmRecorridos = null;
                   $scope.entity.tarifaReferencia = null;
                   $scope.entity.tarifaReal = null;
                   $scope.entity.cantHoras = null;
                   $scope.entity.cargaPesadaDestino = null;
                   $scope.entity.kilogramosEstimados = null;
                   $scope.entity.pesoBruto = null;
                   $scope.entity.pesoTara = null;
                   $scope.entity.numeroContrato = null;
                   $scope.entity.observaciones = null;

                   $scope.entity.numeroCartaDePorte = null;
                   $scope.entity.ctg = null;
                   $scope.entity.chofer = null;
                   $scope.entity.estadoFlete = null;                   

                   $scope.setControls();
                   $scope.setDefaultValues();
                   $('#modalModelo').modal('show');
               };

               $scope.confirmArribo = function () {
                   $scope.resultModal = { hasErrors: false, messages: [] };
                   $scope.operation = 'Arribo';
                   $('#modalConfirm').modal('show');
               };

               $scope.confirmAnulacion = function () {
                   $scope.resultModal = { hasErrors: false, messages: [] };
                   $scope.operation = 'Anulacion';
                   $('#modalConfirm').modal('show');
               };

               $scope.modalConfirm = function() {
                   if ($scope.operation == 'Arribo') {
                       $scope.setArribo();
                   } else {
                       $scope.setAnulacion();
                   }
               };

               $scope.setArribo = function()
               {
                   $('#modalConfirm').modal('hide');
               };

               $scope.setAnulacion = function () {
                   solicitudesService.anular($scope.entity.id).then(function(response) {
                       $scope.resultModal = response.data;
                       if ($scope.resultModal.hasErrors) return;

                       $('#modalConfirm').modal('hide');
                       window.location = window.location.origin + '/solicitudes#/edit/' + $scope.entity.id;
                   });                   
               };

               //#region Clientes

               $scope.resultModal = { hasErrors: false, messages: [] };
               
               $scope.setCreateCliente = function (prop) {
                   $scope.prop = prop;
                   $scope.resultModal = { hasErrors: false, messages: [] };
                   $scope.cliente = {};
                   $('#clienteModal').modal('show');
               };

               $scope.createCliente = function () {
                   if (!$scope.isValidCliente()) return;

                   generalService.createCliente($scope.cliente).then(function (response) {
                       $scope.resultModal = response.data.result;                       
                       if ($scope.resultModal.hasErrors) return;

                       $('#clienteModal').modal('hide');
                       $scope.entity[$scope.prop] = response.data.data;
                   }, function () { throw 'Error on createCliente'; });                                      
               };

               $scope.isValidCliente = function () {
                   $scope.resultModal = { hasErrors: false, messages: [] };

                   if (!$scope.cliente.razonSocial) {
                       $scope.resultModal.messages.push('Ingrese la Razon Social o Nombre de Fantasia del cliente');
                   }

                   if (!$scope.cliente.cuit) {
                       $scope.resultModal.messages.push('Ingrese el ' + $scope.usuario.currentEmpresaLabelCuit);
                   }

                   if ($scope.esGrupoCresud && $scope.cliente.cuit && !$scope.isValidCuit($scope.cliente.cuit)) {
                       $scope.resultModal.messages.push($scope.usuario.currentEmpresaLabelCuit + ' inválido');
                   }

                   $scope.resultModal.hasErrors = $scope.resultModal.messages.length;
                   return !$scope.resultModal.hasErrors;
               };

               $scope.isValidCuit = function (cuit) {

                   if (cuit.length != 11) {
                       return false;
                   }

                   var acumulado = 0;
                   var digitos = cuit.split("");
                   var digito = digitos.pop();

                   for (var i = 0; i < digitos.length; i++) {
                       acumulado += digitos[9 - i] * (2 + (i % 6));
                   }

                   var verif = 11 - (acumulado % 11);
                   if (verif == 11) {
                       verif = 0;
                   } else if (verif == 10) {
                       verif = 9;
                   }

                   return digito == verif;
               };


               //#endregion

               //#region Proveedores

               $scope.setCreateProveedor = function (prop) {
                   $scope.prop = prop;
                   $scope.resultModal = { hasErrors: false, messages: [] };
                   $scope.proveedor = {};
                   $('#proveedorModal').modal('show');
               };

               $scope.createProveedor = function () {
                   if (!$scope.isValidProveedor()) return;

                   generalService.createProveedor($scope.proveedor).then(function (response) {
                       $scope.resultModal = response.data.result;
                       if ($scope.resultModal.hasErrors) return;

                       $('#proveedorModal').modal('hide');
                       $scope.entity[$scope.prop] = response.data.data;
                   }, function () { throw 'Error on createProveedor'; });
               };

               $scope.isValidProveedor = function () {
                   $scope.resultModal = { hasErrors: false, messages: [] };

                   if (!$scope.proveedor.nombre) {
                       $scope.resultModal.messages.push('Ingrese el nombre del proveedor');
                   }

                   if (!$scope.proveedor.numeroDocumento) {
                       $scope.resultModal.messages.push('Ingrese el ' + $scope.usuario.currentEmpresaLabelCuit);
                   }

                   if ($scope.esGrupoCresud && $scope.proveedor.numeroDocumento && !$scope.isValidCuit($scope.proveedor.numeroDocumento)) {
                       $scope.resultModal.messages.push($scope.usuario.currentEmpresaLabelCuit + ' inválido');
                   }

                   $scope.resultModal.hasErrors = $scope.resultModal.messages.length;
                   return !$scope.resultModal.hasErrors;
               };

               //#endregion

               //#region Choferes

               $scope.setCreateChofer = function (prop) {
                   $scope.prop = prop;
                   $scope.resultModal = { hasErrors: false, messages: [] };
                   $scope.chofer = {};
                   $('#choferModal').modal('show');
               };

               $scope.createChofer = function () {
                   if (!$scope.isValidChofer()) return;

                   $scope.chofer.grupoEmpresaId = $scope.usuario.currentEmpresa.grupoEmpresa.id;

                   choferesService.createEntity($scope.chofer).then(function (response) {
                       $scope.resultModal = response.data.result;
                       if ($scope.resultModal.hasErrors) return;

                       $('#choferModal').modal('hide');
                       $scope.entity[$scope.prop] = response.data.data;
                   }, function () { throw 'Error on createEntity'; });
               };

               $scope.isValidChofer = function () {
                   $scope.resultModal = { hasErrors: false, messages: [] };
                   var patenteOldRegex = /^[A-ZÑ]{3}\d{3}$/;
                   var patenteNewRegex = /^[A-ZÑ]{2}\d{3}[A-ZÑ]{2}$/;
                   var esChoferTransportista = $scope.prop == 'choferTransportista';

                   if (!$scope.chofer.nombre) {
                       $scope.resultModal.messages.push(esChoferTransportista ? 'Ingrese la descripción' : 'Ingrese el nombre');
                   }

                   if (!esChoferTransportista && !$scope.chofer.apellido) {
                       $scope.resultModal.messages.push('Ingrese el apellido');
                   }

                   if ($scope.esGrupoCresud && !$scope.chofer.cuit) {
                       $scope.resultModal.messages.push('Ingrese el cuit');
                   }

                   if ($scope.esGrupoCresud && $scope.chofer.cuit && !$scope.isValidCuit($scope.chofer.cuit)) {
                       $scope.resultModal.messages.push('Cuit inválido');
                   }                  

                   //if (!esChoferTransportista && $scope.esGrupoCresud && !$scope.chofer.camion) {
                   //    $scope.resultModal.messages.push('Ingrese la patente del camión');
                   //}

                   if (!esChoferTransportista &&
                        $scope.esGrupoCresud &&
                        $scope.chofer.camion &&
                        !($scope.chofer.camion.match(patenteOldRegex) || $scope.chofer.camion.match(patenteNewRegex))) {
                       $scope.resultModal.messages.push('Formato de patente de camión inválido. Formato corrercto ej: AAA111 o AA111AA');
                   }

                   //if (!esChoferTransportista && $scope.esGrupoCresud && !$scope.chofer.acoplado) {
                   //    $scope.resultModal.messages.push('Ingrese la patente del acoplado');
                   //}

                   if (!esChoferTransportista &&
                        $scope.esGrupoCresud &&
                        $scope.chofer.acoplado &&
                        !($scope.chofer.acoplado.match(patenteOldRegex) || $scope.chofer.acoplado.match(patenteNewRegex))) {
                       $scope.resultModal.messages.push('Formato de patente de acoplado inválido. Formato corrercto ej: AAA111 o AA111AA');
                   }

                   $scope.resultModal.hasErrors = $scope.resultModal.messages.length;
                   return !$scope.resultModal.hasErrors;
               };

               //#endregion

               //#region Select UI
                              
               $scope.selectList = [];               
               $scope.currentPage = 0;
               $scope.pageCount = 0;

               $scope.getSelectSource = function($select, $event) {
                   if ($scope.loading) return;

                   var source = $scope.sources[$select.$element.attr('name')];

                   if (!$event) {
                       $scope.currentPage = 1;
                       $scope.pageCount = 0;
                       $scope.selectList = [];
                   } else {
                       $event.stopPropagation();
                       $event.preventDefault();
                       $scope.currentPage++;
                   }

                   source.filter.currentPage = $scope.currentPage;
                   source.filter.multiColumnSearchText = $select.search;

                   source.service[source.method](source.filter).then(function (response) {
                       $scope.selectList = $scope.selectList.concat(response.data.data);
                       $scope.pageCount = Math.ceil(response.data.count / 20);
                   }, function() { throw 'Error on getSelectByFilter'; });
               };

               //#endregion

               //#region Watches

               $scope.$watch('entity.tipoDeCartaId', function (newValue, oldValue) {
                   if ($scope.loading) return;
                   $scope.setControls();
                   $scope.setDefaultValues();
               });
              

               $scope.$watch('controlsVisibility.ctg', function (newValue, oldValue) {
                   if ($scope.loading) return;
                   
                   if (!newValue) {
                       $scope.entity.ctg = null;
                   }
               });

               $scope.$watch('controlsVisibility.numeroCartaDePorte', function (newValue, oldValue) {
                   if ($scope.loading) return;

                   if (!newValue) {
                       $scope.entity.numeroCartaDePorte = null;
                   }
               });

               $scope.$watch('controlsVisibility.cee', function (newValue, oldValue) {
                   if ($scope.loading) return;

                   if (!newValue) {
                       $scope.entity.cee = null;
                   }
               });
               
               $scope.$watch('entity.cargaPesadaDestino', function (newValue, oldValue) {
                   if ($scope.loading) return;

                   $scope.entity.pesoTara = null;
                   $scope.entity.pesoBruto = null;
                   $scope.entity.kilogramosEstimados = null;
               });

               $scope.$watch('entity.estadoFlete', function (newValue, oldValue) {
                   if ($scope.loading) return;

                   $scope.entity.clientePagadorDelFlete = null;

                   if (newValue ==  2) {
                       $scope.entity.clientePagadorDelFlete = $scope.data.clienteDefault;
                   }
               });

               $scope.$watch('entity.clientePagadorDelFlete', function (newValue, oldValue) {
                   if ($scope.loading || !newValue || $scope.data.clienteDefault == newValue) return;
                   
                   empresasService.getByClienteId(newValue.id).then(function (response) {
                       $scope.entity.empresaClientePagadorFlete = response.data.data;

                       if ($scope.entity.empresaClientePagadorFlete) {
                           $scope.entity.proveedorTransportista = null;
                       } else {
                           $scope.entity.choferTransportista = null;
                       }
                   }, function () { throw 'Error on getSelectByFilter'; });                  
               });

               $scope.$watch('entity.establecimientoDestino', function (newValue, oldValue) {
                   if ($scope.loading) return;

                   $scope.entity.clienteDestino = null;

                   if (newValue) {
                       generalService.getClienteById(newValue.interlocutorDestinatarioId).then(function (response) {
                           $scope.entity.clienteDestino = response.data.data;                         
                       }, function () { throw 'Error on getClienteById'; });                                                         
                   }
               });

               $scope.$watch('entity.clienteRemitenteComercial', function (newValue, oldValue) {
                   if ($scope.loading) return;
                   $scope.entity.empresaClienteRemitenteComercial = null;

                   if (!newValue) return;

                   empresasService.getByClienteId(newValue.id).then(function (response) {
                       $scope.entity.empresaClienteRemitenteComercial = response.data.data;
                   }, function () { throw 'Error on getByClienteId'; });
               });

               $scope.$watch('entity.proveedorTitularCartaDePorte', function (newValue, oldValue) {
                   if ($scope.loading) return;
                   $scope.entity.empresaProveedorTitularCartaDePorte = null;

                   if (!newValue) return;

                   empresasService.getBySapId(newValue.sapId).then(function (response) {
                       $scope.entity.empresaProveedorTitularCartaDePorte = response.data.data;
                   }, function () { throw 'Error on getBySapId'; });
               });

               $scope.$watch('entity.grano', function (newValue, oldValue) {
                   if ($scope.loading) return;
                   $scope.entity.loteDeMaterial = null;

                   if (!newValue) return;

                   $scope.entity.loteDeMaterial = newValue.sujetoALote;
               });
                              
               //#endregion
           }]);
﻿function ViewCommonProcess() {
    var createUrl;
  
    var apuestasModel;

    var renderDetalleApuestaListUrl = "/GenerarApuestas/RenderListDetallesOrden";

    var addAndRenderDetalleApuestaListUrl = "/GenerarApuestas/AddAndRenderListDetallesOrden";

    var apuestasVarias = function (usuarioId, usuarioNombre, arrayApuestas) {
        this.UsuarioId = usuarioId;
        this.UsuarioNombre = usuarioNombre;
        this.Detalles = arrayApuestas;
    };

    this.initializeApuestas = function () {
        try {
            apuestasModel = new apuestasVarias(-1, "", []);
            return apuestasModel;
        } catch (exc) {
            return false;
        }
    };

    var detalleApuestas = function (numeroId, numero, montoApuesta, borrar, errorCode, errorDesc) {
        this.IdNumero = numeroId;
        this.Numeros = numero;
        this.Monto = montoApuesta;

        this.Borrar = borrar; // si hay que actualizar algun dato
        this.ErrorDescription = errorDesc;
        this.ErrorCode = errorCode;
    };

    this.addApuestaDetalle = function (numeroId, numero, montoApuesta,  borrar, errorCode, errorDesc) {
        try {
           // numeroApuesta = $("<div>").html(numeroApuesta).text();
            var apuestaDetalleItem = new detalleApuestas(numeroId, numero, montoApuesta, borrar, errorCode, errorDesc);
            apuestasModel.Detalles.push(apuestaDetalleItem);
            return apuestaDetalleItem;
        } catch (exc) { return false; }
    };

    this.addApuestaDetalleP = function (numeroId, numero, montoApuesta) {
        try {
            // numeroApuesta = $("<div>").html(numeroApuesta).text();
            var apuestaDetalleItem = new detalleApuestas(numeroId, numero, montoApuesta);
            apuestasModel.Detalles.push(apuestaDetalleItem);
            return apuestaDetalleItem;
        } catch (exc) { return false; }
    };

    function removeApuestaDetalle(index) {
        try {
            //facturasVariasModel.Detalles.splice(index, 1);

            $.each(apuestasModel.Detalles, function (i, value) {
                if (index == value.IdNumero) {
                    value.Borrar = 1;
                }
            });

            renderDetallesApuestas();

            return true;
        }
        catch (exc) { return false; }
    };



    this.clearDetallesList = function () {
        try {
            apuestasModel.Detalles = [];
        } catch (exc) { return false; }
    };


    this.setApuestasV = function (usuarioId, usuarioNombre) {
        try {
            apuestasModel.UsuarioId = usuarioId;
            apuestasModel.UsuarioNombre = usuarioNombre;

            return true;
        } catch (exc) { return false; }
    };

    this.getApuestasV = function () {
        try {
            return apuestasModel;
        } catch (exc) {
            return false;
        }
    };


    //---- Errors Alerts
    function clearErrors() { $('#msgErrorAnyModal').html(''); $('#IndexAlerts').html(''); $('#detalleInventarioAlerts').html(''); }

    function writeError(control, msg, type) {
        var errMsg = '<div class="alert alert-danger" role="alert"><a class="close" data-dismiss="alert" href="#">×</a><h4 class="alert-heading">' + msg + '</h4></div>';
        $('#' + control).html(errMsg);
    }

  
    function renderDetallesApuestas() {
        clearErrors();
        $.ajax({
            url: renderDetalleApuestaListUrl,
            type: "POST",
            cache: false,
            data: { jsonDetallesList: JSON.stringify(apuestasModel.Detalles) },
            success: function (data) {
                if (data.Error == "-1") { writeAlert('detalleInventarioAlerts', 'Error al agregar detalle a la orden.', 'error'); }
                else {
                    $("#anyListDetailsEntity").html(data);
                    clearDetallesFields();
                    disableDetallesFields();
                }
            }, error: function () { writeAlert('detalleInventarioAlerts', data.Message, 'error'); }
        });
    }

    this.addAndRenderDetallesApuestaList = function (idNumero, numero, montoApuesta) {
        clearErrors();
        $.ajax({
            url: addAndRenderDetalleApuestaListUrl,
            type: "POST",
            cache: false,
            data: { jsonDetallesList: JSON.stringify(apuestasModel.Detalles), IdNumero: idNumero, Numero: numero, MontoApuesta: montoApuesta},
            success: function (data) {
                if (data.Error == "-1") {
                    writeError('detalleInventarioAlerts', data.Message, 'danger');
                }
                else {
                    $("#anyListDetailsEntity").html(data);
                    clearDetallesFields();
                    disableDetallesFields();
                }
            }, error: function () { writeAlert('detalleInventarioAlerts', 'Error al agregar detalle a la orden.', 'error'); }
        });
    }

    function clearDetallesFields() {
        $(".clearDetalle").val('');
        return true;
    }

    function enableDetallesFields() {
        $(".detalleAddPanel").prop('disabled', false);
        return true;
    }
    function disableDetallesFields() {
        $(".detalleAddPanel").prop('disabled', true);
        return true;
    }


   this.removerDetalle= function  (idNumero) {
        clearErrors();
        removeApuestaDetalle(idNumero);
        return false;
    };



    //this.updateOrdenesList = function () {
    //    updateSolicitudesListLocal();
    //};


   


    function cerrarForm() {
        clearErrors();
        $("#anyPanelBody").html("");
        return false;
    };


    //$('a.btnCreateMainForm').click( function () {
    //    clearErrors();
    //    $("#anyPanelBody").html("");
    //    if (!isEmpty(createUrl)) {
    //        getRequestNoModal(createUrl);
    //    }
    //    return false;
    //});


    



    //Enviar y recibir facturas

    //$('#guardarFacturaVaria').click(function () {
    //    clearErrors();
    //    var contador = 0;
    //    var detalle = facturasVariasModel.Detalles.length;
    //    for (var i = 0; i < facturasVariasModel.Detalles.length; i++) {
    //        if (facturasVariasModel.Detalles[i].Borrar == 1) {
    //            {
    //                contador++;
    //            }
    //        }
    //    }
    //    if (contador == detalle) {
    //        writeAlert('IndexAlerts', 'La factura no tiene ningún detalle asociado.', 'error');
    //        return;
    //    }

    //    if (facturasVariasModel.Detalles < 1) {
    //        writeAlert('IndexAlerts', 'La factura no tiene ningún detalle asociado.', 'error');
    //        return;
    //    }
    //    $('body').addClass("loading");
    //    $.ajax({
    //        url: createUrl,
    //        type: "POST",
    //        cache: false,
    //        data: { jsonDetallesList: JSON.stringify(facturasVariasModel.Detalles), abonadoId: $("#AbonadoId_Hidden").val(), abonadoNom: $("#AbonadoName_text").val() },
    //        success: function (data) {
    //            if (Number(data) != -1) {
    //                if (data.TipoImpresora == "0") {
    //                    imprimirFacturaVariasImpPunto(data);
    //                    updateSolicitudesListLocal();
    //                    writeAlert('IndexAlerts', 'Factura procesada.', 'success');
    //                    cerrarForm();
    //                } else if (data.TipoImpresora == "1") {
    //                    imprimirFacturaVariasImpMatriz(data);
    //                    writeAlert('IndexAlerts', 'Factura procesada.', 'success');
    //                    cerrarForm();
    //                }

    //            } else {
    //                writeAlert('IndexAlerts', 'Error al imprimir la factura.', 'error');
    //            }
    //        },
    //        error: function () { writeAlert('IndexAlerts', 'Error al agregar notas a la factura.', 'error'); }
    //    });
    //});

    
}

ViewCommonVariable = new ViewCommonProcess();
function ViewCommonProcess() {
    var createUrl;
  
    var apuestasModel;

    var createDetalleCompraUrl;

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

    var detalleApuestas = function (detalleId, numeroId, numero, montoApuesta, guardado, actualizar, borrar, errorCode, errorDesc, index) {
        this.Index = index;
        this.DetalleId = detalleId;
        this.NumeroId = numeroId;
        this.Numero = numero;
        this.MontoApuesta = montoApuesta;

        this.Guardado = guardado; //si ya esta guardado a a orden
        this.Actualizar = actualizar; // si hay que actualizar algun dato
        this.Borrar = borrar; // si hay que actualizar algun dato
        this.DisponibilidadArticulo = 0;
        this.ErrorDescription = errorDesc;
        this.ErrorCode = errorCode;
    };

    this.addApuestaDetalle = function (detalleId, numeroId, numero, montoApuesta, guardado, actualizar, borrar, errorCode, errorDesc, index) {
        try {
           // numeroApuesta = $("<div>").html(numeroApuesta).text();
            var apuestaDetalleItem = new detalleApuestas(detalleId, numeroId, numero, montoApuesta, guardado, actualizar, borrar, errorCode, errorDesc, index);
            apuestasModel.Detalles.push(apuestaDetalleItem);
            return apuestaDetalleItem;
        } catch (exc) { return false; }
    };

    this.addApuestaDetalleP = function (numeroId, numero, montoApuesta) {
        try {
            // numeroApuesta = $("<div>").html(numeroApuesta).text();
            var apuestaDetalleItem = new detalleApuestas(detalleId, numeroId, numero, montoApuesta);
            apuestasModel.Detalles.push(apuestaDetalleItem);
            return apuestaDetalleItem;
        } catch (exc) { return false; }
    };

    function removeApuestaDetalle(index) {
        try {
            //facturasVariasModel.Detalles.splice(index, 1);

            $.each(apuestasModel.Detalles, function (i, value) {
                if (index == value.Index) {
                    value.Borrar = 1;
                    value.Actualizar = 1;
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


    this.setUrlViewsApuestasFunctions = function (createUrlp,  renderDetalleApuestaListUrlp, addAndRenderDetalleApuestaListUrlp) {
        createUrl = createUrlp;
        renderDetalleApuestaListUrl = renderDetalleApuestaListUrlp;
        addAndRenderDetalleApuestaListUrl = addAndRenderDetalleApuestaListUrlp;
    };


    //---- Errors Alerts
    function clearErrors() { $('#msgErrorAnyModal').html(''); $('#IndexAlerts').html(''); $('#detalleInventarioAlerts').html(''); }

    function writeError(control, msg, type) {
        var errMsg = '<div class="alert alert-block alert-' + type + '"><a class="close" data-dismiss="alert" href="#">×</a><h4 class="alert-heading">' + msg + '</h4></div>';
        $('#' + control).html(errMsg);
    }

    //------------Articulos y Detalles
    function getRequest(url) {
        $.ajax({
            url: url,
            context: document.body,
            success: function (data) {
                $('.modal-body p.body').html(data);
                $(this).addClass("done");
                $('#anyModalForm').modal('show');
            }, error: function (err) { writeError('msgErrorAnyModal', err, 'error'); }
        });
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

    this.addAndRenderDetallesApuestaList = function (detalleId, idNumero, numero, montoApuesta) {
        clearErrors();
        $.ajax({
            url: addAndRenderDetalleApuestaListUrl,
            type: "POST",
            cache: false,
            data: { jsonDetallesList: JSON.stringify(apuestasModel.Detalles), DetalleId: detalleId, IdNumero: idNumero, Numero: numero, MontoApuesta: montoApuesta},
            success: function (data) {
                if (data.Error == "-1") {
                    writeAlert('detalleInventarioAlerts', data.Message, 'error');
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

    //$('#guardarApuestasBtn').click( function () {
    //    clearErrors();
    //    var detalleId = $("#ProductoId_Hidden").val();
    //    var idNumero = $("#IdNumero_Hidden").val();
    //    var numero = $("#Numero_Hidden").val();
    //    var montoApuesta = $("#MontoApuesta_Hidden").val();
    //    var isUpdateVal = $("#isUpdateHidden").val();


    //    var index = $("#selectedIndexArticuloHidden").val();
    //    if (isUpdateVal == 0) { addAndRenderDetallesApuestaList(detalleId, idNumero, numero, montoApuesta); }
    //    return false;
    //});


    //$('.btnDeleteDetalle').click(function () {
    //    clearErrors();
    //    var index = $(this).attr("data-idDetalleOrden");
    //    removeApuestaDetalle(index);
    //    return false;
    //});



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


    //$('a.btnModalCancelPaseInventarioMainConfirmation').live('click', function () {
    //    clearErrors();
    //    $('#PaseInventarioConfirmationMain_modal').modal('hide');
    //    return false;
    //});


    

    this.logfacturaDetalle = function (id, index, isNota, isProducto, tipo, cantidad, nombre, descripcion, impuestos, monto) {
        this.clearDetallesList();
        try {
            var ordenDetalleItem = new detalleInventario(id, isProducto, isNota, tipo, nombre, cantidad, monto, impuestos, descripcion, 0, 1, 0, 0, "", index);
            facturasVariasModel.Detalles.push(ordenDetalleItem);
            return ordenDetalleItem;
        } catch (exc) { }
    };




}

ViewCommonVariable = new ViewCommonProcess();
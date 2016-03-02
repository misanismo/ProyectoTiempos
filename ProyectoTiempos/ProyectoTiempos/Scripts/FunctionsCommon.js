/// <reference path="jquery.validate.unobtrusive.min.js" />
function ViewCommonProcess() {
    var createUrl = "/GenerarApuestas/GenerarApuesta";
    var apuestasModel;
    var renderDetalleApuestaListUrl = "/GenerarApuestas/RenderListDetallesOrden";
    var addAndRenderDetalleApuestaListUrl = "/GenerarApuestas/AddAndRenderListDetallesOrden";


    //Crea las variables para la funcion InitializeApuestas

    var apuestasVarias = function (usuarioId, usuarioNombre, arrayApuestas) {
        this.UsuarioId = usuarioId;
        this.UsuarioNombre = usuarioNombre;
        this.Detalles = arrayApuestas;
    };


    // con las variables anteriores ejecuto la funcion, la cual realiza la apuesta

    this.initializeApuestas = function () {
        try {
            apuestasModel = new apuestasVarias(-1, "", []);
            return apuestasModel;
        } catch (exc) {
            return false;
        }
    };


    // creo las variables que se van a necesitar para mostrat el detalle de las apuestas

    var detalleApuestas = function (numeroId, numero, montoApuesta, borrar, errorCode, errorDesc) {
        this.IdNumero = numeroId;
        this.Numeros = numero;
        this.Monto = montoApuesta;

        this.Borrar = borrar; // si hay que actualizar algun dato
        this.ErrorDescription = errorDesc;
        this.ErrorCode = errorCode;
    };


    //ejecuto la funcion de agregar la apuesta detallada


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



    //funcion que elimina la apuesta detallada



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


    //limpia los detalles de las apuestas de la lista


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

    function writeSuccess(control, msg, type) {
        var errMsg = '<div class="alert alert-success" role="alert"><a class="close" data-dismiss="alert" href="#">×</a><h4 class="alert-heading">' + msg + '</h4></div>';
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
            }, error: function () { writeAlert('alertsDetalles', 'Error al agregar detalle a la orden.', 'error'); }
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


    function cerrarForm() {
        clearErrors();
        $("#anyPanelBody").html("");
        return false;
    };



    this.guardarTodoApuesta= function (idUsuario, idSorteo) {
        clearErrors();
        var contador = 0;
        var detalle = apuestasModel.Detalles.length;
        for (var i = 0; i < apuestasModel.Detalles.length; i++) {
            if (apuestasModel.Detalles[i].Borrar == 1) {
                {
                    contador++;
                }
            }
        }
        if (contador == detalle) {
            writeError('alertsDetalles', 'No ha seleccionado numeros para apostar.', 'error');
            return;
        }

        if (apuestasModel.Detalles < 1) {
            writeError('alertsDetalles', 'No ha seleccionado numeros para apostar.', 'error');
            return;
        }
        $.ajax({
            url: createUrl,
            type: "POST",
            cache: false,
            data: { jsonDetallesList: JSON.stringify(apuestasModel.Detalles), idUsuario: idUsuario, idSorteo: idSorteo },
            success: function (data) {
                if (data.Error == "-1") {
                    writeError('alertsDetalles', data.Message, 'danger');
                 } else {
                    writeSuccess('alertsDetalles', 'La Apuesta se ha guardado correctamente.', 'error');
                    $("#anyListDetailsEntity").html(data);
                    initializeApuestas();
                    clearDetallesFields();
                }
            },
            error: function () { writeError('alertsDetalles', 'Error .', 'error'); }
        });
    };

    
}

ViewCommonVariable = new ViewCommonProcess();
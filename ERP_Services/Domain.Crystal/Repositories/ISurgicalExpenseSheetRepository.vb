'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Cesar Collazos
' Created          : 27-03-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Crystal.Entities

Public Interface ISurgicalExpenseSheetRepository

    ''' <summary>
    ''' Función para obtener la hoja de gasto QX
    ''' </summary>
    ''' <param name="idProgramacion"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Function GetListSurgicalExpenseSheetByScheduleId(idProgramacion As Integer, admissionNumber As String) As List(Of HCHOJAGASTOQX_Model)

    ''' <summary>
    ''' Sp que se encarga de listar los productos asociados a la hoja de gasto Qx
    ''' </summary>
    ''' <param name="idHojaGastoQX"></param>
    ''' <returns></returns>
    Function SPHC_ListarProductosHojaGastoQX(idHojaGastoQX As Integer) As List(Of ProductSurgicalExpenseSheetModel)

    ''' <summary>
    ''' Sp que se encarga de confirmar la solicitud de paquete Qx
    ''' </summary>
    ''' <param name="programationId"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="xmlProducts"></param>
    ''' <returns></returns>
    Function SP_AGE_ConfirmarSolicitarPaquete(programationId As Integer, codeUser As String, xmlProducts As String) As SP_AGE_ConfirmarSolicitarPaquete_Result

    ''' <summary>
    ''' Sp que confirma la hoja de gasto quirúrgico
    ''' </summary>
    ''' <param name="origenDevolutivo"></param>
    ''' <param name="camaOrigen"></param>
    ''' <param name="nombreUnidadDestino"></param>
    ''' <param name="codigoBodega"></param>
    ''' <param name="centroCosto"></param>
    ''' <param name="usuarioIndigo"></param>
    ''' <param name="centroAtencion"></param>
    ''' <param name="unidadFuncional"></param>
    ''' <param name="ingreso"></param>
    ''' <param name="paciente"></param>
    ''' <param name="profesional"></param>
    ''' <param name="xmlProductos"></param>
    ''' <param name="usuarioHoja"></param>
    ''' <param name="idHojaToConfirm"></param>
    ''' <param name="timeStampFromHoja"></param>
    ''' <param name="source"></param>
    ''' <returns></returns>
    Function SPHC_ConfirmarHojaGastoQX(origenDevolutivo As Integer, camaOrigen As String, nombreUnidadDestino As String, nombreUsuarioIndigo As String, codigoBodega As String, centroCosto As String, userCode As String, centroAtencion As String,
                                       unidadFuncional As String, ingreso As String, paciente As String, profesional As String, xmlProductos As String, usuarioHoja As String, idHojaToConfirm As Integer) As SPHC_ConfirmarHojaGastoQX_Result
End Interface

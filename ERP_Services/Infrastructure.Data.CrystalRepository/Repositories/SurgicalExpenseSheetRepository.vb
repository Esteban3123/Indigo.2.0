'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Cesar Collazos
' Created          : 27-05-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Crystal.Entities
Imports Domain.Crystal
Imports Infrastructure.Data.Base
Imports System.Data.Entity.Infrastructure

Public Class SurgicalExpenseSheetRepository
    Inherits GenericRepository(Of HCHOJAGASTOQX)
    Implements ISurgicalExpenseSheetRepository

    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Función para obtener la hoja de gasto QX
    ''' </summary>
    ''' <param name="idProgramacion"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Function GetListSurgicalExpenseSheetByScheduleId(idProgramacion As Integer, admissionNumber As String) As List(Of HCHOJAGASTOQX_Model) Implements ISurgicalExpenseSheetRepository.GetListSurgicalExpenseSheetByScheduleId

        Dim parmas As List(Of (String, Object)) = New List(Of (String, Object)) From {("@idProgramacion", idProgramacion), ("@admissionNumber", admissionNumber)}

        Dim query = Me.ExecuteQueryDR(Of HCHOJAGASTOQX_Model)("

        SELECT H.ID, H.CONSECUTIVO,H.ESTADO, H.FECHAREGISTRO , Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') as Procedimiento , H.NUMINGRES as Ingreso,H.IPCODPACI as Identificacion,
        CASE C.ORIGENQX
            WHEN 1 then 'Ambulatoria'
            ELSE 'Hospitalaria' END as OrigenQX,RTRIM(F.CODPROSAL) + ' - ' + Rtrim(F.NOMMEDICO) As 'Profesional',Rtrim(J.UFUDESCRI) AS 'UnidadFuncional',B.DESCRIPSAL As 'Sala', C.CODAUTONU AS IdProgramacionCX,
         H.ConcurrencyControl, H.UserTryingConfirm
         FROM dbo.HCHOJAGASTOQX H with(nolock)
         INNER JOIN dbo.AGEPROGQX C with(nolock) on C.CODAUTONU = H.IDAGEPROGQX
         INNER JOIN dbo.INCUPSIPS E with(nolock) ON E.CODSERIPS = C.CODSERIPS
         INNER JOIN dbo.AGENSALAC B with(nolock) ON C.AGENSALAC = B.CODCONCEC
         INNER JOIN dbo.INUNIFUNC J with(nolock) ON B.UFUCODIGO = J.UFUCODIGO
         INNER JOIN dbo.INPROFSAL F with(nolock) ON C.CODPROSAL = F.CODPROSAL
         LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = C.IDDESCRIPCIONRELACIONADA
         LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
         WHERE H.IDAGEPROGQX = @idProgramacion AND H.NUMINGRES = @admissionNumber 
            ", parmas)?.ToList()

        If query?.Any() Then
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Sp que se encarga de listar los productos asociados a la hoja de gasto Qx
    ''' </summary>
    ''' <param name="idHojaGastoQX"></param>
    ''' <returns></returns>
    Public Function SPHC_ListarProductosHojaGastoQX(idHojaGastoQX As Integer) As List(Of ProductSurgicalExpenseSheetModel) Implements ISurgicalExpenseSheetRepository.SPHC_ListarProductosHojaGastoQX
        Dim result = _crystalContext.SPHC_ListarProductosHojaGastoQX(idHojaGastoQX).ToList()
        Dim listProducts As New List(Of ProductSurgicalExpenseSheetModel)()

        For Each item In result
            Dim product As New ProductSurgicalExpenseSheetModel() With {
            .Orden = item.Orden,
            .ID = item.ID,
            .IDHCHOJAGASTOQX = item.IDHCHOJAGASTOQX,
            .CodigoProducto = item.CodigoProducto,
            .CodigoP = item.CodigoP,
            .Producto = item.Producto,
            .NombreP = item.NombreP,
            .CANTIDADENTREGADA = item.CANTIDADENTREGADA,
            .CANTIDADACEPTADADEV = item.CANTIDADACEPTADADEV,
            .CANTIDADGASTADA = item.CANTIDADENTREGADA,
            .CantidadGastadaInicial = item.CANTIDADENTREGADA,
            .CANTIDADDEVOLVER = 0,
            .ORIGENSOLICITUD = item.ORIGENSOLICITUD,
            .FECHAREGISTRO = item.FECHAREGISTRO,
            .OrigenProducto = item.OrigenProducto,
            .IDAGEPROGQX = item.IDAGEPROGQX,
            .CONSEKARDEX = item.CONSEKARDEX,
            .RequestType = item.RequestType,
            .StatusOrder = item.StatusOrder
            }
            listProducts.Add(product)
        Next
        Return listProducts
    End Function

    ''' <summary>
    ''' Sp que se encarga de confirmar la solicitud de paquete qx
    ''' </summary>
    ''' <param name="programationId"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="xmlProducts"></param>
    ''' <returns></returns>
    Public Function SP_AGE_ConfirmarSolicitarPaquete(programationId As Integer, codeUser As String, xmlProducts As String) As SP_AGE_ConfirmarSolicitarPaquete_Result Implements ISurgicalExpenseSheetRepository.SP_AGE_ConfirmarSolicitarPaquete
        DirectCast(_crystalContext, IObjectContextAdapter).ObjectContext.CommandTimeout = 15000
        Return _crystalContext.SP_AGE_ConfirmarSolicitarPaquete(programationId, codeUser, xmlProducts).SingleOrDefault()
    End Function

    ''' <summary>
    ''' Funcion que llama al SP que confirma la hoja de gasto QX
    ''' </summary>
    ''' <returns></returns>
    Public Function SPHC_ConfirmarHojaGastoQX(origenDevolutivo As Integer, camaOrigen As String, nombreUnidadDestino As String, nombreUsuarioIndigo As String, codigoBodega As String, centroCosto As String, userCode As String, centroAtencion As String, unidadFuncional As String,
                                       ingreso As String, paciente As String, profesional As String, xmlProductos As String, usuarioHoja As String, idHojaToConfirm As Integer) As SPHC_ConfirmarHojaGastoQX_Result Implements ISurgicalExpenseSheetRepository.SPHC_ConfirmarHojaGastoQX

        DirectCast(_crystalContext, IObjectContextAdapter).ObjectContext.CommandTimeout = 15000
        Return _crystalContext.SPHC_ConfirmarHojaGastoQX(origenDevolutivo, camaOrigen, nombreUnidadDestino, nombreUsuarioIndigo, codigoBodega, centroCosto, userCode, centroAtencion, unidadFuncional,
                                       ingreso, paciente, profesional, xmlProductos, usuarioHoja, idHojaToConfirm).SingleOrDefault()
    End Function
#End Region

End Class

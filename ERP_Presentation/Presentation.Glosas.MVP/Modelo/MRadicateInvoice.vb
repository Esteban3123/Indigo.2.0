'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael Patiño
' Created          : 10-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports System.Text
Imports Domain.Security.Entities
Imports Infrastructure.Data.Xpo.BillingRepository
Imports RestSharp
Imports System.Net.Http
Imports Presentation.Base
Imports System.Dynamic
Imports Infrastructure.Base.Security

#End Region



''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>

Public Class MRadicateInvoice
    Implements IDisposable
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance



#Region "Builders"

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    Sub New(Tag As String)
        _tagForm = Tag
    End Sub

#End Region
    ''' <summary>
    ''' Obtiene una lista de facturas RadicatedD
    ''' </summary>
    ''' <param name="radicateinvoiceCid">Nit a consultar</param>
    ''' <returns>Un objeto <see cref="DevExpress.Xpo.XPInstantFeedbackSource" /></returns>
    Public Function ListViewRadicateInvoiceDetail(ByVal radicateinvoiceCid As Integer?, Optional ByVal InvalidateOffice As Boolean = False) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).GlosasService.ListViewRadicateInvoiceDetail(radicateinvoiceCid, InvalidateOffice)
    End Function
    ''' <summary>
    ''' Obtiene una lista de facturas RadicatedD
    ''' </summary>
    ''' <param name="radicateinvoiceCid">Nit a consultar</param>
    ''' <returns>Un objeto <see cref="DevExpress.Xpo.XPInstantFeedbackSource" /></returns>
    Public Function ListXPCollectionRadicateInvoiceD(ByVal radicateinvoiceCid As Integer?, Optional ByVal InvalidateOffice As Boolean = False) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).GlosasService.ListXPCollectionRadicateInvoiceD(radicateinvoiceCid, InvalidateOffice)
    End Function

    ''' <summary>
    ''' Obtiene una lista de facturas RadicatedD
    ''' </summary>
    ''' <returns>Un objeto <see cref="DevExpress.Xpo.XPInstantFeedbackSource" /></returns>
    Public Function ListXPCollectionRadicateInvoiceD() As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).GlosasService.ListXPCollectionRadicateInvoiceD()
    End Function

    ''' <summary>
    ''' Lista la estructura contable de parametros de interfaces segun norma 1121 para validar homologacion de cuentas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ValidateListInvoiceAccountTableFOxPrivate() As Task(Of List(Of AccountSettingsFOX_PrivateMethod))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateListInvoiceAccountTableFOxPrivateAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.PortfolioSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetNumericSequenseGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Function GetServerDate() As DateTime
        Return IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDate()
    End Function

    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOX privado
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAccountTableFOxPrivate(ByVal AccountValidate As String, Optional ByVal ObjectionReception As Boolean = False) As Boolean
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateAccountTableFOxPrivate(AccountValidate, Me.Indigo, ObjectionReception)
    End Function

    ''' <summary>
    ''' Funcion para obtener la recepcion de la objecion
    ''' </summary>
    ''' <param name="Consecutive">El Consecutivo.</param>
    ''' <returns></returns>
    Public Function GetRadicateInvoiceByIdSimple(ByVal Id As Integer) As RadicateInvoiceC
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetInvocieRadicateByIdSimple(Id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener la recepcion de la objecion
    ''' </summary>
    ''' <param name="Consecutive">El Consecutivo.</param>
    ''' <returns></returns>
    Public Async Function GetRadicateInvoiceC(ByVal Consecutive As String) As Task(Of RadicateInvoiceC)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetInvocieRadicateAsync(Consecutive, Me.Indigo)
    End Function
    ''' <summary>
    ''' Lista de factura de radicacion con oficio
    ''' </summary>
    ''' <param name="consecutive">numero de radicado del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetListRadicateD(ByVal consecutive As String) As Task(Of List(Of RadicateInvoiceD))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetListRadicateDAsync(consecutive, Me.Indigo)
    End Function
    ''' <summary>
    ''' Funcion para obtener las facturas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetInvoicesAll(ByVal NameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String) As Task(Of ActionResult(Of List(Of SP_invoiceList_Result)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAllInvoceAsync(NameContainer, nit, InvoiceNumber, Me.Indigo, stringSQl, TopQuery, FlagNotConfirmInvoice)
    End Function

    ''' <summary>
    ''' Funcion para cargra una factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetInvoice(ByVal NameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String) As Task(Of SP_invoiceList_Result)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetInvoiceAsync(NameContainer, nit, InvoiceNumber, Me.Indigo, stringSQl, FlagNotConfirmInvoice)
    End Function

    ''' <summary>
    ''' Funcion para obtener las sucursales o sedes
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetBranchAll() As Task(Of List(Of Domain.Entities.GlosasParametersInterface))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListInterfacesParametersAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerByNit(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerAsync(nit.Trim(), Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Function GetCustomerByNitSimple(ByVal nit As String) As Domain.Entities.Customer
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomer(nit.Trim(), Me.Indigo)
    End Function


    ''' <summary>
    ''' Funcion para guardar y confirmar un traslado cobro jurídicoa radicacion de cuentas
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <param name="IdSecuence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmRadicateC(ByVal Record As RadicateInvoiceC, ByVal IdSecuence As Integer) As Task(Of ActionResult(Of RadicateInvoiceC))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveAndConfirmRadicateCAsync(Record, IdSecuence, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Guardar la recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveRadicateInvoiceC(ByVal Record As RadicateInvoiceC) As Task(Of ActionResult(Of RadicateInvoiceC))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveRadicateInvoiveCAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Confirmar cabecera y detalle de factura de radicacion
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmRadicateInvoiceC(ByVal Record As RadicateInvoiceC, ByVal IdSecuence As Integer) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmInvoiceRadicateCAsync(Record, IdSecuence, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Eliminar la recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteRadicateInvoiceC(ByVal Record As Object) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteSaveRadicateInvoiveCAsync(Record, Me.Indigo)
    End Function


    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Async Function ValidateListInvoiceRadicateDSp(ListInvoices As List(Of String), Nit As String, container As String) As Task(Of ActionResult(Of List(Of RadicateInvoiceD)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateListInvoiceRadicateDSpAsync(ListInvoices, Nit, container, Me.Indigo.IndigoCompany, Me.Indigo)
    End Function

    ''' <summary>
    ''' Eliminar masivamente
    ''' </summary>
    ''' <param name="tmplist">eliminar masivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteListInvoiceD(ByVal tmplist As List(Of String)) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteListInvoiceDAsync(tmplist, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener los campos que permiten nulos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("RadicateInvoiceC", Me.Indigo)
    End Function


    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function


    ''' <summary>
    ''' Actualizar fecha de confirmacion 
    ''' </summary>
    ''' <param name="numberRadicate"></param>
    ''' <param name="newdate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UpdateConfirmDate(ByVal numberRadicate As String, ByVal newdate As Date) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.UpdateDateConfirmAsync(numberRadicate, newdate, Me.Indigo)
    End Function

    ''' <summary>
    ''' Genera RIPS de las Radicaciones CONFIRMADAS
    ''' </summary>
    ''' <param name="IdInvoiceRadicate">Id de la Radicación de Cuentas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateRIPSPlane(ByVal IdInvoiceRadicate As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, DetailPackage As Boolean, GenerateADPlane As Boolean, Optional InvoicesList As List(Of Integer) = Nothing) As Task(Of List(Of ActionMessageResult(Of StringBuilder)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GenerateRIPSPlaneAsync(IdInvoiceRadicate, ConsecutiveRadicateInvoice, CodificationType, ServiceCode, Me.Indigo, DetailPackage, GenerateADPlane, InvoicesList)
    End Function

    ''' <summary>
    ''' Genera FURIPS de las Radicaciones CONFIRMADAS
    ''' </summary>
    ''' <param name="IdInvoiceRadicate">Id de la Radicación de Cuentas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateFURIPSPlane(ByVal IdInvoiceRadicate As Integer, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As Task(Of List(Of ActionMessageResult(Of StringBuilder)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GenerateFURIPSPlaneAsync(IdInvoiceRadicate, Me.Indigo, InvoicesList)
    End Function

    ''' <summary>
    ''' Genera FURIPS de las Radicaciones CONFIRMADAS
    ''' </summary>
    ''' <param name="IdInvoiceRadicate">Id de la Radicación de Cuentas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateFURTRANPlane(InvoicesList As List(Of RIPSBilling)) As Task(Of List(Of ActionMessageResult(Of StringBuilder)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GenerateFURTRANPlaneAsync(InvoicesList, Me.Indigo)
    End Function

    Public Async Function GetMegaRIPSPlane(ByVal IdInvoiceRadicate As Integer, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As Task(Of ActionMessageResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetMegaRIPSByRadicateInvoiceIdAsync(IdInvoiceRadicate, Me.Indigo, InvoicesList)
    End Function

    ''' <summary>
    ''' Genera los Archivos de Mega Plano
    ''' </summary>
    ''' <param name="IdInvoiceRadicate">Id de la Radicación de Cuentas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateRIPSMegaPlane(ByVal IdInvoiceRadicate As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Optional InvoicesList As List(Of Integer) = Nothing) As Task(Of List(Of ActionMessageResult(Of StringBuilder)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GenerateRIPSMegaPlaneAsync(IdInvoiceRadicate, ConsecutiveRadicateInvoice, CodificationType, ServiceCode, Me.Indigo, InvoicesList)
    End Function

    ''' <summary>
    ''' Obitne el usuario por id
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetUserById(ByVal UserId As String) As Task(Of Domain.Security.Entities.User)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByIdAsync(UserId, Me.Indigo)
    End Function

    Public Function ListInvoiceIds(ByVal ListInvoiceNumbers As String) As List(Of InvoiceXpo)
        Dim filter As String = "InvoiceNumber IN (" & ListInvoiceNumbers & " )"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of InvoiceXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' metodo que manda a generar los radicacion electronica
    ''' </summary>
    ''' <param name="listInvoice"></param>
    ''' <param name="entityName"></param>
    ''' <returns></returns>
    Public Async Function GenerateERadication(listInvoice As List(Of String), entityName As String, Optional entityId As Integer? = Nothing) As Task(Of ActionResult)
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(Base.EndpointCodes.Revenue_Cycle)

        If endpoint Is Nothing Then
            Return New ActionResult With {.StateResult = False, .Message = $"Parámetro {NameOf(endpoint)} vacio"}
        End If

        Dim client = New RestClient(String.Format("{0}/portfolio/sendERadication/{1}", endpoint.UrlBase, entityName))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim objectDynamic As Object = New ExpandoObject
        objectDynamic.EntityId = entityId
        objectDynamic.EntityName = entityName
        objectDynamic.ListInvoice = listInvoice

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        req.AddParameter("application/json", Utils.SerializeObjectToJson(objectDynamic), ParameterType.RequestBody)

        Dim response = Await client.ExecuteAsync(Of ServiceResponse(Of String))(req)

        If response?.Data Is Nothing OrElse Not response?.Data?.Status Then
            Return New ActionResult With {.StateResult = False, .Message = If(response?.Data?.Message, response?.ErrorMessage)}
        End If
        Return New ActionResult With {.StateResult = True, .Message = response?.Data?.Message}

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

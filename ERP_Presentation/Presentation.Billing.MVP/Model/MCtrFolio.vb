'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan F. Tamayo
' Created          : 2014-11-10
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports RestSharp
Imports Presentation.Billing.Entities
Imports Infrastructure.Base.Security

#End Region

Public Class MCtrFolio
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista los grupos de atención
    ''' </summary>
    Public Function ListCareGroupByStatus() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).ContractService.ListCareGroupByStatus(True)
    End Function

    ''' <summary>
    ''' Lista los terceros
    ''' </summary>
    Public Function ListThirdParty() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).CommonService.GetThirdParty()
    End Function

    ''' <summary>
    ''' Lista los estados de los folio por permiso de usuario
    ''' </summary>
    Public Function ListConceptsCausesStatusFolioUsers() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListConceptsCausesStatusFolioUsers(_indigoSessionValues.UserIndigoId)
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio o una factura
    ''' </summary>
    ''' <param name="id">Id del RevenueControlDetail</param>
    Public Function ListRevenueControlAsync(ByVal id As Integer, ByVal admissionNumber As String) As IFolio
        Dim useRestControl = FeatureFlagSession.Instance.GetBoolVariation(FeatureFlagSession.LOAD_FOLIO_APIREST, False)

        If useRestControl Then
            Return ListRevenueControlRestAsync(id, admissionNumber).Data
        Else
            Dim dataXpo = XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListRevenueControl(id, admissionNumber)
            Dim folioxpo = New FolioXpo()
            folioxpo.Details = dataXpo.Source
            Return folioxpo
        End If
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio o una factura
    ''' </summary>
    Public Function ListRevenueControlForAnnullateInvoice(ByVal invoiceId As Integer) As IFolio
        Dim useRestControl = FeatureFlagSession.Instance.GetBoolVariation(FeatureFlagSession.LOAD_FOLIO_APIREST, False)

        If useRestControl Then
            Return ListRevenueControlForAnnullateInvoiceRestAsync(invoiceId).Data
        Else
            Dim dataXpo = XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListRevenueControlForAnnullateInvoice(invoiceId)
            Dim folioxpo = New FolioXpo()
            folioxpo.Details = dataXpo.Source
            Return folioxpo
        End If
    End Function

    Public Function ListRevenueControlRestAsync(ByVal id As Integer, ByVal admissionNumber As String) As ServiceResponse(Of Folio)
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim client = New RestClient(String.Format("{0}/billing/folio/{1}", endpoint.UrlBase, id))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        Dim response = client.ExecuteAsync(Of ServiceResponse(Of Folio))(req)
        response.Wait()
        Return response.Result.Data
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio o una factura
    ''' </summary>
    Public Function ListRevenueControlForAnnullateInvoiceRestAsync(ByVal invoiceId As Integer) As ServiceResponse(Of AnnullateFolio)
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim client = New RestClient(String.Format("{0}/billing/invoice/annullate/{1}", endpoint.UrlBase, invoiceId))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        Dim response = client.ExecuteAsync(Of ServiceResponse(Of AnnullateFolio))(req)
        response.Wait()
        Return response.Result.Data
    End Function

    ''' <summary>
    ''' Lista las entidades administradoras de salud
    ''' </summary>
    ''' <param name="type">tipo para filtrar</param>
    ''' <returns>Lista de entidades</returns>
    Function ListHealthAdministrator(Optional ByVal type As Integer = -1) As XPInstantFeedbackSource
        If type = -1 Then
            Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).ContractService.ListHealthAdministratorByStatus(True)
        Else
            Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).ContractService.ListHealthAdministratorByType(type)
        End If
    End Function

    ''' <summary>
    ''' Lista las entidades administradoras de salud exepto un tipo
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    Function ListHealthAdministratorNotType(type As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).ContractService.ListHealthAdministratorByNotType(type)
    End Function

    Function GetPrintgModeByIdRevenueControl(idRevenueControl As Integer) As Byte
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetPrintgModeByIdRevenueControl(idRevenueControl)
    End Function

    Public Async Function GetSettingsBillingByIdUnitOperative(ByVal OperatingUnitId As Integer) As Task(Of ActionResult(Of SettingsBilling))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSettingBillingByUnitOperativeFormAsync(OperatingUnitId)
    End Function

    ''' <summary>
    ''' servicio rest para separar el folio madre en paciente y aaseguradora
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="RevenueControlDetailId"></param>
    ''' <returns></returns>
    Public Async Function SeparateMasterAccountRestAsync(ByVal admissionNumber As String, ByVal RevenueControlDetailId As Integer) As Task(Of ActionResult(Of String))
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim client = New RestClient(String.Format("{0}/billing/SeparateAccount/{1}/{2}", endpoint.UrlBase, admissionNumber, RevenueControlDetailId))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        Dim response = Await client.ExecuteAsync(Of ServiceResponse(Of String))(req)
        Return New ActionResult(Of String) With {.StateResult = response.Data.Status, .Message = response.Data.Message}
    End Function

    ''' <summary>
    ''' servicio rest para unificar los folio paciente -aseguradora
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Async Function UnifyAccountRestAsync(ByVal admissionNumber As String) As Task(Of ActionResult(Of String))
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim client = New RestClient(String.Format("{0}/billing/UnifyAccount/{1}", endpoint.UrlBase, admissionNumber))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        Dim response = Await client.ExecuteAsync(Of ServiceResponse(Of String))(req)
        Return New ActionResult(Of String) With {.StateResult = response.Data.Status, .Message = response.Data.Message}
    End Function


    ''' <summary>
    ''' servicio rest para cerra folio paciente en 0
    ''' </summary>
    ''' <param name="RevenueControlDetailId"></param>
    ''' <returns></returns>
    Public Async Function CloseAccountRestAsync(ByVal RevenueControlDetailId As Integer) As Task(Of ActionResult(Of String))
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim client = New RestClient(String.Format("{0}/billing/CloseAccount/{1}", endpoint.UrlBase, RevenueControlDetailId))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        Dim response = Await client.ExecuteAsync(Of ServiceResponse(Of String))(req)
        Return New ActionResult(Of String) With {.StateResult = response.Data.Status, .Message = response.Data.Message}
    End Function

    ''' <summary>
    ''' servicio de validacion liquidacion folio
    ''' </summary>
    ''' <param name="listRevenueControlDetailIds"></param>
    ''' <returns></returns>
    Public Async Function ValidateLiquidateFolioAsync(ByVal listRevenueControlDetailIds As String) As Task(Of ActionResult(Of String))
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim client = New RestClient(String.Format("{0}/billing/ValidateLiquidateFolio/{1}", endpoint.UrlBase, listRevenueControlDetailIds))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        Dim response = Await client.ExecuteAsync(Of ServiceResponse(Of String))(req)
        If response?.Data Is Nothing Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = response?.StatusDescription}
        End If

        Return New ActionResult(Of String) With {.StateResult = response.Data.Status, .Message = response.Data.Message}
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Function ListHealthAdministratoyByCode(code As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).ContractService.ListHealthAdministratoyByCode(code)
    End Function

    Function ListInventoryProduct() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryProduct()
    End Function

    ''' <summary>
    ''' funcion para consultar a la tabla PorductServiceDetail por el Id del serviceOrderDetail
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    Function ListProductServiceDetail(ServiceOrderDetailId As Integer) As XPCollection(Of ProductServiceDetailXpo)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListProductServiceDetail(ServiceOrderDetailId)
    End Function

End Class

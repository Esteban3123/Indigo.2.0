'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.ContractRepostory
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports DevExpress.Data.PLinq
Imports System.Configuration

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class BillingServiceXpo
    Inherits XpoBaseService

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(Company As String)
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO

        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, Company))
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        'cargo el protocolo
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    End Sub

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

#End Region

#Region "Public Methods"
    ''' <summary>
    ''' Lista todos los grupos de facturacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllServiceOrder() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ServiceOrderXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;AdmissionNumber;PatientCode;OrderDate;Status;StatusName;EntityName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the invoice categories.
    ''' </summary>
    ''' <returns></returns>
    Function ListInvoiceCategories() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(InvoiceCategoriesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the invoice.
    ''' </summary>
    ''' <returns></returns>
    Function ListInvoice() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(InvoicesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the invoice.
    ''' </summary>
    ''' <returns></returns>
    Function ListViewListInvoiceAndPatient() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BillingViewListInvoiceAndPatientXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function GetCategoriesByStatusAndUser(status As Boolean, UserId As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And InvoiceCategoriesUserXpo[UserId=" & UserId & "]")
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(InvoiceCategoriesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber As String, listId As List(Of Integer)) As XPInstantFeedbackSource
        Dim InCriteria As String = String.Join(",", listId.ToArray)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Id] in (" & InCriteria & ")")
        'Dim op As CriteriaOperator = New UnaryOperator(UnaryOperatorType.Not, criteria)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Not [Id] in (" & InCriteria & ") And RecordType = 1  And ServiceOrderId.Status <> 3 And Packaging = 0 And InvoicedQuantity > 0  And ServiceOrderId.AdmissionNumber = " & admissionNumber)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ServiceOrderDetailXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    'Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(listId As List(Of Integer)) As XPInstantFeedbackSource
    '    Dim sessionNew = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Id] not in (?)", String.Join(",", listId))
    '    classEntity = sessionNew.GetClassInfo(GetType(ServiceOrderDetailXpo))
    '    serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    '    Return serverMode
    'End Function

    Public Function ListServiceOrderDetailSurgicalByServiceOrderDetailId(serviceOrderDetailId As Integer) As XPInstantFeedbackSource
        Dim sessionNew As New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[ServiceOrderDetailId].Id = " & serviceOrderDetailId & "")
        classEntity = sessionNew.GetClassInfo(GetType(ServiceOrderDetailSurgicalXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListServiceOrderDetailBySurgeryNumberAndServiceOrderId(surgeryNumber As Byte, serviceOrderId As Integer) As XPCollection(Of ServiceOrderDetailXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[SurgeryNumber]=" & surgeryNumber & " AND [SurgeryNumber] > 0 AND IsDelete = False AND Presentation = 2 AND [ServiceOrderId].Id = " & serviceOrderId)
        Dim collect As XPCollection(Of ServiceOrderDetailXpo) = New XPCollection(Of ServiceOrderDetailXpo)(sessionNew, criteria)
        For Each item In collect
            item.FolioNumbers = String.Join("-", item.ServiceOrderDetailDistributions.Select(Function(o) o.FolioNumber).ToArray)
        Next
        Return collect
    End Function

    ''' <summary>
    ''' lista los detalles de la orden 
    ''' </summary>
    ''' <param name="serviceOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListServiceOrderDetailByServiceOrderId(serviceOrderId As Integer) As XPCollection(Of ServiceOrderDetailXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[ServiceOrderId].Id = " & serviceOrderId & " and IsDelete = 0")
        Dim collect As XPCollection(Of ServiceOrderDetailXpo) = New XPCollection(Of ServiceOrderDetailXpo)(sessionNew, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista los motivos de anulación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAnnulmentReason() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BillingReversalReasonXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las facturas a entidades capitadas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListInvoiceEntityCapitated() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(InvoiceEntityCapitatedXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las autorizaciones de facturación por código de usuario
    ''' </summary>
    ''' <param name="userCode">The user code.</param>
    ''' <returns></returns>
    Public Function ListAllBillingAuthorizationByUserCode(ByVal userCode As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Billing_BillingAuthorizationUsers[UserCode = ?] And Status = True", userCode.Trim())
        classEntity = sessionNew.GetClassInfo(GetType(BillingAuthorizationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ResolutionNumber;ResolutionDate;InvoicePrefix;InitialInvoice;FinalInvoice;InvoiceType;Consecutive;Status;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the type of the service order detail by admission number record.
    ''' </summary>
    Public Function ListServiceOrderDetailByAdmissionNumberRecordType(AdmissionNumber As String, recordType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Billing_ServiceOrderDetails[ServiceOrderId.AdmissionNumber = ?] And Billing_ServiceOrderDetails[RecordType = ?]", AdmissionNumber, recordType)
        classEntity = sessionNew.GetClassInfo(GetType(CUPSEntityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;RIPSCode;RIPSDescription;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los grupos de facturacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BillingGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las boletas de salida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSlipOut() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(SlipOutXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;AdmissionNumber", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las autorizaciones de facturación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingAuthotization() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BillingAuthorizationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los grupos de facturacion por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(BillingGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las facturas por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvoiceByAdmissionStatus(status As Integer, admissionNumber As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And AdmissionNumber=" & admissionNumber)
        classEntity = sessionNew.GetClassInfo(GetType(InvoiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;DocumentType;DocumentTypeName;InvoiceNumber;AdmissionNumber;PatientCode;InvoiceDate;TotalInvoice;NumberDocument;InvoiceCategoryId.CodeName", criteria)
        Return serverMode
    End Function

    Public Function ListInvoiceByStatus(status As Byte) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(InvoiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;DocumentType;DocumentTypeName;InvoiceNumber;AdmissionNumber;PatientCode;InvoiceDate;TotalInvoice;NumberDocument;Status;FolioNumber", criteria)
        Return serverMode
    End Function

    Function ListInvoiceByStatusAndPatientCode(status As Integer, PatientCode As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And PatientCode = '" & PatientCode & "'")
        classEntity = sessionNew.GetClassInfo(GetType(InvoiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;DocumentType;DocumentTypeName;InvoiceNumber;AdmissionNumber;PatientCode;InvoiceDate;TotalInvoice;NumberDocument;Status;FolioNumber", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las facturas por numero de factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewNoSurgicalByInvoiceId(InvoiceId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("InvoiceId=" & InvoiceId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewListNoSurgical), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista el detalle para el reporte de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewInvoiceDetailByInvoiceId(InvoiceId As Integer) As XPCollection(Of BillingVReportInvoiceDetail)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id =" & InvoiceId)
        Dim collect As XPCollection(Of BillingVReportInvoiceDetail) = New XPCollection(Of BillingVReportInvoiceDetail)(sessionNew, criteria)
        collect.Sorting.Add(New SortProperty("ServiceDate", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' lista para el reporte de facturas ------------------------------------------------
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewInvoiceFilters(Filter As String) As XPCollection(Of InvoicesXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(Filter)
        Dim collect As XPCollection(Of InvoicesXpo) = New XPCollection(Of InvoicesXpo)(sessionNew, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista el header para el reporte de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewInvoiceByInvoiceId(InvoiceId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id =" & InvoiceId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BillingVReportInvoice), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista las facturas por numero de factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewSurgicalAndPackageByInvoiceId(InvoiceId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("InvoiceId=" & InvoiceId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewListSurgicalAndPackage), criteria)
        Return collect
    End Function

    Public Function ListRevenueControlCollection(id As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RevenueControlDetailId=" & id)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewListServiceOrderDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio que aún no se ha facturado
    ''' Lista los detalles de un folio o una facturado
    ''' </summary>
    ''' <param name="id">Id del RevenueControlDetail</param>
    Public Function ListRevenueControl(ByVal id As Integer) As PLinqServerModeSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableView As XPQuery(Of ViewListServiceOrderDetailXpo) = New XPQuery(Of ViewListServiceOrderDetailXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableView Where T1.RevenueControlDetailId = id Select T1

        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio que ya ha sido anulado previamente
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRevenueControlForAnnullateInvoice(ByVal invoiceId As Integer) As PLinqServerModeSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableView As XPQuery(Of ViewListRevenueControlForAnnullateInvoiceXpo) = New XPQuery(Of ViewListRevenueControlForAnnullateInvoiceXpo)(sessionNew)
        Dim TmpQueryableSource = From T1 In tableView Where T1.InvoiceId = invoiceId Select T1

        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Informes"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

#End Region

End Class

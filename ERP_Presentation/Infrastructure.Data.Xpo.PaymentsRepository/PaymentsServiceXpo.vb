'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.PaymentsRepostory
' Author           : Carlos Mario Arias Rubiano
' Created          : 04-04-2014
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
Imports System.Configuration
Imports DevExpress.Data.PLinq

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class PaymentsServiceXpo
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
    ''' lista todas las notas de pago
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPaymentsNotesReportFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentsPaymentNotes))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;NoteDate;Status;Nature", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los traslados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTransfersReportPaymentsFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentsPaymentTransfer))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;DocumentDate;ThirdPartyId.NitName;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las facturas por filtro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsAccountPayableReportByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentsAccountPayable))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;BillNumber;DocumentDate;IdThirdParty.Nit;IdThirdParty.Name", criteria)
        serverMode.DefaultSorting = "BillNumber"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuotas de las facturas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSharesWithAccountPayable(ByVal IdSupplier As Integer, ByVal Status As Byte, TransferType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator

        If TransferType = 1 Then 'Mismo proveedor
            criteria = CriteriaOperator.Parse("IdAccountPayable.IdSupplier.Id=" & IdSupplier & " and IdAccountPayable.Status=" & Status & " and Balance > 0")
        Else 'Diferente proveedor
            criteria = CriteriaOperator.Parse("IdAccountPayable.Status=" & Status & " and Balance > 0")
        End If

        classEntity = sessionNew.GetClassInfo(GetType(AccountPayableSharesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSupplierReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(Maintenance_Supplier))
        serverMode = New XPInstantFeedbackSource(classEntity, "IdThirdParty.Nit;Name;IdThirdParty.Name;IdCity.Name;Status", Nothing)
        serverMode.DefaultSorting = "IdThirdParty.Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsAccountPayableReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentsAccountPayable))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;BillNumber;DocumentDate;IdThirdParty.Nit;IdThirdParty.Name", Nothing)
        serverMode.DefaultSorting = "BillNumber"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListThirdPartyReportPayments() As XPCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.CommonThirdPartyXpo)
        Dim collect As XPCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.CommonThirdPartyXpo) = New XPCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.CommonThirdPartyXpo)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los Traslados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTransfersReportPayments() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentsPaymentTransfer))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;DocumentDate;ThirdPartyId.NitName;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los Traslados de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableTransfer() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AccountPayableTransferXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Status;StatusName;FilingUnitSourceId.CodeName;FilingUnitTargetId.CodeName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las notas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsNotesReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentsPaymentNotes))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;NoteDate;Status;Nature", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDepencies() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DependencyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentTransfer() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentTransferXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.CodeName;SupplierId.IdThirdParty.Nit;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInitialBalance() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(InitialBalanceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Observations;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsAccountsPayable() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ConceptsAccountPayableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ConceptTypeName;AccountNumberName;HandlesRetentionName;RetentionConceptCodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsAccountsPayableByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ConceptsAccountPayableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado y si maneja retencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsAccountsPayableByStatusAndHandlesRetention(status As Boolean, handlesRetention As Boolean, Optional session As DevExpress.Xpo.Session = Nothing) As XPCollection
        If session Is Nothing Then
            session = New Session(XpoDefault.DataLayer)
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And HandlesRetention=" & handlesRetention)
        Dim collect As XPCollection = New XPCollection(session, GetType(ConceptsAccountPayableXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista las cuentas por pagar que tenga asociado la unidad de radicacion
    ''' </summary>
    ''' <param name="filingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableByFilingUnitId(filingUnitId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status<>3 And FilingUnitId=" & filingUnitId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(AccountPayableXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos las unidades de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFilingUnit() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(FilingUnitXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFilingUnitByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(FilingUnitXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFilingUnitByStatusCollection(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(FilingUnitXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFilingUnitData() As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(FilingUnitXpo))
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableConceptByHandlesRetention(handlesRetention As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("HandlesRetention=" & handlesRetention & "")
        classEntity = sessionNew.GetClassInfo(GetType(ConceptsAccountPayableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HandlesRetention;HandlesRetentionName;ConceptTypeName;RetentionConceptCodeName;AccountNumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayable() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AccountPayableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;BillNumber;DocumentDate;IdSupplier.Name;IdSupplier.IdThirdParty.Nit;BillDate;Value;Balance;ExpirationDate;CodeBillNumber;StatusName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayablebysupplier(IdSupplier As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdSupplier.Id=" & IdSupplier & "")
        classEntity = sessionNew.GetClassInfo(GetType(AccountPayableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;BillNumber;DocumentDate;IdSupplier.Name;IdSupplier.IdThirdParty.Nit;BillDate;Value;Balance;ExpirationDate;CodeBillNumber;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista la  trazabilidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTrazability(AccountPayableCode As String, BillNumber As String) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AccountPayableCode='" & AccountPayableCode & "' AND BillNumber='" & BillNumber & "'")
        '  classEntity = sessionNew.GetClassInfo(GetType(Payments_VTrazabilityPayments))
        Dim serverMode = New XPCollection(sessionNew, GetType(Payments_VTrazabilityPayments), criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las cxp por id del proveedor y estado para el formulario de traslados de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableBySupplierIdAndStatusForTransfer(ByVal supplierId As Integer, ByVal filingUnitId As Integer) As PLinqServerModeSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableView As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(sessionNew)

        Dim TmpQueryableSource = (From T1 In tableView
                                  Where T1.IdSupplier.Id = supplierId AndAlso T1.FilingUnitId.Id = filingUnitId AndAlso T1.Status = 1
                                  Select T1.Code, T1.DocumentDate).Distinct

        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentNotes() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(NotesDebitCreditXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Nature;Status;IndicatesBillAdvance;IdSupplier.Name;IdSupplier.IdThirdParty.Nit;StatusName", Nothing)
        Return serverMode
    End Function


    ''' <summary>
    ''' Gets the account payable by identifier xpo.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountPayableByBillNumber(ByVal listBillNumber As List(Of String)) As XPCollection
        Dim invoiceNumber As String = String.Empty
        For i As Integer = 0 To listBillNumber.Count - 1 Step 1
            invoiceNumber &= "'" & listBillNumber(i) & "'"
            If i <> listBillNumber.Count - 1 Then
                invoiceNumber &= ","
            End If
        Next
        'Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BillNumber in (" & invoiceNumber & ")")
        'classEntity = sessionNew.GetClassInfo(GetType(AccountPayableXpo))
        Dim collection = New XPCollection(GetType(AccountPayableXpo), criteria)
        Return collection
    End Function

    ' ''' <summary>
    ' ''' Lista los anticipos que tiene el proveedor
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function ListAdvancePayments(ByVal idSupplier As Integer, ByVal Status As Boolean) As XPInstantFeedbackSource
    '    Dim sessionNew = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdSupplier.Id=" & idSupplier & " And Status=" & Status & "")
    '    classEntity = sessionNew.GetClassInfo(GetType(AdvancePaymentsXpo))
    '    serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Value;Balance", criteria)
    '    Return serverMode
    'End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsNotes() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ConceptsNotesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableRejectionReason() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AccountPayableRejectionReasonXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsNotesByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ConceptsNotesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota por estado y tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsNotesByConcepType(status As Boolean, conceptType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ConceptType=" & conceptType)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentsAccountPayableConceptNotesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRoutes() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(RouteXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los anticipos para utilizarlos en el recibo de caja
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdvancePaymentsCashReceipt(idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If idCostCenter = 0 Then
            criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 And IdSupplier.IdThirdParty.Id=" & idThirdParty & " AND IdAccount.Id=" & idMainAccount)
        Else
            criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 And IdSupplier.IdThirdParty.Id=" & idThirdParty & " AND IdAccount.Id=" & idMainAccount & " AND IdCostCenter =" & idCostCenter)
        End If
        classEntity = sessionNew.GetClassInfo(GetType(AdvancePaymentsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuotas con facturas por id del proveedor y estado de la factura
    ''' </summary>
    ''' <param name="supplierId">The supplier identifier.</param>
    ''' <param name="status">The status.</param>
    ''' <returns></returns>
    Public Function ListAccountPayableSharesBySupplierIdAndState(supplierId As Integer, status As Byte) As XPCollection
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdAccountPayable.IdSupplier.Id=" & supplierId & " AND IdAccountPayable.Balance > 0 AND Balance > 0 AND IdAccountPayable.Status=" & status)
        Dim collect As XPCollection = New XPCollection(GetType(AccountPayableSharesXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de cxp por estado, bandera de si maneja retencion y tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableConceptByHandlesRetentionAndConceptType(status As Boolean, handlesRetention As Boolean, conceptType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And HandlesRetention=" & handlesRetention & " And ConceptType=" & conceptType)
        classEntity = sessionNew.GetClassInfo(GetType(ConceptsAccountPayableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HandlesRetention;ConceptType;IdAccount.Number;IdAccount.Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de cxp por estado y tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableConceptByConceptType(status As Boolean, conceptType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ConceptType=" & conceptType)
        classEntity = sessionNew.GetClassInfo(GetType(ConceptsAccountPayableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HandlesRetention;ConceptType", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista para el reporte de AccountPayableDetailConceptLiquidation ------------------------------------------------
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableDetailConceptLiquidationByAccountPayableDetailConceptId(Filter As String) As XPCollection(Of PaymentsAccountPayableConceptLiquidationXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(Filter)
        Dim collect As XPCollection(Of PaymentsAccountPayableConceptLiquidationXpo) = New XPCollection(Of PaymentsAccountPayableConceptLiquidationXpo)(sessionNew, criteria)
        Return collect
    End Function

#End Region

#Region "LinqInstantFeedBackSource Methods"

#Region "GetAccountPayableXpoById"
    Private WithEvents vlinqAccountPayableShareXpo As New LinqInstantFeedbackSource
    Private _filterId As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetAccountPayableXpoById(Id As Integer) As LinqInstantFeedbackSource
        _filterId = Id
        vlinqAccountPayableShareXpo.KeyExpression = "Id"
        Return vlinqAccountPayableShareXpo
    End Function

    Private Sub OnGetQueryableAccountPayXpo(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayableShareXpo.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableAccountPayableShare As XPQuery(Of AccountPayableSharesXpo) = New XPQuery(Of AccountPayableSharesXpo)(sessionNew)
            Dim TmpQueryableSource = From APS In tableAccountPayableShare
                                     Where APS.Id = _filterId
                                     Select APS
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayableShare
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountPayXpo(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayableShareXpo.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ListAccountPayableSharesByIdThirdIdAccountAndState"
    Private WithEvents vlinqAccountPayableShare As New LinqInstantFeedbackSource
    Private _filterIdThird As Integer
    Private _filterIdAccount As Integer
    Private _filterStatus As Byte
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableSharesByIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, Status As Byte) As LinqInstantFeedbackSource
        _filterIdThird = IdThird
        _filterIdAccount = IdAccount
        _filterStatus = Status
        vlinqAccountPayableShare.KeyExpression = "Id"
        Return vlinqAccountPayableShare
    End Function

    Private Sub OnGetQueryableEntityBankAccount(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayableShare.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableAccountPayableShare As XPQuery(Of AccountPayableSharesXpo) = New XPQuery(Of AccountPayableSharesXpo)(sessionNew)
            Dim tableAccountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(sessionNew)
            Dim tableSupplier As XPQuery(Of Maintenance_Supplier) = New XPQuery(Of Maintenance_Supplier)(sessionNew)
            Dim zero As Decimal = 0
            Dim TmpQueryableSource = From APS In tableAccountPayableShare
                                     Join AP In tableAccountPayable On AP.Id Equals APS.IdAccountPayable.Id
                                     Join S In tableSupplier On AP.IdSupplier.Id Equals S.Id
                                     Where S.IdThirdParty.Id = _filterIdThird And AP.IdAccount.Id = _filterIdAccount And AP.Status = _filterStatus And APS.Balance > zero
                                     Select APS
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayable
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableEntityBankAccount(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayableShare.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ListAccountPayableByStateAndNature LinqInstantFeedBackSource"

    'Public Function ListAccountPayableByStateAndNature(state As Integer, nature As Integer) As LinqInstantFeedbackSource
    '    Throw New NotImplementedException
    'End Function
    Private WithEvents vlinqAccountPayableByState As New LinqInstantFeedbackSource
    Private _filterStateBill As Integer
    Private _filterNatur As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableByStateAndNature(StatusBill As Byte, Nature As Byte) As LinqInstantFeedbackSource
        _filterStateBill = StatusBill
        _filterNatur = Nature
        vlinqAccountPayableByState.KeyExpression = "Id"
        Return vlinqAccountPayableByState
    End Function

    Private Sub OnGetQueryableAccountPayableByState(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayableByState.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableAccountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(sessionNew)
            Dim TmpQueryableSource = Nothing
            If _filterNatur = 1 Then
                TmpQueryableSource = From AP In tableAccountPayable
                                     Where AP.Status = _filterStateBill And AP.Balance > 0
                                     Select AP
            ElseIf _filterNatur = 2 Then
                TmpQueryableSource = From AP In tableAccountPayable
                                     Where AP.Status = _filterStateBill And AP.Balance >= 0
                                     Select AP
            End If
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayable
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountPayableByState(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayableByState.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "AccountPayable LinqInstantFeedBackSource"
    Private WithEvents vlinqAccountPayable As New LinqInstantFeedbackSource
    Private _filterIdSupplier As Integer
    Private _filterStatusBill As Integer
    Private _filterNature As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableByIdSupplierAndState(IdSupplier As Integer, StatusBill As Byte, Nature As Byte) As LinqInstantFeedbackSource
        _filterIdSupplier = IdSupplier
        _filterStatusBill = StatusBill
        _filterNature = Nature
        vlinqAccountPayable.KeyExpression = "Id"
        Return vlinqAccountPayable
    End Function

    Private Sub OnGetQueryableAccountPayable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayable.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableAccountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(sessionNew)
            Dim TmpQueryableSource = Nothing
            If _filterNature = 1 Then
                TmpQueryableSource = From AP In tableAccountPayable
                                     Where AP.IdSupplier.Id = _filterIdSupplier And AP.Status = _filterStatusBill And AP.Balance > 0
                                     Select AP
            ElseIf _filterNature = 2 Then
                TmpQueryableSource = From AP In tableAccountPayable
                                     Where AP.IdSupplier.Id = _filterIdSupplier And AP.Status = _filterStatusBill And AP.Balance >= 0
                                     Select AP
            End If
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayable
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountPayable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayable.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "AdvancePayments LinqInstantFeedBackSource"
    Private WithEvents vlinqAdvancePayments As New LinqInstantFeedbackSource
    Private _filterIdSupplierAdvance As Integer
    Private _filterStatusAdvance As Integer
    Private _filterNatureAdvance As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAdvancePayments(IdSupplierAdvance As Integer, StatusAdvance As Byte, NatureAdvance As Byte) As LinqInstantFeedbackSource
        _filterIdSupplierAdvance = IdSupplierAdvance
        _filterStatusAdvance = StatusAdvance
        _filterNatureAdvance = NatureAdvance
        vlinqAdvancePayments.KeyExpression = "Id"
        Return vlinqAdvancePayments
    End Function

    Private Sub OnGetQueryableAdvancePayments(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAdvancePayments.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableAccountPayable As XPQuery(Of AdvancePaymentsXpo) = New XPQuery(Of AdvancePaymentsXpo)(sessionNew)
            Dim TmpQueryableSource = Nothing
            If _filterNatureAdvance = 1 Then
                TmpQueryableSource = From AP In tableAccountPayable
                                     Where AP.IdSupplier.Id = _filterIdSupplierAdvance And AP.Status = _filterStatusAdvance And AP.Balance > 0
                                     Select AP
            ElseIf _filterNatureAdvance = 2 Then
                TmpQueryableSource = From AP In tableAccountPayable
                                     Where AP.IdSupplier.Id = _filterIdSupplierAdvance And AP.Status = _filterStatusAdvance And AP.Balance >= 0
                                     Select AP
            End If
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayable
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAdvancePayments(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAdvancePayments.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ListAccountPayableSharesByIdSupplierAndState"
    Private WithEvents vListAccountPayableSharesByIdSupplierAndState As New LinqInstantFeedbackSource
    Private _filterIdSupplierShare As Integer
    Private _filterStatusShare As Byte
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableSharesByIdSupplierAndState(IdSupplier As Integer, Status As Byte) As LinqInstantFeedbackSource
        _filterIdSupplierShare = IdSupplier
        _filterStatusShare = Status
        vlinqAccountPayableShare.KeyExpression = "Id"
        Return vListAccountPayableSharesByIdSupplierAndState
    End Function

    Private Sub OnGetQueryableShares(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vListAccountPayableSharesByIdSupplierAndState.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableAccountPayableShare As XPQuery(Of AccountPayableSharesXpo) = New XPQuery(Of AccountPayableSharesXpo)(sessionNew)
            Dim tableAccountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(sessionNew)
            Dim zero As Decimal = 0
            Dim TmpQueryableSource = From APS In tableAccountPayableShare
                                     Join AP In tableAccountPayable On AP.Id Equals APS.IdAccountPayable.Id
                                     Where AP.IdSupplier.Id = _filterIdSupplierShare And AP.Balance > 0 And AP.Status = _filterStatusShare And APS.Balance > 0
                                     Select APS
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayableShare
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableShares(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vListAccountPayableSharesByIdSupplierAndState.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Invoice Accpetence Transfer"


    'Private WithEvents vlinqTransfer As New LinqInstantFeedbackSource

    ' ''' <summary>
    ' ''' Obtiene todos los radicados
    ' ''' </summary>
    'Public Function ListAccountPayableTransferAcceptence() As LinqInstantFeedbackSource
    '    vlinqTransfer.KeyExpression = "Id"
    '    Return vlinqTransfer
    'End Function

    'Private Sub OnGetQueryableList(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqTransfer.GetQueryable
    '    Try
    '        Dim sessionNew As New Session(XpoDefault.DataLayer)
    '        Dim tableTransfer As XPQuery(Of PaymentsAccountPayable) = New XPQuery(Of PaymentsAccountPayable)(sessionNew)
    '        Dim TmpQueryableSource1 = From T1 In tableTransfer
    '                                Select New With {.id = T1.Id, .ciode = T1.Code}
    '        e.QueryableSource = TmpQueryableSource1
    '        e.Tag = tableTransfer
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub DismissQueryableList(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqTransfer.DismissQueryable
    '    Try
    '        'Dispose of the DataContext 
    '        CType(e.Tag, Object).Dispose()
    '    Catch ex As Exception
    '        ex.Message.ToString()
    '    End Try
    'End Sub
    'Private WithEvents vlinqConciliation As New LinqInstantFeedbackSource
    'Private AccountPayableTransferId As Integer
    ' ''' <summary>
    ' ''' Obtiene todos los empleados
    ' ''' </summary>
    'Public Function ListPaymentsAccountPayableTransferDetail(ByVal _AccountPayableTransferId As Integer) As LinqInstantFeedbackSource
    '    vlinqConciliation.KeyExpression = "Id"
    '    AccountPayableTransferId = _AccountPayableTransferId
    '    Return vlinqConciliation
    'End Function

    'Private Sub OnGetQueryableConciliationD(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqConciliation.GetQueryable
    '    Try
    '        Dim sessionNew = New Session(XpoDefault.DataLayer)
    '        Dim tableObjD As XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo) = New XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo)(sessionNew)
    '        Dim TmpQueryableSource = From T1 In tableObjD
    '                                 Where T1.AccountPayableTransferId.Id = AccountPayableTransferId
    '                                Select New With {.id = T1.Id, .AccountPayableCode = T1.AccountPayableId.Code, .NitName = T1.AccountPayableId.IdThirdParty.NitName, .State = If(T1.Status = "1", "Pendiente por Aceptacion", If(T1.Status = "2", "Aceptada", If(T1.Status = "3", "Rechazada", "Anulada")))}
    '        e.QueryableSource = TmpQueryableSource
    '        e.Tag = tableObjD
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub DismissQueryableConciliationD(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqConciliation.DismissQueryable
    '    Try
    '        'Dispose of the DataContext 
    '        CType(e.Tag, Object).Dispose()
    '    Catch ex As Exception
    '        ex.Message.ToString()
    '    End Try
    'End Sub

    ''' <summary>
    ''' Lista las cabecera de oficio de traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableTransferAcceptence(ByVal listFillingUnitId As List(Of Integer)) As PLinqServerModeSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim statetmp As Integer = 2 'confirmados
        Dim tableTransfer As XPQuery(Of AccountPayableTransferXpo) = New XPQuery(Of AccountPayableTransferXpo)(sessionNew)
        Dim TmpDocumentype As String = 1
        Dim TmpQueryableSource = From T1 In tableTransfer
                                 Where T1.Status = statetmp And listFillingUnitId.Contains(T1.FilingUnitTargetId.Id)
                  Select New With {T1.Id, T1.Code, .FilingSource = T1.FilingUnitSourceId.Name, .FilingTarget = T1.FilingUnitTargetId.Name, T1.CreationDate, .IdTarget = T1.FilingUnitTargetId.Id, T1.CreationUser}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

    ''' <summary>
    ''' Lista el detalle de un oficio de traslado
    ''' </summary>
    ''' <param name="_AccountPayableTransferId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsAccountPayableTransferDetail(ByVal _AccountPayableTransferId As Integer) As PLinqServerModeSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim tableTransfer As XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo) = New XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo)(sessionNew)
        Dim TmpDocumentype As String = 1
        Dim TmpQueryableSource = From T1 In tableTransfer
                               Where T1.AccountPayableTransferId.Id = _AccountPayableTransferId
                                       Select New With {.id = T1.Id, .AccountPayableCode = T1.AccountPayableId.Code, .NitName = T1.AccountPayableId.IdThirdParty.NitName, .State = If(T1.Status = "1", "Pendiente por Aceptacion", If(T1.Status = "2", "Aceptada", If(T1.Status = "3", "Rechazada", "Anulada"))), .BillNumber = T1.AccountPayableId.BillNumber}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "AccountPayableDetailConceptWithDeferredCausation LinqInstantFeedBackSource"
    Private WithEvents vlinqAccountPayableDetailConcept As New LinqInstantFeedbackSource
    Private _filterSupplierId As Integer
    Private _filterStatusAP As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableByIdSupplierAndStateWithDeferredCausation(SupplierId As Integer, Status As Integer) As LinqInstantFeedbackSource
        _filterSupplierId = SupplierId
        _filterStatusAP = Status
        vlinqAccountPayableDetailConcept.KeyExpression = "Id"
        Return vlinqAccountPayableDetailConcept
    End Function

    Private Sub OnGetQueryableAccountPayableDetailConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayableDetailConcept.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableAccountPayableDetailConcept As XPQuery(Of PaymentsAccountPayableDetailConceptXpoP) = New XPQuery(Of PaymentsAccountPayableDetailConceptXpoP)(sessionNew)
            Dim TmpQueryableSource = Nothing
            TmpQueryableSource = From AP In tableAccountPayableDetailConcept
                                     Where AP.IdAccountPayable.IdSupplier.Id = _filterSupplierId And AP.IdAccountPayable.Status = _filterStatusAP And AP.DeferredCausation = True
                                     Select AP
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayableDetailConcept
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountPayableDetailConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountPayableDetailConcept.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#End Region

#Region "Reports"

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

    ''' <summary>
    ''' Funcion para cargar el datasource del reporte de extracto de cuentas por pagar
    ''' </summary>
    ''' <param name="INDFechaIni">fecha inicial</param>
    ''' <param name="INDFechaEnd">fecha final</param>
    ''' <param name="INDSupplierStart">proveedor inicial</param>
    ''' <param name="INDSupplierEnd">proveedor final</param>
    ''' <param name="INDInvoiceStart">factura inicial</param>
    ''' <param name="INDInvoiceEnd">factura final</param>
    ''' <returns>lista de VReportExtractAccountPayable</returns>
    ''' <remarks></remarks>
    Public Function GetCollectionReportExtractsAccountsByPay(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDSupplierStart As String, ByVal INDSupplierEnd As String, ByVal INDInvoiceStart As String, ByVal INDInvoiceEnd As String) As List(Of VReportExtractAccountPayable)
        Dim resulXpo As IList(Of VReportExtractAccountPayable)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate(MovesDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(MovesDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "#"
        'filtro por terceros
        If INDSupplierStart IsNot Nothing And INDSupplierEnd IsNot Nothing Then
            criteria &= "AND ThirdPartyNit >= '" & INDSupplierStart & "' AND ThirdPartyNit <= '" & INDSupplierEnd & "'"
        End If
        'filtro por cuentas
        If INDInvoiceStart IsNot Nothing And INDInvoiceEnd IsNot Nothing Then
            criteria &= "AND BillNumber >= '" & INDInvoiceStart & "' AND BillNumber <= '" & INDInvoiceEnd & "'"
        End If

        resulXpo = Me.LoadCollection(Of VReportExtractAccountPayable)(XpoDefault.DataLayer, Nothing, criteria)
        Return resulXpo

    End Function

#End Region

End Class

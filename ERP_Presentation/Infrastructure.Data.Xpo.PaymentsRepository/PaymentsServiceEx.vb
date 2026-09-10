'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.PaymentsRepostory
' Author           : Carlos Mario Arias Rubiano
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports DevExpress.Data.PLinq

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class PaymentsServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"
    ''' <summary>
    ''' lista los documentos de control por tipo de documento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPaymentsControlByDocumentType(documentType As Integer) As XPCollection(Of PaymentsControlXpo)
        Dim session As New IndigoXPOSession(Of PaymentsControlXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DocumentType=" & documentType & "")
        Dim classEntity = session.GetClassInfo(GetType(PaymentsControlXpo))
        Return New XPCollection(Of PaymentsControlXpo)(session, criteria)
        'End Using
    End Function

    ''' <summary>
    ''' lista todas las notas de pago
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPaymentsNotesReportFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsPaymentNotes)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
            Dim classEntity = session.GetClassInfo(GetType(PaymentsPaymentNotes))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;NoteDate;Status;Nature", criteria)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los traslados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTransfersReportPaymentsFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsPaymentTransfer)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
            Dim classEntity = session.GetClassInfo(GetType(PaymentsPaymentTransfer))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;DocumentDate;ThirdPartyId.NitName;Status", criteria)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las facturas por filtro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsAccountPayableReportByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsAccountPayable)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
            Dim classEntity = session.GetClassInfo(GetType(PaymentsAccountPayable))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;BillNumber;DocumentDate;IdThirdParty.Nit;IdThirdParty.Name", criteria)
            serverMode.DefaultSorting = "BillNumber"
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista todos las autorizaciones de documento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDocumentSupportAuthorization() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DocumentSupportXpo)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(DocumentSupportXpo)),
                                                        "Id;Code;Name;Status;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las cuotas de las facturas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSharesWithAccountPayable(ByVal IdSupplier As Integer, ByVal Status As Byte, TransferType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountPayableSharesXpo)()
        Dim criteria As CriteriaOperator

            If TransferType = 1 Then 'Mismo proveedor
                criteria = CriteriaOperator.Parse("IdAccountPayable.IdSupplier.Id=" & IdSupplier & " and IdAccountPayable.Status=" & Status & " and Balance > 0")
            Else 'Diferente proveedor
                criteria = CriteriaOperator.Parse("IdAccountPayable.Status=" & Status & " and Balance > 0")
            End If

            Dim classEntity = session.GetClassInfo(GetType(AccountPayableSharesXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSupplierReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Maintenance_Supplier)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_Supplier))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CodeName;IdThirdParty.Nit;Name;IdThirdParty.Name;IdCity.Name;Status", Nothing)
            serverMode.DefaultSorting = "IdThirdParty.Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsAccountPayableReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsAccountPayable)()
        Dim classEntity = session.GetClassInfo(GetType(PaymentsAccountPayable))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;BillNumber;DocumentDate;IdThirdParty.Nit;IdThirdParty.Name;IdThirdParty.NitName", Nothing)
        serverMode.DefaultSorting = "BillNumber"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las facturas de un tercero sin distribuir (Nativo)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayablesWithoutDistribuitByIdThirdParty(ByVal IdThirdParty As Integer, ByVal CostDistributionDirectCostId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of VAccountPayablesWithoutDistribuitXpo)()
        Dim criteria As CriteriaOperator
            If CostDistributionDirectCostId = 0 Then
                criteria = CriteriaOperator.Parse("Status = 1 AND IdThirdParty=" & IdThirdParty & " AND CostDistributionDirectCostId IS NULL")
            Else
                criteria = CriteriaOperator.Parse("Status = 1 And IdThirdParty=" & IdThirdParty & " AND (CostDistributionDirectCostId IS NULL OR CostDistributionDirectCostId=" & CostDistributionDirectCostId & ")")
            End If

            Dim classEntity = session.GetClassInfo(GetType(VAccountPayablesWithoutDistribuitXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;BillNumber;BillDate;IdThirdParty;InvoiceValue;Coments", criteria)
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
        Dim session As New IndigoXPOSession(Of PaymentsPaymentTransfer)()
        Dim classEntity = session.GetClassInfo(GetType(PaymentsPaymentTransfer))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;DocumentDate;ThirdPartyId.NitName;Status", Nothing)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los Traslados de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableTransfer() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountPayableTransferXpo)()
        Dim classEntity = session.GetClassInfo(GetType(AccountPayableTransferXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Status;StatusName;FilingUnitSourceId.CodeName;FilingUnitTargetId.CodeName;TranferType", Nothing)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las notas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsNotesReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsPaymentNotes)()
        Dim classEntity = session.GetClassInfo(GetType(PaymentsPaymentNotes))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;NoteDate;Status;Nature", Nothing)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDepencies() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DependencyXpo)()
        Dim classEntity = session.GetClassInfo(GetType(DependencyXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentTransfer() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentTransferXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PaymentTransferXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.CodeName;SupplierId.IdThirdParty.Nit;Status;AdvancePaymentId;AdvancePaymentId.CurrencyAbbreviation", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInitialBalance() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InitialBalanceXpo)()
        Dim classEntity = session.GetClassInfo(GetType(InitialBalanceXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Observations;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsAccountsPayable() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConceptsAccountPayableXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ConceptsAccountPayableXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ConceptTypeName;AccountNumberName;HandlesRetentionName;RetentionConceptCodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsAccountsPayableByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConceptsAccountPayableXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
            Dim classEntity = session.GetClassInfo(GetType(ConceptsAccountPayableXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", criteria)
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
        Dim session As New IndigoXPOSession(Of AccountPayableXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("((Value = 0 And Balance = 0) Or (Value > 0 And Balance > 0)) And Status<>3 And FilingUnitId=" & filingUnitId)
        Dim collect As XPCollection = New XPCollection(session, GetType(AccountPayableXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos las unidades de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFilingUnit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FilingUnitXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FilingUnitXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFilingUnitByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FilingUnitXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
            Dim classEntity = session.GetClassInfo(GetType(FilingUnitXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFilingUnitByStatusCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of FilingUnitXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(session, GetType(FilingUnitXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFilingUnitData() As XPCollection
        Dim session As New IndigoXPOSession(Of FilingUnitXpo)()
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(session, GetType(FilingUnitXpo))
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableConceptByHandlesRetention(status As Boolean, handlesRetention As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConceptsAccountPayableXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And HandlesRetention=" & handlesRetention & "")
        Dim classEntity = session.GetClassInfo(GetType(ConceptsAccountPayableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HandlesRetention;HandlesRetentionName;ConceptTypeName;RetentionConceptCodeName;AccountNumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayable() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountPayableXpo)()
        Dim classEntity = session.GetClassInfo(GetType(AccountPayableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;BillNumber;DocumentDate;IdSupplier.Name;IdSupplier.IdThirdParty.Nit;BillDate;Value;Balance;ExpirationDate;CodeBillNumber;StatusName;Status;Abbreviation", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayablebysupplier(IdSupplier As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountPayableXpo)()
        Dim Criteria As CriteriaOperator
        If IdSupplier > 0 Then
            Criteria = CriteriaOperator.Parse("IdSupplier.Id=" & IdSupplier & "")
        Else
            Criteria = CriteriaOperator.Parse("Status = 2 AND (EntityId IS NULL OR CostDistributionDirectCostId IS NOT NULL OR  EntityName ='LoadMassive') AND Balance = Value")

        End If
        Dim classEntity = session.GetClassInfo(GetType(AccountPayableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;BillNumber;DocumentDate;IdSupplier.Name;IdSupplier.IdThirdParty.Nit;BillDate;Value;Balance;ExpirationDate;CodeBillNumber;StatusName;IdThirdParty.NitName;InvoiceValue;Abbreviation;CostDistributionDirectCostId", Criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista la  trazabilidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTrazability(AccountPayableCode As String, BillNumber As String) As XPCollection
        Dim session As New IndigoXPOSession(Of Payments_VTrazabilityPayments)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AccountPayableCode='" & AccountPayableCode & "' AND BillNumber='" & BillNumber & "'")
        '  Dim classEntity = session.GetClassInfo(GetType(Payments_VTrazabilityPayments))
        Dim serverMode = New XPCollection(session, GetType(Payments_VTrazabilityPayments), criteria)
        Return serverMode
        'End Using
    End Function

    ''' <summary>
    ''' Lista la  trazabilidad de reembolsos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRefundsTrazability(RefundCode As String) As XPCollection
        Dim session As New IndigoXPOSession(Of VTrazabilityRefundsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RefundCode='" & RefundCode & "'")
        Dim serverMode = New XPCollection(session, GetType(VTrazabilityRefundsXpo), criteria)
        Return serverMode
        'End Using
    End Function

    ''' <summary>
    ''' Lista las cxp por id del proveedor y estado para el formulario de traslados de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableBySupplierIdAndStatusForTransfer(ByVal supplierId As Integer, ByVal filingUnitId As Integer) As PLinqServerModeSource
        Dim session As New IndigoXPOSession(Of AccountPayableXpo)()
        Dim tableView As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(session)

        Dim TmpQueryableSource = (From T1 In tableView
                                  Where T1.IdSupplier.Id = supplierId AndAlso T1.FilingUnitId.Id = filingUnitId AndAlso T1.Status = 1
                                  Select T1.Code, T1.DocumentDate).Distinct

        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentNotes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of NotesDebitCreditXpo)()
        Dim classEntity = session.GetClassInfo(GetType(NotesDebitCreditXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Nature;Status;IndicatesBillAdvance;IdSupplier.Name;IdSupplier.IdThirdParty.Nit;StatusName", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista todas las cuentas por pagar con el valor a pagar.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentNotesDebitCredit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentNotesDebitCreditXPO)()
        Dim classEntity = session.GetClassInfo(GetType(PaymentNotesDebitCreditXPO))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Nature;Status;IndicatesBillAdvance;Name;IdThirdParty;Nit;StatusName;Value;CurrencyAbbreviation", Nothing)
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
        'Dim session = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BillNumber in (" & invoiceNumber & ")")
        'Dim classEntity = session.GetClassInfo(GetType(AccountPayableXpo))
        Dim collection = New XPCollection(GetType(AccountPayableXpo), criteria)
        Return collection
    End Function

    ' ''' <summary>
    ' ''' Lista los anticipos que tiene el proveedor
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function ListAdvancePayments(ByVal idSupplier As Integer, ByVal Status As Boolean) As XPInstantFeedbackSource
    '    Dim session = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdSupplier.Id=" & idSupplier & " And Status=" & Status & "")
    '    Dim classEntity = session.GetClassInfo(GetType(AdvancePaymentsXpo))
    '    Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Value;Balance", criteria)
    '    Return serverMode
    'End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsNotes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConceptsNotesXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ConceptsNotesXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", Nothing)
            Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableRejectionReason() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountPayableRejectionReasonXpo)()
        Dim classEntity = session.GetClassInfo(GetType(AccountPayableRejectionReasonXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsNotesByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConceptsNotesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
            Dim classEntity = session.GetClassInfo(GetType(ConceptsNotesXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota por estado y tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsNotesByConcepType(status As Boolean, conceptType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsAccountPayableConceptNotesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ConceptType=" & conceptType)
            Dim classEntity = session.GetClassInfo(GetType(PaymentsAccountPayableConceptNotesXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota por estado y tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableConceptNoteByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsAccountPayableConceptNotesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status)
            Dim classEntity = session.GetClassInfo(GetType(PaymentsAccountPayableConceptNotesXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRoutes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RouteXpo)()
        Dim classEntity = session.GetClassInfo(GetType(RouteXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los anticipos para utilizarlos en el recibo de caja
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdvancePaymentsCashReceipt(idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AdvancePaymentsXpo)()
        Dim criteria As CriteriaOperator
            If idCostCenter = 0 Then
                criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 And IdSupplier.IdThirdParty.Id=" & idThirdParty & " AND IdAccount.Id=" & idMainAccount)
            Else
                criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 And IdSupplier.IdThirdParty.Id=" & idThirdParty & " AND IdAccount.Id=" & idMainAccount & " AND IdCostCenter =" & idCostCenter)
            End If
            Dim classEntity = session.GetClassInfo(GetType(AdvancePaymentsXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los anticipos para utilizarlos en el recibo de caja
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableCashReceipt(idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsAccountPayable)()
        Dim criteria As CriteriaOperator
            If idCostCenter = 0 Then
                criteria = CriteriaOperator.Parse("Status = 2 And Value > Balance And IdSupplier.IdThirdParty.Id=" & idThirdParty & " AND IdAccount.Id=" & idMainAccount)
            Else
                criteria = CriteriaOperator.Parse("Status = 2 And Value > Balance And IdSupplier.IdThirdParty.Id=" & idThirdParty & " AND IdAccount.Id=" & idMainAccount & " AND IdCostCenter.Id =" & idCostCenter)
            End If
            Dim classEntity = session.GetClassInfo(GetType(PaymentsAccountPayable))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function


    ''' <summary>
    ''' Lista todas las cuotas con facturas por id del proveedor y estado de la factura
    ''' </summary>
    ''' <param name="supplierId">The supplier identifier.</param>
    ''' <param name="status">The status.</param>
    ''' <returns></returns>
    Public Function ListAccountPayableSharesBySupplierIdAndState(supplierId As Integer, status As Byte) As XPCollection
        Dim session As New IndigoXPOSession(Of AccountPayableSharesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdAccountPayable.IdSupplier.Id=" & supplierId & " AND IdAccountPayable.Balance > 0 AND Balance > 0 AND IdAccountPayable.Status=" & status)
        Dim collect As XPCollection = New XPCollection(session, GetType(AccountPayableSharesXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de cxp por estado, bandera de si maneja retencion y tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableConceptByHandlesRetentionAndConceptType(status As Boolean, handlesRetention As Boolean, conceptType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConceptsAccountPayableXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And HandlesRetention=" & handlesRetention & " And ConceptType=" & conceptType)
            Dim classEntity = session.GetClassInfo(GetType(ConceptsAccountPayableXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HandlesRetention;ConceptType;IdAccount.Number;IdAccount.Name;IdAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de cxp por estado y tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableConceptByConceptType(status As Boolean, conceptType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConceptsAccountPayableXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ConceptType=" & conceptType)
            Dim classEntity = session.GetClassInfo(GetType(ConceptsAccountPayableXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HandlesRetention;ConceptType", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista para el reporte de AccountPayableDetailConceptLiquidation ------------------------------------------------
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableDetailConceptLiquidationByAccountPayableDetailConceptId(Filter As String) As XPCollection(Of PaymentsAccountPayableConceptLiquidationXpo)
        Dim session As New IndigoXPOSession(Of PaymentsAccountPayableConceptLiquidationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(Filter)
        Dim collect As XPCollection(Of PaymentsAccountPayableConceptLiquidationXpo) = New XPCollection(Of PaymentsAccountPayableConceptLiquidationXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLoadMassive() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of LoadMassiveXpo)()
        Dim classEntity = session.GetClassInfo(GetType(LoadMassiveXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Observations;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las monedas en accountreceivable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsGroupByCurrencyXpo() As List(Of CommonCurrencyXpo)
        Dim session As New IndigoXPOSession(Of AccountPayableXpo)()
        Dim _accountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(session)
        Dim tmpQueryableSource = (From ar In _accountPayable
                                  Select ar.Currency)?.Distinct().ToList()
        Return tmpQueryableSource
    End Function

#End Region

#Region "LinqInstantFeedBackSource Methods"

#Region "GetAccountPayableXpoById"
    Private _filterId As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetAccountPayableXpoById(Id As Integer) As LinqInstantFeedbackSource
        Dim vlinqAccountPayableShareXpo As New LinqInstantFeedbackSource
        AddHandler vlinqAccountPayableShareXpo.GetQueryable, AddressOf OnGetQueryableAccountPayXpo
        AddHandler vlinqAccountPayableShareXpo.DismissQueryable, AddressOf DismissQueryableAccountPayXpo
        _filterId = Id
        vlinqAccountPayableShareXpo.KeyExpression = "Id"
        Return vlinqAccountPayableShareXpo
    End Function

    Private Sub OnGetQueryableAccountPayXpo(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAccountPayableShare As XPQuery(Of AccountPayableSharesXpo) = New XPQuery(Of AccountPayableSharesXpo)(session)
            Dim TmpQueryableSource = From APS In tableAccountPayableShare
                                     Where APS.Id = _filterId
                                     Select APS
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayableShare
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountPayXpo(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ListAccountPayableSharesByIdThirdIdAccountAndState"
    Private _filterIdThird As Integer
    Private _filterIdAccount As Integer
    Private _filterStatus As Byte
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableSharesByIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, Status As Byte) As LinqInstantFeedbackSource
        Dim vlinqAccountPayableShare As New LinqInstantFeedbackSource
        AddHandler vlinqAccountPayableShare.GetQueryable, AddressOf OnGetQueryableEntityBankAccount
        AddHandler vlinqAccountPayableShare.DismissQueryable, AddressOf DismissQueryableEntityBankAccount
        _filterIdThird = IdThird
        _filterIdAccount = IdAccount
        _filterStatus = Status
        vlinqAccountPayableShare.KeyExpression = "Id"
        Return vlinqAccountPayableShare
    End Function

    Private Sub OnGetQueryableEntityBankAccount(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAccountPayableShare As XPQuery(Of AccountPayableSharesXpo) = New XPQuery(Of AccountPayableSharesXpo)(session)
            Dim tableAccountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(session)
            Dim tableSupplier As XPQuery(Of Maintenance_Supplier) = New XPQuery(Of Maintenance_Supplier)(session)
            Dim zero As Decimal = 0
            Dim TmpQueryableSource = From APS In tableAccountPayableShare
                                     Join AP In tableAccountPayable On AP.Id Equals APS.IdAccountPayable.Id
                                     Join S In tableSupplier On AP.IdSupplier.Id Equals S.Id
                                     Where S.IdThirdParty.Id = _filterIdThird And AP.IdAccount.Id = _filterIdAccount And AP.Status = _filterStatus And APS.Balance > zero
                                     Select APS
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayable
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableEntityBankAccount(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ListAccountPayableByStateAndNature LinqInstantFeedBackSource"

    Private _filterStateBill As Integer
    Private _filterNatur As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableByStateAndNature(StatusBill As Byte, Nature As Byte) As LinqInstantFeedbackSource
        Dim vlinqAccountPayableByState As New LinqInstantFeedbackSource
        AddHandler vlinqAccountPayableByState.GetQueryable, AddressOf OnGetQueryableAccountPayableByState
        AddHandler vlinqAccountPayableByState.DismissQueryable, AddressOf DismissQueryableAccountPayableByState
        _filterStateBill = StatusBill
        _filterNatur = Nature
        vlinqAccountPayableByState.KeyExpression = "Id"
        Return vlinqAccountPayableByState
    End Function

    Private Sub OnGetQueryableAccountPayableByState(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAccountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(session)
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
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountPayableByState(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "AccountPayable LinqInstantFeedBackSource"
    Private _filterIdSupplier As Integer
    Private _filterStatusBill As Integer
    Private _filterNature As Integer
    Private _filterMainAccountIdDistributionLine As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableByIdSupplierAndState(IdSupplier As Integer, StatusBill As Byte, Nature As Byte, Optional MainAccountIdDistributionLine As Integer = 0) As LinqInstantFeedbackSource
        Dim vlinqAccountPayable As New LinqInstantFeedbackSource
        AddHandler vlinqAccountPayable.GetQueryable, AddressOf OnGetQueryableAccountPayable
        AddHandler vlinqAccountPayable.DismissQueryable, AddressOf DismissQueryableAccountPayable
        _filterIdSupplier = IdSupplier
        _filterStatusBill = StatusBill
        _filterNature = Nature
        _filterMainAccountIdDistributionLine = MainAccountIdDistributionLine
        vlinqAccountPayable.KeyExpression = "Id"
        Return vlinqAccountPayable
    End Function

    Private Sub OnGetQueryableAccountPayable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAccountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(session)
            Dim TmpQueryableSource = Nothing



            If _filterMainAccountIdDistributionLine > 0 Then 'Si viene desde el form de notas debito de cxp se filtra por el id del proveedor, el estado de la cxp, el saldo y la cuenta contable
                If _filterNature = 1 Then 'Debito
                    TmpQueryableSource = From AP In tableAccountPayable
                                         Where AP.IdSupplier.Id = _filterIdSupplier And AP.Status = _filterStatusBill And AP.IdAccount.Id = _filterMainAccountIdDistributionLine And AP.Balance > 0
                                         Select AP
                ElseIf _filterNature = 2 Then 'Credito
                    'TmpQueryableSource = From AP In tableAccountPayable
                    '                     Where AP.IdSupplier.Id = _filterIdSupplier And AP.Status = _filterStatusBill And AP.Balance >= 0 And AP.IdAccount.Id = _filterMainAccountIdDistributionLine
                    '                     Select AP
                    TmpQueryableSource = From AP In tableAccountPayable
                                         Where AP.IdSupplier.Id = _filterIdSupplier And AP.Status = _filterStatusBill And AP.IdAccount.Id = _filterMainAccountIdDistributionLine
                                         Select AP
                End If
            Else 'Se llama desde voucherTransactionCrossing
                If _filterNature = 1 Then
                    TmpQueryableSource = From AP In tableAccountPayable
                                         Where AP.IdSupplier.Id = _filterIdSupplier And AP.Status = _filterStatusBill And AP.Balance > 0
                                         Select AP
                ElseIf _filterNature = 2 Then
                    TmpQueryableSource = From AP In tableAccountPayable
                                         Where AP.IdSupplier.Id = _filterIdSupplier And AP.Status = _filterStatusBill And AP.Balance >= 0
                                         Select AP
                End If
            End If




            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayable
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountPayable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "AdvancePayments LinqInstantFeedBackSource"
    Private _filterIdSupplierAdvance As Integer
    Private _filterStatusAdvance As Integer
    Private _filterNatureAdvance As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAdvancePayments(IdSupplierAdvance As Integer, StatusAdvance As Byte, NatureAdvance As Byte) As LinqInstantFeedbackSource
        Dim vlinqAdvancePayments As New LinqInstantFeedbackSource
        AddHandler vlinqAdvancePayments.GetQueryable, AddressOf OnGetQueryableAdvancePayments
        AddHandler vlinqAdvancePayments.DismissQueryable, AddressOf DismissQueryableAdvancePayments
        _filterIdSupplierAdvance = IdSupplierAdvance
        _filterStatusAdvance = StatusAdvance
        _filterNatureAdvance = NatureAdvance
        vlinqAdvancePayments.KeyExpression = "Id"
        Return vlinqAdvancePayments
    End Function

    Private Sub OnGetQueryableAdvancePayments(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAccountPayable As XPQuery(Of AdvancePaymentsXpo) = New XPQuery(Of AdvancePaymentsXpo)(session)
            Dim TmpQueryableSource = Nothing
            If _filterNatureAdvance = 1 Then
                TmpQueryableSource = From AP In tableAccountPayable
                                     Where AP.IdSupplier.Id = _filterIdSupplierAdvance And AP.Status = _filterStatusAdvance And AP.Balance <> AP.Value
                                     Select AP
            ElseIf _filterNatureAdvance = 2 Then
                TmpQueryableSource = From AP In tableAccountPayable
                                     Where AP.IdSupplier.Id = _filterIdSupplierAdvance And AP.Status = _filterStatusAdvance And AP.Balance > 0
                                     Select AP
            End If
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayable
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAdvancePayments(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ListAccountPayableSharesByIdSupplierAndState"
    Private _filterIdSupplierShare As Integer
    Private _filterStatusShare As Byte
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableSharesByIdSupplierAndState(IdSupplier As Integer, Status As Byte) As LinqInstantFeedbackSource
        Dim vListAccountPayableSharesByIdSupplierAndState As New LinqInstantFeedbackSource
        AddHandler vListAccountPayableSharesByIdSupplierAndState.GetQueryable, AddressOf OnGetQueryableShares
        AddHandler vListAccountPayableSharesByIdSupplierAndState.DismissQueryable, AddressOf DismissQueryableShares
        _filterIdSupplierShare = IdSupplier
        _filterStatusShare = Status
        vListAccountPayableSharesByIdSupplierAndState.KeyExpression = "Id"
        Return vListAccountPayableSharesByIdSupplierAndState
    End Function

    Private Sub OnGetQueryableShares(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAccountPayableShare As XPQuery(Of AccountPayableSharesXpo) = New XPQuery(Of AccountPayableSharesXpo)(session)
            Dim tableAccountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(session)
            Dim zero As Decimal = 0
            Dim TmpQueryableSource = From APS In tableAccountPayableShare
                                     Join AP In tableAccountPayable On AP.Id Equals APS.IdAccountPayable.Id
                                     Where AP.IdSupplier.Id = _filterIdSupplierShare And AP.Balance > 0 And AP.Status = _filterStatusShare And APS.Balance > 0
                                     Select APS
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayableShare
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableShares(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
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
    '        Dim session As New Session(XpoDefault.DataLayer)
    '        Dim tableTransfer As XPQuery(Of PaymentsAccountPayable) = New XPQuery(Of PaymentsAccountPayable)(session)
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
    '        Dim session = New Session(XpoDefault.DataLayer)
    '        Dim tableObjD As XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo) = New XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo)(session)
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
        Dim session As New Session(XpoDefault.DataLayer)
        Dim statetmp As Integer = 2 'confirmados
        Dim tableTransfer As XPQuery(Of AccountPayableTransferXpo) = New XPQuery(Of AccountPayableTransferXpo)(session)
        Dim TmpDocumentype As String = 1
        Dim TmpQueryableSource = From T1 In tableTransfer
                                 Where T1.Status = statetmp And listFillingUnitId.Contains(T1.FilingUnitTargetId.Id)
                                 Select New With {T1.TranferType, T1.Id, T1.Code, .FilingSource = T1.FilingUnitSourceId.Name, .FilingTarget = T1.FilingUnitTargetId.Name, T1.CreationDate, .IdTarget = T1.FilingUnitTargetId.Id, T1.CreationUser}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using
    End Function

    ''' <summary>
    ''' Lista el detalle de un oficio de traslado
    ''' </summary>
    ''' <param name="_AccountPayableTransferId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsAccountPayableTransferDetail(ByVal _AccountPayableTransferId As Integer) As PLinqServerModeSource
        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableTransfer As XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo) = New XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo)(session)
        Dim TmpDocumentype As String = 1
        Dim TmpQueryableSource = From T1 In tableTransfer
                                 Where T1.AccountPayableTransferId.Id = _AccountPayableTransferId
                                 Select New With {.id = T1.Id, .AccountPayableCode = T1.AccountPayableId.Code, .NitName = T1.AccountPayableId.IdThirdParty.NitName, .State = If(T1.Status = "1", "Pendiente por Aceptacion", If(T1.Status = "2", "Aceptada", If(T1.Status = "3", "Rechazada", "Anulada"))), .BillNumber = T1.AccountPayableId.BillNumber}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using
    End Function

    ''' <summary>
    ''' Lista el detalle de un oficio de traslado
    ''' </summary>
    ''' <param name="_AccountPayableTransferId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRefundTransferDetail(ByVal _AccountPayableTransferId As Integer) As PLinqServerModeSource
        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableTransfer As XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo) = New XPQuery(Of PaymentsAccountPayableTransferDetailReportXpo)(session)
        Dim TmpDocumentype As String = 1
        Dim TmpQueryableSource = From T1 In tableTransfer
                                 Where T1.AccountPayableTransferId.Id = _AccountPayableTransferId
                                 Select New With {.id = T1.Id, .AccountPayableCode = T1.RefundId.Code, .State = If(T1.Status = "1", "Pendiente por Aceptacion", If(T1.Status = "2", "Aceptada", If(T1.Status = "3", "Rechazada", "Anulada")))}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using
    End Function

#End Region

#Region "AccountPayableDetailConceptWithDeferredCausation LinqInstantFeedBackSource"

    Private _filterSupplierId As Integer
    Private _filterStatusAP As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountPayableByIdSupplierAndStateWithDeferredCausation(SupplierId As Integer, Status As Integer) As LinqInstantFeedbackSource
        Dim vlinqAccountPayableDetailConcept As New LinqInstantFeedbackSource
        AddHandler vlinqAccountPayableDetailConcept.GetQueryable, AddressOf OnGetQueryableAccountPayableDetailConcept
        AddHandler vlinqAccountPayableDetailConcept.DismissQueryable, AddressOf DismissQueryableAccountPayableDetailConcept
        _filterSupplierId = SupplierId
        _filterStatusAP = Status
        vlinqAccountPayableDetailConcept.KeyExpression = "Id"
        Return vlinqAccountPayableDetailConcept
    End Function

    Private Sub OnGetQueryableAccountPayableDetailConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAccountPayableDetailConcept As XPQuery(Of PaymentsAccountPayableDetailConceptXpoP) = New XPQuery(Of PaymentsAccountPayableDetailConceptXpoP)(session)
            Dim TmpQueryableSource = Nothing
            TmpQueryableSource = From AP In tableAccountPayableDetailConcept
                                 Where AP.IdAccountPayable.IdSupplier.Id = _filterSupplierId And AP.IdAccountPayable.Status = _filterStatusAP And AP.DeferredCausation = True
                                 Select AP
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAccountPayableDetailConcept
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountPayableDetailConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
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

        If INDFechaIni <> Nothing AndAlso INDFechaEnd <> Nothing Then
            criteria &= "GetDate(MovesDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(MovesDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# "
        End If

        'filtro por terceros
        If INDSupplierStart IsNot Nothing And INDSupplierEnd IsNot Nothing Then
            If criteria Is Nothing Then
                criteria &= "ThirdPartyNit >= '" & INDSupplierStart & "' AND ThirdPartyNit <= '" & INDSupplierEnd & "'"
            Else
                criteria &= " AND ThirdPartyNit >= '" & INDSupplierStart & "' AND ThirdPartyNit <= '" & INDSupplierEnd & "'"
            End If
        End If

        'filtro por cuentas
        If INDInvoiceStart IsNot Nothing And INDInvoiceEnd IsNot Nothing Then
            If criteria Is Nothing Then
                criteria &= "BillNumber >= '" & INDInvoiceStart & "' AND BillNumber <= '" & INDInvoiceEnd & "'"
            Else
                criteria &= " AND BillNumber >= '" & INDInvoiceStart & "' AND BillNumber <= '" & INDInvoiceEnd & "'"
            End If
        End If

        resulXpo = Me.LoadCollection(Of VReportExtractAccountPayable)(XpoDefault.DataLayer, Nothing, criteria)
        Return resulXpo

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

End Class

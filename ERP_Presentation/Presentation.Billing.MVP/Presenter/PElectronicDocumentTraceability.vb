#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class PElectronicDocumentTraceability

#Region "Fields"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IElectronicDocumentTraceability

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IElectronicDocumentTraceability)
        'If iView Is Nothing Then
        '    Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        'End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista las unidades operativas a las cuales tengo permiso
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeOperatingUnitXpo()
        View.OperatingUnitXpo = SessionValues.Instance.ListOperatingUnitPermission
    End Sub

    ''' <summary>
    ''' Obtiene las facturas electronicas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetInvoices(operatingUnitId As Integer, status As String)
        If status IsNot Nothing AndAlso status.Contains("88") Then
            status = status & ",99,66"
        End If
        View.InvoiceXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicDocumentsTypeInvoices(operatingUnitId, status)
    End Sub

    ''' <summary>
    ''' Obtiene las notas debito
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetDebitNotes(operatingUnitId As Integer, status As String)
        If status IsNot Nothing AndAlso status.Contains("88") Then
            status = status & ",99,66"
        End If
        View.DebitNoteXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicDocumentsTypeDebitNotes(operatingUnitId, status)
    End Sub

    ''' <summary>
    ''' Obtiene las notas creditos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetCreditNotes(operatingUnitId As Integer, status As String)
        If status IsNot Nothing AndAlso status.Contains("88") Then
            status = status & ",99,66"
        End If
        View.CreditNoteXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicDocumentsTypeCreditNotes(operatingUnitId, status)
    End Sub

    ''' <summary>
    ''' Obtiene los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetDetails(electronicDocumentId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicDocumentDetails(electronicDocumentId)
    End Function

    ''' <summary>
    ''' Obtiene las notificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetNotifications(electronicDocumentId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicDocumentNotifications(electronicDocumentId)
    End Function

    ''' <summary>
    ''' Lista los solicitudes con valoración
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListEmailsByThirdPartyId(thirdPartyId) As List(Of Domain.Entities.Email)
        Dim listEmails As New List(Of Domain.Entities.Email)
        Dim filter As String = "Id = " & thirdPartyId

        Dim thirdPartyXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).AccountingService.GetCollection(Of Infrastructure.Data.Xpo.AccountingRepository.CommonThirdPartyXpo)(Nothing, filter).FirstOrDefault()
        If thirdPartyXpo IsNot Nothing Then
            Dim listEmailXpo = thirdPartyXpo.PersonId.CommonEmailCollection
            For Each email In listEmailXpo
                listEmails.Add(New Domain.Entities.Email With {.Email1 = email.Email})
            Next
        End If

        Return listEmails
    End Function

#End Region

End Class

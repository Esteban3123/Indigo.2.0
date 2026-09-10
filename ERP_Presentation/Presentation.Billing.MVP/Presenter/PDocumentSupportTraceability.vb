#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class PDocumentSupportTraceability

#Region "Fields"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IDocumentSupportTraceability

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IDocumentSupportTraceability)
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
    ''' Obtiene los documentos soporte
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetElectronicSupportDocument(operatingUnitId As Integer)
        View.ElectronicSupportDocumentXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicDocumentsTypeSupportDocument("ElectronicSupportDocument", operatingUnitId)
    End Sub

    Public Sub GetElectronicSupportDocumentAdjustmentNote(operatingUnitId As Integer)
        View.AdjustmentNoteXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicDocumentsTypeSupportDocument("ElectronicSupportDocumentAdjustmentNote", operatingUnitId)
    End Sub

    ''' <summary>
    ''' Obtiene los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetDetails(electronicDocumentId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListDocumentSupportDetails(electronicDocumentId)
    End Function
    ''' <summary>
    ''' Obtiene los detalles de la nota
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetNoteDetails(NoteAdjustmentId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListSupportDocumentAdjustmentNoteDetail(NoteAdjustmentId)
    End Function

    ''' <summary>
    ''' Obtiene las notificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetNotifications(electronicDocumentId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicDocumentNotifications(electronicDocumentId)
    End Function

#End Region

End Class

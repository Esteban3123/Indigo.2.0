'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 01-03-2024
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Base

#End Region

Public Class PElectronicRIPSTraceability

#Region "Fields"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IElectronicRIPSTraceability

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

#End Region


#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IElectronicRIPSTraceability)
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
    ''' Obtiene las facturas de rips validadas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetGeneratedRIPS(operatingUnitId As Integer, entityName As String)
        View.GeneratedRIPSXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicsRIPSTraceability(operatingUnitId, entityName)
    End Sub

    Public Sub GetGeneratedRIPSInvoiceTap(operatingUnitId As Integer)
        View.GeneratedRIPSXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicsRIPSTraceabilityInvoiceTap(operatingUnitId)
    End Sub

    ''' <summary>
    ''' Obtiene las notas debit/credito de rips
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetDebitCreditNoteRIPS(operatingUnitId As Integer, entityName As String)
        View.DebitCreditNoteRIPSXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicsRIPSTraceability(operatingUnitId, entityName)
    End Sub

    ''' <summary>
    ''' Obtiene las notas de ajuste de rips
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetAdjustmentNoteRIPS(operatingUnitId As Integer, entityName As String)
        View.AdjustmentNoteRIPSXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicsRIPSTraceability(operatingUnitId, entityName)
    End Sub

    ''' <summary>
    ''' Obtiene los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetDetails(electronicRIPSId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListElectronicRIPSDetailsRules(electronicRIPSId)
    End Function


#End Region

End Class

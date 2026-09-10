#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class MElectronicDocumentTraceability
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me.Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene las facturas electronicas
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function UpdateStateElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument)) As Task(Of ActionResult(Of ElectronicDocument))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateStateElectronicDocumentsAsync(listElectronicDocuments, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene las facturas electronicas
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetInvoices(OperatingUnitId As Integer, StatusId As Integer?) As Task(Of List(Of ElectronicDocument))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListElectronicDocumentsTypeInvoicesAsync(OperatingUnitId, StatusId)
    End Function

    ''' <summary>
    ''' Obtiene las notas debito
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetDebitNotes(OperatingUnitId As Integer, StatusId As Integer?) As Task(Of List(Of ElectronicDocument))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListElectronicDocumentsTypeDebitNotesAsync(OperatingUnitId, StatusId)
    End Function

    ''' <summary>
    ''' Obtiene las notas creditos
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetCreditNotes(OperatingUnitId As Integer, StatusId As Integer?) As Task(Of List(Of ElectronicDocument))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListElectronicDocumentsTypeCreditNotesAsync(OperatingUnitId, StatusId)
    End Function

    ''' <summary>
    ''' Envia notificación
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function SendNotification(listElectronicDocumentNotification As List(Of ElectronicDocumentNotification)) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SendNotificationAsync(listElectronicDocumentNotification)
    End Function

    ''' <summary>
    ''' Reenvia una factura, para facturacion electronica Costa Rica
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ReSendElectronicDocument(InvoiceNumber As String, InvoiceId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReSendElectronicDocumentAsync(InvoiceNumber, InvoiceId, Me.Indigo)
    End Function

    ''' <summary>
    ''' Reenvia documentos electronicos al proceso de facturacion electronica en Costa Rica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function ReSendElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReSendElectronicDocumentsAsync(listElectronicDocuments, Me.Indigo)
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class

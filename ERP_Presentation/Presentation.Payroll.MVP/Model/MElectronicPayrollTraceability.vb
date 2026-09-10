#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class MElectronicPayrollTraceability
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
    ''' Procesa los documentos electronicos
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function ProcessElectronicPayroll(operatingUnitId As Integer, electronicPayrollIds As List(Of Integer)) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoElectronicDocument.ProcessElectronicPayrollAsync(operatingUnitId, electronicPayrollIds)
    End Function

    ''' <summary>
    ''' Obtiene las facturas electronicas
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GenerateAdjustmentNote(operatingUnitId As Integer, electronicPayroll As ElectronicPayroll) As Task(Of ActionResult(Of ElectronicPayroll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateAdjustmentNoteAsync(operatingUnitId, electronicPayroll, Indigo)
    End Function

    ''' <summary>
    ''' Envia notificación
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function SendNotification(listElectronicPayrollNotification As List(Of ElectronicPayrollNotification)) As Task(Of ActionResult(Of String))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SendNotificationAsync(listElectronicPayrollNotification)
    End Function

    Public Async Function GetElectronicPaymentSupportXML(consecutive As Integer) As Task(Of ActionResult(Of NominaIndividual))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoElectronicDocument.GetElectronicPaymentSupportXMLAsync(consecutive)
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

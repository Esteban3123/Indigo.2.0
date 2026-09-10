Imports DevExpress.Data.PLinq
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.CloudAgent

Public Class MRecognition
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub
#End Region

#Region "Methods"
    Friend Async Function GenerateRecognition(careGroupXml As String, OperatingUnitId As Integer, RecognitionDate As DateTime, userCode As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GenerateRevenueRecognitionAsync(careGroupXml, OperatingUnitId, RecognitionDate, userCode)
    End Function

    Friend Async Function GenerateRecognitionByCareGroup(careGroupId As Integer, careGroupTotal As Decimal, operatingUnitId As Integer, recognitionDate As Date, userIndigo As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GenerateRecognitionByCareGroupAsync(careGroupId, careGroupTotal, operatingUnitId, recognitionDate, userIndigo)
    End Function

    Friend Async Function ReverseRecognition(recognitionId As Integer, userIndigo As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReverseRecognitionAsync(recognitionId, userIndigo)
    End Function

    ''' <summary>
    ''' Gets the recognition entrance data.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRecognitionEntranceData(operativeUnitId As Integer) As PLinqServerModeSource 'XPCollection(Of ViewListRecognitionEntrance)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetRecognitionEntranceData(operativeUnitId)
    End Function

    Public Function GetRecognitionEntranceDetailData(careGroupId As Integer, operativeUnitId As Integer) As XPInstantFeedbackSource 'PLinqServerModeSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListRevenueControlByCareGroupId(careGroupId, operativeUnitId)
    End Function

    Public Function ListReverseRecognitionDetail(revenueRecognitionDetail As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListReverseRecognitionDetail(revenueRecognitionDetail)
    End Function

    Friend Function GetReverseRecognition(operativeUnitId As Integer) As PLinqServerModeSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetReverseRecognition(operativeUnitId)
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

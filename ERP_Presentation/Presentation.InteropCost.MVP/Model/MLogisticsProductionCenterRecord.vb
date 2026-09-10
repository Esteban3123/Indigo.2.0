'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Juan F. Tamayo
' Created          : 2016-11-5
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

#End Region

Public Class MLogisticsProductionCenterRecord
    Implements IDisposable

#Region "Fields"

    Private _indigoSessionValues As SessionValues

    Private _tagForm As String

#End Region

#Region "Builders"

    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Function GetLogisticsProductionCenterRecordByCode(code As String) As ActionResult(Of LogisticsProductionCenterRecord)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetLogisticsProductionCenterRecordByCode(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetLogisticsProductionCenterRecordById(id As Int32) As ActionResult(Of LogisticsProductionCenterRecord)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetLogisticsProductionCenterRecordById(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function DeleteLogisticsProductionCenterRecord(entity As LogisticsProductionCenterRecord, idSequense As Int64) As ActionResult(Of LogisticsProductionCenterRecord)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteLogisticsProductionCenterRecord(entity, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function SaveLogisticsProductionCenterRecord(entity As LogisticsProductionCenterRecord, idSequense As Int64) As ActionResult(Of LogisticsProductionCenterRecord)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveLogisticsProductionCenterRecord(entity, Me._indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    Public Async Function GetLogisticsProductionCenterRecordByCodeAsync(code As String) As Task(Of ActionResult(Of LogisticsProductionCenterRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetLogisticsProductionCenterRecordByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetLogisticsProductionCenterRecordByIdAsync(id As Int32) As Task(Of ActionResult(Of LogisticsProductionCenterRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetLogisticsProductionCenterRecordByIdAsync(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function DeleteLogisticsProductionCenterRecordAsync(entity As LogisticsProductionCenterRecord, idSequense As Int64) As Task(Of ActionResult(Of LogisticsProductionCenterRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteLogisticsProductionCenterRecordAsync(entity, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveLogisticsProductionCenterRecordAsync(entity As LogisticsProductionCenterRecord, idSequense As Int64) As Task(Of ActionResult(Of LogisticsProductionCenterRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveLogisticsProductionCenterRecordAsync(entity, Me._indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    Public Async Function GetProductionCenter(id As String) As Task(Of ProductionCenter)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)

            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetProductionCenterByIdAsync(id)
        End Using
    End Function

    Public Async Function GetMeasureUnitById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of InventoryMeasurementUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetMeasureUnitByIdAsync(id, Me._indigoSessionValues.AuditMessageWcf)
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

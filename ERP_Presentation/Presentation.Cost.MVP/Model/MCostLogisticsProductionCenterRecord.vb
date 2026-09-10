'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/12/2016
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
#End Region

Public Class MCostLogisticsProductionCenterRecord
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

    Public Function GetCostLogisticsProductionCenterRecordByCode(code As String) As ActionResult(Of CostLogisticsProductionCenterRecord)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostLogisticsProductionCenterRecordByCode(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetCostLogisticsProductionCenterRecordById(id As Int32) As ActionResult(Of CostLogisticsProductionCenterRecord)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostLogisticsProductionCenterRecordById(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function DeleteCostLogisticsProductionCenterRecord(entity As CostLogisticsProductionCenterRecord, idSequense As Int64) As ActionResult(Of CostLogisticsProductionCenterRecord)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteCostLogisticsProductionCenterRecord(entity, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function SaveCostLogisticsProductionCenterRecord(entity As CostLogisticsProductionCenterRecord, idSequense As Int64) As ActionResult(Of CostLogisticsProductionCenterRecord)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostLogisticsProductionCenterRecord(entity, Me._indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    Public Async Function GetCostLogisticsProductionCenterRecordByCodeAsync(code As String) As Task(Of ActionResult(Of CostLogisticsProductionCenterRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostLogisticsProductionCenterRecordByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetCostLogisticsProductionCenterRecordByIdAsync(id As Int32) As Task(Of ActionResult(Of CostLogisticsProductionCenterRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostLogisticsProductionCenterRecordByIdAsync(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function DeleteCostLogisticsProductionCenterRecordAsync(entity As CostLogisticsProductionCenterRecord, idSequense As Int64) As Task(Of ActionResult(Of CostLogisticsProductionCenterRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteCostLogisticsProductionCenterRecordAsync(entity, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveCostLogisticsProductionCenterRecordAsync(entity As CostLogisticsProductionCenterRecord, idSequense As Int64) As Task(Of ActionResult(Of CostLogisticsProductionCenterRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostLogisticsProductionCenterRecordAsync(entity, Me._indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    Public Async Function GetProductionCenter(id As String) As Task(Of CostProductionCenter)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoCost.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)

            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostProductionCenterByIdAsync(id)
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

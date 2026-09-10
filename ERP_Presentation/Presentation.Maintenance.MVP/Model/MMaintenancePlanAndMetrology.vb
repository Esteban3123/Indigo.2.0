'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Diego Roldán
' Created          : 2018-09-14
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports System.ServiceModel
Imports Domain.Base.Entities

#End Region

Public Class MMaintenancePlanAndMetrology
    Implements IDisposable

    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Methods"
    Sub New()
    End Sub

    Public Async Function GetMaintenancePlanAndMetrology(PhysicalAssetId As Integer, protocolId As Integer) As Task(Of MaintenancePlanAndMetrology)
        Return Await IndigoConecta.Instancia().CurrentCloud.IndigoMaintenance.GetMaintenancePlanAndMetrologyAsync(PhysicalAssetId, protocolId)
    End Function

    Public Async Function SaveMaintenancePlanAndMetrology(maintenancePlan As MaintenancePlanAndMetrology) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia().CurrentCloud.IndigoMaintenance.SaveMaintenancePlanAndMetrologyAsync(maintenancePlan)
    End Function

    Public Async Function DeleteMaintenancePlanAndMetrologyAsync(id As List(Of Integer)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia().CurrentCloud.IndigoMaintenance.DeleteMaintenancePlanAndMetrologyAsync(id)
    End Function

    Public Async Function GetMaintenancePlanAndMetrologyById(programedId As Integer) As Task(Of MaintenancePlanAndMetrology)
        Return Await IndigoConecta.Instancia().CurrentCloud.IndigoMaintenance.GetMaintenancePlanAndMetrologyByIdAsync(programedId)
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

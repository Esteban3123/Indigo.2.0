'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Maintenance.Entities
Imports Presentation.Base
Imports Domain.Base.Entities

Public Class MCostCenter
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "622"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub


    Private Indigo As SessionValues = SessionValues.Instance

    Public Async Function ListAllCostCenterAsync() As Task(Of List(Of CostCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllCostCenterAsync(Indigo)
    End Function

    Public Async Function GetCostCenterAsync(ByVal code As String) As Task(Of CostCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetCostCenterAsync(code, Indigo)
    End Function

    Public Async Function GetCostCenterById(ByVal id As Integer) As Task(Of CostCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetCostCenterByIdAsync(id, True, Indigo)
    End Function

    Public Function GetCostCenterByIdSimple(ByVal id As Integer) As CostCenter
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetCostCenterById(id, True, Indigo)
    End Function

    Public Async Function SaveCostCenterAsync(ByVal costCenter As CostCenter, idSequence As Long) As Task(Of ActionResult(Of CostCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveCostCenterAsync(costCenter, Indigo, idSequence)
    End Function

    Public Async Function DeleteCostCenterAsync(ByVal costCenter As CostCenter) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteCostCenterAsync(costCenter, Indigo)
    End Function

    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of CostCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateCostCenterAsync(code, state, Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("CostCenter", Indigo)
    End Function


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

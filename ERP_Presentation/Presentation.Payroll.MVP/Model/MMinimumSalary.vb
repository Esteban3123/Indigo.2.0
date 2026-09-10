'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Juan Pablo Daza Medina
' Created          : 28-10-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
#End Region


Public Class MMinimumSalary
    Inherits ModelBase
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
        Me._tagForm = tag
        _indigo = SessionValues.Instance
        'Me._indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    Public Async Function ListAllMinimumSalaryAsync() As Task(Of List(Of MinimumSalary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllMinimumsalaryAsync(Me._indigo)
    End Function

    Public Async Function GetMinimumSalaryByYear(ByVal year As String) As Task(Of MinimumSalary)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetMinimunSalaryAsync(year, Me._indigo)
    End Function

    Public Async Function GetMinimumSalaryById(ByVal Id As Integer) As Task(Of MinimumSalary)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetMinimumSalaryByIdAsync(Id, Me._indigo)
    End Function

    Public Async Function SaveMinimumSalary(ByVal MinimumSalary As MinimumSalary, ByVal idSequence As Int64) As Task(Of ActionResult(Of MinimumSalary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveMinimunSalaryAsync(MinimumSalary, Me._indigo, idSequence, Me._indigo.AuditMessageWcf)
    End Function

    Public Async Function DeleteMinimumSalary(ByVal MinimumSalary As MinimumSalary) As Task(Of ActionMessageResult(Of MinimumSalary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteMinimunSalaryAsync(MinimumSalary, Me._indigo)
    End Function
#Region "IDiposable Support"
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


#End Region



End Class
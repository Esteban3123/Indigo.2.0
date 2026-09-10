'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 1-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel

Public Class MRetirementReason
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' obtiene una razon de retiro especifico
    ''' </summary>
    ''' <param name="code">Codigo de la razon de retiro</param>
    ''' <returns>Razon de retiro</returns>
    ''' <remarks></remarks>
    Public Async Function GetRetirementReasonAsync(ByVal code As String) As Task(Of RetirementReason)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetRetirementReasonAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Lista todas las razones de retiro
    ''' </summary>
    ''' <returns>Lista de razones de retiro</returns>
    ''' <remarks></remarks>
    Public Async Function ListAllRetirementReasonAsync() As Task(Of List(Of RetirementReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllRetirementReasonAsync(Indigo)
    End Function

    ''' <summary>
    ''' Graba o actualiza una razon de retiro
    ''' </summary>
    ''' <param name="retirementReason">Razon de retiro</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Async Function SaveRetirementReasonAsync(ByVal retirementReason As RetirementReason, idSequence As Int64) As Task(Of ActionResult(Of RetirementReason))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveRetirementReasonAsync(retirementReason, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveRetirementReasonAsync(retirementReason, Indigo, idSequence, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina razones de retiro
    ''' </summary>
    ''' <param name="retirementReason">Razon de retiro</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Async Function DeleteRetirementReasonAsync(ByVal retirementReason As RetirementReason) As Task(Of ActionResult)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteRetirementReasonAsync(retirementReason, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteRetirementReasonAsync(retirementReason, Indigo, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function GetNullFields()
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("RetirementReason", Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of RetirementReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateRetirementReasonAsync(code, state, Indigo, Me.Indigo.AuditMessageWcf)
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

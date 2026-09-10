'***********************************************************************
' Assembly         : Presentation.AccountManagement.MVP
' Author           : Andrés Steven Rojas
' Created          : 09-01-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.CloudAgent
#End Region

Public Class MRejectionReason
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
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function GetRejectionReasonByCode(ByVal code As String) As Task(Of ActionResult(Of RejectionReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetRejectionReasonByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetRejectionReasonById(ByVal id As Integer) As Task(Of ActionResult(Of RejectionReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetRejectionReasonByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza el motivo de rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="idSecuence"></param>
    ''' <returns></returns>
    Public Async Function SaveRejectionReason(rejectionReason As RejectionReason, ByVal idSecuence As Int64) As Task(Of ActionResult(Of RejectionReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.SaveRejectionReasonAsync(rejectionReason, Me.Indigo.AuditMessageWcf, idSecuence)
    End Function

    ''' <summary>
    ''' Elimina el motivo de rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <returns></returns>
    Public Async Function DeleteRejectionReason(rejectionReason As RejectionReason) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.DeleteRejectionReasonAsync(rejectionReason, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado del motivo de rechazo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function ChangeState(ByVal code As String) As Task(Of ActionResult(Of RejectionReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ChangeStateRejectionReasonAsync(code, Me.Indigo.AuditMessageWcf)
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

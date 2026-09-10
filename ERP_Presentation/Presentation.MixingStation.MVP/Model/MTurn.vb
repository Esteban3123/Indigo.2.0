'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Yoe Andres Cardenas
' Created          : 22/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MTurn
    Implements IDisposable

#Region "Fields"


    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Lista todos los turnos
    ''' </summary>
    Public Function ListAllTurn() As List(Of Turn)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllTurn(Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los turnos asincrono
    ''' </summary>
    Public Async Function ListAllTurnAsync() As Task(Of List(Of Turn))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllTurnAsync(Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Obtiene un turno por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTurn(ByVal code As String) As ActionResult(Of Turn)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetTurn(code, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Obtiene un turno por código asincrono
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetTurnAsync(ByVal code As String) As Task(Of ActionResult(Of Turn))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetTurnAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Obtiene un turno por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTurnById(ByVal id As Integer) As ActionResult(Of Turn)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetTurnById(id, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un turno por id asincrono
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetTurnByIdAsync(ByVal id As Integer) As Task(Of ActionResult(Of Turn))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetTurnByIdAsync(id, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Guarda o actualiza un turno
    ''' </summary>
    ''' <param name="turn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveTurn(ByVal turn As Turn, ByVal idSequence As Int64) As ActionResult(Of Turn)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveTurn(turn, idSequence, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Guarda o actualiza un turno asincrono
    ''' </summary>
    ''' <param name="turn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveTurnAsync(ByVal turn As Turn, ByVal idSequence As Int64) As Task(Of ActionResult(Of Turn))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveTurnAsync(turn, idSequence, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Elimina un turno
    ''' </summary>
    ''' <param name="turn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteTurn(ByVal turn As Turn) As ActionResult
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteTurn(turn, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Elimina un turno asincrono
    ''' </summary>
    ''' <param name="turn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteTurnAsync(ByVal turn As Turn) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteTurnAsync(turn, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Turn))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateStateTurnAsync(code, state, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateAsync(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Turn))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateStateTurnAsync(code, state, Me._sessionValues.AuditMessageWcf)
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

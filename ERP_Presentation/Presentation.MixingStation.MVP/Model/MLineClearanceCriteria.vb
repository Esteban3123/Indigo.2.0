'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Andres Alarcon
' Created          : 20/02/2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class MLineClearanceCriteria
    Inherits ModelBase
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
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
        Me._tagForm = tag
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un registro de criterios de despeje de linea
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLineClearanceCriteriaByCode(ByVal code As String) As Task(Of ActionResult(Of LineClearanceCriteria))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetLineClearanceCriteriaByCodeAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un registro de criterios de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveLineClearanceCriteria(ByVal _lineClearanceCriteria As LineClearanceCriteria, ByVal idSequence As Int64) As Task(Of ActionResult(Of LineClearanceCriteria))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveLineClearanceCriteriaAsync(_lineClearanceCriteria, idSequence, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un registro de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteLineClearanceCriteria(ByVal _lineClearanceCriteria As LineClearanceCriteria) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteLineClearanceCriteriaAsync(_lineClearanceCriteria, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of LineClearanceCriteria))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateLineClearanceCriteriaAsync(code, state, Me._sessionValues.AuditMessageWcf)
    End Function

#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

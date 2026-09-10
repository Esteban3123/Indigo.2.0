'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Diego A. Roldan
' Created          : 2013-06-11
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

''' <summary>
''' Modelo del frontal de devolución
''' </summary>
Public Class MConceptGlosas
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    Sub New(Tag As String)
        Me._tagForm = Tag
    End Sub

#End Region

#Region "Methods"
    Public Async Function GetConceptGlosasByCode(code As String) As Task(Of ConceptGlosas)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetConceptGlosasByCodeAsync(code, Me._indigoSessionValues)
    End Function

    Public Async Function SaveConceptGlosas(conceptGlosa As ConceptGlosas, idSequense As Long) As Task(Of ActionResult(Of ConceptGlosas))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveConceptGlosasAsync(conceptGlosa, Me._indigoSessionValues, idSequense)
    End Function

    Public Async Function DeleteConceptGlosas(conceptGlosa As ConceptGlosas) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteConceptGlosasAsync(conceptGlosa, _indigoSessionValues)
    End Function

    Public Async Function ChangeStateConceptGlosas(code As String, state As Boolean) As Task(Of ActionResult(Of ConceptGlosas))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ChangeConceptGlosasAsync(code, state, _indigoSessionValues)
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

'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Andres Alarcon 
' Created          : 25/11/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

Public Class MCategoryDefects
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

    Public Async Function GetCategoryDefects(ByVal code As String) As Task(Of DefectClassificationGroup)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCategoryDefectsAsync(code, _sessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetCategoryDefectsById(ByVal id As Integer) As Task(Of DefectClassificationGroup)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCategoryDefectsByIdAsync(id)
    End Function

    Public Async Function SaveCategoryDefects(ByVal CategoryDefects As DefectClassificationGroup, ByVal idSequence As Int64) As Task(Of ActionResult(Of DefectClassificationGroup))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCategoryDefectsAsync(CategoryDefects, Me._sessionValues.AuditMessageWcf, idSequence)
    End Function

    Public Async Function DeleteCategoryDefects(ByVal CategoryDefects As DefectClassificationGroup) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteCategoryDefectsAsync(CategoryDefects, Me._sessionValues.AuditMessageWcf)
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

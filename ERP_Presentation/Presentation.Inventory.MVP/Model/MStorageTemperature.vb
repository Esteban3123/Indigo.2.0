'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Andres Alarcon
' Created          : 27-11-2024
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
Imports Presentation.CloudAgent

#End Region

Public Class MStorageTemperature
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private Indigo As SessionValues

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
    ''' Consulta  por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetStorageTemperatureById(ByVal id As Integer) As Task(Of ActionResult(Of StorageTemperature))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetStorageTemperatureByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta por code
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetStorageTemperatureByCode(ByVal Code As String) As Task(Of ActionResult(Of StorageTemperature))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetStorageTemperatureAsync(Code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza 
    ''' </summary>
    ''' <param name="StorageTemperature"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveStorageTemperature(ByVal StorageTemperature As StorageTemperature, ByVal sequenseId As Int64) As Task(Of ActionResult(Of StorageTemperature))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveStorageTemperatureAsync(StorageTemperature, sequenseId, Me.Indigo.AuditMessageWcf)
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

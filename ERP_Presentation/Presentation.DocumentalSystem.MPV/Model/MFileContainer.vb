'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Juan Diego Diaz 
' Created          : 07-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Domain.DocumentalSystem.Entities

#End Region

''' <summary>
''' Realiza la conexion con los servicios
''' </summary>
Public Class MFileContainer
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable de session
    ''' </summary>
    Private Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor del modelo
    ''' </summary>
    Sub New(TagForm As String)
        Me._tagForm = TagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtener registro de archivador
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFileContainer(ByVal Id As String) As Task(Of FileContainer)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.GetFileContainerAsync(Id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener lista de formularios por contenedor
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFileContainersForm(ByVal Id As String) As Task(Of List(Of FileContainersForm))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.ListFileContainersFormByIdFileContainerAsync(Id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guardar registro de archivador
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveFileContainer(ByVal FileContainer As FileContainer) As Task(Of ActionResult(Of FileContainer))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.SaveFileContainerAsync(FileContainer, Me.Indigo)
    End Function

    ''' <summary>
    ''' Borrar registro de archivador
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteFileContainer(ByVal FileContainer As FileContainer) As Task(Of ActionResult(Of FileContainer))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.DeleteFileContainerAsync(FileContainer, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult(Of BlockRecord))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecord)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Async Function GetFieldsNULLAsync() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("FileContainer", Me.Indigo)
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

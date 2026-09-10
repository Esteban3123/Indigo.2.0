'***********************************************************************
' Assembly         : Presentacion.Controls.MVP
' Author           : Juan Diego Diaz
' Created          : 17-09-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.DocumentalSystem.Entities
Imports System.Threading.Tasks
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Modelo de conexion con los servicios distribuidos de digitalización
''' </summary>
Public Class MDigitalization
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener documentos por fullTextIndex
    ''' </summary>
    ''' <param name="searchWord">Texto a buscar</param>
    ''' <returns></returns>
    Public Async Function getDocumentaFullText(ByVal searchWord As String, inMenu As Boolean) As Task(Of List(Of DocumentsStore))
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = searchWord}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.GetDocumentFullTextAsync(sessionInfo, inMenu, 0, 0)
    End Function


    ''' <summary>
    ''' Funcion para obtener los archivadores de un formulario
    ''' </summary>
    ''' <param name="idForm">Formulario a buscar</param>
    ''' <returns></returns>
    Public Async Function listFileContainersByIdForm(idForm As String) As Task(Of List(Of FileContainersForm))
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = idForm}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.ListFileContainersFormByIdFormAsync(idForm, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener documentos según formulario
    ''' </summary>
    ''' <param name="idForm">Formulario a buscar</param>
    ''' <param name="withTop">Variable para establecer top</param>
    ''' <returns></returns>
    Public Async Function getDocumentsByIdForm(idForm As String, withTop As Boolean) As Task(Of List(Of DocumentsStore))
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = idForm}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.getDocumentByIdFormAsync(sessionInfo, withTop)
    End Function

    ''' <summary>
    ''' Funcion para obtener documentos según formulario y registro
    ''' </summary>
    ''' <param name="idForm">Formulario a buscar</param>
    ''' <param name="IdEntity">Registro a buscar</param>
    ''' <param name="withTop">Variable para establecer top</param>
    ''' <returns></returns>
    Public Async Function getDocumentsByIdFormAndIdEntity(idForm As String, IdEntity As String, withTop As Boolean) As Task(Of List(Of DocumentsStore))
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = idForm}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.getDocumentByIdFormAndIdEntityAsync(sessionInfo, CInt(IdEntity), withTop)
    End Function

    ''' <summary>
    ''' Funcion para eliminar un documento
    ''' </summary>
    ''' <param name="documentToDelete"></param>
    ''' <returns></returns>
    Public Async Function DeleteDocument(documentToDelete As DocumentsStore) As Task(Of ActionResult)
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.DeleteDocumentAsync(documentToDelete, sessionInfo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar un documento por entidad
    ''' </summary>
    ''' <param name="IdEntity"></param>
    ''' <returns></returns>
    Public Async Function DeleteIndexedDocumentByIdEntity(IdEntity As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.DeleteDocumentByIdEntityAsync(SessionValues.Instance.VituelContainer, IdEntity)
    End Function

    ''' <summary>
    ''' Funcion para obtener un stream de un documento
    ''' </summary>
    ''' <param name="Id">Id</param>
    ''' <returns></returns>
    Public Async Function getData(ByVal Id As String) As Task(Of System.IO.Stream)
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = Id}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.GetDocumentAsync(sessionInfo)
    End Function

    ''' <summary>
    ''' Metodo para guardar documento
    ''' </summary>
    Public Async Function saveDocument(length As Long, name As String, metaData As String, localFileStream As IO.Stream, attachDate As DateTime, idForm As Integer, idEntity As Integer, idFileContainer As Integer, UserFormMetaData As Boolean) As Task(Of Presentation.CloudAgent.IndigoReference.DocumentalSystem.ResponseDocument)
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.SaveDocumentAsync(attachDate, length, name, idEntity, idFileContainer, idForm, metaData, sessionInfo, UserFormMetaData, localFileStream)
    End Function

    ''' <summary>
    ''' Funcion para obtener documentos según formulario, la entidad y el registro
    ''' </summary>
    ''' <param name="formId">Formulario a buscar</param>
    ''' <param name="entityName">Entidad a buscar</param>
    ''' <param name="entityId">Registro a buscar</param>
    ''' <returns></returns>
    Public Async Function GetAttachmentsByFormAndEntity(formId As String, entityName As String, entityId As String, withTop As Boolean) As Task(Of ActionResult(Of List(Of Domain.Entities.Attachment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetAttachmentsByFormAndEntityAsync(CInt(formId), entityName, CInt(entityId), withTop, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener un documento por id
    ''' </summary>
    ''' <param name="id">Id del registro</param>
    ''' <returns></returns>
    Public Async Function GetAttachmentById(id As Integer) As Task(Of ActionResult(Of Domain.Entities.Attachment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetAttachmentByIdAsync(id, Me.Indigo)
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


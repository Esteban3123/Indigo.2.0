'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego A. Roldán L.
' Created          : 2023-03-24
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

Public Class MRequestParam
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private _session As SessionValues
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
        _session = SessionValues.Instance
        _session.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un parametro de solicitud por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRequestParamByCodeAsync(code As String) As Task(Of RequestParam)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRequestParamByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Obtiene un parametro de solicitud por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRequestParamByIdAsync(id As Integer) As Task(Of RequestParam)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRequestParamByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un parametro de solicitud
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveRequestPara(record As RequestParam, ByVal idSequence As Long) As Task(Of ActionResult(Of RequestParam))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveRequestParamAsync(record, _session.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Elimina un parametro de solicitud
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteRequestParam(record As RequestParam) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteRequestParamAsync(record, _session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateRequestParam(code As String, state As Byte) As Task(Of ActionResult(Of RequestParam))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateStateRequestParamAsync(code, state, _session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Crea detalles de parametros de solicitud por imformación a importar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function LoadRequestParamProductByImportDataAsync(data As List(Of List(Of Object))) As Task(Of ActionResult(Of List(Of RequestParamProduct)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.LoadRequestParamProductByImportDataAsync(data)
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

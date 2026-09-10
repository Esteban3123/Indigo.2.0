'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Cristhian Mauricio Salazar
' Created          : 20-11-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports System.Threading.Tasks
Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

''' <summary>
''' Clase la cual sirve de base para los modelos, esta clase lleva las funciones comunes de los modelos
''' </summary>
''' <remarks></remarks>
Public Class ModelBase

    Protected Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Protected TagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me.TagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me.TagForm
    End Sub

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecord)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult(Of BlockRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

End Class

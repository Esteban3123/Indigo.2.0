'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/10/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

Public Class MRateManualDetail
    Implements IDisposable

#Region "Fields"

    '' <summary>
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
    ''' Obtiene un manual de servicios por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRateManualDetailById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of RateManualDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetRateManualDetailByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un manual de servicios
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveRateManualDetail(ByVal record As RateManualDetail) As Task(Of ActionResult(Of RateManualDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveRateManualDetailAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un listado de manual de servicios
    ''' </summary>
    ''' <param name="ListRateManualDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveListRateManualDetail(ByVal ListRateManualDetail As List(Of RateManualDetail), ByVal ListDeleteRateManualDetail As List(Of RateManualDetail)) As Task(Of ActionResult(Of List(Of RateManualDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveListRateManualDetailAsync(ListRateManualDetail, ListDeleteRateManualDetail, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un manual tarifario
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteRateManualDetail(ByVal record As RateManualDetail) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.DeleteRateManualDetailAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal id As Integer, ByVal state As Boolean) As Task(Of ActionResult(Of RateManualDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ChangeStateRateManualDetailAsync(id, state, Me.Indigo.AuditMessageWcf)
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

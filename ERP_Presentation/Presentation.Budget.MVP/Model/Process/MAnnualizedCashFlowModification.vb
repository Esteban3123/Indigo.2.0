'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 19/08/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class MAnnualizedCashFlowModification
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
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
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' lista los servicios SOAT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAnnualizedCashFlowValidityId(validatyId As Integer, type As Integer, month As Integer) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BudgetService.ListAnnualizedCashFlowValidityId(validatyId, 2, type, Month)
    End Function
    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecordBudget) As Task(Of ActionResult(Of BlockRecordBudget))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveBlockRecordAsync(Record)
    End Function
    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecordBudget)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord)
    End Function
    ''' <summary>
    ''' obtiene una modificacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAnnualizedCashFlowModificationByCode(code As String, type As Integer) As Task(Of AnnualizedCashFlowModification)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAnnualizedCashFlowModificationByCodeAsync(code, type, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtiene una modificacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowModificationById(id As Integer) As AnnualizedCashFlowModification
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAnnualizedCashFlowModificationById(id)
    End Function
    ''' <summary>
    ''' lista los detalles de las modificaciones por id de la cabecera
    ''' </summary>
    ''' <param name="pacModificationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId As Integer) As List(Of AnnualizedCashFlowModificationDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId)
    End Function
    ''' <summary>
    ''' guarda una modificacion del pac
    ''' </summary>
    ''' <param name="AnnualizedCashFlowModification"></param>
    ''' <param name="idCurrentSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAnnualizedCashFlowModification(AnnualizedCashFlowModification As AnnualizedCashFlowModification, idCurrentSequense As Int64) As Task(Of ActionResult(Of AnnualizedCashFlowModification))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveAnnualizedCashFlowModificationAsync(AnnualizedCashFlowModification, idCurrentSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

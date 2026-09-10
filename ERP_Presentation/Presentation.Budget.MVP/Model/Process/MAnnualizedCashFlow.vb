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

#End Region

Public Class MAnnualizedCashFlow
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
    ''' Obtiene una modificacion de presupuesto por su código
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Async Function GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId As Integer, categoryCode As String, type As Integer) As Task(Of ActionResult(Of List(Of AnnualizedCashFlow)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAnnualizedCashFlowByValidityIdAndByCodeCategoryAsync(ValidityId, categoryCode, type, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveListAnnualizedCashFlow(ListAnnualizedCashFlow As List(Of AnnualizedCashFlow), state As Integer, type As Integer) As Task(Of ActionResult(Of List(Of AnnualizedCashFlow)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveListAnnualizedCashFlowHeaderAsync(ListAnnualizedCashFlow, state, type, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia 
    ''' </summary>
    ''' <param name="ValidityId"></param>
    Public Async Function GetAnnualizedCashFlowByValidityId(ValidityId As Integer) As Task(Of List(Of AnnualizedCashFlow))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAnnualizedCashFlowByValidityIdAsync(ValidityId)
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

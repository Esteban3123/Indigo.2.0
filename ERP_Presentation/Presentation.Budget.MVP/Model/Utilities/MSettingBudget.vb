'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 22-05-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jeisson Herrera Peña
' Created          : 19-09-2015
'
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

Public Class MSettingBudget
    Inherits ModelBaseBudget
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' tag del funcional
    ''' </summary>
    Dim _tag As String

    ''' <summary>
    ''' Valores de session
    ''' </summary>
    Dim Indigo As SessionValues
#End Region

#Region "Builder"
    Sub New(ByVal tag As String)
        MyBase.New(tag)
        Me._tag = tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tag
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene la configuracion del presupuesto
    ''' </summary>
    ''' <param name="idOperatingUnit">The identifier operating unit.</param>
    ''' <returns></returns>
    Public Async Function GetSettingBudgetById(ByVal id As Integer) As Task(Of ActionResult(Of SettingsBudget))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetSettingBudgetByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda la configuracion de presupuesto
    ''' </summary>
    ''' <param name="settingBudget">The setting budget.</param>
    ''' <returns></returns>
    Public Async Function SaveSettingBudget(ByVal settingBudget As SettingsBudget) As Task(Of ActionResult(Of SettingsBudget))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveSettingBudgetAsync(settingBudget, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal id As Integer, ByVal state As Boolean) As Task(Of ActionResult(Of SettingPayments))

    End Function

    ''' <summary>
    ''' Elimina un parametro de Presupuesto
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteSettingBudget(ByVal record As SettingsBudget) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteSettingBudgetAsync(record, Me.Indigo.AuditMessageWcf)
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

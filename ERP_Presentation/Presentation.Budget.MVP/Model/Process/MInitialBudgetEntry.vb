'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 03-06-2014
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

Public Class MInitialBudgetEntry
    Inherits ModelBaseBudget
    Implements IDisposable

#Region "fields"

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
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
    End Sub
#End Region


#Region "Methods"

    ''' <summary>
    ''' Obtiene una entidad de contrato por id
    ''' </summary>
    ''' <param name="budgetaryValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetHeader(budgetaryValidityId As Integer, type As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of BudgetHeader))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetHeaderAsync(budgetaryValidityId, type, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener una categoria por el Id de la vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id asociado a la vigencia.</param>
    ''' <returns>La Profesion</returns>
    Public Async Function GetCategorysAsync(ByVal ValidityId As Integer, RevenueType As String) As Task(Of Object)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoBudget.InnerChannel)
            _indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)

            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetCategoryAsync(ValidityId, RevenueType, Me._indigoSessionValues.AuditMessageWcf)
        End Using

    End Function

    ''' <summary>
    ''' Guarda o Actualiza el presupuesto incial
    ''' </summary>
    ''' <param name="BudgetHeader">la entidad</param>
    ''' <param name="state">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SaveBudgetAsync(BudgetHeader As BudgetHeader, state As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of BudgetHeader))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveBudgetAsync(BudgetHeader, state, Me._indigoSessionValues.AuditMessageWcf)
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

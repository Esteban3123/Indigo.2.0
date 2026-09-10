'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic

#End Region

Public Class MReversalReason
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

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
      ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <returns></returns>
    Public Async Function DeleteReversalReasonAsync(reversalReason As Domain.Entities.BillingReversalReason) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DeleteReversalReasonAsync(reversalReason, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una razón de anulacóon por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Async Function GetReversalReasonAsync(code As String, tracking As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetReversalReasonAsync(code, tracking, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una razón ade anulación por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Async Function GetReversalReasonByIdAsync(id As Integer, tracking As Boolean) As Task(Of Domain.Entities.BillingReversalReason)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetReversalReasonByIdAsync(id, tracking)
    End Function

    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <returns></returns>
    Public Async Function SaveReversalReasonAsync(reversalReason As Domain.Entities.BillingReversalReason, idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveReversalReasonAsync(reversalReason, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Updates the state reversal reason.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Async Function UpdateStateReversalReasonAsync(code As String, state As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateStateReversalReasonAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
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

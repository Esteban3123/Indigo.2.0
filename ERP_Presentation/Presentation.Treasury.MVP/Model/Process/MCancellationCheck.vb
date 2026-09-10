'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 24-05-2014
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

#End Region

Public Class MCancellationCheck
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
    ''' Saves the cancellation check.
    ''' </summary>
    ''' <param name="cancellationCheck">The cancellation check.</param>
    ''' <returns></returns>
    Public Async Function SaveCancellationCheck(ByVal cancellationCheck As CancellationChecks) As Task(Of ActionResult(Of CancellationChecks))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCancellationCheckAsync(cancellationCheck, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the cancellation check by entity account and check number.
    ''' </summary>
    ''' <param name="idEntity">The identifier entity.</param>
    ''' <param name="CheckNumber">The check number.</param>
    ''' <returns></returns>
    Public Async Function GetCancellationCheckByEntityAccountAndCheckNumber(ByVal idEntity As Integer, ByVal CheckNumber As String) As Task(Of ActionResult(Of CancellationChecks))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCancellationCheckByEntityAccountAndCheckNumberAsync(idEntity, CheckNumber, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id de chequera y numero de cheque
    ''' </summary>
    ''' <param name="checkBookId"></param>
    ''' <param name="CheckNumber"></param>
    ''' <returns></returns>
    Public Async Function GetCancellationCheckByCheckBookIdAndCheckNumber(checkBookId As Integer, CheckNumber As Long) As Task(Of CancellationChecks)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCancellationCheckByCheckBookIdAndCheckNumberAsync(checkBookId, CheckNumber)
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

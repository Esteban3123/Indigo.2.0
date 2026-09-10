'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-06-2014
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

Public Class MCheck
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
    ''' Saves the check.
    ''' </summary>
    ''' <param name="check">The check.</param>
    ''' <returns></returns>
    Public Async Function SaveCheck(ByVal check As Checkbooks) As Task(Of ActionResult(Of Checkbooks))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCheckAsync(check, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Deletes the check.
    ''' </summary>
    ''' <param name="check">The check.</param>
    ''' <returns></returns>
    Public Async Function DeleteCheck(ByVal check As Checkbooks) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteCheckAsync(check, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un cheque bloqueado por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetCheckBlockById(ByVal Id As Integer) As Task(Of CheckBlock)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCheckBlockByIdAsync(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un cheque bloqueado por id de la chequera y numero
    ''' </summary>
    ''' <param name="IdCheckBook">The identifier check book.</param>
    ''' <param name="checkNumber">The check number.</param>
    ''' <returns></returns>
    Public Async Function GetCheckBlockByIdCheckBookNumber(ByVal IdCheckBook As Integer, ByVal checkNumber As Long) As Task(Of ActionResult(Of CheckBlock))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCheckBlockByIdCheckBookAndNumberAsync(IdCheckBook, checkNumber, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetCheckBlockByIdCheckBookNumberSimple(ByVal IdCheckBook As Integer, ByVal checkNumber As Long) As ActionResult(Of CheckBlock)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCheckBlockByIdCheckBookAndNumber(IdCheckBook, checkNumber, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Saves the check block.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveCheckBlock(ByVal checkBlock As CheckBlock) As Task(Of ActionResult(Of CheckBlock))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCheckBlockAsync(checkBlock, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Saves the check block simple.
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <returns></returns>
    Public Function SaveCheckBlockSimple(ByVal checkBlock As CheckBlock) As ActionResult(Of CheckBlock)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCheckBlock(checkBlock, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Deletes the check block.
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <returns></returns>
    Public Async Function DeleteCheckBlock(ByVal checkBlock As CheckBlock) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteCheckBlockAsync(checkBlock, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Deletes the check block.
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <returns></returns>
    Public Function DeleteCheckBlockSimple(ByVal checkBlock As CheckBlock) As ActionResult
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteCheckBlock(checkBlock, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the check by identifier entity bank account.
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <returns></returns>
    Public Async Function GetCheckByIdEntityBankAccountAndStatus(ByVal IdEntity As Integer, ByVal status As Short) As Task(Of ActionResult(Of Checkbooks))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCheckByIdEntityBankAccountAndStatusAsync(IdEntity, status, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetCheckByIdEntityBankAccountAndStatusSimple(ByVal IdEntity As Integer, ByVal status As Short) As ActionResult(Of Checkbooks)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCheckByIdEntityBankAccountAndStatus(IdEntity, status, Me._indigoSessionValues.AuditMessageWcf)
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

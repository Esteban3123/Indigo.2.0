'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-11-2014
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

Public Class MTreasuryNote
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
    ''' Confirma una nota de tesoreria
    ''' </summary>
    ''' <param name="IdTreasuryNote"></param>
    ''' <returns></returns>
    Public Async Function ConfirmTreasuryNote(IdTreasuryNote As Integer, idSequence As Long) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ConfirmTreasuryNoteAsync(IdTreasuryNote, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obtiene una nota de tesoreria por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function GetTreasuryNote(code As String) As Task(Of ActionResult(Of TreasuryNote))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetTreasuryNoteAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una nota de tesoreria por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetTreasuryNoteById(Id As Integer) As Task(Of TreasuryNote)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetTreasuryNoteByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Guarda una nota de tesoreria
    ''' </summary>
    Public Async Function SaveTreasuryNote(treasuryNote As TreasuryNote, withConfirm As Boolean, ByVal idSequence As Long) As Task(Of ActionResult(Of TreasuryNote))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveTreasuryNoteAsync(treasuryNote, withConfirm, Me._indigoSessionValues.AuditMessageWcf, idSequence)
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
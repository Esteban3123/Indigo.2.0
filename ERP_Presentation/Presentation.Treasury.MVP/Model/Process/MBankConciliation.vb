'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/05/2017
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

Public Class MBankConciliation
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
    ''' Obtiene una conciliación por id
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Function GetBankReconciliationById(ByVal id As Integer) As ActionResult(Of BankReconciliation)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankReconciliationById(id, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una conciliación por código
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Async Function GetBankReconciliationByCode(ByVal code As String) As Task(Of ActionResult(Of BankReconciliation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankReconciliationByCodeAsync(code, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un formato de exógena
    ''' </summary>
    ''' <param name="BankReconciliation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveBankReconciliation(ByVal BankReconciliation As BankReconciliation, Optional ByVal idSequense As Int64 = 0) As Task(Of ActionResult(Of BankReconciliation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveBankReconciliationAsync(BankReconciliation, _indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    ''' <summary>
    ''' Obtiene los detalles de una conciliación bancaria
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Async Function ListBankReconciliationDetail(ByVal criterias As Dictionary(Of String, String)) As Task(Of ActionResult(Of List(Of BankReconciliationDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankReconciliationDetailsAsync(criterias)
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
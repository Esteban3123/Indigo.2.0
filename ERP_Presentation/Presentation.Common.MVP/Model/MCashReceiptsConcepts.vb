'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 04-04-2014
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

''' <summary>
''' Realiza la conexion con los servicios de conceptos de recibos de caja
''' </summary>
Public Class MCashReceiptsConcepts
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
    ''' Saves the cash receipts concepts.
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <returns></returns>
    Public Async Function SaveCashReceiptsConcepts(ByVal cashReceiptConcept As CashReceiptConcepts, ByVal idSequence As Int64) As Task(Of ActionResult(Of CashReceiptConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCashReceiptConceptAsync(cashReceiptConcept, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStateCashReceiptConcept(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of CashReceiptConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.UpdateStateCashReceiptConceptAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Deletes the cash receipt concept.
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <returns></returns>
    Public Async Function DeleteCashReceiptConcept(ByVal cashReceiptConcept As CashReceiptConcepts) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteCashReceiptConceptAsync(cashReceiptConcept, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the cash receipt concept.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetCashReceiptConcept(ByVal code As String) As Task(Of CashReceiptConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptConceptAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obtener un concepto de recibo de caja asincrono
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetCashReceiptConceptByIdAsync(id As Integer) As Task(Of CashReceiptConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptConceptByIdAsync(id)
    End Function

    ''' <summary>
    ''' obtener un concepto de recibo de caja
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCashReceiptConceptById(id As Integer) As CashReceiptConcepts
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptConceptById(id)
    End Function

    ''' <summary>
    ''' Funcion para obtener el tercero
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetThirdPartyByIdSimple(ByVal id As Integer) As ThirdParty
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyById(id, _indigoSessionValues)
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

'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Juan Carlos Bermudez
' Created          : 09/09/2015
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

Public Class MPaymentOrder
    Inherits ModelBaseBudget
    Implements IDisposable

#Region "Fields"

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
        MyBase.New(Tag)
        _tagForm = Tag
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una orden de pago por código
    ''' </summary>
    ''' <param name="code">codigo del compromiso</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Async Function GetPaymentOrderByCode(code As String, BudgetaryValidityId As Integer) As Task(Of PaymentOrder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetPaymentOrderByCodeAsync(code, BudgetaryValidityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda una orden de pago
    ''' </summary>
    ''' <param name="paymentOrder"></param>
    ''' <param name="listDetailsForDelete"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePaymentOrder(paymentOrder As PaymentOrder, ByVal listDetailsForDelete As System.Collections.Generic.List(Of Integer)) As Task(Of ActionResult(Of PaymentOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SavePaymentOrderAsync(paymentOrder, listDetailsForDelete, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

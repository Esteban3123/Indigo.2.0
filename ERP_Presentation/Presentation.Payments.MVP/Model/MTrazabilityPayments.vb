'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Rafael Eduardo Patiño
' Created          : 21/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MTrazabilitypayments
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub


#Region "Methods"

    ''' <summary>
    ''' Lista las razones de rechazo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListSupplier() As Task(Of List(Of Supplier))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllSupplierAsync(Me.Indigo)
    End Function
    ''' <summary>
    ''' Lista de proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSupplierByStatus() As Object
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListSupplierByStatus(True)
    End Function

    ''' <summary>
    ''' Lista de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayablebysupplier(IdSupplier As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayablebysupplier(IdSupplier)
    End Function
    ''' <summary>
    ''' Obtene la trazabilidad de cuentas por pagar
    ''' </summary>
    ''' <param name="AccountPayableCode">codigo de la cuenta por pagar</param>
    ''' <param name="BillNumber">numero de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTrazability(AccountPayableCode As String, BillNumber As String) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetPaymentsTrazability(AccountPayableCode, BillNumber)
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

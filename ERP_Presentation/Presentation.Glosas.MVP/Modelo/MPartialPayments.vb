'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael Patiño
' Created          : 2014-10-10
'
' Last Modified By : rafael Patiño
' Last Modified On : 2014-10-10
' Description      : Modelo del frontal de pago parciales
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' Modelo del frontal de pago parciales
''' </summary>
Public Class MPartialPayments
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' tag del formulario 
    ''' </summary>
    ''' <remarks></remarks>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Sub New(Tag As String)
        Me._tagForm = Tag
    End Sub

#End Region

#Region "Methods"


    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerByNit(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerAsync(nit.Trim(), Me._indigoSessionValues)
    End Function


    ''' <summary>
    ''' Obtiene un cliente por su Id
    ''' </summary>
    ''' <param name="id">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerById(ByVal Id As String) As Task(Of Domain.Entities.Customer)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerByIdAsync(Id, Me._indigoSessionValues)
    End Function


    ''' <summary>
    ''' Listar los campos nulos de la base de datos que se permiten personalizar
    ''' </summary>
    ''' <returns>Un conjunto de datos con los campos marcados como nulos</returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("ConciliationC", Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene una lista de facturas por el nit usando las entidades XPO
    ''' </summary>
    ''' <param name="nit">Nit a consultar</param>
    ''' <returns>Un objeto <see cref="DevExpress.Xpo.XPInstantFeedbackSource" /></returns>
    Public Function ListXpoInvoicesByPartialPayments(ByVal nit As String) As Object
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.ListXpoInvoicesByPartialPayments(nit.Trim())
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene una factura por su numero y nit del cliente
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <param name="invoice">Numero de la factura</param>
    ''' <param name="listStatus">lista de estados a excluir al consultar la factura</param>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetInvoiceByNumber(ByVal nit As String, ByVal invoice As String, ByVal listStatus As List(Of String)) As Task(Of GlosaPortfolioGlosada)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListInvoicesByNumberAsync(invoice.Trim(), nit.Trim(), Me._indigoSessionValues, listStatus)
    End Function

    ''' <summary>
    ''' Funcion para obtener oficio de pagos parciales
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPartialPayments(ByVal consecutive As String) As Task(Of ActionResult(Of PartialPaymentsC))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetPartialPaymentsCAsync(consecutive, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Eliminar una factura del oficio de pago parcial
    ''' </summary>
    ''' <param name="Listtmp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeletePartialPaymentsD(ByVal Listtmp As List(Of PartialPaymentsD)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeletePartialPaymentsDAsync(Listtmp, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Guarda un oficio de pago parcial
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePartialPaymentsC(ByVal PartialPaymentsC As PartialPaymentsC) As Task(Of ActionResult(Of PartialPaymentsC))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SavePartialPaymentsCAsync(PartialPaymentsC, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Anular oficio de pago parcial
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function InvalidatePaymentsC(ByVal PartialPaymentsC As PartialPaymentsC) As Task(Of ActionResult(Of PartialPaymentsC))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.InvalidatePaymentsCAsync(PartialPaymentsC, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' confirmar un oficio de pago parcial
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmPartialPaymentsC(ByVal PartialPaymentsC As PartialPaymentsC) As Task(Of ActionResult(Of PartialPaymentsC))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmPartialPaymentsCAsync(PartialPaymentsC, Me._indigoSessionValues)
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

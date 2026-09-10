'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
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

#End Region

''' <summary>
''' Modelo del frontal de traslado cobro jurídico
''' </summary>
Public Class MTransferJuridicalDebt
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    Sub New(Tag As String)
        Me._tagForm = Tag
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un traslado cobro jurídico por su numero de consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo del traslado cobro jurídico</param>
    ''' <returns>El traslado cobro jurídico</returns>
    Public Async Function GetJuridicalByConsecutive(ByVal consecutive As Long) As Task(Of TransferJuridicalDebtCollectionC)
        Me.indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetTransferJuridicalDebtCByConsecutiveAsync(consecutive.ToString(), Me.indigo)
    End Function

    ''' <summary>
    ''' Obtiene una lista de facturas por el nit usando las entidades XPO
    ''' </summary>
    ''' <param name="nit">Nit a consultar</param>
    ''' <returns>Un objeto <see cref="DevExpress.Xpo.XPInstantFeedbackSource" /></returns>
    Public Function ListPortfolio_TransferJuridicalAccountReceivable(ByVal nit As String) As Object
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).GlosasService.ListPortfolio_TransferJuridicalAccountReceivable(nit.Trim())
    End Function

    ''' <summary>
    ''' Obtiene un listado de facturas válidas a partir del nit del tercero y de un listado
    ''' de facturas sacado de la clipboard
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <param name="list">Listado de facturas sacado de la clipboard</param>
    ''' <param name="_idUnitOperating">unidad operativa</param>
    ''' <returns>Un listado de objetos <see cref=" Domain.Entities.TransferJuridicalDebtCollectionD" /></returns>
    Public Async Function ListInvoiceByNitAndListData(ByVal nit As String, ByVal list As List(Of String), ByVal _idUnitOperating As Integer) As Task(Of List(Of Domain.Entities.TransferJuridicalDebtCollectionD))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateListInvoiceJuridicalAsync(list, nit.Trim(), Me.indigo, _idUnitOperating)
    End Function

    ''' <summary>
    ''' Obtiene la lista de facturas en el detalle del traslado cobro jurídico
    ''' </summary>
    ''' <param name="id">Id del traslado cobro jurídico</param>
    ''' <returns>El detalle del traslado cobro jurídico</returns>
    Public Async Function ListDetailJuridicalById(ByVal id As Integer) As Task(Of List(Of TransferJuridicalDebtCollectionD))
        Me.indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListJuridicalDDByIdJuridicalCAsync(id.ToString(), Me.indigo)
    End Function

    ''' <summary>
    ''' Graba la cabecera del traslado cobro jurídico
    ''' </summary>
    ''' <param name="obj">Cabecera de traslado cobro jurídico a grabar</param>
    ''' <returns>Objeto <see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function SaveJuridicalC(ByVal obj As TransferJuridicalDebtCollectionC) As Task(Of ActionResult(Of TransferJuridicalDebtCollectionC))
        Me.indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveTransferJuridicalDebtCAsync(obj, Me.indigo)
    End Function

    ''' <summary>
    ''' Confirma el traslado cobro jurídico
    ''' </summary>
    ''' <param name="obj">Traslado Cobro Jurídico cabecera</param>
    ''' <returns>ActionResult<see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function ConfirmJuridical(ByVal obj As TransferJuridicalDebtCollectionC) As Task(Of ActionResult)
        Me.indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmTransferJuridicalDebtAsync(obj, Me.indigo)
    End Function

    ''' <summary>
    ''' Reversa un traslado juridico
    ''' </summary>
    ''' <param name="idTransferJuridical"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <returns></returns>
    Public Async Function ReverseJuridical(ByVal idTransferJuridical As Integer, ByVal _IdUnitoperating As Integer) As Task(Of ActionResult(Of String))
        Me.indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ReverseTransferJuridicalAsync(idTransferJuridical, _IdUnitoperating, Me.indigo)
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerByNit(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerAsync(nit.Trim(), Me.indigo)
    End Function
    ''' <summary>
    ''' Obtiene un cliente por su Id
    ''' </summary>
    ''' <param name="id">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerById(ByVal Id As String) As Task(Of Domain.Entities.Customer)
        Me.indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerByIdAsync(Id, Me.indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.indigo)
    End Function


    Public Async Function TransferJuridicalDebtCopyPaste(ByVal unitOperatingId As Integer, customerId As Integer, transferJuridicalDebtCollectionCId As Integer, type As Integer, dataImport As List(Of ImportFileRow), data As List(Of List(Of String))) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of TransferJuridicalDebtCollectionD)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.CopyAndPasteTransferJuridicalDebtCollectionDetailAsync(unitOperatingId, customerId, transferJuridicalDebtCollectionCId, type, dataImport, data, Me.indigo)
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

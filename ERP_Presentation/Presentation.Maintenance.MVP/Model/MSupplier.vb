Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel

'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 10-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MSupplier

    Implements IDisposable

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
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

    ''' <summary>
    ''' Funcion para obtener el fabricante 
    ''' </summary>
    ''' <param name="Code">El codigo del fabricante</param>
    ''' <returns></returns>
    Public Async Function GetSupplier(ByVal Code As String) As Task(Of Supplier)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSupplierAsync(Code, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener el fabricante 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetThirdPartyByIdSupplier(ByVal id As Integer) As Domain.Entities.ThirdParty
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetThirdPartyByIdSupplier(id, Indigo)
    End Function



    ''' <summary>
    ''' Funcion para obtener el fabricante 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSupplierById(ByVal id As Integer) As Supplier
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSupplierById(id, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener el fabricante 
    ''' </summary>
    ''' <param name="id">El id del fabricante</param>
    ''' <returns></returns>
    Public Function GetThirdPartyById(ByVal id As Integer) As Domain.Entities.ThirdParty
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetThirdPartyById(id, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por el id del tercero
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSupplierByIdThirdParty(ByVal id As Integer) As Domain.Entities.Supplier
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSupplierByIdThirdParty(id, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por el id del tercero
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSupplierByIdThirdPartyWithThirdAdded(ByVal id As Integer) As Domain.Entities.Supplier
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSupplierByIdThirdPartyWithThirdAdded(id, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por id del tercero e id de la cuenta contable
    ''' </summary>
    ''' <param name="idThird">The identifier third.</param>
    ''' <param name="idAccountAccounting">The identifier account accounting.</param>
    ''' <returns></returns>
    Public Function GetSupplierByIdThirdPartyAndIdAccountAccounting(ByVal idThird As Integer, ByVal idAccountAccounting As Integer) As Domain.Entities.Supplier
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSupplierByIdThirdPartyAndIdAccountAccounting(idThird, idAccountAccounting, Indigo)
    End Function

    Public Async Function GetSequense() As Task(Of Domain.Entities.PaymentsSecuence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetSequenseByIdFormAsync(Me._tagForm)
    End Function


    ''' <summary>
    ''' Funcion para guardar el fabricante 
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveSupplier(ByVal Record As Supplier, ByVal listSupplierDistributionLines As List(Of Domain.Entities.SuppliersDistributionLines), listSupplierDetailType As List(Of Domain.Entities.SupplierDetailType), ByVal mode As Boolean, ByVal idSequense As Int64) As Task(Of ActionResult(Of Supplier))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim mess2 As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            OperationContext.Current.OutgoingMessageHeaders.Add(header2)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveSupplierAsync(Record, listSupplierDistributionLines, listSupplierDetailType, mode, Indigo)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para eliminar el fabricante
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteSupplier(ByVal Record As Supplier, ByVal listSupplierDistributionLines As List(Of Domain.Entities.SuppliersDistributionLines), listSupplierDetailType As List(Of Domain.Entities.SupplierDetailType)) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteSupplierAsync(Record, listSupplierDistributionLines, listSupplierDetailType, Indigo)
        End Using
    End Function

    Public Async Function ListAllSupplier() As Task(Of List(Of Supplier))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllBSupplierAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Listar los campos nulls de la base de datos y porder customizar 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFieldsNULL() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("Supplier", Me.Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Supplier))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateAsync(code, state, Indigo)
        End Using
    End Function

    ''' <summary>
    ''' Confirma si existen movimientos contables asociados a una cuenta bancaria de un proveedor
    ''' </summary>
    ''' <param name="supplierBankAccountId"></param>
    ''' <returns></returns>
    Public Async Function HasAccountingMovementsForSupplierBankAccount(ByVal supplierBankAccountId As Integer) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.HasAccountingMovementsForSupplierBankAccountAsync(supplierBankAccountId)
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class



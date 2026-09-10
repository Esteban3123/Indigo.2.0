'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Entities
Imports Infrastructure.Base.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports RestSharp

#End Region

Public Class MCashRegister
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
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Async Function GetAccountById(id As Integer, Optional tracking As Boolean = True) As Task(Of Domain.Entities.MainAccounts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountByIdAsync(id, tracking, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.TreasurySequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Saves the block record treasury.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecordTreasury(ByVal record As BlockRecordTreasury) As Task(Of ActionResult(Of BlockRecordTreasury))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveBlockRecordTreasuryAsync(record)
    End Function

    ''' <summary>
    ''' Deletes the block record treasury.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecordTreasury(ByVal record As BlockRecordTreasury) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteBlockRecordTreasuryAsync(record)
    End Function

    ''' <summary>
    ''' Gets the block record treasury.
    ''' </summary>
    ''' <param name="idForm">The identifier form.</param>
    ''' <param name="idRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Async Function GetBlockRecordTreasury(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordTreasury)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBlockRecordTreasuryByIdformAndIdRecordAsync(idForm, idRecord)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetNumericSequenseGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Lista todos los usuarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllUser(ByVal container As String) As DevExpress.Data.Linq.LinqInstantFeedbackSource
        'Return XpoServiceEx.Instance(container).SecurityService.GetAllUser()
        Return XpoServiceEx.Instance(container).SecurityService.ListUserByContainer(_indigoSessionValues.IndigoContainerId)
    End Function

    Public Async Function GetFirstCash(Optional currencyId As Integer? = Nothing) As Task(Of CashRegisters)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetFirstCashbyUserIdAsync(_indigoSessionValues.UserIndigoId, currencyId)
    End Function

    ''' <summary>
    ''' Saves the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <returns></returns>
    Public Async Function SaveCashRegister(ByVal cashRegister As CashRegisters, ByVal idSequence As Int64) As Task(Of ActionResult(Of CashRegisters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCashRegisterAsync(cashRegister, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStateCashRegister(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of CashRegisters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.UpdateStateCashRegisterAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Deletes the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <returns></returns>
    Public Async Function DeleteCashRegister(ByVal cashRegister As CashRegisters) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteCashRegisterAsync(cashRegister, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Getcashes the register.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetcashRegister(ByVal code As String) As Task(Of ActionResult(Of CashRegisters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashRegisterAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Getcashes the register by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetcashRegisterById(ByVal Id As Integer) As Task(Of CashRegisters)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashRegisterByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashRegister() As List(Of CashRegisters)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListCashRegister()
    End Function

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetEntityBankAccountById(ByVal Id As Integer) As Task(Of EntityBankAccounts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetEntityBankAccountByIdAsync(Id, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene la lista del valor del Iva por cada metodo de pago esto se busca por folio
    ''' </summary>
    ''' <param name="revenueControlDetailId"></param>
    ''' <param name="CurrencyId">Moneda en la que se va a facturar(opcional por defecto Moneda Oficial)</param>
    ''' <param name="paymentCurrencyId">Moneda del metodo de pago (opcional por defecto Moneda Oficial)</param>
    ''' <returns></returns>
    Public Function GetTaxDevolutionByRevenueControlDetail(revenueControlDetailId As Integer, Optional CurrencyId As Integer? = Nothing, Optional paymentCurrencyId As Integer? = Nothing) As List(Of TaxDevolution)

        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim client = New RestClient(String.Format("{0}/billing/GetTaxDevolutionByRevenueControlDetail/{1}?CurrencyId={2}&paymentCurrencyId={3}", endpoint.UrlBase, revenueControlDetailId, CurrencyId, paymentCurrencyId))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        Dim response = client.ExecuteAsync(Of ServiceResponse(Of List(Of TaxDevolution)))(req)
        response.Wait()

        If response?.Result?.Data?.Data Is Nothing Then
            Return New List(Of TaxDevolution)
        End If

        Return response?.Result?.Data?.Data
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

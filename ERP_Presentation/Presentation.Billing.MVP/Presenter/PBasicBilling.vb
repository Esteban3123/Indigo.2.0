'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-11-13
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class PBasicBilling

#Region "Fields"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IBasicBilling

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IBasicBilling)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonBilling As New MBlockRecordAndSequense(View.MyTag)
            Me.View.Sequence = Await modelCommmonBilling.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista las autorizaciones de facturacion habilitadas para el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBillingAuthorizationXPO()
        View.BillingAuthorizationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListAllBillingAuthorizationByUserCode(Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Lista los clientes activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCustomerXPO()
        View.CustomerXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListCustomerByStatus(True)
    End Sub

    ''' <summary>
    ''' Obtiene el Tercero por el Id del Cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCustomerById(Id As Integer) As CommonRepository.CommonCustomerXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of CommonRepository.CommonCustomerXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el Tercero por el Id del Cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetThirdPartyByCustomerId(ThirdPartyId As Integer) As Task(Of CommonRepository.CommonThirdPartyXpo)
        Dim filter As String = "Id = " & ThirdPartyId
        Return Await Task.Factory.StartNew(Function() As CommonRepository.CommonThirdPartyXpo
                                               Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetXPOObject(Of CommonRepository.CommonThirdPartyXpo)(filter)
                                           End Function)
    End Function

    ''' <summary>
    ''' Lista las direcciones por tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAddressXPO(PersonId As Integer)
        View.AddressXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListAddressByPersonId(PersonId)
    End Sub

    ''' <summary>
    ''' Lista los almacenes habilitadas para el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeWarehouseXPO()
        View.WarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub



    ''' <summary>
    ''' se incializa el datasource de la moneda
    ''' </summary>
    Public Sub InitializateCurrency()
        View.CurrencyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCurrency()
    End Sub

#Region "Budget Interface"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        If View.BudgetaryEntityXpo Is Nothing Then
            View.BudgetaryEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionBudgetEntityByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        If View.BudgetaryValidityXpo Is Nothing Then
            View.BudgetaryValidityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
        End If
    End Sub

    ''' <summary>
    ''' Carga el presupuesto de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBasicBillingBudget(ValidityId As Integer)
        If View.BudgetXpo Is Nothing Then
            View.BudgetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityId(ValidityId, 2, 1, False)
        End If
    End Sub

#End Region

#End Region

End Class

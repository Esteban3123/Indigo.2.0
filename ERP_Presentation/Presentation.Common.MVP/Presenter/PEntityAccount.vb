'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 10-04-2014
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
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Security.MVP

#End Region

''' <summary>
''' Presentador del frontal Entidades de Cuentas
''' </summary>
Public Class PEntityAccount

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IEntityAccount

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IEntityAccount)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Carga el datasource de los bancos
    ''' </summary>
    Public Sub InitializeBank()
        Dim modelXPO As New MBusqueda
        Me.View.BankDatasource = modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllBank)
    End Sub

    ''' <summary>
    ''' Initializes the account accounting.
    ''' </summary>
    Public Sub InitializeAccountAccounting()
        Dim modelXPO As New MBusqueda
        Dim filter() As Object = {5, True}
        Me.View.AccountAccountingDatasource = modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
    End Sub

    Public Sub InitializeUser()
        Using Model As New MEntityAccount(Me.View.MyTag)
            Me.View.DataSourceUser = Model.ListAllUser(Indigo.SecurityContainer)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia para el frontal de entidad contable
    ''' </summary>
    Public Async Sub GetSequence()
        Using ModelCommonTreasury As New MEntityAccount(Me.View.MyTag)
            Me.View.Sequence = Await ModelCommonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the city.
    ''' </summary>
    Public Sub InitializeCity()
        Dim modelXPO As New MBusqueda
        Me.View.CityDatasource = modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.AllCity)
    End Sub

    ''' <summary>
    ''' Lists the user by identifier entity bank account.
    ''' </summary>
    Public Async Sub ListUserByIdEntityBankAccount(ByVal IdEntityBankAccount As String)
        Using Model As New MEntityAccount(Me.View.MyTag)
            Dim result As ActionResult(Of List(Of EntityBankAccountUser)) = Await Model.ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount)
            Dim ListUserEntity As List(Of EntityBankAccountUser) = result.ObjectEmbbeded
            Dim ListUser As New List(Of Domain.Security.Entities.User)
            Using ModeloUsuario As New MUsuario()
                For Each UserEntity As EntityBankAccountUser In ListUserEntity
                    Dim user As Domain.Security.Entities.User = Await ModeloUsuario.ConsultarUsuarioCodigoContainerId(UserEntity.CodUser)
                    If user.Id > 0 Then
                        ListUser.Add(user)
                    End If
                Next
            End Using
            Me.View.DatasourceGridUser = ListUser
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the third party.
    ''' </summary>
    Public Sub InitializeThirdParty()
        Me.View.ThirdPartyDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Sub

    ''' <summary>
    ''' Initializes the third party.
    ''' </summary>
    Public Sub InitializeThirdPartyCounterpart()
        Me.View.ThirdPartyDatasourceCounterpart = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Sub

    Public Sub InitializeCurrency()
        Me.View.CurrencyDataSource = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCurrency(True)
    End Sub

    ''' <summary>
    ''' Fuentes de financiación
    ''' </summary>
    Public Function InitializeFinancialSource() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListAllFinancialSourceByStatus(True)
    End Function

    ''' <summary>
    ''' Initializes the third party.
    ''' </summary>
    Public Sub InitializeThirdPartyExpenses()
        Me.View.ThirdPartyDatasourceExpenses = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Sub

    ''' <summary>
    ''' Initializes the cost center.
    ''' </summary>
    Public Sub InitializeCostCenter()
        'Dim modelXPO As New MBusqueda
        Me.View.CostCenterDatasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
    End Sub

    ''' <summary>
    ''' Initializes the cost center.
    ''' </summary>
    Public Sub InitializeCostCenterCounterpart()
        'Dim modelXPO As New MBusqueda
        Me.View.CostCenterDatasourceCounterpart = Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
    End Sub
    ''' <summary>
    ''' Initializes the cost center.
    ''' </summary>
    Public Sub InitializeCostCenterExpenses()
        'Dim modelXPO As New MBusqueda
        Me.View.CostCenterDatasourceExpenses = Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
    End Sub
    ''' <summary>
    ''' Initializes the main account payment.
    ''' </summary>
    Public Sub InitializeMainAccountPayment()
        Using Model As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.DatasourceMainAccountPayment = Model.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the main account expenses.
    ''' </summary>
    Public Sub InitializeMainAccountExpenses()
        Using Model As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.DatasourceMainAccountExpense = Model.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Gets the user by code.
    ''' </summary>
    ''' <param name="userCode">The user code.</param>
    ''' <returns></returns>
    Public Async Function GetUserByCode(userCode As String) As Task(Of Domain.Security.Entities.User)

    End Function

End Class

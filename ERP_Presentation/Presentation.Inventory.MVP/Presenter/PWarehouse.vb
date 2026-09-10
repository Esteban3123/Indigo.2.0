'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/03/2014
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
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP

#End Region

Public Class PWarehouse

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IWarehouse

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Dim filter() As Object = {5, True}

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IWarehouse)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa todas las cuentas contables de los search
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeMainAccounts()
        InitializeThirdPartyAccountDebit()
        InitializeThirdPartyAccountCredit()
    End Sub

    Public Sub InitializeThirdPartyAccountDebit()
        Using modelAccountsXPO As New MBusqueda
            Me.View.ThirdPartyAccountDebitXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeThirdPartyAccountCredit()
        Using modelAccountsXPO As New MBusqueda
            Me.View.ThirdPartyAccountCreditXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeUsers()
        Using model As New MFunctionalUnit("")
            Me.View.UserXpo = model.ListAllUser(Indigo.SecurityContainer)
        End Using
    End Sub
    ''' <summary>
    ''' Metodo que Inicia el datasource de usuarios de terceros
    ''' </summary>
    Public Sub InitializeUsersRequest()
        Using model As New MFunctionalUnit("")
            Me.View.UserRequestXpo = model.ListAllUser(Indigo.SecurityContainer)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCostCenter()
        'Using modelAccountsXPO As New MBusqueda
        Me.View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplier()
        Using modelAccountsXPO As New MBusqueda
            Me.View.SupplierXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Supplier)
        End Using
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub InitializeCenterAttentions()
        Me.View.CenterAttentions = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.GetAllCareCenter()
    End Sub

    Public Sub ListConsignmentWarehouseProducts(ByVal WarehouseId As Integer)
        Me.View.ListConsignimentwarehouseProducts = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).InventoryService.GetCollectionAsList(Of Infrastructure.Data.Xpo.InventoryRepository.View.ViewListConsignmentWarehouseProductsXpo)(Nothing, $"WarehouseId={WarehouseId} And QuantityMax > 0")
    End Sub

    Public Sub ListConsignimentwarehouseProductsBatchSerial(ByVal WarehouseId As Integer, ProductId As Integer)
        Me.View.ViewListConsignmentWarehouseProductsBatchSerialXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).InventoryService.GetCollectionAsList(Of Infrastructure.Data.Xpo.InventoryRepository.View.ViewListConsignmentWarehouseProductsBatchSerialXpo)(Nothing, $"WarehouseId = {WarehouseId} and ProductId = {ProductId}")
    End Sub

    ''' <summary>
    ''' Lista las definiciones de tarifa para la rejilla de importar información
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouseConditions(warehouseId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListWarehouseConditions(warehouseId)
    End Function
#End Region

End Class

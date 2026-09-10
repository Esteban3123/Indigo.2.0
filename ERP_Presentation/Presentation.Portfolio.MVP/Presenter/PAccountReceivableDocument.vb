'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 17/06/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PAccountReceivableDocument

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IAccountReceivableDocument

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IAccountReceivableDocument)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' inicializa los clientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCustomerXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {True}
            Me.View.CustomerXPO = model.ConsultarEntidades(eDataSource.ListCustomerByStatus, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenterXPO()
        'Using model As New MBusqueda
        Me.View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las Cuentas
    ''' </summary>
    Public Sub InitializeAccountXPO()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.MainAccountXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa el datasource de la moneda
    ''' </summary>
    Public Sub InitializeCurrency()
        Using model As New MBusqueda
            Me.View.CurrencyDataSourceXpo = model.ConsultarEntidades(eDataSource.Currency)
        End Using
    End Sub

#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
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
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class POpeningBalance

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IOpeningBalance

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IOpeningBalance)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub Initialize()
        Using model As New MBusqueda
            
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa los datasources de los controles de proveedores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSuppliers()
        Using model As New MBusqueda
            Me.View.SuppliersDistributionLinesAdvanceXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines)
            Me.View.SuppliersDistributionLinesBillXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines)
        End Using
    End Sub

    Public Sub InitializeCostCenters()
        'Using model As New MBusqueda
        Me.View.CostCenterAdvanceXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        Me.View.CostCenterBillXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de unidad de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFilingUnit()
        Using model As New MBusqueda
            Me.View.FilingUnitXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFilingUnitByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el tipo de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplierType()
        Using model As New MBusqueda
            Me.View.SupplierTypeXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierTypeByStatusTreeList, True)
        End Using
    End Sub

    ''' <summary>
    ''' Carga las definiciones del layout
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequensePayments(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeAccountXpo()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

#End Region

End Class

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
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class PNotesDebitCredit

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As INotesDebitCredit

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
    Public Sub New(ByRef iview As INotesDebitCredit)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de proveedores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplier()
        Using model As New MBusqueda
            Me.View.SuppliersDistributionLinesXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines), DevExpress.Xpo.XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCostCenter()
        ' Using model As New MBusqueda
        Me.View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        ' End Using
    End Sub

    ''' <summary>
    ''' Trae el data source de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListAccountPayableXpo()
        ' Using model As New MBusqueda
        Me.View.AccountPayableXpo = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PaymentsService.ListAccountPayablebysupplier(0)
        ' End Using
    End Sub


    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequensePayments(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    ''' <summary>
    ''' Metodo para consultar las facturas por id del proveedor y el estado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAccountPayableDatasource(ByVal IdSupplier As Integer, ByVal Nature As Byte, ByVal MainAccountIdDistibutionLine As Integer)
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {IdSupplier, 2, Nature, MainAccountIdDistibutionLine}
            View.AccountPayableDatasource = ModelXpo.ConsultarEntidades(eDataSource.ListAccountPayableByIdSupplierAndState, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para consultar los anticipos a proveedores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAdvancePayments(ByVal IdSupplier As Integer, ByVal Nature As Byte)
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {IdSupplier, 2, Nature}
            View.AdvancePaymentsXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAdvancePayments, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa la entidad de cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEntityAccountPayable(ByVal idAccountPayable As Integer)
        Using model As New MAccountPayable("")
            View.accountPayable = model.GetAccountPayableById(idAccountPayable)
        End Using
    End Sub

    Public Function GetSupplierById(Id As Integer) As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier)(Nothing, filtroConsulta).FirstOrDefault
    End Function

    Public Function GetSuppliersDistributionLinesById(Id As Integer) As Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function
    ''' <summary>
    ''' Obtiene los parametros de pagos por unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer)
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Me.View.PaymentsSettingPaymentsXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetXPOObject(Of PaymentsSettingPaymentsXpo)(filter)
    End Sub

    ''' <summary>
    ''' inicializa el combo de moneda
    ''' </summary>
    Public Sub InitializeCurrency()
        Using ModelXpo As New MBusqueda
            View.DataSourceCurrency = ModelXpo.ConsultarEntidades(eDataSource.Currency)
        End Using
    End Sub
#End Region

End Class

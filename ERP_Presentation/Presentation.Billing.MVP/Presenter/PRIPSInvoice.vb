#Region "Imports"

Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo

#End Region

Public Class PRIPSInvoice

#Region "Builder"

    Private _view As IRIPSInvoice

    Public Sub New(view As IRIPSInvoice)
        _view = view
    End Sub

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Lista los centros de atención
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCareCenter()
        If _view.CareCenterDatasource Is Nothing Then
            _view.CareCenterDatasource = XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.ListCenters()
        End If
    End Sub

    ''' <summary>
    ''' Lista todos los contratos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeContract()
        If _view.ContractDatasource Is Nothing Then
            _view.ContractDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).ContractService.ListContract()
        End If
    End Sub

    ''' <summary>
    ''' Lista las entidades administradoras
    ''' </summary>
    Public Sub InitializeHealthAdministrator()
        If _view.HealthAdministratorDatasource Is Nothing Then
            _view.HealthAdministratorDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).ContractService.ListHealthAdministratorByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' Lista los grupos de atención
    ''' </summary>
    Public Sub InitializeCareGroup()
        If _view.CareGroupDatasource Is Nothing Then
            _view.CareGroupDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).ContractService.ListCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Lista categorias de factura
    ''' </summary>
    Public Sub InitializeInvoiceCategory()
        If _view.InvoiceCategoryDatasource Is Nothing Then
            _view.InvoiceCategoryDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).BillingService.ListInvoiceCategories()
        End If
    End Sub

    ''' <summary>
    ''' Lista los grupos poblacionales
    ''' </summary>
    Public Sub InitializePopulationGroup()
        If _view.PopulationGroupDatasource Is Nothing Then
            _view.PopulationGroupDatasource = XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.ListPopulationGroup()
        End If
    End Sub

    ''' <summary>
    ''' Lista las admisiones
    ''' </summary>
    Public Sub InitializeAdmission()
        If _view.AdmissionDatasource Is Nothing Then
            _view.AdmissionDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).BillingService.ListRevenueControlActive()
        End If
    End Sub

    Public Sub InitializeIncomeCause()
        If _view.IncomeCauseDatasource Is Nothing Then
            _view.IncomeCauseDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).BillingService.ListActiveIncomeCauses()
        End If
    End Sub
#End Region

End Class

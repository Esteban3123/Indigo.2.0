'***********************************************************************
' Assembly         : Presentacion.Portfolio
' Author           : Carlos Ernesto Córdoba
' Created          : 11-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Base
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Glosas
Imports Presentation.Glosas.MVP
Imports Presentation.Portfolio.MVP
#End Region

Public Class FrmPopupBills

#Region "TUPLES"
    Dim _listPortfolioStatus As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListPortfolioStatus As List(Of Tuple(Of Byte, String))
        Get
            If _listPortfolioStatus Is Nothing Then
                _listPortfolioStatus = New List(Of Tuple(Of Byte, String))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(1, "Sin Radicar"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(2, "Radicada"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(3, "Radicada Entidad"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(4, "Objetada"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(5, "Contestada Radicada"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(6, "Aceptada "))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(7, "Certificada Parcial"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(8, "Certificada Total"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(9, "No Subsanable"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(10, "Difícil Recaudo"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(11, "Factura Devuelta"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(12, "Glosa Ratificada"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(13, "Radicación Tramite Objeción"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(14, "Devolución Factura"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(15, "Cuenta Dificil Recaudo"))
                _listPortfolioStatus.Add(New Tuple(Of Byte, String)(16, "Cobro Jurídico"))
            End If
            Return _listPortfolioStatus
        End Get
    End Property


#End Region

#Region "EVENTS"
    ''' <summary>
    ''' Evento publico para agregar facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddBill(sender As Object, e As AddBillEventArgs)
    ''' <summary>
    ''' evento para poner nula la entidad cuando se este edidanto y se presione en deshacer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event SetNothingPortfolioInitialBalanceAccountReceivable(sender As Object, e As EventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' representa la entidad donde se relacionan las facturas con el saldo inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private portfolioInitialBalanceAccountReceivable As PortfolioInitialBalanceAccountReceivable
    ''' <summary>
    ''' representa la entidad donde se agregan la cuentas cuando son facturas totales
    ''' </summary>
    ''' <remarks></remarks>
    Private portfolioInitialBalanceAccountReceivableAccounting As PortfolioInitialBalanceAccountReceivableAccounting
    ''' <summary>
    ''' listado de la relacion entre la factura y las cuentas 
    ''' </summary>
    ''' <remarks></remarks>
    Private listPortfolioInitialBalanceAccountReceivableAccounting As List(Of PortfolioInitialBalanceAccountReceivableAccounting)
    ''' <summary>
    ''' listado de la relacion entre la factura y las cuentas para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private listPortfolioInitialBalanceAccountReceivableAccountingDelete As List(Of PortfolioInitialBalanceAccountReceivableAccounting)
    ''' <summary>
    ''' bandera para saber si se esta editando en el pupop de cuentas
    ''' </summary>
    ''' <remarks></remarks>
    Private popupEditMode As Boolean
    ''' <summary>
    ''' bandera para saber si se cambio la cuenta en el modo edicion del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private AcconunValueChangeEditMode As Boolean
    ''' <summary>
    ''' listado de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private _listBills As List(Of PortfolioInitialBalanceAccountReceivable)
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' data source para cargar las cuentas que se seleccionaron
    ''' </summary>
    ''' <remarks></remarks>
    Private listMainAccountsDataSource As List(Of MainAccounts)
    ''' <summary>
    ''' listado con las cuotas
    ''' </summary>
    ''' <remarks></remarks>
    Private listPortfolioInitialBalanceAccountReceivableShare As List(Of PortfolioInitialBalanceAccountReceivableShare)
    ''' <summary>
    ''' bandera para saber si se esta editando el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private _editMode As Boolean
#End Region

#Region "PROPERTIES"

    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property
    Property AccountWithoutRadicateXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountWithoutRadicate.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountWithoutRadicate.Properties.DataSource = value
        End Set
    End Property

    Property AccountRadicateXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountRadicate.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountRadicate.Properties.DataSource = value
        End Set
    End Property

    Property AccountObjectionRemediedXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountObjectionRemedied.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountObjectionRemedied.Properties.DataSource = value
        End Set
    End Property

    Property AccountConciliationXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountConciliation.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountConciliation.Properties.DataSource = value
        End Set
    End Property

    Property AccountLegalCollectionXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountLegalCollection.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountLegalCollection.Properties.DataSource = value
        End Set
    End Property

    Property AccountDebtorOrderXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountDebtorOrder.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountDebtorOrder.Properties.DataSource = value
        End Set
    End Property

    Property AccountCreditorOrderXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountCreditorOrder.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountCreditorOrder.Properties.DataSource = value
        End Set
    End Property


    Public Property ListBills As List(Of PortfolioInitialBalanceAccountReceivable)
        Get
            Return _listBills
        End Get
        Set(value As List(Of PortfolioInitialBalanceAccountReceivable))
            _listBills = value
        End Set
    End Property
    Public Property Bill As PortfolioInitialBalanceAccountReceivable
        Get
            Return portfolioInitialBalanceAccountReceivable
        End Get
        Set(value As PortfolioInitialBalanceAccountReceivable)
            portfolioInitialBalanceAccountReceivable = value
        End Set
    End Property
    Property CustomerXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleCustomer.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCustomer.Properties.DataSource = value
        End Set
    End Property
    Property MainAccountTotalXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSLeAccountTotal.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSLeAccountTotal.Properties.DataSource = value
        End Set
    End Property
    Property CostCenterTotalXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleCostCenterTotal.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenterTotal.Properties.DataSource = value
        End Set
    End Property
    Property MainAccountShareXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountShare.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleAccountShare.Properties.DataSource = value
        End Set
    End Property
    Property CostCenterShareXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleCostCenterShare.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenterShare.Properties.DataSource = value
        End Set
    End Property

    Property GlosasCostCenterXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleGlosasCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleGlosasCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' establece las cuentas en solo lectura
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property SetReadOnlyAccounts As Boolean
        Set(value As Boolean)
            INDSleAccountWithoutRadicate.Properties.ReadOnly = value
            INDSleAccountRadicate.Properties.ReadOnly = value
            INDSleAccountObjectionRemedied.Properties.ReadOnly = value
            INDSleAccountConciliation.Properties.ReadOnly = value
            INDSleAccountLegalCollection.Properties.ReadOnly = value
            INDSleAccountDebtorOrder.Properties.ReadOnly = value
            INDSleAccountCreditorOrder.Properties.ReadOnly = value
        End Set
    End Property
#End Region

#Region "METHODS"
    Private Sub CleanControls()
        INDSeNumerShares.EditValue = 1
        INDSleCustomer.EditValue = Nothing
        INDSleCustomer.Properties.NullText = String.Empty
        INDTxtInvoiceNumber.Text = String.Empty
        INDGlePortfolioStatus.EditValue = Nothing
        INDSleInvoiceCategory.EditValue = Nothing
        INDDteInvoiceDate.EditValue = Nothing
        INDDteInvoiceDate.EditValue = GetDateServer()
        INDSeTerm.EditValue = Nothing
        INDSeTerm.EditValue = 0
        INDSeNumerShares.EditValue = 1
        INDSeNumerShares.Enabled = True
        INDTxtValueBill.EditValue = 0
        INDTxtValueBill.Properties.ReadOnly = False
        INDTxtValueBalance.EditValue = 0
        INDTxtValueBalance.Properties.ReadOnly = True
        INDGleAffectBudget.EditValue = False
        INDLciBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSleBudget.EditValue = Nothing
        INDSleBudget.Properties.NullText = String.Empty
        INDGleIsElectronicInvoice.EditValue = False
        INDLciCUFE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDtxtCUFE.EditValue = Nothing
        INDtxtCUFE.Properties.NullText = String.Empty
        INDMeObservation.Text = String.Empty
        INDSleGlosasCostCenter.EditValue = Nothing
        INDSleGlosasCostCenter.Properties.NullText = String.Empty
        INDSleAccountWithoutRadicate.EditValue = Nothing
        INDSleAccountWithoutRadicate.Properties.NullText = String.Empty
        INDSleAccountRadicate.EditValue = Nothing
        INDSleAccountRadicate.Properties.NullText = String.Empty
        INDSleAccountRadicate.Enabled = False
        INDSleAccountObjectionRemedied.EditValue = Nothing
        INDSleAccountObjectionRemedied.Properties.NullText = String.Empty
        INDSleAccountObjectionRemedied.Enabled = False
        INDSleAccountConciliation.EditValue = Nothing
        INDSleAccountConciliation.Properties.NullText = String.Empty
        INDSleAccountConciliation.Enabled = False
        INDSleAccountLegalCollection.EditValue = Nothing
        INDSleAccountLegalCollection.Properties.NullText = String.Empty
        INDSleAccountLegalCollection.Enabled = False
        INDSleAccountDebtorOrder.EditValue = Nothing
        INDSleAccountDebtorOrder.Properties.NullText = String.Empty
        INDSleAccountDebtorOrder.Enabled = False
        INDSleAccountCreditorOrder.EditValue = Nothing
        INDSleAccountCreditorOrder.Properties.NullText = String.Empty
        INDSleAccountCreditorOrder.Enabled = False
        INDSleAccountShare.EditValue = Nothing
        INDSleCostCenterShare.EditValue = Nothing
        INDPceAccount.Enabled = False
        INDGcAccount.DataSource = Nothing
        INDGcShares.DataSource = Nothing
        INDBtnAdd.Text = ResourceManager.GetString("Add")
        HideFieldsCompanyType()
        SetReadOnlyAccounts = False
        portfolioInitialBalanceAccountReceivable = Nothing
        portfolioInitialBalanceAccountReceivableAccounting = Nothing
        listPortfolioInitialBalanceAccountReceivableAccounting = Nothing
        listPortfolioInitialBalanceAccountReceivableAccountingDelete = Nothing
        listPortfolioInitialBalanceAccountReceivableShare = Nothing
        IndigoGridControl1.RefreshGrid(INDGcAccount)
        INDSleCustomer.Focus()
    End Sub

    ''' <summary>
    ''' metodo para ocultar campos segun el tipo de compañia
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideFieldsCompanyType()
        If _indigoSession.IndigoCompanyType = 1 Then ' entidades privadas
            INDLciAccountObjectionRemedied.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccountConciliation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccountLegalCollection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccountDebtorOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAccountCreditorOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else 'entidades publicas
            INDLciAccountObjectionRemedied.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAccountConciliation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAccountLegalCollection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAccountDebtorOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccountCreditorOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo para facturas totales
    ''' </summary>
    Public Sub InitializeCostCenterTotalXPO()
        'Using model As New MBusqueda
        Me.CostCenterTotalXPO = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo para facturas por cuotas
    ''' </summary>
    Public Sub InitializeCostCenterShareXPO()
        'Using model As New MBusqueda
        Me.CostCenterShareXPO = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(eDataSource.CostCenter)
        'End Using
    End Sub
    ''' <summary>
    ''' inicializa los centros de costo para glosas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeGlosasCostCenterXPO()
        'Using model As New MBusqueda
        Me.GlosasCostCenterXPO = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' inicializa los clientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCustomerXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {True}
            Me.CustomerXPO = model.ConsultarEntidades(eDataSource.ListCustomerByStatus, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa las cuentas contables para facturas totales
    ''' </summary>
    Public Sub InitializeMainAccountTotalXPO()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.MainAccountTotalXPO = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa las cuentas contables para facturas totales
    ''' </summary>
    Public Sub InitializeMainAccountShareXPO()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.MainAccountShareXPO = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Private Function ValidatePopupAccount() As String
        Dim errors As New StringBuilder
        If INDTxtValueBill.EditValue = 0 Then
            errors.AppendLine("No se ha establecido el valor de la factura")
        End If
        If INDSLeAccountTotal.EditValue Is Nothing Then
            errors.AppendLine(INDLciAccountTotal.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDLciCostCenterTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSleCostCenterTotal.EditValue Is Nothing Then
                errors.AppendLine(INDLciCostCenterTotal.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
            If popupEditMode = False Then
                If listPortfolioInitialBalanceAccountReceivableAccounting IsNot Nothing AndAlso listPortfolioInitialBalanceAccountReceivableAccounting.Count > 0 Then
                    Dim account = listPortfolioInitialBalanceAccountReceivableAccounting.Find(Function(x) x.MainAccountId = INDSLeAccountTotal.EditValue And x.CostCenterId = INDSleCostCenterTotal.EditValue)
                    If account IsNot Nothing Then
                        errors.AppendLine(ResourceManager.GetString("AccountCostCenterExists", "Portfolio"))
                    End If
                End If
            End If
        Else
            If popupEditMode = False Then
                If listPortfolioInitialBalanceAccountReceivableAccounting IsNot Nothing AndAlso listPortfolioInitialBalanceAccountReceivableAccounting.Count > 0 Then
                    Dim account = listPortfolioInitialBalanceAccountReceivableAccounting.Find(Function(x) x.MainAccountId = INDSLeAccountTotal.EditValue)
                    If account IsNot Nothing Then
                        errors.AppendLine(ResourceManager.GetString("AccountExists", "Portfolio"))
                    End If
                End If
            End If
        End If
        If INDTxtValueTotal.EditValue = 0 Then
            errors.AppendLine(INDLciValueTotal.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If listPortfolioInitialBalanceAccountReceivableAccounting IsNot Nothing AndAlso listPortfolioInitialBalanceAccountReceivableAccounting.Count > 0 Then
            Dim valueAccounts As Decimal
            If popupEditMode = True Then
                valueAccounts = listPortfolioInitialBalanceAccountReceivableAccounting.Where(Function(c) Not c.Equals(portfolioInitialBalanceAccountReceivableAccounting)).Sum(Function(x) x.Value)
            Else
                valueAccounts = listPortfolioInitialBalanceAccountReceivableAccounting.Sum(Function(x) x.Value)
            End If

            valueAccounts += INDTxtValueTotal.EditValue
            If valueAccounts > INDTxtValueBill.EditValue Then
                errors.AppendLine("No se puede agregar el registro porque el saldo seria mayor al valor de la factura")
            End If
        Else
            If INDTxtValueTotal.EditValue > INDTxtValueBill.EditValue Then
                errors.AppendLine("No se puede agregar el registro porque el saldo seria mayor al valor de la factura")
            End If
        End If
        Return errors.ToString()
    End Function

    Private Sub CleanControlsPopupAccount()
        INDLciCostCenterTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If INDPceAccount.IsPopupOpen = False Then
            INDPceAccount.Properties.PopupSizeable = True
            INDPccAccount.Size = New Size(416, 192)
            INDPceAccount.Properties.PopupSizeable = False
        End If
        INDBtnAddAccount.Text = ResourceManager.GetString("Add")
        INDSLeAccountTotal.EditValue = Nothing
        INDSLeAccountTotal.Properties.NullText = String.Empty
        INDSleCostCenterTotal.EditValue = Nothing
        INDSleCostCenterTotal.Properties.NullText = String.Empty
        INDTxtValueTotal.EditValue = 0
        popupEditMode = False
        AcconunValueChangeEditMode = False
        Me.portfolioInitialBalanceAccountReceivableAccounting = Nothing
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvAccount, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvAccount.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' metodo para establecer la fecha de vencimiento de la factura
    ''' </summary>
    ''' <param name="totalDays"></param>
    ''' <remarks></remarks>
    Private Sub SetExpiredDate(totalDays As Integer)
        If INDDteInvoiceDate.EditValue IsNot Nothing Then
            INDDteExpiredDate.EditValue = CDate(INDDteInvoiceDate.EditValue).AddDays(totalDays)
            INDDteExpiredDate.Properties.MinValue = INDDteExpiredDate.EditValue
        End If
    End Sub

    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If INDSleCustomer.EditValue = Nothing Then
            errors.AppendLine(INDLciCustomer.Text + ResourceManager.GetString("Empty"))
        End If
        If INDTxtInvoiceNumber.Text = String.Empty Then
            errors.AppendLine(INDLciInvoiceNumber.Text + ResourceManager.GetString("Empty"))
        End If
        If INDGlePortfolioStatus.EditValue Is Nothing Then
            errors.AppendLine("Estado Cartera Vacío")
        End If
        If INDTxtValueBill.EditValue = 0 Then
            errors.AppendLine("Valor Factura Vacío")
        End If
        If INDLciBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSleBudget.EditValue Is Nothing Then
                errors.AppendLine("Presupuesto Vacío")
            End If
        End If

        If INDSeNumerShares.EditValue = 1 Then
            If INDTxtValueBalance.EditValue = 0 Then
                errors.AppendLine("Valor Saldo Vacío")
            End If

            If INDSleGlosasCostCenter.EditValue Is Nothing Then
                errors.AppendLine(INDLciGlosasCostCenter.Text + ResourceManager.GetString("Empty"))
            End If
            If INDSleAccountWithoutRadicate.EditValue Is Nothing Then
                errors.AppendLine(INDLciAccountWithoutRadicate.Text + ResourceManager.GetString("Empty"))
            End If
            If INDSleAccountRadicate.EditValue Is Nothing Then
                errors.AppendLine(INDLciAccountRadicate.Text + ResourceManager.GetString("Empty"))
            End If
            If _indigoSession.IndigoCompanyType = 1 Then ' entidades privadas
                If INDSleAccountObjectionRemedied.EditValue Is Nothing Then
                    errors.AppendLine(INDLciAccountObjectionRemedied.Text + ResourceManager.GetString("Empty"))
                End If
                If INDSleAccountConciliation.EditValue Is Nothing Then
                    errors.AppendLine(INDLciAccountConciliation.Text + ResourceManager.GetString("Empty"))
                End If
                If INDSleAccountLegalCollection.EditValue Is Nothing Then
                    errors.AppendLine(INDLciAccountLegalCollection.Text + ResourceManager.GetString("Empty"))
                End If

            Else 'entidades publicas
                If INDSleAccountDebtorOrder.EditValue Is Nothing Then
                    errors.AppendLine(INDLciAccountDebtorOrder.Text + ResourceManager.GetString("Empty"))
                End If
                If INDSleAccountCreditorOrder.EditValue Is Nothing Then
                    errors.AppendLine(INDLciAccountCreditorOrder.Text + ResourceManager.GetString("Empty"))
                End If
            End If

            If INDGvAccount.RowCount = 0 Then
                errors.AppendLine(ResourceManager.GetString("AccountEmpty", "Portfolio"))
            End If
        Else
            If INDSleAccountShare.EditValue = Nothing Then
                errors.AppendLine(INDLciAccountShare.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
            If INDLciCostCenterShare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDSleCostCenterShare.EditValue = Nothing Then
                    errors.AppendLine(INDLciCostCenterShare.CustomizationFormText + ResourceManager.GetString("Empty"))
                End If
            End If
            Dim shareNotValue = listPortfolioInitialBalanceAccountReceivableShare.FindAll(Function(x) x.Value = 0)
            If shareNotValue.Count > 0 Then
                errors.AppendLine("Se encontraron cuotas sin valor")
            End If
        End If
        If ListBills IsNot Nothing Then
            If portfolioInitialBalanceAccountReceivable Is Nothing Then
                Dim bill = ListBills.Find(Function(x) x.InvoiceNumber = INDTxtInvoiceNumber.Text)
                If bill IsNot Nothing Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("AddedBill", "Portfolio"), INDTxtInvoiceNumber.Text))
                End If
            End If
        End If
        If Not INDTxtInvoiceNumber.Text = String.Empty Then
            Using model As New MAccountReceivable(Tag)
                Dim bill = model.GetAccountReceivableByInvoiceNumber(INDTxtInvoiceNumber.Text)
                If bill IsNot Nothing AndAlso bill.Id > 0 Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("BillExists", "Portfolio"), INDTxtInvoiceNumber.Text))
                End If
            End Using
        End If
        Return errors.ToString()
    End Function

    Private Sub Deshacer()
        CleanControlsPopupAccount()
        CleanControls()
    End Sub

    Private Sub loadControlsForEdit(_portfolioInitialBalanceAccountReceivable As PortfolioInitialBalanceAccountReceivable)
        With _portfolioInitialBalanceAccountReceivable
            HideFieldsCompanyType()
            INDSleCustomer.EditValue = .CustomerId
            INDSleCustomer.Properties.NullText = .CodeNameCustomer
            INDTxtInvoiceNumber.Text = .InvoiceNumber
            INDGlePortfolioStatus.EditValue = .PortfolioStatus
            INDSleInvoiceCategory.EditValue = .InvoiceCategoryId
            INDDteInvoiceDate.EditValue = .AccountReceivableDate
            INDSeTerm.EditValue = .Term
            INDDteExpiredDate.EditValue = .ExpiredDate

            INDTxtValueBill.EditValue = .Value
            INDTxtValueBalance.EditValue = .Balance
            INDGleAffectBudget.EditValue = .AffectBudget
            INDSleBudget.EditValue = .BudgetId
            INDGleIsElectronicInvoice.EditValue = .IsElectronicInvoice
            INDtxtCUFE.Text = .CUFE
            INDMeObservation.Text = .Observations
            If .NumberShares = 1 Then

                INDSleGlosasCostCenter.EditValue = .CostCenterId
                INDSleGlosasCostCenter.Properties.NullText = .CodeNameGlosasCostCenter
                INDSleAccountWithoutRadicate.EditValue = .AccountWithoutRadicateId
                INDSleAccountWithoutRadicate.Properties.NullText = .CodeNameAccountWithoutRadicate
                INDSleAccountRadicate.EditValue = .AccountRadicateId
                INDSleAccountRadicate.Properties.NullText = .CodeNameAccountRadicate
                INDSleAccountObjectionRemedied.EditValue = .AccountObjectionRemediedId
                INDSleAccountObjectionRemedied.Properties.NullText = .CodeNameAccountObjectionRemedied
                INDSleAccountConciliation.EditValue = .AccountConciliationId
                INDSleAccountConciliation.Properties.NullText = .CodeNameAccountConciliation
                INDSleAccountLegalCollection.EditValue = .AccountLegalCollectionId
                INDSleAccountLegalCollection.Properties.NullText = .CodeNameAccountLegalCollection
                INDSleAccountDebtorOrder.EditValue = .AccountDebtorOrder
                INDSleAccountDebtorOrder.Properties.NullText = .CodeNameAccountDebtorOrder
                INDSleAccountCreditorOrder.EditValue = .AccountCreditorOrder
                INDSleAccountCreditorOrder.Properties.NullText = .CodeNameAccountCreditorOrder
                SetDatasourceAccounts()
                listPortfolioInitialBalanceAccountReceivableAccounting = New List(Of PortfolioInitialBalanceAccountReceivableAccounting)
                Me.listPortfolioInitialBalanceAccountReceivableAccounting.AddRange(.PortfolioInitialBalanceAccountReceivableAccounting)
                INDGcAccount.DataSource = Nothing
                INDGcAccount.DataSource = Me.listPortfolioInitialBalanceAccountReceivableAccounting
            Else
                IndigoGridControl1.RefreshGrid(INDGcAccount)
                INDSleAccountShare.EditValue = .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).MainAccountId
                INDSleAccountShare.Properties.NullText = .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).CodeNameMainAccount
                INDSleCostCenterShare.EditValue = .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).CostCenterId
                INDSleCostCenterShare.Properties.NullText = .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).CodeNameCostCenter
                For Each item In .PortfolioInitialBalanceAccountReceivableShare
                    If listPortfolioInitialBalanceAccountReceivableShare Is Nothing Then
                        listPortfolioInitialBalanceAccountReceivableShare = New List(Of PortfolioInitialBalanceAccountReceivableShare)
                    End If
                    listPortfolioInitialBalanceAccountReceivableShare.Add(item.CloneEntity())
                Next
                INDGcShares.DataSource = listPortfolioInitialBalanceAccountReceivableShare
            End If
            INDSeNumerShares.EditValue = .NumberShares
        End With
        INDSleCustomer.Focus()
    End Sub

    WriteOnly Property ListPortfolioInitialBalanceAccountReceivableSharesDatasource As Domain.Entities.TrackableCollection(Of PortfolioInitialBalanceAccountReceivableShare)
        Set(value As Domain.Entities.TrackableCollection(Of PortfolioInitialBalanceAccountReceivableShare))
            listPortfolioInitialBalanceAccountReceivableShare = New List(Of PortfolioInitialBalanceAccountReceivableShare)(value.ToArray)
        End Set
    End Property

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub
#End Region

#Region "HANDLES"
#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        portfolioInitialBalanceAccountReceivable = Nothing
        portfolioInitialBalanceAccountReceivableAccounting = Nothing
        listPortfolioInitialBalanceAccountReceivableAccounting = Nothing
        listPortfolioInitialBalanceAccountReceivableAccountingDelete = Nothing
        popupEditMode = Nothing
        AcconunValueChangeEditMode = Nothing
        _listBills = Nothing
        listMainAccountsDataSource = Nothing
        listPortfolioInitialBalanceAccountReceivableShare = Nothing
        _editMode = Nothing
    End Sub

    Private Sub FrmPopupBills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _indigoSession = SessionValues.Instance
        INDGlePortfolioStatus.Properties.DataSource = ListPortfolioStatus
        Using model As New MPortfolioInitialBalance(Me.Tag)
            INDSleBudget.Properties.DataSource = model.ListBudgetByCategoryItemType()
            INDSleInvoiceCategory.Properties.DataSource = model.ListInvoiceCategories()
        End Using
        INDSleBudget.Properties.Buttons(1).Visible = False
        If Bill IsNot Nothing Then
            loadControlsForEdit(Bill)
            INDBtnAdd.Text = ResourceManager.GetString("Edit")
            INDSleCustomer.Focus()
        Else
            CleanControls()
        End If
        CleanControlsPopupAccount()
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.Minimizar(True)
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        AddActionsColumns()
    End Sub
#End Region

#Region "Shown"
    Private Sub FrmPopupBills_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleCustomer.Focus()
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmPopupBills_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If INDSleCustomer.EditValue IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Sub INDSleTransferLegalJournalVoucherType_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            INDPceAccount.Focus()
            INDSLeAccountTotal.Focus()
            INDPceAccount.ShowPopup()
        End If
    End Sub

    Private Sub INDMeObservation_KeyDown(sender As Object, e As KeyEventArgs) Handles INDMeObservation.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDSeNumerShares.EditValue = 1 Then
                INDSleGlosasCostCenter.Focus()
            Else
                INDSleAccountShare.Focus()
            End If
        End If
    End Sub

    Private Sub INDTxtValueShare_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    Private Sub FrmPopupBills_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDTxtInvoiceNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles INDTxtInvoiceNumber.KeyDown
        If e.KeyCode = Keys.Enter Then
            Using model As New MAccountReceivable(Tag)
                If INDTxtInvoiceNumber.Text Is String.Empty Then
                    Exit Sub
                End If
                Dim bill = model.GetAccountReceivableByInvoiceNumber(INDTxtInvoiceNumber.Text)
                If bill IsNot Nothing AndAlso bill.Id > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = (String.Format(ResourceManager.GetString("BillExists", "Portfolio"), INDTxtInvoiceNumber.Text))
                    INDTxtInvoiceNumber.Text = String.Empty
                    e.SuppressKeyPress = True
                End If
            End Using
        End If
    End Sub

    Private Sub INDSleAccountLegalCollection_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSleAccountLegalCollection.KeyDown, INDSleAccountCreditorOrder.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPceAccount.Focus()
            INDPceAccount.ShowPopup()
            INDSLeAccountTotal.Focus()
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    Private Sub INDSeNumerShares_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeNumerShares.EditValueChanged
        If INDSeNumerShares.EditValue = 1 Then
            INDTxtValueBalance.EditValue = 0
            INDTxtValueBalance.Properties.ReadOnly = True
            INDLcgGlosasAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgTotalBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgShareBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgShares.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDTxtValueBalance.Properties.ReadOnly = True
            INDLcgGlosasAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgTotalBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgShareBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgShares.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If listPortfolioInitialBalanceAccountReceivableShare IsNot Nothing Then
                If INDSeNumerShares.EditValue < listPortfolioInitialBalanceAccountReceivableShare.Count Then
                    listPortfolioInitialBalanceAccountReceivableShare.RemoveAll(Function(x) x.Number > INDSeNumerShares.EditValue)
                    Dim balanceTmp = listPortfolioInitialBalanceAccountReceivableShare.Where(Function(c) c.Value > 0).Sum(Function(x) x.Value)
                    INDTxtValueBalance.EditValue = balanceTmp
                    INDGcShares.DataSource = listPortfolioInitialBalanceAccountReceivableShare
                    INDGcShares.RefreshDataSource()
                    Exit Sub
                End If
            End If
            Dim index As Integer = 0
            If portfolioInitialBalanceAccountReceivable Is Nothing Then
                listPortfolioInitialBalanceAccountReceivableShare = New List(Of PortfolioInitialBalanceAccountReceivableShare)
            Else
                index = listPortfolioInitialBalanceAccountReceivableShare.Count
            End If

            Dim expiredDate = CDate(INDDteInvoiceDate.EditValue)
            For i As Integer = index To INDSeNumerShares.EditValue - 1 Step 1
                expiredDate = expiredDate.AddDays(INDSeTerm.EditValue)
                Dim PortfolioInitialBalanceAccountReceivableShare = New PortfolioInitialBalanceAccountReceivableShare
                PortfolioInitialBalanceAccountReceivableShare.Number = i + 1
                PortfolioInitialBalanceAccountReceivableShare.ExpiredDate = expiredDate
                listPortfolioInitialBalanceAccountReceivableShare.Add(PortfolioInitialBalanceAccountReceivableShare)
            Next
            INDGcShares.DataSource = listPortfolioInitialBalanceAccountReceivableShare
            INDGcShares.RefreshDataSource()
        End If

    End Sub

    Private Sub INDSleAccountShare_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountShare.EditValueChanged
        If INDSleAccountShare.EditValue IsNot Nothing Then
            Using model As New MPUC(Me.Tag)
                Dim account = model.GetAccountByIdSimple(INDSleAccountShare.EditValue, False)
                If account.HandlesCostCenter = True Then
                    INDLciCostCenterShare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDLciCostCenterShare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSleCostCenterShare.EditValue = Nothing
                End If
            End Using
        Else
            INDSleCostCenterShare.EditValue = Nothing
            INDLciCostCenterShare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDDteInvoiceDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteInvoiceDate.EditValueChanged
        SetExpiredDate(CInt(INDSeTerm.EditValue))
        If INDSeNumerShares.EditValue > 1 Then
            SetExpiredDateShares()
        End If
    End Sub

    Private Sub INDSleAccountRadicate_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountRadicate.EditValueChanged
        If INDSleAccountRadicate.EditValue IsNot Nothing Then
            If _indigoSession.IndigoCompanyType = 1 Then ' entidades privadas
                INDSleAccountObjectionRemedied.Enabled = True
            Else
                INDSleAccountDebtorOrder.Enabled = True
            End If
        End If
    End Sub

    Private Sub INDSleAccountObjectionRemedied_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountObjectionRemedied.EditValueChanged
        If INDSleAccountObjectionRemedied.EditValue IsNot Nothing Then
            INDSleAccountConciliation.Enabled = True
        End If
    End Sub

    Private Sub INDSleAccountConciliation_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountConciliation.EditValueChanged
        If INDSleAccountConciliation.EditValue IsNot Nothing Then
            INDSleAccountLegalCollection.Enabled = True
        End If
    End Sub

    Private Sub INDSleAccountLegalCollection_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountLegalCollection.EditValueChanged
        If INDSleAccountLegalCollection.EditValue IsNot Nothing Then
            INDPceAccount.Enabled = True
        End If
    End Sub

    Private Sub INDSleAccountDebtorOrder_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountDebtorOrder.EditValueChanged
        If INDSleAccountDebtorOrder.EditValue IsNot Nothing Then
            INDSleAccountCreditorOrder.Enabled = True
        End If
    End Sub

    Private Sub INDSleAccountCreditorOrder_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountCreditorOrder.EditValueChanged
        If INDSleAccountCreditorOrder.EditValue IsNot Nothing Then
            INDPceAccount.Enabled = True
        End If
    End Sub

    Private Sub INDSleAccountWithoutRadicate_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountWithoutRadicate.EditValueChanged
        If INDSleAccountWithoutRadicate.EditValue IsNot Nothing Then
            INDSleAccountRadicate.Enabled = True
        End If
    End Sub

    Private Sub INDGleIsElectronicInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleIsElectronicInvoice.EditValueChanged
        If INDGleIsElectronicInvoice.EditValue = False Then
            INDLciCUFE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtxtCUFE.EditValue = Nothing
            INDtxtCUFE.Properties.NullText = String.Empty
        Else
            INDLciCUFE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
#End Region

#Region "EditValueChanging"
    Private Sub INDSeTerm_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSeTerm.EditValueChanging
        If e.NewValue IsNot String.Empty Then
            SetExpiredDate(CInt(e.NewValue))
        Else
            INDSeTerm.EditValue = 0
        End If
    End Sub

    Private Sub INDSLeAccountTotal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSLeAccountTotal.EditValueChanging
        If e.NewValue IsNot Nothing Then
            If popupEditMode = True Then
                AcconunValueChangeEditMode = True
            End If
            Using model As New MPUC(Me.Tag)
                Dim accountNew = model.GetAccountByIdSimple(e.NewValue, False)
                Dim accountOld As MainAccounts = Nothing
                If e.OldValue IsNot Nothing Then
                    accountOld = model.GetAccountByIdSimple(e.OldValue, False)
                Else
                    accountOld = New MainAccounts With {.HandlesCostCenter = False}
                End If
                If accountNew.HandlesCostCenter = True Then
                    INDLciCostCenterTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If accountOld IsNot Nothing AndAlso accountOld.HandlesCostCenter = False Then
                        INDPceAccount.Properties.PopupSizeable = True
                        INDPccAccount.Size = New Size(416, 242)
                        INDPceAccount.Properties.PopupSizeable = False
                        INDPceAccount.Focus()
                        INDPceAccount.ShowPopup()
                    End If
                Else
                    INDLciCostCenterTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSleCostCenterTotal.EditValue = Nothing
                    INDSleCostCenterTotal.Properties.NullText = String.Empty
                    If accountOld IsNot Nothing AndAlso accountOld.HandlesCostCenter = True Then
                        INDPceAccount.Properties.PopupSizeable = True
                        INDPccAccount.Size = New Size(416, 192)
                        INDPceAccount.Properties.PopupSizeable = False
                        INDPceAccount.Focus()
                        INDPceAccount.ShowPopup()
                    End If
                End If
            End Using
        Else
            INDSleCostCenterTotal.EditValue = Nothing
            INDSleCostCenterTotal.Properties.NullText = String.Empty
            INDLciCostCenterTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDPceAccount.Properties.PopupSizeable = True
            INDPccAccount.Size = New Size(416, 192)
            INDPceAccount.Properties.PopupSizeable = False
            INDPceAccount.Focus()
            INDPceAccount.ShowPopup()
        End If
        AcconunValueChangeEditMode = False
    End Sub

    Private Sub INDTxtValueBalance_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDTxtValueBalance.EditValueChanging
        If INDSeNumerShares.EditValue > 1 Then
            If e.NewValue > INDTxtValueBill.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor del saldo no puede ser mayor al valor de de la factura"
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    Private Sub INDTxtValueBill_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDTxtValueBill.EditValueChanging
        If INDSeNumerShares.EditValue > 1 Then
            If e.NewValue < INDTxtValueBalance.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor de la factura no puede ser menor al valor del saldo"
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    Private Sub INDSleAccountWithoutRadicate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleAccountWithoutRadicate.EditValueChanging, INDSleAccountRadicate.EditValueChanging, INDSleAccountObjectionRemedied.EditValueChanging, INDSleAccountConciliation.EditValueChanging, INDSleAccountLegalCollection.EditValueChanging, INDSleAccountDebtorOrder.EditValueChanging, INDSleAccountCreditorOrder.EditValueChanging
        If e.NewValue Is Nothing Then
            Exit Sub
        End If
        Dim control = DirectCast(sender, CtrPUC)
        If control.Name <> INDSleAccountWithoutRadicate.Name AndAlso e.NewValue = INDSleAccountWithoutRadicate.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable ya esta en uso para Radicada"
            e.Cancel = True
            control.Focus()
            Exit Sub
        End If
        If control.Name <> INDSleAccountRadicate.Name AndAlso e.NewValue = INDSleAccountRadicate.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable ya esta en uso para Sin Radicar"
            e.Cancel = True
            control.Focus()
            Exit Sub
        End If
        If control.Name <> INDSleAccountObjectionRemedied.Name AndAlso e.NewValue = INDSleAccountObjectionRemedied.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable ya esta en uso para Glosa Subsanable"
            e.Cancel = True
            control.Focus()
            Exit Sub
        End If
        If control.Name <> INDSleAccountConciliation.Name AndAlso e.NewValue = INDSleAccountConciliation.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable ya esta en uso para Conciliación"
            e.Cancel = True
            control.Focus()
            Exit Sub
        End If
        If control.Name <> INDSleAccountLegalCollection.Name AndAlso e.NewValue = INDSleAccountLegalCollection.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable ya esta en uso para Cobro Juridico o de Dificil Recaudo"
            e.Cancel = True
            control.Focus()
            Exit Sub
        End If
        If control.Name <> INDSleAccountDebtorOrder.Name AndAlso e.NewValue = INDSleAccountDebtorOrder.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable ya esta en uso para Orden de Glosa"
            e.Cancel = True
            control.Focus()
            Exit Sub
        End If
        If control.Name <> INDSleAccountCreditorOrder.Name AndAlso e.NewValue = INDSleAccountCreditorOrder.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable ya esta en uso para Acreedores Glosas"
            e.Cancel = True
            control.Focus()
            Exit Sub
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleAccountShare_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountShare.QueryPopUp
        If MainAccountShareXPO Is Nothing Then
            InitializeMainAccountShareXPO()
        End If
    End Sub

    Private Sub INDSleCostCenterShare_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCostCenterShare.QueryPopUp
        If CostCenterShareXPO Is Nothing Then
            InitializeCostCenterShareXPO()
        End If
    End Sub

    Private Sub INDSLeAccountTotal_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSLeAccountTotal.QueryPopUp
        If INDGvAccount.RowCount = 0 Then
            GridView1.ShowLoadingPanel()
            SetDatasourceAccounts()
            GridView1.HideLoadingPanel()
        End If
    End Sub

    Private Sub SetDatasourceAccounts()
        listMainAccountsDataSource = New List(Of MainAccounts)
        Using model As New MPUC(Me.Tag)
            Dim account = model.GetAccountByIdSimple(INDSleAccountWithoutRadicate.EditValue, False)
            listMainAccountsDataSource.Add(account)
            account = model.GetAccountByIdSimple(INDSleAccountRadicate.EditValue, False)
            listMainAccountsDataSource.Add(account)
            If _indigoSession.IndigoCompanyType = 1 Then
                account = model.GetAccountByIdSimple(INDSleAccountObjectionRemedied.EditValue, False)
                listMainAccountsDataSource.Add(account)
                account = model.GetAccountByIdSimple(INDSleAccountConciliation.EditValue, False)
                listMainAccountsDataSource.Add(account)
                account = model.GetAccountByIdSimple(INDSleAccountLegalCollection.EditValue, False)
                listMainAccountsDataSource.Add(account)
            Else
                account = model.GetAccountByIdSimple(INDSleAccountDebtorOrder.EditValue, False)
                listMainAccountsDataSource.Add(account)
                account = model.GetAccountByIdSimple(INDSleAccountCreditorOrder.EditValue, False)
                listMainAccountsDataSource.Add(account)
            End If
        End Using
        INDSLeAccountTotal.Properties.DataSource = listMainAccountsDataSource
    End Sub


    Private Sub INDSleCostCenterTotal_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCostCenterTotal.QueryPopUp
        If CostCenterTotalXPO Is Nothing Then
            InitializeCostCenterTotalXPO()
        End If
    End Sub

    Private Sub INDSleCustomer_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCustomer.QueryPopUp
        If CustomerXPO Is Nothing Then
            InitializeCustomerXPO()
        End If
    End Sub

    Private Sub INDSleAccountWithoutRadicate_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountWithoutRadicate.QueryPopUp
        If AccountWithoutRadicateXPO Is Nothing Then
            Using model As New MPortfolioInitialBalance(Me.Tag)
                AccountWithoutRadicateXPO = model.ListAccountsCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleAccountRadicate_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountRadicate.QueryPopUp
        If AccountRadicateXPO Is Nothing Then
            Using model As New MPortfolioInitialBalance(Me.Tag)
                AccountRadicateXPO = model.ListAccountsCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleAccountObjectionRemedied_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountObjectionRemedied.QueryPopUp
        If AccountObjectionRemediedXPO Is Nothing Then
            Using model As New MPortfolioInitialBalance(Me.Tag)
                AccountObjectionRemediedXPO = model.ListAccountsCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleAccountConciliation_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountConciliation.QueryPopUp
        If AccountConciliationXPO Is Nothing Then
            Using model As New MPortfolioInitialBalance(Me.Tag)
                AccountConciliationXPO = model.ListAccountsCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleAccountLegalCollection_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountLegalCollection.QueryPopUp
        If AccountLegalCollectionXPO Is Nothing Then
            Using model As New MPortfolioInitialBalance(Me.Tag)
                AccountLegalCollectionXPO = model.ListAccountsCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleAccountDebtorOrder_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountDebtorOrder.QueryPopUp
        If AccountDebtorOrderXPO Is Nothing Then
            Using model As New MPortfolioInitialBalance(Me.Tag)
                AccountDebtorOrderXPO = model.ListAccountsCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleAccountCreditorOrder_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountCreditorOrder.QueryPopUp
        If AccountCreditorOrderXPO Is Nothing Then
            Using model As New MPortfolioInitialBalance(Me.Tag)
                AccountCreditorOrderXPO = model.ListAccountsCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleGlosasCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleGlosasCostCenter.QueryPopUp
        If GlosasCostCenterXPO Is Nothing Then
            InitializeGlosasCostCenterXPO()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDSleAccountShare_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleAccountShare.ButtonClick, INDSLeAccountTotal.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPopupPUC
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                If INDLcgTotalBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    InitializeMainAccountTotalXPO()
                    Dim value = INDSLeAccountTotal.EditValue
                    INDSLeAccountTotal.EditValue = Nothing
                    INDSLeAccountTotal.EditValue = value
                    INDSLeAccountTotal.Focus()
                    INDPceAccount.ShowPopup()
                Else
                    InitializeMainAccountShareXPO()
                    Dim value = INDSleAccountShare.EditValue
                    INDSleAccountShare.EditValue = Nothing
                    INDSleAccountShare.EditValue = value
                    INDSleAccountShare.Focus()
                End If
            End Using
        End If
    End Sub

    Private Sub INDSleCostCenterShare_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCostCenterShare.ButtonClick, INDSleCostCenterTotal.ButtonClick, INDSleGlosasCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostCenter
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                If INDLcgTotalBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    Dim control = DirectCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
                    If control.Name = "INDSleGlosasCostCenter" Then
                        InitializeGlosasCostCenterXPO()
                        INDSleGlosasCostCenter.Focus()
                    Else
                        InitializeCostCenterTotalXPO()
                        INDSleCostCenterTotal.Focus()
                        INDPceAccount.ShowPopup()
                    End If

                Else
                    InitializeCostCenterShareXPO()
                    INDSleCostCenterShare.Focus()
                End If
            End Using
        End If
    End Sub

    Private Sub INDSleAccountWithoutRadicate_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleAccountWithoutRadicate.ButtonClick, INDSleAccountRadicate.ButtonClick, INDSleAccountObjectionRemedied.ButtonClick, INDSleAccountConciliation.ButtonClick, INDSleAccountLegalCollection.ButtonClick, INDSleAccountDebtorOrder.ButtonClick, INDSleAccountCreditorOrder.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmPopupPUC
                OpenFormDialog(form)
                Dim search = DirectCast(sender, CtrPUC)
                Using model As New MPortfolioInitialBalance(Me.Tag)
                    AccountWithoutRadicateXPO = model.ListAccountsCostCenter()
                    AccountRadicateXPO = model.ListAccountsCostCenter()
                    AccountObjectionRemediedXPO = model.ListAccountsCostCenter()
                    AccountConciliationXPO = model.ListAccountsCostCenter()
                    AccountLegalCollectionXPO = model.ListAccountsCostCenter()
                    AccountDebtorOrderXPO = model.ListAccountsCostCenter()
                    AccountCreditorOrderXPO = model.ListAccountsCostCenter()
                End Using
                Select Case search.Name
                    Case "INDSleAccountWithoutRadicate"
                        INDSleAccountWithoutRadicate.Focus()
                    Case "INDSleAccountRadicate"
                        INDSleAccountRadicate.Focus()
                    Case "INDSleAccountObjectionRemedied"
                        INDSleAccountObjectionRemedied.Focus()
                    Case "INDSleAccountConciliation"
                        INDSleAccountConciliation.Focus()
                    Case "INDSleAccountLegalCollection"
                        INDSleAccountLegalCollection.Focus()
                    Case "INDSleAccountDebtorOrder"
                        INDSleAccountDebtorOrder.Focus()
                    Case "INDSleAccountCreditorOrder"
                        INDSleAccountCreditorOrder.Focus()
                End Select

            End Using
        End If
    End Sub



    Private Sub INDSleCustomer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCustomer.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCustomers
                OpenFormDialog(formulario)
                CustomerXPO = Nothing
                INDSleCustomer.Focus()
            End Using
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAddAccount_Click(sender As Object, e As EventArgs) Handles INDBtnAddAccount.Click
        Dim errors = ValidatePopupAccount()
        If errors.Length = 0 Then
            If portfolioInitialBalanceAccountReceivableAccounting Is Nothing Then
                portfolioInitialBalanceAccountReceivableAccounting = New PortfolioInitialBalanceAccountReceivableAccounting
            End If
            With portfolioInitialBalanceAccountReceivableAccounting
                .MainAccountId = INDSLeAccountTotal.EditValue
                If INDSLeAccountTotal.Text IsNot String.Empty Then
                    .CodeNameMainAccount = INDSLeAccountTotal.Text
                Else
                    .CodeNameMainAccount = INDSLeAccountTotal.Properties.NullText
                End If
                .CostCenterId = INDSleCostCenterTotal.EditValue
                If INDSleCostCenterTotal.Text IsNot String.Empty Then
                    .CodeNameCostCenter = INDSleCostCenterTotal.Text
                Else
                    .CodeNameCostCenter = INDSleCostCenterTotal.Properties.NullText
                End If
                If CustomerXPO IsNot Nothing Then
                    Dim customerTpm = DirectCast(DirectCast(INDGvSleCustomer.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CommonCustomerXpo)
                    .ThirdPartyId = customerTpm.ThirdPartyId.Id
                End If
                .Value = INDTxtValueTotal.EditValue
            End With
            If Me.listPortfolioInitialBalanceAccountReceivableAccounting Is Nothing Then
                Me.listPortfolioInitialBalanceAccountReceivableAccounting = New List(Of PortfolioInitialBalanceAccountReceivableAccounting)
            End If
            If popupEditMode = False Then
                Me.listPortfolioInitialBalanceAccountReceivableAccounting.Add(Me.portfolioInitialBalanceAccountReceivableAccounting)
            End If

            INDTxtValueBalance.EditValue = listPortfolioInitialBalanceAccountReceivableAccounting.Sum(Function(x) x.Value)

            INDTxtValueBill.Properties.ReadOnly = True


            INDGvAccount.OptionsView.ShowFooter = True
            INDGcAccount.DataSource = Nothing
            INDGcAccount.DataSource = Me.listPortfolioInitialBalanceAccountReceivableAccounting

            CleanControlsPopupAccount()
            SetReadOnlyAccounts = True
            INDSeNumerShares.Enabled = False
            INDSLeAccountTotal.Focus()
        Else
            Mensaje(EeventViewerImages.Advertencia) = errors
        End If
    End Sub

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim errors = ValidateControlsPopup()
        If errors.Length = 0 Then
            If Me.portfolioInitialBalanceAccountReceivable Is Nothing Then
                Me.portfolioInitialBalanceAccountReceivable = New PortfolioInitialBalanceAccountReceivable
            End If
            With Me.portfolioInitialBalanceAccountReceivable
                .AccountReceivableType = 1
                Using modelCustomer As New MCustomers(Me.Tag)
                    Dim customerTpm = modelCustomer.GetCustomerById(INDSleCustomer.EditValue)
                    .ThirdPartyId = customerTpm.ThirdPartyId
                End Using
                .CustomerId = INDSleCustomer.EditValue
                If INDSleCustomer.Text IsNot String.Empty Then
                    .CodeNameCustomer = INDSleCustomer.Text
                Else
                    .CodeNameCustomer = INDSleCustomer.Properties.NullText
                End If
                .InvoiceNumber = INDTxtInvoiceNumber.Text
                .InvoiceCategoryId = INDSleInvoiceCategory.EditValue
                .AccountReceivableDate = INDDteInvoiceDate.EditValue
                .Term = INDSeTerm.EditValue
                .ExpiredDate = INDDteExpiredDate.EditValue
                .AffectBudget = INDGleAffectBudget.EditValue
                .BudgetId = INDSleBudget.EditValue
                .IsElectronicInvoice = INDGleIsElectronicInvoice.EditValue
                .CUFE = INDtxtCUFE.Text
                .Observations = INDMeObservation.Text
                .PortfolioStatus = INDGlePortfolioStatus.EditValue
                .NumberShares = INDSeNumerShares.EditValue
                If INDSeNumerShares.EditValue = 1 Then
                    .CostCenterId = INDSleGlosasCostCenter.EditValue
                    If INDSleGlosasCostCenter.Text IsNot String.Empty Then
                        .CodeNameGlosasCostCenter = INDSleGlosasCostCenter.Text
                    End If
                    'factura total
                    .AccountWithoutRadicateId = INDSleAccountWithoutRadicate.EditValue
                    If INDSleAccountWithoutRadicate.Text IsNot String.Empty Then
                        .CodeNameAccountWithoutRadicate = INDSleAccountWithoutRadicate.Text
                    End If
                    .AccountRadicateId = INDSleAccountRadicate.EditValue
                    If INDSleAccountRadicate.Text IsNot String.Empty Then
                        .CodeNameAccountRadicate = INDSleAccountRadicate.Text
                    End If
                    .AccountObjectionRemediedId = INDSleAccountObjectionRemedied.EditValue
                    If INDSleAccountObjectionRemedied.Text IsNot String.Empty Then
                        .CodeNameAccountObjectionRemedied = INDSleAccountObjectionRemedied.Text
                    End If
                    .AccountConciliationId = INDSleAccountConciliation.EditValue
                    If INDSleAccountConciliation.Text IsNot String.Empty Then
                        .CodeNameAccountConciliation = INDSleAccountConciliation.Text
                    End If
                    .AccountLegalCollectionId = INDSleAccountLegalCollection.EditValue
                    If INDSleAccountLegalCollection.Text IsNot String.Empty Then
                        .CodeNameAccountLegalCollection = INDSleAccountLegalCollection.Text
                    End If
                    .AccountDebtorOrder = INDSleAccountDebtorOrder.EditValue
                    If INDSleAccountDebtorOrder.Text IsNot String.Empty Then
                        .CodeNameAccountDebtorOrder = INDSleAccountDebtorOrder.Text
                    End If
                    .AccountCreditorOrder = INDSleAccountCreditorOrder.EditValue
                    If INDSleAccountCreditorOrder.Text IsNot String.Empty Then
                        .CodeNameAccountCreditorOrder = INDSleAccountCreditorOrder.Text
                    End If


                    .Value = INDTxtValueBill.EditValue
                    .Balance = INDTxtValueBalance.EditValue


                    While .PortfolioInitialBalanceAccountReceivableAccounting.Count > 0
                        If .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).Id > 0 Then
                            .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).MarkAsDeleted()
                        Else
                            .PortfolioInitialBalanceAccountReceivableAccounting.RemoveAt(0)
                        End If
                    End While
                    If portfolioInitialBalanceAccountReceivable.PortfolioStatus = 1 Then
                        Dim AccountWithoutRadicate = portfolioInitialBalanceAccountReceivable.AccountWithoutRadicateId
                        If portfolioInitialBalanceAccountReceivable.Value <> listPortfolioInitialBalanceAccountReceivableAccounting.Where(Function(x) x.MainAccountId = AccountWithoutRadicate).Sum(Function(x) x.Value) Then
                            Mensaje(EeventViewerImages.Advertencia) = "La suma de los movimientos en la información contable no concuerda con el estado sin radicar de la factura"
                            Exit Sub
                        End If
                    End If
                    For Each item In Me.listPortfolioInitialBalanceAccountReceivableAccounting
                        .PortfolioInitialBalanceAccountReceivableAccounting.Add(item)
                    Next
                    While .PortfolioInitialBalanceAccountReceivableShare.Count > 0
                        If .PortfolioInitialBalanceAccountReceivableShare.ElementAt(0).Id > 0 Then
                            .PortfolioInitialBalanceAccountReceivableShare.ElementAt(0).MarkAsDeleted()
                        Else
                            .PortfolioInitialBalanceAccountReceivableShare.RemoveAt(0)
                        End If
                    End While
                    Dim share = New PortfolioInitialBalanceAccountReceivableShare
                    share.Number = 1
                    share.ExpiredDate = .ExpiredDate
                    share.Value = .Value
                    .PortfolioInitialBalanceAccountReceivableShare.Add(share)
                Else
                    'factura cuota
                    .Value = INDTxtValueBill.EditValue
                    .Balance = INDTxtValueBalance.EditValue
                    While .PortfolioInitialBalanceAccountReceivableAccounting.Count > 0
                        If .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).Id > 0 Then
                            .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).MarkAsDeleted()
                        Else
                            .PortfolioInitialBalanceAccountReceivableAccounting.RemoveAt(0)
                        End If
                    End While
                    Dim account = New PortfolioInitialBalanceAccountReceivableAccounting
                    account.ThirdPartyId = .ThirdPartyId
                    account.MainAccountId = INDSleAccountShare.EditValue
                    If INDSleAccountShare.Text IsNot String.Empty Then
                        account.CodeNameMainAccount = INDSleAccountShare.Text
                    Else
                        account.CodeNameMainAccount = INDSleAccountShare.Properties.NullText
                    End If
                    account.CostCenterId = INDSleCostCenterShare.EditValue
                    If INDSleCostCenterShare.Text IsNot Nothing Then
                        account.CodeNameCostCenter = INDSleCostCenterShare.Text
                    Else
                        account.CodeNameCostCenter = INDSleCostCenterShare.Properties.NullText
                    End If
                    account.Value = INDTxtValueBalance.EditValue
                    .PortfolioInitialBalanceAccountReceivableAccounting.Add(account)
                    Dim ListSharesAdded = (From d In listPortfolioInitialBalanceAccountReceivableShare Select d.Number).ToList()
                    Dim listSharedDelete = (From d In .PortfolioInitialBalanceAccountReceivableShare Where Not ListSharesAdded.Contains(d.Number) Select d).ToList()
                    For Each share In listPortfolioInitialBalanceAccountReceivableShare
                        
                        Dim shareAdd = .PortfolioInitialBalanceAccountReceivableShare.Where(Function(x) x.Number = share.Number).FirstOrDefault()
                        If shareAdd IsNot Nothing Then
                            shareAdd.Value = share.Value
                            shareAdd.ExpiredDate = share.ExpiredDate
                        Else
                            .PortfolioInitialBalanceAccountReceivableShare.Add(share)
                        End If

                    Next

                    While listSharedDelete.Count > 0
                        If listSharedDelete.ElementAt(0).Id > 0 Then
                            listSharedDelete.ElementAt(0).MarkAsDeleted()
                        End If
                        listSharedDelete.RemoveAt(0)
                    End While
                End If
            End With
            Dim args As New AddBillEventArgs With {.PortfolioInitialBalanceAccountReceivable = Me.portfolioInitialBalanceAccountReceivable}
            RaiseEvent AddBill(Nothing, args)
            Deshacer()
        Else
            Mensaje(EeventViewerImages.Advertencia) = errors
            INDTxtInvoiceNumber.Focus()
        End If
    End Sub
#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Me.portfolioInitialBalanceAccountReceivableAccounting = DirectCast(INDGvAccount.GetFocusedRow, PortfolioInitialBalanceAccountReceivableAccounting)
        Select Case button.Tag.ToString()
            Case "Edit"
                popupEditMode = True
                With Me.portfolioInitialBalanceAccountReceivableAccounting
                    INDSLeAccountTotal.EditValue = .MainAccountId
                    INDSLeAccountTotal.Properties.NullText = .CodeNameMainAccount
                    INDSleCostCenterTotal.EditValue = .CostCenterId
                    INDSleCostCenterTotal.Properties.NullText = .CodeNameCostCenter
                    INDTxtValueTotal.EditValue = .Value
                End With
                If INDSleCostCenterTotal.EditValue IsNot Nothing Then
                    INDPceAccount.Properties.PopupSizeable = True
                    INDPccAccount.Size = New Size(416, 242)
                    INDPceAccount.Properties.PopupSizeable = False
                End If
                INDBtnAddAccount.Text = ResourceManager.GetString("Edit")
                INDPceAccount.Focus()
                INDSLeAccountTotal.Focus()
                INDPceAccount.ShowPopup()
            Case "Remove"
                If Me.portfolioInitialBalanceAccountReceivableAccounting.Id > 0 Then
                    If Me.listPortfolioInitialBalanceAccountReceivableAccountingDelete Is Nothing Then
                        Me.listPortfolioInitialBalanceAccountReceivableAccountingDelete = New List(Of PortfolioInitialBalanceAccountReceivableAccounting)
                    End If
                    Me.listPortfolioInitialBalanceAccountReceivableAccountingDelete.Add(Me.portfolioInitialBalanceAccountReceivableAccounting)
                End If
                Me.listPortfolioInitialBalanceAccountReceivableAccounting.Remove(Me.portfolioInitialBalanceAccountReceivableAccounting)
                INDGcAccount.DataSource = Nothing
                INDGcAccount.DataSource = Me.listPortfolioInitialBalanceAccountReceivableAccounting
                If Me.listPortfolioInitialBalanceAccountReceivableAccounting.Count > 0 Then
                    INDGvAccount.OptionsFind.AlwaysVisible = True
                    IndigoGridControl1.SetExportButton(INDGcAccount, True)
                    INDTxtValueBalance.EditValue = listPortfolioInitialBalanceAccountReceivableAccounting.Sum(Function(x) x.Value)
                Else
                    SetReadOnlyAccounts = False
                    INDGvAccount.OptionsFind.AlwaysVisible = False
                    IndigoGridControl1.SetExportButton(INDGcAccount, False)
                    INDGvAccount.OptionsView.ShowFooter = False
                    IndigoGridControl1.RefreshGrid(INDGcAccount)
                    INDSeNumerShares.Enabled = True
                    INDTxtValueBill.Properties.ReadOnly = False
                    INDTxtValueBalance.EditValue = 0
                End If
                portfolioInitialBalanceAccountReceivableAccounting = Nothing
        End Select
    End Sub
#End Region

#Region "Popup"
    Private Sub INDPceAccount_Popup(sender As Object, e As EventArgs) Handles INDPceAccount.Popup
        INDGvAccount.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDGcAccount, False)
        INDSLeAccountTotal.Focus()
    End Sub
#End Region

#Region "CloseUp"
    Private Sub INDPceAccount_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceAccount.CloseUp
        If Me.listPortfolioInitialBalanceAccountReceivableAccounting IsNot Nothing AndAlso Me.listPortfolioInitialBalanceAccountReceivableAccounting.Count > 0 Then
            INDGvAccount.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDGcAccount, True)
        Else
            INDGvAccount.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDGcAccount, False)
        End If
        If popupEditMode = True And AcconunValueChangeEditMode = False Then
            CleanControlsPopupAccount()
        End If
    End Sub
#End Region
#End Region

#Region "BARBUTTONS"
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        RaiseEvent SetNothingPortfolioInitialBalanceAccountReceivable(Nothing, EventArgs.Empty)
    End Sub
#End Region



    Private Sub INDRptTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptTxtValue.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If
        Dim shareTmp = DirectCast(INDGvShares.GetFocusedRow, PortfolioInitialBalanceAccountReceivableShare)
        Dim balanceTmp = listPortfolioInitialBalanceAccountReceivableShare.Where(Function(c) c.Value > 0 And c.Number <> shareTmp.Number).Sum(Function(x) x.Value) + e.NewValue
        If balanceTmp > INDTxtValueBill.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El saldo superaria el valor de la factura"
            e.Cancel = True
            Exit Sub
        End If
        INDTxtValueBalance.EditValue = balanceTmp
    End Sub

    Private Sub INDGvShares_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvShares.ShowingEditor
        Dim shareTmp = DirectCast(INDGvShares.GetFocusedRow, PortfolioInitialBalanceAccountReceivableShare)
        Dim prevShare = listPortfolioInitialBalanceAccountReceivableShare.Find(Function(x) x.Number = shareTmp.Number - 1)
        Dim nextShare = listPortfolioInitialBalanceAccountReceivableShare.Find(Function(x) x.Number = shareTmp.Number + 1)
        If shareTmp Is Nothing Then
            Exit Sub
        End If
        If prevShare Is Nothing Then
            INDRptDteExpiredDate.MinValue = INDDteExpiredDate.EditValue

        Else
            INDRptDteExpiredDate.MinValue = prevShare.ExpiredDate
        End If
        If nextShare IsNot Nothing Then
            INDRptDteExpiredDate.MaxValue = nextShare.ExpiredDate
        Else
            INDRptDteExpiredDate.MaxValue = Nothing
        End If
    End Sub

    Private Sub INDSeTerm_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeTerm.EditValueChanged
        If INDSeTerm.EditValue IsNot Nothing AndAlso INDSeNumerShares.EditValue > 1 Then
            SetExpiredDateShares()
        End If
    End Sub

    Private Sub SetExpiredDateShares()
        Dim expiredDate = CDate(INDDteInvoiceDate.EditValue)
        For i As Integer = 0 To listPortfolioInitialBalanceAccountReceivableShare.Count - 1 Step 1
            expiredDate = expiredDate.AddDays(INDSeTerm.EditValue)
            Dim share = listPortfolioInitialBalanceAccountReceivableShare.ElementAt(i)
            share.ExpiredDate = expiredDate
        Next
        INDGcShares.DataSource = listPortfolioInitialBalanceAccountReceivableShare
        INDGcShares.RefreshDataSource()
    End Sub

    Private Sub INDRptDteExpiredDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptDteExpiredDate.EditValueChanging

    End Sub

    Private Sub INDGleAffectBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAffectBudget.EditValueChanged
        If INDGleAffectBudget.EditValue = False Then
            INDLciBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleBudget.EditValue = Nothing
        Else
            INDLciBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    Private Sub INDSleInvoiceCategory_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleInvoiceCategory.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1678", Nothing, True)
        End If
    End Sub
End Class
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base.Extension
Imports Presentation.Base.BaseClass
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Portfolio.MVP
Imports System.Text
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Contract.MVP
Imports Presentation.Billing.MVP
Imports Presentation.Controls
Imports DevExpress.XtraLayout
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Linq
Imports Domain.Billing.POCO
Imports Presentation.Accounting.MVP
Imports Presentation.Billing.Entities

Public Class FrmAdvanceCrossing

#Region "Properties & Variables"

#Region "Flags"
    ''' <summary>
    ''' bandera del anticipo
    ''' </summary>
    Private _flagAdvance As Boolean

    ''' <summary>
    ''' Si tiene permiso (80) para modificar datos del egreso
    ''' </summary>
    Private _egressChange As Boolean

    ''' <summary>
    ''' propiedad privada de parametros de inventario que define si liquida a nivel general cuenta madre
    ''' </summary>
    Private _settingLiquidateMasterAccount As Boolean

    ''' <summary>
    ''' propiedad privada de parametros de la compañia que define si se asocia actividad economica en transacciones
    ''' </summary>
    Private _TransactionEconomicActivity As Boolean = False

    ''' <summary>
    ''' propiedad privada de parametros de facturacion
    ''' </summary>
    Private _SettingBilling As SettingsBilling

    ''' <summary>
    ''' bandera que permite identifica cuando se deben refrescar los valores y cuando no
    ''' </summary>
    Private _flagRefreshValues As Boolean = True

    ''' <summary>
    ''' bandera que identifica cuando se esta ejecutando el .load inicial
    ''' </summary>
    Private _flagInitLoad As Boolean = False

    ''' <summary>
    ''' bandera si el folio se esta cargando
    ''' </summary>
    Private _isLoadFolio As Boolean

    ''' <summary>
    ''' bandera pra identificar si el registro fue cambiado
    ''' </summary>
    Private _isChangeRecord As Boolean

#End Region
    ''' <summary>
    ''' Xpo del ingreso
    ''' </summary>
    Private _admissionXpo As AdmissionXpo

    ''' <summary>
    ''' cursor indigo
    ''' </summary>
    Private _indigoCursor As System.Windows.Forms.Cursor

    ''' <summary>
    ''' objeto ingreso
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionObject As Object

    ''' <summary>
    ''' Obtiene el Id de la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Id de la unidad operativa seleccionad</returns>
    Public Property IdOperatingUnitSelected As Integer

    ''' <summary>
    ''' Obtiene el Id de la autorización seleccionada
    ''' </summary>
    ''' <returns>El Id de la autorización seleccionada</returns>
    Public Property IdBillingAuthorizationSelected As Integer

    ''' <summary>
    ''' The _total patient
    ''' </summary>
    Public _totalPatient As Decimal

    ''' <summary>
    ''' Valor del saldo utilizado para cruce por anticipo
    ''' </summary>
    ''' <value>
    ''' The available balance.
    ''' </value>
    Private Property TotalBalancePortfolioAdvance As Dictionary(Of String, Decimal)

    ''' <summary>
    ''' The _total entity
    ''' </summary>
    Public _totalEntity As Decimal

    ''' <summary>
    ''' Gets or sets the third party identifier parent.
    ''' </summary>
    ''' <value>
    ''' The third party identifier parent.
    ''' </value>
    Property ThirdPartyPatientId As Integer?

    ''' <summary>
    ''' The _crossing total value
    ''' </summary>
    Private _balanceTotalThirdPartyValue As Decimal

    ''' <summary>
    ''' The _crossing total value
    ''' </summary>
    Private _balanceTotalPatientValue As Decimal

    ''' <summary>
    ''' Gets or sets the admission number.
    ''' </summary>
    ''' <value>
    ''' The admission number.
    ''' </value>
    Property AdmissionNumber As String

    ''' <summary>
    ''' Identificación del paciente para trazabilidad en observaciones del anticipo
    ''' </summary>
    Property PatientIdentification As String

    ''' <summary>
    ''' Nombre del paciente para trazabilidad en observaciones del anticipo
    ''' </summary>
    Property PatientName As String

    ''' <summary>
    ''' tipo de ingreso
    ''' </summary>
    Private _admissionType As Integer

    ''' <summary>
    ''' propiedad de devolucion de iva
    ''' </summary>
    Private _taxDevolutionValue As Decimal

    ''' <summary>
    ''' Moneda del anticipo
    ''' </summary>
    Private Property CurrencyAdvanceId As Integer?

    ''' <summary>
    ''' TRM usado para conversion de los anticipos y su saldo
    ''' </summary>
    ''' <returns></returns>
    Private Property _TRMValue As Decimal? = 1

    ''' <summary>
    ''' abreviacion de la moneda segun ISO4217
    ''' </summary>
    ''' <returns></returns>
    Private Property _currencyAbbreviation As String

    ''' <summary>
    ''' cantidad de folios
    ''' </summary>
    Private _folioQuantity As Integer


    ''' <summary>
    ''' valor anterior del cruce por si la validacion sale falsa
    ''' </summary>
    Dim _previusValue As Decimal

    ''' <summary>
    ''' Occurs when [run liquidate folio].
    ''' </summary>
    Public Event RunLiquidateFolio(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Gets or sets the LST folios identifier to liquidate.
    ''' </summary>
    ''' <value>
    ''' The LST folios identifier to liquidate.
    ''' </value>
    Property LstFoliosToLiquidate As List(Of ICtrFolio)

    ''' <summary>
    ''' Gets or sets the care group parent.
    ''' </summary>
    ''' <value>
    ''' The care group parent.
    ''' </value>
    Public Property CareGroupParent As CareGroup

    ''' <summary>
    ''' objeto actual de los datos para liquidar el folio
    ''' </summary>
    Private _objectNavigationCurrent As ObjectFolioCrossing


    ''' <summary>
    ''' valor del TMR que trae la entidad de invoicePartial
    ''' </summary>
    Private Property _tRMInvoicePartial As Decimal

    ''' <summary>
    ''' Redondeo
    ''' </summary>
    Private _decimals As Integer = 2

    ''' <summary>
    ''' Lista de la tasa de cambio
    ''' </summary>
    Private _listTRM As List(Of TRM)

    ''' <summary>
    ''' Lista de opciones de fecha de egreso disponibles de los ingresos unificados
    ''' </summary>
    Private _availableEgressDates As List(Of EgressDateOption)

    ''' <summary>
    ''' identifica si proviene de un folio madre , 2 entidad, 4 paciente
    ''' </summary>
    Private _isMasterAccount As Byte = 0

    ''' <summary>
    ''' Indica si el anticipo seleccionado pertenece al tercero beneficiado = Paciente
    ''' </summary>
    Private IsPortfolioAdvanceByThirdPartyBeneficiary As Boolean = False

    ''' <summary>
    ''' Obtiene el id del anticipo
    ''' </summary>
    Private Property PortfolioAdvanceId As Integer
        Get
            Return INDsleAdvance.EditValue
        End Get
        Set(value As Integer)
            INDsleAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the list portfolio advance.
    ''' </summary>
    Property ListPortfolioAdvance As List(Of PortfolioAdvance)
        Get
            Return CType(INDgcPortfolioAdvance.DataSource, List(Of PortfolioAdvance))
        End Get
        Set(value As List(Of PortfolioAdvance))
            INDgcPortfolioAdvance.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the total patient reference.
    ''' </summary>
    ''' <value>
    ''' The total patient reference.
    ''' </value>
    Private ReadOnly Property TotalPatientReference As Decimal
        Get
            Return If(CareGroupParent.CareGroupType = eCareGroupType.Particulares, TotalEntity, TotalPatient)
        End Get
    End Property

    ''' <summary>
    ''' Establece el total a paciente
    ''' </summary>
    ''' <value>
    ''' The total patient.
    ''' </value>
    Public Property TotalPatient As Decimal
        Get
            Return _totalPatient
        End Get
        Set(value As Decimal)
            _totalPatient = value
            INDlblTotalPatient.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.CurrencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the total entity.
    ''' </summary>
    ''' <value>
    ''' The total entity.
    ''' </value>
    Public Property TotalEntity As Decimal
        Get
            Return _totalEntity
        End Get
        Set(value As Decimal)
            _totalEntity = value
            INDlblTotalEntity.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.CurrencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Establece el descuento paciente
    ''' </summary>
    Public Property PatientDiscount As Decimal
        Get
            Return If(String.IsNullOrEmpty(INDlblPatientDiscount.EditValue), 0, INDlblPatientDiscount.EditValue)
        End Get
        Set(value As Decimal)
            INDlblPatientDiscount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor del anticipo
    ''' </summary>
    Public Property ValuePortfolioAdvance As Decimal
        Get
            Return If(String.IsNullOrEmpty(INDlblValuePortfolioAdvance.EditValue), 0, INDlblValuePortfolioAdvance.EditValue)
        End Get
        Set(value As Decimal)
            INDlblValuePortfolioAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the total value to patient.
    ''' </summary>
    ''' <value>
    ''' The total value to patient.
    ''' </value>
    Public ReadOnly Property TotalValueToPatient As Decimal
        Get
            Return If(CareGroupParent.CareGroupType = eCareGroupType.Particulares OrElse Me._isMasterAccount = 4, TotalEntity, TotalPatient)
        End Get
    End Property

    ''' <summary>
    ''' Gets the total discount to patient.
    ''' </summary>
    ''' <value>
    ''' The total discount to patient.
    ''' </value>
    Public Property TotalDiscountToPatient As Decimal
        Get
            Return If(CareGroupParent.CareGroupType = eCareGroupType.Particulares OrElse Me._isMasterAccount = 4, EntityDiscount, PatientDiscount)
        End Get
        Set(value As Decimal)
            If CareGroupParent.CareGroupType = eCareGroupType.Particulares Then
                EntityDiscount = value
            Else
                PatientDiscount = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Establece el descuento entidad
    ''' </summary>
    Public Property EntityDiscount As Decimal
        Get
            Return INDlblEntityDiscount.EditValue
        End Get
        Set(value As Decimal)
            INDlblEntityDiscount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que Obtiene o establece el saldo del anticipo
    ''' </summary>
    ''' <returns></returns>
    Public Property BalanceAdvance As Decimal
        Get
            Return CType(INDteBalance.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDteBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el valor a cruzar
    ''' </summary>
    ''' <returns></returns>
    Public Property CrossingValue As Decimal
        Get
            Return CType(INDspnCossingValue.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDspnCossingValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece la condicion de venta seleccionada
    ''' </summary>
    ''' <returns></returns>
    Public Property ConditionSalesId As Integer?
        Get
            Return INDSleConditionSale.EditValue
        End Get
        Set(value As Integer?)
            INDSleConditionSale.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivityId As Integer?
        Get
            Return INDSleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDSleEconomicActivity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the crossing value.
    ''' </summary>
    Property BalanceTotalThirdPartyValue As Decimal
        Get
            Return _balanceTotalThirdPartyValue
        End Get
        Set(value As Decimal)
            _balanceTotalThirdPartyValue = value
            INDlblThirdPartyBalance.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.CurrencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the crossing value.
    ''' </summary>
    Property BalanceTotalPatientValue As Decimal
        Get
            Return _balanceTotalPatientValue
        End Get
        Set(value As Decimal)
            _balanceTotalPatientValue = value
            INDlblPatientBalance.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.CurrencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece el tipo de ingreso
    ''' </summary>
    Public WriteOnly Property AdmissionType As Integer
        Set(value As Integer)
            _admissionType = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asigna el permiso para cambiar datos de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Property EgressChange As Boolean
        Get
            Return _egressChange
        End Get
        Set(value As Boolean)
            _egressChange = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asgina la cantidad de folios
    ''' </summary>
    ''' <returns></returns>
    Public Property FolioQuantity As Integer
        Get
            Return _folioQuantity
        End Get
        Set(value As Integer)
            _folioQuantity = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene y asigna el id de la moneda ademas de asignar su abreviacion
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyTypeId(Optional currencyAbbreviation As String = Nothing) As Integer?
        Get
            Return INDsleCurrencyType.EditValue
        End Get
        Set(value As Integer?)

            If String.IsNullOrEmpty(currencyAbbreviation) Then
                currencyAbbreviation = indigo?.CurrencyISO4217
                INDsleCurrencyType.EditValue = indigo?.OfficialCurrencyId
            Else
                INDsleCurrencyType.EditValue = value
            End If
            SetCurrencyFormatUI(currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene y asigna el valor de la devolcuion del iva
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxDevolutionValue As Decimal
        Get
            Return _taxDevolutionValue
        End Get
        Set(value As Decimal)
            INDlblTaxDevolution.EditValue = value
            _taxDevolutionValue = value
            INDLciTaxDevolution.HideControl(value = 0)
        End Set
    End Property


    ''' <summary>
    ''' propiedad que asigna y guarda el Id de la moneda
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return _currencyAbbreviation
        End Get
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asigna la tasa con la que se conviertieron los datos del folio
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TRMInvoicePartial As Decimal
        Get
            Return _tRMInvoicePartial
        End Get
    End Property

    Public WriteOnly Property SettingLiquidateMasterAccount As Boolean
        Set(value As Boolean)
            _settingLiquidateMasterAccount = value
        End Set
    End Property

    Public WriteOnly Property SettingBilling As SettingsBilling
        Set(value As SettingsBilling)
            _SettingBilling = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece los posibles iva devuelto esta en la moneda escogida 
    ''' </summary>
    ''' <returns></returns>
    Private Property _taxDevolution As List(Of TaxDevolution)
    Public Property ListTaxDevolution As List(Of TaxDevolution)
        Get
            Return _taxDevolution
        End Get
        Set(value As List(Of TaxDevolution))
            _taxDevolution = value
        End Set
    End Property

    Private Property _folioOriginalValues As Task(Of InvoicePartialMasterAccount)
    Public ReadOnly Property FolioOriginalValues As Task(Of InvoicePartialMasterAccount)
        Get
            'se consulta si esta vacia o el folio id es diferente al actual
            If _folioOriginalValues Is Nothing OrElse _folioOriginalValues?.Result?.Id <> _objectNavigationCurrent?.RevenueControlDetailId Then
                Dim result = GetInvoicePartial(_objectNavigationCurrent?.RevenueControlDetailId)
                _folioOriginalValues = result
            End If

            Return _folioOriginalValues
        End Get
    End Property
#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me.AdmissionObject = Nothing
        Me._flagAdvance = Nothing
        Me.IdOperatingUnitSelected = Nothing
        Me.IdBillingAuthorizationSelected = Nothing
        Me._indigoCursor = Nothing
        Me._totalPatient = Nothing
        Me.TotalBalancePortfolioAdvance = Nothing
        Me._totalEntity = Nothing
        Me.ThirdPartyPatientId = Nothing
        Me._balanceTotalPatientValue = Nothing
        Me.AdmissionNumber = Nothing
        Me._admissionType = Nothing
        Me._egressChange = Nothing
        Me._previusValue = Nothing
        Me.LstFoliosToLiquidate = Nothing
        Me.CareGroupParent = Nothing
        Me._objectNavigationCurrent = Nothing
        Me._flagRefreshValues = Nothing
        Me._flagInitLoad = Nothing
        Me._tRMInvoicePartial = Nothing
        Me._folioOriginalValues = Nothing
        Me._taxDevolution = Nothing
        Me.ConditionSalesId = Nothing
        Me.EconomicActivityId = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmAdvanceCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmAdvanceCrossing_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me._indigoCursor = ChangeCursorIndigo()
        TotalBalancePortfolioAdvance = New Dictionary(Of String, Decimal)()

        Using model As New MLiquidation()
            sleDiag1.Properties.DataSource = model.ListAllINDIAGNOS

            Dim listFoliosIds = LstFoliosToLiquidate.Select(Function(o) o.Id).ToList()
            Dim res = Await model.GetListUnifiedAdmissions(listFoliosIds, AdmissionNumber)
            If res.StateResult Then
                ShowMessage(EeventViewerImages.Advertencia) = res.Message
                INDLcilueEgressDate.ShowLayout()
                Dim unifiedAdmissions = res.ObjectEmbbeded
                If unifiedAdmissions IsNot Nothing AndAlso unifiedAdmissions.Count > 0 Then
                    ' Crear lista de opciones de fecha de egreso con información del ingreso
                    _availableEgressDates = unifiedAdmissions _
                        .Where(Function(admission) admission.FECHEGRESO.HasValue) _
                        .Select(Function(admission) New EgressDateOption With {
                            .EgressDate = admission.FECHEGRESO.Value,
                            .AdmissionNumber = If(admission.NUMINGRES.Trim(), "N/A"),
                            .AdmissionStatus = GetAdmissionStatusText(admission.IESTADOIN)
                        }) _
                        .OrderBy(Function(opt) opt.EgressDate) _
                        .ToList()

                    ' Configurar el DataSource del selector de fechas de egreso
                    ConfigureEgressDateSelector()
                End If
            End If
        End Using

        IndigoGridView1.SetListAcction(INDgvAdvance, {eAcciones.Remove}.ToList)

        For Each col As GridColumn In INDgvAdvance.Columns
            If col.Name.Equals("colActions") Then
                col.Width = 150
            End If
        Next

        Me._flagInitLoad = True

        If _SettingBilling.LiquidateFolioInSpecificCurrency Then

            If String.IsNullOrEmpty(_SettingBilling?.Currency?.Abbreviation) Then
                ShowMessage(EeventViewerImages.Advertencia) = "No se está postulando la moneda especifica correctamente"
            End If

            Me.CurrencyTypeId(_SettingBilling?.Currency?.Abbreviation) = _SettingBilling.SpecificCurrencyId
            INDsleCurrencyType.Properties.NullText = _SettingBilling?.Currency?.Abbreviation

        Else
            Me.CurrencyTypeId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
            INDsleCurrencyType.Properties.NullText = indigo.CurrencyISO4217
        End If

        Me._flagInitLoad = False

        Using model As New MCompanySettings(Me.Tag)
            Dim setting = Await model.GetCompanySettings
            If setting IsNot Nothing Then
                _TransactionEconomicActivity = setting.TransactionEconomicActivity
            End If
        End Using

        If ModeLiquidation = eModeLiquidation.MULTIPLE_PATIENT Then
            LciAdmissionData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            PrepareFormMultiple()
        Else
            LciAdmissionData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            PrepareForm()
        End If

        If _SettingBilling.requiresConditionsSale Then
            INDLciConditionSale.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Using model As New MLiquidation()
                INDSleConditionSale.Properties.DataSource = Await model.ListConditionSales

                Dim ConditionSales = TryCast(INDSleConditionSale.Properties.DataSource, List(Of ConditionSalesXpo))
                If ConditionSales.Count = 1 Then
                    ConditionSalesId = ConditionSales.FirstOrDefault().Id
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ record navigation change event.
    ''' </summary>
    ''' <param name="Record">The record.</param>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        AssigningValuesToPreviewObject()
        _objectNavigationCurrent = CType(Record, ObjectFolioCrossing)
        Me.Text = String.Format("Cierre de Factura - FOLIO # {0}", _objectNavigationCurrent.Folio.FolioOrder)

        _isChangeRecord = True
        LoadControls(_objectNavigationCurrent)
        _isChangeRecord = False
    End Sub

#End Region

#Region "Closing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmAdvanceCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmAdvanceCrossing_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.BarraBotones.Enabled Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the Closed event of the INDpceAddAdvancePortfolio control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddAdvancePortfolio_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceAddAdvancePortfolio.Closed
        INDsbAcept.Enabled = True
        INDsbAcept.Focus()
    End Sub
#End Region

#Region "Leave"
    ''' <summary>
    ''' Handles the Leave event of the INDlblPatientDiscount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDlblPatientDiscount_Leave(sender As Object, e As EventArgs) Handles INDlblPatientDiscount.Leave
        ValidatePatientDiscount()
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDlblEntityDiscount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDlblEntityDiscount_Leave(sender As Object, e As EventArgs) Handles INDlblEntityDiscount.Leave
        If Not INDlblEntityDiscount.Properties.ReadOnly Then
            ValidatePatientDiscount()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDspnCossingValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDspnCossingValue_Leave(sender As Object, e As EventArgs) Handles INDspnCossingValue.Leave
        If INDspnCossingValue.EditValue IsNot Nothing AndAlso INDteBalance.EditValue IsNot Nothing Then
            If Me.CurrencyTypeId = Me.CurrencyAdvanceId Then
                If BalanceAdvance > TotalValueToPatient Then
                    If CrossingValue > TotalValueToPatient And Not IsPortfolioAdvanceByThirdPartyBeneficiary Then
                        CrossingValue = TotalValueToPatient
                        ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageCrossingValue1", GetType(CtrFolio).Name)
                    End If
                Else
                    If CrossingValue > BalanceAdvance Then
                        CrossingValue = BalanceAdvance
                        ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageCrossingValue2", GetType(CtrFolio).Name)
                    End If
                End If
            Else
                If CrossingValue > INDspnCossingValue.Properties.MaxValue Then
                    ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageCrossingValue2", GetType(CtrFolio).Name)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDrpCrossingValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDrpCrossingValue_Leave(sender As Object, e As EventArgs) Handles INDrpCrossingValue.Leave
        Dim _portfolioAdvance As PortfolioAdvance = CType(INDgvAdvance.GetFocusedRow(), PortfolioAdvance)
        If _portfolioAdvance IsNot Nothing Then
            If Not ValidateCrossingValue(_portfolioAdvance) Then
                _portfolioAdvance.CrossingValue = _previusValue
                INDgcPortfolioAdvance.RefreshDataSource()
            End If
            CrossingTotalRefresh()
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdvance.EditValueChanged
        BalanceAdvance = 0
        Me.INDlciTRM.HideControl()
        If INDsleAdvance.EditValue IsNot Nothing Then
            Dim portfolioAdv As Object
            portfolioAdv = DirectCast(DirectCast(INDgvAdvancePortfolio.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, PortfolioRepository.Portfolio_PortfolioAdvance)
            Me.CurrencyAdvanceId = portfolioAdv?.CurrencyId
            Dim currencyAdvance As String = portfolioAdv?.CurrencyAbbreviation

            SetTaxDevolutionValue(portfolioAdv)
            IsPortfolioAdvanceByThirdPartyBeneficiary = portfolioAdv.ThirdPartyBeneficiaryId?.Id = ThirdPartyPatientId

            If Me.CurrencyTypeId <> Me.CurrencyAdvanceId Then
                INDlciBalanceAdvance.Text = $"Saldo (expresado en {Me.CurrencyAbbreviation})"
                Me.INDlciTRM.HideControl(False)
                Await Me.loadListTRM(Me.CurrencyTypeId, Me.CurrencyAdvanceId)


                If Me._listTRM Is Nothing OrElse Not Me._listTRM?.Any(Function(a) a.CurrencyId = CurrencyTypeId _
                                                               AndAlso a.OfficialCurrencyId = Me.CurrencyAdvanceId _
                                                               AndAlso a.MeasurementDate = GetDateServer().Date) Then
                    CleanAdvancedControl()
                    ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("TRMEmpty")
                    Exit Sub
                End If

                Me._TRMValue = Me._listTRM.FirstOrDefault(Function(a) a.CurrencyId = CurrencyTypeId _
                                                               AndAlso a.OfficialCurrencyId = Me.CurrencyAdvanceId _
                                                               AndAlso a.MeasurementDate = GetDateServer().Date)?.Value

                BalanceAdvance = Math.Round(portfolioAdv.Balance / Me._TRMValue, Me._decimals)
                Dim balanceResidue As Decimal = BalanceAdvance - TotalBalancePortfolioAdvance.Where(Function(o) o.Key.Split("-")(0) = portfolioAdv.Code AndAlso Not o.Key.Split("-")(1).Equals(_objectNavigationCurrent.Folio.FolioOrder)).Sum(Function(o) o.Value)
                CrossingValue = IIf(balanceResidue > BalanceTotalPatientValue, BalanceTotalPatientValue, balanceResidue)
            Else
                Me._TRMValue = 1
                Me.INDlciBalanceAdvance.Text = $"Saldo"
                BalanceAdvance = portfolioAdv?.Balance
                Dim balanceResidue As Decimal = CType(portfolioAdv.Balance, Decimal) - TotalBalancePortfolioAdvance.Where(Function(o) o.Key.Split("-")(0) = portfolioAdv.Code AndAlso Not o.Key.Split("-")(1).Equals(_objectNavigationCurrent.Folio.FolioOrder)).Sum(Function(o) o.Value)
                If Not IsPortfolioAdvanceByThirdPartyBeneficiary Then
                    CrossingValue = IIf(balanceResidue > BalanceTotalPatientValue, BalanceTotalPatientValue, balanceResidue)
                    If ListPortfolioAdvance IsNot Nothing AndAlso ListPortfolioAdvance.Count > 0 Then
                        If (ListPortfolioAdvance.Sum(Function(o) o.CrossingValue) + CrossingValue) > TotalValueToPatient Then
                            CrossingValue = TotalValueToPatient - ListPortfolioAdvance.Sum(Function(o) o.CrossingValue)
                        End If
                    End If
                Else
                    CrossingValue = balanceResidue
                End If
            End If
            INDspnCossingValue.Properties.MaxValue = CrossingValue
            Dim _culture = Me.GetCultuteInfo(Me.CurrencyAbbreviation)
            Me.INDtxtTRM.Properties.NullText = $"{Me.CurrencyAbbreviation} - TRM {Utils.VisibleTRM(Me._TRMValue)}"
            INDspnCossingValue.Focus()
        ElseIf Me.TaxDevolutionValue > 0 AndAlso (ListPortfolioAdvance Is Nothing OrElse Not ListPortfolioAdvance.Any()) Then
            Me.ValidationTaxDevolution()
        End If
    End Sub

    ''' <summary>
    '''  evento que se ejecuta al cambiar de valor la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCurrencyType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrencyType.EditValueChanged
        Await ChargueCurrencyInformation(Me.CurrencyTypeId)
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanging event of the INDlblPatientDiscount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub INDlblPatientDiscount_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDlblPatientDiscount.EditValueChanging
        If CareGroupParent Is Nothing OrElse Not ValidatePatientDiscount(e.NewValue) Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDlblPatientDiscount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDlblPatientDiscount_EditValueChanged(sender As Object, e As EventArgs) Handles INDlblPatientDiscount.EditValueChanged
        If CareGroupParent IsNot Nothing AndAlso INDliPatientDiscount.Visibility <> DevExpress.XtraLayout.Utils.LayoutVisibility.Never AndAlso _flagRefreshValues Then
            CrossingTotalRefresh()
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDlblEntityDiscount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDlblEntityDiscount_EditValueChanged(sender As Object, e As EventArgs) Handles INDlblEntityDiscount.EditValueChanged
        If CareGroupParent IsNot Nothing AndAlso CareGroupParent.CareGroupType = eCareGroupType.Particulares AndAlso _flagRefreshValues Then
            CrossingTotalRefresh()
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanging event of the INDlblEntityDiscount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub INDlblEntityDiscount_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDlblEntityDiscount.EditValueChanging
        If CareGroupParent IsNot Nothing AndAlso CareGroupParent.CareGroupType = eCareGroupType.Particulares Then
            If CareGroupParent Is Nothing OrElse Not ValidatePatientDiscount(e.NewValue) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub dteDatimeEngress_EditValueChanged(sender As Object, e As EventArgs) Handles dteDatimeEngress.EditValueChanged
        If _objectNavigationCurrent IsNot Nothing AndAlso Not _isLoadFolio Then
            _objectNavigationCurrent.OutputDate = dteDatimeEngress.EditValue
            If dteDatimeEngress.EditValue IsNot Nothing Then
                ' Validar que la fecha de egreso no sea menor a la de ingreso
                If dteDatimeEngress.EditValue < _admissionXpo.IFECHAING Then
                    ShowMessage(EeventViewerImages.Advertencia) = "La fecha de egreso: " & Format(dteDatimeEngress.EditValue, "dd/MM/yyyy HH:mm") & " no puede ser menor a la fecha de ingreso: " & Format(_admissionXpo.IFECHAING, "dd/MM/yyyy HH:mm") & " "
                    dteDatimeEngress.EditValue = Nothing
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Maneja el cambio de valor en el selector de fechas de egreso (para ingresos unificados)
    ''' </summary>
    Private Sub lueEgressDate_EditValueChanged(sender As Object, e As EventArgs) Handles lueEgressDate.EditValueChanged
        If _objectNavigationCurrent IsNot Nothing AndAlso Not _isLoadFolio Then
            If lueEgressDate.EditValue IsNot Nothing Then
                ' Convertir el valor a DateTime
                Dim selectedDate As DateTime = CDate(lueEgressDate.EditValue)

                ' Sincronizar con el DateEdit para mantener compatibilidad con lógica existente
                dteDatimeEngress.EditValue = selectedDate

                ' Asignar al objeto de navegación actual
                _objectNavigationCurrent.OutputDate = selectedDate

                ' Validar que la fecha de egreso no sea menor a la de ingreso
                If selectedDate < _admissionXpo.IFECHAING Then
                    ShowMessage(EeventViewerImages.Advertencia) = "La fecha de egreso: " & Format(selectedDate, "dd/MM/yyyy HH:mm") & " no puede ser menor a la fecha de ingreso: " & Format(_admissionXpo.IFECHAING, "dd/MM/yyyy HH:mm") & " "
                    lueEgressDate.EditValue = Nothing
                    dteDatimeEngress.EditValue = Nothing
                End If
            Else
                ' Si se limpia el valor, también limpiar el DateEdit
                dteDatimeEngress.EditValue = Nothing
                _objectNavigationCurrent.OutputDate = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Convierte el código del estado de ingreso a texto legible
    ''' </summary>
    Private Function GetAdmissionStatusText(statusCode As Char) As String
        Select Case statusCode
            Case " "c
                Return "Sin confirmar"
            Case "F"c
                Return "Confirmado"
            Case "A"c
                Return "Anulado"
            Case "C"c
                Return "Cerrado"
            Case Else
                Return "Desconocido"
        End Select
    End Function

    ''' <summary>
    ''' Configura el control selector de fechas de egreso
    ''' </summary>
    Private Sub ConfigureEgressDateSelector()
        ' Configurar según si hay fechas de egreso específicas de ingresos unificados

        If _availableEgressDates IsNot Nothing AndAlso _availableEgressDates.Count > 0 Then
            ' HAY ingresos unificados - Mostrar LookUpEdit con opciones

            ' Configurar el DataSource del LookUpEdit
            lueEgressDate.Properties.DataSource = _availableEgressDates
            lueEgressDate.Properties.DisplayMember = "DisplayText"
            lueEgressDate.Properties.ValueMember = "EgressDate"
            lueEgressDate.Properties.NullText = "Seleccione una fecha de egreso..."

            ' Seleccionar por defecto la fecha de egreso del ingreso cabecera (ingreso actual)
            Dim defaultEgress = _availableEgressDates _
                .FirstOrDefault(Function(opt) Not String.IsNullOrWhiteSpace(opt.AdmissionNumber) AndAlso
                                            opt.AdmissionNumber.Trim().Equals(AdmissionNumber?.Trim(), StringComparison.OrdinalIgnoreCase))

            If defaultEgress IsNot Nothing AndAlso lueEgressDate.EditValue Is Nothing Then
                lueEgressDate.EditValue = defaultEgress.EgressDate
                dteDatimeEngress.EditValue = defaultEgress.EgressDate
            End If

            ' Configurar las columnas visibles en el dropdown
            lueEgressDate.Properties.Columns.Clear()
            lueEgressDate.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Fecha de Egreso - Ingreso - Estado"))

            ' Hacer que se ajuste automáticamente al contenido
            lueEgressDate.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
            lueEgressDate.Properties.PopupWidth = 500

            ' Mostrar el LookUpEdit y ocultar el DateEdit
            INDLcilueEgressDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lueEgressDate.Enabled = True

            INDLciEgressDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            dteDatimeEngress.Enabled = False
        Else
            ' NO hay ingresos unificados - Usar el DateEdit normal

            ' Ocultar el LookUpEdit y mostrar el DateEdit
            INDLcilueEgressDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lueEgressDate.Enabled = False

            INDLciEgressDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            dteDatimeEngress.Enabled = True
        End If
    End Sub

    Private Sub sleDiag1_EditValueChanged(sender As Object, e As EventArgs) Handles sleDiag1.EditValueChanged
        If _objectNavigationCurrent IsNot Nothing AndAlso Not _isLoadFolio Then
            AssigningValuesToPreviewObject()
        End If
    End Sub

    Private Sub INDDteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteInitialDate.EditValueChanged
        If _objectNavigationCurrent IsNot Nothing AndAlso Not _isLoadFolio Then
            _objectNavigationCurrent.InitialDate = INDDteInitialDate.EditValue
            dteDatimeEngress.Properties.MinValue = INDDteInitialDate.EditValue
        End If
    End Sub

    Private Sub INDGleCourtAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleCourtAccount.EditValueChanged
        If _admissionType <> 1 AndAlso INDGleCourtAccount.EditValue IsNot Nothing Then
            If INDGleCourtAccount.EditValue = False Then
                '1. Fecha Ingreso - Fecha Egreso
                INDLciEgressDate.Text = "Fecha de Egreso"
                _objectNavigationCurrent.CutType = 1
                _objectNavigationCurrent.InitialDate = _objectNavigationCurrent.AdmissionDate
                _objectNavigationCurrent.OutputDate = _objectNavigationCurrent.EgressDate

                If _availableEgressDates IsNot Nothing AndAlso _availableEgressDates.Count > 0 Then
                    INDLcilueEgressDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lueEgressDate.Enabled = True
                    lueEgressDate.EditValue = _objectNavigationCurrent.OutputDate

                    INDLciEgressDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    dteDatimeEngress.Enabled = False
                End If

                If _egressChange Then
                    dteDatimeEngress.Properties.ReadOnly = False
                    lueEgressDate.Properties.ReadOnly = False
                Else
                    dteDatimeEngress.Properties.ReadOnly = True
                    lueEgressDate.Properties.ReadOnly = True
                End If
            ElseIf INDGleCourtAccount.EditValue = True Then
                '2. Fecha Ingreso - Fecha Corte
                If _availableEgressDates IsNot Nothing AndAlso _availableEgressDates.Count > 0 Then
                    INDLcilueEgressDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lueEgressDate.Enabled = False

                    INDLciEgressDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    dteDatimeEngress.Enabled = True
                End If
                INDLciEgressDate.Text = "Fecha de Corte"
                If _objectNavigationCurrent.CutDate Is Nothing Then
                    _objectNavigationCurrent.CutType = 2
                    _objectNavigationCurrent.InitialDate = _objectNavigationCurrent.AdmissionDate
                Else
                    _objectNavigationCurrent.CutType = 3
                    _objectNavigationCurrent.InitialDate = _objectNavigationCurrent.CutDate
                End If
                If Not _isChangeRecord Then
                    If _objectNavigationCurrent.hasExit Then
                        _objectNavigationCurrent.OutputDate = _objectNavigationCurrent.EgressDate
                    Else
                        _objectNavigationCurrent.OutputDate = GetDateServer()
                    End If
                End If

                If _egressChange Then
                    dteDatimeEngress.Properties.ReadOnly = False
                    lueEgressDate.Properties.ReadOnly = False
                End If
            End If

            INDDteInitialDate.EditValue = _objectNavigationCurrent.InitialDate
            dteDatimeEngress.Properties.MinValue = _objectNavigationCurrent.InitialDate
            dteDatimeEngress.Properties.MaxValue = _objectNavigationCurrent.OutputDate
            dteDatimeEngress.EditValue = _objectNavigationCurrent.OutputDate

        End If
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAdvance_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAdvance.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            If BalanceTotalPatientValue <= 0 Then
                ShowMessage(EeventViewerImages.Advertencia) = "Debe tener saldo a paciente para poder crear un recibo de caja"
                Exit Sub
            End If
            If ThirdPartyPatientId IsNot Nothing Then
                Using frm As New FrmCashReceivableLiquidation(_objectNavigationCurrent?.RevenueControlDetailId)
                    frm.FolioType = _objectNavigationCurrent.Folio.FolioType
                    frm.SourceDocument = eSourceDocument.Liquidation
                    frm.RevenueControlDetailId = _objectNavigationCurrent.Folio.Id
                    frm.LiquidationType = _objectNavigationCurrent.Folio.LiquidationType
                    frm.CostCenterId = _objectNavigationCurrent.Folio.CareGroupCostCenterId
                    frm.IdThirdParty = If(_objectNavigationCurrent?.ThirdPartyId > 0, _objectNavigationCurrent.ThirdPartyId, ThirdPartyPatientId)
                    frm.AdmissionNumber = AdmissionNumber
                    frm.PatientIdentification = Me.PatientIdentification
                    frm.PatientName = Me.PatientName
                    frm.CashDefaultValue = BalanceTotalPatientValue
                    frm.CurrencyInvoiceId = Me.CurrencyTypeId
                    AddHandler frm.ReturnCashReceivableId, AddressOf GetCashReceivableId
                    Dim transparent As New FrmTransparent(frm, False)
                    transparent.ShowDialog(Me)
                End Using
            Else
                ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("ThirdPartyPatientNecessary", GetType(CtrFolio).Name)
            End If
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Handles the Click event of the INDsbAddAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddAdvance_Click(sender As Object, e As EventArgs) Handles INDsbAddAdvance.Click
        If PortfolioAdvanceId <> 0 Then
            If ListPortfolioAdvance Is Nothing Then
                ListPortfolioAdvance = New List(Of PortfolioAdvance)()
            End If
            Dim portfolioAdv = DirectCast(DirectCast(INDgvAdvancePortfolio.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, PortfolioRepository.Portfolio_PortfolioAdvance)
            If Not AddAdvance(portfolioAdv, CrossingValue) Then
                Exit Sub
            End If
            CleanAdvancedControl()
            INDsleAdvance.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click

        Using model As New MLiquidation()
            Dim res = model.GetPendingRequestsToProcess(RTrim(AdmissionNumber))
            If res IsNot Nothing Then
                ShowMessage(EeventViewerImages.Advertencia) = $"Existen solicitudes en proceso dentro de la Central de mezclas (Campaña #{res.CampaignNumber})"
                INDsbAcept.Enabled = True
                Exit Sub
            End If
        End Using

        Dim resultDialog As System.Windows.Forms.DialogResult = System.Windows.Forms.DialogResult.Yes
        If ModeLiquidation = eModeLiquidation.SIMPLE Then
            resultDialog = MessageIndigo.Show(ResourceManager.GetString("FolioLiquidationContinue", GetType(CtrFolio).Name), MessageType.Question, Me.Text, Botones.SiNo)
            Me.ValidationTaxDevolution()
        End If

        If resultDialog = System.Windows.Forms.DialogResult.Yes Then
            INDsbAcept.Enabled = False

            If Me.BarraBotones.FilterDataSource IsNot Nothing Then 'Multiples folios

                Dim billingSetting As SettingsBilling = Nothing
                Using model As New MBillingSetting(Me.Tag)
                    billingSetting = model.GetSettingsBillingByIdUnitOperativeSimple(Me.IdOperatingUnitSelected, False)
                    If billingSetting Is Nothing OrElse billingSetting.Id = 0 Then
                        ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("ParameterNotExist", GetType(CtrFolio).Name)
                        INDsbAcept.Enabled = True
                        Exit Sub
                    End If
                End Using

                Dim validationMessage As New StringBuilder()
                For Each i In CType(Me.BarraBotones.FilterDataSource, List(Of ObjectFolioCrossing))
                    Dim xTotalEntity = i.Folio.ThirdPartySalesPrice
                    Dim xTotalPatient As Decimal = 0

                    If i.Folio.VoucherValue <> 0 Then
                        xTotalPatient = i.Folio.VoucherValue
                    Else
                        xTotalPatient = i.Folio.TotalPatientWithDiscount
                    End If

                    Dim xBalanceTotalPatientValue As Decimal = 0
                    If i.ListPortfolioAdvance IsNot Nothing AndAlso i.ListPortfolioAdvance.Count > 0 Then
                        xBalanceTotalPatientValue = (IIf(CareGroupParent.CareGroupType = eCareGroupType.Particulares, xTotalEntity, xTotalPatient)) - i.ListPortfolioAdvance.Sum(Function(x) x.CrossingValue) - i.TotalPatientDiscount
                    Else
                        xBalanceTotalPatientValue = (IIf(CareGroupParent.CareGroupType = eCareGroupType.Particulares, xTotalEntity, xTotalPatient)) - i.TotalPatientDiscount
                    End If

                    If xBalanceTotalPatientValue <> 0 AndAlso billingSetting.RequiresPermissionForCxCPatient AndAlso Not i.Folio.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.PermiteCuentaXCobrarPaciente)) Then
                        validationMessage.AppendLine(String.Format("El folio {0} genera pagaré pero el usuario actual no tiene permiso para ejecutar esta acción", i.Folio.FolioOrder))
                    End If

                    If _admissionType = 2 Then
                        If i.InitialDate Is Nothing Then
                            validationMessage.AppendLine(String.Format("El folio {0} no tiene fecha inicial", i.Folio.FolioOrder))
                        End If
                    End If

                    If i.OutputDate Is Nothing Then
                        validationMessage.AppendLine(String.Format("El folio {0} no tiene fecha de egreso", i.Folio.FolioOrder))
                    End If

                    If i.OutputDiagnosis Is Nothing Then
                        validationMessage.AppendLine(String.Format("El folio {0} no tiene diagnostico", i.Folio.FolioOrder))
                    End If

                    If INDLciConditionSale.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        If i.ConditionSalesId Is Nothing Then
                            validationMessage.AppendLine(String.Format("El folio {0} no tiene condicion de venta", i.Folio.FolioOrder))
                        End If
                    End If

                    If i.ObligateAddPortfolioAdvance And Not i.ListPortfolioAdvance.Exists(Function(x) x.PortfolioAdvanceType = 1) Then
                        If MessageIndigo.Show("La Entidad Responsable de Pago tiene anticipos con saldo vinculados al paciente. ¿Desea legalizar el anticipo?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            validationMessage.AppendLine(String.Format("La Entidad Responsable de Pago del folio {0} tiene anticipos disponibles con saldo vinculados al paciente.", i.Folio.FolioOrder))
                        End If
                    End If

                    If _TransactionEconomicActivity And i.IsElectronicBillerThirdParty And i.EconomicActivityId Is Nothing Then
                        validationMessage.AppendLine(String.Format("Por favor diligenciar la Actividad Económica del Cliente del folio {0}, ya que es Facturador Electrónico.", i.Folio.FolioOrder))
                    End If
                Next

                If validationMessage.Length > 0 Then
                    ShowMessage(EeventViewerImages.Advertencia) = validationMessage.ToString()
                    INDsbAcept.Enabled = True
                    Exit Sub
                End If
            Else 'Un folio
                If BalanceTotalPatientValue <> 0 AndAlso Not ValidatePermissionForCxCPatient() Then
                    INDsbAcept.Enabled = True
                    Exit Sub
                End If

                If _admissionType = 2 Then
                    If INDDteInitialDate.EditValue Is Nothing Then
                        ShowMessage(EeventViewerImages.Advertencia) = "Se debe seleccionar una fecha inicial"
                        INDsbAcept.Enabled = True
                        Exit Sub
                    End If
                End If

                If dteDatimeEngress.EditValue Is Nothing OrElse sleDiag1.EditValue Is Nothing Then
                    ShowMessage(EeventViewerImages.Advertencia) = "Se debe seleccionar una fecha y diagnostico para el egreso"
                    INDsbAcept.Enabled = True
                    Exit Sub
                End If

                If INDLciConditionSale.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If ConditionSalesId Is Nothing Then
                        ShowMessage(EeventViewerImages.Advertencia) = "Se debe seleccionar una condicion de venta"
                        INDsbAcept.Enabled = True
                        Exit Sub
                    End If
                End If

                If _objectNavigationCurrent.ObligateAddPortfolioAdvance And Not ListPortfolioAdvance.Exists(Function(x) x.PortfolioAdvanceType = 1) Then
                    If MessageIndigo.Show("La Entidad Responsable de Pago tiene anticipos con saldo vinculados al paciente. ¿Desea legalizar el anticipo?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        INDsbAcept.Enabled = True
                        Exit Sub
                    End If
                End If

                If _objectNavigationCurrent.IsElectronicBillerThirdParty And _TransactionEconomicActivity And EconomicActivityId Is Nothing Then
                    ShowMessage(EeventViewerImages.Advertencia) = "Por favor diligenciar la Actividad Económica del Cliente, ya que es Facturador Electrónico."
                    INDsbAcept.Enabled = True
                    Exit Sub
                End If
            End If

            AssigningValuesToPreviewObject()
            Dim evn As New RunLiquidateFolioEventArgs()
            Dim revenueControlDetailCrossingList As New List(Of RevenueControlDetailCrossing)()

            If LstFoliosToLiquidate.Count > 1 Then
                For Each objectCrossing As ObjectFolioCrossing In Me.BarraBotones.FilterDataSource
                    Dim rcdCrossing As New RevenueControlDetailCrossing()
                    With rcdCrossing
                        .FolioOrder = objectCrossing.Folio.FolioOrder
                        .FolioType = objectCrossing.Folio.FolioType
                        .CareGroupId = objectCrossing.Folio.CareGroupId
                        .RevenueControlDetailId = objectCrossing.RevenueControlDetailId
                        .ListPortfolioAdvance = objectCrossing.ListPortfolioAdvance
                        .TotalPatientDiscount = objectCrossing.TotalPatientDiscount
                        .RevenueControlId = objectCrossing.Folio.RevenueControlId
                        .InitialDate = objectCrossing.InitialDate
                        .CutType = objectCrossing.CutType
                        .OutputDate = objectCrossing.OutputDate
                        .IsCutAccount = objectCrossing.IsCutAccount
                        .OutputDiagnosis = objectCrossing.OutputDiagnosis
                        .CurrencyId = objectCrossing.CurrencyId
                        .TRMValue = objectCrossing.TRMValue
                        .TaxDevolutionValue = objectCrossing.TaxDevolutionValue
                        .ConditionSalesId = objectCrossing.ConditionSalesId
                        .EconomicActivityId = objectCrossing.EconomicActivityId
                    End With
                    revenueControlDetailCrossingList.Add(rcdCrossing)
                Next
            Else
                Dim rcdCrossing As New RevenueControlDetailCrossing()
                With rcdCrossing
                    .FolioOrder = _objectNavigationCurrent.Folio.FolioOrder
                    .FolioType = _objectNavigationCurrent.Folio.FolioType
                    .CareGroupId = _objectNavigationCurrent.Folio.CareGroupId
                    .RevenueControlDetailId = _objectNavigationCurrent.RevenueControlDetailId
                    .ListPortfolioAdvance = _objectNavigationCurrent.ListPortfolioAdvance
                    .TotalPatientDiscount = _objectNavigationCurrent.TotalPatientDiscount
                    .RevenueControlId = _objectNavigationCurrent.Folio.RevenueControlId
                    .CutType = _objectNavigationCurrent.CutType
                    .InitialDate = _objectNavigationCurrent.InitialDate
                    .OutputDate = _objectNavigationCurrent.OutputDate
                    .IsCutAccount = _objectNavigationCurrent.IsCutAccount
                    .OutputDiagnosis = _objectNavigationCurrent.OutputDiagnosis
                    .CurrencyId = Me.CurrencyTypeId
                    .TRMValue = If(_objectNavigationCurrent.TRMValue Is Nothing, Me._tRMInvoicePartial, _objectNavigationCurrent.TRMValue)
                    .TaxDevolutionValue = Me.TaxDevolutionValue
                    .ConditionSalesId = Me.ConditionSalesId
                    .EconomicActivityId = Me.EconomicActivityId
                End With
                revenueControlDetailCrossingList.Add(rcdCrossing)
            End If

            evn.revenueControlDetailCrossingList = revenueControlDetailCrossingList

            '**********Falta retornar los datos*********************'
            RaiseEvent RunLiquidateFolio(Me, evn)
            'Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the FrmAdvanceCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmAdvanceCrossing_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceAddAdvancePortfolio control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddAdvancePortfolio_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAddAdvancePortfolio.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceAddAdvancePortfolio.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDrpCrossingValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDrpCrossingValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDrpCrossingValue.KeyDown
        _previusValue = CType(INDgvAdvance.GetFocusedRow(), PortfolioAdvance).CrossingValue
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' consulta los tipo de moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrencyType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCurrencyType.QueryPopUp
        If INDsleCurrencyType.Properties.DataSource Is Nothing Then
            INDsleCurrencyType.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAdvance_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAdvance.QueryPopUp
        If INDsleAdvance.Properties.DataSource Is Nothing Then
            LoadAdvance()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Popup event of the INDpceAddAdvancePortfolio control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddAdvancePortfolio_Popup(sender As Object, e As EventArgs) Handles INDpceAddAdvancePortfolio.Popup
        INDsbAcept.Enabled = False
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim pFolioAdvance = CType(INDgvAdvance.GetFocusedRow(), PortfolioAdvance)
            ListPortfolioAdvance.Remove(pFolioAdvance)
            If Me.BarraBotones.FilterDataSource IsNot Nothing Then
                If TotalBalancePortfolioAdvance IsNot Nothing AndAlso TotalBalancePortfolioAdvance.Any() Then
                    TotalBalancePortfolioAdvance.Remove(String.Concat(pFolioAdvance.Code, "-", _objectNavigationCurrent.Folio.FolioOrder))
                End If
            End If
            INDgcPortfolioAdvance.RefreshDataSource()
            Me.ValidationTaxDevolution()
            INDsleCurrencyType.Enabled = True
        End If
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls(folioCrossing As ObjectFolioCrossing)
        Try
            Me.INDliTotalPatient.HideControl(False)
            Me.INDliPatientDiscount.HideControl(False)
            Me._isMasterAccount = folioCrossing?.Folio?.IsMasterAccount
            _isLoadFolio = True
            ListPortfolioAdvance = folioCrossing.ListPortfolioAdvance
            SearchCareGroup(Me._settingLiquidateMasterAccount)
            Me.TaxDevolutionValue = 0

            'se consulta los datos originales del folio
            Dim originalValues = Await Me.FolioOriginalValues
            'si viene vacio y solo estamos liquidando un folio al tiempo cierra el popup de cruce para no permitir que  usuario siga usando popup sin esta informacion crucial
            If originalValues Is Nothing Then
                If LstFoliosToLiquidate.Count = 1 Then
                    Me.Close()
                End If
                Exit Sub
            End If

            Await ChargueCurrencyInformation(Me.CurrencyTypeId)

            CheckCanPatientDiscountModified()
            If (CareGroupParent.CareGroupType = eCareGroupType.Particulares AndAlso TotalValueToPatient = 0) OrElse Me._settingLiquidateMasterAccount Then
                INDlblEntityDiscount.Properties.ReadOnly = True
            End If

            Using model As New MLiquidation()
                _admissionXpo = model.GetAdmissionXpo(AdmissionNumber)

                If folioCrossing.ApplyLogicThirdPartyBeneficiary Then
                    Dim advances = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.
                            GetPortfolioAdvanceByThirdPartyIdAndAdmissionAndThirdPartyBeneficiary(
                            folioCrossing.ThirdPartyId,
                            ThirdPartyPatientId,
                            AdmissionNumber)

                    Dim hasAdvances As Boolean = (advances IsNot Nothing AndAlso advances.Count > 0)

                    folioCrossing.ObligateAddPortfolioAdvance = hasAdvances AndAlso
                        (MessageIndigo.Show(
                        "La Entidad Responsable de Pago tiene anticipos con saldo vinculados al paciente. ¿Desea legalizar el anticipo?",
                        MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes)
                Else
                    folioCrossing.ObligateAddPortfolioAdvance = False
                End If

                If folioCrossing.Folio.LiquidationType = 1 AndAlso _TransactionEconomicActivity Then
                    INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    Dim activities As List(Of CommonRepository.CommonEconomicActivity) =
                        Await model.ListEconomicActivities(folioCrossing.ThirdPartyId)

                    INDSleEconomicActivity.Properties.DataSource = activities

                    ' Busca la actividad economica por defecto (Defect = True)
                    Dim defaultEconomicActivityId As Integer? =
                        activities?.
                        SelectMany(Function(ea) ea.CommonThirdPartyEconomicActivitiesXpo).
                        Where(Function(ct) ct.Defect).
                        OrderBy(Function(ct) ct.Id).
                        Select(Function(ct) CType(ct.EconomicActivityId.Id, Integer?)).
                        FirstOrDefault()

                    'Valida facturación electrónica del tercero
                    Dim tp = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.
                            GetXPOObject(Of CommonRepository.CommonThirdPartyXpo)($"Id = {folioCrossing.ThirdPartyId}")

                    folioCrossing.IsElectronicBillerThirdParty = tp?.ElectronicBiller

                    If Not defaultEconomicActivityId.HasValue AndAlso tp?.ElectronicBiller Then
                        ShowMessage(EeventViewerImages.Advertencia) = "Por favor asocie una Actividad Económica al Cliente"
                        Me.Close()
                        Exit Sub
                    End If

                    EconomicActivityId = defaultEconomicActivityId
                End If

                If _admissionType = 1 Then 'ambulatorio
                    Dim HCREGEGRE = model.GetHCREGEGREByAdmissionNumber(AdmissionNumber)

                    If folioCrossing.OutputDiagnosis IsNot Nothing Then
                        folioCrossing.OutputDiagnosis = _admissionXpo.CODDIAEGR
                    End If
                    sleDiag1.EditValue = folioCrossing.OutputDiagnosis
                    If sleDiag1.EditValue IsNot Nothing Then
                        sleDiag1.Properties.ReadOnly = True
                    End If

                    ConditionSalesId = folioCrossing.ConditionSalesId
                    If folioCrossing.EconomicActivityId IsNot Nothing Then
                        EconomicActivityId = folioCrossing.EconomicActivityId
                    End If

                    If _egressChange Then
                        sleDiag1.Properties.ReadOnly = False
                    Else
                        sleDiag1.Properties.ReadOnly = True
                    End If

                    INDLciInitialDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciCourtAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    'no se pregunta por el permiso porque es ambulatorio
                    If HCREGEGRE IsNot Nothing Then
                        If folioCrossing.OutputDate IsNot Nothing Then
                            folioCrossing.OutputDate = HCREGEGRE.FECALTPAC
                        End If

                        dteDatimeEngress.EditValue = folioCrossing.OutputDate.AsDateTime.Value.AddMinutes(10)
                        lueEgressDate.EditValue = dteDatimeEngress.EditValue
                        If _egressChange Then
                            dteDatimeEngress.Properties.ReadOnly = False
                            lueEgressDate.Properties.ReadOnly = False
                        Else
                            dteDatimeEngress.Properties.ReadOnly = True
                            lueEgressDate.Properties.ReadOnly = True
                        End If
                    Else
                        dteDatimeEngress.EditValue = _admissionXpo.IFECHAING.AsDateTime.Value.AddMinutes(10) 'folioCrossing.OutputDate
                        lueEgressDate.EditValue = dteDatimeEngress.EditValue
                        folioCrossing.OutputDate = dteDatimeEngress.EditValue
                    End If

                    folioCrossing.IsCutAccount = False
                    folioCrossing.CutType = 1
                    folioCrossing.InitialDate = _admissionXpo.IFECHAING
                    INDDteInitialDate.EditValue = folioCrossing.InitialDate
                Else 'hospitalario
                    Dim INDIAGNOP = model.GetINDIAGNOPByAdmissionNumber(AdmissionNumber)
                    Dim CHREGEGRE = model.GetCHREGEGREByAdmissionNumber(AdmissionNumber)
                    Dim _listInvoices = model.ListInvoiceByAdminssionNumber(AdmissionNumber)

                    'Cargo los datos necesarios para validar cortes
                    Dim isOnlyFolio As Boolean = If(Me.FolioQuantity > 1, False, True)
                    If _admissionXpo IsNot Nothing Then
                        folioCrossing.AdmissionDate = _admissionXpo.IFECHAING
                    End If
                    If CHREGEGRE IsNot Nothing AndAlso CHREGEGRE.FECEGRESO > New Date(1970, 1, 1) Then
                        folioCrossing.EgressDate = CHREGEGRE.FECEGRESO
                        folioCrossing.hasExit = True
                    Else 'Si no Tomamos la fecha y hora actual
                        folioCrossing.EgressDate = GetDateServer()
                    End If
                    If _listInvoices IsNot Nothing AndAlso _listInvoices.Any(Function(d) d.CareGroupId = CareGroupParent.Id) Then
                        folioCrossing.CutDate = (From x In _listInvoices Where x.CareGroupId = CareGroupParent.Id Select x).FirstOrDefault().OutputDate.AddSeconds(1)
                    End If

                    'Asigno valores si estos no han sido asginados previamente
                    If folioCrossing.InitialDate Is Nothing Then 'Si el folio aun no se le ha asignado fecha inicial
                        folioCrossing.InitialDate = folioCrossing.AdmissionDate
                    End If
                    If folioCrossing.OutputDate Is Nothing Then 'Si el folio aun no se le ha asignado fecha final
                        folioCrossing.OutputDate = folioCrossing.EgressDate
                    End If
                    If folioCrossing.OutputDiagnosis Is Nothing AndAlso INDIAGNOP IsNot Nothing Then
                        folioCrossing.OutputDiagnosis = INDIAGNOP.CODDIAGNO
                    End If

                    'Asigno los valores por defecto
                    INDLciInitialDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDDteInitialDate.Properties.ReadOnly = True
                    INDLciCourtAccount.Visibility = If(isOnlyFolio AndAlso folioCrossing.hasExit, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    INDGleCourtAccount.EditValue = Nothing
                    INDGleCourtAccount.EditValue = folioCrossing.IsCutAccount
                    INDGleCourtAccount.Properties.ReadOnly = If(isOnlyFolio AndAlso folioCrossing.hasExit, True, False)
                    sleDiag1.EditValue = folioCrossing.OutputDiagnosis
                    ConditionSalesId = folioCrossing.ConditionSalesId

                    If folioCrossing.EconomicActivityId IsNot Nothing Then
                        EconomicActivityId = folioCrossing.EconomicActivityId
                    End If

                    'Habilito o no la modificacion de acuerdo a los permisos que se posean
                    If Not _egressChange Then
                        sleDiag1.Properties.ReadOnly = True
                    Else
                        sleDiag1.Properties.ReadOnly = False
                    End If
                End If
            End Using

            INDgcPortfolioAdvance.RefreshDataSource()
            CrossingTotalRefresh()
            INDteBalance.Properties.ReadOnly = True
            INDpceAddAdvancePortfolio.Focus()
            If ListPortfolioAdvance.Count = 0 AndAlso
            (TotalPatient > 0 OrElse (_settingLiquidateMasterAccount AndAlso {4, 0}.Contains(folioCrossing.Folio.IsMasterAccount) AndAlso Me.TotalEntity > 0)) _
            AndAlso Me.BarraBotones.FilterDataSource Is Nothing Then
                AutoLoadAdvances()
            End If
            _isLoadFolio = False
        Catch ex As Exception
            ShowMessage(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' funcion que nos retorna la conversion de los datos dependiendo de la moneda seleccionada
    ''' </summary>
    Private Async Function ChargueCurrencyInformation(currencyTypeId As Integer?) As Task
        If _flagInitLoad Then
            Return
        End If

        If indigo?.OfficialCurrencyId Is Nothing Then
            ShowMessage(EeventViewerImages.Advertencia) = "Moneda oficial del sistema vacía"
            Return
        End If

        If currencyTypeId Is Nothing Then
            Me.CurrencyTypeId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
            Return
        End If

        If currencyTypeId = indigo.OfficialCurrencyId Then
            CurrencyVisualitationValues(Await Me.FolioOriginalValues, False)
            Return
        End If

        Dim result = Await GetInvoicePartial(_objectNavigationCurrent?.RevenueControlDetailId, currencyTypeId)

        SafeInvoke(Sub()
                       CurrencyVisualitationValues(result, False)
                   End Sub)
    End Function

    ''' <summary>
    ''' metodo que prepara el form para liquidar multiples folios
    ''' </summary>
    Private Async Sub PrepareFormMultiple()
        LstFoliosToLiquidate = New List(Of ICtrFolio)()
        AdmissionObject.Id = AdmissionObject.RevenueControlId
        AdmissionObject.SMLV = 0
        INDgvAdvance.ShowLoadingPanel()

        LblAdmission.Text = AdmissionObject.AdmissionCodeWithOutTrim
        LblPatient.Text = AdmissionObject.PatientName

        'Consultamos los ids de los folios que pertenecen al ingreso
        Using model As New MLiquidation()
            Dim dtResult As DataTable = Await model.ExecuteCommandDt($"select * from billing.revenuecontroldetail where revenuecontrolid = {AdmissionObject.RevenueControlId}", SessionValues.Instance.TransactionalContainer)
            If dtResult IsNot Nothing AndAlso dtResult.Rows.Count > 0 Then

                Dim totalFolios As Integer = dtResult.Rows.Count
                Dim countFolios As Integer = 0
                For Each r As DataRow In dtResult.Rows
                    Dim newFolio As New CtrFolio(Me.Owner, AdmissionObject)
                    newFolio.IsOncologycalMode = False
                    newFolio.SetSleNullText("")
                    newFolio.IsUnique = True '(List.Count = 1)
                    newFolio.AdmissionType = CInt(AdmissionObject.AdmissionType)
                    newFolio.EgressChange = EgressChange
                    newFolio.SetDatasourceAsync(r.Item("Id"), r.Item("Status"))
                    AddHandler newFolio.LoadDatasourceEnd, Sub()
                                                               LstFoliosToLiquidate.Add(newFolio)
                                                               countFolios += 1
                                                               If totalFolios = countFolios Then
                                                                   INDgvAdvance.HideLoadingPanel()
                                                                   PrepareForm()
                                                               End If
                                                           End Sub
                Next
            Else
                INDgvAdvance.HideLoadingPanel()
            End If
        End Using

    End Sub

    ''' <summary>
    ''' Asigna los valores al objeto cargado antes de dar click
    ''' </summary>
    Private Sub AssigningValuesToPreviewObject()
        'Aca puedo asignar los valores a la entidad
        If _objectNavigationCurrent IsNot Nothing Then
            _objectNavigationCurrent.TotalPatientDiscount = TotalDiscountToPatient
            _objectNavigationCurrent.OutputDate = dteDatimeEngress.EditValue
            _objectNavigationCurrent.OutputDiagnosis = sleDiag1.EditValue
            _objectNavigationCurrent.InitialDate = INDDteInitialDate.EditValue
            _objectNavigationCurrent.IsCutAccount = INDGleCourtAccount.EditValue
            _objectNavigationCurrent.CurrencyId = Me.CurrencyTypeId
            _objectNavigationCurrent.TaxDevolutionValue = Me.TaxDevolutionValue
            _objectNavigationCurrent.TRMValue = Me.TRMInvoicePartial
            _objectNavigationCurrent.ConditionSalesId = Me.ConditionSalesId
            _objectNavigationCurrent.EconomicActivityId = Me.EconomicActivityId
            'acomulo los saldos utilizados de los anticipos
            For Each adv As PortfolioAdvance In _objectNavigationCurrent.ListPortfolioAdvance
                If TotalBalancePortfolioAdvance.ContainsKey(String.Concat(adv.Code, "-", _objectNavigationCurrent.Folio.FolioOrder)) Then
                    TotalBalancePortfolioAdvance(String.Concat(adv.Code, "-", _objectNavigationCurrent.Folio.FolioOrder)) = adv.CrossingValue
                Else
                    TotalBalancePortfolioAdvance.Add(String.Concat(adv.Code, "-", _objectNavigationCurrent.Folio.FolioOrder), adv.CrossingValue)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Prepares the form.
    ''' </summary>
    Private Sub PrepareForm()
        LstFoliosToLiquidate = LstFoliosToLiquidate.OrderBy(Function(o) o.FolioOrder).ToList()
        Using model As New MLiquidation()
            _admissionXpo = model.GetAdmissionXpo(AdmissionNumber)
            Dim HCREGEGRE = model.GetHCREGEGREByAdmissionNumber(AdmissionNumber)



            If LstFoliosToLiquidate.Count > 1 Then
                Me.ToolBar.Visible = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

                Dim listObjectNavigation As New List(Of ObjectFolioCrossing)()
                For Each folio As CtrFolio In LstFoliosToLiquidate
                    Dim objNav As New ObjectFolioCrossing() With {.Name = String.Format("Folio {0}", folio.GetThirdPartyId), .RevenueControlDetailId = folio.Id, .Folio = folio, .ListPortfolioAdvance = New List(Of PortfolioAdvance)(), .ThirdPartyId = folio.GetThirdPartyId(), .ApplyLogicThirdPartyBeneficiary = folio.ApplyLogicThirdPartyBeneficiary}
                    If _admissionType = 1 Then 'ambulatorio
                        objNav.InitialDate = _admissionXpo.IFECHAING
                        objNav.OutputDiagnosis = _admissionXpo.CODDIAEGR
                        If HCREGEGRE IsNot Nothing Then
                            objNav.OutputDate = HCREGEGRE.FECALTPAC
                        End If
                    End If
                    listObjectNavigation.Add(objNav)
                Next
                Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Folio", .FieldName = "Name"}}.ToList()
                Me.BarraBotones.FilterDataSource = listObjectNavigation
            Else
                Me.Text = String.Format("Cierre de Factura - FOLIO # {0}", LstFoliosToLiquidate(0).FolioOrder)
                _objectNavigationCurrent = New ObjectFolioCrossing() With {.Name = String.Format("Folio {0}", LstFoliosToLiquidate(0).FolioOrder), .RevenueControlDetailId = LstFoliosToLiquidate(0).Id, .Folio = LstFoliosToLiquidate(0), .ListPortfolioAdvance = New List(Of PortfolioAdvance)(), .ThirdPartyId = LstFoliosToLiquidate(0).ThirdPartyId, .ApplyLogicThirdPartyBeneficiary = LstFoliosToLiquidate(0).ApplyLogicThirdPartyBeneficiary}
                If _admissionType = 1 Then 'ambulatorio
                    _objectNavigationCurrent.InitialDate = _admissionXpo.IFECHAING
                    _objectNavigationCurrent.OutputDiagnosis = _admissionXpo.CODDIAEGR
                    If HCREGEGRE IsNot Nothing Then
                        _objectNavigationCurrent.OutputDate = HCREGEGRE.FECALTPAC
                    End If
                End If
                LoadControls(_objectNavigationCurrent)
            End If
        End Using
    End Sub

    Public Sub LoaderAsync(ByVal State As Boolean)
        For i = 0 To Me.PcForm.Controls.Count - 1
            If Me.PcForm.Controls(i).GetType().Equals(GetType(LayoutControl)) Then
                Me.PcForm.Controls(i).Enabled = Not State
                If State = False Then
                    PcForm.Controls(i).Focus()
                End If
                Exit For
            End If
        Next

        INDsbAcept.Enabled = Not State
    End Sub

    ''' <summary>
    ''' Validates the patient discount.
    ''' </summary>
    Private Function ValidatePatientDiscount(Optional newValue As Decimal = 0) As Boolean
        If CareGroupParent IsNot Nothing AndAlso Not Me._settingLiquidateMasterAccount Then
            If TotalPatientReference - ListPortfolioAdvance.Sum(Function(x) x.CrossingValue) - newValue < 0 Then
                ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("DiscountMoreTotalPatient", GetType(CtrFolio).Name)
                'TotalDiscountToPatient = TotalValueToPatient
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Validates the advance.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateAdvance(porfAdv As PortfolioRepository.Portfolio_PortfolioAdvance, crossingValue As Decimal, PortfolioType As Byte) As Boolean
        Dim errorList As New StringBuilder()
        Dim _totalBalanceUsed = TotalBalancePortfolioAdvance.Where(Function(o) o.Key.Split("-")(0) = porfAdv.Code AndAlso Not o.Key.Split("-")(1).Equals(_objectNavigationCurrent.Folio.FolioOrder)).Sum(Function(o) o.Value)
        If (_totalBalanceUsed + crossingValue) > Math.Round(porfAdv.Balance / Me._TRMValue.Value, 2) Then
            errorList.AppendLine("El total a pagar del anticipo " + porfAdv.Code + " no debe superar el saldo. (revisar cruce de los otros folios)")
        End If

        If ListPortfolioAdvance.FindAll(Function(o) o.Id = porfAdv.Id).Count > 0 Then
            errorList.AppendLine(ResourceManager.GetString("AdvanceExist", GetType(CtrFolio).Name))
        End If

        Select Case PortfolioType
            Case 1
                If crossingValue > TotalEntity Then
                    errorList.AppendLine("El valor del anticipo no puede ser superior al total de la Entidad")
                End If

                If (ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 1).Sum(Function(o) o.CrossingValue) + crossingValue) > TotalEntity Then
                    errorList.AppendLine("No se puede superar el valor a pagar por la entidad")
                End If
            Case 2
                If (ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 2).Sum(Function(o) o.CrossingValue) + crossingValue) > TotalValueToPatient Then
                    errorList.AppendLine(ResourceManager.GetString("MessageValidatePatientValue", GetType(CtrFolio).Name))
                End If

                If TotalValueToPatient - ListPortfolioAdvance.Where(Function(n) n.PortfolioAdvanceType = 2).Sum(Function(x) x.CrossingValue) - TotalDiscountToPatient < crossingValue Then
                    errorList.AppendLine("No se puede cruzar mas anticipos por que el saldo es cero")
                End If
        End Select

        If crossingValue = 0 Then
            errorList.AppendLine(ResourceManager.GetString("MessageCrossingValue3", GetType(CtrFolio).Name))
        End If

        If errorList.Length > 0 Then
            ShowMessage(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Loads the advance.
    ''' </summary>
    Private Sub LoadAdvance()
        If ThirdPartyPatientId IsNot Nothing Then
            INDsleAdvance.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.GetPortfolioAdvanceByThirdPartyIdAndAdmission(ThirdPartyPatientId, AdmissionNumber, _objectNavigationCurrent.Folio.ThirdPartyId)
        Else
            ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("ThirdPartyPatientNecessary", GetType(CtrFolio).Name)
        End If
    End Sub

    ''' <summary>
    ''' Gets the cash receivable identifier.
    ''' </summary>
    ''' <param name="cashReceivableId">The cash receivable identifier.</param>
    Private Async Sub GetCashReceivableId(cashReceivableId As Integer)
        INDsleAdvance.Properties.DataSource = Nothing
        LoadAdvance()

        If cashReceivableId > 0 Then
            Using model As New MPortfolioAdvance(Me.Tag)
                Dim portfolioAdv As PortfolioRepository.Portfolio_PortfolioAdvance = model.GetPortfolioAdvanceById(cashReceivableId)
                SetTaxDevolutionValue(portfolioAdv)
                Dim balanceCurrency As Decimal = portfolioAdv?.Balance
                If Me.CurrencyTypeId <> portfolioAdv?.CurrencyId Then
                    Await Me.loadListTRM(Me.CurrencyTypeId, portfolioAdv?.CurrencyId)
                    Me._TRMValue = Me._listTRM?.FirstOrDefault(Function(f) f.CurrencyId = Me.CurrencyTypeId _
                                                                   AndAlso f.OfficialCurrencyId = portfolioAdv?.CurrencyId _
                                                                   AndAlso f.MeasurementDate = GetDateServer().Date)?.Value
                    balanceCurrency = Math.Round(CDec(portfolioAdv?.Balance / _TRMValue), Me._decimals, MidpointRounding.AwayFromZero)
                Else
                    Me._TRMValue = 1
                End If
                Dim balanceResidue As Decimal = balanceCurrency - TotalBalancePortfolioAdvance.Where(Function(o) o.Key.Split("-")(0) = portfolioAdv.Code AndAlso Not o.Key.Split("-")(1).Equals(_objectNavigationCurrent.Folio.FolioOrder)).Sum(Function(o) o.Value)
                Dim crossingValue As Decimal = IIf(balanceResidue > BalanceTotalPatientValue, BalanceTotalPatientValue, balanceResidue)
                If ListPortfolioAdvance Is Nothing Then
                    ListPortfolioAdvance = New List(Of PortfolioAdvance)()
                ElseIf ListPortfolioAdvance.Count > 0 Then

                    If (ListPortfolioAdvance.Sum(Function(o) o.CrossingValue) + crossingValue) > TotalValueToPatient Then
                        crossingValue = TotalValueToPatient - ListPortfolioAdvance.Sum(Function(o) o.CrossingValue)
                    End If
                End If
                AddAdvance(portfolioAdv, crossingValue)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Valida el permiso para generar pagarés
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidatePermissionForCxCPatient() As Boolean
        Using model As New MBillingSetting(Me.Tag)
            Dim billingSetting As SettingsBilling = model.GetSettingsBillingByIdUnitOperativeSimple(Me.IdOperatingUnitSelected, False)
            If billingSetting IsNot Nothing AndAlso billingSetting.Id > 0 Then
                If billingSetting.RequiresPermissionForCxCPatient Then
                    If Not _objectNavigationCurrent.Folio.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.PermiteCuentaXCobrarPaciente)) Then
                        ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("PermissionPagare", GetType(CtrFolio).Name)
                        Return False
                    End If
                End If
            Else
                ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("ParameterNotExist", GetType(CtrFolio).Name)
                Return False
            End If
        End Using
        Return True
    End Function

    ''' <summary>
    ''' Muestra un mensaje en el frontal
    ''' </summary>
    ''' <param name="Icon">Icono del tipo de mensaje</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Property ModeLiquidation As eModeLiquidation = eModeLiquidation.SIMPLE

    ''' <summary>
    ''' Calcula el valor total a cruzar
    ''' </summary>
    Private Sub CrossingTotalRefresh()
        If ListPortfolioAdvance IsNot Nothing AndAlso ListPortfolioAdvance.Count > 0 Then
            ValuePortfolioAdvance = Math.Round(ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 1).Sum(Function(x) x.CrossingValue), 2, MidpointRounding.AwayFromZero)
            BalanceTotalPatientValue = Math.Round((TotalValueToPatient - ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 2).Sum(Function(x) x.CrossingValue) - TotalDiscountToPatient - Me.TaxDevolutionValue), 2, MidpointRounding.AwayFromZero)
            BalanceTotalThirdPartyValue = Math.Round(TotalEntity - ValuePortfolioAdvance, 2, MidpointRounding.AwayFromZero)
        Else
            ValuePortfolioAdvance = Math.Round(ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 1).Sum(Function(x) x.CrossingValue), 2, MidpointRounding.AwayFromZero)
            BalanceTotalThirdPartyValue = ValuePortfolioAdvance
            BalanceTotalPatientValue = Math.Round((TotalValueToPatient - TotalDiscountToPatient - Me.TaxDevolutionValue), 2, MidpointRounding.AwayFromZero)
        End If
        If BalanceTotalPatientValue > 0 Then
            If INDpcPatientBalance.InvokeRequired Then
                INDpcPatientBalance.BeginInvoke(Sub()
                                                    INDpcPatientBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(0, Byte), Integer))
                                                End Sub)
            Else
                INDpcPatientBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(0, Byte), Integer))
            End If
        Else
            If INDpcPatientBalance.InvokeRequired Then
                INDpcPatientBalance.BeginInvoke(Sub()
                                                    INDpcPatientBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
                                                End Sub)
            Else
                INDpcPatientBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            End If
        End If
        _objectNavigationCurrent.TotalPatientDiscount = TotalDiscountToPatient
    End Sub

    ''' <summary>
    ''' Valida el valor a cruzar
    ''' </summary>
    ''' <param name="portfolioAdvance">The portfolio advance.</param>
    ''' <returns></returns>
    Private Function ValidateCrossingValue(portfolioAdvance As PortfolioAdvance) As Boolean
        Dim errorList As New StringBuilder()
        If (ListPortfolioAdvance.Sum(Function(o) o.CrossingValue)) + PatientDiscount > TotalValueToPatient Then
            errorList.AppendLine(ResourceManager.GetString("MessageValidatePatientValue", GetType(CtrFolio).Name))
        End If

        'La suma del valor a cruzar de un anticipo no sea mayor al saldo del anticipo. (suma de todos los cruces de los folios)
        Dim _totalBalanceUsed = TotalBalancePortfolioAdvance.Where(Function(o) o.Key.Split("-")(0) = portfolioAdvance.Code AndAlso Not o.Key.Split("-")(1).Equals(_objectNavigationCurrent.Folio.FolioOrder)).Sum(Function(o) o.Value)
        If (_totalBalanceUsed + CType(portfolioAdvance.CrossingValue, Decimal)) > (portfolioAdvance.Balance) Then
            errorList.AppendLine("El total a pagar del anticipo " + portfolioAdvance.Code + " no debe superar el saldo. (revisar cruce de los otros folios)")
        End If

        'If portfolioAdvance.CrossingValue + PatientDiscount > TotalPatient Then
        '    errorList.AppendLine("El valor a cruzar no debe superar el valor a pagar por el paciente")
        'End If
        'If ListPortfolioAdvance.Sum(Function(o) o.CrossingValue) + PatientDiscount > TotalPatient Then
        '    errorList.AppendLine("El valor a cruzar no debe superar el valor a pagar por el paciente")
        'End If

        If portfolioAdvance.Balance < portfolioAdvance.CrossingValue Then
            errorList.AppendLine(ResourceManager.GetString("MessageCrossingValue2", GetType(CtrFolio).Name))
        End If
        If errorList.Length > 0 Then
            ShowMessage(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Searches the care group.
    ''' </summary>
    Private Sub SearchCareGroup(Optional settingLiquidateMasterAccount As Boolean? = Nothing)
        Using model As New MCareGroup(Me.Tag)
            CareGroupParent = (model.GetCareGroupByIdSimple(_objectNavigationCurrent.Folio.CareGroupId)).ObjectEmbbeded
            If CareGroupParent IsNot Nothing AndAlso CareGroupParent.Id > 0 Then
                If CareGroupParent.CareGroupType = eCareGroupType.Particulares AndAlso (settingLiquidateMasterAccount Is Nothing OrElse Not settingLiquidateMasterAccount) Then
                    INDliEntityTotal.Text = ResourceManager.GetString("TotalPatient", GetType(CtrFolio).Name)
                    INDliTotalPatient.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliPatientDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    INDliEntityDiscount.Text = "Dcto Paciente"
                    INDliEntityDiscount.MaxSize = New Drawing.Size(226, 30)
                    INDliEntityDiscount.MinSize = New Drawing.Size(226, 30)

                    INDliEntityDiscount.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    INDliEntityTotal.MaxSize = New Drawing.Size(226, 60)
                    INDliEntityTotal.MinSize = New Drawing.Size(226, 60)
                    INDpcPatientBalance.Size = New Drawing.Size(236, INDpcPatientBalance.Size.Height)
                    INDliPatientBalance.MaxSize = New Drawing.Size(226, INDliPatientBalance.MaxSize.Height)
                    INDliPatientBalance.MinSize = New Drawing.Size(226, INDliPatientBalance.MaxSize.Height)
                    INDliEntityDiscount.TextSize = New Drawing.Size(110, 25)
                    INDliEntityDiscount.TextToControlDistance = 0
                    INDliEntityDiscount.TextLocation = DevExpress.Utils.Locations.Top

                    INDlblEntityDiscount.MinimumSize = New Drawing.Size(226, 0)
                    INDlblEntityDiscount.MaximumSize = New Drawing.Size(226, 0)
                    INDlblEntityDiscount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                    INDlblEntityDiscount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 16.0!)
                    INDlblEntityDiscount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 16.0!)
                    INDlblEntityDiscount.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Segoe UI Light", 16.0!)
                    INDlblEntityDiscount.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Segoe UI Light", 16.0!)
                Else
                    INDlblEntityDiscount.Properties.ReadOnly = True
                    INDliEntityTotal.Text = ResourceManager.GetString("TotalEntity", GetType(CtrFolio).Name)
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Chequea si el usuario tiene permisos para modificar el descuento a paciente
    ''' </summary>
    Private Sub CheckCanPatientDiscountModified()
        If INDliPatientDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If TotalPatient > 0 AndAlso _objectNavigationCurrent.Folio.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.AgregarDescuentoPaciente)) Then
                INDlblPatientDiscount.Properties.ReadOnly = False
            Else
                INDlblPatientDiscount.Properties.ReadOnly = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Verifica que si hay anticipos con el mismo numero de admision para que los postule en la rejilla.
    ''' Busca anticipos del paciente original Y del tercero responsable del folio asociados al número de admisión.
    ''' </summary>
    Private Sub AutoLoadAdvances()
        ' Obtener anticipos del paciente Y del tercero del folio (si es diferente) asociados al número de admisión
        Dim thirdPartyFolioId As Integer? = _objectNavigationCurrent?.ThirdPartyId
        Dim listPortfolioAdv = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.GetCollectionPortfolioAdvanceByThirdPartyIdAndAdmission(ThirdPartyPatientId, AdmissionNumber, thirdPartyFolioId)

        If listPortfolioAdv?.ToEntityList(Of Object)?.Any() Then
            If ListPortfolioAdvance Is Nothing Then
                ListPortfolioAdvance = New List(Of PortfolioAdvance)()
            End If

            If listPortfolioAdv?.
               ToEntityList(Of PortfolioRepository.Portfolio_PortfolioAdvance)?.
               Any(Function(x) x.Balance >= BalanceTotalPatientValue AndAlso x.CurrencyId = Me.CurrencyTypeId) Then

                Me.SetTaxDevolutionValue(listPortfolioAdv?.ToEntityList(Of PortfolioRepository.Portfolio_PortfolioAdvance)?.
                                                                 OrderByDescending(Function(x) x.Balance)?.
                                                                 First(Function(x) x.Balance >= BalanceTotalPatientValue))

            ElseIf listPortfolioAdv?.
                   ToEntityList(Of PortfolioRepository.Portfolio_PortfolioAdvance)?.
                    All(Function(x) x.PaymentMethodName = listPortfolioAdv?.
                                                          ToEntityList(Of PortfolioRepository.Portfolio_PortfolioAdvance)?.
                                                          FirstOrDefault.PaymentMethodName AndAlso x.CurrencyId = Me.CurrencyTypeId) _
                    AndAlso
                    listPortfolioAdv?.
                   ToEntityList(Of PortfolioRepository.Portfolio_PortfolioAdvance)?.Sum(Function(d) d.Balance) >= BalanceTotalPatientValue Then

                Me.SetTaxDevolutionValue(listPortfolioAdv?.ToEntityList(Of PortfolioRepository.Portfolio_PortfolioAdvance)?.
                                                                First())

            End If

            For Each portfolioAdv In listPortfolioAdv?.
                                    ToEntityList(Of PortfolioRepository.Portfolio_PortfolioAdvance)?.FindAll(Function(x) x.CurrencyId = Me.CurrencyTypeId).
                                     OrderByDescending(Function(x) x.Balance)
                If BalanceTotalPatientValue > 0 Then
                    Me.SetTaxDevolutionValue(portfolioAdv)

                    ' Determinar si el anticipo pertenece al tercero beneficiado (entidad con paciente como beneficiario)
                    ' o si es directamente del paciente. Esto afecta el tipo de anticipo en AddAdvance.
                    IsPortfolioAdvanceByThirdPartyBeneficiary = (portfolioAdv.ThirdPartyBeneficiaryId IsNot Nothing AndAlso
                                                                 portfolioAdv.ThirdPartyBeneficiaryId.Id = ThirdPartyPatientId)

                    Dim balanceResidue As Decimal = portfolioAdv.Balance - TotalBalancePortfolioAdvance.Where(Function(o) o.Key.Split("-")(0) = portfolioAdv.Code AndAlso Not o.Key.Split("-")(1).Equals(_objectNavigationCurrent.Folio.FolioOrder)).Sum(Function(o) o.Value)
                    Dim crossingValue As Decimal = IIf(balanceResidue > BalanceTotalPatientValue, BalanceTotalPatientValue, balanceResidue)

                    ' Ajustar el crossingValue según el tipo de anticipo para evitar sobrepasar los límites
                    If Not IsPortfolioAdvanceByThirdPartyBeneficiary Then
                        ' Para anticipos del paciente, limitar por TotalValueToPatient
                        If ListPortfolioAdvance IsNot Nothing AndAlso ListPortfolioAdvance.Count > 0 Then
                            If (ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 2).Sum(Function(o) o.CrossingValue) + crossingValue) > TotalValueToPatient Then
                                crossingValue = TotalValueToPatient - ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 2).Sum(Function(o) o.CrossingValue)
                            End If
                        End If
                    Else
                        ' Para anticipos de entidad con beneficiario, limitar por TotalEntity
                        If ListPortfolioAdvance IsNot Nothing AndAlso ListPortfolioAdvance.Count > 0 Then
                            If (ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 1).Sum(Function(o) o.CrossingValue) + crossingValue) > TotalEntity Then
                                crossingValue = TotalEntity - ListPortfolioAdvance.Where(Function(y) y.PortfolioAdvanceType = 1).Sum(Function(o) o.CrossingValue)
                            End If
                        End If
                    End If

                    If crossingValue > 0 Then
                        AddAdvance(portfolioAdv, crossingValue)
                    End If
                End If
            Next
        End If
    End Sub

    Private Function AddAdvance(portfolioAdv As PortfolioRepository.Portfolio_PortfolioAdvance, crossingValue As Decimal) As Boolean
        ' Validar que los servicios del folio estén dentro del rango de atención antes de agregar el anticipo
        If Not ValidateServicesAttentionRange() Then
            Return False
        End If

        Dim PortfolioType As Byte = IIf(IsPortfolioAdvanceByThirdPartyBeneficiary, 1, 2)
        Dim PortfolioTypeDescription As String = IIf(PortfolioType = 1, "Anticipos Entidad", "Anticipos Paciente")

        If Not ValidateAdvance(portfolioAdv, crossingValue, PortfolioType) Then
            Return False
        End If

        If ListPortfolioAdvance Is Nothing Then
            ListPortfolioAdvance = New List(Of PortfolioAdvance)()
        End If

        ListPortfolioAdvance.Add(New PortfolioAdvance With
        {
            .Id = portfolioAdv.Id,
            .Code = portfolioAdv.Code,
            .AdmissionNumber = portfolioAdv.AdmissionNumber,
            .ThirdPartyId = portfolioAdv.ThirdPartyId.Id,
            .MainAccountId = portfolioAdv.MainAccountId.Id,
            .CostCenterId = If(portfolioAdv.CostCenterId IsNot Nothing, portfolioAdv.CostCenterId.Id, Nothing),
            .Value = portfolioAdv.Value / Me._TRMValue,
            .Balance = portfolioAdv.Balance / Me._TRMValue,
            .CrossingValue = crossingValue,
            .DocumentDate = portfolioAdv.DocumentDate,
            .CurrencyAbbreviation = Me.CurrencyAbbreviation,
            .PaymentMethodsType = If(portfolioAdv.CashReceiptId IsNot Nothing, portfolioAdv.CashReceiptId.PaymentMethod, 0),
            .PaymentMethodName = portfolioAdv.PaymentMethodName,
            .PortfolioAdvanceType = PortfolioType,
            .PortfolioAdvanceTypeDescription = PortfolioTypeDescription
        })

        INDgcPortfolioAdvance.RefreshDataSource()
        Me.ValidationTaxDevolution()
        INDsleCurrencyType.Enabled = False
        Return True
    End Function

    ''' <summary>
    ''' Valida que los servicios del folio estén dentro del rango de atención (ingreso-egreso seleccionado).
    ''' </summary>
    Private Function ValidateServicesAttentionRange(Optional folioCrossing As ObjectFolioCrossing = Nothing) As Boolean
        Dim target = If(folioCrossing, _objectNavigationCurrent)
        If target Is Nothing Then
            Return True
        End If

        ' Determinar fechas de inicio y fin
        Dim startDate As DateTime? = target.InitialDate
        If Not startDate.HasValue Then
            startDate = target.AdmissionDate
        End If
        If Not startDate.HasValue AndAlso _admissionXpo IsNot Nothing Then
            startDate = _admissionXpo.IFECHAING
        End If

        Dim endDate As DateTime? = target.OutputDate
        If Not endDate.HasValue AndAlso dteDatimeEngress IsNot Nothing AndAlso dteDatimeEngress.EditValue IsNot Nothing Then
            Dim tmpDate As DateTime
            If DateTime.TryParse(dteDatimeEngress.EditValue.ToString(), tmpDate) Then
                endDate = tmpDate
            End If
        End If
        If Not endDate.HasValue Then
            endDate = target.EgressDate
        End If

        ' Si no hay rango definido aún, no se valida
        If Not startDate.HasValue OrElse Not endDate.HasValue Then
            Return True
        End If

        ' Obtener los servicios del folio
        Dim ctr = TryCast(target.Folio, CtrFolio)
        If ctr Is Nothing OrElse ctr.GdcServices Is Nothing OrElse ctr.GdcServices.DataSource Is Nothing Then
            Return True
        End If

        Dim services = TryCast(ctr.GdcServices.DataSource, IEnumerable(Of IFolioDetail))
        If services Is Nothing Then
            Return True
        End If

        Dim outOfRange = services.
            Where(Function(s) s IsNot Nothing AndAlso
                               (s.ServiceDate < startDate.Value OrElse s.ServiceDate > endDate.Value)).
            ToList()

        If outOfRange.Any() Then
            Dim sb As New StringBuilder()
            For Each svc In outOfRange
                Dim cupsCode = If(String.IsNullOrWhiteSpace(svc.CUPS), "N/D", svc.CUPS.Trim())
                Dim cupsDesc = If(String.IsNullOrWhiteSpace(svc.ServiceName), "Servicio sin nombre", svc.ServiceName.Trim())
                Dim orderCode = If(String.IsNullOrWhiteSpace(svc.ServiceOrderCode), "N/D", svc.ServiceOrderCode.Trim())
                sb.AppendLine($"Existen ítems con fecha por fuera del periodo de atención: {cupsCode} – {cupsDesc} con Orden {orderCode} {svc.ServiceDate:dd-MM-yyyy}.")
            Next
            ShowMessage(EeventViewerImages.Advertencia) = sb.ToString()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' funcion para establecer el formato de la moneda seleccionada
    ''' </summary>
    ''' <param name="abbreviation"></param>
    Private Sub SetCurrencyFormatUI(abbreviation As String)
        If String.IsNullOrEmpty(abbreviation) Then
            Exit Sub
        End If
        Me._currencyAbbreviation = abbreviation
        Dim _culture = Me.GetCultuteInfo(abbreviation)
        Me.INDlblEntityDiscount.Properties.Mask.Culture = _culture
        Me.INDlblPatientDiscount.Properties.Mask.Culture = _culture
        Me.INDlblValuePortfolioAdvance.Properties.Mask.Culture = _culture
        Me.INDlblTaxDevolution.Properties.Mask.Culture = _culture
        Me.INDteBalance.Properties.Mask.Culture = _culture
        Me.INDspnCossingValue.Properties.Mask.Culture = _culture

        ColCrossValue = Window.Utils.FormatGrid(ColCrossValue, abbreviation)
        ColBalance = Window.Utils.FormatGrid(ColBalance, abbreviation)
        ColValue = Window.Utils.FormatGrid(ColValue, abbreviation)
    End Sub

    ''' <summary>
    ''' funcion que consulta y establece la posible devolucion del IVA
    ''' </summary>
    ''' <param name="portfolioAdvanced"></param>
    Private Sub SetTaxDevolutionValue(portfolioAdvanced As PortfolioRepository.Portfolio_PortfolioAdvance)
        Try
            If portfolioAdvanced Is Nothing OrElse
            portfolioAdvanced?.CashReceiptId?.TreasuryPaymentMethodsXpo Is Nothing OrElse
            portfolioAdvanced?.CashReceiptId?.TreasuryPaymentMethodsXpo?.Count > 1 OrElse
            _objectNavigationCurrent?.RevenueControlDetailId Is Nothing Then
                Me.TaxDevolutionValue = 0
                Exit Sub
            End If

            If ListTaxDevolution Is Nothing OrElse Not ListTaxDevolution?.Any() Then
                Me.TaxDevolutionValue = 0
                Exit Sub
            End If

            If ListTaxDevolution?.Any(Function(x) x.CurrencyId <> Me.CurrencyTypeId) Then
                Me.TaxDevolutionValue = 0
                Throw New Exception("La moneda de la lista de devolución de iva no es la misma a la seleccionada")
            End If

            Dim PaymentMethodType = portfolioAdvanced.CashReceiptId.TreasuryPaymentMethodsXpo.FirstOrDefault.PaymentMethodTypes

            If ListPortfolioAdvance?.All(Function(x) x.PaymentMethodsType = PaymentMethodType And PaymentMethodType <> 0) OrElse ((ListPortfolioAdvance Is Nothing OrElse Not ListPortfolioAdvance?.Any()) AndAlso PaymentMethodType <> 0) Then
                Using Model As New MLiquidation()

                    Dim Result = ListTaxDevolution.Find(Function(x) x.RevenueControlDetailId = _objectNavigationCurrent.RevenueControlDetailId _
                                                            AndAlso x.CurrencyId = CurrencyTypeId AndAlso x.PaymentMethodType = PaymentMethodType)

                    If Result Is Nothing Then
                        Me.TaxDevolutionValue = 0
                        Exit Sub
                    End If

                    Me.TaxDevolutionValue = Result.TaxValue
                End Using
            Else
                Me.TaxDevolutionValue = 0
            End If

        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = ex.Message
        Finally
            CrossingTotalRefresh()
        End Try
    End Sub

    ''' <summary>
    ''' funcion que valida si se aplica al monto final el Iva devuelto previamente calculado
    ''' si el iva devuelto es cero es porque no aplica
    ''' si la lista de anticipos esta vacia o nulla No aplica
    ''' si el total a pagar menos el iva devuelto es diferente a la suma de los anticipos y estos todos estan con el mismo metodo de pago
    ''' entonces No aplica
    ''' </summary>
    Private Sub ValidationTaxDevolution()
        Try
            If Me.TaxDevolutionValue = 0 Then
                Exit Sub
            End If

            If ListPortfolioAdvance Is Nothing OrElse Not ListPortfolioAdvance.Any() Then
                Me.TaxDevolutionValue = 0
                Exit Sub
            End If

            If Math.Round((TotalValueToPatient - TotalDiscountToPatient - Me.TaxDevolutionValue), 2, MidpointRounding.AwayFromZero) <> ListPortfolioAdvance.Sum(Function(x) x.CrossingValue) _
                AndAlso ListPortfolioAdvance.All(Function(d) d.PaymentMethodsType = ListPortfolioAdvance.FirstOrDefault.PaymentMethodsType) Then
                Me.TaxDevolutionValue = 0
            End If

        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = ex.Message
        Finally
            CrossingTotalRefresh()
        End Try
    End Sub

    ''' <summary>
    ''' limpia los controles del popup de anticipos cuando se agrega o cuando se cambia de moneda
    ''' </summary>
    Private Sub CleanAdvancedControl()
        INDsleAdvance.EditValue = Nothing
        IsPortfolioAdvanceByThirdPartyBeneficiary = False
        BalanceAdvance = 0
        CrossingValue = 0
        INDlciTRM.HideControl()
        Me.INDlciBalanceAdvance.Text = $"Saldo"
    End Sub

    ''' <summary>
    ''' Carga una lista global del TRM
    ''' </summary>
    ''' <param name="fromCurrency">Moneda de la transacción</param>
    ''' <param name="toCurrency">Moneda a convertir</param>
    Private Async Function loadListTRM(toCurrency As Integer?, fromCurrency As Integer?) As Task
        Me._listTRM = If(Me._listTRM Is Nothing, New List(Of TRM), Me._listTRM)
        'Se busca que el TRM de la moneda a convertir no este en la lista
        If Not Me._listTRM?.Any(Function(a) a.CurrencyId = toCurrency AndAlso a.OfficialCurrencyId = fromCurrency AndAlso a.MeasurementDate = GetDateServer().Date) Then
            Using model As New MPortfolioTransfers(Me.Tag)
                Dim result = Await model.GetTRMbyCurrencyId(toCurrency, fromCurrency, Nothing, NameOf(Invoice))
                If Not result?.StateResult Then
                    ShowMessage(EeventViewerImages.Advertencia) = result?.Message
                    Exit Function
                End If
                Me._listTRM.Add(result?.ObjectEmbbeded)
            End Using
        End If
    End Function

    Private Function GetCultuteInfo(CurrencyAbbreviation As String) As CultureInfo
        Dim culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        Return culture
    End Function

    ''' <summary>
    ''' funcion que se encarga de establecer los valores correspondientes a liquidar en la moneda escogida dentro del popup
    ''' </summary>
    ''' <param name="invoicePartial"></param>
    ''' <param name="itsFirstTime"></param>
    Private Sub CurrencyVisualitationValues(invoicePartial As InvoicePartialMasterAccount, itsFirstTime As Boolean, Optional folioCrossing As ObjectFolioCrossing = Nothing)
        'valido si el objeto con los valores para establecer en el popup llegan con datos
        If invoicePartial Is Nothing Then
            ShowMessage(EeventViewerImages.Advertencia) = "No se encontrarón detalles para establecer los nuevos valores"
            Exit Sub
        End If

        'valido que si el TRM es igual a 0 es porque hay un error en la consulta
        If invoicePartial.TRMValue = 0 Then
            CurrencyTypeId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
            ShowMessage(EeventViewerImages.Advertencia) = "No existe un TRM para la fecha de hoy con la moneda seleccionada"
            Exit Sub
        End If

        SetCurrencyFormatUI(invoicePartial.CurrencyAbbreviation)
        'establece la lista posible de devolucion de iva
        Me.ListTaxDevolution = invoicePartial?.TaxDevolution
        Me._tRMInvoicePartial = invoicePartial.TRMValue

        'si el trm es diferente de uno  y diferente a la moneda oficial entonces pongo visible el trm
        If invoicePartial.TRMValue <> 1 AndAlso CurrencyTypeId <> indigo.OfficialCurrencyId Then
            INDlciCurrency.Text = $"Moneda - TRM: {Utils.VisibleTRM(invoicePartial.TRMValue)} "
        Else
            INDlciCurrency.Text = $"Moneda"
        End If

        Me.CleanAdvancedControl()
        INDgcPortfolioAdvance.RefreshDataSource()
        Dim _culture = Me.GetCultuteInfo(Me.CurrencyAbbreviation)

        _flagRefreshValues = False

        'si No liquida cuenta madre y es primera vez y ademas el obj de folio corssin tiene datos se ejecuta de la forma antigua
        If Not Me._settingLiquidateMasterAccount AndAlso itsFirstTime AndAlso folioCrossing IsNot Nothing Then

            TotalEntity = folioCrossing.Folio.ThirdPartySalesPrice
            If folioCrossing.Folio.VoucherValue <> 0 Then
                TotalPatient = folioCrossing.Folio.VoucherValue
            Else
                TotalPatient = folioCrossing.Folio.TotalPatientWithDiscount
            End If

            PatientDiscount = folioCrossing.Folio.PatientDiscount

            If (CareGroupParent.CareGroupType <> eCareGroupType.Particulares) Then
                EntityDiscount = folioCrossing.Folio.GrandTotalDiscount
            End If

            TotalDiscountToPatient = folioCrossing.TotalPatientDiscount

            ' si no se cumple lo anterior solo  se valida no liquida cuenta madre para que tenga encuenta los valore recalculados por la API
        ElseIf Not Me._settingLiquidateMasterAccount Then
            TotalEntity = invoicePartial.ThirdPartySalesValue
            TotalPatient = invoicePartial.TotalPatientWithDiscount
            PatientDiscount = invoicePartial.PatientDiscount

            If (CareGroupParent.CareGroupType <> eCareGroupType.Particulares) Then
                EntityDiscount = invoicePartial.GrandTotalDiscount
            End If

            'de los contrario es por que liquida cuenta madre y en este flujo debe tomar los valores recalculados por la API
        Else
            Me.INDliTotalPatient.HideControl()
            Me.INDliPatientDiscount.HideControl()
            Me.TaxDevolutionValue = 0

            If invoicePartial.IsMasterAccount = 2 OrElse (CareGroupParent.CareGroupType <> eCareGroupType.Particulares AndAlso invoicePartial.IsMasterAccount = 0) Then
                INDliEntityTotal.Text = ResourceManager.GetString("TotalEntity", GetType(CtrFolio).Name)
                INDliEntityDiscount.Text = "Dcto Entidad"
                Me.TotalEntity = invoicePartial.GrandTotalSalesPrice
            ElseIf invoicePartial.IsMasterAccount = 4 OrElse (CareGroupParent.CareGroupType = eCareGroupType.Particulares AndAlso invoicePartial.IsMasterAccount = 0) Then
                INDliEntityTotal.Text = ResourceManager.GetString("TotalPatient", GetType(CtrFolio).Name)
                INDliEntityDiscount.Text = "Dcto Paciente"
                Me.TotalEntity = invoicePartial.GrandTotalSalesPrice + invoicePartial.GrandTotalDiscount
            End If
            Me.EntityDiscount = invoicePartial.GrandTotalDiscount
        End If
        _flagRefreshValues = True
        If Not itsFirstTime Then
            CrossingTotalRefresh()
        End If
    End Sub

    ''' <summary>
    ''' funcion que retorna los valores de la prefactura recalculada en la moneda escogida o por defecto la oficial
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetInvoicePartial(revenueControlDetailId As Integer?, Optional currencyId As Integer? = Nothing) As Task(Of InvoicePartialMasterAccount)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If revenueControlDetailId Is Nothing Then
            ShowMessage(EeventViewerImages.Advertencia) = "El id del folio esta llegando vacío"
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Return Nothing
        End If

        Using Model As New MLiquidation
            Me.Enabled = False
            Dim result = Await Model.GetVReportInvoicePartial(revenueControlDetailId, currencyId)
            Me.Enabled = True

            If result Is Nothing OrElse Not result?.StateResult Then
                ShowMessage(EeventViewerImages.Advertencia) = result?.Message
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Return Nothing
            End If

            Me.Cursor = System.Windows.Forms.Cursors.Default
            Return result.ObjectEmbbeded
        End Using
    End Function

#End Region

#Region "Enum"
    Public Enum eCareGroupType
        EAPBConcontrato = 1
        EAPBSinContrato = 2
        Particulares = 3
        Aseguradoras = 4
    End Enum
#End Region

#Region "BarButton"
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.ToolBar.Visible = False
    End Sub

#End Region

End Class

#Region "ObjectList"
Public Class ObjectFolioCrossing
    Public Property Name As String
    Public Property RevenueControlDetailId As Integer
    Public Property Folio As ICtrFolio
    Public Property ListPortfolioAdvance As List(Of PortfolioAdvance)
    Public Property TotalPatientDiscount As Decimal
    Public Property IsCutAccount As Boolean
    Public Property OutputDiagnosis As String
    Public Property InitialDate As DateTime?
    Public Property OutputDate As DateTime?
    Public Property CutType As Integer
    Public Property AdmissionDate As DateTime?
    Public Property EgressDate As DateTime?
    Public Property CutDate As DateTime?
    Public Property hasExit As Boolean
    Public Property CurrencyId As Integer?
    Public Property TRMValue As Decimal?
    Public Property TaxDevolutionValue As Decimal
    Public Property ConditionSalesId As Integer?
    Public Property EconomicActivityId As Integer?
    Public Property ThirdPartyId As Integer
    Property ApplyLogicThirdPartyBeneficiary As Boolean
    Property ObligateAddPortfolioAdvance As Boolean
    Property IsElectronicBillerThirdParty As Boolean
End Class

''' <summary>
''' Clase para representar las opciones de fecha de egreso en el selector
''' </summary>
Public Class EgressDateOption
    ''' <summary>
    ''' Fecha de egreso del ingreso
    ''' </summary>
    Public Property EgressDate As DateTime

    ''' <summary>
    ''' Número de ingreso
    ''' </summary>
    Public Property AdmissionNumber As String

    ''' <summary>
    ''' Estado del ingreso
    ''' </summary>
    Public Property AdmissionStatus As String

    ''' <summary>
    ''' Texto que se mostrará en el ComboBox
    ''' </summary>
    Public ReadOnly Property DisplayText As String
        Get
            Return $"{EgressDate:dd/MM/yyyy HH:mm} - {AdmissionNumber} - {AdmissionStatus}"
        End Get
    End Property
End Class
#End Region
'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 03-03-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Data.Linq
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Contract.MVP
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.ContractRepository
Imports System.Threading
Imports System.Text
#End Region

Public Class FrmLiquidationData

    Implements ILiquidateData, ICustomizableForm

#Region "Builders"
    Public Sub New(AdmissionNumber As String, CareGroupId As Integer)
        InitializeComponent()
        _admissionNumber = AdmissionNumber
        _careGroupId = CareGroupId

        ' Add any initialization after the InitializeComponent() call.
        AddHandler bw.DoWork, AddressOf bw_DoWork
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
    End Sub
#End Region

#Region "Const"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Billing"
#End Region

#Region "Variables"


    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _admissionNumber As String

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _careGroupId As Integer

    'Private _billingJustificationControl As JustificationControl
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private Presenter As PLiquidationData

    ''' <summary>
    ''' The model
    ''' </summary>
    Dim model As MLiquidationData

    ''' <summary>
    ''' Representa la entidad de autorización de facturacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _liquidationData As LiquidationData

    ''' <summary>
    ''' Representa la entidad de autorización de facturacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _careGroupData As ActionResult(Of CareGroup)

    ''' <summary>
    ''' Representa la entidad de autorización de facturacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _discountContractedCustomer As Double

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling

    ''' <summary>
    ''' Identifica si se esta cargando el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private isLoading As Boolean = False

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Asyncrono
    ''' </summary>
    ''' <remarks></remarks>
    Private bw As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Id del servicio ips que sirve para poder consultar los items qx que tiene asociado, 
    ''' este se saca del item seleccionado del combo servicio ips(Solo se puede seleccionar uno con presentación qx)
    ''' </summary>
    Private IPSServiceId As Integer

    ''' <summary>
    ''' Listado para el datasource de los controles de tipo de regla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListXpCollection As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Obtiene o establece el nombre de la vista de la rejilla que esta con focus
    ''' </summary>
    ''' <remarks></remarks>
    Dim viewNameFocus As String

    ''' <summary>
    ''' entidad de los detalles de los datos de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim liquidationDataDetail As LiquidationDataDetail

    ''' <summary>
    ''' lista de los datos de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listLiquidationDataDetail As List(Of LiquidationDataDetail)

    ''' <summary>
    ''' lista de los datos de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listLiquidationDataDetailDelete As List(Of LiquidationDataDetail)

    ''' <summary>
    ''' bandera para saber si se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim editPopup As Boolean

    ''' <summary>
    ''' Lista que obtiene o estalece los id y nombre de cada item seleccionado
    ''' </summary>
    Private ListConditionsItems As List(Of Tuple(Of Integer, String)) = New List(Of Tuple(Of Integer, String))()

    ''' <summary>
    ''' variable que obtiene o establece los Id de los items seleccionados en una sola variable
    ''' </summary>
    Private convConditionsItemsIds As String

    ''' <summary>
    ''' variable que obtiene o establece los nombres de los items seleccionados en una sola variable
    ''' </summary>
    Private convConditionsItemsName As String

    ''' <summary>
    ''' 
    ''' </summary>
    Private listCupsGroup As List(Of CupsGroup)

    ''' <summary>
    ''' 
    ''' </summary>
    Private listCupsSubgroup As List(Of CupsSubgroup)

    ''' <summary>
    ''' 
    ''' </summary>
    Private listCupsEntity As List(Of CUPSEntity)

#End Region

#Region "Events"
    Public Event RecalculateMasterAccountFolios(sender As Object, e As EventArgs)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ILiquidateData.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Private WriteOnly Property ActionsOnControls As Boolean Implements ILiquidateData.ActionsOnControls
        Set(value As Boolean)
            LcRoot.BeginUpdate()
            LcRoot.EndUpdate()
        End Set
    End Property

    Private ReadOnly Property MyTag As String Implements ILiquidateData.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Private Property Sequense As BillingSequence Implements ILiquidateData.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene Numero de ingreso del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property AdmissionNumber As String Implements ILiquidateData.AdmissionNumber
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    ''' <summary>
    ''' obtiene o asigana si maneja descuento o no
    ''' </summary>
    ''' <returns></returns>
    Public Property ApplyDiscount As Double Implements ILiquidateData.ApplyDiscount
        Get
            Return INDSleApplyDiscount.EditValue
        End Get
        Set(value As Double)
            INDSleApplyDiscount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor o porcentaje de descuento
    ''' </summary>
    ''' <returns></returns>
    Public Property DiscountValueorPercentage As Double Implements ILiquidateData.DiscountValueorPercentage
        Get
            If INDSleApplyDiscount.EditValue = 1 Or INDSleApplyDiscount.EditValue = 3 Then
                Return INDSePercentDiscount.EditValue
            Else
                Return INDTxtValueDiscount.EditValue
            End If
        End Get
        Set(value As Double)
            If INDSleApplyDiscount.EditValue = 1 Or INDSleApplyDiscount.EditValue = 3 Then
                INDSePercentDiscount.EditValue = value
            Else
                INDTxtValueDiscount.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del deductible
    ''' </summary>
    ''' <returns></returns>
    Public Property DeductibleValue As Double Implements ILiquidateData.DeductibleValue
        Get
            Return INDTxtValueDeductible.EditValue
        End Get
        Set(value As Double)
            INDTxtValueDeductible.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si aplica deducible
    ''' </summary>
    ''' <returns></returns>
    Public Property ApplyDeductible As Boolean Implements ILiquidateData.ApplyDeductible
        Get
            Return INDSleApplyDeductible.EditValue
        End Get
        Set(value As Boolean)
            INDSleApplyDeductible.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del copago
    ''' </summary>
    ''' <returns></returns>
    Public Property CopaymentValue As Double Implements ILiquidateData.CopaymentValue
        Get
            Return INDTxtCopaymentValue.EditValue
        End Get
        Set(value As Double)
            INDTxtCopaymentValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de coaseguro de la aseguradora
    ''' </summary>
    ''' <returns></returns>
    Public Property InsuranceCoinsurance As Double Implements ILiquidateData.InsuranceCoinsurance
        Get
            Return INDSeInsuranceCoinsurance.EditValue
        End Get
        Set(value As Double)
            INDSeInsuranceCoinsurance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del coaseguro del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientCoinsurance As Double Implements ILiquidateData.PatientCoinsurance
        Get
            Return INDSePatientCoinsurance.EditValue
        End Get
        Set(value As Double)
            INDSePatientCoinsurance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si aplica condicion general
    ''' </summary>
    ''' <returns></returns>
    Public Property ApplyGeneralLimits As Boolean Implements ILiquidateData.ApplyGeneralLimits
        Get
            Return INDSleApplyGeneralLimits.EditValue
        End Get
        Set(value As Boolean)
            INDSleApplyGeneralLimits.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor limite incluye iba
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxIncludeGeneral As Boolean Implements ILiquidateData.TaxInclude
        Get
            Return INDSleTaxIncludeGeneral.EditValue
        End Get
        Set(value As Boolean)
            INDSleTaxIncludeGeneral.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del limite general
    ''' </summary>
    ''' <returns></returns>
    Public Property LimitValueGeneral As Decimal Implements ILiquidateData.LimitValue
        Get
            Return INDTxtLimitValueGeneral.EditValue
        End Get
        Set(value As Decimal)
            INDTxtLimitValueGeneral.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de filtro
    ''' </summary>
    ''' <returns></returns>
    Public Property ConditionType As Integer?
        Get
            Return INDsleConditionType.EditValue
        End Get
        Set(value As Integer?)
            INDsleConditionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los id de los items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Public Property ConditionsItemsIds As String
        Get
            Return INDpceConditionsItemsIds.EditValue
        End Get
        Set(value As String)
            INDpceConditionsItemsIds.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de limite
    ''' </summary>
    ''' <returns></returns>
    Public Property LimitType As Integer
        Get
            Return INDSleLimitType.EditValue
        End Get
        Set(value As Integer)
            INDSleLimitType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cantidad 
    ''' </summary>
    ''' <returns></returns>
    Public Property Quantity As Integer
        Get
            Return INDTxtQuantity.EditValue
        End Get
        Set(value As Integer)
            INDTxtQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el valor limite inlcuye iva
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxInclude As Boolean
        Get
            Return INDSleTaxInclude.EditValue
        End Get
        Set(value As Boolean)
            INDSleTaxInclude.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor limite
    ''' </summary>
    ''' <returns></returns>
    Public Property LimitValue As Decimal
        Get
            Return INDTxtLimitValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtLimitValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor cubierto aseguradora
    ''' </summary>
    ''' <returns></returns>
    Public Property InsurerCoveredValue As Decimal Implements ILiquidateData.InsurerCoveredValue
        Get
            Return INDTxtInsurerCoveredValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtInsurerCoveredValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor limite coaseguro paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientCoinsuranceLimitValue As Decimal Implements ILiquidateData.PatientCoinsuranceLimitValue
        Get
            Return INDTxtPatientCoinsuranceLimitValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtPatientCoinsuranceLimitValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' valor cubierto aseguradora - especifico
    ''' </summary>
    ''' <returns></returns>
    Property EspecificInsurerCoveredValue As Decimal Implements ILiquidateData.EspecificInsurerCoveredValue
        Get
            Return INDTxtLimitCovered.EditValue
        End Get
        Set(value As Decimal)
            INDTxtLimitCovered.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor del coaseguro por detalle de la aseguradora
    ''' </summary>
    ''' <returns></returns>
    Property InsuranceCoinsuranceDetail As Decimal Implements ILiquidateData.InsuranceCoinsuranceDetail
        Get
            Return INDSleInsuranceCoinsuranceDetail.EditValue
        End Get
        Set(value As Decimal)
            INDSleInsuranceCoinsuranceDetail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor del coaseguro por detalle del paciente
    ''' </summary>
    ''' <returns></returns>
    Property PatientCoinsuranceDetail As Decimal Implements ILiquidateData.PatientCoinsuranceDetail
        Get
            Return INDSlePatientCoinsuranceDetail.EditValue
        End Get
        Set(value As Decimal)
            INDSlePatientCoinsuranceDetail.EditValue = value
        End Set
    End Property

#End Region

#Region "Handlers"

#Region "Load"
    ''' <summary>
    ''' Handles the Load event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmLiquidationData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'me.layoutcontrols.setiscustomizable(me.indlcbillinggroup, true)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PLiquidationData(Me)
        Presenter.GetSequense()
        bw.WorkerSupportsCancellation = True
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvSpecificCondition, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvSpecificCondition.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        IndigoGridControl1.RefreshGrid(INDGcLiquidationDataDetail)

        InitializeTuples()
        CleanControls()

        INDLciPercentDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciValueDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        model = New MLiquidationData(MyTag)
        Await LoadControls()
    End Sub
#End Region

#Region "EditValueChanged"

    Private Async Sub INDSleApplyDiscount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleApplyDiscount.EditValueChanged

        Select Case ApplyDiscount
            Case 1
                INDLciValueDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSePercentDiscount.ReadOnly = False
                INDLciPercentDiscount.Text = "% Descuento"
            Case 2
                INDLciPercentDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValueDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case 3
                INDLciValueDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDLciPercentDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciPercentDiscount.Text = "% Contratado"
                INDSePercentDiscount.ReadOnly = True

                Await GetDataCareGroup()
                INDSePercentDiscount.Value = _discountContractedCustomer

            Case Else
                INDLciPercentDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValueDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End Select
    End Sub

    ''' <summary>
    ''' metodo que se activa cuando el tipo de condicion cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleConditionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConditionType.EditValueChanged
        If ConditionType IsNot Nothing Then
            IPSServiceId = Nothing
            INDgcSurgicalProcedures.DataSource = Nothing
            Me.Cursor = ChangeCursorIndigo()
            HideColumnsOfGridControlsConditionType()
            If bw.IsBusy Then
                Exit Sub
            End If
            bw.RunWorkerAsync()
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    ''' <summary>
    ''' mostrar o no el campo de cantidad dependiendo del tipo de limite
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleLimitType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleLimitType.EditValueChanged
        INDLciQuantity.Visibility = IIf(LimitType = 1, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciLimitCovered.HideControl(LimitType <> 2)
        INDLciTaxInclude.HideControl(LimitType = 3)
        INDLciLimitValue.HideControl(LimitType = 3)
        INDLciInsurerCoInsurerDetail.HideControl(LimitType <> 3)
        INDLciPatientCoinsuranceDetail.HideControl(LimitType <> 3)
        If LimitType = 3 Then
            Me.LimitValue = 0
            Me.EspecificInsurerCoveredValue = 0
            Me.InsuranceCoinsuranceDetail = 0
            Me.PatientCoinsuranceDetail = 100
        Else
            Me.InsuranceCoinsuranceDetail = 0
            Me.PatientCoinsuranceDetail = 0
        End If
    End Sub

#End Region

#Region "EditValueChanging "
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckControlsLiquidationType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckControlsLiquidationType.EditValueChanging

        ''Se obtiene la informacion del item seleccionado
        Dim item = viewControlsRuleType.GetFocusedRow()

        If ListXpCollection Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        If e.NewValue Then

            item.SelectOption = e.NewValue
            'Se obtiene cuantos items han sido seleccionados
            Dim cont = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
            ConditionsItemsIds = cont.ToString + " item seleccionado"

            'Dependiendo de la condicion se obtienen el id y el nombre del item para guardarlos y mostrarlos en el grid
            Select Case ConditionType
                Case 1, 2, 4
                    ListConditionsItems.Add(New Tuple(Of Integer, String)(item.Id, item.Name))
                Case 3
                    ListConditionsItems.Add(New Tuple(Of Integer, String)(item.Id, item.Description))
            End Select

            If cont = ListXpCollection.Count Then
                Me.INDcolSelectionOption.Image = Global.Presentation.Billing.My.Resources.Resources.check
            Else
                Me.INDcolSelectionOption.Image = Global.Presentation.Billing.My.Resources.Resources.undcheck
            End If

        ElseIf e.OldValue Then
            'cuando estoy deschekeando un item se elimina de la lista 
            Select Case ConditionType
                Case 1, 2, 4
                    ListConditionsItems.Remove(New Tuple(Of Integer, String)(item.Id, item.Name))
                Case 3
                    ListConditionsItems.Remove(New Tuple(Of Integer, String)(item.Id, item.Description))
            End Select

            ConditionsItemsIds = ListConditionsItems.Count.ToString + " item seleccionado"
        End If
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Handles the Activated event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmLiquidationData_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDSleApplyDiscount.Enabled Then
            INDSleApplyDiscount.Focus()
        End If
    End Sub
#End Region

#Region "Disposed"
    ''' <summary>
    ''' Handles the Disposed event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmLiquidationData_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        'model.Dispose()
        model = Nothing
        _sequence = Nothing
        Presenter = Nothing
        _idOperativeUnit = Nothing
        _liquidationData = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        editPopup = Nothing
    End Sub
#End Region

#Region "FormClosing"

    Private Sub FrmLiquidationData_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' funcion que reinicia el popup de condicion especifica cuando se cierra
    ''' </summary>
    Private Sub INDPcePortfolioAge_prueba() Handles INDPcePortfolioAge.Closed
        If editPopup Then
            INDBtnAd.Text = ResourceManager.GetString("Add")
            CleanControlsPopup()
        End If
    End Sub


#End Region

#Region "Click"
    ''' <summary>
    ''' Metodo que agrega el item seleccionado en el popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDBtnAd_Click(sender As Object, e As EventArgs) Handles INDBtnAd.Click

        Dim listIdsConditionsItems As New List(Of Integer)
        Dim listNamesConditionsItems As New List(Of String)

        If ValidatePopupData() Then
            Exit Sub
        End If

        For Each tupla As Tuple(Of Integer, String) In ListConditionsItems
            listIdsConditionsItems.Add(tupla.Item1)
            listNamesConditionsItems.Add(tupla.Item2)
        Next

        convConditionsItemsIds = String.Join(",", listIdsConditionsItems)
        convConditionsItemsName = String.Join(",", listNamesConditionsItems)

        If editPopup = False Then
            liquidationDataDetail = New LiquidationDataDetail
        End If

        With liquidationDataDetail
            .ConditionType = ConditionType
            .ConditionsItemsIds = convConditionsItemsIds
            .LimitType = LimitType
            .Quantity = Quantity
            .TaxInclude = TaxInclude
            .LimitValue = LimitValue
            .InsurerCoveredValue = Me.EspecificInsurerCoveredValue
            .InsuranceCoinsurance = Me.InsuranceCoinsuranceDetail
            .PatientCoinsurance = Me.PatientCoinsuranceDetail

            Dim x = INDsleConditionType.GetSelectedDataRow()
            .NameCondition = DirectCast(x, System.Tuple(Of Integer, String)).Item2
            .NameConditionsItems = convConditionsItemsName
        End With
        If listLiquidationDataDetail Is Nothing Then
            listLiquidationDataDetail = New List(Of LiquidationDataDetail)
        End If
        If editPopup = False Then
            listLiquidationDataDetail.Add(liquidationDataDetail)
        End If

        INDGcLiquidationDataDetail.DataSource = Nothing
        INDGcLiquidationDataDetail.DataSource = listLiquidationDataDetail

        CleanControlsPopup()
    End Sub
#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        liquidationDataDetail = DirectCast(INDGvSpecificCondition.GetFocusedRow(), LiquidationDataDetail)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail(liquidationDataDetail)
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        liquidationDataDetail = DirectCast(INDGvSpecificCondition.GetFocusedRow(), LiquidationDataDetail)
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail(liquidationDataDetail)
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' funcion que agrega los items agregador de cada tipo
    ''' </summary>
    Private Sub AddConditionsItems()
        Dim lista = liquidationDataDetail.ConditionsItemsIds.Split(","c).ToList
        'GetDataItemsXpo(liquidationDataDetail?.ConditionType)
        For Each element In lista
            Dim help = (From l In ListXpCollection Where l.Id = CInt(element) Select l).FirstOrDefault
            help.SelectOption = True

            Select Case liquidationDataDetail?.ConditionType
                Case 1, 2, 4
                    ListConditionsItems.Add(New Tuple(Of Integer, String)(help.Id, help.Name))
                Case 3
                    ListConditionsItems.Add(New Tuple(Of Integer, String)(help.Id, help.Description))
            End Select
        Next
    End Sub

    '''' <summary>
    '''' Editar el detalle
    '''' </summary>
    '''' <remarks></remarks>
    Private Async Sub EditDetail(liquidationDataDetail As LiquidationDataDetail)
        editPopup = True

        'se consulta el listado de los items segun la condicion
        GetDataItemsXpo(liquidationDataDetail?.ConditionType)

        'cantidad de items chequeados
        Dim amount = liquidationDataDetail?.ConditionsItemsIds.Split(","c).Count.ToString

        With liquidationDataDetail
            ConditionType = .ConditionType
            ConditionsItemsIds = amount + " item seleccionado"
            LimitType = .LimitType
            Quantity = .Quantity
            TaxInclude = .TaxInclude
            LimitValue = .LimitValue
            Me.EspecificInsurerCoveredValue = .InsurerCoveredValue
            Me.InsuranceCoinsuranceDetail = .InsuranceCoinsurance
            Me.PatientCoinsuranceDetail = .PatientCoinsurance
        End With

        INDBtnAd.Text = ResourceManager.GetString("Edit")
        INDPcePortfolioAge.Focus()
        INDPcePortfolioAge.ShowPopup()
        INDsleConditionType.Focus()
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If liquidationDataDetail.Id > 0 Then
                If listLiquidationDataDetailDelete Is Nothing Then
                    listLiquidationDataDetailDelete = New List(Of LiquidationDataDetail)
                End If
                listLiquidationDataDetailDelete.Add(liquidationDataDetail)
            End If
            Dim index = listLiquidationDataDetail.IndexOf(liquidationDataDetail)
            listLiquidationDataDetail.Remove(liquidationDataDetail)
            liquidationDataDetail = Nothing

            INDGcLiquidationDataDetail.DataSource = Nothing
            INDGcLiquidationDataDetail.DataSource = listLiquidationDataDetail
            CleanControlsPopup()
        End If
    End Sub
#End Region

#Region "ValueChanged"
    Private Sub INDSeInsuranceCoinsurance_ValueChanged(sender As Object, e As EventArgs) Handles INDSeInsuranceCoinsurance.ValueChanged
        PatientCoinsurance = 100 - InsuranceCoinsurance
    End Sub

    Private Sub INDSeInsuranceCoinsuranceDetail_ValueChanged(sender As Object, e As EventArgs) Handles INDSleInsuranceCoinsuranceDetail.ValueChanged
        Me.PatientCoinsuranceDetail = 100 - Me.InsuranceCoinsuranceDetail
    End Sub
#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de repositorio para ver los conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDrepPceConcepts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceConcepts.QueryPopUp

        Dim lista = INDGvSpecificCondition.DataSource(0).ConditionsItemsIds.Split(","c)

        Select Case INDGvSpecificCondition.DataSource(0).ConditionType
            Case 1 'cups group
                listCupsGroup = New List(Of CupsGroup)
                For Each item In lista
                    Using Model As New MCupsGroup(CStr(Me.Tag))
                        Dim res = Await Model.GetCupsGroupById(CInt(item))
                        listCupsGroup.Add(res.ObjectEmbbeded)
                    End Using
                Next
                If listCupsGroup.Count > 0 Then
                    INDgcDetailSpecificRule.DataSource = Nothing
                    INDgcDetailSpecificRule.DataSource = listCupsGroup.ToList
                End If
            Case 2 'cupssubgroup
                listCupsSubgroup = New List(Of CupsSubgroup)
                For Each item In lista
                    Using Model As New MCupsSubGroup(CStr(Me.Tag))
                        Dim res = Await Model.GetCupsSubGroupById(CInt(item))
                        listCupsSubgroup.Add(res.ObjectEmbbeded)
                    End Using
                Next
                If listCupsSubgroup.Count > 0 Then
                    INDgcDetailSpecificRule.DataSource = Nothing
                    INDgcDetailSpecificRule.DataSource = listCupsSubgroup.ToList
                End If
            Case 3 'cupsentitys
                listCupsEntity = New List(Of CUPSEntity)
                For Each item In lista
                    Using Model As New MCupsEntity(CStr(Me.Tag))
                        Dim res = Await Model.GetCupsEntityById(CInt(item))
                        listCupsEntity.Add(res.ObjectEmbbeded)
                    End Using
                Next
                If listCupsEntity.Count > 0 Then
                    INDgcDetailSpecificRule.DataSource = Nothing
                    INDgcDetailSpecificRule.DataSource = listCupsEntity.ToList
                End If
        End Select

    End Sub
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _liquidationData
            .AdmissionNumber = _admissionNumber
            .ApplyDiscount = ApplyDiscount
            .DiscountValueorPercentage = DiscountValueorPercentage
            .DeductibleValue = DeductibleValue
            .ApplyDeductible = ApplyDeductible
            .CopaymentValue = CopaymentValue
            .InsuranceCoinsurance = InsuranceCoinsurance
            .PatientCoinsurance = PatientCoinsurance
            .ApplyGeneralLimits = ApplyGeneralLimits
            .TaxInclude = TaxIncludeGeneral
            .LimitValue = LimitValueGeneral
            .InsurerCoveredValue = Me.InsurerCoveredValue
            .PatientCoinsuranceLimitValue = Me.PatientCoinsuranceLimitValue

            If listLiquidationDataDetail IsNot Nothing Then
                For Each itemSpecific As LiquidationDataDetail In listLiquidationDataDetail
                    '''si el item tiene id 0 es nuevo, se agrega al listado
                    If itemSpecific.Id = 0 Then
                        .LiquidationDataDetail.Add(itemSpecific)
                    End If
                Next
            End If
            If listLiquidationDataDetailDelete IsNot Nothing AndAlso listLiquidationDataDetailDelete.Count > 0 Then
                For Each item In listLiquidationDataDetailDelete
                    .LiquidationDataDetail.Remove(item.MarkAsDeleted())
                Next
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If

        End With
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()

        Dim listApplyDiscount = New List(Of Tuple(Of Integer, String))()
        listApplyDiscount.Add(New Tuple(Of Integer, String)(1, "Porcentual"))
        listApplyDiscount.Add(New Tuple(Of Integer, String)(2, "Valor"))
        listApplyDiscount.Add(New Tuple(Of Integer, String)(3, "Contratado"))
        listApplyDiscount.Add(New Tuple(Of Integer, String)(4, "Ninguno"))
        INDSleApplyDiscount.Properties.DataSource = listApplyDiscount

        Dim listApplyDeductible = New List(Of Tuple(Of Boolean, String))()
        listApplyDeductible.Add(New Tuple(Of Boolean, String)(False, "Antes de coaseguro"))
        listApplyDeductible.Add(New Tuple(Of Boolean, String)(True, "Después de coaseguro"))
        INDSleApplyDeductible.Properties.DataSource = listApplyDeductible

        Dim listConditionType = New List(Of Tuple(Of Integer, String))()
        listConditionType.Add(New Tuple(Of Integer, String)(1, "Grupo cabys"))
        listConditionType.Add(New Tuple(Of Integer, String)(2, "Subgrupo cabys"))
        listConditionType.Add(New Tuple(Of Integer, String)(3, "Catálogo de servicios"))
        listConditionType.Add(New Tuple(Of Integer, String)(4, "Productos"))
        INDsleConditionType.Properties.DataSource = listConditionType

        Dim listLimitType = New List(Of Tuple(Of Integer, String))()
        listLimitType.Add(New Tuple(Of Integer, String)(1, "Valor unitario"))
        listLimitType.Add(New Tuple(Of Integer, String)(2, "Valor total"))
        listLimitType.Add(New Tuple(Of Integer, String)(3, "Coaseguro"))
        INDSleLimitType.Properties.DataSource = listLimitType
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()

        LcRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing

        DiscountValueorPercentage = 0
        DeductibleValue = 0
        ApplyDeductible = 0
        InsuranceCoinsurance = 0
        PatientCoinsurance = 100
        CopaymentValue = 0
        Me.InsurerCoveredValue = 0
        Me.PatientCoinsuranceLimitValue = 0
        editPopup = False
        'Limpiar controles
        _liquidationData = Nothing
        INDlyItemConditionsItemsIds.HideControl()
        CleanControlsPopup()

        LcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' carga los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(ApplyDeductible) AndAlso Not String.IsNullOrWhiteSpace(ApplyDeductible) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                INDSePatientCoinsurance.ReadOnly = True
                Using Model As New MLiquidationData(CStr(Me.Tag))
                    AsyncLoader(True)

                    Dim resultOperation = Await Model.GetLiquidationDataByAdmissionNumber(_admissionNumber)
                    _liquidationData = resultOperation.ObjectEmbbeded

                    LcRoot.BeginUpdate()

                    If _liquidationData.AdmissionNumber IsNot Nothing Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_liquidationData.Id))
                            With _liquidationData
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                isLoading = True

                                'Llenar Entidad
                                ApplyDiscount = .ApplyDiscount
                                DiscountValueorPercentage = .DiscountValueorPercentage
                                DeductibleValue = .DeductibleValue
                                ApplyDeductible = .ApplyDeductible
                                InsuranceCoinsurance = .InsuranceCoinsurance
                                PatientCoinsurance = .PatientCoinsurance
                                CopaymentValue = .CopaymentValue
                                ApplyGeneralLimits = .ApplyGeneralLimits
                                TaxIncludeGeneral = .TaxInclude
                                LimitValueGeneral = .LimitValue
                                Me.PatientCoinsuranceLimitValue = .PatientCoinsuranceLimitValue
                                Me.InsurerCoveredValue = .InsurerCoveredValue
                                listLiquidationDataDetail = .LiquidationDataDetail.ToList

                                INDGcLiquidationDataDetail.DataSource = listLiquidationDataDetail

                                isLoading = False
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._liquidationData.ApplyDeductible)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _liquidationData.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        INDSleApplyDiscount.Focus()
                    End If
                    LcRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDSleApplyDiscount.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo que obtiene el descuento para el cliente
    ''' </summary>
    Private Async Function GetDataCareGroup() As Task
        Using Model As New MCareGroup(CStr(Me.Tag))
            _careGroupData = Await Model.GetCareGroupById(_careGroupId)
            _discountContractedCustomer = _careGroupData.ObjectEmbbeded.DiscountContractedCustomer
        End Using
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        ConditionType = Nothing
        ConditionsItemsIds = "0 Items"
        LimitType = Nothing
        Quantity = Nothing
        TaxInclude = Nothing
        LimitValue = Nothing
        convConditionsItemsIds = Nothing
        ListConditionsItems.Clear()
        editPopup = False
        ListXpCollection = Nothing
        liquidationDataDetail = Nothing
        Me.EspecificInsurerCoveredValue = 0
        Me.InsuranceCoinsuranceDetail = 0
    End Sub

    ''' <summary>
    ''' Muestra u oculta las columnas dependiendo del tipo de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideColumnsOfGridControlsConditionType()
        INDlyItemConditionsItemsIds.HideControl(False)
        INDlyItemConditionsItemsIds.Text = ResourceManager.GetString("ConditionType" + ConditionType.ToString, NAME_MODULE)

        INDpceConditionsItemsIds.Text = "0 item seleccionado"
        INDpceConditionsItemsIds.Properties.ReadOnly = False

        Me.INDcolSelectionOption.Image = Global.Presentation.Billing.My.Resources.Resources.undcheck
        Dim ListStringNames As New List(Of String)
        Select Case ConditionType
            Case 1 'Grupo Catalogo de servicios
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
            Case 2 'Subgrupo catalogo de servicios
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolGroup")
            Case 3 'catalogo de servicios
                ListStringNames.Add("INDcolGroupCups")
                ListStringNames.Add("INDcolSubGroupCups")
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolNameCUPS")
            Case 4 'Productos
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDColProductType")
                ListStringNames.Add("INDColProductGroup")
                ListStringNames.Add("INDColSubProductGroup")
                ListStringNames.Add("INDColProductControl")
        End Select
        FieldsGrid(ListStringNames)
    End Sub

    ''' <summary>
    ''' Funcion para chequear todos los items o deschequearlos en el campo donde se listan los items segun el tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DoubleClick(sender As Object, e As MouseEventArgs) Handles viewControlsRuleType.DoubleClick
        Dim hitPoint = Me.viewControlsRuleType.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(INDcolSelectionOption.Name) Then
                'valido si los items seleccionados sin igual a la cantidad total ,si lo estan sin se deschequean todos pero si no se chequean todos
                If (From x In ListXpCollection Where x.SelectOption = True Select x).Count = (From x In ListXpCollection Select x).Count Then
                    For Each item In INDgcControlsRuleType.DataSource
                        item.SelectOption = False
                        Select Case ConditionType
                            Case 1, 2, 4
                                ListConditionsItems.Remove(New Tuple(Of Integer, String)(item.Id, item.Name))
                            Case 3
                                ListConditionsItems.Remove(New Tuple(Of Integer, String)(item.Id, item.Description))
                        End Select
                    Next
                    INDgcControlsRuleType.RefreshDataSource()
                    INDgcControlsRuleType.Invalidate()
                    Me.INDcolSelectionOption.Image = Global.Presentation.Billing.My.Resources.Resources.undcheck
                Else
                    For Each item In INDgcControlsRuleType.DataSource
                        item.SelectOption = True
                        Select Case ConditionType
                            Case 1, 2, 4
                                ListConditionsItems.Add(New Tuple(Of Integer, String)(item.Id, item.Name))
                            Case 3
                                ListConditionsItems.Add(New Tuple(Of Integer, String)(item.Id, item.Description))
                        End Select
                    Next
                    INDgcControlsRuleType.RefreshDataSource()
                    INDgcControlsRuleType.Invalidate()

                    Me.INDcolSelectionOption.Image = Global.Presentation.Billing.My.Resources.Resources.check
                End If
                Dim cont = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
                ConditionsItemsIds = cont.ToString + " item seleccionado"
            End If
        End If

    End Sub

    ''' <summary>
    ''' Metodo que recorre las columnas de la rejilla y las coloca visible 
    ''' dependiendo del listado de colName que le envien
    ''' </summary>
    ''' <param name="ListStringNames"></param>
    ''' <remarks></remarks>
    Private Sub FieldsGrid(ByVal ListStringNames As List(Of String))
        'Asigno si la columna es visible o no dependiendo del listado que envien anteriormente
        For iColumns = 0 To viewControlsRuleType.Columns.Count - 1
            For iList = 0 To ListStringNames.Count - 1
                If viewControlsRuleType.Columns.Item(iColumns).Name = ListStringNames.Item(iList) Then
                    viewControlsRuleType.Columns.Item(iColumns).Visible = True
                    Exit For
                Else
                    viewControlsRuleType.Columns.Item(iColumns).Visible = False
                End If
            Next
        Next

        'Asigno los visibleIndex para que aparezcan en orden las columnas
        Dim cont As Integer = 0
        For iColumns = 0 To viewControlsRuleType.Columns.Count - 1
            If viewControlsRuleType.Columns.Item(iColumns).Visible = True Then
                viewControlsRuleType.Columns.Item(iColumns).VisibleIndex = cont
                cont += 1
            End If
        Next
        'Se ocultan los agrupadores
        INDcolGroupCups.GroupIndex = -1
        INDcolSubGroupCups.GroupIndex = -1
    End Sub

    ''' <summary>
    ''' funcion que obtiene el listado de los items segun la condicion
    ''' </summary>
    ''' <param name="ConditionType"></param>
    Private Sub GetDataItemsXpo(ConditionType As Integer)
        ListXpCollection = Presenter.InitializeDataSourceGridControlsRulesType(ConditionType)
    End Sub

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        ListXpCollection = Nothing
        'obtenemos el listado de los items
        Me.viewControlsRuleType.ShowLoadingPanel()
        Me.SafeInvoke(Sub()
                          Me.INDlyItemConditionType.Enabled = False
                      End Sub)
        GetDataItemsXpo(ConditionType)

        ListXpCollection.Load()

        If ConditionType = liquidationDataDetail?.ConditionType Then
            If liquidationDataDetail?.ConditionsItemsIds IsNot Nothing Then
                'si esta funcion es accioanada cuando se edita un registo agregas los items checkqueados
                AddConditionsItems()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        INDgcControlsRuleType.DataSource = Nothing
        INDgcControlsRuleType.DataSource = ListXpCollection
        Me.viewControlsRuleType.HideLoadingPanel()
        Me.SafeInvoke(Sub()
                          Me.INDlyItemConditionType.Enabled = True
                      End Sub)
    End Sub

    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, optionCheck)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If optionCheck = 0 Then
                    row.SelectOption = False
                Else
                    row.SelectOption = True
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' valida cuando se de click en agregar o editar que los campos esten debidamente diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidatePopupData() As Boolean
        Dim stringBuilder = New StringBuilder()
        If Me.LimitType = 3 AndAlso Me.InsuranceCoinsuranceDetail = 0 AndAlso Me.PatientCoinsuranceDetail = 0 Then
            stringBuilder.AppendLine("Coaseguro paciente y aseguradora en 0")
        End If

        If stringBuilder.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = $"Validación: {stringBuilder.ToString()}"
            Return True
        End If
        Return False
    End Function

#End Region

#Region "ICrud"

    Public Async Sub Guardar() Implements ICrudBase.Guardar

        If Not ValidateControls() Then
            Exit Sub
        End If

        AssigningValues()

        Try
            Using model As New MLiquidationData(MyTag)
                AsyncLoader(True)
                Dim result As ActionResult(Of LiquidationData) = Await model.SaveLiquidationData(_liquidationData, _idCurrentSequence)

                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._liquidationData = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    RaiseEvent RecalculateMasterAccountFolios(Nothing, New EventArgs())
                Else
                    AsyncLoader(False)
                    INDSleApplyDiscount.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDSleApplyDiscount.Enabled = False
            Throw ex
        End Try

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "BarButtons"
    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region


End Class
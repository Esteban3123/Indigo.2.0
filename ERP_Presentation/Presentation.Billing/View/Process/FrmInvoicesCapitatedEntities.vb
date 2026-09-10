'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 20-12-2014
'
' Last Modified By : Anthony Ocampo
' Last Modified On : 18-08-2025
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Accounting.MVP
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class FrmInvoicesCapitatedEntities
    Implements IInvoicesCapitatedEntities

#Region "GLOBALS"
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.BillingSequence
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Contiene el id de la secuencia de detalle
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean
    ''' <summary>
    ''' The _account billing entities capitated
    ''' </summary>
    Private _invoiceEntityCapitated As InvoiceEntityCapitated
    ''' <summary>
    ''' presentador de cuentas capitadas
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PInvoicesCapitatedEntities

    Private _isLoaded As Boolean

    Private LiquidationType As Integer?

    Private ListGroupers As List(Of ViewCareGroupsWithGroupersXpo)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _varImp As Integer

    ''' <summary>
    ''' Id de la factura capita asociada como Factura anterior
    ''' </summary>
    Private IdPreviousInvoice As Integer?

    ''' <summary>
    ''' Control de usuario del formulario
    ''' </summary>
    Private _CtrTmp As New CtrInvoicesCapitatedEntities()

    ''' <summary>
    ''' Contiene la parametrización de Organizacion y Definición del Tenant
    ''' </summary>
    Private companySettings As CompanySettings

    ''' <summary>
    ''' Grupo de atención seleccionado
    ''' </summary>
    Private _careGroupCollectionMethod As Integer

    Private ContractCareGroupXpoEntity As ContractCareGroupXpo

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    ''' <summary>
    ''' Bandera que determina si se esta llevando a cabo el load control
    ''' </summary>
    Private _isLoadingControl As Boolean
#End Region

#Region "PROPERTIES"

    Public Property Observations() As String Implements IInvoicesCapitatedEntities.Observations
        Get
            Return INDMeObservations.EditValue
        End Get
        Set(value As String)
            INDMeObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la categoria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CategoryId As Integer? Implements IInvoicesCapitatedEntities.CategoryId
        Get
            Return INDsleCategory.EditValue
        End Get
        Set(value As Integer?)
            INDsleCategory.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la categoria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CategoryXpo As XPInstantFeedbackSource Implements IInvoicesCapitatedEntities.CategoryXpo
        Get
            Return INDsleCategory.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCategory.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el DataSource de la Factura anterior RIPS
    ''' </summary>
    ''' <returns></returns>
    Public Property PreviousRIPSInvoiceXpo As XPInstantFeedbackSource Implements IInvoicesCapitatedEntities.PreviousRIPSInvoiceXpo
        Get
            Return INDSlePreviousRIPSInvoice.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlePreviousRIPSInvoice.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el DataSource del campo moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyXpo As XPInstantFeedbackSource Implements IInvoicesCapitatedEntities.CurrencyXpo
        Get
            Return INDsleCurrency.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Grupo de atención
    ''' </summary>
    Public Property CareGroupId As Integer Implements IInvoicesCapitatedEntities.CareGroupId
        Get
            Return CInt(INDSleCareGroup.EditValue)
        End Get
        Set(value As Integer)
            INDSleCareGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la factura previa relacionada
    ''' </summary>
    ''' <returns></returns>
    Public Property PreviousRIPSInvoice As Integer? Implements IInvoicesCapitatedEntities.PreviousRIPSInvoice
        Get
            Return INDSlePreviousRIPSInvoice.EditValue
        End Get
        Set(value As Integer?)
            INDSlePreviousRIPSInvoice.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica el periodo de la factura
    ''' </summary>
    ''' <returns></returns>
    Public Property InvoicePeriod As Integer? Implements IInvoicesCapitatedEntities.InvoicePeriod
        Get
            Return INDSlePeriodInvoice.EditValue
        End Get
        Set(value As Integer?)
            INDSlePeriodInvoice.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la moneda 
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer Implements IInvoicesCapitatedEntities.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
            SetCurrencyUI(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' propiedad para tomar la abreviacion de la moneda y guardarla temp para cuando se necesite editar
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String
        Get
            Return INDsleCurrency.Text
        End Get
    End Property

    ''' <summary>
    ''' Código de la entidad
    ''' </summary>
    Public Property Code As String Implements IInvoicesCapitatedEntities.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    Public Property DocumentDate As DateTime Implements IInvoicesCapitatedEntities.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As DateTime)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha Final
    ''' </summary>
    Public Property EndDate As DateTime? Implements IInvoicesCapitatedEntities.EndDate
        Get
            Dim val = INDDteEndDate.EditValue
            If val Is Nothing OrElse val Is DBNull.Value Then Return Nothing
            Dim dt As DateTime = Convert.ToDateTime(val)
            Return dt.Date.AddDays(1).AddSeconds(-1)
        End Get
        Set(value As DateTime?)
            If value.HasValue Then
                INDDteEndDate.EditValue = value.Value.Date.AddDays(1).AddSeconds(-1)
            Else
                INDDteEndDate.EditValue = Nothing
            End If
        End Set
    End Property

    ''' <summary>
    ''' Fecha inicial
    ''' </summary>
    Public Property InitialDate As DateTime? Implements IInvoicesCapitatedEntities.InitialDate
        Get
            Dim val = INDDteInitialDate.EditValue
            If val Is Nothing OrElse val Is DBNull.Value Then Return Nothing
            Dim dt As DateTime = Convert.ToDateTime(val)
            Return dt.Date
        End Get
        Set(value As DateTime?)
            If value.HasValue Then
                INDDteInitialDate.EditValue = value.Value.Date
            Else
                INDDteInitialDate.EditValue = Nothing
            End If
        End Set
    End Property

    ''' <summary>
    ''' Estado
    ''' </summary>
    Public Property Status As Byte Implements IInvoicesCapitatedEntities.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Valor total
    ''' </summary>
    Public Property TotalValue As Decimal Implements IInvoicesCapitatedEntities.TotalValue
        Get
            Return CDec(INDTxtTotalValue.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtTotalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Número de usuario
    ''' </summary>
    Public Property UserNumber As Integer Implements IInvoicesCapitatedEntities.UserNumber
        Get
            Return CInt(INDSeUsersNumber.EditValue)
        End Get
        Set(value As Integer)
            INDSeUsersNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor de usuario
    ''' </summary>
    Public Property UserValue As Decimal Implements IInvoicesCapitatedEntities.UserValue
        Get
            Return CDec(INDTxtUserValue.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtUserValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    ''' <summary>
    ''' datasource de grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Property CareGroupXPO As XPInstantFeedbackSource Implements IInvoicesCapitatedEntities.CareGroupXPO
        Get
            Return INDSleCareGroup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCareGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInvoicesCapitatedEntities.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IInvoicesCapitatedEntities.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequence As BillingSequence Implements IInvoicesCapitatedEntities.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            Me.DicSequense.Clear()
            For Each seq As BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInvoicesCapitatedEntities.ActionsOnControls
        Set(value As Boolean)

            INDBteCode.Enabled = Not value
            INDDteDocumentDate.Enabled = value
            INDSleCareGroup.Enabled = value
            INDsleCategory.Enabled = value
            INDSlePeriodInvoice.Enabled = value
            INDSlePreviousRIPSInvoice.Enabled = value
            INDDteInitialDate.Enabled = value
            INDDteEndDate.Enabled = value
            INDsleCurrency.Enabled = value
            INDgcGroupers.Enabled = value
            INDSeUsersNumber.Enabled = value
            INDTxtUserValue.Enabled = value
            INDTxtTotalValue.Enabled = value
            INDTxtSubTotalValue.Enabled = value
            INDspDiscountPercentage.Enabled = value
            INDspDiscountValue.Enabled = value
            BarraBotones.StatusRecordVisible = value

            INDTxtTotalValue.Properties.ReadOnly = True
            INDTxtCopaymentAmount.Properties.ReadOnly = True
            INDTxtModeratingFeeAmount.Properties.ReadOnly = True
            INDTxtSharedFeeAmount.Properties.ReadOnly = True

            INDMeObservations.Enabled = value

            If Not value Then
                INDBteCode.Focus()
            Else
                INDSleCareGroup.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Campo de porcentaje de descuento
    ''' </summary>
    ''' <value></value>
    Public Property DiscountPercentage As Decimal Implements IInvoicesCapitatedEntities.DiscountPercentage
        Get
            Return CType(INDspDiscountPercentage.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDspDiscountPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Campo de valor de descuento
    ''' </summary>
    ''' <value></value>
    Public Property DiscountValue As Decimal Implements IInvoicesCapitatedEntities.DiscountValue
        Get
            Return CType(INDspDiscountValue.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDspDiscountValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Campo de subtotal
    ''' </summary>
    ''' <value></value>
    Public Property SubTotalValue As Decimal Implements IInvoicesCapitatedEntities.SubTotalValue
        Get
            Return CDec(INDTxtSubTotalValue.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtSubTotalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Campo de valor copago
    ''' </summary>
    ''' <value></value>
    Public Property CopaymentAmount As Decimal Implements IInvoicesCapitatedEntities.CopaymentAmount
        Get
            Return CDec(INDTxtCopaymentAmount.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtCopaymentAmount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Campo de cuota moderadora
    ''' </summary>
    ''' <value></value>
    Public Property ModeratingFeeAmount As Decimal Implements IInvoicesCapitatedEntities.ModeratingFeeAmount
        Get
            Return CDec(INDTxtModeratingFeeAmount.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtModeratingFeeAmount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Campo de pago compartido
    ''' </summary>
    ''' <value></value>
    Public Property SharedPaymentAmount As Decimal Implements IInvoicesCapitatedEntities.SharedPaymentAmouny
        Get
            Return CDec(INDTxtSharedFeeAmount.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtSharedFeeAmount.EditValue = value
        End Set
    End Property
#End Region

#Region "HANDLERS"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        _searchMode = Nothing
        _invoiceEntityCapitated = Nothing
        _presenter = Nothing
        _isLoaded = Nothing
        _varImp = Nothing
        _isLoadingControl = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmInvoicesCapitatedEntities control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmInvoicesCapitatedEntities_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PInvoicesCapitatedEntities(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()
        If FormSearchObjects Is Nothing Then
            _searchMode = False
        End If
        Dim _DataSourcePeriodInvoice As New List(Of ViewPeriodInvoice)
        _DataSourcePeriodInvoice.Add(New ViewPeriodInvoice With {.Id = 1, .Name = "Inicial"})
        _DataSourcePeriodInvoice.Add(New ViewPeriodInvoice With {.Id = 2, .Name = "Periodo"})
        _DataSourcePeriodInvoice.Add(New ViewPeriodInvoice With {.Id = 3, .Name = "Final"})
        INDSlePeriodInvoice.Properties.DataSource = _DataSourcePeriodInvoice
        LoadCurrencyXpo()
        Using modelCompanySettings As New MCompanySettings(MyTag)
            companySettings = Await modelCompanySettings.GetCompanySettings()
        End Using

        LoadStatus()
        Deshacer()
        _isLoaded = True
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Handles the Activated event of the FrmInvoicesCapitatedEntities control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    Private Sub FrmInvoicesCapitatedEntities_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmInvoicesCapitatedEntities control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmInvoicesCapitatedEntities_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDBteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDBteCode.Text) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDBteCode.Text) Then
                    Me.NewInvoicesCapitatedEntities()
                    If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
                        ActionsOnControls = True
                        INDBteCode.Enabled = False
                    End If
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDSleCareGroup control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If CareGroupXPO Is Nothing Then
            Using model As New MInvoicesCapitatedEntities(MyTag)
                CareGroupXPO = model.GetCareGroup()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de categoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCategory_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCategory.QueryPopUp
        If CategoryXpo Is Nothing Then
            _presenter.GetCategoriesByStatusAndUser(True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control para cargar su dataSource
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePreviousRIPSInvoice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePreviousRIPSInvoice.QueryPopUp, INDSleCareGroup.EditValueChanged
        If CareGroupId = 0 OrElse PreviousRIPSInvoiceXpo IsNot Nothing OrElse Me._isLoadingControl OrElse {2, 3, 4, 5}.Contains(Me.Status) Then Exit Sub
        _presenter.GetPreviousRIPSInvoiceByCareGruop(CareGroupId)
    End Sub

    ''' <summary>
    ''' Evento para asignar el DataSource del campo moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        LoadCurrencyXpo()
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDDteInitialDate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDDteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteInitialDate.EditValueChanged
        INDDteEndDate.Properties.MinValue = INDDteInitialDate.EditValue
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDDteEndDate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDDteEndDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteEndDate.EditValueChanged

        'Se validan campos y se traen los valores totales por concepto de recaudo
        If InitialDate IsNot Nothing AndAlso EndDate IsNot Nothing AndAlso CareGroupId <> 0 Then
            Await GetCollectionValues(InitialDate, EndDate, CareGroupId).ConfigureAwait(False)
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDSeUsersNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSeUsersNumber_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeUsersNumber.EditValueChanged
        GetSubTotalValue()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDTxtUserValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDTxtUserValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtUserValue.EditValueChanged
        GetSubTotalValue()
    End Sub


#End Region



    ''' <summary>
    ''' Funcion que valida si ya existe un RIPS validado de la factura seleccionada
    ''' </summary>
    ''' <param name="invoiceId"></param>
    ''' <returns></returns>
    Private Function ExistsRIPSValidated(invoiceId As Integer?) As Boolean
        ' Validar que invoiceId tenga un valor antes de continuar
        If invoiceId Is Nothing Then
            Throw New ArgumentException("El ID de la factura no puede ser nulo.", NameOf(invoiceId))
        End If
        'Usar.Any() para verificar si hay algún registro sin cargar toda la lista
        Return XpoServiceEx.Instance(indigo.TransactionalContainer) _
                       .InventoryService _
                       .Any(Of BillingRepository.ElectronicRIPSTraceability)($"EntityName='Invoice' AND EntityId={invoiceId}")
        Return Nothing
    End Function
    ''' <summary>
    ''' Valida el estado de la factura previa ante la DIAN
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ValidatePreviousInvoiceStatus() As Task(Of Boolean)
        If IdPreviousInvoice IsNot Nothing Then
            Using model As New MInvoicesCapitatedEntities(CStr(MyTag))
                Dim res = Await model.GetElectronicDocumentByInvoiceId(IdPreviousInvoice)
                If res IsNot Nothing Then
                    If res.Status = 3 Then Return True
                End If
                Return False
            End Using
        Else
            Throw New ArgumentNullException("IdPreviousInvoice")
        End If
    End Function
    ''' <summary>
    ''' News the invoices capitated entities.
    ''' </summary>
    Private Async Sub NewInvoicesCapitatedEntities()
        If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
            Me._invoiceEntityCapitated = New InvoiceEntityCapitated()
            If Me._sequence.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
                ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    Dim res = (From ou As BillingSequenceDetail In Me._sequence.BillingSequenceDetail Where ou.OperatingUnit.Id = Me._idOperativeUnit Select ou).ToList()
                    If res IsNot Nothing AndAlso res.Count > 0 Then
                        Me._idCurrentSequence = res(0).Id
                    Else
                        Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
                    End If
                End If
                If Not Me._sequence.Sequential Then
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Using model As New MBlockRecordAndSequense(Me.Tag)
                                Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                            End Using
                            If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                                Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Else
                                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                            End If
                        End If
                    Else
                        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores a la Entidad
    ''' </summary>
    Private Sub AssigningValues()
        With _invoiceEntityCapitated
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .CareGroupId = CareGroupId
            .InvoicePeriod = InvoicePeriod
            .PreviousRIPSInvoice = PreviousRIPSInvoice
            .InvoiceCategoryId = CategoryId
            .SubTotalValue = SubTotalValue
            If InvoicePeriod Is Nothing OrElse InvoicePeriod <> 3 Then
                .InitialDate = InitialDate
                .EndDate = EndDate
                .UserNumber = UserNumber
                .UserValue = UserValue
                .TotalValue = TotalValue
                .DiscountPercentage = DiscountPercentage
                .DiscountValue = DiscountValue
            Else
                .UserNumber = 0
                .UserValue = 0
                .TotalValue = 0
                .DiscountPercentage = 0
                .DiscountValue = 0
            End If
            .CurrencyId = CurrencyId
            If _invoiceEntityCapitated.ChangeTracker.State = ObjectState.Added Then
                .InvoiceId = Nothing
            End If
            .OperatingUnitId = Me._idOperativeUnit
            .ReversalReasonId = Nothing
            .ReversalDescription = Nothing
            .Observations = Observations
            .CopaymentAmount = 0
            .SharedPaymentAmount = 0
            .ModeratingFeeAmount = 0
        End With
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12},
                              New ColumnInfo() With {.Caption = "Factura", .FieldName = "InvoiceId.InvoiceNumber", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12},
                              New ColumnInfo() With {.Caption = "Fecha del Documento", .FieldName = "DocumentDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.14},
                              New ColumnInfo() With {.Caption = "Grupo Atención", .FieldName = "CareGroupId.CodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.49},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.13}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceEntityCapitated
            .FormParent = Me
            .ShowSearch()
        End With
        _searchMode = True
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        Try
            If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If

                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MInvoicesCapitatedEntities(Me.Tag)
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetInvoiceEntityCapitated(Me.Code)
                    AsyncLoader(False)
                    _invoiceEntityCapitated = resultOperation.ObjectEmbbeded
                    If Not _invoiceEntityCapitated Is Nothing Then
                        If _invoiceEntityCapitated.Id > 0 Then
                            _isLoadingControl = True
                            Using modelBlock As New MBlockRecordAndSequense(Me.Tag)
                                Dim result = Await modelBlock.GetBlockRecord(Me.Tag, _invoiceEntityCapitated.Id)
                                With _invoiceEntityCapitated
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                    Code = .Code
                                    DocumentDate = .DocumentDate
                                    CareGroupId = .CareGroupId
                                    InvoicePeriod = .InvoicePeriod
                                    INDSlePreviousRIPSInvoice.Properties.NullText = .PreviousInvoiceNumberRips
                                    PreviousRIPSInvoice = .PreviousRIPSInvoice
                                    CategoryId = .InvoiceCategoryId
                                    INDsleCategory.Properties.NullText = .CodeNameCategory
                                    InitialDate = .InitialDate
                                    EndDate = .EndDate
                                    CurrencyId(.Currency.Abbreviation) = .CurrencyId
                                    UserNumber = .UserNumber
                                    UserValue = .UserValue
                                    _isLoaded = False
                                    DiscountPercentage = .DiscountPercentage
                                    DiscountValue = .DiscountValue
                                    _isLoaded = True
                                    CopaymentAmount = .CopaymentAmount
                                    ModeratingFeeAmount = .ModeratingFeeAmount
                                    SharedPaymentAmount = .SharedPaymentAmount
                                    TotalValue = .TotalValue
                                    Status = .Status
                                    Observations = .Observations
                                    If .InvoiceId IsNot Nothing Then
                                        AdditionalControlPanel.Controls.Add(_CtrTmp) 'Se adiciona el Ctr solamente si el registro tiene una factura relacionada
                                        _CtrTmp.Dock = Windows.Forms.DockStyle.Fill
                                        _CtrTmp.SalesInvoiceConsecutive = .Invoice.InvoiceNumber
                                        _CtrTmp.LiquidationType = .CareGroup.LiquidationType
                                        _CtrTmp.CollectionsTotalValue = "Valor Recaudos: $" + (.CopaymentAmount + .ModeratingFeeAmount + .SharedPaymentAmount).ToString()
                                    End If
                                End With

                                'Se guarda globalmente el grupo de atención
                                _careGroupCollectionMethod = _invoiceEntityCapitated.CareGroup.MethodFixedAmountCollectionReport

                                INDSleCareGroup.Properties.NullText = _invoiceEntityCapitated.FullNameCareGroup
                                Me.GetDocumentIndexed(Me.Tag & "_" & Me._invoiceEntityCapitated.Code)
                                If result.Id = 0 Then
                                    Dim state = New ObjectChangeTracker
                                    state.State = ObjectState.Added
                                    _record = New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _invoiceEntityCapitated.Id}
                                    Dim operation = Await modelBlock.SaveBlockRecord(_record)
                                    _record = operation.ObjectEmbbeded
                                Else
                                    _record = result
                                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                                End If
                                Me.BarraBotones.SetDocuments(_invoiceEntityCapitated.Id, Me.Tag.ToString(), Nothing, GetType(CashRegisters).Name)

                                If {2, 3, 4, 5}.Contains(_invoiceEntityCapitated.Status) Then 'estado confirmado
                                    ReadOnlyControls(True)
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                Else
                                    Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Anular, ResourceManager.GetString("CaptionAnnulBarButton"))
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                                End If

                                If _invoiceEntityCapitated.Status = 2 Then
                                    Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Anular, ResourceManager.GetString("CaptionReverseBarButton"))
                                    Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.GenerateFile, ResourceManager.GetString("CaptionGenerateRIPSseBarButton"))
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                                    Me.BarraBotones.PrintReport(PrintReportAction.None, _invoiceEntityCapitated.InvoiceId, 0, _invoiceEntityCapitated.InvoiceId)
                                ElseIf _invoiceEntityCapitated.Status = 4 Then
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                                    Me.BarraBotones.PrintReport(PrintReportAction.None, _invoiceEntityCapitated.InvoiceId, 0, _invoiceEntityCapitated.InvoiceId)
                                End If

                                ActionsOnControls = True
                                INDBteCode.Enabled = False
                            End Using
                        Else
                            If Me._sequence.IsManual Then
                                Me.NewInvoicesCapitatedEntities()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                                Me.Code = String.Empty
                                INDBteCode.Focus()
                            End If
                        End If
                    Else
                        If Me._sequence.IsManual Then
                            Me.NewInvoicesCapitatedEntities()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Me.Code = String.Empty
                            Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                            INDBteCode.Focus()
                        End If
                    End If
                End Using
            End If
        Finally
            _isLoadingControl = False
        End Try
    End Function

    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlcRoot.BeginUpdate()
        ActionsOnControls = False
        INDBteCode.Text = String.Empty
        INDDteDocumentDate.EditValue = GetDateServer()
        INDSleCareGroup.EditValue = Nothing
        InvoicePeriod = 2
        PreviousRIPSInvoice = Nothing
        INDMeObservations.EditValue = Nothing
        CategoryId = Nothing
        INDsleCategory.Properties.NullText = String.Empty
        INDDteInitialDate.EditValue = GetDateServer()
        INDDteEndDate.EditValue = GetDateServer()
        CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
        INDSeUsersNumber.EditValue = 1
        _isLoaded = False
        INDspDiscountPercentage.EditValue = 0
        INDspDiscountValue.EditValue = 0
        _isLoaded = True
        INDTxtUserValue.EditValue = 0
        INDTxtTotalValue.EditValue = 0
        INDTxtSubTotalValue.EditValue = 0
        INDSleCareGroup.Properties.NullText = String.Empty
        INDMeObservations.Properties.NullText = String.Empty
        IdPreviousInvoice = Nothing
        INDSlePreviousRIPSInvoice.Properties.NullText = Nothing
        PreviousRIPSInvoiceXpo = Nothing
        Status = 1
        CopaymentAmount = 0
        ModeratingFeeAmount = 0
        SharedPaymentAmount = 0

        LiquidationType = Nothing
        ListGroupers = Nothing

        INDlygGroupers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciUsersNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciUserValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        SetFieldsVisibility(True)
        INDgcGroupers.DataSource = Nothing
        AdditionalControlPanel.Controls.Remove(_CtrTmp)
        _CtrTmp.CleanControls()

        ReadOnlyControls(False)
        INDlcRoot.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
    End Sub

    ''' <summary>
    ''' Obtiene el valor subtotal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSubTotalValue()
        If LiquidationType = 5 Then
            If ListGroupers IsNot Nothing AndAlso ListGroupers.Count > 0 Then
                INDTxtSubTotalValue.EditValue = ListGroupers.Sum(Function(g) g.TotalContract)
            Else
                INDTxtSubTotalValue.EditValue = 0
            End If
        Else
            INDTxtSubTotalValue.EditValue = INDSeUsersNumber.EditValue * INDTxtUserValue.EditValue
        End If
        GetTotalValue()
    End Sub

    ''' <summary>
    ''' Obtiene y asigna los valores totales por concepto de recaudo para facturas capitas/PGP
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetCollectionValues(initialDate As DateTime, endDate As DateTime, careGroupId As Integer) As Task
        Using model As New MInvoicesCapitatedEntities(MyTag)
            If _careGroupCollectionMethod = 1 Then
                Dim collectionValues = Await model.GetCollectionValues(initialDate, endDate, careGroupId)
                CopaymentAmount = collectionValues.CopaymentAmount
                ModeratingFeeAmount = collectionValues.ModeratingFeeAmount
                SharedPaymentAmount = collectionValues.SharedPaymentAmount
            Else
                CopaymentAmount = 0.0D
                ModeratingFeeAmount = 0.0D
                SharedPaymentAmount = 0.0D
            End If
            GetTotalValue()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el valor total, restando subtotal menos valores de descuento o valores de recaudo.
    ''' </summary>
    Private Sub GetTotalValue()
        INDTxtTotalValue.EditValue = INDTxtSubTotalValue.EditValue - (DiscountValue + CopaymentAmount + ModeratingFeeAmount + SharedPaymentAmount)
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._invoiceEntityCapitated.Code, Me._invoiceEntityCapitated.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._invoiceEntityCapitated.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._invoiceEntityCapitated.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._invoiceEntityCapitated.Code, Me._invoiceEntityCapitated.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._invoiceEntityCapitated.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverced"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "5", .StatusName = ResourceManager.GetString("StateConfirmedFinalRips"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Método que maneja la visibilidad de campos
    ''' </summary>
    Private Sub SetFieldsVisibility(ByVal isNewlyOpen As Boolean)
        If isNewlyOpen OrElse LiquidationType Is Nothing OrElse InvoicePeriod Is Nothing Then
            DefaultVisibility()
            Exit Sub
        End If

        'Visibilidad de acuerdo a método de reporte de recaudo
        If _careGroupCollectionMethod = 1 Then
            INDLciCopaymentAmount.HideControl(False)
            INDLciModeratingFeeAmount.HideControl(False)
            INDLciSharedFeeAmount.HideControl(False)

            INDLciDiscountValue.HideControl(True)
            INDLciDiscountPercentage.HideControl(True)
        Else
            INDLciCopaymentAmount.HideControl(True)
            INDLciModeratingFeeAmount.HideControl(True)
            INDLciSharedFeeAmount.HideControl(True)

            INDLciDiscountValue.HideControl(False)
            INDLciDiscountPercentage.HideControl(False)
        End If

        ' Condición de visibilidad para INDLciPeriodInvoice
        Dim showPeriodInvoice = (LiquidationType = 2)
        INDLciPeriodInvoice.HideControl(Not showPeriodInvoice)
        ' Condición de visibilidad para INDLciPreviousRIPSInvoice
        ' Se oculta si InvoicePeriod es diferente de final o si INDLciPeriodInvoice está oculto
        Dim showRIPSInvoice = (InvoicePeriod <> 1) And showPeriodInvoice
        INDLciPreviousRIPSInvoice.HideControl(Not showRIPSInvoice)
        INDLciInitialDate.HideControl(InvoicePeriod = 3)
        INDLciEndDate.HideControl(InvoicePeriod = 3)
        INDlygValues.HideControl(InvoicePeriod = 3)
        INDLciCurrency.HideControl(InvoicePeriod = 3)
    End Sub

    ''' <summary>
    ''' Visualizacion por defecto de los controles
    ''' </summary>
    Private Sub DefaultVisibility()
        INDLciInitialDate.HideControl(False)
        INDLciEndDate.HideControl(False)
        INDlygValues.HideControl(False)
        INDLciPeriodInvoice.HideControl(True)
        INDLciPreviousRIPSInvoice.HideControl(True)
        INDLciCurrency.HideControl(False)

        INDLciCopaymentAmount.HideControl(True)
        INDLciModeratingFeeAmount.HideControl(True)
        INDLciSharedFeeAmount.HideControl(True)
    End Sub


    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyAbbreviationEmpty")
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDTxtUserValue.Properties.Mask.Culture = _culture
        Me.INDTxtSubTotalValue.Properties.Mask.Culture = _culture
        Me.INDspDiscountValue.Properties.Mask.Culture = _culture
        Me.INDTxtTotalValue.Properties.Mask.Culture = _culture
    End Sub

    ''' <summary>
    ''' Obtiene el DataSource del campo moneda para mostrar valores por defecto
    ''' </summary>
    Private Sub LoadCurrencyXpo()
        If CurrencyXpo Is Nothing Then
            Using model As New MCompanySettings(CStr(MyTag))
                CurrencyXpo = model.GetCurrencyDatasource()
            End Using
        End If
    End Sub
#End Region

#Region "CRUD"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If _searchMode = False Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If _invoiceEntityCapitated IsNot Nothing AndAlso _invoiceEntityCapitated.Status <> 3 Then
            If ValidateControls() = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                Exit Sub
            End If
            If _invoiceEntityCapitated.Status = 2 And INDLciPreviousRIPSInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If Not Await ValidatePreviousInvoiceStatus() Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("PreviousInvoiceNoValidated", MODULE_NAME)
                    Exit Sub
                End If
            End If
        End If
        Try
            AssigningValues()

            If _invoiceEntityCapitated.Status = 4 Then
                Using PopUpAnnulmentReason As New PopUpAnnulmentReason()
                    Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                    If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                        AsyncLoader(False)
                        Exit Sub
                    Else
                        _invoiceEntityCapitated.ReversalReasonId = PopUpAnnulmentReason.ReversalReasonId
                        _invoiceEntityCapitated.ReversalDescription = PopUpAnnulmentReason.ReversalDescription
                    End If
                End Using
            End If

            If ContractCareGroupXpoEntity?.ContractId?.HealthAdministratorId?.ThirdPartyId?.Id IsNot Nothing And _invoiceEntityCapitated.Status = 2 Then
                Dim result = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.GetPortfolioAdvanceByThirdPartyIdForFixedAmountInvoice(ContractCareGroupXpoEntity.ContractId.HealthAdministratorId.ThirdPartyId.Id)
                If result?.Count > 0 Then
                    Using FrmAdvance As New FrmAdvanceCrossingByInvoicesCapitatedEntities
                        FrmAdvance.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
                        FrmAdvance.CurrencyId = CurrencyId
                        FrmAdvance.ThirdPartyId = ContractCareGroupXpoEntity?.ContractId?.HealthAdministratorId?.ThirdPartyId?.Id
                        FrmAdvance.CurrencyAbbreviation = INDsleCurrency.Text
                        FrmAdvance.TotalEntity = _invoiceEntityCapitated.TotalValue
                        AddHandler FrmAdvance.RunIncludePortfolioAdvance, AddressOf RunIncludePortfolioAdvance
                        Dim transparent As New FrmTransparent(FrmAdvance, False)
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End If

            Using Model As New MInvoicesCapitatedEntities(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveInvoiceEntityCapitated(Me._invoiceEntityCapitated)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message

                    Me._invoiceEntityCapitated = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _invoiceEntityCapitated.Id, 0, _invoiceEntityCapitated.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _invoiceEntityCapitated.Id, 0, _invoiceEntityCapitated.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _invoiceEntityCapitated.Id, 0, _invoiceEntityCapitated.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _invoiceEntityCapitated.Id, 0, _invoiceEntityCapitated.Id)
                    End Select
                    _searchMode = False
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Runs the Include portfolio advance
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RunIncludePortfolioAdvance(sender As Object, e As RunIncludePortfolioAdvanceArgs)
        Try
            If e.ListPortfolioAdvance.Any() Then
                _invoiceEntityCapitated.ListPortfolioAdvance = e.ListPortfolioAdvance
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If

        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewInvoicesCapitatedEntities()
            If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
                ActionsOnControls = True
                INDBteCode.Enabled = False
            End If
        End If
    End Sub
#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _invoiceEntityCapitated.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _invoiceEntityCapitated.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _invoiceEntityCapitated.InvoiceId, 0, _invoiceEntityCapitated.InvoiceId)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BillingSequenceDetail IsNot Nothing Then
            If Me._sequence.BillingSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idCurrentSequence = Me._sequence.BillingSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me._idOperativeUnit = BarraBotones.OperatingUnitValue
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _invoiceEntityCapitated.Status = If(InvoicePeriod Is Nothing OrElse InvoicePeriod <> 3, 2, 5)
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _invoiceEntityCapitated.Status = If(InvoicePeriod Is Nothing OrElse InvoicePeriod <> 3, 2, 5)
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _invoiceEntityCapitated.Status = 2 OrElse _invoiceEntityCapitated.Status = 4 Then 'Si el estado actual es confirmada, entonces se va a reversar
                _invoiceEntityCapitated.Status = 4
            ElseIf _invoiceEntityCapitated.Status = 1 Then 'Si no, se va a anular
                _invoiceEntityCapitated.Status = 3
            Else
                Mensaje(EeventViewerImages.Advertencia) = "La factura se encuentra en un estado en el cual no se puede anular ni reversar"
                Exit Sub
            End If
            _varImp = 3
            Guardar()
        End If
    End Sub
    ''' <summary>
    ''' Barras the botones_ click Generar Archivo
    ''' </summary>

    Private Sub BarraBotones_ClickGenerateFile() Handles BarraBotones.Click_GenerateFile
        'Obtiene la fecha del servidor
        Dim currentDate As Date = Me.GetDateServer()
        'Valida si la fecha actual(no menor a la fecha final)
        If currentDate < EndDate Then
            MessageIndigo.Show(ResourceManager.GetString("ValidateRegisteredDate"), MessageType.Information, Me.Text)
            Return
        End If

        If ExistsRIPSValidated(_invoiceEntityCapitated.InvoiceId) Then
            MessageIndigo.Show(ResourceManager.GetString("ValidateExistingRips"), MessageType.Information, Me.Text)
            Exit Sub
        End If
    End Sub

    Private Sub INDspDiscountPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDspDiscountPercentage.EditValueChanged
        If Me._isLoaded AndAlso INDTxtSubTotalValue.EditValue IsNot Nothing AndAlso INDTxtSubTotalValue.EditValue > 0 Then
            _isLoaded = False
            INDspDiscountValue.EditValue = INDTxtSubTotalValue.EditValue * (INDspDiscountPercentage.EditValue / 100)
            GetTotalValue()
            _isLoaded = True
        End If
    End Sub

    Private Sub INDspDiscountValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDspDiscountValue.EditValueChanged
        If Me._isLoaded AndAlso INDTxtSubTotalValue.EditValue IsNot Nothing AndAlso INDTxtSubTotalValue.EditValue > 0 Then
            _isLoaded = False
            INDspDiscountPercentage.EditValue = (INDspDiscountValue.EditValue / INDTxtSubTotalValue.EditValue) * 100
            GetTotalValue()
            _isLoaded = True
        End If
    End Sub
    ''' <summary>
    ''' Evento que se ejecuta al cambiar el grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareGroup.EditValueChanged
        LiquidationType = Nothing
        INDTxtSubTotalValue.Properties.ReadOnly = False
        INDlygGroupers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciUsersNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciUserValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDgcGroupers.DataSource = Nothing
        'Validar si es PGP u otro para mostrar u ocultar determinada información
        If INDSleCareGroup.EditValue IsNot Nothing Then
            Dim ContractCareGroupXpo = _presenter.GetContractCareGroupById(CareGroupId)
            If ContractCareGroupXpo IsNot Nothing Then
                ContractCareGroupXpoEntity = ContractCareGroupXpo
                LiquidationType = ContractCareGroupXpo.LiquidationType

                _careGroupCollectionMethod = ContractCareGroupXpo.MethodFixedAmountCollectionReport

                SetFieldsVisibility(False)
                If LiquidationType = 5 Then
                    INDTxtSubTotalValue.Properties.ReadOnly = True
                    INDlygGroupers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciUsersNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciUserValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    If _invoiceEntityCapitated IsNot Nothing AndAlso _invoiceEntityCapitated.Status >= 2 Then
                        ListGroupers = _presenter.ListInvoiceEntityCapitatedGroupers(_invoiceEntityCapitated.Id)
                    Else
                        ListGroupers = _presenter.ListCareGroupsWithGroupers(CareGroupId, InitialDate, EndDate)
                    End If

                    INDgcGroupers.DataSource = ListGroupers
                    GetSubTotalValue()
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de factura periodo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePeriodInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePeriodInvoice.EditValueChanged
        SetFieldsVisibility(False)
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la factura anterior RIPS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSlePreviousRIPSInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePreviousRIPSInvoice.EditValueChanged
        If PreviousRIPSInvoice IsNot Nothing Then
            Using model As New MInvoicesCapitatedEntities(CStr(MyTag))
                Dim InvoiceCapitated = Await model.GetInvoiceEntityCapitatedById(PreviousRIPSInvoice)
                IdPreviousInvoice = InvoiceCapitated.InvoiceId

                'Se establecen los valores de recaudo en base a los parametros de la facturación anterior.
                Await GetCollectionValues(InvoiceCapitated.InitialDate, InvoiceCapitated.EndDate, InvoiceCapitated.CareGroupId)
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        If CurrencyId = 0 OrElse String.IsNullOrEmpty(Me.CurrencyAbbreviation) Then Exit Sub
        SetCurrencyUI(Me.CurrencyAbbreviation)
    End Sub

#End Region

End Class

''' <summary>
''' Simplificacion de clase de la vista
''' Se usa para gestionar los valores directamte
''' </summary>
Public Class ViewPeriodInvoice
    Public Property Id As Integer
    Public Property Name As String
End Class
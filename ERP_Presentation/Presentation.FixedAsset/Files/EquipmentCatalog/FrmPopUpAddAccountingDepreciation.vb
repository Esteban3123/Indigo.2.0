Imports Presentation.FixedAsset.MVP
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Entities
Imports System.Drawing
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository

Public Class FrmPopUpAddAccountingDepreciation

#Region "Builder"
    Public Sub New(ByVal HandlesDepreciationbyDistribution As Boolean, Optional Classification As Integer = 0)
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _handlesDepreciationbyDistribution = HandlesDepreciationbyDistribution
        SetTextToIntangible(Classification)
    End Sub
#End Region

#Region "EVENTS"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddEquipmentCatalogEventArgs(sender As Object, e As AddEquipmentCatalogEventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "FixedAsset"

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetEquipmentCatalogDetail As FixedAssetItemCatalogDetail

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean
    ''' <summary>
    ''' bandera para saber si se esta importando informacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _importDataMode As Boolean

    ''' <summary>
    ''' listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listFixedAssetEquipmentCatalogDetailImportInfo As List(Of FixedAssetItemCatalogDetail)
    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listFixedAssetEquipmentCatalogDetailValidation As List(Of FixedAssetItemCatalogDetail)
    ''' <summary>
    ''' Variable que permite saber si maneja depreciación por distribución o no
    ''' </summary>
    Dim _handlesDepreciationbyDistribution As Boolean
    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _operatingUnitId As Integer

    Dim _MyTag As String

    Dim _FlagTypeCatalog As String

    ''' <summary>
    ''' presenter
    ''' </summary>
    Private Presenter As PPUC
    ''' <summary>
    ''' Almacena las restricciones de las cuentas 
    ''' </summary>
    Dim MainAccountRestrictions As MainAccountRestrictions

    ''' <summary>
    ''' variable que se utiliza para almacenar las nuevas restriccion de las cuentas contables
    ''' </summary>
    Private ListNewMainAccountRestrictions As List(Of MainAccountRestrictions)

#End Region

#Region "Propiedades"

    Property FinancialRentingAccountXpo As XPInstantFeedbackSource
        Get
            Return INDsleFinancialRentingAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFinancialRentingAccount.Properties.DataSource = value
        End Set
    End Property

    Property ExpenseLoanAccountXpo As XPInstantFeedbackSource
        Get
            Return INDsleExpenseLoanAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleExpenseLoanAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de la Estructura Contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountingStructureXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlAccountingStructure.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlAccountingStructure.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Lista las cuentas Gasto Depreciación
    ''' </summary>
    ''' <returns></returns>
    Property AccountingDepreciationXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlSpendDepreciationAccount.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlSpendDepreciationAccount.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Id de la cuenta Gasto Depreciación
    ''' </summary>
    ''' <returns></returns>
    Property IdAccountingDepreciation As Integer
        Get
            Return INDSlSpendDepreciationAccount.EditValue
        End Get
        Set(value As Integer)
            INDSlSpendDepreciationAccount.EditValue = value
        End Set
    End Property

    Property AccountingLeasingXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlSpendLeasingAccount.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlSpendLeasingAccount.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Asigna el Datasource de Centros de Costo
    ''' </summary>
    ''' <returns></returns>
    Property CostCenterXPO As XPInstantFeedbackSource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    Property AccountingStructureId As Integer?
        Get
            Return INDSlAccountingStructure.EditValue
        End Get
        Set(value As Integer?)
            INDSlAccountingStructure.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Id del centro de costo seleccionado
    ''' </summary>
    ''' <returns></returns>
    Property IdCostCenter As Integer?
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Código y Nombre del Centro de Costos
    ''' </summary>
    ''' <returns></returns>
    Property NumberNameCostCenter As String
        Get
            Return INDsleCostCenter.Text
        End Get
        Set(value As String)
            INDsleCostCenter.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Porcentaje de Distribución
    ''' </summary>
    ''' <returns></returns>
    Property DistributionPercentage As Double?
        Get
            Return CType(INDSlDistributionPercentage.EditValue, Double)
        End Get
        Set(value As Double?)
            INDSlDistributionPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MyTag As String
        Set(value As String)
            _MyTag = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property FlagTypeCatalog As String
        Set(value As String)
            _FlagTypeCatalog = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListFixedAssetEquipmentCatalogDetailValidation As List(Of FixedAssetItemCatalogDetail)
        Set(value As List(Of FixedAssetItemCatalogDetail))
            If value IsNot Nothing Then
                _listFixedAssetEquipmentCatalogDetailValidation = New List(Of FixedAssetItemCatalogDetail)(value.ToArray())
            End If
        End Set
    End Property
    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListFixedAssetEquipmentCatalogDetailImportInfo As List(Of FixedAssetItemCatalogDetail)
        Set(value As List(Of FixedAssetItemCatalogDetail))
            _listFixedAssetEquipmentCatalogDetailImportInfo = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad para establecer si se esta importando informacion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ImportDataMode As Boolean
        Set(value As Boolean)
            _importDataMode = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property FixedAssetEquipmentCatalogDetailInformationEdit As FixedAssetItemCatalogDetail
        Set(value As FixedAssetItemCatalogDetail)
            FixedAssetEquipmentCatalogDetail = value
        End Set
    End Property


    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDsleCostCenter.Enabled = value
            INDSlDistributionPercentage.Enabled = value
            INDSlAccountingStructure.Enabled = value
            INDSlSpendDepreciationAccount.Enabled = value
            INDSlSpendLeasingAccount.Enabled = value
            INDsleExpenseLoanAccount.Enabled = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FixedAssetEquipmentCatalogDetail = Nothing
        _editMode = Nothing
        _importDataMode = Nothing
        _listFixedAssetEquipmentCatalogDetailImportInfo = Nothing
        _listFixedAssetEquipmentCatalogDetailValidation = Nothing
        _operatingUnitId = Nothing
        _MyTag = Nothing
        _FlagTypeCatalog = Nothing
    End Sub

    Private Sub FrmPopUpAddAccountingDepreciation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        If _editMode = True Then
            'si esta editando un registro
            LoadControls()
        Else
            'si es un nuevo registro
            CleanControls()
        End If

        If _handlesDepreciationbyDistribution Then
            INDLciCostCenter.HideControl(False)
            INDLciDistributionPercentage.HideControl(False)
            INDLciAccountingStructure.HideControl(True)
        Else
            INDLciCostCenter.HideControl(True)
            INDLciDistributionPercentage.HideControl(True)
        End If
    End Sub
#End Region

#Region "QueryPopUp"

    Private Sub INDsleFinancialRentingAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleFinancialRentingAccount.QueryPopUp
        If FinancialRentingAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(_MyTag)
                FinancialRentingAccountXpo = model.ListAccount()
            End Using
        End If
    End Sub

    Private Sub INDsleExpenseLoanAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleExpenseLoanAccount.QueryPopUp
        If ExpenseLoanAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(_MyTag)
                ExpenseLoanAccountXpo = model.ListAccount()
            End Using
        End If
    End Sub

    Private Sub INDSlAccountingStructure_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlAccountingStructure.QueryPopUp
        If INDSlAccountingStructure.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If AccountingStructureXPO Is Nothing Then
            Using model As New MEquipmentCatalog(_MyTag)
                AccountingStructureXPO = model.ListAccountStructure()
            End Using
        End If
    End Sub

    Private Sub INDSlSpendDepreciationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlSpendDepreciationAccount.QueryPopUp
        If INDSlSpendDepreciationAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If AccountingDepreciationXPO Is Nothing Then
            Using model As New MEquipmentCatalog(_MyTag)
                AccountingDepreciationXPO = model.ListAccount()
            End Using
        End If
    End Sub


    Private Sub INDSlSpendLeasingAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlSpendLeasingAccount.QueryPopUp
        If INDSlSpendLeasingAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If AccountingLeasingXPO Is Nothing Then
            Using model As New MEquipmentCatalog(_MyTag)
                AccountingLeasingXPO = model.ListAccount()
            End Using
        End If
    End Sub

    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.ReadOnly Then
            Exit Sub
        End If
        If CostCenterXPO Is Nothing Then
            Using model As New MEquipmentCatalog(_MyTag)
                CostCenterXPO = model.GetCostCenter()
            End Using
        End If
    End Sub
#End Region

#Region "KeyDown"

    Private Sub FrmPopUpAddAccountingDepreciation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmPopUpAddAccountingDepreciation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If FixedAssetEquipmentCatalogDetail IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If

    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            AsyncLoader(True)
            INDBtnAdd.Enabled = False

            Dim errors = ValidateControlsPopup(_FlagTypeCatalog)
            If errors.Length > 0 Then
                AsyncLoader(False)
                INDBtnAdd.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If

            'si no se esta importando informacion asigno valores
            If _importDataMode = False Then
                SetValues()
            End If
            If _listFixedAssetEquipmentCatalogDetailValidation Is Nothing Then
                _listFixedAssetEquipmentCatalogDetailValidation = New List(Of FixedAssetItemCatalogDetail)
            End If
            Dim args As New AddEquipmentCatalogEventArgs
            If _editMode = True Then
                args.FixedAssetItemCatalogDetail = FixedAssetEquipmentCatalogDetail
                args.EditMode = True
            Else
                args.FixedAssetItemCatalogDetail = FixedAssetEquipmentCatalogDetail
                _listFixedAssetEquipmentCatalogDetailValidation.Add(FixedAssetEquipmentCatalogDetail)
            End If
            If _importDataMode = True Then
            End If


            RaiseEvent AddEquipmentCatalogEventArgs(Nothing, args)

            AsyncLoader(False)
            INDBtnAdd.Enabled = True

            CleanControls()
            Me.Close()
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub

#End Region

#End Region

#Region "METHODS"

    Private Sub SetTextToIntangible(Classification)
        If Classification <> 0 Then
            If Classification = 2 Then
                INDLcgAccountingInformation.Text = "Información Contable de Amortización"
                INDLciSpendAccountDepreciation.Text = "Cuenta Gasto Amortización"
            End If
        End If
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup(FlagTypeCatalog As String) As String
        Dim errors As New StringBuilder

        If IdAccountingDepreciation = 0 Then
            errors.AppendLine(INDLciSpendAccountDepreciation.Text + ResourceManager.GetString("Empty"))
        End If

        If INDSlSpendLeasingAccount.EditValue = 0 Then
            errors.AppendLine(INDLciSpendLeasingAccount.Text + ResourceManager.GetString("Empty"))
        End If

        If INDsleExpenseLoanAccount.EditValue = 0 OrElse INDsleExpenseLoanAccount.EditValue Is Nothing Then
            errors.AppendLine(INDlyItemExpenseLoanAccount.Text + ResourceManager.GetString("Empty"))
        End If

        If INDsleFinancialRentingAccount.EditValue = 0 OrElse INDsleFinancialRentingAccount.EditValue Is Nothing Then
            errors.AppendLine(INDlyItemFinancialRentingAccount.Text + ResourceManager.GetString("Empty"))
        End If

        If _handlesDepreciationbyDistribution Then
            If IdCostCenter Is Nothing Then
                errors.AppendLine(INDLciCostCenter.Text + ResourceManager.GetString("Empty"))
            End If

            If DistributionPercentage Is Nothing Or DistributionPercentage = 0 Then
                errors.AppendLine(INDLciDistributionPercentage.Text + ResourceManager.GetString("Empty"))
            End If

        Else 'Validaciones si NO maneja depreciación por distribución

            If AccountingStructureId Is Nothing Then
                errors.AppendLine(INDLciAccountingStructure.Text + ResourceManager.GetString("Empty"))
            End If

            If _listFixedAssetEquipmentCatalogDetailValidation IsNot Nothing AndAlso _listFixedAssetEquipmentCatalogDetailValidation.Count > 0 Then
                Dim remissionEntranceDetailTmp = _listFixedAssetEquipmentCatalogDetailValidation.Find(Function(x) x.AccountingStructureId = AccountingStructureId)
                If remissionEntranceDetailTmp IsNot Nothing Then
                    errors.AppendLine("Ya existe una Información Contable con esa estructura Contable Seleccionada")
                End If
            End If

        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 730)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        FixedAssetEquipmentCatalogDetail = Nothing
        'ActionsControls = False
        IdCostCenter = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty
        DistributionPercentage = Nothing
        INDSlDistributionPercentage.Properties.NullText = String.Empty

        IdAccountingDepreciation = 0
        INDSlSpendDepreciationAccount.Properties.NullText = String.Empty
        AccountingStructureId = Nothing
        INDSlAccountingStructure.Properties.NullText = String.Empty
        INDSlSpendLeasingAccount.EditValue = 0
        INDSlSpendLeasingAccount.Properties.NullText = String.Empty
        INDsleExpenseLoanAccount.EditValue = 0
        INDsleExpenseLoanAccount.Properties.NullText = String.Empty
        INDsleFinancialRentingAccount.EditValue = 0
        INDsleFinancialRentingAccount.Properties.NullText = String.Empty
        ListFixedAssetEquipmentCatalogDetailImportInfo = Nothing
        ListFixedAssetEquipmentCatalogDetailValidation = Nothing
        _importDataMode = False
        _editMode = False
    End Sub

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        'Administrativo
        If _editMode = False Then
            FixedAssetEquipmentCatalogDetail = New FixedAssetItemCatalogDetail
        End If
        With FixedAssetEquipmentCatalogDetail
            .AccountingStructureId = AccountingStructureId
            .CodeNameAccountingStructure = INDSlAccountingStructure.Text
            .LoanLeasingSpendAccountId = INDSlSpendLeasingAccount.EditValue
            .NumberNameLoanLeasingAccountingAccount = INDSlSpendLeasingAccount.Text
            .LoanSpendAccountId = IdAccountingDepreciation
            .NumberNameLoanSpendAccountingAccount = INDSlSpendDepreciationAccount.Text
            .ExpenseLoanAccountId = INDsleExpenseLoanAccount.EditValue
            .ExpenseLoanAccountDescription = INDsleExpenseLoanAccount.Text
            .LoanFinancialRentingAccountId = INDsleFinancialRentingAccount.EditValue
            .LoanFinancialRentingAccountDescription = INDsleFinancialRentingAccount.Text
            .CostCenterId = IdCostCenter
            .NumberNameCostCenter = NumberNameCostCenter
            .DistributionPercentage = DistributionPercentage
        End With

    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="AdministrativeLoanAccountingInformationTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls(Optional FixedAssetEquipmentCatalogDetailTmp As FixedAssetItemCatalogDetail = Nothing)
        If _editMode = True Then
            INDBtnAdd.Text = ResourceManager.GetString("Edit")
        End If
        If _importDataMode = True Then
            FixedAssetEquipmentCatalogDetail = FixedAssetEquipmentCatalogDetailTmp
        End If

        With FixedAssetEquipmentCatalogDetail
            If _handlesDepreciationbyDistribution Then
                IdCostCenter = .CostCenterId
                INDsleCostCenter.Properties.NullText = .NumberNameCostCenter
                DistributionPercentage = .DistributionPercentage
            Else
                AccountingStructureId = .AccountingStructureId
            End If
            INDSlAccountingStructure.Properties.NullText = .CodeNameAccountingStructure
            IdAccountingDepreciation = .LoanSpendAccountId
            INDSlSpendDepreciationAccount.Properties.NullText = .NumberNameLoanSpendAccountingAccount
            INDSlSpendLeasingAccount.EditValue = .LoanLeasingSpendAccountId
            INDSlSpendLeasingAccount.Properties.NullText = .NumberNameLoanLeasingAccountingAccount
            INDsleExpenseLoanAccount.EditValue = .ExpenseLoanAccountId
            INDsleExpenseLoanAccount.Properties.NullText = .ExpenseLoanAccountDescription
            INDsleFinancialRentingAccount.EditValue = .LoanFinancialRentingAccountId
            INDsleFinancialRentingAccount.Properties.NullText = .LoanFinancialRentingAccountDescription
        End With
    End Sub

    ''' <summary>
    ''' Obtiene las restricciones de las cuentas seleccionadas
    ''' </summary>
    ''' <returns></returns>
    Private Function GetRestrictions() As List(Of MainAccountRestrictions)
        Dim listXpo As XPCollection(Of MainAccountRestrictionsXpo)

        Using model As New MEquipmentCatalog(_MyTag)
            listXpo = model.ListMainAccountRestriction(IdAccountingDepreciation)
        End Using

        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As MainAccountRestrictionsXpo In listXpo
                MainAccountRestrictions = New MainAccountRestrictions
                With MainAccountRestrictions
                    .Id = itemXpo.Id
                    .MainAccountId = itemXpo.MainAccountId.Id
                    .ItemType = itemXpo.ItemType
                    .CostCenterId = itemXpo.CostCenterId?.Id
                    .ThirdPartyId = itemXpo.ThirdPartyId?.Id
                    .RestrictionType = itemXpo.RestrictionType
                    .AllItems = itemXpo.AllItems
                End With

                If itemXpo.CostCenterId IsNot Nothing Then
                    MainAccountRestrictions.EntityName = itemXpo.CostCenterId.CodeName
                ElseIf itemXpo.ThirdPartyId IsNot Nothing Then
                    MainAccountRestrictions.EntityName = itemXpo.ThirdPartyId.NitName
                End If

                ListNewMainAccountRestrictions.Add(MainAccountRestrictions)
            Next
        End If
        Return ListNewMainAccountRestrictions
    End Function

#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
    ''' <summary>
    ''' Llama el formulario de Centro de Costos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la Cuenta de Gasto Depreciación 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlSpendDepreciationAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlSpendDepreciationAccount.EditValueChanged
        If _handlesDepreciationbyDistribution Then
            ValidateRestiction()
        End If
    End Sub

    ''' <summary>
    ''' Método que valida si la cuentaa tiene restricción para el centro de costos
    ''' </summary>
    Private Sub ValidateRestiction()

        If IdAccountingDepreciation <> 0 Then
            If ListNewMainAccountRestrictions Is Nothing Then
                ListNewMainAccountRestrictions = New List(Of MainAccountRestrictions)
            Else
                ListNewMainAccountRestrictions.Clear() 'Se limpia la lista de restricciones para obtener solamente las de la cuenta seleccionada
            End If
            ListNewMainAccountRestrictions = GetRestrictions()

            If IdCostCenter Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione primero un centro de costos"
                INDSlSpendDepreciationAccount.EditValue = 0
                INDSlSpendDepreciationAccount.Properties.NullText = String.Empty
                INDsleCostCenter.Focus()
                Exit Sub
            Else
                If ListNewMainAccountRestrictions.Any(Function(item) item.CostCenterId = IdCostCenter And item.RestrictionType = 2) Then
                    Mensaje(EeventViewerImages.Advertencia) = "Esta cuenta tiene restricción para el Centro de Costos seleccionado"
                    INDSlSpendDepreciationAccount.EditValue = 0
                    INDSlSpendDepreciationAccount.Properties.NullText = String.Empty
                End If
            End If
        End If

    End Sub

#End Region

End Class
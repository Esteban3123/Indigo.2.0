'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 18/06/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Portfolio.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Common
Imports Presentation.Controls.MVP
Imports Presentation.Common.MVP

#End Region

Public Class FrmPopUpDetailAccountReceivableDocument

#Region "EVENTS"
    ''' <summary>
    ''' evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddConceptAccountReceivableDocumentDetail(sender As Object, e As AddConceptAccountReceivableDocumentDetailEventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Portfolio"

    ''' <summary>
    ''' detalle de la orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountReceivableDocumentDetail As AccountReceivableDocumentDetail

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean

    ''' <summary>
    ''' listado del detalle de el documento de cuentas x cobrar 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAccountReceivableDocumentDetailValidation As List(Of AccountReceivableDocumentDetail)

    ''' <summary>
    ''' entidad de cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Dim mainAccount As Domain.Entities.MainAccounts

    ''' <summary>
    ''' entidad de centro de costo
    ''' </summary>
    ''' <remarks></remarks>
    Dim costCenter As Domain.Payroll.Entities.CostCenter

    ''' <summary>
    ''' entidad de tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim thirdParty As Domain.Entities.ThirdParty

    ''' <summary>
    ''' entidad de concepto de cuenta x cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountReceivableConcept As AccountReceivableConcept

    ''' <summary>
    ''' id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim _thirdPartyId As Integer

#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListAccountReceivableDocumentDetailValidation As List(Of AccountReceivableDocumentDetail)
        Set(value As List(Of AccountReceivableDocumentDetail))
            If value IsNot Nothing Then
                _listAccountReceivableDocumentDetailValidation = New List(Of AccountReceivableDocumentDetail)(value.ToArray())
            End If
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
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property accountReceivableDocumentDetailEdit As AccountReceivableDocumentDetail
        Set(value As AccountReceivableDocumentDetail)
            accountReceivableDocumentDetail = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            'INDsleAccountReceivableConceptId.Enabled = value
            'INDsleMainAccountId.Enabled = value
            'INDsleThirdPartyId.Enabled = value
            'INDsleCostCenterId.Enabled = value
            'INDspnValue.Enabled = value
            'INDsleNature.Enabled = value
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

    ''' <summary>
    ''' propiedad para establecer el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property thirdPartyId As Integer
        Set(value As Integer)
            _thirdPartyId = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer la abreviacion de moneda que llega desde el documento
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property CurrencyAbbreviation As String

#End Region

#Region "METHODS"

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
        accountReceivableDocumentDetailEdit = Nothing
        INDsleAccountReceivableConceptId.EditValue = Nothing
        INDsleAccountReceivableConceptId.Properties.NullText = String.Empty
        mainAccount = Nothing
        INDsleMainAccountId.EditValue = Nothing
        INDsleMainAccountId.Properties.NullText = String.Empty
        thirdParty = Nothing
        INDsleThirdPartyId.EditValue = Nothing
        INDsleThirdPartyId.Properties.NullText = String.Empty
        INDlciThirdPartyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        costCenter = Nothing
        INDsleCostCenterId.EditValue = Nothing
        INDsleCostCenterId.Properties.NullText = String.Empty
        INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleNature.EditValue = Nothing
        INDspnValue.EditValue = 0
        INDMemoObservation.EditValue = String.Empty
        'ActionsControls = False
        INDsleAccountReceivableConceptId.Focus()
        BarraBotones.FilterDataSource = Nothing
        _editMode = False
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        If _editMode = True Then
            INDBtnAddConcept.Text = ResourceManager.GetString("Edit")
        End If
        With accountReceivableDocumentDetail
            Using ModelAccountRecivableConcetp As New MAccountReceivableConcept(Me.Tag)
                accountReceivableConcept = ModelAccountRecivableConcetp.GetAccountReceivableConceptById(.AccountReceivableConceptId)
            End Using
            INDsleAccountReceivableConceptId.EditValue = .AccountReceivableConceptId
            INDsleAccountReceivableConceptId.Properties.NullText = .DescriptionAccountReceivableConcept

            INDsleMainAccountId.EditValue = .MainAccountId
            INDsleMainAccountId.Properties.NullText = .DescriptionAccount

            INDsleThirdPartyId.EditValue = .ThirdPartyId
            INDsleThirdPartyId.Properties.NullText = .DescriptionThirdParty

            INDsleCostCenterId.EditValue = .CostCenterId
            INDsleCostCenterId.Properties.NullText = .DescriptionCostCenter

            INDsleNature.EditValue = .Nature
            INDspnValue.EditValue = .Value

            INDMemoObservation.EditValue = .Observation
            'ActionsControls = True
            INDsleAccountReceivableConceptId.Focus()
        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If INDsleAccountReceivableConceptId.EditValue Is Nothing Then
            errors.AppendLine(INDlciAccountReceivableConceptId.Text + ResourceManager.GetString("Empty"))
        End If
        If INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleCostCenterId.EditValue Is Nothing Then
            errors.AppendLine(INDlciCostCenterId.Text + ResourceManager.GetString("Empty"))
        End If
        If INDsleMainAccountId.EditValue Is Nothing Then
            errors.AppendLine(INDlciMainAccountId.Text + ResourceManager.GetString("Empty"))
        End If
        If INDsleNature.EditValue Is Nothing Then
            errors.AppendLine(INDlciNature.Text + ResourceManager.GetString("Empty"))
        End If
        If INDspnValue.EditValue Is Nothing AndAlso INDspnValue.EditValue = 0 Then
            errors.AppendLine(INDlciValue.Text + ResourceManager.GetString("Empty"))
        End If
        Return errors.ToString()
    End Function

    Private Sub SetValues()
        If _editMode = False Then
            accountReceivableDocumentDetail = New AccountReceivableDocumentDetail
        End If
        With accountReceivableDocumentDetail
            .AccountReceivableConceptId = INDsleAccountReceivableConceptId.EditValue
            .AccountReceivableConcept = accountReceivableConcept
            .DescriptionAccountReceivableConcept = accountReceivableConcept.Code + " - " + accountReceivableConcept.Name
            .MainAccountId = INDsleMainAccountId.EditValue
            .DescriptionAccount = mainAccount.Number + " - " + mainAccount.Name
            If mainAccount IsNot Nothing Then
                If thirdParty IsNot Nothing Then
                    .ThirdPartyId = INDsleThirdPartyId.EditValue
                    .DescriptionThirdParty = thirdParty.Nit + " - " + thirdParty.Name
                End If
            End If
            If costCenter IsNot Nothing Then
                .CostCenterId = INDsleCostCenterId.EditValue
                .DescriptionCostCenter = costCenter.Code + " - " + costCenter.Name
            End If
            .Nature = INDsleNature.EditValue
            .Value = INDspnValue.EditValue
            .Observation = INDMemoObservation.EditValue
        End With
    End Sub

#End Region

#Region "Handles"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        accountReceivableDocumentDetail = Nothing
        _editMode = Nothing
        _listAccountReceivableDocumentDetailValidation = Nothing
        mainAccount = Nothing
        costCenter = Nothing
        thirdParty = Nothing
        accountReceivableConcept = Nothing
        _thirdPartyId = Nothing
    End Sub

    ''' <summary>
    ''' se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpDetailAccountReceivableDocument_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False
        If _editMode = True Then
            LoadControls()
        Else
            CleanControls()
        End If
        'cambios en mascara de los campos numerico de moneda
        If CurrencyAbbreviation IsNot Nothing Then
            Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = New Globalization.CultureInfo(Me.CurrencyAbbreviation.GetCultureId()).NumberFormat
            changeNumericFormatByCurrency(_culture.NumberFormat)
        End If

    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpDetailAccountReceivableDocument_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDsleAccountReceivableConceptId.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpDetailAccountReceivableDocument_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If INDsleAccountReceivableConceptId.EditValue IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleAccountReceivableConceptId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountReceivableConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCxCConcepts
                OpenFormDialog(form)
            End Using
            Using model As New MBusqueda
                Dim filter() As Object = {True}
                INDsleAccountReceivableConceptId.Properties.DataSource = model.ConsultarEntidades(eDataSource.GetAllAccountReceivableConceptByStatus, filter)
            End Using
        End If
    End Sub

    Private Sub INDsleMainAccountId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmPopupPUC
                OpenFormDialog(form)
            End Using
            Using model As New MBusqueda
                Dim filter() As Object = {5, True}
                INDsleMainAccountId.Properties.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
            End Using
        End If
    End Sub

    Private Sub INDsleThirdPartyId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdPartyId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmThirdParty
                OpenFormDialog(form)
            End Using
            Using model As New MBusqueda
                INDsleThirdPartyId.Properties.DataSource = model.ConsultarEntidades(eDataSource.ThirdParty)
            End Using
        End If
    End Sub

    Private Sub INDsleCostCenterId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenterId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCostCenter
                OpenFormDialog(form)
            End Using
            'Using model As New MBusqueda
            INDsleCostCenterId.Properties.DataSource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(eDataSource.CostCenter)
            'End Using
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDsleAccountReceivableConceptId_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleAccountReceivableConceptId.QueryPopUp
        If INDsleAccountReceivableConceptId.Properties.DataSource Is Nothing Then
            Using model As New MBusqueda
                Dim filter() As Object = {True}
                INDsleAccountReceivableConceptId.Properties.DataSource = model.ConsultarEntidades(eDataSource.GetAllAccountReceivableConceptByStatus, filter)
            End Using
        End If
    End Sub

    Private Sub INDsleMainAccountId_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleMainAccountId.QueryPopUp
        If INDsleMainAccountId.Properties.DataSource Is Nothing Then
            Using model As New MBusqueda
                Dim filter() As Object = {5, True}
                INDsleMainAccountId.Properties.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
            End Using
        End If
    End Sub

    Private Sub INDsleThirdPartyId_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleThirdPartyId.QueryPopUp
        If INDsleThirdPartyId.Properties.DataSource Is Nothing Then
            Using model As New MBusqueda
                INDsleThirdPartyId.Properties.DataSource = model.ConsultarEntidades(eDataSource.ThirdParty)
            End Using
        End If
    End Sub

    Private Sub INDsleCostCenterId_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCostCenterId.QueryPopUp
        If INDsleCostCenterId.Properties.DataSource Is Nothing Then
            'Using model As New MBusqueda
            INDsleCostCenterId.Properties.DataSource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(eDataSource.CostCenter)
            'End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Async Sub INDsleAccountReceivableConceptId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountReceivableConceptId.EditValueChanged
        If INDsleAccountReceivableConceptId Is Nothing OrElse INDsleAccountReceivableConceptId.EditValue = 0 Then
            Exit Sub
        End If
        ' consultamos el concepto para sacar la cuenta contable
        Using modelAccountConcep As New MAccountReceivableConcept(Me.Tag)
            accountReceivableConcept = modelAccountConcep.GetAccountReceivableConceptById(INDsleAccountReceivableConceptId.EditValue)
        End Using
        ' consultamos la cuenta contable
        Using modelAccount As New MPUC(Me.Tag)
            INDsleMainAccountId.EditValue = accountReceivableConcept.MainAccountId
            mainAccount = Await modelAccount.GetAccountById(accountReceivableConcept.MainAccountId, False)
            INDsleMainAccountId.Properties.NullText = mainAccount.Number + " - " + mainAccount.Name

            ' si la cuenta contable maneja tercero le sugerimos el tercero de la cabecera
            If mainAccount.HandlesThirdParty Then
                INDlciThirdPartyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlciThirdPartyId.ShowInCustomizationForm = False
                If _thirdPartyId > 0 Then
                    Using modelThirdParty As New MThirdParty(Me.Tag)
                        thirdParty = Await modelThirdParty.GetThirdPartyById(_thirdPartyId)
                        INDsleThirdPartyId.EditValue = thirdParty.Id
                        INDsleThirdPartyId.Properties.NullText = thirdParty.Nit + " - " + thirdParty.Name
                    End Using
                End If
            Else
                INDlciThirdPartyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlciThirdPartyId.ShowInCustomizationForm = True
                INDsleThirdPartyId.EditValue = Nothing
                thirdParty = Nothing
            End If
            'si la cuenta maneja centro de costo pedimos el centro de costo
            If mainAccount.HandlesCostCenter Then
                INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlciCostCenterId.ShowInCustomizationForm = False
            Else
                INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlciCostCenterId.ShowInCustomizationForm = True
                INDsleCostCenterId.EditValue = Nothing
            End If
            If accountReceivableDocumentDetail Is Nothing Then
                INDsleNature.EditValue = mainAccount.MainAccountClasses.Nature
            End If

        End Using
    End Sub

    Private Async Sub INDsleThirdPartyId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleThirdPartyId.EditValueChanged
        If INDsleThirdPartyId Is Nothing OrElse INDsleThirdPartyId.EditValue = 0 Then
            Exit Sub
        End If
        Using modelThirdParty As New MThirdParty(Me.Tag)
            thirdParty = Await modelThirdParty.GetThirdPartyById(INDsleThirdPartyId.EditValue)
        End Using
    End Sub

    Private async Sub INDsleCostCenterId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCostCenterId.EditValueChanged
        If INDsleCostCenterId Is Nothing OrElse INDsleCostCenterId.EditValue = 0 Then
            Exit Sub
        End If
        Using modelCostCenter As New MCostCenter(Me.Tag)
            costCenter = Await modelCostCenter.GetCostCenterById(INDsleCostCenterId.EditValue)
        End Using
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmPopUpDetailAccountReceivableDocument_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDspnValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDspnValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAddConcept.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAddConcept_Click(sender As Object, e As EventArgs) Handles INDBtnAddConcept.Click
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        SetValues()
        Dim args As New AddConceptAccountReceivableDocumentDetailEventArgs
        If _editMode = True Then
            args.ItemAccountReceivableDocumentDetail = accountReceivableDocumentDetail
            args.EditMode = True
        Else
            args.ItemAccountReceivableDocumentDetail = accountReceivableDocumentDetail
        End If
        RaiseEvent AddConceptAccountReceivableDocumentDetail(Nothing, args)
        If _editMode = True Then
            accountReceivableConcept = Nothing
            Me.Close()
        Else
            CleanControls()
        End If
    End Sub

#End Region

#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region

End Class
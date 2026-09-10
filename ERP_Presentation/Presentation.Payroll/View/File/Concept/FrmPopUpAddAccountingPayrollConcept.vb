Imports Presentation.Payroll.MVP
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports System.Drawing
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls.MVP

Public Class FrmPopUpAddAccountingPayrollConcept

#Region "EVENTS"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddConceptAccountingPayrollEventArgs(sender As Object, e As AddConceptAccountingPayrollEventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Payroll"

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim ConceptAccountingStructure As ConceptAccountingStructure

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
    Dim _listConceptAccountingStructureImportInfo As List(Of ConceptAccountingStructure)
    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listConceptAccountingStructureValidation As List(Of ConceptAccountingStructure)


    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _operatingUnitId As Integer

    Dim _MyTag As String

    Dim _conceptType As Integer

    Dim _conceptClass As String

#End Region

#Region "Propiedades"
    ''' <summary>
    ''' datasource de la Estructura Contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountingStructureXPO As List(Of AccountingStructure)
        Get
            Return INDSlAccountingStructure.Properties.DataSource
        End Get
        Set(value As List(Of AccountingStructure))
            INDSlAccountingStructure.Properties.DataSource = value
        End Set
    End Property


    'Property AccountingDepreciationXPO As XPInstantFeedbackSource
    '    Get
    '        Return CType(INDSlSpendDepreciationAccount.Properties.DataSource, XPInstantFeedbackSource)
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        INDSlSpendDepreciationAccount.Properties.DataSource = value
    '    End Set
    'End Property

    'Property AccountingLeasingXPO As XPInstantFeedbackSource
    '    Get
    '        Return CType(INDSlSpendLeasingAccount.Properties.DataSource, XPInstantFeedbackSource)
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        INDSlSpendLeasingAccount.Properties.DataSource = value
    '    End Set
    'End Property

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
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListConceptAccountingStructureValidation As List(Of ConceptAccountingStructure)
        Set(value As List(Of ConceptAccountingStructure))
            If value IsNot Nothing Then
                _listConceptAccountingStructureValidation = New List(Of ConceptAccountingStructure)(value.ToArray())
            End If
        End Set
    End Property
    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListConceptAccountingStructureImportInfo As List(Of ConceptAccountingStructure)
        Set(value As List(Of ConceptAccountingStructure))
            _listConceptAccountingStructureImportInfo = value
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
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ConceptType As Integer
        Set(value As Integer)
            _conceptType = value
        End Set
    End Property

    Public WriteOnly Property ConceptClass As String
        Set(value As String)
            _conceptClass = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property ConceptAccountingStructureEdit As ConceptAccountingStructure
        Get
            Return ConceptAccountingStructure
        End Get
        Set(value As ConceptAccountingStructure)
            ConceptAccountingStructure = value
        End Set
    End Property


    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDSlAccountingStructure.Enabled = value
            IndSlAccountDebit.Enabled = value
            IndSlAccountCredit.Enabled = value
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

    Public Property AccountDebitVieXpo As XPInstantFeedbackSource
        Get
            Return CType(IndSlAccountDebit.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            IndSlAccountDebit.Properties.DataSource = value
        End Set
    End Property

    Public Property AccountCreditVieXpo As XPInstantFeedbackSource
        Get
            Return CType(IndSlAccountCredit.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            IndSlAccountCredit.Properties.DataSource = value
        End Set
    End Property

    Public Property InabilityDebitValueEmployeeAccountXpo As XPInstantFeedbackSource
        Get
            Return CType(INDSlInabilityDebitValueEmployeeAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlInabilityDebitValueEmployeeAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property InabilityDebitValueEPSAccountVieXpo As XPInstantFeedbackSource
        Get
            Return CType(INDSlInabilityDebitValueEPSAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlInabilityDebitValueEPSAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property DataSourceBranch As List(Of Domain.Entities.GlosasParametersInterface)
        Get
            Return INDglCompany.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.GlosasParametersInterface))
            INDglCompany.Properties.DataSource = value
            If value IsNot Nothing AndAlso value.Count = 1 Then
                INDglCompany.EditValue = value(0).ContainerName
            End If
        End Set
    End Property
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
        ConceptAccountingStructure = Nothing
        'ActionsControls = False
        IndSlAccountDebit.EditValue = 0
        INDSlAccountingStructure.EditValue = 0
        IndSlAccountCredit.EditValue = 0
        INDSlInabilityDebitValueEmployeeAccount.EditValue = 0
        INDSlInabilityDebitValueEPSAccount.EditValue = 0
        ListConceptAccountingStructureImportInfo = Nothing
        ListConceptAccountingStructureValidation = Nothing
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
            ConceptAccountingStructure = New ConceptAccountingStructure
        End If

        If indigo.IndigoPayrollIntegration = 2 Then
            With ConceptAccountingStructure
                .AccountingStructureId = INDSlAccountingStructure.EditValue
                .AccruedAccount = INDSlDebitAccountDinamica.Text
                .DeductedAccount = INDSlCreditAccountDinamica.Text
                .InterfazName = INDglCompany.EditValue
                .NameAccountingStructure = INDSlAccountingStructure.Text
            End With
        Else
            With ConceptAccountingStructure
                .AccountingStructureId = INDSlAccountingStructure.EditValue
                .AccruedAccount = IndSlAccountDebit.Text
                .DeductedAccount = IndSlAccountCredit.Text
                .InterfazName = indigo.TransactionalContainer
                .NameAccountingStructure = INDSlAccountingStructure.Text
                .InabilityDebitValueEmployeeAccount = INDSlInabilityDebitValueEmployeeAccount.Text
                .InabilityDebitValueEPSAccount = INDSlInabilityDebitValueEPSAccount.Text
            End With
        End If

    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="AdministrativeLoanAccountingInformationTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls(Optional ConceptAccountingStructureTmp As ConceptAccountingStructure = Nothing)

        If _editMode = True Then
            INDBtnAdd.Text = ResourceManager.GetString("Edit")
        End If
        If _importDataMode = True Then
            FixedAssetEquipmentCatalogDetail = ConceptAccountingStructureTmp
        End If
        If _editMode = True Then

            If indigo.IndigoPayrollIntegration = 1 Then

                With ConceptAccountingStructureTmp
                    INDSlAccountingStructure.EditValue = .AccountingStructureId
                    IndSlAccountCredit.Properties.NullText = .DeductedAccount
                    IndSlAccountDebit.Properties.NullText = .AccruedAccount
                    INDSlInabilityDebitValueEmployeeAccount.Properties.NullText = .InabilityDebitValueEmployeeAccount
                    INDSlInabilityDebitValueEPSAccount.Properties.NullText = .InabilityDebitValueEPSAccount
                End With
            Else
                With ConceptAccountingStructureTmp
                    INDSlAccountingStructure.EditValue = .AccountingStructureId
                    INDglCompany.Text = .InterfazName
                    INDSlCreditAccountDinamica.Properties.NullText = .DeductedAccount
                    INDSlDebitAccountDinamica.Properties.NullText = .AccruedAccount
                End With
            End If

        End If
    End Sub

#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ConceptAccountingStructure = Nothing
        _editMode = Nothing
        _importDataMode = Nothing
        _listConceptAccountingStructureImportInfo = Nothing
        _listConceptAccountingStructureValidation = Nothing
        _operatingUnitId = Nothing
        _MyTag = Nothing
        _conceptClass = Nothing
        _conceptType = Nothing
    End Sub
    Private Async Sub FrmPopUpAddAccountingDepreciation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        If _conceptType = 2 Then
            'Conceptos Deducidos, únicamente se pide Cuenta Crédito

            INDSlDebitAccountDinamica.Enabled = False
            INDSlDebitAccountDinamica.Text = String.Empty
            IndSlAccountDebit.Enabled = False
            IndSlAccountDebit.Text = String.Empty
        Else
            INDSlDebitAccountDinamica.Enabled = True
            IndSlAccountDebit.Enabled = True
        End If




        Using model As New MConcept(_MyTag)

            AccountingStructureXPO = Await model.ListAccountingStructure()

            If indigo.IndigoPayrollIntegration = 2 Then

                INDLciDebitAccountDinamica.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciCreditDinamica.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciCreditAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciDebitAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                INDglCompany.Properties.DataSource = Await model.GetBranchAll()
                'INDSlAccountingStructure.Properties.DataSource = Await model.ListAccountingStructure()

            Else
                INDlyiContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciDebitAccountDinamica.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCreditDinamica.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCreditAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciDebitAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Using msearch As New MBusqueda()
                    Dim filter() As Object = {5, True}
                    AccountDebitVieXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
                    AccountCreditVieXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
                    InabilityDebitValueEPSAccountVieXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
                    InabilityDebitValueEmployeeAccountXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
                End Using

            End If
        End Using
        Dim conceptClassCodes As String() = {"021", "022", "023", "024", "027", "067", "068", "069", "070", "075", "076"}
        If conceptClassCodes.Contains(_conceptClass) Then
            INDLciInabilityDebitValueEmployeeAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciInabilityDebitValueEPSAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDebitAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDLciInabilityDebitValueEmployeeAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciInabilityDebitValueEPSAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        End If

        If _editMode = True Then
            'si esta editando un registro
            LoadControls(ConceptAccountingStructureEdit)
        Else
            'si es un nuevo registro
            CleanControls()
        End If
    End Sub
#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region



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
            If _listConceptAccountingStructureValidation Is Nothing Then
                _listConceptAccountingStructureValidation = New List(Of ConceptAccountingStructure)
            End If
            Dim args As New AddConceptAccountingPayrollEventArgs
            If _editMode = True Then
                args.ConceptAccountingStructure = ConceptAccountingStructure
                args.EditMode = True
            Else
                args.ConceptAccountingStructure = ConceptAccountingStructure

                'Se asigna el código porque no lo está mostrando en la rejilla del form principal
                If INDSlAccountingStructure.EditValue IsNot Nothing AndAlso AccountingStructureXPO IsNot Nothing AndAlso AccountingStructureXPO.Count > 0 Then
                    args.ConceptAccountingStructure.CodeAccountingStructure = (From x In AccountingStructureXPO Where x.Id = INDSlAccountingStructure.EditValue Select x.Code).FirstOrDefault
                End If

                _listConceptAccountingStructureValidation.Add(ConceptAccountingStructure)
            End If
            If _importDataMode = True Then
            End If
            RaiseEvent AddConceptAccountingPayrollEventArgs(Nothing, args)

            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            CleanControls()
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup(FlagTypeCatalog As String) As String
        Dim errors As New StringBuilder

        If INDSlAccountingStructure.EditValue = 0 Then
            errors.AppendLine(INDSlAccountingStructure.Name + ResourceManager.GetString("Empty"))
        End If

        If indigo.IndigoPayrollIntegration = 1 Then

            If _conceptType <> 2 Then
                If INDLciDebitAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If IndSlAccountDebit.Text = String.Empty Then
                        errors.AppendLine(IndSlAccountDebit.Name + ResourceManager.GetString("Empty"))
                    End If
                End If
            End If

            If IndSlAccountCredit.Text = String.Empty Then
                errors.AppendLine(IndSlAccountCredit.Name + ResourceManager.GetString("Empty"))
            End If
        Else
            If _conceptType <> 2 Then
                If INDSlDebitAccountDinamica.Text = String.Empty Then
                    errors.AppendLine(IndSlAccountDebit.Name + ResourceManager.GetString("Empty"))
                End If
            End If

            If INDSlCreditAccountDinamica.Text = String.Empty Then
                errors.AppendLine(IndSlAccountCredit.Name + ResourceManager.GetString("Empty"))
            End If
        End If

        If _listConceptAccountingStructureValidation IsNot Nothing AndAlso _listConceptAccountingStructureValidation.Count > 0 Then
            Dim remissionEntranceDetailTmp = _listConceptAccountingStructureValidation.Find(Function(x) x.AccountingStructureId = INDSlAccountingStructure.EditValue)
            If remissionEntranceDetailTmp IsNot Nothing Then
                errors.AppendLine("Ya existe una Información Contable con esa estructura Contable Seleccionada")
            End If
        End If

            Return errors.ToString()
    End Function

    Private Sub FrmPopUpAddAccountingDepreciation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If FixedAssetEquipmentCatalogDetail IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If

    End Sub

    Private Sub FrmPopUpAddAccountingDepreciation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

 
    Private Async Sub INDglCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDglCompany.EditValueChanged
        If INDglCompany.EditValue IsNot Nothing Then
            Using model As New MConcept(_MyTag)
                INDSlDebitAccountDinamica.Properties.DataSource = Await model.ListAccounts(INDglCompany.EditValue)
                INDSlCreditAccountDinamica.Properties.DataSource = Await model.ListAccounts(INDglCompany.EditValue)
            End Using
        End If
    End Sub

End Class
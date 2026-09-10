'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 15-12-2014
'
' Last Modified By : Nicolas Pulido
' Last Modified On : 13-02-2017
' Description      : Solución de error al guardar un centro de producción, solución de Error al eliminar un centro de costo y guardarlo en la base de datos 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Cost.MVP

#End Region

Public Class FrmCostProductionCenter
    Implements ICostProductionCenter

#Region "Properties and Variables"

    Public Property CancellationCostMainAccountId As Integer? Implements ICostProductionCenter.CancellationCostMainAccountId
        Get
            Return INDsleCancellationCostMainAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleCancellationCostMainAccountId.EditValue = value
        End Set
    End Property

    Public Property CancellationCostMainAccountXpo As XPInstantFeedbackSource Implements ICostProductionCenter.CancellationCostMainAccountXpo
        Get
            Return INDsleCancellationCostMainAccountId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCancellationCostMainAccountId.Properties.DataSource = value
        End Set
    End Property

#Region "Poperties Entity"
    ''' <summary>
    ''' Obtiene o establece el código del centro de produccion
    ''' </summary>
    Public Property Code As String Implements ICostProductionCenter.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del centro de produccion
    ''' </summary>
    Public Property NameProductionCenter As String Implements ICostProductionCenter.NameProductionCenter
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el area
    ''' </summary>
    Public Property Area As Decimal Implements ICostProductionCenter.Area
        Get
            Return INDspnArea.EditValue
        End Get
        Set(value As Decimal)
            INDspnArea.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo del centro
    ''' </summary>
    Public Property CenterType As Byte Implements ICostProductionCenter.CenterType
        Get
            Return INDgleCenterType.EditValue
        End Get
        Set(value As Byte)
            INDgleCenterType.EditValue = value
        End Set
    End Property

    Public Property ListCostCenter As List(Of CostProductionCenterCostCenter)
        Get
            Return CType(INDGcCostCenter.DataSource, List(Of CostProductionCenterCostCenter))
        End Get
        Set(value As List(Of CostProductionCenterCostCenter))
            INDGcCostCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista temporal usada en los centros de costo
    ''' </summary>
    Public Property listpreSaveCostCenter = New List(Of CostProductionCenterCostCenter)

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    Public Property Description As String Implements ICostProductionCenter.Description
        Get
            Return INDmeDescription.Text
        End Get
        Set(value As String)
            INDmeDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la estructura organizacional
    ''' </summary>
    Public Property OrganizationalStructureOfCostId As Integer Implements ICostProductionCenter.OrganizationalStructureOfCostId
        Get
            Return INDsleOrganizationalStructure.EditValue
        End Get
        Set(value As Integer)
            INDsleOrganizationalStructure.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la categoria
    ''' </summary>
    Public Property CategoryId As Integer? Implements ICostProductionCenter.CategoryId
        Get
            Return INDsleCategory.EditValue
        End Get
        Set(value As Integer?)
            INDsleCategory.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements ICostProductionCenter.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property
#End Region

#Region "Datasource"

    ''' <summary>
    ''' Gets or sets the organizational structure datasourse.
    ''' </summary>
    Public Property OrganizationalStructureDatasourse As DevExpress.Xpo.XPInstantFeedbackSource Implements ICostProductionCenter.OrganizationalStructureDatasourse
        Get
            Return CType(INDsleOrganizationalStructure.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleOrganizationalStructure.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the categories datasourse.
    ''' </summary>
    Public Property CategoryDatasourse As DevExpress.Xpo.XPInstantFeedbackSource Implements ICostProductionCenter.CategoryDatasourse
        Get
            Return CType(INDsleCategory.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCategory.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the cost center datasource.
    ''' </summary>
    Public Property CostCenterDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ICostProductionCenter.CostCenterDatasource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

#End Region
    ''' <summary>
    ''' Indica que se estan cargando los datos
    ''' </summary>
    Private _isLoading As Boolean

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Cost"

    ''' <summary>
    ''' The _state open pop up deprecation
    ''' </summary>
    Private _stateOpenPopUpDeprecation As Boolean

    ''' <summary>
    ''' bandera para conocer si se abre por primera vez el popup de gastos generales
    ''' </summary>
    Private _statePopUpGeneralExpenses As Boolean

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.CostSecuence

    Private _functionalUnitXpo As Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit

    Private _costCenterXpo As Infrastructure.Data.Xpo.PayrollRepository.PayrollCostCenterXpo

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    Private model As MCostProductionCenter

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequence As CostSecuence Implements ICostProductionCenter.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As CostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.CostSecuenceDetail In Me._sequence.CostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostProductionCenter.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICostProductionCenter.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim _productionCenter As CostProductionCenter

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PCostProductionCenter

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordCost

    ''' <summary>
    ''' Lista los tipos de centro
    ''' </summary>
    Private _centerTypeList As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por mano de obra
    ''' </summary>
    Public Property ListHomologationLabor As List(Of CostProductionCenterHomologation) Implements ICostProductionCenter.ListHomologationLabor
        Get
            Return CType(INDgcLabor.DataSource, List(Of CostProductionCenterHomologation))
        End Get
        Set(value As List(Of CostProductionCenterHomologation))
            INDgcLabor.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por suministro
    ''' </summary>
    Public Property ListHomologationSupply As List(Of CostProductionCenterHomologation) Implements ICostProductionCenter.ListHomologationSupply
        Get
            Return CType(INDgcSupply.DataSource, List(Of CostProductionCenterHomologation))
        End Get
        Set(value As List(Of CostProductionCenterHomologation))
            INDgcSupply.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por consumo
    ''' </summary>
    Public Property ListHomologationConsumption As List(Of CostProductionCenterHomologation) Implements ICostProductionCenter.ListHomologationConsumption
        Get
            Return CType(INDgcConsumption.DataSource, List(Of CostProductionCenterHomologation))
        End Get
        Set(value As List(Of CostProductionCenterHomologation))
            INDgcConsumption.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por egresos generales
    ''' </summary>
    Public Property ListHomologationGeneralExpenses As List(Of CostProductionCenterHomologation) Implements ICostProductionCenter.ListHomologationGeneralExpenses
        Get
            Return CType(INDgcGeneralExpenses.DataSource, List(Of CostProductionCenterHomologation))
        End Get
        Set(value As List(Of CostProductionCenterHomologation))
            INDgcGeneralExpenses.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por depreciacion
    ''' </summary>
    Public Property ListHomologationDeprecation As List(Of CostProductionCenterHomologation) Implements ICostProductionCenter.ListHomologationDeprecation
        Get
            Return CType(INDgcDeprecation.DataSource, List(Of CostProductionCenterHomologation))
        End Get
        Set(value As List(Of CostProductionCenterHomologation))
            INDgcDeprecation.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por depreciacion
    ''' </summary>
    Public Property ListHomologationSales As List(Of CostProductionCenterHomologation) Implements ICostProductionCenter.ListHomologationSales
        Get
            Return CType(INDgcSales.DataSource, List(Of CostProductionCenterHomologation))
        End Get
        Set(value As List(Of CostProductionCenterHomologation))
            INDgcSales.DataSource = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(status As eStatusResult) As String
        Set(value As String)
            If status = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf status = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf status = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostProductionCenter.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleOrganizationalStructure.Enabled = value
            INDsleCategory.Enabled = value
            INDsleCostCenter.Enabled = value
            INDspnArea.Enabled = value
            INDgleCenterType.Enabled = value
            INDsleCancellationCostMainAccountId.Enabled = value
            INDmeDescription.Enabled = value

            INDpceAddDeprecation.Enabled = value
            INDpceAddLabor.Enabled = value
            INDpceAddSupply.Enabled = value
            INDpceConsumption.Enabled = value
            INDpceGeneralExpenses.Enabled = value
            INDpceAddSales.Enabled = value
            INDgcConsumption.Enabled = value
            INDgcDeprecation.Enabled = value
            INDgcGeneralExpenses.Enabled = value
            INDgcLabor.Enabled = value
            INDgcSales.Enabled = value
            INDgcSupply.Enabled = value
            INDGcCostCenter.Enabled = value
            INDSbAddCostCenter.Enabled = value

            Me.BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDgcServiceArea_DataSourceChanged(Nothing, Nothing)
                INDgcDeprecation_DataSourceChanged(Nothing, Nothing)
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property
#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _isLoading = Nothing
        _stateOpenPopUpDeprecation = Nothing
        _statePopUpGeneralExpenses = Nothing
        _sequence = Nothing
        _functionalUnitXpo = Nothing
        _costCenterXpo = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        model = Nothing
        _productionCenter = Nothing
        _presenter = Nothing
        _record = Nothing
        _centerTypeList = Nothing
    End Sub


    ''' <summary>
    ''' Handles the Load event of the FrmProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductionCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PCostProductionCenter(Me)
        model = New MCostProductionCenter(Me.Tag)
        _presenter.GetSequence()
        _presenter.LoadDefinitionLayout()

        SetActionsGrid()
        AddHandler CtrProductionCenter.AddHomologation, AddressOf AddHomologation
        AddHandler CtrProductionCenter.MainAccountOrigin.QueryPopUp, AddressOf MainAccountOriginQueryPopUp
        AddHandler CtrProductionCenter.MainAccountDestination.QueryPopUp, AddressOf MainAccountDestinationQueryPopUp

        CreateCenterType()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The instance containing the event data.</param>
    Private Sub FrmProductionCenter_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
    Dim LegalBookId As Integer
    Private Sub FrmProductionCenter_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim legalBook = model.GetLegalBook()
        If legalBook IsNot Nothing Then
            LegalBookId = legalBook.Id
            CtrProductionCenter.LegalBookId = LegalBookId
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No existe un libro oficial."
            LegalBookId = Nothing
        End If
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
#End Region

#Region "QueryPopUp"

    Private Sub INDsleCancellationCostMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCancellationCostMainAccountId.QueryPopUp
        If CancellationCostMainAccountXpo Is Nothing Then
            _presenter.ListMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleOrganizationalStructure_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleOrganizationalStructure.QueryPopUp
        If INDsleOrganizationalStructure.Properties.DataSource Is Nothing Then
            INDsleOrganizationalStructure.Properties.DataSource = model.ListOrganizationalStructure()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCategory control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCategory_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCategory.QueryPopUp
        If INDsleCategory.Properties.DataSource Is Nothing Then
            INDsleCategory.Properties.DataSource = model.ListCategories()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            INDsleCostCenter.Properties.DataSource = model.ListCostCenter()
        End If
    End Sub

    Private Sub INDrpMainAccountOriginSupply_QueryPopUp(sender As Object, e As CancelEventArgs)
        'If CtrProductionCenterSupply.DatasourceMainAccountOrigin Is Nothing Then
        '    Dim filter As List(Of Integer) = ListServiceArea.Select(Function(x) CInt(x.ServiceAreaId)).ToList()
        '    Me.MainAccountOriginConsumption = model.ListMainAccountSupplyByServiceAreaList(filter)
        '    Me.MainAccountOriginConsumptionRepository = model.ListMainAccountSupplyByServiceAreaList(filter)
        'End If
    End Sub

    Private Sub INDPceAddAccounts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddLabor.QueryPopUp, INDpceAddSupply.QueryPopUp, INDpceConsumption.QueryPopUp, INDpceGeneralExpenses.QueryPopUp, INDpceAddDeprecation.QueryPopUp, INDpceAddSales.QueryPopUp
        If CType(sender, PopupContainerEdit).Properties.PopupControl Is Nothing Then
            CType(sender, PopupContainerEdit).Properties.PopupControl = INDPccAddAccounts
            CtrProductionCenter.MainAccountOrigin.Properties.DataSource = Nothing
        End If
        'Cuenta destino solo aplica para costos integrados
        CtrProductionCenter.ShowSecondaryDistribution = sender.Name = INDpceAddSales.Name
        CtrProductionCenter.INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDbteCode.Text) Then
                    LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(INDbteCode.Text) Then
                    Me.NewProductionCenter()
                Else
                    LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleOrganizationalStructure_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleOrganizationalStructure.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1728", Nothing, True)
            INDsleOrganizationalStructure.Properties.DataSource = model.ListOrganizationalStructure()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCategory control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCategory_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCategory.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("2078", Nothing, True)
            INDsleCategory.Properties.DataSource = model.ListCategories()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCancellationCostMainAccountId control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCancellationCostMainAccountId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCancellationCostMainAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            _presenter.ListMainAccount()
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCostCenter.EditValueChanged
        If _isLoading Then
            INDpceAddDeprecation.Enabled = True
            Exit Sub
        End If
        If CType(INDsleCostCenter.EditValue, Integer) <> 0 Then
            _costCenterXpo = CType(CType(INDgvCostCenter.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PayrollRepository.PayrollCostCenterXpo)
            'INDsleFunctionalUnit.Properties.DataSource = Nothing
        Else
            _costCenterXpo = Nothing
        End If
    End Sub

    Private Sub INDsleServiceArea_EditValueChanged(sender As Object, e As EventArgs)
        '_functionalUnitXpo = CType(CType(INDgvServiceAreaDinamics.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit)
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Acción al agregar un centro de costo al GridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddCostCenter_Click(sender As Object, e As EventArgs) Handles INDSbAddCostCenter.Click
        If INDsleCostCenter.EditValue IsNot Nothing Then
            Dim costCenterId As Integer = CType(INDsleCostCenter.EditValue, Integer)
            If ListCostCenter IsNot Nothing AndAlso ListCostCenter.Count > 0 AndAlso ListCostCenter.Where(Function(x) x.CostCenterId = costCenterId).Count() > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Este centro de costo ya está en el listado"
                Exit Sub
            End If
            Dim _costCenter As New CostProductionCenterCostCenter()
            With _costCenter
                .CostCenterId = _costCenterXpo.Id
                .CostCenterCode = _costCenterXpo.Codigo
                .CostCenterName = _costCenterXpo.Descripcion
            End With
            _productionCenter.CostProductionCenterCostCenter.Add(_costCenter)
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
            ListCostCenter = _productionCenter.CostProductionCenterCostCenter.ToList()
            INDGcCostCenter.RefreshDataSource()
            INDGcCostCenter.DataSource = ListCostCenter
            INDsleCostCenter.EditValue = Nothing
            _costCenterXpo = Nothing
            INDsleCostCenter.Focus()
        End If
    End Sub

#End Region

#Region "IdEntity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._productionCenter IsNot Nothing AndAlso Me._productionCenter.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "DatasourceChanged"
    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcServiceArea control.
    ''' </summary>
    Private Sub INDgcServiceArea_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcSales.DataSourceChanged
        'INDpceAddSupply.Enabled = (ListFunctionalUnit IsNot Nothing AndAlso ListFunctionalUnit.Count > 0)
        'INDpceConsumption.Enabled = (ListFunctionalUnit IsNot Nothing AndAlso ListFunctionalUnit.Count > 0)
        'INDsleCostCenter.Properties.ReadOnly = (ListFunctionalUnit IsNot Nothing AndAlso ListFunctionalUnit.Count > 0)
        ' INDGVCostCenters.Columns.Where(Function(x) x.Name = "colActions").FirstOrDefault().Visible = (ListFunctionalUnit Is Nothing OrElse ListFunctionalUnit.Count = 0) AndAlso (ListHomologationDeprecation Is Nothing OrElse ListHomologationDeprecation.Count = 0)
    End Sub

    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDeprecation control.
    ''' </summary>
    Private Sub INDgcDeprecation_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcDeprecation.DataSourceChanged
        'INDsleCostCenter.Enabled = Not (ListHomologationDeprecation IsNot Nothing AndAlso ListHomologationDeprecation.Count > 0)
        'INDGVCostCenters.Columns.Where(Function(x) x.Name = "colActions").FirstOrDefault().Visible = (ListFunctionalUnit Is Nothing OrElse ListFunctionalUnit.Count = 0) AndAlso (ListHomologationDeprecation Is Nothing OrElse ListHomologationDeprecation.Count = 0)
    End Sub
    Private Sub INDGcCostCenter_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcCostCenter.DataSourceChanged
        INDpceAddDeprecation.Enabled = ListCostCenter IsNot Nothing AndAlso ListCostCenter.Any()
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridViewServiceArea_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridViewServiceArea_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewServiceArea.Click_ButtonAction, IndigoGridViewServiceArea.ContexMenuActions
        If sender.Tag = "Edit" Then
            EditSaleItem()
        Else
            RemoveSaleItem()
        End If
    End Sub

    Private Sub EditSaleItem()
        Dim _productionCenterSales = INDgvSales.GetFocusedObject(Of CostProductionCenterHomologation)()
        CtrProductionCenter.LoadHomologation(_productionCenterSales)
        INDpceAddSales.ShowPopup()
    End Sub

    Private Sub RemoveSaleItem()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _productionCenterSales As CostProductionCenterHomologation = CType(INDgvSales.GetFocusedRow(), CostProductionCenterHomologation)
            _productionCenterSales.MarkAsDeleted()
            ListHomologationSales.Remove(_productionCenterSales)
            INDgcSales.RefreshDataSource()
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridViewLabor_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridViewLabor_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewLabor.Click_ButtonAction, IndigoGridViewLabor.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _productionCenterHomologation As CostProductionCenterHomologation = CType(INDgvLabor.GetFocusedRow(), CostProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationLabor = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 1).ToList()
            INDgcLabor.RefreshDataSource()
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridViewSupply_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridViewSupply_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewSupply.Click_ButtonAction, IndigoGridViewSupply.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _productionCenterHomologation As CostProductionCenterHomologation = CType(INDgvSupply.GetFocusedRow(), CostProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationSupply = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 2).ToList()
            INDgcSupply.RefreshDataSource()
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridViewConsumption_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridViewConsumption_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewConsumption.Click_ButtonAction, IndigoGridViewConsumption.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _productionCenterHomologation As CostProductionCenterHomologation = CType(INDgvConsumption.GetFocusedRow(), CostProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationConsumption = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 3).ToList()
            INDgcConsumption.RefreshDataSource()
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridViewGeneralData_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridViewGeneralData_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewGeneralData.Click_ButtonAction, IndigoGridViewGeneralData.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _productionCenterHomologation As CostProductionCenterHomologation = CType(INDgvGeneralData.GetFocusedRow(), CostProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationGeneralExpenses = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 4).ToList()
            INDgcGeneralExpenses.RefreshDataSource()
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridViewDeprecation_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridViewDeprecation_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewDeprecation.Click_ButtonAction, IndigoGridViewDeprecation.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _productionCenterHomologation As CostProductionCenterHomologation = CType(INDgvDeprecation.GetFocusedRow(), CostProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationDeprecation = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 5).ToList()
            INDgcDeprecation.RefreshDataSource()
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub IndigoGridViewCostCenter_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewCostCenter.Click_ButtonAction, IndigoGridViewCostCenter.ContexMenuActions


        'Valida si se elimina el registro seleccionado del grid
        If MessageIndigo.Show("Desea eliminar el registro?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        'Obtiene el campo a eliminar del grid
        Dim row As CostProductionCenterCostCenter = INDGVCostCenters.GetFocusedRow()
        If row IsNot Nothing Then
            Dim _productionCenterCostCenter As CostProductionCenterCostCenter = CType(INDGVCostCenters.GetFocusedRow(), CostProductionCenterCostCenter)
            _productionCenterCostCenter.MarkAsDeleted()
            ListCostCenter = _productionCenter.CostProductionCenterCostCenter.Where(Function(x) x.ProductionCenterId = row.ProductionCenterId).ToList()
            INDGcCostCenter.RefreshDataSource()
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If

        End If

        'If (ListCostCenter Is Nothing OrElse ListCostCenter.Count = 0) AndAlso (ListCostCenter Is Nothing OrElse ListCostCenter.Count = 0) Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Dim _costCenter As CostProductionCenterCostCenter = CType(INDGVCostCenters.GetFocusedRow(), CostProductionCenterCostCenter)
        '        _costCenter.MarkAsDeleted()
        '        ListCostCenter = _productionCenter.CostProductionCenterCostCenter.ToList()
        '        INDGcCostCenter.RefreshDataSource()
        '        If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
        '            _productionCenter.MarkAsModified()
        '        End If
        '        'INDsleFunctionalUnit.Properties.DataSource = Nothing
        '    End If
        'Else
        '    Mensaje(eStatusResult.WARNING) = "Para poder eliminar los centros de costo primero se deben eliminar las unidades funcionales y los registros de depreciación"
        'End If
    End Sub
#End Region

#Region "Closed"

    Private Sub INDPceAddAccount_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceAddSales.Closed, INDpceAddLabor.Closed, INDpceAddSupply.Closed, INDpceConsumption.Closed, INDpceGeneralExpenses.Closed, INDpceAddDeprecation.Closed
        CtrProductionCenter.ShowSecondaryDistribution = False
        CtrProductionCenter.CleanControls()
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub MainAccountOriginQueryPopUp(sender As Object, e As CancelEventArgs)
        If CtrProductionCenter.MainAccountOrigin.Properties.DataSource Is Nothing Then
            Select Case INDPccAddAccounts.OwnerEdit.Name
                Case INDpceAddLabor.Name
                    If ListCostCenter IsNot Nothing AndAlso ListCostCenter.Any() Then
                        CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountsByStatusAndBookIdAndClass(True, LegalBookId, {5, 6, 7}.ToList, True)
                    End If
                Case INDpceAddSupply.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountsByStatusAndBookIdAndClass(True, LegalBookId, {5, 6, 7}.ToList, True)
                Case INDpceConsumption.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountsByStatusAndBookIdAndClass(True, LegalBookId, {5, 6, 7}.ToList, True)
                Case INDpceGeneralExpenses.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountsByStatusAndBookIdAndClass(True, LegalBookId, {5, 6, 7}.ToList, True)
                Case INDpceAddDeprecation.Name
                    If ListCostCenter IsNot Nothing AndAlso ListCostCenter.Any() Then
                        CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountDeprecation(ListCostCenter.Select(Function(x) x.CostCenterId).ToList(), LegalBookId)
                    End If
                Case INDpceAddSales.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountsByStatusAndBookIdAndClass(True, LegalBookId, {4}.ToList, True)
            End Select
        End If
    End Sub

    Private Sub MainAccountDestinationQueryPopUp(sender As Object, e As CancelEventArgs)
        If CtrProductionCenter.MainAccountDestination.Properties.DataSource Is Nothing Then
            CtrProductionCenter.MainAccountDestination.Properties.DataSource = model.ListMainAccountsByStatusAndBookIdAndClass(True, LegalBookId, {5, 6}.ToList, True)
        End If
    End Sub

    ''' <summary>
    ''' Nuevo centro de producción
    ''' </summary>
    Private Async Sub NewProductionCenter()
        If Sequence Is Nothing OrElse Sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        Me._productionCenter = New CostProductionCenter() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.CostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.CostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense.ContainsKey(CInt(Me._idCurrentSequence)) = True AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MCommonCost(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Sub
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        End If
        Me.ActionsOnControls = True
    End Sub

    ''' <summary>
    ''' Creates the type of the center.
    ''' </summary>
    Private Sub CreateCenterType()
        _centerTypeList = New List(Of Tuple(Of Integer, String))()
        _centerTypeList.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("ProductionCenterTypeOperating", "InteropCost")))
        _centerTypeList.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("ProductionCenterTypeAdministrative", "InteropCost")))
        _centerTypeList.Add(New Tuple(Of Integer, String)(3, "Logístico"))
        INDgleCenterType.Properties.DataSource = _centerTypeList
    End Sub

    ''' <summary>
    ''' Sets the actions grid.
    ''' </summary>
    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridViewServiceArea.SetListAcction(INDgvSales, {eAcciones.Edit, eAcciones.Remove}.ToList())
        IndigoGridViewLabor.SetListAcction(INDgvLabor, _listActions)
        IndigoGridViewSupply.SetListAcction(INDgvSupply, _listActions)
        IndigoGridViewConsumption.SetListAcction(INDgvConsumption, _listActions)
        IndigoGridViewGeneralData.SetListAcction(INDgvGeneralData, _listActions)
        IndigoGridViewDeprecation.SetListAcction(INDgvDeprecation, _listActions)
        IndigoGridViewCostCenter.SetListAcction(INDGVCostCenters, _listActions)
        For Each col As GridColumn In INDgvSales.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next
        For Each col As GridColumn In INDgvLabor.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next
        For Each col As GridColumn In INDgvSupply.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next
        For Each col As GridColumn In INDgvConsumption.Columns
            If col.Name = "colActions" Then
                col.Width = 40
            End If
        Next
        For Each col As GridColumn In INDgvGeneralData.Columns
            If col.Name = "colActions" Then
                col.Width = 40
            End If
        Next
        For Each col As GridColumn In INDgvDeprecation.Columns
            If col.Name = "colActions" Then
                col.Width = 40
            End If
        Next
        For Each col As GridColumn In INDGVCostCenters.Columns
            If col.Name = "colActions" Then
                col.Width = 240
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7},
                              New ColumnInfo() With {.Caption = "Estructura", .FieldName = "OrganizationalStructureOfCostId.Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostProductionCenter
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MCommonCost(Me.Tag)
                Await Model.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub


    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements ICostProductionCenter.AssigningValues
        With _productionCenter
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameProductionCenter
            .OrganizationalStructureOfCostId = OrganizationalStructureOfCostId
            .CategoryId = CategoryId
            .CenterType = CenterType
            .Area = Area

            If INDlyItemCancellationCostMainAccountId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CancellationCostMainAccountId = CancellationCostMainAccountId
            Else
                .CancellationCostMainAccountId = Nothing
            End If
            .Description = Description

            'asigna los valores eliminados en el grid a una lista: listTypeConsecutive
            If ListCostCenter IsNot Nothing AndAlso ListCostCenter.Count > 0 Then
                For Each item In ListCostCenter
                    .CostProductionCenterCostCenter.Add(item)
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements ICostProductionCenter.CleanControls
        INDlcRoot.BeginUpdate()

        ListHomologationConsumption = Nothing
        ListHomologationDeprecation = Nothing
        ListHomologationGeneralExpenses = Nothing
        ListHomologationLabor = Nothing
        ListHomologationSupply = Nothing
        ListHomologationSales = Nothing
        ListCostCenter = Nothing

        ActionsOnControls = False
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDsleCostCenter.EditValue = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty
        INDsleOrganizationalStructure.EditValue = Nothing
        INDsleOrganizationalStructure.Properties.NullText = String.Empty
        INDsleCategory.EditValue = Nothing
        INDsleCategory.Properties.NullText = String.Empty
        INDgleCenterType.EditValue = Nothing
        CancellationCostMainAccountId = Nothing
        INDsleCancellationCostMainAccountId.Properties.NullText = String.Empty
        INDspnArea.EditValue = 0
        INDmeDescription.Text = String.Empty

        _stateOpenPopUpDeprecation = False
        _statePopUpGeneralExpenses = False

        _productionCenter = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        CtrProductionCenter.MainAccountOrigin.Properties.DataSource = Nothing
        CtrProductionCenter.MainAccountDestination.Properties.DataSource = Nothing

        If FormSearchObjects Is Nothing OrElse FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If

        INDlcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._productionCenter.Code, Me._productionCenter.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._productionCenter.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._productionCenter.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._productionCenter.Code, Me._productionCenter.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._productionCenter.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Sub LoadControls() Implements ICostProductionCenter.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            INDsleCostCenter.Properties.DataSource = model.ListCostCenter()
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            Dim res As ActionResult(Of CostProductionCenter) = Await model.GetProductionCenter(Me.Code)
            If res.StatusCode <> eStatusResult.SUCCESS Then
                Mensaje(res.StatusCode) = res.Message
                AsyncLoader(False)
                Exit Sub
            End If
            _productionCenter = res.ObjectEmbbeded
            If _productionCenter IsNot Nothing AndAlso _productionCenter.Id > 0 Then
                Using mCommonCost As New MCommonCost(Me.Tag)
                    Dim result = Await mCommonCost.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _productionCenter.Id)
                    _isLoading = True

                    With _productionCenter
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        NameProductionCenter = .Name
                        OrganizationalStructureOfCostId = .OrganizationalStructureOfCostId
                        INDsleOrganizationalStructure.Properties.NullText = _productionCenter.NullTextOrganizationalStructure
                        CategoryId = .CategoryId
                        INDsleCategory.Properties.NullText = .CategoryCodeName
                        CenterType = .CenterType
                        'Responsible = .Responsible
                        CancellationCostMainAccountId = .CancellationCostMainAccountId
                        INDsleCancellationCostMainAccountId.Properties.NullText = .NumberNameMainAccountCancellationCost
                        Area = .Area
                        Description = .Description
                        Status = .Status

                        INDGcCostCenter.DataSource = _productionCenter.CostProductionCenterCostCenter.ToList()
                        LoadProductionCenterHomologation()
                    End With

                    _isLoading = False
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._productionCenter.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _productionCenter.Id}
                        Dim operation = Await mCommonCost.SaveBlockRecordCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_productionCenter.Id, MyTag, Nothing, GetType(CostProductionCenter).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    AsyncLoader(False)
                    ActionsOnControls = True
                End Using
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewProductionCenter()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If
            INDlcRoot.EndUpdate()
        End If
    End Sub

#Region "AddHomologation"
    ''' <summary>
    ''' Adds the homologation.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologation(productHomologation As CostProductionCenterHomologation)
        Select Case INDPccAddAccounts.OwnerEdit.Name
            Case INDpceAddLabor.Name
                AddHomologationLabor(productHomologation)
            Case INDpceAddSupply.Name
                AddHomologationSupply(productHomologation)
            Case INDpceConsumption.Name
                AddHomologationConsumption(productHomologation)
            Case INDpceGeneralExpenses.Name
                AddHomologationGeneralExpenses(productHomologation)
            Case INDpceAddDeprecation.Name
                AddHomologationDeprecation(productHomologation)
            Case INDpceAddSales.Name
                AddHomologationSales(productHomologation)
        End Select
    End Sub

    Private Function ValidateHomologation(list As List(Of CostProductionCenterHomologation), productHomologation As CostProductionCenterHomologation) As Boolean
        If list.Any(Function(o) o.Id <> productHomologation.Id AndAlso o.AccountOriginId = productHomologation.AccountOriginId _
                        AndAlso If(o.AccountTargetId IsNot Nothing, o.AccountTargetId, 0) = If(productHomologation.AccountTargetId IsNot Nothing, productHomologation.AccountTargetId, 0)) Then
            Mensaje(eStatusResult.WARNING) = "Esta configuración de cuentas ya se encuentra en el listado"
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Adds the homologation labor.
    ''' </summary>
    Private Sub AddHomologationLabor(productHomologation As CostProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 1).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 1
        _productionCenter.CostProductionCenterHomologation.Add(productHomologation)
        ListHomologationLabor = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 1).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation spupply.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationSupply(productHomologation As CostProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 2).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 2
        _productionCenter.CostProductionCenterHomologation.Add(productHomologation)
        ListHomologationSupply = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 2).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation consumption.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationConsumption(productHomologation As CostProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 3).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 3
        _productionCenter.CostProductionCenterHomologation.Add(productHomologation)
        ListHomologationConsumption = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 3).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation general expenses.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationGeneralExpenses(productHomologation As CostProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 4).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 4
        _productionCenter.CostProductionCenterHomologation.Add(productHomologation)
        ListHomologationGeneralExpenses = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 4).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation deprecation.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationDeprecation(productHomologation As CostProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 5).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 5
        _productionCenter.CostProductionCenterHomologation.Add(productHomologation)
        ListHomologationDeprecation = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 5).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation sales.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationSales(productHomologation As CostProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 6).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 6
        _productionCenter.CostProductionCenterHomologation.Add(productHomologation)
        ListHomologationSales = _productionCenter.CostProductionCenterHomologation.Where(Function(x) x.HomologationType = 6).ToList()
    End Sub
#End Region

    ''' <summary>
    ''' Loads the production center homologation.
    ''' </summary>
    Private Sub LoadProductionCenterHomologation()
        If _productionCenter.CostProductionCenterHomologation IsNot Nothing AndAlso _productionCenter.CostProductionCenterHomologation.Count > 0 Then
            ListHomologationLabor = _productionCenter.CostProductionCenterHomologation.Where(Function(c) c.HomologationType = 1).ToList()
            ListHomologationSupply = _productionCenter.CostProductionCenterHomologation.Where(Function(c) c.HomologationType = 2).ToList()
            ListHomologationConsumption = _productionCenter.CostProductionCenterHomologation.Where(Function(c) c.HomologationType = 3).ToList()
            ListHomologationGeneralExpenses = _productionCenter.CostProductionCenterHomologation.Where(Function(c) c.HomologationType = 4).ToList()
            ListHomologationDeprecation = _productionCenter.CostProductionCenterHomologation.Where(Function(c) c.HomologationType = 5).ToList()
            ListHomologationSales = _productionCenter.CostProductionCenterHomologation.Where(Function(c) c.HomologationType = 6).ToList()
        End If
    End Sub
#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar

        If Me._productionCenter IsNot Nothing AndAlso Me._productionCenter.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    INDGcCostCenter.DataSource = Nothing
                    Dim result = Await model.DeleteProductionCenter(Me._productionCenter)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        'Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbteCode.Enabled = False
                    End If
                    Mensaje(result.StatusCode) = result.Message
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result = Await model.SaveProductionCenter(Me._productionCenter, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _productionCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.CostSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._productionCenter = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
            Else
                AsyncLoader(False)
                INDbteCode.Enabled = False
            End If
            Mensaje(result.StatusCode) = result.Message
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                AsyncLoader(True)
                Dim state As Boolean = Not Me._productionCenter.Status
                Dim result = Await model.UpdateStateProductionCenter(Me._productionCenter.Code, state)
                AsyncLoader(False)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._productionCenter = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    INDbteCode.Enabled = False
                End If
                Mensaje(result.StatusCode) = result.Message
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewProductionCenter()
        End If
    End Sub
#End Region

#Region "BarButton Events"
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
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
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    'Private Sub INDgvSales_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvSales.CustomDrawCell
    '    If e.Column.Name = INDColAllowSecondaryDistribution.Name Then
    '        If e.CellValue = True Then
    '            e.DisplayText = "SI"
    '        Else
    '            e.DisplayText = "NO"
    '        End If
    '    End If
    'End Sub

    Private Sub INDgvSales_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDgvSales.CustomUnboundColumnData
        If e.Column.Name = INDColAllowSecondaryDistribution.Name AndAlso e.IsGetData Then
            If CBool(e.Row.AllowSecondaryDistribution) Then
                e.Value = "SI"
            Else
                e.Value = "NO"
            End If
        End If
    End Sub

#End Region

End Class
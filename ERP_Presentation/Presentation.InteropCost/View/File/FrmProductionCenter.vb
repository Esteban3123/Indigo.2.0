'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 15-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.InteropCost.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Controls.MVP
Imports DevExpress.Utils.Menu
Imports System.ComponentModel
Imports DevExpress.Data.PLinq
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Class FrmProductionCenter
    Implements IProductionCenter

#Region "Properties and Variables"

#Region "Poperties Entity"
    ''' <summary>
    ''' Obtiene o establece el código del centro de produccion
    ''' </summary>
    Public Property Code As String Implements IProductionCenter.Code
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
    Public Property NameProductionCenter As String Implements IProductionCenter.NameProductionCenter
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
    Public Property Area As Decimal Implements IProductionCenter.Area
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
    Public Property CenterType As Byte Implements IProductionCenter.CenterType
        Get
            Return INDgleCenterType.EditValue
        End Get
        Set(value As Byte)
            INDgleCenterType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    Public Property Description As String Implements IProductionCenter.Description
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
    Public Property OrganizationalStructureOfCostId As Integer Implements IProductionCenter.OrganizationalStructureOfCostId
        Get
            Return INDsleOrganizationalStructure.EditValue
        End Get
        Set(value As Integer)
            INDsleOrganizationalStructure.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IProductionCenter.Status
        Get
            Return BarraBotones.StatusRecord
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
    Public Property OrganizationalStructureDatasourse As List(Of Domain.Entities.OrganizationalStructureOfCosts) Implements IProductionCenter.OrganizationalStructureDatasourse
        Get
            Return CType(INDsleOrganizationalStructure.Properties.DataSource, List(Of Domain.Entities.OrganizationalStructureOfCosts))
        End Get
        Set(value As List(Of Domain.Entities.OrganizationalStructureOfCosts))
            INDsleOrganizationalStructure.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the cost center datasource.
    ''' </summary>
    Public Property CostCenterDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IProductionCenter.CostCenterDatasource
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
    Public Const MODULE_NAME As String = "InteropCost"

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
    Private _sequence As Domain.Entities.InteropCostSecuence

    Private _costCenterXpo As CTNCENCOSXpo

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    Private model As MProductionCenter

    Public Property CancellationCostMainAccountId As Integer? Implements IProductionCenter.CancellationCostMainAccountId
        Get
            Return INDsleCancellationCostMainAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleCancellationCostMainAccountId.EditValue = value
        End Set
    End Property

    Public Property CancellationCostMainAccountXpo As XPInstantFeedbackSource Implements IProductionCenter.CancellationCostMainAccountXpo
        Get
            Return INDsleCancellationCostMainAccountId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCancellationCostMainAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequence As InteropCostSecuence Implements IProductionCenter.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As InteropCostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.InteropCostSecuenceDetail In Me._sequence.InteropCostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IProductionCenter.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IProductionCenter.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim _productionCenter As ProductionCenter

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PProductionCenter

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordInteropCost

    ''' <summary>
    ''' Lista los tipos de centro
    ''' </summary>
    Private _centerTypeList As List(Of Tuple(Of Integer, String))


    Public Property ListCostCenter As List(Of ProductionCenterCostCenter)
        Get
            Return CType(INDGcCostCenter.DataSource, List(Of ProductionCenterCostCenter))
        End Get
        Set(value As List(Of ProductionCenterCostCenter))
            INDGcCostCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por mano de obra
    ''' </summary>
    Public Property ListHomologationLabor As List(Of ProductionCenterHomologation) Implements IProductionCenter.ListHomologationLabor
        Get
            Return CType(INDgcLabor.DataSource, List(Of ProductionCenterHomologation))
        End Get
        Set(value As List(Of ProductionCenterHomologation))
            INDgcLabor.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por suministro
    ''' </summary>
    Public Property ListHomologationSupply As List(Of ProductionCenterHomologation) Implements IProductionCenter.ListHomologationSupply
        Get
            Return CType(INDgcSupply.DataSource, List(Of ProductionCenterHomologation))
        End Get
        Set(value As List(Of ProductionCenterHomologation))
            INDgcSupply.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por consumo
    ''' </summary>
    Public Property ListHomologationConsumption As List(Of ProductionCenterHomologation) Implements IProductionCenter.ListHomologationConsumption
        Get
            Return CType(INDgcConsumption.DataSource, List(Of ProductionCenterHomologation))
        End Get
        Set(value As List(Of ProductionCenterHomologation))
            INDgcConsumption.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por egresos generales
    ''' </summary>
    Public Property ListHomologationGeneralExpenses As List(Of ProductionCenterHomologation) Implements IProductionCenter.ListHomologationGeneralExpenses
        Get
            Return CType(INDgcGeneralExpenses.DataSource, List(Of ProductionCenterHomologation))
        End Get
        Set(value As List(Of ProductionCenterHomologation))
            INDgcGeneralExpenses.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por depreciacion
    ''' </summary>
    Public Property ListHomologationDeprecation As List(Of ProductionCenterHomologation) Implements IProductionCenter.ListHomologationDeprecation
        Get
            Return CType(INDgcDeprecation.DataSource, List(Of ProductionCenterHomologation))
        End Get
        Set(value As List(Of ProductionCenterHomologation))
            INDgcDeprecation.DataSource = value
        End Set
    End Property

    Public Property ListHomologationSales As List(Of ProductionCenterHomologation) Implements IProductionCenter.ListHomologationSales
        Get
            Return CType(INDgcAccountSales.DataSource, List(Of ProductionCenterHomologation))
        End Get
        Set(value As List(Of ProductionCenterHomologation))
            INDgcAccountSales.DataSource = value
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProductionCenter.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleOrganizationalStructure.Enabled = value
            INDsleCostCenter.Enabled = value
            INDspnArea.Enabled = value
            INDgleCenterType.Enabled = value
            INDsleCancellationCostMainAccountId.Enabled = value
            INDmeDescription.Enabled = value
            INDSbAddCostCenter.Enabled = value

            INDpceAddDeprecation.Enabled = value
            INDpceAddLabor.Enabled = value
            INDpceAddSupply.Enabled = value
            INDpceConsumption.Enabled = value
            INDpceGeneralExpenses.Enabled = value
            INDgcConsumption.Enabled = value
            INDgcDeprecation.Enabled = value
            INDgcGeneralExpenses.Enabled = value
            INDgcLabor.Enabled = value
            INDgcAccountSales.Enabled = value
            INDgcSupply.Enabled = value
            INDGcCostCenter.Enabled = value

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
        _costCenterXpo = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        model.Dispose()
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
        _presenter = New PProductionCenter(Me)
        model = New MProductionCenter(Me.Tag)
        _presenter.GetSequence()
        _presenter.LoadStructure()
        _presenter.LoadDefinitionLayout()

        SetActionsGrid()
        AddHandler CtrProductionCenter.AddHomologation, AddressOf AddHomologation
        AddHandler CtrProductionCenter.MainAccountOrigin.QueryPopUp, AddressOf MainAccountOriginQueryPopUp
        AddHandler CtrProductionCenter.MainAccountDestination.QueryPopUp, AddressOf MainAccountDestinationQueryPopUp
        CtrProductionCenter.INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        CreateCenterType()
        LoadStatus()
        Deshacer()

        'Se valida si la compañia es privada o pública para así mismo ocultar o mostrar la información
        If indigo.IndigoCompanyType = 1 Then 'Privada
            INDlcgLabor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgSupply.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgConsumption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgGeneralExpenses.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgDeprecation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDlyItemCancellationCostMainAccountId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemCancellationCostMainAccountId.AllowHide = True
        Else 'Pública
            INDlcgLabor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgSupply.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgConsumption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgGeneralExpenses.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgDeprecation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDlyItemCancellationCostMainAccountId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemCancellationCostMainAccountId.AllowHide = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductionCenter_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub FrmProductionCenter_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Aqui se controla que solo se pueda seleccionar los nodos de ultimo nivel
    ''' </summary>
    Private Sub INDsleOrganizationalStructure_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleOrganizationalStructure.EditValueChanging
        If Me.INDsleOrganizationalStructure.Properties.DataSource IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim res As Boolean = Me.OrganizationalStructureDatasourse.Any(Function(o) o.ParentId.HasValue AndAlso o.ParentId.Value = CInt(e.NewValue))
            If res Then
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            INDsleCostCenter.Properties.DataSource = model.ListCostCenterDinamic()
        End If
    End Sub

    Private Sub INDPceAddAccounts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceSales.QueryPopUp, INDpceAddLabor.QueryPopUp, INDpceAddSupply.QueryPopUp, INDpceConsumption.QueryPopUp, INDpceGeneralExpenses.QueryPopUp, INDpceAddDeprecation.QueryPopUp
        If CType(sender, PopupContainerEdit).Properties.PopupControl Is Nothing Then
            If INDgleCenterType.EditValue > 1 Then 'Si es diferente a Operativo entonces ocultamos la cuenta de destino
                CtrProductionCenter.INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf CType(sender, PopupContainerEdit).Name.Equals(INDpceSales.Name) Then
                CtrProductionCenter.INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                CtrProductionCenter.INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            CType(sender, PopupContainerEdit).Properties.PopupControl = INDPccAddAccounts
            CtrProductionCenter.MainAccountOrigin.Properties.DataSource = Nothing
        End If
    End Sub

    Private Sub INDsleCancellationCostMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCancellationCostMainAccountId.QueryPopUp
        If CancellationCostMainAccountXpo Is Nothing Then
            _presenter.ListMainAccount()
        End If
    End Sub

#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
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
            OpenForm("1200", Nothing, True)
            _presenter.LoadStructure()
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
            _costCenterXpo = CType(CType(INDgvCostCenter.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InteropCostRepository.CTNCENCOSXpo)
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDSbAddCostCenter_Click(sender As Object, e As EventArgs) Handles INDSbAddCostCenter.Click
        If INDsleCostCenter.EditValue IsNot Nothing Then
            Dim costCenterId As Integer = CType(INDsleCostCenter.EditValue, Integer)
            If ListCostCenter IsNot Nothing AndAlso ListCostCenter.Count > 0 AndAlso ListCostCenter.Where(Function(x) x.CostCenterId = costCenterId).Count() > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Este centro de costo ya está en el listado"
                Exit Sub
            End If
            Dim _costCenter As New ProductionCenterCostCenter()
            With _costCenter
                .CostCenterCode = _costCenterXpo.CCCODIGO
                .CostCenterId = _costCenterXpo.OID
                .CostCenterName = _costCenterXpo.CCNOMBRE
            End With
            _productionCenter.ProductionCenterCostCenter.Add(_costCenter)
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
            ListCostCenter = _productionCenter.ProductionCenterCostCenter.ToList()
            INDGcCostCenter.RefreshDataSource()
            INDsleCostCenter.EditValue = Nothing
            _costCenterXpo = Nothing
            INDsleCostCenter.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the INDsbAddArea control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddArea_Click(sender As Object, e As EventArgs)
        'If INDsleServiceArea.EditValue IsNot Nothing Then
        '    Dim servAreaCode As String = CType(INDsleServiceArea.EditValue, String)
        '    If ListServiceArea IsNot Nothing AndAlso ListServiceArea.Count > 0 AndAlso ListServiceArea.Where(Function(x) x.ServiceAreaCode = servAreaCode).Count() > 0 Then
        '        Mensaje(EeventViewerImages.Advertencia) = "Este área de servicio ya está en el listado"
        '        Exit Sub
        '    End If
        '    Dim _serviceAreaProductionCenter As New ProductionCenterServiceArea()
        '    With _serviceAreaProductionCenter
        '        '.ServiceAreaCode = _areaServicioXpo.GASCODIGO
        '        '.ServiceAreaId = _areaServicioXpo.OID
        '        .ServiceAreaName = _areaServicioXpo.GASNOMBRE
        '        .CTNCuentaServiceArea = _areaServicioXpo.CTNCUENTA2
        '    End With
        '    '_productionCenter.ProductionCenterServiceArea.Add(_serviceAreaProductionCenter)
        '    If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
        '        _productionCenter.MarkAsModified()
        '    End If
        '    'ListServiceArea = _productionCenter.ProductionCenterServiceArea.ToList()
        '    INDgcServiceArea.RefreshDataSource()
        '    INDsleServiceArea.EditValue = Nothing
        '    _areaServicioXpo = Nothing
        '    INDsleServiceArea.Focus()
        'End If
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
    Private Sub INDgcServiceArea_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcAccountSales.DataSourceChanged
        'INDpceAddSupply.Enabled = (ListAccountSales IsNot Nothing AndAlso ListAccountSales.Count > 0)
        'INDpceConsumption.Enabled = (ListAccountSales IsNot Nothing AndAlso ListAccountSales.Count > 0)
        'INDsleCostCenter.Properties.ReadOnly = (ListServiceArea IsNot Nothing AndAlso ListServiceArea.Count > 0)
        'INDGVCostCenters.Columns.Where(Function(x) x.Name = "colActions").FirstOrDefault().Visible = (ListAccountSales Is Nothing OrElse ListAccountSales.Count = 0) AndAlso (ListHomologationDeprecation Is Nothing OrElse ListHomologationDeprecation.Count = 0)
    End Sub

    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDeprecation control.
    ''' </summary>
    Private Sub INDgcDeprecation_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcDeprecation.DataSourceChanged
        'INDsleCostCenter.Enabled = Not (ListHomologationDeprecation IsNot Nothing AndAlso ListHomologationDeprecation.Count > 0)
        'INDGVCostCenters.Columns.Where(Function(x) x.Name = "colActions").FirstOrDefault().Visible = (ListAccountSales Is Nothing OrElse ListAccountSales.Count = 0) AndAlso (ListHomologationDeprecation Is Nothing OrElse ListHomologationDeprecation.Count = 0)
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
    Private Sub IndigoGridViewServiceArea_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewAccountSales.Click_ButtonAction, IndigoGridViewAccountSales.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _productionCenterHomologation As ProductionCenterHomologation = CType(INDgvAccountSales.GetFocusedRow(), ProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationSales = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 6).ToList()
            INDgcAccountSales.RefreshDataSource()
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
            Dim _productionCenterHomologation As ProductionCenterHomologation = CType(INDgvLabor.GetFocusedRow(), ProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationLabor = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 1).ToList()
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
            Dim _productionCenterHomologation As ProductionCenterHomologation = CType(INDgvSupply.GetFocusedRow(), ProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationSupply = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 2).ToList()
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
            Dim _productionCenterHomologation As ProductionCenterHomologation = CType(INDgvConsumption.GetFocusedRow(), ProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationConsumption = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 3).ToList()
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
            Dim _productionCenterHomologation As ProductionCenterHomologation = CType(INDgvGeneralData.GetFocusedRow(), ProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationGeneralExpenses = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 4).ToList()
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
            Dim _productionCenterHomologation As ProductionCenterHomologation = CType(INDgvDeprecation.GetFocusedRow(), ProductionCenterHomologation)
            _productionCenterHomologation.MarkAsDeleted()
            ListHomologationDeprecation = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 5).ToList()
            INDgcDeprecation.RefreshDataSource()
            If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
                _productionCenter.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub IndigoGridViewCostCenter_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewCostCenter.Click_ButtonAction, IndigoGridViewCostCenter.ContexMenuActions
        'If (ListAccountSales Is Nothing OrElse ListAccountSales.Count = 0) AndAlso (ListHomologationDeprecation Is Nothing OrElse ListHomologationDeprecation.Count = 0) Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Dim _costCenter As ProductionCenterCostCenter = CType(INDGVCostCenters.GetFocusedRow(), ProductionCenterCostCenter)
        '        _costCenter.MarkAsDeleted()
        '        ListCostCenter = _productionCenter.ProductionCenterCostCenter.ToList()
        '        INDGcCostCenter.RefreshDataSource()
        '        If _productionCenter.ChangeTracker.State <> ObjectState.Added Then
        '            _productionCenter.MarkAsModified()
        '        End If
        '    End If
        'Else
        '    Mensaje(eStatusResult.WARNING) = "Para poder eliminar los centros de costo primero se deben eliminar las áreas de servicio y los registros de depreciación"
        'End If
    End Sub
#End Region

#Region "Closed"

    Private Sub INDPceAddAccount_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceSales.Closed, INDpceAddLabor.Closed, INDpceAddSupply.Closed, INDpceConsumption.Closed, INDpceGeneralExpenses.Closed, INDpceAddDeprecation.Closed
        CtrProductionCenter.CleanControls()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que valida si la cuenta de origen existe en algunos de los otros grupos
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateMainAccount(MainAccountId As Integer, Type As Integer) As Boolean
        Dim dictionaryType As New Dictionary(Of Integer, String)
        dictionaryType.Add(1, INDlcgLabor.Text)
        dictionaryType.Add(2, INDlcgSupply.Text)
        dictionaryType.Add(3, INDlcgConsumption.Text)
        dictionaryType.Add(4, INDlcgGeneralExpenses.Text)
        dictionaryType.Add(5, INDlcgDeprecation.Text)
        dictionaryType.Add(6, INDlcgServiceArea.Text)
        For i = 1 To 6 Step 1
            If i <> Type Then
                If (From x In _productionCenter.ProductionCenterHomologation Where x.HomologationType = i AndAlso x.AccountOriginId = MainAccountId Select x).Count > 0 Then
                    Mensaje(eStatusResult.WARNING) = "La cuenta de origen seleccionada ya se encuentra en el grupo " + dictionaryType(i)
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Sub MainAccountOriginQueryPopUp(sender As Object, e As CancelEventArgs)
        If CtrProductionCenter.MainAccountOrigin.Properties.DataSource Is Nothing Then
            Select Case INDPccAddAccounts.OwnerEdit.Name
                Case INDpceAddLabor.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountErpByClassAndNivel({5, 6, 7}.ToList(), {5}.ToList())
                Case INDpceSales.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountErpByClassAndNivel({5}.ToList(), {5}.ToList())
                Case INDpceAddSupply.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountErpByClassAndNivel({5, 6, 7}.ToList(), {5}.ToList())
                Case INDpceConsumption.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountComsumo()
                Case INDpceGeneralExpenses.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountErpByClassAndNivel({5, 6, 7}.ToList(), {5}.ToList())
                Case INDpceAddDeprecation.Name
                    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountErpByClassAndNivel({5, 6, 7}.ToList(), {5}.ToList())
                    'Se comentan éstas líneas de código a petición de Geovanny
                    'If ListCostCenter IsNot Nothing AndAlso ListCostCenter.Any() Then
                    '    CtrProductionCenter.MainAccountOrigin.Properties.DataSource = model.ListMainAccountDeprecation(ListCostCenter.Select(Function(x) x.CostCenterId).ToList())
                    'End If
            End Select
        End If
    End Sub

    Private Sub MainAccountDestinationQueryPopUp(sender As Object, e As CancelEventArgs)
        If CtrProductionCenter.MainAccountDestination.Properties.DataSource Is Nothing Then
            CtrProductionCenter.MainAccountDestination.Properties.DataSource = model.ListMainAccountErpByClassAndNivel({5, 6, 7}.ToList(), {5}.ToList())
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
        Me._productionCenter = New ProductionCenter()
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.InteropCostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
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
                        Using model As New MCommonInteropCost(Me.Tag)
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
        _centerTypeList.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("ProductionCenterTypeOperating", MODULE_NAME)))
        _centerTypeList.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("ProductionCenterTypeAdministrative", MODULE_NAME)))
        _centerTypeList.Add(New Tuple(Of Integer, String)(3, "Logístico"))
        INDgleCenterType.Properties.DataSource = _centerTypeList
    End Sub

    ''' <summary>
    ''' Sets the actions grid.
    ''' </summary>
    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridViewAccountSales.SetListAcction(INDgvAccountSales, _listActions)
        IndigoGridViewLabor.SetListAcction(INDgvLabor, _listActions)
        IndigoGridViewSupply.SetListAcction(INDgvSupply, _listActions)
        IndigoGridViewConsumption.SetListAcction(INDgvConsumption, _listActions)
        IndigoGridViewGeneralData.SetListAcction(INDgvGeneralData, _listActions)
        IndigoGridViewDeprecation.SetListAcction(INDgvDeprecation, _listActions)
        IndigoGridViewCostCenter.SetListAcction(INDGVCostCenters, _listActions)
        For Each col As GridColumn In INDgvAccountSales.Columns
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
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7},
                              New ColumnInfo() With {.Caption = "Estructura", .FieldName = "OrganizationalStructureOfCostId.Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListProductionCenter
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
            Using Model As New MCommonInteropCost(Me.Tag)
                Await Model.DeleteBlockRecordInteropCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements IProductionCenter.AssigningValues
        With _productionCenter
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameProductionCenter
            .OrganizationalStructureOfCostId = OrganizationalStructureOfCostId
            .Area = Area
            .CenterType = CenterType
            If INDlyItemCancellationCostMainAccountId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CancellationCostMainAccountId = CancellationCostMainAccountId
            Else
                .CancellationCostMainAccountId = Nothing
            End If
            .Description = Description
            Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                Case eActionsStatusRecords.Active
                    .Status = True
                Case eActionsStatusRecords.Inactive
                    .Status = False
            End Select
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IProductionCenter.CleanControls
        INDlcRoot.BeginUpdate()

        'ListAccountSales = Nothing
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
        INDsleOrganizationalStructure.EditValue = Nothing
        INDspnArea.EditValue = 0
        INDgleCenterType.EditValue = Nothing
        CancellationCostMainAccountId = Nothing
        INDsleCancellationCostMainAccountId.Properties.NullText = String.Empty
        INDmeDescription.Text = String.Empty
        _stateOpenPopUpDeprecation = False

        _statePopUpGeneralExpenses = False

        INDsleOrganizationalStructure.Properties.NullText = String.Empty
        INDsleCostCenter.Properties.NullText = String.Empty
        _productionCenter = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

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
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._productionCenter.Code, Me._productionCenter.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._productionCenter.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._productionCenter.Code),
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
    Public Async Sub LoadControls() Implements IProductionCenter.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            Dim res As ActionResult(Of ProductionCenter) = Await model.GetProductionCenter(Me.Code)
            If res.StatusCode <> eStatusResult.SUCCESS Then
                Mensaje(res.StatusCode) = res.Message
                AsyncLoader(False)
                Exit Sub
            End If
            _productionCenter = res.ObjectEmbbeded
            If _productionCenter IsNot Nothing AndAlso _productionCenter.Id > 0 Then
                Using ModelCommonTreasury As New MCommonInteropCost(Me.Tag)
                    Dim result = Await ModelCommonTreasury.GetBlockRecordInteropCostByIdformAndIdRecord(Me.Tag, _productionCenter.Id)
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
                        Area = .Area
                        CenterType = .CenterType
                        CancellationCostMainAccountId = .CancellationCostMainAccountId
                        INDsleCancellationCostMainAccountId.Properties.NullText = .NumberNameMainAccountCancellationCost
                        Description = .Description
                        Status = .Status
                    End With

                    LoadProductionCenterHomologation()
                    'ListServiceArea = _productionCenter.ProductionCenterServiceArea.ToList()
                    ListCostCenter = _productionCenter.ProductionCenterCostCenter.ToList()
                    '_presenter.InitializeMainAccountOrigin()
                    '_presenter.InitializeMainAccountOriginSupply(ListServiceArea.Select(Function(x) x.ServiceAreaId).Cast(Of Integer).ToList())
                    '_presenter.InitializeMainAccountOriginGeneralExpenses()
                    '_presenter.InitializeMainAccountOriginDeprecation(CostCenterId)
                    INDsleOrganizationalStructure.Properties.NullText = _productionCenter.NullTextOrganizationalStructure
                    _isLoading = False
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._productionCenter.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordInteropCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _productionCenter.Id}
                        Dim operation = Await ModelCommonTreasury.SaveBlockRecordInteropCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_productionCenter.Id, MyTag, Nothing, GetType(ProductionCenter).Name)
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
    Private Sub AddHomologation(productHomologation As ProductionCenterHomologation)
        Dim Type As Integer = 0
        Select Case INDPccAddAccounts.OwnerEdit.Name
            Case INDpceAddLabor.Name
                Type = 1
            Case INDpceAddSupply.Name
                Type = 2
            Case INDpceConsumption.Name
                Type = 3
            Case INDpceGeneralExpenses.Name
                Type = 4
            Case INDpceAddDeprecation.Name
                Type = 5
            Case INDpceSales.Name
                Type = 6
        End Select
        If ValidateMainAccount(productHomologation.AccountOriginId, Type) = False Then
            Exit Sub
        End If

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
            Case INDpceSales.Name
                AddHomologationSales(productHomologation)
        End Select
    End Sub

    Private Function ValidateHomologation(list As List(Of ProductionCenterHomologation), productHomologation As ProductionCenterHomologation) As Boolean
        If list.Any(Function(o) o.AccountOriginId = productHomologation.AccountOriginId AndAlso o.AccountTargetId = productHomologation.AccountTargetId) Then
            Mensaje(eStatusResult.WARNING) = "Esta configuración de cuentas ya se encuentra en el listado"
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Adds the homologation labor.
    ''' </summary>
    Private Sub AddHomologationLabor(productHomologation As ProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 1).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 1
        _productionCenter.ProductionCenterHomologation.Add(productHomologation)
        ListHomologationLabor = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 1).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation spupply.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationSupply(productHomologation As ProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 2).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 2
        _productionCenter.ProductionCenterHomologation.Add(productHomologation)
        ListHomologationSupply = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 2).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation consumption.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationConsumption(productHomologation As ProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 3).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 3
        _productionCenter.ProductionCenterHomologation.Add(productHomologation)
        ListHomologationConsumption = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 3).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation general expenses.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationGeneralExpenses(productHomologation As ProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 4).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 4
        _productionCenter.ProductionCenterHomologation.Add(productHomologation)
        ListHomologationGeneralExpenses = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 4).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation deprecation.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationDeprecation(productHomologation As ProductionCenterHomologation)
        If Not ValidateHomologation(_productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 5).ToList(), productHomologation) Then
            Exit Sub
        End If
        productHomologation.HomologationType = 5
        _productionCenter.ProductionCenterHomologation.Add(productHomologation)
        ListHomologationDeprecation = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 5).ToList()
    End Sub

    ''' <summary>
    ''' Adds the homologation sales.
    ''' </summary>
    ''' <param name="productHomologation">The product homologation.</param>
    Private Sub AddHomologationSales(productHomologation As ProductionCenterHomologation)
        If _productionCenter.ProductionCenterHomologation.Any(Function(o) o.HomologationType = 6 AndAlso o.AccountOriginId = productHomologation.AccountOriginId) Then
            Mensaje(eStatusResult.WARNING) = "Esta configuración de cuentas ya se encuentra en el listado"
            Exit Sub
        End If
        productHomologation.HomologationType = 6
        _productionCenter.ProductionCenterHomologation.Add(productHomologation)
        ListHomologationSales = _productionCenter.ProductionCenterHomologation.Where(Function(x) x.HomologationType = 6).ToList()
    End Sub
#End Region

    ''' <summary>
    ''' Loads the production center homologation.
    ''' </summary>
    Private Sub LoadProductionCenterHomologation()
        If _productionCenter.ProductionCenterHomologation IsNot Nothing AndAlso _productionCenter.ProductionCenterHomologation.Count > 0 Then
            ListHomologationLabor = _productionCenter.ProductionCenterHomologation.Where(Function(c) c.HomologationType = 1).ToList()
            ListHomologationSupply = _productionCenter.ProductionCenterHomologation.Where(Function(c) c.HomologationType = 2).ToList()
            ListHomologationConsumption = _productionCenter.ProductionCenterHomologation.Where(Function(c) c.HomologationType = 3).ToList()
            ListHomologationGeneralExpenses = _productionCenter.ProductionCenterHomologation.Where(Function(c) c.HomologationType = 4).ToList()
            ListHomologationDeprecation = _productionCenter.ProductionCenterHomologation.Where(Function(c) c.HomologationType = 5).ToList()
            ListHomologationSales = _productionCenter.ProductionCenterHomologation.Where(Function(c) c.HomologationType = 6).ToList()
        End If
    End Sub

#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me._productionCenter IsNot Nothing AndAlso Me._productionCenter.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
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
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
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
                        Me.DicSequense(Me._sequence.InteropCostSecuenceDetail(0).Id).RemoveAt(0)
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
                Dim state As Boolean
                Select Case Status
                    Case eActionsStatusRecords.Active
                        state = True
                    Case eActionsStatusRecords.Inactive
                        state = False
                End Select
                AsyncLoader(True)
                Dim result = Await model.UpdateStateProductionCenter(Me._productionCenter.Code, state)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._productionCenter = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
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
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
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
#End Region

End Class
'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 30-12-2014
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
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Presentation.Payroll
Imports Presentation.Payroll.MVP

#End Region

Public Class FrmEstimateCosts
    Implements IEstimateCost

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrDateNavigatorInteropCost()
        _tmpCurrentPeriod.ResizeToBarMenu()
        _tmpCurrentPeriod.WithEvent = False
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
        _tmpCurrentPeriod.ResizeToBarMenu()
        model = New MEstimateCost(Me.Tag)
        presenter = New PEstimateCost(Me)
    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' The model
    ''' </summary>
    Private model As MEstimateCost
    ''' <summary>
    ''' The presenter
    ''' </summary>
    Private presenter As PEstimateCost
    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "InteropCost"
    ''' <summary>
    ''' parametros de costos
    ''' </summary>
    Private _settingsCost As InteropCostSetting
    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInteropCost
    ''' <summary>
    ''' Entidad de distribución de mano de obra
    ''' </summary>
    Private _estimateCost As List(Of SP_EstimateCost_Result)
    ''' <summary>
    ''' The _TMP current period
    ''' </summary>
    Private _tmpCurrentPeriod As CtrDateNavigatorInteropCost
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

    Dim _estimationType As List(Of Tuple(Of Byte, String))
    Public ReadOnly Property GetEstimationType As List(Of Tuple(Of Byte, String))
        Get
            If _estimationType Is Nothing Then
                _estimationType = New List(Of Tuple(Of Byte, String))()
                _estimationType.Add(New Tuple(Of Byte, String)(1, "Estimación Primaria"))
                _estimationType.Add(New Tuple(Of Byte, String)(2, "Estimación Secundaria"))
                _estimationType.Add(New Tuple(Of Byte, String)(3, "Estimación Final"))
            End If
            Return _estimationType
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece el mes de la distribucion secundaria
    ''' </summary>
    Public Property Month As Integer
        Get
            Return _tmpCurrentPeriod.GetMonth
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.SetMonth = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el año de la distribucion secundaria
    ''' </summary>
    Public Property Year As Integer
        Get
            Return _tmpCurrentPeriod.GetYear
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.SetYear = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEstimateCost.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            'Me.BarraBotones.StatusRecordVisible = value
            INDGcEstimationData.Enabled = value

            INDlcRoot.EndUpdate()
            If value Then

            Else

            End If
        End Set
    End Property
    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IEstimateCost.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IEstimateCost.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Public Property SettingsCost As InteropCostSetting Implements IEstimateCost.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As InteropCostSetting)
            If value IsNot Nothing Then
                _tmpCurrentPeriod.SetMonth = value.Month
                _tmpCurrentPeriod.SetYear = value.Year
                _tmpCurrentPeriod.SetDateInformation()
                _settingsCost = value
            End If
        End Set
    End Property
#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model = Nothing
        presenter = Nothing
        _settingsCost = Nothing
        _record = Nothing
        _estimateCost = Nothing
        _tmpCurrentPeriod = Nothing
    End Sub

    Private Sub FrmEstimateCosts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Validar, "Verificar")

        presenter.LoadDefinitionLayout()
        presenter.LoadSettingCost()
        GleEstimationType.Properties.DataSource = GetEstimationType

        CleanControls()
    End Sub
    ''' <summary>
    ''' Handles the Shown event of the FrmEstimateCosts control.
    ''' </summary>
    Private Sub FrmEstimateCosts_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'searchPreviusCostEstimation()
    End Sub
    ''' <summary>
    ''' Handles the Disposed event of the FrmEstimateCosts control.
    ''' </summary>
    Private Sub FrmEstimateCosts_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model.Dispose()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the BtnSelectAll control.
    ''' </summary>
    Private Sub BtnSelectAll_Click(sender As Object, e As EventArgs)
        ChkListOptions.CheckAll()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the BtnSelectNone control.
    ''' </summary>
    Private Sub BtnSelectNone_Click(sender As Object, e As EventArgs)
        ChkListOptions.UnCheckAll()
    End Sub
#End Region

#Region "Crud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

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
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub
    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Initials the values.
    ''' </summary>
    Private Async Sub searchPreviusCostEstimation()
        'INDlcRoot.BeginUpdate()
        'AsyncLoader(True)
        'Me.BarraBotones.StatusRecordVisible = True
        'Dim res = Await model.GetCostEstimationByYearMonth(Year, Month)
        'If res.StatusCode <> eStatusResult.SUCCESS Then
        '    AsyncLoader(False)
        '    Mensaje(res.StatusCode) = res.Message
        '    Exit Sub
        'End If
        '_estimateCost = res.ObjectEmbbeded
        'If _estimateCost.DirectCostDistribution <> 0 Then
        '    ChkListOptions.Items(0).CheckState = CheckState.Checked
        'Else
        '    ChkListOptions.Items(0).CheckState = CheckState.Unchecked
        'End If
        'If _estimateCost.ManPowerDistribution <> 0 Then
        '    ChkListOptions.Items(1).CheckState = CheckState.Checked
        'Else
        '    ChkListOptions.Items(1).CheckState = CheckState.Unchecked
        'End If
        'If _estimateCost.FixedAssetDistribution <> 0 Then
        '    ChkListOptions.Items(2).CheckState = CheckState.Checked
        'Else
        '    ChkListOptions.Items(2).CheckState = CheckState.Unchecked
        'End If
        'If _estimateCost.InitialDistribution <> 0 Then
        '    ChkListOptions.Items(3).CheckState = CheckState.Checked
        'Else
        '    ChkListOptions.Items(3).CheckState = CheckState.Unchecked
        'End If
        'If _estimateCost.IntermediateDistribution <> 0 Then
        '    ChkListOptions.Items(4).CheckState = CheckState.Checked
        'Else
        '    ChkListOptions.Items(4).CheckState = CheckState.Unchecked
        'End If
        'If _estimateCost.SecondaryDistribution <> 0 Then
        '    ChkListOptions.Items(5).CheckState = CheckState.Checked
        'Else
        '    ChkListOptions.Items(5).CheckState = CheckState.Unchecked
        'End If
        'AsyncLoader(False)
        'INDlcRoot.EndUpdate()
    End Sub
    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub
    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements IEstimateCost.AssigningValues

    End Sub
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IEstimateCost.CleanControls
        INDlcRoot.BeginUpdate()
        'INDbteCode.Text = String.Empty
        'Description = Nothing
        'EmployeeId = Nothing

        'INDpceAddDetail.Enabled = False
        'DdbActions.Enabled = False
        'Status = Nothing

        'TotalAccrued = 0
        'TotalProvision = 0
        'TotalEmployerContribution = 0
        'TotalParafiscal = 0

        ChkListOptions.SetItemCheckState(0, CheckState.Indeterminate)
        ChkListOptions.SetItemCheckState(1, CheckState.Indeterminate)
        ChkListOptions.SetItemCheckState(2, CheckState.Indeterminate)
        ChkListOptions.SetItemCheckState(3, CheckState.Indeterminate)

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = True

        GleEstimationType.EditValue = CByte(1)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False

        _estimateCost = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()

        INDlcRoot.EndUpdate()
    End Sub
    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Sub LoadControls() Implements IEstimateCost.LoadControls

    End Sub
    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._estimateCost.Year, Me._estimateCost.Month), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & Me.Tag & "_" & Me._estimateCost.Year & "#$", .IdForm = Me.Tag, _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._estimateCost.Year), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._estimateCost.Year, Me._estimateCost.Month)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._estimateCost.Year)
        'End If
        Return Me._doc
    End Function
#End Region

    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If (_estimateCost Is Nothing OrElse Not _estimateCost.Any()) AndAlso CByte(GleEstimationType.EditValue) < 3 Then
            Mensaje(eStatusResult.WARNING) = "Verifique los resultados antes de confirmar"
            Exit Sub
        End If
        If MessageIndigo.Show("¿Esta seguro que desea confirmar los datos?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim previusDataXml As New StringBuilder()
        If CByte(GleEstimationType.EditValue) < 3 Then
            previusDataXml.Append("<header>")
            For Each item In _estimateCost
                previusDataXml.Append("<Detail>")
                previusDataXml.Append(String.Format("<ProductionCenterId>{0}</ProductionCenterId>", item.ProductionCenterId))
                previusDataXml.Append(String.Format("<DirectCostDistribution>{0}</DirectCostDistribution>", item.DirectCostDistribution.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<AutoCostDistribution>{0}</AutoCostDistribution>", item.AutoCostDistribution.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<ManPowerDistributionDirect>{0}</ManPowerDistributionDirect>", item.ManPowerDistributionDirect.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<ManPowerDistributionInDirect>{0}</ManPowerDistributionInDirect>", item.ManPowerDistributionIndirect.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<FixedAssetDistribution>{0}</FixedAssetDistribution>", item.FixedAssetDistribution.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<DispensingDistribution>{0}</DispensingDistribution>", item.DispensingDistribution.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<TransferDistribution>{0}</TransferDistribution>", item.TransferDistribution.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<InitialDistribution>{0}</InitialDistribution>", item.InitialDistribution.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<IntermediateDistribution>{0}</IntermediateDistribution>", item.IntermediateDistribution.ToString().Replace(",", ".")))
                previusDataXml.Append(String.Format("<SecondaryDistribution>{0}</SecondaryDistribution>", item.SecondaryDistribution.ToString().Replace(",", ".")))
                previusDataXml.Append("</Detail>")
            Next
            previusDataXml.Append("</header>")
        End If

        RunEstimationCost(False, previusDataXml.ToString())
    End Sub

    Private Sub GleEstimationType_EditValueChanged(sender As Object, e As EventArgs) Handles GleEstimationType.EditValueChanged
        If GleEstimationType.EditValue IsNot Nothing Then
            Select Case CByte(GleEstimationType.EditValue)
                Case 1
                    INDLciCheckOptions.HideControl(False)
                    ChkListOptions.SetItemCheckState(0, CheckState.Indeterminate)
                    ChkListOptions.SetItemCheckState(1, CheckState.Indeterminate)
                    ChkListOptions.SetItemCheckState(2, CheckState.Indeterminate)
                    ChkListOptions.SetItemCheckState(3, CheckState.Indeterminate)

                    ChkListOptions.Items(0).Enabled = False
                    ChkListOptions.Items(1).Enabled = False
                    ChkListOptions.Items(2).Enabled = False
                    ChkListOptions.Items(3).Enabled = False
                    'ChkListOptions.Enabled = False
                Case 2, 3
                    INDLciCheckOptions.HideControl()
            End Select
        End If
    End Sub

    Private Sub BarraBotones_Click_Validar() Handles BarraBotones.Click_Validar
        'CleanControls()
        RunEstimationCost(True)
    End Sub

    Private Async Sub RunEstimationCost(simulate As Boolean, Optional previusDataXml As String = "")
        AsyncLoader(True)
        ShowColumnGrisEstimation(CByte(GleEstimationType.EditValue))
        _estimateCost = Await model.GetSPEstimateCost(CByte(GleEstimationType.EditValue), simulate, previusDataXml)
        AsyncLoader(False)
        If _estimateCost IsNot Nothing Then
            If _estimateCost.Any() Then
                Select Case _estimateCost(0).CodeResult
                    Case "000"
                        'Correcto
                        INDGcEstimationData.Enabled = True
                        If Not simulate Then
                            Mensaje(eStatusResult.SUCCESS) = "Los datos se procesaron correctamente"
                            'CleanControls()
                            ChkListOptions.SetItemCheckState(0, CheckState.Checked)
                            ChkListOptions.SetItemCheckState(1, CheckState.Checked)
                            ChkListOptions.SetItemCheckState(2, CheckState.Checked)
                            ChkListOptions.SetItemCheckState(3, CheckState.Checked)
                            If SessionValues.Instance.IndigoPayrollIntegration = 2 Then
                                ChkListOptions.SetItemCheckState(1, CheckState.Unchecked)
                            End If
                        End If

                        If CByte(GleEstimationType.EditValue) < 3 Then
                            INDGcEstimationData.DataSource = _estimateCost
                        Else
                            presenter.LoadSettingCost()
                        End If
                    Case "111"
                        Mensaje(eStatusResult.WARNING) = _estimateCost(0).MessageResult
                        INDGcEstimationData.DataSource = _estimateCost
                        INDGcEstimationData.Enabled = True
                    Case "999"
                        Mensaje(eStatusResult.WARNING) = _estimateCost(0).MessageResult
                End Select
                
            End If
        Else
            Mensaje(eStatusResult.WARNING) = "No se encontraron resultados"
        End If
    End Sub

    Private Sub GridView1_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles ViewInfo.CustomDrawCell
        If e.Column.Name = INDColInitialManpower.Name Then
            Dim obj As SP_EstimateCost_Result = ViewInfo.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                e.CellValue = obj.ManPowerDistributionDirect + obj.ManPowerDistributionIndirect
            End If
        ElseIf e.Column.Name = INDColSecondaryManPower.Name Then
            Dim obj As SP_EstimateCost_Result = ViewInfo.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                e.CellValue = obj.SecondaryManPowerDistributionDirect + obj.SecondaryManPowerDistributionIndirect
            End If
        End If
    End Sub

    Private Sub ShowColumnGrisEstimation(typeEstimation As Byte)
        If typeEstimation = 1 Then
            INDColInitialExpenseDirect.Visible = True
            INDColInitialExpenseDirect.VisibleIndex = 1
            INDColInitialExpenseVariable.Visible = True
            INDColInitialExpenseVariable.VisibleIndex = 2
            INDColInitialManpower.Visible = True
            INDColInitialManpower.VisibleIndex = 3
            INDColInitialFixedAsset.Visible = True
            INDColInitialFixedAsset.VisibleIndex = 4
            INDColInitialDispensing.Visible = True
            INDColInitialDispensing.VisibleIndex = 5
            INDColInitialTransfer.Visible = True
            INDColInitialTransfer.VisibleIndex = 6
            INDColInitialDistribution.VisibleIndex = 7
            INDColSecondaryExpenseDirect.Visible = False
            INDColSecondaryExpenseVariable.Visible = False
            INDColSecondaryManPower.Visible = False
            INDColSecondaryFixedAsset.Visible = False
            INDColSecondaryDispensing.Visible = False
            INDColSecondaryTransfer.Visible = False
            INDColSecondaryDistribution.Visible = False
        Else
            INDColInitialDistribution.VisibleIndex = 1
            INDColInitialExpenseDirect.Visible = False
            INDColInitialExpenseVariable.Visible = False
            INDColInitialManpower.Visible = False
            INDColInitialFixedAsset.Visible = False
            INDColInitialDispensing.Visible = False
            INDColInitialTransfer.Visible = False
            INDColSecondaryExpenseDirect.Visible = True
            INDColSecondaryExpenseDirect.VisibleIndex = 2
            INDColSecondaryExpenseVariable.Visible = True
            INDColSecondaryExpenseVariable.VisibleIndex = 3
            INDColSecondaryManPower.Visible = True
            INDColSecondaryManPower.VisibleIndex = 4
            INDColSecondaryFixedAsset.Visible = True
            INDColSecondaryFixedAsset.VisibleIndex = 5
            INDColSecondaryDispensing.Visible = True
            INDColSecondaryDispensing.VisibleIndex = 6
            INDColSecondaryTransfer.Visible = True
            INDColSecondaryTransfer.VisibleIndex = 7
            INDColSecondaryDistribution.Visible = True
            INDColSecondaryDistribution.VisibleIndex = 8
        End If
    End Sub

End Class
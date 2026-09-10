'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 26-12-2014
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
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Class FrmSecondaryDistribution
    Implements IDirectDistributionSecondary

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmGeneralExpenses"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrMainAccountValueDate()
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
        _presenter = New PDirectDistributionSecondary(Me)
    End Sub
#End Region

#Region "Globals"
    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PDirectDistributionSecondary

    Dim DirectDistributionSecondary As DirectDistributionSecondary

    Dim costStimation As CostEstimationXpo = Nothing
#End Region

#Region "Properties"

    ''' <summary>
    ''' Listado de ids para actualizar el campo import en la tabla LogisticProductionCenterRecordDetail
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListUpdateIds As List(Of Integer)

    ''' <summary>
    ''' Centro de produccion asociado al elemento de distribucion secundaria
    ''' </summary>
    ''' <remarks></remarks>
    Dim ProductionCenterId As Integer = 0

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "InteropCost"

    Private _settingsCost As InteropCostSetting

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Public Property SettingsCost As InteropCostSetting Implements IDirectDistributionSecondary.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As InteropCostSetting)
            _tmpCurrentPeriod.Year = value.Year
            _tmpCurrentPeriod.Month = value.Month
            _tmpCurrentPeriod.LoadDate()
            _settingsCost = value
        End Set
    End Property

    ''' <summary>
    ''' valor a distribuir previo por si la validacion sobre el valor contable sale falso
    ''' </summary>
    Private _previusValue As Decimal



    ''' <summary>
    ''' Valor previo del centro de produccion
    ''' </summary>
    Private _productCenterPrevius As Integer

    ''' <summary>
    ''' Control de Fecha
    ''' </summary>
    Private _tmpCurrentPeriod As CtrMainAccountValueDate

    ''' <summary>
    ''' Estado de abierto del popup de gastos generales
    ''' </summary>
    Private _stateOpenPopUpGeneralExpense As Boolean

    ''' <summary>
    ''' Estado de abierto del popup de centros de producción
    ''' </summary>
    Private _stateOpenPopUpProductionCenter As Boolean

    ''' <summary>
    ''' Obtiene el layout Control
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IDirectDistributionSecondary.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IDirectDistributionSecondary.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.InteropCostSecuence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64



    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInteropCost

    Public Property DistributionSecondaryId As Integer Implements IDirectDistributionSecondary.DistributionSecondaryId
        Get
            Return INDsleDistributionSecondary.EditValue
        End Get
        Set(value As Integer)
            INDsleDistributionSecondary.EditValue = value
        End Set
    End Property

#Region "Properties Entity"
    ''' <summary>
    ''' Obtiene o establece el codigo del gasto directo
    ''' </summary>
    Public Property Code As String Implements IDirectDistributionSecondary.Code
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
    ''' Obtiene o establece el nombre del gasto directo
    ''' </summary>
    Public Property Description As String Implements IDirectDistributionSecondary.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property



    ''' <summary>
    ''' Mes de la distribución del gasto Directo
    ''' </summary>
    Public Property Month As Integer Implements IDirectDistributionSecondary.Month
        Get
            Return _tmpCurrentPeriod.Month
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Month = value
        End Set
    End Property

    ''' <summary>
    ''' Año de la distribución del gasto Directo
    ''' </summary>
    Public Property Year As Integer Implements IDirectDistributionSecondary.Year
        Get
            Return _tmpCurrentPeriod.Year
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Year = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As Domain.Entities.InteropCostSecuence Implements IDirectDistributionSecondary.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.InteropCostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.InteropCostSecuenceDetail In Me._sequence.InteropCostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Public Property Status As String
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    Private Property Value As Decimal
        Get
            Return _tmpCurrentPeriod.Balance
        End Get
        Set(value As Decimal)
            _tmpCurrentPeriod.Balance = value
        End Set
    End Property
#End Region

#Region "Datasource"



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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDirectDistributionSecondary.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleDistributionSecondary.Enabled = value
            INDpceAddDetail.Enabled = False
            INDebtnDistribution.Enabled = False
            INDgcDetail.Enabled = value
            BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDsleDistributionSecondary.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property
#End Region

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        DirectDistributionSecondary = Nothing
        costStimation = Nothing
        ListUpdateIds = Nothing
        ProductionCenterId = Nothing
        _settingsCost = Nothing
        _previusValue = Nothing
        _productCenterPrevius = Nothing
        _tmpCurrentPeriod = Nothing
        _stateOpenPopUpGeneralExpense = Nothing
        _stateOpenPopUpProductionCenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
    End Sub

    Private Sub INDsleDistributionSecondary_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDistributionSecondary.QueryPopUp
        If INDsleDistributionSecondary.Properties.DataSource Is Nothing Then
            Using model As New MDirectDistributionSecondary(MyTag)
                INDsleDistributionSecondary.Properties.DataSource = model.GetDistributionSecondary()
            End Using
        End If
    End Sub


    Private Sub INDsleDistributionSecondary_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDistributionSecondary.EditValueChanged
        If INDsleDistributionSecondary.EditValue IsNot Nothing Then
            ListUpdateIds = Nothing
            Dim DistributionSecondaryXpo = _presenter.GetDistributionSecondary(INDsleDistributionSecondary.EditValue)
            ProductionCenterId = 0
            If DistributionSecondaryXpo IsNot Nothing Then
                ProductionCenterId = DistributionSecondaryXpo.ProductionCenterId.Id
            End If
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            Using model As New MDirectDistributionSecondary(MyTag)
                costStimation = model.GetCostEstimationXpo(INDsleDistributionSecondary.EditValue, Year, Month)
                If costStimation IsNot Nothing Then
                    _tmpCurrentPeriod.Balance = costStimation.InitialDistribution
                    INDpceAddDetail.Enabled = True
                    INDebtnDistribution.Enabled = True
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El centro de producción no tiene estimación de costo de tipo primaria"
                    INDpceAddDetail.Enabled = False
                    INDebtnDistribution.Enabled = False
                    _tmpCurrentPeriod.Balance = 0
                End If
                _tmpCurrentPeriod.LoadDate()
            End Using
            If INDtxtDescription.EditValue = "" Then
                INDtxtDescription.EditValue = INDsleDistributionSecondary.Text
            End If
        End If
    End Sub

    Private Sub FrmSecondaryDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDetail, _listActions)
        INDgvDetail.Columns.ColumnByName("colActions").Width = 70

        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()

        INDebtnDistribution.AddRangeColumns("Centro Produccion", "Unidad Medida", "Cantidad", "Valor Unidad Medida")

        LoadStatus()
        Deshacer()
    End Sub


    Private Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = Keys.Enter Then
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
                    Me.NewDirectDistributionSecondary()
                Else
                    LoadControls()
                End If
            End If
        End If
    End Sub

    Private Sub INDsleProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionCenter.QueryPopUp
        If INDsleProductionCenter.Properties.DataSource Is Nothing Then
            Using model As New MDirectDistributionSecondary(MyTag)
                INDsleProductionCenter.Properties.DataSource = model.GetDistributionSecondaryProductionCenterBySecundaryId(INDsleDistributionSecondary.EditValue)
            End Using
        End If

    End Sub

    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        Dim errors As New StringBuilder
        If INDsleProductionCenter.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un centro de producción")
        End If
        If INDsleMeasurementUnit.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una unidad de medida")
        End If
        If INDtxtMaximumAmount.EditValue <= 0 Then
            errors.AppendLine("El valor debe ser mayor a $0")
        End If

        If DirectDistributionSecondary.DirectDistributionSecondaryDetail.Where(Function(x) x.ProductionCenterId = INDsleProductionCenter.EditValue And x.MeasurementUnitId = INDsleMeasurementUnit.EditValue).FirstOrDefault() IsNot Nothing Then
            errors.AppendLine("Ya se encuentar agregado el centro de producción con la misma unidad de medida")
        End If

        If (DirectDistributionSecondary.DirectDistributionSecondaryDetail.Sum(Function(x) x.Value) + (INDtxtMaximumAmount.EditValue * INDTxtMeasureUnitValue.EditValue)) > costStimation.InitialDistribution Then
            errors.AppendLine("El no se puede agregar el item porque superaria el valor de la estimación de costo")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If

        Dim DirectDistributionSecondaryDetail As New DirectDistributionSecondaryDetail
        With DirectDistributionSecondaryDetail
            .ProductionCenterId = INDsleProductionCenter.EditValue
            .CodeNameProductionCenter = INDsleProductionCenter.Text
            .MeasurementUnitId = INDsleMeasurementUnit.EditValue
            .CodeNameMeasureUnit = INDsleMeasurementUnit.Text
            .Value = INDtxtMaximumAmount.EditValue * INDTxtMeasureUnitValue.EditValue
            .Percentage = (.Value * 100) / costStimation.InitialDistribution
        End With
        INDgcDetail.DataSource = DirectDistributionSecondary.DirectDistributionSecondaryDetail
        INDgcDetail.Refresh()
        DirectDistributionSecondary.DirectDistributionSecondaryDetail.Add(DirectDistributionSecondaryDetail)
        INDsleDistributionSecondary.Properties.ReadOnly = True
        CleanControlsPopup()
        INDgvDetail.OptionsView.ShowFooter = True
        INDsleProductionCenter.Focus()
    End Sub


    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If DirectDistributionSecondary.Status > 1 Then
            Exit Sub
        End If
        If MessageIndigo.Show("Seguro de elimiar el registro", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim detail = DirectCast(INDgvDetail.GetFocusedRow, DirectDistributionSecondaryDetail)
        detail.MarkAsDeleted()
        INDgcDetail.RefreshDataSource()
        If DirectDistributionSecondary.DirectDistributionSecondaryDetail.Count = 0 Then
            INDsleDistributionSecondary.Properties.ReadOnly = False
        End If
    End Sub


    Private Sub INDsleMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasurementUnit.QueryPopUp
        Using model As New MDirectDistributionSecondary(MyTag)
            INDsleMeasurementUnit.Properties.DataSource = model.GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(INDsleDistributionSecondary.EditValue)
        End Using
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Id > 0 Then
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

    Private Sub INDsleMeasurementUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMeasurementUnit.EditValueChanged
        If INDsleMeasurementUnit.EditValue IsNot Nothing Then
            Using model As New MDirectDistributionSecondary(MyTag)
                Dim measureUnit As InventoryMeasurementUnitXpo = model.GetInventoryMeasurementUnitById(INDsleMeasurementUnit.EditValue)
                INDTxtMeasureUnitValue.EditValue = measureUnit.CostValue
                INDTxtMeasureUnitValue.Properties.ReadOnly = Not measureUnit.AllowEditCostValue
            End Using
        End If
    End Sub

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGrid(sender, e.Rows)
    End Sub

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If DistributionSecondaryId = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un elemento de distribución secundaria"
            Exit Function
        End If
        INDgvDetail.ShowLoadingPanel()
        Me.Cursor = ChangeCursorIndigo()
        Using model As New MDistributionSecondary(MyTag)
            Dim result = Await model.SP_CopyPasteSecondaryDistribution(ListInfo, DistributionSecondaryId, costStimation.InitialDistribution)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.MensajeError) = result.Message
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDgvDetail.HideLoadingPanel()
                Exit Function
            End If
            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                If DirectDistributionSecondary.DirectDistributionSecondaryDetail IsNot Nothing AndAlso DirectDistributionSecondary.DirectDistributionSecondaryDetail.Count > 0 Then
                    If result.ObjectEmbbededAux Is Nothing Then
                        result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                    End If
                    For Each x In result.ObjectEmbbeded
                        If (From l In DirectDistributionSecondary.DirectDistributionSecondaryDetail Where l.ProductionCenterId = x.ProductionCenterId And l.MeasurementUnitId = x.MeasurementUnitId Select l).Count > 0 Then
                            result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El centro de producción " + x.CodeNameProductionCenter + " ya existe en la lista con la unidad de medida " + x.CodeNameMeasureUnit, 2))
                            Continue For
                        End If
                        If (DirectDistributionSecondary.DirectDistributionSecondaryDetail.Sum(Function(y) y.Value) + x.Value) > costStimation.InitialDistribution Then
                            result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El centro de producción " + x.CodeNameProductionCenter + " con la unidad de medida " + x.CodeNameMeasureUnit + " no se puede agregar porque superaria el valor de la estimación de costo", 2))
                            Continue For
                        End If
                        DirectDistributionSecondary.DirectDistributionSecondaryDetail.Add(x)
                    Next
                Else
                    If result.ObjectEmbbeded.Sum(Function(y) y.Value) > costStimation.InitialDistribution Then
                        result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("No se pueden agregar los items porque la sumatoria de sus valores superaria el valor de la estimación de costo", 2))
                    Else
                        result.ObjectEmbbeded.ForEach(Sub(x) DirectDistributionSecondary.DirectDistributionSecondaryDetail.Add(x))
                    End If
                End If
            End If
            If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End Using
        INDgvDetail.HideLoadingPanel()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDgcDetail.DataSource = DirectDistributionSecondary.DirectDistributionSecondaryDetail
        INDgcDetail.Refresh()
        INDsleDistributionSecondary.Properties.ReadOnly = True
    End Function

#End Region

#End Region

#Region "Methods"

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        _presenter.LoadSettingCost()
        If SettingsCost Is Nothing OrElse SettingsCost.Id = 0 Then
            Exit Sub
        End If
        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Registrado", 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))

        Dim listMonths As New List(Of Tuple(Of String, Integer))
        listMonths.Add(New Tuple(Of String, Integer)("Enero", 1))
        listMonths.Add(New Tuple(Of String, Integer)("Febrero", 2))
        listMonths.Add(New Tuple(Of String, Integer)("Marzo", 3))
        listMonths.Add(New Tuple(Of String, Integer)("Abril", 4))
        listMonths.Add(New Tuple(Of String, Integer)("Mayo", 5))
        listMonths.Add(New Tuple(Of String, Integer)("Junio", 6))
        listMonths.Add(New Tuple(Of String, Integer)("Julio", 7))
        listMonths.Add(New Tuple(Of String, Integer)("Agosto", 8))
        listMonths.Add(New Tuple(Of String, Integer)("Septiembre", 9))
        listMonths.Add(New Tuple(Of String, Integer)("Octubre", 10))
        listMonths.Add(New Tuple(Of String, Integer)("Noviembre", 11))
        listMonths.Add(New Tuple(Of String, Integer)("Diciembre", 12))

        Dim parameters As Object() = {0, 0}
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Elemento", .FieldName = "DistributionSecondaryId.CodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Mes", .FieldName = "Month", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listMonths},
                              New ColumnInfo() With {.Caption = "Año", .FieldName = "Year", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus, .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDirectDistributionSecondary
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
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    Public Sub AssigningValues() Implements IDirectDistributionSecondary.AssigningValues
        With DirectDistributionSecondary
            .Code = Code
            .DistributionSecondaryId = DistributionSecondaryId
            .Description = Description
            .Year = Year
            .Month = Month
            .CostEstimationId = costStimation.Id
            '.Status = 1
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Public Sub CleanControls() Implements IDirectDistributionSecondary.CleanControls
        ReadOnlyControls(False, INDlcRoot)
        ActionsOnControls = False
        INDbteCode.EditValue = String.Empty
        INDtxtDescription.EditValue = String.Empty
        INDsleDistributionSecondary.Properties.ReadOnly = False
        INDsleDistributionSecondary.EditValue = Nothing
        INDsleDistributionSecondary.Properties.NullText = String.Empty
        INDgcDetail.DataSource = Nothing
        DirectDistributionSecondary = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        _tmpCurrentPeriod.Balance = 0
        _tmpCurrentPeriod.LoadDate()
        Me._doc = Nothing
        ListUpdateIds = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonInteropCost As New MCommonInteropCost(Me.Tag)
                Await ModelCommonInteropCost.DeleteBlockRecordInteropCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    Private Sub CleanControlsPopup()
        INDsleProductionCenter.EditValue = Nothing
        INDsleMeasurementUnit.EditValue = Nothing
        INDTxtMeasureUnitValue.EditValue = 0
        INDtxtMaximumAmount.EditValue = 0
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub
    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        'Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me.DirectDistributionSecondary.Code, Me.DirectDistributionSecondary.Description), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity = "$#" & Me.Tag & "_" & Me.DirectDistributionSecondary.Code & "#$", .IdForm = Me.Tag, _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.DirectDistributionSecondary.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me.DirectDistributionSecondary.Code, Me.DirectDistributionSecondary.Description)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.DirectDistributionSecondary.Code)
        'End If
        'Return Me._doc
    End Function

    Private Async Sub NewDirectDistributionSecondary()
        If Sequence Is Nothing OrElse Sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        _presenter.LoadSettingCost()
        If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
            Exit Sub
        End If
        Me.DirectDistributionSecondary = New DirectDistributionSecondary()
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MCommonInteropCost(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Sub
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
        Me.ActionsOnControls = True
        Status = "1"
    End Sub

    Public Async Sub LoadControls() Implements IDirectDistributionSecondary.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            Using model As New MDirectDistributionSecondary(MyTag)


                AsyncLoader(True)
                DirectDistributionSecondary = Await model.GetDirectDistributionSecondary(Me.Code)


                If DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Id > 0 Then
                    Using ModelCommonTreasury As New MCommonInteropCost(Me.Tag)
                        Dim result = Await ModelCommonTreasury.GetBlockRecordInteropCostByIdformAndIdRecord(Me.Tag, DirectDistributionSecondary.Id)
                        'BarraBotones.StatusRecordVisible = True
                        With DirectDistributionSecondary
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            _presenter.LoadSettingCost()
                            Code = .Code
                            INDsleDistributionSecondary.Properties.DataSource = model.GetDistributionSecondary()
                            DistributionSecondaryId = .DistributionSecondaryId
                            Description = .Description
                            Year = .Year
                            Month = .Month

                            Status = .Status.ToString()

                        End With


                        INDgcDetail.DataSource = DirectDistributionSecondary.DirectDistributionSecondaryDetail

                        Me.GetDocumentIndexed(Me.Tag & "_" & Me.DirectDistributionSecondary.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordInteropCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = DirectDistributionSecondary.Id}
                            Dim operation = Await ModelCommonTreasury.SaveBlockRecordInteropCost(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            _record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(DirectDistributionSecondary.Id, MyTag, Nothing, GetType(DistributionSecondary).Name)

                        Dim _settingsCostQ As InteropCostSetting
                        Using ModelSetting As New MInteropCostSetting(Me.Tag)
                            _settingsCostQ = ModelSetting.GetInteropCostSetting()
                        End Using

                        AsyncLoader(False)
                        ActionsOnControls = True
                        If DirectDistributionSecondary.Status = 1 Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            INDpceAddDetail.Enabled = True
                            INDebtnDistribution.Enabled = True
                        Else
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                            ReadOnlyControls(True)
                        End If
                        INDsleDistributionSecondary.Properties.ReadOnly = True
                        INDbteCode.Enabled = False
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Me.NewDirectDistributionSecondary()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = "No existe el registro"
                        Deshacer()
                    End If
                End If


            End Using
        End If
    End Sub
#End Region

#Region "ICrud"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        CleanControlsPopup()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Me.BarraBotones.Focus()
        If DirectDistributionSecondary.Status <> 3 Then
            If ValidateControls() = True Then
                If INDgvDetail.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo un detalle"
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            AsyncLoader(True)
            Using model As New MDirectDistributionSecondary(MyTag)
                Dim result = Await model.SaveDirectDistributionSecondary(DirectDistributionSecondary, ListUpdateIds, Me._idCurrentSequence)
                Select Case result.StatusCode
                    Case eStatusResult.SUCCESS
                        If DirectDistributionSecondary.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._sequence.InteropCostSecuenceDetail(0).Id).RemoveAt(0)
                            End If
                        End If
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                        Me.Deshacer()
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    Case eStatusResult.WARNING
                        AsyncLoader(False)
                        INDbteCode.Enabled = False
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Case eStatusResult.EXCEPTION
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                End Select
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
            INDbteCode.Enabled = False
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewDirectDistributionSecondary()
        End If
    End Sub
#End Region

#Region "BarButton Events"

    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        'Se valida que hayan seleccionado un elemento de distribucion secundaria
        If INDsleDistributionSecondary.EditValue Is Nothing OrElse INDsleDistributionSecondary.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un elemento de distribución secundaria"
            Exit Sub
        End If

        If costStimation Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "El centro de producción seleccionado en el elemento de distribución secundaria no tiene estimación de costo de tipo primaria"
            Exit Sub
        End If

        'Se consultan los detalles de la tabla LogisticProductionCenterRecordDetail
        Dim ListXpo = _presenter.GetDataImport(ProductionCenterId, Month, Year)
        If ListXpo Is Nothing OrElse ListXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para importar"
            Exit Sub
        End If

        'Se construye el listado que va en la rejilla
        For Each itemXpo In ListXpo
            'Valido que el item que se está recorriendo esté en el listado de la rejilla
            Dim info = (From x In DirectDistributionSecondary.DirectDistributionSecondaryDetail Where x.ProductionCenterId = itemXpo.ProductionCenterId.Id And x.MeasurementUnitId = itemXpo.InventoryMeasurementUnitId.Id Select x).FirstOrDefault
            If info IsNot Nothing Then 'Si se encuentra el item en el datasource de la rejilla se actualizan los valores
                'info.Value += itemXpo.Count * itemXpo.InventoryMeasurementUnitId.CostValue
                'info.Percentage = (info.Value * 100) / costStimation.InitialDistribution
                info.QuantityTmp += itemXpo.Count
            Else 'Sino se crea la entidad de cero y se agrega a la rejilla
                Dim DirectDistributionSecondaryDetail As New DirectDistributionSecondaryDetail
                With DirectDistributionSecondaryDetail
                    .ProductionCenterId = itemXpo.ProductionCenterId.Id
                    .CodeNameProductionCenter = itemXpo.ProductionCenterId.CodeName
                    .MeasurementUnitId = itemXpo.InventoryMeasurementUnitId.Id
                    .CodeNameMeasureUnit = itemXpo.InventoryMeasurementUnitId.Code + " - " + itemXpo.InventoryMeasurementUnitId.Name
                    .QuantityTmp = itemXpo.Count
                    '.Value = 0
                    '.Percentage = 0
                    'If costStimation.InitialDistribution <> 0 Then
                    '    .Value = itemXpo.Count * itemXpo.InventoryMeasurementUnitId.CostValue
                    '    .Percentage = (.Value * 100) / costStimation.InitialDistribution
                    'End If
                End With
                DirectDistributionSecondary.DirectDistributionSecondaryDetail.Add(DirectDistributionSecondaryDetail)
            End If
        Next
        If (costStimation.InitialDistribution > 0) Then
            Dim totalSumValue As Decimal = (From x In DirectDistributionSecondary.DirectDistributionSecondaryDetail Select x.QuantityTmp).Sum()
            For Each itemDetail In DirectDistributionSecondary.DirectDistributionSecondaryDetail
                itemDetail.Percentage = Math.Round((itemDetail.QuantityTmp * 100 / totalSumValue), 2)
                itemDetail.Value = costStimation.InitialDistribution * itemDetail.Percentage / 100
            Next
            Dim sumPercentaje As Decimal = (From x In DirectDistributionSecondary.DirectDistributionSecondaryDetail Select x.Percentage).Sum()
            If (sumPercentaje > 100) Then
                DirectDistributionSecondary.DirectDistributionSecondaryDetail(DirectDistributionSecondary.DirectDistributionSecondaryDetail.Count - 1).Percentage -= (sumPercentaje - 100)
                DirectDistributionSecondary.DirectDistributionSecondaryDetail(DirectDistributionSecondary.DirectDistributionSecondaryDetail.Count - 1).Value = costStimation.InitialDistribution * DirectDistributionSecondary.DirectDistributionSecondaryDetail(DirectDistributionSecondary.DirectDistributionSecondaryDetail.Count - 1).Percentage / 100
            ElseIf (sumPercentaje < 100) Then
                DirectDistributionSecondary.DirectDistributionSecondaryDetail(DirectDistributionSecondary.DirectDistributionSecondaryDetail.Count - 1).Percentage += (100 - sumPercentaje)
                DirectDistributionSecondary.DirectDistributionSecondaryDetail(DirectDistributionSecondary.DirectDistributionSecondaryDetail.Count - 1).Value = costStimation.InitialDistribution * DirectDistributionSecondary.DirectDistributionSecondaryDetail(DirectDistributionSecondary.DirectDistributionSecondaryDetail.Count - 1).Percentage / 100
            End If
        End If

        'Se asignan los ids para enviarlos a guardar
        ListUpdateIds = (From x In ListXpo Select x.Id).ToList()

        INDgcDetail.DataSource = DirectDistributionSecondary.DirectDistributionSecondaryDetail
        INDgcDetail.Refresh()
        INDsleDistributionSecondary.Properties.ReadOnly = True
    End Sub

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
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        DirectDistributionSecondary.Status = 1
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
        DirectDistributionSecondary.Status = 1
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

    ''' <summary>
    ''' Actualizar y confirmar
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        DirectDistributionSecondary.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Guardar y confirmar
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        DirectDistributionSecondary.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Anular
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        DirectDistributionSecondary.Status = 3
        Guardar()
    End Sub

#End Region

End Class
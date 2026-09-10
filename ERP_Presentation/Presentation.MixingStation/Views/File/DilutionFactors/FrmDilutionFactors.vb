'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 06-06-2019

' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Repository
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.FrmPopupDilutionFactorsDetail
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmDilutionFactors
    Implements IDilutionFactors, ICustomizableForm

#Region "Fields"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Referencia la presentador (MixingStation.DilutionFactors)
    ''' </summary>
    Private _presenter As PDilutionFactors

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As MixingStationSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordMixingStation

    ''' <summary>
    ''' peso estandar parte numerica
    ''' </summary>
    Private _WeightStandar As Decimal

    ''' <summary>
    ''' Id de la forma farmaceutica
    ''' </summary>
    Private _pharmaceuticalFormId As Integer?

    ''' <summary>
    ''' entidad de factor de dilucion
    ''' </summary>
    Private _dilutionFactorEntity As DilutionFactors
#End Region

#Region "Propierties IDilutionFactors"

    ''' <summary>
    ''' Propiedad que contiene el código del registro
    ''' </summary>
    Public Property Code As String Implements IDilutionFactors.Code
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
    ''' Obtiene o establece el id del ATC
    ''' </summary>
    Public Property ATCId As Integer? Implements IDilutionFactors.ATCId
        Get
            Return CType(INDsleMedicament.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleMedicament.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del medicamento
    ''' </summary>
    Private _atcName As String
    Public Property ATCName As String
        Get
            Return _atcName
        End Get
        Set(value As String)
            _atcName = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre de la forma farmaceutica
    ''' </summary>
    Public WriteOnly Property PharmaceuticalFormName As String Implements IDilutionFactors.PharmaceuticalFormName
        Set(value As String)
            INDtxtFormFarm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id de la forma farmaceutica
    ''' </summary>
    ''' <returns></returns>
    Public Property PharmaceuticalFormId As Integer? Implements IDilutionFactors.PharmaceuticalFormId
        Get
            Return _pharmaceuticalFormId
        End Get
        Set(value As Integer?)
            _pharmaceuticalFormId = value
        End Set
    End Property

    ''' <summary>
    ''' presentacion del medicamento
    ''' </summary>
    Public Property PresentationMed As String Implements IDilutionFactors.PresentationMed
        Get
            Return INDtxtPresentationMed.EditValue
        End Get
        Set(value As String)
            INDtxtPresentationMed.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la unidad de medida
    ''' </summary>
    ''' <returns></returns>
    Public Property MeasurementUnitId As Integer? Implements IDilutionFactors.MeasurementUnitId
        Get
            Return CType(INDsleMeasurementUnit.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleMeasurementUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' parte decimal del peso estandar
    ''' </summary>
    ''' <returns></returns>
    Public Property WeightStandar As Decimal Implements IDilutionFactors.WeightStandar
        Get
            Return _WeightStandar
        End Get
        Set(value As Decimal)
            _WeightStandar = value
        End Set
    End Property

    ''' <summary>
    ''' peso concatenado con la unidad de medida
    ''' </summary>
    Private _weightStandarAbbreviation As String
    Public Property WeightStandarTxt As String Implements IDilutionFactors.WeightStandarTxt
        Get
            Return _weightStandarAbbreviation
        End Get
        Set(value As String)
            INDTeWeightStandar.EditValue = $"{WeightStandar} {value}"
            _weightStandarAbbreviation = value
        End Set
    End Property

    ''' <summary>
    ''' peso del medicamento
    ''' </summary>
    Private _weightATC As Decimal
    Public Property WeightATC As Decimal Implements IDilutionFactors.WeightATC
        Get
            Return _weightATC
        End Get
        Set(value As Decimal)
            _weightATC = value
        End Set
    End Property

    ''' <summary>
    ''' abreviacion del peso del medicamento
    ''' </summary>
    Private _weightATCAbbreviation As String
    Public Property WeightATCAbbreviation As String Implements IDilutionFactors.WeightATCAbbrebiation
        Get
            Return _weightATCAbbreviation
        End Get
        Set(value As String)
            _weightATCAbbreviation = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto Conteneder del medicamento seleccionado
    ''' </summary>
    Private Property Atc As ATCXpo

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Public Property Sequence As MixingStationSequence Implements IDilutionFactors.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As MixingStationSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As MixingStationSequenceDetail In Me._sequence.MixingStationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDilutionFactors.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IDilutionFactors.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public Sub ActionsOnControls(Action As Boolean, Optional _flagLoadControl As Boolean = False) Implements IDilutionFactors.ActionsOnControls

        INDlycRoot.BeginUpdate()

        INDbteCode.Enabled = Not Action
        INDsleMedicament.Enabled = If(Not _flagLoadControl, Action, Not _flagLoadControl)
        INDBtnAddThinner.Enabled = Action
        INDGcThinner.Enabled = Action

        INDlycRoot.EndUpdate()

        If Action Then
            INDsleMedicament.Focus()
        Else
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene o establece el datasource de ATC
    ''' </summary>
    Public Property ATCDatasource As XPInstantFeedbackSource Implements IDilutionFactors.ATCDatasource
        Get
            Return CType(INDsleMedicament.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMedicament.Properties.DataSource = value
        End Set
    End Property

    Property DilutionFactorsEntity As DilutionFactors Implements IDilutionFactors._dilutionFactors
        Get
            Return _dilutionFactorEntity
        End Get
        Set(value As DilutionFactors)
            _dilutionFactorEntity = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource 
    ''' </summary>
    Private _listDilutionFactorsDetails As List(Of DilutionFactorsDetail)
    Public Property ListDilutionFactorsDetails As List(Of DilutionFactorsDetail) Implements IDilutionFactors.ListDilutionFactorsDetails
        Get
            Return _listDilutionFactorsDetails
        End Get
        Set(value As List(Of DilutionFactorsDetail))
            _listDilutionFactorsDetails = value
        End Set
    End Property

    Private _measurementUnitDefault As MeasureUnitXpo
    Public Property MeasurementUnitDefault As MeasureUnitXpo Implements IDilutionFactors.MeasurementUnit
        Get
            Return _measurementUnitDefault
        End Get
        Set(value As MeasureUnitXpo)
            Me._measurementUnitDefault = value
            Me.MeasurementUnitId = value.Id
            Me.INDsleMeasurementUnit.Properties.NullText = value.CodeName
        End Set
    End Property

#End Region

#Region "Propierties ICrudBase"

    ''' <summary>
    ''' Propiedad que establece los mensajes (Advertencias)
    ''' </summary>
    ''' <param name="icono"></param>
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
    ''' Evento barra de botones Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos en el formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina el turno seleccionado
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException
    End Sub

    ''' <summary>
    ''' Guarda el turnoINDliUnitDoseType
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar

        If Validations() Then
            Exit Sub
        End If

        AssigningValues()
        Try
            Using Model As New MDilutionFactors(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult = Await Model.SaveDilutionFactorsAsync({Me.DilutionFactorsEntity}.ToList, Me._idCurrentSequence)
                AsyncLoader(False)

                If result Is Nothing OrElse Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = If(String.IsNullOrEmpty(result?.Message), "Error No identificado", result?.Message)
                    Exit Sub
                End If
                Mensaje(EeventViewerImages.Informacion) = String.Join(",", result?.MessageResult)
                Deshacer()
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Indica que el dato ya existe y se va a actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Limpia el formulario para iniciar
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo

        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewDilutionFactors()
        End If
    End Sub
#End Region

#Region "Bar Button Events"


    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence?.Id > 0 AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MixingStationSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmDilutionFactors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AsyncLoader(True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        _presenter = New PDilutionFactors(Me)
        '****Carga secuencia #***'
        Await _presenter.GetSequence()
        '****Acciones de la columna***'
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        AddActionsColumns()
        IndigoGridView1.MoreInfoColunmns(INDGvThinner)
        IndigoGridControl1.RefreshGrid(INDGcThinner)
        AsyncLoader(False)
        Deshacer()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _dilutionFactorEntity = Nothing
        _record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDilutionFactors_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedrecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewDilutionFactors()
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
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMedicament_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMedicament.QueryPopUp
        If ATCDatasource Is Nothing Then
            _presenter.InitializeATC()
        End If
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag.ToString
            Case "Activate"
                EditDetail(True)
            Case "Inactivate"
                EditDetail(False)
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega productos al paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddThinner_Click(sender As Object, e As EventArgs) Handles INDBtnAddThinner.Click
        If ValidationToAddOrdEdit() Then
            Exit Sub
        End If
        Using formulario As New FrmPopupDilutionFactorsDetail
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddDilutionFactorsDetail, AddressOf ReturnAddDilutionFactorsDetail
            formulario.AtcIdHeader = ATCId
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.5
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.WeightStandar = Me.WeightStandar
            formulario.WeightStandarAbbreviation = _weightStandarAbbreviation
            formulario.ATCName = ATCName
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDilutionFactors_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim item = TryCast(INDGvThinner.GetFocusedRow(), DilutionFactorsDetail)
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)

        Dim Activate = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Activate)))
        Dim Inactivate = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Inactivate)))
        Dim remove = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Remove)))

        Activate.Visible = False
        Inactivate.Visible = False
        remove.Visible = True

        If item?.ByDefault Then
            Inactivate.Visible = True
        Else
            Activate.Visible = True
        End If
        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * 2)
    End Sub

    ''' <summary>
    ''' popup menu showing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvThinner_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvThinner.PopupMenuShowing
        Dim Activate = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Activate)))
        Dim Inactivate = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Inactivate)))
        Dim remove = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Remove)))

        remove.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Activate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Inactivate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        If TryCast(INDGvThinner.GetFocusedRow(), DilutionFactorsDetail)?.ByDefault Then
            Activate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            Inactivate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' evento para cargar los controles derivados del medicamento principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleMedicament_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMedicament.EditValueChanged
        If ATCId Is Nothing Then
            Exit Sub
        End If

        Atc = XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.GetXPOObject(Of ATCXpo)($"Id = {ATCId}")

        Dim validation = Await _presenter.GetStabilityTableDetailByATCId(ATCId)
        If Not validation?.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "El medicamento seleccionado no tiene parametrizado 'Tabla de estabilidad'"
            Exit Sub
        End If

        If Atc IsNot Nothing Then
            Me.ATCName = Atc.AbbreviationName
            Me.PharmaceuticalFormName = Atc?.PharmaceuticalForm?.Name
            Me.PharmaceuticalFormId = Atc?.PharmaceuticalFormId
            Me.PresentationMed = $"{Atc?.Weight} {Atc?.WeightMeasureUnit?.Abbreviation}"
            MeasurementUnitId = Atc.WeightMeasureUnit.Id
            INDsleMeasurementUnit.Properties.NullText = Atc.WeightMeasureUnit.CodeName
            Me.WeightATC = Atc?.Weight
            Me.WeightATCAbbreviation = Atc?.WeightMeasureUnit?.Abbreviation
            Me.WeightStandar = (Utils.MeasureUnitConvert(WeightATCAbbreviation.ToLower(), Atc.WeightMeasureUnit.Abbreviation.ToLower())) * WeightATC
            Me.WeightStandarTxt = $"{Atc.WeightMeasureUnit.Abbreviation}"
        End If
    End Sub

#End Region

#Region "CustomColumn"

    Private Sub INDGvThinner_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvThinner.CustomColumnDisplayText
        If e.Column.FieldName = "Dilution" And e.Value IsNot Nothing Then
            Dim valor As Decimal = Convert.ToDecimal(e.Value)
            e.DisplayText = Utils.SetPartDecimalToValue(valor)
        End If
    End Sub

#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo que retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedrecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If Not INDbteCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedrecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then

            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Try
                Using Model As New MDilutionFactors(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetDilutionFactorsAsync(INDbteCode.Text.Trim)
                    INDlycRoot.BeginUpdate()

                    If resultOperation Is Nothing OrElse Not resultOperation.StateResult Then
                        Throw New Exception(resultOperation?.Message)
                    End If

                    DilutionFactorsEntity = resultOperation.ObjectEmbbeded
                    If DilutionFactorsEntity IsNot Nothing AndAlso DilutionFactorsEntity.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(DilutionFactorsEntity.Id))
                            With DilutionFactorsEntity
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                WeightStandar = .WeightStandar
                                Me.ATCId = .ATCId
                                INDsleMedicament.Properties.NullText = $"{ .ATC?.Code} - { .ATC?.Name}"
                                ATCName = .ATC?.AbbreviationName
                                PharmaceuticalFormId = .PharmaceuticalFormId
                                PharmaceuticalFormName = .PharmaceuticalForm.Name
                                PresentationMed = .PreMedic
                                MeasurementUnitId = .MeasurementUnitId
                                WeightStandarTxt = .MeasurementUnitAbbreviation
                                INDsleMeasurementUnit.Properties.NullText = .MeasurementUnitCodeName
                                Me.ListDilutionFactorsDetails = .DilutionFactorsDetail.ToList()
                                INDGcThinner.DataSource = Me.ListDilutionFactorsDetails
                                RefrescarRejilla()
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.DilutionFactorsEntity.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = DilutionFactorsEntity.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(DilutionFactorsEntity.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableConceptNotes).Name)

                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                            Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                            AsyncLoader(False)
                            ActionsOnControls(True, True)
                        End Using
                    Else
                        INDBtnAddThinner.Enabled = True
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewDilutionFactors()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = "El registro que está intentando consultar no existe"
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo que prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewDilutionFactors() As Task
        _dilutionFactorEntity = New DilutionFactors()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls(True)
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls(True)
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls(True)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls(True)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls(True)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo que abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                              New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Medicamento", .FieldName = "ATC.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDilutionFactors
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo que asigna los valores
    ''' </summary>
    Private Sub AssigningValues()
        Me.DilutionFactorsEntity = If(Me.DilutionFactorsEntity Is Nothing, New DilutionFactors, Me.DilutionFactorsEntity)

        With Me.DilutionFactorsEntity
            .Code = Code
            .ATCId = ATCId
            .PharmaceuticalFormId = PharmaceuticalFormId
            .PreMedic = PresentationMed
            .WeightStandar = WeightStandar
            .MeasurementUnitId = MeasurementUnitId
            Me.ListDilutionFactorsDetails.ForEach(Sub(x)
                                                      .DilutionFactorsDetail.Add(x)
                                                  End Sub)

        End With
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()

        INDlycRoot.BeginUpdate()
        ActionsOnControls(False)
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        'Limpiar controles
        Me._doc = Nothing
        Code = String.Empty
        ATCId = Nothing
        INDsleMedicament.Properties.NullText = Nothing
        PharmaceuticalFormId = Nothing
        PharmaceuticalFormName = String.Empty
        PresentationMed = String.Empty
        Await _presenter.DefaultMeasureUnit()
        WeightStandar = 0.00F
        WeightStandarTxt = String.Empty
        ListDilutionFactorsDetails = Nothing
        INDGcThinner.DataSource = Nothing
        RefrescarRejilla()

        INDlycRoot.EndUpdate()
        DeleteBlockedrecord()
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Refresca los datos de la rejilla
    ''' </summary>
    Private Sub RefrescarRejilla()
        INDGvThinner.RefreshData()
        INDGcThinner.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Método para agregar acciones a la rejilla de productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        IndigoGridView1.SetListAcction(INDGvThinner, {eAcciones.Activate, eAcciones.Inactivate, eAcciones.Remove}.ToList())
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvThinner.Columns
            If col.Name = "colActions" OrElse col.Name = "MoreInfo" Then
                col.Width = 75
            End If
        Next
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail(_Default As Boolean)
        If ValidationToAddOrdEdit() Then
            Exit Sub
        End If

        Dim itemToEdit = TryCast(INDGvThinner.GetFocusedRow, DilutionFactorsDetail)

        If ListDilutionFactorsDetails.Any(Function(x) x.AtcId <> itemToEdit.AtcId AndAlso x.ByDefault.HasValue And x.ByDefault) And _Default Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro por defecto"
            Exit Sub
        End If

        itemToEdit.ByDefault = _Default
        Me.Mensaje(EeventViewerImages.Informacion) = $"Se {If(_Default, "marcó", "desmarcó")} por defecto el diluyente"

        RefrescarRejilla()
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim itemToDelted = TryCast(INDGvThinner.GetFocusedRow, DilutionFactorsDetail)

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            itemToDelted.MarkAsDeleted
            ListDilutionFactorsDetails.Remove(itemToDelted)
        End If

        INDGcThinner.DataSource = Nothing
        INDGcThinner.DataSource = ListDilutionFactorsDetails
        RefrescarRejilla()
    End Sub

    ''' <summary>
    ''' evento para agregar detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddDilutionFactorsDetail(sender As Object, e As GetDilutionFactorsDetailEventArgs)
        Try
            If e Is Nothing OrElse e.ItemDetails Is Nothing Then
                Exit Sub
            End If
            If Me.ListDilutionFactorsDetails Is Nothing Then
                Me.ListDilutionFactorsDetails = New List(Of DilutionFactorsDetail)
                Me.ListDilutionFactorsDetails.Add(e.ItemDetails)
                INDGcThinner.DataSource = Me.ListDilutionFactorsDetails.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
                Exit Sub
            End If

            If Me.ListDilutionFactorsDetails.Any(Function(x) x.AtcId = e.ItemDetails.AtcId And x.ChangeTracker.State <> ObjectState.Deleted) And e.EditModeForm = False Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "El diluyente ya existe en la parametrizacion"
                Exit Sub
            End If

            Me.ListDilutionFactorsDetails.Add(e.ItemDetails)
            INDGcThinner.DataSource = Me.ListDilutionFactorsDetails.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)

        Catch ex As Exception
        Finally
            Me.RefrescarRejilla()
        End Try
    End Sub

    ''' <summary>
    ''' validacion para editar u agregar
    ''' </summary>
    ''' <returns></returns>ValidationToAddOrdEdit
    Private Function ValidationToAddOrdEdit() As Boolean
        Dim StringBuilder = New StringBuilder
        If Me.ATCId Is Nothing OrElse Me.MeasurementUnitId Is Nothing OrElse Me.WeightStandar = 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = $"Los siguiente Campos estan vacios :{If(ATCId Is Nothing, "Medicamento; ", "")}{If(MeasurementUnitId Is Nothing, "Unidad de medida; ", "")}{If(WeightStandar = 0, "Peso estandar es 0 ", "")} "
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' validacion para guardar o actualiza el registro
    ''' </summary>
    ''' <returns></returns>
    Private Function Validations() As Boolean
        If Not Me.ValidateControls() Then
            Return True
        End If
        If Not Me.ListDilutionFactorsDetails.Any(Function(r) r.ChangeTracker.State <> ObjectState.Deleted And r.ByDefault) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Debe haber por lo menos un detalle marcado como 'Por Defecto'"
            Return True
        End If
        Return False
    End Function
#End Region

End Class


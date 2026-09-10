Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.ComponentModel
Imports System.Drawing

Public Class FrmAntineoplasicosMedication
    Implements IAntineoplasicosMedication

#Region "Constant"
    Private Const NAME_MODULE As String = "Inventory"
#End Region

#Region "Variables"
    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAntineoplasicosMedication

    ''' <summary>
    ''' 
    ''' </summary>
    Dim searchMode As Boolean = False

    ''' <summary>
    ''' 
    ''' </summary> 
    Dim ListaAPM As List(Of AntineoplasicoMedication)

    ''' <summary>
    ''' 
    ''' </summary> 
    Dim ListaAPMDelete As List(Of AntineoplasicoMedication)

    ''' <summary>
    ''' 
    ''' </summary>
    Dim ListaAPMGuardar As List(Of AntineoplasicoMedication)

    Dim ListaClassification As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
#End Region

#Region "Properties"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ATCId As Integer? Implements IAntineoplasicosMedication.ATCId
        Get
            Return INDSleAtc.EditValue
        End Get
        Set(value As Integer?)
            INDSleAtc.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ClassificationId As Byte? Implements IAntineoplasicosMedication.ClassificationId
        Get
            Return INDSleClassification.EditValue
        End Get
        Set(value As Byte?)
            INDSleClassification.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAntineoplasicosMedication.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            INDSleAtc.Enabled = value
            INDSleClassification.Enabled = value
            INDSmbAdd.Enabled = value
            INDGvATCAntineo.OptionsBehavior.ReadOnly = Not value
            INDLcRoot.EndUpdate()
            INDSleAtc.Focus()
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyTag As Object Implements IAntineoplasicosMedication.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Establece el datasource de ATC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InventoryATC As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.InventoryRepository.ATCXpo) Implements IAntineoplasicosMedication.InventoryATC
        Get
            Return CType(INDSleAtc.Properties.DataSource, DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.InventoryRepository.ATCXpo))
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.InventoryRepository.ATCXpo))
            INDSleAtc.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private _idsAtc As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property IdsAtc() As String
        Get
            Return _idsAtc
        End Get
        Set(ByVal value As String)
            _idsAtc = value
        End Set
    End Property
#End Region

#Region "Events"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        searchMode = Nothing
        ListaAPM = Nothing
        ListaAPMDelete = Nothing
        IdsAtc = Nothing
        _idOperativeUnit = Nothing
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAntineoplasicoMedication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PAntineoplasicosMedication(Me)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvATCAntineo, ListActions)
        IndigoGridControl1.RefreshGrid(INDGcATCAntineo)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvATCAntineo.Columns
            If col.Name = "colActions" Then
                col.Width = 75
            End If
        Next
        Deshacer()
        'LoadControls()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDSleAtc.Enabled Then
            INDSleAtc.Focus()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    End Sub

    ''' <summary>
    ''' Acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        Dim btnEdith As DevExpress.XtraEditors.ButtonEdit
        Dim _btnTag As String = String.Empty
        btn = TryCast(sender, DevExpress.XtraEditors.SimpleButton)
        If btn Is Nothing Then
            btnEdith = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            If btnEdith IsNot Nothing Then
                _btnTag = btnEdith.Text
            End If
        Else
            _btnTag = btn.Tag
        End If

        Select Case _btnTag
            Case "Edit", "Editar"

            Case "Remove", "Eliminar"
                ChangeStateATC()

        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de forma farmaceutica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleATC_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAtc.QueryPopUp
        If INDSleAtc.Properties.DataSource Is Nothing Then
            Presenter.InitializeInventoryATC()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleAtc_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleAtc.CloseUp
        Me.IdsAtc = RecuperarSeleccionados(sender)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleAtc_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAtc.EditValueChanged
        If INDSleAtc.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDSleAtc.EditValue) Then
            CleanATC()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub CleanATC()
        IdsAtc = String.Empty
        INDSleAtc.Properties.NullText = String.Empty
        INDSleAtc.ToolTip = String.Empty
        INDGvAtc.ClearSelection()
        If InventoryATC IsNot Nothing AndAlso InventoryATC.Any(Function(_atc) _atc.CheckValue) Then
            For Each li In InventoryATC.Where(Function(_atc) _atc.CheckValue)
                li.CheckValue = False
            Next
            INDGvAtc.GridControl.RefreshDataSource()
            INDGvAtc.GridControl.Invalidate()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleATC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleATC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleAtc.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmATC
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeInventoryATC()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAdd_Click(sender As Object, e As EventArgs) Handles INDSmbAdd.Click
        Dim _mensaje As String = String.Empty
        If String.IsNullOrEmpty(IdsAtc) Then
            INDSleAtc.Focus()
            _mensaje = "ATC"
        End If
        If ClassificationId Is Nothing Then
            If String.IsNullOrEmpty(_mensaje) Then INDSleClassification.Focus()
            _mensaje = String.Format("{0}{1}{2}", _mensaje, IIf(String.IsNullOrEmpty(_mensaje), "", Environment.NewLine), "Clasificación CAC")
        End If
        If String.IsNullOrEmpty(_mensaje) Then

            If ListaAPM Is Nothing Then
                ListaAPM = New List(Of AntineoplasicoMedication)
            End If
            If ListaAPM.Any(Function(APM) IdsAtc.Contains("[" & APM.AtcId & "]")) Then
                _mensaje = "Los siguientes ATC ya estan configurados: " & Environment.NewLine & String.Join(", ",
                            ListaAPM.Where(Function(APM) IdsAtc.Contains("[" & APM.AtcId & "]")).Select(Function(APM) APM.CodeNameATC).ToArray)
                Mensaje(EeventViewerImages.Advertencia) = _mensaje
                Exit Sub
            Else
                Dim APM As AntineoplasicoMedication
                Dim _atcXpo As Infrastructure.Data.Xpo.InventoryRepository.ATCXpo
                Dim _id As Integer = 0
                Using Model As New MAntineoplasicosMedication(Me.Tag.ToString())
                    For Each idAtc In IdsAtc.Split(",")
                        _id = idAtc.Replace("[", "").Replace("]", "")
                        _atcXpo = Model.GetATCXpo(_id)
                        APM = New AntineoplasicoMedication() With {.AtcId = _id, .ClassificationId = ClassificationId, .StateAPM = 1,
                        .CodeNameATC = IIf(_atcXpo IsNot Nothing, _atcXpo.CodeName, ""), .NameClassification = INDSleClassification.Text}
                        ListaAPM.Add(APM)
                    Next
                End Using
            End If
            INDGcATCAntineo.RefreshDataSource()
            CleanAddControls()
        Else
            Mensaje(EeventViewerImages.Advertencia) = _mensaje
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub CleanAddControls()
        ATCId = Nothing
        ClassificationId = Nothing
        CleanATC()
        'INDSmbAdd.Text = "Agregar"
        INDSleAtc.Properties.ReadOnly = False
        INDSleClassification.Properties.ReadOnly = False
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GridViewColumnHeaderExtender1_ColumnCheckedChanged(sender As Object, e As ColumnCheckedChangedEventArgs) Handles GridViewColumnHeaderExtender1.ColumnCheckedChanged

        If InventoryATC IsNot Nothing AndAlso InventoryATC.Count() > 0 Then
            For Each li In InventoryATC
                li.CheckValue = e.Checked
            Next
        End If
        INDGvAtc.GridControl.RefreshDataSource()
        INDGvAtc.GridControl.Invalidate()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvAtc_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvAtc.RowCellClick
        If e.Column.FieldName = "CheckValue" Then
            Dim _atc = TryCast(INDGvAtc.GetFocusedRow(), Infrastructure.Data.Xpo.InventoryRepository.ATCXpo)
            If _atc IsNot Nothing Then
                _atc.CheckValue = Not _atc.CheckValue
                INDGvAtc.GridControl.RefreshDataSource()
                INDGvAtc.GridControl.Invalidate()
            End If
        End If
    End Sub

#Region "bar buttons and events"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    ''' <summary>
    '''Evento load de la barra de fondos.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        searchMode = False
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

#End Region
#End Region

#Region "Process"
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Marcas.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        LoadControls()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar

        If (ListaAPM Is Nothing OrElse ListaAPM.Count = 0 OrElse Not ListaAPM.Any(Function(APM) APM.ChangeTracker.State = ObjectState.Added)) AndAlso (ListaAPMDelete Is Nothing OrElse ListaAPMDelete.Count = 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Agregue algun medicamento Antineoplásicos CAC"
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MAntineoplasicosMedication(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of AntineoplasicoMedication) = Await Model.SaveAntineoplasicosMedicationAsync(ListaAPMGuardar)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    AsyncLoader(False)
                    Deshacer()
                    'LoadControls()
                Else
                    AsyncLoader(False)
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            ShowMessage(eStatusResult.WARNING) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' EL
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDLcRoot.BeginUpdate()
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        CleanAddControls()
        ListaAPM = Nothing
        ListaAPMDelete = Nothing
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        INDLcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        If ListaAPMGuardar Is Nothing Then
            ListaAPMGuardar = New List(Of AntineoplasicoMedication)
        Else
            ListaAPMGuardar.Clear()
        End If

        If ListaAPMDelete IsNot Nothing AndAlso ListaAPMDelete.Count > 0 Then
            For Each APM In ListaAPMDelete
                ListaAPMGuardar.Add(APM)
            Next
        End If
        If ListaAPM IsNot Nothing AndAlso ListaAPM.Any(Function(APM) APM.ChangeTracker.State = ObjectState.Added) Then
            For Each APMa In ListaAPM.Where(Function(APM) APM.ChangeTracker.State = ObjectState.Added)
                ListaAPMGuardar.Add(APMa)
            Next
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub ChangeStateATC()
        Dim _APM As AntineoplasicoMedication = INDGvATCAntineo.GetRow(INDGvATCAntineo.FocusedRowHandle)
        If _APM IsNot Nothing Then
            _APM.MarkAsDeleted()
            ListaAPM.Remove(_APM)

            If _APM.Id > 0 Then
                If ListaAPMDelete Is Nothing Then
                    ListaAPMDelete = New List(Of AntineoplasicoMedication)
                End If
                ListaAPMDelete.Add(_APM)
            End If
            INDGcATCAntineo.RefreshDataSource()
            CleanAddControls()
        End If
    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        Try
            If ListaClassification Is Nothing Then
                ListaClassification = New List(Of Tuple(Of Byte, String))
                ListaClassification.Add(New Tuple(Of Byte, String)(1, "Bleomicina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(2, "Busulfano"))
                ListaClassification.Add(New Tuple(Of Byte, String)(3, "Capecitabina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(4, "Carboplatino"))
                ListaClassification.Add(New Tuple(Of Byte, String)(5, "Ciclofosfamida"))
                ListaClassification.Add(New Tuple(Of Byte, String)(6, "Ciclosporina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(7, "Cisplatino"))
                ListaClassification.Add(New Tuple(Of Byte, String)(8, "Citarabina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(9, "Clorambucilo"))
                ListaClassification.Add(New Tuple(Of Byte, String)(10, "Dacarbazina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(11, "Doxorubicina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(12, "Etopósido"))
                ListaClassification.Add(New Tuple(Of Byte, String)(13, "Fluorouracilo"))
                ListaClassification.Add(New Tuple(Of Byte, String)(14, "Gemcitabina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(15, "Imatinib"))
                ListaClassification.Add(New Tuple(Of Byte, String)(16, "Interferón Alfa"))
                ListaClassification.Add(New Tuple(Of Byte, String)(17, "Melfalan"))
                ListaClassification.Add(New Tuple(Of Byte, String)(18, "Mercaptopurina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(19, "Metotrexato"))
                ListaClassification.Add(New Tuple(Of Byte, String)(20, "Paclitaxel"))
                ListaClassification.Add(New Tuple(Of Byte, String)(21, "Pegfilgrastim"))
                ListaClassification.Add(New Tuple(Of Byte, String)(22, "Procarbazina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(23, "Rituximab"))
                ListaClassification.Add(New Tuple(Of Byte, String)(24, "Tamoxifeno"))
                ListaClassification.Add(New Tuple(Of Byte, String)(25, "Tioguanina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(26, "Trastuzumab"))
                ListaClassification.Add(New Tuple(Of Byte, String)(27, "Vinblastina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(28, "Vincristina"))
                ListaClassification.Add(New Tuple(Of Byte, String)(29, "Prednisona"))
                ListaClassification.Add(New Tuple(Of Byte, String)(30, "Prednisolona"))
                ListaClassification.Add(New Tuple(Of Byte, String)(31, "Metilprednisolona"))
                ListaClassification.Add(New Tuple(Of Byte, String)(32, "Dexametasona"))
                ListaClassification.Add(New Tuple(Of Byte, String)(33, "Otra clasificación de antineoplásico"))

            End If
            INDSleClassification.Properties.DataSource = ListaClassification
            Using Model As New MAntineoplasicosMedication(CStr(Me.Tag))
                AsyncLoader(True)
                ListaAPM = Nothing
                Dim res As ActionResult(Of List(Of Domain.Entities.AntineoplasicoMedication))
                res = Await Model.GetAntineoplasicosMedicationByStatesAsync("1")
                If res IsNot Nothing Then
                    ListaAPM = res.ObjectEmbbeded
                End If
                INDLcRoot.BeginUpdate()
                INDGcATCAntineo.DataSource = ListaAPM
                ActionsOnControls = True
                AsyncLoader(False)
                INDSleAtc.Focus()
                INDLcRoot.EndUpdate()
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDSleAtc.Enabled = False
            ShowMessage(eStatusResult.WARNING) = ex.Message
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    Private Function RecuperarSeleccionados(sender As Object) As String
        Dim edit As DevExpress.XtraEditors.SearchLookUpEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim identificadores As String = String.Empty
        Dim identificador As String = String.Empty
        Dim descripciones As String = String.Empty
        Dim separador As String = String.Empty
        If InventoryATC IsNot Nothing AndAlso InventoryATC.Count() > 0 AndAlso InventoryATC.Any(Function(_atc) _atc.CheckValue) Then
            For Each li In InventoryATC.Where(Function(_atc) _atc.CheckValue)
                identificador = li.Id
                If Not String.IsNullOrEmpty(identificador) Then
                    identificadores += separador & "[" & identificador & "]"
                    descripciones += separador & li.Code
                    separador = ","
                End If
            Next
        End If

        edit.Properties.NullText = descripciones.ToString()
        edit.ToolTip = descripciones.ToString()
        Return identificadores.ToString()
    End Function

#End Region


#Region "Customizacion"
    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLcRoot.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLcRoot.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub
#End Region
End Class
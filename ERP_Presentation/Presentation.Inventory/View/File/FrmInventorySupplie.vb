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

Public Class FrmInventorySupplie
    Implements IInventorySupplie

#Region "Constant"
    Private Const NAME_MODULE As String = "Inventory"
#End Region

#Region "Variables"
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As InventorySequence

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PInventorySupplie

    Dim searchMode As Boolean = False

    ''' <summary>
    ''' Variable que contiene la entidad de los estados de insumo
    ''' </summary> 
    Dim ObjInventorySupplie As InventorySupplie

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad de Codigo del Insumo
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IInventorySupplie.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de Nombre del Insumo
    ''' </summary>
    ''' <returns></returns>
    Public Property SupplieName As String Implements IInventorySupplie.SupplieName
        Get
            Return INDTxeName.Text
        End Get
        Set(value As String)
            INDTxeName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del Nivel de Riesgo del Insumo
    ''' </summary>
    ''' <returns></returns>
    Public Property RiskLevelId As Integer? Implements IInventorySupplie.RiskLevelId
        Get
            Return INDSleLevelRisk.EditValue
        End Get
        Set(value As Integer?)
            INDSleLevelRisk.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del Producto PBS
    ''' </summary>
    ''' <returns></returns>
    Public Property PBSProduct As Boolean? Implements IInventorySupplie.PBSProduct
        Get
            Return INDGleProductPBS.EditValue
        End Get
        Set(value As Boolean?)
            INDGleProductPBS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de si el insumo Exige justificacion de Insumos/Dispositivos
    ''' </summary>
    ''' <returns></returns>
    Public Property JustificationOfInputs As Boolean? Implements IInventorySupplie.JustificationOfInputs
        Get
            Return INDsleJustificationOfInputs.EditValue
        End Get
        Set(value As Boolean?)
            INDsleJustificationOfInputs.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de si el insumo Exige justificacion de Insumos/Dispositivos
    ''' </summary>
    ''' <returns></returns>
    Public Property OsteosynthesisMaterial As Boolean? Implements IInventorySupplie.OsteosynthesisMaterial
        Get
            Return INDsleOsteosynthesisMaterial.EditValue
        End Get
        Set(value As Boolean?)
            INDsleOsteosynthesisMaterial.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad si el Insumo es De consumo
    ''' </summary>
    ''' <returns></returns>
    Public Property Consumption As Boolean? Implements IInventorySupplie.Consumption
        Get
            Return INDsleConsumption.EditValue
        End Get
        Set(value As Boolean?)
            INDsleConsumption.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad si el Insumo es un Dispositivo de optometría
    ''' </summary>
    ''' <returns></returns>
    Public Property OptometryDevice As Boolean? Implements IInventorySupplie.OptometryDevice
        Get
            Return INDsleOptometryDevice.EditValue
        End Get
        Set(value As Boolean?)
            INDsleOptometryDevice.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad si el Insumo es un Dispositivo Medico
    ''' </summary>
    ''' <returns></returns>
    Public Property MedicalDevice As Boolean? Implements IInventorySupplie.MedicalDevice
        Get
            Return INDsleMedicalDevice.EditValue
        End Get
        Set(value As Boolean?)
            INDsleMedicalDevice.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad si el Insumo se usa en una nutrición parenteral o no
    ''' </summary>
    ''' <returns></returns>
    Public Property IsParenteralNutritionSupply As Boolean? Implements IInventorySupplie.IsParenteralNutritionSupply
        Get
            Return INDSleIsParenteralNutritionSupply.EditValue
        End Get
        Set(value As Boolean?)
            INDSleIsParenteralNutritionSupply.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del estado de estado de insumo
    ''' </summary>
    Public Property Status As Boolean Implements IInventorySupplie.Status

        Get
            Return IIf(BarraBotones.StatusRecord = eActionsStatusRecords.Active, True, False)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As InventorySequence Implements IInventorySupplie.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)

            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInventorySupplie.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDTxeName.Enabled = value
            INDSleLevelRisk.Enabled = value
            INDGleProductPBS.Enabled = value
            INDsleJustificationOfInputs.Enabled = value
            INDsleOsteosynthesisMaterial.Enabled = value
            INDsleConsumption.Enabled = value
            INDsleOptometryDevice.Enabled = value
            INDsleMedicalDevice.Enabled = value
            INDSleIsParenteralNutritionSupply.Enabled = value
            'Me.BarraBotones.StatusRecordEnabled = value
            INDLcRoot.EndUpdate()
            If value Then
                INDTxeName.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyTag As Object Implements IInventorySupplie.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Establece el datasource de la forma farmaceutica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InventoryRiskLevel As DevExpress.Xpo.XPInstantFeedbackSource Implements IInventorySupplie.InventoryRiskLevel
        Get
            Return CType(INDSleLevelRisk.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleLevelRisk.Properties.DataSource = value
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


#End Region

#Region "Events"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBtnCode.KeyDown
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
                    Await Me.NewInventorySupplie()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        Presenter = Nothing
        searchMode = Nothing
        ObjInventorySupplie = Nothing
        record = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmInventorySupplie_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PInventorySupplie(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If ObjInventorySupplie IsNot Nothing AndAlso ObjInventorySupplie.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de forma farmaceutica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleLevelRisk_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleLevelRisk.QueryPopUp
        If INDSleLevelRisk.Properties.DataSource Is Nothing Then
            Presenter.InitializeInventoryRiskLevel()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleRiskLevel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleLevelRisk_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleLevelRisk.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmInventoryRiskLevel
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeInventoryRiskLevel()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de si el insumo esta incluido en el plan de beneficios de salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleProductPBS_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDGleProductPBS.QueryPopUp
        If INDGleProductPBS.Properties.DataSource Is Nothing Then
            Dim ListYesNo As List(Of Tuple(Of Boolean, String))
            ListYesNo = New List(Of Tuple(Of Boolean, String))()
            ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
            ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
            INDGleProductPBS.Properties.DataSource = ListYesNo
        End If
    End Sub

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de fondos.
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        searchMode = False
        Me.Deshacer()
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
        Me.Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

#End Region
#End Region

#Region "Process"


    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(80)}, New ColumnInfo() With {.Caption = "Nombre", .FieldName = "SupplieName"}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInventorySupplie
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBtnCode.Text = ReturnValue
        If INDBtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Marcas.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        If ObjInventorySupplie IsNot Nothing AndAlso ObjInventorySupplie.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MInventorySupplie(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteInventorySupplieAsync(ObjInventorySupplie)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    Private Function Validations() As Boolean
        If OsteosynthesisMaterial = True AndAlso OptometryDevice = True Then
            Mensaje(EeventViewerImages.Advertencia) = "Si es dispositivo de optometría no puede ser material de osteosíntesis"
            Return False
        End If

        Return True
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        If Not Validations() Then
            Exit Sub
        End If

        AssigningValues()
        Try
            Using Model As New MInventorySupplie(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of InventorySupplie) = Await Model.SaveInventorySupplieAsync(ObjInventorySupplie, Me._idCurrentSequence, Me.Sequense)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If ObjInventorySupplie.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    ObjInventorySupplie = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewInventorySupplie()
        End If
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
        Status = True
        INDBtnCode.Text = String.Empty
        INDTxeName.Text = String.Empty
        RiskLevelId = Nothing
        INDSleLevelRisk.Properties.NullText = String.Empty
        PBSProduct = Nothing
        INDGleProductPBS.Properties.NullText = String.Empty
        JustificationOfInputs = Nothing
        OsteosynthesisMaterial = Nothing
        Consumption = Nothing
        OptometryDevice = Nothing
        INDsleOptometryDevice.Properties.NullText = String.Empty
        MedicalDevice = Nothing
        INDsleMedicalDevice.Properties.NullText = String.Empty
        IsParenteralNutritionSupply = Nothing
        'Limpiar controles
        ObjInventorySupplie = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        ShowSpecificControlsByCulture()
        INDLcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With ObjInventorySupplie
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .SupplieName = SupplieName
            .RiskLevelId = RiskLevelId.GetValueOrDefault
            .PBSProduct = PBSProduct.GetValueOrDefault
            .JustificationOfInputs = JustificationOfInputs.GetValueOrDefault
            .OsteosynthesisMaterial = OsteosynthesisMaterial.GetValueOrDefault
            .Consumption = Consumption.GetValueOrDefault
            .OptometryDevice = OptometryDevice.GetValueOrDefault
            .MedicalDevice = MedicalDevice.GetValueOrDefault
            .IsParenteralNutritionSupply = IsParenteralNutritionSupply.GetValueOrDefault
        End With
    End Sub


#End Region

#Region "Functions"

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
                Using Model As New MInventorySupplie(CStr(Me.Tag))
                    AsyncLoader(True)
                    ObjInventorySupplie = Nothing
                    Dim res As ActionResult(Of Domain.Entities.InventorySupplie)
                    res = Await Model.GetInventorySupplieAsync(INDBtnCode.Text)
                    If res IsNot Nothing Then
                        ObjInventorySupplie = res.ObjectEmbbeded
                        INDLcRoot.BeginUpdate()
                        If ObjInventorySupplie IsNot Nothing AndAlso ObjInventorySupplie.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True

                            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                                record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(ObjInventorySupplie.Id))
                                With ObjInventorySupplie
                                    LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                    'Llenar Entidad
                                    Code = .Code
                                    SupplieName = .SupplieName
                                    RiskLevelId = .RiskLevelId
                                    INDSleLevelRisk.Properties.NullText = .NullTextLevelRisk
                                    PBSProduct = .PBSProduct
                                    INDGleProductPBS.Properties.NullText = IIf(.PBSProduct, "Si", "No")
                                    JustificationOfInputs = .JustificationOfInputs
                                    OsteosynthesisMaterial = .OsteosynthesisMaterial
                                    Consumption = .Consumption
                                    Status = .SupplieStatus
                                    OptometryDevice = .OptometryDevice
                                    MedicalDevice = .MedicalDevice
                                    IsParenteralNutritionSupply = .IsParenteralNutritionSupply
                                    ShowSpecificControlsByCulture()
                                End With
                                'Llenar NullText
                                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & ObjInventorySupplie.Code)
                                If record.Id = 0 Then
                                    record = (Await ModelRecord.SaveBlockRecord(
                                        New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                            .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = ObjInventorySupplie.Id})
                                        ).ObjectEmbbeded
                                Else
                                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                                End If
                                Me.BarraBotones.SetDocuments(ObjInventorySupplie.Id, Me.Tag.ToString(), Nothing, GetType(InventorySupplie).Name)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                                AsyncLoader(False)
                                ActionsOnControls = True
                            End Using
                        End If
                    End If
                    If res Is Nothing OrElse ObjInventorySupplie Is Nothing OrElse ObjInventorySupplie.Id = 0 Then
                        AsyncLoader(False)

                        If Me._sequence.IsManual Then
                            Await Me.NewInventorySupplie()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDLcRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), ObjInventorySupplie.Code, ObjInventorySupplie.SupplieName, INDTxeName.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & ObjInventorySupplie.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), ObjInventorySupplie.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), ObjInventorySupplie.Code, ObjInventorySupplie.SupplieName, INDTxeName.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), ObjInventorySupplie.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewInventorySupplie() As Task
        ObjInventorySupplie = New InventorySupplie() With {.SupplieStatus = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(ObjInventorySupplie.Code) Then
            Try
                Using model As New MInventorySupplie(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not ObjInventorySupplie.SupplieStatus
                    Dim result As ActionResult(Of InventorySupplie) = Await model.ChangeStateStatus(ObjInventorySupplie.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        ObjInventorySupplie = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Oculta o muestra algunos controles requeridos para cultura diferente a Colombia
    ''' </summary>
    Private Sub ShowSpecificControlsByCulture()
        If Me.indigo.Culture.Name <> "es-CO" Then
            INDLciProductPBS.HideLayout()               'Insumo es un Producto PBS
            PBSProduct = True
            INDlyItemJustificationOfInputs.HideLayout() 'Insumo Exige justificacion de insumos/dispositivos
            JustificationOfInputs = False
            INDlyItemConsumption.HideLayout()           'Insumo es De consumo
            Consumption = False
            INDlyItemOptometryDevice.HideLayout()       'Insumo es Dispositivo de optometria
            OptometryDevice = False
            INDlyItemMedicalDevice.HideLayout()       'Insumo es Dispositivo de optometria
            MedicalDevice = False
            INDlyIsParenteralNutritionSupply.HideLayout()   'Insumo es de nutrición parenteral
            IsParenteralNutritionSupply = False
        Else
            INDLciProductPBS.ShowLayout()
            INDlyItemJustificationOfInputs.ShowLayout()
            INDlyItemConsumption.ShowLayout()
            INDlyItemOptometryDevice.ShowLayout()
            INDlyItemMedicalDevice.ShowLayout()
        End If
    End Sub
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
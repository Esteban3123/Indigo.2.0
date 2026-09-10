#Region "Imports"

Imports System.ComponentModel
Imports DevExpress
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Maintenance.MVP

#End Region

Public Class FrmMaintenanceContract
    Implements IMaintenanceContract, ICustomizableForm
    Private Const NAME_MODULE As String = "Maintenance"

#Region "Globals"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PMaintenanceContract

    ''' <summary>
    ''' Representa el modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MMaintenanceContract

    ''' <summary>
    ''' Representa la entidad de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim contract As MaintenanceContract

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMaintenance

    ''' <summary>
    ''' variable que contiene el tipo de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ContractType As InventoryContractType

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemContractDetail As MaintenanceContractDetail

    Dim _isLoading As Boolean

    Dim OnlyRead As Boolean = False

    Dim nameResourse As String = String.Empty

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

#End Region

#Region " Properties"

    ''' <summary>
    ''' Obtiene o estableces el codigo del  contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IMaintenanceContract.Code
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
    ''' Obtiene o estableces el id del tipo de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractTypeId As Integer Implements IMaintenanceContract.ContractTypeId
        Get
            Return INDGleContractType.EditValue
        End Get
        Set(value As Integer)
            INDGleContractType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IMaintenanceContract.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la fecha incial de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As Date? Implements IMaintenanceContract.InitialDate
        Get
            Return INDDteInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDDteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la fecha final de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As Date? Implements IMaintenanceContract.EndDate
        Get
            Return INDDteEndDate.EditValue
        End Get
        Set(value As Date?)
            INDDteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la descripcion de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IMaintenanceContract.Description
        Get
            If (INDMmoDescription.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDMmoDescription.Text
            End If
        End Get
        Set(value As String)
            INDMmoDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el numero de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractNumber As String Implements IMaintenanceContract.ContractNumber
        Get
            If (INDTxtContractNumber.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtContractNumber.Text
            End If
        End Get
        Set(value As String)
            INDTxtContractNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces las lineas de distribucion del proveedor del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _supplierId As Integer

    Public Property SupplierId As Integer Implements IMaintenanceContract.SupplierId
        Get
            Return _supplierId
        End Get
        Set(value As Integer)
            _supplierId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el proveedor del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineId As Integer Implements IMaintenanceContract.SupplierDistributionLineId
        Get
            Return INDSleSupplier.EditValue
        End Get
        Set(value As Integer)
            INDSleSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la exclusividad del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Exclusivity As Boolean Implements IMaintenanceContract.Exclusivity
        Get
            Return INDGleExclusivity.EditValue
        End Get
        Set(value As Boolean)
            INDGleExclusivity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el origen del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceOrder As Byte Implements IMaintenanceContract.SourceOrder
        Get
            Return INDGleSourcerOrder.EditValue
        End Get
        Set(value As Byte)
            INDGleSourcerOrder.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si es garantia unica del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OnlyGuarantee As Boolean Implements IMaintenanceContract.OnlyGuarantee
        Get
            Return INDGleOnlyGuarantee.EditValue
        End Get
        Set(value As Boolean)
            INDGleOnlyGuarantee.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la supervicion tecnica del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TechnicalSupervicion As String Implements IMaintenanceContract.TechnicalSupervicion
        Get
            If (INDTxtTechnicalSupervicion.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtTechnicalSupervicion.Text
            End If
        End Get
        Set(value As String)
            INDTxtTechnicalSupervicion.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la supervicion tecnica del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupervisionExecution As String Implements IMaintenanceContract.SupervisionExecution
        Get
            If (INDTxtSupervicionExecution.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtSupervicionExecution.Text
            End If
        End Get
        Set(value As String)
            INDTxtSupervicionExecution.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las clausulas del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Clauses As String Implements IMaintenanceContract.Clauses
        Get
            If (INDMmoClauses.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDMmoClauses.Text
            End If
        End Get
        Set(value As String)
            INDMmoClauses.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los anexos del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Attachments As String Implements IMaintenanceContract.Attachments
        Get
            If (INDMmoAttachments.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDMmoAttachments.Text
            End If
        End Get
        Set(value As String)
            INDMmoAttachments.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IMaintenanceContract.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.MaintenanceSequence

    Public Property Sequense As MaintenanceSequence Implements IMaintenanceContract.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMaintenanceContract.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IMaintenanceContract.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Establece las acciones de abilitacion de los controles del frontal
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IMaintenanceContract.ActionsOnControls
        Set(value As Boolean)
            ' Datos Principales
            INDlyContract.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDGleContractType.Enabled = value
            INDdeDocumentDate.Enabled = value
            INDTxtContractNumber.Enabled = value
            INDDteInitialDate.Enabled = value
            INDTxtContractNumber.Enabled = value
            INDDteEndDate.Enabled = value
            INDMmoDescription.Enabled = value

            ' Detalles del Contrato
            INDSleSupplier.Enabled = value
            INDGleSourcerOrder.Enabled = value
            INDGleExclusivity.Enabled = value

            ' Información Adicional
            INDGleOnlyGuarantee.Enabled = value
            INDTxtTechnicalSupervicion.Enabled = value
            INDTxtSupervicionExecution.Enabled = value

            ' Clausulas y Anexos
            INDMmoClauses.Enabled = value
            INDMmoAttachments.Enabled = value

            'Productos
            INDBtnAdd.Enabled = value
            INDGcItemByPlate.Enabled = value

            BarraBotones.StatusRecordVisible = value
            INDlyContract.EndUpdate()
            If value Then
                INDGleContractType.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el proveedor con sus lineas de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuppliersDistributionLinesXpo As Xpo.XPInstantFeedbackSource Implements IMaintenanceContract.SuppliersDistributionLinesXpo
        Get
            Return CType(INDSleSupplier.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As Xpo.XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "DataSource"

    ''' <summary>
    ''' Especifica el origen segun de la Cuantia
    ''' </summary>
    ''' <remarks></remarks>
    Private _source As List(Of Tuple(Of Byte, String))

    Private ReadOnly Property ListSource As List(Of Tuple(Of Byte, String))
        Get
            If _source Is Nothing Then
                _source = New List(Of Tuple(Of Byte, String))
                _source.Add(New Tuple(Of Byte, String)(1, "Orden Simple"))
                _source.Add(New Tuple(Of Byte, String)(2, "Llamado Oferta"))
                _source.Add(New Tuple(Of Byte, String)(3, "Licitacion Publica"))
            End If
            Return _source
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del control de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListSupplier As XPInstantFeedbackSource Implements IMaintenanceContract.ListSupplier
        Get
            Return INDSleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del control de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListContractType As XPInstantFeedbackSource Implements IMaintenanceContract.ListContractType
        Get
            Return INDGleContractType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGleContractType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemPlateXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlItemPlate.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlItemPlate.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewContract()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Eliminar.
    ''' </summary>
    Public Sub Anular() Implements ICrudBase.Eliminar
        If Status <> 1 Then
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("MaintenanceContract_ContractConfirm", NAME_MODULE)
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.Status = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Guardar.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() Then
        Else
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MMaintenanceContract(Me.Tag.ToString())
                AsyncLoader(True)

                Dim Result = Await model.SaveMaintenanceContractAsync(contract, _idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If contract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me.contract = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult.Count > 0 AndAlso Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Events"

#Region "Activated"

    ''' <summary>
    ''' Se dispara cuando el formulario se activa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMaintenanceContract_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub

#End Region

#Region "FromClosing"

    ''' <summary>
    ''' Se dispara cuando el formulario se va a cerrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMaintenanceContract_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        Model = Nothing
        contract = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        _ContractType = Nothing
        ItemContractDetail = Nothing
        OnlyRead = Nothing
        _isLoading = Nothing
        nameResourse = Nothing
        varImp = Nothing
        _source = Nothing
    End Sub

    ''' <summary>
    ''' Se dispara cuando incial la carga del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMaintenanceContract_LoadAsync(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        AddActionsColumns()
        Me.LayoutControls.SetIsCustomizable(Me.INDlyContract, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PMaintenanceContract(Me)
        Presenter.GetSequense()
        INDGleSourcerOrder.Properties.DataSource = ListSource
        LoadStatus()
        Deshacer()
        INDGleSourcerOrder.EditValue = 1

    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control para cargar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
                    Await Me.NewContract()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Realiza la consulta del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If INDSleSupplier.Properties.DataSource Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleContractType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDGleContractType.QueryPopUp
        If INDGleContractType.Properties.DataSource Is Nothing Then
            Presenter.LoadContractTypesByStatus()
        End If
    End Sub

    Private Sub INDSlItemPlate_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlItemPlate.QueryPopUp
        If INDSlItemPlate.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ItemPlateXPO Is Nothing Then
            Using model As New MMaintenanceContract(MyBase.Tag)
                ItemPlateXPO = model.ListPhysicalAsset()
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplier.EditValueChanged
        If SupplierDistributionLineId > 0 Then
            If _isLoading = False Then
                Dim suppplierMainAccount = DirectCast(DirectCast(viewSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                SupplierId = suppplierMainAccount.IdSupplier.Id
            End If
        Else
            SupplierId = 0
        End If
    End Sub

    Private Sub INDDteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteInitialDate.EditValueChanged
        If INDDteInitialDate.EditValue IsNot Nothing Then
            INDDteEndDate.Properties.MinValue = INDDteInitialDate.EditValue
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' accion para agrgar articulo  la gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If INDSlItemPlate.EditValue IsNot Nothing Then

            If contract.MaintenanceContractDetail.Where(Function(x) x.PhysicalAssetId = INDSlItemPlate.EditValue).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El articulo ya se encuentra agregado"
                Return
            End If

            Dim entityXpo = Presenter.ListFixedAssetPhysicalAssetById(INDSlItemPlate.EditValue)
            Dim MaintenanceContractDetail As New MaintenanceContractDetail()
            MaintenanceContractDetail.PhysicalAssetId = entityXpo.Id
            MaintenanceContractDetail.DescriptionPlateName = entityXpo.Plate
            MaintenanceContractDetail.DescriptionItem = entityXpo.ItemId.Description
            contract.MaintenanceContractDetail.Add(MaintenanceContractDetail)

            If contract.ChangeTracker.State = ObjectState.Unchanged Then
                contract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            INDSlItemPlate.EditValue = Nothing
            INDGcItemByPlate.DataSource = contract.MaintenanceContractDetail
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Elija un activo fijo"
        End If

    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.ButtonEdit = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case button.Text
            Case "Eliminar"
                DeleteProduct()
        End Select
        INDGcItemByPlate.DataSource = Nothing
        INDGcItemByPlate.DataSource = contract.MaintenanceContractDetail.ToList()

    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvPlate, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvPlate.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Elimina el activo
    ''' </summary>
    Private Sub DeleteProduct()

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim Objcontract = CType(INDGvPlate.GetFocusedRow(), MaintenanceContractDetail)
            Objcontract.MarkAsDeleted()
            contract.MarkAsModified()
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.contract.Code, Me.contract.ContractNumber),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.contract.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.contract.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.contract.Code, Me.contract.ContractNumber)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.contract.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusLegalized"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Genera una nueva entidad de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function NewContract() As Task
        contract = New MaintenanceContract()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

    End Function

    ''' <summary>
    ''' Despliega el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Tipo Contrato", .FieldName = "ContractTypeId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "No. Contrato", .FieldName = "ContractNumber", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha Inicial", .FieldName = "InitialDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha Final", .FieldName = "EndDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Proveedor", .FieldName = "SupplierId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMaintenanceContract
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyContract.BeginUpdate()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        INDGcItemByPlate.DataSource = Nothing
        INDSleSupplier.EditValue = 0

        Code = String.Empty
        ContractTypeId = Nothing
        INDGleContractType.Properties.NullText = String.Empty
        ContractNumber = String.Empty
        DocumentDate = Nothing
        InitialDate = Nothing
        EndDate = Nothing
        Description = String.Empty
        SupplierId = Nothing
        INDSleSupplier.Properties.NullText = String.Empty
        SupplierDistributionLineId = Nothing
        SourceOrder = Nothing
        Exclusivity = Nothing
        OnlyGuarantee = Nothing
        TechnicalSupervicion = String.Empty
        SupervisionExecution = String.Empty
        Clauses = String.Empty
        Attachments = String.Empty

        Status = 0

        _isLoading = False
        OnlyRead = False
        ReadOnlyControls(False)
        ActionsOnControls = False
        nameResourse = String.Empty

        INDBtnCode.Focus()
        INDlyContract.EndUpdate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que carga los controles de la consulta del contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MMaintenanceContract(CStr(Me.Tag))
                    AsyncLoader(True)
                    contract = Await Model.GetMaintenanceContractByCodeAsync(INDBtnCode.Text.Trim)
                    INDlyContract.BeginUpdate()
                    If contract IsNot Nothing AndAlso contract.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(contract.Id))

                            _isLoading = True
                            With contract
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                ContractTypeId = .ContractTypeId
                                DocumentDate = If(.DocumentDate Is Nothing, .InitialDate, .DocumentDate)
                                INDGleContractType.Properties.NullText = .DescriptionContractType
                                ContractNumber = .ContractNumber
                                InitialDate = .InitialDate
                                EndDate = .EndDate
                                Description = .Description
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDSleSupplier.Properties.NullText = .DescriptionSupplier
                                SupplierId = .SupplierId

                                SourceOrder = .SourceOrder
                                Exclusivity = .Exclusivity
                                OnlyGuarantee = .OnlyGuarantee
                                TechnicalSupervicion = .TechnicalSupervicion
                                SupervisionExecution = .SupervisionExecution
                                Clauses = .Clauses
                                Attachments = .Attachments
                                Status = .Status

                                INDGcItemByPlate.DataSource = .MaintenanceContractDetail.ToList()
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.contract.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = contract.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            ActionsOnControls = True
                            If Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)
                                OnlyRead = False
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                                Me.ReadOnlyControls(True)
                                OnlyRead = True

                                If Status = 2 Then
                                    Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Confirmar, "Legalizar")
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                                    INDDteInitialDate.Properties.ReadOnly = False
                                    INDDteEndDate.Properties.ReadOnly = False
                                    INDTxtTechnicalSupervicion.Properties.ReadOnly = False
                                    INDTxtSupervicionExecution.Properties.ReadOnly = False
                                End If
                            End If

                            Me.BarraBotones.PrintReport(PrintReportAction.None, contract.Id, 0, contract.Id)
                            Me.BarraBotones.SetDocuments(contract.Id, Me.Tag.ToString(), Nothing, GetType(MaintenanceContract).Name)

                            _isLoading = False
                            AsyncLoader(False)

                            INDGleContractType.Focus()
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewContract()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDlyContract.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With contract
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = BarraBotones.OperatingUnit.Id
            .ContractTypeId = ContractTypeId
            .DocumentDate = DocumentDate
            .ContractNumber = ContractNumber
            .InitialDate = InitialDate
            .EndDate = EndDate
            .Description = Description
            .SupplierId = SupplierId
            .SupplierDistributionLineId = SupplierDistributionLineId
            .SourceOrder = SourceOrder
            .Exclusivity = Exclusivity
            .OnlyGuarantee = OnlyGuarantee
            .TechnicalSupervicion = TechnicalSupervicion
            .SupervisionExecution = SupervisionExecution
            .Clauses = Clauses
            .Attachments = Attachments
            .Status = Me.Status

        End With
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.contract IsNot Nothing AndAlso Me.contract.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                INDBtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "BarButtons"

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Status = 1
        varImp = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Status = 2
        varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        Status = 2
        varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anular()
    End Sub

    Private Sub BarraBotones_ClickLegalize() Handles BarraBotones.ClickConfirmar
        Status = 4
        varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class
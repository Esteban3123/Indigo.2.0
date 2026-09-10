Imports System.Threading.Tasks
Imports Domain.Security.Entities
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Security.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.Eform
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Domain.Base.Entities.Core.Enums

Public Class FrmFormulario
    Inherits Presentation.Controls.FormBase
    Implements IFormulario

#Region "Propiedades"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PFormulario
    ''' <summary>
    ''' representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private formulario As VieDBForm
    ''' <summary>
    ''' Listado de Secuencia
    ''' </summary>
    Private ListSequence As List(Of Tuple(Of String, String))

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFormulario.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IFormulario.ActionsOnControls
        Set(value As Boolean)
            INDBeCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDTxtEvent.Enabled = value
            INDRgSequence.Enabled = value
            INDRgNative.Enabled = value
            INDRgHasForm.Enabled = value
            INDTxtClass.Enabled = value
            INDTxtAssembly.Enabled = value
            INDRgMConfirm.Enabled = value
            INDGlueSequence.Enabled = value
            If value Then
                INDTxtName.Focus()
            Else
                INDBeCode.Focus()
            End If
        End Set
    End Property

    Public Property IdForm As Integer Implements IFormulario.IdForm
        Get
            Return INDBeCode.Text
        End Get
        Set(value As Integer)
            INDBeCode.Text = value
        End Set
    End Property

    Public Property FormName As String Implements IFormulario.FormName
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    Public Property PrintEvents As String Implements IFormulario.PrintEvents
        Get
            Return INDTxtEvent.Text
        End Get
        Set(value As String)
            INDTxtEvent.Text = value
        End Set
    End Property

    Public Property HasSequence As Boolean Implements IFormulario.HasSequence
        Get
            Return INDRgSequence.EditValue
        End Get
        Set(value As Boolean)
            INDRgSequence.EditValue = value
        End Set
    End Property

    Public Property IsNativeForm As Boolean Implements IFormulario.IsNativeForm
        Get
            Return INDRgNative.EditValue
        End Get
        Set(value As Boolean)
            INDRgNative.EditValue = value
        End Set
    End Property

    Public Property HasForm As Boolean Implements IFormulario.HasForm
        Get
            Return INDRgHasForm.EditValue
        End Get
        Set(value As Boolean)
            INDRgHasForm.EditValue = value
        End Set
    End Property

    Public Property HandlesMassiveConfirm As Boolean Implements IFormulario.HandlesMassiveConfirm
        Get
            Return INDRgMConfirm.EditValue
        End Get
        Set(value As Boolean)
            INDRgMConfirm.EditValue = value
        End Set
    End Property

    Public Property ClassName As String Implements IFormulario.ClassName
        Get
            Return INDTxtClass.Text
        End Get
        Set(value As String)
            INDTxtClass.Text = value
        End Set
    End Property

    Public Property AssemblyName As String Implements IFormulario.AssemblyName
        Get
            Return INDTxtAssembly.Text
        End Get
        Set(value As String)
            INDTxtAssembly.Text = value
        End Set
    End Property

    Public Property SequenceModule As String Implements IFormulario.SequenceModule
        Get
            Return INDGlueSequence.EditValue
        End Get
        Set(value As String)
            INDGlueSequence.EditValue = value
        End Set
    End Property

    Public Property State As Boolean Implements IFormulario.State
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

    ''' <summary>
    ''' Estado crud del registro
    ''' </summary>
    Private _crud As ECrud
    Public Property Crud As ECrud Implements IFormulario.Crud
        Get
            Return _crud
        End Get
        Set(value As ECrud)
            _crud = value
        End Set
    End Property

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor del formulario
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Presenter = New PFormulario(Me)

        ListSequence = New List(Of Tuple(Of String, String)) From {
            New Tuple(Of String, String)("MixingStation", "Central Mezclas"),
            New Tuple(Of String, String)("Contract", "Contratos"),
            New Tuple(Of String, String)("Billing", "Facturación"),
            New Tuple(Of String, String)("Maintenance", "Mantenimiento"),
            New Tuple(Of String, String)("Budget", "Presupuesto"),
            New Tuple(Of String, String)("Inventory", "Inventarios"),
            New Tuple(Of String, String)("MedicalFees", "Causación de Honorarios"),
            New Tuple(Of String, String)("Portfolio", "Cartera"),
            New Tuple(Of String, String)("Treasury", "Tesoreria"),
            New Tuple(Of String, String)("Glosas", "Glosas"),
            New Tuple(Of String, String)("InteropCost", "Interoperabilidad de Costos"),
            New Tuple(Of String, String)("Authorization", "Autorizaciones"),
            New Tuple(Of String, String)("FixedAssets", "Activos Fijos"),
            New Tuple(Of String, String)("Payroll", "Nomina"),
            New Tuple(Of String, String)("Payments", "Pagos"),
            New Tuple(Of String, String)("Cost", "Costos"),
            New Tuple(Of String, String)("Accounting", "Contabilidad")
        }
    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        IdForm = ReturnValue
        If IdForm <> 0 Then
            Await LoadControls()
            INDBeCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Carga el valor de la busqueda y setea en variables
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        Try
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                Exit Function
            End If
            AsyncLoader(True)
            Await Presenter.ConsultarNombreFormulario(IdForm)

            If Presenter.Formulario.IdForm = 0 Then
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                Crud = ECrud.Added
            Else
                LogicaBotonActualizar(True)
                FormName = Presenter.Formulario.FormName.Trim
                PrintEvents = Presenter.Formulario.PrintEvents.Trim
                HasSequence = Presenter.Formulario.HasSequence
                IsNativeForm = Presenter.Formulario.IsNativeForm
                HasForm = Presenter.Formulario.HasForm
                ClassName = Presenter.Formulario.ClassName.Trim
                AssemblyName = Presenter.Formulario.AssemblyName.Trim
                HandlesMassiveConfirm = Presenter.Formulario.HandlesMassiveConfirm
                State = Presenter.Formulario.State
                SequenceModule = Presenter.Formulario.SequenceModule
                Crud = ECrud.Modified
                If Not String.IsNullOrEmpty(SequenceModule) Then
                    Dim val1 = ListSequence.Where(Function(x) x.Item1 = SequenceModule)?.FirstOrDefault()
                    If val1 IsNot Nothing Then
                        INDGlueSequence.Properties.NullText = val1.Item2
                    End If
                End If

                RefreshGridAction()

                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & IdForm)
                Me.BarraBotones.SetDocuments(IdForm)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                Me.BarraBotones.StatusRecordVisible = True
            End If

            If INDBeCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If

            INDTxtName.Focus()
            AsyncLoader(False)
            ActionsOnControls = True
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Refresca la informacion de la rejilla Titulos
    ''' </summary>
    Private Sub RefreshGridAction()
        INDGcAction.Invalidate()
        INDGcAction.DataSource = Presenter.Formulario.ListFormAction.Where(Function(x) x.Crud <> ECrud.Deleted)
        INDGcAction.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        Try
            If IdForm > 0 Then
                AsyncLoader(True)
                Dim resultado = Await Presenter.CambiarEstado(IdForm, State)
                AsyncLoader(False)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Function ValidateControls() As Boolean
        Dim res = Me.LayoutControls.ValidateFields()
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Método que abre el form de importar la información
    ''' </summary>
    Private Sub OpenImportInfo()
        Dim errors = ValidateControls()
        If errors = False Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New PopupImportFormulario()
            'AddHandler Formulario.ImportInfoEvent, AddressOf ImportInfo
            Formulario.ToolBar.Dock = DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 920
            Formulario.Height = 600
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Crud"
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
    ''' Metodo para buscar en barra de acciones
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Metodo para guardar form en barra de acciones
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If Not ValidateControls() Then
                Exit Sub
            End If

            Dim errors As New StringBuilder

            If IdForm = 0 Then
                errors.AppendLine("Debe ingresar Codigo")
            End If
            If String.IsNullOrEmpty(FormName) Then
                errors.AppendLine("Debe ingresar Nombre")
            End If

            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If
            AsyncLoader(True)

            Dim form = New VieDBForm() With {
                .IdForm = IdForm,
                .FormName = FormName,
                .PrintEvents = PrintEvents,
                .HasSequence = HasSequence,
                .IsNativeForm = IsNativeForm,
                .HasForm = HasForm,
                .HandlesMassiveConfirm = HandlesMassiveConfirm,
                .ClassName = ClassName,
                .AssemblyName = AssemblyName,
                .SequenceModule = SequenceModule,
                .Crud = Crud
            }

            form.ListFormAction = Presenter.Formulario.ListFormAction.Where(Function(x) x.Crud <> 0 AndAlso x.Crud <> ECrud.Unchanged).ToList

            Dim resultado = Await Presenter.GuardarFormulario(form)
            If resultado Then
                'Consultar lista de acciones con Id de DB
                If form.ListFormAction.Any Or Crud = ECrud.Added Then
                    Crud = ECrud.Modified
                    Await Presenter.ConsultarFormActionFormulario(IdForm)
                    RefreshGridAction()
                End If
            End If

            AsyncLoader(False)
            If resultado Then
                BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                LogicaBotonActualizar(True)
                INDBeCode.Enabled = False
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' inicializa los btn para crear un nuevo registro modulo
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If IdForm = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe insertar Codigo."
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
            Exit Sub
        End If
        'ActionsOnControls = True
        'BarraBotones.PrepareToolbar(eAction.OnlySave)
        'INDTxtName.Focus()
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        'State = 1
        'Crud = ECrud.Unchanged
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        Await Me.LoadControls()
    End Sub

    ''' <summary>
    ''' Limpiar variables
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        LayoutControl1.BeginUpdate()
        ActionsOnControls = False
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        IdForm = 0
        FormName = String.Empty
        PrintEvents = String.Empty
        HasSequence = 0
        IsNativeForm = 0
        HasForm = 0
        HandlesMassiveConfirm = 0
        ClassName = String.Empty
        AssemblyName = String.Empty
        SequenceModule = String.Empty
        INDGlueSequence.Properties.NullText = Nothing
        INDGlueSequence.EditValue = 0
        INDGcAction.DataSource = Nothing
        Crud = ECrud.Unchanged
        State = 1

        LayoutControl1.EndUpdate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
    End Sub

    ''' <summary>
    ''' Eliminar registro modulo
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If IdForm > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim resultado = Await Presenter.EliminarFormulario(IdForm)
                    AsyncLoader(False)
                    If resultado Then
                        Deshacer()
                        ActionsOnControls = False
                    End If
                Catch ex As Exception
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.MensajeError) = ex.Message
                End Try
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesDigiteDatos)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para buscar registros
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Codigo", .FieldName = "Id"},
                New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"},
                New ColumnInfo With {.Caption = "Estado", .FieldName = "State"}}.ToList()
            .ValorSolicitado = "Id"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListForm
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

#End Region

#Region "handles"

    ''' <summary>
    ''' Carga el control de tipos de Secuencia Modulo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGlueSequence_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGlueSequence.QueryPopUp
        If INDGlueSequence.Properties.DataSource Is Nothing Then
            INDGlueSequence.Properties.DataSource = ListSequence
        End If
    End Sub

    ''' <summary>
    ''' Buscar codigo de modulo en db
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBeCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBeCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then

            If IdForm > 0 Then
                Await Me.LoadControls()
            Else
                Me.Nuevo()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Actualizar permiso de accion al formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRichePermission_CheckedChanged(sender As Object, e As EventArgs) Handles INDRichePermission.CheckedChanged
        Dim control As DevExpress.XtraEditors.CheckEdit = CType(sender, DevExpress.XtraEditors.CheckEdit)
        Dim gridRow = CType(INDGvAction.GetFocusedRow(), FormAction)
        If gridRow IsNot Nothing Then
            Dim list = Presenter.Formulario.ListFormAction.Where(Function(x) x.IdAction = gridRow.IdAction).FirstOrDefault

            If gridRow.Id = 0 Then
                If control.Checked Then
                    list.Action.State = control.Checked
                    list.Crud = ECrud.Added
                Else
                    list.Action.State = control.Checked
                    list.Crud = ECrud.Unchanged
                End If
            End If

            If gridRow.Id > 0 Then
                If control.Checked = False Then
                    list.Action.State = control.Checked
                    list.Crud = ECrud.Deleted
                Else
                    list.Action.State = control.Checked
                    list.Crud = ECrud.Unchanged
                End If
            End If
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
        Me.indigo = SessionValues.Instance
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, True)
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
        Deshacer()
    End Sub

    ''' <summary>
    ''' Este Metodo Ejecuta el metodo Deshacer
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Deshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ eliminar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Eliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBeCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Actualizar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Actualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ guardar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Guardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento estado barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barra Botones: ImportarInformación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        OpenImportInfo()
    End Sub




#End Region

End Class
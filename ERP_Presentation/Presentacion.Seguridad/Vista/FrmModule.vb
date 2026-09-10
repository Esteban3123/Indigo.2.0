'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Jhon Tovar
' Created          : 2022-04-05
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.Text
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Security.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.Threading.Tasks
Imports Domain.Base.Entities
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities.Core.Enums

Public Class FrmModule
    Inherits Presentation.Controls.FormBase
    Implements IModule

#Region "Propiedades"
    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PModule
    ''' <summary>
    ''' representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private modules As Modules

    ''' <summary>
    ''' tarea de consultar lista de productos
    ''' </summary>
    Private taskListarProductos As Task

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IModule.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IModule.ActionsOnControls
        Set(value As Boolean)
            INDBteCodigo.Enabled = Not value
            INDTxtNombre.Enabled = value
            INDTxtDesc.Enabled = value
            INDGleProducto.Enabled = value
            INDSleTitle.Enabled = value
            INDTeOrder.Enabled = value
            INDSbAddTitle.Enabled = value

            If value Then
                INDBteCodigo.Focus()
            Else
                INDBteCodigo.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del IdModulo
    ''' </summary>
    ''' <returns></returns>
    Public Property IdModule As Integer Implements IModule.IdModule
        Get
            Return INDBteCodigo.Text
        End Get
        Set(value As Integer)
            INDBteCodigo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad Nomnre Modulo
    ''' </summary>
    ''' <returns></returns>
    Public Property ModuleName As String Implements IModule.ModuleName
        Get
            Return INDTxtNombre.Text
        End Get
        Set(value As String)
            INDTxtNombre.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de descripcion de modulo
    ''' </summary>
    ''' <returns></returns>
    Public Property ModuleDescription As String Implements IModule.ModuleDescription
        Get
            Return INDTxtDesc.Text
        End Get
        Set(value As String)
            INDTxtDesc.Text = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad estado del modulo
    ''' </summary>
    ''' <returns></returns>
    Public Property State As Boolean Implements IModule.State
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
    ''' Propiedad del Id Producto
    ''' </summary>
    ''' <returns></returns>
    Public Property IdProduct As Integer Implements IModule.IdProduct
        Get
            Return If(Me.INDGleProducto.EditValue Is Nothing, 0, CInt(Me.INDGleProducto.EditValue))
        End Get
        Set(value As Integer)
            Me.INDGleProducto.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Estado crud del registro
    ''' </summary>
    Private _crud As ECrud
    Public Property Crud As ECrud Implements IModule.Crud
        Get
            Return _crud
        End Get
        Set(value As ECrud)
            _crud = value
        End Set
    End Property
    ''' <summary>
    ''' Metodo usado para mostrar notificaciones
    ''' </summary>
    ''' <param name="Icono"></param>
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



#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor del formulario
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Presenter = New PModule(Me)
    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        IdModule = ReturnValue
        If IdModule <> 0 Then
            Await LoadControls()
            INDBteCodigo.Enabled = False
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
            Await Presenter.ConsultarNombreModule(IdModule)

            If Presenter.Modules.IdModule = 0 Then
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                Crud = ECrud.Added
            Else
                LogicaBotonActualizar(True)
                ModuleName = Presenter.Modules.ModuleName.Trim
                ModuleDescription = Presenter.Modules.Description.Trim
                State = Presenter.Modules.State
                IdProduct = Presenter.Modules.ProductCatalog.IdProduct
                INDGleProducto.Properties.NullText = Presenter.Modules.ProductCatalog.ProductName
                Crud = ECrud.Modified
                RefreshGridTitle()

                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & IdModule)
                Me.BarraBotones.SetDocuments(IdModule)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                Me.BarraBotones.StatusRecordVisible = True
            End If

            If INDBteCodigo.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If

            INDTxtNombre.Focus()
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
    Private Sub RefreshGridTitle()
        INDGcTitle.Invalidate()
        INDGcTitle.DataSource = Presenter.Modules.ModuleTitle.Where(Function(x) x.Crud <> ECrud.Deleted)
        INDGcTitle.RefreshDataSource()
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
            If IdModule > 0 Then
                AsyncLoader(True)
                Dim resultado = Await Presenter.CambiarEstado(IdModule, State)
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
#End Region

#Region "Crud"
    ''' <summary>
    ''' Metodo para buscar en barra de acciones
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Metodo para guardar modulo en barra de acciones
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If Not ValidateControls() Then
                Exit Sub
            End If

            Dim errors As New StringBuilder

            If IdModule = 0 Then
                errors.AppendLine("Debe ingresar Modulo")
            End If
            If IdProduct = 0 Then
                errors.AppendLine("Debe ingresar Producto")
            End If
            If ModuleName Is Nothing Then
                errors.AppendLine("Debe ingresar Nombre")
            End If
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If
            AsyncLoader(True)

            Dim modules = New Modules() With {
                .IdModule = IdModule,
                .ModuleName = ModuleName,
                .Description = ModuleDescription,
                .ProductCatalog = New ProductCatalog() With {
                .IdProduct = IdProduct
                },
                .Crud = Crud
            }

            modules.ModuleTitle = Presenter.Modules.ModuleTitle.Where(Function(x) x.Crud <> 0 AndAlso x.Crud <> ECrud.Unchanged).ToList
            modules.ModuleForm = Presenter.Modules.ModuleForm.Where(Function(x) x.Crud <> 0 AndAlso x.Crud <> ECrud.Unchanged).ToList

            Dim resultado = Await Presenter.GuardarModule(modules)
            If resultado Then
                Crud = ECrud.Modified
                'Actualizar modelos con base a ECrud
                Presenter.Modules.ModuleTitle.RemoveAll(Function(x) x.Crud = ECrud.Deleted)
                Presenter.Modules.ModuleForm.RemoveAll(Function(x) x.Crud = ECrud.Deleted)

                For Each item As ModuleTitle In Presenter.Modules.ModuleTitle.Where(Function(x) x.Crud = ECrud.Added Or x.Crud = ECrud.Modified).ToList
                    item.Crud = ECrud.Unchanged
                Next

                For Each item As ModuleForm In Presenter.Modules.ModuleForm.Where(Function(x) x.Crud = ECrud.Added Or x.Crud = ECrud.Modified).ToList
                    item.Crud = ECrud.Unchanged
                Next
            End If

            AsyncLoader(False)
            If resultado Then
                BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                LogicaBotonActualizar(True)
                INDBteCodigo.Enabled = False
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' inicializa los btn para crear un nuevo registro modulo
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        ActionsOnControls = True
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        INDTxtNombre.Focus()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        State = 1
        Crud = ECrud.Unchanged
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
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

        IdProduct = 0
        ModuleName = String.Empty
        ModuleDescription = String.Empty
        IdModule = 0
        State = False
        modules = Nothing
        INDGcTitle.DataSource = Nothing
        INDSleTitle.EditValue = Nothing
        INDTeOrder.Text = Nothing
        Crud = ECrud.Unchanged

        LayoutControl1.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Eliminar registro modulo
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Not String.IsNullOrEmpty(IdModule.ToString()) AndAlso IdModule > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim resultado = Await Presenter.EliminarModule(IdModule)
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
                New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
            .ValorSolicitado = "Id"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListModule 'TODO:
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

#End Region

#Region "Handles"

    ''' <summary>
    ''' Inicializador de formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmModule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, True)
        Me.indigo = SessionValues.Instance

        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
        Deshacer()
        Presenter.ConsultarTodosForm()
        taskListarProductos = Presenter.ListarProductos()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Consulta de todos los productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleProducto_QueryPopUpAsync(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleProducto.QueryPopUp
        taskListarProductos.Wait()
        If Me.INDGleProducto.Properties.DataSource Is Nothing Then
            Me.INDGleProducto.Properties.DataSource = Me.Presenter.listProductCatalog
        End If
    End Sub

    ''' <summary>
    ''' Grid Consulta de titulos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTitle_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTitle.QueryPopUp
        If INDSleTitle.Properties.DataSource Is Nothing OrElse INDSleTitle.EditValue Is Nothing Then
            INDSleTitle.Properties.DataSource = Presenter.ConsultarTodosTitle()
        End If

    End Sub

    ''' <summary>
    ''' abrir frm title para gestionar crud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTitle_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleTitle.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmTitulo
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Agregar titulo a grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddTitle_Click(sender As Object, e As EventArgs) Handles INDSbAddTitle.Click
        Dim TitleSelect = TryCast(INDSleTitle.GetSelectedDataRow(), Infrastructure.Data.Xpo.SecurityRepository.TitleXpo)
        If TitleSelect IsNot Nothing Then
            Dim TxtOrder As Integer
            If String.IsNullOrEmpty(INDTeOrder.Text) Then
                Dim ListOrder = Presenter.Modules.ModuleTitle.ToList
                If ListOrder.Count > 0 Then
                    TxtOrder = ListOrder?.Max(Function(x) x.Order) + 1
                Else
                    TxtOrder = 1
                End If
            Else
                TxtOrder = INDTeOrder.Text
            End If

            Dim _Title = New Title() With {.IdTitle = TitleSelect.Id, .TitleName = TitleSelect.Name, .TitleOrder = TxtOrder}
            Dim _AlterAddmodules = New ModuleTitle() With {
            .IdModule = IdModule,
            .IdTitle = _Title.IdTitle,
            .Order = _Title.TitleOrder,
            .Title = _Title,
            .Crud = ECrud.Added
            }

            Presenter.Modules.ModuleTitle.Add(_AlterAddmodules)
        End If
        RefreshGridTitle()
        INDSleTitle.EditValue = Nothing
        INDTeOrder.Text = Nothing
    End Sub

    ''' <summary>
    ''' Eliminar titulo de grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbDeleteForms_Click(sender As Object, e As EventArgs) Handles INDSbDeleteForms.Click
        Dim _Title = TryCast(INDGvTitle.GetFocusedRow, ModuleTitle)
        If _Title IsNot Nothing Then
            If _Title.IdTitle > 0 Then
                'Eliminar titulos 
                Dim objDelete = Presenter.Modules.ModuleTitle.FirstOrDefault(Function(x) x.IdTitle = _Title.IdTitle AndAlso (x.Crud = 0 Or x.Crud = ECrud.Unchanged))
                If objDelete IsNot Nothing Then
                    objDelete.Crud = ECrud.Deleted
                End If
                Presenter.Modules.ModuleTitle.RemoveAll(Function(x) x.Crud = ECrud.Added)

                'Procesar formularios relacionados a titulo eliminado
                Presenter.Modules.ModuleForm.RemoveAll(Function(x) x.IdTitle = _Title.IdTitle AndAlso x.Crud = ECrud.Added)
                For Each item As ModuleForm In Presenter.Modules.ModuleForm.Where(Function(x) x.IdTitle = _Title.IdTitle AndAlso (x.Crud = 0 Or x.Crud = ECrud.Unchanged Or x.Crud = ECrud.Modified)).ToList
                    item.Crud = ECrud.Deleted
                Next

            End If
            RefreshGridTitle()
        End If
    End Sub


    ''' <summary>
    ''' Actualiza valor de orden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRitxtOrderUpdateTitle_EditValueChanged(sender As Object, e As EventArgs) Handles INDRitxtOrderUpdateTitle.EditValueChanged
        Dim _Title = TryCast(INDGvTitle.GetFocusedRow, ModuleTitle)
        Dim value = DirectCast(sender, TextEdit)
        If _Title IsNot Nothing AndAlso String.IsNullOrEmpty(value.Text) = False Then
            Dim objUpdate = Presenter.Modules.ModuleTitle.FirstOrDefault(Function(x) x.IdTitle = _Title.IdTitle)
            If objUpdate IsNot Nothing Then
                If objUpdate.Crud = ECrud.Added Then
                    objUpdate.Order = value.Text
                End If

                If (objUpdate.Crud = 0 Or objUpdate.Crud = ECrud.Unchanged Or objUpdate.Crud = ECrud.Modified) Then
                    objUpdate.Order = value.Text
                    objUpdate.Crud = ECrud.Modified
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' Abrir popup de formularios en grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddForms_Click(sender As Object, e As EventArgs) Handles INDSbAddForms.Click
        Dim _title = TryCast(INDGvTitle.GetFocusedRow, ModuleTitle)
        If _title IsNot Nothing Then
            Using formulario As New PopupModuleForms()
                formulario.IdModule = IdModule
                formulario.IdTitle = _title.IdTitle
                formulario.ListModuleForm = Presenter.Modules
                formulario.SleFormDataSource = Presenter.FormDataSource
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.Size = New Size(842, 768)
                Dim transparent = New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
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

    '' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCodigo.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
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
    ''' Buscar codigo de modulo en db
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBteCodigo_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBteCodigo.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then

            If IdModule > 0 Then
                Await Me.LoadControls()
            Else
                Me.Nuevo()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

End Class
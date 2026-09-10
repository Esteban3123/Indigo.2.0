'***********************************************************************
' Assembly         : Presentation.Security
' Author           : Jhon Tovar
' Created          : 10-03-2022
'
' Last Modified By :
' Last Modified On :
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Threading.Tasks
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Security.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Base.Entities.Core.Enums

Public Class FrmProductCatalog
    Inherits Presentation.Controls.FormBase
    Implements IProductCatalog

#Region "Propiedades"
    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PProductCatalog
    ''' <summary>
    ''' representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private productCatalog As ProductCatalog

    ''' <summary>
    ''' Listado de tipos S/N
    ''' </summary>
    Private ListVisible As List(Of Tuple(Of Byte, String))

    Private Property IdProduct As Integer Implements IProductCatalog.IdProduct
        Get
            Return INDBteCode.Text
        End Get
        Set(value As Integer)
            INDBteCode.Text = value
        End Set
    End Property

    Private Property IProductCatalog_ProductName As String Implements IProductCatalog.ProductName
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    Private Property State As Boolean Implements IProductCatalog.State
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

    Private Property IProductCatalog_Visible As Byte? Implements IProductCatalog.Visible
        Get
            Return INDGleVisible.EditValue
        End Get
        Set(ByVal value As Byte?)
            INDGleVisible.EditValue = value
        End Set
    End Property

    Public Property PlatformName As String Implements IProductCatalog.PlatformName
        Get
            Return INDTxtPlatform.EditValue
        End Get
        Set(value As String)
            INDTxtPlatform.Text = value
        End Set
    End Property

    Public Property SuiteName As String Implements IProductCatalog.SuiteName
        Get
            Return INDTxtSuite.EditValue
        End Get
        Set(value As String)
            INDTxtSuite.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Estado crud del registro
    ''' </summary>
    Private _crud As ECrud
    Public Property Crud As ECrud Implements IProductCatalog.Crud
        Get
            Return _crud
        End Get
        Set(value As ECrud)
            _crud = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IProductCatalog.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProductCatalog.ActionsOnControls
        Set(value As Boolean)
            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDTxtPlatform.Enabled = value
            INDTxtSuite.Enabled = value
            INDGleVisible.Enabled = value

            If value Then
                INDBteCode.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

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
    ''' Metodo Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        ListVisible = New List(Of Tuple(Of Byte, String))
        ListVisible.Add(New Tuple(Of Byte, String)(0, "No"))
        ListVisible.Add(New Tuple(Of Byte, String)(1, "Si"))
    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        IdProduct = ReturnValue
        If IdProduct <> 0 Then
            Await LoadControls()
            INDBteCode.Enabled = False
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
            Await Presenter.ConsultarNombreProductCatalog(INDBteCode.Text)

            If Presenter.productCatalog.IdProduct = 0 Then
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                Crud = ECrud.Added
            Else
                LogicaBotonActualizar(True)
                IProductCatalog_ProductName = Presenter.productCatalog.ProductName.Trim
                PlatformName = Presenter.productCatalog.PlatformName.Trim
                SuiteName = Presenter.productCatalog.SuiteName.Trim
                State = Presenter.productCatalog.State
                IProductCatalog_Visible = Presenter.productCatalog.Visible
                Crud = ECrud.Modified
                If IProductCatalog_Visible IsNot Nothing Then
                    Dim val1 = ListVisible.Where(Function(x) x.Item1 = IProductCatalog_Visible)?.FirstOrDefault()
                    INDGleVisible.Properties.NullText = val1.Item2
                End If

                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & IdProduct)
                Me.BarraBotones.SetDocuments(IdProduct)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                Me.BarraBotones.StatusRecordVisible = True
            End If

            If INDBteCode.Enabled = False Then
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
            If IdProduct > 0 Then
                AsyncLoader(True)
                Dim resultado = Await Presenter.CambiarEstado(IdProduct, State)
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
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If Not ValidateControls() Then
                Exit Sub
            End If

            Dim errors As New StringBuilder
            If IProductCatalog_ProductName = "" Then
                errors.AppendLine("Debe ingresar Nombre")
            End If
            If IProductCatalog_Visible Is Nothing Then
                errors.AppendLine("Seleccione Visible")
            End If
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If
            AsyncLoader(True)
            Dim resultado = Await Presenter.GuardarProductCatalog()
            AsyncLoader(False)
            If resultado Then
                BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                LogicaBotonActualizar(True)
                INDBteCode.Enabled = False
                Crud = ECrud.Modified
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Deshacer()
        ActionsOnControls = True
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        INDBteCode.Focus()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        State = 1
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        LayoutControl1.BeginUpdate()
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        IdProduct = 0
        IProductCatalog_ProductName = String.Empty
        PlatformName = String.Empty
        SuiteName = String.Empty
        IProductCatalog_Visible = False
        State = False
        productCatalog = Nothing
        Crud = ECrud.Unchanged
        LayoutControl1.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Not String.IsNullOrEmpty(IdProduct.ToString()) AndAlso IdProduct > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim resultado = Await Presenter.EliminarProductCatalog(IdProduct)
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

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Codigo", .FieldName = "Id"},
                New ColumnInfo With {.Caption = "Nombre", .FieldName = "ProductName"}}.ToList()
            .ValorSolicitado = "Id"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListProductCatalog 'TODO:
            .FormParent = Me
            .ShowSearch()
        End With
        'Indigo.UserViewMode = _ViewForm
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
#End Region

#Region "HANDLES"

    Private Sub FrmProductCatalog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, True)
        Me.indigo = SessionValues.Instance
        Presenter = New PProductCatalog(Me)
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Carga el control de tipos de Visible
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleVisible_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleVisible.QueryPopUp
        If INDGleVisible.Properties.DataSource Is Nothing Then
            INDGleVisible.Properties.DataSource = ListVisible
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
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
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
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then

            If IdProduct > 0 Then
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
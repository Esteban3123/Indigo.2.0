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
Imports System.Threading.Tasks
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Security.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Base.Entities.Core.Enums

Public Class FrmTitulo
    Implements ITitulo, ICustomizableForm

#Region "propiedades"
    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PTitulo

    ''' <summary>
    ''' Esta propiedad establece el valor Control Acciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ITitulo.ActionsOnControls
        Set(value As Boolean)
            INDBeCode.Enabled = Not value
            INDTxtName.Enabled = value
            If value Then
                INDTxtName.Focus()
            Else
                INDBeCode.Focus()
            End If
        End Set
    End Property

    Public Property IdTitle As Integer Implements ITitulo.IdTitle
        Get
            Return INDBeCode.Text
        End Get
        Set(value As Integer)
            INDBeCode.Text = value
        End Set
    End Property

    Public Property TitleName As String Implements ITitulo.TitleName
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    Public Property State As Boolean Implements ITitulo.State
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
    Public Property Crud As ECrud Implements ITitulo.Crud
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
    End Sub
#End Region


#Region "Metodos"
    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        IdTitle = ReturnValue
        If IdTitle <> 0 Then
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
            Await Presenter.ConsultarNombreTitulo(IdTitle)

            If Presenter.Titulo.IdTitle = 0 Then
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                Crud = ECrud.Added
            Else
                LogicaBotonActualizar(True)
                TitleName = Presenter.Titulo.TitleName.Trim
                State = Presenter.Titulo.State
                Crud = ECrud.Modified

                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & IdTitle)
                Me.BarraBotones.SetDocuments(IdTitle)
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
            If IdTitle > 0 Then
                AsyncLoader(True)
                Dim resultado = Await Presenter.CambiarEstado(IdTitle, State)
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
    ''' Esta propiedad contiene todos los mensajes.
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

            If IdTitle = 0 Then
                errors.AppendLine("Debe ingresar Codigo")
            End If
            If String.IsNullOrEmpty(TitleName) Then
                errors.AppendLine("Debe ingresar Nombre")
            End If

            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If
            AsyncLoader(True)

            Dim resultado = Await Presenter.GuardarTitulo()
            If resultado Then
                Crud = ECrud.Modified
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
        If IdTitle = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe insertar Codigo."
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
            Exit Sub
        End If
        Await Me.LoadControls()
    End Sub

    ''' <summary>
    ''' Limpiar variables
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        INDLyTitle.BeginUpdate()
        ActionsOnControls = False
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        IdTitle = 0
        TitleName = String.Empty
        State = True
        Crud = ECrud.Unchanged

        INDLyTitle.EndUpdate()
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
        If IdTitle > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim resultado = Await Presenter.EliminarTitulo(IdTitle)
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
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListTitle
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

#End Region

#Region "Bar Button Events"
    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLyTitle, True)
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Presenter = New PTitulo(Me)
        Me.indigo = SessionValues.Instance

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

#End Region

#Region "handles"

    ''' <summary>
    ''' Buscar codigo de titulo en db
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBeCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBeCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then

            If IdTitle > 0 Then
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
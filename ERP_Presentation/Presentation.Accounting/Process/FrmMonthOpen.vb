'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 19-06-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities.ObjectChangeTracker
Imports Domain.Entities
Imports System.Text

#End Region
''' <summary>
''' 
''' </summary>
Public Class FrmMonthOpen
    Implements ICloseMonth

#Region "Builder"

    ''' <summary>
    '''  En este constructor se inicializan los componentes del formulario y se crea un temporizador con un intervalo de 5 segundos
    ''' </summary>
    Sub New()
        InitializeComponent()
        timer = New Timers.Timer(5000)
    End Sub
#End Region

#Region "Fields"

    ''' <summary>
    ''' La variable timer se convertirá en un objeto que puede responder a eventos específicos generados por el objeto Timers.Timer
    ''' </summary>
    Private WithEvents timer As Timers.Timer

    ''' <summary>
    ''' Gets or sets the month.
    ''' </summary>
    ''' <value>
    ''' The month.
    ''' </value>
    Public Property Month As Integer Implements ICloseMonth.Month

    ''' <summary>
    ''' Se declara la propiedad Status que implementa la interfaz ICloseMonth
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Boolean Implements ICloseMonth.Status

    ''' <summary>
    ''' Se declara la propiedad Year que implementa la interfaz ICloseMonth
    ''' </summary>
    ''' <returns></returns>
    Public Property Year As Integer Implements ICloseMonth.Year

    ''' <summary>
    ''' Propiedad que almacenará una instancia de la clase ClosedMonth
    ''' </summary>
    ''' <returns></returns>
    Public Property _CloseMonth As ClosedMonth

    ''' <summary>
    ''' Propiead que contiene  el datasource de los meses
    ''' </summary>
    Private ListMonth As List(Of CloseMonthComplex)

#End Region

#Region "Constant"

    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Accounting"

#End Region

#Region "Metodos"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        BarraBotones.CleanAuditBasic()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Esta propiedad proporciona una forma de mostrar mensajes con diferentes estilos e iconos en función del tipo de mensaje proporcionado a través del parámetro Icono
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' Metodo para validar si el mes anterior al mes que se quiere cerrar esta cerrado
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ValidateMonth() As Task(Of Boolean)
        Using modelo As New MCloseMonth(Me.Tag)

            Dim MonthLastOpen As Integer
            MonthLastOpen = Await modelo.GetLastMonthOpen(True)

            'si el ultimo mes abierto es menor que el mes que se quiere cerrar se realiza la validacion hacia abajo 
            If MonthLastOpen > glMonth.EditValue Then
                'valido que el mes anterior este cerrado
                Dim year As Integer
                year = INDCtrDateNavigator.GetYear ' GetDateServer.Year
                If Await modelo.ValidateOpenMonth(glMonth.EditValue + 1, year, True) = False Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateOpenMonthUp", "Accounting"), glMonth.Text)
                    Return False
                Else
                    Return True
                End If
            End If
            Return True
        End Using
    End Function

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssingValues()
        With _CloseMonth
            .Month = glMonth.EditValue
            .Year = INDCtrDateNavigator.GetYear
            .Status = True
        End With
    End Sub
#End Region

#Region "Crud"

    ''' <summary>
    ''' metodo para cargar el datasource
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadMonths()
        Using mSearch As New MCloseMonth(Me.Tag)
            Dim year As Integer
            year = INDCtrDateNavigator.GetYear
            ListMonth = Await mSearch.GetAllMonth(year, True)
            glMonth.Properties.DataSource = ListMonth
            glMonth.EditValue = Nothing
        End Using
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Using Model As New MCloseMonth(Me.Tag.ToString())
            '_CloseMonth = Await Model.GetMonth(glMonth.EditValue, GetDateServer.Year)
            If Await ValidateMonth() = False Then
                Exit Sub
            End If
            AssingValues()
            AsyncLoader(True)
            Dim Result = Await Model.SaveCloseMonth(_CloseMonth, Nothing, "", Nothing)
            If Result.StateResult = True Then
                If _CloseMonth.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                ElseIf _CloseMonth.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                _CloseMonth = Nothing
                Deshacer()
                AsyncLoader(False)
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
            LoadMonths()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

#End Region

#Region "Bar user"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        BarraBotones.PrepareToolbar(eAction.Save)
    End Sub
    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
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

#End Region

#Region "eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se hace click en un botón dentro del control "Seleccione Mes"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub glMonth_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles glMonth.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            'Llama la función la cual carga el datasource
            LoadMonths()
            'Inicia el temporizador
            timer.Start()
            glMonth.Properties.Buttons(1).Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmMonthlyClose control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmMonthlyClose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.Minimizar(True)
        INDCtrDateNavigator.HowShowControl = CtrDateNavigator.EHowShowControl.OnlyYear
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Copiar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Pegar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cortar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        BarraBotones.RibbonPagEform.Visible = False
        LoadMonths()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the indBtnProcess control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub indBtnProcess_Click(sender As Object, e As EventArgs) Handles indBtnProcess.Click
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el temporizador alcanza el intervalo especificado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub timer_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles timer.Elapsed
        timer.Stop()
        If glMonth.InvokeRequired Then
            glMonth.BeginInvoke(Sub()
                                    glMonth.Properties.Buttons(1).Enabled = True
                                End Sub)
        Else
            glMonth.Properties.Buttons(1).Enabled = True
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor del control "Seleccione Mes"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub glMonth_EditValueChanged(sender As Object, e As EventArgs) Handles glMonth.EditValueChanged
        If glMonth.EditValue IsNot Nothing Then
            Using model As New MCloseMonth(Me.Tag)
                indBtnProcess.Enabled = False

                'Obtiene el mes seleccionado y su información a través del modelo
                _CloseMonth = Await model.GetMonth(glMonth.EditValue, INDCtrDateNavigator.GetYear)
                If _CloseMonth.RecalculatingBalances Then
                    Mensaje(EeventViewerImages.Advertencia) = "El mes seleccionado se encuentra en un proceso de recalculo de balances contables y no se puede abrir"
                    glMonth.EditValue = Nothing
                    indBtnProcess.Enabled = True
                    Exit Sub
                End If

                'Configura la barra de botones
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                BarraBotones.CleanAuditBasic()
                BarraBotones.SetDocuments(_CloseMonth.Id, Me.Tag, Nothing, GetType(ClosedMonth).Name)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), _CloseMonth.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), _CloseMonth.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), _CloseMonth.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), _CloseMonth.ModificationDate)
                indBtnProcess.Enabled = True
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se cambia de fecha; Llama al método LoadMonths
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCtrDateNavigator_OnChangeDate(sender As Object, e As EventArgs) Handles INDCtrDateNavigator.OnChangeDate
        LoadMonths()
    End Sub
End Class
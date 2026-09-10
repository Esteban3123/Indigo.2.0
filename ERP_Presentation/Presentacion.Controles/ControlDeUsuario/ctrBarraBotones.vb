'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 27-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"

Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Threading.Tasks
Imports DevExpress.LookAndFeel
Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraLayout
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports Domain.DocumentalSystem.Entities
Imports Domain.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports Presentation.Resources
Imports DevExpress.XtraBars.Ribbon.ViewInfo
Imports System.Globalization
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Client.MVP
#End Region

'<System.Drawing.ToolboxBitmap("C:\on.png")>

''' <summary>
''' Esta Clase se utiliza para personalizar la vista de el control de usuario 
''' y desacoplarla de los demas frontales.
''' </summary>
Public Class CtrBarraBotones
#Region "Interfaz Implementada"
    Implements IBarraBotones

#End Region

#Region "Eventos Publicos de la Barra de Botones"

    ''' <summary>
    ''' Se Declara el evento [clic restablecer layout].
    ''' </summary>
    Public Event ClicRestablecerLayout()
    ''' <summary>
    ''' Se Declara el evento [click_ nuevo].
    ''' </summary>
    Public Event ClickNuevo()
    ''' <summary>
    ''' Se Declara el evento [click_ guardar].
    ''' </summary>
    Public Event ClickGuardar()
    ''' <summary>
    ''' Se Declara el evento [click_ liquidar].
    ''' </summary>
    Public Event ClickLiquidar()
    ''' <summary>
    ''' Se Declara el evento [click_ consultliquidar].
    ''' </summary>
    Public Event ClickConsultLiquidar()
    ''' <summary>
    ''' Se Declara el evento [click_ ConfirmLiquidation].
    ''' </summary>
    Public Event ClickConfirmLiquidation()
    ''' <summary>
    ''' Se Declara el evento [click_ Actualizar].
    ''' </summary>
    Public Event ClickActualizar()
    ''' <summary>
    ''' Se Declara el evento [click_ eliminar].
    ''' </summary>
    Public Event ClickEliminar()
    ''' <summary>
    ''' Se Declara el evento [click_ adicionar].
    ''' </summary>
    ''' 
    Public Event ClickAdicionarRejilla()
    ''' <summary>
    ''' Variable que almacena si se ha dado clic en el boton actualizar permisos
    ''' </summary>
    Property ClicBotonActualizar As Boolean Implements IBarraBotones.ClicBotonActualizar
    ''' <summary>
    ''' Se Declara el evento [click_ modificar].
    ''' </summary>
    Public Event ClickModificarRejilla()
    ''' <summary>
    ''' Se Declara el evento [click_ eliminar grilla].
    ''' </summary>
    Public Event ClickEliminarRejilla()
    ''' <summary>
    ''' Se Declara el evento [click_ confirmar].
    ''' </summary>
    Public Event ClickConfirmar()
    ''' <summary>
    ''' Se Declara el evento [click_ confirmar].
    ''' </summary>
    Public Event ClickConfirmarTodos()
    ''' <summary>
    ''' Ocurre cuando se presiona el botón procesar
    ''' </summary>
    Public Event ClickProcesar()
    ''' <summary>
    ''' Se Declara el evento [click_ anular].
    ''' </summary>
    Public Event ClickAnular()
    ''' <summary>
    ''' Se Declara el evento [click_ Suspender]
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ClickSuspender()
    ''' <summary>
    ''' Se Declara el evento [click_ Reactivar]
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ClickReactivar()
    ''' <summary>
    ''' Se Declara el evento [click_ imprimir].
    ''' </summary>
    Public Event ClickImprimir()
    ''' <summary>
    ''' Se Declara el evento [click_ buscar].
    ''' </summary>
    Public Event ClickBuscar()
    ''' <summary>
    ''' Se Declara el evento [click_ deshacer].
    ''' </summary>
    Public Event ClickDeshacer()
    ''' <summary>
    ''' Se Declara el evento [click_ favoritos].
    ''' </summary>
    Public Event ClickFavoritos()
    ''' <summary>
    ''' Se Declara el evento [click_ pegar].
    ''' </summary>
    Public Event ClickPegar()
    ''' <summary>
    ''' Se Declara el evento [click_ ClickOcultarBarra].
    ''' </summary>
    Public Event ClickOcultarBarra()
    ''' <summary>
    ''' Se Declara el evento [click_ ClickCustomizar].
    ''' </summary>
    Public Event ClickCustomizar()
    ''' <summary>
    ''' Occurs when [verifica permiso customizar].
    ''' </summary>
    Public Event VerificaPermisoCustomizar()
    ''' <summary>
    ''' Occurs when [edita flujo pacientes].
    ''' </summary>
    Public Event Click_EditarFlujoPacientes()
    ''' <summary>
    ''' Se lanza cuando se ha minimizado la barra
    ''' </summary>
    Public Event ToolBarsMinimized(ByVal sender As Object, ByVal e As EventArgs)
    ''' <summary>
    ''' Se lanza cuando se ha maximizado la barra
    ''' </summary>
    Public Event ToolBarsMaximized(ByVal sender As Object, ByVal e As EventArgs)
    ''' <summary>
    ''' Evento que se dispara al dar clic en los botones activar e inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_ActiveInactive()
    ''' <summary>
    ''' Evento que se dispara al dar clic sobre el boton refrescar rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_RefreshGrid()
    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton generar archivo
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_GenerateFile()

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton generar archivo
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ChangueOperatingUnit(operatingUnit As OperatingUnit)

    ''' <summary>
    ''' se dispara al dar click sobre guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_GuardarConfirmar()

    ''' <summary>
    ''' Se ejecuta cuando se da click sobre desconfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_Desconfirmar()

    ''' <summary>
    ''' Se ejecuta al dar sobre el boton terminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_Terminar()

    ''' <summary>
    ''' Evento que se dispara al dar click sobre jerarquia
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_Jerarquia()

    ''' <summary>
    ''' Evento que se dispara al dar click sobre importar informacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_ImportarInformacion()

    ''' <summary>
    ''' Evento que se dispara cuando dan Click en Actualizar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_ActualizarConfirmar()

    ''' <summary>
    ''' Evento que se dispara cuando dan Click en Entrega Manual
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_EntregaManual()

    ''' <summary>
    ''' Evento que se dispara cuando dan click en Radicacion respuesta
    ''' </summary>
    Public Event Click_RadicateResponse()

    ''' <summary>
    ''' Evento click boton validar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_Validar()
    ''' <summary>
    ''' Evento click del boton custom dropdown
    ''' </summary>
    Public Event Click_CustomDropDown()
    ''' <summary>
    ''' Evento click boton Deshacer Todo
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_DeshacerTodo()
    Public Event Click_DosisPersonalizada()
    ''' <summary>
    ''' evento para realizar las homologaciones del documento contable
    ''' </summary>
    ''' <param name="legalBookId"></param>
    ''' <remarks></remarks>
    Public Event HomologationJournalVoucher(legalBookId As Integer)
    ''' <summary>
    ''' evento para guardar las homologaciones del documento contable
    ''' </summary>
    ''' <param name="journalVoucherHomologation"></param>
    ''' <remarks></remarks>
    Public Event SaveHomologationJournalVoucher(journalVoucherHomologation As JournalVouchers)
    ''' <summary>
    ''' evento click del boton cargar orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_LoadPurcharseOrder()
    Public Event Click_ModoNavegacion()
    ''' <summary>
    ''' Evento click del botón Conciliar de conciliación bancaria automática
    ''' </summary>
    Public Event ClickConciliar()
    ''' <summary>
    ''' Evento click del botón Cerrar Conciliación de conciliación bancaria automática
    ''' </summary>
    Public Event ClickCerrarConciliacion()

    Public Delegate Function PermitirNavegacionDelegate() As Task(Of Boolean)
    Public _funcPermitirNavegacion As PermitirNavegacionDelegate

    Public Sub SetPermitirNavegacion(funcPermitirNavegacion As PermitirNavegacionDelegate)
        _funcPermitirNavegacion = funcPermitirNavegacion
    End Sub

#End Region

#Region "Variables"
    ''' <summary>
    ''' Almacena el estado del panel de controles adicionales
    ''' </summary>
    Private _stateAdditionalControls As Boolean
    ''' <summary>
    ''' Bandera para detectar si el boton guardar esta disponible para el usuario
    ''' </summary>
    Dim BanderaBtnGuardarActivado As Boolean
    ''' <summary>
    ''' Bandera para detectar si el boton Actualizar esta disponible para el usuario
    ''' </summary>
    Dim BanderaBtnActualizarActivado As Boolean
    ''' <summary>
    ''' Bandera para especificar que ya se encuentra editando un registro y debe aplicar logica del boton Actualizar Guardar
    ''' </summary>
    Dim BanderaEditandoRegistro As Boolean
    ''' <summary>
    ''' Bandera para determinar si el texto seleccionado se va a copiar.
    ''' </summary>
    Dim CopiarTextoSeleccionado As Boolean
    ''' <summary>
    ''' Bandera para determinar si el texto seleccionado se va a pegar.
    ''' </summary>
    Dim PegarTextoSeleccionado As Boolean
    ''' <summary>
    ''' Bandera para determinar si el texto seleccionado se va a cortar.
    ''' </summary>
    Dim CortarTextoSeleccionado As Boolean
    ''' <summary>
    ''' Varialbe que contiene el formulario contenedor en el MDI.
    ''' </summary>
    Dim formularioContenedor As Form
    ''' <summary>
    ''' Varialbe que contiene el formulario contenedor.
    ''' </summary>
    Dim formularioContenedor2 As DevExpress.XtraBars.PopupControlContainer
    ''' <summary>
    ''' Variable que se utiliza para conectarce con el presentador de la barra
    ''' </summary>
    Dim presenter As PBarraBotones
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable para almacenar los permisos del formulario
    ''' </summary>
    Private _permissionsForm As Dictionary(Of Integer, String)
    ''' <summary>
    ''' Variable que contiene el item seleccionado en el layoutControl.
    ''' </summary>
    Dim item As BaseLayoutItem
    ''' <summary>
    ''' LayoutSender
    ''' </summary>
    Dim Layoutsender As Object
    ''' <summary>
    ''' LayoutEventos
    ''' </summary>
    Dim LayoutEventos As EventArgs
    ''' <summary>
    ''' Variable Privada que me permite utilizar las propiedades del layout control seleccionado.
    ''' </summary>
    Dim LayoutControl As LayoutControl
    ''' <summary>
    ''' Variable bandera para permitir consultar
    ''' </summary>
    Private _PermiteConsultar As Boolean
    ''' <summary>
    ''' Variable bandera para permitir guardar responsable de pago.
    ''' </summary>
    Private _PermiteGuardarResponsablePagoTercero As Boolean
    ''' <summary>
    ''' Variable privada para la propiedad PermitirCustomizarFuncional.
    ''' </summary>
    Private _PermitirCustomizarFuncional As Boolean
    ''' <summary>
    ''' LayoutControlItem
    ''' </summary>
    Private ArrastrarItem_Renombrado As LayoutControlItem
    ''' <summary>
    ''' ListViewItem
    ''' </summary>
    Private NuevoItem As ListViewItem
    ''' <summary>
    ''' ListView
    ''' </summary>
    Dim listViewAgregado As ListView
    ''' <summary>
    ''' LayoutControl
    ''' </summary>
    Dim LayoutControlAgregado As LayoutControl
    ''' <summary>
    ''' Variable para establer el modo minimizado
    ''' </summary>
    Dim Minimized As Boolean
    ''' <summary>
    ''' Variable Resource para aplicar los estilos
    ''' </summary>
    ''' <remarks></remarks>
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrBarraBotones))
    ''' <summary>
    ''' Variable para el manejo de la barra de progreso
    ''' </summary>
    Dim _ProgressBar As Boolean
    ''' <summary>
    ''' Variable para el manejo del texto en la barra de progreso
    ''' </summary>
    Dim _ChangeMessageProgressBar As String

    ''' <summary>
    ''' Diccionario que almacena la consulta de Usuarios
    ''' </summary>
    Dim _UsersDictionary As New Dictionary(Of String, String)()
#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Obtiene el primer layout encontrado
    ''' </summary>
    ''' <returns>LayoutControl principal</returns>
    Public Property ProgressBar As Boolean
        Get
            Return _ProgressBar
        End Get
        Set(value As Boolean)
            _ProgressBar = value
            INDMarqueeProgressBarControl.Visible = value
            If Me.ParentForm IsNot Nothing Then
                INDMarqueeProgressBarControl.Width = Me.ParentForm.Width - 210
            End If

            INDMarqueeProgressBarControl.Properties.MarqueeAnimationSpeed = 20
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que permite cambiar el mensaje 'Cargando' de la barra de progreso
    ''' </summary>
    ''' <returns>LayoutControl principal</returns>
    Public Property ChangeMessageProgressBar As String
        Get
            Return _ChangeMessageProgressBar
        End Get
        Set(value As String)
            DirectCast(INDMarqueeProgressBarControl, DevExpress.XtraEditors.BaseEdit).[Text] = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene el primer layout encontrado
    ''' </summary>
    ''' <returns>LayoutControl principal</returns>
    Public ReadOnly Property FirstLayoutControl As LayoutControl
        Get
            Return Me.LayoutControl
        End Get
    End Property

    Public ReadOnly Property BtnValidar As BarButtonItem
        Get
            Return BarBtnValidar
        End Get
    End Property


    Public ReadOnly Property BtnCustomDose As BarButtonItem
        Get
            Return INDBbiCustomDose
        End Get
    End Property

    Private Property ProcessName As String

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se muestra el label de la cabecera
    ''' </summary>
    ''' <value>Valor que indica si se muestra el label de la cabecera</value>
    ''' <returns>El valor que indica si se muestra el label de la cabecera</returns>
    <Browsable(False)>
    Public Property XtraLabelVisibility As Boolean
        Get
            Return (BarLblXtraLabel.Visibility = BarItemVisibility.Always)
        End Get
        Set(value As Boolean)
            If Not Object.Equals(value, Nothing) Then
                Dim vis As BarItemVisibility = BarItemVisibility.Never
                If value Then
                    vis = BarItemVisibility.Always
                Else
                    vis = BarItemVisibility.Never
                End If
                BarLblXtraLabel.Visibility = vis
            End If
        End Set
    End Property

    ''' <summary>
    ''' Boton de auditopria basica
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="value"></param>
    Public Sub AddAuditBasic(name As String, value As Object)
        'Creo el textbox
        If value IsNot Nothing AndAlso value.ToString().Length > 0 Then
            Dim INDtxtTmp As New DevExpress.XtraEditors.TextEdit
            INDtxtTmp.EditValue = value
            INDtxtTmp.Properties.ReadOnly = True
            INDtxtTmp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9)
            INDtxtTmp.Properties.Appearance.Options.UseFont = True

            Dim typeValue = value.GetType()
            'analisa si el tipo de dato es tipo fecha
            If typeValue.Name = GetType(Date).Name Then
                INDtxtTmp.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
            Else
                If Not Me._UsersDictionary.ContainsKey(value) Then
                    Using Model As New MBarraBotones
                        Dim Result = Model.GetUserbyCode(value)
                        Me._UsersDictionary.Add(value, $"{value} - {Result?.IdPerson?.Fullname}")
                    End Using
                End If

                INDtxtTmp.EditValue = Me._UsersDictionary(value)
            End If

            Me.INDlyControlAudit.Controls.Add(INDtxtTmp)
            'Creo el layout control item
            Dim INDlyItemTmp As DevExpress.XtraLayout.LayoutControlItem = New DevExpress.XtraLayout.LayoutControlItem()
            INDlyItemTmp.AppearanceItemCaption.Font = New Font("Segoe UI Light", 9)
            INDlyItemTmp.AppearanceItemCaption.Options.UseFont = True
            INDlyItemTmp.MaxSize = New System.Drawing.Size(360, 30)
            INDlyItemTmp.MinSize = New System.Drawing.Size(360, 30)
            INDlyItemTmp.Size = New System.Drawing.Size(385, 30)
            INDlyItemTmp.Control = INDtxtTmp
            INDlyItemTmp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            INDlyItemTmp.Text = name
            INDlyItemTmp.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
            INDlyItemTmp.TextSize = New System.Drawing.Size(165, 21)
            INDlyItemTmp.TextToControlDistance = 12
            INDlyControlBasicAudit.AddItem(INDlyItemTmp, INDSpaceAudit, DevExpress.XtraLayout.Utils.InsertType.Top)
            INDAuditPcc.Size = New System.Drawing.Size(INDAuditPcc.Width, INDAuditPcc.Height + 30)
        End If
    End Sub

    ''' <summary>
    ''' Limpiar los controles de auditoria basica
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanAuditBasic()
        Dim index = 0
        While index <= INDlyControlBasicAudit.Items.Count - 1
            If Not INDlyControlBasicAudit.Items(index).Name = "INDlyItemBtnAuditAdvance" And Not INDlyControlBasicAudit.Items(index).Name = "INDSpaceAudit" Then
                INDlyControlBasicAudit.RemoveAt(index)
                index -= 1
            End If
            index += 1
        End While
        INDAuditPcc.Size = New System.Drawing.Size(INDAuditPcc.Width, 121)
    End Sub

    ''' <summary>
    ''' Obtiene o asigna la imagen usada para el label de la cabecera
    ''' </summary>
    ''' <value>Nueva imagen</value>
    ''' <returns>La imagen en el label</returns>
    <Browsable(False)>
    Public Property XtraLabelImage As Image
        Get
            Return BarLblXtraLabel.Glyph
        End Get
        Set(value As Image)
            If value IsNot Nothing Then
                BarLblXtraLabel.Glyph = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto en el label de la cabecera
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>El texto</returns>
    <Browsable(False)>
    Public Property XtraLabelText As String
        Get
            Return BarLblXtraLabel.Caption.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                BarLblXtraLabel.Caption = value.Trim()
                'XtraLabelVisibility = True
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad con lista de permisos
    ''' </summary>
    ''' <value>Lista de permisos por formulario</value>
    ''' <returns>Lista de permisos</returns>
    Public Property PermissionsForm As Dictionary(Of Integer, String) Implements IBarraBotones.PermissionsForm
        Get
            Return Me._permissionsForm
        End Get
        Set(value As Dictionary(Of Integer, String))
            Me._permissionsForm = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo para reutilizar funcion en las propiedades LogicaBotonActualizar y Boton
    ''' </summary>
    Private Sub LogicaBotonActualizarFuncion()
        'valida si se muestra guardar o actualizar dependiendo de los permisos
        'y si los tiene ambos que muestre solo el boton guardar.
        'Si el Value = True Indica que existe datos y debe aplicar logica
        'para Actualizar

        If BanderaEditandoRegistro = True Then
            If BanderaBtnGuardarActivado = True And BanderaBtnActualizarActivado = True Then
                BarBtnGuardar.Visibility = BarItemVisibility.Never
                BarBtnActualizar.Visibility = BarItemVisibility.Always
            ElseIf BanderaBtnGuardarActivado = True And BanderaBtnActualizarActivado = False Then
                BarBtnGuardar.Visibility = BarItemVisibility.Never
                BarBtnActualizar.Visibility = BarItemVisibility.Never
            ElseIf BanderaBtnGuardarActivado = False And BanderaBtnActualizarActivado = True Then
                BarBtnGuardar.Visibility = BarItemVisibility.Never
                BarBtnActualizar.Visibility = BarItemVisibility.Always
            End If
            'Si el Value = False Inidca que no existe datos y debe aplicar logica
            'para Guardar unicamente.
        Else
            If BanderaBtnGuardarActivado = True Then
                BarBtnGuardar.Visibility = BarItemVisibility.Always
                BarBtnActualizar.Visibility = BarItemVisibility.Never
            Else
                BarBtnGuardar.Visibility = BarItemVisibility.Never
                BarBtnActualizar.Visibility = BarItemVisibility.Never
            End If
        End If
    End Sub

    ''' <summary>
    ''' Establecer mensaje en barra superior
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="type"></param>
    Sub ShowXtraMessage(ByVal message As String, ByVal type As ImagesXtraLabel, ByVal user As String)


        If user Is Nothing OrElse user.Trim().ToLower().Equals(Indigo.UserIndigo.ToLower()) Then
            Me.INDLbMessage.Visible = False
            Exit Sub
        End If

        Me.INDLbMessage.Text = message
        Me.INDLbMessage.Visible = True
        Me.INDLbMessage.Width = 300

        For Each item As DevExpress.XtraBars.BarItem In RibbonControl.Items.ToList

            If (item.Name <> "BargleStatus" AndAlso item.Name <> "BarBtnCerrar" AndAlso item.Name <> "BarLblXtraLabel" AndAlso item.Name <> "BarBtnDeshacer" AndAlso item.Name <> "BarBtnDeshacerTodo" AndAlso item.Name <> "BarBtnMinimizar") Then

                item.Enabled = False

            End If

        Next

    End Sub

    ''' <summary>
    ''' Habilitar los botones de acciones correspondientes al formulario
    ''' </summary>
    Sub EnableBarItems()

        Me.XtraLabelVisibility = False
        Me.INDLbMessage.Visible = False
        Me.INDLbMessage.Text = String.Empty

        For Each item As DevExpress.XtraBars.BarItem In RibbonControl.Items.ToList

            If (item.Name <> "BarBtnImprimir" AndAlso item.Name <> "BargleStatus" AndAlso item.Name <> "BarBtnCerrar" AndAlso item.Name <> "BarLblXtraLabel" AndAlso item.Name <> "BarBtnDeshacer" AndAlso item.Name <> "BarBtnDeshacerTodo" AndAlso item.Name <> "BarBtnMinimizar") Then

                item.Enabled = True

            End If

        Next

    End Sub

    ''' <summary>
    ''' Propiedad que permite establecer los permisos del usuario.
    ''' </summary>
    ''' <value></value>
    <Category("INDIGO"), Description("Define si se pone o no visible el boton de la barra Modificar")>
    <Obsolete>
    Public WriteOnly Property LogicaBotonActualizar() As Boolean
        Set(ByVal value As Boolean)
            'valida si se muestra guardar o actualizar dependiendo de los permisos
            'y si los tiene ambos que muestre solo el boton guardar.
            'Si el Value = True Indica que existe datos y debe aplicar logica
            'para Actualizar
            If value = True Then
                'Esta bandera me permite identificar que ya me encuentro editando un registro
                'y el usuario da click en el Boton Permisos
                BanderaEditandoRegistro = True
                If BanderaBtnGuardarActivado = True And BanderaBtnActualizarActivado = True Then
                    BarBtnGuardar.Visibility = BarItemVisibility.Never
                    BarBtnActualizar.Visibility = BarItemVisibility.Always
                ElseIf BanderaBtnGuardarActivado = True And BanderaBtnActualizarActivado = False Then
                    BarBtnGuardar.Visibility = BarItemVisibility.Never
                    BarBtnActualizar.Visibility = BarItemVisibility.Never
                ElseIf BanderaBtnGuardarActivado = False And BanderaBtnActualizarActivado = True Then
                    BarBtnGuardar.Visibility = BarItemVisibility.Never
                    BarBtnActualizar.Visibility = BarItemVisibility.Always
                End If
                'Si el Value = False Indica que no existe datos y debe aplicar logica
                'para Guardar unicamente.
            Else
                BanderaEditandoRegistro = False
                If BanderaBtnGuardarActivado = True Then
                    BarBtnGuardar.Visibility = BarItemVisibility.Always
                    BarBtnActualizar.Visibility = BarItemVisibility.Never
                Else
                    BarBtnGuardar.Visibility = BarItemVisibility.Never
                    BarBtnActualizar.Visibility = BarItemVisibility.Never
                End If
            End If
            VisibleAndCollapseRibbonPage()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para controlar si el usuario puede consultar
    ''' </summary>
    Property PermiteConsultar As Boolean
        Get
            Return _PermiteConsultar
        End Get
        Set(ByVal value As Boolean)
            _PermiteConsultar = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para controlar si el usuario puede vincular responsable de pago.
    ''' </summary>
    ''' <returns></returns>
    Property PermiteGuardarResponsablePagoTercero As Boolean
        Get
            Return _PermiteGuardarResponsablePagoTercero
        End Get
        Set(ByVal value As Boolean)
            _PermiteGuardarResponsablePagoTercero = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece un valor que me especifica si el frontal permite o no customizar
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [permitir customizar funcional]; otherwise, <c>false</c>.
    ''' </value>
    Private Property PermitirCustomizarFuncional As Boolean Implements IBarraBotones.PermitirCustomizarFuncional
        Get
            Return _PermitirCustomizarFuncional
        End Get
        Set(ByVal value As Boolean)
            _PermitirCustomizarFuncional = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que permite establecer los permisos del usuario.
    ''' </summary>
    ''' <value></value>
    <Category("INDIGO"), Description("Define si se pone o no visible el boton de la barra Modificar")>
    Private WriteOnly Property Boton(ByVal BotonTag As Integer) As Boolean Implements IBarraBotones.Boton
        Set(ByVal value As Boolean)

            Dim valorVisible As DevExpress.XtraBars.BarItemVisibility
            If value = True Then
                valorVisible = BarItemVisibility.Always
            Else
                valorVisible = BarItemVisibility.Never
            End If
            'TODO: Hacer Case de Botones
            Select Case BotonTag

                Case Is = 1
                    BarBtnEliminar.Visibility = valorVisible
                Case Is = 2
                    BanderaBtnGuardarActivado = True

                Case Is = 3
                    BanderaBtnActualizarActivado = True

                Case Is = 4
                    BarBtnRejillaAdicionar.Visibility = valorVisible
                Case Is = 6
                    BarBtnRejillaEliminar.Visibility = valorVisible
                Case Is = 5
                    BarBtnRejillaModificar.Visibility = valorVisible
                Case Is = 35
                    BarbtnRefreshGrid.Visibility = valorVisible


                Case Is = 7
                    BarBtnConfirmar.Visibility = valorVisible
                    'muestra la pagina procesos si se activa cualquier boton del grupo
                Case Is = 8
                    BarBtnAnular.Visibility = valorVisible
                    'muestra la pagina procesos si se activa cualquier boton del grupo
                Case Is = 9
                    BarBtnImprimir.Visibility = valorVisible
                    'muestra la pagina procesos si se activa cualquier boton del grupo
                    'RibbonPagEform.Visible = True
                Case Is = 10
                    BarBtnFavoritos.Visibility = valorVisible
                Case Is = 11
                    BarBtnCustomizar.Visibility = valorVisible
                Case Is = 12
                    BarSubRedes.Visibility = valorVisible
                Case Is = 13
                    BarSubDocumentos.Visibility = valorVisible
                Case Is = 14
                    BarSubComunicacion.Visibility = valorVisible
                Case Is = 15
                    BarSubOtros.Visibility = valorVisible
                Case Is = 20
                    BarBtnSuspend.Visibility = valorVisible
                    'muestra la pagina procesos si se activa cualquier boton del grupo
                Case Is = 21
                    BarBtnReactivate.Visibility = valorVisible
                    'muestra la pagina procesos si se activa cualquier boton del grupo
                Case Is = 46
                    BarBtnConfirmar.Visibility = valorVisible
                    BarBtnConfirmAll.Visibility = valorVisible
                    'muestra la pagina procesos si se activa cualquier boton del grupo
                Case Is = 40
                    PermiteConsultar = True
                Case Is = 75
                    BarBtnEntregaManual.Visibility = valorVisible
                Case Is = 81
                    BarBtnProcess.Visibility = valorVisible
                Case Is = 85
                    BarBtnReconciled.Visibility = valorVisible
                Case Is = 93
                    _PermiteGuardarResponsablePagoTercero = True
            End Select


            'valida si se muestra guardar o actualizar dependiendo de los permisos
            'y si los tiene ambos que muestre solo el boton guardar.
            If BanderaBtnGuardarActivado = True And BanderaBtnActualizarActivado = True Then
                BarBtnGuardar.Visibility = valorVisible
            ElseIf BanderaBtnGuardarActivado = True And BanderaBtnActualizarActivado = False Then
                BarBtnGuardar.Visibility = valorVisible
            ElseIf BanderaBtnGuardarActivado = False And BanderaBtnActualizarActivado = True Then
                BarBtnActualizar.Visibility = valorVisible
            End If

            'Diparo propiedad de Logica Boton Acutalizar para los casos en donde ya se esta editando un registro
            'Y el usuario da click en el Boton Permisos
            LogicaBotonActualizarFuncion()
            VisibleAndCollapseRibbonPage()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o estable el valor de la huella del lector biometrico
    ''' </summary>
    ''' <value>
    ''' The huella.
    ''' </value>
    Public Property Huella As Byte()
        Get
            If Object.Equals(CtrBiometrico1.ValorTemplate, Nothing) = False Then
                Return (DirectCast(CtrBiometrico1.ValorTemplate.tpt, Byte()))
            Else
                Return Nothing
            End If
        End Get
        Set(value As Byte())
            If value IsNot Nothing Then
                CtrBiometrico1.ValorGauget(1)
                Dim plantilla As New TTemplate
                plantilla.tpt = value
                plantilla.Size = plantilla.tpt.Length
                CtrBiometrico1.ValorTemplate = plantilla
                CtrBiometrico1.BloquerDispositivo()
            Else
                CtrBiometrico1.ValorGauget(0)
                CtrBiometrico1.DesBloquerDispositivo()
            End If
        End Set
    End Property


    Dim _weightPatient As Integer
    Dim _weightPatientShow As Integer
    Dim _CodePatient As String
    Dim _DatePatient As Date
    Dim _weightMeasure As String = "Kg"
    ''' <summary>
    ''' Tamaño del popup de peso paciente
    ''' </summary>
    ''' <param name="weight"></param>
    ''' <param name="CodPatient"></param>
    ''' <param name="DatePatient"></param>
    Public Sub WeightPatient(weight As Integer, CodPatient As String, DatePatient As Date)
        If CodPatient IsNot Nothing Then
            _weightPatient = weight
            _CodePatient = CodPatient
            _DatePatient = DatePatient
            OcultarBotonesSinPermisos(EbuttonsWithoutPermission.PesoPaciente) = False
            If (Date.Now - DatePatient).TotalDays < 90 Then
                _weightMeasure = "Gr"
                _weightPatientShow = _weightPatient
            Else
                _weightMeasure = "Kg"
                _weightPatientShow = _weightPatient / 1000
            End If
            BarBtnPesoPaciente.Caption = "Peso " + _weightPatientShow.ToString() + _weightMeasure
            INDtxtPeso.EditValue = _weightPatientShow
            INDlbMedidaPeso.Text = _weightMeasure
        Else
            OcultarBotonesSinPermisos(EbuttonsWithoutPermission.PesoPaciente) = True
            INDtxtPeso.Text = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Propiedad que me permite habilitar los botones sin permisos que quiero ocultar.
    ''' </summary>
    Public WriteOnly Property OcultarBotonesSinPermisos(ByVal Boton As EbuttonsWithoutPermission) As Boolean
        Set(ByVal value As Boolean)
            Dim visibilidad As BarItemVisibility
            If value = True Then
                visibilidad = BarItemVisibility.Never
            Else
                visibilidad = BarItemVisibility.Always
            End If
            Select Case Boton
                Case EbuttonsWithoutPermission.Nuevo
                    BarBtnNuevo.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Guardar
                    BarBtnGuardar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.GuardarConfirmar
                    BarBtnGuardarConfirmar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Actualizar
                    BarBtnActualizar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.ActualizarConfirmar
                    BarBtnActualizarConfirmar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Eliminar
                    BarBtnEliminar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Customizar
                    BarBtnCustomizar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Confirmar
                    BarBtnConfirmar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.ConfirmarTodos
                    BarBtnConfirmAll.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Desconfirmar
                    BarBtnDesconfirmar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Anular
                    BarBtnAnular.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Terminar
                    BarBtnTerminar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Reactivar
                    BarBtnReactivate.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Suspender
                    BarBtnSuspend.Visibility = visibilidad
                Case EbuttonsWithoutPermission.DosisPersonalizada
                    INDBbiCustomDose.Visibility = visibilidad
                Case EbuttonsWithoutPermission.ActiveInactive
                    If StatusRecord Is Nothing Then
                        BarBtnActive.Visibility = BarItemVisibility.Never
                        BarBtnInactive.Visibility = BarItemVisibility.Never
                    Else
                        Dim state As eActionsStatusRecords = If(StatusRecord.GetType() = GetType(eActionsStatusRecords), StatusRecord, If(StatusRecord = "1", eActionsStatusRecords.Active, eActionsStatusRecords.Inactive))
                        If state = eActionsStatusRecords.Active Then
                            BarBtnActive.Visibility = BarItemVisibility.Never
                            BarBtnInactive.Visibility = visibilidad
                        ElseIf state = eActionsStatusRecords.Inactive Then
                            BarBtnActive.Visibility = visibilidad
                            BarBtnInactive.Visibility = BarItemVisibility.Never
                        Else
                            BarBtnActive.Visibility = BarItemVisibility.Never
                            BarBtnInactive.Visibility = BarItemVisibility.Never
                        End If
                    End If

                Case EbuttonsWithoutPermission.JournalVourcherHomologation
                    BarBtnHomologation.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Procesar
                    BarBtnProcess.Visibility = visibilidad
                Case EbuttonsWithoutPermission.EntregaManual
                    BarBtnEntregaManual.Visibility = visibilidad
                Case EbuttonsWithoutPermission.ConsultarLiquidacion
                    BarBtnConsultLiquidation.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Liquidar
                    BarBtnLiquidar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Jerarquia
                    BarBtnJerarquia.Visibility = visibilidad
                Case EbuttonsWithoutPermission.GenerateFile
                    BarBtnGenerateFile.Visibility = visibilidad
                Case EbuttonsWithoutPermission.ImportarInformacion
                    BarBtnImportar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Validar
                    BarBtnValidar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.ModoNavegacion
                    BarBtnModoNavegacion.Visibility = visibilidad
                Case EbuttonsWithoutPermission.PesoPaciente
                    BarBtnPesoPaciente.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Favoritos
                    BarBtnFavoritos.Visibility = visibilidad
                    'PERSONALIZAR
                Case EbuttonsWithoutPermission.LoadPurcharseOrder
                    BarBtnLoadPurchaseOrder.Visibility = visibilidad

                Case EbuttonsWithoutPermission.Buscar
                    BarBtnBuscar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Deshacer
                    BarBtnDeshacer.Visibility = visibilidad
                Case EbuttonsWithoutPermission.DeshacerTodo
                    BarBtnDeshacerTodo.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Copiar
                    BarBtnCopiar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Cortar
                    BarBtnCortar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Pegar
                    BarBtnPegar.Visibility = visibilidad

                Case EbuttonsWithoutPermission.Imprimir
                    BarBtnImprimir.Visibility = visibilidad

                Case EbuttonsWithoutPermission.GestionDocumental
                    BarBtnDocumentos.Visibility = visibilidad
                Case EbuttonsWithoutPermission.Auditoria
                    BarBtnAudit.Visibility = visibilidad
                Case EbuttonsWithoutPermission.ControlBiometrico
                    BarBtnBiometrico.Visibility = visibilidad

                'INICIO
                'IZQ
                'FILTRO
                'DERECHA
                'FIN

                Case EbuttonsWithoutPermission.RefreshGrid
                    BarbtnRefreshGrid.Visibility = visibilidad
                Case EbuttonsWithoutPermission.AddGrid
                    BarBtnRejillaAdicionar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.DeleteGrid
                    BarBtnRejillaEliminar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.EditGrid
                    BarBtnRejillaModificar.Visibility = visibilidad

                'estado, unidad operativa
                'Control
                'Minimizar
                Case EbuttonsWithoutPermission.Permisos
                    BarBtnRefrescar.Visibility = visibilidad
                Case EbuttonsWithoutPermission.RedesSociales
                    BarSubRedes.Visibility = visibilidad
                Case EbuttonsWithoutPermission.ComunicacionesUnificadas
                    BarSubComunicacion.Visibility = visibilidad
                Case EbuttonsWithoutPermission.OtrosPlugins
                    BarSubOtros.Visibility = visibilidad
                Case EbuttonsWithoutPermission.RadicateResponse
                    BarBtnRadicateResponse.Visibility = visibilidad

                Case EbuttonsWithoutPermission.Conciliar
                    BarBtnReconciled.Visibility = visibilidad

            End Select

            '' Visibles de los RibbonPage

            If _banderacargando Then
                'Task.Run(AddressOf VisibleAndCollapseRibbonPage)
            Else
                VisibleAndCollapseRibbonPage()
            End If

        End Set
    End Property

    ''' <summary>
    ''' Metodo que se encarga de la logica para ocultar o visualizar los ribbon page dependiendo de los botones que esten
    ''' visibles en cada uno
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VisibleAndCollapseRibbonPage()
        If BarBtnNuevo.Visibility = BarItemVisibility.Never And BarBtnGuardar.Visibility = BarItemVisibility.Never And BarBtnActualizar.Visibility = BarItemVisibility.Never And
                        BarBtnEliminar.Visibility = BarItemVisibility.Never And BarBtnGuardarConfirmar.Visibility = BarItemVisibility.Never And
                        BarBtnActualizarConfirmar.Visibility = BarItemVisibility.Never Then
            RibbonPagEform.Visible = False
        Else
            RibbonPagEform.Visible = True
        End If

        If BarBtnConfirmar.Visibility = BarItemVisibility.Never And BarBtnConfirmAll.Visibility = BarItemVisibility.Never And
            BarBtnDesconfirmar.Visibility = BarItemVisibility.Never And BarBtnAnular.Visibility = BarItemVisibility.Never And
            BarBtnTerminar.Visibility = BarItemVisibility.Never And BarBtnReactivate.Visibility = BarItemVisibility.Never And
            BarBtnSuspend.Visibility = BarItemVisibility.Never And BarBtnActive.Visibility = BarItemVisibility.Never And
            BarBtnInactive.Visibility = BarItemVisibility.Never Then
            RibbonPageProcesos.Visible = False
        Else
            RibbonPageProcesos.Visible = True
        End If

        If BarBtnHomologation.Visibility = BarItemVisibility.Never And BarBtnProcess.Visibility = BarItemVisibility.Never And
                BarBtnEntregaManual.Visibility = BarItemVisibility.Never And BarBtnConsultLiquidation.Visibility = BarItemVisibility.Never And
                BarBtnLiquidar.Visibility = BarItemVisibility.Never And BarBtnJerarquia.Visibility = BarItemVisibility.Never And
                BarBtnGenerateFile.Visibility = BarItemVisibility.Never And BarBtnImportar.Visibility = BarItemVisibility.Never And
                BarBtnValidar.Visibility = BarItemVisibility.Never And BarBtnModoNavegacion.Visibility = BarItemVisibility.Never And
                BarBtnPesoPaciente.Visibility = BarItemVisibility.Never And BarBtnFavoritos.Visibility = BarItemVisibility.Never And
                BarBtnCustomizar.Visibility = BarItemVisibility.Never And BarBtnLoadPurchaseOrder.Visibility = BarItemVisibility.Never Then
            RibbonPageActions.Visible = False
        Else
            RibbonPageActions.Visible = True
        End If

        If BarBtnBuscar.Visibility = BarItemVisibility.Never And BarBtnDeshacer.Visibility = BarItemVisibility.Never And
            BarBtnCopiar.Visibility = BarItemVisibility.Never And BarBtnCortar.Visibility = BarItemVisibility.Never And
            BarBtnPegar.Visibility = BarItemVisibility.Never And BarBtnDeshacerTodo.Visibility = BarItemVisibility.Never Then
            RibbonPageEdicion.Visible = False
        Else
            RibbonPageEdicion.Visible = True
        End If

        If BarBtnImprimir.Visibility = BarItemVisibility.Never Then
            RibbonPagePrint.Visible = False
        Else
            RibbonPagePrint.Visible = True
        End If

        If BarBtnDocumentos.Visibility = BarItemVisibility.Never And BarBtnAudit.Visibility = BarItemVisibility.Never And BarBtnBiometrico.Visibility = BarItemVisibility.Never Then
            RibbonPageComponents.Visible = False
        Else
            RibbonPageComponents.Visible = True
        End If

        If BarBtnRejillaAdicionar.Visibility = BarItemVisibility.Never And BarBtnRejillaEliminar.Visibility = BarItemVisibility.Never And
            BarBtnRejillaModificar.Visibility = BarItemVisibility.Never And BarbtnRefreshGrid.Visibility = BarItemVisibility.Never Then
            RibbonPageRejillas.Visible = False
        Else
            RibbonPageRejillas.Visible = True
        End If

        'estado, unidad operativa
        If INDBarEditItemStatus.Visibility = BarItemVisibility.Never And INDBeiOperatingUnits.Visibility = BarItemVisibility.Never Then
            INDRibbonPageData.Visible = False
        Else
            INDRibbonPageData.Visible = True
        End If

        'Control

        'Minimizar

    End Sub


    ''' <summary>
    ''' Valida la visibilidad del boton en la barra de botones
    ''' </summary>
    ''' <param name="Boton"></param>
    ''' <returns></returns>
    Public Function ValidateVisibilityButton(ByVal Boton As EbuttonsWithoutPermission) As Boolean
        Select Case Boton
            Case EbuttonsWithoutPermission.ActualizarConfirmar
                If BarBtnActualizarConfirmar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                    Return True
                End If
            Case EbuttonsWithoutPermission.Desconfirmar
                If BarBtnDesconfirmar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                    Return True
                End If
        End Select
        Return False
    End Function
#End Region

#Region "Eventos de Controles Privados"

    ''' <summary>
    ''' Metodo para limiar el control biometrico
    ''' </summary>
    Public Sub LimpiarBiometrico()
        CtrBiometrico1.LimpiarControles()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Nuevo
    ''' </summary>
    Private Sub BarBtnNuevoN_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnNuevo.ItemClick
        ProcessName = BarBtnNuevo.Caption
        RaiseEvent ClickNuevo()
    End Sub

    Private Sub BarBtnModoNavegacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnModoNavegacion.ItemClick
        ProcessName = BarBtnModoNavegacion.Caption
        RaiseEvent Click_ModoNavegacion()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Guardar
    ''' </summary>
    Private Sub BarBtnGuardarN_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnGuardar.ItemClick
        ProcessName = BarBtnGuardar.Caption
        RaiseEvent ClickGuardar()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Guardar
    ''' </summary>
    Private Sub INDBbiCustomDose_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiCustomDose.ItemClick
        ProcessName = INDBbiCustomDose.Caption
        RaiseEvent Click_DosisPersonalizada()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Actualizar
    ''' </summary>
    Private Sub BarBtnActualizar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnActualizar.ItemClick
        ProcessName = BarBtnActualizar.Caption
        RaiseEvent ClickActualizar()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Eliminar
    ''' </summary>
    Private Sub BarBtnEliminar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnEliminar.ItemClick
        ProcessName = BarBtnEliminar.Caption
        RaiseEvent ClickEliminar()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Adicionar
    ''' </summary>
    Private Sub BarBtnGriCrear_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnRejillaAdicionar.ItemClick
        ProcessName = BarBtnRejillaAdicionar.Caption
        RaiseEvent ClickAdicionarRejilla()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento
    ''' </summary>
    Private Sub BarBtnGriModificar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnRejillaModificar.ItemClick
        ProcessName = BarBtnRejillaModificar.Caption
        RaiseEvent ClickModificarRejilla()
    End Sub

    ''' <summary> 
    ''' Metodo que maneja el evento click y expone el evento Click_EliminarGrilla
    ''' </summary>
    Private Sub BarBtnGriEliminar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnRejillaEliminar.ItemClick
        ProcessName = BarBtnRejillaEliminar.Caption
        RaiseEvent ClickEliminarRejilla()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Confirmar
    ''' </summary>
    Private Sub BarBtnConfirmar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnConfirmar.ItemClick
        ProcessName = BarBtnConfirmar.Caption
        RaiseEvent ClickConfirmar()
        RaiseEvent ClickConfirmLiquidation()
    End Sub

    Private Sub BarBtnConfirmAll_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnConfirmAll.ItemClick
        ProcessName = BarBtnConfirmAll.Caption
        RaiseEvent ClickConfirmarTodos()
    End Sub

    Private Sub BarBtnProcess_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnProcess.ItemClick
        ProcessName = BarBtnProcess.Caption
        RaiseEvent ClickProcesar()
    End Sub

    Private Sub INDReconciled_Click(sender As Object, e As EventArgs) Handles INDReconciled.Click
        ProcessName = INDReconciled.Text
        RaiseEvent ClickConciliar()
    End Sub

    Private Sub INDCloseReconciliation_Click(sender As Object, e As EventArgs) Handles INDCloseReconciation.Click
        ProcessName = INDCloseReconciation.Text
        RaiseEvent ClickCerrarConciliacion()
    End Sub

    ''' <summary>
    ''' Actualiza el texto del botón de Cerrar Conciliación
    ''' </summary>
    ''' <param name="text">El nuevo texto del botón</param>
    Public Sub SetCloseReconciliationButtonText(text As String)
        If INDCloseReconciation IsNot Nothing Then
            INDCloseReconciation.Text = text
        End If
    End Sub

    Private Sub BarbtnRefreshGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarbtnRefreshGrid.ItemClick
        ProcessName = BarbtnRefreshGrid.Caption
        RaiseEvent Click_RefreshGrid()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Liquidar
    ''' </summary>
    Private Sub BarBtnLiquidar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnLiquidar.ItemClick
        ProcessName = BarBtnLiquidar.Caption
        RaiseEvent ClickLiquidar()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Liquidar
    ''' </summary>
    Private Sub BarBtnConsultLiquidation_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnConsultLiquidation.ItemClick
        ProcessName = BarBtnConsultLiquidation.Caption
        RaiseEvent ClickConsultLiquidar()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Anular
    ''' </summary>
    Private Sub BarBtnAnular_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnAnular.ItemClick
        ProcessName = BarBtnAnular.Caption
        RaiseEvent ClickAnular()
    End Sub
    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Suspender
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarBtnSuspend_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnSuspend.ItemClick
        ProcessName = BarBtnSuspend.Caption
        RaiseEvent ClickSuspender()
    End Sub
    ''' <summary>
    '''  Metodo que maneja el evento click y expone el evento Click_Reactivar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarBtnReactivate_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnReactivate.ItemClick
        ProcessName = BarBtnReactivate.Caption
        RaiseEvent ClickReactivar()
    End Sub

    ''' <summary> 
    ''' Metodo que maneja el evento click y expone el evento Click_Imprimir
    ''' </summary>
    Private Sub BarBtnImprimir_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnImprimir.ItemClick
        ProcessName = BarBtnImprimir.Caption
        RaiseEvent ClickImprimir()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Buscar
    ''' </summary>
    Private Sub BarBtnBuscar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnBuscar.ItemClick
        ProcessName = BarBtnBuscar.Caption
        RaiseEvent ClickBuscar()
    End Sub

    ''' <summary> 
    ''' Metodo que maneja el evento click y expone el evento Click_Deshacer
    ''' </summary>
    Private Sub BarBtnDeshacer_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnDeshacer.ItemClick
        Me.CleanAuditBasic()
        ProcessName = BarBtnDeshacer.Caption
        Me.INDLbMessage.Visible = False
        RaiseEvent ClickDeshacer()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento ClickOcultarBarra
    ''' </summary>
    Private Sub INDBtnOcultarBarra_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnOcultarBarra.ItemClick
        RaiseEvent ClickOcultarBarra()
    End Sub

    ''' <summary>
    ''' Metodo clic en el boton activar que dispara el evento activeinactive
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarBtnActive_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnActive.ItemClick
        StatusRecord = eActionsStatusRecords.Active
        RaiseEvent Click_ActiveInactive()
    End Sub

    ''' <summary>
    ''' Metodo clic en el boton inactivar que dispara el evento activeinactive
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarBtnInactive_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnInactive.ItemClick
        StatusRecord = eActionsStatusRecords.Inactive
        RaiseEvent Click_ActiveInactive()
    End Sub

    ''' <summary>
    ''' Metodo clic en el boton generar archivo que dispara el evento generar archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarBtnGenerateFile_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnGenerateFile.ItemClick
        RaiseEvent Click_GenerateFile()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Cerrar
    ''' </summary>
    Private Sub BarBtnCerrar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        MyBase.FindForm.Close()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Copiar
    ''' </summary>
    Private Sub BarBtnCopiar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnCopiar.ItemClick
        CopiarTextoSeleccionado = True
        CopiarPegarCortarTextoSeleccionado()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Cortar
    ''' </summary>
    Private Sub BarBtnCortar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnCortar.ItemClick
        CortarTextoSeleccionado = True
        CopiarPegarCortarTextoSeleccionado()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento Click_Pegar
    ''' </summary>
    Private Sub BarBtnPegar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnPegar.ItemClick
        PegarTextoSeleccionado = True
        CopiarPegarCortarTextoSeleccionado()
    End Sub

    ''' <summary>
    ''' Metodo que maneja el evento click y expone el evento  Click_Favoritos
    ''' </summary>
    Private Sub BarBtnFavoritos_ItemPress(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnFavoritos.ItemClick
        RaiseEvent ClickFavoritos()
    End Sub

    ''' <summary>
    ''' Metodo que refresca la barra de controles para permitir nuevos cambios.
    ''' </summary>
    Private Sub BarBtnRefrescar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnRefrescar.ItemClick
        OcultarBotonesBarra()
        ClicBotonActualizar = True
        ActualizarPermisosBarra(CStr(MyBase.FindForm.Tag))  'actualiza los permisos de la barra dependiendo del formulario hijo activo.
        RaiseEvent VerificaPermisoCustomizar()
    End Sub

    ''' <summary>
    ''' Metodo que se ejecuta al cargar el load de la barra de controles
    ''' </summary>
    Private Sub ctrBarraBotones_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Size = New Size(Me.Size.Width, 130)
        If DesignMode = False Then
            OcultarBotonesBarra()
            BuscarPrimerLayoutControl()
        End If
        Me.StatusRecordVisible = False

        CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ribbonControl1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles RibbonControl.Paint
        LocateControlPanel()
        Dim ribbon As RibbonControl = TryCast(sender, RibbonControl)
        Dim viewInfo As RibbonPanelViewInfo = ribbon.ViewInfo.Panel
        Dim groupWidth As Integer = 0
        Dim imageName As String = String.Empty
        If viewInfo.Groups.Count > 0 Then
            groupWidth = viewInfo.Groups(viewInfo.Groups.Count - 1).Bounds.Right
        End If
        If (viewInfo.Bounds.Width - groupWidth) >= 557 Then
            imageName = "Banner"
            Dim theme As String = DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName
            If theme.Contains("High Contrast") Then
                imageName = "Banner"
            End If
        ElseIf (viewInfo.Bounds.Width - groupWidth) >= 93 Then
            imageName = "Banner_angulo"
        End If
        If (Not String.IsNullOrEmpty(imageName)) AndAlso imageName = "Banner" Then
            Dim theme As String = DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName
            If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (Not theme.Equals("Office 2019 Dark Gray")) AndAlso (Not theme.Equals("Office 2016 Dark")) AndAlso (Not theme.Equals("Office 2010 Black")) AndAlso
                (Not theme.Equals("Black")) AndAlso (Not theme.Equals("Dark Side")) AndAlso (Not theme.Equals("Office 2007 Black")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse
            theme.Contains("High Contrast") OrElse theme.Equals("Blueprint")) Then
                If Indigo.ArchitectureType.GetValueOrDefault = 2 Then 'pass
                    imageName = "BannerERPblanco"
                Else
                    imageName = "BannerERPblanco_OnPremise"
                End If
            Else
                If Indigo.ArchitectureType.GetValueOrDefault = 2 Then 'pass
                    imageName = "BannerERPnegro"
                Else
                    imageName = "BannerERPnegro_OnPremise"
                End If
            End If
        End If

        If Not String.IsNullOrEmpty(imageName) Then
            Dim image1 As Image
            image1 = My.Resources.ResourceManager.GetObject(imageName)

            Dim right As Integer = image1.Size.Width
            e.Graphics.DrawImage(image1, viewInfo.Bounds.Width - right, viewInfo.Bounds.Y, right, image1.Size.Height - 10)
        End If

    End Sub

    ''' <summary>
    ''' Busca el primer layout control que se encuentre en el formulario hijo 
    ''' y lo establece como el layoutControl Principal.
    ''' </summary>
    Private Sub BuscarPrimerLayoutControl()
        If Me.Parent.GetType.ToString = "DevExpress.XtraBars.PopupControlContainer" Then
            formularioContenedor2 = Me.Parent
            If formularioContenedor2 IsNot Nothing AndAlso formularioContenedor2.Controls IsNot Nothing Then
                LayoutControl = FindLayoutControl(formularioContenedor2.Controls)
                If LayoutControl IsNot Nothing Then
                    Me.LayoutControl.SetDefaultLayout()
                End If
            End If
        Else
            formularioContenedor = MyBase.FindForm()
            If formularioContenedor IsNot Nothing AndAlso formularioContenedor.Controls IsNot Nothing Then
                LayoutControl = FindLayoutControl(formularioContenedor.Controls)
                If LayoutControl IsNot Nothing Then
                    Me.LayoutControl.SetDefaultLayout()
                End If
            End If
        End If
    End Sub

    Private Function FindLayoutControl(controls As ControlCollection) As LayoutControl
        For i As Integer = 0 To controls.Count - 1
            If TypeOf controls.Item(i) Is PanelControl Then
                LayoutControl = FindLayoutControl(controls.Item(i).Controls)
                If LayoutControl IsNot Nothing Then
                    Return LayoutControl
                End If
            ElseIf TypeOf controls.Item(i) Is LayoutControl Then
                Return DirectCast(controls.Item(i), LayoutControl)
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Manejador del evento MinimizedChanged del control RibbonControl, que me permite minimizar el tamaño
    ''' del control de usuario para crear el efecto de minimizacion.
    ''' </summary>
    Private Sub RibbonControl_MinimizedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RibbonControl.MinimizedChanged
        If Minimized <> CType(sender, RibbonControl).Minimized Then
            Minimizar(CType(sender, RibbonControl).Minimized)
        End If
    End Sub

    ''' <summary>
    ''' Metodo clic en el boton minimizar barra
    ''' </summary>
    Private Sub BarBtnMinimizar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnMinimizar.ItemClick
        'RibbonControl.Minimized = True
        Minimizar(True)
        'RibbonPageMinimizar.Visible = False
        'BarBtnMinimizar.Visibility = BarItemVisibility.Never
    End Sub

    ''' <summary>
    ''' Manejador de evento ItemClick del control BarBtnCustomizar.
    ''' </summary>
    Private Sub BarBtnCustomizar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnCustomizar.ItemClick
        If Me.LayoutControl IsNot Nothing AndAlso TypeOf LayoutControl Is LayoutControl Then
            Me.LayoutControl.ShowCustomizationForm()
        End If
        RaiseEvent ClickCustomizar()
    End Sub

    ''' <summary>
    ''' Metodo al cambiar pagina seleccionada
    ''' </summary>
    Private Sub RibbonControl_SelectedPageChanging(ByVal sender As Object, ByVal e As DevExpress.XtraBars.Ribbon.RibbonPageChangingEventArgs) Handles RibbonControl.SelectedPageChanging
        If LayoutControl Is Nothing Then
            Exit Sub
        End If
        If Not TypeOf LayoutControl Is LayoutControl Then
            Exit Sub
        End If
        If e.Page IsNot Nothing Then
            If e.Page.Name = "PanelCustomizar" Then
                CrearLayoutControl()
                'LayoutControl.SetDefaultLayout()
                HabilitarControlesCustomizacion()
                LayoutControl.ShowCustomizationForm()
                LayoutControl.CustomizationForm.Hide()   ' feo el salto, corregir
            Else
                LayoutControl.HideCustomizationForm()
                LayoutControl.Location = New Point(LayoutControl.Location.X, LayoutControl.Location.Y - 70)
                LayoutControlAgregado.Visible = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me guarda el nuevo diseño del frontal en las rutas definidas por los estandares.
    ''' </summary>
    Private Sub INDBtnGuardarXml_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnGuardarXml.ItemClick
        formularioContenedor = MyBase.FindForm()
        ' Captura el lenguaje y la cultura establecido xml de la configuracion de idioma.
        Dim configuracionXml As New DataTable("ConfigIdioma")
        configuracionXml.Columns.Add("Idioma")
        'configuracionXml.ReadXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", Indigo.IndigoCompanyName, "\", Indigo.IndigoCompanyName, "\XML\ConfigIdioma.xml"))
        configuracionXml.ReadXml(String.Concat(Window.Utils.LocalFolder(), "\", Indigo.IndigoCompanyName, "\", Indigo.IndigoCompanyName, "\XML\ConfigIdioma.xml"))
        Dim culturaInstalada As String = configuracionXml.Rows(0).Item("Idioma").ToString

        'LayoutControl.SaveLayoutToXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", Indigo.IndigoCompanyName, "\", Indigo.IndigoCompanyName, "\XML\", formularioContenedor.Name, "_", culturaInstalada, ".xml"))
        LayoutControl.SaveLayoutToXml(String.Concat(Window.Utils.LocalFolder(), "\", Indigo.IndigoCompanyName, "\", Indigo.IndigoCompanyName, "\XML\", formularioContenedor.Name, "_", culturaInstalada, ".xml"))
        INDBtnGuardarXml.Enabled = False
    End Sub

    ''' <summary>
    ''' Metodo que Deshace la ultima accion del usuario en el frontal.
    ''' </summary>
    Private Sub INDBtnDeshacerAccion_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnDeshacerAccion.ItemClick
        LayoutControl.UndoManager.Undo()
        If LayoutControl.UndoManager.IsUndoAllowed = False Then
            INDBtnDeshacerAccion.Enabled = False
            INDBtnGuardarXml.Enabled = False
            INDBtnRestaurarDiseno.Enabled = False
        Else
            INDBtnRestaurarDiseno.Enabled = True
            INDBtnRehacerAccion.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Metodo que Rehace la ultima accion del usuario en el frontal.
    ''' </summary>
    Private Sub INDBtnRehacerAccion_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnRehacerAccion.ItemClick
        LayoutControl.UndoManager.Redo()
        If LayoutControl.UndoManager.IsRedoAllowed = False Then
            INDBtnRehacerAccion.Enabled = False
            INDBtnRestaurarDiseno.Enabled = True
        Else
            LayoutControlCambio()
            INDBtnRestaurarDiseno.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Metodo al dar clic en posición izquierda
    ''' </summary>
    Private Sub INDBtnPosicionIzquierda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If item Is Nothing Then
            Exit Sub
        End If
        item.TextLocation = Global.DevExpress.Utils.Locations.Left
        LayoutControlCambio()
    End Sub

    ''' <summary>
    ''' Metodo al dar clic en posición derecha
    ''' </summary>
    Private Sub INDBtnPosicionDerecha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If item Is Nothing Then
            Exit Sub
        End If
        item.TextLocation = Global.DevExpress.Utils.Locations.Right
        LayoutControlCambio()
    End Sub

    ''' <summary>
    ''' Metodo al dar clic en posición arriba
    ''' </summary>
    Private Sub INDBtnPosicionArriba_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If item Is Nothing Then
            Exit Sub
        End If
        item.TextLocation = Global.DevExpress.Utils.Locations.Top
        LayoutControlCambio()
    End Sub

    ''' <summary>
    ''' Metodo al dar clic en posición abajo
    ''' </summary>
    Private Sub INDBtnPosicionAbajo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If item Is Nothing Then
            Exit Sub
        End If
        item.TextLocation = Global.DevExpress.Utils.Locations.Bottom
        LayoutControlCambio()
    End Sub

    ''' <summary>
    ''' Metodo al dar clic en restaurar limite
    ''' </summary>
    Private Sub INDBtnLimiteRestaurar_DownChanged(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnLimiteRestaurar.DownChanged
        If e.Link Is Nothing Then
            Exit Sub
        End If
        If DirectCast(e.Link, BarButtonItemLink).Item.Down = True Then

            ' DirectCast(sender, Global.DevExpress.XtraBars.BarButtonItem).Reset()

            'quita los estados check de los demas controles.
            INDBtnLimiteBloquearAncho.Down = False
            INDBtnLimiteBloquearAlto.Down = False
            INDBtnLimiteBloquearTamano.Down = False
            INDBtnLimiteRedimensionar.Down = False

            'propiedad para poder modificar los tamaños de los layout
            LayoutControl.OptionsItemText.TextAlignMode = TextAlignMode.CustomSize

            'Dim tamano As Size= New Size(item.
            item.MaxSize = New Size(item.Size.Width, item.Size.Height)
            item.MinSize = New Size(item.Size.Width, item.Size.Height)



        Else
            item.MaxSize = New Size(item.Size.Width, item.Size.Height)
            item.MinSize = New Size(item.Size.Width, item.Size.Height)
            ' LayoutControl1.HideItem(item.Item)
        End If
    End Sub

    ''' <summary>
    ''' Metodo al dar clic en bloquear tamaño
    ''' </summary>
    Private Sub INDBtnLimiteBloquearTamano_DownChanged(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnLimiteBloquearTamano.DownChanged
        If e.Link Is Nothing Then
            Exit Sub
        End If
        If DirectCast(e.Link, BarButtonItemLink).Item.Down = True Then
            INDBtnLimiteBloquearAncho.Down = False
            INDBtnLimiteBloquearAlto.Down = False
            INDBtnLimiteRestaurar.Down = False
            INDBtnLimiteRedimensionar.Down = False

            'propiedad para poder modificar los tamaños de los layout
            LayoutControl.OptionsItemText.TextAlignMode = TextAlignMode.CustomSize

            Dim tamano As Size = New Size(item.Size.Width, item.Size.Height)
            item.MinSize = tamano
            item.MaxSize = tamano

        End If
    End Sub

#End Region

#Region "Metodos de la Barra de Botones"

    Public Sub PrepareCustomDropDown(ByRef popupContainerControl As PopupControlContainer, Optional caption As String = "Custom")
        INDBbiCustomDropDown.BeginUpdate()
        INDBbiCustomDropDown.Caption = caption
        INDBbiCustomDropDown.DropDownControl = popupContainerControl
        INDBbiCustomDropDown.EndUpdate()
        INDBbiCustomDropDown.Refresh()
    End Sub

    ''' <summary>
    ''' Actualiza los permisos de la barra.
    ''' </summary>
    Public Sub ActualizarPermisosBarra(ByVal codigoFormulario As String)
        If codigoFormulario IsNot Nothing AndAlso Not codigoFormulario.Equals(String.Empty) Then
            Me.BarBtnRefrescar.Enabled = False
            Me.BarBtnRefrescar.Enabled = True
            presenter.ConsultarPermisos(codigoFormulario)
        End If
    End Sub

    ''' <summary>
    ''' Oculta los botones y grupos de la barra e inicializa las banderas en false 
    ''' </summary>
    Private Sub OcultarBotonesBarra()
        RibbonPageRejillas.Visible = False
        RibbonPageProcesos.Visible = False

        BarBtnEliminar.Visibility = BarItemVisibility.Never
        BarBtnGuardar.Visibility = BarItemVisibility.Never
        BarBtnActualizar.Visibility = BarItemVisibility.Never
        BarBtnCustomizar.Visibility = BarItemVisibility.Never
        BarBtnLiquidar.Visibility = BarItemVisibility.Never
        BarBtnConsultLiquidation.Visibility = BarItemVisibility.Never
        BarBtnRejillaAdicionar.Visibility = BarItemVisibility.Never
        BarBtnRejillaModificar.Visibility = BarItemVisibility.Never
        BarBtnRejillaEliminar.Visibility = BarItemVisibility.Never
        BarbtnRefreshGrid.Visibility = BarItemVisibility.Never
        BarBtnConfirmar.Visibility = BarItemVisibility.Never
        BarBtnConfirmAll.Visibility = BarItemVisibility.Never
        BarBtnProcess.Visibility = BarItemVisibility.Never
        BarBtnSuspend.Visibility = BarItemVisibility.Never
        BarBtnReactivate.Visibility = BarItemVisibility.Never
        BarBtnAnular.Visibility = BarItemVisibility.Never
        BarBtnImprimir.Visibility = BarItemVisibility.Never
        BarBtnFavoritos.Visibility = BarItemVisibility.Never
        RibbonPageRejillas.Visible = False
        RibbonPageProcesos.Visible = False
        BarBtnFavoritos.Visibility = BarItemVisibility.Never
        BarSubRedes.Visibility = BarItemVisibility.Never
        BarSubDocumentos.Visibility = BarItemVisibility.Never
        BarSubComunicacion.Visibility = BarItemVisibility.Never
        BarSubOtros.Visibility = BarItemVisibility.Never
        BarBtnModoNavegacion.Visibility = BarItemVisibility.Never
        BarBtnRadicateResponse.Visibility = BarItemVisibility.Never
        BarBtnReconciled.Visibility = BarItemVisibility.Never
    End Sub

    ''' <summary>
    ''' Metodo privado que Copia, pega o corta el texto seleccionado.
    ''' </summary>
    Private Sub CopiarPegarCortarTextoSeleccionado()
        'encuentro el frontal que esta abierto por el MDIPrincipal
        formularioContenedor = MyBase.FindForm

        'itero sobre los controles que tenga este formulario
        For Each controles In formularioContenedor.Controls.Item(0).Controls
            'filtro sobre los controles que esten dentro de los groupcontrol
            '  If TypeOf controles Is GroupControl Then   'linea comentada porque son de tipo layoutcontrol
            'Editores que heredan de TextEdit
            For i As Integer = 0 To controles.Controls.Count - 1
                'solamente tomo los layout control

                'solamente tomo los controles de tipo textedit
                If TypeOf controles.Controls.Item(i) Is TextEdit Then
                    'filtra por el control que tenga el focus
                    If controles.Controls.Item(i).ContainsFocus = True Then
                        Dim controlEdit As TextEdit = CType(controles.Controls.Item(i), TextEdit)
                        controlEdit.Select()
                        'copia el texto seleccionado al clipboard

                        If CopiarTextoSeleccionado = True Then
                            controlEdit.Copy()
                            controlEdit.DeselectAll()
                            CopiarTextoSeleccionado = False
                            Exit Sub
                        End If
                        If CortarTextoSeleccionado = True Then
                            controlEdit.Cut()
                            controlEdit.DeselectAll()
                            CortarTextoSeleccionado = False
                            Exit Sub
                        End If
                        If PegarTextoSeleccionado = True Then
                            controlEdit.Paste()
                            controlEdit.DeselectAll()
                            PegarTextoSeleccionado = False
                            Exit Sub
                        End If
                    End If
                End If

            Next
        Next
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para ejecutar el plugins desde el cliente.
    ''' </summary>
    Public Event ClickPluginsCliente(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)

#End Region

#Region "Barra de estado"

    ''' <summary>
    ''' Obtiene la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Unidad operativa</returns>
    Private Function GetOperatingUnitSelected(_value As Object) As OperatingUnit

        If _value IsNot Nothing AndAlso Me.ListOperatingUnit IsNot Nothing Then
            Dim queryOperatingUnit = (From i In ListOperatingUnit
                                      Where i IsNot Nothing AndAlso i.Id = CType(_value, Integer)
                                      Select i).ToList
            If queryOperatingUnit IsNot Nothing AndAlso queryOperatingUnit.Count > 0 Then
                Dim operating = CType(queryOperatingUnit(0), OperatingUnit)
                Return operating
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Private _listOperatingUnit As List(Of OperatingUnit)

    ''' <summary>
    ''' Propiedad para asignar las unidades operativas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListOperatingUnit As IEnumerable(Of Object)
        Get
            Return _listOperatingUnit
        End Get
        Set(value As IEnumerable(Of Object))
            If value IsNot Nothing Then
                _listOperatingUnit = value
                Dim listFilter = (From e In value Where e IsNot Nothing Select e).ToList()
                INDRigleOperatingUnits.DataSource = listFilter
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para cambiar el valor de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingUnitValue As Integer
        Get
            If INDBeiOperatingUnits.EditValue IsNot Nothing Then
                Return INDBeiOperatingUnits.EditValue
            Else
                Return 0
            End If

        End Get
        Set(value As Integer)
            If value = 0 Then
                INDBeiOperatingUnits.EditValue = Nothing
            Else
                INDBeiOperatingUnits.EditValue = value
                presenter.UpdateReportPath(Indigo.UserIndigoId, Indigo.IndigoContainerId, value)
            End If

        End Set
    End Property

    ''' <summary>
    ''' Obtiene la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Unidad operativa</returns>
    Public ReadOnly Property OperatingUnit As OperatingUnit
        Get
            If INDBeiOperatingUnits.EditValue IsNot Nothing Then
                Return GetOperatingUnitSelected(INDBeiOperatingUnits.EditValue)
            Else
                Return Nothing
            End If
        End Get

    End Property

    Public Property OperatingUnitVisible As Boolean
        Get
            If INDBeiOperatingUnits.Visibility = BarItemVisibility.Always Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value = True Then
                INDBeiOperatingUnits.Visibility = BarItemVisibility.Always
            Else
                INDBeiOperatingUnits.Visibility = BarItemVisibility.Never
            End If
            VisibleAndCollapseRibbonPage()
        End Set
    End Property

    Dim ListStates As List(Of StatusRecord)

    ''' <summary>
    ''' Propiedad que obtiene o establece los estados que puede tener el registro.
    ''' </summary>
    ''' <value>
    ''' El listado de objetos.
    ''' </value>
    Public Property States As List(Of StatusRecord)
        Get
            Return ListStates
        End Get
        Set(value As List(Of StatusRecord))

            ListStates = value
        End Set
    End Property


    Private _StatesWhitActions As eActionsStatusRecords()
    ''' <summary>
    ''' Propiedad que obtiene y establece un array de acciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatesWhitActions As eActionsStatusRecords()
        Get
            Return _StatesWhitActions
        End Get
        Set(value As eActionsStatusRecords())
            ListStates = New List(Of StatusRecord)
            If value IsNot Nothing Then
                For Each State As eActionsStatusRecords In value
                    Dim NewState As StatusRecord
                    Dim Color As Color
                    Select Case State
                        Case eActionsStatusRecords.Active
                            Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))
                        Case eActionsStatusRecords.Confirmed
                            Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                        Case eActionsStatusRecords.Finalized
                            Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                        Case eActionsStatusRecords.Inactive
                            Color = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))
                        Case eActionsStatusRecords.Invalidate
                            Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                        Case eActionsStatusRecords.Liquidated
                            Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                        Case eActionsStatusRecords.Replaced
                            Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                        Case eActionsStatusRecords.Suspended
                            Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                    End Select
                    NewState = New StatusRecord With {.StatusColor = Color, .StatusName = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(State.ToString, Me.GetType()), .StatusValue = State}
                    ListStates.Add(NewState)
                Next

            End If
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que obtiene o establece si el control esta habilitado.
    ''' </summary>
    Public Property StatusRecordEnabled As Boolean
        Get
            Return INDBarEditItemStatus.Enabled
        End Get
        Set(value As Boolean)
            INDBarEditItemStatus.Enabled = value
            INDBarEditItemStatus.Refresh()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece si el control es visible.
    ''' </summary>
    Public Property StatusRecordVisible As Boolean
        Get
            Return (INDBarEditItemStatus.Visibility = BarItemVisibility.Always)
        End Get
        Set(value As Boolean)
            If value Then
                INDBarEditItemStatus.Visibility = BarItemVisibility.Always
            Else
                INDBarEditItemStatus.Visibility = BarItemVisibility.Never
            End If
            INDBarEditItemStatus.Refresh()
            VisibleAndCollapseRibbonPage()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el estado seleccionado.
    ''' </summary>
    ''' <value>
    ''' valor de estado que tiene el registro.
    ''' </value>
    Public Property StatusRecord As Object
        Get
            Return INDBarEditItemStatus.Tag
        End Get
        Set(value As Object)
            If value IsNot Nothing Then
                If Me.States Is Nothing OrElse Me.States.Count = 0 Then
                    StatesWhitActions = New eActionsStatusRecords() {value}
                End If
                If Not INDBarEditItemStatus.Enabled Then
                    INDBarEditItemStatus.Enabled = True
                End If
                INDBarEditItemStatus.Tag = value
                ChangeStateRecord()
                If Me.PermissionsForm IsNot Nothing Then
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.ActivarInactivar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                    End If
                End If
            Else
                CleanStatusBar()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para limpiar los registros seleccionados
    ''' </summary>
    Public Sub CleanStatusBar()
        INDBarEditItemStatus.EditValue = Nothing
        INDBarEditItemStatus.Tag = Nothing
        INDBarEditItemStatus.Refresh()
        INDRiteStatus.Appearance.BackColor = Color.White
    End Sub

    ''' <summary>
    ''' Propieda para ocultar solo el control del estado
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ControlHideStatus As Boolean
        Set(value As Boolean)
            If value Then
                INDBarEditItemStatus.Visibility = BarItemVisibility.Always
            Else
                INDBarEditItemStatus.Visibility = BarItemVisibility.Never
            End If
            VisibleAndCollapseRibbonPage()
            'LocateControlPanel()
        End Set
    End Property

    ''' <summary>
    ''' Evento que se dispara al seleccionar un estado del registro y cambambia la apariencia del control.
    ''' </summary>
    Private Sub INDgleStatusBar_EditValueChanged(sender As Object, e As EventArgs)
        ChangeStateRecord()
    End Sub


    Private Sub ChangeStateRecord()
        If ListStates Is Nothing OrElse ListStates.Count = 0 Then
            INDRiteStatus.Appearance.BackColor = Color.White
            INDRiteStatus.Appearance.BackColor2 = Color.White
        ElseIf INDBarEditItemStatus.Tag IsNot Nothing Then
            Dim obj = ListStates.Where(Function(x) x.StatusValue = INDBarEditItemStatus.Tag).SingleOrDefault
            If obj IsNot Nothing Then
                INDRiteStatus.Appearance.BackColor = obj.StatusColor
                INDRiteStatus.Appearance.BackColor2 = obj.StatusColor
                INDBarEditItemStatus.EditValue = obj.StatusName
            End If
        End If
    End Sub

#End Region

#Region "Filtros Registros"
    ''' <summary>
    ''' Evento que se dispara cuando se cambia de registro en la navegacion de la barra
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Public Event RecordNavigationChangeEvent(ByVal Record As Object)

    ''' <summary>
    ''' Variable que establece la posicion del registro visible
    ''' </summary>
    ''' <remarks></remarks>
    Private RecordPosition As Integer
    ''' <summary>
    ''' Variable que obtiene la posicion actual del registro visible
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentRecordPosition As Integer
        Get
            Return RecordPosition
        End Get
    End Property

    ''' <summary>
    ''' Variable que establece la posicion del registro antes de ser modificada
    ''' </summary>
    Private _LastRecordPosition As Integer
    ''' <summary>
    ''' Variable que obtiene la posicion del registro antes de ser modificada
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property LastRecordPosition As Integer
        Get
            Return _LastRecordPosition
        End Get
    End Property


    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource para poder navegar a traves de registros
    ''' </summary>
    Private _FilterDataSource As IEnumerable(Of Object)
    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource para poder navegar a traves de registros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilterDataSource As IEnumerable(Of Object)
        Get
            Return _FilterDataSource
        End Get
        Set(value As IEnumerable(Of Object))
            RecordPosition = 0
            _FilterDataSource = value
            INDgcRecords.DataSource = value
            If value IsNot Nothing AndAlso value.Count > 1 Then
                BarBtnFilter.Caption = String.Concat(RecordPosition + 1, " de ", FilterDataSource.Count)
                RibbonPageNavigationRecords.Visible = True
                LoadControlsNavigation()
                RaiseEvent RecordNavigationChangeEvent(value(RecordPosition))
            Else
                RibbonPageNavigationRecords.Visible = False
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad para saber cuantas homologaciones tiene el documento contable
    ''' </summary>
    Dim _homologationsCount As Integer
    Public Property HomologationsCount As Integer
        Get
            Return _homologationsCount
        End Get
        Set(value As Integer)
            _homologationsCount = value
            Select Case value
                Case 0
                    BarBtnHomologation.LargeImageIndex = 27
                Case 1
                    BarBtnHomologation.LargeImageIndex = 31
                Case 2
                    BarBtnHomologation.LargeImageIndex = 32
                Case 3
                    BarBtnHomologation.LargeImageIndex = 33
                Case 4
                    BarBtnHomologation.LargeImageIndex = 34
                Case 5
                    BarBtnHomologation.LargeImageIndex = 35
                Case 6
                    BarBtnHomologation.LargeImageIndex = 36
                Case 7
                    BarBtnHomologation.LargeImageIndex = 37
                Case 8
                    BarBtnHomologation.LargeImageIndex = 38
                Case 9
                    BarBtnHomologation.LargeImageIndex = 39
                Case 10
                    BarBtnHomologation.LargeImageIndex = 40
                Case Else
                    BarBtnHomologation.LargeImageIndex = 41
            End Select
        End Set
    End Property

    ''' <summary>
    ''' id del libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Dim _legalBookId As Integer
    Public Property LegalBookId As Integer
        Get
            Return _legalBookId
        End Get
        Set(value As Integer)
            _legalBookId = value
            If value = 0 Then
                INDGcJournalVourcherDetail.DataSource = Nothing
                If _FilterDataSourceJournalVoucher IsNot Nothing Then
                    _FilterDataSourceJournalVoucher.Clear()
                End If

                _listLegalBookId.Clear()
                INDLciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource para poder navegar a traves de registros
    ''' </summary>
    Private _FilterDataSourceJournalVoucher As List(Of JournalVouchers)

    Private _legalBook As XPCollection

    Private _listLegalBookId As New List(Of Integer)

    Public Sub SetDataSourceJuornalVoucher(journalVoucher As List(Of JournalVouchers), legalBook As XPCollection)
        If _FilterDataSourceJournalVoucher Is Nothing Then
            _FilterDataSourceJournalVoucher = journalVoucher
        Else
            _FilterDataSourceJournalVoucher.AddRange(journalVoucher)
        End If


        _legalBook = legalBook

        If _legalBook.Count > 1 Then
            CtrNavigationRecord1.ColumnInfo = ({New ColumnInfo With {.Caption = "Libro", .FieldName = "CodeName", .AllowEdit = False}, New ColumnInfo With {.Caption = "Tipo", .FieldName = "TypeBookName", .AllowEdit = False}}.ToList())
            CtrNavigationRecord1.FilterDataSource = _legalBook
            INDPcRecordNavigation.Visible = True
        Else
            INDPcRecordNavigation.Visible = False
        End If


        If _legalBook.Count > 0 Then
            INDTxtBook.EditValue = _legalBook(0).CodeName
            Dim dataSourceTmp As JournalVouchers
            If _legalBookId = 0 Then
                _legalBookId = _legalBook(0).Id
                dataSourceTmp = (From a In _FilterDataSourceJournalVoucher Where a.LegalBookId = _legalBook(0).Id Select a).FirstOrDefault()
            Else
                dataSourceTmp = (From a In _FilterDataSourceJournalVoucher Where a.LegalBookId = _legalBookId Select a).FirstOrDefault()

            End If

            If dataSourceTmp IsNot Nothing Then

                INDLciHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDGcJournalVourcherDetail.DataSource = dataSourceTmp.JournalVoucherDetails
                INDGcJournalVourcherDetail.RefreshDataSource()
            Else
                INDLciHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDGcJournalVourcherDetail.DataSource = Nothing
            End If

            Dim Culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Culture.NumberFormat = New CultureInfo(_legalBook(0).CommonCurrency.Abbreviation.ToString().GetCultureId).NumberFormat
            Dim CultureId = _legalBook(0).CommonCurrency.Abbreviation.ToString().GetCultureId
            FormatGrid(CultureId, colCredits, Culture)
            FormatGrid(CultureId, colDebits, Culture)
        End If


    End Sub


    ''' <summary>
    ''' metodo para setear el formato de la moneda en las columnas que se le indiquen
    ''' </summary>
    ''' <param name="CultureId"></param>
    ''' <param name="Column"></param>
    Private Sub FormatGrid(CultureId As Integer, ByRef Column As GridColumn, CultureNumbertFormat As CultureInfo)
        Dim fInfo As FormatInfo = Column.DisplayFormat
        fInfo.FormatType = FormatType.Custom
        fInfo.FormatString = "c2"
        fInfo.Format = CultureNumbertFormat
        Column.SummaryItem.Format = CultureNumbertFormat
    End Sub

    ''' <summary>
    ''' Propiedad para asignar la visibilidad al boton de homologar
    ''' </summary>
    ''' <returns></returns>
    Public Property LyHomologationButton As DevExpress.XtraLayout.Utils.LayoutVisibility
        Get
            Return INDLciHomologation.Visibility
        End Get
        Set(value As DevExpress.XtraLayout.Utils.LayoutVisibility)
            INDLciHomologation.Visibility = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar la visibilidad al boton de guardar homologación
    ''' </summary>
    ''' <returns></returns>
    Public Property LySaveHomologationButton As DevExpress.XtraLayout.Utils.LayoutVisibility
        Get
            Return INDLciSave.Visibility
        End Get
        Set(value As DevExpress.XtraLayout.Utils.LayoutVisibility)
            INDLciSave.Visibility = value
        End Set
    End Property

    Private Sub CtrNavigationRecord1_RecordNavigationChangeEvent(Record As Object) Handles CtrNavigationRecord1.RecordNavigationChangeEvent
        INDTxtBook.EditValue = Record.CodeName
        _legalBookId = Record.Id
        Dim dataSourceTmp = (From a In _FilterDataSourceJournalVoucher Where a.LegalBookId = Record.Id Select a).FirstOrDefault()
        If dataSourceTmp IsNot Nothing Then
            INDLciHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never


            INDGcJournalVourcherDetail.DataSource = dataSourceTmp.JournalVoucherDetails
            INDGcJournalVourcherDetail.RefreshDataSource()
        Else

            INDLciHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGcJournalVourcherDetail.DataSource = Nothing
        End If
        Dim bookIdTmp = _listLegalBookId.Find(Function(x) x = LegalBookId)
        If bookIdTmp = 0 Then
            INDLciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDLciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

    End Sub
    ''' <summary>
    ''' Método para cambiar la posición del navigationBar (se envía la posición en el arreglo)
    ''' </summary>
    ''' <param name="position">The position.</param>
    Public Async Sub FilterNavigationPosition(position As Integer)
        If _FilterDataSource IsNot Nothing Then
            If _funcPermitirNavegacion IsNot Nothing AndAlso Not Await _funcPermitirNavegacion() Then
                Exit Sub
            End If
            _LastRecordPosition = RecordPosition
            RecordPosition = position
            BarBtnFilter.Caption = String.Concat(RecordPosition + 1, " de ", FilterDataSource.Count)
            LoadControlsNavigation()
            RaiseEvent RecordNavigationChangeEvent(_FilterDataSource(RecordPosition))
        End If
    End Sub

    Dim _ColumnInfo As List(Of ColumnInfo) = Nothing
    ''' <summary>
    ''' Obtiene o asigna la lista de columnas que se motraran
    ''' y enlazaran en el datasource de la rejilla
    ''' </summary>
    ''' <value>Lista de columnas a enlazar</value>
    ''' <returns>Lista de columnas enlazadas</returns>
    Public Property ColumnInfo As List(Of ColumnInfo)
        Get
            Return Me._ColumnInfo
        End Get
        Set(value As List(Of ColumnInfo))
            Me._ColumnInfo = value
            INDgcvRecords.Columns.Clear()
            If Me._ColumnInfo IsNot Nothing Then
                For Each ci As ColumnInfo In Me._ColumnInfo
                    INDgcvRecords.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn())
                    INDgcvRecords.Columns(INDgcvRecords.Columns.Count - 1).Name = "Col" & INDgcvRecords.Columns.Count - 1
                    INDgcvRecords.Columns(INDgcvRecords.Columns.Count - 1).Caption = ci.Caption.Trim()
                    INDgcvRecords.Columns(INDgcvRecords.Columns.Count - 1).FieldName = ci.FieldName.Trim()
                    INDgcvRecords.Columns(INDgcvRecords.Columns.Count - 1).Visible = ci.Visible
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Evento del boton filtrar que aplica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnFilter_Click(sender As Object, e As EventArgs) Handles INDbtnFilter.Click
        INDgcvRecords.ApplyFindFilter(INDtxtFilter.Text)
    End Sub

    ''' <summary>
    ''' Metodo para limpiar la informacion del filtro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnClean_Click(sender As Object, e As EventArgs) Handles INDbtnClean.Click
        INDgcvRecords.ApplyFindFilter(String.Empty)
        INDtxtFilter.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo para abrir el registro seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnOpenRecordSelection_Click(sender As Object, e As EventArgs) Handles INDbtnOpenRecordSelection.Click
        OpenRecord()
    End Sub

    ''' <summary>
    ''' Evento cuando se Cambia el texto del control de filtro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtFilter_TextChanged(sender As Object, e As EventArgs) Handles INDtxtFilter.TextChanged
        If INDtxtFilter.Text = String.Empty Then
            INDgcvRecords.ApplyFindFilter(String.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Evento keydown del control de filtro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDgcvRecords.ApplyFindFilter(INDtxtFilter.Text)
        End If
    End Sub

    ''' <summary>
    ''' Evento doble clic sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcvRecords_DoubleClick(sender As Object, e As EventArgs) Handles INDgcvRecords.DoubleClick
        OpenRecord()
    End Sub

    ''' <summary>
    ''' Metodo para abrir un registro seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub OpenRecord()
        If INDgcvRecords.FocusedRowHandle >= 0 Then
            If _funcPermitirNavegacion IsNot Nothing AndAlso Not Await _funcPermitirNavegacion() Then
                Exit Sub
            End If
            RecordPosition = FilterDataSource.ToList.IndexOf(INDgcvRecords.GetFocusedRow())
            BarBtnFilter.Caption = String.Concat(RecordPosition + 1, " de ", FilterDataSource.Count)
            LoadControlsNavigation()
            RaiseEvent RecordNavigationChangeEvent(INDgcvRecords.GetFocusedRow())
            INDgcvRecords.ApplyFindFilter(String.Empty)
            INDtxtFilter.Text = String.Empty
            INDpccFilterRecords.HidePopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento para mostrar el primer registro en la navegacion de la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarBtnFirst_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnFirst.ItemClick
        If _funcPermitirNavegacion IsNot Nothing AndAlso Not Await _funcPermitirNavegacion() Then
            Exit Sub
        End If
        _LastRecordPosition = RecordPosition
        RecordPosition = 0
        BarBtnFilter.Caption = String.Concat(RecordPosition + 1, " de ", FilterDataSource.Count)
        LoadControlsNavigation()
        RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
    End Sub

    ''' <summary>
    ''' Evento para mostrar el anterior registro en la navegacion de la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarBtnBack_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnBack.ItemClick
        If _funcPermitirNavegacion IsNot Nothing AndAlso Not Await _funcPermitirNavegacion() Then
            Exit Sub
        End If
        _LastRecordPosition = RecordPosition
        RecordPosition -= 1
        BarBtnFilter.Caption = String.Concat(RecordPosition + 1, " de ", FilterDataSource.Count)
        LoadControlsNavigation()
        RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
    End Sub

    ''' <summary>
    ''' Evento para mostrar el siguiente registro de la navegacion de la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarBtnNext_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnNext.ItemClick
        If _funcPermitirNavegacion IsNot Nothing AndAlso Not Await _funcPermitirNavegacion() Then
            Exit Sub
        End If
        _LastRecordPosition = RecordPosition
        RecordPosition += 1
        BarBtnFilter.Caption = String.Concat(RecordPosition + 1, " de ", FilterDataSource.Count)
        LoadControlsNavigation()
        RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
    End Sub

    ''' <summary>
    ''' Evento para mostrar el ultimo registro de la navegacion de la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarBtnLast_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnLast.ItemClick
        If _funcPermitirNavegacion IsNot Nothing AndAlso Not Await _funcPermitirNavegacion() Then
            Exit Sub
        End If
        _LastRecordPosition = RecordPosition
        RecordPosition = FilterDataSource.Count - 1
        BarBtnFilter.Caption = String.Concat(RecordPosition + 1, " de ", FilterDataSource.Count)
        LoadControlsNavigation()
        RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
    End Sub

    ''' <summary>
    ''' Metodo para msotrar o ocultar los controles de navegacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControlsNavigation()
        Select Case RecordPosition
            Case FilterDataSource.Count - 1
                BarBtnNext.Enabled = False
                BarBtnLast.Enabled = False
                BarBtnBack.Enabled = True
                BarBtnFirst.Enabled = True
            Case 0
                BarBtnBack.Enabled = False
                BarBtnFirst.Enabled = False
                BarBtnNext.Enabled = True
                BarBtnLast.Enabled = True
            Case Else
                BarBtnBack.Enabled = True
                BarBtnFirst.Enabled = True
                BarBtnNext.Enabled = True
                BarBtnLast.Enabled = True
        End Select
    End Sub
#End Region

#Region "Reports"

    ''' <summary>
    ''' Id del frontal el cual va a mostrar los reportes
    ''' y sus definiciones. Si el valor es cero, se toma el id
    ''' del frontal en donde se encuentra la barra de botones
    ''' </summary>
    Private _idFormToReport As Integer
    ''' <summary>
    ''' Integer que representa el Id del registro que genera el reporte
    ''' </summary>
    Private _idEntityReport As Integer
    ''' <summary>
    ''' Lista de parametros a pasar para el reporte
    ''' </summary>
    Private _parametersReport() As Object
    ''' <summary>
    ''' Lista de definiciones de reporte
    ''' </summary>
    Private _listDefinitions As List(Of ReportDefinition)
    ''' <summary>
    ''' Objeto del perfil de impresión del usuario
    ''' </summary>
    Private _printProfile As PrintProfile

#Region "New Reports"

    ''' <summary>
    ''' Ocurre cuando se inicia la carga de reportes y definiciones
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event QueryLoadReportsAndDefinitions(ByVal sender As Object, ByVal e As LoadReportsAndDefinitionsEventArgs)
    ''' <summary>
    ''' Ocurre cuando se inicia la carga de reportes y definiciones
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Sub OnQueryLoadReportsAndDefinitions(ByVal sender As Object, ByVal e As LoadReportsAndDefinitionsEventArgs)
        RaiseEvent QueryLoadReportsAndDefinitions(sender, e)
    End Sub

    ''' <summary>
    ''' Valida si existe la ruta para los reportes
    ''' </summary>
    ''' <returns></returns>
    Private Function CheckReportsPath() As Boolean
        If String.IsNullOrEmpty(ConfigurationFile.Instance.ReportsPath) Then
            Mensaje(EeventViewerImages.Advertencia) = "El usuario no tiene configurada una ruta para la personalización de los reportes"
            Me.BarBtnImprimir.Enabled = False
            Return False
        End If
        Return True
    End Function


    ''' <summary>
    ''' Realiza la carga de reportes y definiciones asociadas al formulario
    ''' </summary>
    Public Function LoadReportsAndDefinitions() As Boolean
        Me.INDPreviewSmb.Enabled = True
        Me.INDPrintSmb.Enabled = True
        If Not CheckReportsPath() Then
            Return False
        End If
        Dim reports As List(Of VieReport) = BaseClass.ListReportsByIdForm(Me._idFormToReport)
        Dim args As New LoadReportsAndDefinitionsEventArgs With {.ListReportsAndDefinitions = reports}
        Me.OnQueryLoadReportsAndDefinitions(Me, args)
        If args.ListReportsAndDefinitions.Count > 0 Then
            Me._listDefinitions = Nothing
            Me._listDefinitions = New System.Collections.Generic.List(Of ReportDefinition)()
            Dim form = BaseClass.GetFormById(Me._idFormToReport)
            For Each r In args.ListReportsAndDefinitions
                Dim def As New ReportDefinition()
                'Agregamos la definicion compilada del reporte
                def.FormName = form.FormName
                def.ReportName = r.Name
                def.Name = r.Name
                def.Path = String.Empty
                def.ObjVieReport = r
                Me._listDefinitions.Add(def)
                If Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
                    Dim pathRepDefault As String = Path.Combine(ConfigurationFile.Instance.ReportsPath, form.IdForm & "Repx.Default")
                    Dim nameRepDefault As String = String.Empty
                    If File.Exists(pathRepDefault) Then
                        nameRepDefault = File.ReadAllText(pathRepDefault)
                        nameRepDefault = nameRepDefault.Trim()
                    End If
                    'Validamos si la definición compilada es la por defecto
                    If _idReport.HasValue AndAlso _idReport = r.Id Then
                        def.IsDefault = True
                    ElseIf _idReport Is Nothing OrElse _idReport = 0 Then
                        def.IsDefault = (def.Name.ToLower().Equals(nameRepDefault.Trim().ToLower()))
                    End If
                    'Buscamos y agregamos las definiciones personalizadas del reporte
                    Dim pattern As String = form.IdForm & "." & r.ClassName & ".*.repx"
                    Dim found() As String = Directory.GetFiles(ConfigurationFile.Instance.ReportsPath, pattern, SearchOption.TopDirectoryOnly)
                    For Each f In found
                        def = New ReportDefinition()
                        Dim partsName() As String = f.Substring(f.LastIndexOf("\") + 1).Split(".")
                        If partsName.Length >= 4 Then
                            def.FormName = form.FormName
                            def.ReportName = r.Name
                            def.Name = partsName(2)
                            def.Path = f
                            def.ObjVieReport = r
                            def.IsDefault = (partsName(2).ToLower().Equals(nameRepDefault.Trim().ToLower()))
                            Me._listDefinitions.Add(def)
                        End If
                    Next
                End If
            Next
            'Comprobamos si no hubo definiciones por defecto ni personalizadas ni compiladas
            If Not Me._listDefinitions.Any(Function(d) d.IsDefault) Then
                Dim df = Me._listDefinitions.Where(Function(d) d.Path.Equals(String.Empty) And (Not _idReport.HasValue OrElse d.ObjVieReport.Id = _idReport)).ToList()
                df(0).IsDefault = True
                If Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
                    Dim pathRepDefault As String = Path.Combine(ConfigurationFile.Instance.ReportsPath, form.IdForm & "Repx.Default")
                    If df.Count > 0 Then
                        File.WriteAllText(pathRepDefault, df(0).Name)
                    End If
                End If
            End If
            'Cargamos las definiciones en la rejilla
            Me.INDDefReportsGc.DataSource = Me._listDefinitions.FindAll(Function(m) Not _idReport.HasValue OrElse m.ObjVieReport.Id = _idReport)
            Me.INDDefReportsGc.RefreshDataSource()
            Me.INDDefReportsGc.Visible = True
            Me.BarBtnImprimir.Enabled = (Me._listDefinitions.Count > 0)
            Me.INDMessageDefinitionsLbl.Visible = False
        Else
            DisabledOptionReport()
            Return False
        End If
        'Verificamos las preferencias de impresión
        'Dim frm = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms).Where(Function(f) f.Id = Me._idFormToReport.ToString()).FirstOrDefault()
        Dim frm As VieForm
        Using mode As New MBarraBotones()
            frm = mode.ListForms(Me._idFormToReport.ToString())
        End Using

        If frm Is Nothing Then
            Return True
        End If

        Me._printProfile = Window.Utils.GetPrintProfile(SessionValues.Instance.UserIndigo, Me._idFormToReport)

        Me.LyciPrintOnSave.Visibility = If(frm.PrintEvents.Contains("S"), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        Me.ChkPrintOnSave.EditValue = Me._printProfile.OnSave
        Me.LyciPrintOnUpdate.Visibility = If(frm.PrintEvents.Contains("U"), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        Me.ChkPrintOnUpdate.EditValue = Me._printProfile.OnUpdate
        Me.LyciPrintOnConfirm.Visibility = If(frm.PrintEvents.Contains("C"), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        Me.ChkPrintOnConfirm.EditValue = Me._printProfile.OnConfirm
        Me.LyciPrintOnCancel.Visibility = If(frm.PrintEvents.Contains("A"), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        Me.ChkPrintOnCancel.EditValue = Me._printProfile.OnCancel

        Me.RbtnActions.EditValue = If(Me._printProfile.IsPrint, "P", "V")


        Return True
    End Function

    Private _idReport As Integer?
    Public Sub PrintReportByIdReport(ByVal action As PrintReportAction, ByVal idEntityReport As Integer, ByVal idForm As Integer, idReport As Integer, ByVal ParamArray params() As Object)
        _idReport = idReport
        PrintReport(action, idEntityReport, idForm, params)
    End Sub

    ''' <summary>
    ''' Ejecuta la carga de definiciones de los reportes asociados al formulario,
    ''' asi como realiza la impresión del reporte con la definición por defecto
    ''' </summary>
    ''' <param name="action">Evento desde el cual se invoca la impresión</param>
    ''' <param name="idEntityReport">Id de la entidad a imprimir</param>
    ''' <param name="idForm">Id del frontal si es diferente al actual, de lo contrario se debe enviar cero</param>
    ''' <param name="params">Lista de parametros para el reporte</param>
    Public Async Sub PrintReport(ByVal action As PrintReportAction, ByVal idEntityReport As Integer, ByVal idForm As Integer, ByVal ParamArray params() As Object)
        Me._idEntityReport = idEntityReport
        Me._parametersReport = params
        Me._idFormToReport = If(idForm = 0, CInt(Me.ParentForm.Tag), idForm)
        If Me.LoadReportsAndDefinitions() Then
            If action <> PrintReportAction.None Then
                'CType(Me.ParentForm, FormBase).AsyncLoader(True)
                Dim objeto = Me._listDefinitions.Where(Function(d) d.IsDefault).First()
                Me.BarBtnImprimir.Enabled = True
                If action = PrintReportAction.Create Then
                    If objeto.ObjVieReport.ClassType IsNot Nothing AndAlso Me.LyciPrintOnSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso Me.ChkPrintOnSave.Checked Then
                        Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
                        TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
                        If Me._printProfile.IsPrint Then
                            Dim tool = Await AsyncReportPrint(TryCast(rep, IReport), objeto.Path)
                            tool.Print()
                        Else 'Visualización
                            Dim reportExt = Await AsyncReport(TryCast(rep, IReport), objeto.Path)
                            Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me._idFormToReport, Me._idEntityReport, If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path))
                            toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm)
                        End If
                    End If
                ElseIf action = PrintReportAction.Update Then
                    If objeto.ObjVieReport.ClassType IsNot Nothing AndAlso Me.LyciPrintOnUpdate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso Me.ChkPrintOnUpdate.Checked Then
                        Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
                        TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
                        If Me._printProfile.IsPrint Then
                            Dim tool = Await AsyncReportPrint(TryCast(rep, IReport), objeto.Path)
                            tool.Print()
                        Else 'Visualización
                            Dim reportExt = Await AsyncReport(TryCast(rep, IReport), objeto.Path)
                            Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me._idFormToReport, Me._idEntityReport, If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path))
                            toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm)
                        End If
                    End If
                ElseIf action = PrintReportAction.Confirm Then
                    If objeto.ObjVieReport.ClassType IsNot Nothing AndAlso Me.LyciPrintOnConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso Me.ChkPrintOnConfirm.Checked Then
                        Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
                        TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
                        If Me._printProfile.IsPrint Then
                            Dim tool = Await AsyncReportPrint(TryCast(rep, IReport), objeto.Path)
                            tool.Print()
                        Else 'Visualización
                            Dim reportExt = Await AsyncReport(TryCast(rep, IReport), objeto.Path)
                            Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me._idFormToReport, Me._idEntityReport, If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path))
                            toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm)
                        End If
                    End If
                ElseIf action = PrintReportAction.Cancel Then
                    If objeto.ObjVieReport.ClassType IsNot Nothing AndAlso Me.LyciPrintOnCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso Me.ChkPrintOnCancel.Checked Then
                        Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
                        TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
                        If Me._printProfile.IsPrint Then
                            Dim tool = Await AsyncReportPrint(TryCast(rep, IReport), objeto.Path)
                            tool.Print()
                        Else 'Visualización
                            Dim reportExt = Await AsyncReport(TryCast(rep, IReport), objeto.Path)
                            Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me._idFormToReport, Me._idEntityReport, If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path))
                            toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm)
                        End If
                    End If
                ElseIf action = PrintReportAction.DirectPrinting Then
                    If objeto.ObjVieReport.ClassType IsNot Nothing Then
                        Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
                        TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
                        If Me._printProfile.IsPrint Then
                            Dim tool = Await AsyncReportPrint(TryCast(rep, IReport), objeto.Path)
                            tool.Print()
                        Else 'Visualización
                            Dim reportExt = Await AsyncReport(TryCast(rep, IReport), objeto.Path)
                            Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me._idFormToReport, Me._idEntityReport, If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path))
                            toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm)
                        End If
                    End If
                ElseIf action = PrintReportAction.ViewPrinting Then
                    If objeto.ObjVieReport.ClassType IsNot Nothing Then
                        Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
                        TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
                        Dim reportExt = Await AsyncReport(TryCast(rep, IReport), objeto.Path)
                        Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me._idFormToReport, Me._idEntityReport, If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path))
                        toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm)
                    End If
                Else
                    If objeto.ObjVieReport.ClassType IsNot Nothing Then
                        Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
                        TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
                        Dim reportExt = Await AsyncReport(TryCast(rep, IReport), objeto.Path)
                        Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me._idFormToReport, Me._idEntityReport, If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path))
                        toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm)
                    End If
                End If
                'CType(Me.ParentForm, FormBase).AsyncLoader(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se crea o elimina la preferencia de impresión
    ''' al crear un nuevo documento
    ''' </summary>
    Private Sub ChkPrintOnSave_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrintOnSave.CheckedChanged
        Me._printProfile.OnSave = Me.ChkPrintOnSave.Checked
        Window.Utils.CreatePrintProfile(SessionValues.Instance.UserIndigo, Me._idFormToReport, Me._printProfile)
    End Sub

    ''' <summary>
    ''' Aqui se crea o elimina la preferencia de impresión
    ''' al actualizar un documento
    ''' </summary>
    Private Sub ChkPrintOnUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrintOnUpdate.CheckedChanged
        Me._printProfile.OnUpdate = Me.ChkPrintOnUpdate.Checked
        Window.Utils.CreatePrintProfile(SessionValues.Instance.UserIndigo, Me._idFormToReport, Me._printProfile)
    End Sub

    ''' <summary>
    ''' Evento para personalizar reporte
    ''' </summary>
    Private Sub INDEditSmb_Click(sender As Object, e As EventArgs)
        CType(Me.ParentForm, FormBase).AsyncLoader(True)
        Dim objeto As ReportDefinition = CType(Me.INDDefReportsBv.GetRow(Me.INDDefReportsBv.FocusedRowHandle), ReportDefinition)
        Me.INDReportsPcc.HidePopup()
        If objeto.ObjVieReport.ClassType IsNot Nothing Then
            Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
            Dim tempPath = If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path)
            Dim reportBase As New Reporter.ReportBase
            reportBase.EditReport(rep, tempPath)
        End If
        CType(Me.ParentForm, FormBase).AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Evento al cambiar el valor del checkEdit IsDefault
    ''' </summary>
    Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit1.EditValueChanging
        If e.OldValue = True Then
            e.Cancel = True
        Else
            Dim chk As DevExpress.XtraEditors.CheckEdit = CType(sender, DevExpress.XtraEditors.CheckEdit)
            Dim objeto As ReportDefinition = CType(Me.INDDefReportsBv.GetRow(Me.INDDefReportsBv.FocusedRowHandle), ReportDefinition)
            Try
                If Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
                    Dim form = BaseClass.GetFormById(Me._idFormToReport)
                    Dim pathRepDefault As String = Path.Combine(ConfigurationFile.Instance.ReportsPath, form.IdForm & "Repx.Default")
                    objeto.IsDefault = True
                    File.WriteAllText(pathRepDefault, objeto.Name)
                    For Each itemAux As ReportDefinition In TryCast(Me.INDDefReportsBv.DataSource, List(Of ReportDefinition)).Where(Function(d) Not d.Name.Equals(objeto.Name)).ToList()
                        itemAux.IsDefault = False
                        If Not itemAux.Path.Equals(String.Empty) Then
                            If IO.File.Exists(itemAux.Path) AndAlso itemAux.Path.EndsWith(".Default.repx") Then
                                My.Computer.FileSystem.RenameFile(itemAux.Path, itemAux.Path.Substring(itemAux.Path.LastIndexOf("\") + 1).Replace(".Default.repx", ".repx"))
                                itemAux.Path = itemAux.Path.Replace(".Default.repx", ".repx")
                            End If
                        End If
                    Next
                    Me.INDDefReportsBv.RefreshData()
                Else
                    XtraMessageBox.Show("La ruta de reportes especificada en el archivo de configuración no es válida")
                End If
            Catch ex As Exception
                e.Cancel = True
                XtraMessageBox.Show("No se ha podido cambiar la definición por defecto." & Environment.NewLine & ex.Message)
            End Try
        End If
    End Sub

#End Region

#Region "Old Reports"

    Private Sub DisabledOptionReport()
        Me.INDPreviewSmb.Enabled = False
        Me.INDPrintSmb.Enabled = False
        Me.INDMessageDefinitionsLbl.Visible = True
    End Sub

    Public Sub HideOptionReport()
        Me.BarBtnImprimir.Visibility = BarItemVisibility.Never
        VisibleAndCollapseRibbonPage()
    End Sub

    ''' <summary>
    ''' Evento para imprimir reporte
    ''' </summary>
    Private Async Sub INDPrintSmb_Click(sender As Object, e As EventArgs) Handles INDPrintSmb.Click
        Dim objeto As ReportDefinition
        If Me.INDDefReportsBv.FocusedRowHandle >= 0 Then
            objeto = CType(Me.INDDefReportsBv.GetRow(Me.INDDefReportsBv.FocusedRowHandle), ReportDefinition)
        Else
            objeto = Me._listDefinitions.Where(Function(d) d.IsDefault = True).FirstOrDefault()
        End If
        Me.INDReportsPcc.HidePopup()
        CType(Me.ParentForm, FormBase).AsyncLoader(True)
        If objeto.ObjVieReport.ClassType IsNot Nothing Then
            Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
            TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
            Dim pathRep As String = If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path)
            Dim reportExt = Await AsyncReport(TryCast(rep, IReport), pathRep)
            Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me.ParentForm.Tag, Me._idEntityReport, pathRep)
            toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm, "", True)
        End If
        CType(Me.ParentForm, FormBase).AsyncLoader(False)
        Me.INDReportsPcc.HidePopup()
    End Sub

    ''' <summary>
    ''' Evento para visualizar reporte
    ''' </summary>
    Private Async Sub INDPreviewSmb_Click(sender As Object, e As EventArgs) Handles INDPreviewSmb.Click
        Dim objeto As ReportDefinition
        If Me.INDDefReportsBv.FocusedRowHandle >= 0 Then
            objeto = CType(Me.INDDefReportsBv.GetRow(Me.INDDefReportsBv.FocusedRowHandle), ReportDefinition)
        Else
            objeto = Me._listDefinitions.Where(Function(d) d.IsDefault = True).FirstOrDefault()
        End If

        Me.INDReportsPcc.HidePopup()
        CType(Me.ParentForm, FormBase).AsyncLoader(True)
        If objeto.ObjVieReport.ClassType IsNot Nothing Then
            Dim rep As Object = Activator.CreateInstance(objeto.ObjVieReport.ClassType)
            TryCast(rep, IReport).ParametrosReporte = Me._parametersReport
            Dim pathRep As String = If(objeto.Path.Equals(String.Empty), Path.Combine(ConfigurationFile.Instance.ReportsPath, Me._idFormToReport & "." & objeto.ObjVieReport.ClassName & ".{0}.repx"), objeto.Path)
            Dim reportExt = Await AsyncReport(TryCast(rep, IReport), pathRep)
            Dim toolReport = New ReportPrintToolExt(CType(Me.ParentForm, FormBase), reportExt, Me.ParentForm.Tag, Me._idEntityReport, pathRep)
            toolReport.ExecReport(objeto.ObjVieReport.Name, Me.PermissionsForm)
        End If
    End Sub

    ''' <summary>
    ''' Función para cargar asincronamente visor de reportes
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Function AsyncReport(rep As IReport, Optional path As String = "") As Task(Of XtraReport)
        Dim reportBase As New Reporter.ReportBase
        Return reportBase.ExecReport(rep, path)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Function AsyncReportPrint(rep As IReport, Optional path As String = "") As Task(Of ReportPrintTool)
        Dim reportBase As New Reporter.ReportBase
        Return reportBase.PrintReport(rep, path)
    End Function

#End Region

#End Region

#Region "Ejecucion Plugins"

#Region "Fields"

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase que contiene mi plugins
    ''' </summary>
    Dim Plugins As EjecucionPlugins
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase BarButtonItem y hacer uso de las propiedades de los botones
    ''' </summary>
    Dim OpcionItem As BarButtonItem

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que se utiliza para cargar los plugins encontrados en la barra.
    ''' </summary>
    Private Sub CargarPlugins()

        Plugins = New EjecucionPlugins
        OpcionItem = New BarButtonItem
        'Cargamos los plugins que esten disponibles
        Plugins.CargarPlugins()

        ' Iteramos sobre el listado de plugins encontrados 
        For Each ListadoPlugins In Plugins.PluginsAplicacion.ToList

            'Acomodamos los plugins en la barra 
            Select Case ListadoPlugins.TipoPlugins

                'Plugins Redes Sociales
                Case EPluginsTypes.RedesSociales
                    OpcionItem = New BarButtonItem
                    AddHandler OpcionItem.ItemClick, AddressOf ClickPlugins
                    OpcionItem.Name = "Tool_" & ListadoPlugins.NombrePlugins
                    OpcionItem.Caption = ListadoPlugins.NombrePlugins
                    OpcionItem.ImageIndex = ListadoPlugins.IconoPlugins
                    OpcionItem.Tag = Plugins.PluginsAplicacion.IndexOf(ListadoPlugins)
                    BarSubRedes.AddItem(OpcionItem)

                    'Plugins GestionDocumental
                Case EPluginsTypes.GestionDocumental
                    OpcionItem = New BarButtonItem
                    AddHandler OpcionItem.ItemClick, AddressOf ClickPlugins
                    OpcionItem.Name = "Tool_" & ListadoPlugins.NombrePlugins
                    OpcionItem.Caption = ListadoPlugins.NombrePlugins
                    OpcionItem.ImageIndex = ListadoPlugins.IconoPlugins
                    OpcionItem.Tag = Plugins.PluginsAplicacion.IndexOf(ListadoPlugins)
                    BarSubRedes.AddItem(OpcionItem)

                    'Plugins Redes ComunicacionesUnificadas
                Case EPluginsTypes.ComunicacionesUnificadas

                    OpcionItem = New BarButtonItem
                    AddHandler OpcionItem.ItemClick, AddressOf ClickPlugins
                    OpcionItem.Name = "Tool_" & ListadoPlugins.NombrePlugins
                    OpcionItem.Caption = ListadoPlugins.NombrePlugins
                    OpcionItem.ImageIndex = ListadoPlugins.IconoPlugins
                    OpcionItem.Tag = Plugins.PluginsAplicacion.IndexOf(ListadoPlugins)
                    BarSubRedes.AddItem(OpcionItem)

                    'Otros Plugins 
                Case EPluginsTypes.OtrosPlugins

                    OpcionItem = New BarButtonItem
                    AddHandler OpcionItem.ItemClick, AddressOf ClickPlugins
                    OpcionItem.Name = "Tool_" & ListadoPlugins.NombrePlugins
                    OpcionItem.Caption = ListadoPlugins.NombrePlugins
                    OpcionItem.ImageIndex = ListadoPlugins.IconoPlugins
                    OpcionItem.Tag = Plugins.PluginsAplicacion.IndexOf(ListadoPlugins)
                    BarSubRedes.AddItem(OpcionItem)
                Case Else

            End Select
        Next

    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para  Ejecutar el plugins Seleccionado.
    ''' </summary>
    ''' <param name="DatosCompartidos">DatosCompartidos - Objeto creado en el formulario - datos que necesito para cumplir el contrato</param>
    ''' <param name="TagControl">TagControl para saber que plugins ejecutar</param>
    Public Sub EjecutarPlugins(ByVal DatosCompartidos As Object, ByVal TagControl As Integer)
        Plugins.EjecutarPlugins(DatosCompartidos, TagControl)
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para ejecutar el plugins seleccionado dependiendo del tag del boton .
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs" /> instance containing the event data.</param>
    Public Sub ClickPlugins(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        RaiseEvent ClickPluginsCliente(sender, e)
    End Sub

#End Region

#End Region

#Region "Declaracion de eventos"

#Region "Eventos Del ListViewAgregado"

    ''' <summary>
    ''' Evento de give feed back del control list view agregado.
    ''' </summary>
    Private Sub EventoGiveFeedBackListViewAgregado(ByVal sender As System.Object, ByVal e As System.Windows.Forms.GiveFeedbackEventArgs)
        e.UseDefaultCursors = False
    End Sub

    ''' <summary>
    ''' Evento de mouse down en el control list view agregado.
    ''' </summary>
    Private Sub EventoMouseDownListViewAgregado(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        NuevoItem = listViewAgregado.GetItemAt(e.X, e.Y)
    End Sub

    ''' <summary>
    ''' Evento de mouse move del control list view agregado.
    ''' </summary>
    Private Sub EventoMouseMoveListViewAgregado(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If NuevoItem Is Nothing OrElse e.Button <> System.Windows.Forms.MouseButtons.Left Then
            Return
        End If

        If NuevoItem.Text = "Espacio Vacio" Then
            'dragItem_Renamed = New LayoutControlItem()
            ArrastrarItem_Renombrado = New EmptySpaceItem
            ArrastrarItem_Renombrado.Name = Guid.NewGuid().ToString()

            ArrastrarItem_Renombrado.Text = NuevoItem.Text
            listViewAgregado.DoDragDrop(ArrastrarItem_Renombrado, DragDropEffects.Copy)
            Exit Sub
        End If

        If NuevoItem.Text = "Etiqueta" Then
            ArrastrarItem_Renombrado = New SimpleLabelItem
            ArrastrarItem_Renombrado.Name = Guid.NewGuid().ToString()

            ArrastrarItem_Renombrado.Text = NuevoItem.Text
            listViewAgregado.DoDragDrop(ArrastrarItem_Renombrado, DragDropEffects.Copy)
            Exit Sub
        End If

        If NuevoItem.Text = "Divisor" Then
            ArrastrarItem_Renombrado = New SplitterItem
            ArrastrarItem_Renombrado.Name = Guid.NewGuid().ToString()

            ArrastrarItem_Renombrado.Text = NuevoItem.Text
            listViewAgregado.DoDragDrop(ArrastrarItem_Renombrado, DragDropEffects.Copy)
            Exit Sub
        End If
        If NuevoItem.Text = "Separador" Then
            ArrastrarItem_Renombrado = New SimpleSeparator
            ArrastrarItem_Renombrado.Name = Guid.NewGuid().ToString()

            ArrastrarItem_Renombrado.Text = NuevoItem.Text
            listViewAgregado.DoDragDrop(ArrastrarItem_Renombrado, DragDropEffects.Copy)
            Exit Sub
        End If
    End Sub

#End Region

#End Region

#Region "Documental System"

#Region "Fields"

    ''' <summary>
    ''' Model para digitalización
    ''' </summary>
    Private modelDocument As MDigitalization
    ''' <summary>
    ''' Objeto openFileDialog
    ''' </summary>
    Private fileOpener As New OpenFileDialog
    ''' <summary>
    ''' Formulario transparente
    ''' </summary>
    Private frmTrans As FrmTransparent
    ''' <summary>
    ''' Integer que representa el Id del Formulario
    ''' </summary>
    Private _idForm As Integer
    ''' <summary>
    ''' Integer que representa el Id del registro que tiene documentos
    ''' </summary>
    Private _idEntity As Integer
    ''' <summary>
    ''' Nombre de la entidad que se va auditar
    ''' </summary>
    ''' <remarks></remarks>
    Private _entityName As String
    ''' <summary>
    ''' Objeto que permite almacenar controladamente archivos temporales
    ''' </summary>
    ''' <remarks></remarks>
    Private _tempFileCollection As System.CodeDom.Compiler.TempFileCollection
    ''' <summary>
    ''' Formulario digitalización
    ''' </summary>
    Private _frmDigitalization As FrmDigitalization
    ''' <summary>
    ''' Control de Ribbon Documents
    ''' </summary>
    Private _controlRibbonDocuments As Boolean
    ''' <summary>
    ''' Control de Ribbon Audit
    ''' </summary>
    Private _controlRibbonAudit As Boolean
    ''' <summary>
    ''' Variable para la lista de documentos
    ''' </summary>
    Public _listDocuments As List(Of DocumentsStore)
    ''' <summary>
    ''' Mes auditoria
    ''' </summary>
    Public _month As String
    ''' <summary>
    ''' Año auditoria
    ''' </summary>
    Public _year As String
    ''' <summary>
    ''' Formulario base auxiliar
    ''' </summary>
    Private _formAux As FormBase

#End Region

#Region "Properties"

    ''' <summary>
    ''' Permite habilitar o deshabilitar el boton de digitalización
    ''' </summary>
    Public WriteOnly Property SetFormId As Integer
        Set(value As Integer)
            _idForm = value
        End Set
    End Property

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, "")
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, "")
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, Botones.Aceptar, Base.Icono.Errores)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Permite habilitar o deshabilitar el boton de digitalización
    ''' </summary>
    Public WriteOnly Property SetEnableButtonDigitalization As Boolean
        Set(value As Boolean)
            INDDigitalizationSmb.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Permite habilitar o deshabilitar el boton de ver mas
    ''' </summary>
    Public WriteOnly Property SetEnableButtonMoreAttach As Boolean
        Set(value As Boolean)
            INDMoreAttachSmb.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Permite habilitar o deshabilitar el boton adjuntar
    ''' </summary>
    Public WriteOnly Property SetEnableButtonAttach As Boolean
        Set(value As Boolean)
            INDAttachSmb.Enabled = value
        End Set
    End Property

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento al dar click sobre el item de digitalización
    ''' </summary>
    Private Sub BarBtnDigitalization_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnDocumentos.ItemClick
        '/***************/'
    End Sub

    ''' <summary>
    ''' Eliminar archivos al cerrar formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Sub CloseForm(sender As Object, e As EventArgs)
        If Me._tempFileCollection IsNot Nothing AndAlso Me._tempFileCollection.Count > 0 Then
            Me._tempFileCollection.Delete()
        End If
        presenter.UpdateReportPath(Indigo.UserIndigoId, Indigo.IndigoContainerId, Indigo.IndigoOperatingUnitId)
    End Sub

    ''' <summary>
    ''' Evento para abrir el formulario de digitalización
    ''' </summary>
    Private Sub INDDigitalizationSmb_Click(sender As Object, e As EventArgs) Handles INDDigitalizationSmb.Click
        If _frmDigitalization Is Nothing Then
            _frmDigitalization = New FrmDigitalization(Me._idForm, Me._idEntity)
            If Me._formAux IsNot Nothing Then
                _frmDigitalization.TopForm = _formAux
            Else
                _frmDigitalization.TopForm = CType(Me.ParentForm, Presentation.Controls.FormBase)
            End If
            frmTrans = New FrmTransparent(_frmDigitalization, False)
            Me.INDDocumentalSystemPcc.HidePopup()
            frmTrans.ShowDialog()
            If _frmDigitalization.WindowState = FormWindowState.Normal AndAlso _frmDigitalization.DialogResult = DialogResult.Cancel Then
                _frmDigitalization = Nothing
                frmTrans = Nothing
            End If
        Else
            If _frmDigitalization.WindowState = FormWindowState.Minimized Then
                _frmDigitalization.WindowState = FormWindowState.Normal
                frmTrans = New FrmTransparent(_frmDigitalization, False)
                Me.INDDocumentalSystemPcc.HidePopup()
                frmTrans.ShowDialog()
                If _frmDigitalization.WindowState = FormWindowState.Normal AndAlso _frmDigitalization.DialogResult = DialogResult.Cancel Then
                    _frmDigitalization = Nothing
                    frmTrans = Nothing
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' Evento para adjuntar un archivo y guardar en la base de datos
    ''' </summary>
    Private Sub INDAttachSmb_Click(sender As Object, e As EventArgs) Handles INDAttachSmb.Click
        fileOpener = GetOpenFileDialog()
        Me.INDDocumentalSystemPcc.HidePopup()
        If (fileOpener.ShowDialog() = DialogResult.OK) Then
            '_tempFileCollection.AddFile(fileOpener.FileName, keepFile:=False)
            Dim documentInfo As DocumentInfo = Nothing
            Dim fileInfo = New IO.FileInfo(fileOpener.FileName)

            Dim streamRead As System.IO.Stream = System.IO.File.Open(fileOpener.FileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
            'System.IO.File.OpenRead(fileOpener.FileName)
            documentInfo = New DocumentInfo With {.DocumentStream = streamRead, .DocumentStreamLength = fileInfo.Length, .FileName = fileInfo.Name, .SessionInf = Nothing}
            Dim frmMetaData As New FrmMetaData(Me._idForm, Me._idEntity, documentInfo)
            If Me._formAux IsNot Nothing Then
                frmMetaData.TopForm = Me._formAux
            Else
                frmMetaData.TopForm = CType(Me.ParentForm, Presentation.Controls.FormBase)
            End If
            frmMetaData.FilePath = fileOpener.FileName

            frmTrans = New FrmTransparent(frmMetaData, False)
            Me.INDDocumentalSystemPcc.HidePopup()

            If frmTrans.ShowDialog(Me) = DialogResult.OK Then
                If Me._idForm = 2176 Then 'Si el tag del form es del modal de eventos de dashboard autorizaciones
                    If Me._listDocuments Is Nothing Then
                        Me._listDocuments = New List(Of DocumentsStore)
                    End If

                    Dim infoDocumentSotre As New DocumentsStore
                    With infoDocumentSotre
                        .Name = fileInfo.Name
                        .Type = fileInfo.Extension
                        .Content = System.IO.File.ReadAllBytes(fileOpener.FileName)
                        .MetaData = frmMetaData._descriptionString
                        .AttachDate = Date.Now
                    End With

                    Me._listDocuments.Add(infoDocumentSotre)
                    Me.INDDocumentsGc.DataSource = Me._listDocuments
                    Me.Mensaje(EeventViewerImages.Informacion) = "Documento agregado correctamente"
                    TotalItemsGrid(Me._listDocuments.Count)
                Else 'Sigue el proceso normal
                    LoadDocuments()
                    Me.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesGuardado, Eform.Comunes)
                End If
            Else
                '_tempFileCollection.Delete()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de datos adjuntos
    ''' </summary>
    Private Sub INDMoreAttachSmb_Click(sender As Object, e As EventArgs) Handles INDMoreAttachSmb.Click
        Dim frmAttach As New FrmAttach(Me._idForm, Me._idEntity, Me._entityName, Me._listDocuments)
        If Me._formAux IsNot Nothing Then
            frmAttach.TopForm = Me._formAux
        Else
            frmAttach.TopForm = CType(Me.ParentForm, Presentation.Controls.FormBase)
        End If
        frmTrans = New FrmTransparent(frmAttach, False)
        Me.INDDocumentalSystemPcc.HidePopup()
        frmTrans.ShowDialog()
    End Sub

    ''' <summary>
    ''' Evento que al dar doble click se posiciona sobre un documento y permite visualizarlo
    ''' </summary>
    Private Async Sub BandedGridView1_DoubleClick(sender As Object, e As EventArgs) Handles BandedGridView1.DoubleClick
        If Me.PermissionsForm.ContainsKey(PermissionsActionsForm.Abrir) Then
            Me.INDDocumentsGc.Enabled = False
            Dim Document = CType(BandedGridView1.GetRow(BandedGridView1.FocusedRowHandle), DocumentsStore)
            'Dim tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath, System.IO.Path.GetRandomFileName)
            Dim tempFilePath = System.IO.Path.Combine(Window.Utils.TemporalFolder(), System.IO.Path.GetRandomFileName)
            tempFilePath = System.IO.Path.ChangeExtension(tempFilePath, System.IO.Path.GetExtension(Document.Name))

            If _tempFileCollection Is Nothing Then
                _tempFileCollection = New CodeDom.Compiler.TempFileCollection
            End If
            _tempFileCollection.AddFile(tempFilePath, keepFile:=False)

            System.IO.File.Create(tempFilePath).Dispose()

            If Me._idForm = 2178 Then
                Dim result = Await modelDocument.GetAttachmentById(Document.IdEntity.Value)
                If result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing Then
                    File.WriteAllBytes(tempFilePath, result.ObjectEmbbeded.FileAttached)
                End If
            ElseIf Me._idForm = 2176 Then
                File.WriteAllBytes(tempFilePath, Document.Content)
            Else
                Using sqlFileStream = Await modelDocument.getData(Document.Id.ToString)
                    Using localFileStream = New IO.FileStream(tempFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                        sqlFileStream.CopyTo(localFileStream)
                    End Using
                End Using
            End If

            Process.Start(tempFilePath)
            Me.INDDocumentsGc.Enabled = True
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SinPermisoAbrirDocumentos, Eform.CtrBarraBotones)
        End If
    End Sub

    ''' <summary>
    ''' Evento para cambiar el valor de la columna tipo en el gridControl
    ''' </summary>
    Private Sub BandedGridView1_CustomRowCellEdit(sender As Object, e As Views.Grid.CustomRowCellEditEventArgs) Handles BandedGridView1.CustomRowCellEdit
        If e.RowHandle > -1 Then
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim data = CType(view.GetRow(e.RowHandle), DocumentsStore)
            Dim lista = (From document As ImageComboBoxItem In Me.INDImageDocumentsRepositoryIcb.Items Select document.Value).ToList()
            If Not lista.Contains(data.Type) Then
                If Not view.GetRowCellValue(e.RowHandle, Me.TipoDoc) = ".otro" Then
                    view.SetRowCellValue(e.RowHandle, Me.TipoDoc, ".otro")
                End If
            End If
        End If
    End Sub

#End Region

#Region "Methods or Functions"

    ''' <summary>
    ''' Metodo para la carga inicial de los documentos 
    ''' </summary>
    Async Sub LoadDocuments()
        Me.INDLoadingPice.Visible = True

        If Me._idForm = 2178 Then
            Me._listDocuments = Nothing
            Dim result = Await modelDocument.GetAttachmentsByFormAndEntity(Me._idForm, Me._entityName, Me._idEntity, False)
            If result.StateResult Then
                Dim attachments = result.ObjectEmbbeded
                If attachments IsNot Nothing AndAlso attachments.Any Then
                    Me._listDocuments = New List(Of DocumentsStore)
                    For Each attachment In attachments
                        Me._listDocuments.Add(New DocumentsStore With
                        {
                            .IdEntity = attachment.Id,
                            .Name = attachment.Name,
                            .Type = attachment.Extension,
                            .AttachDate = attachment.CreationDate
                        })
                    Next
                End If
            End If
        Else
            Me._listDocuments = Await modelDocument.getDocumentsByIdFormAndIdEntity(Me._idForm, Me._idEntity, False)
        End If

        Dim total = 0
        Dim docFullText As List(Of DocumentsStore) = Nothing
        If Me._listDocuments IsNot Nothing Then
            total = Me._listDocuments.Count
            docFullText = Me._listDocuments.OrderByDescending(Function(x) x.AttachDate).Take(5).ToList
        End If

        If docFullText Is Nothing OrElse docFullText.Count = 0 Then
            Me.INDMoreAttachSmb.Enabled = False
            Me.BarBtnDocumentos.LargeImageIndex = 42
            Me.INDNotDocumentsLbl.Visible = True
            Me.INDNotFileContainerLbl.Visible = False
            Me.INDDocumentsGc.Visible = False
        Else
            Me.INDMoreAttachSmb.Enabled = True
            Me.TotalItemsGrid(total)
            Me.INDNotDocumentsLbl.Visible = False
            Me.INDNotFileContainerLbl.Visible = False
            Me.INDDocumentsGc.Visible = True
            Me.INDDocumentsGc.DataSource = docFullText
        End If

        Me.INDLoadingPice.Visible = False
    End Sub

    Private Sub TotalItemsGrid(_total As Integer)
        Select Case _total
            Case 1
                Me.BarBtnDocumentos.LargeImageIndex = 31
            Case 2
                Me.BarBtnDocumentos.LargeImageIndex = 32
            Case 3
                Me.BarBtnDocumentos.LargeImageIndex = 33
            Case 4
                Me.BarBtnDocumentos.LargeImageIndex = 34
            Case 5
                Me.BarBtnDocumentos.LargeImageIndex = 35
            Case 6
                Me.BarBtnDocumentos.LargeImageIndex = 36
            Case 7
                Me.BarBtnDocumentos.LargeImageIndex = 37
            Case 8
                Me.BarBtnDocumentos.LargeImageIndex = 38
            Case 9
                Me.BarBtnDocumentos.LargeImageIndex = 39
            Case 10
                Me.BarBtnDocumentos.LargeImageIndex = 40
            Case Is > 10
                Me.BarBtnDocumentos.LargeImageIndex = 41
        End Select
    End Sub

    ''' <summary>
    ''' Deshabilita la barra de digitalización
    ''' </summary>
    Public Sub DisableBarDocument()
        OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarBtnDocumentos.LargeImageIndex = 42
        Me._controlRibbonAudit = False
        Me._controlRibbonDocuments = False
        'Me.BarBtnImprimir.Enabled = False
    End Sub

    ''' <summary>
    ''' Evento para mostrar un panel al dar click en el boton atras
    ''' </summary>
    Private Sub INDbackAuditPe_Click(sender As Object, e As EventArgs)
        'Me.INDAuditPanel.Visible = True
    End Sub

    ''' <summary>
    ''' Metodo encargado de habilitar el sistema documental según registro
    ''' </summary>
    Public Async Sub SetDocuments(idEntity As Integer, Optional tagForm As String = "", Optional formAux As FormBase = Nothing, Optional entityName As String = Nothing)
        Me._formAux = formAux
        'Me.INDbackAuditPe.Image = INDImagenesMetroLarges.Images(43)
        'Me.INDAuditPanel.Visible = True
        If tagForm = "" Then
            Me._idForm = CInt(MyBase.FindForm.Tag)
        Else
            Me._idForm = tagForm
        End If
        _entityName = entityName

        If Me.PermissionsForm.ContainsKey(PermissionsActionsForm.Auditoria) Then
            '****AUDITORIA******
            OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
            Me.BarBtnAudit.Enabled = True
            Me.INDAdvancedAuditSmb.Enabled = True
            '*******************
        End If

        OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
        Me.BarBtnDocumentos.Enabled = True
        Me._controlRibbonDocuments = True
        Me._idEntity = idEntity
        Dim flag = 0
        modelDocument = New MDigitalization
        Me.INDLoadingPice.Visible = True
        Try
            If Me._idForm = 2178 Then
                Me.INDAttachSmb.Enabled = False
                Me.INDDigitalizationSmb.Enabled = False
                Me.BandedGridColumn1.HideControl(True)
                Me.INDMoreAttachSmb.Enabled = False

                LoadDocuments()
            Else
                Dim fileCByForm As List(Of FileContainersForm) = Nothing
                If Not String.IsNullOrEmpty(Indigo.DocumentalContainer) Then
                    fileCByForm = Await modelDocument.listFileContainersByIdForm(Me._idForm)
                End If
                If fileCByForm IsNot Nothing AndAlso fileCByForm.Count > 0 Then
                    LoadDocuments()
                Else
                    Me.INDAttachSmb.Enabled = False
                    Me.INDDigitalizationSmb.Enabled = False
                    Me.INDMoreAttachSmb.Enabled = False
                    Me.BarBtnDocumentos.LargeImageIndex = 42
                    Me.INDLoadingPice.Visible = False
                    Me.INDNotFileContainerLbl.Visible = True
                    Me.INDNotDocumentsLbl.Visible = False
                    Me.INDDocumentsGc.Visible = False
                    flag = 1
                End If
            End If
        Catch ex As Exception
            Me.INDAttachSmb.Enabled = False
            Me.INDDigitalizationSmb.Enabled = False
            Me.INDMoreAttachSmb.Enabled = False
            Me.BarBtnDocumentos.LargeImageIndex = 42
            Me.INDLoadingPice.Visible = False
            Me.INDNotFileContainerLbl.Visible = True
            Me.INDNotDocumentsLbl.Visible = False
            Me.INDDocumentsGc.Visible = False
            flag = 1
        End Try
        If flag = 0 Then
            Me.INDAttachSmb.Enabled = True
            Me.INDDigitalizationSmb.Enabled = True
            If Not Me.PermissionsForm.ContainsKey(PermissionsActionsForm.Adjuntar) Then
                Me.INDAttachSmb.Enabled = False
            End If
            If Not Me.PermissionsForm.ContainsKey(PermissionsActionsForm.Digitalizar) Then
                Me.INDDigitalizationSmb.Enabled = False
            End If
        End If
        Me.INDNotFileContainerLbl.Text = obtenerRecurso(Eresources.SinArchivador, Eform.CtrBarraBotones)
        Me.INDNotDocumentsLbl.Text = obtenerRecurso(Eresources.SinDocumentos, Eform.CtrBarraBotones)
        If formAux IsNot Nothing Then
            AddHandler formAux.FormClosing, AddressOf CloseForm
        Else
            If Me.ParentForm IsNot Nothing Then
                AddHandler Me.ParentForm.FormClosing, AddressOf CloseForm
            End If
        End If

        If _tempFileCollection IsNot Nothing Then
            If _tempFileCollection.Count > 0 Then
                _tempFileCollection.Delete()
            End If
        Else
            _tempFileCollection = New System.CodeDom.Compiler.TempFileCollection
        End If

    End Sub

    ''' <summary>
    ''' Función para crear el objeto openFileDialog
    ''' </summary>
    Private Function GetOpenFileDialog() As OpenFileDialog
        fileOpener.CheckPathExists = True
        fileOpener.CheckFileExists = True
        ' fileOpener.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.GIF)|*.bmp;*.jpg;*.jpeg;*.GIF|" + _
        '  "PNG files (*.png)|*.png|text files (*.text)|*.txt|doc files (*.doc)|*.doc|docx files (*.docx)|*.docx|pdf files (*.pdf)|*.pdf"
        fileOpener.Multiselect = False
        fileOpener.AddExtension = True
        fileOpener.ValidateNames = True
        'fileOpener.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        fileOpener.InitialDirectory = Window.Utils.DeskTopFolder()
        Return fileOpener
    End Function

#End Region

#End Region

#Region "Others"

    ''' <summary>
    ''' Cambia el texto de un boton de la barra
    ''' </summary>
    ''' <param name="button"></param>
    ''' <param name="imageIndex"></param>
    ''' <remarks></remarks>
    Public Sub ChangeButtonLargeImageIndex(ByVal button As EbuttonsWithoutPermission, ByVal imageIndex As Integer)
        Select Case button
            Case EbuttonsWithoutPermission.GenerateFile
                BarBtnGenerateFile.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Imprimir
                BarBtnImprimir.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Eliminar
                BarBtnEliminar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Actualizar
                BarBtnActualizar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Guardar
                BarBtnGuardar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Liquidar
                BarBtnLiquidar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.ConsultarLiquidacion
                BarBtnConsultLiquidation.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.ConfirmarTodos
                BarBtnConfirmAll.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Procesar
                BarBtnProcess.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Confirmar
                BarBtnConfirmar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Suspender
                BarBtnSuspend.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Reactivar
                BarBtnReactivate.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.ControlBiometrico
                BarBtnBiometrico.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Nuevo
                BarBtnNuevo.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Copiar
                BarBtnCopiar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Cortar
                BarBtnCortar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Pegar
                BarBtnPegar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Buscar
                BarBtnBuscar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Anular
                BarBtnAnular.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Deshacer
                BarBtnDeshacer.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Permisos
                BarBtnRefrescar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Favoritos
                BarBtnFavoritos.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.RedesSociales
                BarSubRedes.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.GestionDocumental
                BarBtnDocumentos.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.ComunicacionesUnificadas
                BarSubComunicacion.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.OtrosPlugins
                BarSubOtros.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Auditoria
                BarBtnAudit.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Activar
                BarBtnActive.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Terminar
                BarBtnTerminar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.RefreshGrid
                BarbtnRefreshGrid.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.AddGrid
                BarBtnRejillaAdicionar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.EditGrid
                BarBtnRejillaModificar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.DeleteGrid
                BarBtnRejillaEliminar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Desconfirmar
                BarBtnDesconfirmar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.GuardarConfirmar
                BarBtnGuardarConfirmar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Jerarquia
                BarBtnJerarquia.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.ImportarInformacion
                BarBtnImportar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.ActualizarConfirmar
                BarBtnActualizarConfirmar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.EntregaManual
                BarBtnEntregaManual.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Validar
                BarBtnValidar.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.DeshacerTodo
                BarBtnDeshacerTodo.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.PesoPaciente
                BarBtnPesoPaciente.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.JournalVourcherHomologation
                BarBtnHomologation.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.LoadPurcharseOrder
                BarBtnLoadPurchaseOrder.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.ModoNavegacion
                BarBtnModoNavegacion.ImageOptions.LargeImageIndex = imageIndex
            Case EbuttonsWithoutPermission.Conciliar
                BarBtnReconciled.ImageOptions.LargeImageIndex = imageIndex
        End Select
    End Sub


    ''' <summary>
    '''  Cambia la imagen de un boton
    '''  enviando como parametro la imagen
    ''' </summary>
    ''' <param name="button"> EbuttonsWithoutPermission.Validar </param>
    ''' <param name="image"> My.Resources.Mmenu_de_acciones</param>
    Public Sub ChangeButtonLargeImageWithImage(ByVal button As EbuttonsWithoutPermission, ByVal image As Image)
        Dim buttonMapping As New Dictionary(Of EbuttonsWithoutPermission, DevExpress.XtraBars.BarButtonItem)
        buttonMapping.Add(EbuttonsWithoutPermission.Validar, BarBtnValidar)
        buttonMapping.Add(EbuttonsWithoutPermission.Procesar, BarBtnProcess)
        buttonMapping.Add(EbuttonsWithoutPermission.LoadPurcharseOrder, BarBtnLoadPurchaseOrder)
        buttonMapping.Add(EbuttonsWithoutPermission.Auditoria, BarBtnAudit)
        ' Agrega más botones aquí según sea necesario
        If buttonMapping.ContainsKey(button) Then
            buttonMapping(button).ImageOptions.Reset()
            buttonMapping(button).ImageOptions.LargeImage = image
        Else
            Throw New ArgumentException("El botón especificado no es válido.")
        End If
    End Sub

    ''' <summary>
    ''' Copia la imagen SVG de un botón a otro para que tengan exactamente el mismo ícono
    ''' </summary>
    ''' <param name="sourceButton">Botón origen del cual copiar la imagen</param>
    ''' <param name="targetButton">Botón destino al cual aplicar la imagen</param>
    Public Sub CopySvgImageFromButton(ByVal sourceButton As EbuttonsWithoutPermission, ByVal targetButton As EbuttonsWithoutPermission)
        Dim buttonMapping As New Dictionary(Of EbuttonsWithoutPermission, DevExpress.XtraBars.BarButtonItem)
        buttonMapping.Add(EbuttonsWithoutPermission.Imprimir, BarBtnImprimir)
        buttonMapping.Add(EbuttonsWithoutPermission.LoadPurcharseOrder, BarBtnLoadPurchaseOrder)
        ' Agregar más botones aquí según sea necesario

        If buttonMapping.ContainsKey(sourceButton) AndAlso buttonMapping.ContainsKey(targetButton) Then

            buttonMapping(targetButton).ImageOptions.SvgImage = buttonMapping(sourceButton).ImageOptions.SvgImage
            buttonMapping(targetButton).ImageOptions.SvgImageColorizationMode = buttonMapping(sourceButton).ImageOptions.SvgImageColorizationMode
        Else
            Throw New ArgumentException("El botón especificado no es válido.")
        End If
    End Sub

    ''' <summary>
    ''' Cambia el texto de un boton de la barra
    ''' </summary>
    ''' <param name="button"></param>
    ''' <param name="newButtonName"></param>
    ''' <remarks></remarks>
    Public Sub ChangeButtonName(ByVal button As EbuttonsWithoutPermission, ByVal newButtonName As String)
        Select Case button
            Case EbuttonsWithoutPermission.GenerateFile
                BarBtnGenerateFile.Caption = newButtonName
            Case EbuttonsWithoutPermission.Imprimir
                BarBtnImprimir.Caption = newButtonName
            Case EbuttonsWithoutPermission.Eliminar
                BarBtnEliminar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Actualizar
                BarBtnActualizar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Guardar
                BarBtnGuardar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Liquidar
                BarBtnLiquidar.Caption = newButtonName
            Case EbuttonsWithoutPermission.ConsultarLiquidacion
                BarBtnConsultLiquidation.Caption = newButtonName
            Case EbuttonsWithoutPermission.ConfirmarTodos
                BarBtnConfirmAll.Caption = newButtonName
            Case EbuttonsWithoutPermission.Procesar
                BarBtnProcess.Caption = newButtonName
            Case EbuttonsWithoutPermission.Confirmar
                BarBtnConfirmar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Suspender
                BarBtnSuspend.Caption = newButtonName
            Case EbuttonsWithoutPermission.Reactivar
                BarBtnReactivate.Caption = newButtonName
            Case EbuttonsWithoutPermission.ControlBiometrico
                BarBtnBiometrico.Caption = newButtonName
            Case EbuttonsWithoutPermission.Nuevo
                BarBtnNuevo.Caption = newButtonName
            Case EbuttonsWithoutPermission.Copiar
                BarBtnCopiar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Cortar
                BarBtnCortar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Pegar
                BarBtnPegar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Buscar
                BarBtnBuscar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Anular
                BarBtnAnular.Caption = newButtonName
            Case EbuttonsWithoutPermission.Deshacer
                BarBtnDeshacer.Caption = newButtonName
            Case EbuttonsWithoutPermission.Permisos
                BarBtnRefrescar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Favoritos
                BarBtnFavoritos.Caption = newButtonName
            Case EbuttonsWithoutPermission.RedesSociales
                BarSubRedes.Caption = newButtonName
            Case EbuttonsWithoutPermission.GestionDocumental
                BarBtnDocumentos.Caption = newButtonName
            Case EbuttonsWithoutPermission.ComunicacionesUnificadas
                BarSubComunicacion.Caption = newButtonName
            Case EbuttonsWithoutPermission.OtrosPlugins
                BarSubOtros.Caption = newButtonName
            Case EbuttonsWithoutPermission.Auditoria
                BarBtnAudit.Caption = newButtonName
            Case EbuttonsWithoutPermission.Activar
                BarBtnActive.Caption = newButtonName
            Case EbuttonsWithoutPermission.Terminar
                BarBtnTerminar.Caption = newButtonName
            Case EbuttonsWithoutPermission.RefreshGrid
                BarbtnRefreshGrid.Caption = newButtonName
            Case EbuttonsWithoutPermission.AddGrid
                BarBtnRejillaAdicionar.Caption = newButtonName
            Case EbuttonsWithoutPermission.EditGrid
                BarBtnRejillaModificar.Caption = newButtonName
            Case EbuttonsWithoutPermission.DeleteGrid
                BarBtnRejillaEliminar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Desconfirmar
                BarBtnDesconfirmar.Caption = newButtonName
            Case EbuttonsWithoutPermission.GuardarConfirmar
                BarBtnGuardarConfirmar.Caption = newButtonName
            Case EbuttonsWithoutPermission.Jerarquia
                BarBtnJerarquia.Caption = newButtonName
            Case EbuttonsWithoutPermission.ImportarInformacion
                BarBtnImportar.Caption = newButtonName
            Case EbuttonsWithoutPermission.ActualizarConfirmar
                BarBtnActualizarConfirmar.Caption = newButtonName
            Case EbuttonsWithoutPermission.EntregaManual
                BarBtnEntregaManual.Caption = newButtonName
            Case EbuttonsWithoutPermission.Validar
                BarBtnValidar.Caption = newButtonName
            Case EbuttonsWithoutPermission.DeshacerTodo
                BarBtnDeshacerTodo.Caption = newButtonName
            Case EbuttonsWithoutPermission.PesoPaciente
                BarBtnPesoPaciente.Caption = newButtonName
            Case EbuttonsWithoutPermission.JournalVourcherHomologation
                BarBtnHomologation.Caption = newButtonName
            Case EbuttonsWithoutPermission.LoadPurcharseOrder
                BarBtnLoadPurchaseOrder.Caption = newButtonName
            Case EbuttonsWithoutPermission.ModoNavegacion
                BarBtnModoNavegacion.Caption = newButtonName
            Case EbuttonsWithoutPermission.Conciliar
                BarBtnReconciled.Caption = newButtonName
        End Select
    End Sub

    ''' <summary>
    ''' Establece el LayoutControl que va a ejecutar las acciones, sino se especifica se tomara
    ''' el primer layoutcontrol que sea encontrado por el metodo
    ''' </summary>
    ''' <value> BarraBotones1.EstablecerLayoutControl = LayoutControl1</value>
    Public WriteOnly Property EstablecerLayoutControl As LayoutControl
        Set(ByVal value As LayoutControl)
            LayoutControl = value
        End Set
    End Property

    Private Sub INDBtnRenombrar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnRenombrar.ItemClick
        If e.Link Is Nothing Then
            Exit Sub
        End If
        LayoutControlCambio()
        LayoutControl.RenameSelectedItem()
    End Sub

    Private Sub INDBtnRestaurarDiseno_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnRestaurarDiseno.ItemClick
        LayoutControl.RestoreDefaultLayout()
    End Sub

    Private Sub INDBtnEsconderItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnEsconderItem.ItemClick
        If e.Link Is Nothing Then
            Exit Sub
        End If
        If item Is Nothing Then
            Exit Sub
        End If
        LayoutControl.HideItem(item)
        LayoutControlCambio()
    End Sub

    Private Sub INDBtnEsconderTexto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnEsconderTexto.ItemClick
        If e.Link Is Nothing Then
            Exit Sub
        End If
        If item Is Nothing Then
            Exit Sub
        End If
        If e.Item.Caption = "Ocultar Texto" Then
            item.TextVisible = False
            INDBtnEsconderTexto.Caption = "Mostrar Texto"
            INDBtnEsconderTexto.ImageIndex = 0
            EsconderTexto()
        Else
            item.TextVisible = True
            INDBtnEsconderTexto.Caption = "Ocultar Texto"
            INDBtnEsconderTexto.ImageIndex = 11
            EsconderTexto()
        End If
    End Sub

    Private Sub EsconderTexto()
        'Cambio en el boton de esconder texto.
        If TypeOf Layoutsender Is LayoutControlGroup Then
            Return
        End If

        If DirectCast(Layoutsender, LayoutControlItem).TextVisible = True Then
            INDBtnEsconderTexto.Caption = "Ocultar Texto"
            INDBtnEsconderTexto.ImageIndex = 11
            INDBtnRenombrar.Visibility = BarItemVisibility.Always
            'mostrar el boton de direccion de texto
            INDBtnPosicionTexto.Visibility = BarItemVisibility.Always
        Else
            INDBtnEsconderTexto.Caption = "Mostrar Texto"
            INDBtnEsconderTexto.ImageIndex = 0
            INDBtnRenombrar.Visibility = BarItemVisibility.Never
            'ocultar el boton de direccion de texto
            INDBtnPosicionTexto.Visibility = BarItemVisibility.Never

        End If


        LayoutControlCambio()
    End Sub

    ''' <summary>
    ''' Metodo para establecer cambios en layout
    ''' </summary>
    Private Sub LayoutControlCambio()
        If LayoutControl.IsModified = True Then
            INDBtnDeshacerAccion.Enabled = True
            INDBtnGuardarXml.Enabled = True
            INDBtnRestaurarDiseno.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Método que valida si al consultar un registro el usuario no tiene permisos para la unidad operativa,
    ''' se asigna en la barra botones la que viene por defecto del inicio de sesión
    ''' </summary>
    Public Sub ReassignOperatingUnit()
        INDBeiOperatingUnits.Visibility = BarItemVisibility.Always
        VisibleAndCollapseRibbonPage()
        If SessionValues.Instance.IndigoOperatingUnitId <> 0 AndAlso SessionValues.Instance.ListOperatingUnitPermission.Where(Function(x) x.id = SessionValues.Instance.IndigoOperatingUnitId).FirstOrDefault IsNot Nothing Then
            Me.OperatingUnitValue = SessionValues.Instance.IndigoOperatingUnitId
            presenter.UpdateReportPath(Indigo.UserIndigoId, Indigo.IndigoContainerId, OperatingUnitValue)
        End If
    End Sub

    ''' <summary>
    ''' Actualiza la ruta de reportes personalizados utilizando datos de sesión del usuario.
    ''' </summary>
    Public Sub UpdateUserReportPathFromSession()
        presenter.UpdateReportPath(Indigo.UserIndigoId, Indigo.IndigoContainerId, OperatingUnitValue)
    End Sub
    ''' <summary>
    ''' Metodo para habilitar controles de customización
    ''' </summary>
    Private Sub HabilitarControlesCustomizacion()
        INDBtnDeshacerAccion.Enabled = False
        INDBtnRehacerAccion.Enabled = False
        INDBtnRestaurarDiseno.Enabled = False
        If LayoutControl.IsModified = False Then
            INDBtnGuardarXml.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para Crear el panel de customizacion en tiempo de diseño.
    ''' </summary>
    Private Sub CrearLayoutControl()

        formularioContenedor = MyBase.FindForm


        'creamos el layoutcontrol con sus caracteristicas
        LayoutControlAgregado = New LayoutControl
        LayoutControlAgregado.Name = "LayoutControlAgregado"
        LayoutControlAgregado.Location = New System.Drawing.Point(LayoutControl.Location.X, LayoutControl.Location.Y + 10)
        LayoutControlAgregado.AllowCustomization = False
        LayoutControlAgregado.Size = New System.Drawing.Size(464, 71)
        LayoutControlAgregado.MaximumSize = New System.Drawing.Size(464, 71)
        LayoutControlAgregado.MinimumSize = New System.Drawing.Size(464, 71)
        LayoutControlAgregado.Text = "LayoutControl1"


        'creamos los items que van a ir dentro del listview
        Dim ListViewItem1 As ListViewItem = New ListViewItem("Espacio Vacio", 0)
        Dim ListViewItem2 As ListViewItem = New ListViewItem("Etiqueta", 1)
        Dim ListViewItem3 As ListViewItem = New ListViewItem("Separador", 2)
        Dim ListViewItem4 As ListViewItem = New ListViewItem("Divisor", 3)

        'creamos un listimage con las respectivas imagenes
        Dim ImageListAgregado As New ImageList

        ImageListAgregado.TransparentColor = System.Drawing.Color.Transparent
        ImageListAgregado.ImageSize = New Size(16, 16)
        ImageListAgregado.Images.Add(My.Resources.Empty_16x16)
        ImageListAgregado.Images.Add(My.Resources.Label_16x16)
        ImageListAgregado.Images.Add(My.Resources.Separador_16x16)
        ImageListAgregado.Images.Add(My.Resources.Sccroll_16x16)

        'creamos un listimage con las respectivas imagenes
        Dim ImageListBasura As New ImageList

        ImageListBasura.TransparentColor = System.Drawing.Color.Transparent
        ImageListBasura.ImageSize = New Size(32, 32)
        ImageListBasura.Images.Add(My.Resources.Print_32x32)
        ImageListBasura.Images.Add(My.Resources.Undo_32x32)

        'creamos el listview con sus imagenes
        listViewAgregado = New ListView
        listViewAgregado.Name = "ListViewAgregado"
        listViewAgregado.BorderStyle = System.Windows.Forms.BorderStyle.None
        listViewAgregado.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4})
        listViewAgregado.Location = New System.Drawing.Point(12, 12)
        listViewAgregado.MultiSelect = False
        listViewAgregado.Scrollable = False
        listViewAgregado.Size = New System.Drawing.Size(216, 67)
        listViewAgregado.SmallImageList = ImageListAgregado
        listViewAgregado.UseCompatibleStateImageBehavior = False
        listViewAgregado.View = System.Windows.Forms.View.SmallIcon
        AddHandler listViewAgregado.GiveFeedback, AddressOf EventoGiveFeedBackListViewAgregado
        AddHandler listViewAgregado.MouseDown, AddressOf EventoMouseDownListViewAgregado
        AddHandler listViewAgregado.MouseMove, AddressOf EventoMouseMoveListViewAgregado

        ' Creamos un nuevo layout item.
        Dim itemListView As New LayoutControlItem()
        itemListView.Name = "itemListView"
        itemListView.Parent = LayoutControlAgregado.Root
        itemListView.Control = listViewAgregado
        itemListView.CustomizationFormText = "LayoutControlItem1"
        itemListView.Location = New System.Drawing.Point(0, 0)
        itemListView.Size = New System.Drawing.Size(220, 71)
        itemListView.Text = "LayoutControlItem1"
        itemListView.TextSize = New System.Drawing.Size(0, 0)
        itemListView.TextToControlDistance = 0
        itemListView.TextVisible = False
        itemListView.TrimClientAreaToControl = True

        'creamos un label que contiene la imagen de la basura
        Dim Label As New Label
        Label.Name = "Label"
        Label.AllowDrop = True
        Label.ImageIndex = 0
        Label.ImageList = ImageListBasura
        Label.Location = New System.Drawing.Point(261, 12)
        Label.Size = New System.Drawing.Size(42, 67)

        ' Creamos un nuevo layout item.
        Dim itemLabel As New LayoutControlItem()
        itemLabel.Name = "itemLabel"
        itemLabel.Parent = LayoutControlAgregado.Root
        itemLabel.Control = Label
        itemLabel.CustomizationFormText = "LayoutControlItem2"
        itemLabel.Location = New System.Drawing.Point(220, 0)
        itemLabel.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 180, 0, 0)
        itemLabel.Size = New System.Drawing.Size(224, 71)
        itemLabel.Text = "LayoutControlItem2"
        itemLabel.TextSize = New System.Drawing.Size(0, 0)
        itemLabel.TextToControlDistance = 0
        itemLabel.TextVisible = False
        itemLabel.Move(itemListView, DevExpress.XtraLayout.Utils.InsertType.Right)


        formularioContenedor.Controls.Item(0).Controls.Add(LayoutControlAgregado)

        LayoutControl.Location = New Point(LayoutControl.Location.X, LayoutControl.Location.Y + 70)

    End Sub

    ''' <summary>
    ''' Lanza el evento de minimización de la barra
    ''' </summary>
    Private Sub OnMinimizeToolBars(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ToolBarsMinimized(sender, e)
    End Sub

    ''' <summary>
    ''' Lanza el evento de maximización de la barra
    ''' </summary>
    Private Sub OnMaximizeToolBars(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ToolBarsMaximized(sender, e)
    End Sub

    ''' <summary>
    ''' Metodo para minimizar la barra
    ''' </summary>
    Public Sub Minimizar(minimized As Boolean)

        Me.Minimized = minimized
        If Me.Minimized = True Then
            RibbonControl.Minimized = True

            Me.Size = New Size(Me.Width, 28)
            Me.OnMinimizeToolBars(Me, New EventArgs())
            Dim listGroups As New List(Of DevExpress.XtraBars.Ribbon.RibbonPageGroup)
            For Each item As DevExpress.XtraBars.Ribbon.RibbonPage In RibbonControl.Pages
                For Each item1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup In item.Groups
                    If item1.Enabled = False AndAlso item1.Visible = True AndAlso item1.Tag = "Disabled" Then
                        listGroups.Add(item1)
                    End If
                Next
            Next
            Me._stateAdditionalControls = Me.AdditionalControlPanel.Visible
            Me.AdditionalControlPanel.Visible = False
            RibbonControl.ShowPageHeadersMode = Ribbon.ShowPageHeadersMode.Default

            For Each BarItems As DevExpress.XtraBars.Ribbon.RibbonPageGroup In listGroups
                BarItems.Enabled = False
                For Each LinkItem As DevExpress.XtraBars.BarItemLink In BarItems.ItemLinks
                    LinkItem.Item.Enabled = False
                Next
            Next

        Else
            RibbonControl.Minimized = False
            Me.Size = New Size(Me.Width, 130)
            Me.OnMaximizeToolBars(Me, New EventArgs())
            RibbonControl.ShowPageHeadersMode = Ribbon.ShowPageHeadersMode.Default
            RibbonPageMinimizar.Visible = True
            BarBtnMinimizar.Visibility = BarItemVisibility.Always
            Me.AdditionalControlPanel.Visible = Me._stateAdditionalControls
        End If
    End Sub

    ''' <summary>
    ''' Establece la posicion del item seleccionado del layoutcontrol.
    ''' Ejemplo -> BarraBotones1.EstablecerPosicionItem(e.X, e.Y)
    ''' Utilizar en el evento LayoutControl1_MouseClick.
    ''' </summary>
    ''' <param name="x">Posicion x.</param>
    ''' <param name="y">Posicion y.</param>
    Public Sub EstablecerPosicionItem(ByVal x As Integer, ByVal y As Integer)

        If LayoutControl Is Nothing Then
            Exit Sub
        End If
        item = LayoutControl.CalcHitInfo(New Point(x, y)).Item

    End Sub

    ''' <summary>
    ''' Establece parametros layout
    ''' </summary>
    Public Sub EstablecerParametrosLayout(ByVal sender As Object, ByVal e As EventArgs)
        Layoutsender = sender
        LayoutEventos = e

        If TypeOf Layoutsender Is LayoutControlItem Then

            EsconderTexto()

            'si el item esta bloqueado para no permitir esconderse entonces...
            If DirectCast(Layoutsender, LayoutControlItem).AllowHide = False Then
                INDBtnEsconderItem.Visibility = BarItemVisibility.Never
            Else
                INDBtnEsconderItem.Visibility = BarItemVisibility.Always
            End If

        End If

        If TypeOf Layoutsender Is EmptySpaceItem Then


        End If

        If TypeOf Layoutsender Is LayoutControlGroup Then
            INDBtnEsconderItem.Visibility = BarItemVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Prepara la barra cuando no hay permisos
    ''' </summary>
    Private Sub NoPermissions()
        'Se muestran
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
        'Se ocultan
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConfirmarTodos) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Liquidar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RefreshGrid) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.AddGrid) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeleteGrid) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EditGrid) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Jerarquia) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True
    End Sub
    Private _banderacargando As Boolean
    ''' <summary>
    ''' Prepara la barra dependiendo de la acción seleccionada
    ''' </summary>
    ''' <param name="action">Acción seleccionada para preparar la barra</param>
    Public Sub PrepareToolbar(ByVal action As eAction)
        _banderacargando = True
        'Se ocultan

        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True

        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConfirmarTodos) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Terminar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Reactivar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True

        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.JournalVourcherHomologation) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Liquidar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Jerarquia) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ModoNavegacion) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Favoritos) = True
        BarBtnCustomizar.Visibility = BarItemVisibility.Always
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.LoadPurcharseOrder) = True

        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cortar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Copiar) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Pegar) = True

        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True

        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True

        RibbonPageNavigationRecords.Visible = False
        'BarBtnFirst.Visibility = BarItemVisibility.Never
        'BarBtnBack.Visibility = BarItemVisibility.Never
        'BarBtnFilter.Visibility = BarItemVisibility.Never
        'BarBtnNext.Visibility = BarItemVisibility.Never
        'BarBtnLast.Visibility = BarItemVisibility.Never

        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RefreshGrid) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.AddGrid) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeleteGrid) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EditGrid) = True

        'estado, unidad operativa
        'INDBarEditItemStatus.Visibility = BarItemVisibility.Never
        'INDBeiOperatingUnits.Visibility = BarItemVisibility.Never
        'Me.INDLbMessage.Visible = False
        'Control
        'Me.ControlPanel.Visible = False
        'Minimizar
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.OtrosPlugins) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RedesSociales) = True
        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ComunicacionesUnificadas) = True


        Select Case action
            Case eAction.OnlyNavigationControl
            Case eAction.New
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyNew
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.Update
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.ActivarInactivar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUpdate
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.ActivarInactivar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.Delete
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Eliminar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.ActivarInactivar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyDelete
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Eliminar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.Process
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.None
                'Se ocultan
                'Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Case eAction.OnlyProcess
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUndo
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUndoAndAudit
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyButtonProcess
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Procesar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUndoAndPrint
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyFind
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Consultar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.NewAndFind
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Consultar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.Save
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlySave
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.SaveOrDelete
                Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
            Case eAction.OnlySaveWithoutUndoAndFind
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyLiquidate
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar

                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Liquidar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = False
                Else
                    Me.NoPermissions()
                End If

            Case eAction.OnlySuspend
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Suspender)) Then

                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = False
                Else
                    Me.NoPermissions()
                End If


            Case eAction.OnlyConfirmLiquidate
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                    End If
                End If
            Case eAction.OnlyAllConfirmLiquidate
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConfirmarTodos) = False
                    End If
                End If

            Case eAction.UpdateAndProcess
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.UpdateAndProcessWithPrint
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.ImprimirReporte)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.SaveAndProcess
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                Else
                    Me.NoPermissions()
                End If
            Case eAction.UpdateOrDelete
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Eliminar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                    End If

                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.ActivarInactivar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                    End If
                Else
                    Me.NoPermissions()
                End If

            Case eAction.OnlyUpdateOrDelete
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Eliminar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                    End If

                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.ActivarInactivar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyGenerateFile
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GenerateFile)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                End If
            Case eAction.GenerateFileWithAuditAndDocumental
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GenerateFile)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                End If
            Case eAction.OnlyActionsGrid
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Se muestran
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.RefreshGrid)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RefreshGrid) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RefreshGrid) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.AddGrid)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.AddGrid) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.AddGrid) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.DeleteGrid)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeleteGrid) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeleteGrid) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.EditGrid)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EditGrid) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EditGrid) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlySaveConfirm
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlySaveConfirmWithoutUndoAndFind
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Guardar)) Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyConfirmAnnular
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Anular)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyDisconfirmAnnular
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Desconfirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Anular)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUpdateDeleteConfirm
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Eliminar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUpdateConfirmAnnular
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Anular)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUpdateConfirmIntegratedAnnular
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar

                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False



                    'Si tiene permiso a actualizar y a confirmar le activo el valor
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = Not (Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) _
                                                                                                        AndAlso Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)))

                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = Not (Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) _
                                                                                                AndAlso Not (Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar))))

                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                    Else
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Anular)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUpdateConfirmIntegratedAnnularWithoutUndoAndFind
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    Me.RibbonPagEform.Visible = True
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Actualizar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                        'Si tiene permiso a actualizar y a confirmar le activo el valor
                        If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Confirmar)) Then
                            Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = False
                        Else
                            Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = True
                        End If
                    Else
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                    End If
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Anular)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                    Else
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyDisconfirm
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Desconfirmar)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
            Case eAction.OnlyUndoAnnular
                If Me.PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.Count > 0 Then
                    'Aqui se oculta el Page completo para evitar el espacio en balnco
                    'que deja el boton de customisar
                    'Se muestran
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
                    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                    If Me.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Anular)) Then
                        Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                        'Else
                        '    Me.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    End If
                Else
                    Me.NoPermissions()
                End If
        End Select
        _banderacargando = False
        VisibleAndCollapseRibbonPage()
    End Sub

    ''' <summary>
    ''' Localiza el control panel al final de la barra de botones
    ''' </summary>
    Private Sub LocateControlPanel()
        Dim viewInfo As RibbonPanelViewInfo = RibbonControl.ViewInfo.Panel

        If RibbonControl.ViewInfo.Panel.Groups.Count > 0 Then
            Dim vRight As Integer = viewInfo.Groups(RibbonControl.ViewInfo.Panel.Groups.Count - 1).Bounds.Right
            ControlPanel.Height = viewInfo.Bounds.Height - 5
            ControlPanel.Location = New Point(vRight, viewInfo.Bounds.Y + 2)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se elimina la personalización del formulario
    ''' </summary>
    Private Sub INDbtnRestablecerLayout_Click(sender As Object, e As EventArgs) Handles INDbtnRestablecerLayout.Click
        If TypeOf Me.formularioContenedor Is FormBase Then
            CType(Me.formularioContenedor, FormBase).RestoreLayoutForm()
            If Me.LayoutControl IsNot Nothing Then
                Me.LayoutControl.RestoreDefaultLayout()
            End If
            RaiseEvent ClickDeshacer()
        End If
        RaiseEvent ClicRestablecerLayout()
    End Sub

    ''' <summary>
    ''' Evento para customizar valores en la rejilla de auditoria basica
    ''' </summary>
    Private Sub INDAuditBasicGv_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs)
        If e.Column.FieldName = "Operation" Then
            If e.Value IsNot Nothing Then
                Select Case e.Value.ToString.Trim()
                    Case "1"
                        e.DisplayText = "Crear"
                    Case "2"
                        e.DisplayText = "Modificar"
                    Case "3"
                        e.DisplayText = "Confirmar"
                    Case "4"
                        e.DisplayText = "Anular"
                    Case "5"
                        e.DisplayText = "Eliminar"
                    Case "18"
                        e.DisplayText = "Desconfirmar"
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento click para poder abrir el formulario de auditoria avanzada
    ''' </summary>
    Private Sub INDAdvancedAuditSmb_Click(sender As Object, e As EventArgs) Handles INDAdvancedAuditSmb.Click
        Dim frmMetaData As New FrmAdvancedAudit(Me._entityName, Me._idForm, Me._idEntity)
        frmTrans = New FrmTransparent(frmMetaData, False)
        Me.INDAuditPcc.HidePopup()
        If frmTrans.ShowDialog(Me) = DialogResult.OK Then
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando se inhabilita la barra de botones para indicar en el centro de notificaciones que proceso se esta ejecutando
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrBarraBotones_EnabledChanged(sender As Object, e As EventArgs) Handles MyBase.EnabledChanged
        If Me.ParentForm Is Nothing Then
            Return
        End If
        If ProcessName = String.Empty Then
            Base.BaseClass.ProcessAsync(Me.ParentForm.Text, ResourceManager.GetString("Search", Me.GetType()), ResourceManager.GetString("Running", "MDIPrincipal"))
        Else
            Base.BaseClass.ProcessAsync(Me.ParentForm.Text, ProcessName, ResourceManager.GetString("Running", "MDIPrincipal"))
        End If
        If Me.Enabled = True Then
            ProcessName = String.Empty
        End If
    End Sub

    Public Sub New()
        InitializeComponent()

        ControlPanel.Parent = RibbonControl

        presenter = New PBarraBotones(Me)
        Me.ApplyStyleSkin(UserLookAndFeel.Default.ActiveSkinName)
        AddHandler UserLookAndFeel.Default.StyleChanged, AddressOf UserLookAndFeel_StyleChanged
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor de la unidad operativa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleOperatingUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDRigleOperatingUnits.EditValueChanged
        Dim _gridLookUpEdit = CType(sender, GridLookUpEdit)
        INDBeiOperatingUnits.EditValue = _gridLookUpEdit.EditValue
        presenter.UpdateReportPath(Indigo.UserIndigoId, Indigo.IndigoContainerId, INDBeiOperatingUnits.EditValue)
        LoadReportsAndDefinitions()
        RaiseEvent ChangueOperatingUnit(Me.GetOperatingUnitSelected(_gridLookUpEdit.EditValue))
    End Sub

    Private Sub BarBtnGuardarConfirmar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnGuardarConfirmar.ItemClick
        RaiseEvent Click_GuardarConfirmar()
    End Sub

    Private Sub BarBtnDesconfirmar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnDesconfirmar.ItemClick
        RaiseEvent Click_Desconfirmar()
    End Sub

    Private Sub BarBtnTerminar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnTerminar.ItemClick
        RaiseEvent Click_Terminar()
    End Sub

    ''' <summary>
    ''' Evento al dar click en jerarquia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarBtnJerarquia_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnJerarquia.ItemClick
        RaiseEvent Click_Jerarquia()
    End Sub

    ''' <summary>
    ''' Evento click en importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarBtnImportar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnImportar.ItemClick
        RaiseEvent Click_ImportarInformacion()
    End Sub

    ''' <summary>
    ''' Evento click en actualizar y confirmar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarBtnActualizarConfirmar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnActualizarConfirmar.ItemClick
        RaiseEvent Click_ActualizarConfirmar()
    End Sub

#End Region

    Private Sub BarBtnEntregaManual_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnEntregaManual.ItemClick
        RaiseEvent Click_EntregaManual()
    End Sub

    Private Sub BarBtnValidar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnValidar.ItemClick
        RaiseEvent Click_Validar()
    End Sub

    Private Sub BarBtnDeshacerTodo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnDeshacerTodo.ItemClick
        RaiseEvent Click_DeshacerTodo()
    End Sub

    Private Sub BarBtnLoadPurchaseOrder_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnLoadPurchaseOrder.ItemClick
        RaiseEvent Click_LoadPurcharseOrder()
    End Sub

    Private Async Sub RepositoryItemHyperLinkEdit1_Click(sender As Object, e As EventArgs) Handles INDrptBeDelete.Click
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.ParentForm.Text, Botones.SiNo) = DialogResult.Yes Then
            '.GetRow(BandedGridView1.FocusedRowHandle)
            Dim documentToDelete As DocumentsStore = CType(BandedGridView1.GetFocusedRow(), DocumentsStore)
            If _idForm = 2176 Then 'Si se esta eliminando desde el adjuntar documentos desde el form modal de eventos del dashboard autorizaciones
                _listDocuments.Remove(documentToDelete)
                Me.INDDocumentsGc.DataSource = Nothing
                Me.INDDocumentsGc.DataSource = Me._listDocuments
                Me.INDDocumentsGc.RefreshDataSource()
                If Me._listDocuments.Count = 0 Then
                    Me.BarBtnDocumentos.LargeImageIndex = 42
                Else
                    TotalItemsGrid(Me._listDocuments.Count)
                End If
            Else 'Sigue el proceso normal
                Me.Enabled = False
                'Base.BaseClass.ProcessAsync(Me.ParentForm.Text, ResourceManager.GetString("Search", Me.GetType()), ResourceManager.GetString("Running", "MDIPrincipal"))
                Dim result = Await modelDocument.DeleteDocument(documentToDelete) '.getDocumentsByIdFormAndIdEntity(Me._idForm, Me._idEntity, False)
                'Dim result1 = Await modelDocument.DeleteIndexedDocumentByIdEntity(documentToDelete.IdForm & "_" & documentToDelete.Id.ToString())
                Me.Enabled = True
                If result.StateResult Then
                    LoadDocuments()
                Else
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                End If
            End If
        End If
    End Sub

    Private Sub RbtnActions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RbtnActions.SelectedIndexChanged
        If Me.RbtnActions.EditValue IsNot Nothing AndAlso Me.RbtnActions.EditValue.ToString().Equals("V") Then
            Me._printProfile.IsPrint = False
        Else
            Me._printProfile.IsPrint = True
        End If
        Window.Utils.CreatePrintProfile(SessionValues.Instance.UserIndigo, Me._idFormToReport, Me._printProfile)
    End Sub

    Private Sub ChkPrintOnConfirm_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChkPrintOnConfirm.CheckedChanged
        Me._printProfile.OnConfirm = Me.ChkPrintOnConfirm.Checked
        Window.Utils.CreatePrintProfile(SessionValues.Instance.UserIndigo, Me._idFormToReport, Me._printProfile)
    End Sub

    Private Sub ChkPrintOnCancel_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrintOnCancel.CheckedChanged
        Me._printProfile.OnCancel = Me.ChkPrintOnCancel.Checked
        Window.Utils.CreatePrintProfile(SessionValues.Instance.UserIndigo, Me._idFormToReport, Me._printProfile)
    End Sub

    Private Async Sub INDPopupccEdadPaciente_CloseUp(sender As Object, e As EventArgs) Handles INDPopupccEdadPaciente.CloseUp
        Using mode As New MBarraBotones()
            If INDtxtPeso.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(INDtxtPeso.EditValue) AndAlso INDtxtPeso.EditValue <> _weightPatient Then
                If _weightMeasure = "Kg" Then
                    _weightPatient = INDtxtPeso.EditValue * 1000
                Else
                    _weightPatient = INDtxtPeso.EditValue
                End If
                WeightPatient(_weightPatient, _CodePatient, _DatePatient)
            End If
            Await mode.SaveWeightPatient(_weightPatient, _CodePatient)
        End Using
    End Sub


    Private Sub INDBtnHomologation_Click(sender As Object, e As EventArgs) Handles INDBtnHomologation.Click
        INDLciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        _listLegalBookId.Add(_legalBookId)
        RaiseEvent HomologationJournalVoucher(_legalBookId)
    End Sub

    Private Sub INDBtnSave_Click(sender As Object, e As EventArgs) Handles INDBtnSave.Click
        INDLciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Dim journalVoucher = (From a In _FilterDataSourceJournalVoucher Where a.LegalBookId = _legalBookId Select a).FirstOrDefault()
        RaiseEvent SaveHomologationJournalVoucher(journalVoucher)
    End Sub

    ''' <summary>
    ''' Aqui se controla el estilo asignado al control cuando el skin cambia
    ''' </summary>
    Private Sub UserLookAndFeel_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    ''' <summary>
    ''' Aplica el estilo al control dependiendo del Skin seleccionado
    ''' </summary>
    ''' <param name="skinName">Nombre del Skin seleccionado</param>
    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Me.RibbonControl.Images = ThemeResourceManager.GetImageCollectionToToolbarByTheme(UserLookAndFeel.Default.ActiveSkinName, False)
        Me.RibbonControl.LargeImages = ThemeResourceManager.GetImageCollectionToToolbarByTheme(UserLookAndFeel.Default.ActiveSkinName, True)
        Me.INDDigitalizationSmb.Image = ThemeResourceManager.GetScanImageButtonImageByTheme(UserLookAndFeel.Default.ActiveSkinName)
        Me.INDAttachSmb.Image = ThemeResourceManager.GetAttachImageButtonImageByTheme(UserLookAndFeel.Default.ActiveSkinName)
        Me.RibbonControl.Invalidate()
    End Sub

    ''' <summary>
    ''' Panel adicional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AdditionalControlPanel_ControlAdded(sender As Object, e As ControlEventArgs) Handles AdditionalControlPanel.ControlAdded
        Me.AdditionalControlPanel.Visible = True
    End Sub

    Private Sub BarBtnRadicateResponse_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarBtnRadicateResponse.ItemClick
        RaiseEvent Click_RadicateResponse()
    End Sub


End Class

''' <summary>
''' Lugar desde donde se manda a imprimir el reporte
''' </summary>
Public Enum PrintReportAction
    ''' <summary>
    ''' Al momento de crear un nuevo documento
    ''' </summary>
    Create
    ''' <summary>
    ''' Al momento de actualizar un documento
    ''' </summary>
    Update
    ''' <summary>
    ''' Al momento de confirmar un documento
    ''' </summary>
    Confirm
    ''' <summary>
    ''' Al momento de anular un documento
    ''' </summary>
    Cancel
    ''' <summary>
    ''' Al momento de dar click en el botón imprimir, para impresión directa
    ''' </summary>
    DirectPrinting
    ''' <summary>
    ''' Al momento de dar click en el botón imprimir, para visualizar el reporte
    ''' </summary>
    ViewPrinting
    ''' <summary>
    ''' Imprimie o visualiza el reporte según el perfil de impresión definido por el usuario
    ''' </summary>
    PrintProfile
    ''' <summary>
    ''' No tiene efecto
    ''' </summary>
    None
End Enum

''' <summary>
''' Encapsula los datos de una definición de reporte
''' </summary>
Public Class ReportDefinition

    Public Property Name As String
    Public Property FormName As String
    Public Property ReportName As String
    Public Property Path As String
    Public Property ObjVieReport As VieReport
    Public Property IsDefault As Boolean

End Class

''' <summary>
''' Encapsula el estado de un registro
''' </summary>
<Serializable()>
Public Class StatusRecord
    Implements ISerializable

    ''' <summary>
    ''' Obtiene o asigna el nombre del estado
    ''' </summary>
    ''' <value>Nombre del estado</value>
    ''' <returns>El nombre del estado</returns>
    Public Property StatusName As String
    ''' <summary>
    ''' Obtiene o asigna el valor del estado
    ''' </summary>
    ''' <value>Valor del estado</value>
    ''' <returns>El valor del estado</returns>
    Public Property StatusValue As Object
    ''' <summary>
    ''' Obtiene o asigna el color del estado
    ''' </summary>
    ''' <value>Color del estado</value>
    ''' <returns>El color del estado</returns>
    Public Property StatusColor As Color

    Public Sub New()
        Me.StatusName = ""
        Me.StatusValue = ""
        StatusColor = Color.White
    End Sub

    Public Sub New(info As SerializationInfo, context As StreamingContext)
        If info Is Nothing Then
            Throw New ArgumentNullException("information")
        End If
        Me.StatusName = info.GetString("StatusName")
        Me.StatusValue = info.GetValue("StatusValue", GetType(Object))
        Me.StatusColor = Convert.ChangeType(info.GetValue("StatusColor", GetType(Color)), GetType(Color))
    End Sub

    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        If info Is Nothing Then
            Throw New ArgumentNullException("info")
        End If
        info.AddValue("StatusName", Me.StatusName.Trim())
        info.AddValue("StatusValue", Me.StatusValue)
        info.AddValue("StatusColor", Me.StatusColor.ToString())
    End Sub
End Class

Public Class LoadReportsAndDefinitionsEventArgs
    Inherits EventArgs

    Public Property ListReportsAndDefinitions As List(Of VieReport)

End Class
#Region "Enums"

''' <summary>
''' Esta enumeración permite establecer el icono que acompañara
''' al xtraMessage en la barra botones
''' </summary>
Public Enum ImagesXtraLabel

    ''' <summary>
    ''' Información
    ''' </summary>
    Info = 0
    ''' <summary>
    ''' Advertencia
    ''' </summary>
    Warning = 1

End Enum

''' <summary>
''' Esta enumeracion se refiere al tipo de accion a realizar en la barra
''' Nuevo - Agrega un nuevo boton en el page
''' Adicionar - Agrega un link a un boton
''' </summary>
Public Enum eMenu
    Nuevo = 1
End Enum

''' <summary>
''' Lista de acciones para las cuales se
''' puede preparar la barra dependiendo de los permisos
''' </summary>
Public Enum eAction

    ''' <summary>
    ''' Ningún boton
    ''' </summary>
    None
    ''' <summary>
    ''' Habilita solo el botón de búsqueda en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyFind
    ''' <summary>
    ''' Habilita solo el botón de nuevo en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyNew
    ''' <summary>
    ''' Habilita el botón de nuevo y buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    [New]
    ''' <summary>
    ''' Habilita solo el botón de actualizar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyUpdate
    ''' <summary>
    ''' Habilita el botón de actualizar y buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    Update
    ''' <summary>
    ''' Habilita solo el botón de eliminar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyDelete
    ''' <summary>
    ''' Habilita el botón de eliminar y buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    Delete
    ''' <summary>
    ''' Habilita solo el botón de procesos en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyProcess
    ''' <summary>
    ''' Habilita solo el botón de procesar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyButtonProcess
    ''' <summary>
    ''' Habilita el page de procesos y botón buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    Process
    ''' <summary>
    ''' Habilita solo el botón de deshacer en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyUndo
    ''' <summary>
    ''' habilita deshacer y auditoria y gestion documental
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUndoAndAudit
    ''' <summary>
    ''' Habilita el botón de actualizar y procesos en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    UpdateOrProcess
    ''' <summary>
    ''' Habilita el botón nuevo y búscar en la
    ''' barra dependiendo de los permisos
    ''' </summary>
    NewAndFind
    ''' <summary>
    ''' Habilita solo el botón de guardar en la
    ''' barra dependiendo de los permisos
    ''' </summary>
    OnlySave
    ''' <summary>
    ''' Solo boton de guardar sin deshacer y buscar
    ''' </summary>
    OnlySaveWithoutUndoAndFind
    ''' <summary>
    ''' Habilita el page de guardar y botón buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    Save
    ''' <summary>
    ''' Habilita opción de guardar o eliminar
    ''' </summary>
    SaveOrDelete
    ''' <summary>
    ''' Habilita el botón actualizar y eliminar
    ''' en la barra dependiendo de los permisos
    ''' </summary>
    UpdateOrDelete
    ''' <summary>
    ''' Habilita el botón actualizar y procesos
    ''' en la barra dependiendo de los permisos
    ''' </summary>
    UpdateAndProcess
    ''' <summary>
    ''' Habilita el botón actualizar y procesos al igual que el de imprimir
    ''' en la barra dependiendo de los permisos
    ''' </summary>
    UpdateAndProcessWithPrint
    ''' <summary>
    ''' Habilita el botón guardar y procesos
    ''' en la barra dependiendo de los permisos
    ''' </summary>
    SaveAndProcess

    ''' <summary>
    ''' Habilita el boton de liquidar, con formulario de procesos que lo requieran
    ''' </summary>
    ''' <remarks></remarks>
    OnlyLiquidate
    ''' <summary>
    ''' Habilita el boton de liquidar, con formulario de procesos que lo requieran
    ''' </summary>
    ''' <remarks></remarks>
    OnlyConfirmLiquidate
    ''' <summary>
    ''' Habilita los botones de confirmar y confirmarTodos
    ''' </summary>
    ''' <remarks></remarks>
    OnlyAllConfirmLiquidate
    ''' <summary>
    ''' Habilita el boton de imprimir y deshacer 
    ''' </summary>
    OnlyUndoAndPrint
    ''' <summary>
    ''' Habilita el botón actualizar y eliminar
    ''' en la barra dependiendo de los permisos sin mostrar el boton de busqueda
    ''' </summary>
    OnlyUpdateOrDelete

    ''' <summary>
    ''' Oculta el boton de auditoria
    ''' </summary>
    ''' <remarks></remarks>
    OnlyHideAudit

    ''' <summary>
    ''' Oculta el boton de gestion Documental
    ''' </summary>
    ''' <remarks></remarks>
    OnlyHideDocumental

    ''' <summary>
    ''' Oculta los botones de accion dejando visible el boton de suspender
    ''' </summary>
    ''' <remarks></remarks>
    OnlySuspend

    ''' <summary>
    ''' Muestra el boton de generar archivo.
    ''' </summary>
    ''' <remarks></remarks>
    OnlyGenerateFile
    ''' <summary>
    ''' Muestra el boton de generar archivo con auditoria y gestión documental
    ''' </summary>
    ''' <remarks></remarks>
    GenerateFileWithAuditAndDocumental

    ''' <summary>
    ''' Muestra los botones de rejillas si tiene permisos
    ''' </summary>
    ''' <remarks></remarks>
    OnlyActionsGrid

    ''' <summary>
    ''' Muestra los botones de Guardar y guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    OnlySaveConfirm

    ''' <summary>
    ''' Muestra los botones de Guardar y guardar y confirmar, sin deshacer y buscar
    ''' </summary>
    ''' <remarks></remarks>
    OnlySaveConfirmWithoutUndoAndFind

    ''' <summary>
    ''' Muestra los botones de confirmar y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyConfirmAnnular

    ''' <summary>
    ''' Muestra los botones de desconfirmar y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyDisconfirmAnnular

    ''' <summary>
    ''' Muestra los botones de actualizar eliminar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUpdateDeleteConfirm

    ''' <summary>
    ''' Muestra los botones de actualizar confirmar y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUpdateConfirmAnnular

    ''' <summary>
    ''' Muestra los botones de actualizar, (Actualizar y confirmar) y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUpdateConfirmIntegratedAnnular

    ''' <summary>
    ''' muestra  el boton de desconfirmar 
    ''' </summary>
    ''' <remarks></remarks>
    OnlyDisconfirm

    ''' <summary>
    ''' muestra solo el boton de deshacer y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUndoAnnular

    ''' <summary>
    ''' Solo muestra el control de navegación
    ''' </summary>
    OnlyNavigationControl

    ''' <summary>
    ''' Muestra los botones de actualizar, (Actualizar y confirmar) y anular, sin deshacer y buscar
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUpdateConfirmIntegratedAnnularWithoutUndoAndFind

End Enum

#End Region

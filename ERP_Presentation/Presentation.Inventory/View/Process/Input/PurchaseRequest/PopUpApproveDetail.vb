'***********************************************************************
' Assembly         : Presentacion.
' Author           : Hector Rodriguez Rubiano
' Created          : 06-05-2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Drawing
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class PopUpApproveDetail


#Region "Builder"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_Detaill"></param>
    Public Sub New(_OnlyRead As Boolean, _Detaill As PurchaseRequestDetail)
        Me.Detail = _Detaill
        InitializeComponent()
        OnlyRead = _OnlyRead
        If OnlyRead Then INDSmbAccept.Text = "Salir"
    End Sub

#End Region

#Region "Globals"
    ''' <summary>
    ''' Objeto que establece el OtherServiceItemo a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim Detail As PurchaseRequestDetail

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = ""

#End Region

#Region "Properties"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property StatusApprove As Integer
        Get
            Return INDSleApprove.EditValue
        End Get
        Set(value As Integer)
            INDSleApprove.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Especifica si se aprueba o no el detalle de la solicitud de compra de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _listStatusApprove As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListStatusApprove As List(Of Tuple(Of Integer, String))
        Get
            If _listStatusApprove Is Nothing Then
                _listStatusApprove = New List(Of Tuple(Of Integer, String))
                _listStatusApprove.Add(New Tuple(Of Integer, String)(1, "Si"))
                _listStatusApprove.Add(New Tuple(Of Integer, String)(2, "No"))
            End If
            Return _listStatusApprove
        End Get
    End Property
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Observation As String
        Get
            Return INDMmeObservation.EditValue
        End Get
        Set(ByVal value As String)
            INDMmeObservation.EditValue = value
        End Set
    End Property

    Public Property OnlyRead As Boolean
#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el formulario para adicion de OtherServiceItemos
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Limpia los controles para agregar un OtherServiceItemo nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDSleApprove.EditValue = Nothing
        INDMmeObservation.Text = String.Empty
        INDSleApprove.Focus()
    End Sub



#End Region

#Region "Functions"
    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' 
    ''' </summary>
    Private Sub ValidateAcceptDetail()
        Try
            INDSmbAccept.Enabled = False
            '********************* Valida los Campos esten diligenciados ****************'
            If ValidateControls() = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                INDSmbAccept.Enabled = True
                INDSleApprove.Focus()
                Exit Sub
            End If

            '********************* Defino el evento de retorno ****************'
            Dim args As AddPurchaseRequestDetailEventArgs
            '********************* Verifico si el item es para actualziar o agregar ****************'

            If Detail.Id > 0 Then
                Detail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            Detail.Status = StatusApprove
            Detail.ApproveDate = Date.Now
            Detail.ApproveUser = Me.indigo.UserIndigo
            Detail.ApproveObservation = INDMmeObservation.Text
            Detail.StatusName = IIf(Detail.Status = 0, "Registrado", IIf(Detail.Status = 1, "Aprobado", "Rechazado"))

            args = New AddPurchaseRequestDetailEventArgs
            args.ItemPurchaseRequestDetail = Detail
            args.ListPurchaseRequestDetail = Nothing

            CerrarFormulario(args)
        Catch ex As Exception
            INDSmbAccept.Enabled = True
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="args"></param>
    Private Sub CerrarFormulario(args As AddPurchaseRequestDetailEventArgs)
        Detail = Nothing
        CleanControls()
        RaiseEvent ApproveItem(Nothing, args)
        INDSmbAccept.Enabled = True
        INDSleApprove.Focus()
        Me.Close()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub LoadControls()
        INDSleApprove.EditValue = IIf(Detail.Status = 0, 2, Detail.Status) 
        INDMmeObservation.EditValue = Detail.ApproveObservation
        If OnlyRead Then EnabledControls()
    End Sub

     Private Sub EnabledControls()
        INDSleApprove.Enabled = false
        INDMmeObservation.Enabled = false
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupApproveDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        CleanControls()
        INDSleApprove.Properties.DataSource = ListStatusApprove
        If Detail IsNot Nothing Then
        Me.LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupApproveDetail_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
            INDSleApprove.Focus()
    End Sub

    ''' <summary>
    ''' Agrega el OtherServiceItemo al listado de la solicitud de compra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSmbAccept_Click(sender As Object, e As EventArgs) Handles INDSmbAccept.Click
        If Me.OnlyRead Then
            Me.Close
        Else
            ValidateAcceptDetail()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        'CleanControls()
        CerrarFormulario(Nothing)
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupApproveDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupApproveDetail_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (Not Me.OnlyRead) AndAlso INDSleApprove.EditValue IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Handlers"
    ''' <summary>
    ''' evento publico para agregar un OtherServiceItemo a la solicitud de compra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ApproveItem(sender As Object, e As AddPurchaseRequestDetailEventArgs)

#End Region
End Class
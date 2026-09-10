'***********************************************************************
' Assembly         : Presentacion.
' Author           : Hector Rodriguez Rubiano
' Created          : 24-04-2019
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

Public Class PopUpOtherPurchaseRequest
    Implements IPopUpOtherPurchaseRequest

#Region "Builder"

    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <param name="_ReadOnly"></param>
    ''' <param name="_Detaill"></param>
    ''' <param name="_listOtherServiceItems"></param>
    Public Sub New(_ReadOnly As Boolean, _Detaill As PurchaseRequestDetail, _listOtherServiceItems As TrackableCollection(Of PurchaseRequestDetail))
        InitializeComponent()

        If _Detaill IsNot Nothing Then
            INDSmbAdd.Text = ResourceManager.GetString("Edit")
            editModeForm = True
            Detail = _Detaill
        End If
        If _listOtherServiceItems IsNot Nothing Then
            For Each item In _listOtherServiceItems
                ListOtherServiceItem.Add(item)
            Next
        End If
        OnlyRead = _ReadOnly
        If OnlyRead Then INDSmbAdd.Text = "Salir"
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' Obtiene o establece el listado de los detalles agregados
    ''' </summary>
    ''' <remarks></remarks>
    Private ListOtherServiceItem As TrackableCollection(Of PurchaseRequestDetail) = New TrackableCollection(Of PurchaseRequestDetail)

    ''' <summary>
    ''' Objeto que establece el OtherServiceItemo a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim Detail As PurchaseRequestDetail

    ''' <summary>
    ''' Define si el formulario esta editando o no
    ''' </summary>
    ''' <remarks></remarks>
    Dim editModeForm As Boolean = False

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = ""

    ''' <summary>
    ''' Define si el form es solo para visualizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim OnlyRead As Boolean
#End Region

#Region "Properties"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property UnitId As Integer Implements IPopUpOtherPurchaseRequest.UnitId
        Get
            Return INDSleUnit.EditValue
        End Get
        Set(value As Integer)
            INDSleUnit.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ListMeasureUnit As XPInstantFeedbackSource Implements IPopUpOtherPurchaseRequest.ListMeasureUnit
        Get
            Return INDSleUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Nombre As String Implements IPopUpOtherPurchaseRequest.Nombre
        Get
            Return INDTxeName.EditValue
        End Get
        Set(ByVal value As String)
            INDTxeName.EditValue = value
        End Set
    End Property
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
        INDTxeName.EditValue = Nothing
        INDSleUnit.EditValue = Nothing
        INDSpeQuantity.EditValue = 0
        INDMmeDescription.Text = String.Empty
        INDTxeName.Focus()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub EnabledControls()
        INDTxeName.Enabled = False
        INDSleUnit.Enabled = False
        INDSpeQuantity.Enabled = False
        INDMmeDescription.Enabled = False
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
    Private Sub ValidateAddOtherServiceItem()
        Try
            INDSmbAdd.Enabled = False
            '********************* Valida los Campos esten diligenciados ****************'
            If ValidateControls() = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                INDSmbAdd.Enabled = True
                INDTxeName.Focus()
                Exit Sub
            End If
            If INDSpeQuantity.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad debe ser mayor a 0"
                INDSmbAdd.Enabled = True
                Exit Sub
            End If
            '********************* Defino el evento de retorno ****************'
            Dim args As AddPurchaseRequestDetailEventArgs
            '********************* Verifico si el item es para actualziar o agregar ****************'
            If editModeForm Then
                If Detail.Id > 0 Then
                    Detail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                End If
                Detail.OtherRequest = INDTxeName.EditValue
                Detail.MeasurementUnitId = INDSleUnit.EditValue
                Detail.Quantity = INDSpeQuantity.EditValue
                Detail.OutstandingQuantity = INDSpeQuantity.EditValue
                Detail.Description = INDMmeDescription.EditValue
                Detail.DescriptionProduct = Detail.OtherRequest
                Detail.consumptionUnit = INDSleUnit.Text

                args = New AddPurchaseRequestDetailEventArgs
                args.ItemPurchaseRequestDetail = Detail
                args.ListPurchaseRequestDetail = Nothing
            Else
                If ListOtherServiceItem Is Nothing Then
                    ListOtherServiceItem = New TrackableCollection(Of PurchaseRequestDetail)
                End If
                If ListOtherServiceItem.Count <> 0 Then
                    For Each contractOtherServiceItem In ListOtherServiceItem.Where(Function(fa) fa.InventoryProductId Is Nothing AndAlso fa.TrademarkId Is Nothing)
                        If contractOtherServiceItem.OtherRequest = INDTxeName.EditValue Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("DistributionLineDetailExist")
                            INDSmbAdd.Enabled = True
                            CleanControls()
                            Exit Sub
                        End If
                    Next
                End If

                Detail = New PurchaseRequestDetail()

                Detail.OtherRequest = INDTxeName.EditValue
                Detail.MeasurementUnitId = INDSleUnit.EditValue
                Detail.Quantity = INDSpeQuantity.EditValue
                Detail.OutstandingQuantity = INDSpeQuantity.EditValue
                Detail.Description = INDMmeDescription.EditValue
                Detail.DescriptionProduct = Detail.OtherRequest
                Detail.consumptionUnit = INDSleUnit.Text
                Detail.Status = 0
                Detail.StatusName = "Registrado"

                ListOtherServiceItem.Add(Detail)

                args = New AddPurchaseRequestDetailEventArgs
                args.ListPurchaseRequestDetail = ListOtherServiceItem
                args.ItemPurchaseRequestDetail = Detail
            End If

            Detail = Nothing
            CleanControls()
            RaiseEvent AddOtherServiceItem(Nothing, args)
            INDSmbAdd.Enabled = True
            INDTxeName.Focus()
            If editModeForm Then
                Me.Close()
            End If
        Catch ex As Exception
            INDSmbAdd.Enabled = True
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub LoadControls()
        If Me.OnlyRead Then EnabledControls()
        INDTxeName.EditValue = Detail.OtherRequest
        INDSleUnit_QueryPopUp(Nothing, Nothing)
        INDSleUnit.EditValue = Detail.MeasurementUnitId
        INDSpeQuantity.EditValue = Detail.Quantity
        INDMmeDescription.EditValue = Detail.Description
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupOtherServiceItemPurchaseRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        CleanControls()

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
    Private Sub PopupOtherServiceItemsContract_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Not editModeForm Then
            INDTxeName.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Agrega el OtherServiceItemo al listado de la solicitud de compra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSmbAdd_Click(sender As Object, e As EventArgs) Handles INDSmbAdd.Click
        If Me.OnlyRead Then
            Me.Close
        Else
            ValidateAddOtherServiceItem()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUnit.QueryPopUp
        If INDSleUnit.Properties.DataSource Is Nothing Then
            Using presenter = New PPopUpOtherPurchaseRequest(Me)
                presenter.ListMeasureUnit()
            End Using

        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupOtherServiceItemsPurchaseRequest_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupOtherServiceItemsPurchaseRequest_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (Not Me.OnlyRead) AndAlso INDTxeName.EditValue IsNot Nothing Then
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
    Public Event AddOtherServiceItem(sender As Object, e As AddPurchaseRequestDetailEventArgs)

#End Region
End Class
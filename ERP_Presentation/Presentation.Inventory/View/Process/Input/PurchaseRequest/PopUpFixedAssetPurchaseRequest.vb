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

Public Class PopUpFixedAssetPurchaseRequest
    Implements IPopUpFixedAssetPurchaseRequest

#Region "Builder"

    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <param name="_ReadOnly"></param>
    ''' <param name="_Detaill"></param>
    ''' <param name="_listFixedAssetItems"></param>
    Public Sub New(_ReadOnly As Boolean, _Detaill As PurchaseRequestDetail, _listFixedAssetItems As TrackableCollection(Of PurchaseRequestDetail))
        InitializeComponent()

        If _Detaill IsNot Nothing Then
            INDSmbAdd.Text = ResourceManager.GetString("Edit")
            fixedAsset = new FixedAssetItem
            fixedAsset.Id =_Detaill.FixedAssetItemId
            fixedAsset.Code = _Detaill.Code
            fixedAsset.Description = _Detaill.Observation
            editModeForm = True
            Detail = _Detaill
        End If
        If _listFixedAssetItems IsNot Nothing Then
            For Each item In _listFixedAssetItems
                ListFixedAssetItem.Add(item)
            Next
        End If
        OnlyRead = _ReadOnly
        If OnlyRead Then INDSmbAdd.Text = "Salir"
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' Obtiene o establece el FixedAssetItemo seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim fixedAsset As FixedAssetItem

    ''' <summary>
    ''' Obtiene o establece el listado de los detalles agregados
    ''' </summary>
    ''' <remarks></remarks>
    Private ListFixedAssetItem As TrackableCollection(Of PurchaseRequestDetail) = New TrackableCollection(Of PurchaseRequestDetail)

    ''' <summary>
    ''' Objeto que establece el FixedAssetItemo a agregar
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
    Public Property ItemId As Integer Implements IPopUpFixedAssetPurchaseRequest.ItemId
        Get
            Return INDSleFixedAssetItem.EditValue
        End Get
        Set(value As Integer)
            INDSleFixedAssetItem.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemXpo As XPInstantFeedbackSource Implements IPopUpFixedAssetPurchaseRequest.ItemXpo
        Get
            Return INDSleFixedAssetItem.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleFixedAssetItem.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property TrademarkId As Integer Implements IPopUpFixedAssetPurchaseRequest.TrademarkId
        Get
            Return INDSleTrademark.EditValue
        End Get
        Set(value As Integer)
            INDSleTrademark.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property TrademarkXpo As XPInstantFeedbackSource Implements IPopUpFixedAssetPurchaseRequest.TrademarkXpo
        Get
            Return INDSleTrademark.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleTrademark.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el formulario para adicion de FixedAssetItemos
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
    ''' Limpia los controles para agregar un FixedAssetItemo nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        fixedAsset = Nothing
        INDSleFixedAssetItem.EditValue = Nothing
        INDSleTrademark.EditValue = Nothing
        INDTxeModel.Text = String.Empty
        INDSpeQuantity.EditValue = 0
        INDMmeDescription.Text = String.Empty
        INDSleFixedAssetItem.Focus()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub EnabledControls()
        INDSleFixedAssetItem.Enabled = False
        INDSleTrademark.Enabled = False
        INDTxeModel.Enabled = False
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
    Private Sub ValidateAddFixedAssetItem()
        Try
            INDSmbAdd.Enabled = False
            If fixedAsset Is Nothing OrElse fixedAsset.Id <> ItemId Then
                Using presenter As New PPopUpFixedAssetPurchaseRequest(Me)
                    Dim fixedAssetXpo = presenter.GetFixedAssetItemById(ItemId)
                    If fixedAssetXpo IsNot Nothing Then
                        fixedAsset = New FixedAssetItem()
                        fixedAsset.Id = ItemId
                        fixedAsset.Code = fixedAssetXpo.Code
                        fixedAsset.Description = fixedAssetXpo.Description
                        
                    End If
                End Using
            End If

            '********************* Valida los Campos esten diligenciados ****************'
            If ValidateControls() = False OrElse fixedAsset Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                INDSmbAdd.Enabled = True
                INDSleFixedAssetItem.Focus()
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
                    fixedAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                End If
                'Detail.FixedAssetItem = fixedAsset -- no se asigna, al guardar trata de crearlo
                Detail.Code = fixedAsset.Code
                Detail.Observation = fixedAsset.Description
                Detail.FixedAssetItemId = fixedAsset.Id
                Detail.TrademarkId = INDSleTrademark.EditValue
                Detail.Model = INDTxeModel.EditValue
                Detail.Quantity = INDSpeQuantity.EditValue
                Detail.OutstandingQuantity = INDSpeQuantity.EditValue
                Detail.Description = INDMmeDescription.EditValue
                Detail.DescriptionProduct = fixedAsset.Code + " - " + fixedAsset.Description
                Detail.consumptionUnit = INDSleTrademark.Text + " - " + Detail.Model

                args = New AddPurchaseRequestDetailEventArgs
                args.ItemPurchaseRequestDetail = Detail
                args.ListPurchaseRequestDetail = Nothing
            Else
                If ListFixedAssetItem Is Nothing Then
                    ListFixedAssetItem = New TrackableCollection(Of PurchaseRequestDetail)
                End If
                If ListFixedAssetItem.Count <> 0 Then
                    For Each contractFixedAssetItem In ListFixedAssetItem.Where(Function(fa) fa.FixedAssetItemId IsNot Nothing AndAlso fa.FixedAssetItemId.GetValueOrDefault > 0)
                        If contractFixedAssetItem.Code = fixedAsset.Code Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("DistributionLineDetailExist")
                            INDSmbAdd.Enabled = True
                            CleanControls()
                            Exit Sub
                        End If
                    Next
                End If

                Detail = New PurchaseRequestDetail()

                'Detail.FixedAssetItem = fixedAsset
                Detail.Code = fixedAsset.Code
                Detail.Observation = fixedAsset.Description
                Detail.FixedAssetItemId = fixedAsset.Id
                Detail.TrademarkId = INDSleTrademark.EditValue
                Detail.Model = INDTxeModel.EditValue
                Detail.Quantity = INDSpeQuantity.EditValue
                Detail.OutstandingQuantity = INDSpeQuantity.EditValue
                Detail.Description = INDMmeDescription.EditValue
                Detail.DescriptionProduct = fixedAsset.Code + " - " + fixedAsset.Description
                Detail.consumptionUnit = INDSleTrademark.Text + " - " + Detail.Model
                Detail.Status = 0
                Detail.StatusName = "Registrado"

                ListFixedAssetItem.Add(Detail)

                args = New AddPurchaseRequestDetailEventArgs
                args.ListPurchaseRequestDetail = ListFixedAssetItem
                args.ItemPurchaseRequestDetail = Detail
            End If

            Detail = Nothing
            CleanControls()
            RaiseEvent AddFixedAssetItem(Nothing, args)
            INDSmbAdd.Enabled = True
            INDSleFixedAssetItem.Focus()
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
        INDSleFixedAssetItem_QueryPopUp(Nothing, Nothing)
        ItemId = Detail.FixedAssetItemId
        INDSleFixedAssetItem.EditValue = Detail.FixedAssetItemId
        INDsleTrademark_QueryPopUp(Nothing, Nothing)
        INDSleTrademark.EditValue = Detail.TrademarkId
        INDTxeModel.EditValue = Detail.Model
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
    Private Sub PopupFixedAssetItemPurchaseRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    Private Sub PopupFixedAssetItemsContract_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If fixedAsset Is Nothing Then
            INDSleFixedAssetItem.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Agrega el FixedAssetItemo al listado de la solicitud de compra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSmbAdd_Click(sender As Object, e As EventArgs) Handles INDSmbAdd.Click
        If Me.OnlyRead Then
            Me.Close
        Else
            ValidateAddFixedAssetItem()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFixedAssetItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFixedAssetItem.QueryPopUp
        If INDSleFixedAssetItem.Properties.DataSource Is Nothing Then
            Using presenter = New PPopUpFixedAssetPurchaseRequest(Me)
                presenter.InitializeItem()
            End Using

        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleTrademark_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTrademark.QueryPopUp
        If INDSleTrademark.Properties.DataSource Is Nothing Then
            Using presenter = New PPopUpFixedAssetPurchaseRequest(Me)
                presenter.InitializeTrademark()
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
    Private Sub PopupFixedAssetItemsPurchaseRequest_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupFixedAssetItemsPurchaseRequest_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (Not Me.OnlyRead) AndAlso fixedAsset IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Handlers"
    ''' <summary>
    ''' evento publico para agregar un FixedAssetItemo a la solicitud de compra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddFixedAssetItem(sender As Object, e As AddPurchaseRequestDetailEventArgs)
#End Region
End Class
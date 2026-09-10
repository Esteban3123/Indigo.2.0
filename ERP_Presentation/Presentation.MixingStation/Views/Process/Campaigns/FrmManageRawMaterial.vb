'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 02/09/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmManageRawMaterial

#Region "Variables"
    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Obtiene los permisos del usuario para el formulario principal
    ''' </summary>
    Public PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private model As MCampaign

    ''' <summary>
    ''' String de ids que representan a los detalles agrupados, son los ids de los pacientes
    ''' </summary>
    Public RequestMixingStationDetailId As Integer

#End Region

#Region "Properties"

    Private _quantity As Integer
    ''' <summary>
    ''' Entidad xpo de la campaña
    ''' </summary>
    Public Property Quantity As Integer
        Get
            Return _quantity
        End Get
        Set(value As Integer)
            _quantity = value
        End Set
    End Property

    ''' <summary>
    ''' Id's de la Solicitud del Producto (Paquete)
    ''' </summary>
    Public _productPackageIds As List(Of Integer)
    Public Property productPackageIds As List(Of Integer)
        Get
            Return _productPackageIds
        End Get
        Set(value As List(Of Integer))
            _productPackageIds = value
        End Set
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' Inyecta los datos al datasource de la rejilla
    ''' </summary>
    Public Property ListProductDetailItems As List(Of ManageRawMaterialModel)
        Get
            Return INDgcDetail.DataSource
        End Get
        Set(value As List(Of ManageRawMaterialModel))
            INDgcDetail.DataSource = value
            INDgcDetail.RefreshDataSource()
        End Set
    End Property
#End Region

#Region "Event"
    ''' <summary>
    ''' Evento de acción completada
    ''' </summary>
    Public Event ActionCompleted()
#End Region

#Region "Methods and Functions"
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Async Function InitFormAsync() As Task
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        Await GetProductDetails()
    End Function

    ''' <summary>
    ''' Guardo la Materia Prima Gestionada
    ''' </summary>
    Private Async Function SaveCampaignRawMaterial() As Task
        If CType(INDgcDetail.DataSource, List(Of ManageRawMaterialModel)) Is Nothing OrElse CType(INDgcDetail.DataSource, List(Of ManageRawMaterialModel)).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay detalles para gestionar la Materia Prima"
            Exit Function
        End If

        Dim errors As New StringBuilder
        If _productPackageIds.Count = 0 Then
            errors.AppendLine("Debe Seleccionar un Paquete")
        End If

        If RequestMixingStationDetailId = Nothing Then
            errors.AppendLine("Solicitud no Encontrada")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Function
        End If
        Await Guardar(ListProductDetailItems, _productPackageIds, RequestMixingStationDetailId)
    End Function
#End Region

#Region "Handlers"

#Region "Load"
    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Function FrmManageRawMaterial_LoadAsync(sender As Object, e As EventArgs) As Task Handles MyBase.Load
        Await InitFormAsync()
    End Function
#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmManageRawMaterial_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se Dispara al Cambiar la Cantidad utilizada de los Detalles del Medicamento 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptSeQuantityDetails_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantityDetails.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If

        Dim CampaignRawMaterialTmp = INDviewDetail.GetFocusedObject(Of ManageRawMaterialModel)
        Dim subView = DirectCast(DirectCast(DirectCast(sender, Control).Parent, DevExpress.XtraGrid.GridControl).FocusedView, GridView)
        Dim obj = DirectCast(subView.GetFocusedRow(), ProductTotalDelivered)

        If CampaignRawMaterialTmp.UsedQuantity Is Nothing Then
            CampaignRawMaterialTmp.UsedQuantity = CDec(e.OldValue)
        End If

        'Cantidad Individual
        obj.UsedQuantity = CDec(Val(e.NewValue))
        obj.CampaignBalanceQuantity = obj.CampaignQuantity - obj.UsedQuantity

        'Cantidad General
        CampaignRawMaterialTmp.UsedQuantity = CampaignRawMaterialTmp.CampaignDetailValidation.Sum(Function(m) m.UsedQuantity)
        CampaignRawMaterialTmp.CampaignBalanceQuantity = CampaignRawMaterialTmp.CampaignQuantity - CampaignRawMaterialTmp.UsedQuantity
        CampaignRawMaterialTmp.PendingQuantity = CampaignRawMaterialTmp.RequiredQuantity - CampaignRawMaterialTmp.UsedQuantity

    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Cierra el formulario al dar escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmManageRawMaterial_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "MenuContext"

#End Region

#Region "Click"


#End Region

#Region "Privates"
    ''' <summary>
    ''' Consulto los detalles del producto
    ''' </summary>
    Private Async Function GetProductDetails() As Task
        Try
            Using model As New MCampaign(Me.Tag)
                AsyncLoader(True)
                Dim Data As New Tuple(Of Integer, Integer)(_quantity, RequestMixingStationDetailId)
                Dim result = Await model.getProductDetail(_productPackageIds, Data)
                If result IsNot Nothing Then
                    ListProductDetailItems = result.Data.ToList()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para poder continuar"
                End If

                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

    ''' <summary>
    ''' Guarda la Información: Gestion Materia Prima
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial"></param>
    ''' <param name="_productPackageIds"></param>
    ''' <param name="RequestMixingStationDetailId"></param>
    Private Async Function Guardar(myListCampaignRawMaterial As List(Of ManageRawMaterialModel), _productPackageIds As List(Of Integer), RequestMixingStationDetailId As Integer) As Task
        Me.AsyncLoader(True)
        Try
            Using model As New MCampaign(Me.Tag.ToString())
                Dim result = Await model.SaveManageCampaignRawMaterialsAsync(myListCampaignRawMaterial, _productPackageIds, RequestMixingStationDetailId)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Se asignó la materia prima a la campaña correctamente"
                    RaiseEvent ActionCompleted()
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function
#End Region

#Region "BarButton Events"
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
    ''' Click en confirmar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        SaveCampaignRawMaterial()
    End Sub
#End Region

#Region "Atomatic"
    ''' <summary>
    ''' Funcion publica para gestionar la materia prima de forma automatica
    ''' </summary>
    ''' <returns></returns>
    Public Async Function AutomaticManageRawMaterial(RequestMixingStationDetailId As Integer, PermissionsForm As Dictionary(Of Integer, String), Quantity As Integer,
                                                     productPackageIds As List(Of Integer)) As Task(Of ActionResult)
        Try
            Me.RequestMixingStationDetailId = RequestMixingStationDetailId
            Me.PermissionsForm = PermissionsForm
            Me.Quantity = Quantity
            Me.productPackageIds = productPackageIds
            Await GetProductDetails()
            Await SaveCampaignRawMaterial()
            Return New ActionResult With {.Message = "Proceso de gestion automatica de materia prima exitoso", .StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.Message = ex.Message, .StateResult = False}
        End Try
    End Function
#End Region

#End Region

End Class
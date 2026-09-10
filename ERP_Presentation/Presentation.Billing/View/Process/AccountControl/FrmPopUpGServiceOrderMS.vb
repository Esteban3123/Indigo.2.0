'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Giovanny Plazas
' Created          : 2/02/2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Common.MVP
Imports System.Threading
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.Data.Xpo.MixingStationRepository
#End Region

Public Class FrmPopUpGServiceOrderMS

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PServiceOrder

    ''' <summary>
    ''' lista de detalle de tarifa de un paquete por producto
    ''' </summary>
    Private _listProductRateDetailPackage As List(Of ProductRateDetailPackage)

    ''' <summary>
    '''   Permisos del formulario
    ''' </summary>
    Public PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' Lista de las dosis seleccionadas para generar las ordenes de servicio
    ''' </summary>
    Public Property ListPharmaDoseSelected As List(Of Domain.Entities.SP_GetProcessedMedicationItemsForBilling_Result)
        Get
            Return INDGcPharmaDoseSelected.DataSource
        End Get
        Set(value As List(Of Domain.Entities.SP_GetProcessedMedicationItemsForBilling_Result))
            INDGcPharmaDoseSelected.DataSource = value
        End Set
    End Property

    Public Property ProductRateDetailPackage As List(Of ProductRateDetailPackage)
        Get
            Return CType(INDgcPharmaDose.DataSource, List(Of ProductRateDetailPackage))
        End Get
        Set(value As List(Of ProductRateDetailPackage))
            INDgcPharmaDose.DataSource = value
        End Set
    End Property

    Private _careGroupId As Integer
    Public Property CareGroupId As Integer
        Get
            Return _careGroupId
        End Get
        Set(value As Integer)
            _careGroupId = value
        End Set
    End Property

#End Region

#Region "Public Event"

    Public Event GenerateServiceOrder(e As List(Of ProductRateDetailPackage), FlagMixingStation As Byte, ListIdDose As List(Of Guid))

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.PrepareToolbar(eAction.None)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpGServiceOrderMS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PServiceOrder()
        If Not PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.EditGrid)) Then
            INDColQuantity.OptionsColumn.AllowEdit = False
        End If

        Dim ListviewPharmaDoseMixingStations = New List(Of ViewPharmaDoseMixingStation)

        ListPharmaDoseSelected.ForEach(Sub(d)
                                           Dim _viewPharmaDoseMS = New ViewPharmaDoseMixingStation
                                           With _viewPharmaDoseMS
                                               .Id = d.Id
                                               .PackageId = d.PackageId
                                               .QuantityDelivered = d.QuantityDelivered
                                               .AppliedDose = d.AppliedDose
                                               .QuantityReceivable = d.QuantityReceivable
                                               .DeliveryStatus = d.DeliveryStatus
                                               .BatchCode = d.BatchCode
                                               .FunctionalUnitId = d.FunctionalUnitId
                                               .SurchargeApply = d.SurchargeApply
                                               .OrderedHealthProfessionalCode = d.OrderedHealthProfessionalCode
                                               .OrderedHealthProfessionalThirdPartyId = d.OrderedHealthProfessionalThirdPartyId
                                               .HealthAdministratorId = d.HealthAdministratorId
                                               .PerformsProfessionalSpecialty = d.OrderedProfessionalSpecialty
                                               .WarehouseId = d.WarehouseId
                                           End With
                                           ListviewPharmaDoseMixingStations.Add(_viewPharmaDoseMS)
                                       End Sub)

        LoadControls(_careGroupId, ListPharmaDoseSelected.Select(Function(p) p.PackageId.Value).Distinct().ToList(), ListviewPharmaDoseMixingStations.ToList())
    End Sub

    ''' <summary>
    ''' LoadControls
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="PackageIds"></param>
    ''' <param name="ListviewPharmaDoseMixingStations"></param>
    Private Async Sub LoadControls(CareGroupId As Integer, PackageIds As List(Of Integer), ListviewPharmaDoseMixingStations As List(Of ViewPharmaDoseMixingStation))
        Try
            AsyncLoader(True)
            Using Model As New MAccountControl(Me.Tag)
                Dim Result = Await Model.GetPackageValuePerProduct(CareGroupId, PackageIds, GetServerDate(), ListviewPharmaDoseMixingStations)
                AsyncLoader(False)
                If Result Is Nothing Then
                    Throw New Exception
                End If
                If Result.StateResult = False OrElse Result.ObjectEmbbeded.Count = 0 Then
                    ShowMessage(EeventViewerImages.Advertencia) = Result.Message
                    Me.Close()
                    Exit Sub
                End If
                INDgcPharmaDose.DataSource = Result.ObjectEmbbeded
            End Using

        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = "Ocurrió un error al calcular los valores del paquete"
            Me.Close()
        End Try
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Evento Click para generar Orden de servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnGenServiceOrder_Click(sender As Object, e As EventArgs) Handles INDbtnGenServiceOrder.Click
        AsyncLoader(True)
        RaiseEvent GenerateServiceOrder(ProductRateDetailPackage, 2, ListPharmaDoseSelected.Select(Function(x) x.Id).ToList())
        AsyncLoader(False)
        Me.Close()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpGServiceOrderMS_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpGServiceOrderMS_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Actualiza el valor del campo Total Value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiseQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRiseQuantity.EditValueChanging
        Dim ProductPackageTmp = INDgvPharmaDose.GetFocusedObject(Of ProductRateDetailPackage)
        ProductPackageTmp.TotalValue = ProductPackageTmp.UnitValue * e.NewValue
    End Sub

#End Region

#End Region

End Class


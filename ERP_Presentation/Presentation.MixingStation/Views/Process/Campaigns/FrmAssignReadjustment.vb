'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Giovanny Plazas Lozano
' Created          : 23/09/2022
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
Imports Presentation.MixingStation.MVP
Imports DevExpress.Data.Async.Helpers
#End Region

Public Class FrmAssignReadjustment

#Region "Variables"
    Private _requestMixingSDetailId As Integer
#End Region

#Region "Public Event"

    ''' <summary>
    ''' Evento para importar la informacion
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddSelected(sender As Object, e As EventArgs)

#End Region

#Region "Enumerations"
    Enum eUnitDoseTypeMSClass
        NPT = 2
        Antibioticos = 3
        Oncologicos = 4
        Reempaque = 5
        Reenvase = 7
        Magistral = 9
        OtrosEstériles = 10
    End Enum
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

    Private _requestMixingStationDetail As RequestMixingStationDetail
    Private Property RequestMixingStationDetail As RequestMixingStationDetail
        Get
            Return _requestMixingStationDetail
        End Get
        Set(value As RequestMixingStationDetail)
            _requestMixingStationDetail = value
        End Set
    End Property

    Private ReadOnly Property ItemSelected As ViewReadjustmentsToBeAssignedXpo
        Get
            Return INDGvAssingReadjustment.GetSelectedRows().Select(Function(x) TryCast(TryCast(INDGvAssingReadjustment.GetRow(x), ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewReadjustmentsToBeAssignedXpo)).FirstOrDefault
        End Get
    End Property

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignReadjustment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadControls()
    End Sub

    ''' <summary>
    ''' perimte solo guardar 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
    End Sub

#End Region

#Region "Click"

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignReadjustment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Evento que se dispara al seleccionar el check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvAssingReadjustment_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDGvAssingReadjustment.SelectionChanged
        Dim rowHandleInfoSelected = INDGvAssingReadjustment.FocusedRowHandle()
        Dim listRowHandles = INDGvAssingReadjustment.GetSelectedRows()

        If listRowHandles.Length > 0 Then
            For i = 0 To listRowHandles.Count - 1 Step 1
                If rowHandleInfoSelected <> listRowHandles(i) Then
                    INDGvAssingReadjustment.UnselectRow(listRowHandles(i))
                End If
            Next
        End If
    End Sub

#End Region

#Region "Methods"
    Private Async Sub LoadControls()
        Try
            If _requestMixingSDetailId <= 0 Then
                ShowMessage(EeventViewerImages.Informacion) = "Error, no se cargo la solicitud de la campaña"
                Me.Close()
                Exit Sub
            End If
            AsyncLoader(True)

            Using Model As New MReadjustments("")
                Dim Result = Await Model.GetRequestMSDToReadjustmentById(_requestMixingSDetailId)
                If Result Is Nothing OrElse Result?.StateResult = False OrElse Result?.ObjectEmbbeded.Id = 0 Then
                    ShowMessage(EeventViewerImages.Informacion) = Result?.Message
                    Me.Close()
                    Exit Sub
                End If
                RequestMixingStationDetail = Result.ObjectEmbbeded

                Dim StandardPackageId As Integer = 0
                Dim AtcMainMedicine As Integer? = Nothing
                Dim AtcVehicle As Integer? = Nothing
                '--------------------------------------------
                Dim Concentration As Integer? = Nothing
                Dim ConcentrationMeasurementUnitId As Integer? = Nothing
                Dim VolumeTotalOrder As Decimal? = Nothing
                Dim VolumeTotalOrderMeasurementUnitId As Integer? = Nothing
                '--------------------------------------------
                Dim AtcThinner As Integer? = Nothing
                Dim QuantityMainMedicine As Decimal? = Nothing
                Dim QuantityVehicle As Decimal? = Nothing
                Dim QuantityThinner As Decimal? = Nothing
                Dim MeasurementUnitIdMainMedicine As Integer? = Nothing
                Dim MeasurementUnitIdVehicle As Integer? = Nothing
                Dim MeasurementUnitIdThinner As Integer? = Nothing

                If RequestMixingStationDetail.PackagePersonalizedId Is Nothing Then 'Paquete estándar
                    StandardPackageId = RequestMixingStationDetail.PackageId
                    AtcMainMedicine = RequestMixingStationDetail.Package.PackageDetail.FirstOrDefault(Function(s) s.MainMedicine).AtcId
                    AtcVehicle = RequestMixingStationDetail.Package.PackageDetail.FirstOrDefault(Function(s) s.Vehicle).AtcId
                    AtcThinner = RequestMixingStationDetail.Package.PackageDetail.FirstOrDefault(Function(s) s.Thinner)?.AtcId

                    If RequestMixingStationDetail.UnitDoseType.MSClass = eUnitDoseTypeMSClass.Antibioticos Then
                        If AtcMainMedicine IsNot Nothing Then
                            QuantityMainMedicine = RequestMixingStationDetail?.Package.PackageDetail.FirstOrDefault(Function(s) s.MainMedicine).Quantity
                            MeasurementUnitIdMainMedicine = RequestMixingStationDetail?.Package.PackageDetail.FirstOrDefault(Function(s) s.MainMedicine).MeasurementUnitId
                        End If

                        If AtcVehicle IsNot Nothing Then
                            QuantityVehicle = RequestMixingStationDetail.Package.PackageDetail.FirstOrDefault(Function(s) s.Vehicle).Quantity
                            MeasurementUnitIdVehicle = RequestMixingStationDetail.Package.PackageDetail.FirstOrDefault(Function(s) s.Vehicle).MeasurementUnitId
                        End If

                        If AtcThinner IsNot Nothing Then
                            QuantityThinner = RequestMixingStationDetail.Package.PackageDetail.FirstOrDefault(Function(s) s.Thinner)?.Quantity
                            MeasurementUnitIdThinner = RequestMixingStationDetail.Package.PackageDetail.FirstOrDefault(Function(s) s.Thinner)?.MeasurementUnitId
                        End If

                        INDGcAssignReadjustment.DataSource = Model.ViewReadjustmentsToBeAssigned(StandardPackageId, GetServerDate(),
                                                                                                           AtcMainMedicine, QuantityMainMedicine, MeasurementUnitIdMainMedicine,
                                                                                                           AtcVehicle, QuantityVehicle, MeasurementUnitIdVehicle,
                                                                                                           AtcThinner, QuantityThinner, MeasurementUnitIdThinner)

                    Else
                        Concentration = RequestMixingStationDetail.Package.ConcentrationMeasurementUnitId
                        ConcentrationMeasurementUnitId = RequestMixingStationDetail.Package.ConcentrationMeasurementUnitId
                        VolumeTotalOrder = RequestMixingStationDetail.Package.VolumeTotalOrder
                        VolumeTotalOrderMeasurementUnitId = RequestMixingStationDetail.Package.VolumeTotalOrderMeasurementUnitId

                        INDGcAssignReadjustment.DataSource = Model.ViewListToAssignReadjustment(StandardPackageId, Concentration, ConcentrationMeasurementUnitId, VolumeTotalOrder, VolumeTotalOrderMeasurementUnitId, AtcMainMedicine, AtcVehicle, GetServerDate())
                    End If

                Else 'Paquete personalizado
                    StandardPackageId = RequestMixingStationDetail.PackageId
                    AtcMainMedicine = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.MainMedicine)?.AtcId
                    AtcVehicle = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.Vehicle)?.AtcId
                    AtcThinner = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.Thinner)?.AtcId

                    If RequestMixingStationDetail.UnitDoseType.MSClass = eUnitDoseTypeMSClass.Antibioticos Then
                        If AtcMainMedicine IsNot Nothing Then
                            QuantityMainMedicine = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.MainMedicine)?.Quantity
                            MeasurementUnitIdMainMedicine = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.MainMedicine)?.MeasurementUnitId
                        End If

                        If AtcVehicle IsNot Nothing Then
                            QuantityVehicle = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.Vehicle)?.Quantity
                            MeasurementUnitIdVehicle = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.Vehicle)?.MeasurementUnitId
                        End If

                        If AtcThinner IsNot Nothing Then
                            QuantityThinner = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.Thinner)?.Quantity
                            MeasurementUnitIdThinner = RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.FirstOrDefault(Function(s) s.Thinner)?.MeasurementUnitId
                        End If

                        INDGcAssignReadjustment.DataSource = Model.ViewReadjustmentsToBeAssigned(StandardPackageId, GetServerDate(),
                                                                                                           AtcMainMedicine, QuantityMainMedicine, MeasurementUnitIdMainMedicine,
                                                                                                           AtcVehicle, QuantityVehicle, MeasurementUnitIdVehicle,
                                                                                                           AtcThinner, QuantityThinner, MeasurementUnitIdThinner)
                    Else
                        Concentration = RequestMixingStationDetail?.PackagePersonalized?.Concentration
                        ConcentrationMeasurementUnitId = RequestMixingStationDetail?.PackagePersonalized?.ConcentrationMeasurementUnitId
                        VolumeTotalOrder = RequestMixingStationDetail?.PackagePersonalized?.VolumeTotalOrder
                        VolumeTotalOrderMeasurementUnitId = RequestMixingStationDetail?.PackagePersonalized?.VolumeTotalOrderMeasurementUnitId

                        INDGcAssignReadjustment.DataSource = Model.ViewListToAssignReadjustment(StandardPackageId, Concentration, ConcentrationMeasurementUnitId, VolumeTotalOrder, VolumeTotalOrderMeasurementUnitId, AtcMainMedicine, AtcVehicle, GetServerDate())
                    End If

                End If

                INDGcAssignReadjustment.RefreshDataSource()
            End Using

        Catch ex As Exception
            Me.Close()
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' metodo guardar para vinular la readecuacion con el detalle de la solicitud
    ''' </summary>
    Private Async Sub Guardar()
        Try
            AsyncLoader(True)

            If ItemSelected Is Nothing Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                ShowMessage(EeventViewerImages.Informacion) = "No se ha seleccionado la readecuación"
                Exit Sub
            End If

            Using Model As New MReadjustments("")
                Dim result = Await Model.AssignReadjustmentToRequestMSDetail(_requestMixingSDetailId, New Readjustments With {.Id = ItemSelected.Id, .RequestPackageDetailStatusId = ItemSelected.RequestPackageDetailStatusId})

                If result Is Nothing OrElse Not result?.StateResult Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.No
                    ShowMessage(EeventViewerImages.Advertencia) = result?.Message
                    Exit Sub
                End If

                RaiseEvent AddSelected(Nothing, Nothing)
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                ShowMessage(EeventViewerImages.Informacion) = result?.Message
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
            Me.Close()
        End Try
    End Sub
#End Region

#Region "Builder"
    ''' <summary>
    ''' constructor
    ''' </summary>
    ''' <param name="IdRequestMSD">Id del detalle de la Solicitud de la campana </param>
    Public Sub New(IdRequestMSD As Integer)

        _requestMixingSDetailId = IdRequestMSD
        ' This call is required by the designer.
        InitializeComponent()

    End Sub

#End Region

#End Region

End Class
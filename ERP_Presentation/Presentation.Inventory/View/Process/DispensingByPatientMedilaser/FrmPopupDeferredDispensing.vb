'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/09/2018
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Inventory.MVP
Imports Domain.Crystal.Entities

#End Region

Public Class FrmPopupDeferredDispensing

#Region "Builder"
    Sub New(_detail As ViewDashboardPharmacyDetail, dateDocument As DateTime)
        ' This call is required by the designer.
        InitializeComponent()
        detail = _detail
        ProductCode = detail.MedicamentCode
        ' Add any initialization after the InitializeComponent() call.
        INDseQuantityDelivery.Properties.MinValue = 1
        INDsePeriodicity.Properties.MinValue = 1

        If detail.ListDeferred Is Nothing OrElse detail.ListDeferred.Count = 0 Then
            INDseTotalQuantity.EditValue = detail.CantidadSolicitada
            INDdteDateFirstDelivery.EditValue = dateDocument
        Else
            INDseTotalQuantity.EditValue = detail.ListDeferred.Sum(Function(x) x.DeliveryQuantity)
            INDdteDateFirstDelivery.EditValue = detail.ListDeferred(0).FirstDeliveryDate
            INDseQuantityDelivery.EditValue = detail.ListDeferred(0).DeliveryQuantityDeferred
            INDsePeriodicity.EditValue = detail.ListDeferred(0).Periodicity

            ListDeferred = detail.ListDeferred
            INDgcDeferred.DataSource = Nothing
            INDgcDeferred.DataSource = ListDeferred

            If detail.MedicalFormulaDetailId > 0 Then
                INDseQuantityDelivery.Enabled = False
                INDsePeriodicity.Enabled = False
                INDbtnDeferred.Enabled = False
                INDBtnOk.Enabled = False
            End If
        End If

        INDseQuantityDelivery.Properties.MaxValue = INDseTotalQuantity.EditValue - 1
        INDsePeriodicity.Properties.MaxValue = 999
    End Sub
    Sub New(_detail As ViewDashboardPharmacyDetail)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        detail = _detail

        INDseQuantityDelivery.Properties.MinValue = 1
        INDsePeriodicity.Properties.MinValue = 1

        If detail.ListDeferred Is Nothing OrElse detail.ListDeferred.Count = 0 Then
            INDseTotalQuantity.EditValue = detail.CantidadSolicitada
            INDdteDateFirstDelivery.EditValue = DateTime.Now()
        Else
            INDseTotalQuantity.EditValue = detail.ListDeferred.Sum(Function(x) x.DeliveryQuantity)
            INDdteDateFirstDelivery.EditValue = detail.ListDeferred(0).FirstDeliveryDate
            INDseQuantityDelivery.EditValue = detail.ListDeferred(0).DeliveryQuantityDeferred
            INDsePeriodicity.EditValue = detail.ListDeferred(0).Periodicity

            ListDeferred = detail.ListDeferred
            INDgcDeferred.DataSource = Nothing
            INDgcDeferred.DataSource = ListDeferred

            If detail.MedicalFormulaDetailId > 0 Then
                INDseQuantityDelivery.Enabled = False
                INDsePeriodicity.Enabled = False
                INDbtnDeferred.Enabled = False
                INDBtnOk.Enabled = False
            End If
        End If

        INDseQuantityDelivery.Properties.MaxValue = INDseTotalQuantity.EditValue - 1
        INDsePeriodicity.Properties.MaxValue = 999
    End Sub

#End Region

#Region "Event"

    Public Event SuccessDeferredEventArgs(sender As Object, e As SuccessEventArgs)

#End Region

#Region "Variables"

    Dim detail As ViewDashboardPharmacyDetail

    Dim ListDeferred As List(Of ViewDashboardPharmacyDetailDeferred)

    Public ProductCode As String

#End Region

#Region "Properties"

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

    Private Sub ProcessDeferred()
        ListDeferred = New List(Of ViewDashboardPharmacyDetailDeferred)
        Dim tempTotalQuantity As Integer = INDseTotalQuantity.EditValue
        Dim count As Integer = 1
        Dim tempDeliveryDate As DateTime = INDdteDateFirstDelivery.EditValue

        While tempTotalQuantity > 0
            Dim entityTest As New ViewDashboardPharmacyDetailDeferred
            With entityTest
                .Id = 0
                .Number = count
                .DeliveryDate = tempDeliveryDate
                If INDseQuantityDelivery.EditValue > tempTotalQuantity Then
                    .DeliveryQuantity = tempTotalQuantity
                Else
                    .DeliveryQuantity = INDseQuantityDelivery.EditValue
                End If
                .PendingQuantity = .DeliveryQuantity
                .ProductCode = ProductCode
                .FirstDeliveryDate = INDdteDateFirstDelivery.EditValue
                .DeliveryQuantityDeferred = INDseQuantityDelivery.EditValue
                .Periodicity = INDsePeriodicity.EditValue
            End With
            ListDeferred.Add(entityTest)

            count += 1
            tempDeliveryDate = DateAdd(DateInterval.Day, INDsePeriodicity.EditValue, tempDeliveryDate)
            If INDseQuantityDelivery.EditValue > tempTotalQuantity Then
                tempTotalQuantity = 0
            Else
                tempTotalQuantity -= INDseQuantityDelivery.EditValue
            End If
        End While

        INDgcDeferred.DataSource = Nothing
        INDgcDeferred.DataSource = ListDeferred
    End Sub

#End Region

#Region "Handlers"

#Region "Click"

    Private Sub INDbtnDeferred_Click(sender As Object, e As EventArgs) Handles INDbtnDeferred.Click
        ProcessDeferred()
    End Sub

    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        If INDseQuantityDelivery.EditValue Is Nothing OrElse INDseQuantityDelivery.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar no puede ser cero"
            Exit Sub
        End If

        If INDsePeriodicity.EditValue Is Nothing OrElse INDsePeriodicity.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La periodicidad no puede ser cero"
            Exit Sub
        End If

        If ListDeferred Is Nothing OrElse ListDeferred.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha diferido las cantidades"
            Exit Sub
        End If

        RaiseEvent SuccessDeferredEventArgs(Nothing, New SuccessEventArgs With {.ListDeferred = ListDeferred})
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmPopupDeferredDispensing_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class
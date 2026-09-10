'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/02/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports DevExpress.XtraLayout
Imports Presentation.Portfolio.MVP
#End Region

Public Class FrmContractDetail

#Region "Event"

    Public Event AddDetailsArgs(sender As Object, e As AddDetailsEventArgs)

#End Region

#Region "Properties"

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
    ''' propiedad publica para el tag del formulario
    ''' </summary>
    ''' <value>
    ''' The tag form.
    ''' </value>
    Public Property Tag As String
        Get
            Return _tag
        End Get
        Set(value As String)
            _tag = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Id de la unidad operativa
    ''' </summary>
    Public OperatingUnitId As Integer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PContract

    ''' <summary>
    ''' Permite saber si se esta editando
    ''' </summary>
    Public EditMode As Boolean

    ''' <summary>
    ''' Entidad del detalle del contrato
    ''' </summary>
    Public ContractDetail As ContractDetail

    ''' <summary>
    ''' Formulario principal
    ''' </summary>
    Public FrmContract As FrmContract

    ''' <summary>
    ''' listado de las edades de cartera
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPortfolioAge As List(Of AgesPortfolio)

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim settingPortfolio As SettingPortfolio

    ''' <summary>
    ''' variable para asignar el tag del formulario donde esta contenido el control de usuario
    ''' </summary>
    Dim _tag As String

#End Region

#Region "Methods"

    ''' <summary>
    ''' Agrega el detalle al formulario principal
    ''' </summary>
    Private Sub AddDetailToPrincipalForm()
        If ValidateControls() = False Then
            Exit Sub
        End If

        If EditMode = False Then
            ContractDetail = New ContractDetail()
        End If

        If FrmContract.ListContractDetail IsNot Nothing AndAlso FrmContract.ListContractDetail.Count > 0 Then
            If (From x In FrmContract.ListContractDetail Where x.ContractNumber = INDtxtContractNumber.EditValue AndAlso INDtxtContractNumber.EditValue <> ContractDetail.ContractNumber Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El No. de contrato " + INDtxtContractNumber.EditValue + " ya existe en la lista"
                INDtxtContractNumber.Focus()
                Exit Sub
            End If
        End If

        With ContractDetail
            .ContractNumber = INDtxtContractNumber.EditValue
            .ContractName = INDtxtContractName.EditValue
            .ContractNumberName = .ContractNumber + " - " + .ContractName
            .Type = INDsleType.EditValue
            .InitialDate = INDdteInitialDate.EditValue
            .EndDate = INDdteEndDate.EditValue
            .Legalized = INDsleLegalized.EditValue

            .DateLegalization = Nothing
            If INDlyItemDateLegalization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .DateLegalization = INDdteDateLegalization.EditValue
            End If

            .BillingInitialDate = INDdteBillingInitialDate.EditValue
            .BillingEndDate = INDdteBillingEndDate.EditValue
            .RadicatedBillingDate = INDdteRadicatedBillingDate.EditValue
            .PercentageApplyPaymentSoon = INDsePercentageApplyPaymentSoon.EditValue
            .AgesPortfolioId = INDsleAgesPortfolioId.EditValue
            .ValidRecord = INDsleValidRecord.EditValue
            .PrintingMode = INDslePrintingMode.EditValue
            .TerminationControl = INDsleTerminationControl.EditValue
            .Observations = INDmemoObservation.EditValue
            .PermanentObservationOfTheInvoice = INDmemoPermanentObservationOfTheInvoice.EditValue

            If INDlygNotification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .NotificationValueType = Nothing
                If INDlyItemNotificationValueType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .NotificationValueType = CInt(INDsleNotificationValueType.EditValue)
                End If

                .PercentageNotification = Nothing
                If INDlyItemPercentageNotification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PercentageNotification = CDec(INDsePercentageNotification.EditValue)
                End If

                .NotificationValue = Nothing
                If INDlyItemNotificationValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .NotificationValue = CDec(INDtxtNotificationValue.EditValue)
                End If

                .NotificationTimeType = Nothing
                If INDlyItemNotificationTimeType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .NotificationTimeType = CInt(INDsleNotificationTimeType.EditValue)
                End If

                .NotificationDays = Nothing
                If INDlyItemNotificationDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .NotificationDays = CInt(INDseNotificationDays.EditValue)
                End If
            Else
                .NotificationValueType = Nothing
                .PercentageNotification = Nothing
                .NotificationValue = Nothing
                .NotificationTimeType = Nothing
                .NotificationDays = Nothing
            End If
        End With

        Dim args As New AddDetailsEventArgs
        args.ContractDetail = ContractDetail
        args.EditMode = EditMode
        RaiseEvent AddDetailsArgs(Nothing, args)
        CleanControls()
        INDtxtContractNumber.Focus()
    End Sub

    ''' <summary>
    ''' Asigna los datos a los campos
    ''' </summary>
    Private Sub LoadControls()
        With ContractDetail
            INDtxtContractNumber.EditValue = .ContractNumber
            INDtxtContractName.EditValue = .ContractName
            INDsleType.EditValue = .Type
            INDdteInitialDate.EditValue = .InitialDate
            INDdteEndDate.EditValue = .EndDate
            INDsleLegalized.EditValue = .Legalized
            INDdteDateLegalization.EditValue = .DateLegalization
            INDdteBillingInitialDate.EditValue = .BillingInitialDate
            INDdteBillingEndDate.EditValue = .BillingEndDate
            INDdteRadicatedBillingDate.EditValue = .RadicatedBillingDate
            INDsePercentageApplyPaymentSoon.EditValue = .PercentageApplyPaymentSoon
            INDsleAgesPortfolioId.EditValue = .AgesPortfolioId
            INDsleAgesPortfolioId.Properties.NullText = .AgesPortfolioName
            INDsleValidRecord.EditValue = .ValidRecord
            INDslePrintingMode.EditValue = .PrintingMode
            INDsleTerminationControl.EditValue = .TerminationControl
            INDmemoObservation.EditValue = .Observations
            INDsleNotificationValueType.EditValue = .NotificationValueType
            INDsePercentageNotification.EditValue = .PercentageNotification
            INDtxtNotificationValue.EditValue = .NotificationValue
            INDsleNotificationTimeType.EditValue = .NotificationTimeType
            INDseNotificationDays.EditValue = .NotificationDays
            INDmemoPermanentObservationOfTheInvoice.EditValue = .PermanentObservationOfTheInvoice
        End With
    End Sub

    ''' <summary>
    ''' Llena los datasource de los controles con infromación quemada
    ''' </summary>
    Private Sub InitializeSearch()
        Dim ListTypes = New List(Of Tuple(Of Integer, String))
        ListTypes.Add(New Tuple(Of Integer, String)(1, "Nuevo"))
        ListTypes.Add(New Tuple(Of Integer, String)(2, "Inclusión"))
        ListTypes.Add(New Tuple(Of Integer, String)(3, "Otro Si"))
        ListTypes.Add(New Tuple(Of Integer, String)(4, "Adición"))
        ListTypes.Add(New Tuple(Of Integer, String)(5, "Prorroga"))
        ListTypes.Add(New Tuple(Of Integer, String)(6, "Otros"))
        INDsleType.Properties.DataSource = ListTypes

        Dim ListYesNo = New List(Of Tuple(Of Boolean, String))
        ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleLegalized.Properties.DataSource = ListYesNo
        INDsleValidRecord.Properties.DataSource = ListYesNo

        Dim ListPrintingMode = New List(Of Tuple(Of Integer, String))
        ListPrintingMode.Add(New Tuple(Of Integer, String)(1, "Manual Tarifario"))
        ListPrintingMode.Add(New Tuple(Of Integer, String)(2, "CUPS"))
        ListPrintingMode.Add(New Tuple(Of Integer, String)(3, "Código RIPS"))
        ListPrintingMode.Add(New Tuple(Of Integer, String)(4, "Descripción Relacionada"))
        ListPrintingMode.Add(New Tuple(Of Integer, String)(5, "CUPS - Descripción Relacionada"))
        ListPrintingMode.Add(New Tuple(Of Integer, String)(6, "Descripción relacionada ó CUPS"))
        INDslePrintingMode.Properties.DataSource = ListPrintingMode

        Dim ListTerminationControl = New List(Of Tuple(Of Integer, String))
        ListTerminationControl.Add(New Tuple(Of Integer, String)(1, "Ninguno"))
        ListTerminationControl.Add(New Tuple(Of Integer, String)(2, "Fecha Terminación"))
        ListTerminationControl.Add(New Tuple(Of Integer, String)(3, "Valor Contrato"))
        ListTerminationControl.Add(New Tuple(Of Integer, String)(4, "Fecha Terminación o Valor Contrato"))
        INDsleTerminationControl.Properties.DataSource = ListTerminationControl

        Dim ListNotificationTypeValue = New List(Of Tuple(Of Integer, String))
        ListNotificationTypeValue.Add(New Tuple(Of Integer, String)(1, "Ninguna"))
        ListNotificationTypeValue.Add(New Tuple(Of Integer, String)(2, "% del valor del contrato"))
        ListNotificationTypeValue.Add(New Tuple(Of Integer, String)(3, "Valor fijo"))
        INDsleNotificationValueType.Properties.DataSource = ListNotificationTypeValue

        Dim ListNotificationTypeTime = New List(Of Tuple(Of Integer, String))
        ListNotificationTypeTime.Add(New Tuple(Of Integer, String)(1, "Ninguno"))
        ListNotificationTypeTime.Add(New Tuple(Of Integer, String)(2, "Dias de anterioridad"))
        INDsleNotificationTimeType.Properties.DataSource = ListNotificationTypeTime

    End Sub

    ''' <summary>
    ''' Limpia los controles del modal
    ''' </summary>
    Private Sub CleanControls()
        INDtxtContractNumber.EditValue = Nothing
        INDtxtContractName.EditValue = Nothing
        INDsleType.EditValue = Nothing
        INDdteInitialDate.EditValue = Nothing
        INDdteEndDate.EditValue = Nothing
        INDsleLegalized.EditValue = Nothing
        INDdteDateLegalization.EditValue = Nothing
        INDdteBillingInitialDate.EditValue = Nothing
        INDdteBillingEndDate.EditValue = Nothing
        INDdteRadicatedBillingDate.EditValue = Nothing
        INDsePercentageApplyPaymentSoon.EditValue = Nothing
        INDsleAgesPortfolioId.EditValue = Nothing
        INDsleValidRecord.EditValue = Nothing
        INDslePrintingMode.EditValue = Nothing
        INDsleTerminationControl.EditValue = Nothing
        INDmemoObservation.EditValue = Nothing
        INDmemoPermanentObservationOfTheInvoice.EditValue = Nothing
        INDsleNotificationValueType.EditValue = Nothing
        INDsePercentageNotification.EditValue = Nothing
        INDtxtNotificationValue.EditValue = Nothing
        INDsleNotificationTimeType.EditValue = Nothing
        INDseNotificationDays.EditValue = Nothing
        listPortfolioAge = Nothing
        INDlygNotification.HideControl()
        ContractDetail = Nothing
        EditMode = False
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Presenter = New PContract()
        InitializeSearch()
        If EditMode Then
            LoadControls()
        Else
            INDsleTerminationControl.EditValue = 1
        End If
    End Sub

#End Region

#Region "QueryPopup"
    Private Async Sub INDsleAgesPortfolioId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAgesPortfolioId.QueryPopUp
        If INDsleAgesPortfolioId.Properties.DataSource Is Nothing Then
            Using model As New MSettingPortfolio(Tag)
                settingPortfolio = Await model.GetSettingPortfolioByIdOperatingUnitAsync(OperatingUnitId)
            End Using
            Using model As New MAgesPortfolio(Tag)
                listPortfolioAge = model.ListAgesPortfolioByIdSettingPortfolio(settingPortfolio.Id)
            End Using
            INDsleAgesPortfolioId.Properties.DataSource = listPortfolioAge
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDdteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialDate.EditValueChanged
        If INDdteInitialDate.EditValue IsNot Nothing Then
            INDdteEndDate.Properties.MinValue = CDate(INDdteInitialDate.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control fecha inicial de facturación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDdteBillingInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteBillingInitialDate.EditValueChanged
        If INDdteBillingInitialDate.EditValue IsNot Nothing Then
            INDdteBillingEndDate.Properties.MinValue = CDate(INDdteBillingInitialDate.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control legalizado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleLegalized_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLegalized.EditValueChanged
        If INDsleLegalized.EditValue IsNot Nothing Then
            If INDsleLegalized.EditValue = True Then
                INDlyItemDateLegalization.HideControl(False)
            Else
                INDlyItemDateLegalization.HideControl()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de terminación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleTerminationControl_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTerminationControl.EditValueChanged
        If INDsleTerminationControl.EditValue IsNot Nothing Then
            Select Case INDsleTerminationControl.EditValue
                Case 1
                    'Ninguno
                    INDlygNotification.HideControl()
                Case 2
                    'Fecha Terminacion
                    INDlygNotification.HideControl(False)
                    INDlyItemNotificationValueType.HideControl()
                    INDlyItemNotificationValue.HideControl()
                    INDlyItemPercentageNotification.HideControl()
                    INDlyItemNotificationTimeType.HideControl(False)
                    INDlyItemNotificationDays.HideControl()

                    INDsleNotificationTimeType.EditValue = 1
                Case 3
                    'Valor contrato
                    INDlygNotification.HideControl(False)
                    INDlyItemNotificationValueType.HideControl(False)
                    INDlyItemNotificationValue.HideControl()
                    INDlyItemPercentageNotification.HideControl()
                    INDlyItemNotificationTimeType.HideControl()
                    INDlyItemNotificationDays.HideControl()

                    INDsleNotificationValueType.EditValue = 1
                Case 4
                    'Fecha Terminacion o Valor Contrato
                    INDlygNotification.HideControl(False)
                    INDlyItemNotificationValueType.HideControl(False)
                    INDlyItemNotificationValue.HideControl()
                    INDlyItemPercentageNotification.HideControl()
                    INDlyItemNotificationTimeType.HideControl(False)
                    INDlyItemNotificationDays.HideControl()

                    INDsleNotificationTimeType.EditValue = 1
                    INDsleNotificationValueType.EditValue = 1
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control tipo de notificación por valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleNotificationValueType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleNotificationValueType.EditValueChanged
        If INDsleNotificationValueType.EditValue IsNot Nothing Then
            Select Case INDsleNotificationValueType.EditValue
                Case 1
                    'Ninguna
                    INDlyItemPercentageNotification.HideControl()
                    INDlyItemNotificationValue.HideControl()
                Case 2
                    '% del valor del contrato
                    INDlyItemPercentageNotification.HideControl(False)
                    INDlyItemNotificationValue.HideControl()
                Case 3
                    'Valor fijo
                    INDlyItemPercentageNotification.HideControl()
                    INDlyItemNotificationValue.HideControl(False)
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control tipo notificación para tiempo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleNotificationTimeType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleNotificationTimeType.EditValueChanged
        If INDsleNotificationTimeType.EditValue IsNot Nothing Then
            If INDsleNotificationTimeType.EditValue = 1 Then
                'Ninguno
                INDlyItemNotificationDays.HideControl()
            Else
                'Dias de anterioridad
                INDlyItemNotificationDays.HideControl(False)
            End If
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDtxtContractNumber.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se ejecuta al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddDetailToPrincipalForm()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDmemoObservation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDmemoObservation.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDlygNotification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDbtnAddDetail.Focus()
            Else
                If INDlyItemNotificationValueType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDsleNotificationValueType.Focus()
                ElseIf INDlyItemNotificationTimeType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDsleNotificationTimeType.Focus()
                Else
                    INDbtnAddDetail.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleNotificationValueType_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsleNotificationValueType.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDlyItemPercentageNotification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsePercentageNotification.Focus()
            ElseIf INDlyItemNotificationValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDtxtNotificationValue.Focus()
            ElseIf INDlyItemNotificationTimeType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleNotificationTimeType.Focus()
            Else
                INDbtnAddDetail.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsePercentageNotification_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsePercentageNotification.KeyDown, INDtxtNotificationValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDlyItemNotificationTimeType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleNotificationTimeType.Focus()
            Else
                INDbtnAddDetail.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al presionar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleNotificationTimeType_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsleNotificationTimeType.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDlyItemNotificationDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDseNotificationDays.Focus()
            Else
                INDbtnAddDetail.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al presionar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseNotificationDays_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDseNotificationDays.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDbtnAddDetail.Focus()
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddDetailsEventArgs
    Inherits EventArgs

    Property ContractDetail As ContractDetail

    Property EditMode As Boolean

End Class
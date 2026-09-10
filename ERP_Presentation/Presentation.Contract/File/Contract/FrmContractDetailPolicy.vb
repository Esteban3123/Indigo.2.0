'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/02/2020
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
#End Region

Public Class FrmContractDetailPolicy

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

#End Region

#Region "Event"

    Public Event AddDetailsPolicyArgs(sender As Object, e As AddDetailsPolicyEventArgs)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PContract

    ''' <summary>
    ''' Permite saber si se esta editando
    ''' </summary>
    Public EditMode As Boolean

    ''' <summary>
    ''' Entidad del detalle de polizas del contrato
    ''' </summary>
    Public ContractDetailPolicy As ContractDetailPolicy

    ''' <summary>
    ''' Obtiene o establece la descripción del contrato al cual pertenece la novedad
    ''' </summary>
    Public ContractNumberName As String

    ''' <summary>
    ''' Permite saber si se esta cargando desde el loadControls
    ''' </summary>
    Dim IsLoadControls As Boolean = False

#End Region

#Region "Methods"

    ''' <summary>
    ''' Agrega el detalle al formulario principal
    ''' </summary>
    Private Sub AddDetailNoveltyToPrincipalForm()
        If ValidateControls() = False Then
            Exit Sub
        End If

        If EditMode = False Then
            ContractDetailPolicy = New ContractDetailPolicy()
        End If
        With ContractDetailPolicy
            .ContractNumberName = ContractNumberName
            .FixedAssetPolicyId = INDsleFixedAssetPolicyId.EditValue
            .FixedAssetPolicyDescription = INDsleFixedAssetPolicyId.Text
            .FixedAssetInsuranceId = INDsleFixedAssetInsuranceId.EditValue
            .FixedAssetInsuranceDescription = INDsleFixedAssetInsuranceId.Text
            .PolicyNumber = INDtxtPolicyNumber.EditValue
            .EmissionDate = INDdteEmissionDate.EditValue
            .AmountInsured = INDtxtAmountInsured.EditValue
            .CoveragePercentage = INDseCoveragePercentage.EditValue
            .Status = INDsleStatus.EditValue
            .Observation = INDmemoObservation.EditValue
        End With

        Dim args As New AddDetailsPolicyEventArgs
        args.ContractDetailPolicy = ContractDetailPolicy
        args.EditMode = EditMode
        RaiseEvent AddDetailsPolicyArgs(Nothing, args)
        CleanControls()
        INDsleFixedAssetPolicyId.Focus()
    End Sub

    ''' <summary>
    ''' Asigna los datos a los campos
    ''' </summary>
    Private Sub LoadControls()
        With ContractDetailPolicy
            INDsleFixedAssetPolicyId.EditValue = .FixedAssetPolicyId
            INDsleFixedAssetPolicyId.Properties.NullText = .FixedAssetPolicyDescription
            IsLoadControls = True
            INDsleFixedAssetInsuranceId.EditValue = .FixedAssetInsuranceId
            INDsleFixedAssetInsuranceId.Properties.NullText = .FixedAssetInsuranceDescription
            IsLoadControls = False
            INDtxtPolicyNumber.EditValue = .PolicyNumber
            INDdteEmissionDate.EditValue = .EmissionDate
            INDtxtAmountInsured.EditValue = .AmountInsured
            INDseCoveragePercentage.EditValue = .CoveragePercentage
            INDsleStatus.EditValue = .Status
            INDmemoObservation.EditValue = .Observation
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles del modal
    ''' </summary>
    Private Sub CleanControls()
        INDsleFixedAssetPolicyId.EditValue = Nothing
        INDsleFixedAssetPolicyId.Properties.NullText = String.Empty
        INDsleFixedAssetInsuranceId.EditValue = Nothing
        INDsleFixedAssetInsuranceId.Properties.NullText = String.Empty
        INDtxtPolicyNumber.EditValue = Nothing
        INDdteEmissionDate.EditValue = Nothing
        INDtxtAmountInsured.EditValue = Nothing
        INDseCoveragePercentage.EditValue = Nothing
        INDsleStatus.EditValue = Nothing
        INDmemoObservation.EditValue = Nothing
        ContractDetailPolicy = Nothing
        EditMode = False
    End Sub

    ''' <summary>
    ''' Llena los datasource de los controles con infromación quemada
    ''' </summary>
    Private Sub InitializeSearch()
        Dim ListStatus = New List(Of Tuple(Of Boolean, String))
        ListStatus.Add(New Tuple(Of Boolean, String)(True, "Activo"))
        ListStatus.Add(New Tuple(Of Boolean, String)(False, "Inactivo"))
        INDsleStatus.Properties.DataSource = ListStatus
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se ejecuta al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetailPolicy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Presenter = New PContract()
        InitializeSearch()
        If EditMode Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se ejecuta para abrir el form de polizas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFixedAssetPolicyId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFixedAssetPolicyId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1702, Nothing, True)
            INDsleFixedAssetPolicyId.Properties.DataSource = Presenter.InitializePolicy()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta para abrir el form de aseguradoras
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFixedAssetInsuranceId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFixedAssetInsuranceId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1703, Nothing, True)
            INDsleFixedAssetInsuranceId.Properties.DataSource = Presenter.InitializeInsurance()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de poliza
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFixedAssetPolicyId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFixedAssetPolicyId.QueryPopUp
        If INDsleFixedAssetPolicyId.Properties.DataSource Is Nothing Then
            INDsleFixedAssetPolicyId.Properties.DataSource = Presenter.InitializePolicy()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al desplegar el control de aseguradoras
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFixedAssetInsuranceId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFixedAssetInsuranceId.QueryPopUp
        If INDsleFixedAssetInsuranceId.Properties.DataSource Is Nothing Then
            INDsleFixedAssetInsuranceId.Properties.DataSource = Presenter.InitializeInsurance()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetailPolicy_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleFixedAssetPolicyId.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddPolicy_Click(sender As Object, e As EventArgs) Handles INDbtnAddPolicy.Click
        AddDetailNoveltyToPrincipalForm()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de poliza
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFixedAssetPolicyId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFixedAssetPolicyId.EditValueChanged
        If INDsleFixedAssetPolicyId.EditValue IsNot Nothing AndAlso IsLoadControls = False Then
            Dim policyXpo = Presenter.GetFixedAssetPolicyById(INDsleFixedAssetPolicyId.EditValue)
            If policyXpo IsNot Nothing Then
                INDsleFixedAssetInsuranceId.EditValue = policyXpo.InsuranceId.Id
                INDsleFixedAssetInsuranceId.Properties.NullText = policyXpo.InsuranceId.CodeName
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDmemoObservation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDmemoObservation.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDbtnAddPolicy.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetailPolicy_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddDetailsPolicyEventArgs
    Inherits EventArgs

    Property ContractDetailPolicy As ContractDetailPolicy

    Property EditMode As Boolean

End Class
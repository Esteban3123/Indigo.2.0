'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/12/2019
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

Public Class FrmAddDescriptions

#Region "Event"

    Public Event AddDescriptionArgs(sender As Object, e As AddDescriptionEventArgs)

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

#End Region

#Region "Variables"

    Dim Presenter As PCupsEntity

    Public EditMode As Boolean

    Public CUPSEntityContractDescriptions As CUPSEntityContractDescriptions

    Public FrmCupsEntity As FrmCupsEntity

#End Region

#Region "Methods"

    Private Async Sub AddDescriptionToPrincipalForm()
        If ValidateControls() = False Then
            Exit Sub
        End If

        If EditMode = False Then 'Si se esta agregando una descripción se valida que el cups no este parametrizado en las tablas de crystal sin descripción
            AsyncLoader(True)
            Dim result As ActionResult(Of SP_ValidateCUPSInCrystal_Result) = Nothing
            Using model As New MCupsEntity(Tag)
                result = Await model.SP_ValidateCUPSInCrystal(FrmCupsEntity.Code)
                If result.StateResult = False Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If
            End Using
            AsyncLoader(False)
        End If

        If FrmCupsEntity.ListCUPSEntityContractDescriptions IsNot Nothing AndAlso FrmCupsEntity.ListCUPSEntityContractDescriptions.Count > 0 Then
            If EditMode = False Then
                If (From x In FrmCupsEntity.ListCUPSEntityContractDescriptions Where x.ContractDescriptionId = INDsleDescription.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La descripción " + INDsleDescription.Text + " ya existe en la lista"
                    INDsleDescription.Focus()
                    Exit Sub
                End If
            Else
                If (From x In FrmCupsEntity.ListCUPSEntityContractDescriptions Where x.ContractDescriptionId = INDsleDescription.EditValue AndAlso INDsleDescription.EditValue <> CUPSEntityContractDescriptions.ContractDescriptionId Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La descripción " + INDsleDescription.Text + " ya existe en la lista"
                    INDsleDescription.Focus()
                    Exit Sub
                End If
            End If
        End If

        If EditMode = False Then
            If FrmCupsEntity.ListDeleteCUPSEntityContractDescriptions IsNot Nothing AndAlso FrmCupsEntity.ListDeleteCUPSEntityContractDescriptions.Count > 0 Then
                CUPSEntityContractDescriptions = (From x In FrmCupsEntity.ListDeleteCUPSEntityContractDescriptions Where x.ContractDescriptionId = INDsleDescription.EditValue).FirstOrDefault()
                If CUPSEntityContractDescriptions Is Nothing Then
                    CUPSEntityContractDescriptions = New CUPSEntityContractDescriptions()
                End If
            Else
                CUPSEntityContractDescriptions = New CUPSEntityContractDescriptions()
            End If
        End If
        With CUPSEntityContractDescriptions
            .ContractDescriptionId = INDsleDescription.EditValue
            .DescriptionCodeName = INDsleDescription.Text
            .CupsSubgroupId = INDsleSubgroup.EditValue
            .CupsSubgroupCodeName = INDsleSubgroup.Text
            .BillingGroupId = INDsleBillingGroup.EditValue
            .BillingGroupCodeName = INDsleBillingGroup.Text
            .BillingConceptId = INDsleBillingConcept.EditValue
            .BillingConceptCodeName = INDsleBillingConcept.Text
            .IsDelete = 0
        End With

        Dim args As New AddDescriptionEventArgs
        args.CUPSEntityContractDescriptions = CUPSEntityContractDescriptions
        args.EditMode = EditMode
        RaiseEvent AddDescriptionArgs(Nothing, args)
        CleanControls()
        INDsleDescription.Focus()
    End Sub

    Private Sub LoadControls()
        With CUPSEntityContractDescriptions
            INDsleDescription.EditValue = .ContractDescriptionId
            INDsleDescription.Properties.NullText = .DescriptionCodeName
            INDsleSubgroup.EditValue = .CupsSubgroupId
            INDsleSubgroup.Properties.NullText = .CupsSubgroupCodeName
            INDsleBillingGroup.EditValue = .BillingGroupId
            INDsleBillingGroup.Properties.NullText = .BillingGroupCodeName
            INDsleBillingConcept.EditValue = .BillingConceptId
            INDsleBillingConcept.Properties.NullText = .BillingConceptCodeName
        End With
    End Sub

    Private Sub CleanControls()
        INDsleDescription.EditValue = Nothing
        INDsleDescription.Properties.NullText = String.Empty
        INDsleSubgroup.EditValue = Nothing
        INDsleSubgroup.Properties.NullText = String.Empty
        INDsleBillingGroup.EditValue = Nothing
        INDsleBillingGroup.Properties.NullText = String.Empty
        INDsleBillingConcept.EditValue = Nothing
        INDsleBillingConcept.Properties.NullText = String.Empty
        CUPSEntityContractDescriptions = Nothing
        EditMode = False
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmAddDescriptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Presenter = New PCupsEntity()
        If EditMode Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleDescription_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDescription.QueryPopUp
        If INDsleDescription.Properties.DataSource Is Nothing Then
            INDsleDescription.Properties.DataSource = Presenter.InitializeDescriptions()
        End If
    End Sub

    Private Sub INDsleSubgroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSubgroup.QueryPopUp
        If INDsleSubgroup.Properties.DataSource Is Nothing Then
            INDsleSubgroup.Properties.DataSource = Presenter.InitializeSubgroup()
        End If
    End Sub

    Private Sub INDsleBillingGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBillingGroup.QueryPopUp
        If INDsleBillingGroup.Properties.DataSource Is Nothing Then
            INDsleBillingGroup.Properties.DataSource = Presenter.InitializeBillingGroupDescriptions()
        End If
    End Sub

    Private Sub INDsleBillingConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBillingConcept.QueryPopUp
        If INDsleBillingConcept.Properties.DataSource Is Nothing Then
            INDsleBillingConcept.Properties.DataSource = Presenter.InitializeBillingConcept()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleDescription_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDescription.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2096, Nothing, True)
            INDsleDescription.Properties.DataSource = Presenter.InitializeDescriptions()
        End If
    End Sub

    Private Sub INDsleSubgroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSubgroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(969, Nothing, True)
            INDsleSubgroup.Properties.DataSource = Presenter.InitializeSubgroup()
        End If
    End Sub

    Private Sub INDsleBillingGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBillingGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1503, Nothing, True)
            INDsleBillingGroup.Properties.DataSource = Presenter.InitializeBillingGroupDescriptions()
        End If
    End Sub

    Private Sub INDsleBillingConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBillingConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(749, Nothing, True)
            INDsleBillingConcept.Properties.DataSource = Presenter.InitializeBillingConcept()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddDescriptionToPrincipalForm()
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmAddDescriptions_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleDescription.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDsleBillingConcept_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsleBillingConcept.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDbtnAdd.Focus()
        End If
    End Sub

    Private Sub FrmAddDescriptions_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddDescriptionEventArgs
    Inherits EventArgs

    Property CUPSEntityContractDescriptions As CUPSEntityContractDescriptions

    Property EditMode As Boolean

End Class
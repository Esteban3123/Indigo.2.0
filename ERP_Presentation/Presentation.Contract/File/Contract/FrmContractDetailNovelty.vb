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

Public Class FrmContractDetailNovelty

#Region "Event"

    Public Event AddDetailsNoveltyArgs(sender As Object, e As AddDetailsNoveltyEventArgs)

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
    ''' Entidad del detalle de novedades del contrato
    ''' </summary>
    Public ContractDetailNovelty As ContractDetailNovelty

    ''' <summary>
    ''' Obtiene o establece la descripción del contrato al cual pertenece la novedad
    ''' </summary>
    Public ContractNumberName As String

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

#Region "Methods"

    ''' <summary>
    ''' Agrega el detalle al formulario principal
    ''' </summary>
    Private Sub AddDetailNoveltyToPrincipalForm()
        If ValidateControls() = False Then
            Exit Sub
        End If

        If EditMode = False Then
            ContractDetailNovelty = New ContractDetailNovelty()
        End If
        With ContractDetailNovelty
            .ContractNumberName = ContractNumberName
            .NoveltyDate = INDdteNoveltyDate.EditValue
            .NoveltySource = INDsleNoveltySource.EditValue
            .Name = INDtxtName.EditValue
            .Status = INDsleStatus.EditValue
            .Description = INDmemoDescription.EditValue
        End With

        Dim args As New AddDetailsNoveltyEventArgs
        args.ContractDetailNovelty = ContractDetailNovelty
        args.EditMode = EditMode
        RaiseEvent AddDetailsNoveltyArgs(Nothing, args)
        CleanControls()
        INDdteNoveltyDate.Focus()
    End Sub

    ''' <summary>
    ''' Asigna los datos a los campos
    ''' </summary>
    Private Sub LoadControls()
        With ContractDetailNovelty
            INDdteNoveltyDate.EditValue = .NoveltyDate
            INDsleNoveltySource.EditValue = .NoveltySource
            INDtxtName.EditValue = .Name
            INDsleStatus.EditValue = .Status
            INDmemoDescription.EditValue = .Description
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles del modal
    ''' </summary>
    Private Sub CleanControls()
        INDdteNoveltyDate.EditValue = Nothing
        INDsleNoveltySource.EditValue = Nothing
        INDtxtName.EditValue = Nothing
        INDsleStatus.EditValue = Nothing
        INDmemoDescription.EditValue = Nothing
        ContractDetailNovelty = Nothing
        EditMode = False
    End Sub

    ''' <summary>
    ''' Llena los datasource de los controles con infromación quemada
    ''' </summary>
    Private Sub InitializeSearch()
        Dim ListNoveltySource = New List(Of Tuple(Of Integer, String))
        ListNoveltySource.Add(New Tuple(Of Integer, String)(1, "Cliente"))
        ListNoveltySource.Add(New Tuple(Of Integer, String)(2, "Prestador"))
        INDsleNoveltySource.Properties.DataSource = ListNoveltySource

        Dim ListStatus = New List(Of Tuple(Of Integer, String))
        ListStatus.Add(New Tuple(Of Integer, String)(1, "Aprobada"))
        ListStatus.Add(New Tuple(Of Integer, String)(2, "Rechazada"))
        ListStatus.Add(New Tuple(Of Integer, String)(3, "Pendiente"))
        ListStatus.Add(New Tuple(Of Integer, String)(4, "En Proceso"))
        ListStatus.Add(New Tuple(Of Integer, String)(5, "Terminada"))
        INDsleStatus.Properties.DataSource = ListStatus
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetailNovelty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Presenter = New PContract()
        InitializeSearch()
        If EditMode Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetailNovelty_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDdteNoveltyDate.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddNovelty_Click(sender As Object, e As EventArgs) Handles INDbtnAddNovelty.Click
        AddDetailNoveltyToPrincipalForm()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDmemoDescription_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDmemoDescription.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDbtnAddNovelty.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractDetailNovelty_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddDetailsNoveltyEventArgs
    Inherits EventArgs

    Property ContractDetailNovelty As ContractDetailNovelty

    Property EditMode As Boolean

End Class
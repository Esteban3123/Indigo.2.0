'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/05/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.MedicalFeesRepository
Imports Presentation.Base

#End Region

Public Class FrmSelectContract

#Region "Builder"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(code As String, id As Integer, list As XPCollection)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        HealthProfessionalCode = code
        MedicalFeesContractId = id
        ListHealthProfessionalContractXpo = list
        LoadDatasourceGridContracts()
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado de contratos que tiene asociado el médico
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListHealthProfessionalContractXpo As XPCollection

    ''' <summary>
    ''' Evento que se ejecuta al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event SelectOptionEvent(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MedicalFees"

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el código del médico
    ''' </summary>
    ''' <remarks></remarks>
    Dim _healthProfessionalCode As String
    Public Property HealthProfessionalCode As String
        Get
            Return _healthProfessionalCode
        End Get
        Set(value As String)
            _healthProfessionalCode = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el id del contrato que viene del form de causacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _medicalFeesContractId As Integer
    Public Property MedicalFeesContractId As Integer
        Get
            Return _medicalFeesContractId
        End Get
        Set(value As Integer)
            _medicalFeesContractId = value
        End Set
    End Property

#End Region

#Region "Methods"

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
    ''' Metodo que carga la rejilla con los contratos
    ''' que tiene asociado el médico para poder causar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDatasourceGridContracts()
        If ListHealthProfessionalContractXpo IsNot Nothing AndAlso ListHealthProfessionalContractXpo.Count > 0 Then
            If MedicalFeesContractId > 0 Then
                Dim itemXpo As HealthProfessionalContractXpo = (From l In ListHealthProfessionalContractXpo Where l.MedicalFeesContractId.Id = MedicalFeesContractId Select l).FirstOrDefault
                If itemXpo IsNot Nothing Then
                    itemXpo.SelectOption = True
                End If
            End If
        End If
        INDgcSelectContract.DataSource = Nothing
        INDgcSelectContract.DataSource = ListHealthProfessionalContractXpo
    End Sub

    ''' <summary>
    ''' Metodo que llama al evento y cierra el form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CallEventSelectOption()
        Dim itemXpo As HealthProfessionalContractXpo = (From l In ListHealthProfessionalContractXpo Where l.SelectOption = True Select l).FirstOrDefault
        If itemXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectOneOption", NAME_MODULE)
            Exit Sub
        End If

        Dim args As SelectContractEventArgs = New SelectContractEventArgs
        args.MedicalFeesContractId = itemXpo.MedicalFeesContractId.Id
        RaiseEvent SelectOptionEvent(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "Events"

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor
    ''' del check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckSelection_EditValueChanged(sender As Object, e As EventArgs) Handles INDrepCheckSelection.EditValueChanged
        Dim itemXpo As HealthProfessionalContractXpo = ViewSelectOption.GetFocusedRow
        If itemXpo IsNot Nothing Then
            Dim control As DevExpress.XtraEditors.CheckEdit = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
            If control.EditValue = True Then
                For Each item As HealthProfessionalContractXpo In ListHealthProfessionalContractXpo
                    item.SelectOption = False
                Next
                itemXpo.SelectOption = True
                INDgcSelectContract.RefreshDataSource()
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        CallEventSelectOption()
    End Sub

#End Region

#Region "KeyDown"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListHealthProfessionalContractXpo = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSelectContract_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class
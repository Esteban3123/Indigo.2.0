'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/10/2020
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Threading
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP
#End Region

Public Class FrmPatientExternalCareCenter
    Implements IPatientExternalCareCenter

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un paciente de un centro de atención externo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddPatientExternalCareCenterArgs(sender As Object, e As AddPatientExternalCareCenter)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PPatientExternalCareCenter

    ''' <summary>
    ''' Cabacera
    ''' </summary>
    Dim ExternalCareCenterPatient As PatientExternalCareCenter

    ''' <summary>
    ''' Cabacera
    ''' </summary>
    Dim formEditing As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String 'Implements ICrudBase.Mensaje
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
    ''' Estado
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Boolean Implements IPatientExternalCareCenter.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPatientExternalCareCenter.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPatientExternalCareCenter.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnIdentification.Enabled = value
            INDsleIdentificationType.Enabled = value
            INDtxtName.Enabled = value
            INDtxtLastName.Enabled = value
            INDSleGender.Enabled = value
            INDTxtPatientMobileNumber.Enabled = value
            INDTxtPatientEmail.Enabled = value
            INDTxtFunctionalUnit.Enabled = value
            INDTxtBed.Enabled = value
            INDlyRoot.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IPatientExternalCareCenter.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el género
    ''' </summary>
    ''' <returns></returns>
    Public Property ListGenderDatasource As XPInstantFeedbackSource Implements IPatientExternalCareCenter.ListGenderDatasource
        Get
            Return INDSleGender.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleGender.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el tipo de identificacion
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationTypeDatasource As XPInstantFeedbackSource Implements IPatientExternalCareCenter.IdentificationTypeDatasource
        Get
            Return INDsleIdentificationType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdentificationType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Número de identificación del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationNumber As String Implements IPatientExternalCareCenter.IdentificationNumber
        Get
            Return INDbtnIdentification.Text
        End Get
        Set(value As String)
            INDbtnIdentification.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de identificación
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationType As Integer Implements IPatientExternalCareCenter.IdentificationType
        Get
            Return INDsleIdentificationType.EditValue
        End Get
        Set(value As Integer)
            INDsleIdentificationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientName As String Implements IPatientExternalCareCenter.Name
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Apellido del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property LastName As String Implements IPatientExternalCareCenter.LastName
        Get
            Return INDtxtLastName.EditValue
        End Get
        Set(value As String)
            INDtxtLastName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Género del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property Gender As Integer Implements IPatientExternalCareCenter.Gender
        Get
            Return INDSleGender.EditValue
        End Get
        Set(value As Integer)
            INDSleGender.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Número de celular del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientMobileNumber As String Implements IPatientExternalCareCenter.PatientMobileNumber
        Get
            Return INDTxtPatientMobileNumber.EditValue
        End Get
        Set(value As String)
            INDTxtPatientMobileNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Email del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientEmail As String Implements IPatientExternalCareCenter.PatientEmail
        Get
            Return INDTxtPatientEmail.EditValue
        End Get
        Set(value As String)
            INDTxtPatientEmail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad funcional del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property FunctionalUnit As String Implements IPatientExternalCareCenter.FunctionalUnit
        Get
            Return INDTxtFunctionalUnit.EditValue
        End Get
        Set(value As String)
            INDTxtFunctionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Cama del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property Bed As String Implements IPatientExternalCareCenter.Bed
        Get
            Return INDTxtBed.EditValue
        End Get
        Set(value As String)
            INDTxtBed.EditValue = value
        End Set
    End Property

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor del modal
    ''' </summary>
    ''' <param name="PatientIdentificationNumber"></param>
    Public Sub New(PatientIdentificationNumber As String, NewPatientExternalCareCenter As PatientExternalCareCenter, EditMode As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdentificationNumber = PatientIdentificationNumber
        ExternalCareCenterPatient = NewPatientExternalCareCenter
        formEditing = EditMode
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Sub AssigningValues()
        If ExternalCareCenterPatient Is Nothing Then
            ExternalCareCenterPatient = New PatientExternalCareCenter
        End If
        With ExternalCareCenterPatient
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .IdentificationNumber = IdentificationNumber
            .IdentificationTypeId = IdentificationType
            .DescriptionIdentificationType = INDsleIdentificationType.Text
            .Name = PatientName
            .LastName = LastName
            .GenderTypeId = Gender
            .GenderCodeName = INDSleGender.Text
            .PatientMobileNumber = PatientMobileNumber
            .PatientEmail = PatientEmail
            .ExternalFunctionalUnit = FunctionalUnit
            .PatientBed = Bed

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        ActionsOnControls = False

        ExternalCareCenterPatient = Nothing
        Status = True
        IdentificationNumber = Nothing
        IdentificationType = Nothing
        PatientName = Nothing
        LastName = Nothing
        Gender = Nothing
        PatientMobileNumber = Nothing
        PatientEmail = Nothing
        FunctionalUnit = Nothing
        Bed = Nothing

        Me.IdentificationTypeDatasource = Nothing
        Me.ListGenderDatasource = Nothing

        INDlyRoot.EndUpdate()
    End Sub

    '==================================================================

    ''' <summary>
    ''' Carga la información del paciente
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        If String.IsNullOrWhiteSpace(IdentificationNumber) Then
            Mensaje(EeventViewerImages.Advertencia) = "Ingrese el número de documento del paciente"
            Return
        End If

        AsyncLoader(True)
        Dim externalPatient As PatientExternalCareCenter = Nothing

        If ExternalCareCenterPatient IsNot Nothing Then
            externalPatient = ExternalCareCenterPatient
        Else
            externalPatient = Await LoadPatientFromDatabase()
            If externalPatient.Id > 0 Then
                formEditing = True
            End If
        End If

        If externalPatient IsNot Nothing And formEditing Then 'externalPatient.Id > 0 Then
            PopulatePatientData(externalPatient)
        End If

        AsyncLoader(False)
        ActionsOnControls = (externalPatient IsNot Nothing)
    End Function

    Private Async Function LoadPatientFromDatabase() As Task(Of PatientExternalCareCenter)
        Using model As New MPatientExternalCareCenter(CStr(Me.Tag))
            Return Await model.GetPatientExternalCareCenter(INDbtnIdentification.EditValue)
        End Using
    End Function

    Private Sub PopulatePatientData(externalPatient As PatientExternalCareCenter)
        IdentificationType = externalPatient.IdentificationTypeId
        INDsleIdentificationType.Properties.NullText = externalPatient.DescriptionIdentificationType
        PatientName = externalPatient.Name
        LastName = externalPatient.LastName
        Gender = externalPatient.GenderTypeId
        INDSleGender.Properties.NullText = Presenter.GetGenderTypeById(Gender).CodeName
        Status = externalPatient.Status
        PatientMobileNumber = externalPatient.PatientMobileNumber
        PatientEmail = externalPatient.PatientEmail
        FunctionalUnit = externalPatient.ExternalFunctionalUnit
        Bed = externalPatient.PatientBed
    End Sub

    '==================================================================

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmPatientExternalCareCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PPatientExternalCareCenter(Me)
        LoadStatus()

        If IdentificationNumber Is Nothing Then
            CleanControls()
        Else
            Await Me.LoadControls()
        End If

    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se ejecuta al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPatientExternalCareCenter_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleIdentificationType.Focus()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Enter del campo código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnIdentification_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnIdentification.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter AndAlso Not String.IsNullOrEmpty(IdentificationNumber) Then
            Await Me.LoadControls()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    '''' <summary>
    '''' Evento que se ejecuta al abrir el select del campo Tipo de identificación
    '''' </summary>
    Private Sub INDsleIdentificationType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIdentificationType.QueryPopUp
        If Me.IdentificationTypeDatasource Is Nothing Then
            Presenter.InitializaListIdentificationType()
        End If
    End Sub

    '''' <summary>
    '''' Evento que se ejecuta al abrir el select del campo Género
    '''' </summary>
    Private Sub INDSleGender_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleGender.QueryPopUp
        If Me.ListGenderDatasource Is Nothing Then
            ListGenderDatasource = Presenter.InitializaListGender()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If Not ValidateControls() Then
            Exit Sub
        End If

        AssigningValues()

        Dim args As New AddPatientExternalCareCenter
        args.PatientExternalCareCenterDetail = ExternalCareCenterPatient
        RaiseEvent AddPatientExternalCareCenterArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#End Region

End Class

Public Class AddPatientExternalCareCenter
    Inherits EventArgs

    Property PatientExternalCareCenterDetail As PatientExternalCareCenter

End Class
'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/10/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.MedicalFees.MVP

#End Region

Public Class FrmChangeHealthProfessional
    Implements IChangeHealthProfessional

#Region "Builder"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="entityXpoViewSurgicalAndPackage">Entidad xpo Qx</param>
    ''' <param name="entityXpoViewNoSurgical">Entidad xpo NoQx</param>
    ''' <param name="_session">Sesion de la entidad xpo que envio como parametro</param>
    ''' <param name="viewGridSurgical">Variable para saber si esta en la vista de la rejilla de Qx o NoQx (True=Rejilla Qx, False=Rejilla NoQx)</param>
    ''' <remarks></remarks>
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id del medico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthProfessionalId As String Implements IChangeHealthProfessional.HealthProfessionalId
        Get
            Return INDsleHealthProfessional.EditValue
        End Get
        Set(value As String)
            INDsleHealthProfessional.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del medico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthProfessionalXpo As XPInstantFeedbackSource Implements IChangeHealthProfessional.HealthProfessionalXpo
        Get
            Return INDsleHealthProfessional.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleHealthProfessional.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PChangeHealthProfessional

    ''' <summary>
    ''' Representa a la entidad xpo del medico
    ''' </summary>
    ''' <remarks></remarks>
    Dim _HealthCareProfessionalXpo As Infrastructure.Data.Xpo.CrystalRepository.HealthCareProfessionalXpo

    ''' <summary>
    ''' Representa la entidad de tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim thirdParty As Domain.Entities.ThirdParty

    ''' <summary>
    ''' Evento que cambia un médico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeHealthProfessionalEvent(sender As Object, e As EventArgs)

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Muestra el slide de los mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        If HealthProfessionalId = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir un médico."
            INDsleHealthProfessional.Focus()
            Exit Sub
        End If

        Dim args As ChangeHealthProfessionalEventArgs = New ChangeHealthProfessionalEventArgs
        args.PerformsHealthProfessionalCode = _HealthCareProfessionalXpo.CODPROSAL
        args.PerformsHealthProfessionalDescription = _HealthCareProfessionalXpo.CodeName
        args.ThirdPartyId = thirdParty.Id
        args.ThirdPartyDescription = _HealthCareProfessionalXpo.CodeName
        RaiseEvent ChangeHealthProfessionalEvent(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _HealthCareProfessionalXpo = Nothing
        thirdParty = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmChangeHealthProfessional_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PChangeHealthProfessional(Me)
        INDsleHealthProfessional.Properties.Buttons(1).Visible = False
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar por primera vez el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmChangeHealthProfessional_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleHealthProfessional.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHealthProfessional_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthProfessional.QueryPopUp
        If HealthProfessionalXpo Is Nothing Then
            Presenter.InitializeHealthProfessional()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AssigningValues()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de medico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHealthProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHealthProfessional.EditValueChanged
        If HealthProfessionalId <> Nothing Then
            _HealthCareProfessionalXpo = DirectCast(DirectCast(viewSearchHealthProfessional.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CrystalRepository.HealthCareProfessionalXpo)
            Using model As New MThirdParty("")
                thirdParty = model.GetThirdParty(_HealthCareProfessionalXpo.CODIGONIT.TrimStart("0"))
                If thirdParty.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", "Billing"), _HealthCareProfessionalXpo.CodeName)
                    HealthProfessionalId = Nothing
                End If
            End Using
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape al form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmChangeHealthProfessional_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region
    
End Class

Public Class ChangeHealthProfessionalEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Codigo del médico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PerformsHealthProfessionalCode As String

    ''' <summary>
    ''' Codigo y nombre del médico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PerformsHealthProfessionalDescription As String

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer

    ''' <summary>
    ''' Codigo y nombre del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyDescription As String

End Class
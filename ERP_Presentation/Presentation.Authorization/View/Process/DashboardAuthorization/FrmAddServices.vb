'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/06/2020
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
Imports System.Text
Imports Presentation.Authorization.MVP
Imports DevExpress.Xpo
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports DevExpress.XtraEditors.BaseCheckedListBoxControl
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Authorization
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports DevExpress.XtraSplashScreen
Imports Presentation.Billing.MVP
Imports Presentation.Common.MVP
Imports Domain.Crystal.Entities

#End Region

Public Class FrmAddServices

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardAuthorization

    ''' <summary>
    ''' Variable que me establece si controlo el cambio de valor en el search
    ''' </summary>
    Dim ControlEditValueChanged As Boolean = False

    ''' <summary>
    ''' Representa a la entidad xpo de paciente
    ''' </summary>
    Dim PatientXpo As PatientXpo

    Dim ServiceId As Integer
    Dim ServiceCode As String
    Dim ContractDescriptionId As Integer?

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
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

#Region "Event"

    ''' <summary>
    ''' Evento que se ejecuta para realizar las acciones del formulario principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ReturnOpenItemModalArgs(sender As Object, e As OpenItemEventArgs)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el form para buscar el paciente
    ''' </summary>
    Public Sub OpenSearch()
        Using Model As New MAdmissions(Me.Tag)
            Using FormSearchObjects As New FrmBusqueda
                AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
                With FormSearchObjects
                    .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "IPCODPACI", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                                  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "IPNOMCOMP", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)}}.ToList
                    .ValorSolicitado = "IPCODPACI"
                    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients
                    .Text = "Paciente"
                End With
                Dim frm As New FrmTransparent(FormSearchObjects, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                frm.ShowDialog(Me)
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para obtener el paciente seleccionado
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        Try
            AsyncLoader(True)

            'Se obtiene el paciente
            Dim patientCode As String = ReturnValue.ToString().Split("-")(0)
            PatientXpo = Presenter.GetPatientByNit(patientCode)
            If PatientXpo Is Nothing Then 'Si no existe el paciente
                Mensaje(EeventViewerImages.Advertencia) = "No existe un paciente en Indigo Vie para el nit " + INDbePatient.Text
                AsyncLoader(False)
                INDbePatient.Text = String.Empty
                PatientXpo = Nothing
                Exit Sub
            End If

            'Se asigna los valores del paciente al control
            INDbePatient.Text = PatientXpo.IPCODPACI.Trim() + " - " + PatientXpo.IPNOMCOMP.Trim()

            'Se valida que el paciente tenga asociado un grupo de atención
            If PatientXpo.GENCAREGROUP = Nothing OrElse PatientXpo.GENCAREGROUP = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El paciente " + INDbePatient.Text + " no tiene un grupo de atención asociado"
                AsyncLoader(False)
                INDbePatient.Text = String.Empty
                PatientXpo = Nothing
                Exit Sub
            End If

            ControlEditValueChanged = True

            'Se obtiene el grupo de atención asociado al paciente
            Dim careGroupXpo = Presenter.GetCareGroupById(PatientXpo.GENCAREGROUP)
            INDsleCareGroup.EditValue = careGroupXpo.Id
            INDsleCareGroup.Properties.NullText = careGroupXpo.CodeName

            'Se valida si el grupo de atención tiene asociado un contrato
            INDlyItemContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If careGroupXpo.ContractId IsNot Nothing Then
                INDlyItemContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleContract.EditValue = careGroupXpo.ContractId.Id
                INDsleContract.Properties.NullText = careGroupXpo.ContractId.CodeContractName
            End If

            'Se valida si el paciente tiene asociado una entidad
            INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If PatientXpo.GENCONENTITY <> Nothing AndAlso PatientXpo.GENCONENTITY > 0 Then
                Dim healthXpo = Presenter.GetHealthAdministratorById(PatientXpo.GENCONENTITY)
                INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleHealthAdministrator.EditValue = healthXpo.Id
                INDsleHealthAdministrator.Properties.NullText = healthXpo.CodeName

                'Si el grupo de atención es EAPB con contrato = 1
                INDsleHealthAdministrator.Properties.ReadOnly = False
                If careGroupXpo.CareGroupType = 1 Then
                    INDsleHealthAdministrator.Properties.ReadOnly = True
                End If
            End If

            ControlEditValueChanged = False

            AsyncLoader(False)

            INDsleCareGroup.Focus()
        Catch ex As Exception
            AsyncLoader(False)
            INDbePatient.Text = String.Empty
            PatientXpo = Nothing
            ControlEditValueChanged = False
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Inicializa los search que son con datos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listType As New List(Of Tuple(Of Integer, String))
        listType.Add(New Tuple(Of Integer, String)(1, "Servicio"))
        listType.Add(New Tuple(Of Integer, String)(2, "Producto"))
        INDsleType.Properties.DataSource = listType.ToList()
    End Sub

    ''' <summary>
    ''' Valida los controles
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForm() As String
        Dim errors As New StringBuilder

        If INDsleCareCenter.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDsleCareCenter.EditValue) Then
            errors.AppendLine("Seleccione un centro de atención")
        End If

        If INDsleFunctionalUnit.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una unidad funcional")
        End If

        If PatientXpo Is Nothing Then
            errors.AppendLine("Seleccione un paciente")
        End If

        If INDsleCareGroup.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un grupo de atención")
        End If

        If INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleHealthAdministrator.EditValue Is Nothing Then
                errors.AppendLine("Seleccione una entidad")
            End If
        End If

        If INDsleType.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un tipo")
        End If

        If INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleCUPS.EditValue Is Nothing Then
                errors.AppendLine("Seleccione un CUPS")
            End If
        End If

        If INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleProduct.EditValue Is Nothing Then
                errors.AppendLine("Seleccione un producto")
            End If
        End If

        If INDsleSource.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un origen")
        End If

        If INDseQuantity.EditValue Is Nothing OrElse INDseQuantity.EditValue = 0 Then
            errors.AppendLine("Ingrese una cantidad")
        End If

        If INDsleProfessional.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDsleProfessional.EditValue) Then
            errors.AppendLine("Seleccione un profesional")
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Guarda una solicitud
    ''' </summary>
    Private Async Sub Guardar()
        Dim errors = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        Dim TraceabilityPaperwork = New TraceabilityPaperwork
        With TraceabilityPaperwork
            .Folio = Nothing
            .ServiceCode = ServiceCode
            .Type = CInt(INDsleType.EditValue)
            .PatientCode = PatientXpo.IPCODPACI
            .CareCenterCode = INDsleCareCenter.EditValue
            .RequestDate = GetDateServer()
            .RequestQuantity = CInt(INDseQuantity.EditValue)
            .FunctionalUnitCode = INDsleFunctionalUnit.Text.Split("-")(0)
            .AuthorizationSourceId = INDsleSource.EditValue
            .IsManual = 1
            .CareGroupId = INDsleCareGroup.EditValue
            .HealthAdministratorId = Nothing
            If INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .HealthAdministratorId = INDsleHealthAdministrator.EditValue
            End If
            .ProfessionalCode = INDsleProfessional.EditValue
            .ServiceId = ServiceId
            .ContractDescriptionId = ContractDescriptionId
            .Status = 1
        End With

        Me.AsyncLoader(True)

        Try
            Using model As New MDashboardAuthorization("")
                Dim result = Await model.SaveTraceabilityPaperwork({TraceabilityPaperwork}.ToList())
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")

                    Dim args As New OpenItemEventArgs
                    args.Action = 0
                    RaiseEvent ReturnOpenItemModalArgs(Nothing, args)

                    Me.AsyncLoader(False)

                    CleanControls()
                    INDsleCareCenter.Focus()
                Else
                    Me.AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Limpia los controles del form
    ''' </summary>
    Private Sub CleanControls()
        ServiceId = Nothing
        ServiceCode = Nothing
        ContractDescriptionId = Nothing

        INDsleCareCenter.EditValue = Nothing
        INDsleCareCenter.Properties.NullText = String.Empty
        INDsleFunctionalUnit.EditValue = Nothing
        INDsleFunctionalUnit.Properties.NullText = String.Empty
        PatientXpo = Nothing
        INDbePatient.Text = String.Empty
        INDsleCareGroup.EditValue = Nothing
        INDsleCareGroup.Properties.NullText = String.Empty
        INDsleContract.EditValue = Nothing
        INDsleContract.Properties.NullText = String.Empty
        INDlyItemContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleHealthAdministrator.EditValue = Nothing
        INDsleHealthAdministrator.Properties.NullText = String.Empty
        INDsleType.EditValue = Nothing
        INDsleCUPS.EditValue = Nothing
        INDsleCUPS.Properties.NullText = String.Empty
        INDtxtDescription.EditValue = Nothing
        INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleProduct.EditValue = Nothing
        INDsleProduct.Properties.NullText = String.Empty
        INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleSource.EditValue = Nothing
        INDsleSource.Properties.NullText = String.Empty
        INDseQuantity.EditValue = Nothing
        INDsleProfessional.EditValue = Nothing
        INDsleProfessional.Properties.NullText = String.Empty
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PDashboardAuthorization()
        InitializeTuples()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddServices_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareCenter.Focus()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddServices_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Enter del control de paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbePatient_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbePatient.KeyDown
        If Not String.IsNullOrEmpty(INDbePatient.Text.ToString().Trim()) AndAlso e.KeyCode = System.Windows.Forms.Keys.Enter Then
            ReturnValue(INDbePatient.Text.ToString().Trim(), Nothing)
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDsleCareGroup.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el botón plus
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFunctionalUnit_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleFunctionalUnit.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(523, Nothing, True)
            If INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
                Using model As New MControlOutpatientServices("")
                    INDsleFunctionalUnit.Properties.DataSource = model.ListFunctionalUnitCareCenter(INDsleCareCenter.EditValue)
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form para buscar el paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBePatient_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbePatient.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("985", Nothing, True)
            INDsleCareGroup.Properties.DataSource = Presenter.ListCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de contratos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleContract_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContract.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(978, Nothing, True)
            INDsleContract.Properties.DataSource = Presenter.ListContractXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de entidad administradora de salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleHealthAdministrator.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(972, Nothing, True)
            INDsleHealthAdministrator.Properties.DataSource = Presenter.ListHealthAdministratorByStatus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPS_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleCUPS.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(970, Nothing, True)
            If INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
                INDsleCUPS.Properties.DataSource = Presenter.InitializeCUPS(INDsleCareCenter.EditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProduct_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleProduct.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(1034, Nothing, True)
            If INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
                INDsleProduct.Properties.DataSource = Presenter.InitializeProducts(INDsleCareCenter.EditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSource_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleSource.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(2132, Nothing, True)
            INDsleSource.Properties.DataSource = Presenter.InitializeSource()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Se ejecuta cuando se despliega el control de centro atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If INDsleCareCenter.Properties.DataSource Is Nothing Then
            Using model As New MControlOutpatientServices("")
                INDsleCareCenter.Properties.DataSource = model.ListCentersHIS()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se despliega el control de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If INDsleFunctionalUnit.Properties.DataSource Is Nothing AndAlso INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
            Using model As New MControlOutpatientServices("")
                INDsleFunctionalUnit.Properties.DataSource = model.ListFunctionalUnitCareCenter(INDsleCareCenter.EditValue)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If INDsleCareGroup.Properties.DataSource Is Nothing Then
            INDsleCareGroup.Properties.DataSource = Presenter.ListCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de contratos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleContract_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContract.QueryPopUp
        If INDsleContract.Properties.DataSource Is Nothing Then
            INDsleContract.Properties.DataSource = Presenter.ListContractXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad administradora de salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthAdministrator.QueryPopUp
        If INDsleHealthAdministrator.Properties.DataSource Is Nothing Then
            INDsleHealthAdministrator.Properties.DataSource = Presenter.ListHealthAdministratorByStatus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPS_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCUPS.QueryPopUp
        If INDsleCUPS.Properties.DataSource Is Nothing AndAlso INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
            INDsleCUPS.Properties.DataSource = Presenter.InitializeCUPS(INDsleCareCenter.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If INDsleProduct.Properties.DataSource Is Nothing AndAlso INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
            INDsleProduct.Properties.DataSource = Presenter.InitializeProducts(INDsleCareCenter.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSource_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSource.QueryPopUp
        If INDsleSource.Properties.DataSource Is Nothing Then
            INDsleSource.Properties.DataSource = Presenter.InitializeSource()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de profesional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProfessional_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProfessional.QueryPopUp
        If INDsleProfessional.Properties.DataSource Is Nothing Then
            INDsleProfessional.Properties.DataSource = Presenter.InitializeProfessional()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambia el valor del control de centro atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareCenter.EditValueChanged
        If INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
            INDsleFunctionalUnit.EditValue = Nothing
            INDsleFunctionalUnit.Properties.DataSource = Nothing

            ServiceId = Nothing
            ServiceCode = Nothing
            INDsleCUPS.EditValue = Nothing
            INDsleCUPS.Properties.DataSource = Nothing
            INDtxtDescription.EditValue = Nothing
            INDsleProduct.EditValue = Nothing
            INDsleProduct.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareGroup.EditValueChanged
        Dim careGroupXpo As Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupReportXpo = Nothing
        If INDsleCareGroup.EditValue IsNot Nothing Then
            careGroupXpo = Presenter.GetCareGroupById(INDsleCareGroup.EditValue)
        End If
        If ControlEditValueChanged = False Then
            INDlyItemContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDsleContract.EditValue = Nothing
            INDsleContract.Properties.NullText = String.Empty
            INDsleHealthAdministrator.EditValue = Nothing
            INDsleHealthAdministrator.Properties.NullText = String.Empty
            INDsleHealthAdministrator.Properties.ReadOnly = False

            If INDsleCareGroup.EditValue IsNot Nothing Then
                INDsleHealthAdministrator.Properties.DataSource = Nothing
                INDsleHealthAdministrator.Properties.NullText = String.Empty
                Select Case careGroupXpo.CareGroupType
                    Case Is = 1 'Con contrato (mostrat EAPB)
                        INDlyItemContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDsleContract.Properties.NullText = careGroupXpo.ContractId.CodeContractName
                        INDsleContract.EditValue = careGroupXpo.ContractId.Id
                        INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        INDsleHealthAdministrator.EditValue = careGroupXpo.ContractId.HealthAdministratorId.Id
                        INDsleHealthAdministrator.Properties.NullText = careGroupXpo.ContractId.HealthAdministratorId.CodeName
                        INDsleHealthAdministrator.Properties.ReadOnly = True
                    Case Is = 2 'Sin contrato (preguntar por EAPB diferente a aseguradoras)
                        INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Case Is = 4 'Aseguradoras (preguntar por EAPB = aseguradoras)
                        INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleContract_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleContract.EditValueChanged
        If INDsleContract.EditValue IsNot Nothing AndAlso ControlEditValueChanged = False Then
            Dim contractXpo = Presenter.GetContractById(INDsleContract.EditValue)
            INDsleHealthAdministrator.EditValue = contractXpo.HealthAdministratorId.Id
            INDsleHealthAdministrator.Properties.NullText = contractXpo.HealthAdministratorId.CodeName
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue IsNot Nothing Then
            If INDsleType.EditValue = 1 Then 'Servicios
                INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else 'Producto
                INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambia el valor del CUPS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPS_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCUPS.EditValueChanged
        ServiceId = Nothing
        ServiceCode = Nothing

        If INDsleCUPS.EditValue IsNot Nothing Then
            Dim cups = Presenter.GetCUPSSusceptible(INDsleCUPS.EditValue)
            ServiceId = cups.CUPSEntityId
            ServiceCode = cups.CUPSEntityCode
            ContractDescriptionId = cups.ContractDescriptionId
            INDtxtDescription.EditValue = cups.ContractDescriptionCodeName
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambia el valor del Producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProduct.EditValueChanged
        ServiceId = Nothing
        ServiceCode = Nothing

        If INDsleProduct.EditValue IsNot Nothing Then
            Dim product = Presenter.GetProductSusceptible(INDsleProduct.EditValue, INDsleCareCenter.EditValue)
            ServiceId = product.InventoryProductId
            ServiceCode = product.InventoryProductCode
            ContractDescriptionId = Nothing
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveWithoutUndoAndFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

#End Region

End Class
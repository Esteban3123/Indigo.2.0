'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/02/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Extension

Imports Domain.Entities
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Presentation.Security.MVP
Imports Domain.Security.Entities
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports Presentation.Controls.MVP
Imports Domain.Entities.Service
Imports DevExpress.Utils.Menu
Imports System.Globalization
Imports DevExpress.Data.Linq
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.Utils
Imports DevExpress.XtraLayout
Imports Presentation.Common.MVP
Imports Domain.Crystal.Entities
Imports Presentation.Billing.MVP
Imports Presentation.Contract.MVP
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic
Imports System.Collections.Concurrent
Imports System.Drawing
Imports Presentation.Resources
Imports System.Threading
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class FrmAccountControlAmbulatory
    Implements IAccountControlAmbulatory

#Region "ICrud"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public Sub Buscar() Implements IcrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Presentador del formulario
    ''' </summary>
    Dim Presenter As PAccountControlAmbulatory

    ''' <summary>
    ''' Variable que representa la entidad de parámetros de contratos
    ''' </summary>
    Dim _settingsContractXpo As Infrastructure.Data.Xpo.ContractRepository.SettingsContractXpo

    ''' <summary>
    ''' Permite saber si se carga los ingresos abiertos o facturados
    ''' </summary>
    Private StateOpen As Boolean

    ''' <summary>
    ''' Variable utilizada para no permitir siempre asignar el datasource al control de admisiones
    ''' </summary>
    Private OldStateOpen As Boolean = True

    ''' <summary>
    ''' Representa al objeto del ingreso
    ''' </summary>
    Private _admissionObjectPOCO As Object

    ''' <summary>
    ''' Formulario de detalles del ingreso
    ''' </summary>
    Dim FormDetail As FrmAccountControlAmbulatoryDetail

    ''' <summary>
    ''' Obtiene el número del ingreso del search
    ''' </summary>
    Dim AdmissionCode As String

    ''' <summary>
    ''' Obtiene la identificación del paciente del search
    ''' </summary>
    Dim PatientCode As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteOutpatientServices As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteOutpatientServices)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios intrahospitalarios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteIntrahospitalServices As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteIntrahospitalServices)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteServices As Boolean
        Get
            Dim result As Boolean
            If Me._admissionObjectPOCO IsNot Nothing Then
                If Me._admissionObjectPOCO.AdmissionType = 1 Then
                    result = Me.RequestQuoteOutpatientServices
                Else
                    result = Me.RequestQuoteIntrahospitalServices
                End If
            End If
            Return result
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Ejecuta la consulta para traer los datos
    ''' </summary>
    Private Sub GetListCareCenterHIS()
        INDviewSearchCareCenter.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim result As ActionResult(Of List(Of SP_ListCareCenterHis_Result)) = Nothing
                                  Using model As New MPatientDeparture(Me.Tag)
                                      result = model.SP_ListCareCenterHisNotAsync()
                                  End Using

                                  If Not tokenAsync.IsCancellationRequested Then
                                      INDsleCareCenter.SafeInvoke(Sub()
                                                                      INDsleCareCenter.Properties.DataSource = result.ObjectEmbbeded
                                                                      INDviewSearchCareCenter.HideLoadingPanel()
                                                                  End Sub)
                                  End If
                              End Sub, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Obtiene los ingresos por centro de atención
    ''' </summary>
    Private Sub GetAdmissions()
        If INDsleCareCenter.EditValue = Nothing OrElse INDsleCareCenter.EditValue = "" Then
            Exit Sub
        End If

        INDbtnRefresh.Enabled = False
        INDviewAdmissions.ShowLoadingPanel()
        INDsleCareCenter.Properties.ReadOnly = True
        Task.Factory.StartNew(Sub()
                                  Dim ListAdmissions = Presenter.ListAdmissions(INDsleCareCenter.EditValue)

                                  If Not tokenAsync.IsCancellationRequested Then
                                      INDgcAdmissions.SafeInvoke(Sub()
                                                                     INDgcAdmissions.DataSource = Nothing
                                                                     INDgcAdmissions.DataSource = ListAdmissions
                                                                     INDsleCareCenter.Properties.ReadOnly = False
                                                                     INDbtnRefresh.Enabled = True
                                                                     INDviewAdmissions.HideLoadingPanel()
                                                                 End Sub)
                                  End If
                              End Sub, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Carga la información del ingreso en el popup
    ''' </summary>
    ''' <param name="admissionObject"></param>
    Private Async Sub LoadControlsInfo(admissionObject As Object)
        _admissionObjectPOCO = admissionObject
        AdmissionCode = admissionObject.AdmissionCode.ToString().Trim()
        PatientCode = admissionObject.PatientCode.ToString().Trim()
        If _admissionObjectPOCO IsNot Nothing Then
            'Asignamos los datos a los diferentes controles
            Me.INDSleAdmissionNumber2.DisplayNullText = String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admissionObject.AdmissionCode.ToString().Trim(), admissionObject.PatientCode.ToString().Trim(), admissionObject.PatientName.ToString().Trim())
            'DATOS DEL PACIENTE
            Dim documentType As String = String.Empty
            Select Case admissionObject.PatientDocumentType.ToString().Trim()
                Case "1"
                    documentType = "CC"
                Case "2"
                    documentType = "CE"
                Case "3"
                    documentType = "TI"
                Case "4"
                    documentType = "RC"
                Case "5"
                    documentType = "PA"
                Case "6"
                    documentType = "AS"
                Case "7"
                    documentType = "MS"
                Case "8"
                    documentType = "NU"
            End Select
            Me.TxtContact.Text = admissionObject.PatientPhone
            Me.TxtPatientCode.Text = If(admissionObject.PatientCode Is Nothing, String.Empty, String.Concat(documentType, " - ", admissionObject.PatientCode.ToString().Trim()))
            Me.TxtPatientName.Text = If(admissionObject.PatientName Is Nothing, String.Empty, admissionObject.PatientName.ToString().Trim())
            Me.TxtPatientBirth.Text = Convert.ToDateTime(admissionObject.PatientBirth).ToString(SessionValues.Instance.Culture)
            Me.TxtPatientAge.Text = Utils.AgeToString(Convert.ToDateTime(admissionObject.PatientBirth))
            Me.TxtPatientType.EditValue = Convert.ToInt32(admissionObject.PatientType)
            Me.TxtAfiliationType.EditValue = Convert.ToInt32(admissionObject.PatientAfiliation)
            'Using model As New MCareGroup(Me.Tag)
            If admissionObject.PatientCareGroupId IsNot Nothing AndAlso admissionObject.CareGroupCodeName IsNot Nothing Then
                'Dim caregroup As CareGroup = (model.GetCareGroupByIdSimple(CType(admissionObject.PatientCareGroupId, Integer))).ObjectEmbbeded
                'If caregroup IsNot Nothing AndAlso caregroup.Id > 0 Then
                Select Case admissionObject.CareGroupType
                    Case 1, 2, 4 'EAPB con contrato
                        Me.TxtPatientEntityName.Text = String.Concat(admissionObject.PatientEntityCode, " - ", admissionObject.PatientEntityName)
                    Case 3 'Particulares
                        LiEntity.Text = ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", GetType(CtrFolio).Name)
                        'tercero
                        Using modelT As New MThirdParty(Me.Tag)
                            Dim third As Domain.Entities.ThirdParty = modelT.GetThirdParty(admissionObject.PatientCode.ToString().Trim())
                            Me.TxtPatientEntityName.Text = String.Concat(third.Nit, " - ", third.Name)
                        End Using
                End Select
                Me.TxtCareGroupPatient.Text = admissionObject.CareGroupCodeName
                'End If
            End If
            'End Using
            Me.TxtPatientEstrato.Text = (admissionObject.NivelCode & " - " & admissionObject.NivelName)
            'DATOS DEL INGRESO
            Me.TxtAdmissionCode.Text = admissionObject.AdmissionCode.ToString().Trim()
            Me.TxtAdmissionDate.Text = Convert.ToDateTime(admissionObject.AdmissionDate).ToString(SessionValues.Instance.Culture)
            'Using model As New MCareGroup(Me.Tag)
            ' Dim caregroup As CareGroup = model.GetCareGroupByIdSimple(CType(admissionObject.AdmissionCaregroupId, Integer)).ObjectEmbbeded
            'If caregroup IsNot Nothing AndAlso caregroup.Id > 0 Then
            Me.TxtCareGroupAdmission.Text = admissionObject.AdmissionCareGroupCodeName 'String.Concat(caregroup.Code, " - ", caregroup.Name)
            'End If
            'End Using
            Me.TxtEntityNameAdmission.Text = If(admissionObject.EntityName Is Nothing, String.Empty, admissionObject.EntityName.ToString().Trim())
            Select Case admissionObject.AdmissionRiskType
                Case "1"
                    Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
                Case "2"
                    Me.TxtRiskType.Text = "Accidente de Tránsito"
                Case "3"
                    Me.TxtRiskType.Text = "Catástrofe"
                Case "4"
                    Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
                Case "5"
                    Me.TxtRiskType.Text = "Accidente de Trabajo"
                Case "6"
                    Me.TxtRiskType.Text = "Enfermedad Profesional"
                Case "7"
                    Me.TxtRiskType.Text = "Atención Inicial de Urgencias"
                Case "8"
                    Me.TxtRiskType.Text = "Otro Tipo de Accidente"
                Case "9"
                    Me.TxtRiskType.Text = "Lesión Por Agresión"
                Case "10"
                    Me.TxtRiskType.Text = "Lesión AutoInfligida"
                Case "11"
                    Me.TxtRiskType.Text = "Maltrato Físico"
                Case "12"
                    Me.TxtRiskType.Text = "Promoción y Prevención"
                Case "13"
                    Me.TxtRiskType.Text = "Otro"
                Case "14"
                    Me.TxtRiskType.Text = "Accidente Rabico"
                Case "15"
                    Me.TxtRiskType.Text = "Accidente Ofídico"
                Case "16"
                    Me.TxtRiskType.Text = "Sopecha de Abuso Sexual"
                Case "17"
                    Me.TxtRiskType.Text = "Sopecha de Violencia Sexual"
                Case "18"
                    Me.TxtRiskType.Text = "Sopecha de Maltrato Emocional"
            End Select
            Me.TxtPlaceEntry.Text = ResourceManager.GetString("PlaceEntry_" & admissionObject.PlaceEntry.ToString(), "IndigoCrystalHis")
            Me.TxtAdmissionType.EditValue = Convert.ToInt32(admissionObject.AdmissionType)
            Me.TxtBedStay.Text = If(admissionObject.BedStay Is Nothing, String.Empty, admissionObject.BedStay.ToString().Trim())
            Me.TxtLiquidationType.EditValue = Convert.ToInt32(admissionObject.LiquidationType)
            Me.TxtBenefitPlan.Text = admissionObject.BenefitPlan
            Me.TxtAtentionCenter.Text = admissionObject.AdmissionCentAtencCodeName
            Me.TxtFunctionalUnitAdmission.Text = admissionObject.AdmissionUniFuncCodeName.ToString().Trim()
            Me.TxtResponsibleName.Text = If(admissionObject.ResponsibleName Is Nothing, String.Empty, admissionObject.ResponsibleName.ToString().Trim())
            Me.TxtResponsiblePhone.Text = admissionObject.ResponsiblePhone
            'DATOS DEL EGRESO
            If admissionObject.FECALTPAC IsNot Nothing Then
                INDtxtFunctionalUnitOut.Text = admissionObject.UniFuncEgre
                INDdeOut.EditValue = admissionObject.FECALTPAC
            End If
            INDSleAdmissionNumber2.SetMoreInfoData(admissionObject)
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmLiquidation_AdmissionDontExists", "Billing")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el formulario de detalles del ingreso
    ''' </summary>
    Private Sub OpenFormDetailAdmission(_admissionCode As String, _patientCode As String)
        FormDetail = New FrmAccountControlAmbulatoryDetail()
        FormDetail.RequestQuoteServices = Me.RequestQuoteServices
        FormDetail.TopLevel = False
        FormDetail.Parent = INDpcDocuments
        FormDetail.ToolBar.Dock = DockStyle.None
        FormDetail.ViewModeEditHold = False
        FormDetail.Dock = DockStyle.Fill
        FormDetail.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        FormDetail.AdmissionCode = _admissionCode
        FormDetail.CareCenterCode = INDsleCareCenter.EditValue
        FormDetail.PatientCode = _patientCode
        'AddHandler frmVoucherTransaction.LoadControlsFinish, AddressOf LoadControlsFinishVoucherTransaction
        'AddHandler frmVoucherTransaction.Shown, AddressOf VoucherTransaction_Shown
        FormDetail.Show()
        INDpanelPrincipalForm.Visible = False
        INDpcDocumentDetail.Visible = True
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAccountControlAmbulatory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        Presenter = New PAccountControlAmbulatory(Me)

        'parámetos de contratos
        _settingsContractXpo = Presenter.GetSettingsContractByOperatingUnitId(Me.BarraBotones.OperatingUnitValue)

        AddHandler INDSleAdmissionNumber2.QueryPopUp, AddressOf Admission_QueryPopUp
        Me.INDSleAdmissionNumber2.FuncQueryOnKeyEnterPressed = AddressOf Me.INDSleAdmissionNumber2_KeyDown
        StateOpen = False
        MBtnOpenAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        MBtnCloseAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Process)
        IndigoGridView1.SetListAcction(INDviewAdmissions, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewAdmissions.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareCenter.EditValueChanged
        GetAdmissions()
    End Sub

#End Region

#Region "NewSelectValue"
    Private Sub SleFindAdmission_NewSelectedValue(sender As Object, e As EditValueChangedEventArgs) Handles INDSleAdmissionNumber2.EditValueChanged
        If INDSleAdmissionNumber2.EditValue IsNot Nothing Then
            If StateOpen Then
                Dim admissionTmp As Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidation
                admissionTmp = CType(e.NewObject, Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidation)
                Dim objTemp = Me.viewSearchAdmission.GetFocusedRow
                If objTemp IsNot Nothing Then
                    Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                    If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                        LoadControlsInfo(obj.OriginalRow)
                    End If
                End If
            Else
                Dim admissionTmp As Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidationConfirm
                admissionTmp = CType(e.NewObject, Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidationConfirm)
                Dim objTemp = Me.viewSearchAdmission.GetFocusedRow
                If objTemp IsNot Nothing Then
                    Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                    If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                        LoadControlsInfo(obj.OriginalRow)
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se ejecuta despues de pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAccountControlAmbulatory_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareCenter.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleCareCenter.Size.Width - 11, 0)
        INDsleCareCenter.Properties.PopupFormSize = New System.Drawing.Size(INDsleCareCenter.Size.Width - 11, 0)
        INDsleCareCenter.Focus()
        GetListCareCenterHIS()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAccountControlAmbulatory_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "BarMenu"

    ''' <summary>
    ''' Click al menu facturados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MBtnOpenAdmission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnOpenAdmission.ItemClick
        StateOpen = True
        OldStateOpen = False
        MBtnOpenAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        MBtnCloseAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
    End Sub

    ''' <summary>
    ''' Click al menu abiertos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MBtnCloseAdmission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnCloseAdmission.ItemClick
        StateOpen = False
        MBtnOpenAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        MBtnCloseAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento key_down del control de ingresos
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Private Function INDSleAdmissionNumber2_KeyDown(code As String) As Object
        AsyncLoader(True)
        Task.Factory.StartNew(Sub()
                                  Using model As New MLiquidation()
                                      Dim admissionTempKeyDown = Nothing
                                      If StateOpen Then
                                          admissionTempKeyDown = model.GetAdmissionObjectByNumIngresAndIINGREPOR(code)
                                      Else
                                          admissionTempKeyDown = model.GetAdmissionConfirmByNumIngresAndIINGREPOR(code)
                                      End If
                                      INDSleAdmissionNumber2.BeginInvoke(Sub()
                                                                             If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown.Count > 0 Then
                                                                                 If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown(0).GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
                                                                                     LoadControlsInfo(admissionTempKeyDown(0))
                                                                                 End If
                                                                                 AsyncLoader(False)
                                                                             Else
                                                                                 AsyncLoader(False)
                                                                                 Mensaje(EeventViewerImages.Advertencia) = "El número del ingreso no existe"
                                                                                 INDSleAdmissionNumber2.EditValue = Nothing
                                                                                 INDSleAdmissionNumber2.DisplayNullText = String.Empty
                                                                                 INDSleAdmissionNumber2.Focus()
                                                                             End If
                                                                         End Sub)
                                  End Using
                              End Sub)
    End Function

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Establece el datasource del control de ingresos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Admission_QueryPopUp(sender As Object, e As CancelEventArgs)
        If StateOpen Then
            If OldStateOpen <> StateOpen Then
                Using m As New MLiquidation()
                    Me.INDSleAdmissionNumber2.Datasource = m.ListAdmissionsToLiquidationByIINGREPOR()
                End Using
            End If
        Else
            If OldStateOpen <> StateOpen Then
                Using m As New MLiquidation()
                    Me.INDSleAdmissionNumber2.Datasource = m.ListAdmissionsToLiquidationConfirmByIINGREPOR()
                End Using
            End If
        End If
        OldStateOpen = StateOpen
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento click del botón procesar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnProcess_Click(sender As Object, e As EventArgs) Handles INDbtnProcess.Click
        If AdmissionCode Is Nothing OrElse AdmissionCode = "" Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un ingreso"
            Exit Sub
        End If
        If Not (INDsleCareCenter.EditValue <> Nothing AndAlso INDsleCareCenter.EditValue <> "") Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un centro de atención"
            Exit Sub
        End If
        OpenFormDetailAdmission(AdmissionCode, PatientCode)
    End Sub

    ''' <summary>
    ''' CTRs the navigation1_ click back.
    ''' </summary>
    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        INDpanelPrincipalForm.Visible = True
        INDpcDocumentDetail.Visible = False
        FormDetail = Nothing
        INDpcDocuments.Controls.Clear()
        GetAdmissions()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de refrescar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnRefresh_Click(sender As Object, e As EventArgs) Handles INDbtnRefresh.Click
        GetAdmissions()
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim info = DirectCast(DirectCast(INDviewAdmissions.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewAdmissionsToAccountControlAmbulatoryXpo)
        OpenFormDetailAdmission(info.AdmissionNumber.ToString().Trim(), info.PatientIdentification.ToString().Trim())
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim info = DirectCast(DirectCast(INDviewAdmissions.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewAdmissionsToAccountControlAmbulatoryXpo)
        OpenFormDetailAdmission(info.AdmissionNumber.ToString().Trim(), info.PatientIdentification.ToString().Trim())
    End Sub

#End Region

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

#End Region

End Class
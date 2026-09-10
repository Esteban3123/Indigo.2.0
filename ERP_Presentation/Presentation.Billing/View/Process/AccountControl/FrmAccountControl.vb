'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Diego Andrés Roldán
' Created          : 02-07-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Drawing
Imports System.Dynamic
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Newtonsoft.Json
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Billing.MVP
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Contract.MVP
Imports Presentation.Controls
Imports Presentation.Resources

#End Region

Public Class FrmAccountControl
    Implements ICustomizableForm

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        IndigoGridControl1.SetHideNoRecords(INDgcConsultation, True)
        IndigoGridControl1.SetHideNoRecords(INDgcDetailServiciosNoRealizados, True)
        IndigoGridControl1.SetHideNoRecords(INDgcDetailServiciosRealizados, True)
        IndigoGridControl1.SetHideNoRecords(INDgcImagesDx, True)
        IndigoGridControl1.SetHideNoRecords(INDgcLaboratories, True)
        IndigoGridControl1.SetHideNoRecords(INDgcMedicineSupplier, True)
        IndigoGridControl1.SetHideNoRecords(INDgcNurseProcedure, True)
        IndigoGridControl1.SetHideNoRecords(INDgcOxygenConsumption, True)
        IndigoGridControl1.SetHideNoRecords(INDgcPathology, True)
        IndigoGridControl1.SetHideNoRecords(INDgcProceduresNoQX, True)
        IndigoGridControl1.SetHideNoRecords(INDgcProceduresQx, True)
        IndigoGridControl1.SetHideNoRecords(INDGcProceduresRealizados, True)
        IndigoGridControl1.SetHideNoRecords(INDGcQxEquipe, True)
        IndigoGridControl1.SetHideNoRecords(INDgcTerapy, True)
        IndigoGridControl1.SetHideNoRecords(INDgcValoraciones, True)
        IndigoGridControl1.SetHideNoRecords(INDGcHemoDetail, True)
        IndigoGridControl1.SetHideNoRecords(INDGcStays, True)
        ThemeResourceManager.ApplyStyleThemeToControl(LblState)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
        _stateOpen = True
        CargarMedicoRealizoDataSource()
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        ThemeResourceManager.ApplyStyleThemeToControl(LblState)
    End Sub
#End Region

#Region "Properties"
    Public Const PROCEDURES_REPORT_QX As String = "Procedimientos con Informe QX"

    Private _stateAdmission As String
    Private _stateOpen As Boolean

    Public Function GetStatusByCode(code As String) As Color
        _stateAdmission = code
        Select Case code
            Case " "
                Return Color.FromArgb(0, 70, 109)
            Case "F"
                Return Color.Green
            Case "A"
                Return Color.OrangeRed
            Case "C"
                Return Color.FromArgb(128, 128, 128)
            Case "P"
                Return Color.FromArgb(168, 188, 52)
            Case Else
                Return Color.Transparent
        End Select
    End Function

    ''' <summary>
    ''' Obtiene el Id de la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Id de la unidad operativa seleccionad</returns>
    Public Property IdOperatingUnitSelected As Integer
        Get
            Return Me.SleOperatingUnit.EditValue
        End Get
        Set(value As Integer)
            Me.SleOperatingUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que representa la entidad de parámetros de contratos
    ''' </summary>
    Dim _settingsContractXpo As Infrastructure.Data.Xpo.ContractRepository.SettingsContractXpo

    ''' <summary>
    ''' Obtiene o asigna la lista de unidades operativas
    ''' </summary>
    ''' <value>Lista de unidades operativa</value>
    ''' <returns>La lista de unidades operativas</returns>
    Public Property ListOperatingUnit As List(Of OperatingUnit)
        Get
            Return Me.SleOperatingUnit.Properties.DataSource
        End Get
        Set(value As List(Of OperatingUnit))
            If value IsNot Nothing Then
                Dim listFilter = (From e In value Where e IsNot Nothing Select e).ToList()
                Me.SleOperatingUnit.Properties.DataSource = listFilter
                If listFilter.Count > 0 Then
                    Me.SleOperatingUnit.EditValue = SessionValues.Instance.IndigoOperatingUnitId
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Unidad operativa seleccionada</returns>
    Public ReadOnly Property OperatingUnitSelected As OperatingUnit
        Get
            If Me.SleOperatingUnit.EditValue IsNot Nothing AndAlso Me.SleOperatingUnit.Properties.DataSource IsNot Nothing Then
                Dim queryOperatingUnit = (From i In Me.ListOperatingUnit
                                          Where i IsNot Nothing AndAlso i.Id = CType(SleOperatingUnit.EditValue, Integer)
                                          Select i).ToList
                If queryOperatingUnit IsNot Nothing AndAlso queryOperatingUnit.Count > 0 Then
                    Dim operating = CType(queryOperatingUnit(0), OperatingUnit)
                    Return operating
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Get
    End Property

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

    ''' <summary>
    ''' propiedad que establece u obtiene si el sistema es o no impuestos incluidos
    ''' </summary>
    Public Property TaxInclude As Boolean
        Get
            Return Me._flagTaxInclude
        End Get
        Set(value As Boolean)
            Me._flagTaxInclude = value
        End Set
    End Property
#End Region

#Region "Variables"
    ''' <summary>
    ''' Presentador de control servicios ambulatorios
    ''' </summary>
    ''' <remarks></remarks>
    Dim PresenterControlOutpatientServices As PControlOutpatientServices

    ''' <summary>
    ''' Variable que almacena el datasource del detalle de la pestaña Central de mezclas
    ''' </summary>
    Private ListPharmaDoseMSDetail As Task(Of List(Of Domain.Entities.SP_GetProcessedMedicationItemsForBilling_Result))

    Private Const MODULE_NAME As String = "Billing"
    Private listServiceOrderDetail As List(Of ServiceOrderDetail)
    Public Property PatientCode As String
    Public Property PatientCodeName As String
    Public Property AdmissionCode As String
    Public Property AdmissionAtentionCenterCode As String
    Private asyncLaboratories As New BackgroundWorker
    Private _datasourceMedicines As Boolean
    Private _datasourceMedicinesMixingStation As Boolean
    Private _datasourceLaboratories As Boolean
    Private _datasourcePathology As Boolean
    Private _datasourceImagesDX As Boolean
    Private _datasourceProceduresQX As Boolean
    Private _datasourceProceduresNoQX As Boolean
    Private _datasourceConsultation As Boolean
    Private _datasourceTherapy As Boolean
    Private _datasourceOxygen As Boolean
    Private _datasourceNursing As Boolean
    Private _datasourceReviews As Boolean
    Private _datasourceHemo As Boolean
    Private _datasourceStays As Boolean = False
    Private _admissionObjectPOCO As Object
    Private _idOperativeUnit As Integer
    Private datasourceServicesProceduresQx As List(Of ViewServicesProceduresQx)
    Private datasourceServicesProceduresReportQx As List(Of ViewServicesProceduresReportQx)
    Private datasourceProceduresQxRealizados As List(Of ViewSurgeriesPerformed)
    Private datasourceProceduresQxEquipe As List(Of ViewSurgicalEquipment)
    Private datasourceProceduresQxInform As List(Of ViewSurgicalReport)
    Private datasourceServicesHemo As List(Of ViewServicesHemocomponent)
    Private _treatmentType As Integer? ' ingreso tratamiento, 3 tipo oncologico
    ''' <summary>
    ''' guarda si el sistema es impuesto incluido o no
    ''' </summary>
    Private _flagTaxInclude As Boolean
#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listServiceOrderDetail = Nothing
        PatientCode = Nothing
        PatientCodeName = Nothing
        AdmissionCode = Nothing
        AdmissionAtentionCenterCode = Nothing
        asyncLaboratories = Nothing
        _datasourceMedicines = Nothing
        _datasourceMedicinesMixingStation = Nothing
        _datasourceLaboratories = Nothing
        _datasourcePathology = Nothing
        _datasourceImagesDX = Nothing
        _datasourceProceduresQX = Nothing
        _datasourceProceduresNoQX = Nothing
        _datasourceConsultation = Nothing
        _datasourceTherapy = Nothing
        _datasourceOxygen = Nothing
        _datasourceNursing = Nothing
        _datasourceReviews = Nothing
        _datasourceHemo = Nothing
        _admissionObjectPOCO = Nothing
        _idOperativeUnit = Nothing
        datasourceServicesProceduresQx = Nothing
        datasourceServicesProceduresReportQx = Nothing
        datasourceProceduresQxRealizados = Nothing
        datasourceProceduresQxEquipe = Nothing
        datasourceProceduresQxInform = Nothing
        datasourceServicesHemo = Nothing
    End Sub

    ''' <summary>
    ''' Variable utilizada para no permitir siempre asignar el datasource al control de admisiones
    ''' </summary>
    Private _oldStateOpen As Boolean = False

    Private Sub FrmAccountControl_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleAdmissionNumber2.Focus()
    End Sub

    Private Async Sub FrmAccountControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        LoadListOperatingUnit()
        AddHandler INDSleAdmissionNumber2.QueryPopUp, AddressOf Admission_QueryPopUp
        Me.INDSleAdmissionNumber2.FuncQueryOnKeyEnterPressed = AddressOf Me.INDSleAdmissionNumber2_KeyDown
        CleanControls()
        PresenterControlOutpatientServices = New PControlOutpatientServices()
        'parámetos de contratos
        _settingsContractXpo = PresenterControlOutpatientServices.GetSettingsContractByOperatingUnitId(_idOperativeUnit)
        AddMoreInfoColumn()
        'Se Consulta los parametros de company
        Using Model As New MServiceOrder("")
            Dim CompanySettings = Await Model.CompanySettings
            Me.TaxInclude = If(CompanySettings?.SalePriceIncludeTax, False)
        End Using
    End Sub

    Private Sub AddMoreInfoColumn()
        IndigoGridView1.MoreInfoColunmns(INDgvImagesDX)
        IndigoGridView2.MoreInfoColunmns(INDgvConsultation)
        IndigoGridView3.MoreInfoColunmns(INDgvLaboratories)
        IndigoGridView4.MoreInfoColunmns(INDgvPathologies)
        IndigoGridView5.MoreInfoColunmns(INDgvNursingProcedures)
        IndigoGridView6.MoreInfoColunmns(INDgvTherapy)
        IndigoGridView7.MoreInfoColunmns(INDgvValorations)
        IndigoGridView8.MoreInfoColunmns(INDgvProceduresNoQx)

        INDgvImagesDX.Columns.Where(Function(x) x.Name = "MoreInfo").ToList().FirstOrDefault.Width = 50
        INDgvConsultation.Columns.Where(Function(x) x.Name = "MoreInfo").ToList().FirstOrDefault.Width = 50
        INDgvLaboratories.Columns.Where(Function(x) x.Name = "MoreInfo").ToList().FirstOrDefault.Width = 50
        INDgvPathologies.Columns.Where(Function(x) x.Name = "MoreInfo").ToList().FirstOrDefault.Width = 50
        INDgvNursingProcedures.Columns.Where(Function(x) x.Name = "MoreInfo").ToList().FirstOrDefault.Width = 50
        INDgvTherapy.Columns.Where(Function(x) x.Name = "MoreInfo").ToList().FirstOrDefault.Width = 50
        INDgvValorations.Columns.Where(Function(x) x.Name = "MoreInfo").ToList().FirstOrDefault.Width = 50
        INDgvProceduresNoQx.Columns.Where(Function(x) x.Name = "MoreInfo").ToList().FirstOrDefault.Width = 50
    End Sub

    Private Function INDSleAdmissionNumber2_KeyDown(code As String) As Task(Of Object)
        AsyncLoader(True)
        CleanControlsGrids()
        Task.Factory.StartNew(Sub()
                                  Using model As New MLiquidation()
                                      Dim admissionTempKeyDown = Nothing
                                      If _stateOpen = True Then
                                          admissionTempKeyDown = model.GetAdmissionsToLiquidationCollection(code)
                                      ElseIf _stateOpen = False Then
                                          admissionTempKeyDown = model.GetAdmissionsToLiquidationConfirmCollection(code)
                                      End If
                                      INDSleAdmissionNumber2.BeginInvoke(Sub()
                                                                             If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown.Count > 0 Then
                                                                                 If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown(0).GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
                                                                                     If admissionTempKeyDown(0).Status = "B" Then
                                                                                         ShowMessage(EeventViewerImages.Advertencia) = "El número del ingreso se encuentra bloqueado"
                                                                                     End If
                                                                                     CleanControls()
                                                                                     LoadControlsInfo(admissionTempKeyDown(0))
                                                                                 End If
                                                                                 AsyncLoader(False)
                                                                             Else
                                                                                 AsyncLoader(False)
                                                                                 ShowMessage(EeventViewerImages.Advertencia) = "El número del ingreso no existe"
                                                                                 INDSleAdmissionNumber2.EditValue = Nothing
                                                                                 INDSleAdmissionNumber2.DisplayNullText = String.Empty
                                                                                 INDSleAdmissionNumber2.Focus()
                                                                             End If
                                                                         End Sub)
                                  End Using
                              End Sub)
    End Function

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._admissionObjectPOCO IsNot Nothing Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Me.INDSleAdmissionNumber2.EditValue = Me.IdEntity.Trim()
                INDSleAdmissionNumber2_KeyDown(Me.IdEntity.Trim())
            End If
        Else 'Realiza la consulta normal
            Me.INDSleAdmissionNumber2.EditValue = Me.IdEntity.Trim()
            INDSleAdmissionNumber2_KeyDown(Me.IdEntity.Trim())
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Click"
    Private Sub INDsbGererateServiceOrder_Click(sender As Object, e As EventArgs) Handles INDsbGererateServiceOrder.Click
        Using model As New MLiquidation()
            Dim admissionState = Nothing
            admissionState = model.GetAdmissionByNumIngres(_admissionObjectPOCO.AdmissionCode).FirstOrDefault
            If admissionState IsNot Nothing AndAlso admissionState.Status = "B" AndAlso admissionState.IncomeLockType <> 1 Then
                ShowMessage(EeventViewerImages.MensajeError) = "No se puede generar orden debido a que el ingreso se encuentra bloqueado"
                Exit Sub
            End If
            GenerateServiceOrder(Nothing, IIf(INDtcgSources.SelectedTabPageName = INDLycgMixingStation.Name, 1, 0))
        End Using
    End Sub

    Private Sub INDsbClear_Click(sender As Object, e As EventArgs) Handles INDsbClear.Click
        CleanControls()
    End Sub

    ''' <summary>
    ''' click para generar justificacion a los elementos seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbJustification_Click(sender As Object, e As EventArgs) Handles INDSbJustification.Click
        Try
            AsyncLoader(True)
            If Not ValidateCheckItems(True) Then
                AsyncLoader(False)
                Exit Sub
            End If

            Dim myListDetail As New List(Of AccountControlJustification)

            Select Case INDtcgSources.SelectedTabPageName
                Case INDlcgLaboratories.Name
                    Dim itemDetail = TryCast(INDgcLaboratories.DataSource, List(Of ViewServicesProceduresLaboratories))?.Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing)?.ToList()
                    itemDetail.ForEach(Sub(obj As ViewServicesProceduresLaboratories)
                                           Dim listRow = obj?.Row?.Split(",")?.ToList()
                                           Dim listjustification = obj?.ACJustificationId?.Split(",")?.ToList()
                                           Dim index As Integer = 0
                                           For Each id In listRow
                                               Dim detail = New AccountControlJustification
                                               detail.EntityId = id
                                               detail.EntityName = obj.EntityName
                                               detail.EntityTap = INDlcgLaboratories.Name
                                               If listjustification IsNot Nothing AndAlso listjustification?.Any() Then
                                                   Dim ACJustificationId As Integer? = listjustification(index)
                                                   detail.Id = ACJustificationId
                                                   detail.CreationUser = obj.ACCreationUser
                                                   detail.CreationDate = obj.ACCreationDate
                                                   detail.MarkAsModified()
                                               End If
                                               myListDetail.Add(detail)
                                               index += 1
                                           Next
                                       End Sub)
                Case INDlcgImagesDX.Name
                    Dim itemDetail = CType(INDgcImagesDx.DataSource, List(Of ViewServicesProceduresImagesDx)).Where(Function(o) o.Seleccione <> 0 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresImagesDx)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.Row
                                                     detail.EntityName = obj.EntityName
                                                     detail.EntityTap = INDlcgImagesDX.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = obj.ACCreationUser
                                                         detail.CreationDate = obj.ACCreationDate
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)

                Case INDlcgPathology.Name
                    Dim itemDetail = CType(INDgcPathology.DataSource, List(Of ViewServicesProceduresPathologies)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresPathologies)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.Row
                                                     detail.EntityName = obj.EntityName
                                                     detail.EntityTap = INDlcgPathology.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = obj.ACCreationUser
                                                         detail.CreationDate = obj.ACCreationDate
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)

                Case INDlcgProceduresNoQX.Name
                    Dim itemDetail = CType(INDgcProceduresNoQX.DataSource, List(Of ViewServicesProceduresNoQx)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresNoQx)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.Row
                                                     detail.EntityName = obj.EntityName
                                                     detail.EntityTap = INDlcgProceduresNoQX.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = obj.ACCreationUser
                                                         detail.CreationDate = obj.ACCreationDate
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)

                Case INDlcgConsultation.Name
                    Dim itemDetail = CType(INDgcConsultation.DataSource, List(Of ViewServicesProceduresConsultation)).Where(Function(o) o.Seleccione <> 0 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresConsultation)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.Row
                                                     detail.EntityName = obj.EntityName
                                                     detail.EntityTap = INDlcgConsultation.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = obj.ACCreationUser
                                                         detail.CreationDate = obj.ACCreationDate
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)

                Case INDLcgHemo.Name
                    Dim itemDetail = datasourceServicesHemo.Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Parallel.ForEach(itemDetail, Sub(obj As ViewServicesHemocomponent)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.Row
                                                     detail.EntityName = obj.EntityName
                                                     detail.EntityTap = INDLcgHemo.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = obj.ACCreationUser
                                                         detail.CreationDate = obj.ACCreationDate
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)

                Case INDlcgTerapy.Name
                    Dim itemDetail = CType(INDgcTerapy.DataSource, List(Of ViewServicesProceduresTherapy)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresTherapy)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.Row
                                                     detail.EntityName = obj.EntityName
                                                     detail.EntityTap = INDlcgTerapy.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = obj.ACCreationUser
                                                         detail.CreationDate = obj.ACCreationDate
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)

                Case INDlcgOxigenConsumer.Name
                    Dim itemDetail = CType(INDgcOxygenConsumption.DataSource, List(Of ViewOxygenConsumption)).Where(Function(o) o.Seleccione <> 0 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Dim errors As New List(Of String)()
                    Parallel.ForEach(itemDetail, Sub(obj As ViewOxygenConsumption)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.Row
                                                     detail.EntityName = obj.EntityName
                                                     detail.EntityTap = INDlcgOxigenConsumer.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = obj.ACCreationUser
                                                         detail.CreationDate = obj.ACCreationDate
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)

                Case INDlcgNurseProcedure.Name
                    Dim itemDetail = CType(INDgcNurseProcedure.DataSource, List(Of ViewServiceProceduresNursingProcedure)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    itemDetail.ForEach(Sub(obj As ViewServiceProceduresNursingProcedure)
                                           Dim listRow = obj?.Row?.Split(",")?.ToList()
                                           Dim listjustification = obj?.ACJustificationId?.Split(",")?.ToList()
                                           Dim index As Integer = 0
                                           For Each id In listRow
                                               Dim detail = New AccountControlJustification
                                               detail.EntityId = id
                                               detail.EntityName = obj.EntityName
                                               detail.EntityTap = INDlcgNurseProcedure.Name
                                               If listjustification IsNot Nothing AndAlso listjustification?.Any() Then
                                                   Dim ACJustificationId As Integer? = listjustification(index)
                                                   detail.Id = ACJustificationId
                                                   detail.CreationUser = obj.ACCreationUser
                                                   detail.CreationDate = obj.ACCreationDate
                                                   detail.MarkAsModified()
                                               End If
                                               myListDetail.Add(detail)
                                               index += 1
                                           Next
                                       End Sub)

                Case INDlcgValoraciones.Name
                    Dim itemDetail = CType(INDgcValoraciones.DataSource, List(Of ViewReviews)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Dim errors As New List(Of String)()
                    Parallel.ForEach(itemDetail, Sub(obj As ViewReviews)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.ID
                                                     detail.EntityName = obj.EntityName
                                                     detail.EntityTap = INDlcgValoraciones.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = obj.ACCreationUser
                                                         detail.CreationDate = obj.ACCreationDate
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)
                Case INDLcgStays.Name
                    Dim itemdetail = CType(INDGcStays.DataSource, List(Of StayInfoModel)).Where(Function(o) o.Selected AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                    Dim errors As New List(Of String)()
                    Parallel.ForEach(itemdetail, Sub(obj As StayInfoModel)
                                                     Dim detail = New AccountControlJustification
                                                     detail.EntityId = obj.AccountControlStaysId
                                                     detail.EntityName = "AccountControlStays" 'quemado
                                                     detail.EntityTap = INDLcgStays.Name
                                                     If obj.ACJustificationId IsNot Nothing Then
                                                         detail.Id = obj.ACJustificationId
                                                         detail.CreationUser = SessionValues.Instance.UserIndigo
                                                         detail.CreationDate = GetDateServer()
                                                         detail.MarkAsModified()
                                                     End If
                                                     myListDetail.Add(detail)
                                                 End Sub)


                Case INDlcgProceduresQX.Name
                    If datasourceServicesProceduresQx IsNot Nothing And datasourceProceduresQxRealizados IsNot Nothing Then
                        Dim itemDetail = datasourceProceduresQxRealizados.Where(Function(o) o.Seleccione = 1 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewSurgeriesPerformed)
                                                         Dim detail = New AccountControlJustification
                                                         detail.EntityId = obj.CONSECUQX
                                                         detail.EntityName = obj.EntityName
                                                         detail.EntityTap = INDlcgProceduresQX.Name
                                                         If obj.ACJustificationId IsNot Nothing Then
                                                             detail.Id = obj.ACJustificationId
                                                             detail.CreationUser = obj.ACCreationUser
                                                             detail.CreationDate = obj.ACCreationDate
                                                             detail.MarkAsModified()
                                                         End If
                                                         myListDetail.Add(detail)
                                                     End Sub)
                    End If

            End Select

            Using formulario As New FrmPopupAddJustification()
                formulario.ListDetail = myListDetail
                formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.4)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.Text = "Justificación"
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                formulario.FormFather = Me
                If transparent.ShowDialog(Me) = DialogResult.OK Then
                    BeginReloadDatasource()
                Else
                    AsyncLoader(False)
                    Exit Sub
                End If
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            ShowMessage(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

#End Region

#Region "CustomRowCellEdit"

    Private Sub INDgvCustomRowEdit_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDgvLaboratories.CustomRowCellEdit,
        INDgvPathologies.CustomRowCellEdit,
        INDGvHemoDetail.CustomRowCellEdit,
        INDgvOxygen.CustomRowCellEdit,
        INDgvValorations.CustomRowCellEdit,
        INDGvSurgeriesPerformed.CustomRowCellEdit,
        INDgvImagesDX.CustomRowCellEdit,
        INDgvProceduresNoQx.CustomRowCellEdit,
        INDgvConsultation.CustomRowCellEdit,
        INDgvTherapy.CustomRowCellEdit,
        INDgvNursingProcedures.CustomRowCellEdit,
        INDgvPharmaDoseMSDetail.CustomRowCellEdit, INDGvStays.CustomRowCellEdit
        If e.Column.FieldName = "Seleccione" OrElse e.Column.FieldName = "Selected" Then
            Select Case CType(sender, GridView).Name
                Case INDgvLaboratories.Name
                    Dim obj = INDgvLaboratories.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkLaboratories
                        Else
                            e.RepositoryItem = INDRptLaboratoriesGenerated
                        End If
                    End If
                Case INDgvPathologies.Name
                    Dim obj = INDgvPathologies.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkPathologies
                        Else
                            e.RepositoryItem = INDRptPathologiesGenerated
                        End If
                    End If
                Case INDGvHemoDetail.Name
                    Dim obj = INDGvHemoDetail.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        Dim _sel = obj.Seleccione
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkHemo
                        Else
                            e.RepositoryItem = INDRptHemoGenerated
                        End If
                    End If
                Case INDgvOxygen.Name
                    Dim obj = INDgvOxygen.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkOrygenConsumpsion
                        Else
                            e.RepositoryItem = INDrptImeOxygen
                        End If
                    End If
                Case INDgvValorations.Name
                    Dim obj = INDgvValorations.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDRptChkValoration
                        Else
                            e.RepositoryItem = INDRptImgGenerado
                        End If
                    End If
                Case INDGvSurgeriesPerformed.Name
                    Dim obj = TryCast(INDgvProceduresQx.GetDetailView(INDgvProceduresQx.FocusedRowHandle, 0), GridView)?.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDRICESelected
                        Else
                            e.RepositoryItem = INDRptImgGenerated
                        End If
                    End If
                Case INDgvImagesDX.Name
                    Dim obj = INDgvImagesDX.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkImagesDx
                        Else
                            e.RepositoryItem = INDRptImagesGenerated
                        End If
                    End If
                Case INDgvProceduresNoQx.Name
                    Dim obj = INDgvProceduresNoQx.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkProceduresNoQx
                        Else
                            e.RepositoryItem = INDRptNoQxGenerated
                        End If
                    End If
                Case INDgvConsultation.Name
                    Dim obj = INDgvConsultation.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkInterSearch
                        Else
                            e.RepositoryItem = INDRptIntercosultasGenerated
                        End If
                        If obj.GENSERVICEORDER IsNot Nothing AndAlso obj.Tipo = "Con Respuesta" AndAlso obj.ExistsInViewReviews Then
                            e.RepositoryItem = INDRptInterconsultasValidated
                        End If
                    End If
                Case INDgvTherapy.Name
                    Dim obj = INDgvTherapy.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkTherapy
                        Else
                            e.RepositoryItem = INDRptTherapyGenerated
                        End If
                    End If
                Case INDgvNursingProcedures.Name
                    Dim obj = INDgvNursingProcedures.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.GENSERVICEORDER Is Nothing Then
                            e.RepositoryItem = INDrptChkNursingProcedures
                        Else
                            e.RepositoryItem = INDRptNursingGenerated
                        End If
                    End If
                Case INDgvPharmaDoseMSDetail.Name
                    Dim obj = INDGvMixingStation.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.DeliveryStatus = 2 Then
                            e.RepositoryItem = INDRptMixingStationGenerated
                        Else
                            e.RepositoryItem = INDRIselector
                        End If
                    End If
                Case INDGvStays.Name
                    Dim obj = INDGvStays.GetRow(e.RowHandle)
                    If obj IsNot Nothing Then
                        If obj.LiquidationDate IsNot Nothing Then
                            e.RepositoryItem = INDRptImgGeneratedStay
                        Else
                            e.RepositoryItem = INDRptChkSelectStay
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub INDgvDetailServiciosRealizados_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDgvDetailServiciosRealizados.CustomRowCellEdit
        If e.Column.FieldName = "Seleccione" Then
            Dim obj = INDgvDetailServiciosRealizados.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                If obj.GENSERVICEORDER Is Nothing Then
                    e.RepositoryItem = INDrptChkSeleccioneServiceProcedure
                Else
                    e.RepositoryItem = INDrpticeGenerated
                End If
            End If
        End If
    End Sub

#End Region

#Region "SelectPageChanged"
    Private Sub INDtcgSources_SelectedPageChanged(sender As Object, e As LayoutTabPageChangedEventArgs) Handles INDtcgSources.SelectedPageChanged
        If AdmissionCode Is Nothing OrElse PatientCode Is Nothing Then
            Exit Sub
        End If
        Me.Cursor = ChangeCursorIndigo()
        If BarraBotones.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.GenerarOrdenServicio)) Then
            INDliGenerateServiceOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        INDLciJustification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDsbGererateServiceOrder.Enabled = False

        Select Case e.Page.Name
            Case INDlcgMedicinesSupplies.Name 'medicamentos
                INDliGenerateServiceOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciJustification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                If Not _datasourceMedicines Then
                    LoadDatasourceMedicinesSupliers()
                    _datasourceMedicines = True
                End If
            Case INDLycgMixingStation.Name 'Central de Mezclas
                INDLciJustification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceMedicinesMixingStation Then
                    LoadDatasourceMedicinesSupliersMixingStation()
                    _datasourceMedicinesMixingStation = True
                End If
            Case INDlcgLaboratories.Name 'laboratorios
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceLaboratories Then
                    LoadDatasourceLaboratories()
                    _datasourceLaboratories = True
                End If
                INDrptPceDetailLaboratories.PopupControl = INDpccServicesProcedures
            Case INDlcgPathology.Name 'patologias
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourcePathology Then
                    LoadDatasourcePathologies()
                    _datasourcePathology = True
                End If
                INDrptPceDetailPathologies.PopupControl = INDpccServicesProcedures
            Case INDlcgImagesDX.Name 'imagenes dx
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceImagesDX Then
                    LoadDatasourceImagesDx()
                    _datasourceImagesDX = True
                End If

            Case INDlcgProceduresQX.Name 'procedimiento qx
                If Not _datasourceProceduresQX Then
                    LoadDatasourceProceduresQx()
                    _datasourceProceduresQX = True
                Else
                    ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosQx, False)
                End If
                INDrptPceDetailProceduresQx.PopupControl = INDpccServicesProcedures
            Case INDlcgProceduresNoQX.Name 'procedimiento no QX
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceProceduresNoQX Then
                    LoadDatasourceProceduresNoQx()
                    _datasourceProceduresNoQX = True
                End If
                INDrptPceDetailProceduresNoQx.PopupControl = INDpccServicesProcedures
            Case INDlcgConsultation.Name 'interconsultas
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceConsultation Then
                    LoadDatasourceConsultation()
                    _datasourceConsultation = True
                End If
            Case INDlcgTerapy.Name 'terapias
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceTherapy Then
                    LoadDatasourceTherapy()
                    _datasourceTherapy = True
                End If
                INDrptPceDetailTherapy.PopupControl = INDpccServicesProcedures
            Case INDlcgOxigenConsumer.Name 'consumo de oxigeno
                If Not _datasourceOxygen Then
                    LoadDatasourceOxygenConsumption()
                    _datasourceOxygen = True
                Else
                    ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.OxygenConsumption, False)
                End If
            Case INDlcgNurseProcedure.Name 'procedimiento de enfermeria
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceNursing Then
                    LoadDatasourceNursingProcedures()
                    _datasourceNursing = True
                End If
                INDrptPceNursingProcedures.PopupControl = INDpccServicesProcedures
            Case INDlcgValoraciones.Name 'valoracioens
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceReviews Then
                    LoadDatasourceReviews()
                    _datasourceReviews = True
                End If
            Case INDLcgHemo.Name 'HEMOCOMPONENTES
                If Not _datasourceHemo Then
                    LoadDatasourceHemocomponent()
                    _datasourceHemo = True
                Else
                    ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.Hemocomponent, False)
                End If
            Case INDLcgStays.Name ' Estancias
                INDsbGererateServiceOrder.Enabled = True
                If Not _datasourceStays Then
                    LoadDatasourceStays()
                    _datasourceStays = True
                End If
        End Select
        Me.Cursor = Cursors.Default
    End Sub
#End Region

#Region "NewSelectValue"
    Private Sub SleFindAdmission_NewSelectedValue(sender As Object, e As EditValueChangedEventArgs) Handles INDSleAdmissionNumber2.EditValueChanged
        If INDSleAdmissionNumber2.EditValue IsNot Nothing Then
            If _stateOpen Then
                Dim admissionTmp As Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidation
                admissionTmp = CType(e.NewObject, Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidation)
                Dim objTemp = Me.viewSearchAdmission.GetFocusedRow
                If objTemp IsNot Nothing Then
                    Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                    If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                        CleanControls()
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
                        CleanControls()
                        LoadControlsInfo(obj.OriginalRow)
                    End If
                End If
            End If
        End If
        'If e.AdmissionObject IsNot Nothing AndAlso e.AdmissionObject.GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
        '    CleanControls()
        '    LoadControlsInfo(e.AdmissionObject)
        'End If
    End Sub
#End Region

#Region "CloseUp"
    Private Sub INDrptPceDetailLaboratories_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDrptPceDetailLaboratories.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[CODSERIPS] = '" & INDgvLaboratories.GetFocusedRowCellValue("CODSERIPS") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvLaboratories.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

    Private Sub INDrptPceDetailPathologies_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDrptPceDetailPathologies.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[CODSERIPS] = '" & INDgvPathologies.GetFocusedRowCellValue("CODSERIPS") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvPathologies.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

    Private Sub INDrptDetailImagesDX_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDrptDetailImagesDX.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[CODSERIPS] = '" & INDgvImagesDX.GetFocusedRowCellValue("CODSERIPS") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvImagesDX.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

    Private Sub INDrptPceDetailProceduresQx_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDrptPceDetailProceduresQx.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[CODSERIPS] = '" & INDgvProceduresQx.GetFocusedRowCellValue("CODSERIPS") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvProceduresQx.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

    Private Sub INDrptPceDetailProceduresNoQx_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDrptPceDetailProceduresNoQx.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[CODSERIPS] = '" & INDgvProceduresNoQx.GetFocusedRowCellValue("CODSERIPS") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvProceduresNoQx.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

    Private Sub INDrptPceDetailInterSearch_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDrptPceDetailConsultation.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[CODSERIPS] = '" & INDgvConsultation.GetFocusedRowCellValue("CODSERIPS") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvConsultation.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

    Private Sub INDrptPceDetailTherapy_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDrptPceDetailTherapy.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[CODSERIPS] = '" & INDgvTherapy.GetFocusedRowCellValue("CODSERIPS") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvTherapy.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

    Private Sub INDrptPceDetailNursingProcedures_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDrptPceNursingProcedures.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[Actividad] = '" & INDgvNursingProcedures.GetFocusedRowCellValue("Actividad") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvNursingProcedures.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

    Private Sub INDRptPceDetailValoration_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDRptPceDetailValoration.QueryCloseUp
        'si se desmarcaron todos en el detalle, desmarcar el padre
        INDgvDetailServiciosRealizados.ActiveFilterString = "[CODSERIPS] = '" & INDgvValorations.GetFocusedRowCellValue("CODSERIPS") & "' AND Seleccione = True"
        If INDgvDetailServiciosRealizados.RowCount = 0 Then
            INDgvValorations.SetFocusedRowCellValue("Seleccione", 0)
        End If
    End Sub

#End Region

#Region "CheckStateChanged"
    '''' <summary>
    '''' 
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    'Private Sub INDrptChkHemo_CheckStateChanged(sender As Object, e As EventArgs) Handles INDrptChkHemo.CheckStateChanged
    '    Dim control As CheckEdit = CType(sender, CheckEdit)
    '    If control.CheckState <> CheckState.Indeterminate Then
    '        Dim headerRow = CType(INDGvHemoDetail.GetFocusedRow(), ViewServicesHemocomponent)
    '        If headerRow IsNot Nothing Then
    '            Dim services = datasourceServicesHemo.Where(Function(o) o.CODSERIPS = headerRow.CODSERIPS AndAlso o.GENSERVICEORDER Is Nothing AndAlso o.EstadoServicio = "Realizados").ToList()
    '            If control.CheckState = CheckState.Checked Then
    '                services.ForEach(Sub(o)
    '                                     o.Seleccione = 1
    '                                 End Sub)
    '            Else
    '                services.ForEach(Sub(o)
    '                                     o.Seleccione = 0
    '                                 End Sub)
    '            End If
    '        End If
    '    End If
    'End Sub
#End Region

#Region "CheckedChanged"
    Private Sub INDrptChkSeleccione_CheckedChanged(sender As Object, e As EventArgs) Handles INDrptChkSeleccioneServiceProcedure.CheckedChanged
        Dim control As CheckEdit = CType(sender, CheckEdit)
        Select Case INDtcgSources.SelectedTabPageName
            Case INDlcgProceduresQX.Name
                Dim headerRow = CType(INDgvProceduresQx.GetFocusedRow(), ViewProceduresQx)
                Dim headerRowDetail = datasourceProceduresQxRealizados.Where(Function(j) j.NUMEFOLIO = headerRow.NUMEFOLIO).ToList()
                If headerRow.Tipo = PROCEDURES_REPORT_QX Then
                    If CType(INDgvDetailServiciosRealizados.GetFocusedRow(), ViewServicesProceduresReportQx).GENSERVICEORDER Is Nothing Then
                        CType(INDgvDetailServiciosRealizados.GetFocusedRow(), ViewServicesProceduresReportQx).Seleccione = control.Checked
                        Dim services = CType(INDgcDetailServiciosRealizados.DataSource, List(Of ViewServicesProceduresReportQx)).Where(Function(o) o.CODSERIPS = headerRow.CODSERIPS).ToList()
                        If (services.Where(Function(o) o.Seleccione <> 0).ToList().Count = services.Count) Then
                            headerRowDetail.ForEach(Sub(x)
                                                        x.Seleccione = 1
                                                    End Sub)
                        ElseIf services.Where(Function(o) o.Seleccione <> 0).ToList().Count > 0 Then
                            headerRowDetail.ForEach(Sub(x)
                                                        x.Seleccione = 2
                                                    End Sub)
                        Else
                            headerRowDetail.ForEach(Sub(x)
                                                        x.Seleccione = 0
                                                    End Sub)
                        End If
                    End If
                Else
                    If CType(INDgvDetailServiciosRealizados.GetFocusedRow(), ViewServicesProceduresQx).GENSERVICEORDER IsNot Nothing Then
                        CType(INDgvDetailServiciosRealizados.GetFocusedRow(), ViewServicesProceduresQx).Seleccione = False
                    Else
                        CType(INDgvDetailServiciosRealizados.GetFocusedRow(), ViewServicesProceduresQx).Seleccione = control.Checked
                        Dim services = CType(INDgcDetailServiciosRealizados.DataSource, List(Of ViewServicesProceduresQx)).Where(Function(o) o.CODSERIPS = headerRow.CODSERIPS).ToList()
                        If (services.Where(Function(o) o.Seleccione <> 0).ToList().Count = services.Count) Then
                            headerRowDetail.ForEach(Sub(x)
                                                        x.Seleccione = 1
                                                    End Sub)
                        ElseIf services.Where(Function(o) o.Seleccione <> 0).ToList().Count > 0 Then
                            headerRowDetail.ForEach(Sub(x)
                                                        x.Seleccione = 2
                                                    End Sub)
                        Else
                            headerRowDetail.ForEach(Sub(x)
                                                        x.Seleccione = 0
                                                    End Sub)
                        End If
                    End If
                End If
        End Select
    End Sub
#End Region

#Region "SelectionChanged"
    Private Sub SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDgvProceduresQx.SelectionChanged
        If e.ControllerRow = -1 Then
            Exit Sub
        End If
    End Sub
#End Region

#Region "ShowingEditor"
    Private Sub INDgvDetailServiciosRealizados_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDgvDetailServiciosRealizados.ShowingEditor
        Select Case INDtcgSources.SelectedTabPageName
            Case INDlcgProceduresQX.Name
                Dim headerRow = CType(INDgvProceduresQx.GetFocusedRow(), ViewProceduresQx)
                If headerRow.Tipo = "Procedimientos con Informe QX" Then
                    Dim obj = DirectCast(INDgvDetailServiciosRealizados.GetFocusedRow, ViewServicesProceduresReportQx)
                    INDrpticeGenerated.ReadOnly = obj.GENSERVICEORDER IsNot Nothing
                Else
                    Dim obj = DirectCast(INDgvDetailServiciosRealizados.GetFocusedRow, ViewServicesProceduresQx)
                    INDrpticeGenerated.ReadOnly = obj.GENSERVICEORDER IsNot Nothing
                End If
            Case Else
                Dim obj = INDgvDetailServiciosRealizados.GetFocusedRow
                INDrpticeGenerated.ReadOnly = obj.GENSERVICEORDER IsNot Nothing
        End Select
    End Sub

    Private Sub ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDgvLaboratories.ShowingEditor, INDgvNursingProcedures.ShowingEditor, INDgvPathologies.ShowingEditor,
        INDgvOxygen.ShowingEditor, INDgvProceduresQx.ShowingEditor, INDgvProceduresNoQx.ShowingEditor, INDgvImagesDX.ShowingEditor,
        INDgvTherapy.ShowingEditor, INDgvConsultation.ShowingEditor, INDGvHemoDetail.ShowingEditor, INDgvValorations.ShowingEditor,
        INDgvPharmaDoseMSDetail.ShowingEditor, INDGvStays.ShowingEditor
        Select Case CType(sender, GridView).Name
            Case INDgvLaboratories.Name
                Dim headerRow = CType(INDgvLaboratories.GetFocusedRow(), ViewServicesProceduresLaboratories)

                INDrptChkLaboratories.ReadOnly = headerRow.Tipo = "No Realizados" OrElse headerRow.Tipo = "Anulados" OrElse headerRow.Tipo = "Solicitados" OrElse headerRow.GENSERVICEORDER IsNot Nothing
                INDRptLaboratoriesGenerated.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing

                If CType(sender, GridView).FocusedColumn.Name <> "MoreInfo" And INDrptChkLaboratories.ReadOnly Then
                    ShowMessage(eStatusResult.WARNING) = $"No se puede seleccionar el laboratorio porque:{IIf({"No Realizados", "Anulados", "Solicitados"}.Contains(headerRow.Tipo), $" el tipo es {headerRow.Tipo}", " ya tiene una Orden de Servicio") }"
                End If

            Case INDgvPathologies.Name
                Dim headerRow = CType(INDgvPathologies.GetFocusedRow(), ViewServicesProceduresPathologies)

                INDrptChkPathologies.ReadOnly = headerRow.Tipo = "No Realizados" OrElse headerRow.Tipo = "Anulados" OrElse headerRow.Tipo = "Solicitados" OrElse headerRow.GENSERVICEORDER IsNot Nothing
                INDRptPathologiesGenerated.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing

                If CType(sender, GridView).FocusedColumn.Name <> "MoreInfo" And INDrptChkPathologies.ReadOnly Then
                    ShowMessage(eStatusResult.WARNING) = $"No se puede seleccionar la patología porque:{IIf({"No Realizados", "Anulados", "Solicitados"}.Contains(headerRow.Tipo), $" el tipo es {headerRow.Tipo}", " ya tiene una Orden de Servicio") }"
                End If

            Case INDgvImagesDX.Name
                Dim headerRow = CType(INDgvImagesDX.GetFocusedRow(), ViewServicesProceduresImagesDx)

                INDrptChkImagesDx.ReadOnly = headerRow.Tipo = "Estudios No Realizados" OrElse headerRow.Tipo = "Anulado" OrElse headerRow.GENSERVICEORDER IsNot Nothing
                INDRptImagesGenerated.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing

                If CType(sender, GridView).FocusedColumn.Name <> "MoreInfo" And INDrptChkImagesDx.ReadOnly Then
                    ShowMessage(eStatusResult.WARNING) = $"No se puede seleccionar la Imagen DX porque:{IIf(headerRow.Tipo = "Estudios No Realizados" OrElse headerRow.Tipo = "Anulado", $" el tipo es {headerRow.Tipo}", " ya tiene una Orden de Servicio") }"
                End If

            Case INDGvSurgeriesPerformed.Name
                Dim headerRow = CType(INDgvProceduresQx.GetFocusedRow(), ViewProceduresQx)
                INDRptImgGenerated.ReadOnly = headerRow.Tipo = "Procedimientos Solicitados" OrElse headerRow.GENSERVICEORDER IsNot Nothing

            Case INDgvProceduresNoQx.Name
                Dim headerRow = CType(INDgvProceduresNoQx.GetFocusedRow(), ViewServicesProceduresNoQx)

                INDrptChkProceduresNoQx.ReadOnly = Not {"Realizados"}.Contains(headerRow.Tipo) OrElse headerRow.GENSERVICEORDER IsNot Nothing
                INDRptNoQxGenerated.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing

                If CType(sender, GridView).FocusedColumn.Name <> "MoreInfo" And INDrptChkProceduresNoQx.ReadOnly Then
                    ShowMessage(eStatusResult.WARNING) = $"No se puede seleccionar el procedimiento No QX porque:{IIf({"No Realizados", "Anulados", "Solicitados"}.Contains(headerRow.Tipo), $" el tipo es {headerRow.Tipo}", " ya tiene una Orden de Servicio") }"
                End If

            Case INDgvConsultation.Name
                Dim headerRow = CType(INDgvConsultation.GetFocusedRow(), ViewServicesProceduresConsultation)

                INDrptChkInterSearch.ReadOnly = headerRow.Tipo = "Solicitadas" OrElse headerRow.GENSERVICEORDER IsNot Nothing
                INDRptIntercosultasGenerated.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing

                If CType(sender, GridView).FocusedColumn.Name <> "MoreInfo" And INDrptChkInterSearch.ReadOnly Then
                    ShowMessage(eStatusResult.WARNING) = $"No se puede seleccionar la interconsulta porque:{IIf(headerRow.Tipo = "Solicitadas", $" el tipo es {headerRow.Tipo}", " ya tiene una Orden de Servicio") }"
                End If

            Case INDgvNursingProcedures.Name
                Dim headerRow = CType(INDgvNursingProcedures.GetFocusedRow(), ViewServiceProceduresNursingProcedure)

                INDrptChkNursingProcedures.ReadOnly = headerRow.Tipo = "No Facturable" OrElse headerRow.GENSERVICEORDER IsNot Nothing
                INDRptNursingGenerated.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing

                If CType(sender, GridView).FocusedColumn.Name <> "MoreInfo" And INDrptChkNursingProcedures.ReadOnly Then
                    ShowMessage(eStatusResult.WARNING) = $"No se puede seleccionar el procedimiento de enfermeria porque :{IIf(headerRow.Tipo = "No Facturable", $" el tipo es {headerRow.Tipo}", " ya tiene una Orden de Servicio") }"
                End If

            Case INDgvOxygen.Name
                Dim headerRow = CType(INDgvOxygen.GetFocusedRow(), ViewOxygenConsumption)
                INDrptImeOxygen.ReadOnly = Not headerRow.GENSERVICEORDER Is Nothing

            Case INDGvHemoDetail.Name
                Dim headerRow = CType(INDGvHemoDetail.GetFocusedRow(), ViewServicesHemocomponent)
                INDrptChkHemo.ReadOnly = headerRow.EstadoServicio = "No Realizados" OrElse headerRow.GENSERVICEORDER IsNot Nothing
                INDRptHemoGenerated.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing

                If CType(sender, GridView).FocusedColumn.Name <> "MoreInfo" And INDrptChkHemo.ReadOnly Then
                    ShowMessage(eStatusResult.WARNING) = $"No se puede seleccionar el hemocomponente porque:{IIf({"No Realizados", "Anulados", "Solicitados"}.Contains(headerRow.EstadoServicio), $" el tipo es {headerRow.EstadoServicio}", " ya tiene una Orden de Servicio") }"
                End If
            Case INDgvTherapy.Name
                Dim headerRow = CType(INDgvTherapy.GetFocusedRow(), ViewServicesProceduresTherapy)

                INDrptChkTherapy.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing
                INDRptTherapyGenerated.ReadOnly = headerRow.GENSERVICEORDER IsNot Nothing

                If CType(sender, GridView).FocusedColumn.Name <> "MoreInfo" And INDrptChkTherapy.ReadOnly Then
                    ShowMessage(eStatusResult.WARNING) = $"No se puede seleccionar el procedimiento de terapia porque ya tiene una Orden de Servicio"
                End If

            Case INDgvValorations.Name
                Dim obj = DirectCast(INDgvValorations.GetFocusedRow, ViewReviews)
                INDRptImgGenerado.ReadOnly = obj.GENSERVICEORDER IsNot Nothing

            Case INDgvPharmaDoseMSDetail.Name
                Dim obj = CType(INDGvMixingStation.GetDetailView(INDGvMixingStation.FocusedRowHandle, 0), GridView).GetFocusedRow()
                INDRptMixingStationGenerated.ReadOnly = obj.DeliveryStatus = 2
            Case INDGvStays.Name
                Dim obj = CType(INDGvStays.GetFocusedRow(), StayInfoModel)
                INDRptImgGeneratedStay.ReadOnly = obj.LiquidationDate IsNot Nothing
        End Select
    End Sub

#End Region

#Region "QueryPopUp"
    Private Sub Admission_QueryPopUp(sender As Object, e As CancelEventArgs)
        If _stateOpen Then
            If _oldStateOpen <> _stateOpen Then
                Using m As New MLiquidation()
                    Me.INDSleAdmissionNumber2.Datasource = m.ListAdmissionsToLiquidation()
                End Using
            End If
        Else
            If _oldStateOpen <> _stateOpen Then
                Using m As New MLiquidation()
                    Me.INDSleAdmissionNumber2.Datasource = m.ListAdmissionsToLiquidationConfirm()
                End Using
            End If
        End If
        _oldStateOpen = _stateOpen
    End Sub

    Private Async Sub INDrptPceDetailLaboratories_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceDetailLaboratories.QueryPopUp
        INDrptPceDetailLaboratories.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.Laboratorios, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.Laboratorios)
        'determino si se muestran realizados o no realizados

        TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If INDgvLaboratories.GetFocusedRowCellValue("Tipo") = "No Realizados" OrElse INDgvLaboratories.GetFocusedRowCellValue("Tipo") = "Anulados" OrElse INDgvLaboratories.GetFocusedRowCellValue("Tipo") = "Solicitados" Then
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDgvLaboratories.GetFocusedRowCellValue("Tipo") = "Realizados" Then
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub INDRptPceDetailValoration_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRptPceDetailValoration.QueryPopUp
        INDRptPceDetailValoration.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.Valoration, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.Valoration)
        'determino si se muestran realizados o no realizados
        TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If INDgvValorations.GetFocusedRowCellValue("Tipo") = "No Realizados" OrElse INDgvValorations.GetFocusedRowCellValue("Tipo") = "Anulados" OrElse INDgvValorations.GetFocusedRowCellValue("Tipo") = "Solicitados" Then
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDgvValorations.GetFocusedRowCellValue("Tipo") = "Realizados" Then
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub INDrptPceDetailPathologies_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceDetailPathologies.QueryPopUp
        INDrptPceDetailPathologies.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.Patologias, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        'RepositoryItemDetalleVer.PopupControl = INDpccDetalleMenuLabyPato
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.Patologias)
        'determino si se muestran realizados o no realizados

        TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If INDgvPathologies.GetFocusedRowCellValue("Tipo") = "No Realizados" OrElse INDgvPathologies.GetFocusedRowCellValue("Tipo") = "Anulados" OrElse INDgvPathologies.GetFocusedRowCellValue("Tipo") = "Solicitados" Then
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDgvPathologies.GetFocusedRowCellValue("Tipo") = "Realizados" Then
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub INDrptDetailImagesDX_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptDetailImagesDX.QueryPopUp
        INDrptDetailImagesDX.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.Imagenes_Dx, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.Imagenes_Dx)
        'determino si se muestran realizados o no realizados

        TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If INDgvImagesDX.GetFocusedRowCellValue("Tipo") = "Estudios No Realizados" OrElse INDgvImagesDX.GetFocusedRowCellValue("Tipo") = "Anulado" Then
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDgvImagesDX.GetFocusedRowCellValue("Tipo") = "Estudios Realizados" OrElse INDgvImagesDX.GetFocusedRowCellValue("Tipo") = "Estudios Realizados en Sitio" Then
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub INDrptPceDetailProceduresQx_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceDetailProceduresQx.QueryPopUp

        INDrptPceDetailProceduresQx.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosQx, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosQx)
        'determino si se muestran realizados o no realizados

        If INDgvProceduresQx.GetFocusedRowCellValue("Tipo") = "Procedimientos con Informe QX" Then
            'INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf INDgvProceduresQx.GetFocusedRowCellValue("Tipo") = "Procedimientos Solicitados" Then
            'INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub INDrptPceDetailProceduresNoQx_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceDetailProceduresNoQx.QueryPopUp
        INDrptPceDetailProceduresNoQx.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosNoQx, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosNoQx)
        'determino si se muestran realizados o no realizados

        TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If INDgvProceduresNoQx.GetFocusedRowCellValue("Tipo") = "No Realizados" Then
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf INDgvProceduresNoQx.GetFocusedRowCellValue("Tipo") = "Realizados" Then
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub INDrptPceDetailConsultation_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceDetailConsultation.QueryPopUp
        INDrptPceDetailConsultation.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.Interconsultas, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.Interconsultas)
        'determino si se muestran realizados o no realizados

        TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If INDgvConsultation.GetFocusedRowCellValue("Tipo") = "Solicitadas" Then
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDgvConsultation.GetFocusedRowCellValue("Tipo") = "Con Respuesta" Then
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub INDrptPceDetailTherapy_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceDetailTherapy.QueryPopUp
        INDrptPceDetailTherapy.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.Terapias, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.Terapias)
        'determino si se muestran realizados o no realizados
        INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Async Sub INDrptPceNursingProcedures_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceNursingProcedures.QueryPopUp
        INDrptPceNursingProcedures.PopupControl = INDpccServicesProcedures
        Await ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosEnfermeria, False)
        INDtcgServicesProcedures.SelectedTabPageIndex = 0
        VistaDetalleRealizados(eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosEnfermeria)
        'determino si se muestran realizados o no realizados

        TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If INDgvNursingProcedures.GetFocusedRowCellValue("Tipo") = "Facturable" Then
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf INDgvNursingProcedures.GetFocusedRowCellValue("Tipo") = "No Facturable" Then
            INDtpServiciosRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

#End Region

#Region "Custom Repository"

    Private Sub INDgvProceduresNoQx_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvProceduresNoQx.CustomColumnDisplayText
        If e.Column.FieldName = "CUPSEntityContractDescriptionId" Then
            If e.ListSourceRowIndex >= 0 Then
                Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                e.DisplayText = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "ContractDescriptionCodeName").ToString()
            End If
        End If
    End Sub

    Private Sub INDgvProceduresNoQx_ShownEditor(sender As Object, e As EventArgs) Handles INDgvProceduresNoQx.ShownEditor
        Dim view = CType(sender, DevExpress.XtraGrid.Views.Base.ColumnView)
        If view.FocusedColumn.FieldName = "CUPSEntityContractDescriptionId" AndAlso TypeOf view.ActiveEditor Is DevExpress.XtraEditors.SearchLookUpEdit Then
            Dim edit = CType(view.ActiveEditor, DevExpress.XtraEditors.SearchLookUpEdit)
            Dim cupsEntityCode = view.GetFocusedRowCellValue("CODSERIPS")
            edit.Properties.DataSource = PresenterControlOutpatientServices.ListContractDescriptionsByCupsEntityCode(cupsEntityCode)
        End If
    End Sub

    Private Sub INDrptSleContractDescriptionProceduresNoQx_EditValueChanged(sender As Object, e As EventArgs) Handles INDrptSleContractDescriptionProceduresNoQx.EditValueChanged
        Dim searchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        If searchLookUpEdit Is Nothing Then
            Exit Sub
        End If

        Dim selectedhandle = INDgvProceduresNoQx.GetSelectedRows()
        If selectedhandle IsNot Nothing Then
            Dim headerRow = CType(INDgvProceduresNoQx.GetFocusedRow(), ViewServicesProceduresNoQx)
            If searchLookUpEdit.EditValue Is Nothing Then
                headerRow.ContractDescriptionId = Nothing
                headerRow.ContractDescriptionCodeName = String.Empty
            Else
                Dim riSearchLookUpEdit = CType(searchLookUpEdit.Properties, RepositoryItemSearchLookUpEdit)
                Dim rowByKeyValue = DirectCast(DirectCast(riSearchLookUpEdit.GetRowByKeyValue(searchLookUpEdit.EditValue), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.ContractRepository.CUPSEntityContractDescriptionsXpo)
                If rowByKeyValue IsNot Nothing Then
                    headerRow.ContractDescriptionId = rowByKeyValue.ContractDescriptionId.Id
                    headerRow.ContractDescriptionCodeName = rowByKeyValue.ContractDescriptionId.CodeName
                End If
            End If
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"
    Private Sub INDgcLaboratories_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcLaboratories.MouseDoubleClick
        Dim hitPoint = Me.INDgvLaboratories.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSeleccioneLaboratorio.Name) Then
                If CType(INDgcLaboratories.DataSource, List(Of ViewServicesProceduresLaboratories)).Where(Function(x) x.Seleccione AndAlso x.Tipo = "Realizados").Count() = CType(INDgcLaboratories.DataSource, List(Of ViewServicesProceduresLaboratories)).Where(Function(x) x.Tipo = "Realizados").Count() Then
                    For Each headerRow In CType(INDgcLaboratories.DataSource, List(Of ViewServicesProceduresLaboratories))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo = "Realizados" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = False
                        End If
                    Next
                Else
                    For Each headerRow In CType(INDgcLaboratories.DataSource, List(Of ViewServicesProceduresLaboratories))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo = "Realizados" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = True
                        End If
                    Next
                End If
                INDgcLaboratories.RefreshDataSource()
                Me.INDgcLaboratories.Invalidate()
            End If
        End If
    End Sub
    Private Sub INDgcPathology_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcPathology.MouseDoubleClick
        Dim hitPoint = Me.INDgvPathologies.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSelPathologies.Name) Then
                If CType(INDgcPathology.DataSource, List(Of ViewServicesProceduresPathologies)).Where(Function(x) x.Seleccione AndAlso x.Tipo = "Realizados").Count() = CType(INDgcPathology.DataSource, List(Of ViewServicesProceduresPathologies)).Where(Function(x) x.Tipo = "Realizados").Count() Then
                    For Each headerRow In CType(INDgcPathology.DataSource, List(Of ViewServicesProceduresPathologies))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo = "Realizados" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = False
                        End If
                    Next
                Else
                    For Each headerRow In CType(INDgcPathology.DataSource, List(Of ViewServicesProceduresPathologies))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo = "Realizados" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = True
                        End If
                    Next
                End If
                INDgcPathology.RefreshDataSource()
                Me.INDgcPathology.Invalidate()
            End If
        End If
    End Sub
    Private Sub INDgcImagesDx_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcImagesDx.MouseDoubleClick
        Dim hitPoint = Me.INDgvImagesDX.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSelImagesDx.Name) Then
                If CType(INDgcImagesDx.DataSource, List(Of ViewServicesProceduresImagesDx)).Where(Function(x) x.Seleccione AndAlso (x.Tipo <> "Estudios No Realizados" AndAlso x.Tipo <> "Anulado")).Count() = CType(INDgcImagesDx.DataSource, List(Of ViewServicesProceduresImagesDx)).Where(Function(x) (x.Tipo <> "Estudios No Realizados" AndAlso x.Tipo <> "Anulado")).Count Then
                    For Each headerRow In CType(INDgcImagesDx.DataSource, List(Of ViewServicesProceduresImagesDx))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo <> "Estudios No Realizados" AndAlso headerRow.Tipo <> "Anulado" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = False
                        End If
                    Next
                Else
                    For Each headerRow In CType(INDgcImagesDx.DataSource, List(Of ViewServicesProceduresImagesDx))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo <> "Estudios No Realizados" AndAlso headerRow.Tipo <> "Anulado" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = True
                        End If
                    Next
                End If
                INDgcImagesDx.RefreshDataSource()
                Me.INDgcImagesDx.Invalidate()
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento doble click para seleccionar o des seleccionar todos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcProceduresQx_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcProceduresQx.MouseDoubleClick
        Dim hitPoint = CType(INDgvProceduresQx.GetDetailView(INDgvProceduresQx.FocusedRowHandle, 0), GridView)?.CalcHitInfo(e.Location)
        If hitPoint?.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSelQx.Name) Then

                If TryCast(INDgvProceduresQx.GetFocusedRow, ViewProceduresQx).Tipo = PROCEDURES_REPORT_QX Then

                    If TryCast(CType(INDgvProceduresQx.GetDetailView(INDgvProceduresQx.FocusedRowHandle, 0), GridView).DataSource, List(Of ViewSurgeriesPerformed)).Where(Function(w) w.Seleccione = 1 AndAlso w.GENSERVICEORDER Is Nothing).Count =
                            TryCast(CType(INDgvProceduresQx.GetDetailView(INDgvProceduresQx.FocusedRowHandle, 0), GridView).DataSource, List(Of ViewSurgeriesPerformed)).Where(Function(w) w.GENSERVICEORDER Is Nothing).Count Then

                        For Each headerRow In TryCast(CType(INDgvProceduresQx.GetDetailView(INDgvProceduresQx.FocusedRowHandle, 0), GridView).DataSource, List(Of ViewSurgeriesPerformed)).Where(Function(w) w.GENSERVICEORDER Is Nothing).ToList()
                            headerRow.Seleccione = 0
                        Next
                    Else
                        For Each headerRow In TryCast(CType(INDgvProceduresQx.GetDetailView(INDgvProceduresQx.FocusedRowHandle, 0), GridView).DataSource, List(Of ViewSurgeriesPerformed)).Where(Function(w) w.GENSERVICEORDER Is Nothing).ToList()
                            headerRow.Seleccione = 1
                        Next
                    End If

                    INDgcProceduresQx.RefreshDataSource()
                    Me.INDgcProceduresQx.Invalidate()
                End If
            End If
        End If
    End Sub
    '
    Private Sub INDgcProceduresNoQX_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcProceduresNoQX.MouseDoubleClick
        Dim hitPoint = Me.INDgvProceduresNoQx.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSelNoQx.Name) Then
                If CType(INDgcProceduresNoQX.DataSource, List(Of ViewServicesProceduresNoQx)).Where(Function(x) x.Seleccione AndAlso x.Tipo = "Realizados").Count() = CType(INDgcProceduresNoQX.DataSource, List(Of ViewServicesProceduresNoQx)).Where(Function(x) x.Tipo = "Realizados").Count() Then
                    For Each headerRow In CType(INDgcProceduresNoQX.DataSource, List(Of ViewServicesProceduresNoQx))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo = "Realizados" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = False
                        End If
                    Next
                Else
                    For Each headerRow In CType(INDgcProceduresNoQX.DataSource, List(Of ViewServicesProceduresNoQx))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo = "Realizados" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = True
                        End If
                    Next
                End If
                INDgcProceduresNoQX.RefreshDataSource()
                Me.INDgcProceduresNoQX.Invalidate()
            End If
        End If
    End Sub

    Private Sub INDgcConsultation_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcConsultation.MouseDoubleClick
        Dim hitPoint = Me.INDgvConsultation.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSelInterconsulta.Name) Then
                If CType(INDgcConsultation.DataSource, List(Of ViewServicesProceduresConsultation)).Where(Function(x) x.Seleccione AndAlso x.Tipo = "Con Respuesta").Count() = CType(INDgcConsultation.DataSource, List(Of ViewServicesProceduresConsultation)).Where(Function(x) x.Tipo = "Con Respuesta").Count() Then
                    For Each headerRow In CType(INDgcConsultation.DataSource, List(Of ViewServicesProceduresConsultation))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo = "Con Respuesta" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = False
                        End If
                    Next
                Else
                    For Each headerRow In CType(INDgcConsultation.DataSource, List(Of ViewServicesProceduresConsultation))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo = "Con Respuesta" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = True
                        End If
                    Next
                End If
                INDgcConsultation.RefreshDataSource()
                Me.INDgcConsultation.Invalidate()
            End If
        End If
    End Sub

    Private Sub INDgcTerapy_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcTerapy.MouseDoubleClick
        Dim hitPoint = Me.INDgvTherapy.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSelTerapy.Name) Then
                If CType(INDgcTerapy.DataSource, List(Of ViewServicesProceduresTherapy)).Where(Function(x) x.Seleccione).Count() = CType(INDgcTerapy.DataSource, List(Of ViewServicesProceduresTherapy)).Count Then
                    For Each headerRow In CType(INDgcTerapy.DataSource, List(Of ViewServicesProceduresTherapy))
                        If headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = False
                        End If
                    Next
                Else
                    For Each headerRow In CType(INDgcTerapy.DataSource, List(Of ViewServicesProceduresTherapy))
                        If headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = True
                        End If
                    Next
                End If
                INDgcTerapy.RefreshDataSource()
                Me.INDgcTerapy.Invalidate()
            End If
        End If
    End Sub

    Private Sub INDgcOxygenConsumption_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcOxygenConsumption.MouseDoubleClick
        Dim hitPoint = Me.INDgvOxygen.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSelOxygen.Name) Then
                If CType(INDgcOxygenConsumption.DataSource, List(Of ViewOxygenConsumption)).Where(Function(x) x.Seleccione).Count() = CType(INDgcOxygenConsumption.DataSource, List(Of ViewOxygenConsumption)).Count Then
                    CType(INDgcOxygenConsumption.DataSource, List(Of ViewOxygenConsumption)).ToList.ForEach(Sub(headerRow)
                                                                                                                headerRow.Seleccione = 0
                                                                                                            End Sub)
                Else
                    CType(INDgcOxygenConsumption.DataSource, List(Of ViewOxygenConsumption)).ToList.ForEach(Sub(headerRow)
                                                                                                                headerRow.Seleccione = 1
                                                                                                            End Sub)
                End If
                INDgcOxygenConsumption.RefreshDataSource()
                Me.INDgcOxygenConsumption.Invalidate()
            End If
        End If
    End Sub

    Private Sub INDgcNurseProcedure_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcNurseProcedure.MouseDoubleClick
        Dim hitPoint = Me.INDgvNursingProcedures.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(ColSelNursingProcedures.Name) Then
                If CType(INDgcNurseProcedure.DataSource, List(Of ViewServiceProceduresNursingProcedure)).Where(Function(x) x.Seleccione AndAlso x.Tipo <> "No Facturable").Count() = CType(INDgcNurseProcedure.DataSource, List(Of ViewServiceProceduresNursingProcedure)).Where(Function(x) x.Tipo <> "No Facturable").Count() Then
                    For Each headerRow In CType(INDgcNurseProcedure.DataSource, List(Of ViewServiceProceduresNursingProcedure))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo <> "No Facturable" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = False
                        End If
                    Next
                Else
                    For Each headerRow In CType(INDgcNurseProcedure.DataSource, List(Of ViewServiceProceduresNursingProcedure))
                        If headerRow IsNot Nothing AndAlso headerRow.Tipo <> "No Facturable" AndAlso headerRow.GENSERVICEORDER Is Nothing Then
                            headerRow.Seleccione = True
                        End If
                    Next
                End If
                INDgcNurseProcedure.RefreshDataSource()
                Me.INDgcNurseProcedure.Invalidate()
            End If
        End If
    End Sub

    Private Sub INDGcHemoDetail_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDGcHemoDetail.MouseDoubleClick
        Dim hitPoint = Me.INDGvHemoDetail.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(INDColSelHemo.Name) Then
                If CType(INDGcHemoDetail.DataSource, List(Of ViewServicesHemocomponent)).Where(Function(x) x.Seleccione = 1 AndAlso x.EstadoServicio = "Realizados").Count() = CType(INDGcHemoDetail.DataSource, List(Of ViewServicesHemocomponent)).Where(Function(x) x.EstadoServicio = "Realizados").Count() Then
                    For Each headerRow In CType(INDGcHemoDetail.DataSource, List(Of ViewServicesHemocomponent))
                        If headerRow IsNot Nothing AndAlso headerRow.EstadoServicio = "Realizados" Then
                            headerRow.Seleccione = 0
                            Dim services = datasourceServicesHemo.Where(Function(o) o.CODSERIPS = headerRow.CODSERIPS AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                            If services IsNot Nothing AndAlso services.Count > 0 Then
                                services.ForEach(Sub(o)
                                                     o.Seleccione = 0
                                                 End Sub)
                            End If
                        End If
                    Next
                Else
                    For Each headerRow In CType(INDGcHemoDetail.DataSource, List(Of ViewServicesHemocomponent))
                        If headerRow IsNot Nothing AndAlso headerRow.EstadoServicio = "Realizados" Then
                            headerRow.Seleccione = 1
                            Dim services = datasourceServicesHemo.Where(Function(o) o.CODSERIPS = headerRow.CODSERIPS AndAlso o.GENSERVICEORDER Is Nothing AndAlso o.EstadoServicio = "Realizados").ToList()
                            If services IsNot Nothing AndAlso services.Count > 0 Then
                                services.ForEach(Sub(o)
                                                     o.Seleccione = 1
                                                 End Sub)
                            End If
                        End If
                    Next
                End If
                INDGcHemoDetail.RefreshDataSource()
                Me.INDGcHemoDetail.Invalidate()
            End If
        End If
    End Sub
#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de la rejilla de servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemDateEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemDateEdit1.EditValueChanging
        'If e.NewValue IsNot Nothing Then
        '    If e.NewValue < INDgvDetailServiciosRealizados.GetFocusedRow().Fecha Then
        '        e.Cancel = True
        '        ShowMessage(eStatusResult.WARNING) = "La fecha de realización no puede ser inferior a la fecha de solicitud"
        '        Exit Sub
        '    End If
        '    If e.NewValue > GetDateServer() Then
        '        e.Cancel = True
        '        ShowMessage(eStatusResult.WARNING) = "La fecha de realización no puede ser superior a la fecha actual"
        '        Exit Sub
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' identifica si el elemento de la sub rejilla seleccionado tiene generada la orden de servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRIselector_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRIselector.EditValueChanging
        If ListPharmaDoseMSDetail.Result IsNot Nothing Then
            Dim obj = CType(INDGvMixingStation.GetDetailView(INDGvMixingStation.FocusedRowHandle, 0), GridView).GetFocusedRow()
            If obj?.DeliveryStatus IsNot Nothing AndAlso obj.DeliveryStatus = 2 Then
                e.NewValue = False
                Me.ShowMessage(EeventViewerImages.Advertencia) = "La dosis a seleccionar ya tiene una orden de servicio"
            End If
        End If
    End Sub
#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvDetailServicios_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgvDetailServiciosRealizados.PopupMenuShowing, INDgvDetailServiciosNoRealizados.PopupMenuShowing, INDGvHemoDetail.PopupMenuShowing
        Dim View = CType(sender, GridView)
        PopupMenu2.Manager = BarManager2
        PopupMenu2.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDbbiOpenRequest_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiOpenRequest.ItemClick
        Try
            Dim viewXpo As Object = Nothing

            Select Case INDtcgSources.SelectedTabPageName
                Case INDlcgLaboratories.Name
                    viewXpo = CType(INDgvLaboratories.GetFocusedRow(), ViewServicesProceduresLaboratories)
                Case INDlcgProceduresQX.Name
                    Dim headerRow = CType(INDgvProceduresQx.GetFocusedRow(), ViewProceduresQx)
                    If headerRow.Tipo = PROCEDURES_REPORT_QX Then
                        viewXpo = CType(INDgvDetailServiciosRealizados.GetFocusedRow(), ViewServicesProceduresReportQx)
                    Else
                        viewXpo = CType(INDgvDetailServiciosRealizados.GetFocusedRow(), ViewServicesProceduresQx)
                    End If
                Case INDlcgImagesDX.Name
                    viewXpo = CType(INDgvImagesDX.GetFocusedRow(), ViewServicesProceduresImagesDx)
                Case INDlcgPathology.Name
                    viewXpo = CType(INDgvPathologies.GetFocusedRow(), ViewServicesProceduresPathologies)
                Case INDlcgProceduresNoQX.Name
                    viewXpo = CType(INDgvDetailServiciosRealizados.GetFocusedRow(), ViewServicesProceduresNoQx)
                Case INDlcgConsultation.Name
                    viewXpo = CType(INDgvConsultation.GetFocusedRow(), ViewServicesProceduresConsultation)
                Case INDLcgHemo.Name
                    viewXpo = CType(INDGvHemoDetail.GetFocusedRow(), ViewServicesHemocomponent)
                Case INDlcgTerapy.Name
                    viewXpo = CType(INDgvTherapy.GetFocusedRow(), ViewServicesProceduresTherapy)
                Case INDlcgNurseProcedure.Name
                    viewXpo = CType(INDgvNursingProcedures.GetFocusedRow(), ViewServiceProceduresNursingProcedure)
            End Select

            If viewXpo Is Nothing Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud válida"
                Exit Sub
            End If

            If INDpccServicesProcedures.OwnerEdit IsNot Nothing Then
                INDpccServicesProcedures.OwnerEdit.ClosePopup()
            End If

            Using Formulario As New PopUpRequests(viewXpo.EntityName, viewXpo.Row)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.Width = 780
                Formulario.Height = 768
                Dim frm As New FrmTransparent(Formulario, False)
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            Me.ShowMessage(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "Methods"
    Private Function ListDetailProcedures(e As eServicioHistoriasClinicasOrdenesMedicas, todos As Boolean, Optional Reload As Boolean = False) As Task
        If e <> eServicioHistoriasClinicasOrdenesMedicas.OxygenConsumption AndAlso e <> eServicioHistoriasClinicasOrdenesMedicas.Valoration Then
            If INDgcDetailServiciosRealizados.InvokeRequired Then
                INDgcDetailServiciosRealizados.BeginInvoke(Sub()
                                                               INDgvDetailServiciosRealizados.ActiveFilterString = String.Empty
                                                               INDgcDetailServiciosRealizados.DataSource = Nothing
                                                               INDgvDetailServiciosNoRealizados.ActiveFilterString = String.Empty
                                                               INDgcDetailServiciosNoRealizados.DataSource = Nothing
                                                           End Sub)
            Else
                INDgvDetailServiciosRealizados.ActiveFilterString = String.Empty
                INDgcDetailServiciosRealizados.DataSource = Nothing
                INDgvDetailServiciosNoRealizados.ActiveFilterString = String.Empty
                INDgcDetailServiciosNoRealizados.DataSource = Nothing
            End If

        End If

        Return Task.Factory.StartNew(Sub()

                                         Dim filterCode As String = String.Empty
                                         Dim filterTipo As String = String.Empty
                                         Select Case e
                                             Case eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosQx
                                                 Dim filterNumeFolio As String = String.Empty
                                                 If Not todos Then
                                                     INDgcDetailServiciosRealizados.BeginInvoke(Sub()
                                                                                                    INDtpServiciosNoRealizados.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                                                                End Sub)
                                                     filterCode = INDgvProceduresQx.GetFocusedRowCellValue("CODSERIPS")
                                                     filterNumeFolio = INDgvProceduresQx.GetFocusedRowCellValue("NUMEFOLIO")
                                                 End If
                                                 Dim filterType = INDgvProceduresQx.GetFocusedRowCellValue("Tipo")
                                                 Using model As New MAccountControl(Me.Tag)
                                                     If datasourceServicesProceduresQx Is Nothing OrElse Reload Then
                                                         datasourceServicesProceduresQx = model.ListServicesProceduresQxByadmissionCodeIngreso(PatientCode, AdmissionCode).ToList()
                                                     End If
                                                     If datasourceServicesProceduresReportQx Is Nothing Then
                                                         datasourceServicesProceduresReportQx = model.ListServicesProceduresReportQxByadmissionCodeIngreso(PatientCode, AdmissionCode).ToList()
                                                     End If
                                                     If datasourceProceduresQxEquipe Is Nothing OrElse Reload Then
                                                         datasourceProceduresQxEquipe = model.ListQxEquipeByadmissionCodeIngreso(PatientCode, AdmissionCode).ToList()
                                                     End If
                                                     If datasourceProceduresQxInform Is Nothing OrElse Reload Then
                                                         datasourceProceduresQxInform = model.ListQxInformByadmissionCodeIngreso(PatientCode, AdmissionCode).ToList()
                                                     End If
                                                     INDgcDetailServiciosRealizados.BeginInvoke(Sub()
                                                                                                    If filterType = "Procedimientos con Informe QX" Then
                                                                                                        INDgcDetailServiciosRealizados.DataSource = datasourceServicesProceduresReportQx
                                                                                                        INDGcQxEquipe.DataSource = datasourceProceduresQxEquipe

                                                                                                        If Not String.IsNullOrEmpty(filterNumeFolio) Then
                                                                                                            Dim informe As ViewSurgicalReport = datasourceProceduresQxInform.Where(Function(x) x.CODSERIPS.Equals(filterCode))(0)
                                                                                                            If informe IsNot Nothing Then
                                                                                                                INDTxtObservaciones.Text = informe.DESPROCED
                                                                                                                INDMeHallazgosOperatorios.Text = informe.DESHALLOP
                                                                                                                INDTxtProcedimientosRealizados.Text = informe.DESPROCED
                                                                                                                INDTxtTipoAnestesia.Text = informe.TipoAnestesiaName
                                                                                                                INDTxtQxTime.Text = (New Date(informe.FECHORFIN.Ticks - informe.FECHORINI.Ticks)).TimeOfDay.ToString() 'informe.FECHORFIN - informe.FECHORINI
                                                                                                                If informe.NUMCANMUE = 0 Then
                                                                                                                    INDTxtMuestraPatologicas.Text = "Ninguna"
                                                                                                                    LciObservations.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                                                                                Else
                                                                                                                    INDTxtMuestraPatologicas.Text = informe.NUMCANMUE
                                                                                                                    LciObservations.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                                                                                End If
                                                                                                            End If
                                                                                                        End If
                                                                                                    Else
                                                                                                        INDgcDetailServiciosNoRealizados.DataSource = datasourceServicesProceduresQx
                                                                                                    End If
                                                                                                    INDgvDetailServiciosNoRealizados.ActiveFilterString = "CODSERIPS='" & filterCode & "'"
                                                                                                    INDGvQxEquipe.ActiveFilterString = "NUMEFOLIO='" & filterNumeFolio & "'"
                                                                                                End Sub)
                                                 End Using
                                         End Select

                                         INDsbGererateServiceOrder.SafeInvoke(Sub()
                                                                                  INDsbGererateServiceOrder.Enabled = True
                                                                              End Sub)

                                     End Sub)
    End Function

    Private Function ListarDetalleServiciosyProcedimientos(e As eServicioHistoriasClinicasOrdenesMedicas, todos As Boolean, Optional Reload As Boolean = False) As Task
        Return ListDetailProcedures(e, todos, Reload)
    End Function

    ''' <summary>
    ''' metodo pasa saber que columnas se muetran/ocultan en el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VistaDetalleRealizados(ByVal Tipo As eServicioHistoriasClinicasOrdenesMedicas)
        Select Case Tipo
            Case eServicioHistoriasClinicasOrdenesMedicas.Patologias, eServicioHistoriasClinicasOrdenesMedicas.Laboratorios, eServicioHistoriasClinicasOrdenesMedicas.Imagenes_Dx, eServicioHistoriasClinicasOrdenesMedicas.Interconsultas
                'muestro la interpretacion
                INDgvDetailServiciosRealizados.Columns("Folio").Visible = True
                INDgvDetailServiciosRealizados.Columns("Observacion").Visible = True
                INDgvDetailServiciosRealizados.Columns("Observacion").VisibleIndex = 4
                INDgvDetailServiciosRealizados.Columns("Interpretacion").Visible = True
                INDgvDetailServiciosRealizados.Columns("Interpretacion").VisibleIndex = 6
            Case eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosNoQx, eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosQx, eServicioHistoriasClinicasOrdenesMedicas.Hemocomponent
                'muestro la justificacion tecnico-cientifica y formato de procedimientos menores
                INDgvDetailServiciosRealizados.Columns("Folio").Visible = True
                INDgvDetailServiciosRealizados.Columns("Observacion").Visible = True
                INDgvDetailServiciosRealizados.Columns("Observacion").VisibleIndex = 4
                INDgvDetailServiciosRealizados.Columns("Interpretacion").Visible = False
            Case eServicioHistoriasClinicasOrdenesMedicas.Terapias, eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosEnfermeria
                INDgvDetailServiciosRealizados.Columns("Interpretacion").Visible = False
                INDgvDetailServiciosRealizados.Columns("Folio").Visible = False
                INDgvDetailServiciosRealizados.Columns("Observacion").Visible = False
            Case eServicioHistoriasClinicasOrdenesMedicas.Valoration

        End Select
    End Sub

    ''' <summary>
    ''' Carga la lista de unidades operativas
    ''' </summary>
    Public Async Sub LoadListOperatingUnit()
        Using model = New Security.MVP.MUsuario
            Dim _Company = UnifiedConfiguration.Instance.ListCompanies.Where(Function(c) c.Id = SessionValues.Instance.IndigoContainerId).FirstOrDefault
            Me.ListOperatingUnit = Await model.GetOperatingUnitByContainerPermission(SessionValues.Instance.IndigoContainerId, SessionValues.Instance.UserType, SessionValues.Instance.UserIndigo, _Company.Administrator)
            _idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        End Using
    End Sub

    Private Sub CleanControls()
        INDtcgSources.SelectedTabPageIndex = 0
        INDtcgSources.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSleAdmissionNumber2.EditValue = Nothing
        INDSleAdmissionNumber2.IsReadOnly = False
        INDSleAdmissionNumber2.DisplayNullText = String.Empty
        PatientCode = String.Empty
        AdmissionCode = String.Empty
        listServiceOrderDetail = Nothing
        _admissionObjectPOCO = Nothing
        _datasourceMedicines = False
        _datasourceMedicinesMixingStation = False
        _datasourceLaboratories = False
        _datasourcePathology = False
        _datasourceImagesDX = False
        _datasourceProceduresQX = False
        _datasourceProceduresNoQX = False
        _datasourceConsultation = False
        _datasourceTherapy = False
        _datasourceOxygen = False
        _datasourceNursing = False
        _datasourceReviews = False
        _datasourceHemo = False
        _datasourceStays = False
        INDliGenerateServiceOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        datasourceServicesProceduresQx = Nothing
        datasourceServicesProceduresReportQx = Nothing
        datasourceProceduresQxEquipe = Nothing
        datasourceProceduresQxInform = Nothing
        datasourceProceduresQxRealizados = Nothing
        datasourceServicesHemo = Nothing

        INDSleAdmissionNumber2.Focus()
        CleanControlsGrids()
    End Sub

    Private Sub CleanControlsGrids()
        INDgcMedicineSupplier.DataSource = Nothing
        INDgcLaboratories.DataSource = Nothing
        INDgcPathology.DataSource = Nothing
        INDgcImagesDx.DataSource = Nothing
        INDgcProceduresQx.DataSource = Nothing
        INDgcProceduresNoQX.DataSource = Nothing
        INDgcConsultation.DataSource = Nothing
        INDgcTerapy.DataSource = Nothing
        INDgcOxygenConsumption.DataSource = Nothing
        INDgcNurseProcedure.DataSource = Nothing
        INDgcValoraciones.DataSource = Nothing
        INDGcStays.DataSource = Nothing
        INDGcProceduresRealizados.DataSource = Nothing
        INDGcQxEquipe.DataSource = Nothing
        INDGcHemoDetail.DataSource = Nothing
        INDGcMixingStation.DataSource = Nothing
    End Sub

    Private Async Sub LoadControlsInfo(admissionObject As Object)
        'Consultamos los datasources de manera asincrona
        CleanControlsGrids()
        INDtcgSources.SelectedTabPageIndex = 0
        'AsyncLoader(True)
        _admissionObjectPOCO = admissionObject
        If _admissionObjectPOCO IsNot Nothing Then
            Me._treatmentType = admissionObject.TratamientoEspecial
            _stateAdmission = admissionObject.Status.ToString()
            AdmissionAtentionCenterCode = admissionObject.AdmissionCentAtencCodeName.ToString().Split(" - ")(0).ToString().Trim()
            LblState.Text = admissionObject.StatusFullName.ToString()
            PatientCode = admissionObject.PatientCode
            PatientCodeName = String.Concat(admissionObject.PatientCode.ToString().Trim(), " - ", admissionObject.PatientName.ToString().Trim())
            AdmissionCode = admissionObject.AdmissionCode
            LoadDatasourceMedicinesSupliers()
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
            INDtcgSources.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmLiquidation_AdmissionDontExists", "Billing")
        End If
    End Sub

    ''' <summary>
    ''' Muestra un mensaje en el frontal
    ''' </summary>
    ''' <param name="Icon">Icono del tipo de mensaje</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Sets the show message.
    ''' </summary>
    ''' <value>
    ''' The show message.
    ''' </value>
    Public WriteOnly Property ShowMessage(StatusCode As eStatusResult) As String
        Set(value As String)
            If StatusCode = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf StatusCode = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf StatusCode = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' FlagMixingStation =0 NORMAL, 1-Tarificacion del paquete central de mix, 2- generar orden de servicio central de mix
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="FlagMixingStation"></param>
    Private Async Sub _raiseEvent(e As List(Of ProductRateDetailPackage), FlagMixingStation As Byte, ListIdDose As List(Of Guid))
        If e.Count > 0 Then
            GenerateServiceOrder(Nothing, FlagMixingStation, e, ListIdDose)
        End If
    End Sub


    Private Async Sub GenerateServiceOrder(Optional listHomologation As List(Of List(Of CupsHomologation)) = Nothing, Optional FlagMixingStation As Byte = 0,
                                           Optional e As List(Of ProductRateDetailPackage) = Nothing, Optional ListIdDose As List(Of Guid) = Nothing, Optional arguments As Object = Nothing)
        Try
            AsyncLoader(True)

            'Se valida que hayan seleccionado la unidad operativa
            If SleOperatingUnit.EditValue Is Nothing OrElse SleOperatingUnit.EditValue = 0 Then
                ShowMessage(eStatusResult.WARNING) = "Debe seleccionar una unidad operativa"
                AsyncLoader(False)
                Exit Sub
            End If

            If Not ValidateCheckItems() Then
                AsyncLoader(False)
                Exit Sub
            End If
            Dim admissionState = Nothing
            Using model As New MLiquidation()
                admissionState = model.GetAdmissionByNumIngres(_admissionObjectPOCO.AdmissionCode).FirstOrDefault
            End Using
            If _stateOpen = False OrElse Not (_stateAdmission.Equals(" ") OrElse _stateAdmission.Equals("P") OrElse Not (_stateAdmission.Equals("B") AndAlso admissionState.IncomeLockType <> 1)) Then
                ShowMessage(eStatusResult.WARNING) = "El ingreso debe estar abierto para generar ordenes de servicio"
                AsyncLoader(False)
                Exit Sub
            End If

            Dim args As Object = New ExpandoObject()

            If arguments Is Nothing Then
                If _admissionObjectPOCO.AdmissionCaregroupId Is Nothing OrElse CInt(_admissionObjectPOCO.AdmissionCaregroupId <= 0) Then
                    ShowMessage(eStatusResult.WARNING) = "El ingreso no tiene asociado un grupo de atención"
                    AsyncLoader(False)
                    Exit Sub
                End If

                If FlagMixingStation = 1 Then
                    If ListPharmaDoseMSDetail.Result.Any(Function(x) x.Selected = True AndAlso x.AppliedDose = 0 And x.PaidToEndCampaign Is Nothing) Then
                        ShowMessage(eStatusResult.WARNING) = "Ha seleccionado dosis que no han sido aplicadas"
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    Using Formulario As New FrmPopUpGServiceOrderMS()
                        Formulario.ListPharmaDoseSelected = ListPharmaDoseMSDetail.Result.Where(Function(x) x.Selected = True And x.DeliveryStatus = 1).ToList()
                        Formulario.CareGroupId = _admissionObjectPOCO.AdmissionCaregroupId
                        Formulario.PermissionsForm = BarraBotones.PermissionsForm
                        Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.Top
                        Formulario.ViewModeEditHold = True
                        Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Formulario.ToolBar.Visible = True
                        Formulario.Width = 900
                        Formulario.Height = 700
                        AddHandler Formulario.GenerateServiceOrder, AddressOf _raiseEvent
                        Dim transparent = New Base.FrmTransparent(Formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                    AsyncLoader(False)
                    Exit Sub
                End If

                args.CareGroupId = _admissionObjectPOCO.AdmissionCaregroupId
                args.AdmissionNumber = AdmissionCode
                args.CareCenterCode = AdmissionAtentionCenterCode
                args.PatientCode = PatientCode

                'Se obtiene el grupo de atención, para saber si se envia el no. de autorización del ingreso
                Dim careGroupXpo As Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupReportXpo = Nothing
                Using modelAccountControl As New MAccountControl(Me.Tag)
                    careGroupXpo = modelAccountControl.GetCareGroupById(_admissionObjectPOCO.AdmissionCaregroupId)
                End Using
                If careGroupXpo IsNot Nothing AndAlso careGroupXpo.AuthorizationRequired Then
                    args.AutorizationNumber = _admissionObjectPOCO.AuthorizationNumber
                End If

                Dim myListDetail As New ConcurrentBag(Of Object)()
                Dim ahora As Date = GetDateServer()
                'DatasourceType: 1 - Laboratorios, 2 - ImagenesDx, 3 - Patologías, 4 - ProcedimientosNoQx, 5 - Interconsultas, 6 - Terapias, 7 - Procedimientos de Enfermeria, 8 - Consumo de Oxigeno, 9 - Valoraciones, 10- central de mezclas
                Dim validations As New StringBuilder()
                Select Case INDtcgSources.SelectedTabPageName
                    Case INDlcgLaboratories.Name
                        Dim itemDetail = CType(INDgcLaboratories.DataSource, List(Of ViewServicesProceduresLaboratories)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresLaboratories)
                                                         Dim detail As Object = New ExpandoObject()

                                                         If obj?.FechaRealizacion Is Nothing Then
                                                             validations.AppendLine($"Laboratorios: La fecha de realización para el servicio {obj.CODSERIPS} no puede venir vacía")
                                                             Exit Sub
                                                         End If

                                                         Dim Fecha As String = obj.Fecha
                                                         Dim FechaRealizacion As String = obj.FechaRealizacion

                                                         Dim AhoraTimeServer As String = ahora
                                                         Fecha = FormatDateTime(Fecha, DateFormat.ShortDate)
                                                         FechaRealizacion = FormatDateTime(FechaRealizacion, DateFormat.ShortDate)
                                                         AhoraTimeServer = FormatDateTime(ahora, DateFormat.ShortDate)

                                                         If CDate(FechaRealizacion) < CDate(Fecha) Then
                                                             validations.AppendLine($"Laboratorios: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser inferior a la fecha de solicitud")
                                                         End If

                                                         If CDate(FechaRealizacion) > CDate(AhoraTimeServer) Then
                                                             validations.AppendLine($"Laboratorios: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser superior a la fecha actual")
                                                         End If

                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.MedicoRealizo
                                                         detail.Date = obj.FechaRealizacion
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = obj.CANSERIPS
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 1
                                                         detail.RiasId = 0
                                                         detail.ContractDescriptionId = obj.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId

                                                         myListDetail.Add(detail)
                                                     End Sub)
                    Case INDlcgProceduresQX.Name
                        If datasourceServicesProceduresQx IsNot Nothing AndAlso datasourceProceduresQxRealizados IsNot Nothing Then
                            Dim itemDetail = datasourceProceduresQxRealizados.Where(Function(o) o.Seleccione = 1 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                            Parallel.ForEach(itemDetail, Sub(obj As ViewSurgeriesPerformed)
                                                             Dim detail As Object = New ExpandoObject()
                                                             Dim informeQx = datasourceProceduresQxInform.Where(Function(o) o.NUMEFOLIO = obj.NUMEFOLIO).FirstOrDefault()
                                                             detail.CupsEntityCode = obj.CODSERIPS
                                                             detail.FunctionalUnitCode = informeQx.UFUCODIGO
                                                             detail.ProfessionalCode = informeQx.MedicoRealizo
                                                             detail.Date = informeQx.FechaRealizacion
                                                             detail.ProfessionalSpecialistCode = informeQx.CODESPEC
                                                             detail.AdmissionCode = informeQx.NUMINGRES
                                                             detail.PatientCode = informeQx.IPCODPACI
                                                             detail.Quantity = obj.CANTIDAQX
                                                             detail.NitMedico = informeQx.CODIGONIT
                                                             detail.ReportQx = IIf(obj.Tipo = "Procedimientos con Informe QX", True, False)
                                                             detail.NumeFolio = obj.NUMEFOLIO
                                                             detail.Auto = obj.CONSECUQX
                                                             detail.RiasId = 0

                                                             detail.ContractDescriptionId = obj.ContractDescriptionId
                                                             detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId

                                                             myListDetail.Add(detail)
                                                         End Sub)
                        End If
                    Case INDlcgImagesDX.Name
                        Dim itemDetail = CType(INDgcImagesDx.DataSource, List(Of ViewServicesProceduresImagesDx)).Where(Function(o) o.Seleccione <> 0 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresImagesDx)
                                                         Dim detail As Object = New ExpandoObject()
                                                         If obj.FechaRealizacion < obj.Fecha Then
                                                             validations.AppendLine($"Imágenes Dx: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser inferior a la fecha de solicitud")
                                                         End If
                                                         If obj.FechaRealizacion > ahora Then
                                                             validations.AppendLine($"Imágenes: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser superior a la fecha actual")
                                                         End If
                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.MedicoRealizo
                                                         detail.Date = obj.FechaRealizacion
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = obj.CANSERIPS
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 2
                                                         detail.RiasId = 0
                                                         detail.ContractDescriptionId = obj.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId

                                                         myListDetail.Add(detail)
                                                     End Sub)
                    Case INDlcgPathology.Name
                        Dim itemDetail = CType(INDgcPathology.DataSource, List(Of ViewServicesProceduresPathologies)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresPathologies)
                                                         Dim detail As Object = New ExpandoObject()
                                                         If obj.FechaRealizacion < obj.Fecha Then
                                                             validations.AppendLine($"Patologías: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser inferior a la fecha de solicitud")
                                                         End If
                                                         If obj.FechaRealizacion > ahora Then
                                                             validations.AppendLine($"Patologías: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser superior a la fecha actual")
                                                         End If
                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.MedicoRealizo
                                                         detail.Date = obj.FechaRealizacion
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = obj.CANSERIPS
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 3
                                                         detail.RiasId = 0
                                                         detail.ContractDescriptionId = obj.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId

                                                         myListDetail.Add(detail)
                                                     End Sub)
                    Case INDlcgProceduresNoQX.Name
                        Dim itemDetail = CType(INDgcProceduresNoQX.DataSource, List(Of ViewServicesProceduresNoQx)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresNoQx)
                                                         Dim detail As Object = New ExpandoObject()
                                                         If obj.FechaRealizacion < obj.Fecha Then
                                                             validations.AppendLine($"Procedimientos No Qx: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser inferior a la fecha de solicitud")
                                                         End If
                                                         If obj.FechaRealizacion > ahora Then
                                                             validations.AppendLine($"Procedimientos No Qx: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser superior a la fecha actual")
                                                         End If
                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.MedicoRealizo
                                                         detail.Date = obj.FechaRealizacion
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = obj.CANSERIPS
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 4
                                                         detail.EntityName = obj.EntityName
                                                         detail.RiasId = 0
                                                         detail.ContractDescriptionId = obj.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId

                                                         myListDetail.Add(detail)
                                                     End Sub)
                    Case INDlcgConsultation.Name
                        Dim itemDetail = CType(INDgcConsultation.DataSource, List(Of ViewServicesProceduresConsultation)).Where(Function(o) o.Seleccione <> 0 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresConsultation)
                                                         Dim detail As Object = New ExpandoObject()
                                                         If obj.FechaRealizacion < obj.FechaSolicitud Then
                                                             validations.AppendLine($"Interconsultas: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser inferior a la fecha de solicitud")
                                                         End If
                                                         If obj.FechaRealizacion > ahora Then
                                                             validations.AppendLine($"Interconsultas: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser superior a la fecha actual")
                                                         End If
                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.MedicoRealizado
                                                         detail.Date = obj.FechaRealizacion
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = obj.CANSERIPS
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 5
                                                         detail.RiasId = 0
                                                         detail.ContractDescriptionId = obj.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId

                                                         myListDetail.Add(detail)
                                                     End Sub)
                    Case INDlcgTerapy.Name
                        Dim itemDetail = CType(INDgcTerapy.DataSource, List(Of ViewServicesProceduresTherapy)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewServicesProceduresTherapy)
                                                         Dim detail As Object = New ExpandoObject()
                                                         If obj.FechaRealizacion < obj.Fecha Then
                                                             validations.AppendLine($"Terapias: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser inferior a la fecha de solicitud")
                                                         End If
                                                         If obj.FechaRealizacion > ahora Then
                                                             validations.AppendLine($"Terapias: La fecha de realización para el servicio {obj.CODSERIPS} no puede ser superior a la fecha actual")
                                                         End If
                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.MedicoRealizo
                                                         detail.Date = obj.FechaRealizacion
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = obj.CANSERIPS
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 6
                                                         detail.RiasId = 0
                                                         detail.ContractDescriptionId = obj.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                                         myListDetail.Add(detail)
                                                     End Sub)
                    Case INDlcgNurseProcedure.Name
                        Dim itemDetail = CType(INDgcNurseProcedure.DataSource, List(Of ViewServiceProceduresNursingProcedure)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewServiceProceduresNursingProcedure)
                                                         Dim detail As Object = New ExpandoObject()
                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.CODPROSAL
                                                         detail.Date = obj.Fecha
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = obj.CANSERIPS
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 7
                                                         detail.RiasId = 0
                                                         detail.ContractDescriptionId = obj.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                                         myListDetail.Add(detail)
                                                     End Sub)
                    Case INDlcgOxigenConsumer.Name

                        Dim OptionQuantity As Byte = 1
                        OptionQuantity = careGroupXpo.TypeLiquidationOxygen

                        Dim itemDetail = CType(INDgcOxygenConsumption.DataSource, List(Of ViewOxygenConsumption)).Where(Function(o) o.Seleccione <> 0 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Dim errors As New List(Of String)()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewOxygenConsumption)
                                                         Dim detail As Object = New ExpandoObject()

                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.CODPROSAL
                                                         detail.Date = obj.DIAS
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = IIf(OptionQuantity = 1, obj.TOTLITADM, obj.TOTHORAS)
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 8
                                                         detail.RiasId = 0
                                                         myListDetail.Add(detail)

                                                         If obj.CODSERIPS Is Nothing AndAlso Not errors.Contains(obj.CODVIAADM) Then
                                                             errors.Add("No se ha asignado código CUPS en los parámetros de oxigeno para la vía de administración " + obj.CODVIAADM + " - " + obj.DESVIAADM)
                                                         End If

                                                     End Sub)
                        If errors.Count > 0 Then
                            ShowMessage(EeventViewerImages.Advertencia) = String.Join(vbCrLf, errors.Distinct().ToArray())
                            AsyncLoader(False)
                            Exit Sub
                        End If
                    Case INDlcgValoraciones.Name
                        Dim itemDetail = CType(INDgcValoraciones.DataSource, List(Of ViewReviews)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Dim errors As New List(Of String)()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewReviews)
                                                         Dim detail As Object = New ExpandoObject()
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.CODPROSAL
                                                         detail.Date = obj.FECHISPAC
                                                         detail.ProfessionalSpecialistCode = obj.CODESPECI
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = 1
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.ID
                                                         detail.CodEspTratante = obj.CODESPTRA
                                                         detail.Tratante = obj.Tratante
                                                         detail.FirstEmergencyCare = obj.FirstEmergencyCare
                                                         If obj.CUPSManejo Is Nothing Then
                                                             errors.Add($"No se ha asignado código CUPS de Manejo para la especialidad ({obj.CODESPECI.Trim()} - {obj.DESESPECI.Trim()})")
                                                         End If

                                                         If obj.FirstEmergencyCare Then
                                                             If obj.CupsEmergency Is Nothing Then
                                                                 errors.Add($"No se ha asignado código CUPS para el folio inicial de urgencias para la especialidad ({obj.CODESPECI.Trim()} - {obj.DESESPECI.Trim()})")
                                                             Else
                                                                 detail.CupsEntityCode = obj.CupsEmergency
                                                                 detail.ContractDescriptionId = obj.ContractDescriptionIdEmergency
                                                                 detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionIdEmergency
                                                             End If
                                                         ElseIf obj.Tratante Then
                                                             detail.CupsEntityCode = obj.CUPSManejo
                                                             detail.ContractDescriptionId = obj.ContractDescriptionIdManejo
                                                             detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionIdManejo
                                                         Else
                                                             detail.CupsEntityCode = obj.CUPSInterconsulta
                                                             detail.ContractDescriptionId = obj.ContractDescriptionIdInterconsulta
                                                             detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionIdInterconsulta
                                                         End If

                                                         detail.MultipleValoracion = False

                                                         detail.CupsManejo = obj.CUPSManejo
                                                         detail.ContractDescriptionIdManejo = obj.ContractDescriptionIdManejo
                                                         detail.CUPSEntityContractDescriptionIdManejo = obj.CUPSEntityContractDescriptionIdManejo

                                                         detail.CupsInterconsulta = obj.CUPSInterconsulta
                                                         detail.ContractDescriptionIdInterconsulta = obj.ContractDescriptionIdInterconsulta
                                                         detail.CUPSEntityContractDescriptionIdInterconsulta = obj.CUPSEntityContractDescriptionIdInterconsulta

                                                         detail.CupsControl = obj.CUPSControl

                                                         detail.DatasourceType = 9
                                                         detail.RiasId = 0
                                                         myListDetail.Add(detail)
                                                     End Sub)

                        'validamos los Items
                        'Si hay items tratantes y no se encuentra CUPSManejo se debe manejar como interconsultante
                        args.OptionsCUPS = 0
                        If Not (myListDetail.ToList().Count = 1 AndAlso myListDetail.ToList().FirstOrDefault.FirstEmergencyCare) Then
                            Using frm As New FrmPopUpOpcionCups()
                                Dim frmTrasnsp = New FrmTransparent(frm, False)
                                If myListDetail.ToList().Count = 1 AndAlso myListDetail.ToList().FirstOrDefault.Tratante Then
                                    frm.Options = 3
                                End If
                                If frmTrasnsp.ShowDialog(Me) = DialogResult.OK Then
                                    args.OptionsCUPS = frm.Options
                                Else
                                    AsyncLoader(False)
                                    Exit Sub
                                End If
                            End Using
                            Dim vr As ViewReviews
                            For Each dl In myListDetail.Where(Function(x) Not x.FirstEmergencyCare).ToList()
                                Select Case CByte(args.OptionsCUPS)
                                    Case 1
                                        dl.CupsEntityCode = dl.CupsInterconsulta
                                        dl.ContractDescriptionId = dl.ContractDescriptionIdInterconsulta
                                        dl.CUPSEntityContractDescriptionId = dl.CUPSEntityContractDescriptionIdInterconsulta
                                        If dl.CupsEntityCode Is Nothing Then
                                            vr = CType(INDgcValoraciones.DataSource, List(Of ViewReviews)).FirstOrDefault(Function(o) o.ID = dl.Auto)
                                            If vr IsNot Nothing Then
                                                errors.Add($"No se ha asignado código CUPS de Interconsulta para la especialidad ({vr.CODESPECI.Trim()} - {vr.DESESPECI.Trim()})")
                                            Else
                                                errors.Add($"No se ha asignado código CUPS de Interconsulta para la especialidad ({dl.ProfessionalSpecialistCode.Trim()})")
                                            End If
                                        End If
                                    Case 2
                                        dl.CupsEntityCode = dl.CupsControl
                                        dl.ContractDescriptionId = Nothing
                                        dl.CUPSEntityContractDescriptionId = Nothing
                                        If dl.CupsEntityCode Is Nothing Then
                                            vr = CType(INDgcValoraciones.DataSource, List(Of ViewReviews)).FirstOrDefault(Function(o) o.ID = dl.Auto)
                                            If vr IsNot Nothing Then
                                                errors.Add($"No se ha asignado código CUPS de Control para la especialidad ({vr.CODESPECI.Trim()} - {vr.DESESPECI.Trim()})")
                                            Else
                                                errors.Add($"No se ha asignado código CUPS de Control para la especialidad ({dl.ProfessionalSpecialistCode.Trim()})")
                                            End If
                                        End If
                                    Case 3
                                        dl.CupsEntityCode = dl.CupsManejo
                                        dl.ContractDescriptionId = dl.ContractDescriptionIdManejo
                                        dl.CUPSEntityContractDescriptionId = dl.CUPSEntityContractDescriptionIdManejo
                                        If dl.CupsEntityCode Is Nothing Then
                                            vr = CType(INDgcValoraciones.DataSource, List(Of ViewReviews)).FirstOrDefault(Function(o) o.ID = dl.Auto)
                                            If vr IsNot Nothing Then
                                                errors.Add($"No se ha asignado código CUPS de Manejo para la especialidad ({vr.CODESPECI.Trim()} - {vr.DESESPECI.Trim()})")
                                            Else
                                                errors.Add($"No se ha asignado código CUPS de Manejo para la especialidad ({dl.ProfessionalSpecialistCode.Trim()})")
                                            End If
                                        End If
                                End Select
                            Next
                        End If
                        If errors.Count > 0 Then
                            ShowMessage(EeventViewerImages.Advertencia) = String.Join(vbCrLf, errors.Distinct().ToArray())
                            AsyncLoader(False)
                            Exit Sub
                        End If
                    Case INDLcgHemo.Name
                        Dim itemDetail = datasourceServicesHemo.Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        Parallel.ForEach(itemDetail, Sub(obj As ViewServicesHemocomponent)
                                                         Dim detail As Object = New ExpandoObject()
                                                         detail.CupsEntityCode = obj.CODSERIPS
                                                         detail.FunctionalUnitCode = obj.UFUCODIGO
                                                         detail.ProfessionalCode = obj.CODPROSAL
                                                         detail.Date = obj.Fecha
                                                         detail.ProfessionalSpecialistCode = obj.CODESPEC1
                                                         detail.AdmissionCode = obj.NUMINGRES
                                                         detail.PatientCode = obj.IPCODPACI
                                                         detail.Quantity = obj.CANSERIPS
                                                         detail.NitMedico = obj.NitMedico
                                                         detail.Auto = obj.Row
                                                         detail.DatasourceType = 10
                                                         detail.RiasId = 0
                                                         detail.ContractDescriptionId = obj.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                                         myListDetail.Add(detail)
                                                     End Sub)
                    Case INDLycgMixingStation.Name
                        Dim Lock As Object = New Object
                        Dim ItemDetail = e
                        Parallel.ForEach(ItemDetail, Sub(obj As ProductRateDetailPackage)
                                                         Dim detail As Object = New ExpandoObject()
                                                         detail.ProductRateDetailPackage = obj
                                                         detail.Id = obj.Id
                                                         detail.ProductRateDetailId = obj.ProductRateDetailId
                                                         detail.PackageDetailId = obj.PackageDetailId
                                                         detail.ProductId = obj.ProductId
                                                         detail.Quantity = obj.Quantity
                                                         detail.DoseNumber = obj.DoseNumber
                                                         detail.ProductCodeName = obj.ProductCodeName
                                                         detail.TotalQuantities = obj.TotalQuantities
                                                         detail.UnitValue = obj.UnitValue
                                                         detail.TotalValue = obj.TotalValue
                                                         detail.FunctionalUnitId = obj.FunctionalUnitId
                                                         detail.SurchargeApply = obj.SurchargeApply
                                                         detail.OrderedHealthProfessionalCode = obj.OrderedHealthProfessionalCode
                                                         detail.OrderedHealthProfessionalThirdPartyId = obj.OrderedHealthProfessionalThirdPartyId
                                                         detail.HealthAdministratorId = obj.HealthAdministratorId
                                                         detail.PerformsProfessionalSpecialty = obj.PerformsProfessionalSpecialty
                                                         detail.WarehouseId = obj.WarehouseId
                                                         detail.ProductGroupId = obj.InventoryProduct.ProductGroupId
                                                         detail.FinalProductCost = obj.InventoryProduct.FinalProductCost
                                                         detail.ProductCost = obj.InventoryProduct.ProductCost
                                                         detail.DatasourceType = 11
                                                         SyncLock Lock
                                                             myListDetail.Add(detail)
                                                         End SyncLock
                                                     End Sub)
                        args.ListIdDose = ListIdDose
                    Case INDLcgStays.Name
                        Dim itemDetail = CType(INDGcStays.DataSource, List(Of StayInfoModel)).Where(Function(o) o.Selected AndAlso o.GENSERVICEORDER Is Nothing).ToList()
                        args.DatasourceType = 12
                        Parallel.ForEach(itemDetail, Sub(m As StayInfoModel)
                                                         Dim detail As Object = New ExpandoObject()
                                                         detail.CupsEntityCode = m.CUPsCodeName.Split(" - ")(0).Trim()
                                                         detail.FunctionalUnitCode = m.FunctionalUnitCode
                                                         detail.ProfessionalCode = m.Stay.CODPROSAL
                                                         detail.Date = m.EndDate '??
                                                         detail.ProfessionalSpecialistCode = m.Stay.CODESPECI
                                                         detail.AdmissionCode = m.AdmissionCode
                                                         detail.PatientCode = m.Stay.IPCODPACI
                                                         detail.Quantity = 1
                                                         detail.NitMedico = m.Stay.INPROFSAL.CODIGONIT
                                                         detail.Auto = m.Stay.ID
                                                         detail.DatasourceType = 12
                                                         detail.CupsId = m.CUPSId

                                                         detail.ContractDescriptionId = m.ContractDescriptionId
                                                         detail.CUPSEntityContractDescriptionId = m.CUPSEntityContractDescriptionId

                                                         detail.InitialDate = m.InitialDate
                                                         detail.EndDate = m.EndDate
                                                         detail.Stay = JsonConvert.SerializeObject(m.Stay)
                                                         myListDetail.Add(detail)
                                                     End Sub)
                End Select
                If validations.Length > 0 Then
                    ShowMessage(EeventViewerImages.Advertencia) = validations.ToString()
                    AsyncLoader(False)
                    Exit Sub
                End If

                If _admissionObjectPOCO.HealthAdministratorId IsNot Nothing AndAlso CInt(_admissionObjectPOCO.HealthAdministratorId) > 0 Then
                    args.HealthAdministratorId = _admissionObjectPOCO.HealthAdministratorId
                    If _admissionObjectPOCO.HealthAdministratorId IsNot Nothing Then
                        Using md As New MHealthAdministrator(Me.Tag)
                            Dim healthAdm As ActionResult(Of HealthAdministrator) = md.GetHealthAdministratorByIdSimple(_admissionObjectPOCO.HealthAdministratorId)
                            If healthAdm.StateResult AndAlso healthAdm.ObjectEmbbeded IsNot Nothing Then
                                args.ThirdPartyId = healthAdm.ObjectEmbbeded.ThirdPartyId
                            End If
                        End Using
                    End If
                End If

                args.OperativeUnitId = IdOperatingUnitSelected 'BarraBotones.OperatingUnitValue
                args.IsProcedureQx = INDtcgSources.SelectedTabPageName = INDlcgProceduresQX.Name
                args.Details = myListDetail

                Using _model As New MAccountControl(Me.Tag)
                    'Listado de códigos cups para validar si son suceptibles a autorización
                    Dim listCupsCodes As New List(Of String)

                    'Listado de códigos de unidades funcionales
                    Dim listFunctionalUnitCodes As New List(Of String)

                    If FlagMixingStation = 0 Then
                        For Each x In args.Details 'Se recorre el listado para obtener los códigos respectivos
                            listCupsCodes.Add("'" & x.CupsEntityCode.ToString().Trim() & "'")
                            listFunctionalUnitCodes.Add("'" & x.FunctionalUnitCode.ToString().Trim() & "'")
                        Next

                        If Me._treatmentType IsNot Nothing AndAlso Me._treatmentType = 3 AndAlso INDtcgSources.SelectedTabPageName = INDlcgProceduresNoQX.Name Then
                            'Listado que se asigna cuando se abre el modal para seleccionar las autorizaciones
                            Dim ListViewTraceabilityPaperworkAuthorizedXpo As List(Of ViewTraceabilityPaperworkAuthorizedXpo) = Nothing

                            'Se valida si los cups son suceptibles a autorización
                            Dim listValidationSusceptible = PresenterControlOutpatientServices.ValidateCUPSSusceptible(listCupsCodes, args.CareGroupId)
                            If listValidationSusceptible IsNot Nothing AndAlso listValidationSusceptible.Count > 0 Then
                                Dim codesString = String.Join(",", listValidationSusceptible.ToArray())
                                If MessageIndigo.Show("Los siguientes CUPS " & codesString & " son suceptibles a autorización, Desea asociar una autorización?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                                    Using Formulario As New FrmAsociateAuthorization()
                                        Formulario.CareCenterCode = AdmissionAtentionCenterCode
                                        Formulario.FunctionalUnitCodes = String.Join(",", listFunctionalUnitCodes.ToArray())
                                        Formulario.PatientCode = PatientCode
                                        Formulario.ListCupsCodes = listValidationSusceptible
                                        Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                                        Formulario.ViewModeEditHold = True
                                        Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                                        Formulario.ToolBar.Visible = False
                                        Formulario.Width = 900
                                        Formulario.Height = 500
                                        Dim frm As New FrmTransparent(Formulario, False)
                                        Me.Cursor = System.Windows.Forms.Cursors.Default
                                        frm.ShowDialog(Me)

                                        If Formulario.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                                            AsyncLoader(False)
                                            Exit Sub
                                        End If

                                        ListViewTraceabilityPaperworkAuthorizedXpo = Formulario.ListViewTraceabilityPaperworkAuthorizedXpo

                                        If ListViewTraceabilityPaperworkAuthorizedXpo IsNot Nothing AndAlso ListViewTraceabilityPaperworkAuthorizedXpo.Count > 0 Then
                                            For Each itemArgs In args.Details
                                                Dim itemXpo = (From x In ListViewTraceabilityPaperworkAuthorizedXpo Where x.ServiceCode.Trim() = itemArgs.CupsEntityCode.ToString().Trim()).FirstOrDefault()
                                                If itemXpo IsNot Nothing Then
                                                    itemArgs.TraceabilityPaperworkEventsId = itemXpo.TraceabilityPaperworkEventsId
                                                    itemArgs.AuthorizationNumber = itemXpo.AuthorizationNumber
                                                End If
                                            Next
                                        End If
                                    End Using
                                End If
                            End If
                        End If

                        'Se valida si los cups necesitan asociar una cotización
                        Dim messageQuoted = _model.GetCUPSWithQuoted(args, args.CareGroupId)
                        If Not String.IsNullOrEmpty(messageQuoted.Item1) Then
                            If MessageIndigo.Show(messageQuoted.Item1, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                                If Me.RequestQuoteServices Then
                                    AsyncLoader(False)
                                    Exit Sub
                                End If
                            Else
                                Me.Cursor = ChangeCursorIndigo()
                                Using Formulario As New FrmImportQuotation()
                                    AddHandler Formulario.ImportEvent, AddressOf ImportInfo
                                    Formulario.PatientCode = PatientCode.Trim()
                                    Formulario.ListCupsEntityId = messageQuoted.Item2
                                    Formulario.arg = args
                                    Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                                    Formulario.ViewModeEditHold = True
                                    Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                                    Formulario.Width = 1054
                                    Formulario.Height = 500
                                    Dim frm As New FrmTransparent(Formulario, False)
                                    Me.Cursor = System.Windows.Forms.Cursors.Default
                                    frm.ShowDialog(Me)

                                    If Formulario.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                                        AsyncLoader(False)
                                        Exit Sub
                                    End If
                                End Using
                            End If
                        End If
                    End If
                End Using

            Else
                args = arguments
            End If

            Using model As New MAccountControl(Me.Tag)

                Dim result As ActionResult(Of List(Of List(Of CupsHomologation))) = Await model.GenerateServiceOrderMassive(args, listHomologation)
                If result.StatusCode <> eStatusResult.SUCCESS AndAlso result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    AsyncLoader(False)
                    If result.MessageResult IsNot Nothing Then 'Hay multiples homologaciones
                        Dim formulario As New FrmHomologationsCups
                        AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                        formulario.CareCenterCode = AdmissionAtentionCenterCode
                        formulario.IsQx = CBool(args.IsProcedureQx)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        formulario.ListHomologation = result.ObjectEmbbeded
                        formulario.arguments = args
                        Dim transParent As New FrmTransparent(formulario, False)
                        transParent.ShowDialog(Me)
                    Else
                        OpenFormServiceOrderDetailHomologation(result.ObjectEmbbeded, args)
                    End If
                Else
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        ShowMessage(EeventViewerImages.Informacion) = String.Format("Se generó una órden de servicio con código {0}", result.Message)
                        If result.MessageResult IsNot Nothing AndAlso Not result.MessageResult(0).Equals(String.Empty) Then
                            ShowMessage(EeventViewerImages.Informacion) = result.MessageResult(0)
                        End If
                        BeginReloadDatasource()
                    Else
                        ShowMessage(result.StatusCode) = result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al aceptar las cotizaciones
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub ImportInfo(e As AddImportQuotationServiceOrderDetail)
        If e IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo.Count > 0 Then
            For Each itemArgs In e.arg.Details
                Dim quotationServiceOrderDetailXpo = (From x In e.ListQuotationServiceOrderDetailXpo Where x.CUPSEntityId.Code = itemArgs.CupsEntityCode.ToString().Trim()).FirstOrDefault()
                If quotationServiceOrderDetailXpo IsNot Nothing Then
                    itemArgs.QuotationServiceOrderDetailId = quotationServiceOrderDetailXpo.Id
                End If
            Next
        End If
    End Sub

    Private Sub ReturnSetHomologation(sender As Object, e As HomologationCupsEventArgs)
        If e.IsProcedureQx Then
            OpenFormServiceOrderDetailHomologation(e.ListHomologations, e.arguments)
        Else
            GenerateServiceOrder(e.ListHomologations, 0, Nothing, Nothing, e.arguments)
        End If
    End Sub

    Private Async Sub OpenFormServiceOrderDetailHomologation(listHomologation As List(Of List(Of CupsHomologation)), args As Object)
        AsyncLoader(True)
        'Dim listServiceOrderDetailQx As New List(Of ServiceOrderDetail)()
        Dim result As ActionResult(Of ServiceOrder)
        Using model As New MAccountControl(Me.Tag)
            result = Await model.GetServiceOrderDetailHomologation(CInt(args.CareGroupId), listHomologation, args)
            If result.StateResult Then
                'listServiceOrderDetailQx = result.ObjectEmbbeded.ServiceOrderDetail.ToList()
            Else
                AsyncLoader(False)
                ShowMessage(EeventViewerImages.Advertencia) = result.Message
                Exit Sub
            End If
        End Using
        Dim listServiceOrderDetailPopup = result.ObjectEmbbeded

        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New FrmPopUpServiceOrderDetailQx
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.listServiceOrderDetailPopup = listServiceOrderDetailPopup.ServiceOrderDetail.ToList()
            formulario.Admission = AdmissionCode
            formulario.Patient = PatientCodeName
            If _admissionObjectPOCO.BedStay Is Nothing Then
                formulario.Stay = ""
            Else
                formulario.Stay = _admissionObjectPOCO.BedStay.ToString().Trim()
            End If
            formulario.PatientDateBirth = _admissionObjectPOCO.PatientBirth
            formulario.PatientGenus = _admissionObjectPOCO.PatientGenus
            formulario.AdmissionDate = CDate(_admissionObjectPOCO.AdmissionDate)
            formulario.EditMode = False
            AddHandler formulario.AddServiceOrderDetail, AddressOf ReturAddServiceOrderDetail
            formulario.ListServiceOrderDetailSurgicalIntervention = listServiceOrderDetailPopup.ServiceOrderDetail.ToList()
            formulario.ListServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
            Using model As New MServiceOrder(Me.Tag)
                formulario.ListServiceOrderDetailDatasourceIncludeService.AddRange(model.ListServiceOrderDetailsByAdmissionNumber(AdmissionCode))
            End Using
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
            AsyncLoader(False)
        End Using
    End Sub

    Private Async Sub ReturAddServiceOrderDetail(sender As Object, e As AddServiceEventArgs)
        Dim form As FrmPopUpServiceOrderDetailQx = CType(sender, FrmPopUpServiceOrderDetailQx)
        listServiceOrderDetail = New List(Of ServiceOrderDetail)
        Dim errorsEvent As New StringBuilder
        'recalculamos para saber que item va coomo primer evento y asi aplicar o no los porcentajes
        For Each item In e.ListServiceOrderDetail
            If item.Presentation = 2 Then
                If item.SurgicalInterventionType <> 1 Then
                    Dim detailFirstEvent = listServiceOrderDetail.Find(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.IsFirstEvent = True)
                    If detailFirstEvent IsNot Nothing Then
                        If item.SubTotalSalesPrice > detailFirstEvent.SubTotalSalesPrice Then

                            'valido que los items no esten bloqueados o facturados en los folios  
                            Dim listEventsTmp = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.Id > 0)
                            For Each itemEvent In listEventsTmp
                                If itemEvent.IsPackage Then
                                    errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el servicio " + itemEvent.CodeNameIpsService + " esta empaquetado")
                                    form.AsyncLoader(False)
                                    Exit For
                                End If
                                Using model As New MServiceOrder(Me.Tag)
                                    'valido que los items no esten distribuidos
                                    Dim listDistribution = model.GetServiceOrderDetailDistributionByServideOrderDetailId(itemEvent.Id)
                                    If listDistribution.Count > 1 Then
                                        errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el servicio " + itemEvent.CodeNameIpsService + " esta distribuido")
                                        form.AsyncLoader(False)
                                        Exit For
                                    End If
                                    'valido que los folios no esten bloqueado o facturados
                                    Dim revenueControlDetail = model.GetRevenueControlDetailByServiceOrderDetailId(itemEvent.Id)
                                    If revenueControlDetail.Status > 1 Then
                                        If revenueControlDetail.Status = 2 Then
                                            errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta facturado")
                                        Else
                                            errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta bloqueado")
                                        End If
                                        form.AsyncLoader(False)
                                        Exit For
                                    End If
                                End Using
                            Next

                            If errorsEvent.Length > 0 Then
                                Continue For
                            End If
                            item.IsFirstEvent = True
                            If item.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(item)
                            End If
                            detailFirstEvent.IsFirstEvent = False
                            If detailFirstEvent.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(detailFirstEvent)
                            End If
                        Else
                            item.IsFirstEvent = False
                            If item.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(item)
                            End If
                        End If
                    Else
                        item.IsFirstEvent = True
                        If item.SurgicalInterventionType < 9 Then
                            GetValueSurgicalEvenst(item)
                        End If
                    End If
                End If
            End If
            listServiceOrderDetail.Add(item)
        Next

        ValidateMIVIE()
        If errorsEvent.Length > 0 Then
            ShowMessage(EeventViewerImages.Advertencia) = errorsEvent.ToString()
            form.AsyncLoader(False)
            Exit Sub
        End If

        If BarraBotones.OperatingUnitValue = 0 Then
            ShowMessage(EeventViewerImages.Advertencia) = "Seleccione una unidad Operativa"
            form.AsyncLoader(False)
            Exit Sub
        End If
        Dim args As Object = New ExpandoObject()
        args.CareGroupId = _admissionObjectPOCO.AdmissionCaregroupId
        args.AdmissionNumber = AdmissionCode
        args.CareCenterCode = AdmissionAtentionCenterCode
        args.PatientCode = PatientCode
        args.OperativeUnitId = BarraBotones.OperatingUnitValue
        args.EntityName = "AccountControl"
        args.IsProcedureQx = INDtcgSources.SelectedTabPageName = INDlcgProceduresQX.Name
        Dim myListDetail As New ConcurrentBag(Of Object)()

        If datasourceProceduresQxInform IsNot Nothing Then
            Dim itemDetail = datasourceProceduresQxRealizados.Where(Function(o) o.Seleccione = 1 AndAlso o.GENSERVICEORDER Is Nothing).ToList()
            For Each itemD In itemDetail
                Dim detail As Object = New ExpandoObject()
                detail.ReportQx = IIf(itemD.Tipo = "Procedimientos con Informe QX", True, False)
                detail.Auto = itemD.CONSECUQX
                myListDetail.Add(detail)
            Next
        End If
        args.Details = myListDetail
        Using model As New MAccountControl(Me.Tag)
            AsyncLoader(True)
            Dim result As ActionResult = Await model.GenerateServiceOrderMassiveWithListDetail(args, listServiceOrderDetail)
            AsyncLoader(False)
            form.AsyncLoader(False)
            If Not result.StateResult Then
                ShowMessage(EeventViewerImages.Advertencia) = result.Message
            Else
                listServiceOrderDetail = Nothing
                form.CanForceClose = True
                form.Close()
                ShowMessage(EeventViewerImages.Informacion) = String.Format("Se generó una órden de servicio con código {0}", result.Message)
                If result.MessageResult IsNot Nothing AndAlso Not result.MessageResult(0).Equals(String.Empty) Then
                    ShowMessage(EeventViewerImages.Informacion) = result.MessageResult(0)
                End If
                BeginReloadDatasource()
            End If
        End Using
    End Sub

    Public Sub ValidateMIVIE()
        Dim listEvents = (From e In listServiceOrderDetail Where e.SettlementType = 1 Select e.SurgeryNumber).Distinct().ToList()
        For item As Integer = 0 To listEvents.Count - 1 Step 1
            Dim firstEvent = listServiceOrderDetail.Find(Function(x) x.IsFirstEvent = True And x.SurgeryNumber = listEvents(item))
            Dim index = 2
            Dim listMIVIE = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = listEvents(item) AndAlso x.RateManualType < 3 AndAlso x.SurgicalInterventionType IsNot Nothing AndAlso x.SurgicalInterventionType = 3 And (x.LiquidateAllMIVIE Is Nothing OrElse Not x.LiquidateAllMIVIE))
            If listMIVIE.Count > 2 Then
                listMIVIE = (From l In listMIVIE Order By l.RateManualSalePrice Descending).ToList()
                If listMIVIE.Exists(Function(x) x.IsFirstEvent = True) Then
                    listMIVIE.Remove(firstEvent)
                    index = 1
                End If
                For i As Integer = index To listMIVIE.Count - 1 Step 1
                    listMIVIE.ElementAt(i).SubTotalSalesPrice = 0
                    listMIVIE.ElementAt(i).TotalSalesPrice = 0
                    listMIVIE.ElementAt(i).GrandTotalSalesPrice = 0
                    For Each itemSurgical In listMIVIE.ElementAt(i).ServiceOrderDetailSurgical
                        itemSurgical.TotalSalesPrice = 0
                    Next
                Next
            End If
        Next
    End Sub

    Private Sub GetValueSurgicalEvenst(serviceOrdeDetailItem As ServiceOrderDetail)
        If serviceOrdeDetailItem.RateManualId Is Nothing Then
            Exit Sub
        End If
        Using model As New MServiceOrder(Me.Tag)
            Dim SurgeriesPercentageManual = model.GetSurgeriesPercetageManualByRateManualIdInterventionType(serviceOrdeDetailItem.RateManualId, serviceOrdeDetailItem.SurgicalInterventionType)
            If serviceOrdeDetailItem.IsFirstEvent = True Then
                If SurgeriesPercentageManual.MainHundredPercent = False Then
                    For Each item In serviceOrdeDetailItem.ServiceOrderDetailSurgical
                        Select Case item.ClassServiceIps?.ToUpper()
                            Case ResourceManager.GetString("Surgeon", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.SurgeonPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.AnesthesiologistPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("Assistant", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.AssistantPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("RightRoom", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.RoomPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("SutureMaterials", "Contract").ToUpper()
                                item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.MaterialsPercentage / 100
                        End Select
                    Next
                End If
            Else
                For Each item In serviceOrdeDetailItem.ServiceOrderDetailSurgical
                    Select Case item.ClassServiceIps?.ToUpper()
                        Case ResourceManager.GetString("Surgeon", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.SurgeonPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        Case ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.AnesthesiologistPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        Case ResourceManager.GetString("Assistant", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.AssistantPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        Case ResourceManager.GetString("RightRoom", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.RoomPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        Case ResourceManager.GetString("SutureMaterials", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.MaterialsPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                    End Select
                Next
            End If

            With serviceOrdeDetailItem
                '/**************--Segmento Impuestos--**********************/
                'se suman el detalle de los qx
                .SubTotalSalesPrice = .ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                .GrossValue = .SubTotalSalesPrice
                .TaxValue = 0
                'independientmente si viene de detalles qx o no al final en base a la parametrizacion del servicio y el sistema se establece el valor bruto, el iva y el subtotal
                Dim _dictionaryValues = Utils.SetValueSalesPrice(Me._flagTaxInclude,
                                                                  .SubTotalSalesPrice,
                                                                  ?.TaxPercent)
                If?.TaxedService AndAlso _dictionaryValues?.Any() Then
                    .GrossValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                    .TaxValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                    .SubTotalSalesPrice = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
                    If .SubTotalSalesPrice > .RoundService Then
                        .SubTotalSalesPrice = Utils.RoundValue(.SubTotalSalesPrice, .RoundService)
                    End If
                End If
                '/***************************************************************/
                .TotalSalesPrice = .SubTotalSalesPrice - .ThirdPartyDiscount
                .GrandTotalSalesPrice = .TotalSalesPrice * .InvoicedQuantity
            End With
        End Using
    End Sub

    Private Function ValidateCheckItems(Optional _justificationFlag As Boolean = False) As Boolean
        Dim validationResult As Boolean = True
        Dim validationRealizatedResult As Boolean = True
        Select Case INDtcgSources.SelectedTabPageName
            Case INDlcgLaboratories.Name
                If CType(INDgcLaboratories.DataSource, List(Of ViewServicesProceduresLaboratories)).Where(Function(o) o.Seleccione <> 0 AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDlcgProceduresQX.Name
                If datasourceProceduresQxRealizados.Where(Function(o) o.Seleccione = 1 AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If

                If datasourceProceduresQxRealizados.Where(Function(o) o.Seleccione = 1 AndAlso (o.MedicoRealizo Is Nothing OrElse o.FechaRealizacion Is Nothing)).ToList().Count = 0 Then
                    validationRealizatedResult = False
                End If

            Case INDlcgImagesDX.Name
                If CType(INDgcImagesDx.DataSource, List(Of ViewServicesProceduresImagesDx)).Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
                If CType(INDgcImagesDx.DataSource, List(Of ViewServicesProceduresImagesDx)).Where(Function(o) o.Seleccione AndAlso o.GENSERVICEORDER Is Nothing AndAlso (o.MedicoRealizo Is Nothing OrElse o.FechaRealizacion Is Nothing)).ToList().Count > 0 Then
                    validationRealizatedResult = False
                End If
            Case INDlcgPathology.Name
                If CType(INDgcPathology.DataSource, List(Of ViewServicesProceduresPathologies)).Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
                If CType(INDgcPathology.DataSource, List(Of ViewServicesProceduresPathologies)).Where(Function(o) o.Seleccione = True AndAlso (o.MedicoRealizo Is Nothing OrElse o.FechaRealizacion Is Nothing)).ToList().Count > 0 Then
                    validationRealizatedResult = False
                End If
            Case INDlcgProceduresNoQX.Name
                If CType(INDgcProceduresNoQX.DataSource, List(Of ViewServicesProceduresNoQx)).Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
                If CType(INDgcProceduresNoQX.DataSource, List(Of ViewServicesProceduresNoQx)).Where(Function(o) o.Seleccione = True AndAlso (o.MedicoRealizo Is Nothing OrElse o.FechaRealizacion Is Nothing)).ToList().Count > 0 Then
                    validationRealizatedResult = False
                End If
            Case INDlcgConsultation.Name
                If CType(INDgcConsultation.DataSource, List(Of ViewServicesProceduresConsultation)).Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
                If CType(INDgcConsultation.DataSource, List(Of ViewServicesProceduresConsultation)).Where(Function(o) o.Seleccione = True AndAlso (o.MedicoRealizado Is Nothing OrElse o.FechaRealizacion Is Nothing)).ToList().Count > 0 Then
                    validationRealizatedResult = False
                End If
            Case INDlcgTerapy.Name
                If CType(INDgcTerapy.DataSource, List(Of ViewServicesProceduresTherapy)).Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
                If CType(INDgcTerapy.DataSource, List(Of ViewServicesProceduresTherapy)).Where(Function(o) o.Seleccione = True AndAlso (o.MedicoRealizo Is Nothing OrElse o.FechaRealizacion Is Nothing)).ToList().Count > 0 Then
                    validationRealizatedResult = False
                End If
            Case INDlcgNurseProcedure.Name
                If CType(INDgcNurseProcedure.DataSource, List(Of ViewServiceProceduresNursingProcedure)).Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDlcgOxigenConsumer.Name
                If CType(INDgcOxygenConsumption.DataSource, List(Of ViewOxygenConsumption)).Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDLcgHemo.Name
                If CType(INDGcHemoDetail.DataSource, List(Of ViewServicesHemocomponent)).Where(Function(o) o.Seleccione = True AndAlso o.GENSERVICEORDER Is Nothing).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDLycgMixingStation.Name
                If ListPharmaDoseMSDetail.Result.Where(Function(x) x.Selected = True).Count = 0 Then
                    validationResult = False
                End If
        End Select

        If Not validationResult Then
            ShowMessage(EeventViewerImages.Advertencia) = $"Debe seleccionar al menos un item para poder generar la {IIf(_justificationFlag, "Justificación", "órden de servicio")}"
            Return False
        End If
        If Not validationRealizatedResult Then
            ShowMessage(EeventViewerImages.Advertencia) = "Debe seleccionar El medico y la fecha en que se realizo el servicio"
            Return False
        End If
        Return validationResult
    End Function

    Private Sub BeginReloadDatasource()
        Select Case INDtcgSources.SelectedTabPageName
            Case INDlcgLaboratories.Name
                LoadDatasourceLaboratories()
            Case INDlcgValoraciones.Name
                LoadDatasourceReviews()
            Case INDlcgProceduresQX.Name
                datasourceServicesProceduresQx = Nothing
                datasourceServicesProceduresReportQx = Nothing
                LoadDatasourceProceduresQx(True)
            Case INDlcgImagesDX.Name
                LoadDatasourceImagesDx()
            Case INDlcgPathology.Name
                LoadDatasourcePathologies()
            Case INDlcgProceduresNoQX.Name
                LoadDatasourceProceduresNoQx()
            Case INDlcgConsultation.Name
                LoadDatasourceConsultation()
            Case INDlcgTerapy.Name
                LoadDatasourceTherapy()
            Case INDlcgNurseProcedure.Name
                LoadDatasourceNursingProcedures()
            Case INDlcgOxigenConsumer.Name
                LoadDatasourceOxygenConsumption()
            Case INDLcgHemo.Name
                datasourceServicesHemo = Nothing
                LoadDatasourceHemocomponent()
            Case INDLycgMixingStation.Name
                LoadDatasourceMedicinesSupliersMixingStation()
            Case INDLcgStays.Name
                LoadDatasourceStays()
        End Select
    End Sub

#End Region

#Region "BarButtonEvents"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub
#End Region

#Region "SetDatasources"

#Region "MedicineSuppliers"
    Private KardexMedicineSupplierXpo As Task(Of List(Of ViewKardexMedicineSupplier))
    Private listManualMovements As Task(Of List(Of ViewManualMovementsXpo))
    Public Delegate Function LoadMedicineSupplierDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewMedicinesSupplies)
    Private Sub LoadDatasourceMedicinesSupliers()
        INDgvMedicineSupplier.ShowLoadingPanel()

        listManualMovements = Task.Factory.StartNew(Function()
                                                        Using model As New MAccountControl(Me.Tag)
                                                            Dim query = model.listManualMovementsAsync(PatientCode, AdmissionCode).ToList()
                                                            If query?.Any() Then
                                                                Return query.ToList()
                                                            Else
                                                                Return Nothing
                                                            End If
                                                        End Using
                                                    End Function)

        KardexMedicineSupplierXpo = Task.Factory.StartNew(Function() As List(Of ViewKardexMedicineSupplier)
                                                              Using model As New MAccountControl(Me.Tag)
                                                                  Dim res = model.ListKardexMedicineSupplierByPatientCodeIngreso("", AdmissionCode).ToList()
                                                                  If res IsNot Nothing AndAlso res.Count > 0 Then
                                                                      Return res.ToList()
                                                                  Else
                                                                      Return Nothing
                                                                  End If
                                                              End Using
                                                          End Function)
        Task.Factory.StartNew(Sub()
                                  Using model As New MAccountControl(Me.Tag)
                                      Dim result = model.ListMedicineSupplierAggregatesByPatientCodeIngreso(PatientCode, AdmissionCode).ToList()
                                      If INDgcMedicineSupplier IsNot Nothing Then
                                          If INDgcMedicineSupplier.InvokeRequired Then
                                              INDgcMedicineSupplier.BeginInvoke(Sub()
                                                                                    INDgcMedicineSupplier.DataSource = result
                                                                                    INDgvMedicineSupplier.HideLoadingPanel()
                                                                                End Sub)
                                          Else
                                              INDgcMedicineSupplier.DataSource = result
                                              INDgvMedicineSupplier.HideLoadingPanel()
                                          End If
                                      End If
                                      _datasourceMedicines = True
                                  End Using

                              End Sub)
    End Sub

    Private Sub INDgvMedicineSupplier_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs) Handles INDgvMedicineSupplier.MasterRowGetRelationCount, INDGvMixingStation.MasterRowGetRelationCount, INDgvProceduresQx.MasterRowGetRelationCount
        Dim gridView As GridView = TryCast(sender, GridView)
        If gridView.Name = NameOf(INDgvMedicineSupplier) Then
            e.RelationCount = 2
        Else
            e.RelationCount = 1
        End If
    End Sub

    Private Sub INDgvMedicineSupplier_MasterRowEmpty(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs) Handles INDgvMedicineSupplier.MasterRowEmpty, INDGvMixingStation.MasterRowEmpty, INDgvProceduresQx.MasterRowEmpty
        e.IsEmpty = False
    End Sub

    Private Sub gridView1_MasterRowGetRelationName(ByVal sender As Object, ByVal e As MasterRowGetRelationNameEventArgs) Handles INDgvMedicineSupplier.MasterRowGetRelationName, INDGvMixingStation.MasterRowGetRelationName
        Select Case e.RelationIndex
            Case 0
                e.RelationName = "KardexMedicineSupplier"
            Case 1
                e.RelationName = "ManualMovements"
        End Select
    End Sub

    Private Sub gridView_MasterRowGetRelationName(ByVal sender As Object, ByVal e As MasterRowGetRelationNameEventArgs) Handles INDGvMixingStation.MasterRowGetRelationName
        e.RelationName = "PharmaDoseMSDetail"
    End Sub

    Private Async Sub gridView1_MasterRowGetChildList(ByVal sender As Object, ByVal e As MasterRowGetChildListEventArgs) Handles INDgvMedicineSupplier.MasterRowGetChildList
        Dim obj As ViewMedicinesSupplies = INDgvMedicineSupplier.GetRow(e.RowHandle)
        Select Case e.RelationIndex
            Case 0
                INDgvMedicineSupplier.ShowLoadingPanel()
                Dim data = (Await KardexMedicineSupplierXpo)
                If e.ChildList Is Nothing Then
                    e.ChildList = data.FindAll(Function(m) m.Codigo = obj.Codigo)
                    INDgvMedicineSupplier.HideLoadingPanel()
                End If
            Case 1
                INDGvManualMovements.ShowLoadingPanel()
                Dim data = (Await listManualMovements)
                If e.ChildList Is Nothing AndAlso data?.Any() Then
                    e.ChildList = data?.FindAll(Function(m) m.CodeProduct.Trim() = obj.Codigo.Trim())
                    INDGvManualMovements.HideLoadingPanel()
                End If
        End Select
    End Sub

#End Region

#Region "MixingStation"
    ''' <summary>s
    ''' Carga el Datasource para la pestaña central de Mezclas
    ''' </summary>
    Private Async Sub LoadDatasourceMedicinesSupliersMixingStation()
        INDGvMixingStation.ShowLoadingPanel()
        Using model As New MAccountControl(Me.Tag)
            ListPharmaDoseMSDetail = model.GetProcessedMedicationItemsForBillingListAsync(AdmissionCode, True)

            Dim result = Await model.GetProcessedMedicationItemsForBillingListAsync(AdmissionCode)

            Me.SafeInvoke(Sub()
                              INDGcMixingStation.DataSource = result
                              INDGvMixingStation.HideLoadingPanel()
                              INDliGenerateServiceOrder.Enabled = True
                          End Sub)

            _datasourceMedicinesMixingStation = True
        End Using
    End Sub

    ''' <summary>
    ''' Funcion para cargar las sub tablas de los items
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub gridView_MasterRowGetChildListMixingStation(ByVal sender As Object, ByVal e As MasterRowGetChildListEventArgs) Handles INDGvMixingStation.MasterRowGetChildList
        INDGvMixingStation.ShowLoadingPanel()
        Dim data = (Await ListPharmaDoseMSDetail)
        If e.ChildList Is Nothing Then
            Dim obj As Domain.Entities.SP_GetProcessedMedicationItemsForBilling_Result = INDGvMixingStation.GetRow(e.RowHandle)
            e.ChildList = data.FindAll(Function(m) m.ProductId = obj.ProductId And m.Child = obj.Father)
            INDGvMixingStation.HideLoadingPanel()
        End If
    End Sub
#End Region

#Region "Laboratories"
    Public Delegate Function LoadLaboratoriesDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresLaboratories)
    Private Sub LoadDatasourceLaboratories()
        INDgvLaboratories.ShowLoadingPanel()

        Task.Factory.StartNew(Sub()
                                  Using model As New MAccountControl(Me.Tag)
                                      Dim result = model.ListServicesProceduresLaboratoriesByadmissionCodeIngreso(PatientCode, AdmissionCode).ToList()
                                      INDgcLaboratories.BeginInvoke(Sub()
                                                                        INDgcLaboratories.DataSource = result
                                                                        INDgvLaboratories.HideLoadingPanel()
                                                                    End Sub)
                                  End Using
                              End Sub)
    End Sub

#End Region

#Region "Pathologies"
    Public Delegate Function LoadPathologiesDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresPathologies)
    Private Sub LoadDatasourcePathologies()
        INDgvPathologies.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim _functionLoadInfoDelegate As New LoadPathologiesDelegate(AddressOf loadPathologies)
                                  _functionLoadInfoDelegate.BeginInvoke(PatientCode, AdmissionCode, Sub(x)
                                                                                                        Dim result = _functionLoadInfoDelegate.EndInvoke(x).ToList()
                                                                                                        INDgcPathology.BeginInvoke(Sub()
                                                                                                                                       INDgcPathology.DataSource = result
                                                                                                                                       INDgvPathologies.HideLoadingPanel()
                                                                                                                                   End Sub)
                                                                                                    End Sub, Nothing)
                              End Sub)
    End Sub

    Private Function loadPathologies(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresPathologies)
        Using model As New MAccountControl(Me.Tag)
            Return model.ListServicesProceduresPathologiesByadmissionCodeIngreso(patientCode, admissionCode)
        End Using
    End Function
#End Region

#Region "ImagesDx"
    Public Delegate Function LoadImagesDxDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresImagesDx)
    Private Sub LoadDatasourceImagesDx()
        INDgvImagesDX.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim _functionLoadInfoDelegate As New LoadImagesDxDelegate(AddressOf loadImagesDX)
                                  _functionLoadInfoDelegate.BeginInvoke(PatientCode, AdmissionCode, Sub(x)
                                                                                                        Dim result = _functionLoadInfoDelegate.EndInvoke(x).ToList()
                                                                                                        INDgcImagesDx.BeginInvoke(Sub()
                                                                                                                                      INDgcImagesDx.DataSource = result
                                                                                                                                      INDgvImagesDX.HideLoadingPanel()
                                                                                                                                  End Sub)
                                                                                                    End Sub, Nothing)
                              End Sub)
    End Sub

    Private Function loadImagesDX(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresImagesDx)
        Using model As New MAccountControl(Me.Tag)
            Return model.ListServicesProceduresImagesDxByadmissionCodeIngreso(patientCode, admissionCode)
        End Using
    End Function
#End Region

#Region "ProceduresQx"
    Public Delegate Function LoadProceduresQxDelegate(patientCode As String, admissionCode As String, Reload As Boolean) As XPCollection(Of ViewProceduresQx)
    Private Sub LoadDatasourceProceduresQx(Optional Reload As Boolean = False)
        INDgvProceduresQx.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim _functionLoadInfoDelegate As New LoadProceduresQxDelegate(AddressOf loadProceduresQx)
                                  _functionLoadInfoDelegate.BeginInvoke(PatientCode, AdmissionCode, Reload, Sub(x)
                                                                                                                Dim result = _functionLoadInfoDelegate.EndInvoke(x).ToList()
                                                                                                                INDgcProceduresQx.BeginInvoke(Sub()
                                                                                                                                                  INDgcProceduresQx.DataSource = result
                                                                                                                                                  INDgvProceduresQx.HideLoadingPanel()
                                                                                                                                              End Sub)
                                                                                                            End Sub, Nothing)
                                  Using model As New MAccountControl(Me.Tag)
                                      datasourceProceduresQxRealizados = model.ListQxRealizadosByadmissionCodeIngreso(PatientCode, AdmissionCode).ToList()
                                  End Using
                              End Sub)
    End Sub

    Private Function loadProceduresQx(patientCode As String, admissionCode As String, Optional Reload As Boolean = False) As XPCollection(Of ViewProceduresQx)
        Using model As New MAccountControl(Me.Tag)
            ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.ProcedimientosQx, True, Reload)
            Return model.ListProceduresQxByPatientCodeIngreso(patientCode, admissionCode)
        End Using
    End Function
#End Region

#Region "ProceduresNoQx"
    Public Delegate Function LoadProceduresNoQxDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresNoQx)
    Private Sub LoadDatasourceProceduresNoQx()
        INDgvProceduresNoQx.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim _functionLoadInfoDelegate As New LoadProceduresNoQxDelegate(AddressOf loadProceduresNoQx)
                                  _functionLoadInfoDelegate.BeginInvoke(PatientCode, AdmissionCode, Sub(x)
                                                                                                        Dim result = _functionLoadInfoDelegate.EndInvoke(x).ToList()
                                                                                                        INDgcProceduresNoQX.BeginInvoke(Sub()
                                                                                                                                            INDgcProceduresNoQX.DataSource = result
                                                                                                                                            INDgvProceduresNoQx.HideLoadingPanel()
                                                                                                                                        End Sub)
                                                                                                    End Sub, Nothing)
                              End Sub)
    End Sub

    Private Function loadProceduresNoQx(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresNoQx)
        Using model As New MAccountControl(Me.Tag)
            Return model.ListServicesProceduresNoQxByadmissionCodeIngreso(patientCode, admissionCode)
        End Using
    End Function
#End Region

#Region "Consultation"
    Public Delegate Function LoadConsultationDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresConsultation)
    Private Sub LoadDatasourceConsultation()
        INDgvConsultation.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim _functionLoadInfoDelegate As New LoadConsultationDelegate(AddressOf loadConsultation)
                                  _functionLoadInfoDelegate.BeginInvoke(PatientCode, AdmissionCode, Sub(x)
                                                                                                        Dim result = _functionLoadInfoDelegate.EndInvoke(x).ToList()
                                                                                                        INDgcConsultation.BeginInvoke(Sub()
                                                                                                                                          INDgcConsultation.DataSource = result
                                                                                                                                          INDgvConsultation.HideLoadingPanel()
                                                                                                                                      End Sub)
                                                                                                    End Sub, Nothing)
                              End Sub)
    End Sub

    Private Function loadConsultation(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresConsultation)
        Using model As New MAccountControl(Me.Tag)
            Return model.ListServicesProceduresConsultationByadmissionCodeIngreso(patientCode, admissionCode)
        End Using
    End Function
#End Region

#Region "Therapy"
    Public Delegate Function LoadTherapyDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresTherapy)
    Private Sub LoadDatasourceTherapy()
        INDgvTherapy.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim _functionLoadInfoDelegate As New LoadTherapyDelegate(AddressOf loadTherapy)
                                  _functionLoadInfoDelegate.BeginInvoke(PatientCode, AdmissionCode, Sub(x)
                                                                                                        Dim result = _functionLoadInfoDelegate.EndInvoke(x).ToList()
                                                                                                        INDgcTerapy.BeginInvoke(Sub()
                                                                                                                                    INDgcTerapy.DataSource = result
                                                                                                                                    INDgvTherapy.HideLoadingPanel()
                                                                                                                                End Sub)
                                                                                                    End Sub, Nothing)
                              End Sub)
    End Sub

    Private Function loadTherapy(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesProceduresTherapy)
        Using model As New MAccountControl(Me.Tag)
            Return model.ListServicesProceduresTherapyByadmissionCodeIngreso(patientCode, admissionCode)
        End Using
    End Function
#End Region

#Region "OxygenConsumption"
    Public Delegate Function LoadOxygenConsumptionDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewOxygenConsumption)
    Private Sub LoadDatasourceOxygenConsumption()
        INDgvOxygen.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim _functionLoadInfoDelegate As New LoadOxygenConsumptionDelegate(AddressOf loadOxygenConsumption)
                                  _functionLoadInfoDelegate.BeginInvoke(PatientCode, AdmissionCode, Sub(x)
                                                                                                        Dim result = _functionLoadInfoDelegate.EndInvoke(x).ToList()
                                                                                                        INDgcOxygenConsumption.BeginInvoke(Sub()
                                                                                                                                               INDgcOxygenConsumption.DataSource = result
                                                                                                                                               INDgvOxygen.HideLoadingPanel()
                                                                                                                                           End Sub)
                                                                                                    End Sub, Nothing)
                              End Sub)
    End Sub

    Private Function loadOxygenConsumption(patientCode As String, admissionCode As String) As XPCollection(Of ViewOxygenConsumption)
        Using model As New MAccountControl(Me.Tag)
            ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.OxygenConsumption, True)
            Return model.ListOxygenConsumptionByadmissionCodeIngreso(patientCode, admissionCode)
        End Using
    End Function
#End Region

#Region "NursingProcedures"
    Public Delegate Function LoadNursingProceduresDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewServiceProceduresNursingProcedure)
    Private Sub LoadDatasourceNursingProcedures()
        INDgvNursingProcedures.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim _functionLoadInfoDelegate As New LoadNursingProceduresDelegate(AddressOf loadNursingProcedures)
                                  _functionLoadInfoDelegate.BeginInvoke(PatientCode, AdmissionCode, Sub(x)
                                                                                                        Dim result = _functionLoadInfoDelegate.EndInvoke(x).ToList()
                                                                                                        INDgcNurseProcedure.BeginInvoke(Sub()
                                                                                                                                            INDgcNurseProcedure.DataSource = result
                                                                                                                                            INDgvNursingProcedures.HideLoadingPanel()
                                                                                                                                        End Sub)
                                                                                                    End Sub, Nothing)
                              End Sub)
    End Sub

    Private Function loadNursingProcedures(patientCode As String, admissionCode As String) As XPCollection(Of ViewServiceProceduresNursingProcedure)
        Using model As New MAccountControl(Me.Tag)
            Return model.ListServicesNursingProceduresByadmissionCodeIngresoAtentionCenter(patientCode, admissionCode, AdmissionAtentionCenterCode)
        End Using
    End Function
#End Region

#Region "Reviews"
    Public Delegate Function LoadReviewsDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewReviews)
    Private Sub LoadDatasourceReviews()
        INDgvValorations.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Using model As New MAccountControl(Me.Tag)
                                      Dim result = model.ListReviewsByadmissionCodeIngreso(PatientCode, AdmissionCode).ToList()
                                      INDgcLaboratories.BeginInvoke(Sub()
                                                                        INDgcValoraciones.DataSource = result
                                                                        INDgvValorations.HideLoadingPanel()
                                                                    End Sub)
                                  End Using
                              End Sub)
    End Sub

#End Region

#Region "Stays"
    Private Async Sub LoadDatasourceStays()
        INDGvStays.ShowLoadingPanel()
        Dim model As New MAccountControl(Me.Tag)
        Dim res = Await model.ListStaysByadmissionIngresoAsync(AdmissionCode)
        model.Dispose()

        INDGvStays.HideLoadingPanel()
        If res.StateResult Then
            INDGcStays.DataSource = res.ObjectEmbbeded
        Else
            ShowMessage(EeventViewerImages.Advertencia) = res.Message
        End If
    End Sub
#End Region

#Region "Hemocomponent"
    '''' <summary>
    '''' 
    '''' </summary>
    '''' <param name="patientCode"></param>
    '''' <param name="admissionCode"></param>
    '''' <returns></returns>
    ''Public Delegate Function LoadHemocomponentDelegate(patientCode As String, admissionCode As String) As XPCollection(Of ViewServicesHemocomponent)
    Private Sub LoadDatasourceHemocomponent()
        INDGvHemoDetail.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Using model As New MAccountControl(Me.Tag)
                                      ListarDetalleServiciosyProcedimientos(eServicioHistoriasClinicasOrdenesMedicas.Hemocomponent, True)
                                      Dim result = model.ListServicesHemocomponentByadmissionIngreso(AdmissionCode).ToList()
                                      INDGcHemoDetail.BeginInvoke(Sub()
                                                                      datasourceServicesHemo = result
                                                                      INDGcHemoDetail.DataSource = result
                                                                      INDGvHemoDetail.HideLoadingPanel()
                                                                  End Sub)
                                  End Using
                              End Sub)
    End Sub

    Private Sub INDrptPceHemo_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDrptPceHemo.QueryPopUp
        INDrptPceHemo.PopupControl = INDPccHemo

        If INDGvHemoDetail.GetFocusedRowCellValue("TIPOSERVICIO") <> "Rastreo Anticuerpos" Then
            Dim IdServicio = INDGvHemoDetail.GetFocusedRowCellValue("BOLSAID")
            Using model As New MAccountControl(Me.Tag)
                Dim rowComponente As HCORHEMBOLXpo = model.GetHCORHEMBOLByID(IdServicio)
                If rowComponente IsNot Nothing Then
                    Dim profesional = model.GetHCORHEMBOLByCode(rowComponente.BACRESENTR)
                    If profesional IsNot Nothing Then INDteBacEntrega.Text = profesional.NOMMEDICO
                    profesional = model.GetHCORHEMBOLByCode(rowComponente.PROFSOLRES)
                    If profesional IsNot Nothing Then INDteMedReaSolicitudReserva.Text = profesional.NOMMEDICO
                    profesional = model.GetHCORHEMBOLByCode(rowComponente.PROFSOLTRA)
                    If profesional IsNot Nothing Then INDteMedReaSolicitudTransfusion.Text = profesional.NOMMEDICO
                    profesional = model.GetHCORHEMBOLByCode(rowComponente.PROFAPLICA)
                    If profesional IsNot Nothing Then INDteMedRealizoRegTransfusión.Text = profesional.NOMMEDICO
                    profesional = model.GetHCORHEMBOLByCode(rowComponente.ENFAPLICA)
                    If profesional IsNot Nothing Then INDteEnfRealizoRegistroAplicacion.Text = profesional.NOMMEDICO
                    INDteComponente.Text = INDGvHemoDetail.GetFocusedRowCellValue("COMPONENTE")
                    INDteFechaEntrega.Text = ObtenerValorDBNULL(rowComponente.FECENTREGA)
                    INDteNumeroUnidad.Text = ObtenerValorDBNULL(rowComponente.NUMBOLSA)
                    INDteSelloCalidad.Text = ObtenerValorDBNULL(rowComponente.SELLOCALIDAD)
                    INDteFechaExpiracion.Text = ObtenerValorDBNULL(rowComponente.FECHAEXPIRA)
                    INDrgPruebaCru.EditValue = ObtenerValorDBNULL(rowComponente.REAPRUECRU)
                    Dim RealizoRastreoAnticuerpos = INDGvHemoDetail.GetFocusedRowCellValue("REARASANT")
                    INDrgRastreoAnti.EditValue = If(RealizoRastreoAnticuerpos Is DBNull.Value, "", RealizoRastreoAnticuerpos)
                    INDteFechaAplicacionMed.Text = ObtenerValorDBNULL(rowComponente.FECAPLICMED)
                    INDteFechaAplicacionEnf.Text = ObtenerValorDBNULL(rowComponente.FECAPLICENF)
                    INDTeDateIniTrans.Text = ObtenerValorDBNULL(rowComponente.FECHINITRA)
                    INDTeDateEndTrans.Text = ObtenerValorDBNULL(rowComponente.FECHFINTRA)
                    INDrptPceHemo.PopupControl.Show()
                End If

            End Using
        Else
            e.Cancel = True
        End If
    End Sub

    Public Shared Function ObtenerValorDBNULL(value As Object) As Object
        If value IsNot DBNull.Value Then
            Return value
        Else
            Return Nothing
        End If
    End Function
#End Region

#End Region

#Region "Enum"
    Public Enum eServicioHistoriasClinicasOrdenesMedicas
        Laboratorios
        Patologias
        Imagenes_Dx
        ProcedimientosQx
        ProcedimientosNoQx
        Interconsultas
        Terapias
        OxygenConsumption
        ProcedimientosEnfermeria
        Valoration
        Hemocomponent
    End Enum
#End Region

#Region "MasterRowGet"
    ''' <summary>
    ''' Eventos para asignar la relacion y el dataSource a la sub-rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvProceduresQx_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles INDgvProceduresQx.MasterRowGetChildList
        If e.ChildList Is Nothing Then
            Dim ProceduresQxTmp = INDgvProceduresQx.GetFocusedObject(Of ViewProceduresQx)
            If datasourceProceduresQxRealizados IsNot Nothing Then
                e.ChildList = datasourceProceduresQxRealizados.ToList().Where(Function(d) d.NUMEFOLIO = ProceduresQxTmp.NUMEFOLIO).ToList()
            End If
        End If
    End Sub

    Private Sub INDviewMaster_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles INDgvProceduresQx.MasterRowGetRelationName
        e.RelationName = "Detail"
    End Sub

#End Region

    Private Sub INDRICESelected_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRICESelected.EditValueChanging
        If e.NewValue IsNot Nothing AndAlso CByte(e.NewValue) = 1 Then
            Dim headerRow = CType(INDgvProceduresQx.GetFocusedRow(), ViewProceduresQx)
            If headerRow.Tipo = "Procedimientos Solicitados" Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub MBtnOpenAdmission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnOpenAdmission.ItemClick
        _stateOpen = True
        INDSleAdmissionNumber2.Datasource = Nothing
        Using m As New MLiquidation()
            Me.INDSleAdmissionNumber2.Datasource = m.ListAdmissionsToLiquidation()
        End Using
    End Sub

    Private Sub FrmAccountControl_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not Me.IsMdiChild AndAlso e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub MBtnCloseAdmission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnCloseAdmission.ItemClick
        _stateOpen = False
        INDSleAdmissionNumber2.Datasource = Nothing
        Using m As New MLiquidation()
            Me.INDSleAdmissionNumber2.Datasource = m.ListAdmissionsToLiquidationConfirm()
        End Using
    End Sub

    Private Sub INDRptSearchMedicoRealizo_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRptSearchMedicoRealizo.QueryPopUp
    End Sub

    Private Sub CargarMedicoRealizoDataSource()
        Using model As New MServiceOrder(Me.Tag)
            INDRptSearchMedicoRealizo.DataSource = model.ListHealthCareProfessional()
        End Using
    End Sub

    Private Sub INDGvStays_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvStays.CustomUnboundColumnData
        Dim row As StayInfoModel = e.Row

        If row IsNot Nothing AndAlso e.IsGetData AndAlso e.Column.Name = INDColFunctionalUnitStay.Name Then
            e.Value = $"{row.FunctionalUnitCode} - {row.FunctionalUnitName}"
        End If
    End Sub
End Class
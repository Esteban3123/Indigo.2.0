'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/02/2019
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
Imports DevExpress.XtraSplashScreen
Imports Presentation.Billing

#End Region

Public Class FrmAccountControlAmbulatoryDetail

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    Dim Presenter As PAccountControlAmbulatory

    ''' <summary>
    ''' Prefijo de los recursos de PlaceEntry
    ''' </summary>
    Private Const PREFIX_PLACEENTRY As String = "PlaceEntry_"

    ''' <summary>
    ''' Permite saber si se va a realizar el proceso con el ingreso actual o no
    ''' </summary>
    Dim IsCurrentAdmission As Boolean

    ''' <summary>
    ''' Representa al ingreso que se selecciono
    ''' </summary>
    Dim admissionRecord As ViewLiquidationGetAdmissionAll

    ''' <summary>
    ''' Splash de espera
    ''' </summary>
    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True)

    ''' <summary>
    ''' Variable que me permite establecer si se debe controlar el cambio de pestaña
    ''' </summary>
    Dim ControlChangedTabPage As Boolean = False

    ''' <summary>
    ''' Permite establecer en que rejilla se esta trabajando para enviar a los servicios,
    ''' 0 = Rejilla de procedimientos odontologicos
    ''' 1 = Rejilla de laboratorios
    ''' </summary>
    Dim TypeGrid As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios
    ''' </summary>
    Dim _requestQuoteServices As Boolean
    Public WriteOnly Property RequestQuoteServices As Boolean
        Set(value As Boolean)
            _requestQuoteServices = value
        End Set
    End Property

    Private _admissionCode As String
    ''' <summary>
    ''' Obtiene o establece el numero del ingreso
    ''' </summary>
    ''' <returns></returns>
    Public Property AdmissionCode As String
        Get
            Return _admissionCode
        End Get
        Set(value As String)
            _admissionCode = value
        End Set
    End Property

    Private _patientCode As String
    ''' <summary>
    ''' Obtiene o establece la identificación del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientCode As String
        Get
            Return _patientCode
        End Get
        Set(value As String)
            _patientCode = value
        End Set
    End Property

    Private _careCenterCode As String
    ''' <summary>
    ''' Obtiene o establece el código del centro de atención
    ''' </summary>
    ''' <returns></returns>
    Public Property CareCenterCode As String
        Get
            Return _careCenterCode
        End Get
        Set(value As String)
            _careCenterCode = value
        End Set
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para establecer los datos del ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetAdmission()
        AsyncLoader(True)
        Task.Factory.StartNew(Sub()
                                  admissionRecord = Presenter.GetAmissionByAdmissionNumber(_admissionCode)
                                  If admissionRecord IsNot Nothing Then
                                      INDSleAdmissionNumber2.BeginInvoke(Sub()
                                                                             Me.INDSleAdmissionNumber2.DisplayNullText = String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"),
                                                                                                                        admissionRecord.AdmissionCode.ToString().Trim(),
                                                                                                                        admissionRecord.PatientCode.ToString().Trim(),
                                                                                                                        admissionRecord.PatientName.ToString().Trim())

                                                                             INDlblStatus.Text = admissionRecord.StatusName
                                                                             INDlblStatus.BackColor = GetStatusByCode(admissionRecord.Status.ToString())
                                                                             INDtxtDate.Text = admissionRecord.AdmissionDate.ToString()
                                                                             INDicceType.EditValue = admissionRecord.AdmissionType
                                                                             INDtxtFor.Text = ResourceManager.GetString(PREFIX_PLACEENTRY & admissionRecord.PlaceEntry.ToString(), "IndigoCrystalHis")

                                                                             Dim documentType As String = GetDocumentTypeName(admissionRecord.PatientDocumentType.ToString().Trim())
                                                                             Me.TxtPatientCode.Text = If(admissionRecord.PatientCode Is Nothing, String.Empty, String.Concat(documentType, " - ", admissionRecord.PatientCode.ToString().Trim()))
                                                                             Me.TxtPatientName.Text = If(admissionRecord.PatientName Is Nothing, String.Empty, admissionRecord.PatientName.ToString().Trim())
                                                                             Me.TxtPatientBirth.Text = Convert.ToDateTime(admissionRecord.PatientBirth).ToString(SessionValues.Instance.Culture)
                                                                             Me.TxtPatientAge.Text = Utils.AgeToString(Convert.ToDateTime(admissionRecord.PatientBirth))
                                                                             Me.TxtPatientType.EditValue = Convert.ToInt32(admissionRecord.PatientType)
                                                                             Me.TxtAfiliationType.EditValue = Convert.ToInt32(admissionRecord.PatientAfiliation)
                                                                             Me.TxtPatientEstrato.Text = (admissionRecord.NivelCode & " - " & admissionRecord.NivelName)

                                                                             Me.TxtAdmissionCode.Text = admissionRecord.AdmissionCode.ToString().Trim()
                                                                             Me.TxtAdmissionDate.Text = Convert.ToDateTime(admissionRecord.AdmissionDate).ToString(SessionValues.Instance.Culture)
                                                                             Me.TxtEntityNameAdmission.Text = admissionRecord.EntityName
                                                                             Me.TxtPlaceEntry.Text = ResourceManager.GetString(PREFIX_PLACEENTRY & admissionRecord.PlaceEntry.ToString(), "IndigoCrystalHis")
                                                                             Me.TxtAdmissionType.EditValue = Convert.ToInt32(admissionRecord.AdmissionType)
                                                                             Me.TxtBedStay.Text = If(admissionRecord.BedStay Is Nothing, String.Empty, admissionRecord.BedStay.ToString().Trim())
                                                                             Me.TxtLiquidationType.EditValue = Convert.ToInt32(admissionRecord.LiquidationType)
                                                                             Me.TxtAuthorization.Text = admissionRecord.AuthorizationNumber.ToString().Trim()
                                                                             Me.TxtAtentionCenter.Text = admissionRecord.AdmissionCentAtencCodeName
                                                                             Me.TxtFunctionalUnitAdmission.Text = admissionRecord.AdmissionUniFuncCodeName.ToString().Trim()
                                                                             Me.TxtResponsibleName.Text = If(admissionRecord.ResponsibleName Is Nothing, String.Empty, admissionRecord.ResponsibleName.ToString().Trim())
                                                                             Me.TxtResponsiblePhone.Text = admissionRecord.ResponsiblePhone

                                                                             If CInt(admissionRecord.CareGroupTypePatient) <> -1 Then
                                                                                 Me.TxtPatientEntityName.Text = String.Concat(admissionRecord.PatientEntityCode, " - ", admissionRecord.PatientEntity)
                                                                                 Me.TxtCareGroupPatient.Text = admissionRecord.CareGroupCodeName
                                                                             End If
                                                                             Me.TxtCareGroupAdmission.Text = admissionRecord.AdmissionCareGroupCodeName

                                                                             AsyncLoader(False)
                                                                             BeginReloadDatasource()
                                                                         End Sub)
                                  Else
                                      AsyncLoader(False)
                                  End If
                              End Sub)
    End Sub

    ''' <summary>
    ''' Obtiene el color del estado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Private Function GetStatusByCode(code As String) As Color
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
    ''' Obtiene el tipo de documento
    ''' </summary>
    ''' <param name="PatientDocumentType"></param>
    ''' <returns></returns>
    Private Function GetDocumentTypeName(PatientDocumentType As String) As String
        Select Case PatientDocumentType
            Case "1"
                Return "CC"
            Case "2"
                Return "CE"
            Case "3"
                Return "TI"
            Case "4"
                Return "RC"
            Case "5"
                Return "PA"
            Case "6"
                Return "AS"
            Case "7"
                Return "MS"
            Case "8"
                Return "NU"
            Case Else
                Return ""
        End Select
    End Function

    ''' <summary>
    ''' Obtiene el tipo
    ''' </summary>
    ''' <param name="AdmissionRiskType"></param>
    ''' <returns></returns>
    Private Function GetAdmissionRiskType(AdmissionRiskType As String) As String
        Select Case AdmissionRiskType
            Case "1"
                Return "Enfermedad General y Maternidad"
            Case "2"
                Return "Accidente de Tránsito"
            Case "3"
                Return "Catástrofe"
            Case "4"
                Return "Enfermedad General y Maternidad"
            Case "5"
                Return "Accidente de Trabajo"
            Case "6"
                Return "Enfermedad Profesional"
            Case "7"
                Return "Atención Inicial de Urgencias"
            Case "8"
                Return "Otro Tipo de Accidente"
            Case "9"
                Return "Lesión Por Agresión"
            Case "10"
                Return "Lesión AutoInfligida"
            Case "11"
                Return "Maltrato Físico"
            Case "12"
                Return "Promoción y Prevención"
            Case "13"
                Return "Otro"
            Case "14"
                Return "Accidente Rabico"
            Case "15"
                Return "Accidente Ofídico"
            Case "16"
                Return "Sopecha de Abuso Sexual"
            Case "17"
                Return "Sopecha de Violencia Sexual"
            Case "18"
                Return "Sopecha de Maltrato Emocional"
            Case Else
                Return ""
        End Select
    End Function

    ''' <summary>
    ''' Carga el datasource de los procedimientos odontológicos
    ''' </summary>
    Private Sub LoadDatasourceOdontologyProcedures()
        If INDgcOdontologyProcedures.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewOdontologyProcedure.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListOdontologyProcedures(_admissionCode, _patientCode, _careCenterCode)
                                  INDgcOdontologyProcedures.BeginInvoke(Sub()
                                                                            INDgcOdontologyProcedures.DataSource = result
                                                                            INDviewOdontologyProcedure.HideLoadingPanel()
                                                                        End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga el datasource de los laboratorios
    ''' </summary>
    Private Sub LoadDatasourceLaboratories()
        If INDgcLaboratories.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewLaboratories.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListLaboratories(_admissionCode, _patientCode, _careCenterCode)
                                  INDgcLaboratories.BeginInvoke(Sub()
                                                                    INDgcLaboratories.DataSource = result
                                                                    INDviewLaboratories.HideLoadingPanel()
                                                                End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga el datasource de las patologias
    ''' </summary>
    Private Sub LoadDatasourcePathologies()
        If INDgcPathologies.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewPathologies.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListPathologies(_admissionCode, _patientCode, _careCenterCode)
                                  INDgcPathologies.BeginInvoke(Sub()
                                                                   INDgcPathologies.DataSource = result
                                                                   INDviewPathologies.HideLoadingPanel()
                                                               End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga el datasource de las imagenes
    ''' </summary>
    Private Sub LoadDatasourceImages()
        If INDgcImages.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewImages.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListImages(_admissionCode, _patientCode, _careCenterCode)
                                  INDgcImages.BeginInvoke(Sub()
                                                              INDgcImages.DataSource = result
                                                              INDviewImages.HideLoadingPanel()
                                                          End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga el datasource de las interconsultas
    ''' </summary>
    Private Sub LoadDatasourceInterconsults()
        If INDgcInterconsults.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewInterconsults.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListInterconsults(_admissionCode, _patientCode, _careCenterCode)
                                  INDgcInterconsults.BeginInvoke(Sub()
                                                                     INDgcInterconsults.DataSource = result
                                                                     INDviewInterconsults.HideLoadingPanel()
                                                                 End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga el datasource de las notas administrativas
    ''' </summary>
    Private Sub LoadDatasourceAdministrativeNotes()
        If INDgcAdministrativeNotes.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewAdministrativeNotes.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListAdministrativeNotes(_admissionCode, _patientCode, _careCenterCode)
                                  INDgcAdministrativeNotes.BeginInvoke(Sub()
                                                                           INDgcAdministrativeNotes.DataSource = result
                                                                           INDviewAdministrativeNotes.HideLoadingPanel()
                                                                       End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga el datasource de las citas relacionadas al ingreso
    ''' </summary>
    Private Sub LoadDatasourceAppointments()
        If INDgcAppointments.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewAppointment.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListAppointments(_admissionCode, _patientCode)
                                  INDgcAppointments.BeginInvoke(Sub()
                                                                    INDgcAppointments.DataSource = result
                                                                    INDviewAppointment.HideLoadingPanel()
                                                                End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Valida que haya algun item con check dentro de la rejilla
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateCheckItems() As Boolean
        'Variable para retornar
        Dim validationResult As Boolean = True

        'Se valida dependiendo del tab que este activo si tiene items checkiados
        Select Case INDtcgInfo.SelectedTabPageName
            Case INDlcgOdontologyProcedures.Name 'Procedimientos Odontológicos
                If CType(INDgcOdontologyProcedures.DataSource, List(Of ViewOdontologyProceduresXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDlcgLaboratories.Name 'Laboratorios
                If CType(INDgcLaboratories.DataSource, List(Of ViewLaboratoriesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDlcgPathologies.Name 'Patologias
                If CType(INDgcPathologies.DataSource, List(Of ViewPathologiesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDlcgImages.Name 'Imagenes
                If CType(INDgcImages.DataSource, List(Of ViewImagesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDlcgInterconsults.Name 'Interconsultas
                If CType(INDgcInterconsults.DataSource, List(Of ViewInterconsultsAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList().Count = 0 Then
                    validationResult = False
                End If
            Case INDlcgAdministrativeNotes.Name 'Notas Administrativas
                If CType(INDgcAdministrativeNotes.DataSource, List(Of ViewAdministrativeNotesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList().Count = 0 Then
                    validationResult = False
                End If
        End Select

        If Not validationResult Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item para poder generar la orden de servicio"
        End If

        Return validationResult
    End Function

    ''' <summary>
    ''' Método que genera la orden de servicio
    ''' </summary>
    ''' <param name="listHomologation"></param>
    Private Sub ProcessAccountControlAmbulatory(Optional listHomologation As List(Of List(Of CupsHomologation)) = Nothing)
        AsyncLoader(True)

        'Se valida que hayan seleccionado items de la rejilla del tab activo
        If Not ValidateCheckItems() Then
            AsyncLoader(False)
            Exit Sub
        End If

        'Si se selecciono que se realice con el ingreso actual se valida que el ingreso no este anulado
        If IsCurrentAdmission = True AndAlso admissionRecord.Status = "A" Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede generar la orden de servicio porque el ingreso está anulado"
            AsyncLoader(False)
            Exit Sub
        End If

        'Si se selecciono que se realice con el ingreso actual se valida que el ingreso tenga el grupo de atención asociado
        If IsCurrentAdmission = True AndAlso (admissionRecord.AdmissionCaregroupId Is Nothing OrElse CInt(admissionRecord.AdmissionCaregroupId <= 0)) Then
            Mensaje(EeventViewerImages.Advertencia) = "El ingreso no tiene asociado un grupo de atención"
            AsyncLoader(False)
            Exit Sub
        End If

        'Listado de detalles
        Dim ListDetail = New ConcurrentBag(Of Object)

        'Permite saber si hay servicios que aplican a rias
        'Dim CountDetailsApplyRias As Integer = 0

        'Dependiendo del tab activo se obtienen los registros checkiados y se realizan los objetos
        Select Case INDtcgInfo.SelectedTabPageName
            Case INDlcgOdontologyProcedures.Name 'Procedimientos Odontológicos

                'Se asigna desde donde se esta generando los detalles
                TypeGrid = 0

                'Se obtienen los registros de la rejilla que fueron checkiados y que no tengan generada la orden de servicio
                Dim itemDetail = CType(INDgcOdontologyProcedures.DataSource, List(Of ViewOdontologyProceduresXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList()

                'Se recorren los items checkiados para crear el objeto de detalles que se envia para generar la orden de servicio
                Parallel.ForEach(itemDetail, Sub(obj As ViewOdontologyProceduresXpo)
                                                 Dim detail As Object = New ExpandoObject()
                                                 detail.CupsEntityCode = obj.CupsCode
                                                 detail.CupsDescription = obj.CupsDescription
                                                 detail.FunctionalUnitCode = obj.FunctionalUnitCode
                                                 detail.ProfessionalCode = obj.ProfessionalCode
                                                 detail.Date = obj.RegisterDate
                                                 detail.ProfessionalSpecialistCode = obj.ProfessionalEspecialty
                                                 detail.AdmissionCode = obj.AdmissionNumber
                                                 detail.PatientCode = obj.PatientCode
                                                 detail.Quantity = obj.Quantity
                                                 detail.NitMedico = obj.ProfessionalNit
                                                 detail.Auto = obj.Id
                                                 detail.DatasourceType = 0
                                                 detail.FolioNumber = obj.FolioNumber
                                                 detail.ProcedureCode = obj.ProcedureCode
                                                 detail.ApplyRIAS = obj.ApplyRIAS
                                                 detail.RiasCupsId = obj.RiasCupsId
                                                 detail.RiasDescription = obj.RiasDescription
                                                 ListDetail.Add(detail)
                                             End Sub)

                'Se obtienen los registros de la rejilla que apliquen a rias
                'CountDetailsApplyRias = CType(INDgcOdontologyProcedures.DataSource, List(Of ViewOdontologyProceduresXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0 AndAlso x.ApplyRIAS = True).Count()

            Case INDlcgLaboratories.Name 'Laboratorios

                'Se asigna desde donde se esta generando los detalles
                TypeGrid = 1

                'Se obtienen los registros de la rejilla que fueron checkiados y que no tengan generada la orden de servicio
                Dim itemDetail = CType(INDgcLaboratories.DataSource, List(Of ViewLaboratoriesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList()

                'Se recorren los items checkiados para crear el objeto de detalles que se envia para generar la orden de servicio
                Parallel.ForEach(itemDetail, Sub(obj As ViewLaboratoriesAccountControlAmbulatoryXpo)
                                                 Dim detail As Object = New ExpandoObject()
                                                 detail.CupsEntityCode = obj.CupsCode
                                                 detail.CupsDescription = obj.CupsDescription
                                                 detail.FunctionalUnitCode = obj.FunctionalUnitCode
                                                 detail.ProfessionalCode = obj.ProfessionalCode
                                                 detail.Date = Date.Now()
                                                 detail.ProfessionalSpecialistCode = obj.ProfessionalEspecialty
                                                 detail.AdmissionCode = obj.AdmissionNumber
                                                 detail.PatientCode = obj.PatientCode
                                                 detail.Quantity = obj.Quantity
                                                 detail.NitMedico = obj.ProfessionalNit
                                                 detail.Auto = obj.Id
                                                 detail.DatasourceType = 1
                                                 detail.FolioNumber = obj.FolioNumber
                                                 detail.ApplyRIAS = obj.ApplyRIAS
                                                 detail.RiasCupsId = obj.RiasCupsId
                                                 detail.RiasDescription = obj.RiasDescription
                                                 detail.EntityId = obj.EntityId
                                                 detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                                 detail.ContractDescriptionId = obj.ContractDescriptionId
                                                 ListDetail.Add(detail)
                                             End Sub)

                'Se obtienen los registros de la rejilla que apliquen a rias
                'CountDetailsApplyRias = CType(INDgcLaboratories.DataSource, List(Of ViewLaboratoriesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0 AndAlso x.ApplyRIAS = True).Count()

            Case INDlcgPathologies.Name 'Patologias

                'Se asigna desde donde se esta generando los detalles
                TypeGrid = 2

                'Se obtienen los registros de la rejilla que fueron checkiados y que no tengan generada la orden de servicio
                Dim itemDetail = CType(INDgcPathologies.DataSource, List(Of ViewPathologiesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList()

                'Se recorren los items checkiados para crear el objeto de detalles que se envia para generar la orden de servicio
                Parallel.ForEach(itemDetail, Sub(obj As ViewPathologiesAccountControlAmbulatoryXpo)
                                                 Dim detail As Object = New ExpandoObject()
                                                 detail.CupsEntityCode = obj.CupsCode
                                                 detail.CupsDescription = obj.CupsDescription
                                                 detail.FunctionalUnitCode = obj.FunctionalUnitCode
                                                 detail.ProfessionalCode = obj.ProfessionalCode
                                                 detail.Date = obj.MedicalOrderDate
                                                 detail.ProfessionalSpecialistCode = obj.ProfessionalEspecialty
                                                 detail.AdmissionCode = obj.AdmissionNumber
                                                 detail.PatientCode = obj.PatientCode
                                                 detail.Quantity = obj.Quantity
                                                 detail.NitMedico = obj.ProfessionalNit
                                                 detail.Auto = obj.Id
                                                 detail.DatasourceType = 0
                                                 detail.FolioNumber = obj.FolioNumber
                                                 detail.ApplyRIAS = obj.ApplyRIAS
                                                 detail.RiasCupsId = obj.RiasCupsId
                                                 detail.RiasDescription = obj.RiasDescription
                                                 detail.EntityId = obj.EntityId
                                                 detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                                 detail.ContractDescriptionId = obj.ContractDescriptionId
                                                 ListDetail.Add(detail)
                                             End Sub)

                'Se obtienen los registros de la rejilla que apliquen a rias
                'CountDetailsApplyRias = CType(INDgcPathologies.DataSource, List(Of ViewPathologiesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0 AndAlso x.ApplyRIAS = True).Count()

            Case INDlcgImages.Name 'Imagenes

                'Se asigna desde donde se esta generando los detalles
                TypeGrid = 3

                'Se obtienen los registros de la rejilla que fueron checkiados y que no tengan generada la orden de servicio
                Dim itemDetail = CType(INDgcImages.DataSource, List(Of ViewImagesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList()

                'Se recorren los items checkiados para crear el objeto de detalles que se envia para generar la orden de servicio
                Parallel.ForEach(itemDetail, Sub(obj As ViewImagesAccountControlAmbulatoryXpo)
                                                 Dim detail As Object = New ExpandoObject()
                                                 detail.CupsEntityCode = obj.CupsCode
                                                 detail.CupsDescription = obj.CupsDescription
                                                 detail.FunctionalUnitCode = obj.FunctionalUnitCode
                                                 detail.ProfessionalCode = obj.ProfessionalCode
                                                 detail.Date = obj.MedicalOrderDate
                                                 detail.ProfessionalSpecialistCode = obj.ProfessionalEspecialty
                                                 detail.AdmissionCode = obj.AdmissionNumber
                                                 detail.PatientCode = obj.PatientCode
                                                 detail.Quantity = obj.Quantity
                                                 detail.NitMedico = obj.ProfessionalNit
                                                 detail.Auto = obj.Id
                                                 detail.DatasourceType = 2
                                                 detail.FolioNumber = obj.FolioNumber
                                                 detail.ApplyRIAS = obj.ApplyRIAS
                                                 detail.RiasCupsId = obj.RiasCupsId
                                                 detail.RiasDescription = obj.RiasDescription
                                                 detail.EntityId = obj.EntityId
                                                 detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                                 detail.ContractDescriptionId = obj.ContractDescriptionId
                                                 ListDetail.Add(detail)
                                             End Sub)

                'Se obtienen los registros de la rejilla que apliquen a rias
                'CountDetailsApplyRias = CType(INDgcImages.DataSource, List(Of ViewImagesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0 AndAlso x.ApplyRIAS = True).Count()

            Case INDlcgInterconsults.Name 'Interconsultas

                'Se asigna desde donde se esta generando los detalles
                TypeGrid = 4

                'Se obtienen los registros de la rejilla que fueron checkiados y que no tengan generada la orden de servicio
                Dim itemDetail = CType(INDgcInterconsults.DataSource, List(Of ViewInterconsultsAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList()

                'Se recorren los items checkiados para crear el objeto de detalles que se envia para generar la orden de servicio
                Parallel.ForEach(itemDetail, Sub(obj As ViewInterconsultsAccountControlAmbulatoryXpo)
                                                 Dim detail As Object = New ExpandoObject()
                                                 detail.CupsEntityCode = obj.CupsCode
                                                 detail.CupsDescription = obj.CupsDescription
                                                 detail.FunctionalUnitCode = obj.FunctionalUnitCode
                                                 detail.ProfessionalCode = obj.ProfessionalCode
                                                 detail.Date = obj.MedicalOrderDate
                                                 detail.ProfessionalSpecialistCode = obj.ProfessionalEspecialty
                                                 detail.AdmissionCode = obj.AdmissionNumber
                                                 detail.PatientCode = obj.PatientCode
                                                 detail.Quantity = obj.Quantity
                                                 detail.NitMedico = obj.ProfessionalNit
                                                 detail.Auto = obj.Id
                                                 detail.DatasourceType = 0
                                                 detail.FolioNumber = obj.FolioNumber
                                                 detail.ApplyRIAS = obj.ApplyRIAS
                                                 detail.RiasCupsId = obj.RiasCupsId
                                                 detail.RiasDescription = obj.RiasDescription
                                                 detail.EntityId = obj.EntityId
                                                 detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                                 detail.ContractDescriptionId = obj.ContractDescriptionId
                                                 ListDetail.Add(detail)
                                             End Sub)

                'Se obtienen los registros de la rejilla que apliquen a rias
                'CountDetailsApplyRias = CType(INDgcInterconsults.DataSource, List(Of ViewInterconsultsAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0 AndAlso x.ApplyRIAS = True).Count()

            Case INDlcgAdministrativeNotes.Name 'Notas Administrativas

                'Se asigna desde donde se esta generando los detalles
                TypeGrid = 5

                'Se obtienen los registros de la rejilla que fueron checkiados y que no tengan generada la orden de servicio
                Dim itemDetail = CType(INDgcAdministrativeNotes.DataSource, List(Of ViewAdministrativeNotesAccountControlAmbulatoryXpo)).Where(Function(x) x.Selection <> 0 AndAlso x.ServiceOrderId = 0).ToList()

                'Se recorren los items checkiados para crear el objeto de detalles que se envia para generar la orden de servicio
                Parallel.ForEach(itemDetail, Sub(obj As ViewAdministrativeNotesAccountControlAmbulatoryXpo)
                                                 Dim detail As Object = New ExpandoObject()
                                                 detail.CupsEntityCode = obj.CupsCode
                                                 detail.CupsDescription = obj.CupsDescription
                                                 detail.FunctionalUnitCode = obj.FunctionalUnitCode
                                                 detail.ProfessionalCode = obj.ProfessionalCode
                                                 detail.Date = obj.MedicalOrderDate
                                                 detail.ProfessionalSpecialistCode = obj.ProfessionalEspecialty
                                                 detail.AdmissionCode = obj.AdmissionNumber
                                                 detail.PatientCode = obj.PatientCode
                                                 detail.Quantity = obj.Quantity
                                                 detail.NitMedico = obj.ProfessionalNit
                                                 detail.Auto = obj.Id
                                                 detail.DatasourceType = 0
                                                 detail.FolioNumber = obj.FolioNumber
                                                 detail.ApplyRIAS = obj.ApplyRIAS
                                                 detail.RiasCupsId = obj.RiasCupsId
                                                 detail.RiasDescription = obj.RiasDescription
                                                 detail.EntityId = obj.EntityId
                                                 ListDetail.Add(detail)
                                             End Sub)

        End Select

        'Se valida si se realiza el proceso con el ingreso actual y el grupo de atención aplica a rias y hay items en la rejilla que aplican a rias o
        'el proceso se realiza con un ingreso nuevo siempre se abre el modal
        AsyncLoader(False)
        Using formulario As New FrmRiasAndNewAdmission
            AddHandler formulario.ReturnToProcess, AddressOf ReturnToProcess
            formulario.RequestQuoteServices = _requestQuoteServices
            formulario.IsCurrentAdmission = IsCurrentAdmission
            formulario.AdmissionRecord = admissionRecord
            formulario.ListDetail = ListDetail
            formulario.CareCenterCode = _careCenterCode
            formulario.PatientCode = _patientCode
            formulario.ToolBar.Visible = False
            formulario.StartPosition = FormStartPosition.CenterParent
            Dim size As System.Drawing.Size
            size.Width = 900
            size.Height = 730
            formulario.Size = size
            Dim transParent As New FrmTransparent(formulario, False)
            transParent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que me recibe la info del modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnToProcess(sender As Object, e As ReturnToProcessEventArgs)
        GenerateServiceOrder(e.Details, e.NewAdmission, Nothing, e.CareGroupId, e.AuthorizationNumber)
    End Sub

    ''' <summary>
    ''' Metodo que genera la orden de servicio
    ''' </summary>
    Private Async Sub GenerateServiceOrder(ListDetail As ConcurrentBag(Of Object), Optional NewAdmission As Object = Nothing, Optional listHomologation As List(Of List(Of CupsHomologation)) = Nothing, Optional CareGroupId As Integer = 0, Optional AuthorizationNumber As String = "")
        AsyncLoader(True)

        'Se instancia el objeto que se va a enviar al método de generación de orden de servicio
        Dim args As Object = New ExpandoObject()

        'Se asignan los valores necesarios para enviar
        args.CareGroupId = CareGroupId
        args.AdmissionNumber = _admissionCode
        args.CareCenterCode = _careCenterCode
        args.PatientCode = _patientCode
        args.IsCurrentAdmission = IsCurrentAdmission
        args.IsAccountControlAmbulatory = True
        args.OperativeUnitId = BarraBotones.OperatingUnitValue
        args.IsProcedureQx = 0
        args.NewAdmission = NewAdmission
        args.TypeGrid = TypeGrid

        'Si el ingreso tiene asociado una entidad administradora se consulta
        If admissionRecord.HealthAdministratorId IsNot Nothing AndAlso CInt(admissionRecord.HealthAdministratorId) > 0 Then
            args.HealthAdministratorId = admissionRecord.HealthAdministratorId
            args.ThirdPartyId = admissionRecord.HealthAdministratorThirdPartyId
        End If

        'Se asignan los detalles generados para enviar a los servicios
        args.Details = ListDetail

        Using model As New MAccountControl(Me.Tag)
            'Se ejecuta el proceso de generar la orden de servicio
            Dim result As ActionResult(Of List(Of List(Of CupsHomologation))) = Await model.GenerateServiceOrderMassive(args, listHomologation)
            If result.StatusCode <> eStatusResult.SUCCESS AndAlso result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                AsyncLoader(False)
                If result.MessageResult IsNot Nothing Then 'Hay multiples homologaciones
                    Dim formulario As New FrmHomologationsCups
                    AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                    formulario.IsQx = CBool(0)
                    formulario.listDetail = ListDetail
                    formulario.NewAdmission = NewAdmission
                    formulario.CareCenterCode = _careCenterCode
                    formulario.CareGroupId = CareGroupId
                    formulario.AuthorizationNumber = AuthorizationNumber
                    formulario.StartPosition = FormStartPosition.CenterParent
                    formulario.ListHomologation = result.ObjectEmbbeded
                    formulario.arguments = args
                    Dim transParent As New FrmTransparent(formulario, False)
                    transParent.ShowDialog(Me)
                Else
                    GenerateServiceOrder(ListDetail, NewAdmission, result.ObjectEmbbeded, CareGroupId, AuthorizationNumber)
                End If
            Else
                AsyncLoader(False)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Mensaje(EeventViewerImages.Informacion) = String.Format("Se generó una orden de servicio con código {0}", result.Message)
                    If result.MessageResult IsNot Nothing AndAlso Not result.MessageResult(0).Equals(String.Empty) Then
                        Mensaje(EeventViewerImages.Informacion) = result.MessageResult(0)
                    End If
                    'If result.MessageResult IsNot Nothing AndAlso Not result.MessageResult(1).Equals(String.Empty) Then
                    '    OpenFormLiquidation(result.MessageResult(1).ToString().Trim())
                    'End If
                    SetAdmission()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Retorno del modal de homologaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnSetHomologation(sender As Object, e As HomologationCupsEventArgs)
        If e IsNot Nothing Then
            GenerateServiceOrder(e.listDetail, e.NewAdmission, e.ListHomologations, e.CareGroupId, e.AuthorizationNumber)
        End If
    End Sub

    ''' <summary>
    ''' Método que abre el form de liqudiación
    ''' </summary>
    ''' <param name="numIngres"></param>
    Private Sub OpenFormLiquidation(numIngres As String)
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Using FrmLiquidation As New FrmLiquidation()
            FrmLiquidation.Size = New System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            FrmLiquidation.MinimizeBox = False
            FrmLiquidation.MaximizeBox = False
            Using m As New MControlOutpatientServices(Me.Tag)
                Dim admissionCollection = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                                                    .CrystalService.Liquidation_GetAdmission(numIngres)
                FrmLiquidation.AuxAdmissionToReload = admissionCollection
            End Using
            FrmLiquidation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            AddHandler FrmLiquidation.Shown, AddressOf HideLoaderForm
            Dim transparent As New FrmTransparent(FrmLiquidation, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que se dispara al cerrar el formulario de liquidación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub HideLoaderForm(sender As Object, e As EventArgs)
        waitForm.CloseWaitForm()
    End Sub

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        Select Case INDtcgInfo.SelectedTabPageName
            Case INDlcgOdontologyProcedures.Name 'Procedimientos Odontológicos
                INDgcOdontologyProcedures.DataSource = Nothing
                LoadDatasourceOdontologyProcedures()
            Case INDlcgLaboratories.Name 'Laboratorios
                INDgcLaboratories.DataSource = Nothing
                LoadDatasourceLaboratories()
            Case INDlcgPathologies.Name 'Patologias
                INDgcPathologies.DataSource = Nothing
                LoadDatasourcePathologies()
            Case INDlcgImages.Name 'Imagenes
                INDgcImages.DataSource = Nothing
                LoadDatasourceImages()
            Case INDlcgInterconsults.Name 'Interconsultas
                INDgcInterconsults.DataSource = Nothing
                LoadDatasourceInterconsults()
            Case INDlcgAdministrativeNotes.Name 'Notas Administrativas
                INDgcAdministrativeNotes.DataSource = Nothing
                LoadDatasourceAdministrativeNotes()
        End Select
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAccountControlAmbulatoryDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PAccountControlAmbulatory()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAccountControlAmbulatoryDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ControlChangedTabPage = True
        INDtcgInfo.SelectedTabPageIndex = 0
        INDtcgAppointments.SelectedTabPageIndex = 0
        ControlChangedTabPage = False
        SetAdmission()
    End Sub

#End Region

#Region "SelectPageChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la pagina de los tab
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtcgInfo_SelectedPageChanged(sender As Object, e As LayoutTabPageChangedEventArgs) Handles INDtcgInfo.SelectedPageChanged
        If ControlChangedTabPage = False Then
            Select Case e.Page.Name
                Case INDlcgOdontologyProcedures.Name 'Procedimientos Odontológicos
                    LoadDatasourceOdontologyProcedures()
                Case INDlcgLaboratories.Name 'Laboratorios
                    LoadDatasourceLaboratories()
                Case INDlcgPathologies.Name 'Patologias
                    LoadDatasourcePathologies()
                Case INDlcgImages.Name 'Imagenes
                    LoadDatasourceImages()
                Case INDlcgInterconsults.Name 'Interconsultas
                    LoadDatasourceInterconsults()
                Case INDlcgAdministrativeNotes.Name 'Notas Administrativas
                    LoadDatasourceAdministrativeNotes()
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia la pagina de los tab
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtcgAppointments_SelectedPageChanged(sender As Object, e As LayoutTabPageChangedEventArgs) Handles INDtcgAppointments.SelectedPageChanged
        Select Case e.Page.Name
            Case INDlcgAppointment.Name 'Citas
                LoadDatasourceAppointments()
        End Select
    End Sub

#End Region

#Region "BarMenu"

    ''' <summary>
    ''' Click al menu facturados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MBtnCurrentAdmission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnCurrentAdmission.ItemClick
        IsCurrentAdmission = True
        ProcessAccountControlAmbulatory()
    End Sub

    ''' <summary>
    ''' Click al menu abiertos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MBtnNewAdmission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnNewAdmission.ItemClick
        IsCurrentAdmission = False
        ProcessAccountControlAmbulatory()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla de procedimientos odontologicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelection_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelection.EditValueChanging
        If e IsNot Nothing Then
            Dim info As ViewOdontologyProceduresXpo = INDviewOdontologyProcedure.GetFocusedRow()
            info.Selection = e.NewValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla de laboratorios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectionLaboratories_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectionLaboratories.EditValueChanging
        If e IsNot Nothing Then
            Dim info As ViewLaboratoriesAccountControlAmbulatoryXpo = INDviewLaboratories.GetFocusedRow()
            info.Selection = e.NewValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla de patologias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectionPathologies_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectionPathologies.EditValueChanging
        If e IsNot Nothing Then
            Dim info As ViewPathologiesAccountControlAmbulatoryXpo = INDviewPathologies.GetFocusedRow()
            info.Selection = e.NewValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla de imagenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectionImages_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectionImages.EditValueChanging
        If e IsNot Nothing Then
            Dim info As ViewImagesAccountControlAmbulatoryXpo = INDviewImages.GetFocusedRow()
            info.Selection = e.NewValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla de interconsultas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectionInterconsults_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectionInterconsults.EditValueChanging
        If e IsNot Nothing Then
            Dim info As ViewInterconsultsAccountControlAmbulatoryXpo = INDviewInterconsults.GetFocusedRow()
            info.Selection = e.NewValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla de notas administrativas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectionAdministrativeNotes_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectionAdministrativeNotes.EditValueChanging
        If e IsNot Nothing Then
            Dim info As ViewAdministrativeNotesAccountControlAmbulatoryXpo = INDviewAdministrativeNotes.GetFocusedRow()
            info.Selection = e.NewValue
        End If
    End Sub

#End Region

#Region "CustomRowCellEdit"

    ''' <summary>
    ''' Evento que se dispara al pintar las celdas de la rejilla de procedimientos odontologicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewOdontologyProcedure_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDviewOdontologyProcedure.CustomRowCellEdit
        If e.Column.FieldName = "Selection" Then
            Dim obj = INDviewOdontologyProcedure.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                If obj.ServiceOrderId = 0 Then
                    e.RepositoryItem = INDrepCheckSelection
                Else
                    e.RepositoryItem = INDrepIcbeGenerated
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las celdas de la rejilla de laboratorios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewLaboratories_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDviewLaboratories.CustomRowCellEdit
        If e.Column.FieldName = "Selection" Then
            Dim obj = INDviewLaboratories.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                If obj.ServiceOrderId = 0 Then
                    e.RepositoryItem = INDrepCheckSelectionLaboratories
                Else
                    e.RepositoryItem = INDrepIcbeGeneratedLaboratories
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las celdas de la rejilla de patologias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewPathologies_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDviewPathologies.CustomRowCellEdit
        If e.Column.FieldName = "Selection" Then
            Dim obj = INDviewPathologies.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                If obj.ServiceOrderId = 0 Then
                    e.RepositoryItem = INDrepCheckSelectionPathologies
                Else
                    e.RepositoryItem = INDrepIcbeGeneratedPathologies
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las celdas de la rejilla de imagenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewImages_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDviewImages.CustomRowCellEdit
        If e.Column.FieldName = "Selection" Then
            Dim obj = INDviewImages.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                If obj.ServiceOrderId = 0 Then
                    e.RepositoryItem = INDrepCheckSelectionImages
                Else
                    e.RepositoryItem = INDrepIcbeGeneratedImages
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las celdas de la rejilla de interconsultas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewInterconsults_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDviewInterconsults.CustomRowCellEdit
        If e.Column.FieldName = "Selection" Then
            Dim obj = INDviewInterconsults.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                If obj.ServiceOrderId = 0 Then
                    e.RepositoryItem = INDrepCheckSelectionInterconsults
                Else
                    e.RepositoryItem = INDrepIcbeGeneratedInterconsults
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las celdas de la rejilla de notas administrativas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewAdministrativeNotes_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDviewAdministrativeNotes.CustomRowCellEdit
        If e.Column.FieldName = "Selection" Then
            Dim obj = INDviewAdministrativeNotes.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                If obj.ServiceOrderId = 0 Then
                    e.RepositoryItem = INDrepCheckSelectionAdministrativeNotes
                Else
                    e.RepositoryItem = INDrepIcbeGeneratedAdministrativeNotes
                End If
            End If
        End If
    End Sub

#End Region

#End Region

End Class
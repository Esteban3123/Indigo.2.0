'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2018
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports DevExpress.XtraGrid.Columns
Imports System.Drawing
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Text
Imports Domain.Base.Entities
Imports System.Windows.Forms

#End Region

Public Class FrmDispensingByPatient
    Implements IDispensingPatient

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        reportDef = New Reporter.rptPharmaceuticalDispensingNeckBand
        _frmReportViewer = New FrmReportViewer
        _frmReportViewer.TopLevel = False
        _frmReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        _frmReportViewer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        INDpanelViewReport.Controls.Add(_frmReportViewer)

        ctrTmp = New CtrStatusTransactions()
        ctrTmp.SetInfoFunction(AddressOf getValues)
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Tuple(Of Boolean, Boolean)
        Return New Tuple(Of Boolean, Boolean)(StatusTransactionVie, StatusTransactionHeon)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Codigo de centro de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Property CareCenterCode As String Implements IDispensingPatient.CareCenterCode
        Get
            Return INDsleCareCenter.EditValue
        End Get
        Set(value As String)
            INDsleCareCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource centro de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Property CareCenterXpo As XPInstantFeedbackSource Implements IDispensingPatient.CareCenterXpo
        Get
            Return INDsleCareCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCareCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Mensajes de feedback al usuario
    ''' </summary>
    ''' <param name="Icono"></param>
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

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDispensingPatient.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IDispensingPatient.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Tipo de despacho
    ''' </summary>
    ''' <returns></returns>
    Public Property OfficeType As Integer Implements IDispensingPatient.OfficeType
        Get
            Return INDsleOfficeType.EditValue
        End Get
        Set(value As Integer)
            INDsleOfficeType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Operador logistico
    ''' </summary>
    ''' <returns></returns>
    Public Property LogisticOperator As Integer Implements IDispensingPatient.LogisticOperator
        Get
            Return INDsleLogisticOperator.EditValue
        End Get
        Set(value As Integer)
            INDsleLogisticOperator.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Formula medica
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientIdentification As String Implements IDispensingPatient.PatientIdentification
        Get
            Return INDtxtPatientIdentification.EditValue
        End Get
        Set(value As String)
            INDtxtPatientIdentification.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    Public Property WarehouseCode As String Implements IDispensingPatient.WarehouseCode
        Get
            Return INDsleWarehouse.EditValue
        End Get
        Set(value As String)
            INDsleWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseXPO As XPInstantFeedbackSource Implements IDispensingPatient.WarehouseXpo
        Get
            Return CType(INDsleWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleWarehouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo identificación
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeIdentification As Integer Implements IDispensingPatient.TypeIdentification
        Get
            Return INDsleIdentificationType.EditValue
        End Get
        Set(value As Integer)
            INDsleIdentificationType.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Control de estado de transacciones
    ''' </summary>
    Public ctrTmp As CtrStatusTransactions

    ''' <summary>
    ''' Estado de transacción de VIE
    ''' </summary>
    Dim StatusTransactionVie As Boolean = False

    ''' <summary>
    ''' Estado de transacción de HEON
    ''' </summary>
    Dim StatusTransactionHeon As Boolean = False

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListTypeIdentification As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListLogisticOperator As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListOfficeType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDispensingByPatient

    ''' <summary>
    ''' Codigo de habilitacion
    ''' </summary>
    Dim HabilitationCode As String

    ''' <summary>
    ''' Reporte
    ''' </summary>
    Private _frmReportViewer As FrmReportViewer

    ''' <summary>
    ''' Reporte
    ''' </summary>
    Dim reportDef As Reporter.rptPharmaceuticalDispensingNeckBand

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements IcrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
        CleanControls()
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

    ''' <summary>
    ''' Abre el formulario de búsqueda personalizado por rango de fechas
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Using FormRequest As New FrmSearch()
            AddHandler FormRequest.ReturnMedicalOrderRecipe, AddressOf ReturnMedicalOrderRecipe

            Me.Cursor = ChangeCursorIndigo()
            FormRequest.SetDateValues = GetDateServer()
            FormRequest.LogisticOperator = LogisticOperator
            FormRequest.OfficeType = OfficeType
            FormRequest.HabilitationCode = HabilitationCode
            FormRequest.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(FormRequest, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        BarraBotones.StatusRecordVisible = False
        'CareCenterCode = Nothing
        'WarehouseCode = Nothing
        'OfficeType = Nothing
        'LogisticOperator = Nothing
        'HabilitationCode = String.Empty
        TypeIdentification = Nothing
        PatientIdentification = Nothing
        INDlyItemMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
    End Sub

    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        ListTypeIdentification = New List(Of Tuple(Of Integer, String))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(1, "Nit - No. Identificación Tributaria"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(2, "Cédula Ciudadanía"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(3, "Cédula Extranjería "))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(4, "Registro Civil - NIP"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(5, "Tarjeta Identidad"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(6, "Pasaporte"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(7, "Registro Mercantil"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(9, "Registro Civil - NUIP"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(10, "Adulto Sin Identificación"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(18, "Menor Sin Identificación"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(20, "IN - Iden temporal  personas sin no. comple en RC"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(21, "Ruc - Registro unico contribuyente"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(25, "Identificación Diplomática"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(27, "Certificado de Nacido Vivo"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(28, "Salvoconducto de Permanencia"))
        ListTypeIdentification.Add(New Tuple(Of Integer, String)(29, "Permiso Especial de Permanencia"))
        INDsleIdentificationType.Properties.DataSource = ListTypeIdentification.ToList

        ListOfficeType = New List(Of Tuple(Of Integer, String))
        ListOfficeType.Add(New Tuple(Of Integer, String)(2, "Ambito Hospitalario"))
        INDsleOfficeType.Properties.DataSource = ListOfficeType.ToList

        ListLogisticOperator = New List(Of Tuple(Of Integer, String))
        ListLogisticOperator.Add(New Tuple(Of Integer, String)(2, "Farmaquirurgicos SAS"))
        INDsleLogisticOperator.Properties.DataSource = ListLogisticOperator.ToList
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As String
        Dim errors As New StringBuilder

        If String.IsNullOrEmpty(CareCenterCode) Then
            errors.AppendLine("Debe seleccionar un centro de atención")
        End If

        If String.IsNullOrEmpty(WarehouseCode) Then
            errors.AppendLine("Debe seleccionar un almacén")
        End If

        If OfficeType = Nothing Then
            errors.AppendLine("Debe seleccionar un tipo de despacho")
        End If

        If LogisticOperator = Nothing Then
            errors.AppendLine("Debe seleccionar un operador logistico")
        End If

        If TypeIdentification = Nothing Then
            errors.AppendLine("Debe seleccionar un tipo de identificación")
        End If

        If String.IsNullOrEmpty(PatientIdentification) Then
            errors.AppendLine("Debe ingresar una identificación de paciente")
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Ejecuta el proceso para la dispensación
    ''' </summary>
    Private Async Sub ProcessGetDispensing()
        Try
            Dim errors = ValidateFields()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If

            AsyncLoader(True)

            Dim args As Object = New Dynamic.ExpandoObject()
            args.OfficeType = OfficeType
            args.LogisticOperator = LogisticOperator
            args.PatientIdentification = PatientIdentification
            args.TypeIdentification = TypeIdentification

            Dim result As ActionResult(Of WebServiceObject)

            Using model As New MDispensingByPatient(Me.MyTag)
                result = Await model.GetDispensingByPatient(args)
            End Using

            If result.StateResult = False Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Exit Sub
            End If

            If result.ObjectEmbbeded Is Nothing OrElse result.ObjectEmbbeded.paciente Is Nothing OrElse result.ObjectEmbbeded.solicitudDetalle.Count = 0 Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "No existen datos con los filtros ingresados"
                Exit Sub
            End If

            AsyncLoader(False)

            Using FormRequest As New FrmPopUpDashBoardPharmacyRequest(1)
                Me.Cursor = ChangeCursorIndigo()
                AddHandler FormRequest.DashBoardPharmacyRequestSuccessEventArgs, AddressOf DashBoardPharmacyRequestSuccess
                FormRequest.WebServiceObject = result.ObjectEmbbeded
                FormRequest.OfficeType = OfficeType
                FormRequest.LogisticOperator = LogisticOperator
                FormRequest.MedicalOrderRecipe = PatientIdentification
                FormRequest.PatientCode = result.ObjectEmbbeded.paciente.identPaciente
                FormRequest.PatientFirstName = result.ObjectEmbbeded.paciente.priNombrePaciente
                FormRequest.PatientMiddleName = result.ObjectEmbbeded.paciente.segNombrePaciente
                FormRequest.PatientLastName = result.ObjectEmbbeded.paciente.priApellidoPaciente
                FormRequest.PatientSecondLastName = result.ObjectEmbbeded.paciente.segApellidoPaciente
                FormRequest.PatientName = result.ObjectEmbbeded.paciente.priNombrePaciente + " " + result.ObjectEmbbeded.paciente.segNombrePaciente + " " + result.ObjectEmbbeded.paciente.priApellidoPaciente + " " + result.ObjectEmbbeded.paciente.segApellidoPaciente
                FormRequest.Consecutive = result.ObjectEmbbeded.solicitudDetalle(0).idIngreso.ToString()
                FormRequest.AdmissionNumber = result.ObjectEmbbeded.solicitudDetalle(0).idIngreso.ToString()
                FormRequest.FunctionalUnitCode = result.ObjectEmbbeded.solicitudDetalle(0).UnidadFuncional
                FormRequest.Store = WarehouseCode
                FormRequest.CareCenter = CareCenterCode
                FormRequest.ConsecutivePescription = 0
                FormRequest.ConsecutiveInputs = ""
                FormRequest.ConsecutivePharmacy = 0
                FormRequest.HistoryType = ""

                FormRequest.ViewModeEditHold = True
                FormRequest.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
                FormRequest.ControlBox = True
                FormRequest.MaximizeBox = False
                FormRequest.MinimizeBox = False
                FormRequest.Text = "Solicitudes"
                FormRequest.Size = New System.Drawing.Size(1090, 750)
                FormRequest.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                FormRequest.Owner = Me.MdiParent

                args = New Dynamic.ExpandoObject()
                args.LogisticOperator = LogisticOperator
                args.OfficeType = OfficeType
                args.CareCenterCode = CareCenterCode
                FormRequest.Args = args

                Dim transparent = New Base.FrmTransparent(FormRequest, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        Catch ex As Exception
            Me.Cursor = System.Windows.Forms.Cursors.Default
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para abrir el reporte con lo que devuelva el form modal de dispensación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DashBoardPharmacyRequestSuccess(sender As Object, e As DashBoardPharmacyEventArgs)
        If e.PharmaceuticalDispensing IsNot Nothing Then
            ReportHelper.ExecuteReport(_frmReportViewer, False, reportDef, Me, Me.BarraBotones.PermissionsForm, e.PharmaceuticalDispensing, 1)
            INDlyItemMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            BarraBotones.StatusRecordVisible = True
            StatusTransactionVie = e.StatusTransactionVie
            StatusTransactionHeon = e.StatusTransactionHeon
            ctrTmp.PrintInfo()
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para adquirir la orden medica desde el formulario de busqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnMedicalOrderRecipe(sender As Object, e As SearchMedicalOrderRecipeArgs)
        If Not String.IsNullOrEmpty(e.PatientIdentification) Then
            PatientIdentification = e.PatientIdentification
            TypeIdentification = e.TypeIdentification
            ProcessGetDispensing()
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        StatusTransactionHeon = Nothing
        StatusTransactionVie = Nothing
        ListTypeIdentification = Nothing
        ListLogisticOperator = Nothing
        ListOfficeType = Nothing
        Presenter = Nothing
        HabilitationCode = Nothing
        _frmReportViewer = Nothing
        reportDef = Nothing
    End Sub

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDispensingByPatient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Presenter = New PDispensingByPatient(Me)
        Deshacer()
        InitializeTuple()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDispensingByPatient_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareCenter.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Carga el datasource del centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If CareCenterXpo Is Nothing Then
            Presenter.LoadCareCenter()
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource de almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleWareHouse_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleWarehouse.QueryPopUp
        If INDsleWarehouse.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If WarehouseXPO Is Nothing Then
            Using model As New MReferralEntry(MyTag)
                WarehouseXPO = model.ListWarehouse()
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara cuando es seleccionado un almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareCenter.EditValueChanged
        If Not String.IsNullOrEmpty(CareCenterCode) Then
            Dim objectXpo = DirectCast(DirectCast(INDviewSearchCareCenter.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CentersXpo)
            If objectXpo IsNot Nothing Then
                HabilitationCode = objectXpo.CODIPSSEC
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de búsqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtRecetarioOMedica_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtPatientIdentification.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            Dim errors As New StringBuilder
            If LogisticOperator = Nothing Then
                errors.AppendLine("Debe seleccionar un operador logístico")
            End If
            If OfficeType = Nothing Then
                errors.AppendLine("Debe seleccionar un tipo de despacho")
            End If
            If CareCenterCode = Nothing Then
                errors.AppendLine("Debe seleccionar un centro de atención")
            End If
            If errors.ToString().Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara para cerrar la tirilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnCloseReport_Click(sender As Object, e As EventArgs) Handles INDbtnCloseReport.Click
        INDlyItemMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de identificación paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtPatientIdentification_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtPatientIdentification.KeyDown
        If e.KeyCode = Keys.Enter Then
            ProcessGetDispensing()
        End If
    End Sub

#End Region

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        INDsleIdentificationType.Focus()
    End Sub

    ''' <summary>
    ''' Procesar solicitud
    ''' </summary>
    Private Sub BarraBotones_ClickProcesar() Handles BarraBotones.ClickProcesar
        ProcessGetDispensing()
    End Sub

#End Region

End Class
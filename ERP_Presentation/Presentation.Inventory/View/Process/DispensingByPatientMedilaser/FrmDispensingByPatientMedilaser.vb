'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/09/2018
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
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Billing.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.XtraSplashScreen
Imports System.ComponentModel

#End Region

Public Class FrmDispensingByPatientMedilaser
    Implements IDispensingPatientMedilaser

#Region "Builder"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _frmReportViewer = New FrmReportViewer
        _frmReportViewer.TopLevel = False
        _frmReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        _frmReportViewer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        INDpanelViewReport.Controls.Add(_frmReportViewer)

        AddHandler bgw_viewReport.DoWork, AddressOf bgw_viewReport_DoWork
        AddHandler bgw_viewReport.RunWorkerCompleted, AddressOf bgw_viewReport_RunWorkerCompleted
    End Sub


#End Region

#Region "Properties"

    Public Property CareCenterCode As String Implements IDispensingPatientMedilaser.CareCenterCode
        Get
            Return INDsleCareCenter.EditValue
        End Get
        Set(value As String)
            INDsleCareCenter.EditValue = value
        End Set
    End Property

    Public Property CareCenterXpo As XPInstantFeedbackSource Implements IDispensingPatientMedilaser.CareCenterXpo
        Get
            Return INDsleCareCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCareCenter.Properties.DataSource = value
        End Set
    End Property

    Public Property WarehouseId As Integer Implements IDispensingPatientMedilaser.WarehouseId
        Get
            Return INDsleWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDsleWarehouse.EditValue = value
        End Set
    End Property

    Public Property WarehouseXpo As XPInstantFeedbackSource Implements IDispensingPatientMedilaser.WarehouseXpo
        Get
            Return INDsleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleWarehouse.Properties.DataSource = value
        End Set
    End Property

    Public Property CareGroupId As Integer Implements IDispensingPatientMedilaser.CareGroupId
        Get
            Return INDsleCareGroup.EditValue
        End Get
        Set(value As Integer)
            INDsleCareGroup.EditValue = value
        End Set
    End Property

    Public Property CareGroupXpo As XPInstantFeedbackSource Implements IDispensingPatientMedilaser.CareGroupXpo
        Get
            Return INDsleCareGroup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCareGroup.Properties.DataSource = value
        End Set
    End Property

    Public Property BillingAuthorizationId As Integer Implements IDispensingPatientMedilaser.BillingAuthorizationId
        Get
            Return INDsleBillingAuthorization.EditValue
        End Get
        Set(value As Integer)
            INDsleBillingAuthorization.EditValue = value
        End Set
    End Property

    Public Property BillingAuthorizationXpo As XPInstantFeedbackSource Implements IDispensingPatientMedilaser.BillingAuthorizationXpo
        Get
            Return INDsleBillingAuthorization.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBillingAuthorization.Properties.DataSource = value
        End Set
    End Property

    Public Property DateItem As DateTime? Implements IDispensingPatientMedilaser.DateItem
        Get
            Return INDdteDateItem.EditValue
        End Get
        Set(value As DateTime?)
            INDdteDateItem.EditValue = value
        End Set
    End Property

    Public Property Number As String Implements IDispensingPatientMedilaser.Number
        Get
            Return INDtxtNumber.EditValue
        End Get
        Set(value As String)
            INDtxtNumber.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements IDispensingPatientMedilaser.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDispensingPatientMedilaser.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

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

#End Region

#Region "Variables"

    ''' <summary>
    ''' Reporte
    ''' </summary>
    Private _frmReportViewer As FrmReportViewer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDispensingByPatientMedilaser

    ''' <summary>
    ''' Splash de espera para sacar la tirilla
    ''' </summary>
    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)

    ''' <summary>
    ''' Ejecuta el proceso de visualizar el reporte en segundo plano
    ''' </summary>
    Private bgw_viewReport As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Definición del reporte en tirilla
    ''' </summary>
    Dim reportDef As Reporter.rptSaleInvoiceReducedByDispensing

    ''' <summary>
    ''' Resultado del proceso
    ''' </summary>
    Dim SP_SaveDispensingByPatientMedilaser_Result As SP_SaveDispensingByPatientMedilaser_Result

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements IcrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        BarraBotones.StatusRecordVisible = False
        CareCenterCode = Nothing
        WarehouseId = Nothing
        CareGroupId = Nothing
        BillingAuthorizationId = Nothing
        DateItem = Nothing
        Number = Nothing
        INDbtnCloseReport.Enabled = True
        INDlyItemPanelMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
        reportDef = Nothing
    End Sub

    ''' <summary>
    ''' Método que procesa la fórmula médica y trae el listado de medicamentos
    ''' </summary>
    Private Async Sub Process()
        Try
            'Se validan los campos del formulario
            Dim errors = ValidateFields()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If

            'Se valida si la formula médica esta siendo utilizada por otra persona
            Dim query As New StringBuilder()
            Dim flag As Boolean = False
            Using model As New MLiquidation()

                If SessionValues.Instance.ArchitectureType = 2 Then
                    query.AppendLine($"SELECT ICD.DocumentNumber, ICD.DocumentUser, P.FirstName +' '+ P.SecondName +' '+ P.FirstLastName +' '+ P.SecondLastName AS UserName FROM Inventory.InventoryControlDocument ICD WITH (NOLOCK) LEFT JOIN Security.[User] U ON ICD.DocumentUser = U.UserCode LEFT JOIN Security.Person P ON U.IdPerson = P.Id WHERE ICD.DocumentNumber = '" & Number & "' AND ICD.DocumentType = 50")
                Else
                    query.AppendLine($"SELECT ICD.DocumentNumber, ICD.DocumentUser, P.FirstName +' '+ P.SecondName +' '+ P.FirstLastName +' '+ P.SecondLastName AS UserName FROM Inventory.InventoryControlDocument ICD WITH (NOLOCK) LEFT JOIN " & SessionValues.Instance.SecurityContainer & ".Security.[User] U WITH (NOLOCK) ON ICD.DocumentUser = U.UserCode LEFT JOIN " & SessionValues.Instance.SecurityContainer & ".Security.Person P WITH (NOLOCK) ON U.IdPerson = P.Id WHERE ICD.DocumentNumber = '" & Number & "' AND ICD.DocumentType = 50")
                End If

                Dim dtResult As DataTable = Await model.ExecuteCommandDt(query.ToString(), SessionValues.Instance.TransactionalContainer)
                Dim user = {indigo.UserIndigo}
                Dim _userInFormula = String.Empty
                If dtResult.Rows.Count > 0 Then
                    _userInFormula = dtResult(0)("DocumentUser").ToString()
                End If
                If dtResult IsNot Nothing AndAlso dtResult.Rows.Count > 0 Then
                    If user(0).Equals(_userInFormula) Then
                        flag = True
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "La fórmula médica " + Number + " ya está siendo utilizada por el usuario " + dtResult(0)("DocumentUser") + " - " + dtResult(0)("UserName")
                        Exit Sub
                    End If
                End If
            End Using

            AsyncLoader(True)

            'Resultado para los medicamentos de Medilaser
            Dim result As ActionResult(Of List(Of SP_ListHCPRESCRDByCODCONCEC_Result)) = Nothing

            'Resultado para los medicamentos de las tablas propias
            Dim resultXpo As MedicalFormulaXpo = Nothing

            'Se valida si ya existe un registro con la formula medica
            resultXpo = Presenter.GetMedicalFormulaByNumber(Number)

            'Si no hay registro en las tablas propias con la formula medica, se consume el servicio para traer los medicamentos de Medilaser
            If resultXpo Is Nothing Then
                Using model As New MDispensingByPatientMedilaser(Me.Tag)
                    result = Await model.GetListProductsByCODCONCEC(Number)
                End Using

                If result.StateResult = False Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If

                If result.ObjectEmbbeded Is Nothing OrElse result.ObjectEmbbeded.Count = 0 Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = "No hay medicamentos con la fórmula médica ingresada"
                    Exit Sub
                End If
            End If

            'Si hay medicamentos con la formula médica se bloquea la formula
            If flag = False Then
                query.Clear()
                query.AppendLine($"insert into Inventory.InventoryControlDocument(DocumentNumber, DocumentType, DocumentUser, DocumentDate) values('{Number}', 50, '{indigo.UserIndigo}', '{Date.Now().Year}-{Date.Now().Day}-{Date.Now().Month}')")
                Using model As New MLiquidation()
                    Dim resultControl = Await model.ExecuteQuery(query.ToString(), SessionValues.Instance.TransactionalContainer)
                    If Not resultControl Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un error guardando la fórmula médica en la tabla de control"
                        Exit Sub
                    End If
                End Using
            End If

            Using FormRequest As New FrmPopUpDashBoardPharmacyRequest(2)
                Me.Cursor = ChangeCursorIndigo()
                AddHandler FormRequest.DashBoardPharmacyRequestSuccessEventArgs, AddressOf DashBoardPharmacyRequestSuccess

                'Si el resultado viene de Medilaser
                If result IsNot Nothing Then
                    FormRequest.DocumentDate = Convert.ToDateTime(INDdteDateItem.EditValue)
                    FormRequest.ListProductsByCODCONCEC = result.ObjectEmbbeded
                    FormRequest.MedicalFormulaDetailXpo = Nothing
                    FormRequest.PatientCode = result.ObjectEmbbeded(0).IPCODPACI.Trim()
                    FormRequest.PatientFirstName = result.ObjectEmbbeded(0).IPPRINOMB.Trim()
                    FormRequest.PatientMiddleName = result.ObjectEmbbeded(0).IPSEGNOMB.Trim()
                    FormRequest.PatientLastName = result.ObjectEmbbeded(0).IPPRIAPEL.Trim()
                    FormRequest.PatientSecondLastName = result.ObjectEmbbeded(0).IPSEGAPEL.Trim()
                    FormRequest.PatientName = result.ObjectEmbbeded(0).IPNOMCOMP.Trim()
                    FormRequest.Consecutive = result.ObjectEmbbeded(0).NUMINGRES.Trim()
                    FormRequest.AdmissionNumber = result.ObjectEmbbeded(0).NUMINGRES.Trim()
                    FormRequest.FunctionalUnitCode = result.ObjectEmbbeded(0).UFUCODIGO.Trim() + " - " + result.ObjectEmbbeded(0).UFUDESCRI.Trim()
                Else 'Si el resultado viene de las tablas propias
                    FormRequest.DocumentDate = Convert.ToDateTime(INDdteDateItem.EditValue).ToString("dd/MM/yyyy")
                    FormRequest.ListProductsByCODCONCEC = Nothing
                    FormRequest.MedicalFormulaDetailXpo = resultXpo.MedicalFormulaDetailXpo.ToList() '.Where(Function(d) d.PendingQuantity > 0).ToList()
                    FormRequest.PatientCode = resultXpo.PatientCode
                    FormRequest.PatientFirstName = resultXpo.PatientFirstName
                    FormRequest.PatientMiddleName = resultXpo.PatientSecondName
                    FormRequest.PatientLastName = resultXpo.PatientFirstLastName
                    FormRequest.PatientSecondLastName = resultXpo.PatientSecondLastName
                    FormRequest.PatientName = resultXpo.PatientName
                    FormRequest.Consecutive = IIf(String.IsNullOrEmpty(resultXpo.AdmissionNumber), 0, resultXpo.AdmissionNumber)
                    FormRequest.AdmissionNumber = resultXpo.AdmissionNumber
                    FormRequest.FunctionalUnitCode = resultXpo.FunctionalUnidCode + " - " + resultXpo.FunctionalUnitName
                End If

                FormRequest.Store = INDsleWarehouse.Text
                FormRequest.CareCenter = CareCenterCode
                FormRequest.ConsecutivePescription = 0
                FormRequest.ConsecutiveInputs = ""
                FormRequest.ConsecutivePharmacy = 0
                FormRequest.HistoryType = ""

                FormRequest.CareCenterCodeIntegrationMedilaser = CareCenterCode
                FormRequest.WareHouseIdIntegrationMedilaser = WarehouseId
                FormRequest.CareGroupIdIntegrationMedilaser = CareGroupId
                FormRequest.BillingAuthorizationIdIntegrationMedilaser = BillingAuthorizationId
                FormRequest.DateIntegrationMedilaser = DateItem
                FormRequest.NumberIntegrationMedilaser = Number

                FormRequest.ViewModeEditHold = True
                FormRequest.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
                FormRequest.ControlBox = True
                FormRequest.MaximizeBox = False
                FormRequest.MinimizeBox = False
                FormRequest.Text = "Listado de medicamentos para la fórmula médica " + Number
                FormRequest.Size = New System.Drawing.Size(1090, 750)
                FormRequest.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                FormRequest.Owner = Me.MdiParent

                Dim transparent = New Base.FrmTransparent(FormRequest, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using

            'Se elimina la formula medica de la tabla de control
            query.Clear()
            query.AppendLine($"delete from Inventory.InventoryControlDocument where DocumentNumber = '{Number}' and DocumentType = 50")
            Using model As New MLiquidation()
                Dim resultControl = Await model.ExecuteQuery(query.ToString(), SessionValues.Instance.TransactionalContainer)
                If Not resultControl Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un error eliminando la fórmula médica en la tabla de control"
                    Exit Sub
                End If
            End Using

            AsyncLoader(False)
        Catch ex As Exception
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
        If e.SP_SaveDispensingByPatientMedilaser_Result IsNot Nothing Then
            SP_SaveDispensingByPatientMedilaser_Result = e.SP_SaveDispensingByPatientMedilaser_Result
            INDlyItemPanelMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDbtnCloseReport.Enabled = False

            'Se ejecuta el proceso de visualización del reporte
            bgw_viewReport.RunWorkerAsync()
        End If
    End Sub

    ''' <summary>
    ''' Inicia el asyncrono
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_viewReport_DoWork(sender As Object, e As DoWorkEventArgs)
        reportDef = New Reporter.rptSaleInvoiceReducedByDispensing
        reportDef.ParametrosReporte = New Object() {SP_SaveDispensingByPatientMedilaser_Result.InvoiceId, SP_SaveDispensingByPatientMedilaser_Result.AdmissionNumber}
        reportDef.CargarDataSource()
    End Sub

    ''' <summary>
    ''' Termina el asyncrono
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_viewReport_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        ReportHelper.ExecuteReport(_frmReportViewer, False, reportDef, Me, Me.BarraBotones.PermissionsForm)
        INDbtnCloseReport.Enabled = True
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

        If WarehouseId = Nothing Then
            errors.AppendLine("Debe seleccionar un almacén")
        End If

        If CareGroupId = Nothing Then
            errors.AppendLine("Debe seleccionar un grupo de atención")
        End If

        If BillingAuthorizationId = Nothing Then
            errors.AppendLine("Debe seleccionar una autorización de facturación")
        End If

        If DateItem Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha")
        End If

        If String.IsNullOrEmpty(Number) Then
            errors.AppendLine("Debe ingresar una fórmula médica")
        End If

        Return errors.ToString()
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmDispensingByPatientMedilaser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Presenter = New PDispensingByPatientMedilaser(Me)
        Deshacer()
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If CareCenterXpo Is Nothing Then
            Presenter.LoadCareCenter()
        End If
    End Sub

    Private Sub INDsleWarehouse_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleWarehouse.QueryPopUp
        If WarehouseXpo Is Nothing Then
            Presenter.LoadWareHouse()
        End If
    End Sub

    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If CareGroupXpo Is Nothing Then
            Presenter.LoadCareGroup()
        End If
    End Sub

    Private Sub INDsleBillingAuthorization_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBillingAuthorization.QueryPopUp
        If BillingAuthorizationXpo Is Nothing Then
            Presenter.LoadBillingAuthorization()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(302, Nothing, True)
            Presenter.LoadWareHouse()
        End If
    End Sub

    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(985, Nothing, True)
            Presenter.LoadCareGroup()
        End If
    End Sub

    Private Sub INDsleBillingAuthorization_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBillingAuthorization.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1517, Nothing, True)
            Presenter.LoadBillingAuthorization()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmDispensingByPatientMedilaser_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareCenter.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Click sobre el boton para cerrar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnCloseReport_Click(sender As Object, e As EventArgs) Handles INDbtnCloseReport.Click
        INDlyItemPanelMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
        reportDef = Nothing
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
    End Sub

    ''' <summary>
    ''' Procesar solicitud
    ''' </summary>
    Private Sub BarraBotones_ClickProcesar() Handles BarraBotones.ClickProcesar
        Process()
    End Sub

#End Region

End Class
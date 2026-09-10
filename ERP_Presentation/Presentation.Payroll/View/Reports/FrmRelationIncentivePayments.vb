#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmRelationIncentivePayments
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoUnit As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Estado de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingStatus As List(Of Tuple(Of String, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of String, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of String, String))
                _FillingStatus.Add(New Tuple(Of String, String)("C", "Confirmados"))
                _FillingStatus.Add(New Tuple(Of String, String)("", "Sin Confirmar"))
                _FillingStatus.Add(New Tuple(Of String, String)("S", "Saldo Inicial"))
                _FillingStatus.Add(New Tuple(Of String, String)("T", "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property
    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de tipo de reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Estado de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingTypeEmployee As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeEmployee As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeEmployee Is Nothing Then
                _FillingTypeEmployee = New List(Of Tuple(Of Integer, String))
                ' Cargar desde base de datos
                Dim employeeTypes = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollEmployeeTypeXpo)(Nothing, "State = True")
                For Each empType In employeeTypes
                    _FillingTypeEmployee.Add(New Tuple(Of Integer, String)(empType.Id, empType.Name))
                Next
                ' Agregar opción "Todo"
                _FillingTypeEmployee.Add(New Tuple(Of Integer, String)(0, "Todo"))
            End If
            Return _FillingTypeEmployee
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Tupla de Datos de Periodo
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingPeriod As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingPeriod As List(Of Tuple(Of Integer, String))
        Get
            If _FillingPeriod Is Nothing Then
                _FillingPeriod = New List(Of Tuple(Of Integer, String))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(1, "1"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(2, "2"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _FillingPeriod
        End Get
    End Property

    Private criteria As String = Nothing

#End Region

    '''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        'Valida Grupo
        If INDSleGroupStart.EditValue Is Nothing And INDSleGroupEnd.EditValue IsNot Nothing Or INDSleGroupEnd.EditValue Is Nothing And INDSleGroupStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If

        'Validar Filtros
        ''Valida Grupo
        'If INDSleGroupStart.EditValue Is Nothing And INDSleGroupEnd.EditValue IsNot Nothing Or INDSleGroupEnd.EditValue Is Nothing And INDSleGroupStart.EditValue IsNot Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
        '    Me.INDSleGroupStart.Focus()
        '    Validations = False
        'ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
        '    Me.INDSleGroupStart.Focus()
        '    Validations = False
        'End If

        'Valida Unidad funcional
        'If INDSleUnitStart.EditValue Is Nothing And INDSleUnitEnd.EditValue IsNot Nothing Or INDSleUnitEnd.EditValue Is Nothing And INDSleUnitStart.EditValue IsNot Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblUnit.Text)
        '    Me.INDSleUnitStart.Focus()
        '    Validations = False
        'ElseIf INDSleUnitEnd.EditValue < INDSleUnitStart.EditValue Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblUnit.Text)
        '    Me.INDSleUnitStart.Focus()
        '    Validations = False
        'End If
        Return Validations
    End Function

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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
    ''' metodo para Cargar el data source Del Control INDSleGroupStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ' ''' <summary>
    ' ''' metodo para Cargar el data source Del Control INDSleUnitStart
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub LoadXpoUnitStart()
    '    Using msearch As New MBusqueda
    '        ProoftCloseXpoUnit = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
    '        INDSleUnitStart.Datasource = ProoftCloseXpoUnit
    '    End Using
    'End Sub

    ' ''' <summary>
    ' ''' metodo para Cargar el data source Del Control INDSleUnitEnd
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub LoadXpoUnitEnd()
    '    Using msearch As New MBusqueda
    '        ProoftCloseXpoUnit = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
    '        INDSleUnitEnd.Datasource = ProoftCloseXpoUnit
    '    End Using
    'End Sub

    '' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleEmployee.Datasource = ProoftCloseXpoEmployee
        End Using
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerateReport
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>    
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        If Me.ValidateControlsReports Then
            ' Obtener el tipo de empleado seleccionado
            Dim selectedEmployeeType = FillingTypeEmployee.FirstOrDefault(Function(x) x.Item1 = INDGleTypeEmployee.EditValue)
            Dim employeeTypeName As String = If(selectedEmployeeType IsNot Nothing, selectedEmployeeType.Item2, "")


            If INDGleTypeReport.EditValue = 1 Then

                AsyncLoader(True)
                Dim reporte As New rptRelationIncentivePayments()

                reporte.ParametrosReporte = {
                    INDDateStart.EditValue,
                    INDDateEnd.EditValue,
                    INDSleEmployee.EditValue,
                    INDSleGroupStart.EditValue,
                    INDSleGroupEnd.EditValue,
                    INDGlePeriod.EditValue,
                    INDsleSucursalStart.EditValue,
                    INDsleSucursalEnd.EditValue}

                INDDvViewReport.DocumentSource = reporte

                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
            Else
                'INI----------------Liquidación de primas

                If employeeTypeName = "ADMINISTRATIVO" Then
                    If INDSleGroupStart.EditValue Is Nothing Then
                        AsyncLoader(True)
                        Dim reporte As New rptIncentivePaymentBasicadministrative()

                        reporte.ParametrosReporte = {
                            INDDateStart.EditValue,
                            INDDateEnd.EditValue,
                            INDGleTypeEmployee.EditValue,
                            INDSleEmployee.EditValue,
                            INDGlePeriod.EditValue,
                            Nothing,
                            INDsleSucursalStart.EditValue,
                            INDsleSucursalEnd.EditValue}

                        INDDvViewReport.DocumentSource = reporte

                        reporte.CargarDataSource()
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            reporte.CreateDocument(True)
                        End If
                        AsyncLoader(False)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcViewReport.Visible = True
                            INDDvViewReport.Show()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                            Me.INDDateStart.Focus()
                        End If
                    Else
                        AsyncLoader(True)
                        Dim reporte As New rptIncentivePaymentBasicadministrativeByGroup2()

                        reporte.ParametrosReporte = {
                            INDDateStart.EditValue,
                            INDDateEnd.EditValue,
                            INDGleTypeEmployee.EditValue,
                            INDSleEmployee.EditValue,
                            INDSleGroupStart.EditValue,
                            INDSleGroupEnd.EditValue,
                            INDGlePeriod.EditValue,
                            INDsleSucursalStart.EditValue,
                            INDsleSucursalEnd.EditValue}

                        INDDvViewReport.DocumentSource = reporte

                        reporte.CargarDataSource()
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            reporte.CreateDocument(True)
                        End If
                        AsyncLoader(False)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcViewReport.Visible = True
                            INDDvViewReport.Show()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                            Me.INDDateStart.Focus()
                        End If
                    End If
                ElseIf employeeTypeName = "ASISTENCIAL" Then
                    If INDSleGroupStart.EditValue Is Nothing Then
                        AsyncLoader(True)
                        Dim reporte As New rptIncentivePaymentBasicOperative2()

                        reporte.ParametrosReporte = {
                            INDDateStart.EditValue,
                            INDDateEnd.EditValue,
                            INDGleTypeEmployee.EditValue,
                            INDSleEmployee.EditValue,
                            INDGlePeriod.EditValue,
                            Nothing,
                            INDsleSucursalStart.EditValue,
                            INDsleSucursalEnd.EditValue}

                        INDDvViewReport.DocumentSource = reporte

                        reporte.CargarDataSource()
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            reporte.CreateDocument(True)
                        End If
                        AsyncLoader(False)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcViewReport.Visible = True
                            INDDvViewReport.Show()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                            Me.INDDateStart.Focus()
                        End If
                    Else
                        AsyncLoader(True)
                        Dim reporte As New rptIncentivePaymentBasicOperativeByGroup2()

                        reporte.ParametrosReporte = {
                            INDDateStart.EditValue,
                            INDDateEnd.EditValue,
                            INDGleTypeEmployee.EditValue,
                            INDSleEmployee.EditValue,
                            INDSleGroupStart.EditValue,
                            INDSleGroupEnd.EditValue,
                            INDGlePeriod.EditValue,
                            INDsleSucursalStart.EditValue,
                            INDsleSucursalEnd.EditValue}

                        INDDvViewReport.DocumentSource = reporte

                        reporte.CargarDataSource()
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            reporte.CreateDocument(True)
                        End If
                        AsyncLoader(False)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcViewReport.Visible = True
                            INDDvViewReport.Show()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                            Me.INDDateStart.Focus()
                        End If
                    End If
                Else
                    If INDSleGroupStart.EditValue Is Nothing Then

                        AsyncLoader(True)
                        Dim reporte As New rptIncentivePaymentBasiCallTheIncome2()

                        reporte.ParametrosReporte = {
                            INDDateStart.EditValue,
                            INDDateEnd.EditValue,
                            INDGleTypeEmployee.EditValue,
                            INDSleEmployee.EditValue,
                            INDGlePeriod.EditValue,
                            Nothing,
                            INDsleSucursalStart.EditValue,
                            INDsleSucursalEnd.EditValue,
                            "FrmRelationIncentivePayments"}

                        INDDvViewReport.DocumentSource = reporte

                        reporte.CargarDataSource()
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            reporte.CreateDocument(True)
                        End If
                        AsyncLoader(False)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcViewReport.Visible = True
                            INDDvViewReport.Show()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                            Me.INDDateStart.Focus()
                        End If
                    Else
                        AsyncLoader(True)
                        Dim reporte As New rptIncentivePaymentBasiCallTheIncomeByGroup2()

                        reporte.ParametrosReporte = {
                            INDDateStart.EditValue,
                            INDDateEnd.EditValue,
                            INDGleTypeEmployee.EditValue,
                            INDSleEmployee.EditValue,
                            INDSleGroupStart.EditValue,
                            INDSleGroupEnd.EditValue,
                            INDGlePeriod.EditValue,
                            INDsleSucursalStart.EditValue,
                            INDsleSucursalEnd.EditValue}

                        INDDvViewReport.DocumentSource = reporte

                        reporte.CargarDataSource()
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            reporte.CreateDocument(True)
                        End If
                        AsyncLoader(False)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcViewReport.Visible = True
                            INDDvViewReport.Show()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                            Me.INDDateStart.Focus()
                        End If
                    End If
                End If

                'FIN----------------Liquidación de primas
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDateStart.Focus()
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportPayrollTotal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ''Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport
        Me.INDGleTypeEmployee.Properties.DataSource = FillingTypeEmployee
        Me.INDGlePeriod.Properties.DataSource = FillingPeriod
        Me.INDGlePeriod.EditValue = 3
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub

    ' ''' <summary>
    ' ''' se ejecuta en el evento QueryPopUp del Control INDSleUnitStart
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub INDSleUnitStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs)
    '    If INDSleUnitStart.Datasource Is Nothing Then
    '        LoadXpoUnitStart()
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' se ejecuta en el evento QueryPopUp del Control INDSleUnitEnd
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub INDSleUnitEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs)
    '    If INDSleUnitEnd.Datasource Is Nothing Then
    '        LoadXpoUnitEnd()
    '    End If
    'End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Datasource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub

    Private Sub FrmReportPayrollTotal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.ToolBar.Visible = False
        'Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetDocumentType
        'Me.INDSleVoucherTypeEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetDocumentType
        'Me.INDSleUnitStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        'Me.INDSleUnitEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
    End Sub

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 2 Then
            INDLciTypeEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDGleTypeEmployee.EditValue = Nothing
            INDLciTypeEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDsleSucursalStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalStart.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

    Private Sub INDsleSucursalEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalEnd.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub
End Class
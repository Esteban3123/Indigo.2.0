#Region "Imports"

Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Reporter
Imports Presentation.Base.BaseClass
#End Region

Public Class FrmReportBudgetExecutionRegulationColombia

#Region "Variables"

    Private _pucModel As MCommon

    Private _yearValidity As Integer

    Private _criterias As Dictionary(Of String, String)

#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad usada para el xpo de vigencia
    ''' </summary>
    ''' <returns></returns>
    Private Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para llenar el pop pup del tipo de reporte con tuplas
    ''' </summary>
    Private _TypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property TypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _TypeReport Is Nothing Then
                _TypeReport = New List(Of Tuple(Of Integer, String))
                _TypeReport.Add(New Tuple(Of Integer, String)(1, "Programación de ingresos"))
                _TypeReport.Add(New Tuple(Of Integer, String)(2, "Ejecución de ingresos"))
                _TypeReport.Add(New Tuple(Of Integer, String)(3, "Programación de gastos - departamentos, municipios, San Andrés y Providencia y Bogotá D.C"))
                _TypeReport.Add(New Tuple(Of Integer, String)(4, "Programación de gastos para establecimientos públicos territoriales"))
                _TypeReport.Add(New Tuple(Of Integer, String)(5, "Ejecución de gastos - departamentos, municipios, San Andrés y Providencia y Bogotá D.C"))
                _TypeReport.Add(New Tuple(Of Integer, String)(6, "Ejecución de gastos para establecimientos públicos territoriales"))
            End If
            Return _TypeReport
        End Get
    End Property
    ''' <summary>
    ''' Propiedad del id del reporte
    ''' </summary>
    ''' <returns></returns>
    Private Property ReportTypeId() As Integer?
        Get
            Return INDGleTypeReport.EditValue
        End Get
        Set(ByVal value As Integer?)
            INDGleTypeReport.EditValue = value
        End Set
    End Property

#End Region

#Region "BarraBotones"
    ''' <summary>
    ''' Carga lo botones de la barra de permisos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Metodo para traer el primer dato por defecto si la vigencia del xpo tiene estado 2
    ''' </summary>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleValidity.EditValue = item.Id
                _yearValidity = item.Year
                INDDnDate.SetYear = _yearValidity
                CleanControlsSelectValidity()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles 
    ''' </summary>
    Sub CleanControlsSelectValidity()
        INDDnDate.Enabled = True
        INDGleTypeReport.Enabled = True
        INDSbGenerateReport.Enabled = True
        INDSbGenerateExcell.Enabled = True
        ReportTypeId = 1
    End Sub

    ''' <summary>
    ''' valida que los campos no esten vacios 
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDsleValidity.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDlciValidity.Text))
            Me.INDsleValidity.Focus()
        End If

        If ReportTypeId Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDLciTypeReport.Text))
            Me.INDGleTypeReport.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("ValidityId", INDsleValidity.EditValue)
        _criterias.Add("Year", INDDnDate.GetYear())
        _criterias.Add("Month", INDDnDate.GetMonth())
        _criterias.Add("TypeReport", INDGleTypeReport.EditValue)

        Return True
    End Function

    ''' <summary>
    ''' Funcion para cargar la categoria por medio de la criteria y pasada al xpo
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadCategory() As Object
        Dim criteria = "Status = True"
        Select Case ReportTypeId
            Case 1, 2
                criteria = String.Format("{0} And ItemType = {1}", criteria, 1)
            Case 3, 4, 5, 6
                criteria = String.Format("{0} And ItemType = {1}", criteria, 2)
        End Select

        If INDsleValidity.EditValue IsNot Nothing Then
            criteria &= " And BudgetaryValidityId.Id =" & INDsleValidity.EditValue
        End If
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCategoryByFilter(criteria)
    End Function

#Region "ToExcel"
    ''' <summary>
    ''' Cargamos la Data que s eusara para general el Excel
    ''' </summary>
    ''' <param name="dtReportBudget"></param>
    Private Sub chargueDataSource(ByVal dtReportBudget As DataTable)
        Dim dt As New DataTable
        'validar tipo de reporte
        Select Case ReportTypeId
            Case 1

                        '"Programación de ingresos"
                        'dt.Columns.Add("Nombre", GetType(Type))
                        ''Procesar registros
                        'For Each item In dtReportBudget.Rows
                        '    Dim row As DataRow = dt.NewRow
                        '    'Dar formato asignar
                        '    row.Item("Nombre") = item("Name")
                        '    dt.Rows.Add(row)
                        'Next
            Case 2
                        '"Ejecución de ingresos"
                        'dt.Columns.Add("Nombre", GetType(Type))
                        'For Each item In dtReportBudget.Rows
                        '    Dim row As DataRow = dt.NewRow
                        '    'Dar formato asignar
                        '    row.Item("Nombre") = item("Name")
                        '    dt.Rows.Add(row)
                        'Next
            Case 3
                        '"Programación de gastos - departamentos, municipios, San Andrés y Providencia y Bogotá D.C"
                        'dt.Columns.Add("Nombre", GetType(Type))
                       'For Each item In dtReportBudget.Rows
                        '    Dim row As DataRow = dt.NewRow
                        '    'Dar formato asignar
                        '    row.Item("Nombre") = item("Name")
                        '    dt.Rows.Add(row)
                        'Next
            Case 4
                '"Programación de gastos para establecimientos públicos territoriales"
                dt.Columns.Add("Vigencia gasto", GetType(Decimal))
                'dt.Columns.Add("Nombre", GetType(Type))
                'dt.Columns.Add("Nombre", GetType(Type))
                For Each item In dtReportBudget.Rows
                    Dim row As DataRow = dt.NewRow
                    'Dar formato asignar
                    row.Item("Vigencia gasto") = item("ValidityType")
                    'row.Item("Nombre") = item("Name")
                    dt.Rows.Add(row)
                Next
            Case 5
                        '"Ejecución de gastos - departamentos, municipios, San Andrés y Providencia y Bogotá D.C"
                        'dt.Columns.Add("Nombre", GetType(Type))
                       'For Each item In dtReportBudget.Rows
                        '    Dim row As DataRow = dt.NewRow
                        '    'Dar formato asignar
                        '    row.Item("Nombre") = item("Name")
                        '    dt.Rows.Add(row)
                        'Next
            Case 6
                '"Ejecución de gastos para establecimientos públicos territoriales"
                'dt.Columns.Add("Nombre", GetType(Type))
                'For Each item In dtReportBudget.Rows
                '    Dim row As DataRow = dt.NewRow
                '    'Dar formato asignar
                '    row.Item("Nombre") = item("Name")
                '    dt.Rows.Add(row)
                'Next
        End Select
        'INDGcExportExcell.DataSource = dt
    End Sub


    'Private Sub generateExcel()
    '    Dim _gridView = Me.INDGcExportExcell
    '    _gridView.MainView.PopulateColumns()
    '    If _gridView IsNot Nothing Then
    '        Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
    '        Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
    '        _gridView.ExportToXlsx(fileName, param)
    '        If System.IO.File.Exists(fileName) Then
    '            System.Diagnostics.Process.Start(fileName)
    '        End If
    '    End If
    '    Me.INDGcExportExcell.DataSource = Nothing
    '    Me.INDGcExportExcell.RefreshDataSource()
    'End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' se carga el Frm 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportBudgetExecutionRegulationColombia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = TypeReport

        'Dar un valores por defecto
        ReportTypeId = 1

        'Establecer la fecha actual
        Dim dateNow = Me.GetDateServer()
        INDDnDate.SetMonth = Month(dateNow)
        INDDnDate.SetYear = Year(dateNow)
    End Sub

    ''' <summary>
    ''' fucnion para liberar memmoria la cerrar el frm 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()

        _model = Nothing
        _yearValidity = Nothing
        _criterias = Nothing
        _TypeReport = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If INDsleEntity.Properties.DataSource Is Nothing Then
            INDsleEntity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
        End If
    End Sub


#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se ejecuta en el evento OnChangeDate del control INDCdnDateStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDDnDate_OnChangeDate(sender As Object, e As EventArgs) Handles INDDnDate.OnChangeDate
        If INDDnDate.GetYear <> _yearValidity Then
            INDDnDate.SetYear = _yearValidity
        End If
    End Sub

    ''' <summary>
    ''' evento para cargar resolucion , valor y estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If INDsleEntity.EditValue IsNot Nothing Then
            INDsleValidity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(INDsleEntity.EditValue)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor de la vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If INDsleValidity.EditValue IsNot Nothing Then
            CleanControlsSelectValidity()
            Me._pucModel.ValidityId = INDsleValidity.EditValue
            Dim item = (From l In BudgetaryValidityXpo Where l.Id = INDsleValidity.EditValue Select l).FirstOrDefault
            If item IsNot Nothing Then
                _yearValidity = item.Year
                INDDnDate.SetYear = _yearValidity
            End If
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)

                Dim reporte = Nothing
                Select Case ReportTypeId
                    Case 1
                        '"Programación de ingresos"
                        reporte = New rptReportIncomeProgramming
                    Case 2
                        '"Ejecución de ingresos"
                        'reporte = New rptReportBudget...
                    Case 3
                        '"Programación de gastos - departamentos, municipios, San Andrés y Providencia y Bogotá D.C"
                        'reporte = New rptReportBudget...
                    Case 4
                        '"Programación de gastos para establecimientos públicos territoriales"
                        reporte = New rptExpendituresProgrammingForTerritorialPublicEstablisments
                    Case 5
                        '"Ejecución de gastos - departamentos, municipios, San Andrés y Providencia y Bogotá D.C"
                        'reporte = New rptReportBudget...
                    Case 6
                        '"Ejecución de gastos para establecimientos públicos territoriales"
                        reporte = New rptReportBudgetExecutionOfExpensesForTerritorialPublicEntities
                End Select
                reporte.ParametrosReporte = New Object() {_criterias}
                INDDvViewReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDnDate.Focus()
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateTxt_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Dim fileName As String = ""
                Dim resultStringBuilder As StringBuilder = Nothing
                Using model As New Presentation.Budget.MVP.MReports(Me.Tag)
                    Select Case ReportTypeId
                        Case 1
                            '"Programación de ingresos"
                            resultStringBuilder = Await model.GenerateArchiveReportIncomeProgramming(_criterias)
                            fileName = "ARCHIVO PLANO PROGRAMACIÓN DE INGRESOS"
                        Case 2
                            '"Ejecución de ingresos"
                            'ds = Await model.GetListReport...(_criterias)
                            'nameTable = ""
                        Case 3
                            '"Programación de gastos - departamentos, municipios, San Andrés y Providencia y Bogotá D.C"
                            'ds = Await model.GetListReport...(_criterias)
                            'nameTable = ""
                        Case 4
                            '"Programación de gastos para establecimientos públicos territoriales"
                            resultStringBuilder = Await model.GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments(_criterias)
                            fileName = "ARCHIVO PLANO PROGRAMACIÓN DE GASTOS PARA ESTABLECIMIENTOS PÚBLICOS TERRITORIALES"
                        Case 5
                            '"Ejecución de gastos - departamentos, municipios, San Andrés y Providencia y Bogotá D.C"
                            'ds = Await model.GetListReport...(_criterias)
                            'nameTable = ""
                        Case 6
                            '"Ejecución de gastos para establecimientos públicos territoriales"
                            resultStringBuilder = Await model.GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntitiesAsync(_criterias)
                            fileName = "ARCHIVO PLANO EJECUCIÓN DE GASTOS PARA ESTABLECIMIENTOS PÚBLICOS TERRITORIALES"
                    End Select
                    DialogGenerateFile(resultStringBuilder, fileName)
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Metodo para confirmar si se desea guardar el archivo
    ''' </summary>
    ''' <param name="content"></param>
    ''' <param name="fileName"></param>
    Public Sub DialogGenerateFile(content As StringBuilder, fileName As String)
        Dim save As SaveFileDialog = New SaveFileDialog()
        save.Filter = "Texto|*.txt"
        save.Title = fileName
        save.FileName = fileName & ".txt"
        If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim file = save.OpenFile()
            Dim streamWrite As New StreamWriter(file)
            streamWrite.Write(content)
            streamWrite.Flush()
            streamWrite.Close()
            If MessageIndigo.Show("¿Desea abrir el archivo?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Process.Start(save.FileName)
            End If
        End If
    End Sub
    ''' <summary>
    ''' metodo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeReport_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDGleTypeReport.EditValueChanging
        If e.NewValue Is Nothing OrElse
        ((e.OldValue >= 1 AndAlso e.OldValue <= 2) AndAlso (e.NewValue >= 3 AndAlso e.NewValue <= 6)) OrElse
        ((e.OldValue >= 3 AndAlso e.OldValue <= 6) AndAlso (e.NewValue >= 1 AndAlso e.NewValue <= 2)) Then
        End If
    End Sub

#End Region

#End Region

End Class
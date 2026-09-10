#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
#End Region

Public Class FrmReportSubaccount
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
#End Region

#Region "Properties"
    Public Property ProoftCloseXpoSubaccount As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoResponsible As XPInstantFeedbackSource
    Public Property ProoftCloseXpoItem As XPInstantFeedbackSource
    Public Property bookXpcollection As XPCollection
    Public Property ProoftCloseXpoSubaccount2 As DataSet

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida Subcuenta
        'If INDSleSubAccountStart2.EditValue Is Nothing OrElse INDSleSubAccountStart2.EditValue = String.Empty And INDSleSubAccountEnd2.EditValue IsNot Nothing OrElse INDSleSubAccountEnd2.EditValue <> String.Empty Or INDSleSubAccountEnd2.EditValue Is Nothing OrElse INDSleSubAccountEnd2.EditValue = String.Empty And INDSleSubAccountStart2.EditValue IsNot Nothing OrElse INDSleSubAccountStart2.EditValue <> String.Empty Then
        If ((INDSleSubAccountStart2.EditValue Is Nothing OrElse INDSleSubAccountStart2.EditValue = String.Empty) AndAlso (INDSleSubAccountEnd2.EditValue IsNot Nothing OrElse INDSleSubAccountEnd2.EditValue <> String.Empty)) Then
            'OrElse ((INDSleSubAccountEnd2.EditValue Is Nothing OrElse INDSleSubAccountEnd2.EditValue = String.Empty) AndAlso (INDSleSubAccountStart2.EditValue IsNot Nothing OrElse INDSleSubAccountStart2.EditValue <> String.Empty)) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblSubaccount.Text)
            Me.INDSleSubAccountStart2.Focus()
            Validations = False
        ElseIf INDSleSubAccountEnd2.EditValue < INDSleSubAccountStart2.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblSubaccount.Text)
            Me.INDSleSubAccountStart2.Focus()
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

        'Valida Artículo
        If INDSleItemStart.EditValue Is Nothing And INDSleItemEnd.EditValue IsNot Nothing Or INDSleItemEnd.EditValue Is Nothing And INDSleItemStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblItem.Text)
            Me.INDSleItemStart.Focus()
            Validations = False
        ElseIf INDSleItemEnd.EditValue < INDSleItemStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblItem.Text)
            Me.INDSleItemStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

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
    ''' Propiedad que se usa para cargar la Dupla del tipo de reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingTypeReport
        End Get
    End Property
#End Region

#Region "Eventos"
    ''' <summary>
    ''' cargamos el datasource de la Subcuenta Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSubAccountStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSubAccountStart.QueryPopUp
        If INDSleSubAccountStart.Datasource Is Nothing Then
            LoadXpoSubaccountStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de la Subcuenta Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSubAccountEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSubAccountEnd.QueryPopUp
        If INDSleSubAccountEnd.Datasource Is Nothing Then
            LoadXpoSubaccountEnd()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Tipo de Equipos Inicial
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
    ''' cargamos el datasource de Tipo de Equipos Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource del Artículo Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemStart.QueryPopUp
        If INDSleItemStart.Datasource Is Nothing Then
            LoadXpoItemStart()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource del Artículo Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemEnd.QueryPopUp
        If INDSleItemEnd.Datasource Is Nothing Then
            LoadXpoItemEnd()
        End If
    End Sub
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoSubaccount = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoResponsible = Nothing
        ProoftCloseXpoItem = Nothing
        bookXpcollection = Nothing
        ProoftCloseXpoSubaccount2 = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        Dim reporte As Object
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            If INDGleTypeReport.EditValue = 1 Then
                reporte = New rptSubaccountFixedAsset
            Else
                reporte = New rptSubaccountFixedAssetSummarized
            End If
            reporte.ParametrosReporte = New Object() {INDSleSubAccountStart2.EditValue,
                                                      INDSleSubAccountEnd2.EditValue,
                                                      INDSleItemStart.EditValue,
                                                      INDSleItemEnd.EditValue,
                                                      INDSleGroupStart.EditValue,
                                                      INDSleGroupEnd.EditValue,
                                                      INDsleBook.EditValue}

            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSleSubAccountStart.Focus()
            End If
            AsyncLoader(False)
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
        Me.INDSleSubAccountStart.Focus()
    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportSubaccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        'Me.INDSleSubAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleSubAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        'Me.INDSleGroupEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode

        INDSleSubAccountStart.View.OptionsView.ShowGroupPanel = False
        INDSleSubAccountEnd.View.OptionsView.ShowGroupPanel = False
        INDSleGroupStart.View.OptionsView.ShowGroupPanel = False
        INDSleGroupEnd.View.OptionsView.ShowGroupPanel = False
        LoadXpoBook()
        SetOfficialBook()
        LoadXpoSubaccountStart2()
        LoadXpoSubaccountEnd2()
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetOfficialBook()
        If bookXpcollection IsNot Nothing AndAlso bookXpcollection.Count > 0 Then
            Dim item = (From l In bookXpcollection Where l.OfficialBook = True Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleBook.EditValue = item.Id
            End If
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBook()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            bookXpcollection = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            INDsleBook.Properties.DataSource = bookXpcollection
        End Using
    End Sub


    ''' <summary>
    ''' Variable par obtener la tabla de MainAccountsFixedAssetByStatusAndBookId
    ''' </summary>
    Dim dtReportMainAccountsFixedAsset As DataTable

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSubaccountStart2
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSubaccountStart2()
        ProoftCloseXpoSubaccount2 = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReporMainAccountsFixedAssetByStatusAndBookId(True, INDsleBook.EditValue, Me.IndigoSessionValues)

        If ProoftCloseXpoSubaccount2.Tables(0).Rows.Count > 0 Then
            dtReportMainAccountsFixedAsset = ProoftCloseXpoSubaccount2.Tables("ReportFixedAssetByStatusAndBookId")
            INDSleSubAccountStart2.Properties.DataSource = dtReportMainAccountsFixedAsset
        Else
            INDSleSubAccountStart2.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSubaccountEnd2
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSubaccountEnd2()
        ProoftCloseXpoSubaccount2 = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReporMainAccountsFixedAssetByStatusAndBookId(True, INDsleBook.EditValue, Me.IndigoSessionValues)

        If ProoftCloseXpoSubaccount2.Tables(0).Rows.Count > 0 Then
            dtReportMainAccountsFixedAsset = ProoftCloseXpoSubaccount2.Tables("ReportFixedAssetByStatusAndBookId")
            INDSleSubAccountEnd2.Properties.DataSource = dtReportMainAccountsFixedAsset
        Else
            INDSleSubAccountEnd2.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSubaccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSubaccountStart()
        'If ProoftCloseXpoSubaccount2 IsNot Nothing Then
        '    If ProoftCloseXpoSubaccount2.Tables.Contains("ReportFixedAssetByStatusAndBookId") = True Then
        '        ProoftCloseXpoSubaccount2.Tables(0).Rows.Clear()
        '    End If
        'End If

        ProoftCloseXpoSubaccount2 = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReporMainAccountsFixedAssetByStatusAndBookId(True, INDsleBook.EditValue, Me.IndigoSessionValues)

        If ProoftCloseXpoSubaccount2.Tables(0).Rows.Count > 0 Then
            dtReportMainAccountsFixedAsset = ProoftCloseXpoSubaccount2.Tables("ReportFixedAssetByStatusAndBookId")
            INDSleSubAccountStart.Datasource = dtReportMainAccountsFixedAsset
        Else
            INDSleSubAccountStart.Datasource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSubaccountEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSubaccountEnd()
            ProoftCloseXpoSubaccount2 = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReporMainAccountsFixedAssetByStatusAndBookId(True, INDsleBook.EditValue, Me.IndigoSessionValues)
            If ProoftCloseXpoSubaccount2.Tables(0).Rows.Count > 0 Then
                dtReportMainAccountsFixedAsset = ProoftCloseXpoSubaccount2.Tables("ReportFixedAssetByStatusAndBookId")
                INDSleSubAccountEnd.Datasource = dtReportMainAccountsFixedAsset
            Else
                INDSleSubAccountEnd.Datasource = Nothing
            End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleItemStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoItemStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoItem = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            INDSleItemStart.Datasource = ProoftCloseXpoItem
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleItemEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoItemEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoItem = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            INDSleItemEnd.Datasource = ProoftCloseXpoItem
        End Using
    End Sub

    Private Sub FrmReportSubaccount_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
    End Sub

    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        If INDsleBook.EditValue IsNot Nothing Then
            LoadXpoSubaccountStart2()
            LoadXpoSubaccountEnd2()
        End If
    End Sub

#Region "Excel"

    Private Async Sub INDSbExportReport_Click(sender As Object, e As EventArgs) Handles INDSbExportReport.Click
        If ValidateControlsReports() = True Then
            Try
                AsyncLoader(True)
                Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportSubAccountAsync(INDSleSubAccountStart2.EditValue,
                                                      INDSleSubAccountEnd2.EditValue,
                                                      INDSleItemStart.EditValue,
                                                      INDSleItemEnd.EditValue,
                                                      INDSleGroupStart.EditValue,
                                                      INDSleGroupEnd.EditValue,
                                                      INDsleBook.EditValue,
                                                      IndigoSessionValues)
                If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dtReportSubAccount As DataTable = ds.Tables("ReportSubAccount")

                    Await Task.Factory.StartNew(Sub()
                                                    If INDGleTypeReport.EditValue = 1 Then
                                                        chargueDataSourceDetailed(dtReportSubAccount)
                                                    ElseIf INDGleTypeReport.EditValue = 2 Then
                                                        chargueDatasourceSummary(dtReportSubAccount)
                                                    End If
                                                End Sub)

                    If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                        generateExcel()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDataSourceDetailed(dtReportSubAccount As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Cuenta Contable")
        dt.Columns.Add("Placa")
        dt.Columns.Add("Artículo")
        dt.Columns.Add("Tipo de Equipo")
        dt.Columns.Add("Valor Histórico", GetType(Decimal))
        dt.Columns.Add("Valor Valorización", GetType(Decimal))
        dt.Columns.Add("Valor Devaluación", GetType(Decimal))
        dt.Columns.Add("Cuenta Depreciación")
        dt.Columns.Add("Depreciación Acumulada", GetType(Decimal))
        dt.Columns.Add("Saldo en Libros", GetType(Decimal))

        For Each item In dtReportSubAccount.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Cuenta Contable") = String.Format("{0} - {1}", item("NumberAccount"), item("NameAccount"))
            row.Item("Placa") = item("Plate")
            row.Item("Artículo") = String.Format("{0} - {1}", item("Code"), item("Description"))
            row.Item("Tipo de Equipo") = String.Format("{0} - {1}", item("CodeType"), item("NameType"))
            row.Item("Valor Histórico") = item("HistoricalValue")
            row.Item("Valor Valorización") = item("Valorization")
            row.Item("Valor Devaluación") = item("Devaluation")
            row.Item("Cuenta Depreciación") = String.Format("{0} - {1}", item("DeprecationAccount"), item("DeprecationAccountName"))
            row.Item("Depreciación Acumulada") = item("DepreciatedValue")
            row.Item("Saldo en Libros") = item("ResidualValue")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub


    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceSummary(dtReportSubAccount As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Cuenta Contable")
        dt.Columns.Add("Valor Histórico", GetType(Decimal))
        dt.Columns.Add("Valor Valorización", GetType(Decimal))
        dt.Columns.Add("Valor Devaluación", GetType(Decimal))
        dt.Columns.Add("Depreciación Acumulada", GetType(Decimal))
        dt.Columns.Add("Saldo en Libros", GetType(Decimal))

        Dim groupedResult As IEnumerable(Of Object) = From t In dtReportSubAccount.AsEnumerable()
                                                      Group t By Key = New With
                                                      {
                                                          Key .NumberAccount = t.Field(Of String)("NumberAccount"),
                                                          Key .NameAccount = t.Field(Of String)("NameAccount")
                                                      } Into Group
                                                      Select New With
                                                      {
                                                          .NumberAccount = Key.NumberAccount,
                                                          .NameAccount = Key.NameAccount
                                                      }

        For Each item In groupedResult
            Dim filterResult As List(Of DataRow) = dtReportSubAccount.AsEnumerable().Where(Function(t) t.Field(Of String)("NumberAccount") = item.NumberAccount AndAlso t.Field(Of String)("NameAccount") = item.NameAccount).ToList()

            Dim row As DataRow = dt.NewRow()
            row.Item("Cuenta Contable") = String.Format("{0} - {1}", item.NumberAccount, item.NameAccount)
            row.Item("Valor Histórico") = filterResult.Sum(Function(d) d.Field(Of Decimal)("HistoricalValue"))
            row.Item("Valor Valorización") = filterResult.Sum(Function(d) d.Field(Of Decimal)("Valorization"))
            row.Item("Valor Devaluación") = filterResult.Sum(Function(d) d.Field(Of Decimal)("Devaluation"))
            row.Item("Depreciación Acumulada") = filterResult.Sum(Function(d) d.Field(Of Decimal)("DepreciatedValue"))
            row.Item("Saldo en Libros") = filterResult.Sum(Function(d) d.Field(Of Decimal)("ResidualValue"))
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

#End Region

#End Region
End Class
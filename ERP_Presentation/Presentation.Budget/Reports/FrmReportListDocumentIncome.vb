#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportListDocumentIncome

#Region "Variables"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
    ''' <summary>
    ''' Lista los tipos de docuementos por medio de tuplas
    ''' </summary>
    Private _LoadTypeDocument As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeDocument As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeDocument Is Nothing Then
                _LoadTypeDocument = New List(Of Tuple(Of Integer, String))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(1, "Reconocimiento"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(2, "Reconocimiento Modificación"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(3, "Recaudo"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(4, "Recaudo Modificación"))
            End If
            Return _LoadTypeDocument
        End Get
    End Property
    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Lista el tipo de reporte por tupla
    ''' </summary>
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

    Dim _yearValidity As Integer

    Private criterias As Dictionary(Of String, String)

#End Region

#Region "XPO"

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

#End Region
    ''' <summary>
    ''' Carga la barra de permisos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se inicializan los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        'Me.INDSleCategoryStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCategoryBudgetByCode
        'Me.INDSleCategoryEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCategoryBudgetByCode

        'Cargar GridLookUpEdit
        Me.INDGleTypeDocument.Properties.DataSource = LoadTypeDocument
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeDocument.EditValue = 1
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleGroupBy.EditValue = 0
        Me.INDGleGroupBy.Properties.NullText = "Sin Agrupar"
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
        _yearValidity = Nothing

        _selector = Nothing
    End Sub

#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' por pup que lista el tipo de agrupamiento por medio de tupla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleGroupBy_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleGroupBy.QueryPopUp
        If INDGleGroupBy.Properties.DataSource Is Nothing Then
            Dim _list As New List(Of Tuple(Of Byte, String))
            _list.Add(New Tuple(Of Byte, String)(0, "Sin Agrupar"))
            _list.Add(New Tuple(Of Byte, String)(1, "Tercero"))
            _list.Add(New Tuple(Of Byte, String)(2, "Rubro"))
            _list.Add(New Tuple(Of Byte, String)(3, "Recurso"))
            _list.Add(New Tuple(Of Byte, String)(4, "Tipo"))
            INDGleGroupBy.Properties.DataSource = _list
        End If
    End Sub

    ''' <summary>
    ''' pop pup que lista el id del tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThird_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyId.QueryPopUp
        If INDSleThirdPartyId.Properties.DataSource Is Nothing Then
            INDSleThirdPartyId.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListThird
        End If
    End Sub

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

    ''' <summary>
    ''' carga el datasource de reconocimientos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleRecognition_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRecognition.QueryPopUp
        If INDSleRecognition.Datasource Is Nothing Then
            INDSleRecognition.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListRecognition(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de reconocimiento modificaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleRecognitionModification_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRecognitionModification.QueryPopUp
        If INDSleRecognitionModification.Datasource Is Nothing Then
            INDSleRecognitionModification.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListRecognitionModificationByValidityId(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de recaudos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCollection_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCollection.QueryPopUp
        If INDSleCollection.Datasource Is Nothing Then
            INDSleCollection.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCollection(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de recaudo modificaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCollectionModification_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCollectionModification.QueryPopUp
        If INDSleCollectionModification.Datasource Is Nothing Then
            INDSleCollectionModification.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCollectionModification(INDsleValidity.EditValue)
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue IsNot Nothing And INDGleTypeReport.EditValue = 1 Then
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleGroupBy.EditValue = Nothing
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
        End If
    End Sub

    ''' <summary>
    ''' al cambiar el tipo de documento se muestra el filtro del documento seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeDocument_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeDocument.EditValueChanged
        If INDGleTypeDocument.EditValue = 1 Then
            INDLciRecognition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCollection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCollectionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRecognitionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleCollection.EditValue = Nothing
            INDSleCollectionModification.EditValue = Nothing
            INDSleRecognitionModification.EditValue = Nothing
        ElseIf INDGleTypeDocument.EditValue = 2 Then
            INDLciRecognition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCollection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCollectionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRecognitionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSleRecognition.EditValue = Nothing
            INDSleCollection.EditValue = Nothing
            INDSleCollectionModification.EditValue = Nothing
        ElseIf INDGleTypeDocument.EditValue = 3 Then
            INDLciRecognition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCollection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCollectionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRecognitionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleRecognition.EditValue = Nothing
            INDSleCollectionModification.EditValue = Nothing
            INDSleRecognitionModification.EditValue = Nothing
        ElseIf INDGleTypeDocument.EditValue = 4 Then
            INDLciRecognition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCollection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCollectionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciRecognitionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleRecognition.EditValue = Nothing
            INDSleCollection.EditValue = Nothing
            INDSleRecognitionModification.EditValue = Nothing
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

                Dim reporte As Object
                Select Case INDGleTypeReport.EditValue
                    Case 1
                        Select Case INDGleTypeDocument.EditValue
                            Case 1
                                reporte = New rptListRecognition
                            Case 2
                                reporte = New rptListRecognitionModification
                            Case 3
                                reporte = New rptListCollections
                            Case 4
                                reporte = New rptListCollectionModification
                            Case Else
                                AsyncLoader(False)
                                Exit Sub
                        End Select
                    Case 2
                        Select Case INDGleTypeDocument.EditValue
                            Case 1
                                reporte = New rptSubRecognition
                            Case 2
                                reporte = New rptSubRecognitionModification
                            Case 3
                                reporte = New rptSubCollections
                            Case 4
                                reporte = New rptSubCollectionModification
                            Case Else
                                AsyncLoader(False)
                                Exit Sub
                        End Select
                    Case Else
                        Exit Sub
                End Select

                reporte.ParametrosReporte = New Object() {criterias}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDteDateStart.Focus()
                End If
            Catch ex As Exception
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
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDteDateStart.Focus()
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Budget.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportListDocumentIncome(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportListDocumentIncome As DataTable = ds.Tables("ReportListDocumentIncome")

                        Await Task.Factory.StartNew(Sub()
                                                        Select Case INDGleTypeDocument.EditValue
                                                            Case 1
                                                                chargueDatasourceRecognition(dtReportListDocumentIncome)
                                                            Case 2
                                                                chargueDatasourceRecognitionModification(dtReportListDocumentIncome)
                                                            Case 3
                                                                chargueDatasourceCollection(dtReportListDocumentIncome)
                                                            Case 4
                                                                chargueDatasourceCollectionModification(dtReportListDocumentIncome)
                                                            Case Else
                                                                Exit Sub
                                                        End Select
                                                    End Sub)

                        If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

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
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
                _yearValidity = item.Year
                CleanControlsSelectValidity()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControlsSelectValidity()
        INDDteDateStart.Enabled = True
        INDDteDateStart.EditValue = Nothing
        INDDteDateEnd.Enabled = True
        INDDteDateEnd.EditValue = Nothing
        INDGleTypeReport.Enabled = True
        INDGleTypeDocument.Enabled = True
        INDSleRecognition.Enabled = True
        INDSleRecognition.EditValue = Nothing
        INDSleRecognitionModification.Enabled = True
        INDSleRecognitionModification.EditValue = Nothing
        INDSleCollection.Enabled = True
        INDSleCollection.EditValue = Nothing
        INDSleCollectionModification.Enabled = True
        INDSleCollectionModification.EditValue = Nothing
        INDSbGenerateReport.Enabled = True
        INDSbGenerateExcell.Enabled = True
        INDGleGroupBy.Enabled = True
        INDGleGroupBy.EditValue = 0
        INDGleGroupBy.Properties.NullText = "Sin Agrupar"
        INDSleThirdPartyId.Enabled = True
        INDSleThirdPartyId.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDDteDateStart.EditValue Is Nothing Or INDDteDateEnd.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons")))
            Me.INDDteDateStart.Focus()
        ElseIf Me.INDDteDateStart.EditValue > INDDteDateEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting")))
            Me.INDDteDateStart.Focus()
        End If

        If INDGleTypeReport.EditValue = 1 AndAlso INDGleGroupBy.EditValue Is Nothing Then
            errors.AppendLine("Seleccione Agrupar Por")
            Me.INDGleGroupBy.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("TypeDocument", INDGleTypeDocument.EditValue)
        criterias.Add("TypeReport", INDGleTypeReport.EditValue)
        criterias.Add("BudgetValidityId", INDsleValidity.EditValue)
        criterias.Add("DateStart", INDDteDateStart.EditValue)
        criterias.Add("DateEnd", INDDteDateEnd.EditValue)
        criterias.Add("GroupBy", INDGleGroupBy.EditValue)
        criterias.Add("RecognitionCode", INDSleRecognition.EditValue)
        criterias.Add("RecognitionModificationCode", INDSleRecognitionModification.EditValue)
        criterias.Add("CollectionCode", INDSleCollection.EditValue)
        criterias.Add("CollectionModificationCode", INDSleCollectionModification.EditValue)
        criterias.Add("ThirdParties", _selector.GetKeys())

        Return True
    End Function

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel de reconocimientos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceRecognition(ByVal dtReportListDocumentIncome As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Tipo Reconocimiento")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))
        dt.Columns.Add("Ejecutado", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))

        For Each item In dtReportListDocumentIncome.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Tipo Reconocimiento") = item("RecognitionTypeName")
            row.Item("Documento") = item("Document")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Valor Inicial") = item("InitialValue")
            row.Item("Valor Débito") = item("DebitValue")
            row.Item("Valor Crédito") = item("CreditValue")
            row.Item("Total") = item("TotalValue")
            row.Item("Ejecutado") = item("ExecutedValue")
            row.Item("Saldo") = item("Balance")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de modificacion de reconocimientos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceRecognitionModification(ByVal dtReportListDocumentIncome As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Reconocimiento")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Valor", GetType(Decimal))
        dt.Columns.Add("Reconocimiento Total", GetType(Decimal))
        dt.Columns.Add("Reconocimiento Saldo", GetType(Decimal))
        dt.Columns.Add("Presupuesto Ejecutado", GetType(Decimal))
        dt.Columns.Add("Presupuesto Saldo", GetType(Decimal))

        For Each item In dtReportListDocumentIncome.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Reconocimiento") = item("RecognitionCode")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Documento") = item("Document")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Naturaleza") = item("NatureName")
            row.Item("Valor") = item("Value")
            row.Item("Reconocimiento Total") = item("RecognitionTotal")
            row.Item("Reconocimiento Saldo") = item("RecognitionBalance")
            row.Item("Presupuesto Ejecutado") = item("BudgetExecutedValue")
            row.Item("Presupuesto Saldo") = item("BudgetBalance")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de recaudos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceCollection(ByVal dtReportListDocumentIncome As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))

        For Each item In dtReportListDocumentIncome.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Valor Inicial") = item("InitialValue")
            row.Item("Valor Débito") = item("DebitValue")
            row.Item("Valor Crédito") = item("CreditValue")
            row.Item("Total") = item("TotalValue")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel modificacion de recaudos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceCollectionModification(ByVal dtReportListDocumentIncome As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Recaudo")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Valor", GetType(Decimal))
        dt.Columns.Add("Recaudo Total", GetType(Decimal))
        dt.Columns.Add("Reconocimiento Ejecutado", GetType(Decimal))
        dt.Columns.Add("Reconocimiento Saldo", GetType(Decimal))

        For Each item In dtReportListDocumentIncome.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Recaudo") = item("CollectionCode")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Documento") = item("Document")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Naturaleza") = item("NatureName")
            row.Item("Valor") = item("Value")
            row.Item("Recaudo Total") = item("CollectionTotal")
            row.Item("Reconocimiento Ejecutado") = item("RecognitionExecutedValue")
            row.Item("Reconocimiento Saldo") = item("RecognitionBalance")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

    Private _selector As SelectorCache = New SelectorCache("Id", "Nit")
    ''' <summary>
    ''' Evento al cutomizar la celda de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvThirdParty_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvThirdParty.CustomUnboundColumnData
        If e.IsGetData Then
            e.Value = _selector.ValidateExistsRow(e.Row)
        End If
    End Sub
    ''' <summary>
    ''' Evento cuando se celcciona la celda de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvThirdParty_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvThirdParty.RowCellClick
        If e.Column.FieldName = "UnboundSelection" Then
            If e.RowHandle >= 0 Then
                Dim row = INDGvThirdParty.GetRow(e.RowHandle)
                _selector.SetValue(row)
            Else
                _selector.Clear()
            End If
            INDGvThirdParty.RefreshData()
        End If
    End Sub
    ''' <summary>
    ''' Evento al cerrar terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdPartyId_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleThirdPartyId.Closed
        INDSleThirdPartyId.EditValue = Nothing
        INDSleThirdPartyId.Properties.NullText = _selector.ToString()
    End Sub

#End Region

#End Region

End Class
#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
'Imports Infrastructure.Data.Xpo.TaxesRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Common.MVP
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Domain.Entities
Imports Domain.Common.Entities
Imports Infrastructure.Data.Xpo.TaxesRepository
Imports Presentation.Taxes.MVP

#End Region

Public Class FrmMassivePersuasivePayment

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    Private _modelTaxes As MMassivePersuasivePayment
#End Region

    '#Region "Properties"

    Public Property ProoftCloseXpoAccounts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Private _FillingType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingType Is Nothing Then
                _FillingType = New List(Of Tuple(Of Integer, String))
                _FillingType.Add(New Tuple(Of Integer, String)(3, "Industria y Comercio"))
                _FillingType.Add(New Tuple(Of Integer, String)(8, "Impuesto Predial"))
            End If
            Return _FillingType
        End Get
    End Property
    Private criteria As String = Nothing

    '#End Region

    '    ''' <summary>
    '    ''' propiedad para registar el mensaje en el visor
    '    ''' </summary>
    '    ''' <param name="Icono"></param>
    '    ''' <value></value>
    '    ''' <remarks></remarks>
    '    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
    '        Set(value As String)

    '            If Icono = EeventViewerImages.Advertencia Then
    '                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
    '            ElseIf Icono = EeventViewerImages.Informacion Then
    '                MessageIndigo.Show(value, MessageType.Information, Me.Text)
    '            ElseIf Icono = EeventViewerImages.MensajeError Then
    '                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
    '            End If
    '        End Set
    '    End Property


    '    ''' <summary>
    '    ''' metodo para Cargar el data source Del Control INDSleAccountEnd
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub LoadXpoAccountsEnd()
    '        criteria = Nothing
    '        If (INDGleType.EditValue = 1) Then
    '            criteria = "RetencionType = 3"
    '        Else
    '            criteria &= "RetencionType = 2"
    '        End If

    '        Using msearch As New MBusqueda
    '            ProoftCloseXpoAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsReportByFilter, criteria)
    '            INDSleAccountsEnd.Datasource = ProoftCloseXpoAccounts
    '        End Using
    '    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            'ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyStart.Properties.DataSource = msearch.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            'ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyEnd.Properties.DataSource = msearch.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    '    ''' <summary>
    '    ''' propiedad para Realizar las validaciones del formulario
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Function ValidateControlsReports()
    '        Dim Validations As Boolean = True

    '        'Valida Año y Mes
    '        If INDCdNavigatorStart.GetYear > INDCdNavigatorEnd.GetYear Then
    '            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPeriod.Text)
    '            Me.INDCdNavigatorStart.Focus()
    '            Validations = False
    '        ElseIf INDCdNavigatorStart.GetYear = INDCdNavigatorEnd.GetYear Then
    '            If INDCdNavigatorStart.GetMonth > INDCdNavigatorEnd.GetMonth Then
    '                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPeriod.Text)
    '                Me.INDCdNavigatorStart.Focus()
    '                Validations = False
    '            End If
    '        End If

    '        'Valida Cuentas
    '        If INDSleAccountsStart.EditValue Is Nothing And INDSleAccountsEnd.EditValue IsNot Nothing Or INDSleAccountsEnd.EditValue Is Nothing And INDSleAccountsStart.EditValue IsNot Nothing Then
    '            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccounts.Text)
    '            Me.INDSleAccountsStart.Focus()
    '            Validations = False
    '        ElseIf INDSleAccountsEnd.EditValue < INDSleAccountsStart.EditValue Then
    '            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccounts.Text)
    '            Me.INDSleAccountsStart.Focus()
    '            Validations = False
    '        End If

    '        'Valida Terceros
    '        If INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Or INDSleThirdPartyEnd.EditValue Is Nothing And INDSleThirdPartyStart.EditValue IsNot Nothing Then
    '            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
    '            Me.INDSleThirdPartyStart.Focus()
    '            Validations = False
    '        ElseIf INDSleThirdPartyEnd.EditValue < INDSleThirdPartyStart.EditValue Then
    '            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
    '            Me.INDSleThirdPartyStart.Focus()
    '            Validations = False
    '        End If

    '        Return Validations
    '    End Function

    '    ''' <summary>
    '    ''' se ejecuta en el evento ClickBack del control INDCtnReturn
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub INDCtnReturn_ClickBack() Handles INDCtnReturn.ClickBack
    '        Me.INDPcReportViewer.Visible = False
    '        Me.INDLcBase.Visible = True
    '        Me.INDCncNavigation.Visible = True
    '    End Sub

    ''' <summary>
    ''' Se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCertificateReteICA_IVA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me._modelTaxes = New MMassivePersuasivePayment(Me.Tag)
    End Sub

    '    ''' <summary>
    '    ''' se ejecuta en el evento querypopup del control INDSleAccountsStart
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub INDSleAccountsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountsStart.QueryPopUp
    '        If INDSleAccountsStart.Datasource Is Nothing Then
    '            LoadXpoAccountsStart()
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' se ejecuta en el evento querypopup del control INDSleAccountsEnd
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub INDSleAccountsEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountsEnd.QueryPopUp
    '        If INDSleAccountsEnd.Datasource Is Nothing Then
    '            LoadXpoAccountsEnd()
    '        End If
    '    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Properties.DataSource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Properties.DataSource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        _modelTaxes = Nothing
    End Sub

    ''' <summary>
    ''' Se ejecuta al cargar por primer vez en formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCertificateReteICA_IVA_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleType.Properties.DataSource = FillingType

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleType.EditValue = 3
    End Sub

    '    ''' <summary>
    '    ''' Se ejecuta en el evento Changed del control INDGleType
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub INDGleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleType.EditValueChanged
    '        LoadXpoAccountsStart()
    '        LoadXpoAccountsEnd()
    '    End Sub

    '    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
    '        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    '    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        Presentation.Base.BaseClass.ChangeCursorIndigo()
        INDSbGenerateReport.Enabled = False
        Try
            Dim ruta = My.Application.Info.DirectoryPath
            Dim texto As String = ""
            Dim dtDatos As New System.Data.DataTable
            dtDatos.Columns.Add("NitTercero", GetType(String))
            dtDatos.Columns.Add("NombreTercero", GetType(String))
            dtDatos.Columns.Add("Saldo", GetType(String))
            dtDatos.Columns.Add("Factura", GetType(String))
            dtDatos.Columns.Add("TipoImpuesto", GetType(String))
            dtDatos.Columns.Add("Valor", GetType(String))
            dtDatos.Columns.Add("Vigencia", GetType(String))


            Dim filtros As String = ""
            If INDGleType.EditValue IsNot Nothing Then
                filtros = " Tipo = " & INDGleType.EditValue
            End If

            If INDSleThirdPartyStart.EditValue IsNot Nothing AndAlso INDSleThirdPartyEnd.EditValue IsNot Nothing Then
                If filtros Is String.Empty Then
                    filtros += " (NitTercero > '" & INDSleThirdPartyStart.EditValue & "' AND NitTercero < '" & INDSleThirdPartyEnd.EditValue & "')"
                Else
                    filtros += " AND (NitTercero > '" & INDSleThirdPartyStart.EditValue & "' AND NitTercero < '" & INDSleThirdPartyEnd.EditValue & "')"
                End If
            End If

            If INDteInitialValue.EditValue <> 0 AndAlso INDteFinalValue.EditValue <> 0 Then
                If filtros Is String.Empty Then
                    filtros += " (Saldo >= " & INDteInitialValue.EditValue & " AND Saldo <= " & INDteFinalValue.EditValue & ")"
                Else
                    filtros += " AND (Saldo >= " & INDteInitialValue.EditValue & " AND Saldo <= " & INDteFinalValue.EditValue & ")"
                End If
            End If



            'Dim listado = Me._modelTaxes.ListMassivePersuasivePayment(filtros)
            'For Each Item As Infrastructure.Data.Xpo.TaxesRepository.TaxesViewMassivePersuasivePaymentXpo In listado.ToList
            '    INDRichPivote.CreateNewDocument()
            '    INDRichPivote.Options.MailMerge.DataSource = Nothing
            '    INDRichPivote.Options.MailMerge.ViewMergedData = False
            '    dtDatos.Clear()

            '    Dim newRow As System.Data.DataRow = dtDatos.NewRow
            '    newRow("NitTercero") = Item.NitTercero
            '    newRow("NombreTercero") = Item.NombreTercero
            '    newRow("Saldo") = Item.Saldo
            '    newRow("TipoImpuesto") = Item.TipoImpuesto
            '    newRow("Factura") = Item.Factura
            '    newRow("Valor") = Item.Valor
            '    newRow("Vigencia") = Item.Vigencia
            '    dtDatos.Rows.Add(newRow)

            '    INDRichPivote.LoadDocument(ruta & "\N00.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)

            '    INDRichPivote.Options.MailMerge.DataSource = dtDatos
            '    INDRichPivote.Options.MailMerge.ViewMergedData = True


            '    'INDRichPivote.Document.InsertDocumentContent()

            '    'texto += INDRichPivote.
            'Next

            'INDrecPlantilla.HtmlText = texto







            Dim listado = Me._modelTaxes.ListMassivePersuasivePayment(filtros)

            INDRichPivote.CreateNewDocument()
            INDRichPivote.LoadDocument(ruta & "\N00.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
            INDRichPivote.Options.MailMerge.DataSource = listado.ToList
            INDRichPivote.Options.MailMerge.ViewMergedData = True


            Dim myMergeOptions As DevExpress.XtraRichEdit.API.Native.MailMergeOptions = INDRichPivote.Document.CreateMailMergeOptions()
            myMergeOptions.FirstRecordIndex = 1
            myMergeOptions.LastRecordIndex = listado.ToList.Count
            myMergeOptions.MergeMode = DevExpress.XtraRichEdit.Model.MergeMode.NewSection
            INDRichPivote.Document.MailMerge(INDrecPlantilla.Document)

        Catch ex As Exception
            Presentation.Base.BaseClass.ChageCursorDefault()
            INDSbGenerateReport.Enabled = True
        End Try
        Presentation.Base.BaseClass.ChageCursorDefault()
        INDSbGenerateReport.Enabled = True
        


    End Sub

    Dim listDatosPruebas As List(Of DatosPruebas) = New List(Of DatosPruebas)()

    Private Sub FrmMassivePersuasivePayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listDatosPruebas.Add(New DatosPruebas() With {.NitTercero = "123456789", .NombreTercero = "Kevin Garay", .Saldo = "$90.200", .TipoImpuesto = "ICA", .Valor = "$50.000", .Vigencia = "2016"})
        listDatosPruebas.Add(New DatosPruebas() With {.NitTercero = "124578", .NombreTercero = "asdas das das", .Saldo = "$90.200", .TipoImpuesto = "ICA", .Valor = "$50.000", .Vigencia = "2016"})
        listDatosPruebas.Add(New DatosPruebas() With {.NitTercero = "875456321", .NombreTercero = "dasd as das", .Saldo = "$90.200", .TipoImpuesto = "ICA", .Valor = "$50.000", .Vigencia = "2016"})
        listDatosPruebas.Add(New DatosPruebas() With {.NitTercero = "+8935554", .NombreTercero = "aksldkasd", .Saldo = "$90.200", .TipoImpuesto = "ICA", .Valor = "$50.000", .Vigencia = "2016"})
        listDatosPruebas.Add(New DatosPruebas() With {.NitTercero = "9887564", .NombreTercero = "asdlaskd asdl asj d", .Saldo = "$90.200", .TipoImpuesto = "ICA", .Valor = "$50.000", .Vigencia = "2016"})
        listDatosPruebas.Add(New DatosPruebas() With {.NitTercero = "4564231", .NombreTercero = "askdla sd jasd", .Saldo = "$90.200", .TipoImpuesto = "ICA", .Valor = "$50.000", .Vigencia = "2016"})
        listDatosPruebas.Add(New DatosPruebas() With {.NitTercero = "564897564", .NombreTercero = "askdklasld", .Saldo = "$90.200", .TipoImpuesto = "ICA", .Valor = "$50.000", .Vigencia = "2016"})
        listDatosPruebas.Add(New DatosPruebas() With {.NitTercero = "987654321", .NombreTercero = "Jhon Garay", .Saldo = "$90.200", .TipoImpuesto = "ICA", .Valor = "$50.000", .Vigencia = "2016"})

    End Sub
End Class

Public Class DatosPruebas
    Public Property NitTercero As String
    Public Property NombreTercero As String
    Public Property Vigencia As String
    Public Property TipoImpuesto As String
    Public Property Valor As String
    Public Property Saldo As String
End Class
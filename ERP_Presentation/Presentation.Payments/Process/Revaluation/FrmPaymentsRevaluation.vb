'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Juan David Capera N.
' Created          : 09/06/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Presentation.Payments.MVP
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class FrmPaymentsRevaluation
    Implements IPaymentsRevaluation

#Region "Builder"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler CtrDateNavigator1.OnChangeDate, AddressOf EditValueChangedDate
        AddHandler bwCreateTabs.DoWork, AddressOf bwCreateTabs_DoWork
        AddHandler bwCreateTabs.RunWorkerCompleted, AddressOf bwCreateTabs_RunWorkCompleted
    End Sub
#End Region

#Region "Properties"

    ''' <summary>
    ''' LayoutControl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPaymentsRevaluation.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IPaymentsRevaluation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPaymentsRevaluation.ActionsOnControls
        Set(value As Boolean)
            INDlyPaymentRevaluation.BeginUpdate()
            INDlyPaymentRevaluation.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        Try
            Using model As New MPaymentsRevaluation(Me.Tag)
                AsyncLoader(True)
                Dim Result = Await model.CalculateRevaluation(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, ModeConfirm)

                If Result.Count = 0 Then
                    Mensaje(EeventViewerImages.MensajeError) = "Ocurrio un error en el proceso de revalorizacion de la cartera"
                ElseIf Result.Any(Function(x) x.MessageCode <> 0) Then
                    Dim message As String = String.Join(",", Result.Select(Of String)(Function(x) x.MessageVoucher))
                    Mensaje(EeventViewerImages.MensajeError) = message
                    AsyncLoader(False)
                    Exit Sub
                End If


                AsyncLoader(False)
                Dim sucessMessage As String = String.Join(",", Result.Select(Of String)(Function(x) x.MessageVoucher))
                Mensaje(EeventViewerImages.Informacion) = sucessMessage
                SearchMode = False
                EditValueChangedDate(Nothing, Nothing)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        Deshacer()
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Variables"
#Region "Constantes"
    Private Const NAME_MODULE As String = "Payments"
#Enable Warning
#End Region

    ''' <summary>
    ''' Entidad de tipo XPO
    ''' </summary>
    Dim paymentsRevaluation As PaymentsRevaluationXpo

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean?

    ''' <summary>
    ''' Variable que me permite saber si se esta confirmando
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModeConfirm As Integer

    ''' <summary>
    ''' Asyncrono para crear las rejillas correspondientes a los libros
    ''' </summary>
    ''' <remarks></remarks>
    Private bwCreateTabs As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' instancia el presentador
    ''' </summary>
    Dim Presenter As PPaymentsRevaluation

    ''' <summary>
    ''' 
    ''' </summary>
    Dim Currencyformats As Dictionary(Of Integer, String)

#End Region

#Region "Indexing"
    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.paymentsRevaluation.Id),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.paymentsRevaluation.Id & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.paymentsRevaluation.Id),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.paymentsRevaluation.Id)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.paymentsRevaluation.Id)
            Return Me._doc
        End If
    End Function
#End Region

#Region "Events"

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        ModeConfirm = 0
        Guardar()
    End Sub

    ''' <summary>
    ''' COnfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        ModeConfirm = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, 0, 0, CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, Me.BarraBotones.OperatingUnit.Id)
    End Sub

#End Region

#Region "Load"
    Private Sub FrmPaymentsRevaluation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Me.Presenter = New PPaymentsRevaluation(Me)
        Me.SearchMode = False
        LoadStatus()
        Deshacer()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Guardar, "Revalorizar")
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me.paymentsRevaluation = Nothing
        Me.Presenter = Nothing
        Me.SearchMode = Nothing
    End Sub
#End Region

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Limpia los controles del frontal
    ''' </summary>
    Private Sub CleanControls()

        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Metodo que se dispara al cambiar el valor del control de fecha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub EditValueChangedDate(sender As Object, e As EventArgs)
        If CtrDateNavigator1.GetMonth > 0 AndAlso CtrDateNavigator1.GetYear > 0 Then
            Try
                CtrDateNavigator1.Enabled = False
                AsyncLoader(True)
                bwCreateTabs.RunWorkerAsync()
            Catch ex As Exception
                AsyncLoader(False)
                CtrDateNavigator1.Enabled = True
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bwCreateTabs_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        GetCurrencyFormats()
    End Sub

    Private Sub GetCurrencyFormats()
        Me.paymentsRevaluation = Presenter.GetPaymentsRevaluationByMonthAndYear(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear)
        If Me.paymentsRevaluation?.PaymentsRevaluationDetail?.Any() Then
            Me.Currencyformats = New Dictionary(Of Integer, String)
            Me.paymentsRevaluation.PaymentsRevaluationDetail.ToList.ForEach(Sub(item)
                                                                                If Not Currencyformats.ContainsKey(item.CurrencyConverterId.Id) Then
                                                                                    Currencyformats.Add(item.CurrencyConverterId.Id, item.CurrencyConverterId.CurrencyName)
                                                                                End If
                                                                            End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se termina el evento
    ''' </summary>
    Private Sub bwCreateTabs_RunWorkCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.BarraBotones.StatusRecordVisible = True
        If Me.paymentsRevaluation Is Nothing OrElse Me.paymentsRevaluation?.Id = 0 Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Guardar, "Revalorizar")
            Me.BarraBotones.StatusRecord = "1"
            RemoveControls()
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SinRevaluaciones, Comunes)
            AsyncLoader(False)
            CtrDateNavigator1.Enabled = True
            Exit Sub
        End If
        If Me.paymentsRevaluation.PaymentsRevaluationDetail?.Any Then

            CreateTabs(Me.Currencyformats)
            'Se modifica la barra de botones
            If Me.paymentsRevaluation?.Status = 1 Then 'Si esta registrada la depreciación
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Guardar, "Revalorizar")
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.GuardarConfirmar, "Confirmar")
                If BarraBotones.PermissionsForm.ContainsKey(23) Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, 0, 0, CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, Me.BarraBotones.OperatingUnit.Id)
                End If
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                If BarraBotones.PermissionsForm.ContainsKey(23) Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, 0, 0, CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, Me.BarraBotones.OperatingUnit.Id)
                End If
            End If

            Me.BarraBotones.StatusRecord = Me.paymentsRevaluation?.Status.ToString
        End If
        AsyncLoader(False)
        CtrDateNavigator1.Enabled = True
    End Sub

    ''' <summary>
    ''' Método que remuve controles del layout principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveControls()
        'Se elimina el tabGroup principal que contiene todo
        INDlygPaymentsRevaluation.BeginUpdate()
        If INDlygPaymentsRevaluation.Items.ItemCount > 1 Then
            INDlygPaymentsRevaluation.Items.RemoveAt(1)
        End If
        INDlygPaymentsRevaluation.EndUpdate()

        'Se recorren los gridControls que hayan en el layout principal y se eliminan para que posteriormente se generen denuevo
        For i As Integer = 0 To INDlyPaymentRevaluation.Controls.Count - 1
            If i <= (INDlyPaymentRevaluation.Controls.Count - 1) AndAlso TypeOf INDlyPaymentRevaluation.Controls(i) Is DevExpress.XtraGrid.GridControl Then
                INDlyPaymentRevaluation.Controls.RemoveAt(i)
                i -= 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' Método que crea los tabs con sus rejillas para pintar los registros de los libros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateTabs(currency As Dictionary(Of Integer, String))
        If currency Is Nothing Then
            Me.GetCurrencyFormats()
        End If
        INDlyPaymentRevaluation.BeginUpdate()

        'Se eliminan los controles
        RemoveControls()

        Dim TabbedControlGroup1 As New DevExpress.XtraLayout.TabbedControlGroup()
        CType(TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        INDlygPaymentsRevaluation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabbedControlGroup1})

        'Se crea el tabGroup en donde se ubican los tabs
        TabbedControlGroup1.Location = New System.Drawing.Point(0, 100)
        'TabbedControlGroup1.Name = "TabbedControlGroup1"
        TabbedControlGroup1.SelectedTabPageIndex = 1
        TabbedControlGroup1.Size = New System.Drawing.Size(1114, 435)
        AddHandler TabbedControlGroup1.SelectedPageChanged, AddressOf SelectedPageChanged

        Dim contAssigningPopupContainer = 0

        For Each itemLegalBook In currency
            'Se genera el nuevo tab
            Dim TabBooks As New DevExpress.XtraLayout.LayoutControlGroup
            CType(TabBooks, System.ComponentModel.ISupportInitialize).BeginInit()
            TabBooks.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            TabBooks.AppearanceGroup.Options.UseFont = True
            TabBooks.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            TabBooks.AppearanceItemCaption.Options.UseFont = True
            TabBooks.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            TabBooks.AppearanceTabPage.Header.Options.UseFont = True
            TabBooks.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
            TabBooks.AppearanceTabPage.HeaderActive.Options.UseFont = True
            TabBooks.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            TabBooks.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
            TabBooks.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            TabBooks.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
            TabBooks.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            TabBooks.AppearanceTabPage.PageClient.Options.UseFont = True
            Me.IndigoLayoutControlGroup1.SetCampoObligatorio(TabBooks, False)
            TabBooks.Location = New System.Drawing.Point(0, 0)
            TabBooks.Size = New System.Drawing.Size(1090, 381)
            TabBooks.Text = itemLegalBook.Value

            'Se crea la rejilla 
            Dim GridControl As New DevExpress.XtraGrid.GridControl()
            CType(GridControl, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.IndigoGridControl1.SetAddActions(GridControl, Nothing)
            Me.IndigoGridControl1.SetControlNextFocus(GridControl, Nothing)
            Me.IndigoGridControl1.SetGuardarXml(GridControl, True)
            Me.IndigoGridControl1.SetHoldSize(GridControl, False)
            Me.IndigoGridControl1.SetHotTrack(GridControl, False)
            GridControl.Location = New System.Drawing.Point(36, 161)
            GridControl.Size = New System.Drawing.Size(1090, 381)
            Me.IndigoGridControl1.SetSizeConstraintsType(GridControl, DevExpress.XtraLayout.SizeConstraintsType.Custom)
            Me.IndigoGridControl1.SetSizeLayoutItem(GridControl, New System.Drawing.Size(1090, 0))

            INDlyPaymentRevaluation.Controls.Add(GridControl)

            'Se crean las columnas de la rejilla
            Dim gcDocumentType As New DevExpress.XtraGrid.Columns.GridColumn()
            gcDocumentType.Caption = "Documento"
            gcDocumentType.OptionsColumn.AllowEdit = False
            gcDocumentType.OptionsColumn.AllowFocus = False
            gcDocumentType.Visible = True
            gcDocumentType.VisibleIndex = 0
            gcDocumentType.FieldName = "DocumentName"

            Dim INDGcDocumentNumber As New DevExpress.XtraGrid.Columns.GridColumn()
            INDGcDocumentNumber.Caption = "Número Documento"
            INDGcDocumentNumber.OptionsColumn.AllowEdit = False
            INDGcDocumentNumber.OptionsColumn.AllowFocus = False
            INDGcDocumentNumber.Visible = True
            INDGcDocumentNumber.VisibleIndex = 0
            INDGcDocumentNumber.FieldName = "DocumentNumber"

            Dim GridColumn2 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn2.Caption = "Factura"
            GridColumn2.OptionsColumn.AllowEdit = False
            GridColumn2.OptionsColumn.AllowFocus = False
            GridColumn2.Visible = True
            GridColumn2.VisibleIndex = 0
            GridColumn2.FieldName = "AccountPayableId.BillNumber"

            Dim GridColumn12 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn12.Caption = "Tercero"
            GridColumn12.OptionsColumn.AllowEdit = False
            GridColumn12.OptionsColumn.AllowFocus = False
            GridColumn12.Visible = True
            GridColumn12.VisibleIndex = 2
            GridColumn12.FieldName = "ThirdPartyId.NitName"

            Dim GridColumn6 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn6.Caption = "Moneda"
            GridColumn6.OptionsColumn.AllowEdit = False
            GridColumn6.OptionsColumn.AllowFocus = False
            GridColumn6.Visible = True
            GridColumn6.VisibleIndex = 3
            GridColumn6.FieldName = "CurrencyId.CurrencyName"

            Dim GridColumn7 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn7.Caption = "Valor"
            GridColumn7.OptionsColumn.AllowEdit = False
            GridColumn7.OptionsColumn.AllowFocus = False
            GridColumn7.Visible = True
            GridColumn7.VisibleIndex = 4
            GridColumn7.FieldName = "Value"
            GridColumn7.DisplayFormat.FormatString = "n2"
            GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric

            Dim GridColumn8 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn8.Caption = "Saldo"
            GridColumn8.OptionsColumn.AllowEdit = False
            GridColumn8.OptionsColumn.AllowFocus = False
            GridColumn8.Visible = True
            GridColumn8.VisibleIndex = 5
            GridColumn8.FieldName = "Balance"
            GridColumn8.DisplayFormat.FormatString = "n2"
            GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric

            Dim fieldTRM = "ValueCurrency"
            Dim fieldTRM2 = "ActualValueCurrency"
            If itemLegalBook.Key = SessionValues.Instance.OfficialCurrencyId Then
                fieldTRM = "ValueCurrencyReverse"
                fieldTRM2 = "ActualValueCurrencyReverse"
            End If

            Dim GridColumn9 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn9.Caption = "Tasa Cambio Documento"
            GridColumn9.OptionsColumn.AllowEdit = False
            GridColumn9.OptionsColumn.AllowFocus = False
            GridColumn9.Visible = True
            GridColumn9.VisibleIndex = 6
            GridColumn9.FieldName = fieldTRM
            GridColumn9.DisplayFormat.FormatString = "n2"
            GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric

            Dim GridColumn10 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn10.Caption = "Tasa Cambio Actual"
            GridColumn10.OptionsColumn.AllowEdit = False
            GridColumn10.OptionsColumn.AllowFocus = False
            GridColumn10.Visible = True
            GridColumn10.VisibleIndex = 7
            GridColumn10.FieldName = fieldTRM2
            GridColumn10.DisplayFormat.FormatString = "n2"
            GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric

            Dim GridColumn11 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn11.Caption = "Cuenta Contable"
            GridColumn11.OptionsColumn.AllowEdit = False
            GridColumn11.OptionsColumn.AllowFocus = False
            GridColumn11.Visible = True
            GridColumn11.VisibleIndex = 8
            GridColumn11.FieldName = "MainAccountId.NumberName"

            Dim GridColumn99 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn99.Caption = "Saldo Anterior en " + itemLegalBook.Value
            GridColumn99.OptionsColumn.AllowEdit = False
            GridColumn99.OptionsColumn.AllowFocus = False
            GridColumn99.Visible = True
            GridColumn99.VisibleIndex = 9
            GridColumn99.FieldName = "BalanceConverted"
            GridColumn99.DisplayFormat.FormatString = "n2"
            GridColumn99.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridColumn99.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BalanceConverted", "{0:n2}")})

            Dim GridColumn4 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn4.Caption = "Saldo en " + itemLegalBook.Value
            GridColumn4.OptionsColumn.AllowEdit = False
            GridColumn4.OptionsColumn.AllowFocus = False
            GridColumn4.Visible = True
            GridColumn4.VisibleIndex = 10
            GridColumn4.FieldName = "ActualBalanceConverter"
            GridColumn4.DisplayFormat.FormatString = "n2"
            GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridColumn4.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ActualBalanceConverter", "{0:n2}")})

            Dim GridColumn3 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn3.Caption = "Ganancia/Perdida"
            GridColumn3.OptionsColumn.AllowEdit = False
            GridColumn3.OptionsColumn.AllowFocus = False
            GridColumn3.Visible = True
            GridColumn3.VisibleIndex = 11
            GridColumn3.FieldName = "ProfitLostValue"
            GridColumn3.DisplayFormat.FormatString = "n2"
            GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridColumn3.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ProfitLostValue", "{0:n2}")})

            'Se crea la vista para la rejilla
            Dim view As New DevExpress.XtraGrid.Views.Grid.GridView()
            CType(view, System.ComponentModel.ISupportInitialize).BeginInit()
            view.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
            view.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
            view.Appearance.FocusedRow.Options.UseBorderColor = True
            view.Appearance.FocusedRow.Options.UseFont = True
            view.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            view.Appearance.GroupRow.Options.UseFont = True
            view.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            view.Appearance.HeaderPanel.Options.UseFont = True
            view.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
            view.Appearance.Row.Options.UseFont = True
            view.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
            view.Appearance.ViewCaption.Options.UseFont = True
            view.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {gcDocumentType, INDGcDocumentNumber, GridColumn2, GridColumn12, GridColumn6, GridColumn7, GridColumn8, GridColumn9, GridColumn10, GridColumn11, GridColumn99, GridColumn4, GridColumn3})
            view.GridControl = GridControl
            view.OptionsView.EnableAppearanceEvenRow = True
            view.OptionsView.EnableAppearanceOddRow = True
            view.OptionsView.ShowAutoFilterRow = True
            view.OptionsView.ShowDetailButtons = False
            view.OptionsView.ShowGroupPanel = False
            view.OptionsView.ShowFooter = True
            view.OptionsBehavior.AutoExpandAllGroups = True
            view.GroupCount = 1
            view.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(gcDocumentType, DevExpress.Data.ColumnSortOrder.Ascending)})
            Me.IndigoGridView1.SetTemaIndigoMetro(view, False)

            GridControl.MainView = view
            GridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {view})
            Me.IndigoGridControl1.SetExportButtonControl(GridControl, True)
            Me.IndigoGridControl1.SetExportButton(GridControl, True)
            'Se crea el layout que se ubica en cada tab y el que contiene la rejilla
            Dim LayoutItem As New DevExpress.XtraLayout.LayoutControlItem()
            CType(LayoutItem, System.ComponentModel.ISupportInitialize).BeginInit()
            LayoutItem.Control = GridControl
            LayoutItem.Location = New System.Drawing.Point(0, 0)
            LayoutItem.MaxSize = New System.Drawing.Size(0, 0)
            LayoutItem.MinSize = New System.Drawing.Size(1090, 24)
            LayoutItem.Size = New System.Drawing.Size(1090, 381)
            LayoutItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutItem.TextSize = New System.Drawing.Size(0, 0)
            LayoutItem.TextVisible = False

            'Se asigna el datasource a la rejilla correspondiente
            GridControl.DataSource = Me.paymentsRevaluation.PaymentsRevaluationDetail.ToList.Where(Function(item) item.CurrencyConverterId.Id = itemLegalBook.Key).ToList
            GridControl.RefreshDataSource()

            TabBooks.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {LayoutItem})

            'Se agrega el nuevo tab al tabControlGroup
            TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabBooks})
            CType(GridControl, System.ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutItem, System.ComponentModel.ISupportInitialize).EndInit()
            CType(TabBooks, System.ComponentModel.ISupportInitialize).EndInit()

            contAssigningPopupContainer += 1
        Next

        CType(TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        INDlyPaymentRevaluation.EndUpdate()
    End Sub

    ''' <summary>
    ''' Se dispara al cambiar de tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs)
        Dim TabGroup = CType(sender, DevExpress.XtraLayout.TabbedControlGroup)
        Dim TabPage = CType(TabGroup.SelectedTabPage, DevExpress.XtraLayout.LayoutControlGroup)
        Dim GridControl = DirectCast(TabPage.Items(0), DevExpress.XtraLayout.LayoutControlItem).Control
    End Sub

#End Region

End Class
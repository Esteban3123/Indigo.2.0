'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/06/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class FrmFixedAssetDepreciation
    Implements IFixedAssetDepreciation

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler CtrDateNavigator1.OnChangeDate, AddressOf EditValueChangedDate
        AddHandler bwCreateTabs.DoWork, AddressOf BwCreateTabs_DoWork
        AddHandler bwCreateTabs.RunWorkerCompleted, AddressOf bwCreateTabs_RunWorkerCompleted

        CtrDate = New CtrYearMonthHorizontal()
        CtrDate.SetInfo(AddressOf getYearMonth)
        CtrDate.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(CtrDate)
    End Sub

    Private Function getYearMonth() As Tuple(Of Integer, Integer)
        Return New Tuple(Of Integer, Integer)(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' LayoutControl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetDepreciation.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IFixedAssetDepreciation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Private FixedAssetDepreciation As FixedAssetDepreciation

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private Presenter As PFixedAssetDepreciation

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Variable que me permite saber si se esta confirmando o solo depreciando
    ''' </summary>
    ''' <remarks></remarks>
    Private ModeConfirm As Boolean

    ''' <summary>
    ''' Asyncrono para crear las rejillas correspondientes a los libros
    ''' </summary>
    ''' <remarks></remarks>
    Private bwCreateTabs As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private varImp As Integer

    Private CtrDate As CtrYearMonthHorizontal

#End Region

#Region "BackgroundWorker"

    ''' <summary>
    ''' Entidad xpo para depreciación
    ''' </summary>
    ''' <remarks></remarks>
    Private depreciationXpo As FixedAssetDepreciationXpo

    ''' <summary>
    ''' Diccionario para libros contables
    ''' </summary>
    ''' <remarks></remarks>
    Private dictionaryLegalBook As Dictionary(Of Integer, BookXpo)

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BwCreateTabs_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        'Se crea el diccionario para agrupar los libros del detalle de la depreciación
        dictionaryLegalBook = New Dictionary(Of Integer, BookXpo)

        'Consulta si ya hay depreciación para el mes y el año seleccionado
        depreciationXpo = Presenter.GetFixedAssetDeprececiationByMonthAndYear(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear)

        If depreciationXpo IsNot Nothing Then

            If depreciationXpo.FixedAssetDepreciationDetailXpo?.Any() Then
                depreciationXpo.FixedAssetDepreciationDetailXpo.GroupBy(Function(i) New With {i.LegalBookId.Id, i.LegalBookId}).ToList().ForEach(Sub(i)
                                                                                                                                                     If Not dictionaryLegalBook.ContainsKey(i.Key.LegalBookId.Id) Then
                                                                                                                                                         dictionaryLegalBook.Add(i.Key.LegalBookId.Id, i.Key.LegalBookId)
                                                                                                                                                     End If
                                                                                                                                                 End Sub)
            End If

            If depreciationXpo.FixedAssetAmortizationDetailXpo?.Any() Then
                depreciationXpo.FixedAssetAmortizationDetailXpo.GroupBy(Function(i) New With {i.FixedAssetPhysicalAssetDetailBookId.LegalBookId.Id, i.FixedAssetPhysicalAssetDetailBookId.LegalBookId}).ToList().ForEach(Sub(i)
                                                                                                                                                                                                                             If Not dictionaryLegalBook.ContainsKey(i.Key.LegalBookId.Id) Then
                                                                                                                                                                                                                                 dictionaryLegalBook.Add(i.Key.LegalBookId.Id, i.Key.LegalBookId)
                                                                                                                                                                                                                             End If
                                                                                                                                                                                                                         End Sub)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwCreateTabs_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.BarraBotones.StatusRecordVisible = True

        If depreciationXpo Is Nothing Then 'Si no hay depreciaciones con el mes y el año seleccionado
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Guardar, ResourceManager.GetString("Depreciate", NAME_MODULE))
            Me.BarraBotones.StatusRecord = "1"

            Mensaje(EeventViewerImages.Advertencia) = "No hay registrada depreciación para el mes y el año seleccionado"
            AsyncLoader(False)
            CtrDateNavigator1.Enabled = True
            Exit Sub
        End If

        INDLcgPeriod.HideControl()
        If depreciationXpo.FixedAssetDepreciationDetailXpo?.Any() Then
            INDlygDepreciation.HideControl(False)
        End If

        If depreciationXpo.FixedAssetAmortizationDetailXpo?.Any() Then
            INDlygAmortization.HideControl(False)
        End If

        If INDlygDepreciation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always OrElse
            INDlygAmortization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then

            'Verificamos cuantos libros hay registrados para asi mismo crear los tabs con sus rejillas en el form en tiempo de ejecución
            CreateTabs(dictionaryLegalBook)

            'Se modifica la barra de botones
            If depreciationXpo.Status = 1 Then 'Si esta registrada la depreciación
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Guardar, ResourceManager.GetString("Depreciate", NAME_MODULE))
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.GuardarConfirmar, ResourceManager.GetString("Confirm", NAME_MODULE))

                If BarraBotones.PermissionsForm.ContainsKey(23) Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, 0, 0, CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, Me.BarraBotones.OperatingUnit.Id)
                End If
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False

                If BarraBotones.PermissionsForm.ContainsKey(23) Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, 0, 0, CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, Me.BarraBotones.OperatingUnit.Id)
                End If
            End If

            Me.BarraBotones.StatusRecord = depreciationXpo.Status.ToString
        End If

        AsyncLoader(False)
        CtrDateNavigator1.Enabled = True
    End Sub

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()

        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)

        CtrDate.RefreshInfo()
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
            Using model As New MFixedAssetDepreciation(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveDepreciation(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, Me.BarraBotones.OperatingUnit.Id, ModeConfirm)
                If Result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    AsyncLoader(False)
                    Me.FixedAssetDepreciation = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, Result.ObjectEmbbeded.Id, 0, CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, Me.BarraBotones.OperatingUnit.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, Result.ObjectEmbbeded.Id, 0, Result.ObjectEmbbeded.Id, Me.BarraBotones.OperatingUnit.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, Result.ObjectEmbbeded.Id, 0, Result.ObjectEmbbeded.Id, Me.BarraBotones.OperatingUnit.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, Result.ObjectEmbbeded.Id, 0, Result.ObjectEmbbeded.Id, Me.BarraBotones.OperatingUnit.Id)
                    End Select

                    AsyncLoader(False)

                    Deshacer()
                    EditValueChangedDate(Nothing, Nothing)
                Else
                    AsyncLoader(False)
                    If Result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    ElseIf Result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
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

#End Region

#Region "Methods"

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
                CtrDate.RefreshInfo()
            Catch ex As Exception
                AsyncLoader(False)
                CtrDateNavigator1.Enabled = True
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetDepreciation.ActionsOnControls
        Set(value As Boolean)
            INDlyDepreciation.BeginUpdate()

            INDlyDepreciation.EndUpdate()
            If value Then

            Else

            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

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

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.34)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListFixedAssetChangePlate
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetDepreciation.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetDepreciation.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetDepreciation.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetDepreciation.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetDepreciation.Code)
            Return Me._doc
        End If
    End Function

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
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyDepreciation.BeginUpdate()

        ActionsOnControls = False
        INDLcgPeriod.HideControl(False)
        INDlygDepreciation.HideControl()
        INDlygAmortization.HideControl()
        Me.BarraBotones.StatusRecordVisible = False

        INDlyDepreciation.EndUpdate()

        RemoveControls()

        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Método que remuve controles del layout principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveControls()
        'Se elimina el tabGroup principal que contiene todo
        INDlygDepreciation.BeginUpdate()

        If INDlygDepreciation.Items.ItemCount > 0 Then
            INDlygDepreciation.Items.RemoveAt(0)
        End If

        INDlygDepreciation.EndUpdate()

        INDlygAmortization.BeginUpdate()

        If INDlygAmortization.Items.ItemCount > 0 Then
            INDlygAmortization.Items.RemoveAt(0)
        End If

        INDlygAmortization.EndUpdate()

        INDlyDepreciation.BeginUpdate()

        'Se recorren los gridControls que hayan en el layout principal y se eliminan para que posteriormente se generen denuevo
        For i As Integer = 0 To INDlyDepreciation.Controls.Count - 1
            If i <= (INDlyDepreciation.Controls.Count - 1) AndAlso TypeOf INDlyDepreciation.Controls(i) Is DevExpress.XtraGrid.GridControl Then
                INDlyDepreciation.Controls.RemoveAt(i)
                i -= 1
            End If
        Next

        INDlyDepreciation.EndUpdate()
    End Sub

    ''' <summary>
    ''' Método que crea los tabs con sus rejillas para pintar los registros de los libros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateTabs(dictionaryLegalBook As Dictionary(Of Integer, BookXpo))
        Task.Factory.StartNew(Sub()
                                  Me.SafeInvoke(Sub()
                                                    TabDepreciation(dictionaryLegalBook)
                                                    TabAmortization(dictionaryLegalBook)
                                                End Sub)
                              End Sub)
    End Sub

#Region "Depreciation"

    ''' <summary>
    ''' Método que crea el tab de "Depreciacion" con su rejilla
    ''' </summary>
    Private Sub TabDepreciation(dictionaryLegalBook As Dictionary(Of Integer, BookXpo))
        INDlygDepreciation.BeginUpdate()

        Dim TabbedControlGroup1 As New DevExpress.XtraLayout.TabbedControlGroup()
        CType(TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        INDlygDepreciation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabbedControlGroup1})

        'Se crea el tabGroup en donde se ubican los tabs
        TabbedControlGroup1.Location = New System.Drawing.Point(0, 0)
        TabbedControlGroup1.SelectedTabPageIndex = 1
        TabbedControlGroup1.Size = New System.Drawing.Size(1114, 435)
        AddHandler TabbedControlGroup1.SelectedPageChanged, AddressOf SelectedDepreciationPageChanged

        Dim contAssigningPopupContainer = 0

        For Each itemLegalBook In dictionaryLegalBook

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
            TabBooks.Text = itemLegalBook.Value.CodeName

            'Se crea el repositorio tipo popup que va en la columna No. 5
            Dim RepositoryPopup As New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
            CType(RepositoryPopup, System.ComponentModel.ISupportInitialize).BeginInit()
            RepositoryPopup.AutoHeight = False
            RepositoryPopup.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            RepositoryPopup.Name = "INDrepPopupPartsDetailBook"

            If contAssigningPopupContainer = 0 Then
                RepositoryPopup.PopupControl = INDpopupDetailCost
            End If

            RepositoryPopup.PopupSizeable = False
            RepositoryPopup.ShowPopupCloseButton = False
            RepositoryPopup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
            AddHandler RepositoryPopup.Popup, AddressOf PopupDepreciation

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
            GridControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {RepositoryPopup})
            Me.IndigoGridControl1.SetSizeConstraintsType(GridControl, DevExpress.XtraLayout.SizeConstraintsType.Custom)
            Me.IndigoGridControl1.SetSizeLayoutItem(GridControl, New System.Drawing.Size(1090, 0))

            INDlyDepreciation.Controls.Add(GridControl)

            'Se crean las columnas de la rejilla
            Dim GridColumn2 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn2.SetDataMainGridColumn("Articulo", "FixedAssetPhysicalAssetId.ItemId.CodeDescription", 0)

            Dim GridColumn3 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn3.SetDataMainGridColumn("Cuenta Contable", "FixedAssetPhysicalAssetId.MainAccountId.NumberName", 1)

            Dim GridColumn4 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn4.SetDataMainGridColumn("Serie", "FixedAssetPhysicalAssetId.Serie", 2)

            Dim GridColumn5 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn5.SetDataMainGridColumn("Placa", "FixedAssetPhysicalAssetId.Plate", 3)

            Dim GridColumn6 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn6.SetDataMainGridColumn("Valor Histórico", "FixedAssetPhysicalAssetDetailBookId.HistoricalValue", 4)
            GridColumn6.SetFormatNumericGridColumn()
            GridColumn6.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FixedAssetPhysicalAssetDetailBookId.HistoricalValue", "{0:c2}")})
            GridColumn6 = Window.Utils.FormatGrid(GridColumn6, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn7 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn7.SetDataMainGridColumn("Descuento Financiero", "FinantialDiscount", 5)
            GridColumn7.SetFormatNumericGridColumn()
            GridColumn7.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FinantialDiscount", "{0:c2}")})
            GridColumn7 = Window.Utils.FormatGrid(GridColumn7, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn8 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn8.SetDataMainGridColumn("Valor Histórico Neto", "NetHistoricalValue", 6)
            GridColumn8.SetFormatNumericGridColumn()
            GridColumn8.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NetHistoricalValue", "{0:c2}")})
            GridColumn8 = Window.Utils.FormatGrid(GridColumn8, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn9 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn9.SetDataMainGridColumn("Vida Util (Dias)", "FixedAssetPhysicalAssetDetailBookId.LifeTimeInDays", 7)

            Dim GridColumn10 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn10.SetDataMainGridColumn("Días Depreciados", "DepreciatedDays", 8)

            Dim GridColumn11 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn11.SetDataMainGridColumn("Valor Depreciación", "DepreciationValue", 9)
            GridColumn11.SetFormatNumericGridColumn()
            GridColumn11.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DepreciationValue", "{0:c2}")})
            GridColumn11 = Window.Utils.FormatGrid(GridColumn11, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn12 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn12.SetDataMainGridColumn("Ajuste Depreciación", "FinantialDiscountAdjusment", 10)
            GridColumn12.SetFormatNumericGridColumn()
            GridColumn12.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FinantialDiscountAdjusment", "{0:c2}")})
            GridColumn12 = Window.Utils.FormatGrid(GridColumn12, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn13 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn13.SetDataMainGridColumn("Depreciación Neta", "NetDepreciationValue", 11)
            GridColumn13.SetFormatNumericGridColumn()
            GridColumn13.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NetDepreciationValue", "{0:c2}")})
            GridColumn13 = Window.Utils.FormatGrid(GridColumn13, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn14 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn14.AppearanceCell.Options.UseTextOptions = True
            GridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridColumn14.AppearanceHeader.Options.UseTextOptions = True
            GridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridColumn14.Caption = "+"
            GridColumn14.ColumnEdit = RepositoryPopup
            GridColumn14.Visible = True
            GridColumn14.VisibleIndex = 12
            GridColumn14.Width = 30

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
            view.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {GridColumn2, GridColumn3, GridColumn4, GridColumn5, GridColumn6, GridColumn7, GridColumn8, GridColumn9, GridColumn10, GridColumn11, GridColumn12, GridColumn13, GridColumn14})
            view.GridControl = GridControl
            view.OptionsView.EnableAppearanceEvenRow = True
            view.OptionsView.EnableAppearanceOddRow = True
            view.OptionsView.ShowAutoFilterRow = True
            view.OptionsView.ShowDetailButtons = False
            view.OptionsView.ShowGroupPanel = False
            view.OptionsView.ShowFooter = True
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
            GridControl.DataSource = depreciationXpo.FixedAssetDepreciationDetailXpo.ToList.Where(Function(item) item.LegalBookId.Id = itemLegalBook.Key).ToList
            GridControl.RefreshDataSource()

            TabBooks.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {LayoutItem})

            'Se agrega el nuevo tab al tabControlGroup
            TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabBooks})
            CType(RepositoryPopup, System.ComponentModel.ISupportInitialize).EndInit()
            CType(GridControl, System.ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutItem, System.ComponentModel.ISupportInitialize).EndInit()
            CType(TabBooks, System.ComponentModel.ISupportInitialize).EndInit()

            contAssigningPopupContainer += 1
        Next

        CType(TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()

        INDlygDepreciation.EndUpdate()
    End Sub

    ''' <summary>
    ''' Se dispara al cambiar de tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectedDepreciationPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs)
        Dim TabGroup = CType(sender, DevExpress.XtraLayout.TabbedControlGroup)
        Dim TabPage = CType(TabGroup.SelectedTabPage, DevExpress.XtraLayout.LayoutControlGroup)
        Dim GridControl = DirectCast(TabPage.Items(0), DevExpress.XtraLayout.LayoutControlItem).Control
        Dim RepositoryItem = DirectCast(DirectCast(GridControl, DevExpress.XtraGrid.GridControl).RepositoryItems(0), DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit)
        RepositoryItem.PopupControl = INDpopupDetailCost
        INDgcDetailCost.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' Se dispara al momento de desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupDepreciation(sender As Object, e As EventArgs)
        Dim GridControl = DirectCast(DirectCast(sender, DevExpress.XtraEditors.PopupContainerEdit).Parent, DevExpress.XtraGrid.GridControl)
        Dim view = DirectCast(GridControl.Views(0), DevExpress.XtraGrid.Views.Grid.GridView)
        Dim depreciationDetail As FixedAssetDepreciationDetailXpo = view.GetFocusedRow()
        INDgcDetailCost.DataSource = Nothing
        INDgcDetailCost.DataSource = depreciationDetail.FixedAssetDepreciationDetailCostXpo.ToList
    End Sub

#End Region

#Region "Amortization"

    ''' <summary>
    ''' Método que crea el tab de "Amortizacion" con su rejillas
    ''' </summary>
    Private Sub TabAmortization(dictionaryLegalBook As Dictionary(Of Integer, BookXpo))
        INDlygAmortization.BeginUpdate()

        Dim TabbedControlGroup2 As New DevExpress.XtraLayout.TabbedControlGroup()
        CType(TabbedControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        INDlygAmortization.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabbedControlGroup2})

        'Se crea el tabGroup en donde se ubican los tabs
        TabbedControlGroup2.Location = New System.Drawing.Point(0, 0)
        TabbedControlGroup2.SelectedTabPageIndex = 1
        TabbedControlGroup2.Size = New System.Drawing.Size(1114, 435)
        AddHandler TabbedControlGroup2.SelectedPageChanged, AddressOf SelectedAmortizationPageChanged

        Dim cont = 0

        For Each itemLegalBook In dictionaryLegalBook
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
            TabBooks.Text = itemLegalBook.Value.CodeName

            'Se crea el repositorio tipo popup para el detalle de la cuenta (Centro de costo y tercero)
            Dim RepositoryPopup As New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
            CType(RepositoryPopup, System.ComponentModel.ISupportInitialize).BeginInit()
            RepositoryPopup.AutoHeight = False
            RepositoryPopup.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            RepositoryPopup.Name = "INDRpAmortizationDetailCost"

            If cont = 0 Then
                RepositoryPopup.PopupControl = INDPopupAmortizationDetailCost
            End If

            RepositoryPopup.PopupSizeable = False
            RepositoryPopup.ShowPopupCloseButton = False
            RepositoryPopup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
            AddHandler RepositoryPopup.Popup, AddressOf PopupAmortization

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
            GridControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {RepositoryPopup})
            Me.IndigoGridControl1.SetSizeConstraintsType(GridControl, DevExpress.XtraLayout.SizeConstraintsType.Custom)
            Me.IndigoGridControl1.SetSizeLayoutItem(GridControl, New System.Drawing.Size(1090, 0))

            INDlyDepreciation.Controls.Add(GridControl)

            'Se crean las columnas de la rejilla
            Dim GridColumn20 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn20.SetDataMainGridColumn("Articulo", "FixedAssetPhysicalAssetDetailBookId.PhysicalAssetId.ItemId.CodeDescription", 0)

            Dim GridColumn5 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn5.SetDataMainGridColumn("Placa", "FixedAssetPhysicalAssetDetailBookId.PhysicalAssetId.Plate", 1)

            Dim GridColumn21 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn21.SetDataMainGridColumn("Cuenta Contable", "FixedAssetPhysicalAssetDetailBookId.PhysicalAssetId.MainAccountId.NumberName", 2)

            Dim GridColumn22 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn22.SetDataMainGridColumn("Valor Histórico", "FixedAssetPhysicalAssetDetailBookId.HistoricalValue", 3)
            GridColumn22.SetFormatNumericGridColumn()
            GridColumn22.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FixedAssetPhysicalAssetDetailBookId.HistoricalValue", "{0:c2}")})
            GridColumn22 = Window.Utils.FormatGrid(GridColumn22, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn23 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn23.SetDataMainGridColumn("Periodo (Días)", "FixedAssetPhysicalAssetDetailBookId.LifeTimeInDays", 4)

            Dim GridColumn24 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn24.SetDataMainGridColumn("Días amortizados", "AmortizedDays", 5)

            Dim GridColumn25 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn25.SetDataMainGridColumn("Valor Amortizado", "AmortizedValue", 6)
            GridColumn25.SetFormatNumericGridColumn()
            GridColumn25.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AmortizedValue", "{0:c2}")})
            GridColumn25 = Window.Utils.FormatGrid(GridColumn25, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn27 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn27.SetDataMainGridColumn("Amortización Acumulada", "AccumulatedAmortization", 7)
            GridColumn27.SetFormatNumericGridColumn()
            GridColumn27.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AccumulatedDepreciation", "{0:c2}")})
            GridColumn27 = Window.Utils.FormatGrid(GridColumn27, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn26 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn26.AppearanceCell.Options.UseTextOptions = True
            GridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridColumn26.AppearanceHeader.Options.UseTextOptions = True
            GridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridColumn26.Caption = "+"
            GridColumn26.ColumnEdit = RepositoryPopup
            GridColumn26.Visible = True
            GridColumn26.VisibleIndex = 12
            GridColumn26.Width = 30

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
            view.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {GridColumn20, GridColumn5, GridColumn21, GridColumn22, GridColumn23, GridColumn24, GridColumn25, GridColumn27, GridColumn26})
            view.GridControl = GridControl
            view.OptionsView.EnableAppearanceEvenRow = True
            view.OptionsView.EnableAppearanceOddRow = True
            view.OptionsView.ShowAutoFilterRow = True
            view.OptionsView.ShowDetailButtons = False
            view.OptionsView.ShowGroupPanel = False
            view.OptionsView.ShowFooter = True
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
            GridControl.DataSource = depreciationXpo.FixedAssetAmortizationDetailXpo.ToList.Where(Function(item) item.FixedAssetPhysicalAssetDetailBookId.LegalBookId.Id = itemLegalBook.Key).ToList
            GridControl.RefreshDataSource()

            TabBooks.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {LayoutItem})

            'Se agrega el nuevo tab al tabControlGroup
            TabbedControlGroup2.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabBooks})
            CType(RepositoryPopup, System.ComponentModel.ISupportInitialize).EndInit()
            CType(GridControl, System.ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutItem, System.ComponentModel.ISupportInitialize).EndInit()
            CType(TabBooks, System.ComponentModel.ISupportInitialize).EndInit()

            cont += 1
        Next

        CType(TabbedControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        INDlygAmortization.EndUpdate()
    End Sub

    Private Sub SelectedAmortizationPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs)
        Dim TabGroup = CType(sender, DevExpress.XtraLayout.TabbedControlGroup)
        Dim TabPage = CType(TabGroup.SelectedTabPage, DevExpress.XtraLayout.LayoutControlGroup)
        Dim GridControl = DirectCast(TabPage.Items(0), DevExpress.XtraLayout.LayoutControlItem).Control
        Dim RepositoryItem = DirectCast(DirectCast(GridControl, DevExpress.XtraGrid.GridControl).RepositoryItems(0), DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit)
        RepositoryItem.PopupControl = INDPopupAmortizationDetailCost
        INDgcAmortizationDetailCost.DataSource = Nothing
    End Sub

    Private Sub PopupAmortization(sender As Object, e As EventArgs)
        Dim GridControl = DirectCast(DirectCast(sender, DevExpress.XtraEditors.PopupContainerEdit).Parent, DevExpress.XtraGrid.GridControl)
        Dim view = DirectCast(GridControl.Views(0), DevExpress.XtraGrid.Views.Grid.GridView)
        Dim amortizationDetail As FixedAssetAmortizationDetailXpo = view.GetFocusedRow()
        INDgcAmortizationDetailCost.DataSource = Nothing
        INDgcAmortizationDetailCost.DataSource = amortizationDetail.FixedAssetAmortizationDetailCostXpo.ToList
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FixedAssetDepreciation = Nothing
        Presenter = Nothing
        ModeConfirm = Nothing
        bwCreateTabs = Nothing
        varImp = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetDepreciation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyDepreciation, True)
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetDepreciation(Me)
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetDepreciation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetDepreciation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CtrDateNavigator1.Focus()
    End Sub

#End Region

#Region "IndigoControl"

    ''' <summary>
    ''' Se ejecuta cuando se termina el endInit del indigoControl
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridControl1_EndInitCompleted(sender As Object, e As EventArgs) Handles IndigoGridControl1.EndInitCompleted
        EditValueChangedDate(Nothing, Nothing)
    End Sub

#End Region

#End Region

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
        varImp = 1
        ModeConfirm = False
        Guardar()
    End Sub

    ''' <summary>
    ''' COnfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        ModeConfirm = True
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

End Class
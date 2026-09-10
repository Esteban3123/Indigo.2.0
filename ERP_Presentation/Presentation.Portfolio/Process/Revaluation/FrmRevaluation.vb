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
Imports Presentation.Portfolio.MVP
Imports Infrastructure.Data.Xpo.PortfolioRepository

#End Region

Public Class FrmRevaluation
    Implements IPortfolioRevaluation

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler CtrDateNavigator1.OnChangeDate, AddressOf EditValueChangedDate
        AddHandler bwCreateTabs.DoWork, AddressOf bwCreateTabs_DoWork
        AddHandler bwCreateTabs.RunWorkerCompleted, AddressOf bwCreateTabs_RunWorkerCompleted
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' LayoutControl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPortfolioRevaluation.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IPortfolioRevaluation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim PortfolioRevaluation As PortfolioRevaluationXpo

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PPortfolioRevaluation

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

    ''' <summary>
    ''' Listado de detalles de la depreciación
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRevaluationDetail As List(Of PortfolioRevaluationDetailXpo)

    ''' <summary>
    ''' Variable que me permite saber si se esta confirmando o solo depreciando
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModeConfirm As Integer

    ''' <summary>
    ''' Asyncrono para crear las rejillas correspondientes a los libros
    ''' </summary>
    ''' <remarks></remarks>
    Private bwCreateTabs As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

#End Region

#Region "BackgroundWorker"

    ''' <summary>
    ''' Entidad xpo para depreciación
    ''' </summary>
    ''' <remarks></remarks>
    Dim revaluationXpo As PortfolioRevaluationXpo

    ''' <summary>
    ''' Diccionario para libros contables
    ''' </summary>
    ''' <remarks></remarks>
    Dim dictionaryLegalBook As Dictionary(Of Integer, String)

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwCreateTabs_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        'Consulta si ya hay depreciación para el mes y el año seleccionado
        revaluationXpo = Presenter.GetPortfolioRevaluationByMonthAndYear(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear)
        If revaluationXpo IsNot Nothing AndAlso revaluationXpo.Portfolio_Revaluation IsNot Nothing AndAlso revaluationXpo.Portfolio_Revaluation.Count > 0 Then
            'Se crea el diccionario para agrupar los libros del detalle de la depreciación
            dictionaryLegalBook = New Dictionary(Of Integer, String)
            revaluationXpo.Portfolio_Revaluation.ToList.ForEach(Sub(item)
                                                                    If Not dictionaryLegalBook.ContainsKey(item.CurrencyConverterId.Id) Then
                                                                        dictionaryLegalBook.Add(item.CurrencyConverterId.Id, item.CurrencyConverterId.CurrencyName)
                                                                    End If
                                                                End Sub)
            'Se genera el listado que van en las rejillas
            'Se comenta esta linea porque en pitalito se demoraba mucho la conversion de xpo a entity
            'entonces se esta pegando a la rejilla directamente el xpo
            'GenerateList(depreciationXpo.FixedAssetDepreciationDetailXpo.ToList)
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
        If revaluationXpo Is Nothing OrElse revaluationXpo?.Id = 0 Then 'Si no hay depreciaciones con el mes y el año seleccionado
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
        If revaluationXpo.Portfolio_Revaluation IsNot Nothing AndAlso revaluationXpo.Portfolio_Revaluation.Count > 0 Then
            'Verificamos cuantos libros hay registrados para asi mismo crear los tabs con sus rejillas en el form en tiempo de ejecución
            CreateTabs(dictionaryLegalBook)

            'Se modifica la barra de botones
            If revaluationXpo.Status = 1 Then 'Si esta registrada la depreciación
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

            Me.BarraBotones.StatusRecord = revaluationXpo.Status.ToString
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
            Using model As New MPortfolioRevaluation(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.CalculateRevaluation(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, ModeConfirm)

                If Result.Count = 0 Then
                    Mensaje(EeventViewerImages.MensajeError) = "Ocurrio un error en el proceso de revalorizacion de la cartera"
                    AsyncLoader(False)
                    Exit Sub
                ElseIf Result.Any(Function(x) x.MessageCode <> 0) Then
                    Dim message As String = String.Join(",", Result.Select(Of String)(Function(x) x.MessageVoucher))
                    Mensaje(EeventViewerImages.MensajeError) = message
                    AsyncLoader(False)
                    Exit Sub
                End If

                Dim sucessMessage As String = String.Join(",", Result.Select(Of String)(Function(x) x.MessageVoucher))
                Mensaje(EeventViewerImages.Informacion) = sucessMessage
                AsyncLoader(False)
                EditValueChangedDate(Nothing, Nothing)
                AsyncLoader(False)
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPortfolioRevaluation.ActionsOnControls
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
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetChangePlate
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.PortfolioRevaluation.Id),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.PortfolioRevaluation.Id & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.PortfolioRevaluation.Id),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.PortfolioRevaluation.Id)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.PortfolioRevaluation.Id)
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
        INDlyDepreciation.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        'DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()

    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True

        'Consulta si ya hay depreciación para el mes y el año seleccionado
        Dim revaluationXpo = Presenter.GetPortfolioRevaluationByMonthAndYear(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear)
        Me.PortfolioRevaluation = revaluationXpo
        If revaluationXpo Is Nothing Then 'Si no hay depreciaciones con el mes y el año seleccionado
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Guardar, "Revalorizar")
            Me.BarraBotones.StatusRecord = "1"
            RemoveControls()
            Mensaje(EeventViewerImages.Advertencia) = "No hay registrada depreciación para el mes y el año seleccionado"
            Exit Sub
        End If
        If revaluationXpo.Portfolio_Revaluation IsNot Nothing AndAlso revaluationXpo.Portfolio_Revaluation.Count > 0 Then

            'Se crea el diccionario para agrupar los libros del detalle de la depreciación
            Dim dictionaryLegalBook As New Dictionary(Of Integer, String)
            revaluationXpo.Portfolio_Revaluation.ToList.ForEach(Sub(item)
                                                                    If Not dictionaryLegalBook.ContainsKey(item.CurrencyConverterId.Id) Then
                                                                        dictionaryLegalBook.Add(item.CurrencyConverterId.Id, item.CurrencyConverterId.CurrencyName)
                                                                    End If
                                                                End Sub)

            'Se genera el listado que van en las rejillas
            GenerateList(revaluationXpo.Portfolio_Revaluation.ToList)

            'Verificamos cuantos libros hay registrados para asi mismo crear los tabs con sus rejillas en el form en tiempo de ejecución
            CreateTabs(dictionaryLegalBook)

            'Se modifica la barra de botones
            If revaluationXpo.Status = 1 Then 'Si esta registrada la depreciación
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Guardar, "Revalorizar")
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.GuardarConfirmar, "Confirmar")
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            End If

            Me.BarraBotones.StatusRecord = revaluationXpo.Status.ToString
            'Report
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
            Me.BarraBotones.PrintReport(PrintReportAction.None, Me.PortfolioRevaluation.Id, 0, CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear, Me.BarraBotones.OperatingUnit.Id)
        End If
    End Sub

    ''' <summary>
    ''' Método que remuve controles del layout principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveControls()
        'Se elimina el tabGroup principal que contiene todo
        INDlygDepreciation.BeginUpdate()
        If INDlygDepreciation.Items.ItemCount > 1 Then
            INDlygDepreciation.Items.RemoveAt(1)
        End If
        INDlygDepreciation.EndUpdate()

        'Se recorren los gridControls que hayan en el layout principal y se eliminan para que posteriormente se generen denuevo
        For i As Integer = 0 To INDlyDepreciation.Controls.Count - 1
            If i <= (INDlyDepreciation.Controls.Count - 1) AndAlso TypeOf INDlyDepreciation.Controls(i) Is DevExpress.XtraGrid.GridControl Then
                INDlyDepreciation.Controls.RemoveAt(i)
                i -= 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que genera el listado que van en las rejillas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateList(List As List(Of PortfolioRevaluationDetailXpo))
        'Se instancia el listado para asignarle los valores correspondientes a los libros
        ListRevaluationDetail = New List(Of PortfolioRevaluationDetailXpo)
        ListRevaluationDetail = List
    End Sub

    ''' <summary>
    ''' Método que crea los tabs con sus rejillas para pintar los registros de los libros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateTabs(dictionaryLegalBook As Dictionary(Of Integer, String))
        INDlyDepreciation.BeginUpdate()

        'Se eliminan los controles
        RemoveControls()

        Dim TabbedControlGroup1 As New DevExpress.XtraLayout.TabbedControlGroup()
        CType(TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        INDlygDepreciation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabbedControlGroup1})

        'Se crea el tabGroup en donde se ubican los tabs
        TabbedControlGroup1.Location = New System.Drawing.Point(0, 100)
        'TabbedControlGroup1.Name = "TabbedControlGroup1"
        TabbedControlGroup1.SelectedTabPageIndex = 1
        TabbedControlGroup1.Size = New System.Drawing.Size(1114, 435)
        AddHandler TabbedControlGroup1.SelectedPageChanged, AddressOf SelectedPageChanged

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

            INDlyDepreciation.Controls.Add(GridControl)

            'Se crean las columnas de la rejilla
            Dim gcDocumentType As New DevExpress.XtraGrid.Columns.GridColumn()
            gcDocumentType.Caption = "Documento"
            gcDocumentType.OptionsColumn.AllowEdit = False
            gcDocumentType.OptionsColumn.AllowFocus = False
            gcDocumentType.Visible = True
            gcDocumentType.VisibleIndex = 0
            gcDocumentType.FieldName = "DocumentName"

            Dim GridColumn2 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn2.Caption = "Número Documento"
            GridColumn2.OptionsColumn.AllowEdit = False
            GridColumn2.OptionsColumn.AllowFocus = False
            GridColumn2.Visible = True
            GridColumn2.VisibleIndex = 0
            GridColumn2.FieldName = "DocumentNumber"

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
            view.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {gcDocumentType, GridColumn2, GridColumn12, GridColumn6, GridColumn7, GridColumn8, GridColumn9, GridColumn10, GridColumn11, GridColumn99, GridColumn4, GridColumn3})
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
            GridControl.DataSource = revaluationXpo.Portfolio_Revaluation.ToList.Where(Function(item) item.CurrencyConverterId.Id = itemLegalBook.Key).ToList
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
        INDlyDepreciation.EndUpdate()
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

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        PortfolioRevaluation = Nothing
        SearchMode = Nothing
        Presenter = Nothing
        ListRevaluationDetail = Nothing
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
    Private Sub FrmRevaluation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyDepreciation, True)
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PPortfolioRevaluation(Me)
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
        SearchMode = False
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
    Private Sub FrmRevaluation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRevaluation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CtrDateNavigator1.Focus()
    End Sub

#End Region

#Region "MenuContext"

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
        varImp = 1
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

End Class
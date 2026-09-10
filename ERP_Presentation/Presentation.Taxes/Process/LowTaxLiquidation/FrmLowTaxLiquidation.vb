'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Jhossept Kevin Garay
' Created          : 26-08-2016
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
Imports Presentation.Taxes.MVP
Imports Presentation.Accounting.MVP

#End Region

Public Class FrmLowTaxLiquidation
    Implements ILowTaxLiquidation


#Region "EVENTS"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddLowTaxLiqDetail(sender As Object, e As AddLowTaxLiquidationEventArgs)
#End Region

#Region "BUILDER"
    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrTotalInfo()
        ctrTmp.SetInfoFunction(AddressOf getInfolowTaxLiquidation)
        ctrTmp.PrintInfo()
        ctrTmp.MaskTotalValue = "c0"
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    ''' <summary>
    ''' retorna la informacion que se establece en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfolowTaxLiquidation() As Tuple(Of String)
        If listLowTaxLiquidationDetail IsNot Nothing AndAlso listLowTaxLiquidationDetail.Count > 0 Then
            _totalValue = listLowTaxLiquidationDetail.Sum(Function(x) x.TotalValueTax)
        Else
            _totalValue = 0
        End If
        Return New Tuple(Of String)(_totalValue.ToString("c0"))
    End Function
#End Region

#Region "GLOBALS"

    Dim ctrTmp As CtrTotalInfo

    Dim _totalValue As Decimal = 0

    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    ' Private record As BlockRecordTaxes

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.InventorySequence

    ''' <summary>
    ''' presenter de remision de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PLowTaxLiquidation
    ''' <summary>
    ''' entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim lowTaxLiquidation As LowTaxLiquidation
    ''' <summary>
    ''' listado del detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim listLowTaxLiquidationDetail As List(Of LowTaxLiquidationDetail)

    Dim UVTValue As Decimal = 0

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
#End Region

#Region "PROPERTIES"

    Dim _listTypes As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListTypes As List(Of Tuple(Of Byte, String))
        Get
            If _listTypes Is Nothing Then
                _listTypes = New List(Of Tuple(Of Byte, String))
                _listTypes.Add(New Tuple(Of Byte, String)(1, "Impuesto Publicidad Exterior Visual y avisos"))
                _listTypes.Add(New Tuple(Of Byte, String)(2, "Impuesto Publicidad visual Móvil"))
                _listTypes.Add(New Tuple(Of Byte, String)(3, "Impuesto de Circulación y Tránsito"))
                _listTypes.Add(New Tuple(Of Byte, String)(4, "Impuesto de Azar y Juegos Permitidos"))
                _listTypes.Add(New Tuple(Of Byte, String)(5, "Impuesto de Espectáculos Públicos"))
                _listTypes.Add(New Tuple(Of Byte, String)(6, "Impuesto de Delineación urbana"))
                _listTypes.Add(New Tuple(Of Byte, String)(7, "Impuesto de Delineación urbana"))
                _listTypes.Add(New Tuple(Of Byte, String)(8, "Impuesto por Ocupación de Vías, plazas y lugares públicos"))
                _listTypes.Add(New Tuple(Of Byte, String)(9, "Impuesto de Registro de Patentes, Marcas y Herretes"))
                _listTypes.Add(New Tuple(Of Byte, String)(10, "Impuesto de Degüello de Ganado"))
                _listTypes.Add(New Tuple(Of Byte, String)(11, "Impuesto Sobretasa a la Gasolina a Motor"))
                _listTypes.Add(New Tuple(Of Byte, String)(12, "Participación en Plusvalía en el Municipio de Neiva"))
            End If
            Return _listTypes
        End Get
    End Property

    Public Property Consecutive As String Implements ILowTaxLiquidation.Consecutive
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.EditValue
            End If
        End Get
        Set(value As String)
            INDBteCode.EditValue = value
        End Set
    End Property
     

    Public Property DocumentDate As DateTime
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As DateTime)
            INDDteDate.EditValue = value
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements ILowTaxLiquidation.ActionsOnControls
        Set(value As Boolean)
            INDlcLowTaxLiquidation.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = False
            INDsleThirdParty.Enabled = value
            INDMeDetail.Enabled = value
            INDBtnAdd.Enabled = value
            INDgleTaxType.Enabled = value
            INDlcLowTaxLiquidation.EndUpdate()
            If value = False Then
                INDBteCode.Focus()
            Else
                INDgleTaxType.Focus()
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ILowTaxLiquidation.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements ILowTaxLiquidation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdParty As XPInstantFeedbackSource Implements ILowTaxLiquidation.ThirdParty
        Get
            Return INDsleThirdParty.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "CRUD"
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        'OpenSearch()
    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If _searchMode = False Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            End If
        End If
    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If lowTaxLiquidation IsNot Nothing AndAlso lowTaxLiquidation.Status < 3 Then
            If ValidateControls() = True Then
                If INDGvLowTaxLiquidation.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddRemissionDetail", MODULE_NAME)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MLowTaxesLiquidation(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveTaxesLiquidation(lowTaxLiquidation, 1)
                AsyncLoader(False)
                If result.StateResult = True Then
                    If lowTaxLiquidation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        ' Se descarta la secuencia numerica usada
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Consecutive)
                    Else
                        If lowTaxLiquidation.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.lowTaxLiquidation = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, lowTaxLiquidation.Id, 0, lowTaxLiquidation.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, lowTaxLiquidation.Id, 0, lowTaxLiquidation.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, lowTaxLiquidation.Id, 0, lowTaxLiquidation.Id)
                    End Select

                    _searchMode = False
                    Me.Deshacer()
                Else
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If lowTaxLiquidation.Id > 0 Then
                        lowTaxLiquidation = Await model.GetLowTaxLiquidation(INDBteCode.Text)
                    Else
                        lowTaxLiquidation = New LowTaxLiquidation
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException
    End Sub

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        NewlowTaxLiquidation()
    End Sub

    ''' <summary>
    ''' metodo para guardar y confirmar o para actualizar y confirmar
    ''' </summary>
    ''' <param name="action"></param>
    ''' <remarks></remarks>
    Private Async Sub SaveOrUpdateAndConfirm(action As Integer)
        If ValidateControls() = True Then
            If INDGvLowTaxLiquidation.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddRemissionDetail", MODULE_NAME)
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            Using model As New MLowTaxesLiquidation(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveTaxesLiquidation(lowTaxLiquidation, 2)
                AsyncLoader(False)
                If result.StateResult = True And result.StateResultAux = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    lowTaxLiquidation = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, lowTaxLiquidation.Id, 0, lowTaxLiquidation.Id)
                    _searchMode = False
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    lowTaxLiquidation = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, lowTaxLiquidation.Id, 0, lowTaxLiquidation.Id)
                    _searchMode = False
                    Me.Deshacer()
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    If result.MessageResult IsNot Nothing Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If lowTaxLiquidation.Id > 0 Then
                        lowTaxLiquidation = Await model.GetLowTaxLiquidation(INDBteCode.Text)
                    Else
                        lowTaxLiquidation = New LowTaxLiquidation
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try

    End Sub
#End Region

#Region "METHODS"

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        _searchMode = True
        ' DeleteBlockedRecord()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Consecutivo", .FieldName = "Consecutive", .ColumnWidth = 40}, _
                             New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 90}, _
                              New ColumnInfo With {.Caption = "Nit", .FieldName = "ThirdPartyId.Nit", .ColumnWidth = 100}, _
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "ThirdPartyId.Name", .ColumnWidth = 250}
                             }.ToList()
            .ValorSolicitado = "Consecutive"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListLowTaxesLiquidation
            .FiltroBusqueda = BarraBotones.OperatingUnitValue.ToString()
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub


    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDBteCode.EditValue = ReturnValue
        If INDBteCode.EditValue <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvLowTaxLiquidation, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvLowTaxLiquidation.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    'Private Async Sub DeleteBlockedRecord()
    '    If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
    '        Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
    '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
    '            Await model.DeleteBlockRecord(record)
    '            record = Nothing
    '        End Using
    '    Else
    '        Me.BarraBotones.EnableBarItems()
    '    End If
    'End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        'Using model As New MBlockRecordAndSequense(MyTag)
        '    Dim result = Await model.GetBlockRecord(Me.Tag, Me.lowTaxLiquidation.Id)
        '    If result IsNot Nothing AndAlso result.Id = 0 Then
        '        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '        state.State = Domain.Base.Entities.ObjectState.Added
        '        record = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.lowTaxLiquidation.Id}
        '        Dim operation = Await model.SaveBlockRecord(record)
        '        record = operation.ObjectEmbbeded
        '    Else
        '        Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
        '        record = result
        '        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '    End If
        'End Using
    End Sub

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        'Dim content = String.Format(ResourceManager.GetString("FrmLowTaxLiquidation_IndexContent", MODULE_NAME), lowTaxLiquidation.Code, If(INDSleSupplierDistributionLine.Text = String.Empty, INDSleSupplierDistributionLine.Properties.NullText, INDSleSupplierDistributionLine.Text), RemissionDate, If(INDSleWareHouse.Text = String.Empty, INDSleWareHouse.Properties.NullText, INDSleWareHouse.Text))
        'Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = content, _
        '        .CreationDate = dateServer, _
        '        .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName, _
        '        .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity = "$#" & Me.Tag & "_" & Me.lowTaxLiquidation.Code & "#$", _
        '        .IdForm = Me.Tag, _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.lowTaxLiquidation.Code), _
        '        .Update = dateServer,
        '        .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
        '    Me._doc.Content = content
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.lowTaxLiquidation.Code)
        '    Return Me._doc
        'End If
    End Function

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewlowTaxLiquidation()
        Me.lowTaxLiquidation = New LowTaxLiquidation()
        Me.ActionsOnControls = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        DocumentDate = GetDateServer()
        Me.BarraBotones.StatusRecordVisible = True
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddlowTaxLiquidationDetail(sender As Object, e As AddLowTaxLiquidationEventArgs)

        INDGcLowTaxLiquidation.DataSource = Nothing
        INDGcLowTaxLiquidation.DataSource = listLowTaxLiquidationDetail
        ctrTmp.PrintInfo()


    End Sub


    ''' <summary>
    ''' carga los controles con la informacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        Using model As New MLowTaxesLiquidation(MyTag)
            INDlcLowTaxLiquidation.BeginUpdate()
            AsyncLoader(True)
            lowTaxLiquidation = Await model.GetLowTaxLiquidation(INDBteCode.EditValue)
            If lowTaxLiquidation IsNot Nothing AndAlso lowTaxLiquidation.Id > 0 Then
                With lowTaxLiquidation
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                    'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                    'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    Me.BarraBotones.StatusRecordVisible = True
                    BarraBotones.OperatingUnitValue = .OperatingUnitId
                    INDBteCode.EditValue = .Consecutive
                    INDDteDate.EditValue = .DocumentDate
                    INDgleTaxType.EditValue = .Type
                    INDMeDetail.Text = .Detail
                    listLowTaxLiquidationDetail = .LowTaxLiquidationDetail.ToList
                    INDGcLowTaxLiquidation.DataSource = listLowTaxLiquidationDetail
                    INDsleThirdParty.EditValue = .ThirdPartyId
                    INDsleThirdParty.Properties.NullText = .ThirdParty.Nit & " - " & .ThirdParty.Name
                    _idOperativeUnit = .OperatingUnitId
                    BarraBotones.StatusRecord = .Status.ToString()
                    If .Status = 2 Then
                        Me.BarraBotones.PrintReport(PrintReportAction.None, .Id, Me.Tag, .Id)
                        '   Me.BarraBotones.PrintReport(PrintReportAction.None, lowTaxLiquidation.Id, 0, lowTaxLiquidation.Id)
                        INDlbInvoice.Text = .InvoiceNumber
                        INDlyiInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        INDlbInvoice.Text = String.Empty
                        INDlyiInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If
                End With

                'BarraBotones.SetDocuments(lowTaxLiquidation.Id)
                'Me.GetDocumentIndexed(MyTag & "_" & lowTaxLiquidation.Code)
                'GenerateBlockRecord()


                AsyncLoader(False)
                ActionsOnControls = True
                INDBtnAdd.Enabled = True
                If lowTaxLiquidation.Status <> 1 Then
                    INDBtnAdd.Enabled = False
                End If
                Select Case lowTaxLiquidation.Status
                    Case 1
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    Case Else
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        ReadOnlyControls(True)
                End Select
                INDGcLowTaxLiquidation.DataSource = Nothing
                INDGcLowTaxLiquidation.DataSource = listLowTaxLiquidationDetail
                INDDteDate.Focus()

                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                'Me.BarraBotones.PrintReport(PrintReportAction.None, lowTaxLiquidation.Id, 0, lowTaxLiquidation.Id)
                ctrTmp.PrintInfo()
            Else
                AsyncLoader(False)
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                INDBteCode.EditValue = String.Empty
                Deshacer()
                INDBteCode.Focus()
            End If
            INDlcLowTaxLiquidation.EndUpdate()
        End Using
    End Function

    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlcLowTaxLiquidation.BeginUpdate()
        ReadOnlyControls(False)
        INDlbInvoice.Text = String.Empty
        INDlyiInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        BarraBotones.CleanAuditBasic()
        INDBteCode.EditValue = String.Empty
        INDDteDate.EditValue = Nothing
        INDgleTaxType.EditValue = Nothing
        INDsleThirdParty.EditValue = Nothing
        INDsleThirdParty.Properties.NullText = String.Empty
        INDMeDetail.EditValue = String.Empty
        INDGcLowTaxLiquidation.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcLowTaxLiquidation)
        listLowTaxLiquidationDetail = Nothing
        lowTaxLiquidation = Nothing
        ActionsOnControls = False
        'ctrTmp = New CtrTotalInfo()
        'ctrTmp.SetInfoFunction(AddressOf getInfolowTaxLiquidation)
        ctrTmp.PrintInfo()
        INDlcLowTaxLiquidation.EndUpdate()
    End Sub

    ''' <summary>
    ''' asigna los valroes a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With lowTaxLiquidation
            .Detail = INDMeDetail.Text
            .DocumentDate = INDDteDate.EditValue
            .Type = INDgleTaxType.EditValue
            .ThirdPartyId = INDsleThirdParty.EditValue
            .OperatingUnitId = _idOperativeUnit
            .Status = 1
            .UVTValue = UVTValue
            For Each item In listLowTaxLiquidationDetail
                .LowTaxLiquidationDetail.Add(item)
            Next

        End With
    End Sub

    ''' <summary>
    ''' Controla el boton de agregar producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateEditValue()
        'If INDSleSupplierDistributionLine.EditValue IsNot Nothing AndAlso INDSleWareHouse.EditValue IsNot Nothing Then
        '    INDBtnAdd.Enabled = True
        '    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        'Else
        '    INDBtnAdd.Enabled = False
        '    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        'End If
    End Sub

    Dim indexEditRecord As Integer = -1
    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        Dim LowTaxLiquidationDetail = DirectCast(INDGvLowTaxLiquidation.GetFocusedRow(), LowTaxLiquidationDetail)
        indexEditRecord = listLowTaxLiquidationDetail.IndexOf(LowTaxLiquidationDetail)
        Using formulario As New FrmPopupConcepts
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddLowTaxliquidationDetail, AddressOf ReturnAddlowTaxLiquidationDetail
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.UVTValue = UVTValue
            formulario.ListDetails = listLowTaxLiquidationDetail
            formulario.lowTaxLiquidationDetailEdit = LowTaxLiquidationDetail
            formulario.EditMode = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub


    ''' <summary>
    ''' listado del detalle de prestamo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listlowTaxLiquidationDetailDelete As List(Of LowTaxLiquidationDetail)

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim LowTaxLiquidationDetail As LowTaxLiquidationDetail = DirectCast(INDGvLowTaxLiquidation.GetFocusedRow(), LowTaxLiquidationDetail)
            If LowTaxLiquidationDetail.Id > 0 Then
                If listlowTaxLiquidationDetailDelete Is Nothing Then
                    listlowTaxLiquidationDetailDelete = New List(Of LowTaxLiquidationDetail)
                End If
                LowTaxLiquidationDetail.MarkAsDeleted()
                listlowTaxLiquidationDetailDelete.Add(LowTaxLiquidationDetail)
            End If
            listLowTaxLiquidationDetail.Remove(LowTaxLiquidationDetail)
            INDGcLowTaxLiquidation.DataSource = Nothing
            INDGcLowTaxLiquidation.DataSource = listLowTaxLiquidationDetail
            ctrTmp.PrintInfo()
        End If
    End Sub

#End Region

#Region "HANDLES"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        _totalValue = Nothing
        _idOperativeUnit = Nothing
        _searchMode = Nothing
        _sequense = Nothing
        presenter = Nothing
        lowTaxLiquidation = Nothing
        listLowTaxLiquidationDetail = Nothing
        UVTValue = Nothing
        varImp = Nothing
    End Sub

    Private Sub FrmLowTaxLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlcLowTaxLiquidation, True)
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        presenter = New PLowTaxLiquidation(Me)
        presenter.LoadDefinitionLayout()
        ' presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        AddActionsColumns()
        Deshacer()
        LoadStatus()
        _searchMode = False
        INDgleTaxType.Properties.DataSource = ListTypes
        'valor del UVT
        loadUVTValue()
    End Sub

    Async Function loadUVTValue() As Task
        Using model As New MCompanySettings(Me.Tag)
            Dim companySetting = Await model.GetCompanySettings()
            UVTValue = companySetting.UVT
        End Using
    End Function
#End Region

#Region "Activated"
    Private Sub FrmLowTaxLiquidation_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmLowTaxLiquidation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'DeleteBlockedRecord()
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If listLowTaxLiquidationDetail Is Nothing Then listLowTaxLiquidationDetail = New List(Of LowTaxLiquidationDetail)
        Using formulario As New FrmPopupConcepts
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddLowTaxliquidationDetail, AddressOf ReturnAddlowTaxLiquidationDetail
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ListDetails = listLowTaxLiquidationDetail
            formulario.UVTValue = UVTValue
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If INDsleThirdParty.Properties.DataSource Is Nothing Then
            presenter.InitializeThirdParty()
        End If
    End Sub

    'Private Sub INDgleTaxType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDgleTaxType.QueryPopUp
    '    If INDgleTaxType.Properties.DataSource Is Nothing Then
    '        INDgleTaxType.Properties.DataSource = ListTypes
    '    End If
    'End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub
#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "KeyDown"
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If String.IsNullOrEmpty(INDBteCode.Text.Trim()) Then
                Me.NewlowTaxLiquidation()
            Else
                Await Me.LoadControls()
            End If
        End If
    End Sub

    Private Sub INDMeDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub
#End Region

#Region "EditValueChanged"


#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.lowTaxLiquidation IsNot Nothing AndAlso Me.lowTaxLiquidation.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                ' DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pitar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLowTaxLiquidation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBteCode.Focus()
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.InventorySequenceDetail IsNot Nothing Then
            If Me._sequense.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.InventorySequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
        If operatingUnit IsNot Nothing Then
            _idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            lowTaxLiquidation.Status = 3
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, lowTaxLiquidation.Id, 0, lowTaxLiquidation.Id)
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveOrUpdateAndConfirm(1)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveOrUpdateAndConfirm(2)
    End Sub
#End Region

    Private Sub INDGvLowTaxLiquidation_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvLowTaxLiquidation.CustomColumnDisplayText
        If e.Value IsNot Nothing Then
            Select Case e.Column.FieldName
                Case Is = "Concept"
                    Select Case e.Value
                        Case Is = 1
                            e.DisplayText = "Pasacalles"
                        Case Is = 2
                            e.DisplayText = "Avisos no adosados a la pared inferior a 8 mestros cuadrados"
                        Case Is = 3
                            e.DisplayText = "Pendones y festones"
                        Case Is = 4
                            e.DisplayText = "Afiches y Volantes"
                    End Select
                Case Is = "NumberOfDays"
                    Dim _concept = INDGvLowTaxLiquidation.GetRowCellValue(e.ListSourceRowIndex, "Concept")
                    If _concept = 2 Then
                        e.DisplayText = CInt(CInt(e.Value) / 30) & "  meses"
                    Else
                        e.DisplayText = e.Value & "  días"
                    End If
            End Select
        End If
    End Sub


    Private Sub INDgleTaxType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleTaxType.EditValueChanged
        If INDgleTaxType.EditValue IsNot Nothing AndAlso INDgleTaxType.EditValue = 1 Then
            INDBtnAdd.Enabled = True
        Else
            INDBtnAdd.Enabled = False
        End If
    End Sub





End Class
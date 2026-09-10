'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Ernesto Cordoba
' Created          : 13-07-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Portfolio.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports DevExpress.Spreadsheet
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

#End Region

Public Class FrmHardPortfolioCollection
    Implements IHardCollection, ICustomizableForm

#Region "GLOBALS"
    Dim presenter As PHardCollection

    Dim hardCollection As HardCollection

    Dim listHardCollection As List(Of HardCollectionDetail)

    Dim listHardCollectionDelete As List(Of HardCollectionDetail)
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Portfolio"
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPortfolio
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing
#End Region

#Region "PROPERTIES"
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements IHardCollection.ActionsOnControls
        Set(value As Boolean)
            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = value
            INDPceInvoice.Enabled = value
            INDGcInvoice.Enabled = value
            INDEsbInvoice.Enabled = value
            INDBtnImportFileInvoice.Enabled = value
            If Not value Then
                INDBteCode.Focus()
            Else
                INDDteDate.Focus()
            End If
        End Set
    End Property

    Public Property Code As String Implements IHardCollection.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Public Property DocumentDate As Date Implements IHardCollection.DocumentDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As Date)
            INDDteDate.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IHardCollection.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IHardCollection.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequense As PortfolioSequence Implements IHardCollection.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property BillsXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleInvoice.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleInvoice.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "CRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        If hardCollection IsNot Nothing AndAlso hardCollection.Status < 3 Then
            If Not ValidateControls() Then
                Exit Sub
            End If
            If INDGvInvoice.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar minimo una factura"
                Exit Sub
            End If
            AssigningValues()

        End If
        Execute()
    End Sub

    ''' <summary>
    ''' metdo para ejecutar el proceso de guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub Execute()
        Try
            Using model As New MHardCollection(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveHardCollection(hardCollection)
                Select Case result.StatusCode
                    Case eStatusResult.SUCCESS
                        If hardCollection.ChangeTracker.State = ObjectState.Added Then
                            If Not Me._sequence.Sequential Then
                                Me.DicSequense(_idCurrentSequence).RemoveAt(0)
                            End If
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                        Else
                            If hardCollection.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = result.Message
                            Else
                                Mensaje(EeventViewerImages.Informacion) = result.Message
                            End If
                        End If
                        AsyncLoader(False)
                        Me.Deshacer()
                    Case eStatusResult.WARNING
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Case eStatusResult.EXCEPTION
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                End Select
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewHardCollection()
        End If
    End Sub

    Private Sub SaveAndConfirm()
        If Not ValidateControls() Then
            Exit Sub
        End If
        If INDGvInvoice.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar minimo una factura"
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            AssigningValues()
            hardCollection.Status = 2
            Execute()
        End If
    End Sub
#End Region

#Region "METHODS"
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Registrado", 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = 200, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListHardCollection
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
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)

        IndigoGridView1.SetListAcction(INDGvInvoice, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvInvoice.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewHardCollection() As Task
        hardCollection = New HardCollection()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"

        'Me.hardCollection = New HardCollection
        'If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '    Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail(0).Id
        'ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '    If Me._sequence.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '        Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '        Exit Sub
        '    End If
        'End If
        'If Not Me._sequence.Sequential Then
        '    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '        If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        Else
        '            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '            End Using
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '            End If
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'Else
        '    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'End If
        'BarraBotones.StatusRecordVisible = True
        'BarraBotones.StatusRecord = "1"
    End Function

    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.hardCollection.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordPortfolio With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me.hardCollection.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    Private Sub CleanControls()
        INDLcMain.BeginUpdate()
        ReadOnlyControls(False)
        INDBteCode.EditValue = Nothing
        INDDteDate.EditValue = Nothing
        INDSleInvoice.EditValue = Nothing
        INDGcInvoice.DataSource = Nothing
        hardCollection = Nothing
        listHardCollection = Nothing
        listHardCollectionDelete = Nothing
        ActionsOnControls = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        Me.BarraBotones.ReassignOperatingUnit()
        INDLcMain.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MHardCollection(CStr(Me.Tag))
                    AsyncLoader(True)
                    hardCollection = Await Model.GetHardCollectionByCode(INDBteCode.Text.Trim)
                    INDLcMain.BeginUpdate()
                    If hardCollection IsNot Nothing AndAlso hardCollection.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(hardCollection.Id))
                            listHardCollection = Model.GetHardCollectionDetail(hardCollection.Id)
                            With hardCollection
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.StatusRecordVisible = True
                                BarraBotones.OperatingUnitValue = .OperatingUnitId

                                Code = .Code
                                DocumentDate = .DocumentDate
                                BarraBotones.StatusRecord = .Status.ToString()
                                INDGcInvoice.DataSource = listHardCollection
                                INDGcInvoice.RefreshDataSource()
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.hardCollection.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = hardCollection.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(hardCollection.Id, Me.Tag.ToString(), Nothing, GetType(HardCollection).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            Select Case hardCollection.Status
                                Case 1
                                    'Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                Case Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    ReadOnlyControls(True)
                                    INDBtnImportFileInvoice.Enabled = False
                            End Select
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewHardCollection()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcMain.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If



        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If
        'Using model As New MHardCollection(MyTag)
        '    INDLcMain.BeginUpdate()
        '    AsyncLoader(True)
        '    hardCollection = Await model.GetHardCollectionByCode(Code)

        '    If hardCollection IsNot Nothing AndAlso hardCollection.Id > 0 Then
        '        listHardCollection = model.GetHardCollectionDetail(hardCollection.Id)
        '        With hardCollection
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
        '            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
        '            Me.BarraBotones.StatusRecordVisible = True
        '            BarraBotones.OperatingUnitValue = .OperatingUnitId

        '            Code = .Code
        '            DocumentDate = .DocumentDate
        '            BarraBotones.StatusRecord = .Status.ToString()
        '            INDGcInvoice.DataSource = listHardCollection
        '            INDGcInvoice.RefreshDataSource()
        '        End With
        '        AsyncLoader(False)
        '        ActionsOnControls = True
        '        Select Case hardCollection.Status
        '            Case 1
        '                'Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
        '                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        '            Case Else
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                ReadOnlyControls(True)
        '                INDBtnImportFileInvoice.Enabled = False
        '        End Select
        '        INDDteDate.Focus()
        '        GenerateBlockRecord()
        '    Else
        '        AsyncLoader(False)
        '        Me.Mensaje(EeventViewerImages.Advertencia) = "La cuenta de dificil recaudo no existe"
        '        Code = String.Empty
        '        Deshacer()
        '        INDBteCode.Focus()
        '    End If
        'End Using
        'INDLcMain.EndUpdate()
        'BarraBotones.OperatingUnitValue = .OperatingUnitId
    End Function

    Private Sub AssigningValues()
        With hardCollection
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            For Each item In listHardCollection
                .HardCollectionDetail.Add(item)
            Next
            If listHardCollectionDelete IsNot Nothing AndAlso listHardCollectionDelete.Count > 0 Then
                For Each item In listHardCollectionDelete
                    .HardCollectionDetail.Add(item)
                Next
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(1)})
                                              End SyncLock
                                          End Sub)
    End Sub
#End Region

#Region "HANDLES"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        hardCollection = Nothing
        listHardCollection = Nothing
        listHardCollectionDelete = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        rows = Nothing
        listRows = Nothing
        myStream = Nothing
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.hardCollection IsNot Nothing AndAlso Me.hardCollection.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
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

    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#Region "Load"
    Private Sub FrmHardPortfolioCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDEsbInvoice.AddRangeColumns("Factura")
        _indigoSession = SessionValues.Instance
        AddActionsColumns()
        presenter = New PHardCollection(Me)
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Deshacer()
        LoadStatus()
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleInvoice_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleInvoice.QueryPopUp
        If BillsXPO Is Nothing Then
            Using model As New MHardCollection(MyTag)
                BillsXPO = model.ListBillsHardCollection()
            End Using
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If INDSleInvoice.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una factura"
            Exit Sub
        End If
        Using model As New MHardCollection(MyTag)
            If listHardCollection Is Nothing Then
                listHardCollection = New List(Of HardCollectionDetail)
            End If

            Dim invoiceTmp = model.GetPortfolioAccountReceivableById(INDSleInvoice.EditValue)
            Dim invoiceAdded = listHardCollection.Find(Function(x) x.AccountReceivableId = invoiceTmp.Id)
            If invoiceAdded IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "La factura seleccionada ya esta agregada"
                Exit Sub
            End If
            Dim hardCollectionDetail = New HardCollectionDetail
            With hardCollectionDetail
                .AccountReceivableId = invoiceTmp.Id
                .Balance = invoiceTmp.Balance
                .InvoiceNumber = invoiceTmp.InvoiceNumber
            End With

            listHardCollection.Add(hardCollectionDetail)
            INDGcInvoice.DataSource = listHardCollection
            INDGcInvoice.RefreshDataSource()
            INDSleInvoice.EditValue = Nothing
            INDSleInvoice.Focus()
        End Using
    End Sub
#End Region

#Region "KeyDown"
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewHardCollection()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "ContexMenuActions - Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If hardCollection.Status > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el documento"
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim hardCollectionDetail = DirectCast(INDGvInvoice.GetFocusedRow(), HardCollectionDetail)
            If hardCollectionDetail.Id > 0 Then
                If listHardCollectionDelete Is Nothing Then
                    listHardCollectionDelete = New List(Of HardCollectionDetail)
                End If
                listHardCollectionDelete.Add(hardCollectionDetail.MarkAsDeleted())
            End If
            listHardCollection.Remove(hardCollectionDetail)
            INDGcInvoice.DataSource = listHardCollection
            INDGcInvoice.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub
#End Region

#Region "PasteToGrid"
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If hardCollection.Id > 0 AndAlso hardCollection.Status > 1 Then
            Exit Sub
        End If
        AsyncLoader(True)
        Using model As New MHardCollection(MyTag)
            Dim result = Await model.SetCopyPasteOrImportFileHardCollection(Nothing, e.Rows)

            If result.StatusCode = eStatusResult.EXCEPTION Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                AsyncLoader(False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If

            Dim listErrors = New List(Of String)
            If listHardCollection IsNot Nothing AndAlso listHardCollection.Count > 0 Then
                For Each item In result.ObjectEmbbeded

                    Dim invoiceAdded = listHardCollection.Find(Function(x) x.AccountReceivableId = item.AccountReceivableId)
                    If invoiceAdded IsNot Nothing Then
                        result.MessageResult.Add("La factura " + item.InvoiceNumber + ", ya se encuantra agregada")
                        Continue For
                    End If
                    listHardCollection.Add(item)
                Next
            Else
                listHardCollection = result.ObjectEmbbeded
            End If

            INDGcInvoice.DataSource = listHardCollection
            INDGcInvoice.RefreshDataSource()
            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Using
        AsyncLoader(False)
    End Sub
#End Region

#Region "ImportFile"
    Private Async Sub INDBtnImportFileInvoice_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileInvoice.Click
        If hardCollection.Id > 0 AndAlso hardCollection.Status > 1 Then
            Exit Sub
        End If
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                    sddf.AllowDrop = False
                    sddf.LoadDocument(myStream)
                    Dim workBook As IWorkbook = sddf.Document

                    rows = workBook.Worksheets(0).Rows
                    If rows.LastUsedIndex = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    Using model As New MHardCollection(MyTag)
                        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()

                        SetRow(1, rows.LastUsedIndex + 1)

                        Dim result = Await model.SetCopyPasteOrImportFileHardCollection(listRows.ToList(), Nothing)



                        If result.StatusCode = eStatusResult.EXCEPTION Then
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            AsyncLoader(False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If

                        Dim listErrors = New List(Of String)
                        If listHardCollection IsNot Nothing AndAlso listHardCollection.Count > 0 Then
                            For Each item In result.ObjectEmbbeded

                                Dim invoiceAdded = listHardCollection.Find(Function(x) x.AccountReceivableId = item.AccountReceivableId)
                                If invoiceAdded IsNot Nothing Then
                                    result.MessageResult.Add("La factura " + item.InvoiceNumber + ", ya se encuantra agregada")
                                    Continue For
                                End If
                                listHardCollection.Add(item)
                            Next
                        Else
                            listHardCollection = result.ObjectEmbbeded
                        End If

                        INDGcInvoice.DataSource = listHardCollection
                        INDGcInvoice.RefreshDataSource()
                        If result.MessageResult.Count > 0 Then
                            Using formulario As New FrmListErrors(result.MessageResult)
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                transparent.ShowDialog(Me)
                            End Using
                        End If
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                    End Using
                End If
                AsyncLoader(False)
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        hardCollection.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        hardCollection.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PortfolioSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveAndConfirm()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            hardCollection.Status = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveAndConfirm()
    End Sub
#End Region
    
End Class
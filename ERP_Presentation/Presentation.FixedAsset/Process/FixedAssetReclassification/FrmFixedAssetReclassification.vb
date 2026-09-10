'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-06-26
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
Imports DevExpress.Xpo
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports System.Text

#End Region

Public Class FrmFixedAssetReclassification
    Implements IFixedAssetReclassification, ICustomizableForm

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler bwCreateTabs.DoWork, AddressOf bwCreateTabs_DoWork
        AddHandler bwCreateTabs.RunWorkerCompleted, AddressOf bwCreateTabs_RunWorkerCompleted
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetReclassification.MyLayoutControl
        Get
            Return LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetReclassification.MyTag
        Get
            Return Tag
        End Get
    End Property

    ''' <summary>
    ''' Secuencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As FixedAssetSequence Implements IFixedAssetReclassification.Sequense
        Get
            Return _sequence
        End Get
        Set(value As FixedAssetSequence)
            _sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In _sequence.FixedAssetSequenceDetail
                    DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetReclassification.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IFixedAssetReclassification.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de reclasificación (1 - Finalización Contrato Leasing, 2 - Corrección Catálogo, 3 - Corrección Artículo)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReclassificationType As Integer? Implements IFixedAssetReclassification.ReclassificationType
        Get
            Return INDGleReclassificationType.EditValue
        End Get
        Set(value As Integer?)
            INDGleReclassificationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Detalles
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Detail As String Implements IFixedAssetReclassification.Detail
        Get
            Return INDmemoDetail.EditValue
        End Get
        Set(value As String)
            INDmemoDetail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Artículo previo a la reclasificación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemIdPrevious As Integer? Implements IFixedAssetReclassification.ItemIdPrevious
        Get
            Return INDSleItemPrevious.EditValue
        End Get
        Set(value As Integer?)
            INDSleItemPrevious.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del artículo previo a la reclasificación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemPreviousXpo As XPInstantFeedbackSource Implements IFixedAssetReclassification.ItemPreviousXpo
        Get
            Return INDSleItemPrevious.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleItemPrevious.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Catálogo previo a la reclasificación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemCatalogIdPrevious As Integer? Implements IFixedAssetReclassification.ItemCatalogIdPrevious
        Get
            Return INDSleCatalogPrevious.EditValue
        End Get
        Set(value As Integer?)
            INDSleCatalogPrevious.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del catálogo previo a la reclasificación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemCatalogPreviousXpo As XPInstantFeedbackSource Implements IFixedAssetReclassification.ItemCatalogPreviousXpo
        Get
            Return INDSleCatalogPrevious.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCatalogPrevious.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Nuevo valor de Artículo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemId As Integer? Implements IFixedAssetReclassification.ItemId
        Get
            Return INDSleItem.EditValue
        End Get
        Set(value As Integer?)
            INDSleItem.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del artículo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemXpo As XPInstantFeedbackSource Implements IFixedAssetReclassification.ItemXpo
        Get
            Return INDSleItem.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleItem.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Nuevo valor de Catálogo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemCatalogId As Integer? Implements IFixedAssetReclassification.ItemCatalogId
        Get
            Return INDSleCatalog.EditValue
        End Get
        Set(value As Integer?)
            INDSleCatalog.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del catálogo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemCatalogXpo As XPInstantFeedbackSource Implements IFixedAssetReclassification.ItemCatalogXpo
        Get
            Return INDSleCatalog.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCatalog.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private Presenter As PFixedAssetReclassification

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Asyncrono para crear las rejillas correspondientes a los libros
    ''' </summary>
    ''' <remarks></remarks>
    Private bwCreateTabs As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Private FixedAssetReclassification As FixedAssetReclassification

    ''' <summary>
    ''' Entidad xpo para la reclasificación
    ''' </summary>
    ''' <remarks></remarks>
    Private fixedAssetForReclassificationXpo As List(Of ViewListFixedAssetForReclassificationXpo)

    ''' <summary>
    ''' Entidad xpo para la reclasificación
    ''' </summary>
    ''' <remarks></remarks>
    Private fixedAssetForReclassifiedXpo As List(Of ViewListFixedAssetReclassifiedXpo)

    ''' <summary>
    ''' Diccionario para libros contables
    ''' </summary>
    ''' <remarks></remarks>
    Private dictionaryLegalBook As Dictionary(Of Integer, BookXpo)

    ''' <summary>
    ''' Permite identificar si se esta cargando algun registro
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagIsLoad As Boolean = False

#End Region

#Region "Tuples"
    Private ListReclassificationType As List(Of Tuple(Of Integer, String))
#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If _sequence.IsManual Then
            Deshacer()
        Else
            Await NewFixedAssetReclassification()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If FixedAssetReclassification.Status < 3 Then
            Dim errors = ValidateControlsInForm()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
        End If

        Try
            AssigningValues()
            Using model As New MFixedAssetReclassification(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveFixedAssetReclassification(FixedAssetReclassification, _idCurrentSequence)
                If Result.StateResult = True Then
                    FixedAssetReclassification = Result.ObjectEmbbeded
                    If FixedAssetReclassification.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not _sequence.IsManual AndAlso Not _sequence.Sequential Then
                            DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If

                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        BarraBotones.PrintReport(PrintReportAction.Create, FixedAssetReclassification.Id, 0, FixedAssetReclassification.Id)
                    Else
                        If FixedAssetReclassification.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            BarraBotones.PrintReport(PrintReportAction.Cancel, FixedAssetReclassification.Id, 0, FixedAssetReclassification.Id)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            BarraBotones.PrintReport(PrintReportAction.Update, FixedAssetReclassification.Id, 0, FixedAssetReclassification.Id)
                        End If
                    End If
                    UpdateIndexedDocument(BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.StateResult = False And Result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        If FixedAssetReclassification.Status < 3 Then
            Dim errors = ValidateControlsInForm()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
        End If

        Try
            AssigningValues()
            Using model As New MFixedAssetReclassification(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.ConfirmFixedAssetReclassification(FixedAssetReclassification, _idCurrentSequence)
                If Result.StateResult = True Then
                    FixedAssetReclassification = Result.ObjectEmbbeded
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    BarraBotones.PrintReport(PrintReportAction.Confirm, FixedAssetReclassification.Id, 0, FixedAssetReclassification.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListReclassificationType = New List(Of Tuple(Of Integer, String))
        ListReclassificationType.Add(New Tuple(Of Integer, String)(1, "Corrección Catálogo"))
        ListReclassificationType.Add(New Tuple(Of Integer, String)(2, "Corrección Artículo"))
        INDGleReclassificationType.Properties.DataSource = ListReclassificationType
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo() With {.Caption = "Tipo de Reclasificación", .FieldName = "ReclassificationTypeName", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetReclassification
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewFixedAssetReclassification() As Task
        FixedAssetReclassification = New FixedAssetReclassification()
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"
        If _sequence.IsManual Then
            ActionsOnControls = True
            BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If _sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _idCurrentSequence = _sequence.FixedAssetSequenceDetail(0).Id
            ElseIf _sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If _sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = _idOperativeUnit) Then
                    _idCurrentSequence = _sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = _idOperativeUnit).Id
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If _sequence.Sequential Then
                Code = ResourceManager.GetString("LabelOrTextboxNew")
                ActionsOnControls = True
                BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If DicSequense IsNot Nothing AndAlso DicSequense.Count > 0 Then
                    If DicSequense(CInt(_idCurrentSequence)).Count > 0 Then
                        Code = DicSequense(CInt(_idCurrentSequence))(0)
                        ActionsOnControls = True
                        BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Tag))
                            DicSequense(CInt(_idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(_idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If DicSequense(CInt(_idCurrentSequence)) IsNot Nothing AndAlso DicSequense(CInt(_idCurrentSequence)).Count > 0 Then
                            Code = DicSequense(CInt(_idCurrentSequence))(0)
                            ActionsOnControls = True
                            BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Code = ResourceManager.GetString("LabelOrTextboxNew")
                    ActionsOnControls = True
                    BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Try
                Using Model As New MFixedAssetReclassification(CStr(Tag))
                    AsyncLoader(True)
                    Dim tmpFixedAssetReclassification = (Await Model.GetFixedAssetReclassificationByCode(INDbtnCode.Text.Trim)).ObjectEmbbeded

                    INDlyReclassification.BeginUpdate()
                    If tmpFixedAssetReclassification IsNot Nothing AndAlso tmpFixedAssetReclassification.Id > 0 Then
                        BarraBotones.StatusRecordVisible = True

                        'Cargar unidad operativa
                        BarraBotones_ChangueOperatingUnit(New OperatingUnit With {.Id = tmpFixedAssetReclassification.OperatingUnitId})
                        FixedAssetReclassification = tmpFixedAssetReclassification
                        BarraBotones_ChangueOperatingUnit(New OperatingUnit With {.Id = Me.BarraBotones.OperatingUnit.Id})

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Tag), CStr(FixedAssetReclassification.Id))
                            With FixedAssetReclassification
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                FlagIsLoad = True

                                Code = .Code
                                DocumentDate = .DocumentDate
                                ReclassificationType = .ReclassificationType
                                Detail = .Detail
                                BarraBotones.StatusRecord = .Status.ToString

                                'Detalle segun el tipo de reclasificación
                                ItemIdPrevious = .ItemIdPrevious
                                INDSleItemPrevious.Properties.NullText = .ItemPreviousCodeDescription
                                ItemCatalogIdPrevious = .ItemCatalogIdPrevious
                                INDSleCatalogPrevious.Properties.NullText = .ItemCatalogPreviousCodeDescription
                                If ReclassificationType = 2 Then
                                    ItemId = .ItemId
                                    INDSleItem.Properties.NullText = .ItemCodeDescription
                                End If

                                ItemCatalogId = .ItemCatalogId
                                INDSleCatalog.Properties.NullText = .ItemCatalogCodeDescription

                                FlagIsLoad = False
                            End With

                            GetDocumentIndexed(Tag.ToString() & "_" & FixedAssetReclassification.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = indigo.UserIndigoName, .IdForm = Tag, .CodUser = indigo.UserIndigo, .IdRecord = FixedAssetReclassification.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            BarraBotones.SetDocuments(FixedAssetReclassification.Id, Tag.ToString(), Nothing, GetType(FixedAssetReclassification).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If FixedAssetReclassification.Status = 1 Then
                                BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                ReadOnlyControls(True)

                                INDdteDocumentDate.Properties.ReadOnly = False
                                INDmemoDetail.Properties.ReadOnly = False
                                INDSleCatalogPrevious.Properties.ReadOnly = True
                                INDSleCatalog.Properties.ReadOnly = IIf(ReclassificationType = 1, False, True)
                            Else
                                BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If

                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            BarraBotones.PrintReport(PrintReportAction.None, FixedAssetReclassification.Id, 0, FixedAssetReclassification.Id, _idOperativeUnit)
                        End Using
                    Else
                        AsyncLoader(False)
                        If _sequence.IsManual Then
                            Await NewFixedAssetReclassification()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyReclassification.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If FixedAssetReclassification IsNot Nothing AndAlso FixedAssetReclassification.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                INDbtnCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            INDbtnCode.Text = IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = GetDateServer()
        If _doc Is Nothing Then
            _doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), FixedAssetReclassification.Code, FixedAssetReclassification.DocumentDate),
                .CreationDate = dateServer, .CreationUser = indigo.UserIndigo & "-" & indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Tag) & "_" & FixedAssetReclassification.Code & "#$", .IdForm = CStr(Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), FixedAssetReclassification.Code),
                .Update = dateServer, .UpdateUser = indigo.UserIndigo & "-" & indigo.UserIndigoName}
            Return _doc
        Else
            _doc.Update = dateServer
            _doc.UpdateUser = indigo.UserIndigo & "-" & indigo.UserIndigoName
            _doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), FixedAssetReclassification.Code, FixedAssetReclassification.DocumentDate)
            _doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), FixedAssetReclassification.Code)
            Return _doc
        End If
    End Function

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlyReclassification.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        DocumentDate = Nothing
        ReclassificationType = Nothing
        Detail = Nothing
        ItemIdPrevious = Nothing
        INDSleItemPrevious.Properties.NullText = String.Empty
        ItemCatalogIdPrevious = Nothing
        INDSleCatalogPrevious.Properties.NullText = String.Empty
        ItemId = Nothing
        INDSleItem.Properties.NullText = String.Empty
        ItemCatalogId = Nothing
        INDSleCatalog.Properties.NullText = String.Empty
        FixedAssetReclassification = Nothing
        INDSleCatalogPrevious.Properties.ReadOnly = True
        INDlygDetails.HideControl()
        RemoveControls()
        INDlyReclassification.EndUpdate()

        'Recargar la unidad operativa
        If Me.BarraBotones.OperatingUnit IsNot Nothing Then
            BarraBotones_ChangueOperatingUnit(New OperatingUnit With {.Id = Me.BarraBotones.OperatingUnit.Id})
        End If

        _doc = Nothing
        FlagIsLoad = False
        Await DeleteBlockedRecord()
        Me.BarraBotones.ReassignOperatingUnit()
        BarraBotones.CleanAuditBasic()
        BarraBotones.StatusRecordVisible = False
        BarraBotones.EnableBarItems()
        BarraBotones.DisableBarDocument()
        If indigo.UserViewMode AndAlso Not FormSearchObjects.IsDisposed Then
            BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Método que remuve controles del layout principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveControls()
        'Se elimina el tabGroup principal que contiene todo
        INDlygDetails.BeginUpdate()
        If INDlygDetails.Items.ItemCount > 0 Then
            INDlygDetails.Items.RemoveAt(0)
        End If
        INDlygDetails.EndUpdate()

        'Se recorren los gridControls que hayan en el layout principal y se eliminan para que posteriormente se generen denuevo
        For i As Integer = 0 To INDlyReclassification.Controls.Count - 1
            If i <= (INDlyReclassification.Controls.Count - 1) AndAlso TypeOf INDlyReclassification.Controls(i) Is DevExpress.XtraGrid.GridControl Then
                INDlyReclassification.Controls.RemoveAt(i)
                i -= 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetReclassification.ActionsOnControls
        Set(value As Boolean)

            INDlyReclassification.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDGleReclassificationType.Enabled = value
            INDmemoDetail.Enabled = value
            INDSleItemPrevious.Enabled = value
            INDSleCatalogPrevious.Enabled = value
            INDSleItem.Enabled = value
            INDSleCatalog.Enabled = value
            INDlyReclassification.EndUpdate()

            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With FixedAssetReclassification
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = Me._idOperativeUnit
            .Code = Code
            .DocumentDate = DocumentDate
            .ReclassificationType = ReclassificationType
            .Detail = Detail
            .ItemIdPrevious = ItemIdPrevious
            .ItemCatalogIdPrevious = ItemCatalogIdPrevious
            .ItemId = IIf(ReclassificationType = 2, ItemId, Nothing)
            .ItemCatalogId = ItemCatalogId

            If .Id > 0 Then
                .MarkAsModified()
            Else
                .FixedAssetReclassificationDetail.Clear()
                fixedAssetForReclassificationXpo.ForEach(Sub(i)
                                                             Dim FixedAssetReclassificationDetail As FixedAssetReclassificationDetail
                                                             If .FixedAssetReclassificationDetail IsNot Nothing AndAlso .FixedAssetReclassificationDetail.Where(Function(d) d.PhysicalAssetId = i.PhysicalAssetId).Count > 0 Then
                                                                 FixedAssetReclassificationDetail = .FixedAssetReclassificationDetail.Where(Function(d) d.PhysicalAssetId = i.PhysicalAssetId).FirstOrDefault
                                                             Else
                                                                 FixedAssetReclassificationDetail = New FixedAssetReclassificationDetail
                                                                 FixedAssetReclassificationDetail.PhysicalAssetId = i.PhysicalAssetId
                                                                 FixedAssetReclassificationDetail.Status = i.Status
                                                                 FixedAssetReclassificationDetail.AdquisitionType = i.AdquisitionType
                                                                 FixedAssetReclassificationDetail.HasOutput = i.HasOutput
                                                                 FixedAssetReclassificationDetail.OutputRefund = i.OutputRefund
                                                                 .FixedAssetReclassificationDetail.Add(FixedAssetReclassificationDetail)
                                                             End If

                                                             Dim FixedAssetReclassificationDetailBook As New FixedAssetReclassificationDetailBook
                                                             FixedAssetReclassificationDetailBook.LegalBookId = i.LegalBookId.Id
                                                             FixedAssetReclassificationDetailBook.DepreciatedValue = i.DepreciatedValue
                                                             FixedAssetReclassificationDetailBook.ResidualValue = i.ResidualValue
                                                             FixedAssetReclassificationDetailBook.HistoricalValue = i.HistoricalValue
                                                             FixedAssetReclassificationDetail.FixedAssetReclassificationDetailBook.Add(FixedAssetReclassificationDetailBook)
                                                         End Sub)
            End If
        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsInForm() As String
        Dim errors As New StringBuilder

        If DocumentDate Is Nothing Then
            errors.AppendLine(INDlyItemDocumentDate.Text + ResourceManager.GetString("Empty"))
        End If

        If ReclassificationType Is Nothing Then
            errors.AppendLine(INDlyItemReclassificationType.Text + ResourceManager.GetString("Empty"))
        End If

        If Detail Is Nothing Then
            errors.AppendLine(INDlyItemDetail.Text + ResourceManager.GetString("Empty"))
        End If

        If ItemIdPrevious Is Nothing Then
            errors.AppendLine(INDlyItemItemPrevious.Text + ResourceManager.GetString("Empty"))
        End If

        If ItemCatalogIdPrevious Is Nothing Then
            errors.AppendLine(INDlyItemCatalogPrevious.Text + ResourceManager.GetString("Empty"))
        End If

        If ReclassificationType = 2 Then
            If ItemId Is Nothing Then
                errors.AppendLine(INDlyItemItem.Text + ResourceManager.GetString("Empty"))
            End If
        End If

        If ItemCatalogId Is Nothing Then
            errors.AppendLine(INDlyItemCatalog.Text + ResourceManager.GetString("Empty"))
        End If

        If ItemCatalogIdPrevious IsNot Nothing AndAlso ItemCatalogId IsNot Nothing Then
            If ItemCatalogIdPrevious = ItemCatalogId Then
                errors.AppendLine("Los catálogos no pueden ser iguales")
            End If
        End If

        If FixedAssetReclassification Is Nothing OrElse FixedAssetReclassification.Id = 0 Then
            If fixedAssetForReclassificationXpo Is Nothing OrElse fixedAssetForReclassificationXpo.Count = 0 Then
                errors.AppendLine("No existen activos a reclasificar")
            End If
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Método que crea los tabs con sus rejillas para pintar los registros de los libros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateTabs(dictionaryLegalBook As Dictionary(Of Integer, BookXpo))
        INDlyReclassification.BeginUpdate()

        'Se eliminan los controles
        RemoveControls()

        Dim TabbedControlGroup1 As New DevExpress.XtraLayout.TabbedControlGroup()
        CType(TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        INDlygDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabbedControlGroup1})

        'Se crea el tabGroup en donde se ubican los tabs
        TabbedControlGroup1.Location = New System.Drawing.Point(0, 0)
        TabbedControlGroup1.MaxSize = New System.Drawing.Size(828, 0)
        TabbedControlGroup1.MinSize = New System.Drawing.Size(828, 24)
        TabbedControlGroup1.SelectedTabPageIndex = 1
        TabbedControlGroup1.Size = New System.Drawing.Size(828, 435)

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
            TabBooks.Size = New System.Drawing.Size(828, 474)
            TabBooks.Text = itemLegalBook.Value.CodeName

            'Se crea la rejilla
            Dim GridControl As New DevExpress.XtraGrid.GridControl()
            CType(GridControl, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.IndigoGridControl1.SetAddActions(GridControl, Nothing)
            Me.IndigoGridControl1.SetControlNextFocus(GridControl, Nothing)
            Me.IndigoGridControl1.SetGuardarXml(GridControl, True)
            Me.IndigoGridControl1.SetHoldSize(GridControl, False)
            Me.IndigoGridControl1.SetHotTrack(GridControl, False)
            GridControl.Location = New System.Drawing.Point(438, 59)
            GridControl.Size = New System.Drawing.Size(824, 470)
            Me.IndigoGridControl1.SetSizeConstraintsType(GridControl, DevExpress.XtraLayout.SizeConstraintsType.Custom)
            INDlyReclassification.Controls.Add(GridControl)

            'Se crean las columnas de la rejilla

            Dim GridColumn1 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn1.Caption = "Articulo"
            GridColumn1.OptionsColumn.AllowEdit = False
            GridColumn1.OptionsColumn.AllowFocus = False
            GridColumn1.Visible = True
            GridColumn1.VisibleIndex = 0
            GridColumn1.FieldName = "ItemCodeDescription"

            Dim GridColumn2 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn2.Caption = "Serie"
            GridColumn2.OptionsColumn.AllowEdit = False
            GridColumn2.OptionsColumn.AllowFocus = False
            GridColumn2.Visible = True
            GridColumn2.VisibleIndex = 1
            GridColumn2.FieldName = "Serie"

            Dim GridColumn3 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn3.Caption = "Placa"
            GridColumn3.OptionsColumn.AllowEdit = False
            GridColumn3.OptionsColumn.AllowFocus = False
            GridColumn3.Visible = True
            GridColumn3.VisibleIndex = 2
            GridColumn3.FieldName = "Plate"

            Dim GridColumn4 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn4.Caption = "Estado"
            GridColumn4.OptionsColumn.AllowEdit = False
            GridColumn4.OptionsColumn.AllowFocus = False
            GridColumn4.Visible = True
            GridColumn4.VisibleIndex = 3
            GridColumn4.FieldName = "StatusName"

            Dim GridColumn5 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn5.Caption = "Tipo de Adquisición"
            GridColumn5.OptionsColumn.AllowEdit = False
            GridColumn5.OptionsColumn.AllowFocus = False
            GridColumn5.Visible = True
            GridColumn5.VisibleIndex = 4
            GridColumn5.FieldName = "AdquisitionTypeName"

            Dim GridColumn6 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn6.Caption = "Valor Depreciado"
            GridColumn6.OptionsColumn.AllowEdit = False
            GridColumn6.OptionsColumn.AllowFocus = False
            GridColumn6.Visible = True
            GridColumn6.VisibleIndex = 5
            GridColumn6.FieldName = "DepreciatedValue"
            GridColumn6.DisplayFormat.FormatString = "c2"
            GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridColumn6.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DepreciatedValue", "{0:c2}")})
            GridColumn6 = Window.Utils.FormatGrid(GridColumn6, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn7 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn7.Caption = "Valor Residual"
            GridColumn7.OptionsColumn.AllowEdit = False
            GridColumn7.OptionsColumn.AllowFocus = False
            GridColumn7.Visible = True
            GridColumn7.VisibleIndex = 6
            GridColumn7.FieldName = "ResidualValue"
            GridColumn7.DisplayFormat.FormatString = "c2"
            GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridColumn7.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ResidualValue", "{0:c2}")})
            GridColumn7 = Window.Utils.FormatGrid(GridColumn7, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn8 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn8.Caption = "Valor Historico"
            GridColumn8.OptionsColumn.AllowEdit = False
            GridColumn8.OptionsColumn.AllowFocus = False
            GridColumn8.Visible = True
            GridColumn8.VisibleIndex = 7
            GridColumn8.FieldName = "HistoricalValue"
            GridColumn8.DisplayFormat.FormatString = "c2"
            GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridColumn8.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "HistoricalValue", "{0:c2}")})
            GridColumn8 = Window.Utils.FormatGrid(GridColumn8, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

            Dim GridColumn9 As New DevExpress.XtraGrid.Columns.GridColumn()
            GridColumn9.Caption = "Valor Reclasificado"
            GridColumn9.OptionsColumn.AllowEdit = False
            GridColumn9.OptionsColumn.AllowFocus = False
            GridColumn9.Visible = True
            GridColumn9.VisibleIndex = 8
            GridColumn9.FieldName = "ReclasificatedValue"
            GridColumn9.DisplayFormat.FormatString = "c2"
            GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridColumn9.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ReclasificatedValue", "{0:c2}")})
            GridColumn9 = Window.Utils.FormatGrid(GridColumn9, itemLegalBook.Value.OfficialCurrencyId.Abbreviation)

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
            view.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {GridColumn1, GridColumn2, GridColumn3, GridColumn4, GridColumn5, GridColumn6, GridColumn7, GridColumn8, GridColumn9})
            view.GridControl = GridControl
            view.OptionsView.EnableAppearanceEvenRow = True
            view.OptionsView.EnableAppearanceOddRow = True
            view.OptionsView.ShowAutoFilterRow = True
            view.OptionsView.ShowDetailButtons = False
            view.OptionsView.ShowGroupPanel = False
            view.OptionsView.ShowFooter = True
            Me.IndigoGridView1.SetTemaIndigoMetro(view, False)

            'Se asigna el view al gridcontrol
            GridControl.MainView = view
            GridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {view})
            Me.IndigoGridControl1.SetExportButtonControl(GridControl, True)
            Me.IndigoGridControl1.SetExportButton(GridControl, True)

            'Se crea el layout que se ubica en cada tab y el que contiene la rejilla
            Dim LayoutItem As New DevExpress.XtraLayout.LayoutControlItem()
            CType(LayoutItem, System.ComponentModel.ISupportInitialize).BeginInit()
            LayoutItem.Control = GridControl
            LayoutItem.Location = New System.Drawing.Point(0, 0)
            LayoutItem.MaxSize = New System.Drawing.Size(828, 0)
            LayoutItem.MinSize = New System.Drawing.Size(828, 24)
            LayoutItem.Size = New System.Drawing.Size(828, 474)
            LayoutItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutItem.TextSize = New System.Drawing.Size(0, 0)
            LayoutItem.TextVisible = False

            'Se asigna el datasource a la rejilla correspondiente
            GridControl.DataSource = Nothing
            If FixedAssetReclassification Is Nothing OrElse FixedAssetReclassification.Id = 0 Then
                GridControl.DataSource = fixedAssetForReclassificationXpo.Where(Function(item) item.LegalBookId.Id = itemLegalBook.Key).ToList
            Else
                GridControl.DataSource = fixedAssetForReclassifiedXpo.Where(Function(item) item.LegalBookId.Id = itemLegalBook.Key).ToList
            End If
            GridControl.RefreshDataSource()

            'Se agrega el nuevo tab al tabControlGroup
            TabBooks.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {LayoutItem})
            TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {TabBooks})
            CType(GridControl, System.ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutItem, System.ComponentModel.ISupportInitialize).EndInit()
            CType(TabBooks, System.ComponentModel.ISupportInitialize).EndInit()
        Next

        CType(TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        INDlyReclassification.EndUpdate()
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        bwCreateTabs = Nothing
        FixedAssetReclassification = Nothing
        fixedAssetForReclassificationXpo = Nothing
        fixedAssetForReclassifiedXpo = Nothing
        dictionaryLegalBook = Nothing
        FlagIsLoad = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetReclassification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LayoutControls.SetIsCustomizable(INDlyReclassification, True)
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        _doc = Nothing
        _funct = AddressOf GenerateDoc
        indigo = SessionValues.Instance
        Presenter = New PFixedAssetReclassification(Me)
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        InitializeTuples()
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmFixedAssetReclassification_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If _sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await NewFixedAssetReclassification()
                Else
                    Await LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemPrevious_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleItemPrevious.QueryPopUp
        If ItemPreviousXpo Is Nothing Then
            Presenter.InitializeItemPrevious()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCatalogPrevious_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCatalogPrevious.QueryPopUp
        If ItemCatalogPreviousXpo Is Nothing Then
            Presenter.InitializeItemCatalogPrevious()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItem_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleItem.QueryPopUp
        If ItemXpo Is Nothing Then
            Presenter.InitializeItem()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCatalog_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCatalog.QueryPopUp
        If ItemCatalogXpo Is Nothing Then
            Presenter.InitializeItemCatalog()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de artículos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleItem_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleItem.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(572, Nothing, True)
            Presenter.InitializeItem()
            Presenter.InitializeItemPrevious()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de catalogo de equipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCatalog_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCatalog.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1697, Nothing, True)
            Presenter.InitializeItemCatalog()
            Presenter.InitializeItemCatalogPrevious()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleReclassificationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReclassificationType.EditValueChanged
        INDSleItem.EditValue = Nothing
        INDSleItem.Properties.NullText = String.Empty
        INDSleCatalog.EditValue = Nothing
        INDSleCatalog.Properties.NullText = String.Empty

        If ReclassificationType = 1 Then
            'Si es reclasificación de catálogo
            INDlyItemItem.HideLayout()
            INDSleCatalog.Properties.ReadOnly = False
        Else
            'Si es reclasificación de artículo
            INDlyItemItem.ShowLayout()
            INDSleCatalog.Properties.ReadOnly = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemPrevious_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleItemPrevious.EditValueChanged
        INDlygDetails.HideControl()
        If ItemIdPrevious IsNot Nothing Then
            Try
                AsyncLoader(True)
                bwCreateTabs.RunWorkerAsync()
            Catch ex As Exception
                AsyncLoader(False)
            End Try
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDSleItemPrevious_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleItemPrevious.EditValueChanging
        INDSleCatalogPrevious.EditValue = Nothing
        INDSleCatalogPrevious.Properties.NullText = String.Empty

        If FlagIsLoad = False Then
            If e.NewValue IsNot Nothing Then
                Dim xpo = DirectCast(DirectCast(INDgvItemPrevious.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetEquipmentXpo)
                If xpo.ItemCatalogId IsNot Nothing Then
                    INDSleCatalogPrevious.EditValue = xpo.ItemCatalogId.Id
                    INDSleCatalogPrevious.Properties.NullText = xpo.ItemCatalogId.CodeDescription
                End If
            End If
        End If
    End Sub

    Private Sub INDSleItem_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleItem.EditValueChanging
        INDSleCatalog.EditValue = Nothing
        INDSleCatalog.Properties.NullText = String.Empty

        If e.NewValue IsNot Nothing AndAlso FlagIsLoad = False Then
            Dim xpo = DirectCast(DirectCast(INDgvItem.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetEquipmentXpo)
            If xpo.ItemCatalogId IsNot Nothing Then
                INDSleCatalog.EditValue = xpo.ItemCatalogId.Id
                INDSleCatalog.Properties.NullText = xpo.ItemCatalogId.CodeDescription
            End If
        End If
    End Sub

#End Region

#End Region

#Region "BackgroundWorker"

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwCreateTabs_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        If FixedAssetReclassification Is Nothing OrElse FixedAssetReclassification.Id = 0 Then

            'Consulta si ya hay activos por reclasificar
            fixedAssetForReclassificationXpo = Presenter.GetFixedAssetForReclassification(ItemIdPrevious)
            If fixedAssetForReclassificationXpo?.Any() Then

                'Se crea el diccionario para agrupar los libros del detalle
                dictionaryLegalBook = New Dictionary(Of Integer, BookXpo)
                fixedAssetForReclassificationXpo.GroupBy(Function(i) New With {i.LegalBookId, i.LegalBookCodeName}).ToList().ForEach(Sub(i)
                                                                                                                                         If Not dictionaryLegalBook.ContainsKey(i.Key.LegalBookId.Id) Then
                                                                                                                                             dictionaryLegalBook.Add(i.Key.LegalBookId.Id, i.Key.LegalBookId)
                                                                                                                                         End If
                                                                                                                                     End Sub)
            End If
        Else
            'Consulta los activos ya reclasificados
            fixedAssetForReclassifiedXpo = Presenter.GetFixedAssetReclassified(FixedAssetReclassification.Id)
            If fixedAssetForReclassifiedXpo.Any() Then
                'Se crea el diccionario para agrupar los libros del detalle
                dictionaryLegalBook = New Dictionary(Of Integer, BookXpo)
                fixedAssetForReclassifiedXpo.GroupBy(Function(i) New With {i.LegalBookId, i.LegalBookCodeName}).ToList().ForEach(Sub(i)
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
        Dim found = False

        'Verificamos cuantos libros hay registrados para asi mismo crear los tabs con sus rejillas en el form en tiempo de ejecución
        If FixedAssetReclassification Is Nothing OrElse FixedAssetReclassification.Id = 0 Then
            If fixedAssetForReclassificationXpo?.Any() Then
                found = True
            End If
        Else
            If fixedAssetForReclassifiedXpo?.Any() Then
                found = True
            End If
        End If

        If found Then
            CreateTabs(dictionaryLegalBook)
            INDlygDetails.HideControl(False)
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró ningún activo por reclasificar"
            INDlygDetails.HideControl()
        End If
        AsyncLoader(False)
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            _idOperativeUnit = operatingUnit.Id
            If _sequence IsNot Nothing AndAlso _sequence.Scope IsNot Nothing AndAlso _sequence.Scope.Equals("OU") AndAlso _sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not _sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        FixedAssetReclassification.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        FixedAssetReclassification.Status = 2
        Confirmar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        FixedAssetReclassification.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        FixedAssetReclassification.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        FixedAssetReclassification.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        BarraBotones.PrintReport(PrintReportAction.DirectPrinting, FixedAssetReclassification.Id, 0, FixedAssetReclassification.Id, _idOperativeUnit)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

#End Region

End Class
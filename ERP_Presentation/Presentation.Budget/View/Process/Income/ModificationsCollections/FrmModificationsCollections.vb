'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 24-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

Public Class FrmModificationsCollections
    Implements ICollectionModification

#Region "BUILDER"

    Public ctrTmp As CtrInfoEntity

    Public Sub New()
        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfoEntity()
        ' This call is required by the designer.
        InitializeComponent()
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshInfo()
        ctrTmp.PopupContainerControlEntity = INDpccChangeEntity
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Tuple(Of Integer, String, Integer, String, String)
        Return New Tuple(Of Integer, String, Integer, String, String)(BudgetEntitiesId, _EntityDescription, BudgetaryValidityId, _Year, statusValidity)
    End Function

#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' descripcion de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _EntityDescription As String
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.BudgetSequence
    ''' <summary>
    ''' Presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PCollectionModification
    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable de sesiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' representa la entidad de modificaciones del PAC
    ''' </summary>
    ''' <remarks></remarks>
    Dim collectionModification As CollectionModification
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"
    ''' <summary>
    ''' Año vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _Year As String
    ''' <summary>
    ''' Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim statusValidity As String
    ''' <summary>
    ''' Mes Ingresos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _IncomeMonth As Integer
    ''' <summary>
    ''' listado con los detalles para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCollectionModificationDetail As List(Of CollectionModificationDetail)
    ''' <summary>
    ''' Listado de eliminados de detalles de la modificacion del recaudo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteCollectionModificationDetail As List(Of Integer)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
#End Region

#Region "PROPERTIES"
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

    ''' <summary>
    ''' Obtiene o establece el id de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesId As Integer
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que muestra los mensajes del visor
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' Prpiedad que habilita o deshabilita los controles  
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICollectionModification.ActionsOnControls
        Set(value As Boolean)
            INDLcCollectionModification.BeginUpdate()
            INDbtnConsecutive.Enabled = Not value
            INDdteDate.Enabled = value
            INDSleCollection.Enabled = value
            INDtxtDocument.Enabled = value
            INDmemoObservations.Enabled = value
            If INDSleCollection.EditValue Is Nothing Then
                INDBtnAdd.Enabled = False
            End If
            INDgcDetail.Enabled = value
            INDLcCollectionModification.EndUpdate()
            If value Then
                INDdteDate.Focus()
            Else
                INDbtnConsecutive.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' Establece o habilita el datosurce de las vigencias dentro del popup
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityPopUpXpo As XPCollection Implements ICollectionModification.BudgetaryValidityPopUpXpo
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource de vigencias
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityXpo As XPCollection Implements ICollectionModification.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Establece o habilita el datosurce de entidades dentro del popup
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetEntitiesPopUpXpo As XPInstantFeedbackSource Implements ICollectionModification.BudgetEntitiesPopUpXpo
        Get
            Return INDsleBudgetEntityPopUp.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetEntityPopUp.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource de entidades
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetEntitiesXpo As XPInstantFeedbackSource Implements ICollectionModification.BudgetEntitiesXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements ICollectionModification.Code
        Get
            If (INDbtnConsecutive.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnConsecutive.Text
            End If
        End Get
        Set(value As String)
            INDbtnConsecutive.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de las colleciones
    ''' </summary>
    ''' <returns></returns>
    Public Property CollectionId As Integer Implements ICollectionModification.CollectionId
        Get
            Return INDSleCollection.EditValue
        End Get
        Set(value As Integer)
            INDSleCollection.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece los documentos
    ''' </summary>
    ''' <returns></returns>
    Public Property Document As String Implements ICollectionModification.Document
        Get
            Return INDtxtDocument.EditValue
        End Get
        Set(value As String)
            INDtxtDocument.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece las fecchas de los documentos
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As Date Implements ICollectionModification.DocumentDate
        Get
            Return INDdteDate.EditValue
        End Get
        Set(value As Date)
            INDdteDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el layout control
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICollectionModification.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece el tag del frmm
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements ICollectionModification.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establecce las observaciones 
    ''' </summary>
    ''' <returns></returns>
    Public Property Observations As String Implements ICollectionModification.Observations
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece las secuencias 
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As BudgetSequence Implements ICollectionModification.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As BudgetSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.BudgetSequenceDetail In Me._sequense.BudgetSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de las collecciones 
    ''' </summary>
    ''' <returns></returns>
    Property CollectionXpo As XPInstantFeedbackSource
        Get
            Return CType(INDSleCollection.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCollection.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "CRUD"
    ''' <summary>
    ''' Metodo Buscar
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub
    ''' <summary>
    ''' Metodo  deshacer
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.NewAndFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub
    ''' <summary>
    ''' Metodo Eliminar, sin usarse
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub
    ''' <summary>
    ''' Metodo guardar
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Me.collectionModification.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            Dim errors As String = ValidateDetail()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MCollectionModification(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveCollectionModification(Me.collectionModification, _listDeleteCollectionModificationDetail)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    collectionModification = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, collectionModification.Id, 0, collectionModification.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, collectionModification.Id, 0, collectionModification.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, collectionModification.Id, 0, collectionModification.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, collectionModification.Id, 0, collectionModification.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Metodo actualizar, sin usarse
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
    ''' <summary>
    ''' Metodo nuevo, sin usarse
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

#End Region

#Region "METHODS"
    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewCollectionModification()
        If INDsleBudgetEntity.EditValue IsNot Nothing AndAlso CType(INDsleBudgetEntity.EditValue, String) <> "" Then
            Me.collectionModification = New CollectionModification
            If Me._sequense.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequense.Scope Is Nothing Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.Code = String.Empty
                    INDbtnConsecutive.Focus()
                    Exit Sub
                End If
                If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail(0).Id
                ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                        Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Exit Sub
                    End If
                End If
                If Not Me._sequense.Sequential Then
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
                            PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
                        Else
                            Using model As New ModelBaseBudget(Me.Tag)
                                Me.DicSequense(Me._idCurrentSequense) = Await model.GetNumericSequenseGroup(Me._idCurrentSequense)
                            End Using
                            If Me.DicSequense(Me._idCurrentSequense) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
                                PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
                            Else
                                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                            End If
                        End If
                    Else
                        PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
                    End If
                Else
                    PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
                _Year = item.Year
                statusValidity = item.StatusText
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        Me.Code = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecord = "1"
        Me.BarraBotones.ControlHideStatus = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        INDsleBudgetEntityPopUp.Properties.ReadOnly = True
        INDsleValidityPopUp.Properties.ReadOnly = True
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

    ''' <summary>
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.collectionModification.Code), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me.collectionModification.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.collectionModification.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.collectionModification.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.collectionModification.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' abre el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo With {.Caption = "Recaudo", .FieldName = "CollectionId.Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "CollectionId.ThirdPartyId.NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {BudgetaryValidityId}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCollectionModification
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
        INDbtnConsecutive.Text = ReturnValue
        If INDbtnConsecutive.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnConsecutive.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INDLcCollectionModification.BeginUpdate()
        Me.collectionModification = Nothing
        _listCollectionModificationDetail = Nothing
        _listDeleteCollectionModificationDetail = Nothing
        INDsleBudgetEntityPopUp.Properties.ReadOnly = False
        INDsleValidityPopUp.Properties.ReadOnly = False
        ReadOnlyControls(False)
        Code = String.Empty
        INDdteDate.EditValue = Nothing
        INDSleCollection.EditValue = Nothing
        INDSleCollection.Properties.ReadOnly = False
        INDSleCollection.Properties.NullText = String.Empty
        Document = String.Empty
        Observations = String.Empty
        INDgcDetail.DataSource = Nothing
        ActionsOnControls = False
        Me.BarraBotones.StatusRecord = -1
        BarraBotones.ControlHideStatus = False
        DeleteBlockedRecord()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDLcCollectionModification.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBudgetModification(MyTag)
                Await Model.DeleteBlockRecord(blockRecord)
            End Using
            blockRecord = Nothing
        End If
    End Sub
    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup()
        INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlygCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.ControlHideStatus = False
        BarraBotones.PrepareToolbar(eAction.NewAndFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DeshacerTodo()
        CleanControlsTodo()
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControlsTodo()
        INDLcCollectionModification.BeginUpdate()
        ActionsOnControls = False
        INDsleValidity.EditValue = Nothing
        CleanControls()
        INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        BarraBotones.StatusRecordVisible = False
        INDsleBudgetEntity.Focus()
        INDLcCollectionModification.EndUpdate()
    End Sub
    ''' <summary>
    ''' Metodo para validar los detalles
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateDetail() As String
        Dim errors As New StringBuilder
        If INDgvDetail.RowCount = 0 Then
            errors.AppendLine("Se debe agregar minimo un detalle para la modificación")
        End If
        If INDgvDetail.RowCount > 0 Then
            Dim listNotValue = _listCollectionModificationDetail.FindAll(Function(x) x.Value = 0)
            If listNotValue.Count > 0 Then
                errors.AppendLine("Se encontraron items sin valor")
            End If
        End If
        Return errors.ToString()
    End Function
    ''' <summary>
    ''' Metodo que asigna valores
    ''' </summary>
    Private Sub AssigningValues()
        With collectionModification
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = _idOperativeUnit
            .Code = Code
            .BudgetaryValidityId = BudgetaryValidityId
            .DocumentDate = DocumentDate
            .CollectionId = CollectionId
            .Document = Document
            .Observations = Observations
            .CollectionModificationDetail.Clear()
            If _listCollectionModificationDetail IsNot Nothing AndAlso _listCollectionModificationDetail.Count > 0 Then
                _listCollectionModificationDetail.FindAll(Function(item) item.Value > 0 AndAlso (item.ChangeTracker.State = ObjectState.Added OrElse item.ChangeTracker.State = ObjectState.Modified)).ForEach(Sub(item)
                                                                                                                                                                                                                   .CollectionModificationDetail.Add(item)
                                                                                                                                                                                                               End Sub)
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub
    ''' <summary>
    ''' Metodo que carga los controles
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        Using model As New MCollectionModification(MyTag)
            AsyncLoader(True)
            Me.collectionModification = Await model.GetCollectionModificationByCode(Code, BudgetaryValidityId)
            INDLcCollectionModification.BeginUpdate()
            If collectionModification IsNot Nothing AndAlso collectionModification.Id > 0 Then

                _listCollectionModificationDetail = model.ListCollectionModificationDetailByCollectionId(collectionModification.Id)
                Dim result = Await model.GetBlockRecord(MyTag, collectionModification.Id)
                INDsleBudgetEntityPopUp.Properties.ReadOnly = True
                INDsleValidityPopUp.Properties.ReadOnly = True
                With collectionModification
                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                    Code = .Code
                    BudgetaryValidityId = .BudgetaryValidityId
                    BudgetEntitiesId = .BudgetEntityId
                    DocumentDate = .DocumentDate
                    CollectionId = .CollectionId
                    INDSleCollection.Properties.NullText = .CodeCollection
                    INDSleCollection.Properties.ReadOnly = True
                    Document = .Document
                    Observations = .Observations
                    Me.BarraBotones.StatusRecord = .Status.ToString()
                    Me.BarraBotones.ControlHideStatus = True
                End With
                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.collectionModification.Code)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = collectionModification.Id}
                    Dim operation = Await model.SaveBlockRecord(blockRecord)
                    blockRecord = operation.ObjectEmbbeded
                Else
                    blockRecord = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                If collectionModification.Status = 1 Then 'Registrado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Else 'Confirmado o Anulado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.SetDocuments(collectionModification.Id)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, collectionModification.Id, 0, collectionModification.Id)
                AsyncLoader(False)
                ActionsOnControls = True
                INDgcDetail.DataSource = _listCollectionModificationDetail
                INDgcDetail.RefreshDataSource()
            Else
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewCollectionModification()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Deshacer()
                    INDbtnConsecutive.Focus()
                End If
            End If
        End Using
        INDLcCollectionModification.EndUpdate()
    End Function
#End Region

#Region "HANDLES"
#Region "Load"
    ''' <summary>
    ''' Libera la memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        _EntityDescription = Nothing
        _sequense = Nothing
        presenter = Nothing
        blockRecord = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
        collectionModification = Nothing
        _Year = Nothing
        statusValidity = Nothing
        _IncomeMonth = Nothing
        _listCollectionModificationDetail = Nothing
        _listDeleteCollectionModificationDetail = Nothing
        varImp = Nothing
    End Sub
    ''' <summary>
    ''' Evento que carga FrmModificationsCollections
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmModificationsCollections_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcCollectionModification, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        presenter = New PCollectionModification(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDgcDetail)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDetail.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        LoadStatus()
        DeshacerTodo()
    End Sub
#End Region

#Region "Shown"
    ''' <summary>
    ''' Enfooca el campo INDsleBudgetEntity 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmModificationsCollections_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleBudgetEntity.Focus()
    End Sub
#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Evento al cerrar el frm FrmModificationsCollections
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmModificationsCollections_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub
    ''' <summary>
    ''' Query popup de collecciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCollection_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCollection.QueryPopUp
        If CollectionXpo Is Nothing Then
            Using model As New MCollectionModification(MyTag)
                CollectionXpo = model.GetCollection(BudgetaryValidityId)
            End Using
        End If
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' abre el frm entidades presupuestales 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", INDsleBudgetEntity.EditValue, True)
        End If
    End Sub
    ''' <summary>
    ''' abre el frm entidades presupuestales 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetEntityPopUp_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntityPopUp.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", INDsleBudgetEntityPopUp.EditValue, True)
        End If
    End Sub
    ''' <summary>
    ''' abre el frm recaudos 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCollection_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCollection.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("215", Nothing, True)
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento al editar el campo de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            _EntityDescription = INDsleBudgetEntity.Text
            BudgetaryValidityId = Nothing
            BudgetaryValidityXpo = Nothing
            presenter.InitializeValidity(BudgetEntitiesId)
            SetFirstOrDefaultValidity()
            ctrTmp.RefreshInfo()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar el campo de vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If BudgetaryValidityId <> Nothing AndAlso BudgetaryValidityId <> 0 Then
            If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
                Dim item = (From l In BudgetaryValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
                If item IsNot Nothing Then
                    _Year = item.Year
                    statusValidity = item.StatusText
                    _IncomeMonth = item.IncomeMonth
                    ctrTmp.RefreshInfo()
                    LoadLayoutGroup()
                    If BudgetEntitiesPopUpXpo Is Nothing Then
                        presenter.InitializeBudgetEntityPopUp()
                    End If
                    INDsleBudgetEntityPopUp.EditValue = BudgetEntitiesId
                    INDsleValidityPopUp.EditValue = BudgetaryValidityId
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento al modificar el Popup de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntityPopUp.EditValueChanged
        If INDsleBudgetEntityPopUp.EditValue IsNot Nothing Then
            BudgetEntitiesId = INDsleBudgetEntityPopUp.EditValue
            If INDsleBudgetEntityPopUp.Text <> String.Empty Then
                _EntityDescription = INDsleBudgetEntityPopUp.Text
            End If
            INDsleValidityPopUp.EditValue = Nothing
            BudgetaryValidityPopUpXpo = Nothing
            presenter.InitializeValidityPopUp(INDsleBudgetEntityPopUp.EditValue)
            ctrTmp.RefreshInfo()
        End If
    End Sub
    ''' <summary>
    ''' Evento al modificar el Popup de vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidityPopUp.EditValueChanged
        If INDsleValidityPopUp.EditValue IsNot Nothing Then
            If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
                BudgetaryValidityId = INDsleValidityPopUp.EditValue
                Dim item = (From l In BudgetaryValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
                If item IsNot Nothing Then
                    _Year = item.Year
                    statusValidity = item.StatusText
                    _IncomeMonth = item.IncomeMonth
                    ctrTmp.RefreshInfo()
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar el campo de colecciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCollection_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCollection.EditValueChanged
        If INDSleCollection.EditValue IsNot Nothing Then
            If collectionModification IsNot Nothing AndAlso collectionModification.Status < 2 Then
                INDBtnAdd.Enabled = True
            End If
        End If
    End Sub
#End Region

#Region "EditValueChanging"
    ''' <summary>
    ''' Evento al editar el campo INDRptTxtValue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptTxtValue.EditValueChanging
        If e.NewValue = String.Empty Then
            Exit Sub
        End If
        Dim collectionDetail = DirectCast(INDgvDetail.GetFocusedRow(), CollectionModificationDetail)

        If collectionDetail.CollectionBalance < e.NewValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo del recaudo"
            e.Cancel = True
        End If

    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Evento al presionar enter en el campo de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If BudgetaryValidityId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una vigencia."
                Exit Sub
            End If
            If Me._sequense Is Nothing OrElse Me._sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnConsecutive.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnConsecutive.Text.Trim()) Then
                    Me.NewCollectionModification()
                Else
                    Await LoadControls()
                End If
            End If
        End If
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.collectionModification IsNot Nothing AndAlso Me.collectionModification.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDbtnConsecutive.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnConsecutive.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Click_ButtonAction"
    ''' <summary>
    ''' Evento al mofiicar la grilla de registros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If Me.collectionModification.Status > 1 Then
            If Me.collectionModification.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta confirmado"
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta anulado"
            End If
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim modificationDetail = DirectCast(INDgvDetail.GetFocusedRow(), CollectionModificationDetail)
            If modificationDetail.Id > 0 Then
                If _listDeleteCollectionModificationDetail Is Nothing Then
                    _listDeleteCollectionModificationDetail = New List(Of Integer)
                End If
                modificationDetail.MarkAsDeleted()
                _listDeleteCollectionModificationDetail.Add(modificationDetail.Id)
            End If
            _listCollectionModificationDetail.Remove(modificationDetail)
            INDgcDetail.DataSource = Nothing
            INDgcDetail.DataSource = _listCollectionModificationDetail
            INDgcDetail.RefreshDataSource()
            If _listCollectionModificationDetail.Count = 0 Then
                INDSleCollection.Properties.ReadOnly = False
            End If
        End If

    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Evento al dar click en agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim frmDetail As New FrmPopupCollection()
        frmDetail.Size = New System.Drawing.Size(800, 730)
        frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        frmDetail.ValidatyId = BudgetaryValidityId
        frmDetail.CollectionId = CollectionId
        Dim frmTransparent As New FrmTransparent(frmDetail, False)
        If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            If _listCollectionModificationDetail Is Nothing Then
                _listCollectionModificationDetail = frmDetail.ListCollectionModificationDetail
            Else
                For Each item In frmDetail.ListCollectionModificationDetail
                    Dim detailAdded = _listCollectionModificationDetail.Find(Function(x) x.CollectionDetailId = item.CollectionDetailId)
                    If detailAdded IsNot Nothing Then
                        Continue For
                    End If
                    _listCollectionModificationDetail.Add(item)
                Next

            End If
            INDSleCollection.Properties.ReadOnly = True
            INDgcDetail.DataSource = _listCollectionModificationDetail
            INDgcDetail.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "CustomSummaryCalculate"
    ''' <summary>
    ''' Variable para almacenar el valor total 
    ''' </summary>
    Private ValueTotal As Decimal
    ''' <summary>
    ''' Totaliza los valores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvDetail_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles INDgvDetail.CustomSummaryCalculate
        If (CType(e.Item, GridSummaryItem).FieldName <> "Value") Then
            Exit Sub
        End If
        If e.IsTotalSummary OrElse e.IsGroupSummary Then
            Dim view As Views.Grid.GridView = CType(sender, Views.Grid.GridView)
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                ValueTotal = 0
            ElseIf e.SummaryProcess = CustomSummaryProcess.Calculate Then
                Dim value As Decimal = CType(e.FieldValue, Decimal)
                Dim nature = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Nature"))
                If nature = 1 Then
                    ValueTotal = ValueTotal - value
                ElseIf nature = 2 Then
                    ValueTotal = ValueTotal + value
                End If
            ElseIf e.SummaryProcess = CustomSummaryProcess.Finalize Then
                e.TotalValue = ValueTotal
            End If
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
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnConsecutive.ButtonClick
        Buscar()
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
        Me.collectionModification.Status = 1
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
        Me.collectionModification.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.collectionModification.Status = 3
            varImp = 4
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()

                PopUpAnnulmentReason.BudgetaryValidityId = BudgetaryValidityId

                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    collectionModification.AnnulmentConceptId = PopUpAnnulmentReason.ReversalReasonId
                    collectionModification.AnnulmentDescription = PopUpAnnulmentReason.ReversalDescription
                End If
            End Using
            Guardar()
        End If
    End Sub



    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BudgetSequenceDetail IsNot Nothing Then
            If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Else
                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        DeshacerTodo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.collectionModification.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.collectionModification.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, collectionModification.Id, 0, collectionModification.Id)
    End Sub
#End Region
End Class
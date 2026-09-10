'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 28/08/2015
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
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

Public Class FrmRecognitionModification
    Implements IRecognitionModification

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

#Region "Consts"

    ''' <summary>
    ''' Nombre del control de la rejilla para el valor
    ''' </summary>
    ''' <remarks></remarks>
    Private Const controlTextEdit As String = "TextEdit"

    ''' <summary>
    ''' Nombre del control de la rejilla para la naturaleza
    ''' </summary>
    ''' <remarks></remarks>
    Private Const controlImageComboBox As String = "ImageComboBoxEdit"

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
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PRecognitionModification

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
    ''' representa la entidad de reconocimiento modificacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim recognitionModification As RecognitionModification

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
    Dim _listRecognitionModificationDetail As List(Of RecognitionModificationDetail)

    ''' <summary>
    ''' listado con los detalles para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteRecognitionModificationDetail As List(Of Integer)

    ''' <summary>
    ''' Variable para saber si guarda, actualiza o confirma (1 = guarda, 2 = Actualiza, 3 = Guardar y confirmar, 4 = Confirmar, 5 = Anular)
    ''' </summary>
    ''' <remarks></remarks>
    Dim state As Byte

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
    Public Property BudgetaryValidityId As Integer Implements IRecognitionModification.BudgetaryValidityId
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
    Public Property BudgetEntitiesId As Integer Implements IRecognitionModification.BudgetEntitiesId
        Get
            Return INDsleEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleEntity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para mostrar mensajes en el visor
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
    ''' Habilita o deshabilita los controles del frm
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRecognitionModification.ActionsOnControls
        Set(value As Boolean)
            INDlcRecognitionModification.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDsleRecognition.Enabled = value
            INDtxtDocument.Enabled = value
            INDmeObservations.Enabled = value
            INDsbAddCategory.Enabled = value
            INDgcDetail.Enabled = value
            INDlcRecognitionModification.EndUpdate()
            If value = True Then
                INDdeDocumentDate.Focus()
            Else
                If Object.Equals(INDsleEntity.EditValue, Nothing) = True Then
                    INDsleValidity.Focus()
                Else
                    INDbtnCode.Focus()
                End If
            End If
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IRecognitionModification.Code
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
    ''' Obtiene o establece el documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Document As String Implements IRecognitionModification.Document
        Get
            Return INDtxtDocument.EditValue
        End Get
        Set(value As String)
            INDtxtDocument.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements IRecognitionModification.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece el id de reconocimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RecognitionId As Integer Implements IRecognitionModification.RecognitionId
        Get
            Return INDsleRecognition.EditValue
        End Get
        Set(value As Integer)
            INDsleRecognition.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el listado de reconocimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RecognitionXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRecognitionModification.RecognitionXpo
        Get
            Return INDsleRecognition.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRecognition.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene el LayoutControl
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRecognitionModification.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obtiene el tag del frm
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IRecognitionModification.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece la observacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements IRecognitionModification.Observations
        Get
            Return INDmeObservations.EditValue
        End Get
        Set(value As String)
            INDmeObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property status As Byte Implements IRecognitionModification.status
        Get
            Return CByte(Me.BarraBotones.StatusRecord)
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la sequiencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As BudgetSequence Implements IRecognitionModification.Sequense
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
    ''' obtiene o establece el datasource para entidades presupuestales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRecognitionModification.BudgetEntitiesXpo
        Get
            Return INDsleEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection Implements IRecognitionModification.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesPopUpXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRecognitionModification.BudgetEntitiesPopUpXpo
        Get
            Return INDsleEntityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityPopUpXpo As DevExpress.Xpo.XPCollection Implements IRecognitionModification.BudgetaryValidityPopUpXpo
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer(Optional withBudgetEntity As Boolean = True)
        CleanControls(withBudgetEntity)
        If SearchMode = False Then
            'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            End If
        End If
        INDsleEntity.Focus()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer1() Implements IcrudBase.Deshacer

    End Sub
    ''' <summary>
    ''' Metodo eliminar, sin uso
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    '''  METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If recognitionModification.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            Dim errors As String = ValidateDetail()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            AssignsValues()
        End If
        Try
            Using model As New MRecognitionModification(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveRecognitionModification(recognitionModification, _listDeleteRecognitionModificationDetail)
                AsyncLoader(False)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    recognitionModification = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, recognitionModification.Id, 0, recognitionModification.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, recognitionModification.Id, 0, recognitionModification.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, recognitionModification.Id, 0, recognitionModification.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, recognitionModification.Id, 0, recognitionModification.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer(False)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            NewEntity()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
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
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code"},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate"},
                              New ColumnInfo With {.Caption = "Documento", .FieldName = "Document"},
                              New ColumnInfo With {.Caption = "Reconocimiento", .FieldName = "RecognitionId.Code"},
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "RecognitionId.ThirdPartyId.NitName"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName"}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {BudgetaryValidityId}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListRecognitionModificationByValidityId
            'BarraBotones.PrepareToolbar(eAction.OnlyNew)
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
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
    ''' Metodo que valida la naturaleza con respecto al saldo y el valor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateNature(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim recognitionModificationDetail As Domain.Entities.RecognitionModificationDetail = CType(INDgvDetail.GetFocusedRow, Domain.Entities.RecognitionModificationDetail)
            If recognitionModificationDetail IsNot Nothing Then
                Dim control As DevExpress.XtraEditors.BaseEdit = sender
                Select Case True
                    Case control.EditorTypeName = controlTextEdit
                        recognitionModificationDetail.Value = e.NewValue
                    Case control.EditorTypeName = controlImageComboBox
                        recognitionModificationDetail.Nature = e.NewValue
                End Select
                If recognitionModificationDetail.Nature = 1 Then
                    If recognitionModificationDetail.RecognitionDetail.Balance < recognitionModificationDetail.Value Then
                        Mensaje(EeventViewerImages.Advertencia) = "Con la naturaleza débito el saldo del reconocimiento no puede ser menor al valor."
                        Select Case True
                            Case control.EditorTypeName = controlTextEdit
                                recognitionModificationDetail.Value = e.OldValue
                            Case control.EditorTypeName = controlImageComboBox
                                recognitionModificationDetail.Nature = e.OldValue
                        End Select
                        e.Cancel = True
                    End If
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
    ''' Limpia los grupos
    ''' </summary>
    ''' <param name="Bandera"></param>
    Private Sub CleanGroups(ByVal Bandera As Boolean)
        If Bandera Then
            INDlcgRecognitionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If BudgetEntitiesPopUpXpo Is Nothing Then
                presenter.InitializeBudgetEntityPopUp()
            End If
            INDsleEntityPopUp.EditValue = BudgetEntitiesId
            INDsleValidityPopUp.EditValue = BudgetaryValidityId

            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.ControlHideStatus = False
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Else
            INDlcgRecognitionModification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.BarraBotones.StatusRecordVisible = False
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), recognitionModification.Code), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & recognitionModification.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), recognitionModification.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), recognitionModification.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), recognitionModification.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = CByte(1), .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = CByte(2), .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = CByte(3), .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <param name="withBudgetaryEntity">saber si se limpia tabn la entidad presupuestal o no</param>
    ''' <remarks></remarks>
    Private Async Function CleanControls(Optional withBudgetaryEntity As Boolean = True) As Task
        INDlcRecognitionModification.BeginUpdate()
        Await DeleteBlockedRecord()
        INDbtnCode.Text = String.Empty
        INDdeDocumentDate.EditValue = Me.GetDateServer()
        INDtxtDocument.Text = String.Empty
        INDmeObservations.Text = String.Empty
        INDgcDetail.DataSource = Nothing
        INDsleRecognition.EditValue = Nothing
        INDsleRecognition.Properties.NullText = String.Empty
        Me.status = 1
        Me._doc = Nothing
        recognitionModification = Nothing
        _listRecognitionModificationDetail = Nothing
        _listRecognitionModificationDetail = New List(Of Domain.Entities.RecognitionModificationDetail)
        _listDeleteRecognitionModificationDetail = Nothing
        Me.BarraBotones.ControlHideStatus = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        ReadOnlyControls(False)
        ActionsOnControls = False
        If withBudgetaryEntity = True Then
            BudgetaryValidityId = Nothing
            INDsleValidityPopUp.EditValue = Nothing
            _Year = Nothing
            statusValidity = Nothing
            CleanGroups(False)
        End If
        INDlcRecognitionModification.EndUpdate()
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Using Model As New MRecognitionModification(MyTag)
            AsyncLoader(True)
            recognitionModification = Await Model.GetRecognitionModificationByCode(Me.INDbtnCode.Text.Trim, BudgetaryValidityId)
            INDlcRecognitionModification.BeginUpdate()
            If Not recognitionModification Is Nothing AndAlso recognitionModification.Id > 0 Then
                ActionsOnControls = True
                Dim result = Await Model.GetBlockRecord(Me.Tag, recognitionModification.Id)
                With recognitionModification
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                    INDbtnCode.EditValue = .Code
                    INDdeDocumentDate.EditValue = .DocumentDate
                    INDtxtDocument.Text = .Document
                    INDmeObservations.Text = .Observations
                    INDsleRecognition.EditValue = .RecognitionId
                    INDsleRecognition.Properties.NullText = .CodeRecognition
                    _listRecognitionModificationDetail = .RecognitionModificationDetail.ToList()
                    Me.status = .Status
                    Me.BarraBotones.ControlHideStatus = True
                    If Me.status = 1 Then ' confirmado
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.recognitionModification.Code)
                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(recognitionModification.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = recognitionModification.Id}
                    Dim operation = Await Model.SaveBlockRecord(blockRecord)
                    blockRecord = operation.ObjectEmbbeded
                Else
                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                    blockRecord = result
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                If status = 1 Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.SetDocuments(recognitionModification.Id)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, recognitionModification.Id, 0, recognitionModification.Id)
                AsyncLoader(False)
                ActionsOnControls = True
                INDgcDetail.DataSource = Nothing
                INDgcDetail.DataSource = _listRecognitionModificationDetail
            Else
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewEntity()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Deshacer(False)
                    INDbtnCode.Focus()
                End If
            End If
        End Using
        INDlcRecognitionModification.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewEntity()
        If INDsleEntity.EditValue IsNot Nothing AndAlso CType(INDsleEntity.EditValue, String) <> "" Then
            recognitionModification = New Domain.Entities.RecognitionModification()
            If Me._sequense.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.StatusRecord = 1
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail(0).Id
                ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                        Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                    Else
                        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
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
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SeleccioneEntidadPresupuestal", NAME_MODULE)
            INDsleEntity.Focus()
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        INDbtnCode.Text = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecord = CByte(1)
        Me.BarraBotones.ControlHideStatus = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        INDsleEntityPopUp.Properties.ReadOnly = True
        INDsleValidityPopUp.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As String
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de traslado en la rejilla
        If Not (_listRecognitionModificationDetail IsNot Nothing AndAlso _listRecognitionModificationDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de modificacion de reconocimiento para poder guardar.")
        End If
        'Se valida que el debito sea igual que el credito
        If _listRecognitionModificationDetail IsNot Nothing AndAlso _listRecognitionModificationDetail.Count > 0 Then
            'Se valida que en los detalles no haya ningun item con naturaleza ninguna
            For Each item In _listRecognitionModificationDetail.FindAll(Function(x) x.Value > 0)
                If item.Nature = 0 Then
                    listErrors.AppendLine("La naturaleza del rubro " + item.CodeNameCategory + " con el tipo de ingreso " + item.RecognitionDetail.CodeNameRevenueType + " no puede ser Ninguna.")
                End If
                If item.Nature = 1 And item.Value > item.RecognitionDetail.Balance Then
                    listErrors.AppendLine("El valor debito del rubro " + item.CodeNameCategory + " con el tipo de ingreso " + item.RecognitionDetail.CodeNameRevenueType + " no puede ser mayor que el saldo del reconocimiento.")
                End If
            Next
        End If
        'Se valida que hayan elegido una vigencia
        If BudgetaryValidityId = 0 Then
            listErrors.AppendLine("Debe elegir una vigencia.")
        End If
        'Se valida que el mes y el año de la fecha del documento sea igual al mes y año de la vigencia
        Dim montDocument = Month(DocumentDate)
        Dim yearDocument = Year(DocumentDate)
        If montDocument <> Me._IncomeMonth Then
            listErrors.AppendLine(String.Format(ResourceManager.GetString("SelectMonth", NAME_MODULE), MonthName(Me._IncomeMonth, False)))
        End If
        If yearDocument <> CInt(_Year) Then
            listErrors.AppendLine(String.Format(ResourceManager.GetString("SelectYear", NAME_MODULE), _Year.ToString))
        End If
        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' asigna valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssignsValues()
        With recognitionModification
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = _idOperativeUnit
            .Code = Code
            .BudgetaryValidityId = BudgetaryValidityId
            .DocumentDate = DocumentDate
            .RecognitionId = RecognitionId
            .Document = INDtxtDocument.Text
            .Observations = Observations
            .RecognitionModificationDetail.Clear()
            If _listRecognitionModificationDetail IsNot Nothing AndAlso _listRecognitionModificationDetail.Count > 0 Then
                _listRecognitionModificationDetail.FindAll(Function(item) item.Value > 0 AndAlso (item.ChangeTracker.State = ObjectState.Added OrElse item.ChangeTracker.State = ObjectState.Modified)).ForEach(Sub(item)
                                                                                                                                                                                                                    .RecognitionModificationDetail.Add(item)
                                                                                                                                                                                                                End Sub)
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With

    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Function DeleteBlockedRecord() As Task
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBudgetModification(MyTag)
                Await Model.DeleteBlockRecord(blockRecord)
            End Using
            blockRecord = Nothing
        End If
    End Function

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim detail As RecognitionModificationDetail = INDgvDetail.GetFocusedRow
        If detail.Id > 0 Then
            If _listDeleteRecognitionModificationDetail Is Nothing Then
                _listDeleteRecognitionModificationDetail = New List(Of Integer)
            End If
            detail.MarkAsDeleted()
            _listDeleteRecognitionModificationDetail.Add(detail.Id)
        End If
        _listRecognitionModificationDetail.Remove(detail)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listRecognitionModificationDetail
        If _listRecognitionModificationDetail Is Nothing OrElse _listRecognitionModificationDetail.Count = 0 Then
            INDsleRecognition.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.recognitionModification IsNot Nothing AndAlso Me.recognitionModification.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Caraga el datasource de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadRecognitionDetail() As Task
        Dim listXpo = presenter.LoadRecognitionDetailByRecognitionId(RecognitionId)
        Await LoadListRecognitionModification(listXpo)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listRecognitionModificationDetail
        INDgcDetail.RefreshDataSource()
    End Function

    ''' <summary>
    ''' Metodo que carga el listado de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadListRecognitionModification(listXpo As XPCollection) As Task
        Using Model As New MBudgetItem()
            _listRecognitionModificationDetail = New List(Of Domain.Entities.RecognitionModificationDetail)
            If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                For Each itemXpo As BudgetRecognitionDetailXpo In listXpo
                    Dim RecognitionModificationDetail As New Domain.Entities.RecognitionModificationDetail
                    With RecognitionModificationDetail
                        .RecognitionDetailId = itemXpo.Id
                        .Nature = 0
                        .Value = 0
                        .CodeNameCategory = itemXpo.CategoryId.Code + " - " + itemXpo.CategoryId.Name
                        .CodeNameFinancialSource = itemXpo.CategoryId.FinancialSourceId.Code + " - " + itemXpo.CategoryId.FinancialSourceId.Name
                        .CodeNameRevenueType = itemXpo.RevenueTypeId.Code + " - " + itemXpo.RevenueTypeId.Name
                        .BalanceBudget = Await Model.GetBalanceBudgetByCategoryIdAndRevenueTypeId(itemXpo.CategoryId.Id, itemXpo.RevenueTypeId.Id)
                        'armamos el detalle de reconocimiento
                        .RecognitionDetail = New Domain.Entities.RecognitionDetail
                        .RecognitionDetail.Id = itemXpo.Id
                        .RecognitionDetail.RevenueTypeId = itemXpo.RevenueTypeId.Id
                        .RecognitionDetail.CategoryId = itemXpo.CategoryId.Id
                        .RecognitionDetail.RecognitionId = itemXpo.RecognitionId.Id
                        .RecognitionDetail.InitialValue = itemXpo.InitialValue
                        .RecognitionDetail.DebitValueModification = itemXpo.DebitValueModification
                        .RecognitionDetail.CreditValueModification = itemXpo.CreditValueModification
                        .RecognitionDetail.TotalRecognition = itemXpo.TotalRecognition
                        .RecognitionDetail.ExecutedValue = itemXpo.ExecutedValue
                        .RecognitionDetail.Balance = itemXpo.Balance
                        .RecognitionDetail.MarkAsUnchanged()
                    End With
                    _listRecognitionModificationDetail.Add(RecognitionModificationDetail)
                Next
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo Que Abre el pop up para agregar nuevos rubros
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddNewDetail()
        Dim frmDetail As New FrmPopUpRecognitionModification()
        frmDetail.Size = New System.Drawing.Size(800, 730)
        frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        frmDetail.ValidatyId = BudgetaryValidityId
        Dim frmTransparent As New FrmTransparent(frmDetail, False)
        If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            If Not (_listRecognitionModificationDetail IsNot Nothing AndAlso _listRecognitionModificationDetail.Count > 0) Then
                _listRecognitionModificationDetail = New List(Of RecognitionModificationDetail)
            Else
                Dim listErrors As New StringBuilder
                For Each item As Domain.Entities.RecognitionModificationDetail In frmDetail.ListRecognitionModificationDetail
                    Dim cont = (From l In _listRecognitionModificationDetail Where l.RecognitionDetail.CategoryId = item.RecognitionDetail.CategoryId And l.RecognitionDetail.RevenueTypeId = item.RecognitionDetail.RevenueTypeId).Count
                    If cont > 0 Then
                        listErrors.AppendLine("No se puede agregar el rubro " + item.CodeNameCategory + " porque ya existe en la lista del formulario principal.")
                    End If
                Next
                If listErrors.ToString.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                    Exit Sub
                End If
            End If
            _listRecognitionModificationDetail.AddRange(frmDetail.ListRecognitionModificationDetail)
            INDgcDetail.DataSource = _listRecognitionModificationDetail
            INDgcDetail.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "Handles"

#Region "Load"
    ''' <summary>
    ''' Libera la memoria del frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        _EntityDescription = Nothing
        _sequense = Nothing
        SearchMode = Nothing
        presenter = Nothing
        blockRecord = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
        recognitionModification = Nothing
        _Year = Nothing
        statusValidity = Nothing
        _IncomeMonth = Nothing
        _listRecognitionModificationDetail = Nothing
        _listDeleteRecognitionModificationDetail = Nothing
        state = Nothing
        varImp = Nothing
    End Sub


    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRecognitionModification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRecognitionModification, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        presenter = New PRecognitionModification(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
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
        Deshacer(True)
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRecognitionModification_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        INDsleEntity.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Async Sub FrmRecognitionModification_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Captura la tecla enter, para buscar un regitro por codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Me.NewEntity()
                Else
                    Await LoadControls()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Evento al dar click en agregar una nueva categoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsbAddCategory_Click(sender As Object, e As EventArgs) Handles INDsbAddCategory.Click
        AddNewDetail()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' evento para cargar resolucion , valor y estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            _EntityDescription = INDsleEntity.Text
            BudgetaryValidityId = Nothing
            BudgetaryValidityXpo = Nothing
            presenter.InitializeValidity(BudgetEntitiesId)
            SetFirstOrDefaultValidity()
            ctrTmp.RefreshInfo()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
    End Sub

    ''' <summary>
    ''' Oculta el grupo de entidad y vigencia y muestra los grupos de registro de modificacion de reconocimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If BudgetaryValidityId <> Nothing AndAlso BudgetaryValidityId <> 0 Then
            If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
                Dim item = (From l In BudgetaryValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
                If item IsNot Nothing Then
                    _Year = item.Year
                    statusValidity = item.StatusText
                    _IncomeMonth = item.IncomeMonth
                    ctrTmp.RefreshInfo()
                    CleanGroups(True)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' si la entidad y la vigencia estan seleccionados en el pop up actualiza el control de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' si la entidad y la vigencia estan seleccionados en el pop up actualiza el control de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityPopUp.EditValueChanged
        If INDsleEntityPopUp.EditValue IsNot Nothing Then
            BudgetEntitiesId = INDsleEntityPopUp.EditValue
            If INDsleEntityPopUp.Text <> String.Empty Then
                _EntityDescription = INDsleEntityPopUp.Text
            End If
            INDsleValidityPopUp.EditValue = Nothing
            BudgetaryValidityPopUpXpo = Nothing
            presenter.InitializeValidityPopUp(INDsleEntityPopUp.EditValue)
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' carga los detalles del reconocimiento en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleRecognition_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRecognition.EditValueChanged
        If RecognitionId <> Nothing AndAlso recognitionModification.ChangeTracker.State = ObjectState.Added Then
            Await LoadRecognitionDetail()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la rejilla de la naturaleza
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepIceNature_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepIceNature.EditValueChanging
        ValidateNature(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la rejilla de valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        ValidateNature(sender, e)
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' evento buttonclick que abre el formulario de registro de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
            presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Abrir busqueda con el boton en la caja de texto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCode_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbtnCode.Properties.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Abrir busqueda con el boton en la caja de texto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityPopUp.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
            presenter.InitializeBudgetEntityPopUp()
        End If
    End Sub

    ''' <summary>
    ''' Abrir busqueda con el boton en la caja de texto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRecognition_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRecognition.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("221", Nothing, True)
            presenter.InitializeBudgetEntityPopUp()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de reconocimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRecognition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRecognition.QueryPopUp
        If RecognitionXpo Is Nothing Then
            presenter.InitializeRecognition(BudgetaryValidityId)
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Evento que se dispara al desplegar con click derecho el menu sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "CustomSummaryCalculate"
    ''' <summary>
    ''' Variable para almacenar los totales
    ''' </summary>
    Private ValueTotal As Decimal
    ''' <summary>
    ''' Totaliza los valores finales 
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

#Region "Eventos Barra Botones"

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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer(False)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        state = 1
        recognitionModification.Status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer(False)
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        state = 2
        recognitionModification.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            state = 5
            recognitionModification.Status = 3
            varImp = 4
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()

                PopUpAnnulmentReason.BudgetaryValidityId = BudgetaryValidityId

                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    recognitionModification.AnnulmentConceptId = PopUpAnnulmentReason.ReversalReasonId
                    recognitionModification.AnnulmentDescription = PopUpAnnulmentReason.ReversalDescription
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
        SearchMode = False
        Deshacer(True)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            state = 4
            recognitionModification.Status = 2
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
            state = 3
            recognitionModification.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, recognitionModification.Id, 0, recognitionModification.Id)
    End Sub

#End Region

End Class
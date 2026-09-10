'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez 
' Created          : 26/08/2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data
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

Public Class FrmBudgetAmendmentsExpense
    Implements IBudgetAmendments

#Region "Build"

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
        Return New Tuple(Of Integer, String, Integer, String, String)(_budgetEntityId, _budgetEntityCodeName, _validityId, _validityCodeName, _validityStatusText)
    End Function

#End Region

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

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

#Region "Variables"

    ''' <summary>
    ''' Presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PBudgetAmendments

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Contiene la entidad de modificacion de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _budgetModification As BudgetModification

    ''' <summary>
    ''' Listado de rubros por agregar al presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listBudgetNew As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Listado de los detalles de modificación de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Property _listBudgetModificationDetail As List(Of Domain.Entities.BudgetModificationDetail)

    ''' <summary>
    ''' Listado de los detalles eliminados de modificación de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Property _listDeleteBudgetModificationDetail As List(Of Integer)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

#Region "Validity"

    ''' <summary>
    ''' Establece el id de la entidad presupuestal tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim _budgetEntityId As Integer

    ''' <summary>
    ''' Establece el codigo y nombre de la entidad presupuestal tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim _budgetEntityCodeName As String

    ''' <summary>
    ''' Establece el id de la vigencia tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityId As Integer

    ''' <summary>
    ''' Establece el codigo y nombre de la vigencia tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityCodeName As String

    ''' <summary>
    ''' Representa al año
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityYear As Integer

    ''' <summary>
    ''' Representa al mes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityMonth As Integer

    ''' <summary>
    ''' Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityStatus As String

    ''' <summary>
    ''' Texto Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityStatusText As String

    ''' <summary>
    ''' Controla el changed de los search
    ''' </summary>
    ''' <remarks></remarks>
    Dim _controlerChanged As Boolean

#End Region

#End Region

#Region "Properties"

    ''' <summary>
    ''' Devuelve el tag del frontal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IBudgetAmendments.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' devuelve los layout controls
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IBudgetAmendments.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As BudgetSequence Implements IBudgetAmendments.Sequense
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
    ''' Obtiene o establece el id de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesId As Integer Implements IBudgetAmendments.BudgetEntitiesId
        Get
            Return INDsleEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityIdPopup As Integer? Implements IBudgetAmendments.BudgetEntityIdPopup
        Get
            Return INDsleEntityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityPopUp.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer Implements IBudgetAmendments.BudgetaryValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityIdPopup As Integer? Implements IBudgetAmendments.ValidityIdPopup
        Get
            Return INDsleValidityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleValidityPopUp.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IBudgetAmendments.Code
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
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements IBudgetAmendments.DocumentDate
        Get
            Return INDteDate.EditValue
        End Get
        Set(value As Date)
            INDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentText As String Implements IBudgetAmendments.DocumentText
        Get
            Return INDtxtDocument.EditValue
        End Get
        Set(value As String)
            INDtxtDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la Observación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements IBudgetAmendments.Observations
        Get
            Return INDmeObservations.EditValue
        End Get
        Set(value As String)
            INDmeObservations.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBudgetAmendments.BudgetEntitiesXpo
        Get
            Return INDsleEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesPopUpXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBudgetAmendments.BudgetEntitiesPopUpXpo
        Get
            Return INDsleEntityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection Implements IBudgetAmendments.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityPopUpXpo As DevExpress.Xpo.XPCollection Implements IBudgetAmendments.BudgetaryValidityPopUpXpo
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICRUD"
    ''' <summary>
    ''' Metodo buscar
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub
    ''' <summary>
    ''' Metodo deshacer
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Undo(False)
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Undo(Optional withBudgetEntity As Boolean = True)
        Await CleanControls(withBudgetEntity)
        If withBudgetEntity Then
            BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Else
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
        INDsleEntity.Focus()
    End Sub
    ''' <summary>
    ''' Metodo Eliminar de IcrudBase sin usar
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar
    End Sub
    ''' <summary>
    ''' Metodo Guardar
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If _budgetModification.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            Dim errors As String = ValidateDetail()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MBudgetModification(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveBudgetMoficationAsync(_budgetModification, _listDeleteBudgetModificationDetail)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    If Not String.IsNullOrEmpty(result.MessageAux) Then
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageAux
                    End If

                    Me._budgetModification = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _budgetModification.Id, 0, _budgetModification.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _budgetModification.Id, 0, _budgetModification.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _budgetModification.Id, 0, _budgetModification.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _budgetModification.Id, 0, _budgetModification.Id)
                    End Select
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

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

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            NewEntity()
        End If
    End Sub

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
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName"}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {_validityId, 2}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBudgetModifications
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

#End Region

#Region "Methods"

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
    ''' Acciones sobre los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBudgetAmendments.ActionsOnControls
        Set(value As Boolean)
            INDlcBudgetM.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDteDate.Enabled = value
            INDtxtDocument.Enabled = value
            INDmeObservations.Enabled = value
            INDsbAddCategory.Enabled = value
            INDgcDetail.Enabled = value
            INDlcBudgetM.EndUpdate()
            If value = True Then
                INDteDate.Focus()
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
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <param name="withBudgetaryEntity">saber si se limpia tabn la entidad presupuestal o no</param>
    ''' <remarks></remarks>
    Private Async Function CleanControls(Optional withBudgetaryEntity As Boolean = True) As Task
        INDlcBudgetM.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbtnCode.Text = String.Empty
        INDteDate.EditValue = Me.GetDateServer()
        INDtxtDocument.Text = String.Empty
        INDmeObservations.Text = String.Empty
        INDgcDetail.DataSource = Nothing

        _doc = Nothing
        _budgetModification = Nothing
        _listBudgetNew = Nothing
        _listBudgetModificationDetail = Nothing
        _listBudgetModificationDetail = New List(Of Domain.Entities.BudgetModificationDetail)

        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.ControlHideStatus = False
        INDsleEntityPopUp.Properties.ReadOnly = False
        INDsleValidityPopUp.Properties.ReadOnly = False

        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If withBudgetaryEntity = True Then
            BudgetaryValidityId = Nothing
            ValidityIdPopup = Nothing
            _validityId = Nothing
            _validityYear = Nothing
            _validityMonth = Nothing
            _validityStatus = Nothing
            _validityStatusText = Nothing
            LoadLayoutGroup(False)
        End If

        INDlcBudgetM.EndUpdate()
    End Function

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        INDbtnCode.Text = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecord = "1"
        Me.BarraBotones.ControlHideStatus = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        INDsleEntityPopUp.Properties.ReadOnly = True
        INDsleValidityPopUp.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup(ByVal Bandera As Boolean)
        If Bandera Then
            INDlyGrBudgetAmendmentsRevenue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyGrRubro.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If BudgetEntitiesPopUpXpo Is Nothing Then
                _presenter.InitializeBudgetEntityPopUp()
            End If
            INDsleEntityPopUp.EditValue = BudgetEntitiesId
            INDsleValidityPopUp.EditValue = BudgetaryValidityId

            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.ControlHideStatus = False
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Else
            INDlyGrBudgetAmendmentsRevenue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyGrRubro.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.BarraBotones.StatusRecordVisible = False
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
                _validityYear = item.Year
                _validityStatus = item.Status
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que coloca el estado en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatus()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 AndAlso BudgetaryValidityId <> 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
            If item IsNot Nothing Then
                _validityStatus = item.Status
                Select Case item.Status
                    Case 1
                        _validityStatusText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        _validityStatusText = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        _validityStatusText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        _validityStatusText = String.Empty
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que coloca el estado en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatusPopup()
        If BudgetaryValidityPopUpXpo IsNot Nothing AndAlso BudgetaryValidityPopUpXpo.Count > 0 AndAlso ValidityIdPopup IsNot Nothing Then
            Dim item = (From l In BudgetaryValidityPopUpXpo Where l.Id = ValidityIdPopup Select l).FirstOrDefault
            If item IsNot Nothing Then
                _validityStatus = item.Status
                Select Case item.Status
                    Case 1
                        _validityStatusText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        _validityStatusText = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        _validityStatusText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        _validityStatusText = String.Empty
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Using Model As New MBudgetModification(MyTag)
            AsyncLoader(True)
            INDlcBudgetM.BeginUpdate()
            _budgetModification = Await Model.GetBudgetModificationAsync(Me.INDbtnCode.Text.Trim, 2, _validityId)
            If Not _budgetModification Is Nothing AndAlso _budgetModification.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, _budgetModification.Id)
                INDsleEntityPopUp.Properties.ReadOnly = True
                INDsleValidityPopUp.Properties.ReadOnly = True
                With _budgetModification
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                    INDbtnCode.EditValue = .Code
                    INDteDate.EditValue = .DocumentDate
                    INDtxtDocument.Text = .Document
                    INDmeObservations.Text = .Observations
                    _listBudgetModificationDetail = .BudgetModificationDetail.ToList()

                    INDgcDetail.DataSource = Nothing
                    INDgcDetail.DataSource = _listBudgetModificationDetail

                    Me.BarraBotones.StatusRecord = .Status.ToString()
                    Me.BarraBotones.ControlHideStatus = True
                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me._budgetModification.Code)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _budgetModification.Id}
                    Dim operation = Await Model.SaveBlockRecord(_blockRecord)
                    _blockRecord = operation.ObjectEmbbeded
                Else
                    _blockRecord = result
                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                If _budgetModification.Status = 1 Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                End If

                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.SetDocuments(_budgetModification.Id)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, _budgetModification.Id, 0, _budgetModification.Id)

                AsyncLoader(False)
                ActionsOnControls = True
            Else
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewEntity()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Deshacer()
                    INDbtnCode.Focus()
                End If
            End If
        End Using
        INDlcBudgetM.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewEntity()
        If INDsleEntity.EditValue IsNot Nothing AndAlso CType(INDsleEntity.EditValue, String) <> "" Then
            _budgetModification = New Domain.Entities.BudgetModification() With {.Status = 1}
            If Me._sequense.IsManual Then
                Me.ActionsOnControls = True
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
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), _budgetModification.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & _budgetModification.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), _budgetModification.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), _budgetModification.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), _budgetModification.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBudgetModification(MyTag)
                Await Model.DeleteBlockRecord(_blockRecord)
            End Using
            _blockRecord = Nothing
        End If
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._budgetModification IsNot Nothing AndAlso Me._budgetModification.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Await DeleteBlockedRecord()
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
    ''' Metodo que valida la naturaleza con respecto al saldo y el valor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateNature(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim budgetModificationDetail As Domain.Entities.BudgetModificationDetail = CType(INDgvDetail.GetFocusedRow, Domain.Entities.BudgetModificationDetail)
            If budgetModificationDetail IsNot Nothing Then
                Dim control As DevExpress.XtraEditors.BaseEdit = sender
                Select Case True
                    Case control.EditorTypeName = controlTextEdit
                        budgetModificationDetail.Value = e.NewValue
                    Case control.EditorTypeName = controlImageComboBox
                        budgetModificationDetail.Nature = e.NewValue
                End Select
                If budgetModificationDetail.Nature = 1 Then
                    If budgetModificationDetail.BalanceBudget < budgetModificationDetail.Value Then
                        Mensaje(EeventViewerImages.Advertencia) = "Con la naturaleza débito el saldo no puede ser menor al valor."
                        Select Case True
                            Case control.EditorTypeName = controlTextEdit
                                budgetModificationDetail.Value = e.OldValue
                            Case control.EditorTypeName = controlImageComboBox
                                budgetModificationDetail.Nature = e.OldValue
                        End Select
                        e.Cancel = True
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim detail As BudgetModificationDetail = INDgvDetail.GetFocusedRow
        If detail.Id > 0 Then
            If _listDeleteBudgetModificationDetail Is Nothing Then
                _listDeleteBudgetModificationDetail = New List(Of Integer)
            End If
            detail.MarkAsDeleted()
            _listDeleteBudgetModificationDetail.Add(detail.Id)
        End If
        _listBudgetModificationDetail.Remove(detail)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listBudgetModificationDetail
    End Sub

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As String
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de traslado en la rejilla
        If Not (_listBudgetModificationDetail IsNot Nothing AndAlso _listBudgetModificationDetail.Any()) Then
            listErrors.AppendLine("Se debe agregar minimo un detalle para la modificación.")
        End If

        If _validityId = 0 Then
            listErrors.AppendLine("Debe elegir una vigencia.")
        ElseIf Not {1, 2}.Contains(_validityStatus) Then
            listErrors.AppendLine(String.Format("La vigencia se encuentra en estado {0}.", _validityStatusText))
        End If

        'Se valida que el mes y el año de la fecha del documento sea igual al mes y año de la vigencia
        If Year(DocumentDate) <> CInt(_validityYear) Then
            listErrors.AppendLine(String.Format(ResourceManager.GetString("SelectYear", NAME_MODULE), _validityYear.ToString))
        End If
        If Month(DocumentDate) <> Me._validityMonth Then
            listErrors.AppendLine(String.Format(ResourceManager.GetString("SelectMonth", NAME_MODULE), MonthName(Me._validityMonth, False)))
        End If

        'Se valida que el debito sea igual que el credito
        If _listBudgetModificationDetail IsNot Nothing AndAlso _listBudgetModificationDetail.Count > 0 Then
            'Se valida que en los detalles no haya ningun item con naturaleza ninguna
            _listBudgetModificationDetail.ForEach(Sub(item)
                                                      If item.Nature = 0 Then
                                                          listErrors.AppendLine("La naturaleza del rubro " + item.CodeCategory + " - " + item.NameCategory + " con el tipo de gasto " + item.CodeNameRevenueType + " no puede ser Ninguna.")
                                                      End If
                                                      If item.Nature = 1 And item.Value > item.BalanceBudget Then
                                                          listErrors.AppendLine("El valor debito del rubro " + item.CodeCategory + " - " + item.NameCategory + " con el tipo de gasto " + item.CodeNameRevenueType + " no puede ser mayor que el saldo.")
                                                      End If
                                                  End Sub)
        End If

        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' asigna valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssigningValues()
        With _budgetModification
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = _idOperativeUnit
            .BudgetaryValidityId = _validityId
            .Code = Code
            .DocumentDate = DocumentDate
            .DocumentSource = 2
            .Document = INDtxtDocument.Text
            .Observations = Observations
            .BudgetModificationDetail.Clear()
            If _listBudgetModificationDetail IsNot Nothing AndAlso _listBudgetModificationDetail.Count > 0 Then
                _listBudgetModificationDetail.FindAll(Function(item) item.Value > 0 AndAlso (item.ChangeTracker.State = ObjectState.Added OrElse item.ChangeTracker.State = ObjectState.Modified)).ForEach(Sub(item)
                                                                                                                                                                                                               .BudgetModificationDetail.Add(item)
                                                                                                                                                                                                           End Sub)
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#End Region

#Region "Handles"

#Region "Load"

    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetAmendments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcBudgetM, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PBudgetAmendments(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDgcDetail)

        'Cargar las acciones a la rejilla
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDetail.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        LoadStatus()
        Undo()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing

        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _blockRecord = Nothing
        _budgetModification = Nothing
        _listBudgetNew = Nothing
        _listBudgetModificationDetail = Nothing
        _listDeleteBudgetModificationDetail = Nothing
        varImp = Nothing

        _budgetEntityId = Nothing
        _budgetEntityCodeName = Nothing
        _validityId = Nothing
        _validityCodeName = Nothing
        _validityYear = Nothing
        _validityMonth = Nothing
        _validityStatus = Nothing
        _validityStatusText = Nothing
        _controlerChanged = Nothing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetAmendments_Activated(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleEntity.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Async Sub FrmAmendments_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
    Private Async Sub INDtxtConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _validityId = 0 Then
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

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            _presenter.InitializeBudgetEntity()
        End If
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
            _presenter.InitializeBudgetEntity()
        End If
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
            _presenter.InitializeBudgetEntityPopUp()
        End If
    End Sub

    ''' <summary>
    ''' Click en remover item de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcDetail_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles INDgcDetail.EmbeddedNavigator.ButtonClick
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
            e.Handled = True
            _budgetModification.BudgetModificationDetail.Item(_budgetModification.BudgetModificationDetail.IndexOf(CType(INDgvDetail.GetFocusedRow, BudgetModificationDetail))).MarkAsDeleted()
            INDgcDetail.DataSource = _budgetModification.BudgetModificationDetail
            INDgcDetail.RefreshDataSource()
        End If
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
            _budgetEntityId = BudgetEntitiesId
            _budgetEntityCodeName = INDsleEntity.Text

            BudgetaryValidityId = Nothing
            BudgetaryValidityXpo = Nothing
            _presenter.InitializeValidity(BudgetEntitiesId)
            SetFirstOrDefaultValidity()

            _presenter.InitializeBudgetEntityPopUp()
            _controlerChanged = True
            BudgetEntityIdPopup = BudgetEntitiesId
            _controlerChanged = False
            _presenter.InitializeValidityPopUp(BudgetEntityIdPopup)

            ctrTmp.RefreshInfo()
        End If
    End Sub

    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityPopUp.EditValueChanged
        If INDsleEntityPopUp.EditValue IsNot Nothing AndAlso _controlerChanged = False Then
            _budgetEntityId = BudgetEntityIdPopup
            _budgetEntityCodeName = INDsleEntityPopUp.Text

            _validityId = 0
            _validityCodeName = String.Empty
            _validityStatus = 0
            _validityStatusText = String.Empty
            ValidityIdPopup = Nothing
            BudgetaryValidityPopUpXpo = Nothing
            _presenter.InitializeValidityPopUp(BudgetEntityIdPopup)
            ctrTmp.RefreshInfo()
        End If
    End Sub

    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If BudgetaryValidityId <> 0 Then
            _validityId = BudgetaryValidityId
            _validityCodeName = INDsleValidity.Text

            _controlerChanged = True
            ValidityIdPopup = BudgetaryValidityId
            _controlerChanged = False

            SetStatus()
            ctrTmp.RefreshInfo()
            LoadLayoutGroup(True)
        End If
    End Sub

    Private Sub INDsleValidityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidityPopUp.EditValueChanged
        If ValidityIdPopup IsNot Nothing AndAlso ValidityIdPopup <> 0 AndAlso _controlerChanged = False Then
            _validityId = ValidityIdPopup
            _validityCodeName = INDsleValidityPopUp.Text

            Dim item = (From l In BudgetaryValidityPopUpXpo Where l.Id = ValidityIdPopup Select l).FirstOrDefault
            _validityYear = item.Year
            _validityMonth = item.ExpenseMonth

            SetStatusPopup()
            ctrTmp.RefreshInfo()
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

#Region "Click"

    ''' <summary>
    ''' Click en adicionar modificacion detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAddCategory_Click(sender As Object, e As EventArgs) Handles INDsbAddCategory.Click
        Dim frmDetail As New FrmModificationShowDialogExpense(2, Me.MyTag, _validityId, _listBudgetNew)
        frmDetail.Size = New System.Drawing.Size(800, 730)
        frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim frmTransparent As New FrmTransparent(frmDetail, False)
        _listBudgetNew = New List(Of Domain.Entities.Budget)
        If frmTransparent.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If Not (_listBudgetModificationDetail IsNot Nothing AndAlso _listBudgetModificationDetail.Count > 0) Then
                _listBudgetModificationDetail = New List(Of BudgetModificationDetail)
            Else
                Dim listErrors As New StringBuilder
                For Each item As Domain.Entities.BudgetModificationDetail In frmDetail.ListModificationDetail
                    Dim cont = (From l In _listBudgetModificationDetail Where l.CategoryId = item.CategoryId AndAlso l.RevenueTypeId = item.RevenueTypeId Select l).Count
                    If cont > 0 Then
                        listErrors.AppendLine("No se puede agregar el rubro " + item.CodeCategory + " - " + item.NameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " porque ya existe en la lista del formulario principal.")
                    End If
                Next
                If listErrors.ToString.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                    Exit Sub
                End If
            End If
            _listBudgetModificationDetail.AddRange(frmDetail.ListModificationDetail)
            If frmDetail.ListBudgetEntryNew IsNot Nothing AndAlso frmDetail.ListBudgetEntryNew.Count > 0 Then
                _listBudgetNew.AddRange(frmDetail.ListBudgetEntryNew)
            End If
            INDgcDetail.DataSource = _listBudgetModificationDetail
            INDgcDetail.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "CustomSummaryCalculate"

    Private ValueTotal As Decimal
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

#End Region

#Region "Buttons Bar"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Undo(False)
    End Sub

    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        Undo(True)
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
        _budgetModification.Status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _budgetModification.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _budgetModification.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _budgetModification.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _budgetModification.Status = 3
            varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _budgetModification.Id, 0, _budgetModification.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
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

#End Region

End Class
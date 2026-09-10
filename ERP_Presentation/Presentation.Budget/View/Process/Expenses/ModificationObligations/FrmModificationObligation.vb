'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
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

Public Class FrmModificationObligation
    Implements IModificationObligations

#Region "Builder"

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
    ''' Presentador del form
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PModificationObligations

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
    ''' Representa a la entidad de modificacion de obligaciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim _obligationModification As ObligationModification

    ''' <summary>
    ''' Listado de detalles de la modificacion de la obligacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listObligationModificationDetail As List(Of ObligationModificationDetail)

    ''' <summary>
    ''' Listado de eliminados de detalles de la modificacion de la obligacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteObligationModificationDetail As List(Of Integer)

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
    ''' Retorna el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IModificationObligations.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Retorna el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IModificationObligations.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As BudgetSequence Implements IModificationObligations.Sequense
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
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesId As Integer Implements IModificationObligations.BudgetEntitiesId
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityIdPopup As Integer? Implements IModificationObligations.BudgetEntityIdPopup
        Get
            Return INDsleEntityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityPopUp.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer Implements IModificationObligations.BudgetaryValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityIdPopup As Integer? Implements IModificationObligations.ValidityIdPopup
        Get
            Return INDsleValidityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleValidityPopUp.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IModificationObligations.Code
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
    ''' Obtiene o establece la fecha
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements IModificationObligations.DocumentDate
        Get
            Return INDdteDateDocument.EditValue
        End Get
        Set(value As Date)
            INDdteDateDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la obligacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObligationId As Integer Implements IModificationObligations.ObligationId
        Get
            Return INDsleObligation.EditValue
        End Get
        Set(value As Integer)
            INDsleObligation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de documento hasta el cual se reintegra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpTo As Byte Implements IModificationObligations.UpTo
        Get
            Return INDsleUntil.EditValue
        End Get
        Set(value As Byte)
            INDsleUntil.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Document As String Implements IModificationObligations.Document
        Get
            Return INDtxtDocument.EditValue
        End Get
        Set(value As String)
            INDtxtDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements IModificationObligations.Observations
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesXpo As XPInstantFeedbackSource Implements IModificationObligations.BudgetEntitiesXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesPopUpXpo As XPInstantFeedbackSource Implements IModificationObligations.BudgetEntitiesPopUpXpo
        Get
            Return INDsleEntityPopUp.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As XPCollection Implements IModificationObligations.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityPopUpXpo As XPCollection Implements IModificationObligations.BudgetaryValidityPopUpXpo
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la obligacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObligationXpo As XPInstantFeedbackSource Implements IModificationObligations.ObligationXpo
        Get
            Return INDsleObligation.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleObligation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene la lista de hasta donde reintegrar
    ''' </summary>
    Private _ListUntil As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property ListUntil As List(Of Tuple(Of Integer, String))
        Get
            If _ListUntil Is Nothing Then
                _ListUntil = New List(Of Tuple(Of Integer, String))
                _ListUntil.Add(New Tuple(Of Integer, String)(2, "Compromiso"))
                _ListUntil.Add(New Tuple(Of Integer, String)(3, "Disponibilidad"))
                _ListUntil.Add(New Tuple(Of Integer, String)(4, "Presupuesto"))
            End If
            Return _ListUntil
        End Get
    End Property

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
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
        INDsleBudgetEntity.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que elimina la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Metodo Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If _obligationModification.Status <> 3 Then
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
            Using model As New MModificationObligation(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveObligationModification(_obligationModification, _listDeleteObligationModificationDetail)
                AsyncLoader(False)
                If Result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    If Not String.IsNullOrEmpty(Result.MessageAux) Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.MessageAux
                    End If

                    _obligationModification = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _obligationModification.Id, 0, _obligationModification.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _obligationModification.Id, 0, _obligationModification.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _obligationModification.Id, 0, _obligationModification.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _obligationModification.Id, 0, _obligationModification.Id)
                    End Select
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
    ''' Metodo para actualizar, actualemente no se esta usando
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad que establece los mensajes 
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' MEtodo Cuando se da click en boton nuevo
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
    ''' Metodo para abrir busqueda
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
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo With {.Caption = "Obligación", .FieldName = "ObligationId.Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo With {.Caption = "Documento", .FieldName = "Document", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "ObligationId.ThirdPartyId.NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.28)},
                              New ColumnInfo With {.Caption = "Hasta", .FieldName = "UpToName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {_validityId}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListObligationModificationByValidityId
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
    ''' Activa o Inactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IModificationObligations.ActionsOnControls
        Set(value As Boolean)
            INDlyModificationObligations.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDateDocument.Enabled = value
            INDsleObligation.Enabled = value
            INDsleUntil.Enabled = value
            INDtxtDocument.Enabled = value
            INDmemoObservations.Enabled = value
            INDbtnAddDetail.Enabled = value
            INDgcDetail.Enabled = value
            INDlyModificationObligations.EndUpdate()
            If value Then
                INDdteDateDocument.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <param name="withBudgetaryEntity">saber si se limpia tabn la entidad presupuestal o no</param>
    ''' <remarks></remarks>
    Private Async Function CleanControls(Optional withBudgetaryEntity As Boolean = True) As Task
        INDlyModificationObligations.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbtnCode.Text = String.Empty
        INDdteDateDocument.EditValue = Me.GetDateServer()
        INDsleObligation.EditValue = Nothing
        INDsleObligation.Properties.NullText = String.Empty
        INDsleUntil.EditValue = Nothing
        INDtxtDocument.EditValue = String.Empty
        INDmemoObservations.EditValue = String.Empty
        INDgcDetail.DataSource = Nothing

        _doc = Nothing
        _obligationModification = Nothing
        _listObligationModificationDetail = Nothing
        _listDeleteObligationModificationDetail = Nothing

        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.ControlHideStatus = False
        INDsleEntityPopUp.Properties.ReadOnly = False
        INDsleValidityPopUp.Properties.ReadOnly = False

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If withBudgetaryEntity = True Then
            BudgetaryValidityId = Nothing
            ValidityIdPopup = Nothing
            _validityId = Nothing
            _validityYear = Nothing
            _validityMonth = Nothing
            _validityStatus = Nothing
            _validityStatusText = Nothing
            INDsleEntityPopUp.EditValue = Nothing
            INDsleValidityPopUp.EditValue = Nothing

            LoadLayoutGroup(False)
        End If

        INDlyModificationObligations.EndUpdate()
    End Function

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
        INDsleEntityPopUp.Properties.ReadOnly = True
        INDsleValidityPopUp.Properties.ReadOnly = True
        DocumentDate = GetDateServer()
    End Sub

    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup(ByVal Bandera As Boolean)
        If Bandera Then
            INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlygObligationDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
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
            INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygObligationDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
    ''' Activa o inactiva algunas columnas de l rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FieldsGrid()
        If _obligationModification.ObligationType = 1 Then '1 Obligacion
            INDcolCommitmentBalance.Visible = True
            INDcolObligationBalance.Caption = "Saldo Obli."
        Else '2 CxP
            INDcolCommitmentBalance.Visible = False
            INDcolObligationBalance.Caption = "Saldo CxP"
        End If
        'Asigno los visibleIndex para que aparezcan en orden las columnas
        Dim cont As Integer = 0
        For iColumns = 0 To viewDetail.Columns.Count - 1
            If viewDetail.Columns.Item(iColumns).Visible = True Then
                viewDetail.Columns.Item(iColumns).VisibleIndex = cont
                cont += 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que agrega un detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddNewDetail()
        If ObligationId = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una Obligación."
            Exit Sub
        End If

        Using Formulario As New FrmAddDetail
            AddHandler Formulario.AddInfoFormModalToFormPrincipal, AddressOf AddInfoFormModalToFormPrincipal
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 760
            Formulario.Height = 476
            Formulario.ObligationId = ObligationId
            Formulario.ObligationCode = INDsleObligation.Text
            Formulario.ListObligationModificationDetail = _listObligationModificationDetail
            Dim frm As New FrmTransparent(Formulario, False)
            frm.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Agrega la informacion que viene del formulario modal a la rejilla
    ''' del formulario principal
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddInfoFormModalToFormPrincipal(e As AddInfoEventArgs)
        If _listObligationModificationDetail Is Nothing Then
            _listObligationModificationDetail = New List(Of ObligationModificationDetail)
        End If
        Dim listToAggregate As New List(Of ObligationModificationDetail)
        listToAggregate.AddRange(e.ListObligationModificationDetail.Select(Function(x) x.Clone()).Cast(Of ObligationModificationDetail).ToList())
        For Each item In e.ListObligationModificationDetail
            If _listObligationModificationDetail.Any(Function(d) d.ObligationDetailId = item.ObligationDetailId) Then
                listToAggregate.RemoveAll(Function(d) d.ObligationDetailId = item.ObligationDetailId)
            End If
        Next

        If listToAggregate.Count > 0 Then
            _listObligationModificationDetail.AddRange(listToAggregate)
            INDgcDetail.DataSource = Nothing
            INDgcDetail.DataSource = _listObligationModificationDetail
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        End If

        If _listObligationModificationDetail IsNot Nothing AndAlso _listObligationModificationDetail.Count > 0 Then
            INDsleObligation.Properties.ReadOnly = True
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Using Model As New MModificationObligation(MyTag)
            AsyncLoader(True)
            INDlyModificationObligations.BeginUpdate()
            Dim resultOperation = Await Model.GetObligationModification(Code, _validityId)
            If Not resultOperation.StateResult Then
                Me.Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                Deshacer()
                Exit Function
            End If
            _obligationModification = resultOperation.ObjectEmbbeded
            If Not _obligationModification Is Nothing AndAlso _obligationModification.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(MyTag, _obligationModification.Id)
                INDsleObligation.Properties.ReadOnly = True
                INDsleEntityPopUp.Properties.ReadOnly = True
                INDsleValidityPopUp.Properties.ReadOnly = True
                With _obligationModification
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                    Code = .Code
                    DocumentDate = .DocumentDate
                    ObligationId = .ObligationId
                    INDsleObligation.Properties.NullText = .CodeObligation
                    UpTo = .UpTo
                    Document = .Document
                    Observations = .Observations
                    _listObligationModificationDetail = .ObligationModificationDetail.ToList

                    INDgcDetail.DataSource = Nothing
                    INDgcDetail.DataSource = _listObligationModificationDetail

                    FieldsGrid()

                    Me.BarraBotones.StatusRecord = .Status.ToString()
                    Me.BarraBotones.ControlHideStatus = True
                End With
                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._obligationModification.Code)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _obligationModification.Id}
                    Dim operation = Await Model.SaveBlockRecord(_blockRecord)
                    _blockRecord = operation.ObjectEmbbeded
                Else
                    _blockRecord = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                If _obligationModification.Status = 1 Then 'Registrado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Else 'Confirmado o Anulado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                End If

                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.SetDocuments(_obligationModification.Id)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, _obligationModification.Id, 0, _obligationModification.Id)

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
        INDlyModificationObligations.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewEntity()
        If INDsleBudgetEntity.EditValue IsNot Nothing AndAlso CType(INDsleBudgetEntity.EditValue, String) <> "" Then
            _obligationModification = New ObligationModification With {.Status = 1}
            If Me._sequense.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.StatusRecord = "1"
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
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SeleccioneEntidadPresupuestal", NAME_MODULE)
            INDsleBudgetEntity.Focus()
        End If
    End Sub

    ''' <summary>
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._obligationModification.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._obligationModification.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._obligationModification.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._obligationModification.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._obligationModification.Code)
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
        If Me._obligationModification IsNot Nothing AndAlso Me._obligationModification.Id > 0 Then
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
            Dim obligationModificationDetail As ObligationModificationDetail = CType(viewDetail.GetFocusedRow, ObligationModificationDetail)
            If obligationModificationDetail IsNot Nothing Then
                Dim message As New StringBuilder
                Dim control As DevExpress.XtraEditors.BaseEdit = sender

                Select Case True
                    Case control.EditorTypeName = controlTextEdit
                        obligationModificationDetail.Value = e.NewValue
                    Case control.EditorTypeName = controlImageComboBox
                        obligationModificationDetail.Nature = e.NewValue
                End Select

                If obligationModificationDetail.Nature = 1 Then 'Debito: el valor no puede ser mayor al saldo de la obligacion
                    If obligationModificationDetail.Value > obligationModificationDetail.BalanceObligation Then
                        message.AppendLine("El valor no puede ser mayor al saldo de la obligación.")
                    End If
                ElseIf obligationModificationDetail.Nature = 2 Then 'Pregunto por el tipo de obligacion
                    If _obligationModification.ObligationType = 1 Then '1 Obligacion
                        'Credito: el valor no puede ser mayor al saldo del compromiso
                        If Not (UpTo > 2) AndAlso obligationModificationDetail.Value > obligationModificationDetail.BalanceCommitment Then
                            message.AppendLine("El valor no puede ser mayor al saldo del compromiso.")
                        End If
                    Else '2 CxP
                        If UpTo <> 4 Then
                            message.AppendLine("La modificación debe ir hasta el presupuesto cuando el tipo de obligación es CxP.")
                        End If
                    End If
                End If

                If message.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = message.ToString
                    Select Case True
                        Case control.EditorTypeName = controlTextEdit
                            obligationModificationDetail.Value = e.OldValue
                        Case control.EditorTypeName = controlImageComboBox
                            obligationModificationDetail.Nature = e.OldValue
                    End Select
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la modificacion del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        If _obligationModification.Status > 1 Then
            If _obligationModification.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar el registro porque esta confirmado"
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar el registro porque esta anulado"
            End If
            Exit Sub
        End If

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim detail As ObligationModificationDetail = viewDetail.GetFocusedRow
        If detail.Id > 0 Then
            If _listDeleteObligationModificationDetail Is Nothing Then
                _listDeleteObligationModificationDetail = New List(Of Integer)
            End If
            detail.MarkAsDeleted()
            _listDeleteObligationModificationDetail.Add(detail.Id)
        End If
        _listObligationModificationDetail.Remove(detail)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listObligationModificationDetail
        If _listObligationModificationDetail Is Nothing OrElse _listObligationModificationDetail.Count = 0 Then
            INDsleObligation.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Valida los detalles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As String
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de traslado en la rejilla
        If Not (_listObligationModificationDetail IsNot Nothing AndAlso _listObligationModificationDetail.Any(Function(d) d.Value > 0)) Then
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

        'Se valida los datos del detalle
        If _listObligationModificationDetail IsNot Nothing AndAlso _listObligationModificationDetail.Count > 0 Then
            _listObligationModificationDetail.ForEach(Sub(item)
                                                          If item.Nature = 0 Then 'Se valida que en los detalles no haya ningun item con naturaleza ninguna
                                                              listErrors.AppendLine("La naturaleza del rubro " + item.CodeNameCategory + " con el tipo de gasto " + item.CodeNameRevenueType + " no puede ser Ninguna.")
                                                          ElseIf item.Nature = 1 AndAlso item.Value > item.BalanceObligation Then
                                                              listErrors.AppendLine("El valor debito del rubro " + item.CodeNameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " no puede ser mayor que el saldo del compromiso.")
                                                          ElseIf item.Nature = 2 Then
                                                              If _obligationModification.ObligationType = 1 Then '1 Obligacion
                                                                  If Not (UpTo > 2) AndAlso item.Value > item.BalanceCommitment Then 'Credito: el valor no puede ser mayor al saldo del compromiso
                                                                      listErrors.AppendLine("El valor credito del rubro " + item.CodeNameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " no puede ser mayor que el saldo del compromiso.")
                                                                  End If
                                                              Else '2 CxP
                                                                  If UpTo <> 4 Then
                                                                      listErrors.AppendLine("La modificación debe ir hasta el presupuesto cuando el tipo de obligación es CxP.")
                                                                  End If
                                                              End If
                                                          End If
                                                      End Sub)
        End If

        Return listErrors.ToString()
    End Function



    ''' <summary>
    ''' Metodos para asignar valores a la entidad financial source
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _obligationModification
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = _idOperativeUnit
            .BudgetaryValidityId = _validityId
            .Code = Code
            .DocumentDate = DocumentDate
            .ObligationId = ObligationId
            .UpTo = UpTo
            .Document = Document
            .Observations = Observations
            .ObligationModificationDetail.Clear()
            If _listObligationModificationDetail IsNot Nothing AndAlso _listObligationModificationDetail.Count > 0 Then
                _listObligationModificationDetail.FindAll(Function(item) item.Value > 0 AndAlso (item.ChangeTracker.State = ObjectState.Added OrElse item.ChangeTracker.State = ObjectState.Modified)).ForEach(Sub(item)
                                                                                                                                                                                                                   .ObligationModificationDetail.Add(item)
                                                                                                                                                                                                               End Sub)
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Loado del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmModificationObligation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyModificationObligations, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PModificationObligations(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDgcDetail)

        'Cargar las acciones a la rejilla
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewDetail.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        LoadStatus()
        Me.Undo()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing

        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _blockRecord = Nothing
        _obligationModification = Nothing
        _listObligationModificationDetail = Nothing
        _listDeleteObligationModificationDetail = Nothing
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
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmModificationObligation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDsleUntil.Properties.DataSource = ListUntil
        INDsleBudgetEntity.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmModificationObligation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
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

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se ejecuta al desplegar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            _presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de obligaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleObligation_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleObligation.QueryPopUp
        If ObligationXpo Is Nothing Then
            _presenter.InitializeObligations(_validityId)
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que abre el form de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeBudgetEntity()
                If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
                    _presenter.InitializeValidity(BudgetEntitiesId)
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de entidad presupuestal sobre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityPopUp.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeBudgetEntityPopUp()
                If BudgetEntityIdPopup IsNot Nothing AndAlso BudgetEntityIdPopup <> 0 Then
                    _presenter.InitializeValidityPopUp(BudgetEntityIdPopup)
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de obligaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleObligation_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleObligation.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmObligation
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeObligations(_validityId)
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            _budgetEntityId = BudgetEntitiesId
            _budgetEntityCodeName = INDsleBudgetEntity.Text

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

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de entidad presupuestal del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de vigencia del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidityPopUp.EditValueChanged
        If ValidityIdPopup IsNot Nothing AndAlso ValidityIdPopup <> 0 AndAlso _controlerChanged = False Then
            _validityId = ValidityIdPopup
            _validityCodeName = INDsleValidityPopUp.Text

            Dim item = (From l In BudgetaryValidityPopUpXpo Where l.Id = ValidityIdPopup Select l).FirstOrDefault
            _validityYear = item.Year
            _validityMonth = item.ExpenseMonth
            ObligationXpo = Nothing

            SetStatusPopup()
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la obligacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleObligation_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleObligation.EditValueChanged
        If ObligationId <> Nothing AndAlso _obligationModification.ChangeTracker.State = ObjectState.Added Then
            Dim obligationXpo = DirectCast(DirectCast(viewSearchObligation.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BudgetRepository.BudgetObligationXpo)
            If obligationXpo IsNot Nothing Then
                _obligationModification.ObligationType = obligationXpo.ObligationType
                FieldsGrid()
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de la naturaleza de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepIceNature_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepIceNature.EditValueChanging
        ValidateNature(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio del valor de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        ValidateNature(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de fecha de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepDteExpiredDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepDteExpiredDate.EditValueChanging
        If e.NewValue < DocumentDate Then
            Mensaje(EeventViewerImages.Advertencia) = "La fecha de vencimiento no puede ser menor a la fecha del documento."
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento click del boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddNewDetail()
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas del control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvValidity_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvValidity.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDColVStatus.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                Case 2
                    e.DisplayText = obtenerRecurso(Activa, Eform.BudgetEntities)
                Case 3
                    e.DisplayText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                Case Else

            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas del control de vigencia del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGvValidityPopUp_CustomColumnDisplayText(sender As Object, e As Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvValidityPopUp.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolStatusPopup.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                Case 2
                    e.DisplayText = obtenerRecurso(Activa, Eform.BudgetEntities)
                Case 3
                    e.DisplayText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                Case Else

            End Select
        End If
    End Sub

#End Region

#Region "CustomSummaryCalculate"

    Private ValueTotal As Decimal
    Private Sub viewDetail_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles viewDetail.CustomSummaryCalculate
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

#Region "ContextMenu"

    ''' <summary>
    ''' Menu contextual del menu con click derecho
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

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
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
        _obligationModification.Status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento guardarConfirmar de la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _obligationModification.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _obligationModification.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento actualizarConfirmar de la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _obligationModification.Status = 2
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
            _obligationModification.Status = 3
            varImp = 4
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()

                PopUpAnnulmentReason.BudgetaryValidityId = BudgetaryValidityId

                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    _obligationModification.AnnulmentConceptId = PopUpAnnulmentReason.ReversalReasonId
                    _obligationModification.AnnulmentDescription = PopUpAnnulmentReason.ReversalDescription
                End If
            End Using
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _obligationModification.Id, 0, _obligationModification.Id)
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BudgetSequenceDetail IsNot Nothing Then
            If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

#End Region

End Class
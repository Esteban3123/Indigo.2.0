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
Imports DevExpress.Xpo
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

Public Class FrmObligation
    Implements IObligation

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

    Dim meses() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PObligation

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
    ''' representa la entidad de modificaciones del PAC
    ''' </summary>
    ''' <remarks></remarks>
    Dim _obligation As Obligation

    ''' <summary>
    ''' listado con los detalles para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listObligationDetail As List(Of ObligationDetail)

    ''' <summary>
    ''' listado con los detalles para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteObligationDetail As List(Of Integer)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' 1 = Guardar
    ''' 2 = Actualizar
    ''' 3 = Anular
    ''' 4 = Guardar y Confirmar
    ''' 5 = Actualizar y confirmar
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
    ''' Obtiene el tag del frm
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IObligation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Accede a la propiedad LayoutControls
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IObligation.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece la secuencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As BudgetSequence Implements IObligation.Sequense
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
    ''' Obtiene o establece el id de las entidades presupuestales
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetEntitiesId As Integer? Implements IObligation.BudgetEntityId
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id del pop up de la entidad presupuestal
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetEntityIdPopup As Integer? Implements IObligation.BudgetEntityIdPopup
        Get
            Return INDsleBudgetEntityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntityPopUp.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id d ela vigencia presupuestal
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityId As Integer Implements IObligation.BudgetaryValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id del popup de vigencia
    ''' </summary>
    ''' <returns></returns>
    Public Property ValidityIdPopup As Integer? Implements IObligation.ValidityIdPopup
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
    ''' <returns></returns>
    Public Property Code As String Implements IObligation.Code
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
    ''' Obtiene o estabñece el tipo de obligacion
    ''' </summary>
    ''' <returns></returns>
    Public Property ObligationType As Integer Implements IObligation.ObligationType
        Get
            Return INDGleDocumentType.EditValue
        End Get
        Set(value As Integer)
            INDGleDocumentType.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As Date Implements IObligation.DocumentDate
        Get
            Return INDdteDate.EditValue
        End Get
        Set(value As Date)
            INDdteDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de terceros
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyId As Integer Implements IObligation.ThirdPartyId
        Get
            Return INDSleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDSleThirdParty.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el documento
    ''' </summary>
    ''' <returns></returns>
    Public Property Document As String Implements IObligation.Document
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
    ''' <returns></returns>
    Public Property Observations As String Implements IObligation.Observations
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la propiedad que define si se genera automaticamente la orden de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutomaticPaymentOrder As Boolean Implements IObligation.AutomaticPaymentOrder
        Get
            Return INDGleAutomaticPaymentOrder.EditValue
        End Get
        Set(value As Boolean)
            INDGleAutomaticPaymentOrder.EditValue = value
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
    Public Property BudgetEntitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IObligation.BudgetEntitiesXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesPopUpXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IObligation.BudgetEntitiesPopUpXpo
        Get
            Return INDsleBudgetEntityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBudgetEntityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection Implements IObligation.BudgetaryValidityXpo
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
    Public Property BudgetaryValidityPopUpXpo As DevExpress.Xpo.XPCollection Implements IObligation.BudgetaryValidityPopUpXpo
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Variable que contiene la lista de tipos de documento
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listObligationType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListObligationType As List(Of Tuple(Of Byte, String))
        Get
            If _listObligationType Is Nothing Then
                _listObligationType = New List(Of Tuple(Of Byte, String))
                _listObligationType.Add(New Tuple(Of Byte, String)(1, "Obligación"))
                _listObligationType.Add(New Tuple(Of Byte, String)(2, "Cuenta por Pagar"))
            End If
            Return _listObligationType
        End Get
    End Property

    ''' <summary>
    ''' Estable el datasource de los terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyXPO As XPInstantFeedbackSource Implements IObligation.ListThirdParty
        Get
            Return CType(INDSleThirdParty.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud Base"
    ''' <summary>
    ''' Metodo para buscar
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub
    ''' <summary>
    ''' Metodo para deshacer 
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Undo(False)
    End Sub
    ''' <summary>
    ''' Metodo para revertir cambios
    ''' </summary>
    ''' <param name="withBudgetEntity"></param>
    Public Async Sub Undo(Optional withBudgetEntity As Boolean = True)
        Await CleanControls(withBudgetEntity)
        If withBudgetEntity Then
            BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Else
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
    End Sub
    ''' <summary>
    ''' Metodo Crud para eliminar, sin usarse
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub
    ''' <summary>
    ''' Metodo crud para guardar
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Me._obligation.Status <> 3 Then
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
            Using model As New MObligation(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveObligation(Me._obligation, _listDeleteObligationDetail)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me._obligation = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _obligation.Id, 0, _obligation.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _obligation.Id, 0, _obligation.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _obligation.Id, 0, _obligation.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _obligation.Id, 0, _obligation.Id)
                        Case 5
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _obligation.Id, 0, _obligation.Id)
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
    ''' Metodo que controla la logia del btn actualizar, sin usarse
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
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
    ''' Metodo para crear un nuevo indicador economico
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        NewObligation()
    End Sub
    ''' <summary>
    ''' Metodo para mostrar la busqueda
    ''' </summary>
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
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "ThirdPartyId.NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo With {.Caption = "Documento", .FieldName = "Document", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo With {.Caption = "Valor", .FieldName = "InitialValue", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15), .ColumnFormat = "C0"},
                              New ColumnInfo With {.Caption = "Saldo", .FieldName = "Balance", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15), .ColumnFormat = "C0"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {_validityId}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListObligation
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub
    ''' <summary>
    ''' Funcion asincrona que evalua el contenido del INDbtnConsecutive
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbtnConsecutive.Text = ReturnValue
        If INDbtnConsecutive.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnConsecutive.Enabled = False
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IObligation.ActionsOnControls
        Set(value As Boolean)
            INDlyObligations.BeginUpdate()
            INDbtnConsecutive.Enabled = Not value
            INDGleDocumentType.Enabled = value
            INDdteDate.Enabled = value
            INDSleThirdParty.Enabled = value
            INDtxtDocument.Enabled = value
            INDmemoObservations.Enabled = value
            INDGleAutomaticPaymentOrder.Enabled = value
            If INDSleThirdParty.EditValue Is Nothing Then
                INDBtnAddCommitment.Enabled = False
            End If
            INDGcDetail.Enabled = value
            INDlyObligations.EndUpdate()
            If value Then
                INDGleDocumentType.Focus()
            Else
                INDbtnConsecutive.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function CleanControls(Optional withBudgetaryEntity As Boolean = True) As Task
        INDlyObligations.BeginUpdate()
        Await DeleteBlockedRecord()

        Code = String.Empty
        INDGleDocumentType.EditValue = 1
        INDGleDocumentType.Properties.ReadOnly = False
        INDdteDate.EditValue = Nothing
        INDdteDate.Properties.ReadOnly = False
        INDSleThirdParty.EditValue = Nothing
        INDSleThirdParty.Properties.ReadOnly = False
        INDSleThirdParty.Properties.NullText = String.Empty
        INDtxtDocument.EditValue = String.Empty
        Observations = String.Empty
        AutomaticPaymentOrder = False
        INDGcDetail.DataSource = Nothing

        _doc = Nothing
        _obligation = Nothing
        _listObligationDetail = Nothing
        _listDeleteObligationDetail = Nothing

        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.ControlHideStatus = False
        INDsleBudgetEntityPopUp.Properties.ReadOnly = False
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
            INDsleBudgetEntityPopUp.EditValue = Nothing
            INDsleValidityPopUp.EditValue = Nothing

            LoadLayoutGroup(False)
        End If

        INDlyObligations.EndUpdate()
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
        INDsleBudgetEntityPopUp.Properties.ReadOnly = True
        INDsleValidityPopUp.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup(ByVal Bandera As Boolean)
        If Bandera Then
            INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgCommitment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If BudgetEntitiesPopUpXpo Is Nothing Then
                _presenter.InitializeBudgetEntityPopUp()
            End If
            INDsleBudgetEntityPopUp.EditValue = BudgetEntitiesId
            INDsleValidityPopUp.EditValue = BudgetaryValidityId

            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.ControlHideStatus = False
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Else
            INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgCommitment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
    ''' Metodo para agrega run nuevo detalle
    ''' </summary>
    Private Sub AddNewDetail()
        If ObligationType = 1 Then 'Obligacion
            Dim frmDetail As New FrmPopupAddCommitment()
            frmDetail.Size = New System.Drawing.Size(800, 730)
            frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            frmDetail.ValidatyId = _validityId
            frmDetail.ThirdPartyId = ThirdPartyId
            frmDetail.DocumentDate = DocumentDate
            Dim frmTransparent As New FrmTransparent(frmDetail, False)
            If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                If _listObligationDetail Is Nothing Then
                    _listObligationDetail = frmDetail.ListObligationDetail
                Else
                    For Each item In frmDetail.ListObligationDetail
                        Dim detailAdded As ObligationDetail = Nothing
                        If item.CommitmentDetailId IsNot Nothing Then
                            detailAdded = _listObligationDetail.Find(Function(x) x.CommitmentDetailId = item.CommitmentDetailId)
                        Else
                            detailAdded = _listObligationDetail.Find(Function(x) x.CategoryId = item.CategoryId And x.RevenueTypeId = item.RevenueTypeId)
                        End If

                        If detailAdded IsNot Nothing Then
                            Continue For
                        End If
                        _listObligationDetail.Add(item)
                    Next

                End If

                INDGcDetail.DataSource = _listObligationDetail
                INDGcDetail.RefreshDataSource()
                INDGvDetail.ExpandAllGroups()

                INDGleDocumentType.Properties.ReadOnly = True
                INDSleThirdParty.Properties.ReadOnly = True
                INDdteDate.Properties.ReadOnly = True
            End If
        Else 'cuenta por pagar
            Dim frmDetail As New FrmPopupAddBudget()
            frmDetail.Size = New System.Drawing.Size(800, 730)
            frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            frmDetail.ValidatyId = _validityId
            frmDetail.ValidityYear = _validityYear
            Dim frmTransparent As New FrmTransparent(frmDetail, False)
            If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                If _listObligationDetail Is Nothing Then
                    _listObligationDetail = frmDetail.ListObligationDetail
                Else
                    For Each item In frmDetail.ListObligationDetail
                        Dim detailAdded = _listObligationDetail.Find(Function(x) x.CategoryId = item.CategoryId And x.RevenueTypeId = item.RevenueTypeId)
                        If detailAdded IsNot Nothing Then
                            Continue For
                        End If
                        _listObligationDetail.Add(item)
                    Next

                End If
                INDGcDetail.DataSource = _listObligationDetail
                INDGcDetail.RefreshDataSource()

                INDGleDocumentType.Properties.ReadOnly = True
                INDSleThirdParty.Properties.ReadOnly = True
                INDdteDate.Properties.ReadOnly = True
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

        Using model As New MObligation(MyTag)
            AsyncLoader(True)
            INDlyObligations.BeginUpdate()
            Me._obligation = Await model.GetObligationByCode(Code, _validityId)
            If _obligation IsNot Nothing AndAlso _obligation.Id > 0 Then
                Dim result = Await model.GetBlockRecord(Me.Tag, _obligation.Id)
                INDsleBudgetEntityPopUp.Properties.ReadOnly = True
                INDsleValidityPopUp.Properties.ReadOnly = True
                With _obligation
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
                    ObligationType = .ObligationType
                    INDGleDocumentType.Properties.ReadOnly = True
                    DocumentDate = .DocumentDate
                    INDdteDate.Properties.ReadOnly = True
                    ThirdPartyId = .ThirdPartyId
                    INDSleThirdParty.Properties.NullText = .NitNameThirdParty
                    INDSleThirdParty.Properties.ReadOnly = True
                    Document = .Document
                    Observations = .Observations
                    AutomaticPaymentOrder = .AutomaticPaymentOrder

                    _listObligationDetail = model.ListObligationDetailByObligationId(_obligation.Id)
                    INDGcDetail.DataSource = Nothing
                    INDGcDetail.DataSource = _listObligationDetail
                    INDGvDetail.ExpandAllGroups()

                    Me.BarraBotones.StatusRecord = .Status.ToString
                    Me.BarraBotones.ControlHideStatus = True
                End With
                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._obligation.Code)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _obligation.Id}
                    Dim operation = Await model.SaveBlockRecord(_blockRecord)
                    _blockRecord = operation.ObjectEmbbeded
                Else
                    _blockRecord = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                If _obligation.Status = 1 Then 'Registrado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Else 'Confirmado o Anulado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.SetDocuments(_obligation.Id)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, _obligation.Id, 0, _obligation.Id)

                AsyncLoader(False)
                ActionsOnControls = True
            Else
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewObligation()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Deshacer()
                    INDbtnConsecutive.Focus()
                End If
            End If
        End Using
        INDlyObligations.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewObligation()
        If INDsleBudgetEntity.EditValue IsNot Nothing AndAlso CType(INDsleBudgetEntity.EditValue, String) <> "" Then
            Me._obligation = New Obligation With {.Status = 1}
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
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._obligation.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._obligation.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._obligation.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._obligation.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._obligation.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Function DeleteBlockedRecord() As Task
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
        If Me._obligation IsNot Nothing AndAlso Me._obligation.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Await DeleteBlockedRecord()
                Me.INDbtnConsecutive.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnConsecutive.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        If Me._obligation.Status > 1 Then
            If Me._obligation.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta confirmado"
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta anulado"
            End If
            Exit Sub
        End If

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim detail = DirectCast(INDGvDetail.GetFocusedRow(), ObligationDetail)
        If detail.Id > 0 Then
            If _listDeleteObligationDetail Is Nothing Then
                _listDeleteObligationDetail = New List(Of Integer)
            End If
            detail.MarkAsDeleted()
            _listDeleteObligationDetail.Add(detail.Id)
        End If
        _listObligationDetail.Remove(detail)
        INDGcDetail.DataSource = Nothing
        INDGcDetail.DataSource = _listObligationDetail
        INDGcDetail.RefreshDataSource()

        If _listObligationDetail.Count = 0 Then
            INDGleDocumentType.Properties.ReadOnly = False
            INDSleThirdParty.Properties.ReadOnly = False
            INDdteDate.Properties.ReadOnly = False
        End If
    End Sub
    ''' <summary>
    ''' Funcion que valida el detalles
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateDetail() As String
        Dim listErrors As New StringBuilder
        If INDGvDetail.RowCount = 0 Then
            listErrors.AppendLine("Se debe agregar minimo un detalle para la modificación")
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

        If INDGvDetail.RowCount > 0 Then
            Dim listNotValue = _listObligationDetail.FindAll(Function(x) x.InitialValue = 0)
            If listNotValue.Count > 0 Then
                listErrors.AppendLine("Se encontraron items sin valor")
            End If
        End If

        Return listErrors.ToString()
    End Function
    ''' <summary>
    ''' Asigna los valores de los controles
    ''' </summary>
    Private Sub AssigningValues()
        With _obligation
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = _idOperativeUnit
            .BudgetaryValidityId = _validityId
            .Code = Code
            .ObligationType = ObligationType
            .DocumentDate = DocumentDate
            .ThirdPartyId = ThirdPartyId
            .Document = Document
            .Observations = Observations
            .AutomaticPaymentOrder = AutomaticPaymentOrder
            .ObligationDetail.Clear()
            If _listObligationDetail IsNot Nothing AndAlso _listObligationDetail.Count > 0 Then
                _listObligationDetail.FindAll(Function(item) item.InitialValue > 0 AndAlso (item.ChangeTracker.State = ObjectState.Added OrElse item.ChangeTracker.State = ObjectState.Modified)).ForEach(Sub(item)
                                                                                                                                                                                                              .ObligationDetail.Add(item)
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
    ''' Carga el frm de obligaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmObligation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyObligations, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PObligation(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDGcDetail)

        'Cargar las acciones a la rejilla
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvDetail.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        LoadStatus()
        Me.Undo()
    End Sub
    ''' <summary>
    ''' Libera la memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing

        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _blockRecord = Nothing
        _obligation = Nothing
        _listObligationDetail = Nothing
        _listDeleteObligationDetail = Nothing
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
    ''' Muestra el frm de obligaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmObligation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDGleDocumentType.Properties.DataSource = ListObligationType
        INDsleBudgetEntity.Focus()
    End Sub

#End Region

#Region "FormClosing"

    Private Async Sub FrmObligation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"
    ''' <summary>
    ''' evento al dar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnConsecutive.KeyDown
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
                If Not String.IsNullOrEmpty(INDbtnConsecutive.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnConsecutive.Text.Trim()) Then
                    Me.NewObligation()
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
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            _presenter.InitializeBudgetEntity()
        End If
    End Sub
    ''' <summary>
    ''' Query Popup que muestra los tercerceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ThirdPartyXPO Is Nothing Then
            Using model As New MCollection(Me.Tag)
                ThirdPartyXPO = model.ListThirdParty()
            End Using
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAddCommitment_Click(sender As Object, e As EventArgs) Handles INDBtnAddCommitment.Click
        AddNewDetail()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", INDsleBudgetEntity.EditValue, True)
        End If
    End Sub

    Private Sub INDsleBudgetEntityPopUp_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntityPopUp.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", INDsleBudgetEntityPopUp.EditValue, True)
        End If
    End Sub

    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("532", INDSleThirdParty.EditValue, True)
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento al cambiar el valor de INDsleEntity
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntitiesId IsNot Nothing AndAlso BudgetEntitiesId <> 0 Then
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
    ''' Evento que se dispara al editar el control INDsleBudgetEntityPopUp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntityPopUp.EditValueChanged
        If INDsleBudgetEntityPopUp.EditValue IsNot Nothing AndAlso _controlerChanged = False Then
            _budgetEntityId = BudgetEntityIdPopup
            _budgetEntityCodeName = INDsleBudgetEntityPopUp.Text

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
    ''' Evento que se dispara al editar el control INDsleValidity
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If BudgetaryValidityId <> Nothing AndAlso BudgetaryValidityId <> 0 Then
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
    ''' Evento que se dispara al editar el control INDsleValidityPopUp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <summary>
    ''' Evento que se dispara al editar el control INDSleThirdParty
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleThirdParty.EditValueChanged
        If INDSleThirdParty.EditValue IsNot Nothing Then
            If _obligation IsNot Nothing AndAlso _obligation.Status < 2 Then
                INDBtnAddCommitment.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar al tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleDocumentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDocumentType.EditValueChanged
        If ObligationType = 1 Then 'obligacion
            INDColCode.Visible = True
            INDColDocument.Visible = True
            INDColCommitmentType.Visible = True
            INDColCommitmentType.GroupIndex = 0
        Else
            INDColCode.Visible = False
            INDColDocument.Visible = False
            INDColCommitmentType.Visible = False
            INDColCommitmentType.GroupIndex = -1
        End If
    End Sub

#End Region

#Region "EditValueChanging"
    ''' <summary>
    ''' Evento que se dispara si la fecha del documento es menor al del nuevo valor ingresado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRpDteExpiredDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRpDteExpiredDate.EditValueChanging
        If e.NewValue < DocumentDate Then
            Mensaje(EeventViewerImages.Advertencia) = "La fecha de vencimiento no puede ser menor a la fecha del documento"
            e.Cancel = True
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al al cambiar el valor de INDRpTxtValue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRpTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRpTxtValue.EditValueChanging
        If e.NewValue = String.Empty Then
            Exit Sub
        End If
        Dim detail = DirectCast(INDGvDetail.GetFocusedRow, ObligationDetail)
        If detail.BalanceCommitment < e.NewValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "MenuContext"
    ''' <summary>
    ''' Evento que se elimina los detalles al dar click en IndigoGridView1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        DeleteDetail()
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnConsecutive.ButtonClick
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
        Me._obligation.Status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._obligation.Status = 2
            varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me._obligation.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._obligation.Status = 2
            varImp = 5
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._obligation.Status = 3
            varImp = 3
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()

                PopUpAnnulmentReason.BudgetaryValidityId = BudgetaryValidityId

                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    _obligation.AnnulmentConceptId = PopUpAnnulmentReason.ReversalReasonId
                    _obligation.AnnulmentDescription = PopUpAnnulmentReason.ReversalDescription
                End If
            End Using
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _obligation.Id, 0, _obligation.Id)
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
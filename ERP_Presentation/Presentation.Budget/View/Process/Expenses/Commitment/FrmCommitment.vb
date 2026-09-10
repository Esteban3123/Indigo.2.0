'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 02/09/2015
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
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

Public Class FrmCommitment
    Implements ICommitment

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

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PCommitment

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
    ''' representa la entidad de compromiso
    ''' </summary>
    ''' <remarks></remarks>
    Dim _commitment As Commitment

    ''' <summary>
    ''' Objeto que contiene el tercero
    ''' </summary>
    Dim _thirdParty As ThirdParty

    ''' <summary>
    ''' listado con los detalles para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCommitmentDetail As List(Of CommitmentDetail)

    ''' <summary>
    ''' listado con los detalles para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteCommitmentDetail As List(Of Integer)

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
    ''' Obtiene el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ICommitment.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICommitment.MyLayoutControl
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
    Public Property Sequense As BudgetSequence Implements ICommitment.Sequense
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
    Public Property BudgetEntitiesId As Integer Implements ICommitment.BudgetEntitiesId
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
    Public Property BudgetEntityIdPopup As Integer? Implements ICommitment.BudgetEntityIdPopup
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
    Public Property BudgetaryValidityId As Integer Implements ICommitment.BudgetaryValidityId
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
    Public Property ValidityIdPopup As Integer? Implements ICommitment.ValidityIdPopup
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
    Public Property Code As String Implements ICommitment.Code
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
    ''' Tipo de Compromiso
    ''' </summary>
    ''' <value>Compromiso = 1, Reserva = 2, Vigencia Futura = 3</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CommitmentType As Byte Implements ICommitment.CommitmentType
        Get
            Return INDsleCommitmentType.EditValue
        End Get
        Set(value As Byte)
            INDsleCommitmentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements ICommitment.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer Implements ICommitment.ThirdPartyId
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Origen del Documento 
    ''' </summary>
    ''' <value>Otro = 1, Orden de Trabajo = 2, Contrato = 3</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentSource As Byte Implements ICommitment.DocumentSource
        Get
            Return INDsleDocumentSource.EditValue
        End Get
        Set(value As Byte)
            INDsleDocumentSource.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' numero del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Document As String Implements ICommitment.Document
        Get
            Return INDtxtDocument.EditValue
        End Get
        Set(value As String)
            INDtxtDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la observacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements ICommitment.Observations
        Get
            Return INDmeObservations.EditValue
        End Get
        Set(value As String)
            INDmeObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la propiedad que define si se genera automaticamente la obligación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutomaticObligation As Boolean Implements ICommitment.AutomaticObligation
        Get
            Return INDGleAutomaticObligation.EditValue
        End Get
        Set(value As Boolean)
            INDGleAutomaticObligation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la propiedad que define si se genera automaticamente la orden de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutomaticPaymentOrder As Boolean Implements ICommitment.AutomaticPaymentOrder
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
    Public Property BudgetEntitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICommitment.BudgetEntitiesXpo
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
    Public Property BudgetEntitiesPopUpXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICommitment.BudgetEntitiesPopUpXpo
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
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection Implements ICommitment.BudgetaryValidityXpo
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
    Public Property BudgetaryValidityPopUpXpo As DevExpress.Xpo.XPCollection Implements ICommitment.BudgetaryValidityPopUpXpo
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
    Private _ListCommitmentType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property ListCommitmentType As List(Of Tuple(Of Integer, String))
        Get
            If _ListCommitmentType Is Nothing Then
                _ListCommitmentType = New List(Of Tuple(Of Integer, String))
                _ListCommitmentType.Add(New Tuple(Of Integer, String)(1, "Compromiso"))
                _ListCommitmentType.Add(New Tuple(Of Integer, String)(2, "Reserva"))
            End If
            Return _ListCommitmentType
        End Get
    End Property

    ''' <summary>
    ''' Estable el datasource de los terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListThirdParty As XPInstantFeedbackSource Implements ICommitment.ListThirdParty
        Get
            Return INDsleThirdParty.Datasource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdParty.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene la lista de origen de documento
    ''' </summary>
    ''' <remarks></remarks>
    Private _ListDocumentSource As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListDocumentSource As List(Of Tuple(Of Byte, String))
        Get
            If _ListDocumentSource Is Nothing Then
                _ListDocumentSource = New List(Of Tuple(Of Byte, String))
                _ListDocumentSource.Add(New Tuple(Of Byte, String)(1, "Otro"))
                _ListDocumentSource.Add(New Tuple(Of Byte, String)(2, "Orden de Trabajo"))
                _ListDocumentSource.Add(New Tuple(Of Byte, String)(3, "Contrato"))
            End If
            Return _ListDocumentSource
        End Get
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
    ''' Metodo eliminar sin uso 
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    '''  METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If _commitment.Status <> 3 Then
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
            AssignsValues()
            Using model As New MCommitment(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveCommitment(_commitment, _listDeleteCommitmentDetail)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Me._commitment = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _commitment.Id, 0, _commitment.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _commitment.Id, 0, _commitment.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _commitment.Id, 0, _commitment.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _commitment.Id, 0, _commitment.Id)
                        Case 5
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _commitment.Id, 0, _commitment.Id)
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

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
    ''' <summary>
    ''' Prpiedad para mostrar el mensaje en el visor
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Me.Deshacer()
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
                              New ColumnInfo With {.Caption = "Tipo Documento", .FieldName = "CommitmentTypeName"},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate"},
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "ThirdPartyId.NitName"},
                              New ColumnInfo With {.Caption = "Origen Documento", .FieldName = "DocumentSourceName"},
                              New ColumnInfo With {.Caption = "Documento", .FieldName = "Document"},
                              New ColumnInfo With {.Caption = "Valor Inicial", .FieldName = "InitialValue", .ColumnFormat = "C0"},
                              New ColumnInfo With {.Caption = "Valor Total", .FieldName = "TotalCommitment", .ColumnFormat = "C0"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName"}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {_validityId}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCommitmentByValidityId
            'BarraBotones.PrepareToolbar(eAction.OnlyNew)
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICommitment.ActionsOnControls
        Set(value As Boolean)
            INDlcCommitment.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleCommitmentType.Enabled = value
            INDdeDocumentDate.Enabled = value
            INDsleThirdParty.Enabled = value
            INDsleDocumentSource.Enabled = value
            INDtxtDocument.Enabled = value
            INDmeObservations.Enabled = value
            INDGleAutomaticObligation.Enabled = value
            INDGleAutomaticPaymentOrder.Enabled = value
            INDsbAddCategory.Enabled = value
            INDgcDetailAvailability.Enabled = value
            INDGcCategory.Enabled = value
            INDlcCommitment.EndUpdate()
            If value = True Then
                INDsleCommitmentType.Focus()
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
        INDlcCommitment.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbtnCode.Text = String.Empty
        INDsleCommitmentType.EditValue = Nothing
        INDdeDocumentDate.EditValue = Me.GetDateServer()
        INDsleThirdParty.EditValue = Nothing
        INDsleThirdParty.DisplayNullText = String.Empty
        INDsleDocumentSource.EditValue = Nothing
        INDtxtDocument.EditValue = String.Empty
        INDmeObservations.Text = String.Empty
        AutomaticObligation = False
        AutomaticPaymentOrder = False
        INDgcDetailAvailability.DataSource = Nothing
        INDGcCategory.DataSource = Nothing

        _doc = Nothing
        _commitment = Nothing
        _listCommitmentDetail = Nothing
        _listDeleteCommitmentDetail = Nothing

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

        INDlcCommitment.EndUpdate()
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
    End Sub

    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup(ByVal Bandera As Boolean)
        If Bandera Then
            INDlcgCommitment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
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
            INDlcgCommitment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
                _validityStatusText = item.StatusText
            End If
        End If
    End Sub

    ''' <summary>
    ''' Buscar tercero por nit
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <returns></returns>
    Private Async Function SearchThirdParty(ByVal nit As String) As Task(Of Domain.Entities.ThirdParty)
        Using ModelThird As New MThirdParty(MThirdParty.TAG)
            _thirdParty = Await ModelThird.GetThirdPartyAsync(nit)
            If _thirdParty IsNot Nothing AndAlso _thirdParty.Id > 0 Then
                INDsleDocumentSource.Focus()
            Else
                Me.INDsleThirdParty.DisplayNullText = String.Empty
                Me.INDsleThirdParty.EditValue = Nothing
                Me.INDsleThirdParty.DisplayMember = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "El Tercero No Existe"
                INDsleThirdParty.Focus()
            End If
            Return _thirdParty
        End Using
    End Function

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
    ''' Metodo Que Abre el pop up para agregar nuevos rubros
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddNewDetail()
        If CommitmentType = 1 Then
            Dim frmDetail As New FrmPopUpCommitment()
            frmDetail.Size = New System.Drawing.Size(800, 730)
            frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            frmDetail.ValidatyId = _validityId
            frmDetail.Year = _validityYear
            Dim frmTransparent As New FrmTransparent(frmDetail, False)
            If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                If Not (_listCommitmentDetail IsNot Nothing AndAlso _listCommitmentDetail.Count > 0) Then
                    _listCommitmentDetail = New List(Of CommitmentDetail)
                Else
                    Dim listErrors As New StringBuilder
                    For Each item As Domain.Entities.CommitmentDetail In frmDetail.ListCommitmentDetail
                        Dim cont = (From l In _listCommitmentDetail Where l.CategoryId = item.CategoryId And l.RevenueTypeId = item.RevenueTypeId And l.AvailabilityDetailId = item.AvailabilityDetailId).Count
                        If cont > 0 Then
                            listErrors.AppendLine("No se puede agregar el rubro " + item.CodeNameCategory + " porque ya existe en la lista del formulario principal.")
                        End If
                    Next
                    If listErrors.ToString.Length > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                        Exit Sub
                    End If
                End If
                _listCommitmentDetail.AddRange(frmDetail.ListCommitmentDetail)
                INDgcDetailAvailability.DataSource = _listCommitmentDetail
                INDgcDetailAvailability.RefreshDataSource()
            End If
        Else
            Dim frmDetail As New FrmPopUpCommitmentCategory()
            frmDetail.Size = New System.Drawing.Size(800, 730)
            frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            frmDetail.ValidatyId = _validityId
            frmDetail.Year = _validityYear
            Dim frmTransparent As New FrmTransparent(frmDetail, False)
            If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                If Not (_listCommitmentDetail IsNot Nothing AndAlso _listCommitmentDetail.Count > 0) Then
                    _listCommitmentDetail = New List(Of CommitmentDetail)
                Else
                    Dim listErrors As New StringBuilder
                    For Each item As Domain.Entities.CommitmentDetail In frmDetail.ListCommitmentDetail
                        Dim cont = (From l In _listCommitmentDetail Where l.CategoryId = item.CategoryId And l.RevenueTypeId = item.RevenueTypeId).Count
                        If cont > 0 Then
                            listErrors.AppendLine("No se puede agregar el rubro " + item.CodeNameCategory + " porque ya existe en la lista del formulario principal.")
                        End If
                    Next
                    If listErrors.ToString.Length > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                        Exit Sub
                    End If
                End If
                _listCommitmentDetail.AddRange(frmDetail.ListCommitmentDetail)
                INDGcCategory.DataSource = _listCommitmentDetail
                INDGcCategory.RefreshDataSource()
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

        Using Model As New MCommitment(MyTag)
            AsyncLoader(True)
            INDlcCommitment.BeginUpdate()
            _commitment = Await Model.GetCommitmentByCode(Me.INDbtnCode.Text.Trim, _validityId)
            If Not _commitment Is Nothing AndAlso _commitment.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, _commitment.Id)
                INDsleEntityPopUp.Properties.ReadOnly = True
                INDsleValidityPopUp.Properties.ReadOnly = True
                With _commitment
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
                    CommitmentType = .CommitmentType
                    DocumentDate = .DocumentDate
                    ThirdPartyId = .ThirdPartyId
                    INDsleThirdParty.DisplayNullText = .NameThirdParty
                    DocumentSource = .DocumentSource
                    Document = .Document
                    Observations = .Observations
                    AutomaticObligation = .AutomaticObligation
                    AutomaticPaymentOrder = .AutomaticPaymentOrder
                    _listCommitmentDetail = .CommitmentDetail.ToList()

                    If .CommitmentType = 1 Then
                        INDgcDetailAvailability.DataSource = Nothing
                        INDgcDetailAvailability.DataSource = _listCommitmentDetail
                    Else
                        INDGcCategory.DataSource = Nothing
                        INDGcCategory.DataSource = _listCommitmentDetail
                    End If

                    Me.BarraBotones.StatusRecord = .Status.ToString()
                    Me.BarraBotones.ControlHideStatus = True
                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me._commitment.Code)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _commitment.Id}
                    Dim operation = Await Model.SaveBlockRecord(_blockRecord)
                    _blockRecord = operation.ObjectEmbbeded
                Else
                    _blockRecord = result
                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                If _commitment.Status = 1 Then 'Registrado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Else 'Confirmado o Anulado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.SetDocuments(_commitment.Id)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, _commitment.Id, 0, _commitment.Id)

                AsyncLoader(False)
                ActionsOnControls = True
            Else
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewEntity()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Me.Deshacer()
                    INDbtnCode.Focus()
                End If
            End If
        End Using
        INDlcCommitment.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewEntity()
        If INDsleEntity.EditValue IsNot Nothing AndAlso CType(INDsleEntity.EditValue, String) <> "" Then
            _commitment = New Commitment() With {.Status = 1}
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), _commitment.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & _commitment.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), _commitment.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), _commitment.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), _commitment.Code)
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
        If Me._commitment IsNot Nothing AndAlso Me._commitment.Id > 0 Then
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
    ''' Metodo que valida la fecha de vencimiento de los detalles con la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim commitmentDetail As Domain.Entities.CommitmentDetail = CType(INDgvDetail.GetFocusedRow, Domain.Entities.CommitmentDetail)
            If commitmentDetail IsNot Nothing Then
                Dim control As DevExpress.XtraEditors.BaseEdit = sender
                If e.NewValue < DocumentDate Then
                    Mensaje(EeventViewerImages.Advertencia) = "La fecha de Vencimiento no puede ser menor a la fecha del documento."
                    commitmentDetail.ExpiredDate = e.OldValue
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida la naturaleza con respecto al saldo y el valor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateValue(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim commitmentDetail As Domain.Entities.CommitmentDetail = CType(INDgvDetail.GetFocusedRow, Domain.Entities.CommitmentDetail)
            If commitmentDetail IsNot Nothing Then
                Dim control As DevExpress.XtraEditors.BaseEdit = sender
                If e.NewValue > commitmentDetail.BalanceAffects Then
                    Mensaje(EeventViewerImages.Advertencia) = "El Valor del compromiso no puede ser mayor al saldo de la disponibilidad."
                    commitmentDetail.InitialValue = e.OldValue
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail(FlagCommitment As Boolean)
        If _commitment.Status > 1 Then
            If _commitment.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta confirmado"
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta anulado"
            End If
            Exit Sub
        End If

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim detail As CommitmentDetail
        If FlagCommitment Then
            detail = INDgvDetail.GetFocusedRow
        Else
            detail = viewBudget.GetFocusedRow
        End If
        If detail.Id > 0 Then
            If _listDeleteCommitmentDetail Is Nothing Then
                _listDeleteCommitmentDetail = New List(Of Integer)
            End If
            detail.MarkAsDeleted()
            _listDeleteCommitmentDetail.Add(detail.Id)
        End If
        _listCommitmentDetail.Remove(detail)
        If CommitmentType = 1 Then
            INDgcDetailAvailability.DataSource = Nothing
            INDgcDetailAvailability.DataSource = _listCommitmentDetail
        Else
            INDGcCategory.DataSource = Nothing
            INDGcCategory.DataSource = _listCommitmentDetail
        End If
    End Sub

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As String
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de compromiso en la rejilla
        If Not (_listCommitmentDetail IsNot Nothing AndAlso _listCommitmentDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles del compromiso para poder guardar.")
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

        If _listCommitmentDetail IsNot Nothing AndAlso _listCommitmentDetail.Count > 0 Then
            For Each item In _listCommitmentDetail
                If item.InitialValue = 0 Then
                    listErrors.AppendLine("el valor inicial del rubro " + item.CodeNameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " no puede ser 0.")
                End If
                If CommitmentType = 1 Then
                    If item.InitialValue > item.BalanceAffects Then
                        listErrors.AppendLine("el valor inicial del rubro " + item.CodeNameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " no puede ser mayor al saldo de la disponibilidad.")
                    End If
                    If item.DateAvailability.AsDate > DocumentDate.AsDate Then
                        listErrors.AppendLine("No se puede agregar los rubros de la disponibilidad " + item.CodeAvailability + " porque la fecha de la disponibilidad es superior a la fecha del compromiso.")
                    End If
                    If item.DateExpirationAvailability.AsDate < DocumentDate.AsDate Then
                        listErrors.AppendLine("No se puede agregar los rubros de la disponibilidad " + item.CodeAvailability + " porque la fecha de Vencimiento de la disponibilidad es menor a la fecha del compromiso.")
                    End If
                End If
                If item.ExpiredDate.AsDate < DocumentDate.AsDate Then
                    listErrors.AppendLine("la fecha de vencimiento del rubro " + item.CodeNameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " debe ser superior a la fecha del documento.")
                End If
            Next
        End If

        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' asigna valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssignsValues()
        With _commitment
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .BudgetaryValidityId = _validityId
            .Code = Code
            .CommitmentType = CommitmentType
            .DocumentDate = DocumentDate
            .ThirdPartyId = ThirdPartyId
            .DocumentSource = DocumentSource
            .Document = Document
            .Observations = Observations
            .AutomaticObligation = AutomaticObligation
            .AutomaticPaymentOrder = AutomaticPaymentOrder
            .CommitmentDetail.Clear()
            If _listCommitmentDetail IsNot Nothing AndAlso _listCommitmentDetail.Count > 0 Then
                _listCommitmentDetail.FindAll(Function(item) item.InitialValue > 0 AndAlso (item.ChangeTracker.State = ObjectState.Added OrElse item.ChangeTracker.State = ObjectState.Modified)).ForEach(Sub(item)
                                                                                                                                                                                                              .CommitmentDetail.Add(item)
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
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCommitment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcCommitment, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PCommitment(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me.INDsleThirdParty.FuncQueryOnKeyEnterPressed = AddressOf Me.SearchThirdParty
        Me.INDsleThirdParty.View.OptionsView.ShowGroupPanel = False
        IndigoGridControl1.RefreshGrid(INDgcDetailAvailability)
        IndigoGridControl1.RefreshGrid(INDGcCategory)

        'Cargar las acciones a la rejilla
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDetail.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        IndigoGridView2.SetListAcction(viewBudget, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewBudget.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        LoadStatus()
        Me.Undo()
    End Sub
    ''' <summary>
    ''' Libera memoria al cerrar el frm
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
        _commitment = Nothing
        _thirdParty = Nothing
        _listCommitmentDetail = Nothing
        _listDeleteCommitmentDetail = Nothing
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
    ''' Evento que se dispara al pintar por primera vez el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCommitment_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDsleCommitmentType.Properties.DataSource = ListCommitmentType
        Me.INDsleDocumentSource.Properties.DataSource = ListDocumentSource
        INDsleEntity.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Async Sub FrmCommitment_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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

    ''' <summary>
    ''' carga el datasource de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If ListThirdParty Is Nothing Then
            _presenter.LoadListThirdParty()
        End If
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' evento click en aderir categoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsbAddCategory_Click(sender As Object, e As EventArgs) Handles INDsbAddCategory.Click
        AddNewDetail()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' evento button click que abre el formulario de registro de entidad
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
    ''' Abre el frm e terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDsleThirdParty.OpenFormButtonClick
        OpenForm(532, Nothing, True)
        _presenter.LoadListThirdParty()
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

    ''' <summary>
    ''' si la entidad y la vigencia estan seleccionados en el pop up actualiza el control de usuario
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
    ''' Oculta el grupo de entidad y vigencia y muestra los grupos de registro de modificacion de reconocimiento
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
    ''' si la entidad y la vigencia estan seleccionados en el pop up actualiza el control de usuario
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

            SetStatusPopup()
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar al tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCommitmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCommitmentType.EditValueChanged
        If CommitmentType = 1 Then
            INDlciGcAvailability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciGcBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDlciGcAvailability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciGcBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar la opción que indica si se generará automaticamente la obligación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleAutomaticObligation_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAutomaticObligation.EditValueChanged
        AutomaticPaymentOrder = False
        INDLciAutomaticPaymentOrder.Visibility = If(AutomaticObligation, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la rejilla de vencimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtDate.EditValueChanging
        ValidateDate(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la rejilla de valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        ValidateValue(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la rejilla de vencimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtDateCategory_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtDateCategory.EditValueChanging
        ValidateDate(sender, e)
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
                DeleteDetail(True)
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar con click derecho el menu sobre la rejilla de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs)
        Select Case (sender.Tag.ToString)
            Case "Remove"
                DeleteDetail(False)
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
    ''' 
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _commitment.Status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _commitment.Status = 2
            varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _commitment.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _commitment.Status = 2
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
            _commitment.Status = 3
            varImp = 3
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()

                PopUpAnnulmentReason.BudgetaryValidityId = BudgetaryValidityId

                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    _commitment.AnnulmentConceptId = PopUpAnnulmentReason.ReversalReasonId
                    _commitment.AnnulmentDescription = PopUpAnnulmentReason.ReversalDescription
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
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _commitment.Id, 0, _commitment.Id)
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
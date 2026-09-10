'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 18-03-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Treasury.MVP
Imports Presentation.Base
Imports Presentation.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Accounting
Imports System.Text
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.XtraGrid.Columns

#End Region

''' <summary>
''' Formulario de Tarjetas
''' </summary>
Public Class FrmCards
    Implements ICards, ICustomizableForm


    Public Sub New()
        InitializeComponent()
        mCards = New MCards(Me.Tag)
    End Sub

#Region "Properties"

    Private mCards As MCards
    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICards.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICards.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequense As TreasurySequence Implements ICards.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property CashReceiptConceptIcaXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements ICards.CashReceiptConceptIcaXPO
        Get
            Return CType(INDSleCashReceiptConceptICA.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDSleCashReceiptConceptICA.Properties.DataSource = value
        End Set
    End Property

    Public Property CashReceiptConceptRtfXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements ICards.CashReceiptConceptRtfXPO
        Get
            Return CType(INDSleCashReceiptConceptRFT.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDSleCashReceiptConceptRFT.Properties.DataSource = value
        End Set
    End Property

    Public Property Code As String Implements ICards.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property


    Public Property IdCashReceiptConceptICA As Integer Implements ICards.IdCashReceiptConceptICA
        Get
            Return INDSleCashReceiptConceptICA.EditValue
        End Get
        Set(value As Integer)
            INDSleCashReceiptConceptICA.EditValue = value
        End Set
    End Property

    Public Property IdCashReceiptConceptRTF As Integer Implements ICards.IdCashReceiptConceptRTF
        Get
            Return INDSleCashReceiptConceptRFT.EditValue
        End Get
        Set(value As Integer)
            INDSleCashReceiptConceptRFT.EditValue = value
        End Set
    End Property

    Public Property IdThirdParty As Integer Implements ICards.IdThirdParty
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    Public Property NameCard As String Implements ICards.NameCard
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    Public Property IdRetentionConceptICA As Integer Implements ICards.IdRetentionConceptICA
        Get
            Return INDSLeRetentionConceptICA.EditValue
        End Get
        Set(value As Integer)
            INDSLeRetentionConceptICA.EditValue = value
        End Set
    End Property

    Public Property IdRetentionConceptRTF As Integer Implements ICards.IdRetentionConceptRTF
        Get
            Return INDSLeRetentionConceptRTF.EditValue
        End Get
        Set(value As Integer)
            INDSLeRetentionConceptRTF.EditValue = value
        End Set
    End Property

    Public Property GetCostCenter As Byte
        Get
            Return INDGleGetCostCenter.EditValue
        End Get
        Set(value As Byte)
            INDGleGetCostCenter.EditValue = value
        End Set
    End Property

    Public Property ListOperatingUnitCostCenter As List(Of CardCostCenter)
        Get
            Return CType(INDGcOperatingUnitCostCenter.DataSource, List(Of CardCostCenter))
        End Get
        Set(value As List(Of CardCostCenter))
            INDGcOperatingUnitCostCenter.DataSource = value
            INDGcOperatingUnitCostCenter.RefreshDataSource()
        End Set
    End Property

    Public Property Status As Boolean Implements ICards.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    Public Property ThirdPartyXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements ICards.ThirdPartyXPO
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    Public Property CashReceiptConceptCommisionXPO As XPCollection(Of CashReceiptConceptXpo) Implements ICards.CashReceiptConceptCommisionXPO
        Get
            Return CType(INDSleCashReceiptConceptCommision.Properties.DataSource, XPCollection(Of CashReceiptConceptXpo))
        End Get
        Set(value As XPCollection(Of CashReceiptConceptXpo))
            INDSleCashReceiptConceptCommision.Properties.DataSource = value
        End Set
    End Property

    Public Property RetentionConceptCommisionXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements ICards.RetentionConceptCommisionXPO
        Get
            Return CType(INDSleRetentionConceptCommision.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleRetentionConceptCommision.Properties.DataSource = value
        End Set
    End Property

    Public Property IdCashReceiptConceptCommision As Integer Implements ICards.IdCashReceiptConceptCommision
        Get
            Return INDSleCashReceiptConceptCommision.EditValue
        End Get
        Set(value As Integer)
            INDSleCashReceiptConceptCommision.EditValue = value
        End Set
    End Property

    Public Property IdRetentionConceptCommision As Integer Implements ICards.IdRetentionConceptCommision
        Get
            Return INDSleRetentionConceptCommision.EditValue
        End Get
        Set(value As Integer)
            INDSleRetentionConceptCommision.EditValue = value
        End Set
    End Property

    Dim _costCenterOptions As List(Of Tuple(Of Byte, String))
    Public ReadOnly Property GetOptionCostCenter As List(Of Tuple(Of Byte, String))
        Get
            If _costCenterOptions Is Nothing Then
                _costCenterOptions = New List(Of Tuple(Of Byte, String))()
                _costCenterOptions.Add(New Tuple(Of Byte, String)(1, "Centro de costo Unico"))
                _costCenterOptions.Add(New Tuple(Of Byte, String)(2, "Centro de costo Por Unidad Operativa"))
            End If
            Return _costCenterOptions
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' variable que contiene una secuencia
    ''' </summary>
    Private _sequence As TreasurySequence

    ''' <summary>
    ''' Contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Presentador de Tarjetas
    ''' </summary>
    Dim Presenter As PCards

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Entidad de tarjetas
    ''' </summary>
    Dim Card As Cards

    ''' <summary>
    ''' variable que almacena el registro bloqueado en el modulo
    ''' </summary>
    Dim record As BlockRecordTreasury

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        'If Not SearchMode Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'If Me.Card IsNot Nothing AndAlso Me.Card.Id > 0 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Try
        '            Using Model As New MCards(Me.Tag.ToString())
        '                Me.Card.MarkAsDeleted()
        '                AsyncLoader(True)
        '                Dim result = Await Model.DeleteCard(Me.Card)

        '                If result.StateResult = True Then
        '                    AsyncLoader(False)
        '                    Await Me.DeleteDocumentIndexed()
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                    SearchMode = False
        '                    Me.Deshacer()
        '                Else
        '                    If result.MessageResult(0) = "-999" Then
        '                        AsyncLoader(False)
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                    ElseIf result.MessageResult(0) = "-000" Then
        '                        AsyncLoader(False)
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                    Else
        '                        AsyncLoader(False)
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                    End If
        '                End If
        '            End Using
        '        Catch ex As Exception
        '            AsyncLoader(False)
        '            INDbteCode.Enabled = False
        '            Throw ex
        '        End Try
        '    End If
        'End If


        If Me.Card IsNot Nothing AndAlso Me.Card.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MCards(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteCard(Me.Card)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Exit Sub
        'End If
        ''If GetCostCenter = 2 AndAlso (ListOperatingUnitCostCenter Is Nothing OrElse Not ListOperatingUnitCostCenter.Any()) Then
        ''    Mensaje(EeventViewerImages.Advertencia) = "Es necesario parametrizar alguna unidad operativa a un centro de costo"
        ''    Exit Sub
        ''End If
        'AssigningValues()
        'Try
        '    Using Model As New MCards(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result = Await Model.SaveCard(Me.Card, Me._idCurrentSequense)
        '        If Result.StateResult = True Then
        '            If Card.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
        '                    Me.DicSequense(Me._sequence.TreasurySequenceDetail(0).Id).RemoveAt(0)
        '                End If
        '                If Me._sequence.Sequential Then
        '                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
        '                Else
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
        '                End If
        '            ElseIf Card.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '            End If
        '            AsyncLoader(False)
        '            Me.Card = Result.ObjectEmbbeded
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            SearchMode = False

        '            Me.Deshacer()
        '        Else
        '            AsyncLoader(False)
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    INDbteCode.Enabled = False
        '    Throw ex
        'End Try




        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MCards(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of Cards) = Await Model.SaveCard(Me.Card, Me._idCurrentSequense)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Card.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If
                    Me.Card = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewCard()
            'If Sequense IsNot Nothing AndAlso Sequense.Id > 0 Then
            '    ActionsOnControls = True
            '    INDbteCode.Enabled = False
            'End If
        End If
    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo concepto de retencion
    ''' </summary>
    Private Async Function NewCard() As Task


        Card = New Cards()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequence.TreasurySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MCommonTreasury(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCard
            .ValorSolicitado = "Code"
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorna el valor obtenido por el formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICards.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleThirdParty.Enabled = value
            INDSleCashReceiptConceptCommision.Enabled = value
            INDSleCostCenterCommision.Enabled = value
            INDSleRetentionConceptCommision.Enabled = value
            INDSLeRetentionConceptICA.Enabled = value
            INDSLeRetentionConceptRTF.Enabled = value
            INDSleCashReceiptConceptRFT.Enabled = value
            INDSleCostCenterRFT.Enabled = value
            INDSleCashReceiptConceptICA.Enabled = value
            INDSleCostCenterICA.Enabled = value
            INDGleGetCostCenter.Enabled = value
            INDPceAddOperatingUnit.Enabled = value
            INDGcOperatingUnitCostCenter.Enabled = value
            INDlycRoot.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Generates the document indexed.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Card.Code, Me.Card.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.Card.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Card.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Card.Code, Me.Card.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Card.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Loads the status.
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        'INDlycRoot.BeginUpdate()


        'INDlycRoot.EndUpdate()

        'Me.BarraBotones.StatusRecordVisible = False
        ' DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        ' Me.BarraBotones.DisableBarDocument()
        'Me.BarraBotones.CleanAuditBasic()

        '////////////////////

        INDlycRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Status = True
        'Limpiar controles
        ActionsOnControls = False
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDsleThirdParty.EditValue = Nothing
        INDsleThirdParty.Properties.NullText = String.Empty
        INDSleCashReceiptConceptCommision.EditValue = Nothing
        INDSleCashReceiptConceptCommision.Properties.NullText = String.Empty
        INDLciCostCenterCommision.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciCostCenterCommision.AllowHide = True
        INDLciCostCenterCommision.ShowInCustomizationForm = True
        INDSleCostCenterCommision.EditValue = Nothing
        INDSleCostCenterCommision.Properties.NullText = String.Empty
        INDSleRetentionConceptCommision.EditValue = Nothing
        INDSleRetentionConceptCommision.Properties.NullText = String.Empty
        INDSLeRetentionConceptICA.EditValue = Nothing
        INDSLeRetentionConceptICA.Properties.NullText = String.Empty
        INDSLeRetentionConceptRTF.EditValue = Nothing
        INDSLeRetentionConceptRTF.Properties.NullText = String.Empty
        INDSleCashReceiptConceptRFT.EditValue = Nothing
        INDSleCashReceiptConceptRFT.Properties.NullText = String.Empty
        INDLciCostCenterRFT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciCostCenterRFT.AllowHide = True
        INDLciCostCenterRFT.ShowInCustomizationForm = True
        INDSleCostCenterRFT.EditValue = Nothing
        INDSleCostCenterRFT.Properties.NullText = String.Empty
        INDSleCashReceiptConceptICA.EditValue = Nothing
        INDSleCashReceiptConceptICA.Properties.NullText = String.Empty
        INDLciCostCenterICA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciCostCenterICA.ShowInCustomizationForm = True
        INDLciCostCenterICA.AllowHide = True
        INDSleCostCenterICA.EditValue = Nothing
        INDSleCostCenterICA.Properties.NullText = String.Empty
        INDGleGetCostCenter.EditValue = Nothing
        INDGcOperatingUnitCostCenter.DataSource = Nothing
        INDLcgParametrizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Card = Nothing

        handlesCostCenterCommision = False
        handlesCostCenterICA = False
        handlesCostCenterRFT = False

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task


        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MCards(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetCard(Me.Code)
                    Card = resultOperation.ObjectEmbbeded
                    INDlycRoot.BeginUpdate()
                    If Card IsNot Nothing AndAlso Card.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MCommonTreasury(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecordTreasury(CStr(Me.Tag), CStr(Card.Id))
                            With Card
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Code = .Code
                                NameCard = .Name
                                IdThirdParty = .IdThirdParty
                                INDsleThirdParty.Properties.NullText = .CodeNameThirdParty
                                INDSleCashReceiptConceptCommision.EditValue = .IdCashReceiptConceptCommision
                                INDSleCashReceiptConceptCommision.Properties.NullText = .CodeNameCashReceiptConceptCommision
                                INDSleCostCenterCommision.EditValue = .CommisionCostCenterId
                                INDSleCostCenterCommision.Properties.NullText = .CodeNameCostCenterCommision
                                INDSleRetentionConceptCommision.EditValue = .IdRetentionConceptCommision
                                INDSleRetentionConceptCommision.Properties.NullText = .CodeNameRetentionConceptCommision
                                IdRetentionConceptRTF = .IdRetentionConceptRTF
                                INDSLeRetentionConceptRTF.Properties.NullText = .CodeNameRetentionConceptRTF
                                IdRetentionConceptICA = .IdRetentionConceptICA
                                INDSLeRetentionConceptICA.Properties.NullText = .CodeNameRetentionConceptICA
                                IdCashReceiptConceptICA = .IdCashReceiptConceptICA
                                INDSleCashReceiptConceptICA.Properties.NullText = .CodeNamedCashReceiptConceptICA
                                INDSleCostCenterICA.EditValue = .ICACostCenterId
                                INDSleCostCenterICA.Properties.NullText = .CodeNameCostCenterICA
                                IdCashReceiptConceptRTF = .IdCashReceiptConceptRTF
                                INDSleCashReceiptConceptRFT.Properties.NullText = .CodeNameCashReceiptConceptRTF
                                INDSleCostCenterRFT.EditValue = .RTFCostCenterId
                                INDSleCostCenterRFT.Properties.NullText = .CodeNameCostCenterRTF
                                GetCostCenter = .GetCostCenter
                                Status = .Status
                            End With
                            'Llenar NullText
                            handlesCostCenterCommision = Card.HandlesCostCenterCommision
                            handlesCostCenterRFT = Card.HandlesCostCenterRFT
                            handlesCostCenterICA = Card.HandlesCostCenterICA

                            ListOperatingUnitCostCenter = Card.CardCostCenter.ToList()
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Card.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Card.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(Card.Id, Me.Tag.ToString(), Nothing, GetType(Cards).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewCard()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function



    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With Card
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameCard
            .IdThirdParty = IdThirdParty
            .IdCashReceiptConceptCommision = IdCashReceiptConceptCommision
            .IdRetentionConceptCommision = IdRetentionConceptCommision
            .CommisionCostCenterId = INDSleCostCenterCommision.EditValue
            .IdRetentionConceptRTF = IdRetentionConceptRTF
            .IdRetentionConceptICA = IdRetentionConceptICA
            .IdCashReceiptConceptICA = IdCashReceiptConceptICA
            .ICACostCenterId = INDSleCostCenterICA.EditValue
            .IdCashReceiptConceptRTF = IdCashReceiptConceptRTF
            .RTFCostCenterId = INDSleCostCenterRFT.EditValue
            .GetCostCenter = GetCostCenter
            .Status = Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()


        If Not String.IsNullOrEmpty(Me.Card.Code) Then
            Try
                Using model As New MCards(Me.Tag)
                    Dim state As Boolean = Status = eActionsStatusRecords.Active
                    AsyncLoader(True)
                    Dim result As ActionResult(Of Cards) = Await model.UpdateStateCard(Me.Card.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Card = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Handles the Load event of the FrmCards control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCards_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        'Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        'Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        'Me.indigo = SessionValues.Instance
        'Presenter = New PCards(Me)
        'Presenter.LoadDefinitionLayout()
        'Presenter.GetSequence()
        'If FormSearchObjects Is Nothing Then
        '    SearchMode = False
        'End If
        'Dim actions As New List(Of eAcciones)
        'actions.Add(eAcciones.Remove)
        'IndigoGridView1.SetListAcction(INDGvOperatingUnit, actions)
        'Dim column As GridColumn = INDGvOperatingUnit.Columns.Where(Function(x) x.Name.Equals("colActions")).FirstOrDefault()
        'If column IsNot Nothing Then
        '    column.Width = 250
        'End If
        'INDGleGetCostCenter.Properties.DataSource = GetOptionCostCenter
        'LoadStatus()
        'Deshacer()


        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PCards(Me)
        Presenter.GetSequence()
        'Presenter.LoadDefinitionLayout()
        Dim actions As New List(Of eAcciones)
        actions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvOperatingUnit, actions)
        Dim column As GridColumn = INDGvOperatingUnit.Columns.Where(Function(x) x.Name.Equals("colActions")).FirstOrDefault()
        If column IsNot Nothing Then
            column.Width = 250
        End If
        INDGleGetCostCenter.Properties.DataSource = GetOptionCostCenter
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewCard()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el formulario esta listo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCards_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    Private Sub INDSbAddCostCenterOperatingUnit_Click(sender As Object, e As EventArgs) Handles INDSbAddCostCenterOperatingUnit.Click
        If INDSleOperatingUnit.EditValue Is Nothing OrElse INDSleCostCenter.EditValue Is Nothing Then
            Exit Sub
        End If
        If ListOperatingUnitCostCenter IsNot Nothing AndAlso ListOperatingUnitCostCenter.Any(Function(o) o.OperatingUnitId = CInt(INDSleOperatingUnit.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "La unidad operativa seleccionada ya se encuentra en el listado"
            Exit Sub
        End If
        Dim _cardCostCenter As New CardCostCenter()
        With _cardCostCenter
            .OperatingUnitId = CInt(INDSleOperatingUnit.EditValue)
            .CostCenterId = CInt(INDSleCostCenter.EditValue)
            .OperatingUnitCodeName = INDSleOperatingUnit.Text
            .CostCenterCodeName = INDSleCostCenter.Text
        End With
        Card.CardCostCenter.Add(_cardCostCenter)
        ListOperatingUnitCostCenter = Card.CardCostCenter.ToList()
        INDSleOperatingUnit.EditValue = Nothing
        INDSleCostCenter.EditValue = Nothing
        INDSleOperatingUnit.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al seleccionar un registro desde el Vituelf
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        'If Me.Card IsNot Nothing AndAlso Me.Card.Id > 0 Then
        '    If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
        '        DeleteBlockedRecord()
        '        Me.INDbteCode.Text = Me.IdEntity.Trim()
        '        Me.LoadControls()
        '    End If
        'Else 'Realiza la consulta normal
        '    Me.INDbteCode.Text = Me.IdEntity.Trim()
        '    Me.LoadControls()
        '    If FormSearchObjects IsNot Nothing Then
        '        FormSearchObjects.Close()
        '    End If
        'End If
        'Me.IdEntity = String.Empty
        If Me.Card IsNot Nothing AndAlso Me.Card.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(800, 700)
                Formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeThirdPartyXPO()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleConceptRet control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCashReceiptConceptICA_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCashReceiptConceptICA.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCashReceiptsConcepts
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(800, 700)
                Formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeCashReceiptConceptIcaXPO()
            End Using
        End If
    End Sub

    Private Sub INDSleCashReceiptConceptRFT_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCashReceiptConceptRFT.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCashReceiptsConcepts
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(800, 700)
                Formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeCashReceiptConceptRtfXPO()
            End Using
        End If
    End Sub


    Private Sub INDSleCashReceiptConceptRFT_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashReceiptConceptRFT.QueryPopUp
        If CashReceiptConceptRtfXPO Is Nothing Then
            Presenter.InitializeCashReceiptConceptRtfXPO()
        End If
    End Sub

    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If ThirdPartyXPO Is Nothing Then
            Presenter.InitializeThirdPartyXPO()
        End If
    End Sub


    Private Sub INDSleCashReceiptConceptICA_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashReceiptConceptICA.QueryPopUp
        If CashReceiptConceptIcaXPO Is Nothing Then
            Presenter.InitializeCashReceiptConceptIcaXPO()
        End If
    End Sub

    Private Sub INDSLeRetentionConceptRFT_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSLeRetentionConceptRTF.ButtonClick, INDSLeRetentionConceptICA.ButtonClick, INDSleRetentionConceptCommision.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmRetentionConcept
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeRetentionConceptXPO()
                Dim sleRetention = DirectCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
                sleRetention.Focus()
            End Using
        End If
    End Sub


    Public Property RetentionConceptICAXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements ICards.RetentionConceptICAXPO
        Get
            Return CType(INDSLeRetentionConceptICA.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSLeRetentionConceptICA.Properties.DataSource = value
        End Set
    End Property

    Public Property RetentionConceptRTFXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements ICards.RetentionConceptRTFXPO
        Get
            Return CType(INDSLeRetentionConceptRTF.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSLeRetentionConceptRTF.Properties.DataSource = value
        End Set
    End Property

    Private Sub INDSLeRetentionConceptRTF_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSLeRetentionConceptRTF.QueryPopUp
        If RetentionConceptRTFXPO Is Nothing Then
            Presenter.InitializeRetentionConceptXPO()
        End If
    End Sub

    Private Sub INDSLeRetentionConceptICA_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSLeRetentionConceptICA.QueryPopUp
        If RetentionConceptICAXPO Is Nothing Then
            Presenter.InitializeRetentionConceptXPO()
        End If
    End Sub

    Private Sub INDSleCashReceiptConceptCommision_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashReceiptConceptCommision.QueryPopUp
        If CashReceiptConceptCommisionXPO Is Nothing Then
            Presenter.InitializeCashReceiptConceptCommisionXPO()
        End If
    End Sub

    Private Sub INDSleRetentionConceptCommision_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleRetentionConceptCommision.QueryPopUp
        If RetentionConceptCommisionXPO Is Nothing Then
            Presenter.InitializeRetentionConceptXPO()
        End If
    End Sub


    ''' <summary>
    ''' Handles the FormClosing event of the FrmThirdPartyAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmCards_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
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
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If


        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

    Private Sub INDSleCashReceiptConceptCommision_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCashReceiptConceptCommision.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm("630", Nothing, True)
        End If
    End Sub

    Dim handlesCostCenterCommision As Boolean
    Dim handlesCostCenterRFT As Boolean
    Dim handlesCostCenterICA As Boolean

    Private Sub INDSleCashReceiptConceptCommision_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashReceiptConceptCommision.EditValueChanged
        If INDSleCashReceiptConceptCommision.EditValue IsNot Nothing Then
            If GvCommision.GetFocusedRow() IsNot Nothing Then
                handlesCostCenterCommision = CType(GvCommision.GetFocusedRow(), CashReceiptConceptXpo).IdMainAccount.HandlesCostCenter
            End If
            INDLciCostCenterCommision.HideControl(INDLcgParametrizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always OrElse Not handlesCostCenterCommision)
            If Not handlesCostCenterCommision Then
                INDSleCostCenterCommision.EditValue = Nothing
                INDSleCostCenterCommision.Properties.NullText = ""
            End If
            'SetHandlesCostCenter(INDSleCashReceiptConceptCommision.EditValue, INDLciCostCenterCommision, INDSleCostCenterCommision)
        End If
    End Sub

    Private Sub INDSleCashReceiptConceptRFT_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashReceiptConceptRFT.EditValueChanged
        If INDSleCashReceiptConceptRFT.EditValue IsNot Nothing Then
            If GvRFT.GetFocusedRow() IsNot Nothing Then
                handlesCostCenterRFT = CType(CType(GvRFT.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CashReceiptConceptXpo).IdMainAccount.HandlesCostCenter
            End If
            INDLciCostCenterRFT.HideControl(INDLcgParametrizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always OrElse Not handlesCostCenterRFT)
            If Not handlesCostCenterRFT Then
                INDSleCostCenterRFT.EditValue = Nothing
                INDSleCostCenterRFT.Properties.NullText = ""
            End If
            'SetHandlesCostCenter(INDSleCashReceiptConceptRFT.EditValue, INDLciCostCenterRFT, INDSleCostCenterRFT)
        End If
    End Sub

    ''' <summary>
    ''' metodo para saber si la cuenta del concepto maneja centro de costo y asi soliocitarlo
    ''' </summary>
    ''' <param name="conceptId"></param>
    ''' <param name="layoutItem"></param>
    ''' <remarks></remarks>
    Private Sub SetHandlesCostCenter(conceptId As Integer, layoutItem As DevExpress.XtraLayout.LayoutControlItem, searchControl As DevExpress.XtraEditors.SearchLookUpEdit)
        Using model As New MCashReceiptsConcepts(MyTag)
            Dim concept = model.GetCashReceiptConceptById(conceptId)
            Using modelPuc As New MPUC(MyTag)
                Dim account = modelPuc.GetAccountByIdSimple(concept.IdMainAccount, False)
                If account.HandlesCostCenter Then
                    layoutItem.HideControl(False)
                    'layoutItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    'layoutItem.ShowInCustomizationForm = False
                    'layoutItem.AllowHide = False
                Else
                    layoutItem.HideControl()
                    'layoutItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'layoutItem.ShowInCustomizationForm = True
                    'layoutItem.AllowHide = True
                    searchControl.EditValue = Nothing
                End If
            End Using
        End Using
    End Sub

    Private Sub INDSleCashReceiptConceptICA_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashReceiptConceptICA.EditValueChanged
        If INDSleCashReceiptConceptICA.EditValue IsNot Nothing Then
            If GvICA.GetFocusedRow() IsNot Nothing Then
                handlesCostCenterICA = CType(CType(GvRFT.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CashReceiptConceptXpo).IdMainAccount.HandlesCostCenter
            End If
            INDLciCostCenterICA.HideControl(INDLcgParametrizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always OrElse Not handlesCostCenterICA)
            If Not handlesCostCenterICA Then
                INDSleCostCenterICA.EditValue = Nothing
                INDSleCostCenterICA.Properties.NullText = ""
            End If
            'SetHandlesCostCenter(INDSleCashReceiptConceptICA.EditValue, INDLciCostCenterICA, INDSleCostCenterICA)
        End If
    End Sub

    Private Sub INDSleCostCenterCommision_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCostCenterCommision.QueryPopUp
        If CostCenterComissionXPO Is Nothing Then
            If INDSleCostCenterCommision.Properties.ReadOnly = False Then
                InitializeCostCenterComissionXPO()
            End If
        End If
    End Sub

    ''' <summary>
    ''' inicializa la consulta de centros de costo
    ''' </summary>
    Private Sub InitializeCostCenterComissionXPO()
        Using model As New MBusqueda
            CostCenterComissionXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetCostCenterByState, "True")
        End Using
    End Sub

    Property CostCenterComissionXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleCostCenterCommision.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCostCenterCommision.Properties.DataSource = value
        End Set
    End Property

    Private Sub INDSleCostCenterRFT_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCostCenterRFT.QueryPopUp
        If CostCenterRTFXPO Is Nothing Then
            If INDSleCostCenterRFT.Properties.ReadOnly = False Then
                InitializeCostCenterRTFXPO()
            End If
        End If
    End Sub

    ''' <summary>
    ''' inicializa la consulta de centros de costo
    ''' </summary>
    Private Sub InitializeCostCenterRTFXPO()
        Using model As New MBusqueda
            CostCenterComissionXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetCostCenterByState, "True")
        End Using
    End Sub

    Property CostCenterRTFXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleCostCenterRFT.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCostCenterRFT.Properties.DataSource = value
        End Set
    End Property

    Private Sub INDSleCostCenterICA_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCostCenterICA.QueryPopUp
        If CostCenterICAXPO Is Nothing Then
            If INDSleCostCenterICA.Properties.ReadOnly = False Then
                InitializeCostCenterICAXPO()
            End If
        End If
    End Sub

    ''' <summary>
    ''' inicializa la consulta de centros de costo
    ''' </summary>
    Private Sub InitializeCostCenterICAXPO()
        Using model As New MBusqueda
            CostCenterComissionXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetCostCenterByState, "True")
        End Using
    End Sub

    Property CostCenterICAXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleCostCenterICA.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCostCenterICA.Properties.DataSource = value
        End Set
    End Property

    Private Sub INDPceAddOperatingUnit_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceAddOperatingUnit.Closed
        INDSleOperatingUnit.EditValue = Nothing
        INDSleCostCenter.EditValue = Nothing
    End Sub

    Private Sub INDPceAddOperatingUnit_QueryPopUp(sender As Object, e As System.EventArgs) Handles INDPceAddOperatingUnit.Popup
        INDSleOperatingUnit.Focus()
    End Sub

    Private Sub INDSleOperatingUnit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleOperatingUnit.QueryPopUp
        If INDSleOperatingUnit.Properties.DataSource Is Nothing Then
            INDSleOperatingUnit.Properties.DataSource = mCards.ListOperatingUnit()
        End If
    End Sub

    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If INDSleCostCenter.Properties.DataSource Is Nothing Then
            INDSleCostCenter.Properties.DataSource = mCards.ListCostCenter()
        End If
    End Sub

    Private Sub INDGleGetCostCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleGetCostCenter.EditValueChanged
        If INDGleGetCostCenter.EditValue IsNot Nothing Then
            If CByte(INDGleGetCostCenter.EditValue) = 1 Then
                If handlesCostCenterCommision Then
                    INDLciCostCenterCommision.HideControl(False)
                End If
                If handlesCostCenterRFT Then
                    INDLciCostCenterRFT.HideControl(False)
                End If
                If handlesCostCenterICA Then
                    INDLciCostCenterICA.HideControl(False)
                End If
                INDLcgParametrizacion.HideControl()
            ElseIf CByte(INDGleGetCostCenter.EditValue) = 2 Then
                INDLciCostCenterCommision.HideControl()
                INDLciCostCenterRFT.HideControl()
                INDLciCostCenterICA.HideControl()
                INDLcgParametrizacion.HideControl(False)
                INDSleCostCenterCommision.EditValue = Nothing
                INDSleCostCenterRFT.EditValue = Nothing
                INDSleCostCenterICA.EditValue = Nothing
                INDSleCostCenterCommision.Properties.NullText = ""
                INDSleCostCenterICA.Properties.NullText = ""
                INDSleCostCenterRFT.Properties.NullText = ""
            End If
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim record As CardCostCenter = CType(INDGvOperatingUnit.GetFocusedRow(), CardCostCenter)
            record.MarkAsDeleted()
            If Card.Id > 0 Then
                Card.MarkAsModified()
            End If
            ListOperatingUnitCostCenter = Card.CardCostCenter.ToList()
        End If
    End Sub

    Private Sub INDGcOperatingUnitCostCenter_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcOperatingUnitCostCenter.DataSourceChanged
        INDGleGetCostCenter.Properties.ReadOnly = ListOperatingUnitCostCenter IsNot Nothing AndAlso ListOperatingUnitCostCenter.Any()
    End Sub
End Class
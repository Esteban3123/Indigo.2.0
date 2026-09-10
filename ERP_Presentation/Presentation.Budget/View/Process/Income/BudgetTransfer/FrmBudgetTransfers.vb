'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/08/2015
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

Public Class FrmBudgetTransfers
    Implements IBudgetTransfers

#Region "Builder"

    Public ctrTmp As CtrInfoEntity

    Public Sub New()
        ' Add any initialization after the InitializeComponent() call.
        InitializeComponent()
        ' This call is required by the designer.
        ctrTmp = New CtrInfoEntity()
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshInfo()
        ctrTmp.PopupContainerControlEntity = INDpccChangeEntity
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Tuple(Of Integer, String, Integer, String, String)
        Return New Tuple(Of Integer, String, Integer, String, String)(budgetEntityIdStandard, budgetEntityCodeName, validityIdStandard, validityCodeName, statusValidity)
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

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As BudgetSequence Implements IBudgetTransfers.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BudgetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.BudgetSequenceDetail In Me._sequence.BudgetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityId As Integer? Implements IBudgetTransfers.BudgetEntityId
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpo As XPInstantFeedbackSource Implements IBudgetTransfers.BudgetEntityXpo
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
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Consecutive As String Implements IBudgetTransfers.Consecutive
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
    ''' Obtiene o establece la fecha
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateBudgetTransfer As Date? Implements IBudgetTransfers.DateBudgetTransfer
        Get
            Return INDdteDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Document As String Implements IBudgetTransfers.Document
        Get
            Return INDtxtDocument.EditValue
        End Get
        Set(value As String)
            INDtxtDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBudgetTransfers.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IBudgetTransfers.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements IBudgetTransfers.Observations
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityId As Integer? Implements IBudgetTransfers.ValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer?)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpo As XPCollection Implements IBudgetTransfers.ValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityIdPopup As Integer? Implements IBudgetTransfers.BudgetEntityIdPopup
        Get
            Return INDsleEntityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityPopUp.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpoPopup As XPInstantFeedbackSource Implements IBudgetTransfers.BudgetEntityXpoPopup
        Get
            Return INDsleEntityPopUp.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityIdPopup As Integer? Implements IBudgetTransfers.ValidityIdPopup
        Get
            Return INDsleValidityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleValidityPopUp.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpoPopup As XPCollection Implements IBudgetTransfers.ValidityXpoPopup
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Controla el changed de los search
    ''' </summary>
    ''' <remarks></remarks>
    Dim controlerChanged As Boolean = False

    ''' <summary>
    ''' Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim statusValidity As String

    ''' <summary>
    ''' Establece el id de la vigencia tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim validityIdStandard As Integer

    ''' <summary>
    ''' Establece el id de la entidad presupuestal tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim budgetEntityIdStandard As Integer

    ''' <summary>
    ''' Establece el codigo y nombre de la vigencia tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim validityCodeName As String

    ''' <summary>
    ''' Establece el codigo y nombre de la entidad presupuestal tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim budgetEntityCodeName As String

    ''' <summary>
    ''' Representa a la entidad de traslado de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Dim BudgetTransfer As BudgetTransfer

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PBudgetTransfer

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Lista los detalles del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListBudgetTransferDetail As List(Of BudgetTransferDetail)

    ''' <summary>
    ''' Lista los detalles eliminados del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteBudgetTransferDetail As List(Of BudgetTransferDetail)

    ''' <summary>
    ''' Listado de nuevos de presupuesto inicial que sera enviado
    ''' al form popup que se despliega
    ''' </summary>
    ''' <remarks></remarks>
    Public ListNewBudget As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Representa al mes
    ''' </summary>
    ''' <remarks></remarks>
    Dim monthValidity As Integer

    ''' <summary>
    ''' Representa al año
    ''' </summary>
    ''' <remarks></remarks>
    Dim yearValidity As Integer

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que coloca el estado en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatus()
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 AndAlso ValidityId IsNot Nothing Then
            Dim item = (From l In ValidityXpo Where l.Id = ValidityId Select l).FirstOrDefault
            If item IsNot Nothing Then
                Select Case item.Status
                    Case 1
                        statusValidity = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        statusValidity = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        statusValidity = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        statusValidity = String.Empty
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que coloca el estado en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatusPopup()
        If ValidityXpoPopup IsNot Nothing AndAlso ValidityXpoPopup.Count > 0 AndAlso ValidityIdPopup IsNot Nothing Then
            Dim item = (From l In ValidityXpoPopup Where l.Id = ValidityIdPopup Select l).FirstOrDefault
            If item IsNot Nothing Then
                Select Case item.Status
                    Case 1
                        statusValidity = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        statusValidity = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        statusValidity = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        statusValidity = String.Empty
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 Then
            Dim item = (From l In ValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                ValidityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que agrega un rubro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCategory()
        If validityIdStandard = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una vigencia."
            Exit Sub
        End If
        Using Formulario As New FrmAddCategory()
            AddHandler Formulario.AddBudgetToGridFormPrincipal, AddressOf AddBudgetToGridFormPrincipal
            Formulario.ToolBar.Dock = DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 870
            Formulario.Height = 768
            Formulario.ValidityId = validityIdStandard
            Formulario.ListNewBudget = ListNewBudget
            Dim frm As New FrmTransparent(Formulario, False)
            frm.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega el presupuesto que viene del form
    ''' que se lanza de forma modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddBudgetToGridFormPrincipal(sender As Object, e As AddInfoToGridFormPrincipal)
        If e.ListSend IsNot Nothing AndAlso e.ListSend.Count > 0 Then
            If Not (ListBudgetTransferDetail IsNot Nothing AndAlso ListBudgetTransferDetail.Count > 0) Then
                ListBudgetTransferDetail = New List(Of BudgetTransferDetail)
            Else
                Dim listErrors As New StringBuilder
                For Each item As Domain.Entities.Budget In e.ListSend
                    Dim cont = (From l In ListBudgetTransferDetail Where l.CategoryId = item.CategoryId AndAlso l.RevenueTypeId = item.RevenueTypeId Select l).Count
                    If cont > 0 Then
                        listErrors.AppendLine("No se puede agregar el rubro " + item.CodeCategory + " - " + item.NameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " porque ya existe en la lista del formulario principal.")
                    End If
                Next
                If listErrors.ToString.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                    Exit Sub
                End If
            End If
            ListNewBudget = e.ListNew

            e.ListSend.ForEach(Sub(item)
                                   Dim budgetTransferDetail As New BudgetTransferDetail
                                   With budgetTransferDetail
                                       .BudgetId = item.Id

                                       .CategoryId = item.CategoryId
                                       .CodeCategory = item.CodeCategory
                                       .NameCategory = item.NameCategory

                                       .FinancialSourceId = item.FinancialSourceId
                                       .CodeNameFinancialSource = item.CodeNameFinancialSource

                                       .RevenueTypeId = item.RevenueTypeId
                                       .CodeNameRevenueType = item.CodeNameRevenueType

                                       .Balance = item.Balance
                                       .Nature = 0
                                       .Value = 0
                                       ListBudgetTransferDetail.Add(budgetTransferDetail)
                                   End With
                                   If item.Id = 0 Then
                                       budgetTransferDetail.Budget = item
                                   End If
                               End Sub)

            INDgcCategory.DataSource = Nothing
            INDgcCategory.DataSource = ListBudgetTransferDetail
            Mensaje(EeventViewerImages.Informacion) = "Detalles agregados correctamente."
        End If
    End Sub

    ''' <summary>
    ''' Metodos para asignar valores a la entidad financial source
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With BudgetTransfer
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Consecutive
            .BudgetaryValidityId = validityIdStandard
            .DocumentSource = 1
            .DocumentDate = DateBudgetTransfer
            .Document = Document
            .Observations = Observations
            If ListBudgetTransferDetail IsNot Nothing AndAlso ListBudgetTransferDetail.Count > 0 Then
                ListBudgetTransferDetail.ForEach(Sub(item)
                                                     .BudgetTransferDetail.Add(item)
                                                 End Sub)
            End If
            If ListDeleteBudgetTransferDetail IsNot Nothing AndAlso ListDeleteBudgetTransferDetail.Count > 0 Then
                ListDeleteBudgetTransferDetail.ForEach(Sub(item)
                                                           .BudgetTransferDetail.Add(item)
                                                       End Sub)
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Using Model As New MBudgetTransfer(MyTag)
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetBudgetTransfer(Consecutive, 1, yearValidity)
            INDlyBudgetTransfer.BeginUpdate()
            BudgetTransfer = resultOperation.ObjectEmbbeded
            If Not BudgetTransfer Is Nothing Then
                If BudgetTransfer.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(MyTag, BudgetTransfer.Id)
                    INDsleEntityPopUp.Properties.ReadOnly = True
                    INDsleValidityPopUp.Properties.ReadOnly = True
                    With BudgetTransfer
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        Consecutive = .Code
                        DateBudgetTransfer = .DocumentDate
                        Document = .Document
                        Observations = .Observations

                        ListBudgetTransferDetail = .BudgetTransferDetail.ToList

                        Me.BarraBotones.StatusRecord = .Status.ToString()
                        Me.BarraBotones.ControlHideStatus = True
                    End With
                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.BudgetTransfer.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = BudgetTransfer.Id}
                        Dim operation = Await Model.SaveBlockRecord(blockRecord)
                        blockRecord = operation.ObjectEmbbeded
                    Else
                        blockRecord = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    If BudgetTransfer.Status = 1 Then 'Registrado
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                    Else 'Confirmado o Anulado
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)

                    End If
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                    Me.BarraBotones.SetDocuments(BudgetTransfer.Id)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, BudgetTransfer.Id, 0, BudgetTransfer.Id)
                    AsyncLoader(False)
                    ActionsOnControls = True
                    INDgcCategory.DataSource = Nothing
                    INDgcCategory.DataSource = ListBudgetTransferDetail
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Me.NewBudgetTransfer()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Consecutive = String.Empty
                        Deshacer()
                        INDbtnConsecutive.Focus()
                    End If
                End If
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewBudgetTransfer()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Consecutive = String.Empty
                    Deshacer()
                    INDbtnConsecutive.Focus()
                End If
            End If
        End Using
        INDlyBudgetTransfer.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewBudgetTransfer()
        BudgetTransfer = New BudgetTransfer
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope Is Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Consecutive = String.Empty
                INDbtnConsecutive.Focus()
                Exit Sub
            End If
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.BudgetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                        PrepareToolbar(Me.DicSequense(Me._idCurrentSequence)(0))
                    Else
                        Using model As New ModelBaseBudget(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            PrepareToolbar(Me.DicSequense(Me._idCurrentSequence)(0))
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
    End Sub

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        Consecutive = code
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
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControlsTodo()
        ActionsOnControls = False
        ValidityId = Nothing
        ValidityIdPopup = Nothing
        CleanControls()
        INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        BarraBotones.StatusRecordVisible = False
        INDsleBudgetEntity.Focus()
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INDlyBudgetTransfer.BeginUpdate()
        INDsleEntityPopUp.Properties.ReadOnly = False
        INDsleValidityPopUp.Properties.ReadOnly = False
        ActionsOnControls = False
        Consecutive = String.Empty
        DateBudgetTransfer = Nothing
        Document = String.Empty
        Observations = String.Empty
        INDgcCategory.DataSource = Nothing
        ListNewBudget = Nothing
        ListBudgetTransferDetail = Nothing
        ListDeleteBudgetTransferDetail = Nothing
        BudgetTransfer = Nothing
        DeleteBlockedRecord()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        BarraBotones.StatusRecord = -1
        Me.BarraBotones.ControlHideStatus = False
        INDlyBudgetTransfer.EndUpdate()
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
    ''' Activa o Inactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBudgetTransfers.ActionsOnControls
        Set(value As Boolean)
            INDlyBudgetTransfer.BeginUpdate()
            INDbtnConsecutive.Enabled = Not value
            INDdteDate.Enabled = value
            INDtxtDocument.Enabled = value
            INDmemoObservations.Enabled = value
            INDbtnAddCategory.Enabled = value
            INDgcCategory.Enabled = value
            INDlyBudgetTransfer.EndUpdate()
            If value Then
                INDdteDate.Focus()
            Else
                INDbtnConsecutive.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), BudgetTransfer.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & BudgetTransfer.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), BudgetTransfer.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), BudgetTransfer.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), BudgetTransfer.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup()
        INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.ControlHideStatus = False
        BarraBotones.PrepareToolbar(eAction.NewAndFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBudgetTransfer(MyTag)
                Await Model.DeleteBlockRecord(blockRecord)
            End Using
            blockRecord = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.BudgetTransfer IsNot Nothing AndAlso Me.BudgetTransfer.Id > 0 Then
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

    ''' <summary>
    ''' Valida el detalle del traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As String
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de traslado en la rejilla
        If Not (ListBudgetTransferDetail IsNot Nothing AndAlso ListBudgetTransferDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de traslado para poder guardar.")
        End If
        'Se valida que el debito sea igual que el credito
        If ListBudgetTransferDetail IsNot Nothing AndAlso ListBudgetTransferDetail.Count > 0 Then
            Dim sumCredit As Decimal = ListBudgetTransferDetail.Sum(Function(item) As Decimal
                                                                        Return IIf(item.Nature = 2, item.Value, CDec(0.0))
                                                                    End Function)
            Dim sumDebit As Decimal = ListBudgetTransferDetail.Sum(Function(item) As Decimal
                                                                       Return IIf(item.Nature = 1, item.Value, CDec(0.0))
                                                                   End Function)
            If sumCredit <> sumDebit Then
                listErrors.AppendLine("Los débitos y los créditos de los detalles del traslado no son iguales.")
            End If
            'Se valida que en los detalles no haya ningun item con naturaleza ninguna
            ListBudgetTransferDetail.ForEach(Sub(item)
                                                 If item.Nature = 0 Then
                                                     listErrors.AppendLine("La naturaleza del rubro " + item.CodeCategory + " - " + item.NameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " no puede ser Ninguna.")
                                                 End If
                                             End Sub)
        End If
        'Se valida que hayan elegido una vigencia
        If validityIdStandard = 0 Then
            listErrors.AppendLine("Debe elegir una vigencia.")
        End If
        'Se valida que el mes y el año de la fecha del documento sea igual al mes y año de la vigencia
        Dim montDocument = Month(DateBudgetTransfer)
        Dim yearDocument = Year(DateBudgetTransfer)
        If montDocument <> monthValidity Then
            listErrors.AppendLine(String.Format(ResourceManager.GetString("SelectMonth", NAME_MODULE), MonthName(monthValidity, False)))
        End If
        If yearDocument <> yearValidity Then
            listErrors.AppendLine(String.Format(ResourceManager.GetString("SelectYear", NAME_MODULE), yearValidity.ToString))
        End If
        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim detail As BudgetTransferDetail = viewBudget.GetFocusedRow
        If detail.Id > 0 Then
            If ListDeleteBudgetTransferDetail Is Nothing Then
                ListDeleteBudgetTransferDetail = New List(Of BudgetTransferDetail)
            End If
            detail.MarkAsDeleted()
            ListDeleteBudgetTransferDetail.Add(detail)
        End If
        ListBudgetTransferDetail.Remove(detail)
        INDgcCategory.DataSource = Nothing
        INDgcCategory.DataSource = ListBudgetTransferDetail
    End Sub

#End Region

#Region "ICrud Base"
    ''' <summary>
    ''' Metodo buscar el cual obtiene la busqueda con el metodo OpenSearch()
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
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
    ''' Metodo que elimina la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        Try
            If BudgetTransfer IsNot Nothing AndAlso BudgetTransfer.Id > -1 Then
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using model As New MBudgetTransfer(MyTag)
                        AsyncLoader(True)
                        BudgetTransfer.MarkAsDeleted()
                        Dim result = Nothing 'Await model.DeleteBudgetDependency(Dependency)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            'SearchMode = False
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnConsecutive.Enabled = False
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If BudgetTransfer.Status <> 3 Then
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
            Using model As New MBudgetTransfer(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveBudgetTransfer(BudgetTransfer, _idCurrentSequence)
                If Result.StateResult = True Then
                    If Result.ObjectEmbbeded.Status = 1 Then 'Guardar o Actualizar
                        If BudgetTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If Me._sequence.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf BudgetTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    ElseIf Result.ObjectEmbbeded.Status = 2 Then 'Confirmar
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                    ElseIf Result.ObjectEmbbeded.Status = 3 Then 'Anular
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                    End If
                    Me.BudgetTransfer = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, BudgetTransfer.Id, 0, BudgetTransfer.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, BudgetTransfer.Id, 0, BudgetTransfer.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, BudgetTransfer.Id, 0, BudgetTransfer.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, BudgetTransfer.Id, 0, BudgetTransfer.Id)
                    End Select

                    AsyncLoader(False)
                    'SearchMode = False
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnConsecutive.Enabled = False
                    If Result.StateResult = False And Result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                    If BudgetTransfer.Id > 0 Then
                        Dim resul = Await model.GetBudgetTransfer(Consecutive, 1, yearValidity)
                        BudgetTransfer = resul.ObjectEmbbeded
                    Else
                        BudgetTransfer = New BudgetTransfer
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Metodo actualizar sin usarse
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
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            NewBudgetTransfer()
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
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo With {.Caption = "Documento", .FieldName = "Document", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {1, validityIdStandard}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBudgetTransferByType
            .FormParent = Me
            .ShowSearch()
        End With
        'SearchMode = True
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

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Libera la memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        controlerChanged = Nothing
        statusValidity = Nothing
        validityIdStandard = Nothing
        budgetEntityIdStandard = Nothing
        validityCodeName = Nothing
        budgetEntityCodeName = Nothing
        BudgetTransfer = Nothing
        presenter = Nothing
        blockRecord = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        ListBudgetTransferDetail = Nothing
        ListDeleteBudgetTransferDetail = Nothing
        ListNewBudget = Nothing
        monthValidity = Nothing
        yearValidity = Nothing
        varImp = Nothing
    End Sub


    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetTransfers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBudgetTransfer, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        presenter = New PBudgetTransfer(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        '****************************** 
        IndigoGridControl1.RefreshGrid(INDgcCategory)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewBudget, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewBudget.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        LoadStatus()
        DeshacerTodo()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de entidades presupuestales
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
                presenter.InitializeBudgetEntity()
                If BudgetEntityId IsNot Nothing Then
                    presenter.InitializeValidity(BudgetEntityId)
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de entidades presupuestales del popup
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
                presenter.InitializeBudgetEntityPopup()
                If BudgetEntityIdPopup IsNot Nothing Then
                    presenter.InitializeValidityPopup(BudgetEntityIdPopup)
                End If
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntityId IsNot Nothing Then
            ValidityId = Nothing
            ValidityXpo = Nothing
            budgetEntityIdStandard = BudgetEntityId
            budgetEntityCodeName = INDsleBudgetEntity.Text
            presenter.InitializeValidity(BudgetEntityId)
            SetFirstOrDefaultValidity()

            presenter.InitializeBudgetEntityPopup()
            controlerChanged = True
            BudgetEntityIdPopup = BudgetEntityId
            controlerChanged = False
            presenter.InitializeValidityPopup(BudgetEntityIdPopup)

            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de entidad presupuestal del info
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityPopUp.EditValueChanged
        If BudgetEntityIdPopup IsNot Nothing AndAlso controlerChanged = False Then
            ValidityIdPopup = Nothing
            ValidityXpoPopup = Nothing
            budgetEntityIdStandard = BudgetEntityIdPopup
            budgetEntityCodeName = INDsleEntityPopUp.Text
            validityIdStandard = 0
            validityCodeName = String.Empty
            statusValidity = String.Empty
            presenter.InitializeValidityPopup(BudgetEntityIdPopup)
            'SetFirstOrDefaultValidity()
            ctrTmp.RefreshInfo()
            'Deshacer()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If ValidityId IsNot Nothing Then
            validityIdStandard = ValidityId
            validityCodeName = INDsleValidity.Text

            controlerChanged = True
            ValidityIdPopup = ValidityId
            controlerChanged = False

            Dim item = (From l In ValidityXpo Where l.Id = ValidityId Select l).FirstOrDefault
            yearValidity = item.Year
            monthValidity = item.IncomeMonth

            SetStatus()
            ctrTmp.RefreshInfo()
            LoadLayoutGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de vigencia del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidityPopUp.EditValueChanged
        If ValidityIdPopup IsNot Nothing AndAlso controlerChanged = False Then
            validityIdStandard = ValidityIdPopup
            validityCodeName = INDsleValidityPopUp.Text

            Dim item = (From l In ValidityXpoPopup Where l.Id = ValidityIdPopup Select l).FirstOrDefault
            yearValidity = item.Year
            monthValidity = item.IncomeMonth

            SetStatusPopup()
            ctrTmp.RefreshInfo()
            'Deshacer()
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

    ''' <summary>
    ''' Metodo que valida la naturaleza con respecto al saldo y el valor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateNature(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim budgetTransferDetail As Domain.Entities.BudgetTransferDetail = CType(viewBudget.GetFocusedRow, Domain.Entities.BudgetTransferDetail)
            If budgetTransferDetail IsNot Nothing Then
                Dim control As DevExpress.XtraEditors.BaseEdit = sender
                Select Case True
                    Case control.EditorTypeName = controlTextEdit
                        budgetTransferDetail.Value = e.NewValue
                    Case control.EditorTypeName = controlImageComboBox
                        budgetTransferDetail.Nature = e.NewValue
                End Select
                If budgetTransferDetail.Nature = 1 Then
                    If budgetTransferDetail.Balance < budgetTransferDetail.Value Then
                        Mensaje(EeventViewerImages.Advertencia) = "Con la naturaleza débito el saldo no puede ser menor al valor."
                        Select Case True
                            Case control.EditorTypeName = controlTextEdit
                                budgetTransferDetail.Value = e.OldValue
                            Case control.EditorTypeName = controlImageComboBox
                                budgetTransferDetail.Nature = e.OldValue
                        End Select
                        e.Cancel = True
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetTransfers_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If ValidityId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una vigencia."
                Exit Sub
            End If
            If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnConsecutive.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnConsecutive.Text.Trim()) Then
                    Me.NewBudgetTransfer()
                Else
                    Await LoadControls()
                End If
            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntityXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad presupuestal del popup del control de info
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEntityPopUp.QueryPopUp
        If BudgetEntityXpoPopup Is Nothing Then
            presenter.InitializeBudgetEntityPopup()
        End If
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas del control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvValidity_CustomColumnDisplayText(sender As Object, e As Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvValidity.CustomColumnDisplayText
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

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar por primera vez el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetTransfers_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown, MyBase.Activated
        If INDsleBudgetEntity.Enabled Then
            INDsleBudgetEntity.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddCategory_Click(sender As Object, e As EventArgs) Handles INDbtnAddCategory.Click
        AddCategory()
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

    Private ValueTotal As Decimal
    ''' <summary>
    ''' totaliza el valor dependiendo de las validaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewBudget_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles viewBudget.CustomSummaryCalculate
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

#Region "BarraBotones"

    ''' <summary>
    ''' Evento actualizarConfirmar de la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            BudgetTransfer.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Evento guardarConfirmar de la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            BudgetTransfer.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Evento anular de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            BudgetTransfer.Status = 3
            varImp = 4
            Guardar()
        End If
    End Sub

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
        'SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        DeshacerTodo()
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
        BudgetTransfer.Status = 1
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
        BudgetTransfer.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, BudgetTransfer.Id, 0, BudgetTransfer.Id)
    End Sub
    ''' <summary>
    ''' Click Boton cambiar unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BudgetSequenceDetail IsNot Nothing Then
            If Me._sequence.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequence.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

#End Region

End Class
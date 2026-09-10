'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Jeisson Herrera Peña
' Created          : 26-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
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

Public Class FrmRecognition
    Implements IRecognition

#Region "Builders"

    Public ctrTmp As CtrInfoEntity

    ''' <summary>
    ''' Construct
    ''' </summary>
    ''' <remarks></remarks>
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

#End Region

#Region "Properties"

    ''' <summary>
    ''' Establece los tipos de reconocimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingRecognitionType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingRecognitionType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingRecognitionType Is Nothing Then
                _FillingRecognitionType = New List(Of Tuple(Of Integer, String))
                _FillingRecognitionType.Add(New Tuple(Of Integer, String)(1, "Reconocimiento"))
                _FillingRecognitionType.Add(New Tuple(Of Integer, String)(2, "Cuenta por Cobrar"))
                _FillingRecognitionType.Add(New Tuple(Of Integer, String)(3, "Radicación de Cuentas"))
            End If
            Return _FillingRecognitionType
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el solicitante
    ''' </summary>
    ''' <remarks></remarks>
    Private _applicant As String
    Public Property Applicant As String Implements IRecognition.Applicant
        Get
            Return _applicant
        End Get
        Set(value As String)
            _applicant = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el recaudo automático
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutomaticCollection As Boolean Implements IRecognition.AutomaticCollection
        Get
            Return INDGleAutomaticCollection.EditValue
        End Get
        Set(value As Boolean)
            INDGleAutomaticCollection.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer Implements IRecognition.BudgetaryValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityId As Integer? Implements IRecognition.BudgetEntityId
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityIdPopup As Integer? Implements IRecognition.BudgetEntityIdPopup
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
    Public Property BudgetEntityXpo As XPInstantFeedbackSource Implements IRecognition.BudgetEntityXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpoPopup As XPInstantFeedbackSource Implements IRecognition.BudgetEntityXpoPopup
        Get
            Return INDsleEntityPopUp.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IRecognition.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estabale la dependencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DependencyId As Integer Implements IRecognition.DependencyId
        Get
            Return INDSleDependency.EditValue
        End Get
        Set(value As Integer)
            INDSleDependency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Document As String Implements IRecognition.Document
        Get
            Return INDTxeDocument.EditValue
        End Get
        Set(value As String)
            INDTxeDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements IRecognition.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRecognition.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IRecognition.MyTag
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
    Public Property Observations As String Implements IRecognition.Observations
        Get
            Return INDMemObservation.EditValue
        End Get
        Set(value As String)
            INDMemObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de reconocimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RecognitionType As Integer Implements IRecognition.RecognitionType
        Get
            Return INDGleRecognitionType.EditValue
        End Get
        Set(value As Integer)
            INDGleRecognitionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As BudgetSequence Implements IRecognition.Sequense
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
    ''' Obtiene o estable el Tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer Implements IRecognition.ThirdPartyId
        Get
            Return INDSleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDSleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityIdPopup As Integer? Implements IRecognition.ValidityIdPopup
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
    Public Property ValidityXpo As XPCollection Implements IRecognition.ValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpoPopup As XPCollection Implements IRecognition.ValidityXpoPopup
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListThirdParty As XPInstantFeedbackSource Implements IRecognition.ListThirdParty
        Get
            Return INDSleThirdParty.Datasource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParty.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListDependency As XPInstantFeedbackSource Implements IRecognition.ListDependency
        Get
            Return INDSleDependency.Datasource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDependency.Datasource = value
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
    ''' Representa a la entidad de reconocimiento
    ''' </summary>
    ''' <remarks></remarks>
    Dim Recognition As Recognition

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PRecognition

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Lista los detalles del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRecognitionDetail As List(Of RecognitionDetail)

    ''' <summary>
    ''' Lista los detalles eliminados del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRecognitionDetail As List(Of Integer)

    ''' <summary>
    ''' Listado de nuevos de presupuesto inicial que sera enviado
    ''' al form popup que se despliega
    ''' </summary>
    ''' <remarks></remarks>
    Public ListNewBudget As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Objeto que contiene el tercero
    ''' </summary>
    Dim ThirdPartyTmp As Domain.Entities.ThirdParty

    ''' <summary>
    ''' Objeto que contiene la dependencia
    ''' </summary>
    Dim DependencyTmp As Domain.Entities.Dependency

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
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 AndAlso BudgetaryValidityId <> 0 Then
            Dim item = (From l In ValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
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
                BudgetaryValidityId = item.Id
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
            If Not (ListRecognitionDetail IsNot Nothing AndAlso ListRecognitionDetail.Count > 0) Then
                ListRecognitionDetail = New List(Of RecognitionDetail)
            Else
                Dim listErrors As New StringBuilder
                For Each item As Domain.Entities.Budget In e.ListSend
                    Dim cont = (From l In ListRecognitionDetail Where l.CategoryId = item.CategoryId AndAlso l.RevenueTypeId = item.RevenueTypeId Select l).Count
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
                                   Dim recognitionDetail As New RecognitionDetail
                                   With recognitionDetail

                                       .RevenueTypeId = item.RevenueTypeId
                                       .CategoryId = item.CategoryId
                                       .CodeCategory = item.CodeCategory
                                       .NameCategory = item.NameCategory

                                       .FinancialSourceId = item.FinancialSourceId
                                       .CodeNameFinancialSource = item.CodeNameFinancialSource

                                       .CodeNameRevenueType = item.CodeNameRevenueType

                                       '.InitialValue = 0
                                       '.DebitValueModification = 0
                                       '.CreditValueModification = 0
                                       '.ExecutedValue = 0
                                       '.TotalRecognition = .InitialValue - .DebitValueModification - .CreditValueModification
                                       ''.Balance = .TotalRecognition - .ExecutedValue
                                       .ValueBalance = item.Balance

                                       ListRecognitionDetail.Add(recognitionDetail)
                                   End With
                               End Sub)

            INDGcCategory.DataSource = Nothing
            INDGcCategory.DataSource = ListRecognitionDetail
            Mensaje(EeventViewerImages.Informacion) = "Detalles agregados correctamente."
        End If
    End Sub

    ''' <summary>
    ''' Metodos para asignar valores a la entidad Recognition
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With Recognition
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me._idOperativeUnit
            .BudgetaryValidityId = validityIdStandard
            .Document = Document
            .DocumentDate = DocumentDate
            .Observations = Observations
            .RecognitonType = RecognitionType
            .ThirdPartyId = ThirdPartyId
            .DependencyId = DependencyId
            .AutomaticCollection = AutomaticCollection
            .Applicant = Applicant
            .RecognitionDetail.Clear()
            If ListRecognitionDetail IsNot Nothing AndAlso ListRecognitionDetail.Count > 0 Then
                ListRecognitionDetail.FindAll(Function(item) item.InitialValue > 0 AndAlso (item.ChangeTracker.State = ObjectState.Added OrElse item.ChangeTracker.State = ObjectState.Modified)).ForEach(Sub(item)
                                                                                                                                                                                                              .RecognitionDetail.Add(item)
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
        Using Model As New MRecognition(MyTag)
            AsyncLoader(True)
            INDLcRecognition.BeginUpdate()
            Dim resultOperation = Await Model.GetRecognition(Code, 1, validityIdStandard)
            Recognition = resultOperation.ObjectEmbbeded
            If Not Recognition Is Nothing Then
                If Recognition.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(MyTag, Recognition.Id)
                    INDsleEntityPopUp.Properties.ReadOnly = True
                    INDsleValidityPopUp.Properties.ReadOnly = True
                    With Recognition
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        Code = .Code
                        Document = .Document
                        DocumentDate = .DocumentDate
                        Observations = .Observations
                        RecognitionType = .RecognitonType
                        ThirdPartyId = .ThirdPartyId
                        INDSleThirdParty.DisplayNullText = .NameThirdParty
                        DependencyId = .DependencyId
                        INDSleDependency.DisplayNullText = .NameDependency
                        AutomaticCollection = .AutomaticCollection
                        Applicant = .Applicant

                        ListRecognitionDetail = .RecognitionDetail.ToList

                        Me.BarraBotones.StatusRecord = .Status.ToString()
                        Me.BarraBotones.ControlHideStatus = True
                    End With
                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Recognition.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Recognition.Id}
                        Dim operation = Await Model.SaveBlockRecord(blockRecord)
                        blockRecord = operation.ObjectEmbbeded
                    Else
                        blockRecord = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    If Recognition.Status = 1 Then 'Registrado
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                    Else 'Confirmado o Anulado
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                    Me.BarraBotones.SetDocuments(Recognition.Id)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, Recognition.Id, 0, Recognition.Id)
                    AsyncLoader(False)
                    ActionsOnControls = True
                    INDGcCategory.DataSource = Nothing
                    INDGcCategory.DataSource = ListRecognitionDetail
                Else
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Me.NewRecognition()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Code = String.Empty
                        Deshacer()
                        INDBtnCode.Focus()
                    End If
                End If
            Else
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewRecognition()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Deshacer()
                    INDBtnCode.Focus()
                End If
            End If
        End Using
        INDLcRecognition.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo reconocimiento
    ''' </summary>
    Private Async Sub NewRecognition()
        Recognition = New Recognition
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope Is Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Code = String.Empty
                INDBtnCode.Focus()
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
    End Sub

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="codes">The code.</param>
    Private Sub PrepareToolbar(codes As String)
        Code = codes
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
        INDLcRecognition.BeginUpdate()
        ActionsOnControls = False
        BudgetaryValidityId = Nothing
        ValidityIdPopup = Nothing
        CleanControls()
        INDLcgPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLcgGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgGeneralInformation2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        BarraBotones.StatusRecordVisible = False
        INDsleBudgetEntity.Focus()
        INDLcRecognition.EndUpdate()
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INDLcRecognition.BeginUpdate()
        INDsleEntityPopUp.Properties.ReadOnly = False
        INDsleValidityPopUp.Properties.ReadOnly = False
        ActionsOnControls = False
        Code = String.Empty
        Document = String.Empty
        DocumentDate = GetDateServer()
        Observations = String.Empty
        RecognitionType = 1
        ThirdPartyId = Nothing
        INDSleThirdParty.DisplayNullText = String.Empty
        DependencyId = Nothing
        INDSleDependency.DisplayNullText = String.Empty
        AutomaticCollection = Nothing
        Applicant = String.Empty
        INDGcCategory.DataSource = Nothing
        ListNewBudget = Nothing
        ListRecognitionDetail = Nothing
        ListDeleteRecognitionDetail = Nothing
        INDTxeValue.Text = String.Empty
        Recognition = Nothing
        Me.BarraBotones.StatusRecord = -1
        BarraBotones.ControlHideStatus = False
        DeleteBlockedRecord()
        BarraBotones.CleanAuditBasic()
        INDLcRecognition.EndUpdate()
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRecognition.ActionsOnControls
        Set(value As Boolean)
            INDLcRecognition.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDDteDocumentDate.Enabled = value
            INDTxeDocument.Enabled = value
            INDMemObservation.Enabled = value
            INDGleRecognitionType.Enabled = value
            INDSleThirdParty.Enabled = value
            INDSleDependency.Enabled = value
            INDTxeValue.Enabled = value
            INDGleAutomaticCollection.Enabled = value
            INDSbAdd.Enabled = value
            INDGcCategory.Enabled = value
            INDLcRecognition.EndUpdate()
            If value Then
                INDDteDocumentDate.Focus()
            Else
                INDBtnCode.Focus()
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
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Recognition.Code), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Recognition.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Recognition.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Recognition.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Recognition.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup()
        INDLcgPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLcgGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLcgGeneralInformation2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.ControlHideStatus = False
        BarraBotones.PrepareToolbar(eAction.NewAndFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MRecognition(MyTag)
                Await Model.DeleteBlockRecord(blockRecord)
            End Using
            blockRecord = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Recognition IsNot Nothing AndAlso Me.Recognition.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Valida el detalle del reconocimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As String
        Dim listErrors As New StringBuilder
        If Not (ListRecognitionDetail IsNot Nothing AndAlso ListRecognitionDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de Reconocimiento para poder guardar.")
        End If
        If ListRecognitionDetail IsNot Nothing AndAlso ListRecognitionDetail.Count > 0 Then
            For Each item In ListRecognitionDetail
                If item.InitialValue = 0 Then
                    'MessageIndigo.Show("Debe digitar un valor a cada uno de los detalles de la Disponibilidad", "Disponibilidad", MessageType.Errores)
                    listErrors.AppendLine("El valor de cada uno de los detalles de la Disponibilidad no puede ser cero (0).")
                    viewBudget.Focus()
                    Exit For
                End If
                If item.InitialValue < 0 Then
                    'MessageIndigo.Show("El valor de ninguno de los detalles de la Disponibilidad puede ser inferior a cero (0)", "Disponibilidad", MessageType.Errores)
                    viewBudget.Focus()
                    listErrors.AppendLine("El valor de ninguno de los detalles de la Disponibilidad puede ser inferior a cero (0).")
                End If
            Next
        End If
        If validityIdStandard = 0 Then
            listErrors.AppendLine("Debe elegir una vigencia.")
        End If
        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim detail As RecognitionDetail = viewBudget.GetFocusedRow
        If detail.Id > 0 Then
            If ListDeleteRecognitionDetail Is Nothing Then
                ListDeleteRecognitionDetail = New List(Of Integer)
            End If
            detail.MarkAsDeleted()
            ListDeleteRecognitionDetail.Add(detail.Id)
        End If
        ListRecognitionDetail.Remove(detail)
        INDGcCategory.DataSource = Nothing
        INDGcCategory.DataSource = ListRecognitionDetail
    End Sub

    Private Async Function SearchThirdParty(ByVal nit As String) As Task(Of Domain.Entities.ThirdParty)
        Using ModelThird As New MThirdParty(MThirdParty.TAG)
            ThirdPartyTmp = Await ModelThird.GetThirdPartyAsync(nit)
            If ThirdPartyTmp IsNot Nothing AndAlso ThirdPartyTmp.Id > 0 Then
                INDSleDependency.Focus()
            Else
                Me.INDSleThirdParty.DisplayNullText = String.Empty
                Me.INDSleThirdParty.EditValue = Nothing
                Me.INDSleThirdParty.DisplayMember = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "El Tercero No Existe"
                INDSleThirdParty.Focus()
            End If
            Return ThirdPartyTmp
        End Using
    End Function

    Private Async Function SearchDependency(ByVal code As String) As Task(Of Domain.Entities.Dependency)
        Using ModelDepedency As New MBudgetDependency(MBudgetDependency.TAG)
            DependencyTmp = Await ModelDepedency.GetBudgetDependencyAsync(code, validityIdStandard)
            If DependencyTmp IsNot Nothing AndAlso DependencyTmp.Id > 0 Then
                INDMemObservation.Focus()
            Else
                Me.INDSleDependency.DisplayNullText = String.Empty
                Me.INDSleDependency.EditValue = Nothing
                Me.INDSleDependency.DisplayMember = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "La Dependencia No Existe"
                INDSleDependency.Focus()
            End If
            Return DependencyTmp
        End Using
    End Function

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' Búsqueda de registros
    ''' </summary>
    ''' <remarks></remarks>
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
            If Recognition IsNot Nothing AndAlso Recognition.Id > -1 Then
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using model As New MRecognition(MyTag)
                        AsyncLoader(True)
                        Recognition.MarkAsDeleted()
                        Dim result = Nothing 'Await model.DeleteBudgetDependency(Dependency)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            'SearchMode = False
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
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
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Recognition.Status <> 3 Then
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
            Using model As New MRecognition(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveRecognition(Recognition, ListDeleteRecognitionDetail)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If Result.ObjectEmbbeded.Status = 1 Then 'Guardar o Actualizar
                        If Recognition.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                                Me.DicSequense(Me._sequense.BudgetSequenceDetail(0).Id).RemoveAt(0)
                            End If
                            If Me._sequense.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf Recognition.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    ElseIf Result.ObjectEmbbeded.Status = 2 Then 'Confirmar
                        If Result.ObjectEmbbeded.AutomaticCollection = True Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmRecognitionAutomaticCollection", "Budget"), Result.ObjectEmbbeded.Code, Result.Message)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                        End If
                    ElseIf Result.ObjectEmbbeded.Status = 3 Then 'Anular
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                    End If
                    Me.Recognition = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, Recognition.Id, 0, Recognition.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, Recognition.Id, 0, Recognition.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, Recognition.Id, 0, Recognition.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, Recognition.Id, 0, Recognition.Id)
                    End Select
                    'SearchMode = False
                    Me.Deshacer()
                Else
                    If Result.StateResult = False And Result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                    If Recognition.Id > 0 Then
                        Dim resullt2 = Await model.GetRecognition(Code, 1, BudgetaryValidityId)
                        Recognition = resullt2.ObjectEmbbeded
                    Else
                        Recognition = New Recognition
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

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
            NewRecognition()
        End If
    End Sub

    ''' <summary>
    ''' Método para abrir búsqueda
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
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "ThirdPartyId.NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo With {.Caption = "Documento", .FieldName = "Document", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.16)},
                              New ColumnInfo With {.Caption = "Valor Inicial", .FieldName = "InitialValue", .ColumnFormat = "C0", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo With {.Caption = "Valor Total", .FieldName = "TotalRecognition", .ColumnFormat = "C0", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {validityIdStandard}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListRecognition
            .FormParent = Me
            .ShowSearch()
        End With
        'SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de búsqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBtnCode.Text = ReturnValue
        If INDBtnCode.Text <> String.Empty Then
            LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Libera memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        controlerChanged = Nothing
        statusValidity = Nothing
        validityIdStandard = Nothing
        budgetEntityIdStandard = Nothing
        validityCodeName = Nothing
        budgetEntityCodeName = Nothing
        Recognition = Nothing
        presenter = Nothing
        blockRecord = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
        ListRecognitionDetail = Nothing
        ListDeleteRecognitionDetail = Nothing
        ListNewBudget = Nothing
        ThirdPartyTmp = Nothing
        DependencyTmp = Nothing
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
    Private Sub FrmRecognition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRecognition, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        presenter = New PRecognition(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        '****************************** 
        Me.INDSleThirdParty.FuncQueryOnKeyEnterPressed = AddressOf Me.SearchThirdParty
        Me.INDSleThirdParty.View.OptionsView.ShowGroupPanel = False
        Me.INDSleDependency.FuncQueryOnKeyEnterPressed = AddressOf Me.SearchDependency
        Me.INDSleDependency.View.OptionsView.ShowGroupPanel = False
        IndigoGridControl1.RefreshGrid(INDGcCategory)

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
            BudgetaryValidityId = Nothing
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
        If BudgetaryValidityId <> 0 Then
            validityIdStandard = BudgetaryValidityId
            validityCodeName = INDsleValidity.Text

            controlerChanged = True
            ValidityIdPopup = BudgetaryValidityId
            controlerChanged = False

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

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleThirdParty_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDSleThirdParty.EditValueChanged
        If Me.INDSleThirdParty.EditValue IsNot Nothing AndAlso Me.INDSleThirdParty.EditValue > 0 Then
            Using ModelThird As New MThirdParty(MThirdParty.TAG)
                Me.ThirdPartyTmp = Await ModelThird.GetThirdPartyById(Me.INDSleThirdParty.EditValue)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de dependencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleDependency_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDSleDependency.EditValueChanged
        If Me.INDSleDependency.EditValue IsNot Nothing AndAlso Me.INDSleDependency.EditValue > 0 Then
            Using ModelDependency As New MBudgetDependency(MBudgetDependency.TAG)
                Me.DependencyTmp = Await ModelDependency.GetBudgetDependencyById(Me.INDSleDependency.EditValue)
                Applicant = DependencyTmp.ResponsibleDescription
            End Using
        End If
    End Sub

#End Region

#Region "FrmClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRecognition_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If validityIdStandard = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una vigencia."
                Exit Sub
            End If
            If Me._sequense Is Nothing OrElse Me._sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDBtnCode.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDBtnCode.Text.Trim()) Then
                    Me.NewRecognition()
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

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Datasource Is Nothing Then
            presenter.LoadListThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de dependenciasS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDependency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDependency.QueryPopUp
        If INDSleDependency.Datasource Is Nothing Then
            presenter.LoadListDependency(validityIdStandard)
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
    Private Sub INDgvValidity_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvValidity.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDStatus.Name Then
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
    Private Sub FrmRecognition_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleRecognitionType.Properties.DataSource = FillingRecognitionType
        Me.INDGleRecognitionType.EditValue = 1
        INDsleBudgetEntity.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
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

#Region "OpenFormButtonClick"
    ''' <summary>
    ''' Evento que abre el frm de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDSleThirdParty.OpenFormButtonClick
        OpenForm(532, Nothing, True)
        presenter.LoadListThirdParty()
    End Sub
    ''' <summary>
    ''' Evento que abre el frm de dependecias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleDependency_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDSleDependency.OpenFormButtonClick
        OpenForm(204, Nothing, True)
        presenter.LoadListDependency(validityIdStandard)
    End Sub

#End Region

#Region "CellValueChanged"

    ''' <summary>
    ''' Suma de rubros para el valor total de reconocimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewBudget_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles viewBudget.CellValueChanged
        If e.Column.Name = "INDValue" Then
            INDTxeValue.Text = ListRecognitionDetail.Sum(Function(x) x.InitialValue)
        End If
    End Sub

#End Region

#Region "DataSourceChanged"

    ''' <summary>
    ''' Suma de rubros para el valor total de reconocimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGcCategory_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcCategory.DataSourceChanged

        INDTxeValue.Text = ListRecognitionDetail.Sum(Function(x) x.InitialValue)

    End Sub

#End Region

#Region "ShownEditor"

    ''' <summary>
    ''' Asignación de icono de información cuando el valor supera el saldo en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewBudget_ShownEditor(sender As Object, e As EventArgs) Handles viewBudget.ShownEditor

        'Dim row As RecognitionDetail = (viewBudget.GetRow(viewBudget.FocusedRowHandle))

        'If row IsNot Nothing Then

        '    Dim view As GridView = CType(sender, GridView)
        '    Dim saldo As GridColumn = view.Columns("InitialValue")

        '    If row.ValueBalance < row.InitialValue Then
        '        view.SetColumnError(saldo, "El valor supera el saldo de presupuesto. El excedente se contabilizara como ingreso no aforado.", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information)
        '    End If

        'End If


        'Dim row As RecognitionDetail = (viewBudget.GetRow(viewBudget.FocusedRowHandle))

        'If row IsNot Nothing Then
        '    Dim view As GridView = CType(sender, GridView)
        '    Dim saldo As GridColumn = View.Columns("InitialValue")

        '    If row.ValueBalance < row.InitialValue Then
        '        View.SetColumnError(saldo, "El valor supera el saldo de presupuesto. El excedente se contabilizara como ingreso no aforado.", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information)
        '    Else
        '        View.ClearColumnErrors()
        '    End If

        'End If

    End Sub

#End Region

#Region "KeyDown Grid"
    ''' <summary>
    ''' Metodo para desplazarse en la vista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewBudget_KeyDown(sender As Object, e As KeyEventArgs) Handles viewBudget.KeyDown

        Dim k As Integer = e.KeyCode
        Select Case k
            Case 9
                e.SuppressKeyPress = True
            Case 13
                If viewBudget.IsLastRow Then
                    viewBudget.MoveFirst()
                Else
                    viewBudget.MoveNext()
                End If
        End Select
    End Sub

#End Region

#Region "ValidatingEditor"
    ''' <summary>
    ''' Valida el editor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewBudget_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles viewBudget.ValidatingEditor

        Dim view As GridView = sender
        Dim valor As Decimal
        If view.FocusedColumn.FieldName = "InitialValue" Then

            valor = Convert.ToDecimal(e.Value)

            Select Case valor
                Case Is < 0
                    e.Valid = False
                    view.ClearColumnErrors()
                Case Is = 0
                    e.Valid = False
                    view.ClearColumnErrors()
            End Select

        End If


        Dim row As RecognitionDetail = (viewBudget.GetRow(viewBudget.FocusedRowHandle))

        If row IsNot Nothing Then
            Dim saldo As GridColumn = view.Columns("InitialValue")

            If row.ValueBalance < valor Then
                view.SetColumnError(saldo, "El valor supera el saldo de presupuesto. El excedente se contabilizara como ingreso no aforado.", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information)
            Else
                view.ClearColumnErrors()
            End If

        End If

    End Sub

#End Region

#Region "InvalidValueException"
    ''' <summary>
    ''' Metodo para invalidar el valor de las excepciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewBudget_InvalidValueException(sender As Object, e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles viewBudget.InvalidValueException

        Dim view As GridView = sender
        Dim valor As Decimal
        If view.FocusedColumn.FieldName = "InitialValue" Then

            valor = Convert.ToDecimal(e.Value)

            Select Case valor

                Case Is < 0
                    e.ExceptionMode = ExceptionMode.DisplayError
                    e.WindowCaption = "Error en el valor ingresado"
                    e.ErrorText = "El valor no puede ser inferior al saldo del presupuesto."
                Case Is = 0
                    e.ExceptionMode = ExceptionMode.DisplayError
                    e.WindowCaption = "Error en el valor ingresado"
                    e.ErrorText = "El valor no puede ser igual a cero (0)."

            End Select

        End If

    End Sub

#End Region

#End Region

#Region "Buttons Bar"

    ''' <summary>
    ''' Evento actualizarConfirmar de la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Recognition.Status = 2
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
            Recognition.Status = 2
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
            Recognition.Status = 3
            varImp = 4
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()

                PopUpAnnulmentReason.BudgetaryValidityId = BudgetaryValidityId

                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    Recognition.AnnulmentConceptId = PopUpAnnulmentReason.ReversalReasonId
                    Recognition.AnnulmentDescription = PopUpAnnulmentReason.ReversalDescription
                End If
            End Using
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
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
        Recognition.Status = 1
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
        Recognition.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Recognition.Id, 0, Recognition.Id)
    End Sub

    ''' <summary>
    ''' Cambiar unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
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
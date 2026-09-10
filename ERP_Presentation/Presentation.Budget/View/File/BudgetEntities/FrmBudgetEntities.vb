'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
'LastModified      : Carlos Mario Arias Rubiano
'DateLastModified  : 15/07/2015
'Description       : Verificar que este funcionando el formulario
'                    y arreglarlo, ademas de cambiar diseño.
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Common
Imports Presentation.Controls

#End Region

''' <summary>
''' Contiene la vista de el frontal fuentes de Entidades de presupuesto
''' </summary>
''' <remarks></remarks>
Public Class FrmBudgetEntities
    Implements IBudgetEntities

#Region "Variables"
    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' DataTable
    ''' </summary>
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim _Indigo As SessionValues

    ''' <summary>
    ''' Variable para controlar la entidad presente en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim BudgetEntities As BudgetaryEntity

    ''' <summary>
    ''' Contiene el modelo del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim model As MBudgetEntities

    ''' <summary>
    ''' Contiene el presentador de formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PBudgetEntities

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Controla el pop up que se abrira con el drop down button
    ''' </summary>
    ''' <remarks></remarks>
    Dim newPopupControlContainer As DevExpress.XtraBars.PopupControlContainer

    ''' <summary>
    ''' Controla la vigencia seleccionada en la regilla para editar
    ''' </summary>
    ''' <remarks></remarks>
    Dim BudgetaryValidity As BudgetaryValidity

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Representa al listado de vigencias presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListBudgetaryValidity As List(Of BudgetaryValidity)

    ''' <summary>
    ''' Representa al listado de eliminados de vigencias presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteBudgetaryValidity As List(Of BudgetaryValidity)

    ''' <summary>
    ''' Permite saber si se esta agregando o editando en la rejilla
    ''' (True=Edita, False=Agrega)
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeEdit As Boolean = False

    ''' <summary>
    ''' Representa el listado de earnings
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListEarningsParamters As List(Of BudgetAccountParameters)

    ''' <summary>
    ''' Representa el listado de expenses
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListExpensesParameters As List(Of BudgetAccountParameters)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece control de fecha y consecutivos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatesAndConsecutivesControl As Boolean? Implements IBudgetEntities.DatesAndConsecutivesControl
        Get
            Return INDrgDatesAndConsecutivesControl.EditValue
        End Get
        Set(value As Boolean?)
            INDrgDatesAndConsecutivesControl.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el mes proceso ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EarningsProcessMonth As Integer Implements IBudgetEntities.EarningsProcessMonth
        Get
            Return INDsleEarningsProcessMonth.EditValue
        End Get
        Set(value As Integer)
            INDsleEarningsProcessMonth.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el med proceso gasto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExpenseProcessMonth As Integer Implements IBudgetEntities.ExpenseProcessMonth
        Get
            Return INDsleExpenseProcessMonth.EditValue
        End Get
        Set(value As Integer)
            INDsleExpenseProcessMonth.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece manejo o control de PAC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PACControl As Boolean? Implements IBudgetEntities.PACControl
        Get
            Return INDrgPACControl.EditValue
        End Get
        Set(value As Boolean?)
            INDrgPACControl.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero de la resolucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResolutionNumber As String Implements IBudgetEntities.ResolutionNumber
        Get
            Return INDtxtResolutionNumber.EditValue
        End Get
        Set(value As String)
            INDtxtResolutionNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResolutionValue As Decimal Implements IBudgetEntities.ResolutionValue
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatusValidity As Integer Implements IBudgetEntities.StatusValidity
        Get
            Return INDsleValidityStatus.EditValue
        End Get
        Set(value As Integer)
            INDsleValidityStatus.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el año
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property YearValidity As Integer Implements IBudgetEntities.YearValidity
        Get
            Return INDseYear.EditValue
        End Get
        Set(value As Integer)
            INDseYear.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del jefe de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetBossId As Integer? Implements IBudgetEntities.BudgetBossId
        Get
            Return INDsleBudgetBoss.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetBoss.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del jefe de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetBossXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IBudgetEntities.BudgetBossXPO
        Get
            Return INDsleBudgetBoss.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBudgetBoss.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del jefe financiero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FinancialBossId As Integer? Implements IBudgetEntities.FinancialBossId
        Get
            Return INDsleFinancialBoss.EditValue
        End Get
        Set(value As Integer?)
            INDsleFinancialBoss.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del jefe financiero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FinancialBossXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IBudgetEntities.FinancialBossXPO
        Get
            Return INDsleFinancialBoss.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFinancialBoss.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del representante legal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalRepresentativeId As Integer? Implements IBudgetEntities.LegalRepresentativeId
        Get
            Return INDsleLegalRepresentative.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalRepresentative.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del representante legal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalRepresentativeXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IBudgetEntities.LegalRepresentativeXPO
        Get
            Return INDsleLegalRepresentative.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleLegalRepresentative.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer Implements IBudgetEntities.BudgetaryValidityId

    Public Property BudgetaryValidityIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBudgetEntities.BudgetaryValidityIdXpo

    ''' <summary>
    ''' Obtiene o establece si la entidad es empresa social ESE
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ESE As Boolean Implements IBudgetEntities.ESE
        Get
            Return INDGleSector.EditValue
        End Get
        Set(value As Boolean)
            INDGleSector.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name1 As String Implements IBudgetEntities.Name
        Get
            Return INDmeName.EditValue
        End Get
        Set(value As String)
            INDmeName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si maneja reconocimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Recognition As Boolean Implements IBudgetEntities.Recognition
        Get
            Return INDrgAcknowledgments.EditValue
        End Get
        Set(value As Boolean)
            INDrgAcknowledgments.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la region
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Region1 As String Implements IBudgetEntities.Region
        Get
            Return INDbteRegion.EditValue
        End Get
        Set(value As String)
            INDbteRegion.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la seccion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Section As String Implements IBudgetEntities.Section
        Get
            Return INDtxtSection.EditValue
        End Get
        Set(value As String)
            INDtxtSection.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el status
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IBudgetEntities.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer Implements IBudgetEntities.ThirdPartyId
        Get
            Return INDSleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDSleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Unit As String Implements IBudgetEntities.Unit
        Get
            Return INDtxtUnit.EditValue
        End Get
        Set(value As String)
            INDtxtUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que devuelve el código completo concatenado de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property GetCode_BudgetEntities As String
        Get
            If INDtxtSection.Text.ToString = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemSection.Text)
                INDtxtSection.Focus()
                Return ""
            End If
            If INDtxtUnit.Text.ToString = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemUnit.Text)
                INDtxtUnit.Focus()
                Return ""
            End If
            If INDbteRegion.Text.ToString = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemRegional.Text)
                INDbteRegion.Focus()
                Return ""
            End If

            Return INDtxtSection.Text.ToString.Trim() & INDtxtUnit.Text.ToString.Trim() & INDbteRegion.Text.ToString.Trim()

        End Get
    End Property

    ''' <summary>
    ''' Control de vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValidityCtr As CtrValidity
        Get
            Return Me.CtrValidity1
        End Get
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBudgetEntities.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o extablece el tercero
    ''' </summary>
    ''' <value>
    ''' The thyrd party xpo.
    ''' </value>
    Public Property ThirdPartyXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IBudgetEntities.ThirdPartyXPO
        Get
            Return CType(INDSleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

    Private _FillingSector As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Propiedad para capturar el tipo de etiqueta
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingLabelSector As List(Of Tuple(Of Byte, String))
        Get
            If _FillingSector Is Nothing Then
                _FillingSector = New List(Of Tuple(Of Byte, String))
                _FillingSector.Add(New Tuple(Of Byte, String)(0, "OTRO"))
                _FillingSector.Add(New Tuple(Of Byte, String)(1, "SALUD Y PROTECCIÓN SOCIAL"))
            End If
            Return _FillingSector
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Propiedad para controlar la accion que se hace sobre los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyBudgetEntities.BeginUpdate()
            INDtxtSection.Enabled = Not value
            INDtxtUnit.Enabled = Not value
            INDbteRegion.Enabled = Not value
            INDmeName.Enabled = value
            INDSleThirdParty.Enabled = value
            INDrgAcknowledgments.Enabled = value
            INDGleSector.Enabled = value
            INDgcValidity.Enabled = value
            INDpceAddValidity.Enabled = value
            INDlyBudgetEntities.EndUpdate()
            If value = True Then
                INDmeName.Focus()
            Else
                INDtxtSection.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.BudgetEntities IsNot Nothing AndAlso Me.BudgetEntities.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                'Me.INDbteRegion.Text = Me.IdEntity.Trim()
                Await Me.LoadControls(Me.IdEntity.Trim())
            End If
        Else 'Realiza la consulta normal
            'Me.INDbteRegion.Text = Me.IdEntity.Trim()
            Await Me.LoadControls(Me.IdEntity.Trim())
        End If
        Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Funcion para retornar el ultimo año de la vigencia registrada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getLastYear() As Integer
        Dim listValidity As Domain.Entities.TrackableCollection(Of BudgetaryValidity) = INDgcValidity.DataSource
        If listValidity IsNot Nothing AndAlso listValidity.Count > 0 Then
            Dim lastValidity As BudgetaryValidity = listValidity.Item(listValidity.Count - 1)
            Return lastValidity.Year
        End If
        Return 0
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls(Optional Code As String = "") As Task
        Using Model As New MBudgetEntities
            AsyncLoader(True)
            INDlyBudgetEntities.BeginUpdate()
            If Code <> "" Then
                BudgetEntities = Await Model.GetBudgetInstitutionAsync(Code)
            Else
                BudgetEntities = Await Model.GetBudgetInstitutionAsync(GetCode_BudgetEntities)
            End If

            If Not BudgetEntities Is Nothing Then
                If BudgetEntities.Id > 0 Then
                    Me.BarraBotones.StatusRecordVisible = True
                    Dim result = Await Model.GetBlockRecord(Me.Tag, BudgetEntities.Id)
                    With BudgetEntities
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        Section = .Section
                        Unit = .Unit
                        Region1 = .Region
                        Name1 = .Name
                        ThirdPartyId = .ThirdPartyId
                        INDSleThirdParty.Properties.NullText = .ThirdPartyDescription
                        Recognition = .Recognition
                        INDGleSector.EditValue = .ESE
                        Status = .Status

                        ListBudgetaryValidity = .BudgetaryValidity1.ToList
                        INDgcValidity.DataSource = Nothing
                        INDgcValidity.DataSource = ListBudgetaryValidity
                    End With

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.BudgetEntities.Code)
                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(BudgetEntities.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = BudgetEntities.Id}
                        Dim operation = Await Model.SaveBlockRecord(blockRecord)
                        blockRecord = operation.ObjectEmbbeded
                    Else
                        blockRecord = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If

                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, BudgetEntities.Id, 0, BudgetEntities.Id)

                    AsyncLoader(False)
                    ActionsOnControls = True
                Else
                    AsyncLoader(False)
                    ActionsOnControls = True
                    BudgetEntities = New BudgetaryEntity
                    BudgetEntities.BudgetaryValidity1 = New Domain.Entities.TrackableCollection(Of BudgetaryValidity)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    BarraBotones.StatusRecordVisible = True
                    Status = True
                End If
            Else
                AsyncLoader(False)
                ActionsOnControls = True
                BudgetEntities = New BudgetaryEntity
                BudgetEntities.BudgetaryValidity1 = New Domain.Entities.TrackableCollection(Of BudgetaryValidity)
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                BarraBotones.StatusRecordVisible = True
                Status = True
            End If
        End Using
        INDlyBudgetEntities.EndUpdate()
    End Function

    ''' <summary>
    ''' Metodos para asignar valores a la entidad financial source
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssigningValues()
        With BudgetEntities
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Section = Section.Trim
            .Unit = Unit.Trim
            .Region = Region1.Trim
            .Code = GetCode_BudgetEntities
            .Name = Name1.Trim
            .ThirdPartyId = ThirdPartyId
            .Recognition = Recognition
            .ESE = INDGleSector.EditValue
            .Status = Status

            If ListBudgetaryValidity IsNot Nothing AndAlso ListBudgetaryValidity.Count > 0 Then
                For Each item In ListBudgetaryValidity
                    .BudgetaryValidity1.Add(item)
                Next
            End If
            If ListDeleteBudgetaryValidity IsNot Nothing AndAlso ListDeleteBudgetaryValidity.Count > 0 Then
                For Each item In ListDeleteBudgetaryValidity
                    .BudgetaryValidity1.Add(item)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If

        End With
    End Sub

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyBudgetEntities.BeginUpdate()
        CleanControlsPopup()
        Section = String.Empty
        Unit = String.Empty
        Region1 = String.Empty
        Name1 = String.Empty
        ThirdPartyId = Nothing
        INDSleThirdParty.Properties.NullText = String.Empty
        Recognition = Nothing
        INDGleSector.Text = String.Empty
        INDGleSector.Properties.ReadOnly = False
        Status = True
        ListBudgetaryValidity = Nothing
        ListDeleteBudgetaryValidity = Nothing
        INDgcValidity.DataSource = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        BarraBotones.CleanAuditBasic()
        BudgetEntities = Nothing
        BudgetaryValidity = Nothing
        ActionsOnControls = False
        INDlyBudgetEntities.EndUpdate()
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmBudgetEntitiesMetaData, Eform.InfoMetaData), Me.BudgetEntities.Code, Me.BudgetEntities.Name, INDSleThirdParty.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.BudgetEntities.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmBudgetEntitiesMetaDataTitle, Eform.InfoMetaData), Me.BudgetEntities.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmBudgetEntitiesMetaData, Eform.InfoMetaData), Me.BudgetEntities.Code, Me.BudgetEntities.Name, INDSleThirdParty.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmBudgetEntitiesMetaDataTitle, Eform.InfoMetaData), Me.BudgetEntities.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBudgetEntities
                Await model.DeleteBlockRecord(blockRecord)
                blockRecord = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Dispara el show dialog de las vigencias para poder agregar o editar una vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowDialogAddValidity(Optional validity As BudgetaryValidity = Nothing)
        'Dim listValidity As Domain.Entities.TrackableCollection(Of BudgetaryValidity) = INDgcValidity.DataSource
        'If validity Is Nothing Then
        '    With Me.ValidityCtr
        '        .BudgetaryValidity = Nothing
        '        If listValidity.Count > 0 Then
        '            .SetYearMaximum = listValidity.Item(listValidity.Count - 1).Year
        '        End If
        '        .SetFocusYear()
        '        AddHandler .ClickAccept, Sub(a, s)
        '                                     If listValidity Is Nothing Then
        '                                         listValidity = New Domain.Entities.TrackableCollection(Of BudgetaryValidity)
        '                                         listValidity.Add(.BudgetaryValidity)
        '                                     Else
        '                                         listValidity.Add(.BudgetaryValidity)
        '                                     End If
        '                                     INDgcValidity.DataSource = listValidity
        '                                     INDgcValidity.RefreshDataSource()
        '                                 End Sub
        '    End With

        'Else
        '    With Me.ValidityCtr
        '        .BudgetaryValidity = validity
        '        If listValidity.Count > 0 Then
        '            .SetYearMaximum = listValidity.Item(listValidity.Count - 1).Year
        '        End If
        '        .SetFocusYear()
        '        AddHandler .ClickAccept, Sub(a, s)
        '                                     If .BudgetaryValidity.ChangeTracker.State = ObjectState.Deleted AndAlso Me.BudgetEntities.ChangeTracker.State = ObjectState.Unchanged Then
        '                                         Me.BudgetEntities.ChangeTracker.State = ObjectState.Modified
        '                                     End If
        '                                     INDgcValidity.RefreshDataSource()
        '                                 End Sub
        '    End With
        'End If
        'INDDDBAddValidity.ShowDropDown()
    End Sub

    ''' <summary>
    ''' Metodo que inicializa las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        INDsleEarningsProcessMonth.Properties.DataSource = BudgetHelper.Months
        INDsleExpenseProcessMonth.Properties.DataSource = BudgetHelper.Months
        INDsleValidityStatus.Properties.DataSource = BudgetHelper.ValidityStatus
        StatusValidity = 1
    End Sub

    ''' <summary>
    ''' Metodo que agrega validity a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddValidity()
        Dim errors As String = ValidateFieldsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If INDsleValidityStatus.EditValue = 2 Then
            If Not (MessageIndigo.Show(obtenerRecurso(ComunesPreguntaConfirmar), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If
        If INDsleValidityStatus.EditValue = 3 Then
            If Not (MessageIndigo.Show(obtenerRecurso(ComunesPreguntaCerrarReg), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If

        If ListBudgetaryValidity Is Nothing Then
            ListBudgetaryValidity = New List(Of BudgetaryValidity)
        Else
            Dim ListYear As List(Of Integer)
            If modeEdit = False Then
                ListYear = (From l In ListBudgetaryValidity Select l.Year).ToList
            Else
                ListYear = (From l In ListBudgetaryValidity Where l.Year <> BudgetaryValidity.Year Select l.Year).ToList
            End If
            Dim cont = (From l In ListYear Where l = YearValidity Select l).Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La vigencia del año " + YearValidity.ToString + " ya existe en la lista."
                Exit Sub
            End If
        End If
        CreateEnitytBudgetaryValidity()
        If modeEdit = False Then
            ListBudgetaryValidity.Add(BudgetaryValidity)
            INDgcValidity.DataSource = Nothing
            INDgcValidity.DataSource = ListBudgetaryValidity
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente a la rejilla."
        Else
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
            INDgcValidity.RefreshDataSource()
        End If
        modeEdit = False
        CleanControlsPopup()
        INDseYear.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que edita el registro de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditValidity()
        modeEdit = True
        BudgetaryValidity = INDgvValidity.GetFocusedRow
        With BudgetaryValidity
            LegalRepresentativeId = .LegalRepresentativeId
            INDsleLegalRepresentative.Properties.NullText = .LegalRepresentativeDescription

            BudgetBossId = .ChiefBudgetOfficerId
            INDsleBudgetBoss.Properties.NullText = .BudgetBossDescription

            FinancialBossId = .ChiefFinancialOfficerId
            INDsleFinancialBoss.Properties.NullText = .FinancialBossDescription

            YearValidity = .Year
            EarningsProcessMonth = .IncomeMonth
            ExpenseProcessMonth = .ExpenseMonth
            ResolutionNumber = .ResolutionNumber
            ResolutionValue = .ResolutionValue
            StatusValidity = .Status
            PACControl = .PACControl
            DatesAndConsecutivesControl = .ControlDateConsecutive
            '*********Consecutivos ingreso********'
            ListEarningsParamters.Find(Function(x) x.Item1 = 207).Item4 = .ConsecutiveModPTOIncome()
            ListEarningsParamters.Find(Function(x) x.Item1 = 209).Item4 = .ConsecutiveTrasPTOIncome
            ListEarningsParamters.Find(Function(x) x.Item1 = 211).Item4 = .ConsecutiveModPACIncome
            ListEarningsParamters.Find(Function(x) x.Item1 = 212).Item4 = .ConsecutiveTrasPACIncome
            ListEarningsParamters.Find(Function(x) x.Item1 = 213).Item4 = .ConsecutiveRecognition
            ListEarningsParamters.Find(Function(x) x.Item1 = 214).Item4 = .ConsecutiveModRecognition
            ListEarningsParamters.Find(Function(x) x.Item1 = 215).Item4 = .ConsecutiveCollection
            ListEarningsParamters.Find(Function(x) x.Item1 = 216).Item4 = .ConsecutiveModCollection
            '*********Consecutivos gasto********'
            ListExpensesParameters.Find(Function(x) x.Item1 = 207).Item4 = .ConsecutiveModPTOExpense
            ListExpensesParameters.Find(Function(x) x.Item1 = 209).Item4 = .ConsecutiveTrasPTOExpense
            ListExpensesParameters.Find(Function(x) x.Item1 = 211).Item4 = .ConsecutiveModPACExpense
            ListExpensesParameters.Find(Function(x) x.Item1 = 212).Item4 = .ConsecutiveTrasPACExpense
            ListExpensesParameters.Find(Function(x) x.Item1 = 228).Item4 = .ConsecutiveCDP
            ListExpensesParameters.Find(Function(x) x.Item1 = 229).Item4 = .ConsecutiveModCDP
            ListExpensesParameters.Find(Function(x) x.Item1 = 231).Item4 = .ConsecutiveRP
            ListExpensesParameters.Find(Function(x) x.Item1 = 232).Item4 = .ConsecutiveModRP
            ListExpensesParameters.Find(Function(x) x.Item1 = 234).Item4 = .ConsecutiveLiabilities
            ListExpensesParameters.Find(Function(x) x.Item1 = 235).Item4 = .ConsecutiveModLiabilities
            ListExpensesParameters.Find(Function(x) x.Item1 = 237).Item4 = .ConsecutiveODP
            ListExpensesParameters.Find(Function(x) x.Item1 = 230).Item4 = .ConsecutiveExtendedCDP
            ListExpensesParameters.Find(Function(x) x.Item1 = 236).Item4 = .ConsecutiveResourceRelease
            ListExpensesParameters.Find(Function(x) x.Item1 = 238).Item4 = .ConsecutiveReinstatement
            ListExpensesParameters.Find(Function(x) x.Item1 = 224).Item4 = .ConsecutiveLiftingPTO
        End With
        INDgcEarning.RefreshDataSource()
        INDgcExpense.RefreshDataSource()
        INDpceAddValidity.ShowPopup()
        INDseYear.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que crea la entidad para guardar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateEnitytBudgetaryValidity()
        If BudgetaryValidity Is Nothing AndAlso modeEdit = False Then
            BudgetaryValidity = New BudgetaryValidity With {.Status = True}
        End If
        With BudgetaryValidity
            .LegalRepresentativeId = LegalRepresentativeId
            .LegalRepresentativeDescription = INDsleLegalRepresentative.Text

            .ChiefBudgetOfficerId = BudgetBossId
            .BudgetBossDescription = INDsleBudgetBoss.Text

            .ChiefFinancialOfficerId = FinancialBossId
            .FinancialBossDescription = INDsleFinancialBoss.Text

            .Year = YearValidity
            .IncomeMonth = EarningsProcessMonth
            .ExpenseMonth = ExpenseProcessMonth
            .ResolutionNumber = ResolutionNumber
            .ResolutionValue = ResolutionValue
            .Status = StatusValidity
            .PACControl = PACControl
            .ControlDateConsecutive = DatesAndConsecutivesControl
            '*********Consecutivos ingreso********'
            .ConsecutiveModPTOIncome = ListEarningsParamters.Find(Function(x) x.Item1 = 207).Item4
            .ConsecutiveTrasPTOIncome = ListEarningsParamters.Find(Function(x) x.Item1 = 209).Item4
            .ConsecutiveModPACIncome = ListEarningsParamters.Find(Function(x) x.Item1 = 211).Item4
            .ConsecutiveTrasPACIncome = ListEarningsParamters.Find(Function(x) x.Item1 = 212).Item4
            .ConsecutiveRecognition = ListEarningsParamters.Find(Function(x) x.Item1 = 213).Item4
            .ConsecutiveModRecognition = ListEarningsParamters.Find(Function(x) x.Item1 = 214).Item4
            .ConsecutiveCollection = ListEarningsParamters.Find(Function(x) x.Item1 = 215).Item4
            .ConsecutiveModCollection = ListEarningsParamters.Find(Function(x) x.Item1 = 216).Item4
            '*********Consecutivos gasto********'
            .ConsecutiveModPTOExpense = ListExpensesParameters.Find(Function(x) x.Item1 = 207).Item4
            .ConsecutiveTrasPTOExpense = ListExpensesParameters.Find(Function(x) x.Item1 = 209).Item4
            .ConsecutiveModPACExpense = ListExpensesParameters.Find(Function(x) x.Item1 = 211).Item4
            .ConsecutiveTrasPACExpense = ListExpensesParameters.Find(Function(x) x.Item1 = 212).Item4
            .ConsecutiveCDP = ListExpensesParameters.Find(Function(x) x.Item1 = 228).Item4
            .ConsecutiveModCDP = ListExpensesParameters.Find(Function(x) x.Item1 = 229).Item4
            .ConsecutiveRP = ListExpensesParameters.Find(Function(x) x.Item1 = 231).Item4
            .ConsecutiveModRP = ListExpensesParameters.Find(Function(x) x.Item1 = 232).Item4
            .ConsecutiveLiabilities = ListExpensesParameters.Find(Function(x) x.Item1 = 234).Item4
            .ConsecutiveModLiabilities = ListExpensesParameters.Find(Function(x) x.Item1 = 235).Item4
            .ConsecutiveODP = ListExpensesParameters.Find(Function(x) x.Item1 = 237).Item4
            .ConsecutiveExtendedCDP = ListExpensesParameters.Find(Function(x) x.Item1 = 230).Item4
            .ConsecutiveResourceRelease = ListExpensesParameters.Find(Function(x) x.Item1 = 236).Item4
            .ConsecutiveReinstatement = ListExpensesParameters.Find(Function(x) x.Item1 = 238).Item4
            .ConsecutiveLiftingPTO = ListExpensesParameters.Find(Function(x) x.Item1 = 224).Item4
        End With
    End Sub

    ''' <summary>
    ''' Funcion Para Validar Controles antes de enviar a guardar una vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFieldsPopup() As String
        Dim listErrors As New StringBuilder
        If String.IsNullOrEmpty(INDseYear.Text) Or INDseYear.Text.Length <> 4 Then
            If INDseYear.EditValue < 0 Then
                listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemYear.Text))
            Else
                listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemYear.Text))
            End If
        End If

        If INDsleEarningsProcessMonth.EditValue Is Nothing Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemEarningsProcessMonth.Text))
        End If

        If INDsleExpenseProcessMonth.EditValue Is Nothing Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemExpenseProcessMonth.Text))
        End If

        If String.IsNullOrEmpty(INDtxtResolutionNumber.Text) Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemResolutionNumber.Text))
        End If

        If String.IsNullOrEmpty(INDtxtValue.Text) Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemValue.Text))
        End If

        If INDsleValidityStatus.EditValue Is Nothing Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemValidityStatus.Text))
        End If

        If INDrgPACControl.EditValue Is Nothing Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemPACControl.Text))
        End If

        If INDrgDatesAndConsecutivesControl.EditValue Is Nothing Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemDatesAndConsecutivesControl.Text))
        End If

        If INDsleLegalRepresentative.EditValue Is Nothing Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemLegalRepresentative.Text))
        End If

        If INDsleBudgetBoss.EditValue Is Nothing Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemBudgetBoss.Text))
        End If

        If INDsleFinancialBoss.EditValue Is Nothing Then
            listErrors.AppendLine(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDsleFinancialBoss.Text))
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        BudgetaryValidity = Nothing
        YearValidity = Nothing
        YearValidity = DateTime.Now().Year
        EarningsProcessMonth = Nothing
        ExpenseProcessMonth = Nothing
        ResolutionNumber = Nothing
        ResolutionValue = Nothing
        PACControl = Nothing
        DatesAndConsecutivesControl = Nothing

        LegalRepresentativeId = Nothing
        INDsleLegalRepresentative.Properties.NullText = Nothing

        BudgetBossId = Nothing
        INDsleBudgetBoss.Properties.NullText = String.Empty

        FinancialBossId = Nothing
        INDsleFinancialBoss.Properties.NullText = String.Empty

        ListEarningsParamters = Nothing
        ListEarningsParamters = BudgetHelper.EarningsParameters.FindAll(Function(x) x.Item1 > 0).ToList
        INDgcEarning.DataSource = Nothing
        INDgcEarning.DataSource = ListEarningsParamters

        ListExpensesParameters = Nothing
        ListExpensesParameters = BudgetHelper.ExpenseParameters.FindAll(Function(x) x.Item1 > 0).ToList
        INDgcExpense.DataSource = Nothing
        INDgcExpense.DataSource = ListExpensesParameters
    End Sub

    ''' <summary>
    ''' Elimina una vigencia de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteValidity()
        Dim detail As BudgetaryValidity = CType(INDgvValidity.GetFocusedRow, BudgetaryValidity)
        If detail.Id > 0 Then
            If ListDeleteBudgetaryValidity Is Nothing Then
                ListDeleteBudgetaryValidity = New List(Of BudgetaryValidity)
            End If
            detail.MarkAsDeleted()
            ListDeleteBudgetaryValidity.Add(detail)
        End If
        ListBudgetaryValidity.Remove(detail)
        INDgcValidity.DataSource = Nothing
        INDgcValidity.DataSource = ListBudgetaryValidity
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        Try
            If Not String.IsNullOrEmpty(BudgetEntities.Code) Then
                Using model As New MBudgetEntities()
                    AsyncLoader(True)
                    Dim state As Boolean = Not BudgetEntities.Status
                    Dim Result = Await model.ChangeState(BudgetEntities.Code, state)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        BudgetEntities = Result.ObjectEmbbeded
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        _Indigo = Nothing
        BudgetEntities = Nothing
        model = Nothing
        presenter = Nothing
        blockRecord = Nothing
        newPopupControlContainer = Nothing
        BudgetaryValidity = Nothing
        _idOperativeUnit = Nothing
        ListBudgetaryValidity = Nothing
        ListDeleteBudgetaryValidity = Nothing
        modeEdit = Nothing
        ListEarningsParamters = Nothing
        ListExpensesParameters = Nothing
    End Sub



    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetEntities_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBudgetEntities, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        presenter = New PBudgetEntities(Me)
        presenter.LoadDefinitionLayout()
        '******************************
        InitializeTuples()
        IndigoGridView1.MoreInfoColunmns(INDgvValidity)
        IndigoGridControl1.RefreshGrid(INDgcValidity)
        IndigoGridControl1.RefreshGrid(INDgcEarning)
        IndigoGridControl1.RefreshGrid(INDgcExpense)
        INDGleSector.Properties.DataSource = FillingLabelSector
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvValidity, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvValidity.Columns
            If col.Name = "colActions" Then
                col.Width = 80
            End If
        Next
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que abre el form de busqueda desde el click del boton buscar del control codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnRegion_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteRegion.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el mas para abrir el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                presenter.InitializeThirdParty()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el mas para abrir el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFinancialBoss_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFinancialBoss.ButtonClick, INDsleLegalRepresentative.ButtonClick, INDsleBudgetBoss.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("532", Nothing, True)
            presenter.InitializeAllThirdParty()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Captura la tecla enter, para buscar un regitro por codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteRegion_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteRegion.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(GetCode_BudgetEntities) Then
                Await LoadControls()
                If INDbteRegion.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                INDbteRegion.Enabled = False
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o F4 sobre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddValidity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAddValidity.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceAddValidity.ShowPopup()
            INDseYear.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmBudgetEntities_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Click en el boton de agregar nueva vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAddValidity_Click(sender As Object, e As EventArgs)
        ShowDialogAddValidity()
    End Sub

    ''' <summary>
    ''' Click editar de la rejilla vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRepLinkEdit1_Click(sender As Object, e As EventArgs)
        Dim validity As BudgetaryValidity = INDgvValidity.GetRow(INDgvValidity.FocusedRowHandle)
        If validity IsNot Nothing Then
            BudgetaryValidity = validity
            Me.CtrValidity1.InizaliteControler(BudgetaryValidity)
            INDpceAddValidity.Properties.Buttons(0).Caption = "Editar Vigencia"
        End If
        Me.INDpceAddValidity.ShowPopup()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de aceptar del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddValidity_Click(sender As Object, e As EventArgs) Handles INDbtnAddValidity.Click
        AddValidity()
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Evento para colocar nombres en datos numericos de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvValidity_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvValidity.CustomColumnDisplayText
        If e.Column.Name = INDColExpenseProcessMonth.Name Or e.Column.Name = INDColEarningMonth.Name Then
            If e.Value IsNot Nothing Then
                e.DisplayText = BudgetHelper.Months.Find(Function(x) x.Item1 = e.Value).Item2
            End If
        End If
        If e.Column.Name = INDColStatus.Name Then
            If e.Value IsNot Nothing Then
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
        End If
    End Sub

#End Region

#Region "EventClickPopup"

    ''' <summary>
    ''' ocurre cuando se presiona el boton aceptar en el control de usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrValidity1_ClickAccept(sender As Object, e As EventArgs) Handles CtrValidity1.ClickAccept
        clickAccept_Validity()

    End Sub

    ''' <summary>
    ''' ocurre cuando se presiona el boton cancelar en el control de usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrValidity1_ClickCancel(sender As Object, e As EventArgs) Handles CtrValidity1.ClickCancel
        clickCancel_Validity()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerra el form de agragar un tercero en el control de vigencia
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrValidity1_CloseFormThirdParty(sender As Object, e As EventArgs) Handles CtrValidity1.CloseFormThirdParty
        Me.INDpceAddValidity.ShowPopup()
    End Sub

    ''' <summary>
    ''' Evento Click del control vigencias
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Public Sub clickAccept_Validity()
        Dim listValidity As Domain.Entities.TrackableCollection(Of BudgetaryValidity) = INDgcValidity.DataSource
        If BudgetaryValidity Is Nothing Then
            If listValidity Is Nothing Then
                listValidity = New Domain.Entities.TrackableCollection(Of BudgetaryValidity)
                listValidity.Add(Me.ValidityCtr.BudgetaryValidity)
            Else
                If Me.ValidityCtr.BudgetaryValidity.Year > listValidity.Max(Function(x) x.Year) Then
                    listValidity.Add(Me.ValidityCtr.BudgetaryValidity)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AñoVigenciaInvalido", "Budget")
                    Exit Sub
                End If
            End If
            INDgcValidity.DataSource = listValidity
            INDgcValidity.RefreshDataSource()
        Else
            If Me.ValidityCtr.BudgetaryValidity.ChangeTracker.State = ObjectState.Deleted AndAlso Me.BudgetEntities.ChangeTracker.State = ObjectState.Unchanged Then
                Me.BudgetEntities.ChangeTracker.State = ObjectState.Modified
            End If
            INDgcValidity.RefreshDataSource()
        End If
        BudgetaryValidity = Nothing
        Me.ValidityCtr.InizaliteControler(BudgetaryValidity)
        Me.INDpceAddValidity.CancelPopup()
        INDpceAddValidity.Properties.Buttons(0).Caption = "Nueva Vigencia"
    End Sub

    ''' <summary>
    ''' Evento cancel del control vigencias
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Public Sub clickCancel_Validity()
        Me.INDpceAddValidity.CancelPopup()
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' evento que se dispara al presionar 
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag
            Case "Remove"
                DeleteValidity()
            Case "Edit"
                EditValidity()
        End Select
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.CloseUpEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddValidity_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAddValidity.CloseUp
        If modeEdit Then
            CleanControlsPopup()
        End If
    End Sub

#End Region

#Region "Activated"

    Private Sub FrmBudgetEntities_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase .Shown 
        If INDtxtSection.Enabled = True Then
            INDtxtSection.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If ThirdPartyXPO Is Nothing Then
            presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para desplegar el control de representante legal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalRepresentative_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleLegalRepresentative.QueryPopUp
        If LegalRepresentativeXPO Is Nothing Then
            presenter.InitializeLegalRepresentative()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para desplegar el control de jefe de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetBoss_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetBoss.QueryPopUp
        If BudgetBossXPO Is Nothing Then
            presenter.InitializeBudgetBoss()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para desplegar el control de jefe financiero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFinancialBoss_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFinancialBoss.QueryPopUp
        If FinancialBossXPO Is Nothing Then
            presenter.InitializeFinancialBoss()
        End If
    End Sub

    ''' <summary>
    ''' Evento que dispara para desplegar el popcontainercontrol de la vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddValidity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddValidity.QueryPopUp
        If ListBudgetaryValidity Is Nothing OrElse ListBudgetaryValidity.Count <= 1 Then
            INDsleValidityStatus.Properties.ReadOnly = False
        Else
            INDsleValidityStatus.Properties.ReadOnly = True
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la rejilla de earnings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRepSpinEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRepSpinEdit.EditValueChanging
        Dim item As BudgetAccountParameters = INDgvEarning.GetFocusedRow()
        If item IsNot Nothing Then
            Dim control As DevExpress.XtraEditors.SpinEdit = DirectCast(sender, DevExpress.XtraEditors.SpinEdit)
            Select Case item.Item1
                Case 207
                    ListEarningsParamters.Find(Function(x) x.Item1 = 207).Item4 = control.EditValue
                Case 209
                    ListEarningsParamters.Find(Function(x) x.Item1 = 209).Item4 = control.EditValue
                Case 211
                    ListEarningsParamters.Find(Function(x) x.Item1 = 211).Item4 = control.EditValue
                Case 212
                    ListEarningsParamters.Find(Function(x) x.Item1 = 212).Item4 = control.EditValue
                Case 213
                    ListEarningsParamters.Find(Function(x) x.Item1 = 213).Item4 = control.EditValue
                Case 214
                    ListEarningsParamters.Find(Function(x) x.Item1 = 214).Item4 = control.EditValue
                Case 215
                    ListEarningsParamters.Find(Function(x) x.Item1 = 215).Item4 = control.EditValue
                Case 216
                    ListEarningsParamters.Find(Function(x) x.Item1 = 216).Item4 = control.EditValue
            End Select
            INDgcEarning.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la rejilla de expenses
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRepConsecutive_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRepConsecutive.EditValueChanging
        Dim item As BudgetAccountParameters = INDgvExpense.GetFocusedRow()
        If item IsNot Nothing Then
            Dim control As DevExpress.XtraEditors.SpinEdit = DirectCast(sender, DevExpress.XtraEditors.SpinEdit)
            Select Case item.Item1
                Case 207
                    ListExpensesParameters.Find(Function(x) x.Item1 = 207).Item4 = control.EditValue
                Case 209
                    ListExpensesParameters.Find(Function(x) x.Item1 = 209).Item4 = control.EditValue
                Case 211
                    ListExpensesParameters.Find(Function(x) x.Item1 = 211).Item4 = control.EditValue
                Case 212
                    ListExpensesParameters.Find(Function(x) x.Item1 = 212).Item4 = control.EditValue
                Case 228
                    ListExpensesParameters.Find(Function(x) x.Item1 = 228).Item4 = control.EditValue
                Case 229
                    ListExpensesParameters.Find(Function(x) x.Item1 = 229).Item4 = control.EditValue
                Case 231
                    ListExpensesParameters.Find(Function(x) x.Item1 = 231).Item4 = control.EditValue
                Case 232
                    ListExpensesParameters.Find(Function(x) x.Item1 = 232).Item4 = control.EditValue
                Case 234
                    ListExpensesParameters.Find(Function(x) x.Item1 = 234).Item4 = control.EditValue
                Case 235
                    ListExpensesParameters.Find(Function(x) x.Item1 = 235).Item4 = control.EditValue
                Case 237
                    ListExpensesParameters.Find(Function(x) x.Item1 = 237).Item4 = control.EditValue
                Case 230
                    ListExpensesParameters.Find(Function(x) x.Item1 = 230).Item4 = control.EditValue
                Case 236
                    ListExpensesParameters.Find(Function(x) x.Item1 = 236).Item4 = control.EditValue
                Case 238
                    ListExpensesParameters.Find(Function(x) x.Item1 = 238).Item4 = control.EditValue
                Case 224
                    ListExpensesParameters.Find(Function(x) x.Item1 = 224).Item4 = control.EditValue
            End Select
            INDgcExpense.RefreshDataSource()
        End If
    End Sub

#End Region

#End Region

#Region "ICrud Base"
    ''' <summary>
    ''' MEtodo para buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Metodo para deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Me.CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If BudgetEntities IsNot Nothing Then
            If BudgetEntities.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Dim result As ActionResult
                    Try
                        AsyncLoader(True)
                        Using modelDelete As New MBudgetEntities
                            result = Await modelDelete.DeleteBudgetInstitutionAsync(BudgetEntities)
                        End Using

                        If result.StateResult = True Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            SearchMode = False
                            Deshacer()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        Else
                            If result.MessageResult.Count > 0 Then
                                If result.MessageResult.Item(0).ToString = "c-0000" Then
                                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                                ElseIf result.MessageResult.Item(0).ToString = "-999" Then
                                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorConcurrencia)
                                Else
                                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                                End If
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                            AsyncLoader(False)
                        End If
                    Catch ex As Exception
                        Throw ex
                        AsyncLoader(False)
                    End Try
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneUnConceptoGeneral, ConceptosGenerales)
            End If
        Else
        End If
    End Sub

    ''' <summary>
    ''' metodo para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using modelSave As New MBudgetEntities
                AsyncLoader(True)
                Dim Result = Await modelSave.SaveBudgetBudgetInstitutionAsync(BudgetEntities)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If BudgetEntities.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    ElseIf BudgetEntities.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.BudgetEntities = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    SearchMode = False
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Obsoleto
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
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
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
            .ListaColumnas = {New ColumnInfo With {.Caption = "Codigo", .FieldName = "Code"}, New ColumnInfo With {.Caption = "Descripción", .FieldName = "Name"}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBudgetEntitiesXPIFS
            CType(.GridViewBusquedas, DevExpress.XtraGrid.Views.Grid.GridView).OptionsDetail.EnableMasterViewMode = False
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        If ReturnValue <> String.Empty Then
            LoadControls(ReturnValue)
            INDbteRegion.Enabled = False
            If INDbteRegion.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub
#End Region

#Region "BarButtons Events"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, BudgetEntities.Id, 0, BudgetEntities.Id)
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub
#End Region

End Class
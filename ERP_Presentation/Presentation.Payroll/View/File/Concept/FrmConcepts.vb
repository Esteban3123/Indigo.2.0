'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 15-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Presentation.Payroll.MVP
Imports Domain.Base.Entities
Imports DevExpress.Data
Imports DevExpress.XtraEditors.Design
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports Domain.Glosas.Entities
Imports DevExpress.Data.Filtering.Helpers
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports System.Drawing
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region

Public Class FrmConcepts
    Implements IConcept
    Implements IDataColumnInfo

#Region "Variables"
    ''' <summary>
    ''' Variable para saber el modo de busqueda del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModoBusqueda As Boolean = False
#End Region

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene el nuevo listado de variables del formulario editor de expresiones
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly m_columns As New List(Of IDataColumnInfo)()

    ''' <summary>
    ''' Variable para el formulario de edicion de expresiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ExpressionEditForm As ExpressionEditorForm

    ''' <summary>
    ''' Variable que establece el diccionario de descripciones de las variables
    ''' </summary>
    Dim descriptions As Dictionary(Of String, String)

    ''' <summary>
    ''' VAriable que contiene los fields nulls para la personalizacion de campos
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que establece un listado de los grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupList As List(Of Group)

    ''' <summary>
    ''' Variable que establece un listado de los grupos de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Dim ConceptGroupList As List(Of ConceptGroup)

    ''' <summary>
    ''' variable para los conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Dim concept As Concept

    ''' <summary>
    ''' Variable para los grupos de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Dim conceptGroup As ConceptGroup

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MConcept(Me.Tag)

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PConcept
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    Dim ListAccountingStructure As List(Of AccountingStructure)

    Dim ConceptAccountinStructureTrack As New TrackableCollection(Of ConceptAccountingStructure)

    Dim FlagManualConceptValue As Boolean = False

    ''' <summary>
    ''' listado del detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListConceptAccountingStructure As List(Of ConceptAccountingStructure)
    ''' <summary>
    ''' listado del detalle de la remision para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListConceptAccountingStructureDelete As List(Of ConceptAccountingStructure)
    ''' <summary>
    ''' entidad del detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim ConceptAccountingStructure As ConceptAccountingStructure

    ''' <summary>
    ''' Representa la entidad de parametros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _generalLedgerSettings As Domain.Entities.GeneralLedgerSettings



#End Region
    ''' <summary>
    ''' Propiedad del estado del concepto
    ''' </summary>
    Public Property Status As Boolean

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

#Region "Properties And Load"


    ''' <summary>
    ''' Establece si la clase de concepto permite diligenciar una formula
    ''' </summary>
    ''' <remarks></remarks>
    Dim _allowConceptFormulate As Boolean
    Public Property AllowConceptFormulate As Boolean
        Get
            Return _allowConceptFormulate
        End Get
        Set(value As Boolean)
            _allowConceptFormulate = value
        End Set
    End Property
    ''' <summary>
    ''' Establece id del concepto nomina electronica
    ''' </summary>
    ''' <returns></returns>
    Public Property IdElectronicPayrollConcepts As Integer?
        Get
            Return INDslConceptPayrollElectronic.EditValue
        End Get
        Set(value As Integer?)
            INDslConceptPayrollElectronic.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que establece el data source de los grupos de concepto
    ''' </summary>
    Public WriteOnly Property DatasourceConceptGroup As TrackableCollection(Of ConceptGroup) Implements IConcept.DatasourceConceptGroup

        Set(value As TrackableCollection(Of ConceptGroup))
            INDGcConceptGroup.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que devuelve la entidad concepto
    ''' </summary>
    Public ReadOnly Property ConceptObject As Concept Implements IConcept.ConceptObject
        Get
            Return concept
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que establece el estado de los controles del formulario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls
        Set(value)
            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDRgType.Enabled = value
            INDMeFormulates.Enabled = value
            INDGcConceptGroup.Enabled = value
            INDCbeClass.Enabled = value
            INDChkAffectIBC.Enabled = value
            INDChkAffectIBCARP.Enabled = value
            INDChkAffectIBCCompensationFund.Enabled = value
            INDChkAffectIBCHealth.Enabled = value
            INDChkAffectIBCICBF.Enabled = value
            INDChkAffectIBCPension.Enabled = value
            INDChkAffectIBCRTF.Enabled = value
            INDChkAffectIBCSENA.Enabled = value
            INDChkAffectIBCSeverance.Enabled = value
            INDChkAffectVacation.Enabled = value
            INDChkAffectIncentivePayment.Enabled = value
            INDChkAffectRetroactive.Enabled = value
            INDChkAffectLimit40.Enabled = value
            INDgcCuentasContables.Enabled = value
            INDslConceptPayrollElectronic.Enabled = value
        End Set
    End Property

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ModoBusqueda = Nothing
        ExpressionEditForm = Nothing
        descriptions = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        GroupList = Nothing
        ConceptGroupList = Nothing
        concept = Nothing
        conceptGroup = Nothing
        Model = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        record = Nothing
        ListAccountingStructure = Nothing
        ConceptAccountinStructureTrack = Nothing
        FlagManualConceptValue = Nothing
        ListConceptAccountingStructure = Nothing
        ListConceptAccountingStructureDelete = Nothing
        ConceptAccountingStructure = Nothing
    End Sub


    Private Sub FrmConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollConcepts.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PConcept(Me)
        LoadStatus()
        Deshacer()
        FormulateFieldsLoad()
        Presenter.Initializes()
        GetSettingAccount()
        AddActionsColumnsAdministrative()
    End Sub


    Private Async Sub GetSettingAccount()
        _generalLedgerSettings = Await Presenter.GetGeneralLedgerSettings(BarraBotones.OperatingUnitValue)
        If _generalLedgerSettings IsNot Nothing AndAlso Not _generalLedgerSettings.HandlesElectronicPayroll Then
            INDLyIConceptPayrollElectronic.HideControl(True)
            INDLyIConceptPayrollElectronic.Enabled = False
            INDslConceptPayrollElectronic.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.concept IsNot Nothing AndAlso Me.concept.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub


#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        'Dim listStates As New List(Of StatusRecord)()
        'listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        'listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        'Me.BarraBotones.States = listStates

        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}

    End Sub

    ''' <summary>
    ''' Metodo para los check all de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        ''Cuando la fila en accion es la primera  (Funcionalidad deshabilitada)
        'If e.RowHandle = 0 Then
        '    Dim i As Integer
        '    For i = 0 To GridView1.DataRowCount - 1
        '        If i > 0 Then
        '            'Valida si selecciono la columna "aplica", para permitir seleccionar o quitar todos
        '            If e.Column.FieldName.Equals("Apply") Then
        '                GridView1.SetRowCellValue(i, e.Column.FieldName, e.Value)
        '            Else
        '                If e.Value = True Then
        '                    If GridView1.GetRowCellValue(i, "Apply") = True Then
        '                        GridView1.SetRowCellValue(i, e.Column.FieldName, e.Value)
        '                    End If
        '                Else
        '                    GridView1.SetRowCellValue(i, e.Column.FieldName, e.Value)
        '                End If
        '            End If
        '        End If
        '    Next

        'End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()

        With concept
            .Code = INDBteCode.Text
            .Name = INDTxtName.Text
            .State = Me.BarraBotones.StatusRecord
            .ConceptType = INDRgType.EditValue
            .Formulates = INDMeFormulates.Text
            .ConceptClass = INDCbeClass.EditValue
            .IdAdjustmentConcept = Nothing
            .AffectIBC = INDChkAffectIBC.EditValue
            .AffectIBCARP = INDChkAffectIBCARP.EditValue
            .AffectIBCCompensationFund = INDChkAffectIBCCompensationFund.EditValue
            .AffectIBCHealth = INDChkAffectIBCHealth.EditValue
            .AffectIBCICBF = INDChkAffectIBCICBF.EditValue
            .AffectIBCPension = INDChkAffectIBCPension.EditValue
            .AffectIBCRTF = INDChkAffectIBCRTF.EditValue
            .AffectIBCSENA = INDChkAffectIBCSENA.EditValue
            .AffectIBCSeverance = INDChkAffectIBCSeverance.EditValue
            .AffectIBCVacation = INDChkAffectVacation.EditValue
            .AffectIBCIncentivePayment = INDChkAffectIncentivePayment.EditValue
            .AffectRetroactive = INDChkAffectRetroactive.EditValue
            .AffectLimit40Law1393 = INDChkAffectLimit40.EditValue
            .IdElectronicPayrollConcepts = IdElectronicPayrollConcepts

            If ListConceptAccountingStructure IsNot Nothing Then
                For Each item In ListConceptAccountingStructure
                    .ConceptAccountingStructure.Add(item)
                Next
                If ListConceptAccountingStructureDelete IsNot Nothing Then
                    For Each item In ListConceptAccountingStructure
                        .ConceptAccountingStructure.Add(item)
                    Next
                End If
            End If


        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = True
        AsyncLoader(True)
        Using Model As New MConcept(MyBase.Tag)
            concept = Await Model.GetConceptAsync(INDBteCode.Text)
        End Using
        If concept IsNot Nothing Then
            If concept.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, concept.Id)
                With concept
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), concept.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), concept.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), concept.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), concept.ModificationDate)
                    INDBteCode.Text = .Code
                    INDTxtName.Text = .Name
                    INDRgType.EditValue = .ConceptType
                    INDCbeClass.EditValue = .ConceptClass
                    INDMeFormulates.Text = .Formulates
                    Status = .State
                    INDChkAffectIBC.EditValue = .AffectIBC
                    INDChkAffectIBCARP.EditValue = .AffectIBCARP
                    INDChkAffectIBCCompensationFund.EditValue = .AffectIBCCompensationFund
                    INDChkAffectIBCHealth.EditValue = .AffectIBCHealth
                    INDChkAffectIBCICBF.EditValue = .AffectIBCICBF
                    INDChkAffectIBCPension.EditValue = .AffectIBCPension
                    INDChkAffectIBCRTF.EditValue = .AffectIBCRTF
                    INDChkAffectIBCSENA.EditValue = .AffectIBCSENA
                    INDChkAffectIBCSeverance.EditValue = .AffectIBCSeverance
                    INDChkAffectVacation.EditValue = .AffectIBCVacation
                    INDChkAffectIncentivePayment.EditValue = .AffectIBCIncentivePayment
                    INDChkAffectRetroactive.EditValue = .AffectRetroactive
                    INDChkAffectLimit40.EditValue = .AffectLimit40Law1393
                    IdElectronicPayrollConcepts = .IdElectronicPayrollConcepts
                    ListConceptAccountingStructure = .ConceptAccountingStructure.ToList()
                    INDgcCuentasContables.DataSource = ListConceptAccountingStructure
                End With

                If result.Id = 0 Then

                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = concept.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If

                Me.BarraBotones.SetDocuments(concept.Id, Me.Tag.ToString(), Nothing, GetType(Concept).Name)

            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        Else
            concept = New Concept With {.State = True}
            INDTxtName.Focus()
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        Await Presenter.Load_ConceptsGroup()
        AsyncLoader(False)
        ActionsOnControls = True
    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()

        INDLyConcept.BeginUpdate()
        AllowConceptFormulate = False
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDBteCode.Focus()
        INDBteCode.Text = String.Empty
        INDTxtName.Text = String.Empty
        INDRgType.SelectedIndex = -1
        INDMeFormulates.EditValue = String.Empty
        INDCbeClass.EditValue = Nothing
        INDChkAffectIBC.EditValue = False
        INDChkAffectIBCARP.EditValue = False
        INDChkAffectIBCCompensationFund.EditValue = False
        INDChkAffectIBCHealth.EditValue = False
        INDChkAffectIBCICBF.EditValue = False
        INDChkAffectIBCPension.EditValue = False
        INDChkAffectIBCRTF.EditValue = False
        INDChkAffectIBCSENA.EditValue = False
        INDChkAffectIBCSeverance.EditValue = False
        INDChkAffectVacation.EditValue = False
        INDChkAffectIncentivePayment.EditValue = False
        INDChkAffectRetroactive.EditValue = False
        INDChkAffectLimit40.EditValue = False
        INDgcCuentasContables.DataSource = Nothing
        ListConceptAccountingStructure = Nothing
        IdElectronicPayrollConcepts = Nothing
        concept = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        INDLyIConceptPayrollElectronic.HideControl(False)
        ActionsOnControls = False
        INDBteCode.Focus()
        INDLyConcept.EndUpdate()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBteCode.Text = String.Empty Then
            INDBteCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDMeFormulates.Text = String.Empty Then
            INDMeFormulates.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDTxtName.Text = String.Empty Then
            INDTxtName.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDRgType.SelectedIndex = -1 Then
            INDRgType.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDCbeClass.EditValue = Nothing Then
            INDCbeClass.Focus()
            ValidateControls = False
            Exit Function
        End If



    End Function

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento para consultar el concepto en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBteDepCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If Not String.IsNullOrEmpty(INDBteCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                Await LoadControls()
                If INDBteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
                'INDBteCode.Enabled = False
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' Evento enter de el memo edit de las formulas, que abre el formulario editor de expresiones
    ''' </summary>
    Private Sub INDMeFormulates_Enter(sender As Object, e As EventArgs) Handles INDMeFormulates.Enter
        If INDCbeClass.EditValue IsNot Nothing AndAlso INDCbeClass.EditValue <> "" Then

            If AllowConceptFormulate = True Then
                FlagManualConceptValue = False
            Else
                FlagManualConceptValue = True
            End If
            FormulateFieldsLoad()

            ExpressionEditForm = New UnboundColumnExpressionEditorForm(Me, Nothing)
            AddHandler CType(ExpressionEditForm.Controls.Item(4), ListBoxControl).SelectedValueChanged, AddressOf selectedChangue
            ExpressionEditForm.Controls.Item(0).Text = INDMeFormulates.Text
            ExpressionEditForm.StartPosition = FormStartPosition.CenterParent

            If ExpressionEditForm.ShowDialog(Me) = DialogResult.OK Then
                INDMeFormulates.Text = ExpressionEditForm.Expression
            End If

        Else
            'Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLyItemClass.Text)
            INDCbeClass.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el item seleccionado de la lista de campos, para establecer la correcta descripción!
    ''' </summary>
    Private Sub selectedChangue(sender As Object, e As EventArgs)
        Dim lista As ListBoxControl = CType(sender, ListBoxControl)
        If lista.ItemCount > 0 Then
            Dim item As String = lista.SelectedItem.ToString().Replace("[", "").Replace("]", "")
            If descriptions.ContainsKey(item) Then
                ExpressionEditForm.Controls.Item(6).Text = descriptions(item)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que carga las variables de nómina para la construccion de formulas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FormulateFieldsLoad()

        descriptions = New Dictionary(Of String, String)
        descriptions.Add("Salario Mínimo", "Salario mínimo establecido para el grupo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Auxilio Transporte", "Auxilio de transporte establecido para el grupo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("% Salud Empleado", "Porcentaje de salud del empleado establecido para el grupo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("% Salud Patrono", "Porcentaje de salud del patrono establecido para el grupo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("% Pensión Empleado", "Porcentaje de pensión del empleado establecido para el grupo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("% Pensión Patrono", "Porcentaje de pensión del patrono establecido para el grupo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("% SENA", "Porcentaje SENA" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("% ICBF", "Porcentaje ICBF" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("% Caja", "Porcentaje caja de compensación" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Aporte Voluntario Salud", "Valor aportes voluntarios de salud" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Aporte Voluntario Pension", "Valor aportes voluntarios de pensión" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Sueldo Contrato", "Salario establecido en el contrato" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Tarifa ARP", "Tarifa ARP" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Horas Laboradas", "Número de horas laboradas en el periodo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Incapacidad", "Días de incapacidad en el periodo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Nómina", "Días de nomina" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Pensión", "Valor Base de Pensión en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Salud", "Valor Base de Salud en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Salud Patrono", "Valor Base de Salud Patrono en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Salud Patrono Integral", "Valor Base de Salud Patrono en el Mes para Salarios Integrales " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Pensión Patrono", "Valor Base de Pensión Patrono en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Pensión Patrono Integral", "Valor Base de Pensión Patrono en el Mes para Salarios Integrales " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Riesgos Profesionales", "Valor Base de Riesgos Profesionales en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Riesgos Profesionales Integral", "Valor Base de Riesgos Profesionales en el Mes para Salarios Integrales" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Aporte Parafiscal Caja de Compensación", "Valor Base de Caja de Compensación en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Aporte Parafiscal Caja de Compensación Integral", "Valor Base de Caja de Compensación en el Mes para Salarios Integrales " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Aporte Parafiscal ICBF", "Valor Base Aporte Parafiscal ICBF en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Aporte Parafiscal ICBF Integral", "Valor Base Aporte Parafiscal ICBF en el Mes para Salarios Integrales " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Aporte SENA Integral", "Valor Base SENA en el Mes para Salarios Integrales " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Aporte SENA", "Valor Base SENA en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Fondo Seguridad Pensional Integral", "Valor Base Seguridad Pensional Integral en el Mes " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Ajuste Vacaciones", "Valor del Ajuste de Vacaciones " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Paga Vacaciones", "Paga Vacaciones " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Fecha Liquidacion Nomina", "Fecha Liquidacion Nomina " & vbNewLine & "Tipo de Dato : " & GetType(DateTime).ToString())
        descriptions.Add("Fecha Contratacion", "Fecha Contratacion " & vbNewLine & "Tipo de Dato : " & GetType(DateTime).ToString())
        descriptions.Add("Bonificacion Año Servicio", "Bonificacion Año Servicio " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Total Dias Vacaciones", "Total Dias Vacaciones " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Bonificacion Año Servicio Retroactivo", "Bonificacion Año Servicio Retroactivo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Embargo", "Valor Embargo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Porcentaje Embargo", "Porcentaje Embargo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Codigo Centro Trabajo", "Codigo Centro Trabajo " & vbNewLine & "Tipo de Dato : " & GetType(String).ToString())
        descriptions.Add("Horas Ajustes Festivos", "Número de horas laboradas en días Festivos" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Sueldo Basico Diario", "Sueldo Basico Diario" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Horas Ajuste Dominical", "Horas Ajuste Dominical. Variable para Compensar las Horas del Domingo y/o festivo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Horas Ajuste Extras Diurnas", "Horas Ajuste Extras Diurnas. Variable para Compensar las Horas del Domingio y/o festivo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Horas Ajuste Extras Nocturnas", "Horas Ajuste Extras Nocturnas. Variable para Compensar las Horas del Domingio y/o festivo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Auxilio Transporte", "Variable para los Días de Auxilio de Transporte. Está ligada a la norma." & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Ajustes Dominical Evento]", "Horas Ajustes Dominical Evento" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Ajuste Extras Diurnas Festivas Evento", "Horas Ajuste Extras Diurnas Festivas Evento" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Ajuste Extras Nocturnas Festivas Evento", "Horas Ajuste Extras Nocturnas Festivas Evento" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Ajuste Extras Nocturnas Evento", "Horas Ajuste Extras Nocturnas Evento" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Ajuste Extras Diurnas Evento", "Horas Ajuste Extras Diurnas Evento" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("[Quincena]", "Quincena: 1 - 1era Quincena, 2 - 2da Quincena" & vbNewLine & "Tipo de Dato : " & GetType(Byte).ToString())
        descriptions.Add("Día Familia", "Día de la Familia: " & vbNewLine & "Tipo de Dato : " & GetType(Byte).ToString())
        descriptions.Add("Días Licencia Luto", "Días de licencia de luto" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())

        m_columns.Add(New ColumnInfo("fieldVARSALARIM", "Salario Mínimo", GetType(Decimal), m_columns, "Salario Minimo"))
        m_columns.Add(New ColumnInfo("fieldVARTRANSPO", "Auxilio Transporte", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPORSALE", "% Salud Empleado", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPORSALP", "% Salud Patrono", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPORPENE", "% Pensión Empleado", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPORPENP", "% Pensión Patrono", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPORSENA", "% SENA", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPORICBF", "% ICBF", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPORCAJA", "% Caja", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALVSAL", "Aporte Voluntario Salud", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALVPEN", "Aporte Voluntario Pension", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVSUELDO", "Sueldo Contrato", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARTARIARP", "Tarifa ARP", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARNUMHORA", "Horas Laboradas", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIASINC", "Días Incapacidad", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIASNOM", "Días Nómina", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASPEN", "Base Pensión", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASPEN", "Base Salud", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASESP", "Base Salud Patrono", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASESI", "Base Salud Patrono Integral", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASEPP", "Base Pensión Patrono", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASEPI", "Base Pensión Patrono Integral", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASARP", "Base Riesgos Profesionales", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASARI", "Base Riesgos Profesionales Integral", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASCDC", "Base Aporte Parafiscal Caja de Compensación", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASCDI", "Base Aporte Parafiscal Caja de Compensación Integral", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASPIC", "Base Aporte Parafiscal ICBF", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASPII", "Base Aporte Parafiscal ICBF Integral", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASSEN", "Base Aporte SENA Integral", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASENA", "Base Aporte SENA", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVBASEFS", "Base Fondo Seguridad Pensional Integral", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVAJUVAC", "Ajuste Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPAGAVAC", "Paga Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARFECLIQN", "Fecha Liquidacion Nomina", GetType(DateTime), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARFECINIC", "Fecha Contratacion", GetType(DateTime), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALBONI", "Bonificacion Año Servicio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALDIAV", "Total Dias Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALBONR", "Bonificacion Año Servicio Retroactivo", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALEMBA", "Valor Embargo", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPOREMBA", "Porcentaje Embargo", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARCODWORC", "Codigo Centro Trabajo", GetType(String), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHORAJFE", "Horas Ajustes Festivos", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARSUELBAD", "Sueldo Basico Diario", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHOAJDOM", "Horas Ajuste Dominical", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHOAJEXD", "Horas Ajuste Extras Diurnas", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHOAJEXN", "Horas Ajuste Extras Nocturnas", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIATRAN", "Dias Auxilio Transporte", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIATRAN", "Dias Auxilio Transporte", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHADEVEN", "Horas Ajustes Dominical Evento", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHAEDFEV", "Horas Ajuste Extras Diurnas Festivas Evento", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHAENFEV", "Horas Ajuste Extras Nocturnas Festivas Evento", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHAENEVE", "Horas Ajuste Extras Nocturnas Evento", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHAEDEVE", "Horas Ajuste Extras Diurnas Evento", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARQUINCEN", "Quincena", GetType(Byte), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAFAMI", "Día Familia", GetType(Byte), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIASLUT", "Días Licencia Luto", GetType(Decimal), m_columns, [String].Empty))


        descriptions.Add("Valor Incapacidad Ambulatoria", "Valor de Incapacidad Ambulatoria en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad Hospitalaria", "Valor de Incapacidad Hospitalaria en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Incapacidad ERP - Ambulatoria", "Días Incapacidad ERP - Ambulatoria " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Incapacidad ERP - Hospitalaria", "Días Incapacidad ERP- Hospitalaria" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Incapacidad Hospitalaria", "Días de Incapacidad Hospitalaria en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Incapacidad Patrono-Hospitalaria", "Días Incapacidad Patrono-Hospitalaria" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Incapacidad Patrono-Ambulatorio", "Días Incapacidad Patrono-Ambulatorio " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad Patrono-Hospitalaria", "Valor Incapacidad Patrono-Hospitalaria" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad Patrono - Ambulatoria", "Valor Incapacidad Patrono - Ambulatoria" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad ERP-Hospitalaria", "Valor Incapacidad ERP-Hospitalaria" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad ERP- Ambulatoria", "Valor Incapacidad ERP- Ambulatoria" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Maternidad", "Valor de Maternidad en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Licencia", "Días de licencia en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor licencia no Remunerada", "Valor de Licencia no remunerada en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días licencia no Remunerada", "Días Licencia no remunerada en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Sanciones", "Valor de sanciones en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad Riesgos Pro", "Valor de incapacidad de riesgos profesionales en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Licencia Luto", "Valor de Licencia por Luto en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Paternidad", "Valor de Licencia por Paternidad en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad General", "Valor de Incapacidad General en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Trabajados", "Numero de Dias trabajados en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Provisión", "Numero de Dias de provision en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC SENA", "IBC SENA " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC ICBF", "IBC ICBF " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC Periodo", "IBC Periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC Periodo Anterior", "IBC Periodo Anterior " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC Cesantias", "IBC Cesantias " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC Primas", "IBC Primas " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC Caja", "IBC Caja " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC Pensión", "IBC Pensión " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC ARP", "IBC ARP " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("IBC Salud", "IBC Salud " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Total Devengado", "Valor Total devengado en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Total Deducido", "Valor Total deducido en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Retención", "Valor total de retención en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Aporte a Salud Empleado", "Aporte a Salud del empleado en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Aporte a Pensión Empleado", "Aporte a Pensión del empleado en el periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Vacaciones", "Valor Vacaciones Calculadas " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Interes de Cesantias", "Valor Interés Cesantías Calculadas " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Primas", "Valor Primas Calculadas " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Calamidad Domestica", "Dias Calamidad Domestica " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Bonificaciones", "Valor Bonificaciones " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Empleado Sindicalizado", "Empleado Sindicalizado " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Concepto Manual", "Valor Concepto Manual " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Gastos Representacion", "Gastos Representacion " & vbNewLine & "Tipo de Dato : " & GetType(Boolean).ToString())
        descriptions.Add("Valor Prima Servicios", "Valor Prima Servicios " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Horas Diarias Laboradas", "Horas Diarias Laboradas " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Numero Contratos Periodo", "Numero Contratos Periodo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Codigo Tipo Empleado", "Codigo Tipo Empleado " & vbNewLine & "Tipo de Dato : " & GetType(String).ToString())
        descriptions.Add("Año Laborado", "Año Laborado " & vbNewLine & "Tipo de Dato : " & GetType(Boolean).ToString())
        descriptions.Add("Valor Bonificacion Recreacion", "Valor Bonificacion Recreacion " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Prima Vacaciones", "Valor Prima Vacaciones " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incremento Vacacional", "Valor Incremento Vacacional " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Paga Credito", "Paga Credito 1. Mes que paga Vacaciones; 2. 1er Mes en Vacaciones; 3. Mes que Retorna de Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Quincena", "Quincena: 1 (Primera Quincena). 2 (Segunda Quincena)" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Días Vacaciones", "Días Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Total IBC Solidaridad", "Total IBC Solidaridad" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Vacaciones Periodo", "Dias Vacaciones Periodo" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Item Contrato", "Item Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Valor Compensacion Vacaciones", "Valor Compensacion Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Horas Minimas Cargo", "Horas Minimas Cargo" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Maximas Cargo", "Horas Maximas Cargo" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("IBC Vacaciones", "IBC Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Double).ToString())
        descriptions.Add("Prima Diciembre", "Prima Diciembre" & vbNewLine & "Tipo de Dato : " & GetType(Double).ToString())
        descriptions.Add("Prima Junio", "Prima Junio" & vbNewLine & "Tipo de Dato : " & GetType(Double).ToString())
        descriptions.Add("Prima Vacaciones Hist", "Prima Vacaciones Hist" & vbNewLine & "Tipo de Dato : " & GetType(Double).ToString())
        descriptions.Add("Horas Ajuste Extras Diurnas Festivas", "Horas Ajuste Extras Diurnas Festivas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Ajuste Extras Nocturnas Festivas", "Horas Ajuste Extras Nocturnas Festivas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Ajuste Recargos Nocturnas Festivas", "Horas Ajuste Recargos Nocturnas Festivas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("IBC Vacaciones FSP", "IBC Vacaciones FSP" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Vacaciones Pagado", "Valor Vacaciones Pagado" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Base Embargo", "Base Embargo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Fecha Posesion", "Fecha Posesion" & vbNewLine & "Tipo de Dato : " & GetType(Date).ToString())
        descriptions.Add("Valor Retroactivo", "Valor Retroactivo" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Retencion Indemnizacion", "Valor Retencion Indemnizacion" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Indemnizacion", "Valor Indemnizacion" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Gasto Representacion", "Valor Gasto Representacion" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Prima Vacaciones Provision", "Valor Prima Vacaciones Provision" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Sancion", "Dias Sancion" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Salario Minimo Institucional", "Salario mínimo establecido por la Institución " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Cotizante Exterior", "Cotizante Exterior " & vbNewLine & "Tipo de Dato : " & GetType(String).ToString())
        descriptions.Add("IBC Pensión Prima Media", "IBC Pensión Prima Media" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Permiso", "Dias Permiso del periodo: " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Licencia Remunerada:", "Dias Licencia Remunerada:" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Vacaciones en Dinero", "Dias Vacaciones en Dinero:" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Incapacidad Profesional Patrono", "Días Incapacidad Profesional Patrono:" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias Incapacidad Profesional ERP", "Días Incapacidad Profesional ERP:" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad Profesional Patrono", "Valor Incapacidad Profesional Patrono:" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Incapacidad Profesional ERP", "Valor Incapacidad Profesional ERP:" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())


        m_columns.Add(New ColumnInfo("fieldVARINCAAMB", "Valor Incapacidad Ambulatoria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARINCAHOS", "Valor Incapacidad Hospitalaria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAIHOS", "Días Incapacidad Hospitalaria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAIHOSPA", "Días Incapacidad Patrono-Hospitalaria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAIAMBERP", "Días Incapacidad ERP - Ambulatoria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAIHOSERP", "Días Incapacidad ERP- Hospitalaria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAIAMBPA", "Días Incapacidad Patrono-Ambulatorio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARMATERNI", "Valor Maternidad", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALINCAHOSPA", "Valor Incapacidad Patrono-Hospitalaria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALINCAAMBPA", "Valor Incapacidad Patrono - Ambulatoria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALINCAHOSERP", "Valor Incapacidad ERP- Hospitalaria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALINCAAMBERP", "Valor Incapacidad ERP- Ambulatoria", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIALICE", "Días Licencia", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARLICNREM", "Valor licencia no Remunerada", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIALNOR", "Días licencia no Remunerada", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARSANCION", "Valor Sanciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALRIES", "Valor Incapacidad Riesgos Pro", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALLUTO", "Valor Licencia Luto", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALLIPA", "Valor Paternidad", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALINCG", "Valor Incapacidad General", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIATRAB", "Dias Trabajados", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAPROV", "Dias Provisión", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCSENA", "IBC SENA", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCICBF", "IBC ICBF", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCPERI", "IBC Periodo", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCPERIPRE", "IBC Periodo Anterior", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCCESA", "IBC Cesantias", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCCPRI", "IBC Primas", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCCAJA", "IBC Caja", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCPENS", "IBC Pensión", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARNIBCARP", "IBC ARP", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARNIBCSAL", "IBC Salud", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARNIBCVAC", "IBC Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDEVENGA", "Total Devengado", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDEDUCID", "Total Deducido", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALRETE", "Valor Retención", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldMODSALUDEM", "Aporte a Salud Empleado", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldMODPENSION", "Aporte a Pensión Empleado", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALVACA", "Valor Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALINTC", "Valor Interes de Cesantias", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALPRIM", "Valor Primas", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIACALD", "Dias Calamidad Domestica", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARBONIFIC", "Valor Bonificaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARSINDICA", "Empleado Sindicalizado", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALCONM", "Valor Concepto Manual", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARGASTREP", "Gastos Representacion", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPRIMSER", "Valor Prima Servicios", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHORDIAR", "Horas Diarias Laboradas", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARNUMCOND", "Numero Contratos Periodo", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARTIPEMPL", "Codigo Tipo Empleado", GetType(String), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARANOLABO", "Año Laborado", GetType(Boolean), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALBONR", "Valor Bonificacion Recreacion", GetType(Boolean), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALPRIV", "Valor Prima Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALINCV", "Valor Incremento Vacacional", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPAGCRED", "Paga Credito", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARQUINCEN", "Quincena", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAVACA", "Días Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARTOIBCSO", "Total IBC Solidaridad", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIASVAP", "Dias Vacaciones Periodo", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARITEMCON", "Item Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARCOMPVAC", "Valor Compensacion Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHORMINC", "Horas Minimas Cargo", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHORMAXC", "Horas Maximas Cargo", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHORMAXC", "IBC Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPRIMDIC", "Prima Diciembre", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPRIMJUN", "Prima Junio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARPRIMVAH", "Prima Vacaciones Hist", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHAEDFES", "Horas Ajuste Extras Diurnas Festivas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHANEFES", "Horas Ajuste Extras Nocturnas Festivas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARHNNEFES", "Horas Ajuste Recargos Nocturnas Festivas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCVACF", "IBC Vacaciones FSP", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALVACP", "Valor Vacaciones Pagado", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARBASEEMB", "Base Embargo", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARFECHPOS", "Fecha Posesion", GetType(Date), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALRETR", "Valor Retroactivo", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALRETI", "Valor Retencion Indemnizacion", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARREPRCOS", "Valor Gasto Representacion", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALVACP", "Valor Prima Vacaciones Provision", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIASANC", "Dias Sancion", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARSALINST", "Salario Minimo Institucional", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARCOTEXT", "Cotizante Exterior", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARIBCPENSPRIMED", "IBC Pensión Prima Media", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAPER", "Dias Permiso", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIALICREM", "Dias Licencia Remunerada", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAVACDIN", "Dias Vacaciones Dinero", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAPATRON", "Dias Incapacidad Profesional Patrono", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARDIAPROERP", "Dias Incapacidad Profesional ERP", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALPROPAT", "Valor Incapacidad Profesional Patrono", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New ColumnInfo("fieldVARVALPROERP", "Valor Incapacidad Profesional ERP", GetType(Decimal), m_columns, [String].Empty))
    End Sub

#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de Centros de estudio.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        CleanControls()
    End Sub
    ''' <summary>
    ''' METODO: Item buscar del control de Centros de estudio.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de Centros de estudio.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If concept IsNot Nothing Then
            If concept.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    While concept.ConceptGroup.Count() > 0
                        concept.ConceptGroup.Item(0).MarkAsDeleted()
                    End While
                    Using Model As New MConcept(MyBase.Tag)
                        AsyncLoader(True)
                        Dim result As ActionMessageResult(Of Concept)
                        result = Await Model.DeleteConceptAsync(concept)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            AsyncLoader(False)
                            ModoBusqueda = False
                            Deshacer()
                        Else
                            If result.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                            AsyncLoader(False)
                        End If
                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnTipodeVinculacion, Eform.TipodeVinculacion)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnTipodeVinculacion, Eform.TipodeVinculacion)
        End If

    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de Profesiones.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        INDTxtName.Focus()
        AssigningValues()
        Dim contador = 0
        'Elimino el primer registro de concept group, porque es la fila del check all           (Funcionalidad deshabilitada)
        'concept.ConceptGroup.Remove(concept.ConceptGroup.Item(0))
        While contador < concept.ConceptGroup.Count()
            Dim conceptGroup As ConceptGroup = concept.ConceptGroup.Item(contador)
            If conceptGroup.Apply = False Then
                conceptGroup.MarkAsDeleted()
                contador -= 1
            End If
            contador += 1
        End While
        Using Model As New MConcept(MyBase.Tag)
            AsyncLoader(True)
            If Await Model.SaveConceptAsync(concept) = True Then
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                If concept.ChangeTracker.State = ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                Else
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
                AsyncLoader(False)
                ModoBusqueda = False
                Deshacer()
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                AsyncLoader(False)
            End If

        End Using
    End Sub
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Concept
            .ListaColumnas = {
                New Presentation.Controls.ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"},
                New Presentation.Controls.ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"},
                New Presentation.Controls.ColumnInfo() With {.Caption = "Tipo", .FieldName = "Tipo"},
                New Presentation.Controls.ColumnInfo() With {.Caption = "Clase", .FieldName = "ConceptClassName"}}.ToList
            .FormParent = Me
            .ShowSearch()
        End With
        ModoBusqueda = True
    End Sub


    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Profesiones.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        showCheckRegion()
        If Not ModoBusqueda Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub


    Private Sub showCheckRegion()
        ' La Ley 1393 del 40% solo aplica para Colombia, NO para Costa Rica
        If indigo.LanguageCulture = "es-CR" Then
            INDlciAffectLimit40.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDlciAffectLimit40.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(ByVal value As String)

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
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2

        Dim ConceptType As String

        If INDRgType.Text = 1 Then
            ConceptType = "Devengado"
        ElseIf INDRgType.Text = 2 Then
            ConceptType = "Deducido"
        ElseIf INDRgType.Text = 3 Then
            ConceptType = "Patronal"
        End If

        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmConceptsMetaData, Eform.InfoMetaData), Me.concept.Code, Me.concept.Name, Me.concept.ConceptType, INDCbeClass.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.concept.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmConceptsMetaDataTitle, Eform.InfoMetaData), Me.concept.Name),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmConceptsMetaData, Eform.InfoMetaData), Me.concept.Code, Me.concept.Name, Me.concept.ConceptType, INDCbeClass.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmConceptsMetaDataTitle, Eform.InfoMetaData), Me.concept.Name)
            Return Me._doc
        End If
    End Function


#End Region

#Region "bar buttons and events"
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
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
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConcepts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento que controla cuando se cambia de clase de concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCbeClass_SelectedValueChanged(sender As Object, e As EventArgs) Handles INDCbeClass.SelectedValueChanged
        If INDCbeClass.EditValue IsNot Nothing AndAlso INDCbeClass.EditValue <> "" Then
            If INDCbeClass.EditValue = "003" Or INDCbeClass.EditValue = "004" Or INDCbeClass.EditValue = "007" Or INDCbeClass.EditValue = "010" Or INDCbeClass.EditValue = "011" Then
                AllowConceptFormulate = False
                INDMeFormulates.Text = "[Valor Concepto Manual]"
            Else
                AllowConceptFormulate = True
                If INDMeFormulates.Text = "[Valor Concepto Manual]" Then
                    INDMeFormulates.Text = ""
                End If
            End If
        End If
    End Sub

#End Region

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLyConcept.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub
    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDLyConcept.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDLyConcept_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'Ejecuatamos la consulta
            Using model As New MConcept(MyBase.Tag)
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDLyConcept.Items.Count - 1
                        INDLyConcept.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDLyConcept.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLyConcept.Items.Item(j).Tag.ToString.Trim Then
                                    INDLyConcept.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyConcept_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs)
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDLyConcept.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDLyConcept.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLyConcept.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(INDBteCode.Text) Then
            Using model As New MConcept(MyBase.Tag)
                AsyncLoader(True)
                Dim state As Boolean = Not concept.State
                Dim Result = Await model.ChangeStateConcept(INDBteCode.Text, state)
                AsyncLoader(False)
                If Result = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    concept.State = state
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")

                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function
#End Region

#Region "Formulates"

    Class ColumnInfo
        Implements IDataColumnInfo
        ReadOnly m_name As String
        ReadOnly m_fieldName As String
        ReadOnly m_fieldType As Type
        ReadOnly m_columns As List(Of IDataColumnInfo)
        Private m_unboundExpression As String

        Public Sub New(name As String, fieldName As String, fieldType As Type, columns As List(Of IDataColumnInfo), unboundExpression As String)
            Me.m_name = name
            Me.m_fieldName = fieldName
            Me.m_fieldType = fieldType
            Me.m_columns = columns
            Me.m_unboundExpression = unboundExpression
        End Sub
        Public ReadOnly Property Caption() As String Implements IDataColumnInfo.Caption
            Get
                Return m_fieldName
            End Get
        End Property

        Public ReadOnly Property Columns() As List(Of IDataColumnInfo) Implements IDataColumnInfo.Columns
            Get
                Return m_columns
            End Get
        End Property

        Public ReadOnly Property Controller() As DataControllerBase Implements IDataColumnInfo.Controller
            Get
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property FieldName() As String Implements IDataColumnInfo.FieldName
            Get
                Return m_fieldName
            End Get
        End Property

        Public ReadOnly Property FieldType() As Type Implements IDataColumnInfo.FieldType
            Get
                Return m_fieldType
            End Get
        End Property

        Public ReadOnly Property Name() As String Implements IDataColumnInfo.Name
            Get
                Return m_name
            End Get
        End Property

        Public ReadOnly Property UnboundExpression() As String Implements IDataColumnInfo.UnboundExpression
            Get
                Return m_unboundExpression
            End Get
        End Property
    End Class

#End Region

#Region "IDataColumnInfo Implementation"

    Public ReadOnly Property Caption() As String Implements IDataColumnInfo.Caption
        Get
            Return "MyExpression"
        End Get
    End Property

    Public ReadOnly Property Columns() As List(Of IDataColumnInfo) Implements IDataColumnInfo.Columns
        Get
            Return m_columns
        End Get
    End Property

    Public ReadOnly Property Controller() As DataControllerBase Implements IDataColumnInfo.Controller
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property FieldName() As String Implements IDataColumnInfo.FieldName
        Get
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property FieldType() As Type Implements IDataColumnInfo.FieldType
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property UnboundExpression() As String Implements IDataColumnInfo.UnboundExpression
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Name1 As String Implements IDataColumnInfo.Name
        Get
            Return Nothing
        End Get
    End Property

#End Region


    Private Sub INDRgType_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgType.EditValueChanged
        Dim RGType As RadioGroup = sender

        If RGType.EditValue = "1" Then '' Concepto Devengado
            INDColDeductedAccount.Visible = True
            INDColAccruedAccount.Visible = True
        ElseIf RGType.EditValue = "2" Then '' Concepto Deducido
            INDColDeductedAccount.Visible = True
            INDColAccruedAccount.Visible = True
        Else '' Concepto Patronal
            INDColDeductedAccount.Visible = True
            INDColAccruedAccount.Visible = True
        End If
        ColConcepts()
        If RGType.EditValue IsNot Nothing Then
            LoadElectronicPayrollConceptsByType(RGType.EditValue.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Carga los conceptos electrónicos de nómina según el tipo de concepto seleccionado
    ''' </summary>
    ''' <param name="conceptTypeValue">Valor del tipo de concepto ("1", "2", "3")</param>
    ''' <remarks></remarks>
    Private Sub LoadElectronicPayrollConceptsByType(ByVal conceptTypeValue As String)
        If String.IsNullOrEmpty(conceptTypeValue) Then
            INDslConceptPayrollElectronic.Properties.DataSource = Nothing
            Return
        End If
        Dim conceptType As Integer
        If Not Integer.TryParse(conceptTypeValue, conceptType) Then
            INDslConceptPayrollElectronic.Properties.DataSource = Nothing
            Return
        End If
        Using conceptModel As New MConcept(MyBase.Tag)
            Dim electronicConcepts = conceptModel.ListElectronicPayrollConceptsByType(conceptType)
            If electronicConcepts IsNot Nothing AndAlso electronicConcepts.Count > 0 Then
                INDslConceptPayrollElectronic.Properties.DataSource = electronicConcepts
            Else
                INDslConceptPayrollElectronic.Properties.DataSource = Nothing
            End If
        End Using
    End Sub

    Private Async Function LoadConcepts(ByVal InterfaceName As String) As Task
        If InterfaceName <> String.Empty Then
            'Dim ConceptAccountingStructure As New ConceptAccountingStructure
            AsyncLoader(True)
            Dim PayrollAccount = Await Model.ListAccounts(InterfaceName)
            AsyncLoader(False)

            If PayrollAccount IsNot Nothing Then
                Me.RepositoryItemGridLookUpEditCuentaDebito.DataSource = PayrollAccount
                RepositoryItemGridLookUpEditCuentaCredito.DataSource = PayrollAccount
            Else
                Me.RepositoryItemGridLookUpEditCuentaDebito.DataSource = Nothing
                RepositoryItemGridLookUpEditCuentaCredito.DataSource = Nothing
                INDgcCuentasContables.DataSource = Nothing

            End If

            Dim ListConcept = ListConceptAccountingStructure.Where(Function(x) x.InterfazName = InterfaceName).ToList()

            If ListConcept IsNot Nothing AndAlso ListConcept.Count() > 0 Then
                INDgcCuentasContables.DataSource = ListConcept

                Me.RepositoryItemGridLookUpEditCuentaDebito.DataSource = PayrollAccount
                RepositoryItemGridLookUpEditCuentaCredito.DataSource = PayrollAccount

            Else

                Dim ListAccountingStructure = Await Model.ListAccountingStructure()
                Dim ListConceptAccountingStructureNew As New List(Of ConceptAccountingStructure)

                For i As Integer = 0 To ListAccountingStructure.Count() - 1
                    Dim ConceptAccountingStructure As New ConceptAccountingStructure
                    ConceptAccountingStructure.AccountingStructure = ListAccountingStructure.Item(i)
                    ConceptAccountingStructure.AccountingStructureId = ListAccountingStructure.Item(i).Id
                    ConceptAccountingStructure.InterfazName = InterfaceName
                    ConceptAccountingStructure.CreationDate = Date.Now()
                    ConceptAccountingStructure.ModifiedDate = Date.Now()
                    ConceptAccountingStructure.CreationUserId = indigo.UserIndigoId
                    ConceptAccountingStructure.ModificationUserId = indigo.UserIndigoId
                    ListConceptAccountingStructureNew.Add(ConceptAccountingStructure)
                Next

                For j As Integer = 0 To ListConceptAccountingStructureNew.Count() - 1
                    ListConceptAccountingStructure.Add(ListConceptAccountingStructureNew.Item(j))
                Next


                INDgcCuentasContables.DataSource = ListConceptAccountingStructureNew
            End If
        End If



    End Function

    Private Async Sub INDglCompany_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        'Dim Container As GridLookUpEdit
        'Container = sender

        Dim InterfaceName = e.NewValue
        If InterfaceName IsNot Nothing Then
            Await LoadConcepts(InterfaceName)
        End If

    End Sub

    ''' <summary>
    ''' Cuando cambia el valor de la formula, verificamos si hay algo escrito, sino desabilitamos el boton de test
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDMeFormulates_EditValueChanged(sender As Object, e As EventArgs) Handles INDMeFormulates.EditValueChanged
        Dim memoEdit = CType(sender, MemoEdit)
        If memoEdit.Text.Length > 0 Then
            INDbtnTestFormulate.Enabled = True
        Else
            INDbtnTestFormulate.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el test de la formula
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnTestFormulate_Click(sender As Object, e As EventArgs) Handles INDbtnTestFormulate.Click
        Dim formulate As String = INDMeFormulates.Text
        Dim reg = New System.Text.RegularExpressions.Regex("\[.+?\]", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim matches = reg.Matches(formulate)
        Dim listVariable As New List(Of String)
        For Each match As System.Text.RegularExpressions.Match In matches
            If listVariable.Contains(match.Value) = False Then
                listVariable.Add(match.Value)
            End If
        Next
        Dim form As New FrmConceptsExecuteFormulate(formulate, listVariable)
        form.StartPosition = FormStartPosition.CenterScreen
        Dim start As New FrmTransparent(form, False)
        start.ShowDialog()
    End Sub

    Private Sub INDCbeClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDCbeClass.EditValueChanged
        ColConcepts()
    End Sub

    Private Sub ColConcepts()
        If INDCbeClass.EditValue = "021" Or INDCbeClass.EditValue = "022" Or INDCbeClass.EditValue = "023" Or INDCbeClass.EditValue = "027" Then
            INDColAccruedAccount.Visible = False
            INDColDeductedAccount.Visible = False
            INDColInabilityDebitValueEmployeeAccount.Visible = True
            INDColInabilityDebitValueEPSAccount.Visible = True
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvCuentas.Columns
                If col.Name = "colActions" Then
                    col.Width = 100
                    col.VisibleIndex = 6
                End If
            Next

        Else
            INDColAccruedAccount.Visible = True
            INDColInabilityDebitValueEmployeeAccount.Visible = False
            INDColInabilityDebitValueEPSAccount.Visible = False
        End If

    End Sub

    Private Async Function LoadConceptsVie() As Task
        ' If InterfaceName <> String.Empty Then
        'Dim ConceptAccountingStructure As New ConceptAccountingStructure


        'If ListConceptAccountingStructure IsNot Nothing AndAlso ListConceptAccountingStructure.Count() > 0 Then
        '    INDgcCuentasContablesVie.DataSource = ListConceptAccountingStructure
        'Else

        Dim ListAccountingStructure = Await Model.ListAccountingStructure()
        Dim ListConceptAccountingStructureNew As New List(Of ConceptAccountingStructure)

        For i As Integer = 0 To ListAccountingStructure.Count() - 1
            Dim ConceptAccountingStructure As New ConceptAccountingStructure
            ConceptAccountingStructure.AccountingStructure = ListAccountingStructure.Item(i)
            ConceptAccountingStructure.AccountingStructureId = ListAccountingStructure.Item(i).Id
            ConceptAccountingStructure.InterfazName = indigo.TransactionalContainer
            ConceptAccountingStructure.CreationDate = Date.Now()
            ConceptAccountingStructure.ModifiedDate = Date.Now()
            ConceptAccountingStructure.CreationUserId = indigo.UserIndigoId
            ConceptAccountingStructure.ModificationUserId = indigo.UserIndigoId
            ListConceptAccountingStructureNew.Add(ConceptAccountingStructure)
        Next

        For j As Integer = 0 To ListConceptAccountingStructureNew.Count() - 1
            ListConceptAccountingStructure.Add(ListConceptAccountingStructureNew.Item(j))
        Next


        ' INDgcCuentasContablesVie.DataSource = ListConceptAccountingStructureNew

        'End If



    End Function

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Using formulario As New FrmPopUpAddAccountingPayrollConcept
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddConceptAccountingPayrollEventArgs, AddressOf ReturnAddConceptAccounting
            formulario.Size = New System.Drawing.Size(800, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ListConceptAccountingStructureImportInfo = ListConceptAccountingStructure
            formulario.ListConceptAccountingStructureValidation = ListConceptAccountingStructure
            formulario.MyTag = MyTag
            formulario.ConceptType = INDRgType.EditValue
            formulario.ConceptClass = INDCbeClass.EditValue
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddConceptAccounting(sender As Object, e As AddConceptAccountingPayrollEventArgs)
        If ListConceptAccountingStructure Is Nothing Then
            'INDSleSupplierDistributionLine.Properties.ReadOnly = True
            ListConceptAccountingStructure = New List(Of ConceptAccountingStructure)
        End If
        If e.EditMode = True Then
            ListConceptAccountingStructure.Remove(ConceptAccountingStructure)
            ListConceptAccountingStructure.Insert(indexEditRecord, e.ConceptAccountingStructure)
        ElseIf e.ImportDataMode = True Then
            ListConceptAccountingStructure.AddRange(e.ConceptAccountingStructure)
        Else
            ListConceptAccountingStructure.Add(e.ConceptAccountingStructure)
        End If
        INDgcCuentasContables.DataSource = Nothing
        INDgcCuentasContables.DataSource = ListConceptAccountingStructure
        'ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumnsAdministrative()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvCuentas, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvCuentas.Columns
            If col.Name = "colActions" Then
                col.Width = 100
                col.VisibleIndex = 6
            End If
        Next
    End Sub


    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetailAdministrative()
        ConceptAccountingStructure = DirectCast(INDgvCuentas.GetFocusedRow(), ConceptAccountingStructure)
        indexEditRecord = ListConceptAccountingStructure.IndexOf(ConceptAccountingStructure)
        Using formulario As New FrmPopUpAddAccountingPayrollConcept
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddConceptAccountingPayrollEventArgs, AddressOf ReturnAddConceptAccounting
            formulario.Size = New System.Drawing.Size(800, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ConceptAccountingStructureEdit = ConceptAccountingStructure
            formulario.EditMode = True
            formulario.ConceptType = INDRgType.EditValue
            formulario.ConceptClass = INDCbeClass.EditValue
            'formulario.INDSlAccountingStructure.EditValue = FixedAssetEquipmentCatalogDetail.IdAccountingStructure
            'formulario.INDSlSpendDepreciationAccount.EditValue = FixedAssetEquipmentCatalogDetail.IdLoanSpendAccountingAccount
            'formulario.INDSlSpendLeasingAccount.EditValue = FixedAssetEquipmentCatalogDetail.IdLoanLeasingSpendAccountingAccount
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub



    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetailAdministrative()
        ConceptAccountingStructure = DirectCast(INDgvCuentas.GetFocusedRow(), ConceptAccountingStructure)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If ConceptAccountingStructure.Id > 0 Then
                If ListConceptAccountingStructureDelete Is Nothing Then
                    ListConceptAccountingStructureDelete = New List(Of ConceptAccountingStructure)
                End If
                ConceptAccountingStructure.MarkAsDeleted()
                ListConceptAccountingStructureDelete.Add(ConceptAccountingStructure)
            End If
            ListConceptAccountingStructure.Remove(ConceptAccountingStructure)

            INDgcCuentasContables.DataSource = Nothing
            INDgcCuentasContables.DataSource = ListConceptAccountingStructure
            'ctrTmp.PrintInfo()
        End If
    End Sub


#Region "MenuContext"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetailAdministrative()
            Case "Remove"
                DeleteDetailAdministrative()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetailAdministrative()
            Case "Remove"
                DeleteDetailAdministrative()
        End Select
    End Sub

    Private Sub INDChkAffectIBC_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkAffectIBC.CheckedChanged
        If INDChkAffectIBC.Checked Then
            RemoveHandler INDChkAffectLimit40.CheckedChanged, AddressOf INDChkAffectLimit40_CheckedChanged
            INDChkAffectLimit40.Checked = False
            AddHandler INDChkAffectLimit40.CheckedChanged, AddressOf INDChkAffectLimit40_CheckedChanged
        End If
    End Sub

    Private Sub INDChkAffectLimit40_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkAffectLimit40.CheckedChanged
        If INDChkAffectLimit40.Checked Then
            RemoveHandler INDChkAffectIBC.CheckedChanged, AddressOf INDChkAffectIBC_CheckedChanged
            INDChkAffectIBC.Checked = False
            AddHandler INDChkAffectIBC.CheckedChanged, AddressOf INDChkAffectIBC_CheckedChanged
        End If
    End Sub


#End Region
End Class


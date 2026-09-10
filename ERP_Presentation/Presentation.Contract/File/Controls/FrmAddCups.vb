Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Contract.MVP
Imports Presentation.Base.BaseClass
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.ContractRepository

Public Class FrmAddCups
    Implements IAddRule

    Public Sub New()
        InitializeComponent()
        Me.SuspendLayout()
        Me.Size = New Drawing.Size(480, 300)
        Me.ResumeLayout()
    End Sub

#Region "Properties"

    ''' <summary>
    ''' Establece el datasource de las descripciones del popup de la primera condición
    ''' </summary>
    ''' <returns></returns>
    Public Property DescriptionPopupFirstConditionXpo As XPCollection Implements IAddRule.DescriptionPopupFirstConditionXpo
        Get

        End Get
        Set(value As XPCollection)

        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RuleType As Integer? Implements IAddRule.RuleType
        Get
            Return INDsleRuleType.EditValue
        End Get
        Set(value As Integer?)
            INDsleRuleType.EditValue = value
        End Set
    End Property

    Public Property ConditionType As Integer? Implements IAddRule.ConditionType
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property Weight As Integer Implements IAddRule.Weight
        Get

        End Get
        Set(value As Integer)

        End Set
    End Property

    Public Property LogicOperator As Integer? Implements IAddRule.LogicOperator
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property ConditionTypeSecond As Integer? Implements IAddRule.ConditionTypeSecond
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property StartTimeFirst As TimeSpan? Implements IAddRule.StartTimeFirst
        Get

        End Get
        Set(value As TimeSpan?)

        End Set
    End Property

    Public Property EndTimeFirst As TimeSpan? Implements IAddRule.EndTimeFirst
        Get

        End Get
        Set(value As TimeSpan?)

        End Set
    End Property

    Public Property SpecialtyIdFirst As String Implements IAddRule.SpecialtyIdFirst
        Get

        End Get
        Set(value As String)

        End Set
    End Property

    Public Property SpecialtyXpoFirst As XPInstantFeedbackSource Implements IAddRule.SpecialtyXpoFirst
        Get

        End Get
        Set(value As XPInstantFeedbackSource)

        End Set
    End Property

    Public Property FunctionalUnitIdFirst As Integer? Implements IAddRule.FunctionalUnitIdFirst
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property FunctionalUnitXpoFirst As XPInstantFeedbackSource Implements IAddRule.FunctionalUnitXpoFirst
        Get

        End Get
        Set(value As XPInstantFeedbackSource)

        End Set
    End Property

    Public Property RIASPopupFirstContidionXpo As XPCollection Implements IAddRule.RIASPopupFirstContidionXpo
        Get

        End Get
        Set(value As XPCollection)

        End Set
    End Property

    Public Property UnitTypeIdFirst As Integer? Implements IAddRule.UnitTypeIdFirst
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property StartTimeSecond As TimeSpan? Implements IAddRule.StartTimeSecond
        Get

        End Get
        Set(value As TimeSpan?)

        End Set
    End Property

    Public Property EndTimeSecond As TimeSpan? Implements IAddRule.EndTimeSecond
        Get

        End Get
        Set(value As TimeSpan?)

        End Set
    End Property

    Public Property SpecialtyIdSecond As String Implements IAddRule.SpecialtyIdSecond
        Get

        End Get
        Set(value As String)

        End Set
    End Property

    Public Property SpecialtyXpoSecond As XPInstantFeedbackSource Implements IAddRule.SpecialtyXpoSecond
        Get

        End Get
        Set(value As XPInstantFeedbackSource)

        End Set
    End Property

    Public Property FunctionalUnitIdSecond As Integer? Implements IAddRule.FunctionalUnitIdSecond
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property FunctionalUnitXpoSecond As XPInstantFeedbackSource Implements IAddRule.FunctionalUnitXpoSecond
        Get

        End Get
        Set(value As XPInstantFeedbackSource)

        End Set
    End Property

    Public Property UnitTypeIdSecond As Integer? Implements IAddRule.UnitTypeIdSecond
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property LiquidationTypePopup As Integer? Implements IAddRule.LiquidationTypePopup
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property SalesValuePopup As Decimal Implements IAddRule.SalesValuePopup
        Get

        End Get
        Set(value As Decimal)

        End Set
    End Property

    Public Property SalesValueWithSurchargePopup As Decimal Implements IAddRule.SalesValueWithSurchargePopup
        Get

        End Get
        Set(value As Decimal)

        End Set
    End Property

    Public Property RateManualValidityIdPopup As Integer? Implements IAddRule.RateManualValidityIdPopup
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As Integer?)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property RateManualValidityXpoPopup As XPInstantFeedbackSource Implements IAddRule.RateManualValidityXpoPopup
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property RateManualIdPopup As Integer? Implements IAddRule.RateManualIdPopup
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property RateManualXpoPopup As XPInstantFeedbackSource Implements IAddRule.RateManualXpoPopup
        Get

        End Get
        Set(value As XPInstantFeedbackSource)

        End Set
    End Property

    Public Property RateVariationPopup As Decimal Implements IAddRule.RateVariationPopup
        Get

        End Get
        Set(value As Decimal)

        End Set
    End Property

    Public Property OperatorFirst As Integer Implements IAddRule.OperatorFirst
        Get

        End Get
        Set(value As Integer)

        End Set
    End Property

    Public Property OperatorSecond As Integer? Implements IAddRule.OperatorSecond
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property LiquidationType As Byte? Implements IAddRule.LiquidationType
        Get

        End Get
        Set(value As Byte?)

        End Set
    End Property

    Public Property SalesValue As Decimal Implements IAddRule.SalesValue
        Get

        End Get
        Set(value As Decimal)

        End Set
    End Property

    Public Property SalesValueWithSurcharge As Decimal Implements IAddRule.SalesValueWithSurcharge
        Get

        End Get
        Set(value As Decimal)

        End Set
    End Property

    Public Property RateManualValidityId As Integer? Implements IAddRule.RateManualValidityId
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As Integer?)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property RateManualValidityXpo As XPInstantFeedbackSource Implements IAddRule.RateManualValidityXpo
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property RateManualId As Integer? Implements IAddRule.RateManualId
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property RateManualXpo As XPInstantFeedbackSource Implements IAddRule.RateManualXpo
        Get

        End Get
        Set(value As XPInstantFeedbackSource)

        End Set
    End Property

    Public Property RateVariation As Decimal? Implements IAddRule.RateVariation
        Get

        End Get
        Set(value As Decimal?)

        End Set
    End Property

    Public Property ManualType As Integer? Implements IAddRule.ManualType
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property ManualTypePopup As Integer? Implements IAddRule.ManualTypePopup
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property AllowValueChange As Boolean Implements IAddRule.AllowValueChange
        Get

        End Get
        Set(value As Boolean)

        End Set
    End Property

    Public Property StartTimePopupFirstCondition As TimeSpan? Implements IAddRule.StartTimePopupFirstCondition
        Get

        End Get
        Set(value As TimeSpan?)

        End Set
    End Property

    Public Property EndTimePopupFirstCondition As TimeSpan? Implements IAddRule.EndTimePopupFirstCondition
        Get

        End Get
        Set(value As TimeSpan?)

        End Set
    End Property

    Public Property UnitTypeIdPopupFirstCondition As Integer? Implements IAddRule.UnitTypeIdPopupFirstCondition
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property LiquidationTypePopupFirstCondition As Integer? Implements IAddRule.LiquidationTypePopupFirstCondition
        Get

        End Get
        Set(value As Integer?)

        End Set
    End Property

    Public Property SalesValuePopupFirstCondition As Decimal Implements IAddRule.SalesValuePopupFirstCondition
        Get

        End Get
        Set(value As Decimal)

        End Set
    End Property

    Public Property SalesValueWithSurchargePopupFirstCondition As Decimal Implements IAddRule.SalesValueWithSurchargePopupFirstCondition
        Get

        End Get
        Set(value As Decimal)

        End Set
    End Property

    Public Property RateManualValidityIdPopupFirstCondition As Integer? Implements IAddRule.RateManualValidityIdPopupFirstCondition
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As Integer?)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property RateManualValidityXpoPopupFirstCondition As XPInstantFeedbackSource Implements IAddRule.RateManualValidityXpoPopupFirstCondition
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property RateManualIdPopupFirstCondition As Integer? Implements IAddRule.RateManualIdPopupFirstCondition
    '    Get

    '    End Get
    '    Set(value As Integer?)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property RateManualXpoPopupFirstCondition As XPInstantFeedbackSource Implements IAddRule.RateManualXpoPopupFirstCondition
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property RateVariationPopupFirstCondition As Decimal Implements IAddRule.RateVariationPopupFirstCondition
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As Decimal)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property ManualTypePopupFirstCondition As Integer? Implements IAddRule.ManualTypePopupFirstCondition
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As Integer?)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property SpecialtyPopupFirstConditionXpo As XPCollection Implements IAddRule.SpecialtyPopupFirstConditionXpo
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As XPCollection)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Public Property FunctionalUnitPopupFirstConditionXpo As XPCollection Implements IAddRule.FunctionalUnitPopupFirstConditionXpo
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As XPCollection)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

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
    ''' Listado para validar si los items estan ya agregados
    ''' </summary>
    ''' <returns></returns>
    Public Property ListGroupersCupsValidate As List(Of GroupersCups)

    Public Property SalesSubtotal As Decimal Implements IAddRule.SalesSubtotal
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Decimal)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property SalesValueIVA As Decimal Implements IAddRule.SalesValueIVA
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Decimal)
            Throw New NotImplementedException()
        End Set
    End Property

    ''' <summary>
    ''' Listado para agregar los items
    ''' </summary>
    ''' <returns></returns>
    Dim ListGroupersCupsAdd As List(Of GroupersCups)

    ''' <summary>
    ''' indica si el folio se está cargado o ya terminó
    ''' </summary>
    Private _isLoading As Boolean

    ''' <summary>
    ''' Cursor de carga indigo
    ''' </summary>
    Private _indigoCursor As System.Windows.Forms.Cursor

    ''' <summary>
    ''' Indica si el folio en estado de carga
    ''' </summary>
    ''' <param name="isLoading">Valor que indica si el folio esta cargando</param>
    Public Sub IsLoading(Optional ByVal isLoading As Boolean = True)
        _isLoading = isLoading
        If isLoading Then
            Cursor = Me._indigoCursor
            LayoutBodyFolio.Enabled = False
            LayoutProgressPanel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Cursor = System.Windows.Forms.Cursors.Default
            LayoutBodyFolio.Enabled = True
            LayoutProgressPanel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Permite saber si controlo el cambio del cambio de valor
    ''' </summary>
    ''' <remarks></remarks>
    Dim controlerEditValueChanged As Boolean = False

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Listado para el datasource de los controles de tipo de regla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListXpCollection As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Presentador del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAddRule
#End Region

#Region "Handlers"
    Private Sub FrmAddCups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PAddRule(Me)
        Me._indigoCursor = ChangeCursorIndigo()
        InitializeTuples()
    End Sub

    Private Async Sub INDsleRuleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRuleType.EditValueChanged
        If RuleType IsNot Nothing AndAlso controlerEditValueChanged = False Then
            HideColumnsOfGridControlsRuleType()
            IsLoading()
            Await Task.Factory.StartNew(Async Function()
                                            ListXpCollection = Nothing
                                            ListXpCollection = Await Presenter.InitializeDataSourceGridControlsRulesType(RuleType)
                                            ListXpCollection.Load()
                                            INDgcControlsRuleType.BeginInvoke(Sub()
                                                                                  INDgcControlsRuleType.DataSource = Nothing
                                                                                  INDgcControlsRuleType.DataSource = ListXpCollection
                                                                                  IsLoading(False)
                                                                              End Sub)
                                        End Function)
        End If
    End Sub

    Private Sub INDSbAddCUPS_Click(sender As Object, e As EventArgs) Handles INDSbAddCUPS.Click
        AddCUPS()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckControlsLiquidationType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckControlsLiquidationType.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = viewControlsRuleType.GetFocusedRow()
            item.SelectOption = e.NewValue
            Dim cont = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
            INDpceControlRuleType.Text = cont.ToString + " item seleccionado"

            If cont = ListXpCollection.Count Then
                Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.check
            Else
                Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

#Region "MouseDoubleClick"

    ''' <summary>
    ''' Evento que se dispara al presionar doble click sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcControlsRuleType_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcControlsRuleType.MouseDoubleClick
        If ListXpCollection IsNot Nothing AndAlso ListXpCollection.Count > 0 Then
            Dim hitPoint = Me.viewControlsRuleType.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelectionOption") Then

                    Dim listFilterXpCollection = viewControlsRuleType.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.check
                    End If
                    Me.INDgcControlsRuleType.RefreshDataSource()
                    Dim contItems = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
                    INDpceControlRuleType.Text = contItems.ToString + " item seleccionado"
                    Me.INDgcControlsRuleType.Invalidate()
                End If
            End If
        End If
    End Sub
#End Region

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListGroupersCupsValidate = Nothing
        ListGroupersCupsAdd = Nothing
        _isLoading = Nothing
        _indigoCursor = Nothing
        controlerEditValueChanged = Nothing
        ListXpCollection = Nothing
        Presenter = Nothing
    End Sub


    Public Event RefreshDatasourceHandler(List As List(Of GroupersCups))

#End Region

#Region "Methods"
    ''' <summary>
    ''' Muestra u oculta las columnas dependiendo del tipo de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideColumnsOfGridControlsRuleType()
        INDlyItemControlRuleType.Text = ResourceManager.GetString("RuleType" + RuleType.ToString, NAME_MODULE)
        If RuleType = 5 Then
            INDpceControlRuleType.Text = "1 item seleccionado"
            INDpceControlRuleType.Properties.ReadOnly = True
        Else
            INDpceControlRuleType.Text = "0 item seleccionado"
            INDpceControlRuleType.Properties.ReadOnly = False
        End If
        Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
        Dim ListStringNames As New List(Of String)
        Select Case RuleType
            Case 1 'IPSService
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolManualType")
                ListStringNames.Add("INDcolPresentation")
            Case 2 'CUPS
                ListStringNames.Add("INDcolGroupCups")
                ListStringNames.Add("INDcolSubGroupCups")
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolNameCUPS")
            Case 3 'CupsSubGroup
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolGroup")
            Case 4 'CupsGroup
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
            Case 5 'General
                'ListStringNames.Add("INDcolSelectionOption")
        End Select
        FieldsGrid(ListStringNames)
    End Sub

    ''' <summary>
    ''' Metodo que recorre las columnas de la rejilla y las coloca visible 
    ''' dependiendo del listado de colName que le envien
    ''' </summary>
    ''' <param name="ListStringNames"></param>
    ''' <remarks></remarks>
    Private Sub FieldsGrid(ByVal ListStringNames As List(Of String))
        'Asigno si la columna es visible o no dependiendo del listado que envien anteriormente
        For iColumns = 0 To viewControlsRuleType.Columns.Count - 1
            For iList = 0 To ListStringNames.Count - 1
                If viewControlsRuleType.Columns.Item(iColumns).Name = ListStringNames.Item(iList) Then
                    viewControlsRuleType.Columns.Item(iColumns).Visible = True
                    Exit For
                Else
                    viewControlsRuleType.Columns.Item(iColumns).Visible = False
                End If
            Next
        Next
        'Asigno los visibleIndex para que aparezcan en orden las columnas
        Dim cont As Integer = 0
        For iColumns = 0 To viewControlsRuleType.Columns.Count - 1
            If viewControlsRuleType.Columns.Item(iColumns).Visible = True Then
                viewControlsRuleType.Columns.Item(iColumns).VisibleIndex = cont
                cont += 1
            End If
        Next
        'Asigno el groupIndex a las columnas para CUPS
        If RuleType = 2 Then
            INDcolGroupCups.GroupIndex = 0
            INDcolSubGroupCups.GroupIndex = 1
        Else
            INDcolGroupCups.GroupIndex = -1
            INDcolSubGroupCups.GroupIndex = -1
        End If
    End Sub

    ''' <summary>
    ''' Initializes the tuples.
    ''' </summary>
    Private Sub InitializeTuples()
        'Tipo de Reglas
        Dim ListRulesType = New List(Of Tuple(Of Integer, String))
        'ListRulesType.Add(New Tuple(Of Integer, String)(1, "Servicio IPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(2, "CUPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(3, "SubGrupo CUPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(4, "Grupo CUPS"))
        'ListRulesType.Add(New Tuple(Of Integer, String)(5, "General"))
        INDsleRuleType.Properties.DataSource = ListRulesType.ToList()
        If INDsleRuleType.Properties.Buttons.Count > 1 Then
            INDsleRuleType.Properties.Buttons(1).Visible = False
        End If
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Valida los controles del form
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsForm() As String
        Dim listErrors As New StringBuilder()

        If INDsleRuleType.EditValue Is Nothing Then
            listErrors.AppendLine("Seleccion un Tipo de Regla.")
        End If
        If ListXpCollection Is Nothing OrElse ListXpCollection.Count = 0 Then
            listErrors.AppendLine("Debe ingresar al menos un CUPS.")
        End If

        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que agrega la regla al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCUPS()
        Dim errors As String = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        Try
            IsLoading()
            Dim banValidate = False
            If ListXpCollection IsNot Nothing Then
                Dim cont = (From l In ListXpCollection Where l.SelectOption = True Select l).Count
                If cont = 0 Then
                    banValidate = True
                End If
            Else
                banValidate = True
            End If
            If banValidate Then
                IsLoading(False)
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione al menos un item de " & INDlyItemControlRuleType.Text & "."
                Exit Sub
            End If
            ListGroupersCupsAdd = New List(Of GroupersCups)()
            Dim listValidation As New StringBuilder()
            Select Case RuleType
                Case 2 'CUPS
                    For Each i As CupsEntityXpo In (From l In ListXpCollection Where l.SelectOption = True Select l)
                        If ListGroupersCupsValidate IsNot Nothing AndAlso ListGroupersCupsValidate.Any(Function(x) x.CUPSEntityId = i.Id) Then
                            listValidation.AppendLine(String.Format("El CUPS {0} ya se encuentra en el listado.",
                                                                                    String.Concat(i.Code, " - ", i.Description)))
                            Continue For
                        End If
                        Dim groupersCups = New GroupersCups()
                        With groupersCups
                            .CUPSEntityId = i.Id
                            .DescriptionCups = i.Description
                            .CupsSubGroupCodeName = i.CUPSSubGroupId.CodeName
                            .CupsGroupCodeName = i.CUPSSubGroupId.CupsGroupId.CodeName
                        End With
                        ListGroupersCupsAdd.Add(groupersCups)
                    Next
                Case 3 'SubGroup
                    For Each i As CupsSubGroupXpo In (From l In ListXpCollection Where l.SelectOption = True Select l)
                        If i.CupsEntityXpo IsNot Nothing AndAlso i.CupsEntityXpo.Count > 0 Then
                            For Each c As CupsEntityXpo In i.CupsEntityXpo
                                If ListGroupersCupsValidate IsNot Nothing AndAlso ListGroupersCupsValidate.Any(Function(x) x.CUPSEntityId = c.Id) Then
                                    listValidation.AppendLine(String.Format("El CUPS {0} ya se encuentra en el listado.",
                                                                                            String.Concat(c.Code, " - ", c.Description)))
                                    Continue For
                                End If
                                Dim groupersCups = New GroupersCups()
                                With groupersCups
                                    .CUPSEntityId = c.Id
                                    .DescriptionCups = c.Description
                                    .CupsSubGroupCodeName = c.CUPSSubGroupId.CodeName
                                    .CupsGroupCodeName = c.CUPSSubGroupId.CupsGroupId.CodeName
                                End With
                                ListGroupersCupsAdd.Add(groupersCups)
                            Next
                        End If
                    Next
                Case 4 'Group
                    For Each g As CupsGroupXpo In (From l In ListXpCollection Where l.SelectOption = True Select l)
                        If g.CupsSubGroupXpo IsNot Nothing AndAlso g.CupsSubGroupXpo.Count > 0 Then

                            For Each i As CupsSubGroupXpo In g.CupsSubGroupXpo
                                If i.CupsEntityXpo IsNot Nothing AndAlso i.CupsEntityXpo.Count > 0 Then
                                    For Each c As CupsEntityXpo In i.CupsEntityXpo
                                        If ListGroupersCupsValidate IsNot Nothing AndAlso ListGroupersCupsValidate.Any(Function(x) x.CUPSEntityId = c.Id) Then
                                            listValidation.AppendLine(String.Format("El CUPS {0} ya se encuentra en el listado.",
                                                                                                    String.Concat(c.Code, " - ", c.Description)))
                                            Continue For
                                        End If
                                        Dim groupersCups = New GroupersCups()
                                        With groupersCups
                                            .CUPSEntityId = c.Id
                                            .DescriptionCups = c.Description
                                            .CupsSubGroupCodeName = c.CUPSSubGroupId.CodeName
                                            .CupsGroupCodeName = c.CUPSSubGroupId.CupsGroupId.CodeName
                                        End With
                                        ListGroupersCupsAdd.Add(groupersCups)
                                    Next
                                End If
                            Next

                        End If
                    Next
            End Select
            IsLoading(False)
            If listValidation.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listValidation.ToString()
                Exit Sub
            End If
            RaiseEvent RefreshDatasourceHandler(ListGroupersCupsAdd)
            Me.Close()
        Catch ex As Exception
            IsLoading(False)
            Throw ex
        End Try
    End Sub
#End Region

End Class
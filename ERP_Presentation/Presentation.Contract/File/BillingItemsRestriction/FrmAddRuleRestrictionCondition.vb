'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Giovanny Plazas 
' Created          : 17/12/2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Presentation.Contract.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base
#End Region


Public Class FrmAddRuleRestrictionCondition
    Implements IAddRuleRestrictionCondition

#Region "Builder"
    ''' <summary>
    ''' constructor con una sola condicion
    ''' </summary>
    ''' <param name="ConditionType1">Tipo de primera condicion</param>
    Public Sub New(ConditionType1 As Utils.EItemsRestrictionConditionType)
        InitializeComponent()
        ShowControlTypeCondition1(ConditionType1)
    End Sub

    ''' <summary>
    ''' constructor con ambas condicion
    ''' </summary>
    ''' <param name="ConditionType1">Tipo de primera condicion</param>
    ''' <param name="ConditionType2">tipo segunda condicion si flagBoth es true esta debe ser diferente nothing</param>
    Public Sub New(ConditionType1 As Utils.EItemsRestrictionConditionType,
                   ConditionType2 As Utils.EItemsRestrictionConditionType)

        InitializeComponent()
        ShowControlTypeCondition1(ConditionType1)
        ShowControlTypeCondition2(ConditionType2)
    End Sub
#End Region

#Region "Properties and Variables"
    Private listEqualsOperator As New List(Of Tuple(Of Byte, String))

    Private presenter As PAddRuleRestrictionCondition

    Private _billingItemsRestrictionDetailCondition As BillingItemsRestrictionDetailCondition
    Private _headerLogicalOperator As String
    Private _editMode As Boolean
    Private _conditionType1 As Integer?
    Private _conditionType2 As Integer?

    Private Property Operator1() As Byte? Implements IAddRuleRestrictionCondition.Operator1
        Get
            Return INDSleOperator1.EditValue
        End Get
        Set(value As Byte?)
            INDSleOperator1.EditValue = value
        End Set
    End Property

    Private Property Operator2() As Byte? Implements IAddRuleRestrictionCondition.Operator2
        Get
            Return INDSleOperator2.EditValue
        End Get
        Set(value As Byte?)
            INDSleOperator2.EditValue = value
        End Set
    End Property

    Private WriteOnly Property DataSourceOperators As List(Of Tuple(Of Byte, String)) Implements IAddRuleRestrictionCondition.DataSourceOperators
        Set(value As List(Of Tuple(Of Byte, String)))
            INDSleOperator1.Properties.DataSource = value
            INDSleOperator2.Properties.DataSource = value
        End Set
    End Property

    Private Property FunctionalUnit1 As Integer? Implements IAddRuleRestrictionCondition.FunctionalUnit1
        Get
            Return INDSleFunctionalUnit1.EditValue
        End Get
        Set(value As Integer?)
            INDSleFunctionalUnit1.EditValue = value
        End Set
    End Property

    Private ReadOnly Property FunctionalUnit1CodeName As String
        Get
            Return If(String.IsNullOrEmpty(Me.INDSleFunctionalUnit1.Text), Me.INDSleFunctionalUnit1.Properties.NullText, Me.INDSleFunctionalUnit1.Text)
        End Get
    End Property

    Private Property FunctionalUnit2 As Integer? Implements IAddRuleRestrictionCondition.FunctionalUnit2
        Get
            Return INDSleFunctionalUnit2.EditValue
        End Get
        Set(value As Integer?)
            INDSleFunctionalUnit2.EditValue = value
        End Set
    End Property

    Private ReadOnly Property FunctionalUnit2CodeName As String
        Get
            Return If(String.IsNullOrEmpty(Me.INDSleFunctionalUnit2.Text), Me.INDSleFunctionalUnit2.Properties.NullText, Me.INDSleFunctionalUnit2.Text)
        End Get
    End Property

    Private Property DataSourceFunctionalUnit1 As XPInstantFeedbackSource Implements IAddRuleRestrictionCondition.DataSourceFunctionalUnit1
        Get
            Return INDSleFunctionalUnit1.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleFunctionalUnit1.Properties.DataSource = value
        End Set
    End Property

    Private Property DataSourceFunctionalUnit2 As XPInstantFeedbackSource Implements IAddRuleRestrictionCondition.DataSourceFunctionalUnit2
        Get
            Return INDSleFunctionalUnit2.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleFunctionalUnit2.Properties.DataSource = value
        End Set
    End Property

    Private Property FunctionalUnitType1 As Byte? Implements IAddRuleRestrictionCondition.FunctionalUnitType1
        Get
            Return INDSleFunctionalUnitType1.EditValue
        End Get
        Set(value As Byte?)
            INDSleFunctionalUnitType1.EditValue = value
        End Set
    End Property

    Private Property FunctionalUnitType2 As Byte? Implements IAddRuleRestrictionCondition.FunctionalUnitType2
        Get
            Return INDSleFunctionalUnitType2.EditValue
        End Get
        Set(value As Byte?)
            INDSleFunctionalUnitType2.EditValue = value
        End Set
    End Property

    Private WriteOnly Property DataSourceFunctionalUnitType As List(Of Tuple(Of Byte, String)) Implements IAddRuleRestrictionCondition.DataSourceFunctionalUnitType
        Set(value As List(Of Tuple(Of Byte, String)))
            INDSleFunctionalUnitType1.Properties.DataSource = value
            INDSleFunctionalUnitType2.Properties.DataSource = value
        End Set
    End Property

    Private Property StayType1 As String Implements IAddRuleRestrictionCondition.StayType1
        Get
            Return INDSleStayType1.EditValue
        End Get
        Set(value As String)
            INDSleStayType1.EditValue = value
        End Set
    End Property

    Private ReadOnly Property StayType1CodeName As String
        Get
            Return If(String.IsNullOrEmpty(Me.INDSleStayType1.Text), Me.INDSleStayType1.Properties.NullText, Me.INDSleStayType1.Text)
        End Get
    End Property

    Private Property StayType2 As String Implements IAddRuleRestrictionCondition.StayType2
        Get
            Return INDSleStayType2.EditValue
        End Get
        Set(value As String)
            INDSleStayType2.EditValue = value
        End Set
    End Property

    Private ReadOnly Property StayType2CodeName As String
        Get
            Return If(String.IsNullOrEmpty(Me.INDSleStayType2.Text), Me.INDSleStayType2.Properties.NullText, Me.INDSleStayType2.Text)
        End Get
    End Property

    Private Property DataSourceStayType1 As XPInstantFeedbackSource Implements IAddRuleRestrictionCondition.DataSourceStayType1
        Get
            Return INDSleStayType1.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleStayType1.Properties.DataSource = value
        End Set
    End Property

    Private Property DataSourceStayType2 As XPInstantFeedbackSource Implements IAddRuleRestrictionCondition.DataSourceStayType2
        Get
            Return INDSleStayType2.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleStayType2.Properties.DataSource = value
        End Set
    End Property

    Private Property ManualRateType1 As Byte? Implements IAddRuleRestrictionCondition.ManualRateType1
        Get
            Return INDSleManualRateType1.EditValue
        End Get
        Set(value As Byte?)
            INDSleManualRateType1.EditValue = value
        End Set
    End Property

    Private Property ManualRateType2 As Byte? Implements IAddRuleRestrictionCondition.ManualRateType2
        Get
            Return INDSleManualRateType2.EditValue
        End Get
        Set(value As Byte?)
            INDSleManualRateType2.EditValue = value
        End Set
    End Property

    Private WriteOnly Property DataSourceManualRateType As List(Of Tuple(Of Byte, String)) Implements IAddRuleRestrictionCondition.DataSourceManualRateType
        Set(value As List(Of Tuple(Of Byte, String)))
            INDSleManualRateType1.Properties.DataSource = value
            INDSleManualRateType2.Properties.DataSource = value
        End Set
    End Property

    Private Property QxGroup1 As Integer? Implements IAddRuleRestrictionCondition.QxGroup1
        Get
            Return INDSleQxGroup1.EditValue
        End Get
        Set(value As Integer?)
            INDSleQxGroup1.EditValue = value
        End Set
    End Property

    Private ReadOnly Property QxGroup1CodeName As String
        Get
            Return If(String.IsNullOrEmpty(Me.INDSleQxGroup1.Text), Me.INDSleQxGroup1.Properties.NullText, Me.INDSleQxGroup1.Text)
        End Get
    End Property

    Private Property QxGroup2 As Integer? Implements IAddRuleRestrictionCondition.QxGroup2
        Get
            Return INDSleQxGroup2.EditValue
        End Get
        Set(value As Integer?)
            INDSleQxGroup2.EditValue = value
        End Set
    End Property

    Private ReadOnly Property QxGroup2CodeName As String
        Get
            Return If(String.IsNullOrEmpty(Me.INDSleQxGroup2.Text), Me.INDSleQxGroup2.Properties.NullText, Me.INDSleQxGroup2.Text)
        End Get
    End Property

    Private Property DataSourceQxGroup1 As XPInstantFeedbackSource Implements IAddRuleRestrictionCondition.DataSourceQxGroup1
        Get
            Return INDSleQxGroup1.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleQxGroup1.Properties.DataSource = value
        End Set
    End Property

    Private Property DataSourceQxGroup2 As XPInstantFeedbackSource Implements IAddRuleRestrictionCondition.DataSourceQxGroup2
        Get
            Return INDSleQxGroup2.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleQxGroup2.Properties.DataSource = value
        End Set
    End Property

    Private Property UVRRange1 As Integer? Implements IAddRuleRestrictionCondition.UVRRange1
        Get
            Return INDSleUVRRange1.EditValue
        End Get
        Set(value As Integer?)
            INDSleUVRRange1.EditValue = value
        End Set
    End Property

    Private ReadOnly Property UVRRange1CodeName As String
        Get
            Return If(String.IsNullOrEmpty(Me.INDSleUVRRange1.Text), Me.INDSleUVRRange1.Properties.NullText, Me.INDSleUVRRange1.Text)
        End Get
    End Property

    Private Property UVRRange2 As Integer? Implements IAddRuleRestrictionCondition.UVRRange2
        Get
            Return INDSleUVRRange2.EditValue
        End Get
        Set(value As Integer?)
            INDSleUVRRange2.EditValue = value
        End Set
    End Property

    Private ReadOnly Property UVRRange2CodeName As String
        Get
            Return If(String.IsNullOrEmpty(Me.INDSleUVRRange2.Text), Me.INDSleUVRRange2.Properties.NullText, Me.INDSleUVRRange2.Text)
        End Get
    End Property

    Private Property DataSourceUVRRange1 As XPInstantFeedbackSource Implements IAddRuleRestrictionCondition.DataSourceUVRRange1
        Get
            Return INDSleUVRRange1.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleUVRRange1.Properties.DataSource = value
        End Set
    End Property

    Private Property DataSourceUVRRange2 As XPInstantFeedbackSource Implements IAddRuleRestrictionCondition.DataSourceUVRRange2
        Get
            Return INDSleUVRRange2.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleUVRRange2.Properties.DataSource = value
        End Set
    End Property

    Public Property BillingItemsRestrictionDetailCondition As BillingItemsRestrictionDetailCondition
        Get
            Return _billingItemsRestrictionDetailCondition
        End Get
        Set(value As BillingItemsRestrictionDetailCondition)
            _billingItemsRestrictionDetailCondition = value
        End Set
    End Property

    Public Property EditMode As Boolean
        Get
            Return _editMode
        End Get
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    Public WriteOnly Property HeaderLogicOperator As String
        Set(value As String)
            _headerLogicalOperator = value
        End Set
    End Property
#End Region

#Region "Events"

    ''' <summary>
    ''' Evento publico para agregar una regla a
    ''' la rejilla del form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddInfoToGridBillingItemsRestrictionDetailCondition(sender As Object, e As AddInfoToGridBillingItemsRestrictionCondition)

#Region "Load"
    Private Sub FrmAddRuleRestrictionCondition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.TuplesDataSource()
        Me.presenter = New PAddRuleRestrictionCondition(Me)
        Me.CleanControls(Me.EditMode)
        If Me.EditMode Then
            Me.LoadControls()
        End If
    End Sub
#End Region

#Region "QueryPopUp"

    Private Sub INDSleFunctionalUnit1_Properties_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnit1.Properties.QueryPopUp
        If Me.DataSourceFunctionalUnit1 Is Nothing Then
            Me.DataSourceFunctionalUnit1 = presenter.ListFunctionalUnit()
        End If
    End Sub

    Private Sub INDSleFunctionalUnit2_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnit2.QueryPopUp
        If Me.DataSourceFunctionalUnit2 Is Nothing Then
            Me.DataSourceFunctionalUnit2 = presenter.ListFunctionalUnit()
        End If
    End Sub

    Private Sub INDSleStayType1_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleStayType1.QueryPopUp
        If Me.DataSourceStayType1 Is Nothing Then
            Me.DataSourceStayType1 = presenter.GetAllStayType()
        End If
    End Sub

    Private Sub INDSleStayType2_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleStayType2.QueryPopUp
        If Me.DataSourceStayType2 Is Nothing Then
            Me.DataSourceStayType2 = presenter.GetAllStayType()
        End If
    End Sub

    Private Sub INDSleQxGroup1_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleQxGroup1.QueryPopUp
        If Me.DataSourceQxGroup1 Is Nothing Then
            Me.DataSourceQxGroup1 = presenter.ListSurgicalGroupByStatus()
        End If
    End Sub

    Private Sub INDSleQxGroup2_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleQxGroup2.QueryPopUp
        If Me.DataSourceQxGroup2 Is Nothing Then
            Me.DataSourceQxGroup2 = presenter.ListSurgicalGroupByStatus()
        End If
    End Sub

    Private Sub INDSleUVRRange1_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUVRRange1.QueryPopUp
        If Me.DataSourceUVRRange1 Is Nothing Then
            Me.DataSourceUVRRange1 = presenter.ListUVRRangeByStatus()
        End If
    End Sub

    Private Sub INDSleUVRRange2_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUVRRange2.QueryPopUp
        If Me.DataSourceUVRRange2 Is Nothing Then
            Me.DataSourceUVRRange2 = presenter.ListUVRRangeByStatus()
        End If
    End Sub
#End Region


#End Region

#Region "Methods"
    Private Sub TuplesDataSource()
        'operadores de igualdad
        Me.DataSourceOperators = Utils.EqualsOperator

        'Tipo de Unidades
        DataSourceFunctionalUnitType = Utils.UnitTypes

        Me.DataSourceManualRateType = Utils.RateManualType
    End Sub

    Private Sub ShowControlTypeCondition1(ConditionType As Utils.EItemsRestrictionConditionType)
        Me._conditionType1 = ConditionType
        Select Case ConditionType
            Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                INDLciFunctionalUnit1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                INDLciFunctionalUnitType1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.StayType
                INDLciStayType1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.RateManualType
                INDLciManualRateType1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.QxGroup
                INDLciQxGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.UVRRange
                INDLciUVRRange1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End Select
    End Sub

    Private Sub ShowControlTypeCondition2(ConditionType2 As Utils.EItemsRestrictionConditionType)
        Me._conditionType2 = ConditionType2
        INDLcgCondition2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Select Case ConditionType2
            Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                INDLciFunctionalUnit2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                INDLciFunctionalUnitType2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.StayType
                INDLciStayType2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.RateManualType
                INDLciManualRateType2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.QxGroup
                INDLciQxGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Utils.EItemsRestrictionConditionType.UVRRange
                INDLciUVRRange2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End Select
    End Sub

    Private Sub CleanControls(Optional editMode As Boolean = False)
        Me.Operator1 = Nothing
        Me.Operator2 = Nothing
        Me.FunctionalUnit1 = Nothing
        Me.FunctionalUnit2 = Nothing
        Me.FunctionalUnitType1 = Nothing
        Me.FunctionalUnitType2 = Nothing
        Me.StayType1 = Nothing
        Me.StayType2 = Nothing
        Me.ManualRateType1 = Nothing
        Me.ManualRateType2 = Nothing
        Me.QxGroup1 = Nothing
        Me.QxGroup2 = Nothing
        Me.UVRRange1 = Nothing
        Me.UVRRange2 = Nothing
        Me.EditMode = editMode
        Me.INDSleFunctionalUnit1.Properties.NullText = Nothing
        Me.INDSleFunctionalUnit2.Properties.NullText = Nothing
        Me.INDSleQxGroup1.Properties.NullText = Nothing
        Me.INDSleQxGroup2.Properties.NullText = Nothing
        Me.INDSleStayType1.Properties.NullText = Nothing
        Me.INDSleStayType2.Properties.NullText = Nothing
        Me.INDSleUVRRange1.Properties.NullText = Nothing
        Me.INDSleUVRRange2.Properties.NullText = Nothing
    End Sub

    ''' <summary>
    ''' evento para agrega a la rejilla del detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnFrmAddRuleRestrictionCondition_Click(sender As Object, e As EventArgs) Handles INDbtnFrmAddRuleRestrictionCondition.Click

        Dim errors = ValidateToAdd()
        If Not String.IsNullOrEmpty(errors) Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        AssignValues()
        'Se instancia el objeto que se envia al evento
        Dim args As New AddInfoToGridBillingItemsRestrictionCondition With {.BillingItemsRestrictionDetailCondition = Me.BillingItemsRestrictionDetailCondition, .ReturnValueOk = True, .ModeEdit = Me.EditMode}
        RaiseEvent AddInfoToGridBillingItemsRestrictionDetailCondition(Nothing, args)

        AsyncLoader(False)

        If args.ReturnValueOk Then
            CleanControls()
        End If
    End Sub

    Private Function ValidateToAdd() As String

        Dim listErrors = New StringBuilder

        If EditMode AndAlso Me.BillingItemsRestrictionDetailCondition Is Nothing Then
            listErrors.AppendLine("El objeto de la condicion esta vacio y se esta en modo edición")
        End If

        Dim flag As Boolean
        Select Case _conditionType1
            Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                flag = FunctionalUnit1 Is Nothing
            Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                flag = Me.FunctionalUnitType1 Is Nothing
            Case Utils.EItemsRestrictionConditionType.QxGroup
                flag = Me.QxGroup1 Is Nothing
            Case Utils.EItemsRestrictionConditionType.RateManualType
                flag = Me.ManualRateType1 Is Nothing
            Case Utils.EItemsRestrictionConditionType.StayType
                flag = String.IsNullOrEmpty(Me.StayType1)
            Case Utils.EItemsRestrictionConditionType.UVRRange
                flag = Me.UVRRange1 Is Nothing
            Case Else
                flag = True
        End Select

        If Me.Operator1 Is Nothing OrElse flag Then
            listErrors.AppendLine("Debe diligenciar los campos pedidos en la primera condicion")
        End If

        If INDLcgCondition2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso _conditionType2 Is Nothing Then

            listErrors.AppendLine("El segmento de segunda condición esta habilitado pero no tienen informacion suficiente")

        End If

        If _conditionType2 IsNot Nothing Then
            flag = False

            Select Case _conditionType2
                Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                    flag = FunctionalUnit2 Is Nothing
                Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                    flag = Me.FunctionalUnitType2 Is Nothing
                Case Utils.EItemsRestrictionConditionType.QxGroup
                    flag = Me.QxGroup2 Is Nothing
                Case Utils.EItemsRestrictionConditionType.RateManualType
                    flag = Me.ManualRateType2 Is Nothing
                Case Utils.EItemsRestrictionConditionType.StayType
                    flag = String.IsNullOrEmpty(Me.StayType2)
                Case Utils.EItemsRestrictionConditionType.UVRRange
                    flag = Me.UVRRange2 Is Nothing
                Case Else
                    flag = True
            End Select

            If Me.Operator2 Is Nothing OrElse flag Then
                listErrors.AppendLine("Debe diligenciar los campos pedidos en la segunda condicion")
            End If
        End If

        Return listErrors.ToString()

    End Function

    Private Sub AssignValues()
        If Not Me.EditMode Then
            Me.BillingItemsRestrictionDetailCondition = New BillingItemsRestrictionDetailCondition
        End If

        With Me.BillingItemsRestrictionDetailCondition
            .Operator = Me.Operator1
            .Operator2 = Me.Operator2
            .FunctionalUnitId = Me.FunctionalUnit1
            .FunctionalUnitId2 = Me.FunctionalUnit2
            .UnitTypeId = Me.FunctionalUnitType1
            .UnitTypeId2 = Me.FunctionalUnitType2
            .StayType = Me.StayType1
            .StayType2 = Me.StayType2
            .SurgicalGroupId = Me.QxGroup1
            .SurgicalGroupId2 = Me.QxGroup2
            .ManualType = Me.ManualRateType1
            .ManualType2 = Me.ManualRateType2
            .UVRRangeId = Me.UVRRange1
            .UVRRangeId2 = Me.UVRRange2
            Select Case _conditionType1
                Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                    .ConditionName = Me.FunctionalUnit1CodeName
                    .FunctionalUnitCodeName = Me.FunctionalUnit1CodeName
                Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                    .ConditionName = Me.INDSleFunctionalUnitType1.Text
                Case Utils.EItemsRestrictionConditionType.QxGroup
                    .ConditionName = Me.QxGroup1CodeName
                    .SurgicalGroupCodeName = Me.QxGroup1CodeName
                Case Utils.EItemsRestrictionConditionType.RateManualType
                    .ConditionName = Me.INDSleManualRateType1.Text
                Case Utils.EItemsRestrictionConditionType.StayType
                    .ConditionName = Me.StayType1CodeName
                    .StayTypeCodeName = Me.StayType1CodeName
                Case Utils.EItemsRestrictionConditionType.UVRRange
                    .ConditionName = Me.UVRRange1CodeName
                    .UVRRangeCodeName = Me.UVRRange1CodeName
            End Select

            .ConditionName &= $" ({Me.INDSleOperator1.Text}) {Me._headerLogicalOperator} "

            If _conditionType2 IsNot Nothing Then
                Select Case _conditionType2
                    Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                        .ConditionName &= Me.FunctionalUnit2CodeName
                        .FunctionalUnitCodeName = Me.FunctionalUnit2CodeName
                    Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                        .ConditionName &= Me.INDSleFunctionalUnitType2.Text
                    Case Utils.EItemsRestrictionConditionType.QxGroup
                        .ConditionName &= Me.QxGroup2CodeName
                        .SurgicalGroupCodeName = Me.QxGroup2CodeName
                    Case Utils.EItemsRestrictionConditionType.RateManualType
                        .ConditionName &= Me.INDSleManualRateType2.Text
                    Case Utils.EItemsRestrictionConditionType.StayType
                        .ConditionName &= Me.StayType2CodeName
                        .StayTypeCodeName = Me.StayType2CodeName
                    Case Utils.EItemsRestrictionConditionType.UVRRange
                        .ConditionName &= Me.UVRRange2CodeName
                        .UVRRangeCodeName = Me.UVRRange2CodeName
                End Select
                .ConditionName &= $" ({Me.INDSleOperator2.Text})"
            End If
        End With
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub LoadControls()
        If Me.EditMode AndAlso Me.BillingItemsRestrictionDetailCondition Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Se esta en modo de edicion pero no se cargó el detalle"
            Me.Close()
            Exit Sub
        End If
        With Me.BillingItemsRestrictionDetailCondition
            Me.Operator1 = .Operator
            Select Case _conditionType1
                Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                    Me.FunctionalUnit1 = .FunctionalUnitId
                    Me.INDSleFunctionalUnit1.Properties.NullText = .FunctionalUnitCodeName
                Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                    Me.FunctionalUnitType1 = .UnitTypeId
                Case Utils.EItemsRestrictionConditionType.QxGroup
                    Me.QxGroup1 = .SurgicalGroupId
                    Me.INDSleQxGroup1.Properties.NullText = .SurgicalGroupCodeName
                Case Utils.EItemsRestrictionConditionType.RateManualType
                    Me.ManualRateType1 = .ManualType
                Case Utils.EItemsRestrictionConditionType.StayType
                    Me.StayType1 = .StayType
                    Me.INDSleStayType1.Properties.NullText = .StayTypeCodeName
                Case Utils.EItemsRestrictionConditionType.UVRRange
                    Me.UVRRange1 = .UVRRangeId
                    Me.INDSleUVRRange1.Properties.NullText = Me.UVRRange1CodeName
            End Select


            If _conditionType2 IsNot Nothing AndAlso _conditionType2 <> Utils.EItemsRestrictionConditionType.Nothing Then
                Me.Operator2 = .Operator2
                Select Case _conditionType2
                    Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                        Me.FunctionalUnit2 = .FunctionalUnitId2
                        Me.INDSleFunctionalUnit2.Properties.NullText = .FunctionalUnitCodeName
                    Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                        Me.FunctionalUnitType2 = .UnitTypeId2
                    Case Utils.EItemsRestrictionConditionType.QxGroup
                        Me.QxGroup2 = .SurgicalGroupId2
                        Me.INDSleQxGroup2.Properties.NullText = .SurgicalGroupCodeName
                    Case Utils.EItemsRestrictionConditionType.RateManualType
                        Me.ManualRateType2 = .ManualType2
                    Case Utils.EItemsRestrictionConditionType.StayType
                        Me.StayType2 = .StayType2
                        Me.INDSleStayType2.Properties.NullText = .StayTypeCodeName
                    Case Utils.EItemsRestrictionConditionType.UVRRange
                        Me.UVRRange2 = .UVRRangeId
                        Me.INDSleUVRRange2.Properties.NullText = .UVRRangeCodeName
                End Select
            End If
        End With
    End Sub

#End Region
#Region "ICrudBase"
    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

    Public Sub Buscar() Implements ICrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Me.CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

End Class

Public Class AddInfoToGridBillingItemsRestrictionCondition
    Inherits EventArgs


    Property BillingItemsRestrictionDetailCondition As BillingItemsRestrictionDetailCondition


    Property ReturnValueOk As Boolean


    Property ModeEdit As Boolean

End Class

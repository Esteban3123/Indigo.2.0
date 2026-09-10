'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Inventory.MVP
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Payroll
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Accounting.MVP
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmAddRule
    Implements IAddRule

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler bw.DoWork, AddressOf bw_DoWork
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
    End Sub

#End Region

#Region "PublicEvents"

    ''' <summary>
    ''' Evento publico para agregar una regla a
    ''' la rejilla del form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddInfoToGridFormPrincipal(sender As Object, e As AddInfoToGridFormPrincipal)

#End Region

#Region "Properties"
    Private _companySettings As Task(Of CompanySettings)

    ''' <summary>
    ''' Asyncrono
    ''' </summary>
    ''' <remarks></remarks>
    Private bw As BackgroundWorker = New BackgroundWorker


    ''' <summary>
    ''' Obtiene o establece si permite cambiar valores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductGroupId As Integer? Implements IAddRule.ProductGroupId
        Get
            Return INDSleProductGroup.EditValue
        End Get
        Set(value As Integer?)
            INDSleProductGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de manual del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductTypeId As Integer? Implements IAddRule.ProductTypeId
        Get
            Return INDSleProductType.EditValue
        End Get
        Set(value As Integer?)
            INDSleProductType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductSubGroupId As Integer? Implements IAddRule.ProductSubGroupId
        Get
            Return INDSleSubgroupProduct.EditValue
        End Get
        Set(value As Integer?)
            INDSleSubgroupProduct.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el tipo manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConditionType As Integer Implements IAddRule.ConditionType
        Get
            Return INDsleConditionType.EditValue
        End Get
        Set(value As Integer)
            INDsleConditionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As DateTime Implements IAddRule.InitialDate
        Get
            Return INDtxtInitialDate.EditValue
        End Get
        Set(value As DateTime)
            INDtxtInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As DateTime Implements IAddRule.EndDate
        Get
            Return INDtxtEndDate.EditValue
        End Get
        Set(value As Date)
            INDtxtEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValue As Decimal? Implements IAddRule.SalesValue
        Get
            Return INDtxtSalesValue.EditValue
        End Get
        Set(value As Decimal?)
            INDtxtSalesValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RuleType As Integer Implements IAddRule.RuleType
        Get
            Return INDsleRuleType.EditValue
        End Get
        Set(value As Integer)
            INDsleRuleType.EditValue = value
        End Set
    End Property


    Public Property RateType As Integer Implements IAddRule.RateType
        Get
            Return INDSleRateType.EditValue
        End Get
        Set(value As Integer)
            INDSleRateType.EditValue = value
        End Set
    End Property

    Public Property PercentageBasedOn As Integer? Implements IAddRule.PercentageBasedOn
        Get
            Return INDslePercentageBasedOn.EditValue
        End Get
        Set(value As Integer?)
            INDslePercentageBasedOn.EditValue = value
        End Set
    End Property

    Public Property Percentage As Decimal? Implements IAddRule.Percentage
        Get
            Return INDtxtGeneralPercentage.EditValue
        End Get
        Set(value As Decimal?)
            INDtxtGeneralPercentage.EditValue = value
        End Set
    End Property

    Public Property Observations As String Implements IAddRule.Observations
        Get
            Return INDTxtObservation.EditValue
        End Get
        Set(value As String)
            INDTxtObservation.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Tupla para el tipo de regla
    ''' </summary>
    Dim ListRuleType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el tipo de tarifa
    ''' </summary>
    Dim ListRateType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el tipo de  porcentage
    ''' </summary>
    Dim ListPercentageBasedOn As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el tipo de condicion
    ''' </summary>
    Dim ListConditionType As New List(Of Tuple(Of Integer, String))


    ''' <summary>
    ''' Variable que se utiliza para obtener los tipos de filtro
    ''' </summary>
    Private productTypeSelectedRows As String

    ''' <summary>
    ''' Variable que se utiliza para obtener los tipos de filtro
    ''' </summary>
    Private productGroupSelectedRows As String

    ''' <summary>
    ''' Variable que se utiliza para obtener los tipos de filtro
    ''' </summary>
    Private productSubGroupSelectedRows As String

    ''' <summary>
    ''' Variable que se utiliza para obtener los tipos de filtro
    ''' </summary>
    Private ListNewCondition As List(Of ProductRateGeneralCondition)
    Private ListDeleteCondition As List(Of ProductRateGeneralCondition)


    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim _productConditionRate As List(Of ProductRateGeneral)

    Dim nameEntities As List(Of String)

    Dim editModeDetails As Boolean
    ''' <summary>
    ''' Establece si el registro es para modificar o guardar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ModeEditAll As Boolean
    ''' <summary>
    ''' Establece si el registro es para visualizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IsViewMode As Boolean

    Property ProductRateGeneralEdit As ProductRateGeneral

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

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

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Function LoadControls() As Task
        Try
            AsyncLoader(True)
            If ProductRateGeneralEdit IsNot Nothing Then

                RuleType = ProductRateGeneralEdit.RuleType
                ProductTypeId = ProductRateGeneralEdit.ProductTypeId
                If (ProductRateGeneralEdit.ProductTypeId > 0) Then
                    ProductGroupId = ProductRateGeneralEdit.ProductTypeId
                    INDSleProductType.Properties.NullText = ProductRateGeneralEdit.EntityName
                End If
                If (ProductRateGeneralEdit.ProductGroupId > 0) Then
                    ProductGroupId = ProductRateGeneralEdit.ProductGroupId
                    INDSleProductGroup.Properties.NullText = ProductRateGeneralEdit.EntityName
                End If
                If (ProductRateGeneralEdit.ProductSubGroupId > 0) Then
                    ProductSubGroupId = ProductRateGeneralEdit.ProductSubGroupId
                    INDSleSubgroupProduct.Properties.NullText = ProductRateGeneralEdit.EntityName
                End If
                InitialDate = ProductRateGeneralEdit.InitialDate
                EndDate = ProductRateGeneralEdit.EndDate
                RateType = ProductRateGeneralEdit.RateType
                ListNewCondition = ProductRateGeneralEdit.ProductRateGeneralCondition.ToList()
                INDgcRates.DataSource = ListNewCondition
                INDgcRates.RefreshDataSource()
                PercentageBasedOn = ProductRateGeneralEdit.PercentageBasedOn
                ConditionType = ProductRateGeneralEdit.ConditionType
                Percentage = ProductRateGeneralEdit.Percentage
                SalesValue = ProductRateGeneralEdit.SalesValue
                Observations = ProductRateGeneralEdit.Observations
                If IsViewMode Then
                    enableControls(False)
                Else
                    enableControls(True)
                End If
                AsyncLoader(False)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = "No se pudo cargar la información"
            Exit Function
        End Try
    End Function

    Private Sub getCompanySettings()
        Using model As New MCompanySettings(Tag)
            _companySettings = model.GetCompanySettings()
        End Using
    End Sub

    Private Sub enableControls(state As Boolean)
        INDlyItemRuleType.Enabled = state
        INDlyItemPorductType.Enabled = state
        INDlyItemPorductGroup.Enabled = state
        INDlyItemSubgroupProduct.Enabled = state
        INDlyItemRateType.Enabled = state
        INDlyItemRateType.Enabled = state
        INDlyItemObservation.Enabled = state
        INDlyitemConditionType.Enabled = state
        LayoutControlItem1.Enabled = state
        LayoutControlItem2.Enabled = state
        INDlyItemSalesValue.Enabled = state
        INDLyGeneralPercentage.Enabled = state
        INDpceRates.Enabled = state
        INDgcRates.Enabled = state
        INDlygRate.Enabled = state
        INDbtnAddRule.Enabled = state
        INDlyitemPercentageBasedOn.Enabled = state
    End Sub

    Private Sub AssignValues()

        If viewProductType?.GetSelectedRows?.Count > 0 Then
            For Each item In viewProductType.GetSelectedRows
                Dim dataId = viewProductType.GetRowCellValue(item, "Id")
                Dim dataName = viewProductType.GetRowCellValue(item, "Name")
                If dataId > 0 Then
                    ContentList(dataId, dataName, 1)
                End If
            Next
        End If
        If viewProductGroup?.GetSelectedRows?.Count > 0 Then
            For Each item In viewProductGroup.GetSelectedRows
                Dim dataId = viewProductGroup.GetRowCellValue(item, "Id")
                Dim dataName = viewProductGroup.GetRowCellValue(item, "Name")
                If dataId > 0 Then
                    ContentList(dataId, dataName, 2)
                End If
            Next
        End If
        If viewProductSubgroup?.GetSelectedRows?.Count > 0 Then
            For Each item In viewProductSubgroup.GetSelectedRows
                Dim dataId = viewProductSubgroup.GetRowCellValue(item, "Id")
                Dim dataName = viewProductSubgroup.GetRowCellValue(item, "Name")
                If dataId > 0 Then
                    ContentList(dataId, dataName, 3)
                End If
            Next
        End If
        Dim args As New AddInfoToGridFormPrincipal With {.ProductRateGeneralReturn = _productConditionRate, .ReturnValueOk = True, .ModeEdit = 0, .ListDeleteDefinitionRateDetailCondition = Nothing}
        RaiseEvent AddInfoToGridFormPrincipal(Nothing, args)

    End Sub

    Public Sub ContentList(item As Integer, Name As String, type As Integer)
        If _productConditionRate Is Nothing Then
            _productConditionRate = New List(Of ProductRateGeneral)
        End If
        Dim newItem As ProductRateGeneral = New ProductRateGeneral
        With newItem
            .RuleType = RuleType
            .ProductTypeId = If(type = 1, item, Nothing)
            .ProductGroupId = If(type = 2, item, Nothing)
            .ProductSubGroupId = If(type = 3, item, Nothing)
            .InitialDate = InitialDate
            .EndDate = EndDate
            .RateType = RateType
            .PercentageBasedOn = If(PercentageBasedOn Is Nothing, 0, PercentageBasedOn)
            .ConditionType = ConditionType
            .Percentage = If(Percentage Is Nothing, 0, Percentage)
            .SalesValue = If(SalesValue Is Nothing, 0, SalesValue)
            .Observations = Observations
            .EntityName = Name
            'Saco los item que fueron agregados o que fueron editados para enviarlos a guardar
            Dim list As Domain.Entities.TrackableCollection(Of ProductRateGeneralCondition) = New Domain.Entities.TrackableCollection(Of ProductRateGeneralCondition)
            If ListNewCondition?.Any() Then
                Dim ListSave As List(Of ProductRateGeneralCondition) = ListNewCondition.FindAll(Function(x) x.ChangeTracker.State = ObjectState.Added OrElse x.ChangeTracker.State = ObjectState.Modified)
                For Each detail In ListSave
                    Dim itemadd As ProductRateGeneralCondition = New ProductRateGeneralCondition
                    itemadd.InitialValue = detail.InitialValue
                    itemadd.EndValue = detail.EndValue
                    itemadd.Percentage = detail.Percentage
                    itemadd.SalesValue = detail.SalesValue
                    .ProductRateGeneralCondition.Add(itemadd)
                Next
            End If
            ''Se vuelven a agregar los detalles eliminados pero con changetracker en delete
            If ListDeleteCondition?.Any() Then
                For Each detail In ListDeleteCondition
                    .ProductRateGeneralCondition.Add(detail)
                Next
            End If

        End With
        If Not _productConditionRate.Contains(newItem) Then
            _productConditionRate.Add(newItem)
        End If
    End Sub


    Private Sub ShowHideControls()
        If RateType > 0 Then
            Select Case RateType
                Case 1
                    INDlyitemPercentageBasedOn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    If ConditionType <> 0 Then
                        If ConditionType = 1 Then
                            INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDlygRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDLyGeneralPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Else
                            INDlygRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            SalesValue = 0
                            INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDLyConditionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDlyConditionPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDColSalesValue.Visible = True
                            INDColSalesValue.VisibleIndex = 2
                            INDColPercentage.Visible = False
                            INDgcRates.RefreshDataSource()
                        End If
                    End If
                Case 2
                    INDlyitemPercentageBasedOn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If ConditionType <> 0 Then
                        If ConditionType = 1 Then
                            INDLyGeneralPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDlygRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Else
                            INDLyGeneralPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            Percentage = 0
                            INDlygRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDLyConditionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDlyConditionPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDColSalesValue.Visible = False
                            INDColPercentage.Visible = True
                            INDColPercentage.VisibleIndex = 2
                            INDgcRates.RefreshDataSource()
                        End If
                    End If
            End Select
        End If
    End Sub


    Public Sub AddCondition()
        If INDtxtInitialValue Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = "El campo de valor inicial es obligatorio"
            Exit Sub
        End If
        If INDtxtEndValue Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = "El campo de valor final es obligatorio"
            Exit Sub
        End If
        If INDLyConditionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDtxtConditionSalesValue.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = "El campo de valor  es obligatorio"
            Exit Sub
        End If
        If INDlyConditionPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDseConditionPercentage.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = "El campo de porcentaje  es obligatorio"
            Exit Sub
        End If
        If INDtxtInitialValue.EditValue > INDtxtEndValue.EditValue Then
            Mensaje(EeventViewerImages.Informacion) = "El valor-condición inicial no puede ser mayor a la final"
            Exit Sub
        End If
        Dim newItem As ProductRateGeneralCondition = New ProductRateGeneralCondition()
        newItem.InitialValue = INDtxtInitialValue.EditValue
        newItem.EndValue = INDtxtEndValue.EditValue
        newItem.SalesValue = INDtxtConditionSalesValue.EditValue
        newItem.Percentage = INDseConditionPercentage.EditValue
        If ListNewCondition Is Nothing Then
            ListNewCondition = New List(Of ProductRateGeneralCondition)
        End If
        ListNewCondition.Add(newItem)
        INDpceRates.ClosePopup()
        INDgcRates.DataSource = ListNewCondition
        INDgcRates.RefreshDataSource()
        CleanControlsPopup()
    End Sub

    ''' <summary>
    ''' Valida los controles del form
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsForm() As String
        Dim listErrors As New StringBuilder
        ''Se valida que haya al menos un item en la rejilla siempre y cuando el grupo de tarifas esta visible

        If Not RuleType > 0 Then
            listErrors.AppendLine("Debe ingresar una clase.")
        End If

        If String.IsNullOrEmpty(InitialDate) Then
            listErrors.AppendLine("Debe ingresar fecha inicial.")
        End If

        If String.IsNullOrEmpty(EndDate) Then
            listErrors.AppendLine("Debe ingresar fecha final.")
        End If
        If Not RateType > 0 Then
            listErrors.AppendLine("Debe ingresar un tipo de tarifa.")
        End If

        If Not ConditionType > 0 Then
            listErrors.AppendLine("Debe ingresar un tipo de condición.")
        End If
        If InitialDate > EndDate Then
            listErrors.AppendLine("La fecha final no puede ser menor a la inicial")
        End If

        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que agrega la regla al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddRule()
        ''Se valida que los controles esten llenos
        If ValidateControls() = False Then
            Exit Sub
        End If

        Dim errors As String = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        Try
            AsyncLoader(True)
            AssignValues()
            CleanControls()
            AsyncLoader(False)

        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Valida el listado cuando se va a modificar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateListModify() As String
        'Dim listErrors As New StringBuilder
        'Dim cont As Integer = 0
        'Select Case RuleType
        '    Case 1 'IPSService
        '        If DefinitionRateDetail.DefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso DefinitionRateDetail.DefinitionRateDetailSurgicalProcedures.Count > 0 Then
        '            Dim listQxIdEdit = (From x In DefinitionRateDetail.DefinitionRateDetailSurgicalProcedures Select x.IPSServiceId).ToList()
        '            Dim listQxCheckNew = (From x In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where x.SelectOption = True AndAlso Not listQxIdEdit.Contains(x.IPSServiceId.Id) Select x).ToList()

        '            Dim listDRDSP As New List(Of DefinitionRateDetailSurgicalProcedures)
        '            For Each itemTemp In (From x In ListValidate Where x.IPSServiceId = DefinitionRateDetail.IPSServiceId AndAlso x.CUPSEntityId = DefinitionRateDetail.CUPSEntityId AndAlso x.DefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso x.DefinitionRateDetailSurgicalProcedures.Count > 0 Select x).ToList()
        '                listDRDSP.AddRange(itemTemp.DefinitionRateDetailSurgicalProcedures)
        '            Next

        '            If listQxCheckNew IsNot Nothing AndAlso listQxCheckNew.Count > 0 AndAlso listDRDSP IsNot Nothing AndAlso listDRDSP.Count > 0 Then
        '                For Each itemSurgical In listQxCheckNew
        '                    If (From x In listDRDSP Where x.IPSServiceId = itemSurgical.IPSServiceId.Id).Count > 0 Then
        '                        listErrors.AppendLine("Ya existe el detalle Qx " + itemSurgical.IPSServiceId.CodeName + " en el listado")
        '                    End If
        '                Next
        '            End If
        '        Else
        '            cont = (From l In ListValidate Where l.IPSServiceId = DefinitionRateDetail.IPSServiceId AndAlso l.CUPSEntityId = DefinitionRateDetail.CUPSEntityId AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
        '        End If
        '    Case 2 'CUPS
        '        cont = (From l In ListValidate Where l.CUPSEntityId = DefinitionRateDetail.CUPSEntityId AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
        '    Case 3 'SubGroup
        '        cont = (From l In ListValidate Where l.CUPSSubgroupId = DefinitionRateDetail.CUPSSubgroupId AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
        '    Case 4 'Group
        '        cont = (From l In ListValidate Where l.CUPSGroupId = DefinitionRateDetail.CUPSGroupId AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
        '    Case 5 'General
        '        cont = (From l In ListValidate Where l.RuleType = DefinitionRateDetail.RuleType AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
        'End Select
        'If cont > 0 Then
        '    If listErrors.ToString().Length = 0 Then
        '        listErrors.AppendLine("La regla " + DefinitionRateDetail.RuleDescription + " ya existe con la primera condición " + INDsleConditionType.Text + " y la segunda condición " + INDsleConditionTypeSecond.Text)
        '    End If
        'End If
        'Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        RuleType = Nothing
        ProductTypeId = Nothing
        ProductGroupId = Nothing
        ProductSubGroupId = Nothing
        InitialDate = Nothing
        EndDate = Nothing
        RateType = Nothing
        PercentageBasedOn = Nothing
        ConditionType = Nothing
        Percentage = 0
        SalesValue = 0
        Observations = Nothing
        ListNewCondition = Nothing
        INDSleProductType.Properties.NullText = ""
        INDSleProductGroup.Properties.NullText = ""
        INDSleSubgroupProduct.Properties.NullText = ""
        INDgcRates.RefreshDataSource()
        viewProductType.ClearSelection()
        viewProductGroup.ClearSelection()
        viewProductSubgroup.ClearSelection()
        INDgcRates.DataSource = Nothing
        INDgcRates.RefreshDataSource()
        INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyitemPercentageBasedOn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLyGeneralPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDtxtInitialValue.EditValue = Nothing
        INDtxtEndValue.EditValue = Nothing
        INDtxtConditionSalesValue.EditValue = Nothing
        INDseConditionPercentage.EditValue = Nothing
    End Sub

    '
    ''' <summary>
    ''' Carga el datasource de las tuplas
    ''' </summary>
    Private Sub InitializeTuples()

        ListRuleType = New List(Of Tuple(Of Integer, String))
        ListRuleType.Add(New Tuple(Of Integer, String)(1, "Tipo de producto"))
        ListRuleType.Add(New Tuple(Of Integer, String)(2, "Grupo de producto"))
        ListRuleType.Add(New Tuple(Of Integer, String)(3, "Subgrupo de producto"))
        INDsleRuleType.Properties.DataSource = ListRuleType.ToList

        ListRateType = New List(Of Tuple(Of Integer, String))
        ListRateType.Add(New Tuple(Of Integer, String)(1, "Tarifa Fija"))
        ListRateType.Add(New Tuple(Of Integer, String)(2, "Basado en porcentaje"))
        INDSleRateType.Properties.DataSource = ListRateType.ToList

        ListPercentageBasedOn = New List(Of Tuple(Of Integer, String))
        ListPercentageBasedOn.Add(New Tuple(Of Integer, String)(0, "N/A"))
        ListPercentageBasedOn.Add(New Tuple(Of Integer, String)(1, "Costo promedio ponderado"))
        ListPercentageBasedOn.Add(New Tuple(Of Integer, String)(2, "Ultimo costo"))
        INDslePercentageBasedOn.Properties.DataSource = ListPercentageBasedOn

        ListConditionType = New List(Of Tuple(Of Integer, String))
        ListConditionType.Add(New Tuple(Of Integer, String)(1, "Ninguna"))
        ListConditionType.Add(New Tuple(Of Integer, String)(2, "Valor total"))
        INDsleConditionType.Properties.DataSource = ListConditionType
    End Sub

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        'ListXpCollection = Nothing
        'ListXpCollection = Presenter.InitializeDataSourceGridControlsRulesType(RuleType)
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        'INDgcControlsRuleType.DataSource = Nothing
        'INDgcControlsRuleType.DataSource = ListXpCollection
    End Sub


    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, optionCheck)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If optionCheck = 0 Then
                    row.SelectOption = False
                Else
                    row.SelectOption = True
                End If
            End If
        Next
    End Sub

    Public Sub editCondition()
        editModeDetails = True
        INDpceRates.ShowPopup()
    End Sub

    ''' Elimina la tarifa
    ''' <remarks></remarks>
    Private Sub DeleteRateWithConditions()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If ListDeleteCondition Is Nothing Then
                ListDeleteCondition = New List(Of ProductRateGeneralCondition)
            End If
            Dim row = DirectCast(viewRates.GetFocusedRow(), ProductRateGeneralCondition)
            Dim _indexEditRecord = Me.ListNewCondition.IndexOf(row)
            ListNewCondition.Item(_indexEditRecord).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
            ListDeleteCondition.Add(ListNewCondition.Item(_indexEditRecord))
            ListNewCondition.Remove(row)
            INDgcRates.DataSource = ListNewCondition.Where(Function(x) x.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList()
            INDgcRates.RefreshDataSource()
        End If
    End Sub


#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        'ListRuleType = Nothing
        'ListRulesType = Nothing
        'ListConditionType = Nothing
        'ListConditionTypeSecond = Nothing
        'ListLogicOperator = Nothing
        'ListUnitType = Nothing
        'ListLiquidationType = Nothing
        'ListEqualsOperator = Nothing
        'bw = Nothing
        'ListXpCollection = Nothing
        'Presenter = Nothing
        'controlerEditValueChanged = Nothing
        'ListDefinitionRateDetailCondition = Nothing
        'ListDeleteDefinitionRateDetailCondition = Nothing
        'definitionRateDetailCondition = Nothing
        'modeEditGridRates = Nothing
        'ListValidate = Nothing
        'viewNameFocus = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddRule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyAddRule, True)
        IndigoGridControl1.RefreshGrid(INDgcRates)
        'IndigoGridControl1.RefreshGrid(INDgcControlsRuleType)
        getCompanySettings()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewRates, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRates.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        'Presenter = New PAddRule(Me)
        InitializeTuples()
        'Deshacer()
        If ModeEditAll Then
            LoadControls()
        End If
        changeNumericFormatByCurrency()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddRule_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleRuleType.Focus()
        'If ModeEdit = True AndAlso RuleType IsNot Nothing Then
        '    HideColumnsOfGridControlsRuleType()
        '    INDSleProductType.Text = "1 item seleccionado"
        '    INDSleProductType.Properties.ReadOnly = True
        '    bw.RunWorkerAsync()
        'End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleRuleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRuleType.EditValueChanged
        If RuleType > 0 Then
            Select Case RuleType
                Case 1
                    INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Case 2
                    INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 3
                    INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End Select
        End If
    End Sub


    Private Sub INDsleConditionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConditionType.EditValueChanged
        ShowHideControls()

    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRule_Click(sender As Object, e As EventArgs) Handles INDbtnAddRule.Click
        AddRule()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del popup con una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddFirstCondition_Click(sender As Object, e As EventArgs) Handles INDbtnAddFirstCondition.Click
        AddCondition()
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleProductType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductType.QueryPopUp
        If INDSleProductType.Properties.DataSource Is Nothing Then
            Using model As New MProductType(Tag)
                INDSleProductType.Properties.DataSource = model.ListAllProductTypes()
            End Using
        End If
    End Sub

    Private Sub INDSleProductGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductGroup.QueryPopUp
        If INDSleProductGroup.Properties.DataSource Is Nothing Then
            Using model As New Inventory.MVP.MGroup(Tag)
                INDSleProductGroup.Properties.DataSource = model.ListProductGroupsByState(True)
            End Using
        End If
    End Sub

    Private Sub INDSleSubgroupProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSubgroupProduct.QueryPopUp
        If INDSleSubgroupProduct.Properties.DataSource Is Nothing Then
            Using model As New Inventory.MVP.MGroup(Tag)
                INDSleSubgroupProduct.Properties.DataSource = model.ListProductSubGroupsByStateAndHandlesBatch(True, True)
            End Using
        End If
    End Sub
#End Region


#Region "ContextMenu"

    ''' <summary>
    ''' Menu de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim btn = viewRates.GetFocusedRow()
        Select Case (sender.Text)
            'Case "Edit"
            '    Dim controlPopup = INDpceRates.Properties.PopupControl
            '    editCondition()
            '    editModeDetails = False
            Case "Eliminar"
                DeleteRateWithConditions()
        End Select
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProductType.CloseUp
        Dim view As GridView = viewProductType
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected?.Any() Then
            productTypeSelectedRows = String.Join(",", listHandlesSelected)
            INDSleProductType.Properties.NullText = listHandlesSelected?.Count().ToString() + " Item Seleccionados"

        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductGroup_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleProductGroup.CloseUp
        Dim view As GridView = viewProductGroup
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected?.Any() Then
            productGroupSelectedRows = String.Join(",", listHandlesSelected)
            INDSleProductGroup.Properties.NullText = listHandlesSelected?.Count().ToString() + " Item Seleccionados"

        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSubgroupProduct_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleSubgroupProduct.CloseUp
        Dim view As GridView = viewProductSubgroup
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected?.Any() Then
            productGroupSelectedRows = String.Join(",", listHandlesSelected)
            INDSleSubgroupProduct.Properties.NullText = listHandlesSelected?.Count().ToString() + " Item Seleccionados"
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        'Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        'ModeEdit = False
    End Sub

    Private Sub INDSleRateType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRateType.EditValueChanged
        ShowHideControls()
    End Sub

    Private Sub INDtxtInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtInitialDate.EditValueChanged
        If Not String.IsNullOrEmpty(InitialDate) AndAlso Not String.IsNullOrEmpty(EndDate) Then
            INDtxtEndDate.Properties.MinValue = InitialDate
        End If
    End Sub

    Private Sub INDpceRates_Popup(sender As Object, e As EventArgs) Handles INDpceRates.Popup
        If editModeDetails Then
            LoadControlsDetailConditions()
        End If
    End Sub
    ''' <summary>
    ''' Se cargan los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadControlsDetailConditions()
        Dim row = DirectCast(viewRates.GetFocusedRow(), ProductRateGeneralCondition)
        INDtxtInitialValue.EditValue = row.InitialValue
        INDtxtEndValue.EditValue = row.EndValue
        INDtxtConditionSalesValue.EditValue = row.SalesValue
        INDseConditionPercentage.EditValue = row.Percentage
    End Sub

#End Region

End Class
Public Class AddInfoToGridFormPrincipal
    Inherits EventArgs

    ''' <summary>
    ''' Representa al detalle de la definición de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductRateGeneralReturn As List(Of ProductRateGeneral)

    ''' <summary>
    ''' Representa al listado del detalle de la definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductRateGeneralCondition As List(Of ProductRateGeneralCondition)

    ''' <summary>
    ''' Establece si el retorno es satisfactorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReturnValueOk As Boolean

    ''' <summary>
    ''' Establece si el registro es para modificar o guardar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ModeEdit As Boolean

    ''' <summary>
    ''' Listado de eliminados de condiciones de los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Property ListDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition)

End Class

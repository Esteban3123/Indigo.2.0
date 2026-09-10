'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/08/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Inventory
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class FrmFixedAssetEntryDevolution
    Implements IFixedAssetEntryDevolution, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrTotalInvoiceEntranceVoucher()
        ctrTmp.SetInfoFunction(AddressOf getValuesRetention)
        ctrTmp.PrintInfo()
        ctrTmp.PopupContainerControlTotalValue = PopUpSummarySettlement
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)

        AddHandler bwLoadDetails.DoWork, AddressOf bwLoadDetails_DoWork
        AddHandler bwLoadDetails.RunWorkerCompleted, AddressOf bwLoadDetails_RunWorkerCompleted
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getValuesRetention() As Tuple(Of String, String, String, String)
        Return New Tuple(Of String, String, String, String)(NetoValue, DiscountValue, IvaValue, TotalValue)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Porcentaje del flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightIVAPercentage As Decimal Implements IFixedAssetEntryDevolution.FreightIVAPercentage
        Get
            Return INDseFreightIVAPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDseFreightIVAPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor del iva del flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightIVAValue As Decimal Implements IFixedAssetEntryDevolution.FreightIVAValue
        Get
            Return INDtxtFreightIVAValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFreightIVAValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor del flete que se escribe en el form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightValue As Decimal Implements IFixedAssetEntryDevolution.FreightValue
        Get
            Return INDtxtFreightValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFreightValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor del flete que se obtiene desde el ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightValueEntry As Decimal Implements IFixedAssetEntryDevolution.FreightValueEntry
        Get
            Return INDtxtFreightValueEntry.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFreightValueEntry.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillDate As Date? Implements IFixedAssetEntryDevolution.BillDate
        Get
            Return INDdteBillDate.EditValue
        End Get
        Set(value As Date?)
            INDdteBillDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDescription As String Implements IFixedAssetEntryDevolution.SupplierDescription
        Get
            Return INDtxtSupplier.EditValue
        End Get
        Set(value As String)
            INDtxtSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Term As Integer Implements IFixedAssetEntryDevolution.Term
        Get
            Return INDseTerm.EditValue
        End Get
        Set(value As Integer)
            INDseTerm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' No. factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillNumber As String Implements IFixedAssetEntryDevolution.BillNumber
        Get
            Return INDtxtBillNumber.EditValue
        End Get
        Set(value As String)
            INDtxtBillNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetEntryDevolution.Code
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
    ''' Detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IFixedAssetEntryDevolution.Description
        Get
            Return INDmemoDescription.EditValue
        End Get
        Set(value As String)
            INDmemoDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IFixedAssetEntryDevolution.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del ingreso de activos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixedAssetEntryId As Integer? Implements IFixedAssetEntryDevolution.FixedAssetEntryId
        Get
            Return INDsleFixedAssetEntry.EditValue
        End Get
        Set(value As Integer?)
            INDsleFixedAssetEntry.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del ingreso de activos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixedAssetEntryXpo As XPInstantFeedbackSource Implements IFixedAssetEntryDevolution.FixedAssetEntryXpo
        Get
            Return INDsleFixedAssetEntry.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFixedAssetEntry.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Layout del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetEntryDevolution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetEntryDevolution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Secuencia del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As FixedAssetSequence Implements IFixedAssetEntryDevolution.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
    ''' <summary>
    ''' Establece o toma la configuracion de parametros de activos
    ''' </summary>
    Private _SettingsFixedAsset As SettingFixedAsset
    Public Property SettingsFixedAsset As SettingFixedAsset Implements IFixedAssetEntryDevolution.SettingsFixedAsset
        Get
            Return _SettingsFixedAsset
        End Get
        Set(value As SettingFixedAsset)
            _SettingsFixedAsset = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Permite saber si se esta cargando desde el loadControls
    ''' </summary>
    Private flagLoad As Boolean = False

    ''' <summary>
    ''' Obtiene el listado de obligaciones asociadas a los compromisos que realizó el ingreso de activos
    ''' </summary>
    Private ListFixedAssetEntryDevolutionObligationBudget As List(Of FixedAssetEntryDevolutionObligationBudget)

    ''' <summary>
    ''' Obtiene el listado de eliminados de obligaciones asociadas a los compromisos que realizó el ingreso de activos
    ''' </summary>
    Private ListDeleteFixedAssetEntryDevolutionObligationBudget As List(Of FixedAssetEntryDevolutionObligationBudget)

    ''' <summary>
    ''' Sumatoria del valor neto de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Private NetoValue As Decimal

    ''' <summary>
    ''' Sumatoria de los descuentos de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Private DiscountValue As Decimal

    ''' <summary>
    ''' Sumatoria del valor del iva de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Private IvaValue As Decimal

    ''' <summary>
    ''' Total a pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private TotalValue As Decimal

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrTotalInvoiceEntranceVoucher

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Private FixedAssetEntryDevolution As FixedAssetEntryDevolution

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private Presenter As PFixedAssetEntryDevolution

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Asyncrono para consultar los detalles de la devolución de ingreso de activos
    ''' </summary>
    ''' <remarks></remarks>
    Private bwLoadDetails As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Listado que representa a los detalles traidos con xpo por medio de una vista
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDetails As List(Of ViewListFixedAssetEntryItemDetail)

    ''' <summary>
    ''' Permite saber si la consulta viene desde el load controls
    ''' </summary>
    ''' <remarks></remarks>
    Private IsLoadControls As Boolean

#End Region

#Region "BackgroundWorker"

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwLoadDetails_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        'Se inicia el loading en la rejilla
        INDviewItem.ShowLoadingPanel()
        'Se consultan los detalles con xpo
        ListDetails = Presenter.ListFixedAssetEntryItemDetailByFixedAssetEntryId(FixedAssetEntryId)
        Dim detail As ViewListFixedAssetEntryItemDetail
        Dim diferencia As Decimal

        For Each g In ListDetails.GroupBy(Function(d) d.FixedAssetEntryItemId)
            detail = g.FirstOrDefault()
            If (detail IsNot Nothing) Then
                diferencia = detail.SubTotalValueSource - g.Sum(Function(d) d.UnitValue)
                If (diferencia <> 0) Then
                    detail.UnitValue += diferencia
                End If
                diferencia = detail.RetentionSource - g.Sum(Function(d) d.RTFValue)
                If (diferencia <> 0) Then
                    detail.RTFValue += diferencia
                End If
                diferencia = detail.SubTotalValueSource - g.Sum(Function(d) d.SubTotalValue)
                If (diferencia <> 0) Then
                    detail.SubTotalValue += diferencia
                End If
                diferencia = detail.IvaValueSource - g.Sum(Function(d) d.IvaValue)
                If (diferencia <> 0) Then
                    detail.IvaValue += diferencia
                End If
                diferencia = detail.DiscountValueSource - g.Sum(Function(d) d.DiscountValue)
                If (diferencia <> 0) Then
                    detail.DiscountValue += diferencia
                End If
                diferencia = detail.TotalValueSource - g.Sum(Function(d) d.TotalValue)
                If (diferencia <> 0) Then
                    detail.TotalValue += diferencia
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwLoadDetails_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        'Se valida si viene desde el loadControls
        If IsLoadControls AndAlso ListDetails IsNot Nothing AndAlso ListDetails.Any() Then
            'Se asigna el check en false
            ListDetails.ForEach(Sub(item) item.SelectOption = False)

            'Se consulta los detalles de la devolución para asignarle el id a los detalles anteriores
            Dim ListDevolutionDetail = Presenter.ListFixedAssetEntryDevolutionDetailByFixedAssetEntryDevolutionId(FixedAssetEntryDevolution.Id)
            If ListDevolutionDetail IsNot Nothing AndAlso ListDevolutionDetail.Any() Then
                'Se recorre el listado de la tabla FixedAssetEntryDevolutionDetail para asignarle el id al listado que va en la rejilla
                ListDevolutionDetail.ForEach(Sub(itemEntryDevolutionDetail)
                                                 Dim info = (From x In ListDetails Where x.FixedAssetEntryItemDetailId = itemEntryDevolutionDetail.FixedAssetEntryItemDetailId Select x).FirstOrDefault
                                                 If info IsNot Nothing Then
                                                     info.SelectOption = True
                                                     info.FixedAssetEntryDevolutionDetailId = itemEntryDevolutionDetail.Id
                                                 End If
                                             End Sub)
            End If
        End If

        'Se asignan los valores a los controles del popup
        NetoValue = 0
        IvaValue = 0
        DiscountValue = 0
        TotalValue = 0
        NetoValue = (From x In ListDetails Where x.SelectOption = True Select x.SubTotalValue).Sum()
        IvaValue = (From x In ListDetails Where x.SelectOption = True Select x.IvaValue).Sum()
        DiscountValue = (From x In ListDetails Where x.SelectOption = True Select x.DiscountValue).Sum()
        TotalValue = (From x In ListDetails Where x.SelectOption = True Select x.TotalValue).Sum()
        INDPopTxtValue.EditValue = NetoValue
        INDPopTxtValueTax.EditValue = IvaValue
        INDPopTxtFreightValue.EditValue = FreightValue
        INDPopTxtDiscountValue.EditValue = DiscountValue
        INDPopTxtWithholdingTax.EditValue = (From x In ListDetails Where x.SelectOption = True Select x.WithholdingTax).Sum()
        INDPopTxtWithholdingICA.EditValue = (From x In ListDetails Where x.SelectOption = True Select x.WithholdingICA).Sum()
        INDPopTxtRetentionSource.EditValue = (From x In ListDetails Where x.SelectOption = True Select x.RTFValue).Sum()
        INDPopTxtRetentionOther.EditValue = 0
        INDPopTxtDeductionOther.EditValue = 0
        INDPopTxtDistricTaxes.EditValue = 0
        CalculateTotalValue()
        ctrTmp.PrintInfo()

        'Se asigna lo consultado al datasource de la rejilla
        INDgcItem.DataSource = Nothing
        INDgcItem.DataSource = ListDetails

        'Se oculta el loading de la rejilla
        INDviewItem.HideLoadingPanel()

        'Se quita el readOnly al control porque ya se termino la consulta
        INDsleFixedAssetEntry.Properties.ReadOnly = False
    End Sub

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        Try
            AsyncLoader(True)
            If Not ValidateControls() Then
                Exit Sub
            End If

            If FixedAssetEntryDevolution.Status <> 3 Then
                If ListDetails Is Nothing OrElse ListDetails.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No hay detalles en la rejilla"
                    Exit Sub
                End If

                If (From x In ListDetails Where x.SelectOption = True Select x).Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item de la rejilla"
                    Exit Sub
                End If

                AssigningValues()
            End If

            Using model As New MFixedAssetEntryDevolution(MyTag)

                Dim Result = Await model.SaveFixedAssetEntryDevolution(FixedAssetEntryDevolution, _idCurrentSequence)
                If Result.StateResult Then

                    If FixedAssetEntryDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If FixedAssetEntryDevolution.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        ElseIf FixedAssetEntryDevolution.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                        End If
                    ElseIf FixedAssetEntryDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If FixedAssetEntryDevolution.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf FixedAssetEntryDevolution.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                        ElseIf FixedAssetEntryDevolution.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    Me.FixedAssetEntryDevolution = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                Else
                    INDbtnCode.Enabled = False
                    If Result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    ElseIf Result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            INDbtnCode.Enabled = False
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEntryDevolution()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        Try
            AsyncLoader(True)
            If ValidateControls() Then
                If FixedAssetEntryDevolution.Status <> 3 Then
                    If ListDetails Is Nothing OrElse ListDetails.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No hay detalles en la rejilla"
                        Exit Sub
                    End If

                    If (From x In ListDetails Where x.SelectOption = True Select x).Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item de la rejilla"
                        Exit Sub
                    End If

                    AssigningValues()
                End If

                Using model As New MFixedAssetEntryDevolution(MyTag)
                    Dim Result = Await model.ConfirmFixedAssetEntryDevolution(FixedAssetEntryDevolution, _idCurrentSequence)

                    If Result.StatusCode = eStatusResult.SUCCESS Then
                        Mensaje(EeventViewerImages.Informacion) = Result.Message
                        Me.FixedAssetEntryDevolution = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.Deshacer()
                    Else
                        INDbtnCode.Enabled = False
                        If Result.StatusCode = eStatusResult.WARNING Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        ElseIf Result.StatusCode = eStatusResult.EXCEPTION Then
                            Mensaje(EeventViewerImages.MensajeError) = Result.Message
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            INDbtnCode.Enabled = False
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que carga las obligaciones asociadas al compromiso generado por el ingreso de activos
    ''' </summary>
    Private Async Sub LoadObligationsOfCommitmentsFixedAssetEntry()
        CheckForIllegalCrossThreadCalls = False

        If flagLoad Then
            INDviewObligation.HideLoadingPanel()
            Exit Sub
        End If

        Dim listXpo = Await Presenter.ListObligationsOfFixedAssetEntry(FixedAssetEntryId)
        If listXpo IsNot Nothing AndAlso listXpo.Any() Then
            For Each itemXpo In listXpo
                Dim entity As New FixedAssetEntryDevolutionObligationBudget
                With entity
                    .ObligationDetailId = itemXpo.ObligationDetailId
                    .Value = 0
                    .ObligationCode = itemXpo.ObligationCode
                    .ObligationDocument = itemXpo.ObligationDocument
                    .CategoryName = itemXpo.CategoryDescription
                    .FinancialSourceDescription = itemXpo.FinancialSourceDescription
                    .RevenueTypeDescription = itemXpo.RevenueTypeDescription
                    .ObligationBalance = itemXpo.Balance
                    .CommitmentDetailId = itemXpo.CommitmentDetailId
                End With
                ListFixedAssetEntryDevolutionObligationBudget.Add(entity)
            Next

            INDgcObligation.DataSource = Nothing
            INDgcObligation.DataSource = ListFixedAssetEntryDevolutionObligationBudget
            INDgcObligation.RefreshDataSource()
        End If

        INDviewObligation.HideLoadingPanel()
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using model As New Presentation.Payments.MVP.MParameters(MyTag)
            Dim budgetInterface As Boolean = False
            Dim _parameter = Await model.GetSettingPaymentsByIdOperatingUnit(_idOperativeUnit)
            If _parameter IsNot Nothing AndAlso _parameter.ObjectEmbbeded IsNot Nothing AndAlso _parameter.StateResult Then
                budgetInterface = _parameter.ObjectEmbbeded.BudgetInterface
            End If

            INDlygBudget.Visibility = If(budgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        End Using
    End Function

    ''' <summary>
    ''' Calcula el total del valor de la cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateTotalValue()
        INDPopTxtTotalCxp.EditValue = INDPopTxtValue.EditValue + INDPopTxtValueTax.EditValue + INDPopTxtFreightValue.EditValue + INDPopSpnFreightIVA.EditValue - INDPopTxtDiscountValue.EditValue - INDPopTxtWithholdingTax.EditValue - INDPopTxtWithholdingICA.EditValue - INDPopTxtRetentionSource.EditValue
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup de retenciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlValues(FlagClean As Boolean)
        INDPopTxtValue.EditValue = 0
        INDPopTxtValueTax.EditValue = 0
        If FlagClean Then
            INDPopTxtFreightValue.EditValue = 0
            INDPopSpnFreightIVA.EditValue = 0
            INDPopTxtTotalCxp.EditValue = 0
        Else
            INDPopTxtTotalCxp.EditValue = INDPopTxtFreightValue.EditValue + INDPopSpnFreightIVA.EditValue
        End If

        INDPopTxtDiscountValue.EditValue = 0
        INDPopTxtWithholdingTax.EditValue = 0
        INDPopTxtWithholdingICA.EditValue = 0
        INDPopTxtRetentionSource.EditValue = 0
        INDPopTxtRetentionOther.EditValue = 0
        INDPopTxtDeductionOther.EditValue = 0
        INDPopTxtDistricTaxes.EditValue = 0
        NetoValue = 0
        IvaValue = 0
        DiscountValue = 0
        TotalValue = 0
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Limpia los controles del flete
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanFreigth()
        FreightValue = 0
        FreightValueEntry = 0
        FreightIVAPercentage = 0
        FreightIVAValue = 0
    End Sub

    ''' <summary>
    ''' Carga los detalles de la devolución a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDetails()
        Try
            'Se corre el backgroundworker
            INDsleFixedAssetEntry.Properties.ReadOnly = True 'Se coloca en readOnly al control mientras se consulta
            bwLoadDetails.RunWorkerAsync()
        Catch ex As Exception
            INDviewItem.HideLoadingPanel()
            INDsleFixedAssetEntry.Properties.ReadOnly = False
        End Try
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.FixedAssetEntryDevolution IsNot Nothing AndAlso Me.FixedAssetEntryDevolution.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetEntryDevolution.ActionsOnControls
        Set(value As Boolean)
            INDlyEntryDevolution.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDmemoDescription.Enabled = value
            INDsleFixedAssetEntry.Enabled = value
            INDtxtBillNumber.Enabled = value
            INDdteBillDate.Enabled = value
            INDtxtSupplier.Enabled = value
            INDseTerm.Enabled = value
            INDtxtFreightValue.Enabled = value
            INDgcItem.Enabled = value

            INDgcObligation.Enabled = value

            INDlyEntryDevolution.EndUpdate()
            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If

            INDtxtBillNumber.Properties.ReadOnly = True
            INDdteBillDate.Properties.ReadOnly = True
            INDtxtSupplier.Properties.ReadOnly = True
            INDseTerm.Properties.ReadOnly = True
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

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

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.35)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.35)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListFixedAssetEntryDevolution
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
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

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetEntryDevolution.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetEntryDevolution.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetEntryDevolution.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetEntryDevolution.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetEntryDevolution.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
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
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyEntryDevolution.BeginUpdate()
        ReadOnlyControls(False)
        CleanFreigth()
        CleanControlValues(True)
        INDlygFreigth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ActionsOnControls = False
        Code = String.Empty
        DocumentDate = Nothing
        Description = String.Empty
        FixedAssetEntryId = Nothing
        INDsleFixedAssetEntry.Properties.NullText = String.Empty
        INDsleFixedAssetEntry.Properties.ReadOnly = False
        BillNumber = String.Empty
        BillDate = Nothing
        SupplierDescription = String.Empty
        Term = Nothing
        INDgcItem.DataSource = Nothing
        ListDetails = Nothing
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        INDgcObligation.DataSource = Nothing
        ListFixedAssetEntryDevolutionObligationBudget = Nothing
        ListDeleteFixedAssetEntryDevolutionObligationBudget = Nothing

        INDlyEntryDevolution.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With FixedAssetEntryDevolution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = Me.BarraBotones.OperatingUnit.Id
            .Code = Code
            .DocumentDate = DocumentDate
            .FixedAssetEntryId = FixedAssetEntryId
            .Description = Description
            .FreightValue = FreightValue
            .FreightIVAPercentage = FreightIVAPercentage
            .FreightIVAValue = FreightIVAValue
            .Value = INDPopTxtValue.EditValue
            .ValueDiscount = INDPopTxtDiscountValue.EditValue
            .ValueTax = INDPopTxtValueTax.EditValue
            .WithholdingTax = INDPopTxtWithholdingTax.EditValue
            .WithholdingICA = INDPopTxtWithholdingICA.EditValue
            .RetentionSource = INDPopTxtRetentionSource.EditValue
            .RetentionOther = 0
            .DeductionOther = 0
            .TotalValue = TotalValue

            If ListDetails IsNot Nothing AndAlso ListDetails.Any() Then 'Se recorren los detalles para poder armar los objetos y enviarlos al sp
                .FixedAssetEntryDevolutionDetail.Clear()
                For Each itemDetail In (From x In ListDetails Where (x.SelectOption) Select x).ToList

                    Dim FixedAssetEntryDevolutionDetail As New FixedAssetEntryDevolutionDetail
                    FixedAssetEntryDevolutionDetail.Id = itemDetail.FixedAssetEntryDevolutionDetailId
                    If itemDetail.FixedAssetEntryDevolutionDetailId > 0 Then
                        FixedAssetEntryDevolutionDetail.FixedAssetEntryDevolutionId = .Id
                    End If

                    FixedAssetEntryDevolutionDetail.FixedAssetEntryItemId = itemDetail.FixedAssetEntryItemId
                    FixedAssetEntryDevolutionDetail.FixedAssetEntryItemDetailId = itemDetail.FixedAssetEntryItemDetailId
                    FixedAssetEntryDevolutionDetail.UnitValue = itemDetail.UnitValue
                    FixedAssetEntryDevolutionDetail.SubTotalValue = itemDetail.SubTotalValue
                    FixedAssetEntryDevolutionDetail.IvaPercentage = itemDetail.IvaPercentage
                    FixedAssetEntryDevolutionDetail.IvaValue = itemDetail.IvaValue
                    FixedAssetEntryDevolutionDetail.DiscountPercentage = itemDetail.DiscountPercentage
                    FixedAssetEntryDevolutionDetail.DiscountValue = itemDetail.DiscountValue
                    FixedAssetEntryDevolutionDetail.TotalValue = itemDetail.TotalValue
                    FixedAssetEntryDevolutionDetail.RTFPercentage = itemDetail.RTFPercentage
                    FixedAssetEntryDevolutionDetail.RTFValue = itemDetail.RTFValue
                    FixedAssetEntryDevolutionDetail.CheckOption = itemDetail.SelectOption
                    .FixedAssetEntryDevolutionDetail.Add(FixedAssetEntryDevolutionDetail)
                Next
            End If

            If ListFixedAssetEntryDevolutionObligationBudget IsNot Nothing AndAlso ListFixedAssetEntryDevolutionObligationBudget.Any() Then
                ListFixedAssetEntryDevolutionObligationBudget.ForEach(Sub(item) .FixedAssetEntryDevolutionObligationBudget.Add(item))
            End If

            If ListDeleteFixedAssetEntryDevolutionObligationBudget IsNot Nothing AndAlso ListDeleteFixedAssetEntryDevolutionObligationBudget.Any() Then
                ListDeleteFixedAssetEntryDevolutionObligationBudget.ForEach(Sub(item) .FixedAssetEntryDevolutionObligationBudget.Add(item))
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MFixedAssetEntryDevolution(CStr(Me.Tag))
                    AsyncLoader(True)
                    FixedAssetEntryDevolution = (Await Model.GetFixedAssetEntryDevolution(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    If FixedAssetEntryDevolution.Status = 2 OrElse FixedAssetEntryDevolution.Status = 3 Then
                        INDlyEntryDevolution.Enabled = False
                        INDsleFixedAssetEntry.Properties.ReadOnly = True
                    Else
                        INDsleFixedAssetEntry.Properties.ReadOnly = False
                    End If
                    INDlyEntryDevolution.BeginUpdate()
                    If FixedAssetEntryDevolution IsNot Nothing AndAlso FixedAssetEntryDevolution.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetEntryDevolution.Id))
                            With FixedAssetEntryDevolution
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                Description = .Description

                                flagLoad = True
                                FixedAssetEntryId = .FixedAssetEntryId
                                INDsleFixedAssetEntry.Properties.NullText = .EntryDescription
                                flagLoad = False

                                ListFixedAssetEntryDevolutionObligationBudget = .FixedAssetEntryDevolutionObligationBudget.ToList()
                                INDgcObligation.DataSource = Nothing
                                INDgcObligation.DataSource = ListFixedAssetEntryDevolutionObligationBudget

                                'Se cargan los valores del flete siempre y cuando el ingreso maneje estos valores
                                If .FreightValue > 0 Then
                                    INDlygFreigth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                    FreightIVAPercentage = .FreightIVAPercentage
                                    FreightValue = .FreightValue
                                    FreightIVAValue = .FreightIVAValue
                                Else
                                    CleanFreigth()
                                    INDlygFreigth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                End If

                                BarraBotones.StatusRecord = .Status.ToString
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetEntryDevolution.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetEntryDevolution.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(FixedAssetEntryDevolution.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetEntryDevolution).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If FixedAssetEntryDevolution.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetEntryDevolution.Id, 0, FixedAssetEntryDevolution.Id, _idOperativeUnit)
                            INDsleFixedAssetEntry.Properties.ReadOnly = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEntryDevolution()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyEntryDevolution.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewEntryDevolution() As Task
        If Me.BarraBotones.OperatingUnit Is Nothing OrElse Me.BarraBotones.OperatingUnit.Id = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una unidad operativa"
            Exit Function
        End If
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El formulario no tiene parametrizado la secuencia numérica"
            Exit Function
        End If
        Me.FixedAssetEntryDevolution = New FixedAssetEntryDevolution()
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = "1"
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Setea las acciones de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetActions()

        IndigoGridControl1.RefreshGrid(INDgcItem)
        IndigoGridView1.SetListAcction(INDviewItem, {eAcciones.CheckOptions, eAcciones.UnCheckOptions}.ToList())

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewItem.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next
    End Sub
    ''' <summary>
    ''' Metodo de customizacion a partir de la moneda
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat
        GridColumn8 = Window.Utils.FormatGrid(GridColumn8, _currencyAbbreviation)
        Me.ctrTmp.CodeISO4217 = _currencyAbbreviation
        Me.INDPopTxtValue.Properties.Mask.Culture = _culture
        Me.INDPopTxtDiscountValue.Properties.Mask.Culture = _culture
        Me.INDPopTxtTotalCxp.Properties.Mask.Culture = _culture
        Me.INDPopTxtValueTax.Properties.Mask.Culture = _culture
        Me.INDPopTxtFreightValue.Properties.Mask.Culture = _culture
        Me.INDPopTxtWithholdingTax.Properties.Mask.Culture = _culture
        Me.INDPopTxtWithholdingICA.Properties.Mask.Culture = _culture
        Me.INDPopSpnFreightIVA.Properties.Mask.Culture = _culture
        Me.INDPopTxtRetentionSource.Properties.Mask.Culture = _culture
        Me.INDPopTxtRetentionOther.Properties.Mask.Culture = _culture
        Me.INDPopTxtDeductionOther.Properties.Mask.Culture = _culture
        Me.INDPopTxtDistricTaxes.Properties.Mask.Culture = _culture


        ctrTmp.Refresh()
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        NetoValue = Nothing
        DiscountValue = Nothing
        IvaValue = Nothing
        TotalValue = Nothing
        ctrTmp = Nothing
        FixedAssetEntryDevolution = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        bwLoadDetails = Nothing
        ListDetails = Nothing
        IsLoadControls = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmFixedAssetEntryDevolution_LoadAsync(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyEntryDevolution, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetEntryDevolution(Me)
        Await Presenter.GetSettingFixedAssetByOperatingUnitId(_idOperativeUnit)
        SetCurrencyUI(SettingsFixedAsset?.Currency?.Abbreviation)

        SetActions()
        Await Me.LoadParameters()
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetEntryDevolution_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewEntryDevolution()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de ingreso de activos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleFixedAssetEntry_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFixedAssetEntry.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1116, Nothing, True)
            Await Presenter.InitializeFixedAssetEntry()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de ingreso de activos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleFixedAssetEntry_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFixedAssetEntry.QueryPopUp
        If FixedAssetEntryXpo Is Nothing Then
            Await Presenter.InitializeFixedAssetEntry()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de ingreso de activos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleFixedAssetEntry_EditValueChangedAsync(sender As Object, e As EventArgs) Handles INDsleFixedAssetEntry.EditValueChanged
        If FixedAssetEntryId IsNot Nothing Then 'Si han seleccionado algun valor
            'Se obtiene el item seleccionado en el search
            Dim entryXpo = Await Presenter.GetFixedAssetEntryById(FixedAssetEntryId)
            Dim abreviation = entryXpo.CurrencyId.Abbreviation
            SetCurrencyUI(abreviation)
            If entryXpo IsNot Nothing Then
                INDgcObligation.DataSource = Nothing

                If ListFixedAssetEntryDevolutionObligationBudget IsNot Nothing AndAlso ListFixedAssetEntryDevolutionObligationBudget.Any() Then
                    While ListFixedAssetEntryDevolutionObligationBudget.Count > 0

                        If ListFixedAssetEntryDevolutionObligationBudget(0).Id > 0 Then
                            If ListDeleteFixedAssetEntryDevolutionObligationBudget Is Nothing Then
                                ListDeleteFixedAssetEntryDevolutionObligationBudget = New List(Of FixedAssetEntryDevolutionObligationBudget)
                            End If
                            ListFixedAssetEntryDevolutionObligationBudget(0).MarkAsDeleted()
                            ListDeleteFixedAssetEntryDevolutionObligationBudget.Add(ListFixedAssetEntryDevolutionObligationBudget(0))
                        End If

                        ListFixedAssetEntryDevolutionObligationBudget.Remove(ListFixedAssetEntryDevolutionObligationBudget(0))

                    End While
                Else
                    ListFixedAssetEntryDevolutionObligationBudget = New List(Of FixedAssetEntryDevolutionObligationBudget)
                End If

                'Se valida que el ingreso tenga asociado una cxp
                If entryXpo.AccountPayableId IsNot Nothing Then
                    INDlyItemBillNumber.ShowLayout()
                    INDlyItemBillDate.ShowLayout()
                    INDlyItemSupplier.ShowLayout()
                    INDlyItemTerm.ShowLayout()

                    'Se asignan los valores a los controles de la entidad xpo
                    BillNumber = entryXpo.AccountPayableId.CodeBillNumber
                    BillDate = entryXpo.AccountPayableId.BillDate
                    SupplierDescription = entryXpo.SupplierId.CodeName
                    Term = entryXpo.SupplierId.TimeLimitDays

                    INDviewObligation.ShowLoadingPanel()
                    Await Task.Factory.StartNew(Sub() LoadObligationsOfCommitmentsFixedAssetEntry())
                Else
                    INDlyItemBillNumber.HideLayout()
                    INDlyItemBillDate.HideLayout()
                    INDlyItemSupplier.HideLayout()
                    INDlyItemTerm.HideLayout()
                End If
                If FixedAssetEntryDevolution.Id = 0 Then
                    IsLoadControls = False 'Los detalles se cargan desde la vista sin la tabla de detalles de devolución
                    'Se cargan los valores del flete siempre y cuando el ingreso maneje estos valores
                    If entryXpo.FreightValue > 0 Then
                        INDlygFreigth.HideControl(False)
                        FreightValueEntry = entryXpo.FreightValue
                        FreightIVAPercentage = entryXpo.FreightIVAPercentage
                        FreightValue = entryXpo.FreightValue
                        FreightIVAValue = entryXpo.FreightIVAValue
                    Else
                        CleanFreigth()
                        INDlygFreigth.HideControl()
                    End If
                Else
                    IsLoadControls = True 'Los detalles se cargan desde la visto con la tabla de detalles de devolución
                    'Se cargan los valores del flete siempre y cuando el ingreso maneje estos valores
                    If entryXpo.FreightValue > 0 Then
                        FreightValueEntry = entryXpo.FreightValue
                    End If
                End If
                LoadDetails()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del flete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtFreightValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtFreightValue.EditValueChanged
        If FreightValue > 0 Then
            FreightIVAValue = Utils.RoundValue(FreightValue * FreightIVAPercentage / 100, Utils.RoundLevel.Unit)
            INDPopTxtFreightValue.EditValue = FreightValue
            INDPopSpnFreightIVA.EditValue = FreightIVAValue
            FreightIVAPercentage = FreightIVAPercentage
            CalculateTotalValue()
            ctrTmp.PrintInfo()
        Else
            FreightIVAValue = 0
            INDPopTxtFreightValue.EditValue = 0
            INDPopSpnFreightIVA.EditValue = 0
            CalculateTotalValue()
            ctrTmp.PrintInfo()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del control de la rejilla de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim entity As FixedAssetEntryDevolutionObligationBudget = INDviewObligation.GetFocusedRow()
            If CDec(e.NewValue) > CDec(entity.ObligationBalance) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor a ejecutar no puede ser mayor al saldo de la obligación"
                e.Cancel = True
                Exit Sub
            End If

            entity.Value = e.NewValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control del repositorio de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        Dim itemXpo As ViewListFixedAssetEntryItemDetail = INDviewItem.GetFocusedRow()

        If itemXpo IsNot Nothing Then
            itemXpo.SelectOption = e.NewValue
            INDgcItem.RefreshDataSource()

            'Se realiza la operación con los valores del popup
            NetoValue = (From x In ListDetails Where x.SelectOption = True Select x.SubTotalValue).Sum()
            IvaValue = (From x In ListDetails Where x.SelectOption = True Select x.IvaValue).Sum()
            DiscountValue = (From x In ListDetails Where x.SelectOption = True Select x.DiscountValue).Sum()
            TotalValue = (From x In ListDetails Where x.SelectOption = True Select x.TotalValue).Sum()
            INDPopTxtValue.EditValue = NetoValue
            INDPopTxtValueTax.EditValue = IvaValue
            INDPopTxtFreightValue.EditValue = FreightValue
            INDPopTxtDiscountValue.EditValue = DiscountValue
            INDPopTxtWithholdingTax.EditValue = (From x In ListDetails Where x.SelectOption = True Select x.WithholdingTax).Sum()
            INDPopTxtWithholdingICA.EditValue = (From x In ListDetails Where x.SelectOption = True Select x.WithholdingICA).Sum()
            INDPopTxtRetentionSource.EditValue = (From x In ListDetails Where x.SelectOption = True Select x.RTFValue).Sum()
            INDPopTxtRetentionOther.EditValue = 0
            INDPopTxtDeductionOther.EditValue = 0
            INDPopTxtDistricTaxes.EditValue = 0
            CalculateTotalValue()
            ctrTmp.PrintInfo()
        End If
    End Sub

#End Region

#Region "MenuContext"

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "CheckOptions"
                CheckOrUnCheck(1)
            Case "UnCheckOptions"
                CheckOrUnCheck(0)
        End Select
    End Sub

    ''' <summary>
    ''' Selecciona o deselecciona los item de la rejilla
    ''' OptionCheck(0=Sin check, 1=Con check)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckOrUnCheck(OptionCheck As Integer)
        'Se captura los item que se hayan seleccionado
        Dim listHandlesSelected = INDviewItem.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            'Se recorre los items seleccionados
            For i = 0 To listHandlesSelected.Count - 1
                'Se obtiene el item de la rejilla
                Dim row As ViewListFixedAssetEntryItemDetail = INDviewItem.GetRow(listHandlesSelected(i))
                'Se selecciona o deselecciona el check
                row.SelectOption = OptionCheck
            Next

            INDgcItem.RefreshDataSource()
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
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        FixedAssetEntryDevolution.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
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
        FixedAssetEntryDevolution.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        FixedAssetEntryDevolution.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        FixedAssetEntryDevolution.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        FixedAssetEntryDevolution.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, FixedAssetEntryDevolution.Id, 0, FixedAssetEntryDevolution.Id, _idOperativeUnit)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class
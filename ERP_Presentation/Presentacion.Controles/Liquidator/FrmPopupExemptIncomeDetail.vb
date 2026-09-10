Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Controls.MVP


#Region "Enumerations"

''' <summary>
''' Enum Source para Saber desde que control se envia al Abrir Popup
''' </summary>
Public Enum ExemptIncomeType
    ExemptIncomeControl = 1
    ExemptIncomeAndDeductionsControl = 2
End Enum

Public Enum eOriginType
    AccountPayable = 1
    Adjustment = 2
    Note = 3
End Enum

#End Region

Public Class FrmPopupExemptIncomeDetail

#Region "EVENTS"
    ''' <summary>
    ''' Evento para adicionar un nuevo ajuste a los conceptos de liquidación de rentas para trabajadores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddConceptLiquidationAdjustment(sender As Object, e As AddAdjustmentsEventArgs)

    ''' <summary>
    ''' Evento para eliminar un ajuste a los conceptos de liquidación de rentas para trabajadores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event RemoveConceptLiquidationAdjustment(sender As Object, e As AddAdjustmentsEventArgs)
#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PLiquidator

    ''' <summary>
    ''' Variable para el Id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim ThirdPartyId As Integer

    ''' <summary>
    ''' Variable para el año fiscal
    ''' </summary>
    ''' <remarks></remarks>
    Dim FiscalYear As Integer

    ''' <summary>
    ''' Bandera para saber desde donde se instancia el form
    ''' 1 = desde rentas exentas y deducciones
    ''' 2 = desde ingresos acumalados
    ''' </summary>
    Public flagCall As Integer = 1

    ''' <summary>
    ''' Variable para saber que control envia a abrir el popUp
    ''' </summary>
    Dim Source As ExemptIncomeType

    ''' <summary>
    ''' Variable para dataSource del gridcontrol
    ''' </summary>
    Dim ViewRetentionDetail As List(Of Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionXpo)

#End Region

#Region "Properties"

    Public ListConceptLiquidationAdjustment As List(Of AccountPayableDetailConceptLiquidationAdjusments)

    ''' <summary>
    ''' Obtiene el estado de la cuenta por pagar
    ''' </summary>
    Public OriginStatus As Byte

    ''' <summary>
    ''' Obtiene el código de la cuenta por pagar / nota / documento
    ''' </summary>
    Public OriginCode As String

    ''' <summary>
    ''' Obtiene número de la factura
    ''' </summary>
    Public BillNumber As String

    ''' <summary>
    ''' Fecha del documento que viene de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime

    ''' <summary>
    ''' Fecha de creación del documento que viene de la cabecera
    ''' </summary>
    Public Property CreationDate As DateTime

#End Region

#Region "Builder"

    Public Sub New(_presenter As PLiquidator, _thirdPartyId As Integer, _fiscalYear As Integer, _source As ExemptIncomeType, _documentDate As DateTime)
        ' This call is required by the designer.
        InitializeComponent()

        Presenter = _presenter
        ThirdPartyId = _thirdPartyId
        FiscalYear = _fiscalYear
        Source = _source
        DocumentDate = _documentDate
    End Sub

    Public Sub New(_presenter As PLiquidator, _thirdPartyId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Presenter = _presenter
        ThirdPartyId = _thirdPartyId
    End Sub

#End Region

#Region "Methods"

    Private Sub ReturnAddConceptLiquidationAdjustment(sender As Object, e As AddAdjustmentsEventArgs)
        ListConceptLiquidationAdjustment.Add(e.NewAdjustment)
        AddNewAdjustmentsToView(e.NewAdjustment)

        INDGcExemptIncomeDetail.DataSource = Nothing
        INDGcExemptIncomeDetail.DataSource = ViewRetentionDetail

        RaiseEvent AddConceptLiquidationAdjustment(Nothing, e)
    End Sub

    Private Sub LoadView()
        If flagCall = 1 Then
            ViewRetentionDetail = Presenter.GetViewThirdPartyRetentionByFilter(String.Format("ThirdPartyId = {0} AND Year = {1}", ThirdPartyId, FiscalYear))
        Else
            Dim initialDate As Date = New Date(DocumentDate.Year, DocumentDate.Month, 1)
            Dim endDate As Date = initialDate.AddMonths(1).AddDays(-1)
            Dim filters As String = String.Format("ThirdPartyId = {0} AND Type IN(1,3) AND DocumentDate >= #{1}# AND DocumentDate <= #{2}#", ThirdPartyId, initialDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"))

            If DocumentDate.Month = Me.CreationDate.Month Then
                filters &= String.Format(" AND CreationDate <= #{0}#", Me.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"))
            End If

            ViewRetentionDetail = Presenter.GetViewThirdPartyRetentionByFilter(filters)
        End If

        If ViewRetentionDetail Is Nothing Then
            ViewRetentionDetail = New List(Of Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionXpo)
        End If

        Dim itemsForDelete = New List(Of Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionXpo)
        For index = 0 To ViewRetentionDetail.Count - 1
            Dim item = ViewRetentionDetail.ElementAt(index)
            If item.Type = eOriginType.Adjustment Then
                If item.ConceptType <> Source Then
                    itemsForDelete.Add(item)
                End If
            End If
        Next

        For Each item In itemsForDelete
            ViewRetentionDetail.Remove(item)
        Next

        If OriginStatus < 2 Then
            If ListConceptLiquidationAdjustment IsNot Nothing AndAlso ListConceptLiquidationAdjustment.Count > 0 Then
                For Each item In ListConceptLiquidationAdjustment
                    AddNewAdjustmentsToView(item)
                Next
            End If
        End If

        INDGcExemptIncomeDetail.DataSource = ViewRetentionDetail
    End Sub
#End Region

#Region "Events"

    Private Sub FrmPopupExemptIncomeDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If OriginStatus < 2 Then
            INDSbAdd.Visible = True
        End If

        If flagCall = 1 Then
            INDSbAdd.Visible = True
            If Source = ExemptIncomeType.ExemptIncomeControl Then
                INDColExemptIncome.Visible = True
            Else
                INDColMaxDeductionsAndRentExents.Visible = True
            End If
        Else
            INDSbAdd.Visible = False
            INDColExemptIncome.Visible = False
            INDColMaxDeductionsAndRentExents.Visible = False
            If flagCall = 3 Then
                INDColPrevoiusRetentions.Visible = True
            Else
                INDColPrevoiusRetentions.Visible = False
            End If
        End If

        LoadView()
    End Sub

    Private Sub AddNewAdjustmentsToView(item As AccountPayableDetailConceptLiquidationAdjusments)
        Dim detail = New Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionXpo
        detail.Id = Guid.NewGuid()
        detail.Type = eOriginType.Adjustment
        detail.TypeName = "Ajuste"
        detail.Code = OriginCode
        detail.BillNumber = BillNumber
        detail.DocumentDate = DocumentDate
        detail.AdjustmentUUID = item.UUID

        If item.Nature = 1 Then
            detail.DebitValue = item.TotalIncome
        Else
            detail.CreditValue = item.TotalIncome
        End If

        If item.ConceptType = ExemptIncomeType.ExemptIncomeControl Then
            If item.Nature = 1 Then
                detail.ExemptIncome = item.Value
            Else
                detail.ExemptIncome = item.Value * -1
            End If
        Else
            If item.Nature = 1 Then
                detail.MaxDeductionsAndRentExents = item.Value
            Else
                detail.MaxDeductionsAndRentExents = item.Value * -1
            End If
        End If


        ViewRetentionDetail.Add(detail)
    End Sub

    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        Using formulario As New Controls.FrmPopupConceptLiquidationAdjusments(Source)
            formulario.StartPosition = FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            AddHandler formulario.AddConceptLiquidationAdjustment, AddressOf ReturnAddConceptLiquidationAdjustment
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub INDGvExemptIncomeDetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles INDGvExemptIncomeDetail.RowStyle
        Dim view = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim uuid As String = view.GetRowCellValue(e.RowHandle, "AdjustmentUUID")
        If Not String.IsNullOrEmpty(uuid) Then
            e.Appearance.BackColor = Color.Yellow
            e.HighPriority = True
        End If
    End Sub

    Private Sub INDGvExemptIncomeDetail_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvExemptIncomeDetail.PopupMenuShowing
        INDBbiRemove.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Dim View = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim uuid As String = View.GetRowCellValue(e.HitInfo.RowHandle, "AdjustmentUUID")
        If Not String.IsNullOrEmpty(uuid) Then
            INDBbiRemove.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        INDPopMenuActions2.Manager = BarManager2
        INDPopMenuActions2.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

    Private Sub INDBbiRemove_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRemove.ItemClick
        Dim viewXpo = CType(INDGvExemptIncomeDetail.GetFocusedRow(), Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionXpo)
        Dim adjustment = ListConceptLiquidationAdjustment.First(Function(d) d.UUID = viewXpo.AdjustmentUUID)

        ViewRetentionDetail.Remove(viewXpo)
        ListConceptLiquidationAdjustment.Remove(adjustment)

        INDGcExemptIncomeDetail.DataSource = Nothing
        INDGcExemptIncomeDetail.DataSource = ViewRetentionDetail

        Dim args As New AddAdjustmentsEventArgs
        args.NewAdjustment = adjustment
        RaiseEvent RemoveConceptLiquidationAdjustment(Nothing, args)
    End Sub

#End Region

End Class
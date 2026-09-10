#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports System.Globalization

#End Region

Public Class rptAdjustmentInventory
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Valor total en letras
    ''' </summary>
    ''' <remarks></remarks>
    Dim ValueLetter As Decimal = 0

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)

        Dim INDList As List(Of InventoryAdjustmentReportXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of InventoryAdjustmentReportXpo)(Nothing, filtroConsulta)
        If INDList.Count > 0 Then
            Dim INDNameUser = CType(INDList(0), InventoryAdjustmentReportXpo).CreationUser.Trim()

            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing Then
                Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
                Me.INDUserCreate.Text = INDCodName
            End If
        End If

        Dim inventoryAdjustment = INDList.Item(0)
        For Each item In inventoryAdjustment.InventoryAdjustmentDetailReportXpo
            ValueLetter += (item.Quantity * item.UnitValue)
        Next
        Me.DataSource = INDList
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptAdjustmentInventory_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        'INDcllCostLetter.Text = Utils.Num2Text(Convert.ToDouble(ValueLetter)).ToString & IndigoSessionValues.CurrencyISO4217

        Dim _integerPart As Integer = Int(Convert.ToDecimal(ValueLetter))
        Dim _decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(ValueLetter) - _integerPart, "0.00"), 2)
        INDcllCostLetter.Text = String.Format("{0} {1}{2}",
                                                  Utils.Num2Text(_integerPart).ToString,
                                                   IndigoSessionValues.CurrencyName.ToUpper,
                                                  If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString } {Utils.ListDecimalCurrency(IndigoSessionValues.CurrencyISO4217)}", ""))

        Dim rewriteUserPrint = String.IsNullOrEmpty(IndigoSessionValues.UserIndigo)
        Dim userCode = If(rewriteUserPrint, GetCurrentColumnValue("UserCode"), IndigoSessionValues.UserIndigo)
        Dim userName = If(rewriteUserPrint, GetCurrentColumnValue("FullNameUser"), IndigoSessionValues.UserIndigoName)
        INDUserImp.Text = "Usuario Impresión : " & userCode & " - " & userName
        If ParametrosReporte(1) = 3 Then
            INDHeaderAdjustmentDetail.Visible = False
            DetailReport.Visible = False
            XrTable5.Visible = False
            XrTableRow8.Visible = False

            INDHeaderAdjustmentControl.Visible = True
            DetailReport1.Visible = True
        Else
            INDHeaderAdjustmentDetail.Visible = True
            DetailReport.Visible = True
            XrTable5.Visible = True
            XrTableRow8.Visible = True

            INDHeaderAdjustmentControl.Visible = False
            DetailReport1.Visible = False

        End If
    End Sub
End Class
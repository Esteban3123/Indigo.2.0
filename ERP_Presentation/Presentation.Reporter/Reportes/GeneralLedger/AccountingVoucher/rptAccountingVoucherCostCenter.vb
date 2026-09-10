#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo

#End Region

Public Class rptAccountingVoucherCostCenter
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Unidad operativa
    ''' </summary>
    Dim _operatingUnit As OperatingUnit

    Const CNameReport = "Contabilidad General.FrmAccountingVoucher"

    Dim listreport As List(Of JournalVoucherDetailsXpo)
    Private INDUser As Object
    Private OperatingUnit As xpCollection

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "IdAccounting.Id =" & ParametrosReporte(0)
        listreport = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of JournalVoucherDetailsXpo)(Nothing, filtroConsulta)

        For Each item In listreport
            'Se coloca en el campo de documento el nombre del tipo el cual genero el comprobante
            'If item.IdAccounting.EntityCode IsNot Nothing AndAlso item.IdAccounting.EntityCode.Count > 0 Then
            '    item.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(item.IdAccounting.EntityName), "") + " " + item.IdAccounting.EntityCode
            'Else
            '    item.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), "") + " " + item.IdAccounting.EntityCode
            'End If
            If item.IdAccounting.EntityCode IsNot Nothing AndAlso item.IdAccounting.EntityCode.Count > 0 Then
                item.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(item.IdAccounting.EntityName), item.IdAccounting.EntityCode)
            Else
                item.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), item.IdAccounting.Consecutive)
            End If
        Next
        Dim INDCodeUser = CType(listreport(0), JournalVoucherDetailsXpo).IdAccounting.CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        Me.DataSource = listreport

        If Me.ParametrosReporte(1) IsNot Nothing Then
            OperatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.ListOperatingUnitByIdd(ParametrosReporte(1))
            Me.INDlblAddress.Text = If(Me.OperatingUnit(0).Address IsNot Nothing, Me.OperatingUnit(0).Address.Trim() & If(Me.OperatingUnit(0).IdCity IsNot Nothing, If(Me.OperatingUnit(0).IdCity.Name IsNot Nothing, " " & Me.OperatingUnit(0).IdCity.Name.Trim() & If(Me.OperatingUnit(0).IdCity.DepartamentId IsNot Nothing, " - " & Me.OperatingUnit(0).IdCity.DepartamentId.Name.Trim(), String.Empty), String.Empty), String.Empty), String.Empty)
            Me.INDlblPhoneEmail.Text = If(Me.OperatingUnit(0).Phone IsNot Nothing, Me.OperatingUnit(0).Phone.Trim() & If(Me.OperatingUnit(0).EmailAudit IsNot Nothing, " - " & Me.OperatingUnit(0).EmailAudit.Trim(), String.Empty), String.Empty)
        End If

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptAccountingVoucherCostCenter.CNameReport
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptAccountingVoucherCostCenter_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        If INDUser.count() > 0 Then
            INDUserCreate.Text = INDUser(0).CodeName
        End If
        Dim table As XRTable = CType(XrTable2, XRTable)
        Dim row As XRTableRow = table.Rows(0)

        For Each item In listreport
            If item.Detail = "" Then
                table.Rows.Remove(XrTableRow3)
            End If
        Next
    End Sub
End Class
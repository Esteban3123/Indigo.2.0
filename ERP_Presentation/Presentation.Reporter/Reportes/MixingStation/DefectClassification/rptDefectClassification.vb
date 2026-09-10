#Region "Librerias Importadas"
Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
#End Region

Public Class rptDefectClassification
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private UserSupervisor As SecurityRepository.UserXpo

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim INDlist = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewDefectClassificationToReportXpo)(Nothing, $"CampaignDetailId = {CInt(ParametrosReporte(0))}")

        If INDlist IsNot Nothing AndAlso INDlist.Any() Then
            UserSupervisor = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetXPOObject(Of SecurityRepository.UserXpo)("UserCode = '" & INDlist(0).UserSupervisor.Trim() & "'")
            Me.DataSource = INDlist
        End If
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptSlipOut_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        If UserSupervisor IsNot Nothing Then
            XrTableCell8.Text = UserSupervisor.CodeName
        End If
    End Sub

    Private Sub GroupParenteralNutrition_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrLabel19.BeforePrint, XrTableRow7.BeforePrint,
                                                                                                    XrTableRow8.BeforePrint, XrTableRow9.BeforePrint,
                                                                                                    XrLabel21.BeforePrint
        If Not TryCast(DataSource, List(Of ViewDefectClassificationToReportXpo)).Any(Function(x) x.UnitDoseTypeMSClass = EUnitDoseTypeClass.ParenteralNutrition) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub XrLabel19_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrLabel19.BeforePrint
        If TryCast(DataSource, List(Of ViewDefectClassificationToReportXpo)).Any(Function(x) x.ValidationResult) Then
            XrLabel19.Text = "El peso de la nutrición SI se encuentra dentro del rango permitido."
        Else
            XrLabel19.Text = "El peso de la nutrición NO se encuentra dentro del rango permitido."
        End If
    End Sub

End Class
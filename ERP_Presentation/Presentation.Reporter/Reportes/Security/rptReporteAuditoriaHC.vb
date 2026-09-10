#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo
#End Region



Public Class rptReporteAuditoriaHC
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte


    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        'Dim filtro As String = " FECHCONSU >= '17-05-2019'"
        Dim filtro As String = ""
        
        filtro = "FECHCONSU >= '" & ParametrosReporte(0) & "' AND FECHCONSU <= '" & ParametrosReporte(1) & "' AND INGRESO IS NOT NULL"
        'Si el codigo del usuario no viene vacio
        If ParametrosReporte(2) <> ""
            filtro &= " AND  CODUSUCONS = '" & ParametrosReporte(2) & "'"
        End If

        'S el codigo del paciente no viene vacio
        If ParametrosReporte(3) <> ""
            filtro &= " AND  CODPACQCON = '" & ParametrosReporte(3) & "'"
        End If

        Dim filterOrder As List(Of HCAUDITORIAXpo ) = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).CrystalService.GetCollection(Of HCAUDITORIAXpo)(Nothing, filtro)
        
        Me.DataSource = filterOrder
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Private Sub rptReporteAuditoriaHC_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        
        'Cargar los valores del titulo
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class
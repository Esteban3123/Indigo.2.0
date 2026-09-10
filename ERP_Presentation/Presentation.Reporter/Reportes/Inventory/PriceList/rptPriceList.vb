#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
#End Region

Public Class rptPriceList
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = Nothing

        ' si filtra por Grupo
        If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta = "ProductGroupId.Code >= '" & ParametrosReporte(0) & "' AND ProductGroupId.Code <= '" & ParametrosReporte(1) & "'"
            Else
                filtroConsulta &= " AND ProductGroupId.Code >= '" & ParametrosReporte(0) & "' AND ProductGroupId.Code <= '" & ParametrosReporte(1) & "'"
            End If
        End If

        ' si filtra por SubGrupo
        If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta = "ProductSubGroupId.Code >= '" & ParametrosReporte(2) & "' AND ProductSubGroupId.Code <= '" & ParametrosReporte(3) & "'"
            Else
                filtroConsulta &= " AND ProductSubGroupId.Code >= '" & ParametrosReporte(2) & "' AND ProductSubGroupId.Code <= '" & ParametrosReporte(3) & "'"
            End If
        End If

        'si filtra por Producto
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta = "Code >= '" & ParametrosReporte(4) & "' AND Code <= '" & ParametrosReporte(5) & "'"
            Else
                filtroConsulta &= " AND Code >= '" & ParametrosReporte(4) & "' AND Code <= '" & ParametrosReporte(5) & "'"
            End If
        End If


        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of InventoryProductReportXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPriceList_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class
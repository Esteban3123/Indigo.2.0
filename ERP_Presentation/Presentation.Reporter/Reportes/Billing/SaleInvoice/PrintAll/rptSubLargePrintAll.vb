#Region "Librerias Importadas"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class rptSubLargePrintAll
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim INDList As New List(Of SubLargeDetail)

        If ParametrosReporte IsNot Nothing AndAlso ParametrosReporte(0) IsNot Nothing Then
            Dim i As Integer = 1
            For Each param In ParametrosReporte(0)
                If param.IsInvoiced = True Then 'Si Facturado
                    INDList.Add(New SubLargeDetail With
                        {
                            .Id = i,
                            .IsInvoiced = param.IsInvoiced,
                            .InvoiceId = param.InvoiceId,
                            .LiquidateMasterAccount = param.LiquidateMasterAccount,
                            .RevenueControlDetailId = param.RevenueControlDetailId,
                            .AdmissionNumber = param.AdmissionNumber,
                            .IsMasterAccount = param.IsMasterAccount
                        }
                    )
                Else 'No Facturado
                    INDList.Add(New SubLargeDetail With
                        {
                            .Id = i,
                            .IsInvoiced = param.IsInvoiced,
                            .RevenueControlDetailId = param.RevenueControlDetailId,
                            .AdmissionNumber = param.AdmissionNumber,
                            .LiquidateMasterAccount = param.LiquidateMasterAccount
                        }
                    )
                End If
                i = i + 1
            Next
        End If

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
End Class
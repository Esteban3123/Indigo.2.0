#Region "Librerias Importadas"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo

#End Region

Public Class rptDocumentAccountsPayable
    Implements IReport

    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarImagenes() Implements IReport.CargarImagenes
    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            Dim status = ParametrosReporte(2)
            Dim distributionLines As String = ParametrosReporte(3)
            Dim suppliers As String = ParametrosReporte(4)
            Dim accountPayables As String = ParametrosReporte(5)

            If status IsNot Nothing Then
                filtroConsulta &= String.Format(" AND Status IN ({0})", status)
            End If

            If Not String.IsNullOrEmpty(distributionLines) Then
                filtroConsulta &= String.Format(" AND IdSuppliersDistributionLines.IdDistributionLine.Id IN ({0})", distributionLines)
            End If

            If Not String.IsNullOrEmpty(suppliers) Then
                filtroConsulta &= String.Format(" AND IdSupplier.Id IN ({0})", suppliers)
            End If

            If Not String.IsNullOrEmpty(accountPayables) Then
                filtroConsulta &= String.Format(" AND Id IN ({0})", accountPayables)
            End If

            Dim listReport As List(Of PaymentsAccountPayable) = XpoServiceex.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsAccountPayable)(Nothing, filtroConsulta)
            Dim listDocumentsAccountPayable As List(Of DocumentsAccountPayable) = Nothing
            
            If listReport IsNot Nothing AndAlso listReport.Count > 0 Then
                listDocumentsAccountPayable = New List(Of DocumentsAccountPayable)
                For Each code In listReport.Select(Function(d) d.Code).Distinct()
                    listDocumentsAccountPayable.Add(New DocumentsAccountPayable With{.Code = code})
                Next
            End If

            Me.DataSource = listDocumentsAccountPayable
        Catch ex As Exception
            Base.MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

End Class
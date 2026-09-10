#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class rptSubCashReceipt
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "DocumentDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "#"

            Dim status = ParametrosReporte(2)
            Dim cashRegisters As String = ParametrosReporte(3)
            Dim entityBankAccounts As String = ParametrosReporte(4)
            Dim thirdParties As String = ParametrosReporte(5)
            Dim documents As String = ParametrosReporte(6)
            Dim users As String = ParametrosReporte(7)
            Dim currencyId As Integer? = ParametrosReporte(9)

            If status IsNot Nothing Then
                filtroConsulta &= String.Format(" AND Status IN ({0})", status)
            End If

            If Not String.IsNullOrEmpty(cashRegisters) AndAlso Not String.IsNullOrEmpty(entityBankAccounts) Then
                filtroConsulta &= String.Format(" AND (IdCashRegister.Id IN ({0}) OR IdBankAccount.Id IN ({1}))", cashRegisters, entityBankAccounts)
            ElseIf Not String.IsNullOrEmpty(cashRegisters) Then
                filtroConsulta &= String.Format(" AND IdCashRegister.Id IN ({0})", cashRegisters)
            ElseIf Not String.IsNullOrEmpty(entityBankAccounts) Then
                filtroConsulta &= String.Format(" AND IdBankAccount.Id IN ({0})", entityBankAccounts)
            End If

            If Not String.IsNullOrEmpty(thirdParties) Then
                filtroConsulta &= String.Format(" AND IdThirdParty.Id IN ({0})", thirdParties)
            End If

            If Not String.IsNullOrEmpty(documents) Then
                filtroConsulta &= String.Format(" AND Id IN ({0})", documents)
            End If

            If Not String.IsNullOrEmpty(users) Then
                filtroConsulta &= String.Format(" AND CreationUser IN ({0})", String.Join(",", users.Split(",").Select(Function(x) String.Format("'{0}'", x)).ToList()))
            End If

            If Not String.IsNullOrEmpty(currencyId) Then
                filtroConsulta &= $" AND CurrencyId = {currencyId}"
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCashReceiptsXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
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

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte
End Class
#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Resources
Imports System.Threading
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports System.Globalization
#End Region

Public Class rptThirdPartyDate2
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable par obtener la tabla de Trazabilidad
    ''' </summary>
    Dim dtReportAuxiliar As DataTable

    ''' <summary>
    ''' Diccionario para obtener unicamente los datos del Tercero y cuenta  para consultar el saldo
    ''' </summary>
    Dim dictionarySum As New Dictionary(Of Int64, Dictionary(Of Int64, Decimal))

    ''' <summary>
    ''' Variable par obtener el nombre de la moneda del libro
    ''' </summary>
    Dim currencyName As String

    ''' <summary>
    ''' Variable par obtener la abreviacion de la moneda del libro
    ''' </summary>
    Dim currencyAbbreviation As String

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSource1() As Task
        Try
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportAuxiliarAsync(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), ParametrosReporte(6), ParametrosReporte(7), ParametrosReporte(8), ParametrosReporte(9), ParametrosReporte(10), ParametrosReporte(11), ParametrosReporte(12), ParametrosReporte(13), False, Me.IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                dtReportAuxiliar = ds.Tables("ReportAuxiliar")
                Me.DataSource = dtReportAuxiliar
                Me.DataMember = "ReportAuxiliar"
                currencyName = dtReportAuxiliar.Rows(0).Field(Of String)("LegalBookCurrency")
                currencyAbbreviation = dtReportAuxiliar.Rows(0).Field(Of String)("LegalBookCurrencyAbbreviation")
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

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

    Private Sub rptThirdPartyDate2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        If currencyAbbreviation IsNot Nothing Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = New CultureInfo(currencyAbbreviation.GetCultureId).NumberFormat
            ApplyLocalization(_culture)
        End If

        'INDSortByDateOrConsecutive.Value = ParametrosReporte(7)
        If ParametrosReporte(2) = True Then
            'ocultamos detalles y cabeceras que no se muestran en el resumido y establecemos un tamañoa la detalle
            XrTable6.Visible = False
            XrTable5.Visible = False
            XrTable8.Visible = False
            XrTable9.Visible = False
            XrTable6.HeightF = 0
            XrTable5.HeightF = 0
            XrTable9.HeightF = 0

            Detail.HeightF = 0
            GroupHeader1.HeightF = 0
        End If

        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        Me.INDLblBook.Text = Me.ParametrosReporte(15).ToString()
        Me.CurrencyLabel.Text = "Moneda: " + currencyName
    End Sub

    Private Sub XrTable8_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable8.BeforePrint
        If ParametrosReporte(2) = False Then
            Dim table As XRTable = CType(sender, XRTable)
            If String.IsNullOrEmpty(GetCurrentColumnValue("Detail").ToString.Trim) AndAlso String.IsNullOrEmpty(GetCurrentColumnValue("CodeCostCenter").ToString.Trim) Then
                table.HeightF = 0
                table.Visible = False
                Detail.HeightF = 20
            Else
                table.Visible = True
                table.HeightF = 20
                Detail.HeightF = 40
            End If
        End If
    End Sub

    Private Sub XrTable5_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable5.BeforePrint
        'Validar si viene Null el Tercero
        Dim IdThirdParty As Int64

        If String.IsNullOrEmpty(GetCurrentColumnValue("IdThirdParty").ToString()) Then
            IdThirdParty = 0
        Else
            IdThirdParty = GetCurrentColumnValue("IdThirdParty")
        End If

        'Concatenamos el IdTercero y la IdCuenta para crear le Key del Diccionario y obtener el Balance para cada Tercero
        If Not dictionarySum.ContainsKey(IdThirdParty) Then
            Dim dictionaryThirdParty As New Dictionary(Of Int64, Decimal)
            If GetCurrentColumnValue("Nature") = 1 Then
                dictionaryThirdParty(IdThirdParty) = ((GetCurrentColumnValue("DebitValue") - GetCurrentColumnValue("CreditValue")) + GetCurrentColumnValue("Balance"))
                'Dim cell As XRTableCell = CType(XrTableCell23, XRTableCell)
                'solo cuando No es resumiso se tiene encuenta esta logica para mostrar el total
                If ParametrosReporte(2) = False Then
                    XrTableCell23.Text = Format(CDec(dictionaryThirdParty(IdThirdParty).ToString()), "c2")
                End If
            Else
                dictionaryThirdParty(IdThirdParty) = ((GetCurrentColumnValue("CreditValue") - GetCurrentColumnValue("DebitValue")) + GetCurrentColumnValue("Balance"))
                'Dim cell As XRTableCell = CType(XrTableCell23, XRTableCell)
                'solo cuando No es resumiso se tiene encuenta esta logica para mostrar el total
                If ParametrosReporte(2) = False Then
                    XrTableCell23.Text = Format(CDec(dictionaryThirdParty(IdThirdParty).ToString()), "c2")
                End If
            End If
            dictionarySum.Add(IdThirdParty, dictionaryThirdParty)
        Else
            Dim dictionaryThirdParty = dictionarySum(IdThirdParty)
            If Not dictionaryThirdParty.ContainsKey(IdThirdParty) Then
                dictionaryThirdParty.Add(IdThirdParty, 0)
                If GetCurrentColumnValue("Nature") = 1 Then
                    dictionaryThirdParty(IdThirdParty) += ((GetCurrentColumnValue("DebitValue") - GetCurrentColumnValue("CreditValue")) + GetCurrentColumnValue("Balance"))
                    'XrTableCell23.Text = String.Format("{0:$0}", ValueSum.ToString())
                    'solo cuando No es resumiso se tiene encuenta esta logica para mostrar el total
                    If ParametrosReporte(2) = False Then
                        XrTableCell23.Text = Format(CDec(dictionaryThirdParty(IdThirdParty).ToString()), "c2")
                    End If
                Else
                    dictionaryThirdParty(IdThirdParty) = 0
                    dictionaryThirdParty(IdThirdParty) += ((GetCurrentColumnValue("CreditValue") - GetCurrentColumnValue("DebitValue")) + GetCurrentColumnValue("Balance"))
                    'XrTableCell23.Text = ValueSum.ToString
                    'solo cuando No es resumiso se tiene encuenta esta logica para mostrar el total
                    If ParametrosReporte(2) = False Then
                        XrTableCell23.Text = Format(CDec(dictionaryThirdParty(IdThirdParty).ToString()), "c2")
                    End If
                End If
            Else
                If GetCurrentColumnValue("Nature") = 1 Then
                    dictionaryThirdParty(IdThirdParty) += (GetCurrentColumnValue("DebitValue") - GetCurrentColumnValue("CreditValue"))
                    'XrTableCell23.Text = ValueSum.ToString
                    'solo cuando No es resumiso se tiene encuenta esta logica para mostrar el total
                    If ParametrosReporte(2) = False Then
                        XrTableCell23.Text = Format(CDec(dictionaryThirdParty(IdThirdParty).ToString()), "c2")
                    End If
                Else
                    dictionaryThirdParty(IdThirdParty) += (GetCurrentColumnValue("CreditValue") - GetCurrentColumnValue("DebitValue"))
                    'XrTableCell23.Text = ValueSum.ToString
                    'solo cuando No es resumiso se tiene encuenta esta logica para mostrar el total
                    If ParametrosReporte(2) = False Then
                        XrTableCell23.Text = Format(CDec(dictionaryThirdParty(IdThirdParty).ToString()), "c2")
                    End If
                End If
            End If
            dictionarySum(IdThirdParty) = dictionaryThirdParty
        End If
    End Sub

    Private Sub XrTableCell35_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell35.BeforePrint
        'Validar si viene Null el Tercero
        Dim idThird As Integer
        If String.IsNullOrEmpty(GetCurrentColumnValue("IdThirdParty").ToString()) Then
            idThird = 0
        Else
            idThird = GetCurrentColumnValue("IdThirdParty")
        End If

        XrTableCell35.Text = Format(CDec(dictionarySum(idThird).Sum(Function(x) x.Value).ToString()), "c2")
    End Sub

    Private Sub XrTableRow5_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow5.BeforePrint
        Dim isMovement = GetCurrentColumnValue("IsMovement")
        If Not isMovement Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub XrTableRow9_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow9.BeforePrint
        Dim isMovement = GetCurrentColumnValue("IsMovement")
        If Not isMovement Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub
End Class
#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports Presentation.Base
#End Region

Public Class rptContributionFundsIncentivePayments2
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private boss As List(Of PayrollPayrollSettingsReportXpo)

    Private bossOnly As String
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try

            Dim filtroConsulta As String = Nothing

            filtroConsulta = "PeriodInitialDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND PeriodEndDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            ''filtro por estado
            'If ParametrosReporte(3) IsNot Nothing AndAlso Not String.IsNullOrEmpty(ParametrosReporte(3).ToString.Trim) Then
            '    filtroConsulta &= " AND RegisterStatus = '" & ParametrosReporte(3) & "'"
            'End If

            'filtro Fondo Salud
            If ParametrosReporte(2) = 1 Then
                If ParametrosReporte(8) = True Then
                    If ParametrosReporte(7) IsNot Nothing Then
                        filtroConsulta &= " AND ConceptClass = '019' AND IdThirdParty = '" & ParametrosReporte(7) & "'"
                    Else
                        filtroConsulta &= " AND ConceptClass = '019'"
                    End If
                Else
                    If ParametrosReporte(7) IsNot Nothing Then
                        filtroConsulta &= " AND ConceptClass = '017' AND IdThirdParty = '" & ParametrosReporte(7) & "'"
                    Else
                        filtroConsulta &= " AND ConceptClass = '017'"
                    End If
                End If
            End If

            'filtro Fondo Pensión
            If ParametrosReporte(2) = 2 Then
                If ParametrosReporte(8) = True Then
                    If ParametrosReporte(7) IsNot Nothing Then
                        filtroConsulta &= " AND ConceptClass = '016' AND IdThirdParty = '" & ParametrosReporte(7) & "'"
                    Else
                        filtroConsulta &= " AND ConceptClass = '016'"
                    End If
                Else
                    If ParametrosReporte(7) IsNot Nothing Then
                        filtroConsulta &= " AND ConceptClass = '014' AND IdThirdParty = '" & ParametrosReporte(7) & "'"
                    Else
                        filtroConsulta &= " AND ConceptClass = '014'"
                    End If
                End If
            End If

            'filtro Fondo Cesantías
            If ParametrosReporte(2) = 3 Then
                If ParametrosReporte(7) IsNot Nothing Then
                    filtroConsulta &= " AND ConceptClass = '008' AND IdThirdParty = '" & ParametrosReporte(7) & "'"
                Else
                    filtroConsulta &= " AND ConceptClass = '008'"
                End If
            End If

            'filtro Fondo Riesgos Profesionales
            If ParametrosReporte(2) = 4 Then
                If ParametrosReporte(7) IsNot Nothing Then
                    filtroConsulta &= " AND ConceptClass = '009' AND IdThirdParty = '" & ParametrosReporte(7) & "'"
                Else
                    filtroConsulta &= " AND ConceptClass = '009'"
                End If
            End If

            'filtro Pensión fondo de solidaridad
            If ParametrosReporte(2) = 5 Then
                If ParametrosReporte(7) IsNot Nothing Then
                    filtroConsulta &= " AND ConceptClass = '038' AND IdThirdParty = '" & ParametrosReporte(7) & "'"
                Else
                    filtroConsulta &= " AND ConceptClass = '038'"
                End If
            End If


            'filtro Pensión – Fondo de Solidaridad
            If ParametrosReporte(2) = 6 Then
                If ParametrosReporte(7) IsNot Nothing Then
                    filtroConsulta &= " AND ConceptClass = '014' AND IdThirdParty = '" & ParametrosReporte(7) & "'"
                Else
                    filtroConsulta &= " AND ConceptClass = '014'"
                End If
            End If


            'filtro por estado
            If ParametrosReporte(3) <> "T" Then
                filtroConsulta &= " AND RegisterStatus = '" & ParametrosReporte(3) & "'"
            End If

            'filtro por empleado
            If ParametrosReporte(4) IsNot Nothing Then
                filtroConsulta &= " AND EmployeeId = " & ParametrosReporte(4)
            End If

            'filtro por Grupo
            If ParametrosReporte(5) IsNot Nothing And ParametrosReporte(6) IsNot Nothing Then
                filtroConsulta &= " AND GroupCode >= '" & ParametrosReporte(5) & "' AND GroupCode <= '" & ParametrosReporte(6) & "'"
            End If

            'filtro Periodo
            If ParametrosReporte(9) <> 3 Then
                filtroConsulta &= " And Period = " & ParametrosReporte(9)
            End If

            Dim sucursalIni As String = IIf(ParametrosReporte(10) Is Nothing Or CStr(ParametrosReporte(10)) = String.Empty, "NULL", CStr(ParametrosReporte(10)))
            Dim sucursalFin As String = IIf(ParametrosReporte(11) Is Nothing Or CStr(ParametrosReporte(11)) = String.Empty, "NULL", CStr(ParametrosReporte(11)))

            filtroConsulta = String.Format("{0} AND ((BranchOfficeId >= {1} AND BranchOfficeId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVContributionFundsIncentiveReportXpo)(Nothing, filtroConsulta)
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

    Private Sub rptContributionFundsIncentivePayments2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDPrGroup.Value = ParametrosReporte(2)
        'If ParametrosReporte(2) = 1 Then
        '    Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(537.1201!, 0.0!)
        '    Me.XrLine1.SizeF = New System.Drawing.SizeF(262.882!, 4.249954!)

        '    If ParametrosReporte(8) = True Then
        '        ''titulo
        '        XrTable6.Visible = True

        '        ''detalle
        '        XrTable14.Visible = False

        '        ''totales grupo
        '        XrTable8.Visible = False

        '        ''totales reporte
        '        XrTable9.Visible = False

        '        INDPrGroup.Value = 10
        '    Else
        '        ''titulo
        '        XrTable6.Visible = True

        '        ''detalle
        '        XrTable14.Visible = True

        '        ''totales grupo
        '        XrTable8.Visible = False

        '        ''totales reporte
        '        XrTable9.Visible = False

        '        INDPrGroup.Value = ParametrosReporte(2)
        '    End If
        'ElseIf ParametrosReporte(2) = 2 Then
        '    Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(537.1201!, 0.0!)
        '    Me.XrLine1.SizeF = New System.Drawing.SizeF(262.882!, 4.249954!)

        '    If ParametrosReporte(8) = True Then
        '        ''titulo
        '        XrTable6.Visible = True

        '        ''cabecera
        '        XrTableCell25.Text = "Pensión"

        '        ''detalle
        '        XrTable14.Visible = False

        '        ''totales grupo
        '        XrTable8.Visible = False

        '        ''totales reporte
        '        XrTable9.Visible = False

        '        INDPrGroup.Value = 11
        '    Else
        '        ''titulo
        '        XrTable6.Visible = True

        '        ''cabecera
        '        XrTableCell25.Text = "Pensión"
        '        ''detalle

        '        XrTable14.Visible = False

        '        ''totales grupo
        '        XrTable8.Visible = False

        '        ''totales reporte
        '        XrTable9.Visible = False

        '        INDPrGroup.Value = ParametrosReporte(2)
        '    End If
        '    XrTableCell16.Text = "Fondos de Pensión:"

        'ElseIf ParametrosReporte(2) = 3 Then
        '    Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(537.1201!, 0.0!)
        '    Me.XrLine1.SizeF = New System.Drawing.SizeF(262.882!, 4.249954!)
        '    ''titulo
        '    XrTable6.Visible = True

        '    ''cabecera
        '    XrTableCell25.Text = "Cesantías"

        '    ''detalle
        '    XrTable14.Visible = False

        '    ''totales grupo
        '    XrTable8.Visible = True

        '    ''totales reporte
        '    XrTable9.Visible = True

        '    XrTableCell16.Text = "Fondos de Cesantías:"
        'ElseIf ParametrosReporte(2) = 4 Then
        '    Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(537.1201!, 0.0!)
        '    Me.XrLine1.SizeF = New System.Drawing.SizeF(262.882!, 4.249954!)
        '    ''titulo
        '    XrTable6.Visible = True

        '    ''cabecera
        '    XrTableCell25.Text = "Riesgos Profesionales"

        '    ''detalle
        '    XrTable14.Visible = False

        '    ''totales grupo
        '    XrTable8.Visible = False

        '    ''totales reporte
        '    XrTable9.Visible = False

        '    XrTableCell16.Text = "Fondos de Riesgos:"
        'ElseIf ParametrosReporte(2) = 5 Then
        '    Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(537.1201!, 0.0!)
        '    Me.XrLine1.SizeF = New System.Drawing.SizeF(262.882!, 4.249954!)
        '    ''titulo
        '    XrTable6.Visible = True

        '    ''cabecera
        '    XrTableCell25.Text = "Solidaridad"

        '    ''detalle
        '    XrTable14.Visible = False

        '    ''totales grupo
        '    XrTable8.Visible = False

        '    ''totales reporte
        '    XrTable9.Visible = False

        '    XrTableCell16.Text = "Fondos de Solidaridad:"

        'ElseIf ParametrosReporte(2) = 6 Then
        '    'Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(480.6701!, 0.0!)
        '    'Me.XrLine1.SizeF = New System.Drawing.SizeF(319.33!, 4.249954!)
        '    ''titulo
        '    XrTable6.Visible = False

        '    ''cabecera
        '    XrTableCell25.Text = "Pensión fondo de solidaridad"
        '    ''detalle
        '    XrTable14.Visible = False

        '    ''totales grupo
        '    XrTable8.Visible = False

        '    ''totales reporte
        '    XrTable9.Visible = False


        '    XrTableCell16.Text = "Fondos de Pensión fondo de solidaridad:"
        'End If


        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblDate.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName


        boss = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollPayrollSettingsReportXpo)(Nothing, Nothing)

        Dim bos = From b In boss
                   Select jefe = b.PayrollChiefThirdPartyId.Name

        For Each j In bos
            bossOnly = j.ToString()
        Next

        XrLabel1.Text = bossOnly


    End Sub
End Class
#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Presentation.Base
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class rptFixedAssetEntryParts
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim dictionarySumTotal As New Dictionary(Of String, Integer)

    'Dim sumTotal As Integer = 0

    Dim sumQuantity As Integer = 0

    Dim INDLIst As List(Of FixedAssetVReportFixedAssetEntryItemDetailPartReportXpo)
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "IdEntry = " & ParametrosReporte(0)
            'INDLIst = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of FixedAssetVReportFixedAssetEntryItemDetailPartReportXpo)(Nothing, filtroConsulta)
            'If INDLIst.Count > 0 Then
            '    'Consultar CreationUser
            '    Dim INDNameUser = 0 ' CType(INDLIst(0), FixedAssetVReportFixedAssetEntryItemDetailPartReportXpo).FixedAssetEntryItemId.FixedAssetEntryId.CreationUser.Trim()

            '    Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            '    If INDListUser IsNot Nothing Then
            '        Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
            '        Me.INDUserCreate.Text = INDCodName
            '    End If
            'End If
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of FixedAssetVReportFixedAssetEntryItemDetailPartReportXpo)(Nothing, filtroConsulta)
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

    Private Sub rptFixedAssetEntryParts_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 And Parameters(1).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDPrFixedAssetEntryId").Value, ParametrosFilter("INDPrOperatingUnitId").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName

        Dim address, phoneNumber, codeips, city, owner As String
        'cargar direccion, telefono y codigo ips
        If ParametrosReporte(1) IsNot Nothing Then
            Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(ParametrosReporte(1))
            Dim SettingFixedAsset = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.ListSettingFixedAssetByOperatingUnitId(ParametrosReporte(1))
            owner = SettingFixedAsset(0).IdThirdPartyResponsible.NitName
            address = operatingUnit(0).Address
            phoneNumber = operatingUnit(0).Phone
            codeips = operatingUnit(0).IPSCode
            If operatingUnit(0).IdCity IsNot Nothing Then
                city = operatingUnit(0).IdCity.Descripcion
            Else
                city = "No asignada(o)"
            End If
        Else
            address = "No asignada(o)"
            phoneNumber = "No asignada(o)"
            codeips = "No asignada(o)"
            city = "No asignada(o)"
            owner = "No asignada(o)"
        End If

        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDTcAddress.Text = address
        INDTcCity.Text = city
        INDTcPhone.Text = phoneNumber
        Jefe.Text = owner

        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

    Private Sub GroupHeader3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader3.BeforePrint
        'llave agrupada por Codeitem and Plate para Dictionary
        Dim IdKey As String = (GetCurrentColumnValue("CodeItem") & GetCurrentColumnValue("Plate")).ToString

        'Pintar el total por reporte
        If Not dictionarySumTotal.ContainsKey(IdKey) Then
            'sumTotal = sumTotal + GetCurrentColumnValue("UnitValue")
            sumQuantity = sumQuantity + 1
            XrTableCell32.Text = sumQuantity.ToString
            'XrTableCell76.Text = "$" & sumTotal.ToString
            dictionarySumTotal.Add(IdKey, sumQuantity)
        End If

        'ocultar grupos
        If String.IsNullOrEmpty((GetCurrentColumnValue("CodePartAccesorieConsumible"))) Then
            'Dim table1 As XRTable = CType(XrTable14, XRTable)
            'table1.Visible = False
            CType(XrTable14, XRTable).Visible = False
            CType(XrTable13, XRTable).Visible = False
            CType(XrTable15, XRTable).Visible = False

            CType(XrTable14, XRTable).HeightF = 0
            CType(XrTable13, XRTable).HeightF = 0
            CType(XrTable15, XRTable).HeightF = 0
        Else
            CType(XrTable14, XRTable).Visible = True
            CType(XrTable13, XRTable).Visible = True
            CType(XrTable15, XRTable).Visible = True

            CType(XrTable14, XRTable).HeightF = 20
            CType(XrTable13, XRTable).HeightF = 20
            CType(XrTable15, XRTable).HeightF = 20
        End If
    End Sub
End Class
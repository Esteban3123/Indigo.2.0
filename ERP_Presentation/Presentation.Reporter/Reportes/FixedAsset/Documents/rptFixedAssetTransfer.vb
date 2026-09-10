#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports System.Globalization

#End Region

Public Class rptFixedAssetTransfer
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim INDList As List(Of FixedAssetTransferDetailReportXpo)



    ''' <summary>
    ''' Almacenar  las configuraciones de activos fijos por unidad operativa
    ''' </summary>
    Private resSettingFixedAssetCache As New Dictionary(Of Integer, SettingFixedAssetXpo)()




    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "FixedAssetTransferId.Id = " & ParametrosReporte(0)
        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetTransferDetailReportXpo)(Nothing, filtroConsulta)
        If INDList.Count > 0 Then
            Dim INDNameUser = CType(INDList(0), FixedAssetTransferDetailReportXpo).FixedAssetTransferId.CreationUser.Trim()

            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing Then
                Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
                Me.INDUserCreate.Text = INDCodName
            End If

            'oculta Responsible
            Dim table8 As XRTable = CType(XrTable8, XRTable)
            'quien entrega
            Dim table9 As XRTable = CType(XrTable9, XRTable)
            Dim table16 As XRTable = CType(XrTable16, XRTable)
            'quien recibe
            Dim table11 As XRTable = CType(XrTable11, XRTable)
            Dim table17 As XRTable = CType(XrTable17, XRTable)


            'oculta Location
            Dim table10 As XRTable = CType(XrTable10, XRTable)
            'quien entrega
            Dim table14 As XRTable = CType(XrTable14, XRTable)
            Dim table12 As XRTable = CType(XrTable12, XRTable)
            'quien recibe
            Dim table15 As XRTable = CType(XrTable15, XRTable)
            Dim table13 As XRTable = CType(XrTable13, XRTable)


            If (CType(INDList(0), FixedAssetTransferDetailReportXpo).FixedAssetTransferId.SourceLocationId Is Nothing OrElse CType(INDList(0), FixedAssetTransferDetailReportXpo).FixedAssetTransferId.TargetLocationId Is Nothing) Then
                'XrTable10.Visible = False
                table10.Rows.Remove(XrTableRow12)
                table10.Rows.Remove(XrTableRow9)

                table14.Rows.Remove(XrTableRow7)
                table12.Rows.Remove(XrTableRow19)

                table15.Rows.Remove(XrTableRow8)
                table13.Rows.Remove(XrTableRow16)


            ElseIf (CType(INDList(0), FixedAssetTransferDetailReportXpo).FixedAssetTransferId.SourceResponsibleId Is Nothing OrElse CType(INDList(0), FixedAssetTransferDetailReportXpo).FixedAssetTransferId.TargetResponsibleId Is Nothing) Then
                'XrTable8.Visible = False
                table8.Rows.Remove(XrTableRow4)

                table9.Rows.Remove(XrTableRow2)
                table16.Rows.Remove(XrTableRow13)

                table11.Rows.Remove(XrTableRow14)
                table17.Rows.Remove(XrTableRow15)
            End If
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

    Private Sub rptFixedAssetTransfer_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDIdTransfer").Value, ParametrosFilter("INDOperationUnit").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName


        Dim address, phoneNumber, codeips, city As String
        'cargar direccion, telefono y codigo ips
        If ParametrosReporte(1) IsNot Nothing Then
            Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(ParametrosReporte(1))
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
        End If

        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit & " - Dirección: " & address &
                            " - Teléfono: " & phoneNumber & " - Código IPS: " & codeips

        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

    Private Sub XrTable3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable3.BeforePrint
        Dim Observation = (GetCurrentColumnValue("INDCfObservation"))
        Dim table8 As XRTable = CType(XrTable19, XRTable)
        Dim DetailHeader As DetailBand = CType(Detail, DetailBand)
        If String.IsNullOrEmpty(Observation) Then
            'table8.Rows.Remove(XrTableRow2)
            table8.Visible = False
            table8.HeightF = 0
            DetailHeader.HeightF = 23
        Else
            table8.Visible = True
            DetailHeader.HeightF = 49
            table8.HeightF = 20
        End If
    End Sub




    Private Sub XrTableCell32_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell32.BeforePrint

        Dim currentRow As FixedAssetTransferDetailReportXpo = GetCurrentRow()
        Dim operatingUnitId As Integer = currentRow.FixedAssetTransferId.OperatingUnitId
        Dim resSettingFixedAsset As SettingFixedAssetXpo
        If Not resSettingFixedAssetCache.TryGetValue(operatingUnitId, resSettingFixedAsset) Then
            Dim filter As String = "OperatingUnitId = " & operatingUnitId
            resSettingFixedAsset = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer) _
            .FixedAsset.GetCollection(Of SettingFixedAssetXpo)(Nothing, filter).FirstOrDefault()

            If resSettingFixedAsset IsNot Nothing Then
                resSettingFixedAssetCache(operatingUnitId) = resSettingFixedAsset
            End If
        End If
        Dim decimalPlaces As Integer = resSettingFixedAsset.CurrencyId.RoundingType
        Dim formatString As String = GetDecimalFormatString(decimalPlaces)


        XrTableCell32.Text = Utils.GetMoneyWithISO4217(
        currentRow.PhysicalAssetId.HistoricalValue.ToString(formatString),
        If(String.IsNullOrEmpty(resSettingFixedAsset?.CurrencyId?.Abbreviation),
           resSettingFixedAsset.CurrencyId.ISO4217Xpo.CodeAbbreviation,
           resSettingFixedAsset.CurrencyId.Abbreviation)
    )


    End Sub

    ''' <summary>
    ''' Deveulve el formateo deacuerdo a la cantidad parametrizada
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="decimalPlaces"></param>
    ''' <returns></returns>
    Private Function GetDecimalFormatString(decimalPlaces As Integer) As String
        Dim formattedValue As String
        Select Case decimalPlaces
            Case 1 ' 0,01 A Dos Decimales
                formattedValue = "F2" ' Dos decimales
            Case 2 ' 0,1 A Un Decimal
                formattedValue = "F1" ' Un decimal
            Case 3 ' 1 Ninguno
                formattedValue = "F0" ' Sin decimales
            Case 4 ' 10 A la Decena
                formattedValue = "F0" ' Sin decimales
            Case 5 ' 100 A la Centena
                formattedValue = "F0" ' Sin decimales
            Case 6 ' 1000 A la Milésima
                formattedValue = "F0" ' Sin decimales
            Case Else
                formattedValue = "F2" ' Valor por defecto
        End Select
        Return formattedValue
    End Function

    Private Sub XrTableCell40_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell40.SummaryGetResult

        Dim currentRow As FixedAssetTransferDetailReportXpo = GetCurrentRow()
        Dim operatingUnitId As Integer = currentRow.FixedAssetTransferId.OperatingUnitId
        Dim resSettingFixedAsset As SettingFixedAssetXpo
        If Not resSettingFixedAssetCache.TryGetValue(operatingUnitId, resSettingFixedAsset) Then
            Dim filter As String = "OperatingUnitId = " & operatingUnitId
            resSettingFixedAsset = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer) _
            .FixedAsset.GetCollection(Of SettingFixedAssetXpo)(Nothing, filter).FirstOrDefault()

            If resSettingFixedAsset IsNot Nothing Then
                resSettingFixedAssetCache(operatingUnitId) = resSettingFixedAsset
            End If
        End If
        Dim totalValue As Decimal = e.CalculatedValues.ToEntityList(Of Decimal).Sum()
        Dim decimalPlaces As Integer = resSettingFixedAsset.CurrencyId.RoundingType

        Dim formatString As String = GetDecimalFormatString(decimalPlaces)

        e.Result = Utils.GetMoneyWithISO4217(
        totalValue.ToString(formatString),
        If(String.IsNullOrEmpty(resSettingFixedAsset?.CurrencyId?.Abbreviation),
           resSettingFixedAsset.CurrencyId.ISO4217Xpo.CodeAbbreviation,
           resSettingFixedAsset.CurrencyId.Abbreviation)
    )
        e.Handled = True

    End Sub
End Class
#Region "Librerias Importadas"
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Globalization
Imports System.Linq
#End Region

Public Class rptEmployeeResume
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Almacena la abreviación de la moneda
    ''' </summary>
    Private CurrencyAbbreviation As String = SessionValues.Instance.CurrencyISO4217

    ''' <summary>
    ''' Lista del empleado que sera almacenado en el datasorce
    ''' </summary>
    Private employeeList As New List(Of Domain.Payroll.Entities.Employee)

    ''' <summary>
    ''' Propiedad que obtiene los parametros de reporte 
    ''' </summary>
    ''' <returns></returns>
    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    ''' <summary>
    ''' Constante que almacena la clase a la que pertenece este reporte
    ''' </summary>
    Const CNameReport = "Payroll.FrmEmployee"

    ''' <summary>
    ''' Obtengo el nombre del reporte
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptEmployeeResume.CNameReport
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para asignar el datasource del reporte
    ''' </summary>
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim employee As Domain.Payroll.Entities.Employee = DirectCast(ParametrosReporte(0), Domain.Payroll.Entities.Employee)

        ' Recorre cada contrato y filtra solo fondos activos
        For Each contract In employee.Contract

            Dim activeFunds = contract.FundContract.Where(Function(f) f.State).ToList()

            contract.FundContract.Clear()

            For Each fund In activeFunds
                contract.FundContract.Add(fund)
            Next
        Next

        employeeList.Add(employee)
        Me.DataSource = employeeList
        GetContractAudit(employee.Id)
    End Sub

    ''' <summary>
    ''' Obtiene y filtra los cambios de unidad funcional del historial
    ''' de auditoría de contratos
    ''' </summary>
    ''' <param name="id">Id del empleado</param>
    Private Sub GetContractAudit(id As Integer)
        Try
            Dim auditDataCollection = Infrastructure.Data.Xpo.XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.ListContractAuditByEmployeeId(id)
            ' Filtrar solo los cambios de unidad funcional
            ' Se filtra por Type que contenga "unidad funcional" o FieldName que contenga "FunctionalUnitId" o "FunctionalUnit"
            Dim functionalUnitChanges As New List(Of ViewContractAuditXpo)
            For Each item As ViewContractAuditXpo In auditDataCollection
                Dim fieldNameValue As String = If(item.FieldName, String.Empty).ToLower()
                ' Verificar si es un cambio de unidad funcional
                If fieldNameValue.Contains("functionalunitid") Then
                    functionalUnitChanges.Add(item)
                End If
            Next
            DetailReport.DataSource = functionalUnitChanges
        Catch ex As Exception
            DetailReport.DataSource = New List(Of ViewContractAuditXpo)()
        End Try
    End Sub
    ''' <summary>
    ''' Meotod para asignar los labels con la informacion personal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Label_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles AddressLabel.BeforePrint, CityLabel.BeforePrint, DepartmentLabel.BeforePrint,
            PhoneLabel.BeforePrint, EmailLabel.BeforePrint

        Dim label = CType(sender, XRLabel)
        Dim employee = CType(GetCurrentRow(), Domain.Payroll.Entities.Employee)

        Select Case label.Name
            Case NameOf(AddressLabel)
                label.Text = employee?.ThirdParty?.Person?.Address?.LastOrDefault()?.Addresss
            Case NameOf(CityLabel)
                label.Text = employee?.ThirdParty?.Person?.Address?.LastOrDefault()?.CityName
            Case NameOf(DepartmentLabel)
                label.Text = employee?.ThirdParty?.Person?.Address?.LastOrDefault()?.DepartmentName
            Case NameOf(PhoneLabel)
                label.Text = employee?.ThirdParty?.Person?.Phone?.Where(Function(p) p.IdPhoneType = 2).LastOrDefault()?.Phone1
            Case NameOf(EmailLabel)
                label.Text = employee?.ThirdParty?.Person?.Email?.LastOrDefault()?.Email1
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que se ejecuta antes de iniciar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rptEmployeeResume_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit & " - Dirección: " & IndigoSessionValues.IndigoCompanyAddress
    End Sub


    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint

    End Sub

    ''' <summary>
    ''' Formatea la fecha en el formato "dd de mes de aaaa" en español (ej: 26 de noviembre de 2025)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub XrTableCell81_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrTableCell81.BeforePrint
        Try
            Dim cell = CType(sender, XRTableCell)
            Dim currentRow = cell.Report.GetCurrentRow()

            If currentRow IsNot Nothing Then
                Dim auditItem = TryCast(currentRow, ViewContractAuditXpo)
                If auditItem IsNot Nothing Then
                    Dim fechaValue As Object = auditItem.Date
                    If fechaValue IsNot Nothing AndAlso TypeOf fechaValue Is DateTime Then
                        Dim fecha As DateTime = CType(fechaValue, DateTime)
                        Dim cultureInfo As New CultureInfo("es-CO")
                        cell.Text = fecha.ToString("dd 'de' MMMM 'de' yyyy", cultureInfo)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

End Class
#Region "Librerias Importadas"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountManagementRepository
Imports Infrastructure.Data.Xpo.AccountManagementRespository

#End Region

''' <summary>
''' Reporte Resumido de Gestión de Cuentas
''' </summary>
Public Class rptAccountManagementReportSummary
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Carga el datasource del reporte de forma síncrona
    ''' </summary>
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filter As String = Nothing

        'Filtro por fechas (obligatorio) - usando AdmissionDate de la vista
        If ParametrosReporte(0) IsNot Nothing AndAlso ParametrosReporte(1) IsNot Nothing Then
            Dim fechaInicio As Date = CDate(ParametrosReporte(0)).Date
            Dim fechaFin As Date = CDate(ParametrosReporte(1)).Date.AddDays(1).AddSeconds(-1)

            filter = "AdmissionDate >= #" & fechaInicio.ToString("yyyy-MM-dd HH:mm:ss") & "# AND AdmissionDate <= #" & fechaFin.ToString("yyyy-MM-dd HH:mm:ss") & "#"
        End If

        'Si filtra por Estado Folio (opcional)
        If ParametrosReporte(2) IsNot Nothing Then
            filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("FolioStatus = {0}", ParametrosReporte(2))
        End If

        'Si filtra por Centro de Atención (opcional)
        If ParametrosReporte(3) IsNot Nothing AndAlso Not String.IsNullOrEmpty(ParametrosReporte(3).ToString()) Then
            filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("AttentionCenterCode = '{0}'", ParametrosReporte(3))
        End If

        'Si filtra por Tercero (Paciente) - usa NITs/Códigos de paciente
        'NOTA: ParametrosReporte(4) contiene NITs separados por comas desde el selector
        If Not String.IsNullOrEmpty(ParametrosReporte(4)) Then
            ' Los NITs son strings, necesitan ir entre comillas simples
            Dim nits = ParametrosReporte(4).ToString().Split(","c)
            Dim nitsQuoted = String.Join(",", nits.Select(Function(n) "'" & n.Trim() & "'"))
            filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("PatientCode IN ({0})", nitsQuoted)
        End If

        'Si filtra por Entidad (opcional, múltiple)
        If Not String.IsNullOrEmpty(ParametrosReporte(5)) Then
            filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("HealthAdministrationId IN ({0})", ParametrosReporte(5))
        End If

        'Si filtra por Grupo de Atención (opcional, múltiple)
        If Not String.IsNullOrEmpty(ParametrosReporte(6)) Then
            filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("CareGroupId IN ({0})", ParametrosReporte(6))
        End If

        'Si filtra por Usuario Asignado (opcional, múltiple) - usa UserCode
        If Not String.IsNullOrEmpty(ParametrosReporte(7)) Then
            ' Los UserCode son strings, necesitan ir entre comillas simples
            Dim userCodes = ParametrosReporte(7).ToString().Split(","c)
            Dim userCodesQuoted = String.Join(",", userCodes.Select(Function(u) "'" & u.Trim() & "'"))
            filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("CurrentOwnerCode IN ({0})", userCodesQuoted)
        End If

        'Si filtra por Área de Gestión (opcional, múltiple)
        If Not String.IsNullOrEmpty(ParametrosReporte(8)) Then
            filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("ManagementAreaId IN ({0})", ParametrosReporte(8))
        End If

        Dim data = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountManagementService.GetCollection(Of ViewReportAccountManagementXpo)(Nothing, filter)
        'Cargar datos desde la vista
        Me.BindingSource1.DataSource = data

    End Sub

    ''' <summary>
    ''' Carga el datasource del reporte de forma asíncrona
    ''' </summary>
    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    ''' <summary>
    ''' Carga las imágenes del reporte (logos, encabezados, etc.)
    ''' </summary>
    Public Sub CargarImagenes() Implements IReport.CargarImagenes
        ' Implementar si se requieren imágenes en el reporte
    End Sub

    ''' <summary>
    ''' Nombre del reporte
    ''' </summary>
    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return "Reporte Resumido de Gestión de Cuentas"
        End Get
    End Property

    ''' <summary>
    ''' Parámetros del reporte
    ''' 0 = Fecha Inicial
    ''' 1 = Fecha Final
    ''' 2 = Estado Folio (opcional)
    ''' 3 = Centro de Atención (opcional)
    ''' 4 = IDs Terceros (opcional, string separado por comas)
    ''' 5 = IDs Entidades (opcional, string separado por comas)
    ''' 6 = IDs Grupos de Atención (opcional, string separado por comas)
    ''' 7 = IDs Usuarios (opcional, string separado por comas)
    ''' 8 = IDs Áreas de Gestión (opcional, string separado por comas)
    ''' </summary>
    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    ''' <summary>
    ''' Evento antes de imprimir el reporte
    ''' </summary>
    Private Sub rptAccountManagementReportSummary_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.BeforePrint
        'Configurar títulos y encabezados
        Me.INDLblTitle.Text = "INFORME GESTION DE CUENTAS RESUMIDO"
        Me.INDLblNameCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "NIT: " & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        'Mostrar rango de fechas
        If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
            Me.INDLblSubTitle.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " A " & CDate(Me.ParametrosReporte(1)).ToString("dd De MMMM Del yyyy")
        End If

        'Configurar fecha y hora de impresión
        Me.INDLblDatePrint.Text = "Fecha Impresión: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
    End Sub

End Class


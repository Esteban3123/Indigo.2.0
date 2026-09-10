Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports System.IO
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.Parameters
Imports System.Windows.Forms
Imports System.Xml
Imports Presentation.Reporter
Imports System.Text
Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Base

'migracion
Public Class rptADRegistroEventos
    Implements IReport

    Dim Indigo As SessionValues = SessionValues.Instance

    Public PatientCode As String

    Public AdmissionNumber As String

    Public EventId As Integer

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport

    Public Sub New()
        ' Llamada necesaria para el DiseÃ±ador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicializaciÃ³n despuÃ©s de la llamada a InitializeComponent().

    End Sub

    Private Sub CargarDataSource() Implements IReport.CargarDataSource
        CargarDeveloper()
        CargarImagenes()
    End Sub

    Public Sub CargarDeveloper()
        Dim dsdatos As New DataSet("Datos Reporte")

        Dim query As New StringBuilder()
        query.AppendLine($" exec {Indigo.HisContainer}..SPREP_HC_Generales_CabeceraReportes '{PatientCode}', '{AdmissionNumber}'")
        Dim dtCabeceraReporte As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)
        dtCabeceraReporte.TableName = "Cabecera Reporte"

        If dtCabeceraReporte.Rows.Count > 0 Then
            dtCabeceraReporte.Columns("FECHA DE NACIMIENTO").ReadOnly = False
            dtCabeceraReporte.Columns("FECHA DE NACIMIENTO").MaxLength = 40
            dtCabeceraReporte.Rows(0).Item("FECHA DE NACIMIENTO") = CDate(dtCabeceraReporte.Rows(0).Item("EDAD")).Date
            dtCabeceraReporte.Rows(0).Item("FECHA DE NACIMIENTO") = CDate(dtCabeceraReporte.Rows(0).Item("EDAD")).Date
        End If

        Dim flagArchitectureType = String.Format(Indigo.SecurityContainer + ".")
        If SessionValues.Instance.ArchitectureType = 2 Then 'si es Paas
            flagArchitectureType = ""
        End If

        query = New StringBuilder()
        query.AppendLine($" select ha.Name NOMENTIDA, CASE e.ReportType WHEN '1' THEN 'Llamada Telefonica' WHEN '2' THEN 'Envío Físico' WHEN '3' THEN 'Registro Pagina Web' else 'Correo Electrónico' END TipoEvento,
        e.ReportType TIPREPENT, e.PhoneNumber NUMTELCON, e.Extension NUMEXTTEL, e.InitialTime FECHORINI, e.EndTime FECHORFIN, e.ContactPerson NOMCONENT, e.Charge CARCONENT, '' NUMFAXCON, '' NUMEXTFAX,
        '' NUMINTENV, '' FECENVFAX, e.URL URLSERWEB, e.RegistrationDate FECREGWEB, e.Observations COMGENREG, e.AuthorizationNumber NUMVALDER, e.AuthorizedQuantity CantidadAutorizada,
        e.CreationDate FechaRegistro, e.CreationUser + ' - ' + p.Fullname UsuarioRegistra, 2 TipoR,
        CASE e.Status WHEN 1 THEN 'Pendiente por Autorizar' WHEN 2 THEN 'Autorizado' WHEN 3 THEN 'No Autorizado' END AS ESTADO, ISNULL(ce.Code, pro.Code) + ' - ' + ISNULL(ce.Description, pro.Name) Servicio,
        t.RequestQuantity CantidadSolcitada, CASE e.PatientNotificated WHEN 1 THEN 'Si' ELSE 'No' END AS PacienteNotificado, e.InformationPatient InfoPaciente
        from {Indigo.TransactionalContainer}.[Authorization].TraceabilityPaperworkEvents e
        inner join {Indigo.TransactionalContainer}.[Authorization].TraceabilityPaperwork t on t.Id = e.TraceabilityPaperworkId
        inner join {Indigo.TransactionalContainer}.Contract.HealthAdministrator ha on ha.Id = e.HealthAdministratorId
        inner join {flagArchitectureType}Security.[User] u on u.UserCode = e.CreationUser
        inner join {flagArchitectureType}Security.Person p on p.Id = u.IdPerson
        left join {Indigo.TransactionalContainer}.Contract.CUPSEntity ce on ce.Code = t.ServiceCode
        left join {Indigo.TransactionalContainer}.Inventory.InventoryProduct pro on pro.Code = t.ServiceCode
        where e.Id = {EventId}")
        Dim dtEvento As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.TransactionalContainer)
        dtEvento.TableName = "Eventos Todo"

        'tipo evento
        GroupLlamadaTelefonica.Visible = False
        GroupFax.Visible = False
        GroupPaginaWeb.Visible = False
        Select Case dtEvento.Rows(0).Item("TIPREPENT")
            Case "1" 'Llamada Telefonica
                GroupLlamadaTelefonica.Visible = True
            Case "2" 'Envio de FAX
                GroupFax.Visible = True
            Case "3" 'Registro Pagina Web
                GroupPaginaWeb.Visible = True
        End Select

        If dtEvento IsNot Nothing AndAlso dtEvento.Rows.Count > 0 Then
            LblUsuarioRegistro.Text = dtEvento.Rows(0).Item("UsuarioRegistra").ToString.Trim
            LblFechaRegistro.Text = dtEvento.Rows(0).Item("FechaRegistro")

            If dtEvento.Rows(0).Item("TipoR") IsNot DBNull.Value AndAlso dtEvento.Rows(0).Item("TipoR") = 2 Then ''Autorización de Servicio
                GroupAutorizacionServicios.Visible = True
                GroupAtencionUrgencias.Visible = False
            ElseIf dtEvento.Rows(0).Item("TipoR") IsNot DBNull.Value AndAlso dtEvento.Rows(0).Item("TipoR") = 1 Then ''Urgencias
                GroupAtencionUrgencias.Visible = True
                GroupAutorizacionServicios.Visible = False
            End If

            If dtEvento.Rows(0).Item("PacienteNotificado") IsNot DBNull.Value AndAlso dtEvento.Rows(0).Item("InfoPaciente") IsNot DBNull.Value Then ''Autorización de Servicio
                INDlbPacienteNotificado.Visible = True
                INDlbPacienteNotificadoDato.Visible = True
                INDlbInfoPaciente.Visible = True
                INDlbInfoPacienteDato.Visible = True
            End If

        End If

        dsdatos.Tables.Add(dtCabeceraReporte)
        dsdatos.Tables.Add(dtEvento)

        'igualamos los datos del cliente (el nombre de la empresa y el nit).
        Me.INDLblNombreEmpresaCliente.Text = Indigo.IndigoCompanyName
        Me.INDLblNitCliente.Text = String.Concat("NIT: ", Indigo.IndigoCompanyNit)

        Me.DataSource = dsdatos

    End Sub

    ''' <summary>
    ''' Evento para la correcta visualizacion del bookmark del folio.
    ''' </summary>
    Public Sub Label_PrintOnPage(sender As Object, e As DevExpress.XtraReports.UI.PrintOnPageEventArgs)
        'CType(sender, XRLabel).Bookmark = CType(sender, XRLabel).Bookmark & ": " & CType(sender, XRLabel).Text
    End Sub
    ''' <summary>
    ''' Evento para la correcta visualizacion del bookmark de la Fecha de Historia.
    ''' </summary>
    Public Sub INDlblFechas_PrintOnPage(sender As Object, e As DevExpress.XtraReports.UI.PrintOnPageEventArgs)
        'CType(sender, XRLabel).Bookmark = CType(sender, XRLabel).Bookmark & ": " & String.Format(CType(sender, XRLabel).Text, "dd/MM/yyyy HH:mm")
    End Sub

    Private Sub CargarImagenes() Implements IReport.CargarImagenes
        Dim Posicion As System.Drawing.PointF
        Dim ExisteLogoDerecha As Boolean = False
        Dim ExisteLogoIzquierda As Boolean = False

        If File.Exists(My.Application.Info.DirectoryPath & "\Temp\LogoDerecha.png") Then
            ExisteLogoDerecha = True
            'creamos un objeto de imagen y le asignamos sus propiedades.
            Dim INDPictureBoxDerecha As New XRPictureBox
            INDPictureBoxDerecha.Name = "INDPictureBoxDerechaLogo"
            INDPictureBoxDerecha.ImageUrl = My.Application.Info.DirectoryPath & "\Temp\LogoDerecha.png"
            INDPictureBoxDerecha.Sizing = ImageSizeMode.AutoSize
            INDPictureBoxDerecha.LockedInUserDesigner = True
            'Nuevos Anchos de los Titulos
            INDLblNombreEmpresaCliente.WidthF -= INDPictureBoxDerecha.WidthF
            INDLblNitCliente.WidthF -= INDPictureBoxDerecha.WidthF
            INDLblNombreReporte.WidthF -= INDPictureBoxDerecha.WidthF
            'Nueva posicion para la imagen hacia la derecha
            Posicion.X = INDLblNombreEmpresaCliente.WidthF
            Posicion.Y = INDLblNombreEmpresaCliente.LocationF.Y
            INDPictureBoxDerecha.LocationF = Posicion

            Me.CabeceraReporte.Controls.Add(INDPictureBoxDerecha)   ' agregacion de la imagen a la banda
        End If

        If File.Exists(My.Application.Info.DirectoryPath & "\Temp\LogoIzquierda.png") Then
            ExisteLogoIzquierda = True
            'creamos un objeto de imagen y le asignamos sus propiedades.
            Dim INDPictureBoxIzquierda As New XRPictureBox
            INDPictureBoxIzquierda.Name = "INDPictureBoxIzquierdaLogo"
            INDPictureBoxIzquierda.ImageUrl = My.Application.Info.DirectoryPath & "\Temp\LogoIzquierda.png"
            INDPictureBoxIzquierda.Sizing = ImageSizeMode.AutoSize
            INDPictureBoxIzquierda.LockedInUserDesigner = True
            'Nueva posicion para la imagen hacia la izquierda.
            Posicion.X = INDLblNombreEmpresaCliente.LocationF.X
            Posicion.Y = INDLblNombreEmpresaCliente.LocationF.Y
            INDPictureBoxIzquierda.LocationF = Posicion
            Me.CabeceraReporte.Controls.Add(INDPictureBoxIzquierda)  ' agregacion de la imagen a la banda

            Posicion.X += INDPictureBoxIzquierda.WidthF
            'Nuevos Anchos de los Titulos
            INDLblNombreEmpresaCliente.WidthF -= Posicion.X
            INDLblNitCliente.WidthF -= Posicion.X
            INDLblNombreReporte.WidthF -= Posicion.X
        End If
        If (ExisteLogoIzquierda = False And ExisteLogoDerecha = True) Or (ExisteLogoIzquierda = False And ExisteLogoDerecha = False) Then
            Exit Sub
        End If
        ' establecemos la nueva posicion de los controles
        Posicion.Y = INDLblNombreEmpresaCliente.LocationF.Y
        INDLblNombreEmpresaCliente.LocationF = Posicion    'nueva posicion nombre empresa.

        Posicion.Y = INDLblNitCliente.LocationF.Y
        INDLblNitCliente.LocationF = Posicion              'nueva posicion numero nit.

        Posicion.Y = INDLblNombreReporte.LocationF.Y
        INDLblNombreReporte.LocationF = Posicion           'nueva posicion imagen.
    End Sub

End Class
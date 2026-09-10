Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.Parameters
Imports System.Text
Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Reporter

'migracion
Public Class rptADSolicituddeServicios
    Implements IReport

    Public PatientCode As String

    Dim Indigo As SessionValues = SessionValues.Instance

    Public CareCenterCode As String

    Public ProfessionalCode As String

    Public HealthAdministratorCode As String

    Public NumberFolio As String

    Public DiagnosticCode As String

    Public TypeRequestServices As Integer

    Public PriorityAttention As Integer

    Public Justification As String

    Public FunctionalUnitCode As String

    Public FunctionalUnitName As String

    Public AnnexId As Integer

    Public AnnexesConsecutives As String

    Public AdmissionNumber As String

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        CargaReporte()
    End Sub

    Private Sub CargaReporte()

        Dim query As New StringBuilder()
        query.AppendLine($" SELECT '' NUMINFORM,GETDATE() AS FECHA,GETDATE() AS HORA,'' AS NOMENTIDA,'' AS CODADMPAG,IPPRIAPEL,CASE RTRIM(IPSEGAPEL) WHEN '' THEN 'NO TIENE' ELSE IPSEGAPEL END AS IPSEGAPEL,
        IPPRINOMB,CASE RTRIM(IPSEGNOMB) WHEN '' THEN 'NO TIENE' ELSE IPSEGNOMB END AS IPSEGNOMB,CASE IPTIPODOC WHEN '1' THEN 'X' END AS TipoDoc1,CASE IPTIPODOC WHEN '2' THEN 'X' END AS TipoDoc2,
        CASE IPTIPODOC WHEN '3' THEN 'X' END AS TipoDoc3,CASE IPTIPODOC WHEN '4' THEN 'X' END AS TipoDoc4,CASE IPTIPODOC WHEN '5' THEN 'X' END AS TipoDoc5,CASE IPTIPODOC WHEN '6' THEN 'X' END AS TipoDoc6,
        CASE IPTIPODOC WHEN '7' THEN 'X' END AS TipoDoc7, B.IPCODPACI,IPFECNACI, RTRIM(IPDIRECCI) AS DireccionPaciente,IPTELEFON,RTRIM(E.nomdepart) AS DepartamentoPaciente,E.DEPCODIGO AS DepartamentoPacienteCodigo,
        RTRIM(D.MUNNOMBRE) AS MunicipioPaciente,D.MUNCODIGO AS MunicipioPacienteCodigo,IPTELMOVI AS Celular,RTRIM(CORELEPAC) AS Correo,CASE TIPCOBSAL WHEN '1' THEN 'X' END AS TipoCobertura1,
        CASE TIPCOBSAL WHEN '2' THEN 'X' END AS TipoCobertura2,CASE TIPCOBSAL WHEN '3' THEN 'X' END AS TipoCobertura3,CASE TIPCOBSAL WHEN '4' THEN 'X' END AS TipoCobertura4,
        CASE TIPCOBSAL WHEN '5' THEN 'X' END AS TipoCobertura5,CASE TIPCOBSAL WHEN '6' THEN 'X' END AS TipoCobertura6,CASE TIPCOBSAL WHEN '7' THEN 'X' END AS TipoCobertura7,
        CASE TIPCOBSAL WHEN '8' THEN 'X' END AS TipoCobertura8, CASE '' WHEN '01' THEN 'X' END AS Origen01,CASE '' WHEN '02' THEN 'X' END AS Origen02,
        CASE '' WHEN '06' THEN 'X' END AS Origen06,CASE '' WHEN '13' THEN 'X' END AS Origen13,CASE '' WHEN '14' THEN 'X' END AS Origen14,
        CASE '' WHEN '1' THEN 'X' END AS  Servicios1,CASE '' WHEN '2' THEN 'X' END AS Servicios2,CASE '' WHEN '1' THEN 'X' END AS PrioritarioSi,
        CASE '' WHEN '2' THEN 'X' END AS PrioritarioNo,CASE '' WHEN '1' THEN 'X' END AS Ubicacion1,CASE '' WHEN '2' THEN 'X' END AS Ubicacion2,
        CASE '' WHEN '3' THEN 'X' END AS Ubicacion3,'' UFUDESCRI,'' AS Cama,'' AS Guia,'' AS Justificacion, 
        '' AS Persona,'' INDNUMTEL, '' NUMTELCEN, '' NUMEXTTEL, '' NUMCELCEN,
        CASE 0 WHEN 1 THEN 'Medico General' WHEN 2 THEN 'Medico Especialista' WHEN 3 THEN 'Enfermera' WHEN 4 THEN 'Auxiliar Enfermeria' WHEN 5 THEN 'Odontologo General' WHEN 6 THEN 'Odontologo Especialista' 
        WHEN 7 THEN 'Nutricionista' WHEN 8 THEN 'Higienista' WHEN 9 THEN 'Psicologo' WHEN 10 THEN 'Trabajadora Social' WHEN 11 THEN 'Promotor de Saneamiento' WHEN 12 THEN 'Ingeniero Sanitario' 
        WHEN 13 THEN 'Medico Veterinario' WHEN 14 THEN 'Ingeniero Alimento' WHEN 15 THEN 'Auxiliar Bacteriologo' WHEN 16 THEN 'Terapeuta' WHEN 17 THEN 'Optometra' WHEN 18 THEN 'Optometra' 
        WHEN 19 THEN 'Radiologo' WHEN 20 THEN 'Tecnologo Radiologo' WHEN 21 THEN 'Instrumentador Qx' WHEN 22 THEN 'Auxiliar Patologia' WHEN 23 THEN 'Otros' END AS Cargo,
        '' NUMEFOLIO,'' TARJETAPR
        FROM {Indigo.HisContainer}.dbo.INPACIENT B
        INNER JOIN {Indigo.HisContainer}.dbo.INUBICACI C ON B.AUUBICACI=C.AUUBICACI
        INNER JOIN {Indigo.HisContainer}.dbo.INMUNICIP D ON C.DEPMUNCOD=D.DEPMUNCOD
        INNER JOIN {Indigo.HisContainer}.dbo.INDEPARTA E ON D.DEPCODIGO=E.depcodigo 
        where B.IPCODPACI = '{PatientCode}'")
        Dim dtAutorizacion As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)

        'Se obtiene el anexo
        query = New StringBuilder
        query.AppendLine($" select Id, TraceabilityPaperworkId, HealthAdministratorId, TypeRequestServices, PriorityAttention, Justification, Folio, Consecutive, CreationDate, CreationUser
        from {Indigo.TransactionalContainer}.[Authorization].TraceabilityPaperworkAnnexes
        where Id = {AnnexId}")
        Dim dtAnnex As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.TransactionalContainer)

        If dtAutorizacion.Rows.Count > 0 Then
            'Cargo los datos del profesional
            query = New StringBuilder
            query.AppendLine($" select CODPROSAL, NOMMEDICO,
            CASE TIPPROFES WHEN 1 THEN 'Medico General' WHEN 2 THEN 'Medico Especialista' WHEN 3 THEN 'Enfermera' WHEN 4 THEN 'Auxiliar Enfermeria' WHEN 5 THEN 'Odontologo General' WHEN 6 THEN 'Odontologo Especialista' WHEN 7 THEN 'Nutricionista' WHEN 8 THEN 'Higienista' WHEN 9 THEN 'Psicologo' WHEN 10 THEN 'Trabajadora Social' WHEN 11 THEN 'Promotor de Saneamiento' WHEN 12 THEN 'Ingeniero Sanitario' WHEN 13 THEN 'Medico Veterinario' WHEN 14 THEN 'Ingeniero Alimento' WHEN 15 THEN 'Auxiliar Bacteriologo' WHEN 16 THEN 'Terapeuta' WHEN 17 THEN 'Optometra' WHEN 18 THEN 'Optometra' WHEN 19 THEN 'Radiologo' WHEN 20 THEN 'Tecnologo Radiologo' WHEN 21 THEN 'Instrumentador Qx' WHEN 22 THEN 'Auxiliar Patologia' WHEN 23 THEN 'Otros' END AS Cargo,
            TARJETAPR
            from {Indigo.HisContainer}.dbo.INPROFSAL
            where CODPROSAL = '{ProfessionalCode}'")
            Dim dtProfessional As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)
            dtAutorizacion.Rows(0).Item("Persona") = dtProfessional.Rows(0).Item("NOMMEDICO")
            dtAutorizacion.Rows(0).Item("Cargo") = dtProfessional.Rows(0).Item("Cargo")
            dtAutorizacion.Rows(0).Item("TARJETAPR") = dtProfessional.Rows(0).Item("TARJETAPR")

            'Cargo los datos de la entidad
            query = New StringBuilder
            query.AppendLine($" select CODENTIDA, NOMENTIDA, CODADMPAG
            from {Indigo.HisContainer}.dbo.INENTIDAD
            where CODENTIDA = '{HealthAdministratorCode}'")
            Dim dtHealthAdministrator As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)
            dtAutorizacion.Rows(0).Item("NOMENTIDA") = dtHealthAdministrator.Rows(0).Item("NOMENTIDA")
            dtAutorizacion.Rows(0).Item("CODADMPAG") = dtHealthAdministrator.Rows(0).Item("CODADMPAG")

            'Cargo los datos del ingreso
            If String.IsNullOrEmpty(AdmissionNumber) = False Then
                query = New StringBuilder
                query.AppendLine($" select ICAUSAING
                from {Indigo.HisContainer}..ADINGRESO
                where NUMINGRES = '{AdmissionNumber}'")
                Dim dtAdmission As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)
                Dim causeIncome = dtAdmission.Rows(0).Item("ICAUSAING")
                Dim originAttention As String = ReturnCauseIncome(causeIncome)
                dtAutorizacion.Rows(0).Item("Origen01") = If(originAttention = "01", "X", "")
                dtAutorizacion.Rows(0).Item("Origen02") = If(originAttention = "02", "X", "")
                dtAutorizacion.Rows(0).Item("Origen06") = If(originAttention = "06", "X", "")
                dtAutorizacion.Rows(0).Item("Origen13") = If(originAttention = "13", "X", "")
                dtAutorizacion.Rows(0).Item("Origen14") = If(originAttention = "14", "X", "")
            End If

            'Cargo la unidad funcional
            query = New StringBuilder
            query.AppendLine($" select UFUTIPUNI
            from {Indigo.HisContainer}..INUNIFUNC 
            where UFUCODIGO = '{FunctionalUnitCode}'")
            Dim dtFunctionalUnit As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)
            Dim functionalUnitType = dtFunctionalUnit.Rows(0).Item("UFUTIPUNI")
            Dim ubication As String = ReturnUbication(functionalUnitType)
            dtAutorizacion.Rows(0).Item("Ubicacion1") = If(ubication = "1", "X", "")
            dtAutorizacion.Rows(0).Item("Ubicacion2") = If(ubication = "2", "X", "")
            dtAutorizacion.Rows(0).Item("Ubicacion3") = If(ubication = "3", "X", "")

            dtAutorizacion.Rows(0).Item("Servicios1") = If(TypeRequestServices = 1, "X", "")
            dtAutorizacion.Rows(0).Item("Servicios2") = If(TypeRequestServices = 2, "X", "")
            dtAutorizacion.Rows(0).Item("PrioritarioSi") = If(PriorityAttention = 1, "X", "")
            dtAutorizacion.Rows(0).Item("PrioritarioNo") = If(PriorityAttention = 2, "X", "")

            dtAutorizacion.Rows(0).Item("NUMINFORM") = AnnexesConsecutives
            dtAutorizacion.Rows(0).Item("UFUDESCRI") = FunctionalUnitName
            dtAutorizacion.Rows(0).Item("NUMEFOLIO") = NumberFolio
            dtAutorizacion.Rows(0).Item("Justificacion") = Justification

            dtAutorizacion.Rows(0).Item("FECHA") = String.Format("{0:yyyy-MM-dd}", dtAnnex.Rows(0).Item("CreationDate"))
            dtAutorizacion.Rows(0).Item("HORA") = String.Format("{0:HH:mm}", dtAnnex.Rows(0).Item("CreationDate"))
            dtAutorizacion.Rows(0).Item("IPFECNACI") = String.Format("{0:yyyy-MM-dd}", dtAutorizacion.Rows(0).Item("IPFECNACI"))
        End If

        Me.DataSource = dtAutorizacion
        Me.DataMember = dtAutorizacion.TableName
        Me.FillDataSource()

        'cargo los datos de la empresa
        query = New StringBuilder
        Dim indigoContainer = Indigo.IndigoContainer.ToString().PadLeft(3, "0")
        query.AppendLine($" SELECT RTRIM(INDNOMEMP) AS 'Nombre Empresa', CASE INDTIPIDE WHEN '1' THEN 'X' END AS CC, CASE INDTIPIDE WHEN '2' THEN 'X' END AS NIT, INDNUMIDE, INDDIGVER 
        FROM {Indigo.HisContainer}.dbo.INEMPRESU 
        WHERE INDCODEMP = '{indigoContainer}'")
        Dim dtEmpresa As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)

        'cargo los datos del prestador(ips)
        query = New StringBuilder
        query.AppendLine($" SELECT CODIPSSEC,RTRIM(DIRCENATE) AS Direccion,INDNUMTEL,NUMTELCEN,RTRIM(nomdepart) AS Departamento,C.depcodigo,RTRIM(MUNNOMBRE) AS Municipio,
        B.MUNCODIGO, NUMEXTTEL, NUMCELCEN
        FROM {Indigo.HisContainer}.dbo.ADCENATEN A
        INNER JOIN {Indigo.HisContainer}.dbo.INMunicip B ON A.DEPMUNCOD=B.DEPMUNCOD
        INNER JOIN {Indigo.HisContainer}.dbo.INDEPARTA C ON B.DEPCODIGO=C.DEPCODIGO 
        WHERE CODCENATE = '{CareCenterCode}'")
        Dim dtprestador As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)

        If dtAutorizacion.Rows.Count > 0 Then
            dtAutorizacion.Rows(0).Item("INDNUMTEL") = dtprestador.Rows(0).Item("INDNUMTEL")
            dtAutorizacion.Rows(0).Item("NUMTELCEN") = dtprestador.Rows(0).Item("NUMTELCEN")
            dtAutorizacion.Rows(0).Item("NUMEXTTEL") = dtprestador.Rows(0).Item("NUMEXTTEL")
            dtAutorizacion.Rows(0).Item("NUMCELCEN") = dtprestador.Rows(0).Item("NUMCELCEN")
        End If

        Dim Datos As XRBinding
        'datos empresa
        Datos = New XRBinding("Text", dtEmpresa, "Datos.Nombre Empresa")
        lblEmpresaNombre.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtEmpresa, "Datos.NIT")
        lblEmpresaNit.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtEmpresa, "Datos.CC")
        lblEmpresaCC.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtEmpresa, "Datos.INDNUMIDE")
        lblEmpresaNumero.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtEmpresa, "Datos.INDDIGVER")
        lblEmpresaDV.DataBindings.Add(Datos)

        'datos prestador
        Datos = New XRBinding("Text", dtprestador, "Datos.CODIPSSEC")
        lblPrestadorCodigo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtprestador, "Datos.Direccion")
        lblPrestadorDireccion.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtprestador, "Datos.INDNUMTEL")
        lblPrestadorindicativo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtprestador, "Datos.NUMTELCEN")
        lblPrestadorNumerotelefono.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtprestador, "Datos.Departamento")
        lblPrestadorDepartamento.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtprestador, "Datos.depcodigo")
        lblPrestadorDepartamentoCodigo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtprestador, "Datos.Municipio")
        lblPrestadorMunicipio.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtprestador, "Datos.MUNCODIGO")
        lblPrestadorMunicipioCodigo.DataBindings.Add(Datos)

        'datos del informe
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.NUMINFORM")
        lblInformeNumero.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.FECHA", "{0:yyyy-MM-dd}")
        lblInformefecha.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.HORA", "{0:HH:mm}")
        lblInformeHora.DataBindings.Add(Datos)

        'datos del pagador o entidad
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.NOMENTIDA")
        lblPagadorNombre.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.CODADMPAG")
        lblPagadorCodigo.DataBindings.Add(Datos)

        'datos del paciente
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.IPPRIAPEL")
        lblPaciente1Apellido.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.IPSEGAPEL")
        lblPaciente2Apellido.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.IPPRINOMB")
        lblPaciente1Nombre.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.IPSEGNOMB")
        lblPaciente2Nombre.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoDoc1")
        lblTipoDoc1.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoDoc2")
        lblTipoDoc2.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoDoc3")
        lblTipoDoc3.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoDoc4")
        lblTipoDoc4.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoDoc5")
        lblTipoDoc5.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoDoc6")
        lblTipoDoc6.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoDoc7")
        lblTipoDoc7.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.IPCODPACI")
        lblPacienteNumero.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.IPFECNACI", "{0:yyyy-MM-dd}")
        lblPacienteFecha.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.DireccionPaciente")
        lblPacienteDireccion.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.IPTELEFON")
        lblPacienteTelefono.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.DepartamentoPaciente")
        lblPacienteDepartamento.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.DepartamentoPacienteCodigo")
        lblPacienteDepartamentoCodigo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.MunicipioPaciente")
        lblPacientemunicipio.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.MunicipioPacienteCodigo")
        lblPacientemunicipioCodigo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Celular")
        lblPacienteCelular.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Correo")
        lblPacienteCorreo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoCobertura1")
        lblTipoCobertura1.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoCobertura2")
        lblTipoCobertura2.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoCobertura3")
        lblTipoCobertura3.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoCobertura4")
        lblTipoCobertura4.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoCobertura5")
        lblTipoCobertura5.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoCobertura6")
        lblTipoCobertura6.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoCobertura7")
        lblTipoCobertura7.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TipoCobertura8")
        lblTipoCobertura8.DataBindings.Add(Datos)

        'informacion de la atencion
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Origen01")
        lblOrigen01.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Origen02")
        lblOrigen02.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Origen06")
        lblOrigen06.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Origen13")
        lblOrigen13.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Origen14")
        lblOrigen14.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Servicios1")
        lblServicios1.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Servicios2")
        lblServicios2.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.PrioritarioSi")
        lblPrioridadSi.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.PrioritarioNo")
        lblPrioridadNo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Ubicacion1")
        lblUbicacion1.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Ubicacion2")
        lblUbicacion2.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Ubicacion3")
        lblUbicacion3.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.UFUDESCRI")
        lblServicio.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Cama")
        lblCama.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Guia")
        lblGuia.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Justificacion")
        lblJustificacionClinica.DataBindings.Add(Datos)

        'Se obtiene los servicios del anexo
        query = New StringBuilder
        query.AppendLine($" select ROW_NUMBER() OVER( ORDER BY a.Id) AS Fila, t.ServiceCode CODGOCUPS, t.RequestQuantity CANSERIPS, IIF(ce.Id is not null, ce.Description, p.Name) DESSERIPS
        from {Indigo.TransactionalContainer}.[Authorization].TraceabilityPaperworkAnnexes a
        inner join {Indigo.TransactionalContainer}.[Authorization].TraceabilityPaperwork t on t.Id = a.TraceabilityPaperworkId
        left join {Indigo.TransactionalContainer}.Contract.CUPSEntity ce on ce.Code = t.ServiceCode
        left join {Indigo.TransactionalContainer}.Inventory.InventoryProduct p on p.Code = t.ServiceCode
        where a.Consecutive = {AnnexesConsecutives}")
        Dim dtServicios As DataTable = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.TransactionalContainer)

        DetailReport.DataSource = dtServicios
        DetailReport.DataMember = dtServicios.TableName
        Datos = New XRBinding("Text", dtServicios, "Datos.Fila")
        lblLineaCUPS.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtServicios, "Datos.CODGOCUPS")
        lblCUPSCodigo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtServicios, "Datos.CANSERIPS")
        lblCUPSCantidad.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtServicios, "Datos.DESSERIPS")
        lblCUPSDescripcion.DataBindings.Add(Datos)

        Dim dtDiagnosticos As DataTable = Nothing
        If dtAutorizacion.Rows.Count > 0 Then
            query = New StringBuilder

            If String.IsNullOrEmpty(NumberFolio) = False Then 'Si viene no. de folio es porque el item tiene asignado un ingreso
                query.AppendLine($" SELECT A.CODDIAGNO,RTRIM(NOMDIAGNO) AS NOMDIAGNO,A.CODDIAPRI
                FROM {Indigo.HisContainer}.dbo.INDIAGNOH A 
                INNER JOIN {Indigo.HisContainer}.dbo.INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO
                LEFT JOIN {Indigo.HisContainer}.dbo.ADENFHUERFANAS C ON A.IDADENFHUERFANAS=C.ID
                WHERE NUMEFOLIO = '{NumberFolio}' AND IPCODPACI = '{PatientCode}'")
            Else 'Si no, es porque el item se registro manualmente y no tiene ingreso asignado
                query.AppendLine($" SELECT B.CODDIAGNO,RTRIM(NOMDIAGNO) AS NOMDIAGNO,1 CODDIAPRI
                FROM {Indigo.HisContainer}.dbo.INDIAGNOS B 
                WHERE CODDIAGNO = '{DiagnosticCode}'")
            End If

            dtDiagnosticos = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt(query.ToString(), Indigo.HisContainer)
        End If
        If dtDiagnosticos IsNot Nothing AndAlso dtDiagnosticos.Rows.Count > 0 Then
            Dim drPrincipal As DataRow()
            drPrincipal = dtDiagnosticos.Select("CODDIAPRI=1")
            lblDiagnosticoPrincipalCodigo.Text = drPrincipal(0).Item("CODDIAGNO")
            lblDiagnosticoPrincipalDescripcion.Text = drPrincipal(0).Item("NOMDIAGNO")
            Dim drRelacionados As DataRow()
            drRelacionados = dtDiagnosticos.Select("CODDIAPRI=0")
            If drRelacionados.Length >= 1 Then
                lblDiagnostico1Codigo.Text = drRelacionados(0).Item("CODDIAGNO")
                lblDiagnostico1Descripcion.Text = drRelacionados(0).Item("NOMDIAGNO")
            End If
            If drRelacionados.Length >= 2 Then
                lblDiagnostico2Codigo.Text = drRelacionados(1).Item("CODDIAGNO")
                lblDiagnostico2Descripcion.Text = drRelacionados(1).Item("NOMDIAGNO")
            End If
            If drRelacionados.Length >= 3 Then
                lblDiagnostico3Codigo.Text = drRelacionados(2).Item("CODDIAGNO")
                lblDiagnostico3Descripcion.Text = drRelacionados(2).Item("NOMDIAGNO")
            End If
        End If

        'informacion de quien reporta
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Persona")
        lblPersonaNombre.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.INDNUMTEL")
        lblPersonaIndicativo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.NUMTELCEN")
        lblPersonaNumeroTelefono.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.NUMEXTTEL")
        lblPersonaextension.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.Cargo")
        lblPersonaCargo.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.NUMCELCEN")
        lblPersonaCelular.DataBindings.Add(Datos)
        Datos = New XRBinding("Text", dtAutorizacion, "Datos.TARJETAPR")
        INDlblTarjetaProfesional.DataBindings.Add(Datos)
    End Sub

    Private Sub lblInformeHora_BeforePrint(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles lblInformeHora.BeforePrint
        Dim obj As XRLabel = CType(sender, XRLabel)
        obj.Text = Mid(obj.Text, 1, 5)
    End Sub

    Private Function ReturnCauseIncome(causeIncome) As String
        Dim originAttention As String

        Select Case causeIncome
            Case 10
                originAttention = "01"
            Case 6
                originAttention = "02"
            Case 7
                originAttention = "06"
            Case 3, 4
                originAttention = "13"
            Case 2
                originAttention = "14"
            Case Else
                originAttention = "13"
        End Select

        Return originAttention
    End Function

    Private Function ReturnUbication(functionalUnitType As Integer) As String
        Dim ubication As String

        Select Case functionalUnitType
            Case 1
                ubication = "2"
            Case 2
                ubication = "3"
            Case 3
                ubication = "1"
            Case Else
                ubication = ""
        End Select

        Return ubication
    End Function

End Class
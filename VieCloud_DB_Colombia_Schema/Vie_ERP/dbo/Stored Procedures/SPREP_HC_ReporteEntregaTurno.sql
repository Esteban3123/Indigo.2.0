CREATE PROCEDURE [dbo].[SPREP_HC_ReporteEntregaTurno]
(
    -- Add the parameters for the stored procedure here
	@INDEntregaTurno integer,
	@TipoSolicitud integer --1:Cabecera Turno; 2:Profesionales;  3:Pacientes
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
	SET NOCOUNT ON

    -- Insert statements for procedure here
	--Consulto Cabecera del Entrega Turno
IF @TipoSolicitud = 1 
		BEGIN
			SELECT
				A.ID as 'IDEntregaTurno',
				A.FECHAREGISTRO AS 'FechaRegistro',
			CASE
				WHEN A.DASHBOARD = 0 THEN 'Médico'
				WHEN A.DASHBOARD = 1 THEN 'Enfermería'
				WHEN A.DASHBOARD = 2 THEN 'Académico'
				WHEN A.DASHBOARD = 3 THEN 'Terapia'
				END AS 'Dashboard',
				RTRIM(B.CODCENATE) AS 'CodigoCentroAtencion' ,
				RTRIM(B.NOMCENATE) AS 'CentroAtencion' ,
				RTRIM(C.UFUCODIGO) AS 'CodigoUnidad Funcional',
				RTRIM(C.UFUDESCRI) AS 'UnidadFuncional' ,
			CASE
				WHEN A.JORNADA = 0 THEN 'Mañana'
				WHEN A.JORNADA = 1 THEN 'Tarde'
				WHEN A.JORNADA = 2 THEN 'Noche'
				END AS 'Jornada'
			FROM
				HCENTREGATURNOC A WITH (NOLOCK)
				INNER JOIN ADCENATEN B WITH (NOLOCK)
				ON A.CODCENATE = B.CODCENATE
				INNER JOIN INUNIFUNC C WITH (NOLOCK)
				ON A.UFUCODIGO = C.UFUCODIGO
				WHERE
					A.ID = @INDEntregaTurno			
	END

	--Consulto Información Profesionales
ELSE IF @TipoSolicitud = 2
			BEGIN
				SELECT
						A.ID,
					CASE
						WHEN B.ROLPROFESIONAL = 0 THEN 'Entrega el Turno'
						WHEN B.ROLPROFESIONAL = 1 THEN 'Recibe el Turno'
					END AS 'Rol',
						C.CODPROSAL AS 'Codigo',
						C.NOMMEDICO AS 'Nombre',
						D.DESESPECI AS 'Especialidad',	
						C.MEDIFIRMA Firma
						FROM
						HCENTREGATURNOC A WITH (NOLOCK)
						INNER JOIN HCENTREGATURNOPROFSAL B WITH (NOLOCK)
						ON A.ID = B.IDHCENTREGATURNOC
						INNER JOIN INPROFSAL C WITH (NOLOCK)
						ON B.CODPROSAL = C.CODPROSAL
						INNER JOIN INESPECIA D WITH (NOLOCK) 
						ON B.CODESPECI = D.CODESPECI						
						WHERE
						A.ID = @INDEntregaTurno				
	END

	-- Consulto Información Pacientes
ELSE IF @TipoSolicitud = 3 
	BEGIN
	--Consulto los Medicamentos
		DECLARE @TablaMedicamentosDX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Codigo VARCHAR(max), Indicaciones VARCHAR(MAX), Descripcion VARCHAR(MAX), Administracion VARCHAR(MAX))
		INSERT INTO @TablaMedicamentosDX
		SELECT H.IPCODPACI 'Identificacion', A.CODPRODUC 'Codigo',IIF(F.INDAPLMED IS NULL,'',CONCAT('-',F.INDAPLMED))  'Indicaciones', E.DESPRODUC 'Descripcion', F.DESADMINI 'Administracion'
		FROM HCENTREGATURNOMEDICA A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN H WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = H.ID
		JOIN HCPRESCRA F WITH (NOLOCK) ON  A.CODPRODUC = F.CODPRODUC AND A.NUMEFOLIO = F.NUMEFOLIO AND H.IPCODPACI = F.IPCODPACI
		JOIN IHLISTPRO E  WITH (NOLOCK) ON F.CODPRODUC = E.CODPRODUC
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	
		-- Actualizo el Campo de Indicaciones - From NULL To 'Sin Indicaciones'
		UPDATE @TablaMedicamentosDX
		SET Indicaciones = ''
		WHERE Indicaciones IS NULL;
		-- Guardo Medicamentos Para Consultar: @TablaMedicamentos
		DECLARE @TablaMedicamentos AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), Medicamentos VARCHAR(MAX))
		INSERT INTO @TablaMedicamentos
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Codigo)) + '-' + RTRIM(LTRIM(B.Descripcion)) + RTRIM(LTRIM(B.Indicaciones) + ' ' + RTRIM(LTRIM(B.Administracion)))
		FROM @TablaMedicamentosDX B
		WHERE B.Identificacion = a.IPCODPACI FOR XML PATH('')), 1, 2, '') As Medicamentos
		FROM HCENTREGATURNOCPACIEN a WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	

	--Consulto los Diagnosticos
		DECLARE @TablaDiagnosticosDX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Codigo VARCHAR(max), Diagnostico VARCHAR(MAX), Observacion VARCHAR(MAX))
		INSERT INTO @TablaDiagnosticosDX
		SELECT B.IPCODPACI 'Identificacion', C.CODDIAGNO 'Codigo', D.NOMDIAGNO 'Diagnostico', IIF(RTRIM(C.OBSDIAGNO) = '','',CONCAT('-',RTRIM(C.OBSDIAGNO))) 'Observacion'
		FROM HCENTREGATURNODIAGNO  A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN INDIAGNOP C WITH (NOLOCK) ON  A.CODDIAGNO = C.CODDIAGNO AND A.NUMEFOLIO = C.NUMEFOLIO AND B.IPCODPACI = C.IPCODPACI
		JOIN INDIAGNOS D WITH (NOLOCK) ON  C.CODDIAGNO = D.CODDIAGNO
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	
		-- Actualizo el Campo de Observacion - From NULL To 'Sin Observacion.'
		UPDATE @TablaDiagnosticosDX
		SET Observacion = ''
		WHERE Observacion IS NULL;
		-- Guardo los Diagnosticos Para Consultar: @TablaDiagnosticos
		DECLARE @TablaDiagnosticos AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), Diagnosticos VARCHAR(MAX))
		INSERT INTO @TablaDiagnosticos
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Codigo)) + '-' + RTRIM(LTRIM(B.Diagnostico)) + RTRIM(LTRIM(B.Observacion))
		FROM @TablaDiagnosticosDX B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As Diagnosticos
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno
		
	--Consulto las Imágenes
		DECLARE @TablaImagnesDX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Fecha VARCHAR(2000), Codigo VARCHAR(2000),Servicio VARCHAR(2000), Relacionada VARCHAR(2000), Lateralidad VARCHAR(2000), Medico VARCHAR(2000), TipoExamen VARCHAR(2000),Estado VARCHAR(2000), Cantidad VARCHAR(2000), Datos VARCHAR(2000))
		INSERT INTO @TablaImagnesDX
		SELECT B.IPCODPACI 'Identificacion', C.FECORDMED 'Fecha', C.CODSERIPS 'Codigo', RTRIM(D.DESSERIPS) 'Servicio', IIF(RTRIM(CD.Name) IS NULL,'',CONCAT('-',RTRIM(CD.Name)))  'Relacionada', CASE C.LATERALIDAD WHEN '0' THEN 'No Aplica' WHEN '1' THEN 'Izquierda' WHEN '2' THEN 'Derecha' WHEN '3' THEN 'Ambos' END AS 'Lateralidad', E.NOMMEDICO 'Medico', CASE C.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutina' END AS 'TipoExamen',CASE C.ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Estudio Realizado' WHEN '3' THEN 'Imagen Procesada' WHEN '4' THEN 'Estudio Interpretado' WHEN '5' THEN 'Remitido' WHEN '6' THEN 'Anulado' WHEN '7' THEN 'Extramural' END AS 'Estado', C.CANSERIPS 'Cantidad', IIF(C.OBSSERIPS IS NULL,'',CONCAT('-',C.OBSSERIPS)) 'Datos'
		FROM HCENTREGATURNOCUPS A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN HCORDIMAG C WITH (NOLOCK) ON  A.CODSERIPS = C.CODSERIPS AND A.NUMEFOLIO = C.NUMEFOLIO AND B.IPCODPACI = C.IPCODPACI
		JOIN INCUPSIPS D WITH (NOLOCK) ON  C.CODSERIPS = D.CODSERIPS
	    JOIN INPROFSAL E WITH (NOLOCK) ON  C.CODPROSAL = E.CODPROSAL 
		JOIN INUNIFUNC G WITH (NOLOCK) ON  C.UFUCODIGO = G.UFUCODIGO 
	    JOIN INCUPSIPS H WITH (NOLOCK) ON  C.CODSERIPS=H.CODSERIPS 
		JOIN HCHISPACA J WITH (NOLOCK) ON  C.NUMEFOLIO=J.NUMEFOLIO AND C.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN  .INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id=C.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id=CDD.ContractDescriptionId 
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno AND A.TIPOSERVICIO = 1
		-- Actualizo el Campo de Relacionada - From NULL To ''
		UPDATE @TablaImagnesDX
		SET Relacionada = ''
		WHERE Relacionada IS NULL;
		UPDATE @TablaImagnesDX
		SET Datos = ''
		WHERE Datos IS NULL;
		-- Guardo Imagenes Para Consultar: @TablaImagenes
		DECLARE @TablaImagenes AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), Imagenes VARCHAR(MAX))
		INSERT INTO @TablaImagenes
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Fecha)) + '-' + RTRIM(LTRIM(B.Codigo)) + '-' + RTRIM(LTRIM(B.Servicio)) +  RTRIM(LTRIM(B.Relacionada)) + '-' + RTRIM(LTRIM(B.Lateralidad)) + '-' + RTRIM(LTRIM(B.Medico)) + '-' + RTRIM(LTRIM(B.TipoExamen)) + '-' + RTRIM(LTRIM(B.Estado)) + '-(' + RTRIM(LTRIM(B.Cantidad)) + ')' + RTRIM(LTRIM(B.Datos))
		FROM @TablaImagnesDX B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As Imagenes
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno
	
 		--Consulto los laboratorios
		DECLARE @TablaLaboratoriosDX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Fecha VARCHAR(2000), Codigo VARCHAR(2000),Servicio VARCHAR(2000), Relacionada VARCHAR(2000), Medico VARCHAR(2000), TipoExamen VARCHAR(2000),Estado VARCHAR(2000), Cantidad VARCHAR(1000), Datos VARCHAR(2000))
		INSERT INTO @TablaLaboratoriosDX
		SELECT B.IPCODPACI 'Identificacion', C.FECORDMED 'Fecha', C.CODSERIPS 'Codigo', RTRIM(E.DESSERIPS) 'Servicio', IIF(RTRIM(CD.Name) IS NULL,'',CONCAT('-',RTRIM(CD.Name))) 'Relacionada', D.NOMMEDICO 'Medico', CASE C.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' END AS 'TipoExamen',CASE C.ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Muestra Recolectada' WHEN '3' THEN 'Resultado Entregado' WHEN '4' THEN 'Examen Interpretado' WHEN '5' THEN 'Remitido' WHEN '6' THEN 'Anulado' WHEN '7' THEN 'Extramural' WHEN '8' THEN 'Muestra Recolectada Parcialmente' END AS 'Estado', C.CANSERIPS 'Cantidad',IIF(C.OBSSERIPS IS NULL,'',CONCAT('-',C.OBSSERIPS)) 'Observacion'
		FROM HCENTREGATURNOCUPS A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN HCORDLABO C WITH (NOLOCK) ON  A.CODSERIPS = C.CODSERIPS AND A.NUMEFOLIO = C.NUMEFOLIO AND B.IPCODPACI = C.IPCODPACI
		JOIN INPROFSAL D WITH (NOLOCK) ON C.CODPROSAL = D.CODPROSAL
        JOIN INCUPSIPS E ON C.CODSERIPS=E.CODSERIPS 	
		JOIN HCHISPACA AS J WITH(NOLOCK) ON C.NUMEFOLIO=J.NUMEFOLIO AND C.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id=C.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id=CDD.ContractDescriptionId 
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno AND A.TIPOSERVICIO = 3
		-- Actualizo el Campo de Relacionada y datos - From NULL To ''
		UPDATE @TablaLaboratoriosDX
		SET Datos = ''
		WHERE Datos IS NULL;
		UPDATE @TablaLaboratoriosDX
		SET Relacionada = ''
		WHERE Relacionada IS NULL;
		-- Guardo laboratorios Para Consultar: @Tablalaboratorios
		DECLARE @TablaLaboratorios AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), Laboratorios VARCHAR(MAX))
		INSERT INTO @TablaLaboratorios
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Fecha)) + '-' + RTRIM(LTRIM(B.Codigo)) + '-' + RTRIM(LTRIM(B.Servicio)) + RTRIM(LTRIM(B.Relacionada)) + '-' + RTRIM(LTRIM(B.Medico))  + '-' + RTRIM(LTRIM(B.TipoExamen)) + '-' + RTRIM(LTRIM(B.Estado)) + '-(' + RTRIM(LTRIM(B.Cantidad))  + ')' + RTRIM(LTRIM(B.Datos)) 
		FROM @TablaLaboratoriosDX B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As Laboratorios
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno		
		

  		--Consulto las Patologias
		DECLARE @TablaPatologiasDX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Fecha VARCHAR(2000), Codigo VARCHAR(2000),Servicio VARCHAR(2000), Relacionada VARCHAR(2000), Medico VARCHAR(2000), TipoExamen VARCHAR(2000),Estado VARCHAR(2000), Cantidad VARCHAR(1000), Datos VARCHAR(2000))
		INSERT INTO @TablaPatologiasDX
		SELECT B.IPCODPACI 'Identificacion', C.FECORDMED 'Fecha', C.CODSERIPS 'Codigo', RTRIM(E.DESSERIPS) 'Servicio', IIF(RTRIM(CD.Name) IS NULL,'',CONCAT('-',RTRIM(CD.Name))) 'Relacionada', D.NOMMEDICO 'Medico', CASE C.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' END AS 'TipoExamen',CASE C.ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Muestra Recolectada' WHEN '3' THEN 'Resultado Entregado' WHEN '4' THEN 'Examen Interpretado' WHEN '5' THEN 'Remitido' WHEN '6' THEN 'Anulado' WHEN '7' THEN 'Extramural' END AS 'Estado', C.CANSERIPS 'Cantidad',IIF(C.OBSSERIPS IS NULL,'',CONCAT('-',C.OBSSERIPS)) 'Datos'
		FROM HCENTREGATURNOCUPS A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN HCORDPATO C WITH (NOLOCK) ON  A.CODSERIPS = C.CODSERIPS AND A.NUMEFOLIO = C.NUMEFOLIO AND B.IPCODPACI = C.IPCODPACI
		JOIN INPROFSAL D WITH (NOLOCK) ON C.CODPROSAL = D.CODPROSAL
        JOIN INCUPSIPS E WITH(NOLOCK) ON C.CODSERIPS=E.CODSERIPS 	
		JOIN HCHISPACA AS J WITH(NOLOCK) ON C.NUMEFOLIO=J.NUMEFOLIO AND C.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id=C.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id=CDD.ContractDescriptionId 
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno  AND A.TIPOSERVICIO = 4	
		-- Actualizo el Campo de Relacionada & Datos - From NULL To ''
		UPDATE @TablaPatologiasDX
		SET Relacionada = ''
		WHERE Relacionada IS NULL;
		UPDATE @TablaPatologiasDX
		SET Datos = ''
		WHERE Datos IS NULL;
		-- Guardo Patologias Para Consultar: @TablaPatologias
		DECLARE @TablaPatologias AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), Patologias VARCHAR(MAX))
		INSERT INTO @TablaPatologias
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Fecha)) + '-' + RTRIM(LTRIM(B.Codigo)) + '-' + RTRIM(LTRIM(B.Servicio)) + RTRIM(LTRIM(B.Relacionada))  + '-' + RTRIM(LTRIM(B.Medico)) + '-' + RTRIM(LTRIM(B.TipoExamen)) + '-' + RTRIM(LTRIM(B.Estado)) + '-(' + RTRIM(LTRIM(B.Cantidad))  + ')' + RTRIM(LTRIM(B.Datos)) 
		FROM @TablaPatologiasDX B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As Patologias
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno

	--Consulto las Interconsultas
		DECLARE @TablaInterconsultaDX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Fecha VARCHAR(2000), Codigo VARCHAR(2000),Servicio VARCHAR(2000), Medico VARCHAR(2000), Especialidad VARCHAR(2000), TipoExamen VARCHAR(2000),Estado VARCHAR(2000), Observacion VARCHAR(2000))
		INSERT INTO @TablaInterconsultaDX
		SELECT B.IPCODPACI 'Identificacion', C.FECORDMED 'Fecha', C.CODSERIPS 'Codigo', RTRIM(E.DESSERIPS) 'Servicio', D.NOMMEDICO 'Medico', RTRIM(Esp.DESESPECI) 'Especialidad' , CASE C.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' END AS 'TipoExamen',CASE C.ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Solicitud Enviada' WHEN '3' THEN 'Interconsulta Realizada' WHEN '4' THEN 'Extramural' WHEN '5' THEN 'Anulado' END AS 'Estado', IIF(C.OBSSERIPS IS NULL,'',CONCAT('-',C.OBSSERIPS)) 'Observacion'
		FROM HCENTREGATURNOCUPS A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN HCORDINTE C WITH (NOLOCK) ON  A.CODSERIPS = C.CODSERIPS AND A.NUMEFOLIO = C.NUMEFOLIO AND B.IPCODPACI = C.IPCODPACI
		JOIN INPROFSAL D WITH (NOLOCK) ON C.CODPROSAL = D.CODPROSAL
        JOIN INCUPSIPS E WITH(NOLOCK) ON C.CODSERIPS=E.CODSERIPS 
		JOIN INESPECIA Esp WITH(NOLOCK) ON C.CODESPECI=Esp.CODESPECI 
		INNER JOIN DBO.HCHISPACA AS J WITH(NOLOCK) ON C.NUMEFOLIO=J.NUMEFOLIO AND C.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id=C.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id=CDD.ContractDescriptionId 
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno AND A.TIPOSERVICIO = 2		
		-- Actualizo el Campo de Observacion - From NULL To ''
		UPDATE @TablaInterconsultaDX
		SET Observacion = ''
		WHERE Observacion IS NULL;
		-- Guardo tabla de Interconsultas: @TablaInterconsulta
		DECLARE @TablaInterconsulta AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), Interconsultas VARCHAR(MAX))
		INSERT INTO @TablaInterconsulta
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Fecha)) + '-' + RTRIM(LTRIM(B.Codigo)) + '-' + RTRIM(LTRIM(B.Servicio)) + '-' + RTRIM(LTRIM(B.Medico)) + '-' + RTRIM(LTRIM(B.Especialidad)) + '-' + RTRIM(LTRIM(B.TipoExamen)) + '-' + RTRIM(LTRIM(B.Estado)) + RTRIM(LTRIM(B.Observacion)) 
		FROM @TablaInterconsultaDX B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As Interconsultas
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno

	--Consulto las Procedimiento Qx
		DECLARE @TablaProcedimientosQX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Fecha VARCHAR(2000), Codigo VARCHAR(2000),Servicio VARCHAR(2000), Relacionada VARCHAR(2000), lateralidad VARCHAR(2000), Medico VARCHAR(2000), TipoExamen VARCHAR(2000),Sala VARCHAR(2000), Estado VARCHAR(2000),Cantidad VARCHAR(1000), Observacion VARCHAR(2000))
		INSERT INTO @TablaProcedimientosQX
		SELECT B.IPCODPACI 'Identificacion', C.FECORDMED 'Fecha', C.CODSERIPS 'Codigo', RTRIM(E.DESSERIPS) 'Servicio', IIF(RTRIM(CD.Name) IS NULL,'',CONCAT('-',RTRIM(CD.Name))) 'Relacionada', CASE C.LATERALIDAD WHEN '0' THEN 'No Aplica' WHEN '1' THEN 'Izquierda' WHEN '3' THEN 'Derecha' WHEN '4' THEN 'Ambos' END AS 'lateralidad', D.NOMMEDICO 'Medico', CASE C.PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' END AS 'TipoExamen', CASE C.SOLICITASALA WHEN '0' THEN 'No requiere sala' WHEN '1' THEN 'Requiere sala' END AS 'Sala', CASE C.ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Sala Programada' WHEN '3' THEN 'Cancelado' WHEN '4' THEN 'Resultado Revisado' END AS 'Estado', C.CANSERIPS 'Cantidad',IIF(C.OBSSERIPS IS NULL,'',CONCAT('-',C.OBSSERIPS)) 'Observacion'
		FROM HCENTREGATURNOCUPS A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN HCORDPROQ C WITH (NOLOCK) ON  A.CODSERIPS = C.CODSERIPS AND A.NUMEFOLIO = C.NUMEFOLIO AND B.IPCODPACI = C.IPCODPACI
		JOIN INPROFSAL D WITH (NOLOCK) ON C.CODPROSAL = D.CODPROSAL
        JOIN INCUPSIPS E WITH(NOLOCK) ON C.CODSERIPS=E.CODSERIPS 	
		JOIN HCHISPACA AS J WITH(NOLOCK) ON C.NUMEFOLIO=J.NUMEFOLIO AND C.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id=C.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id=CDD.ContractDescriptionId 
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno AND A.TIPOSERVICIO = 6
		-- Actualizo el Campo de Relacionada - From NULL To ''
		UPDATE @TablaProcedimientosQX
		SET Relacionada = ''
		WHERE Relacionada IS NULL;
		UPDATE @TablaProcedimientosQX
		SET Observacion = ''
		WHERE Observacion IS NULL;		
		-- Guardo Procedmientos Para Consultar: @TablaProcemientoQX
		DECLARE @TablaProcemdientoQX AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), ProcedimientosQX VARCHAR(MAX))
		INSERT INTO @TablaProcemdientoQX
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Fecha)) + '-' + RTRIM(LTRIM(B.Codigo)) + '-' + RTRIM(LTRIM(B.Servicio)) + RTRIM(LTRIM(B.Relacionada))  + '-' + RTRIM(LTRIM(B.lateralidad)) + '-' + RTRIM(LTRIM(B.Medico)) + '-' + RTRIM(LTRIM(B.TipoExamen)) + '-' + RTRIM(LTRIM(B.Sala)) + '-' + RTRIM(LTRIM(B.Estado)) + '-(' + RTRIM(LTRIM(B.Cantidad))  + ')' + RTRIM(LTRIM(B.Observacion)) 
		FROM @TablaProcedimientosQX B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As ProcedimientosQX
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	

 	--Consulto las Procedimientos NoQx
		DECLARE @TablaProcedimientosNoQX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Fecha VARCHAR(2000), Codigo VARCHAR(2000),Servicio VARCHAR(2000), Relacionada VARCHAR(2000), lateralidad VARCHAR(2000), Medico VARCHAR(2000), TipoExamen VARCHAR(2000),Sitio VARCHAR(2000), Estado VARCHAR(2000),Cantidad VARCHAR(1000), Observacion VARCHAR(2000))
		INSERT INTO @TablaProcedimientosNoQX
		SELECT B.IPCODPACI 'Identificacion', C.FECORDMED 'Fecha', C.CODSERIPS 'Codigo', RTRIM(E.DESSERIPS) 'Servicio',  IIF(RTRIM(CD.Name) IS NULL,'',CONCAT('-',RTRIM(CD.Name))) 'Relacionada', CASE C.LATERALIDAD WHEN '0' THEN 'No Aplica' WHEN '1' THEN 'Izquierda' WHEN '2' THEN 'Derecha' WHEN '3' THEN 'Ambos' END AS 'lateralidad', D.NOMMEDICO 'Medico', CASE C.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutina' END AS 'TipoExamen', CASE C.EXREASITI WHEN '0' THEN 'No Examen Sitio' WHEN '1' THEN 'No Examen Sitio' END AS 'Sitio', CASE C.ESTSERIPS WHEN '1' THEN 'Ordenado' WHEN '2' THEN 'Completado' WHEN '3' THEN 'Interpretado' WHEN '4' THEN 'Sin Interfaz' WHEN '5' THEN 'Anulado'  END AS 'Estado', C.CANSERIPS 'Cantidad',IIF(C.OBSSERIPS IS NULL,'',CONCAT('-',C.OBSSERIPS)) 'Observacion'
		FROM HCENTREGATURNOCUPS A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN HCORDPRON C WITH (NOLOCK) ON  A.CODSERIPS = C.CODSERIPS AND A.NUMEFOLIO = C.NUMEFOLIO AND B.IPCODPACI = C.IPCODPACI
		JOIN INPROFSAL D WITH (NOLOCK) ON C.CODPROSAL = D.CODPROSAL
        JOIN INCUPSIPS E WITH(NOLOCK) ON C.CODSERIPS=E.CODSERIPS 	
		JOIN HCHISPACA AS J WITH(NOLOCK) ON C.NUMEFOLIO=J.NUMEFOLIO AND C.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id=C.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id=CDD.ContractDescriptionId 
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno AND A.TIPOSERVICIO = 5
		-- Actualizo el Campo de Relacionada & Observacion - From NULL To ''
		UPDATE @TablaProcedimientosNoQX
		SET Relacionada = ''
		WHERE Relacionada IS NULL;
		UPDATE @TablaProcedimientosNoQX
		SET Observacion = ''
		WHERE Observacion IS NULL;
		-- Guardo Procedimientos NoQX Para Consultar: @TablaProcedimientosNoQx
		DECLARE @TablaProcedimientoNoQx AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), ProcedimientosNoQX VARCHAR(MAX))
		INSERT INTO @TablaProcedimientoNoQx
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Fecha)) + '-' + RTRIM(LTRIM(B.Codigo)) + '-' + RTRIM(LTRIM(B.Servicio)) + RTRIM(LTRIM(B.Relacionada))  + '-' + RTRIM(LTRIM(B.lateralidad)) + '-' + RTRIM(LTRIM(B.Medico)) + '-' + RTRIM(LTRIM(B.TipoExamen)) + '-' + RTRIM(LTRIM(B.Sitio)) + '-' + RTRIM(LTRIM(B.Estado)) + '-(' + RTRIM(LTRIM(B.Cantidad))  + ')' + RTRIM(LTRIM(B.Observacion)) 
		FROM @TablaProcedimientosNoQX B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As ProcedimientosNoQX
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	
	

  	--Consulto las Referencias
		DECLARE @TablaReferenciaQx AS TABLE(Id INT IDENTITY(1,1), Fecha VARCHAR(2000), Estado VARCHAR(2000),Identificacion VARCHAR(25), Paciente VARCHAR(2000),Ingreso VARCHAR(2000), Profesional VARCHAR(2000), Especialidad VARCHAR(2000), UnidadFuncional VARCHAR(2000))
		INSERT INTO @TablaReferenciaQx
		SELECT C.FECSOLICIT As 'Fecha', CASE C.ESTADO WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Pendiente o Gestionando' WHEN '3' THEN 'Aceptado con pendiente de salida' WHEN '4' THEN 'Suspendido' WHEN '5' THEN 'Extramural / Ya salio' WHEN '6' THEN 'Solicitud con Pertinencia'  END as 'Estado', B.IPCODPACI 'Identificacion', RTRIM(I.IPNOMCOMP) as 'Paciente', C.NUMINGRES 'Ingreso', D.NOMMEDICO 'Profesional', N.DESESPECI 'Especialidad', U.UFUDESCRI 'UnidadFuncional'
		FROM HCENTREGATURNOREFEREN A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN INPACIENT I WITH (NOLOCK) ON B.IPCODPACI = I.IPCODPACI
		JOIN HCREFCONP C WITH (NOLOCK) ON B.IPCODPACI = C.IPCODPACI
		JOIN INPROFSAL D WITH (NOLOCK) ON C.CODPROSAL = D.CODPROSAL
		JOIN INUNIFUNC U WITH (NOLOCK) ON U.UFUCODIGO = C.UFUCODIGO
		LEFT JOIN INESPECIA N WITH (NOLOCK) ON C.CODESPECI=N.CODESPECI 
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	
		-- Guardo Referencias @TablaReferencia
		DECLARE @TablaReferencia AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), Referencias VARCHAR(MAX))
		INSERT INTO @TablaReferencia
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Fecha)) + '-' + RTRIM(LTRIM(B.Estado)) + '-' + RTRIM(LTRIM(B.Identificacion)) + '-' + RTRIM(LTRIM(B.Paciente))  + '-' + RTRIM(LTRIM(B.Ingreso)) + '-' + RTRIM(LTRIM(B.Profesional)) + '-' + RTRIM(LTRIM(B.Especialidad)) + '-' + RTRIM(LTRIM(B.UnidadFuncional)) 
		FROM @TablaReferenciaQx B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As Referencias
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno

   	--Consulto los Hemocomponentes
		DECLARE @TablaHemoDX AS TABLE(Id INT IDENTITY(1,1), Identificacion VARCHAR(25), Fecha VARCHAR(2000), Prioridad VARCHAR(2000), CodComponenteSanguineo VARCHAR(2000),NomComponenteSanguineo VARCHAR(2000), EstadoComponente VARCHAR(2000), TipoSolicitud VARCHAR(2000), CodMedico VARCHAR(2000), NomMed VARCHAR(2000))
		INSERT INTO @TablaHemoDX
		SELECT DISTINCT B.IPCODPACI 'Identificacion', C.FECORDMED 'Fecha', CASE C.PRIOTRAN WHEN '1' THEN 'Emergencia (hasta 15 min)' WHEN '2' THEN 'Urgencia (hasta 1 hora)'  WHEN '3' THEN 'Urgencia Diferida (hasta 3 horas)' WHEN '4' THEN 'Normal (hasta 24 min)' END AS 'Prioridad' , E.CODCOMSAM 'CodComponenteSanguineo', E.DESCOMSAM 'NomComponenteSanguineo',CASE D.ESTADO WHEN '1' THEN 'Solicitud Reserva' WHEN '2' THEN 'Solicitud de Tranfusión' WHEN '3' THEN 'Reserva sin Solicitud de Transfusión' WHEN '4' THEN 'Reserva con Solicitud de Transfusión' WHEN '5' THEN 'Liberado' WHEN '6' THEN 'Entregado' WHEN '7' THEN 'Aplicado' WHEN '8' THEN 'No Aplicado' WHEN '9' THEN 'Anulado' WHEN '10' THEN 'Descartado por salida del paciente' WHEN '11' THEN 'Extramural'  END AS 'EstadoComponente' , CASE D.TIPOSOLICI WHEN '1' THEN 'Reserva' WHEN '2' THEN 'Transfusión' WHEN '3' THEN 'Reserva & Transfusión' END AS TipoSolicitud, D.PROFSOLRES 'CodMedico', F.NOMMEDICO 'NomMed'
		FROM HCENTREGATURNOHEMOC A WITH (NOLOCK)
		JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IDHCENTREGATURNOCPACIEN = B.ID
		JOIN HCORHEMBOL D WITH (NOLOCK) ON A.IDBOLSA =D.ID	
		JOIN HCORHEMCO C WITH (NOLOCK) ON B.IPCODPACI = C.IPCODPACI AND D.HCORHEMCOID = C.ID
		JOIN HCCOMSAN E WITH (NOLOCK) ON E.ID = D.COMSAMID
		JOIN INPROFSAL F WITH(NOLOCK) ON F.CODPROSAL = D.PROFSOLRES
	    WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	
		--SELECT * FROM @TablaHemoDX
		-- Guardo Hemocomponentes @TablaHemo
		DECLARE @TablaHemo AS TABLE(Id INT IDENTITY(1,1),IPCODPACI VARCHAR(25), Hemocomponentes VARCHAR(MAX))
		INSERT INTO @TablaHemo
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' -' + RTRIM(LTRIM(B.Fecha)) + '-' + RTRIM(LTRIM(B.Prioridad)) + '-' + RTRIM(LTRIM(B.CodComponenteSanguineo)) + '-' + RTRIM(LTRIM(B.NomComponenteSanguineo))  + '-' + RTRIM(LTRIM(B.EstadoComponente)) + '-' + RTRIM(LTRIM(B.TipoSolicitud)) + '-' + RTRIM(LTRIM(B.CodMedico)) + '-' + RTRIM(LTRIM(B.NomMed)) 
		FROM @TablaHemoDX B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As Hemocomponentes
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	
	
		--Consulto escalas del paciente
		DECLARE @TablaEscalas AS TABLE(Id INT IDENTITY(1,1), Identificacion VARCHAR(25), NombreEscala VARCHAR(150), Interpretacion VARCHAR(max))
		INSERT INTO @TablaEscalas
		SELECT A.IPCODPACI 'Identificacion', CASE A.TIPOESCALA WHEN 1 THEN 'CAGE'  WHEN 2 THEN 'APGAR Familiar' WHEN 3 THEN 'EDPS' WHEN 4 THEN 'Biopsicosocial' WHEN 5 THEN 'Tamizaje Violencia Domestica' WHEN 6 THEN 'Riesgo Framingham' WHEN 7 THEN 'Morisky' WHEN 8 THEN 'Test FINDRISC' WHEN 9 THEN 'Test MiniMental' WHEN 10 THEN 'Test Dependencia Nicotina' WHEN 11 THEN 'Tanner Desarrollo Mamario Mujer' WHEN 12 THEN 'Tanner Desarrollo Vello Pubiano Mujer' WHEN 13 THEN 'Tanner Desarrollo Genital Hombre' WHEN 14 THEN 'Tanner Desarrollo Vello Pubiano Hombre' WHEN 15 THEN 'Wagner' WHEN 16 THEN 'Modificada Disnea'
		WHEN 17 THEN 'CAT COPD Assessment Test' WHEN 18 THEN 'Exacerbaciones' WHEN 19 THEN 'Clasificacion EPOC' WHEN 20 THEN 'Test Goodenough' WHEN 21 THEN 'GOLD EPOC' WHEN 22 THEN 'Abreviada Desarrollo' WHEN 23 THEN 'TISS 28' WHEN 24 THEN 'Branden' WHEN 25 THEN 'ApacheII' WHEN 26 THEN 'Karnosfky' WHEN 27 THEN 'Ecog' WHEN 28 THEN 'Nems' WHEN 29 THEN 'Glasgow Mayor 5 Anos' WHEN 30 THEN 'Glasgow de 1 a 5 Anos' WHEN 31 THEN 'Glasgow Menor 1 Ano' WHEN 32 THEN 'SOFA' WHEN 33 THEN 'Charlson' WHEN 34 THEN 'SAPS3' WHEN 35 THEN 'Barthel' WHEN 36 THEN 'Morse' WHEN 37 THEN 'Macdems'
		WHEN 38 THEN 'NSRAS' WHEN 39 THEN 'MSTS' WHEN 40 THEN 'Person' WHEN 41 THEN 'beck' WHEN 42 THEN 'Zarit' WHEN 43 THEN 'RQC' WHEN 44 THEN 'Bacteriana Silness' WHEN 45 THEN 'VALE' WHEN 46 THEN 'RASS' WHEN 47 THEN 'Downton' WHEN 48 THEN 'Norton' WHEN 49 THEN 'Vass' WHEN 50 THEN 'Nutricion' WHEN 51 THEN 'SQR' WHEN 52 THEN 'M CHAT' WHEN 53 THEN 'WHOOLEY'  WHEN 54 THEN 'AUDIT' WHEN 55 THEN 'LindaFried' WHEN 56 THEN 'Lawton Brody' WHEN 57 THEN 'GAD' WHEN 58 THEN 'MNA' WHEN 59 THEN 'MNA Simplificada' WHEN 60 THEN 'Assist' WHEN 61 THEN 'News' WHEN 62 THEN 'CHA2DS2 VASc' WHEN 63 THEN 'CRUSADE' WHEN 64 THEN 'HAS BLED'
		WHEN 65 THEN 'HEMORR2HAGES' WHEN 66 THEN 'EUROS CORE II' WHEN 67 THEN 'NYHA' WHEN 68 THEN 'KILLIP' WHEN 69 THEN 'PADUA' WHEN 70 THEN 'CAPRINI' WHEN 71 THEN 'MUST' WHEN 72 THEN 'STRONG KIDS' WHEN 73 THEN 'VGSDEN'  WHEN 74 THEN 'TIMI CEST' WHEN 75 THEN 'WELLS TVP' WHEN 76 THEN 'WELLS TEP' WHEN 77 THEN 'NPC' WHEN 78 THEN 'GRACE' WHEN 79 THEN 'TIMI SEST' WHEN 80 THEN 'ANTHONISEN' WHEN 81 THEN 'DAS 28' WHEN 82 THEN 'MRS' WHEN 83 THEN 'HAQ' WHEN 84 THEN 'ASPECT' WHEN 85 THEN 'Abreviada Desarrollo V3' WHEN 86 THEN 'Indice OLeary' WHEN 87 THEN 'NIHSS' WHEN 88 THEN 'Humpty Dumpty'  WHEN 89 THEN 'Riesgo Enfermedades Potencial Transmisibles' 
		WHEN 90 THEN 'PIPP R' WHEN 91 THEN 'FLACC' WHEN 92 THEN 'OFRAS' WHEN 93 THEN 'FPSR' 
		WHEN 94 THEN 'Escala (NRS) numérica del dolor' WHEN 95 THEN 'Escala de valoración sociofamiliar de Gijón modificada' WHEN 96 THEN 'Escala Cam (Confusion Assessment Method)' WHEN 97 THEN 'Escala de valoración del riesgo de infección' WHEN 98 THEN 'Escala de valoración del riesgo farmacológico' WHEN 99 THEN 'Escala de valoración riesgo psicosocial (conducta suicida)' WHEN 100 THEN 'Escala de valoración sociofamiliar de Gijón original' WHEN 101 THEN 'Escala obstétrica de alerta temprana'
		WHEN 102 THEN 'Escala MPEWS - Sistema de alerta temprana pediátrica modificado' WHEN 103 THEN 'Escala BPEWS - sistema de slerta temprana pediátrica al lado de la cama' WHEN 104 THEN 'Escala eventos tromboembólicos venosos durante la gestación, parto y puerperio' WHEN 105 THEN 'Escala de Bishop'
		END 'Escala', 
		dbo.InterpretacionEscala(A.ID,A.TIPOESCALA,A.RESULTADO,A.RESULTADODECIMAL,C.IPSEXOPAC) 'Resultado'
		FROM HCESCALAS A
		INNER JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES
		INNER JOIN INPACIENT C WITH(NOLOCK) ON B.IPCODPACI = C.IPCODPACI
		WHERE B.IDHCENTREGATURNOC = @INDEntregaTurno 
		ORDER BY A.FECHAREGISTRO DESC
		-- Guardo Escalas en la tabla @TablaEscalasPaciente
		DECLARE @TablaEscalasPaciente AS TABLE(Id INT IDENTITY(1,1), IPCODPACI VARCHAR(25), Escala VARCHAR(MAX))
		INSERT INTO @TablaEscalasPaciente
		SELECT A.IPCODPACI,
		STUFF(( SELECT ' *' + B.NombreEscala + ': ' + B.Interpretacion + char(10)
		FROM @TablaEscalas B
		WHERE B.Identificacion = A.IPCODPACI FOR XML PATH('')), 1, 2, '') As Escalas
		FROM HCENTREGATURNOCPACIEN A WITH(NOLOCK)
		WHERE A.IDHCENTREGATURNOC = @INDEntregaTurno	

	-- Camas:	
		DECLARE @CentroAtencion NVARCHAR(100)
		DECLARE  @UnidadFuncional NVARCHAR(100)
		SET @CentroAtencion = ( SELECT A.CODCENATE FROM HCENTREGATURNOC A WHERE ID = @INDEntregaTurno)
		SET @UnidadFuncional = ( SELECT A.UFUCODIGO FROM HCENTREGATURNOC A WHERE ID = @INDEntregaTurno)
		DECLARE @TablaCama AS TABLE(Id INT IDENTITY(1,1),Codigo VARCHAR(25), Cama VARCHAR(2000), IPCODPACI VARCHAR(2000), Ingreso VARCHAR(2000), Paciente VARCHAR(2000))
		INSERT INTO @TablaCama
		SELECT DISTINCT A.CODICAMAS AS 'Codigo',  RTRIM(DESCCAMAS) AS Cama, C.IPCODPACI ,C.NUMINGRES AS Ingreso,RTRIM(H.IPNOMCOMP) AS Paciente
		FROM dbo.CHCAMASHO A with (nolock)
		INNER JOIN dbo.ADcenaten D with (nolock) ON A.CODCENATE=D.CODCENATE 
		INNER JOIN dbo.INUNIFUNC E with (nolock) ON A.UFUCODIGO=E.UFUCODIGO 
		LEFT OUTER JOIN dbo.CHREGESTA C with (nolock) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
		LEFT OUTER JOIN dbo.CHTIPESTA G with (nolock) ON G.CODTIPEST=C.CODTIPEST 
		INNER JOIN dbo.INPacient H with (nolock) ON C.IPCODPACI=H.IPCODPACI 
		INNER JOIN dbo.HCENTREGATURNOCPACIEN M with (nolock) ON H.IPCODPACI= M.IPCODPACI 
		WHERE A.CODCENATE=@CentroAtencion AND A.UFUCODIGO = @UnidadFuncional AND ESTADCAMA ='2' AND M.IDHCENTREGATURNOC = @INDEntregaTurno
		ORDER BY A.CODICAMAS

		-- Información Pacientes
		SELECT
		A.ID          'Turno',
		dbo.TipoDocumento(C.IPTIPODOC) AS 'tipodocumento',
		C.IPCODPACI   'Identificacion',
		C.IPNOMCOMP   'Nombre',
		D.NOMENTIDA   'Entidad',
		C.IPFECNACI   AS 'EDAD', '' AS 'FECHADENACIMIENTO',
		IIF(B.OBSERVACION = '','Sin Observación',B.OBSERVACION) AS 'ObservacionTurno',
		E.Medicamentos,
		F.Diagnosticos,
		G.Imagenes,
		H.Laboratorios,
		I.Patologias,
		J.Interconsultas,
		K.ProcedimientosQX,
		L.ProcedimientosNoQX,
		N.Referencias,
		M.Hemocomponentes,
		O.Cama,
		P.Escala
		FROM
		HCENTREGATURNOC A WITH (NOLOCK)
		INNER JOIN HCENTREGATURNOCPACIEN B WITH (NOLOCK) ON A.ID = B.IDHCENTREGATURNOC
		INNER JOIN INPACIENT C WITH (NOLOCK) ON B.IPCODPACI = C.IPCODPACI
		INNER JOIN INENTIDAD D WITH (NOLOCK) ON C.CODENTIDA = D.CODENTIDA
		INNER JOIN @TablaMedicamentos E ON C.IPCODPACI = E.IPCODPACI
		INNER JOIN @TablaDiagnosticos F ON C.IPCODPACI = F.IPCODPACI
		INNER JOIN @TablaImagenes G ON C.IPCODPACI = G.IPCODPACI
		INNER JOIN @TablaLaboratorios H ON C.IPCODPACI = H.IPCODPACI
		INNER JOIN @TablaPatologias I ON C.IPCODPACI = I.IPCODPACI
		INNER JOIN @TablaInterconsulta J ON C.IPCODPACI = J.IPCODPACI
		INNER JOIN @TablaProcemdientoQX K ON C.IPCODPACI = K.IPCODPACI
		INNER JOIN @TablaProcedimientoNoQx L ON C.IPCODPACI = L.IPCODPACI
		INNER JOIN @TablaReferencia N ON C.IPCODPACI = N.IPCODPACI
		INNER JOIN @TablaHemo M ON C.IPCODPACI = M.IPCODPACI
		LEFT JOIN @TablaCama O ON C.IPCODPACI = O.IPCODPACI --Se deja left ya que al consultar histórico los pacientes pueden no estar con estancia activa BUG-18367
		INNER JOIN @TablaEscalasPaciente P ON P.IPCODPACI = B.IPCODPACI
		WHERE
			A.ID = @INDEntregaTurno	
	END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de entrega de turno clínico en tres modos según el parámetro de tipo de solicitud: (1) cabecera del turno con sede, unidad funcional, jornada (mañana/tarde/noche) y dashboard (Médico/Enfermería/Académico/Terapia); (2) listado de profesionales de la salud que entregan y reciben el turno con su nombre, especialidad y firma; (3) detalle de los pacientes incluidos en el turno con sus medicamentos activos (código, descripción, indicaciones y vía de administración), diagnósticos CIE-10 (código, nombre y observación), imágenes diagnósticas solicitadas y otros datos clínicos relevantes. Integra las tablas de entregas de turno nocturno, pacientes, prescripciones, diagnósticos e imágenes para centralizar toda la información que un profesional de la salud necesita al hacer el cambio de guardia en una unidad funcional de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ReporteEntregaTurno';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ReporteEntregaTurno';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la información se filtra por el identificador de entrega de turno recibido como parámetro.; Los catálogos de estado, prioridad, lateralidad y tipo de examen se traducen a literales legibles en español según el tipo de orden (imagen, laboratorio, patología, interconsulta, procedimiento Qx/NoQx, hemocomponente).; Los campos de observación, indicaciones y descripción relacionada nulos se normalizan a cadena vacía antes de concatenar.; Las concatenaciones por paciente se realizan con STUFF + FOR XML PATH agrupando por IPCODPACI.; El listado final de pacientes hace LEFT JOIN sobre camas (un paciente puede no tener cama activa en consultas históricas) e INNER JOIN sobre el resto de bloques clínicos.; Las consultas usan WITH (NOLOCK) en todas las lecturas, asumiendo lectura sucia aceptable para el reporte.; El procedimiento solo lee datos: no modifica tablas físicas, únicamente tablas variables temporales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ReporteEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Entrega de turno clínico; Cambio de guardia; Profesional de la salud (entrega/recibe); Especialidad médica; Firma del profesional; Paciente hospitalizado; Medicamentos / prescripción / vía de administración; Diagnósticos (CIE); Órdenes de imágenes diagnósticas; Órdenes de laboratorio; Órdenes de patología; Interconsultas; Procedimientos quirúrgicos y no quirúrgicos; Lateralidad; Prioridad / tipo de examen (Urgente, Rutina, Emergencia); Estado de orden clínica; Referencias y contrarreferencias; Hemocomponentes / transfusión / reserva de sangre; Prioridad transfusional; Escalas clínicas (Glasgow, Apache II, Braden, Morse, NIHSS, etc.); Camas hospitalarias / ocupación; Centro de atención y unidad funcional; Jornada (Mañana/Tarde/Noche); Dashboard (Médico/Enfermería/Académico/Terapia); Entidad aseguradora / pagador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ReporteEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de solicitud = 1 (Cabecera) → Devuelve datos de cabecera de la entrega de turno: fecha, dashboard (Médico/Enfermería/Académico/Terapia), centro de atención, unidad funcional y jornada (Mañana/Tarde/Noche).; si Tipo de solicitud = 2 (Profesionales) → Devuelve los profesionales que entregan (rol 0) y reciben (rol 1) el turno con código, nombre, especialidad y firma.; si Tipo de solicitud = 3 (Pacientes) → Construye y devuelve el detalle clínico consolidado por paciente: medicamentos, diagnósticos, imágenes, laboratorios, patologías, interconsultas, procedimientos Qx/NoQx, referencias, hemocomponentes, cama y escalas.; si TIPOSERVICIO = 1 en HCENTREGATURNOCUPS → El registro se clasifica como Imagen diagnóstica (HCORDIMAG).; si TIPOSERVICIO = 2 → Se clasifica como Interconsulta (HCORDINTE).; si TIPOSERVICIO = 3 → Se clasifica como Laboratorio (HCORDLABO).; si TIPOSERVICIO = 4 → Se clasifica como Patología (HCORDPATO).; si TIPOSERVICIO = 5 → Se clasifica como Procedimiento No Quirúrgico (HCORDPRON).; si TIPOSERVICIO = 6 → Se clasifica como Procedimiento Quirúrgico (HCORDPROQ).; si Cama con ESTADCAMA = ''2'' y CHREGESTA.REGESTADO = 1 → Se considera que la cama está ocupada por el paciente y se incluye en el listado de camas del turno.; si OBSERVACION del paciente vacía → Se reemplaza por la literal ''Sin Observación''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ReporteEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.InterpretacionEscala; dbo.TipoDocumento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ReporteEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCENTREGATURNOC; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.HCENTREGATURNOPROFSAL; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCENTREGATURNOMEDICA; dbo.HCENTREGATURNOCPACIEN; dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.HCENTREGATURNODIAGNO; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCENTREGATURNOCUPS; dbo.HCORDIMAG; dbo.INCUPSIPS; dbo.HCHISPACA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDINTE; dbo.HCORDPROQ; dbo.HCORDPRON; dbo.HCENTREGATURNOREFEREN; dbo.INPACIENT; dbo.HCREFCONP; dbo.HCENTREGATURNOHEMOC; dbo.HCORHEMBOL; dbo.HCORHEMCO (+6 adicionales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ReporteEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ReporteEntregaTurno';
-- GO

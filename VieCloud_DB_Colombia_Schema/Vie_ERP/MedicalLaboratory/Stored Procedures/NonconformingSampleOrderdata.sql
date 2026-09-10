

CREATE PROCEDURE [MedicalLaboratory].[NonconformingSampleOrderdata] (
    @IdOrden INT,
	@Hospitalario BIT
  ) AS BEGIN
SET
  NOCOUNT ON;
  DECLARE @NEWID UNIQUEIDENTIFIER = Newid(), @TipoEvento AS VARCHAR(50) = 'Muestra no conforme', @FechaEvento as varchar(20) = FORMAT(common.GETDATE(), 'dd/MM/yyyy HH:mm:ss');

  IF @Hospitalario = 1
  BEGIN	

	IF NOT EXISTS (SELECT 1 FROM HCORDLABO WHERE AUTO = @IdOrden AND ESTSERIPS = 2)
	BEGIN		
		RETURN
	END

	SELECT @NEWID AS Id, @TipoEvento AS EventType, @IdOrden AS AggregateId, @FechaEvento AS FechaEvento,
	(SELECT 
		--===================================
		-- Evento
		--===================================
		'Laboratorio' AS 'Evento.Origen', 
		@TipoEvento As 'Evento.TipoEvento',
		@FechaEvento AS 'Evento.FechaEvento',
		@NEWID AS 'Evento.IdEvento',
		SUBSTRING(DB_NAME(),7,3) AS 'Evento.CodeBD',

		--===================================
		-- Datos de la orden médica
		--===================================
		RTRIM(A.AUTO)	   AS 'Orden.IdOrden',
		'Hospitalario'	   AS 'Orden.Origen',
		RTRIM(A.NUMINGRES) AS 'Orden.Ingreso',
		RTRIM(A.NUMEFOLIO) AS 'Orden.Folio',
		RTRIM(D.CODSERIPS) AS 'Orden.Servicio.CodigoServicio',
		RTRIM(D.DESSERIPS) AS 'Orden.Servicio.DescripcionServicio',
		ISNULL(CONVERT(VARCHAR(50), A.IDDESCRIPCIONRELACIONADA), '') AS 'Orden.Servicio.IdDescripcionRelacionada',
		RTRIM(ISNULL(CD.name,'')) AS 'Orden.Servicio.DescripcionRelacionada',
		RTRIM(A.CODMOTIVOMUESTRANOCONFORME) AS 'Orden.MuestraNoConforme.CodigoMotivo',
		RTRIM(I.DESMOTANU) AS 'Orden.MuestraNoConforme.Motivo',
		RTRIM(A.OBSERMUESTRANOCONFORME) AS 'Orden.MuestraNoConforme.Observacion',
	
		--===================================
		-- Datos del centro de atención
		--===================================
		RTRIM(E.CODCENATE) AS 'CentroAtencion.CodigoCentro',
		RTRIM(E.NOMCENATE) AS 'CentroAtencion.NombreCentro',
	
		--===================================
		-- Datos de la unidad funcional
		--===================================
		RTRIM(F.UFUCODIGO) AS 'UnidadFuncional.CodigoUnidad',
		RTRIM(F.UFUDESCRI) AS 'UnidadFuncional.DescripcionUnidad',
	
		--===================================
		-- Datos del paciente
		--===================================
		RTRIM(G.SIGLA)	   AS 'Paciente.TipoDocumentoSiglas',
		RTRIM(G.NOMBRE)	   AS 'Paciente.TipoDocumento',
		RTRIM(B.IPCODPACI) AS 'Paciente.CodigoPaciente',
		RTRIM(B.IPNOMCOMP) AS 'Paciente.NombreCompleto'		
	
	FROM HCORDLABO A
		INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI
		INNER JOIN INPROFSAL C ON A.CODPROSAL = C.CODPROSAL
		INNER JOIN INCUPSIPS D ON A.CODSERIPS = D.CODSERIPS
		INNER JOIN ADCENATEN E ON A.CODCENATE = E.CODCENATE
		INNER JOIN INUNIFUNC F ON A.UFUCODIGO = F.UFUCODIGO
		INNER JOIN ADTIPOIDENTIFICA G ON G.CODIGO = B.IPTIPODOC
		INNER JOIN HCMOANULB I ON A.CODMOTIVOMUESTRANOCONFORME = I.CODMOTANU
		LEFT JOIN CONTRACT.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN CONTRACT.ContractDescriptions  CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId 
	WHERE A.AUTO = @IdOrden
	FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS Payload
  END
  ELSE -- Ambulatorio
  BEGIN

	IF NOT EXISTS (SELECT 1 FROM AMBORDLAB WHERE AUTO = @IdOrden AND ESTSERIPS = 2)
	BEGIN		
		RETURN
	END

	SELECT @NEWID AS Id, @TipoEvento AS EventType, @IdOrden AS AggregateId, @FechaEvento AS FechaEvento,
  	(SELECT 
		--===================================
		-- Evento
		--===================================
		'Laboratorio' AS 'Evento.Origen', 
		@TipoEvento As 'Evento.TipoEvento',
		@FechaEvento AS 'Evento.FechaEvento',
		@NEWID AS 'Evento.IdEvento',
		SUBSTRING(DB_NAME(),7,3) AS 'Evento.CodeBD',

		--===================================
		-- Datos de la orden médica
		--===================================
		RTRIM(A.AUTO)	   AS 'Orden.IdOrden',
		'Ambulatorio'	   AS 'Orden.Origen',
		RTRIM(A.NUMINGRES) AS 'Orden.Ingreso',
		'' AS 'Orden.Folio',
		RTRIM(D.CODSERIPS) AS 'Orden.Servicio.CodigoServicio',
		RTRIM(D.DESSERIPS) AS 'Orden.Servicio.DescripcionServicio',
		ISNULL(CONVERT(VARCHAR(50), A.IDDESCRIPCIONRELACIONADA), '') AS 'Orden.Servicio.IdDescripcionRelacionada',
		RTRIM(ISNULL(CD.name,'')) AS 'Orden.Servicio.DescripcionRelacionada',
		RTRIM(A.CODMOTIVOMUESTRANOCONFORME) AS 'Orden.MuestraNoConforme.CodigoMotivo',
		RTRIM(I.DESMOTANU) AS 'Orden.MuestraNoConforme.Motivo',
		RTRIM(A.OBSERMUESTRANOCONFORME) AS 'Orden.MuestraNoConforme.Observacion',

		--===================================
		-- Datos del centro de atención
		--===================================
		RTRIM(E.CODCENATE) AS 'CentroAtencion.CodigoCentro',
		RTRIM(E.NOMCENATE) AS 'CentroAtencion.NombreCentro',
	
		--===================================
		-- Datos de la unidad funcional
		--===================================
		RTRIM(F.UFUCODIGO) AS 'UnidadFuncional.CodigoUnidad',
		RTRIM(F.UFUDESCRI) AS 'UnidadFuncional.DescripcionUnidad',
	
		--===================================
		-- Datos del paciente
		--===================================
		RTRIM(G.SIGLA)	   AS 'Paciente.TipoDocumentoSiglas',
		RTRIM(G.NOMBRE)	   AS 'Paciente.TipoDocumento',
		RTRIM(B.IPCODPACI) AS 'Paciente.CodigoPaciente',
		RTRIM(B.IPNOMCOMP) AS 'Paciente.NombreCompleto'		
	
	FROM AMBORDLAB A
		INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI
		INNER JOIN INPROFSAL C ON A.CODPROSAL = C.CODPROSAL
		INNER JOIN INCUPSIPS D ON A.CODSERIPS = D.CODSERIPS
		INNER JOIN ADCENATEN E ON A.CODCENATE = E.CODCENATE
		INNER JOIN INUNIFUNC F ON A.UFUCODIGO = F.UFUCODIGO
		INNER JOIN ADTIPOIDENTIFICA G ON G.CODIGO = B.IPTIPODOC		
		INNER JOIN HCMOANULB I ON A.CODMOTIVOMUESTRANOCONFORME = I.CODMOTANU
		LEFT JOIN CONTRACT.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN CONTRACT.ContractDescriptions  CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId 
	WHERE A.AUTO = @IdOrden
	FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS Payload
  END 
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera un payload JSON con los datos de una orden de laboratorio con muestra no conforme, diferenciando entre órdenes hospitalarias (tabla `HCORDLABO`) y ambulatorias (`AMBORDLAB`), validando previamente que la orden tenga estado `ESTSERIPS = 2`. El resultado incluye metadatos del evento, datos de la orden (motivo y observación de no conformidad), centro de atención, unidad funcional y paciente, para ser consumido como evento de dominio en un bus de integración.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y devuelve, en formato JSON, el evento de integración ''Muestra no conforme'' de una orden de laboratorio (hospitalaria o ambulatoria) cuando la orden está en estado de muestra no conforme.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden identificada debe existir en HCORDLABO (hospitalario) o AMBORDLAB (ambulatorio) según corresponda; La orden debe estar en estado ESTSERIPS=2 (muestra no conforme); Deben existir registros relacionados de paciente, profesional, servicio CUPS, centro de atención, unidad funcional, tipo de identificación y motivo de anulación para la orden; El nombre de la base de datos debe tener al menos 9 caracteres para extraer el código (SUBSTRING posición 7,3)', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se genera el evento ''Muestra no conforme'' cuando la orden tiene ESTSERIPS=2; El tipo de evento siempre es ''Muestra no conforme'' y el origen siempre ''Laboratorio''; Cada invocación produce un IdEvento (UNIQUEIDENTIFIER) nuevo; La fecha del evento se toma del reloj central (common.GETDATE) formateada dd/MM/yyyy HH:mm:ss; El código de base de datos publicado se obtiene de los caracteres 7 a 9 del nombre de la BD; Para órdenes ambulatorias el folio se publica vacío (no aplica NUMEFOLIO); El payload se serializa como JSON PATH sin envoltorio de arreglo (objeto único); Solo se emite información si la orden tiene paciente, profesional, servicio CUPS, centro, unidad funcional, tipo de documento y motivo de anulación válidos (joins INNER obligatorios)', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Muestra no conforme; Motivo de no conformidad; Paciente; Tipo de documento de identificación; Centro de atención; Unidad funcional; Profesional de salud; Servicio CUPS; Atención hospitalaria; Atención ambulatoria; Descripción contractual del servicio', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Cuando @Hospitalario=1 y existe HCORDLABO con AUTO=@IdOrden y ESTSERIPS=2 → devuelve fila con Id, EventType=''Muestra no conforme'', AggregateId, FechaEvento y Payload JSON con Origen=''Hospitalario'' (incluye NUMEFOLIO); [RETURN_RESULT] (resultset): Cuando @Hospitalario=0 y existe AMBORDLAB con AUTO=@IdOrden y ESTSERIPS=2 → devuelve fila con Id, EventType=''Muestra no conforme'', AggregateId, FechaEvento y Payload JSON con Origen=''Ambulatorio'' y Folio vacío; [RETURN_RESULT] (resultset): Cuando no existe la orden con ESTSERIPS=2 en la tabla correspondiente → RETURN sin devolver resultset', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Indicador hospitalario = 1 → Valida existencia de orden en HCORDLABO con ESTSERIPS=2 y arma payload JSON marcando Origen=''Hospitalario'' incluyendo Folio (NUMEFOLIO) else Valida existencia de orden en AMBORDLAB con ESTSERIPS=2 y arma payload JSON marcando Origen=''Ambulatorio'' con Folio vacío; si NOT EXISTS orden con AUTO=@IdOrden y ESTSERIPS=2 (en HCORDLABO o AMBORDLAB según el caso) → RETURN sin emitir evento ni payload', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDLABO; AMBORDLAB; INPACIENT; INPROFSAL; INCUPSIPS; ADCENATEN; INUNIFUNC; ADTIPOIDENTIFICA; HCMOANULB; CONTRACT.CUPSEntityContractDescriptions; CONTRACT.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'NonconformingSampleOrderdata';
-- GO

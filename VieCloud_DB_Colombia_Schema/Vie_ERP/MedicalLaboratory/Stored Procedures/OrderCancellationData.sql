

CREATE PROCEDURE [MedicalLaboratory].[OrderCancellationData] (
    @IdOrden INT,
	@Hospitalario BIT
  ) AS BEGIN
SET
  NOCOUNT ON;
  DECLARE @NEWID UNIQUEIDENTIFIER = Newid(), @TipoEvento AS VARCHAR(50) = 'Anulacion', @FechaEvento as varchar(20) = FORMAT(common.GETDATE(), 'dd/MM/yyyy HH:mm:ss');

  IF @Hospitalario = 1
  BEGIN	

	IF NOT EXISTS (SELECT 1 FROM HCORDLABO WHERE AUTO = @IdOrden AND ESTSERIPS = 6)
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
		RTRIM(I.CODMOTANU) AS 'Orden.Anulacion.CodigoMotivoAnulacion',
		RTRIM(I.DESMOTANU) AS 'Orden.Anulacion.MotivoAnulacion',
		RTRIM(H.OBSERVACI) AS 'Orden.Anulacion.ObservacionAnulacion',
	
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
		INNER JOIN HCLABANUL H ON A.IPCODPACI = H.IPCODPACI AND A.NUMINGRES = H.NUMINGRES AND A.CODSERIPS = H.CODSERIPS AND A.NUMEFOLIO = H.NUMEFOLIO
		INNER JOIN HCMOANULB I ON H.CODMOTANU = I.CODMOTANU
		LEFT JOIN CONTRACT.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN CONTRACT.ContractDescriptions  CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId 
	WHERE A.AUTO = @IdOrden
	FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS Payload
  END
  ELSE -- Ambulatorio
  BEGIN

	IF NOT EXISTS (SELECT 1 FROM AMBORDLAB WHERE AUTO = @IdOrden AND ESTSERIPS = 6)
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
		RTRIM(I.CODMOTANU) AS 'Orden.Anulacion.CodigoMotivoAnulacion',
		RTRIM(I.DESMOTANU) AS 'Orden.Anulacion.MotivoAnulacion',
		RTRIM(H.OBSERVACI) AS 'Orden.Anulacion.ObservacionAnulacion',

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
		INNER JOIN HCLABANUL H ON A.IPCODPACI = H.IPCODPACI AND A.NUMINGRES = H.NUMINGRES AND A.CODSERIPS = H.CODSERIPS
		INNER JOIN HCMOANULB I ON H.CODMOTANU = I.CODMOTANU
		LEFT JOIN CONTRACT.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN CONTRACT.ContractDescriptions  CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId 
	WHERE A.AUTO = @IdOrden
	FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS Payload
  END 
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que genera un payload JSON con los datos de anulación de una orden de laboratorio, diferenciando entre órdenes hospitalarias (HCORDLABO, estado ESTSERIPS=6) y ambulatorias (AMBORDLAB, estado ESTSERIPS=6). Consolida información del evento de anulación, la orden médica, el motivo y observación de anulación, el centro de atención, la unidad funcional y el paciente, incluyendo opcionalmente la descripción contractual CUPS asociada.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el payload JSON del evento de anulación de una orden de laboratorio (hospitalaria o ambulatoria) con datos del paciente, centro, unidad funcional y motivo de anulación.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe existir en HCORDLABO (hospitalario) o AMBORDLAB (ambulatorio) con ESTSERIPS = 6 (estado anulado); de lo contrario el procedimiento retorna sin producir resultado.; Debe existir un registro asociado en HCLABANUL con la información de anulación de la orden.; El motivo de anulación debe estar parametrizado en HCMOANULB.; El paciente, profesional, servicio CUPS, centro de atención, unidad funcional y tipo de identificación referenciados deben existir en sus tablas maestras.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se emite el evento de anulación si la orden está en estado ESTSERIPS = 6.; El TipoEvento siempre es ''Anulacion'' y el Origen siempre es ''Laboratorio''.; Cada invocación genera un IdEvento único (NEWID) y una FechaEvento con la hora actual de common.GETDATE().; El campo Evento.CodeBD se deriva siempre de los caracteres 7 a 9 del nombre de la base de datos.; En el flujo ambulatorio, Orden.Folio siempre se emite vacío.; La descripción relacionada del servicio se obtiene opcionalmente vía CUPSEntityContractDescriptions → ContractDescriptions; si no existe, queda en cadena vacía.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Anulación de orden; Motivo de anulación; Paciente; Tipo de documento; Centro de atención; Unidad funcional; Servicio CUPS; Atención hospitalaria; Atención ambulatoria; Evento de integración (Anulación)', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @Hospitalario = 1 y existe la orden en HCORDLABO con ESTSERIPS = 6, devuelve un resultset con Id, EventType=''Anulacion'', AggregateId, FechaEvento y Payload JSON marcando Orden.Origen=''Hospitalario''.; [RETURN_RESULT] RESULTSET: Cuando @Hospitalario = 0 y existe la orden en AMBORDLAB con ESTSERIPS = 6, devuelve un resultset con Id, EventType=''Anulacion'', AggregateId, FechaEvento y Payload JSON marcando Orden.Origen=''Ambulatorio'' y Orden.Folio vacío.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Hospitalario = 1 → Valida existencia en HCORDLABO con ESTSERIPS=6 y construye el payload de origen Hospitalario incluyendo el folio (NUMEFOLIO) y join de anulación por paciente+ingreso+servicio+folio. else Valida existencia en AMBORDLAB con ESTSERIPS=6 y construye el payload de origen Ambulatorio sin folio y con join de anulación por paciente+ingreso+servicio.; si NOT EXISTS orden con ESTSERIPS = 6 → RETURN inmediato sin devolver datos. else Continúa generando el payload del evento.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDLABO; AMBORDLAB; INPACIENT; INPROFSAL; INCUPSIPS; ADCENATEN; INUNIFUNC; ADTIPOIDENTIFICA; HCLABANUL; HCMOANULB; CONTRACT.CUPSEntityContractDescriptions; CONTRACT.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderCancellationData';
-- GO

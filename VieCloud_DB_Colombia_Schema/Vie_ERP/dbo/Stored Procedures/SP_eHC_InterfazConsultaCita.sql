-- =============================================
-- Author:		<Author, Duvan Mejia>
-- Create date: <Create Date,21 Enero 2021,>
-- Description:	<Description, Interfaz Consulta Cita>
-- =============================================
CREATE PROCEDURE  [dbo].[SP_eHC_InterfazConsultaCita]
	-- Add the parameters for the stored procedure here
 @FechaInicial date,
 @FechaFinal date,
 @CodigoPaciente varchar(25),
 @TipoIdentificacion Int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @TablaControlCitaDX AS TABLE(Id INT IDENTITY(1,1),Identificacion VARCHAR(25), Fechacita Date, IDCITA INT, CUPS VARCHAR(1000))
	INSERT INTO @TablaControlCitaDX
SELECT  a.IPCODPACI,a.FECHORAIN,
                (SELECT TOP 1 tmp.CODAUTONU FROM AGASICITA AS tmp WHERE  tmp.FECHORAIN = A.FECHORAIN AND tmp.FECHORAFI = A.FECHORAFI and tmp.IPCODPACI= a.IPCODPACI AND tmp.CODESTCIT = '0' AND tmp.TIPSOLICITU = 2) AS IdCita    ,  
                STUFF((SELECT ', ' + RTRIM(LTRIM(C.CODSERIPS)) + ' - ' + RTRIM(LTRIM(C.DESSERIPS)) FROM AGASICITA AS tmp2 INNER JOIN INCUPSIPS C ON tmp2.CODSERIPS = C.CODSERIPS WHERE tmp2.FECHORAIN = A.FECHORAIN AND tmp2.FECHORAFI = A.FECHORAFI and tmp2.IPCODPACI= a.IPCODPACI AND tmp2.CODESTCIT = '0' AND tmp2.TIPSOLICITU = 2 group by tmp2.IPCODPACI,  tmp2.FECHORAIN, C.CODSERIPS,C.DESSERIPS  FOR XML PATH('')),1,2,'') AS CUPS
    FROM AGASICITA a INNER JOIN INPACIENT B ON a.IPCODPACI = B.IPCODPACI
    WHERE
        Cast(A.FECHORAIN As Date) BETWEEN @FechaInicial AND @FechaFinal
        AND A.IPCODPACI =  @CodigoPaciente
        AND B.IPTIPODOC = @TipoIdentificacion
        AND A.CODESTCIT = '0'
        AND A.TIPSOLICITU = 2
        group by A.IPCODPACI,  A.FECHORAIN, A.FECHORAFI

		

    -- Insert statements for procedure here
	SELECT 
		CASE A.TIPSOLICITU
		    WHEN '1' THEN 'Cita Medica'
			WHEN '2' THEN 'Cita Apoyo Diagnostico'
            WHEN '3' THEN 'Cita Tratamiento Especiales'
        End  As 'TIPOSOLICITUD', 
        CASE B.IPTIPODOC 
			WHEN 1 THEN 'Cédula de Ciudadanía' 
            WHEN 2 THEN 'Cédula de Extranjería'
            WHEN 3 THEN 'Tarjeta de Identidad'
            WHEN 4 THEN 'Registro Civil'
            WHEN 5 THEN 'Pasaporte'
            WHEN 6 THEN 'Adulto Sin Identificación'
            WHEN 7 THEN 'Menor Sin Identificación'
            WHEN 8 THEN 'Número único de identificación personal'
            WHEN 9 THEN 'Certificado de Nacido Vivo'
            WHEN 10 THEN 'Carnet Diplomático (Aplica para extranjeros)'
            WHEN 11 THEN 'Salvoconducto (Aplica para extranjeros)'
            WHEN 12 THEN 'Permiso especial de Permanencia (Aplica para extranjeros)'
         End  As 'TIPODOCUMENTO', 
         A.IPCODPACI, 
         B.IPNOMCOMP,
		 B.IPTELMOVI As 'MOVIL',
		 B.CORELEPAC As 'CORREO',
         C1.NOMENTIDA, 
                        A.CODPROSAL, 
                        D.NOMMEDICO, 
                        A.CODESPECI,
                        P.DESESPECI,
                        A.CODACTMED,
                        M.DESACTMED,
                        A.FECHORAIN,
                        A.FECHORAFI,
                        CASE A.CODESTCIT
                            WHEN 0 THEN 'Asignada' 
                            WHEN 1 THEN 'Cumplida'
                            WHEN 2 THEN 'Incumplida'
                            WHEN 3 THEN 'PreAsignada'
                            WHEN 4 THEN 'Cita Cancelada' 
                        End  As 'ESTADOCITA',
                        CASE A.MODALIDAD
                            WHEN 0 THEN 'Presencial' 
                         WHEN 1 THEN 'Teleconsulta'
                         End  As 'MODALIDAD', 
                          NULL As ORIGENQX, 
                        A.CODCENATE, 
                        C.NOMCENATE, 
                     CASE  	
                      WHEN A.IDSALA IS NULL AND A.CODIGOCON IS NOT NULL THEN RTRIM(UF2.UFUCODIGO)
                      WHEN A.IDSALA IS NOT NULL AND A.CODIGOCON IS NULL THEN RTRIM(UF1.UFUCODIGO)
                   ELSE 'none'
                      END  AS 'UFUCODIGO', 
                        CASE  	
                         WHEN A.IDSALA IS NULL AND A.CODIGOCON IS NOT NULL THEN RTRIM(UF2.UFUDESCRI)
                         WHEN A.IDSALA IS NOT NULL AND A.CODIGOCON IS NULL THEN RTRIM(UF1.UFUDESCRI)
                         ELSE 'none'
                         END  AS 'UFUDESCRI',
                    A.CODIGOCON,
                    G.DESCRICON, 
                    E.CODIGSALA,
                    E.DESCRIPSAL,
         CASE A.TIPTRATAMIENTO
			WHEN '1' THEN 'Quimioterapia'
            WHEN '2' THEN 'RadioTerapia'
            WHEN '3' THEN 'Diálisis'
            WHEN '4' THEN 'Braquiterapia'
         End  As 'TIPOTRATAMIENTO',
         NULL AS CUPS 
	FROM 
		dbo.AGASICITA As A WITH (NOLOCK) 
        INNER JOIN dbo.INPACIENT As B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI 
        INNER JOIN dbo.ADCENATEN As C WITH (NOLOCK) ON A.CODCENATE = C.CODCENATE 
        INNER JOIN dbo.INENTIDAD As C1 WITH (NOLOCK) ON C1.CODENTIDA = B.CODENTIDA 
        LEFT JOIN dbo.INPROFSAL As D WITH (NOLOCK) ON A.CODPROSAL = D.CODPROSAL 
        LEFT JOIN dbo.INESPECIA As P WITH (NOLOCK) ON A.CODESPECI = P.CODESPECI 
        LEFT JOIN dbo.AGENSALAC As E WITH (NOLOCK) ON A.IDSALA = E.CODCONCEC AND A.CODCENATE = E.CODCENATE 
        LEFT JOIN dbo.INUNIFUNC As UF1 WITH (NOLOCK) ON E.UFUCODIGO = UF1.UFUCODIGO 
        LEFT JOIN dbo.AGCONSULT As G WITH (NOLOCK) ON A.CODIGOCON = G.CODIGOCON AND A.CODCENATE = G.CODCENATE 
        LEFT JOIN dbo.INUNIFUNC As UF2 WITH (NOLOCK) ON G.UFUCODIGO = UF2.UFUCODIGO 
        LEFT JOIN dbo.AGACTIMED As M WITH (NOLOCK) ON A.CODACTMED = M.CODACTMED 
        LEFT JOIN Contract.CUPSEntity As CCE WITH (NOLOCK) ON A.CODSERIPS = CCE.CODE 
	WHERE
		Cast(A.FECHORAIN As Date) BETWEEN @FechaInicial AND @FechaFinal
		AND A.IPCODPACI = @CodigoPaciente
		AND B.IPTIPODOC = @TipoIdentificacion
		AND A.CODESTCIT = '0' 
		AND A.TIPSOLICITU IN(1,3)
UNION ALL
    -- Insert statements for procedure here
	SELECT 
		CASE A.TIPSOLICITU
		    WHEN '1' THEN 'Cita Medica'
			WHEN '2' THEN 'Cita Apoyo Diagnostico'
            WHEN '3' THEN 'Cita Tratamiento Especiales'
        End  As 'TIPOSOLICITUD', 
        CASE B.IPTIPODOC 
			WHEN 1 THEN 'Cédula de Ciudadanía' 
            WHEN 2 THEN 'Cédula de Extranjería'
            WHEN 3 THEN 'Tarjeta de Identidad'
            WHEN 4 THEN 'Registro Civil'
            WHEN 5 THEN 'Pasaporte'
            WHEN 6 THEN 'Adulto Sin Identificación'
            WHEN 7 THEN 'Menor Sin Identificación'
            WHEN 8 THEN 'Número único de identificación personal'
            WHEN 9 THEN 'Certificado de Nacido Vivo'
            WHEN 10 THEN 'Carnet Diplomático (Aplica para extranjeros)'
            WHEN 11 THEN 'Salvoconducto (Aplica para extranjeros)'
            WHEN 12 THEN 'Permiso especial de Permanencia (Aplica para extranjeros)'
         End  As 'TIPODOCUMENTO', 
         A.IPCODPACI, 
         B.IPNOMCOMP,
		 B.IPTELMOVI As 'MOVIL',
		 B.CORELEPAC As 'CORREO',		 
         C1.NOMENTIDA, 
                        A.CODPROSAL, 
                        D.NOMMEDICO, 
                        A.CODESPECI,
                        P.DESESPECI,
                        A.CODACTMED,
                        M.DESACTMED,
                        A.FECHORAIN,
                        A.FECHORAFI,
                        CASE A.CODESTCIT
                            WHEN 0 THEN 'Asignada' 
                            WHEN 1 THEN 'Cumplida'
                            WHEN 2 THEN 'Incumplida'
                            WHEN 3 THEN 'PreAsignada'
                            WHEN 4 THEN 'Cita Cancelada' 
                        End  As 'ESTADOCITA',
                        CASE A.MODALIDAD
                            WHEN 0 THEN 'Presencial' 
                         WHEN 1 THEN 'Teleconsulta'
                         End  As 'MODALIDAD', 
                          NULL As ORIGENQX, 
                        A.CODCENATE, 
                        C.NOMCENATE, 
                     CASE  	
                      WHEN A.IDSALA IS NULL AND A.CODIGOCON IS NOT NULL THEN RTRIM(UF2.UFUCODIGO)
                      WHEN A.IDSALA IS NOT NULL AND A.CODIGOCON IS NULL THEN RTRIM(UF1.UFUCODIGO)
                   ELSE 'none'
                      END  AS 'UFUCODIGO', 
                        CASE  	
                         WHEN A.IDSALA IS NULL AND A.CODIGOCON IS NOT NULL THEN RTRIM(UF2.UFUDESCRI)
                         WHEN A.IDSALA IS NOT NULL AND A.CODIGOCON IS NULL THEN RTRIM(UF1.UFUDESCRI)
                         ELSE 'none'
                         END  AS 'UFUDESCRI',
                    A.CODIGOCON,
                    G.DESCRICON, 
                    E.CODIGSALA,
                    E.DESCRIPSAL,
         CASE A.TIPTRATAMIENTO
			WHEN '1' THEN 'Quimioterapia'
            WHEN '2' THEN 'RadioTerapia'
            WHEN '3' THEN 'Diálisis'
            WHEN '4' THEN 'Braquiterapia'
         End  As 'TIPOTRATAMIENTO',
         CONTR.CUPS 
	FROM 
		@TablaControlCitaDX CONTR INNER JOIN AGASICITA As A WITH (NOLOCK) ON A.CODAUTONU= CONTR.IDCITA 
        INNER JOIN dbo.INPACIENT As B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI 
        INNER JOIN dbo.ADCENATEN As C WITH (NOLOCK) ON A.CODCENATE = C.CODCENATE 
        INNER JOIN dbo.INENTIDAD As C1 WITH (NOLOCK) ON C1.CODENTIDA = B.CODENTIDA 
        LEFT JOIN dbo.INPROFSAL As D WITH (NOLOCK) ON A.CODPROSAL = D.CODPROSAL 
        LEFT JOIN dbo.INESPECIA As P WITH (NOLOCK) ON A.CODESPECI = P.CODESPECI 
        LEFT JOIN dbo.AGENSALAC As E WITH (NOLOCK) ON A.IDSALA = E.CODCONCEC AND A.CODCENATE = E.CODCENATE 
        LEFT JOIN dbo.INUNIFUNC As UF1 WITH (NOLOCK) ON E.UFUCODIGO = UF1.UFUCODIGO 
        LEFT JOIN dbo.AGCONSULT As G WITH (NOLOCK) ON A.CODIGOCON = G.CODIGOCON AND A.CODCENATE = G.CODCENATE 
        LEFT JOIN dbo.INUNIFUNC As UF2 WITH (NOLOCK) ON G.UFUCODIGO = UF2.UFUCODIGO 
        LEFT JOIN dbo.AGACTIMED As M WITH (NOLOCK) ON A.CODACTMED = M.CODACTMED 
        LEFT JOIN Contract.CUPSEntity As CCE WITH (NOLOCK) ON A.CODSERIPS = CCE.CODE 
        LEFT JOIN Contract.ContractDescriptions As CD WITH (NOLOCK) ON CD.Id = A.IDDESCRIPCIONRELACIONADA 
	UNION ALL	
	SELECT DISTINCT
		'Cirugía'  As 'TIPOSOLICITUD', 

		CASE B.IPTIPODOC 
			WHEN 1 THEN 'Cédula de Ciudadanía' 
			WHEN 2 THEN 'Cédula de Extranjería'
			WHEN 3 THEN 'Tarjeta de Identidad'
			WHEN 4 THEN 'Registro Civil'
			WHEN 5 THEN 'Pasaporte'
			WHEN 6 THEN 'Adulto Sin Identificación'
			WHEN 7 THEN 'Menor Sin Identificación'
			WHEN 8 THEN 'Número único de identificación personal'
			WHEN 9 THEN 'Certificado de Nacido Vivo'
			WHEN 10 THEN 'Carnet Diplomático (Aplica para extranjeros)'
			WHEN 11 THEN 'Salvoconducto (Aplica para extranjeros)'
			WHEN 12 THEN 'Permiso especial de Permanencia (Aplica para extranjeros)'
		End  As 'TIPODOCUMENTO', 

		X.IPCODPACI, 
		B.IPNOMCOMP, 
		B.IPTELMOVI As 'MOVIL',
		B.CORELEPAC As 'CORREO',
		C1.NOMENTIDA,
		X.CODPROSAL, 
		D.NOMMEDICO, 
		X.CODESPECI,
		P.DESESPECI,
		NULL AS 'CODACTEMED',
		NULL AS 'DESACTMED',
	    X.FECHORAIN,
		X.FECHORAFI,
		CASE X.CODESTPQX
		WHEN 0 THEN 'Programada' 
		WHEN 1 THEN 'Paciente admitido (Cirugia Origen Ambulatoria)'
		WHEN 2 THEN 'Paciente en sala de espera'
		WHEN 3 THEN 'Paciente en sala quirurgica'
		WHEN 4 THEN 'Paciente en recuperación'
		WHEN 5 THEN 'Paciente con alta' 
		WHEN 6 THEN 'Anulado - Cancelada' 
		End  As 'ESTADOCITA',
	    NULL AS 'MODALIDAD',
		CASE X.ORIGENQX
		WHEN 1 THEN 'Ambulatoria'
		WHEN 2 THEN 'Hospitalaria'
		End  As 'ORIGENQX', 

		X.CODCENATE, 
		C.NOMCENATE,
		CASE  	
			WHEN UF1.UFUCODIGO IS NULL  THEN ' No Aplica '
			WHEN UF1.UFUCODIGO IS NOT NULL THEN RTRIM(UF1.UFUCODIGO)
			ELSE 'none'
		END  AS 'UFUCODIGO',

		CASE  	
			WHEN UF1.UFUCODIGO IS NULL THEN ' No Aplica '
			WHEN UF1.UFUCODIGO IS NOT NULL  THEN RTRIM(UF1.UFUDESCRI)
			ELSE 'none'
		END  AS 'UFUDESCRI',
		NULL AS CODIGOCON,
		NULL AS DESCRICON,
		E.CODIGSALA,
		E.DESCRIPSAL,
		NULL AS TIPTRATAMIENTO,	
		RTRIM(LTRIM(Y.CODSERIPS)) + ' - ' + RTRIM(LTRIM(Y.DESSERIPS)) As CUPS
	FROM dbo.AGEPROGQX As X WITH (NOLOCK)
	INNER JOIN dbo.INPACIENT As B WITH (NOLOCK) ON X.IPCODPACI = B.IPCODPACI 
	INNER JOIN dbo.ADCENATEN As C WITH (NOLOCK) ON X.CODCENATE = C.CODCENATE
	INNER JOIN dbo.AGENSALAC As E WITH (NOLOCK) ON X.AGENSALAC = E.CODCONCEC AND X.CODCENATE = E.CODCENATE 
	INNER JOIN dbo.INPROFSAL As D WITH (NOLOCK) ON X.CODPROSAL = D.CODPROSAL 
	INNER JOIN INCUPSIPS AS Y WITH (NOLOCK) ON X.CODSERIPS = Y.CODSERIPS
    LEFT JOIN dbo.INENTIDAD As C1 WITH (NOLOCK) ON C1.CODENTIDA = B.CODENTIDA 
	LEFT JOIN dbo.INESPECIA As P WITH (NOLOCK) ON X.CODESPECI = P.CODESPECI 
	LEFT JOIN dbo.INUNIFUNC As UF1 WITH (NOLOCK) ON E.UFUCODIGO = UF1.UFUCODIGO
	WHERE 
		Cast(X.FECHORAIN As Date) BETWEEN @FechaInicial AND @FechaFinal 
		AND X.IPCODPACI = @CodigoPaciente AND 
		B.IPTIPODOC = @TipoIdentificacion AND
		X.CODESTPQX = '0' 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las citas médicas programadas de un paciente específico dentro de un rango de fechas, filtrando por código de paciente y tipo de documento de identificación (cédula de ciudadanía, tarjeta de identidad, pasaporte, etc.). Integra información de agendamiento (AGASICITA), datos del paciente (INPACIENT), centro de atención, profesional de salud, especialidad, consultorio, sala de procedimientos y servicios CUPS asociados a cada cita. Retorna el tipo de solicitud (cita médica, apoyo diagnóstico o tratamiento especial), la modalidad (presencial o teleconsulta), el estado de la cita (asignada, cumplida, incumplida, cancelada), los datos del paciente y los servicios CUPS agrupados por cita. Se usa como interfaz o servicio de consulta de agenda para portales, aplicaciones móviles o integraciones externas que necesiten mostrar el historial o próximas citas de un paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_eHC_InterfazConsultaCita';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_eHC_InterfazConsultaCita';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta unificada de citas (médicas, apoyo diagnóstico, tratamientos especiales y cirugías programadas) de un paciente en un rango de fechas, decodificando catálogos a texto legible para una interfaz externa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_InterfazConsultaCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT con el tipo de identificación indicado; Las citas deben estar dentro del rango de fechas y pertenecer al paciente solicitado; Para citas de agenda solo se consideran las que tienen estado ''0'' (Asignada); Para cirugías solo se consideran las que tienen estado ''0'' (Programada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_InterfazConsultaCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen citas activas: agenda con CODESTCIT=''0'' y cirugías con CODESTPQX=''0''; Filtra siempre por paciente y tipo de documento del solicitante; Las citas de apoyo diagnóstico se consolidan por franja horaria con todos sus CUPS concatenados, evitando duplicar la cita por cada servicio; ORIGENQX solo se reporta para registros de cirugía; en citas de agenda siempre es NULL; MODALIDAD y TIPTRATAMIENTO no aplican a cirugías (NULL); Los catálogos de tipo de documento, estado de cita, modalidad, tipo de solicitud y tipo de tratamiento se traducen a texto fijo embebido en el SP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_InterfazConsultaCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Cita de apoyo diagnóstico; Cita de tratamiento especial (Quimioterapia, Radioterapia, Diálisis, Braquiterapia); Cirugía programada (ambulatoria/hospitalaria); Paciente; Tipo de identificación; Profesional de salud; Especialidad médica; Actividad médica; Centro de atención; Unidad funcional; Sala/Consultorio; Entidad/Aseguradora; CUPS; Modalidad presencial/teleconsulta; Estado de cita; Estado quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_InterfazConsultaCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TablaControlCitaDX: Cuando existen citas de apoyo diagnóstico (TIPSOLICITU=2) Asignadas (CODESTCIT=''0'') del paciente en el rango, se inserta una fila por combinación de paciente y franja horaria con la lista concatenada de CUPS asociados; [RETURN_RESULT] RESULTSET: Devuelve la unión de: (a) citas de AGASICITA con TIPSOLICITU IN (1,3) y CODESTCIT=''0''; (b) citas de apoyo diagnóstico (TIPSOLICITU=2) agrupadas con sus CUPS concatenados desde la tabla temporal; (c) cirugías programadas de AGEPROGQX con CODESTPQX=''0''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_InterfazConsultaCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPSOLICITU = 1 → Etiqueta como ''Cita Medica''; si TIPSOLICITU = 2 → Etiqueta como ''Cita Apoyo Diagnostico'' y se agrupan con CUPS concatenados desde tabla temporal; si TIPSOLICITU = 3 → Etiqueta como ''Cita Tratamiento Especiales''; si IDSALA IS NULL AND CODIGOCON IS NOT NULL → Toma la unidad funcional desde el consultorio (UF2) else Si IDSALA IS NOT NULL AND CODIGOCON IS NULL toma la unidad funcional desde la sala (UF1); en otro caso ''none''; si Registro proviene de AGEPROGQX (cirugía) → Decodifica CODESTPQX (0=Programada,1=Admitido,2=Sala espera,3=Sala quirúrgica,4=Recuperación,5=Alta,6=Anulado) y ORIGENQX (1=Ambulatoria,2=Hospitalaria); si MODALIDAD = 0/1 → Traduce a ''Presencial'' o ''Teleconsulta''; si TIPTRATAMIENTO 1-4 → Traduce a Quimioterapia, Radioterapia, Diálisis o Braquiterapia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_InterfazConsultaCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INPACIENT; dbo.ADCENATEN; dbo.INENTIDAD; dbo.INPROFSAL; dbo.INESPECIA; dbo.AGENSALAC; dbo.INUNIFUNC; dbo.AGCONSULT; dbo.AGACTIMED; dbo.INCUPSIPS; dbo.AGEPROGQX; Contract.CUPSEntity; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_InterfazConsultaCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_InterfazConsultaCita';
-- GO

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
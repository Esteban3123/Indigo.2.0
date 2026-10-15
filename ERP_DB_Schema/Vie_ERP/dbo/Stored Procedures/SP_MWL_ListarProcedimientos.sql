    -- =============================================
    -- Author:		Emanuel Olaya Penagos
    -- Create date: 09/01/2020
    -- Description: sp para listar en el Modality WorkList(MWL)
    -- =============================================
    CREATE PROCEDURE [dbo].[SP_MWL_ListarProcedimientos]
		@attentionCenter VARCHAR(100),
	    @ScheduledStationAeTitle VARCHAR(16),
		@Modality VARCHAR(16),
		@PatientsName VARCHAR(64),
		@PatientId VARCHAR(25),
		@ScheduledProcedureStepStartDateS VARCHAR(30),
		@ScheduledProcedureStepStartDateE VARCHAR(30)
AS BEGIN
SET NOCOUNT ON;
SELECT
	CONCAT(rtrim(ltrim(A.NUMINGRES)), '-', rtrim(ltrim(A.AUTO))) as numero_acceso,
    A.AUTO AS id_paso_procedimiento,
    A.NUMINGRES AS numero_solicitud,
    A.CODSERIPS as cups,
    A.FECORDMED as fecha_hora_solicitud,
    -- P.TIPMODALI as modalidad,
	RG.ABREVIACION as modalidad,
    S.DESSERIPS as nombre_cups,
    --VIEFU.UFUCODIGO AS codigo_interno_servicio,
    rtrim(ltrim(IP.IPCODPACI)) AS documento,
    rtrim(ltrim(IP.IPPRINOMB)) AS primer_nombre,
    rtrim(ltrim(IP.IPSEGNOMB)) AS segundo_nombre,
    rtrim(ltrim(IP.IPPRIAPEL)) AS primer_apellido,
    rtrim(ltrim(IP.IPSEGAPEL)) AS segundo_apellido,
    CASE
        IP.IPSEXOPAC
        WHEN 1 THEN 'M'
        WHEN 2 THEN 'F'
    END AS sexo,
    IP.IPFECNACI AS Fecha_nacimiento,
    PRO.NOMMEDICO as nombre_medico,
    'Rutinario' as prioridad,
	'UNKNOWN' as aet,
    'AMBULATORIO' as via_ingreso,
	A.CODCENATE as centro_atencion
FROM
    AMBORDIMA A
    INNER JOIN INPACIENT IP ON IP.IPCODPACI = A.IPCODPACI
    INNER JOIN INCUPSIPS S ON S.CODSERIPS = A.CODSERIPS
    INNER JOIN INPROFSAL PRO ON PRO.CODPROSAL = A.CODPROSAL
    INNER JOIN INCUPSSUB CS with(nolock) on CS.CODGRUSUB=S.CODGRUSUB
	INNER JOIN RISGRIMAGE RG with(nolock) on RG.ID = CS.IDRISGRIMAGE
WHERE
    A.ESTSERIPS = '1'
	AND A.CODCENATE in (select value from [dbo].[SplitString](@attentionCenter))
	--AND cast (A.FECORDMED as Date) = cast('20180826' as Date) -- DEMO PURPORSES
	--AND RG.ABREVIACION = 'CR'
	--AND (@Modality IS NULL OR RG.ABREVIACION = @Modality)
	--AND (@PatientsName IS NULL OR IP.IPNOMCOMP like @PatientsName)
	--AND (@PatientId IS NULL OR rtrim(ltrim(IP.IPCODPACI)) = @PatientId)
	--AND (@ScheduledProcedureStepStartDateS IS NULL OR A.FECORDMED >= @ScheduledProcedureStepStartDateS)
	--AND (@ScheduledProcedureStepStartDateE IS NULL OR A.FECORDMED < @ScheduledProcedureStepStartDateE)

UNION
ALL
SELECT
	CONCAT(rtrim(ltrim(A.NUMINGRES)), '-', rtrim(ltrim(A.AUTO))) as numero_acceso,
    A.AUTO AS id_paso_procedimiento,
    A.NUMINGRES AS numero_solicitud,
    A.CODSERIPS as cups,
    A.FECORDMED as fecha_hora_solicitud,
    -- P.TIPMODALI as modalidad,
	RG.ABREVIACION as modalidad,
    S.DESSERIPS as nombre_cups,
    rtrim(ltrim(IP.IPCODPACI)) AS documento,
    rtrim(ltrim(IP.IPPRINOMB)) AS primer_nombre,
    rtrim(ltrim(IP.IPSEGNOMB)) AS segundo_nombre,
    rtrim(ltrim(IP.IPPRIAPEL)) AS primer_apellido,
    rtrim(ltrim(IP.IPSEGAPEL)) AS segundo_apellido,
    CASE
        IP.IPSEXOPAC
        WHEN 1 THEN 'M'
        WHEN 2 THEN 'F'
    END AS sexo,
    IPFECNACI AS Fecha_nacimiento,
    PRO.NOMMEDICO as nombre_medico,
    CASE
		A.PRISERIPS
		when '1' then 'Urgente'
		when '2' then 'Rutinario'
		else 'Rutinario'
	END AS prioridad,
	'UNKNOWN' as aet,
    'HOSPITALARIO' as via_ingreso,
	A.CODCENATE as centro_atencion
FROM
    HCORDIMAG A
    INNER JOIN INPACIENT IP ON IP.IPCODPACI = A.IPCODPACI
    INNER JOIN INCUPSIPS S ON S.CODSERIPS = A.CODSERIPS
    INNER JOIN INPROFSAL PRO ON PRO.CODPROSAL = A.CODPROSAL
	INNER JOIN INCUPSSUB CS with(nolock) on CS.CODGRUSUB=S.CODGRUSUB
	INNER JOIN RISGRIMAGE RG with(nolock) on RG.ID = CS.IDRISGRIMAGE
WHERE
    A.ESTSERIPS = '1'
	AND A.CODCENATE in (select value from [dbo].[SplitString](@attentionCenter))
	--AND cast (A.FECORDMED as Date) = cast('20180826' as Date) -- DEMO PURPORSES
	--AND RG.ABREVIACION = 'CR'
	--AND (@Modality IS NULL OR RG.ABREVIACION = @Modality)
	--AND (@PatientsName IS NULL OR IP.IPNOMCOMP like @PatientsName)
	--AND (@PatientId IS NULL OR rtrim(ltrim(IP.IPCODPACI)) = @PatientId)
	--AND (@ScheduledProcedureStepStartDateS IS NULL OR A.FECORDMED >= @ScheduledProcedureStepStartDateS)
	--AND (@ScheduledProcedureStepStartDateE IS NULL OR A.FECORDMED < @ScheduledProcedureStepStartDateE) 

END


-- exec [dbo].[SP_MWL_ListarProcedimientos] '0545', null, null, null, null, null, null

-- select cast('20180328' as Date)
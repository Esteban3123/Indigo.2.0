CREATE PROCEDURE [dbo].[SPHC_ListarPacientesGestionLogisticaFarmaceutica]
(
@CentroAtencion Char(10),
@UnidadFuncional varchar(250)
)
AS
BEGIN
	SET NOCOUNT ON;

DECLARE @UnidadFuncionalAux as varchar(250) = RTRIM(@UnidadFuncional)
	if @UnidadFuncionalAux IS NOT NULL AND LEN(@UnidadFuncionalAux) = 0 SET @UnidadFuncionalAux = NULL;

WITH CTE_OrdenesMedicamentos
AS
(
	SELECT A.IPCODPACI,  A.NUMINGRES, A.CODPRODUC    
	from
	HCFARMEPD AS A 
		INNER JOIN HCFARMEPC AS B ON A.CODCONCEC = B.CODCONCEC AND A.SENDTO IN ('0') AND A.VIEPROCESSED = 0 AND PROESTADO = 1
		INNER JOIN HCPRESCRA AS C ON A.IdSourceTable = C.ID AND A.SourceTable ='HCPRESCRA' 
	WHERE 
	B.ORDESTADO = 1
	AND B.CODCENATE = @CentroAtencion	
	AND C.PREESTADO IN (1,6)
	AND (@UnidadFuncionalAux IS NULL OR (@UnidadFuncionalAux IS NOT NULL AND RTRIM(B.UFUCODIGO) in (select * from  [dbo].[SplitString](@UnidadFuncionalAux))))
	group by A.IPCODPACI , A.NUMINGRES , A.CODPRODUC 
UNION
	SELECT A.IPCODPACI,  A.NUMINGRES , A.CODPRODUC  
	from
	HCFARMEPD AS A 
		INNER JOIN  HCFARMEPC AS B ON A.CODCONCEC = B.CODCONCEC AND A.SENDTO IN ('0') AND A.VIEPROCESSED = 0 AND PROESTADO = 1
		INNER JOIN HCINFLIQA AS C ON A.IdSourceTable = C.CONSECUTI AND A.SourceTable ='HCINFLIQA' 
		INNER JOIN HCINFCONC AS CONC ON CONC.CODCONCEC = C.CODCONCEC 
	WHERE 
	B.ORDESTADO = 1
	AND B.CODCENATE = @CentroAtencion	
	AND C.PREESTADO IN (1,5)
	AND (@UnidadFuncionalAux IS NULL OR (@UnidadFuncionalAux IS NOT NULL AND RTRIM(B.UFUCODIGO) in (select * from  [dbo].[SplitString](@UnidadFuncionalAux))))
	group by A.IPCODPACI , A.NUMINGRES , A.CODPRODUC 
--UNION
--	SELECT A.IPCODPACI,  A.NUMINGRES , A.CODPRODUC  
--	from
--	HCFARMEPD AS A 
--		INNER JOIN HCFARMEPC AS B ON A.CODCONCEC = B.CODCONCEC AND A.SENDTO IN (0,2) AND A.VIEPROCESSED = 0 
--		INNER JOIN HCNUTPAREC AS C ON A.IdSourceTable = C.ID AND A.SourceTable ='HCNUTPAREC' 
--	WHERE 
--	B.ORDESTADO = 1
--	AND B.CODCENATE = @CentroAtencion	
--	AND C.STATUS IN (1)
--	AND (@UnidadFuncionalAux IS NULL OR (@UnidadFuncionalAux IS NOT NULL AND RTRIM(B.UFUCODIGO) = @UnidadFuncionalAux))
--	group by A.IPCODPACI , A.NUMINGRES , A.CODPRODUC 
)

--para listar los medicamentos
SELECT 
FAR.IPCODPACI AS 'Identificacion',
FAR.NUMINGRES AS 'Ingreso',
ING.IFECHAING as 'FechaIngreso',
RTRIM(PAC.IPNOMCOMP) AS 'Paciente',
PAC.IPFECNACI AS 'FechaNacimiento', 
CAST('' AS CHAR(50)) AS 'Edad',
RTRIM(DIAG.CODDIAGNO) + ' - ' + RTRIM(DIAG.NOMDIAGNO) as 'Diagnostico',
CAMA.CODICAMAS AS 'CodigoCama',
RTRIM(CAMA.DESCCAMAS) AS 'Cama',
FAR.UFUCODIGO, RTRIM(UFU.UFUDESCRI) AS 'UnidadFuncional',
ING.CODCENATE, 
RTRIM(ATE.NOMCENATE) AS 'CentroAtencion',
MAX(FAR.FECHAORDE) As 'FechaOrden'
FROM HCFARMEPC FAR
		INNER JOIN HCFARMEPD B WITH (NOLOCK) ON FAR.CODCONCEC = B.CODCONCEC AND B.SENDTO IN (0) AND B.VIEPROCESSED = 0
		INNER JOIN INPACIENT PAC WITH (NOLOCK) ON FAR.IPCODPACI = PAC.IPCODPACI
		INNER JOIN ADINGRESO ING WITH (NOLOCK) ON ING.NUMINGRES = FAR.NUMINGRES
		INNER JOIN ADCENATEN ATE WITH (NOLOCK) ON ING.CODCENATE = ATE.CODCENATE
		INNER JOIN INUNIFUNC UFU WITH (NOLOCK) ON UFU.UFUCODIGO = FAR.UFUCODIGO
		INNER JOIN INDIAGNOS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = FAR.IPCODPACI AND NUMINGRES = FAR.NUMINGRES AND CODDIAPRI = 1)  
		INNER JOIN CHCAMASHO CAMA WITH (NOLOCK) ON CAMA.CODICAMAS = ING.CODCAMACT
		INNER JOIN (SELECT IPCODPACI FROM CTE_OrdenesMedicamentos) as Ordenes ON FAR.IPCODPACI = Ordenes.IPCODPACI 
WHERE 
FAR.ORDESTADO = 1
AND FAR.CODCENATE = @CentroAtencion	
AND (@UnidadFuncionalAux IS NULL OR (@UnidadFuncionalAux IS NOT NULL AND RTRIM(FAR.UFUCODIGO) in (select * from  [dbo].[SplitString](@UnidadFuncionalAux))))
group by FAR.IPCODPACI, FAR.NUMINGRES, ING.IFECHAING, PAC.IPNOMCOMP, PAC.IPFECNACI, DIAG.CODDIAGNO, DIAG.NOMDIAGNO, CAMA.CODICAMAS, CAMA.DESCCAMAS, FAR.UFUCODIGO, UFU.UFUDESCRI, ING.CODCENATE, ATE.NOMCENATE
ORDER BY FAR.IPCODPACI ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que tienen órdenes de medicamentos pendientes de despacho farmacéutico, filtrando por centro de atención y opcionalmente por una o varias unidades funcionales (por ejemplo, pisos o servicios de hospitalización). Combina órdenes provenientes de prescripciones médicas regulares (HCPRESCRA) y de infusiones o mezclas magistrales (HCINFLIQA/HCINFCONC), considerando solo órdenes activas que aún no han sido procesadas ni enviadas a bodega. Para cada paciente retorna información clave de la atención: cédula, número de ingreso, fecha de ingreso, nombre completo, fecha de nacimiento, diagnóstico principal (CIE-10), cama asignada, unidad funcional, centro de atención y fecha de la última orden de medicamento. Se usa en la gestión logística de farmacia para que los farmacéuticos identifiquen qué pacientes hospitalizados tienen medicamentos pendientes de preparación o despacho.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con órdenes de medicamentos pendientes de envío a logística farmacéutica, filtrados por centro de atención y opcionalmente por una o varias unidades funcionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe corresponder a un valor existente en HCFARMEPC.CODCENATE.; La cadena de unidades funcionales puede venir nula o vacía; si está vacía se trata como nula y no filtra.; Cada paciente debe tener un diagnóstico principal (CODDIAPRI=1) en INDIAGNOP para aparecer en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes con cabecera activa (ORDESTADO=1) y detalle no procesado para envío (SENDTO=''0'', VIEPROCESSED=0, PROESTADO=1).; Solo se consideran pacientes del centro de atención solicitado.; Para cada paciente se toma un único diagnóstico, el marcado como principal (CODDIAPRI=1).; La fecha de orden reportada es la más reciente (MAX(FECHAORDE)) por paciente/ingreso/unidad funcional.; El procedimiento es de solo lectura: no realiza modificaciones de datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Orden de medicamentos; Prescripción; Unidad funcional; Centro de atención; Diagnóstico principal; Cama hospitalaria; Logística farmacéutica; Liquidación (info de liquidación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un conjunto de pacientes con órdenes de medicamentos activas (ORDESTADO=1, SENDTO=0, VIEPROCESSED=0, PROESTADO=1) cuya prescripción asociada está en estados válidos (HCPRESCRA.PREESTADO IN (1,6) o HCINFLIQA.PREESTADO IN (1,5)) en el centro de atención indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @UnidadFuncional viene nulo o con longitud cero tras RTRIM → No se aplica filtro por unidad funcional y se incluyen todas las del centro else Se filtra por las unidades funcionales contenidas en la lista delimitada usando dbo.SplitString; si Origen de la orden farmacéutica es HCPRESCRA → La prescripción debe estar en estado 1 o 6 (PREESTADO) else Si origen es HCINFLIQA, la prescripción debe estar en estado 1 o 5', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.HCPRESCRA; dbo.HCINFLIQA; dbo.HCINFCONC; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INDIAGNOS; dbo.INDIAGNOP; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesGestionLogisticaFarmaceutica';
-- GO

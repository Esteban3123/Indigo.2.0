CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MezclasyLiquidos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT 
	CODCONCEC AS CONSECUTIVO,
	RTRIM(MEZLIQPAC)AS 'MEZCLAS Y LIQUIDOS',
	RTRIM(ADMMEZLIQ) AS 'ADMINISTRACION MEZCLAS Y LIQUIDOS',
	ISNULL(RTRIM(INDAPLMED),'--No Refiere--') AS 'INDICACIONES DE APLICACION',
	CASE WHEN FOLIOINIC=NUMEFOLIO THEN 'N' WHEN TRATMODIF='1' THEN 'M' ELSE '' END AS LEYENDA,
	CODCENATE,UFUCODIGO,IPCODPACI,CANPROCAL AS 'CANTIDAD',
	CASE WHEN RTRIM(DURACIINFM) IS NULL THEN 'No aplica' ELSE RTRIM(DURACIINFM) END AS  'DURACION',
	(CASE WHEN CANCCHORA IS NOT NULL AND CANCCHORA <> 0 THEN RTRIM(CAST(CANCCHORA AS varchar)) + ' CC/Hora' ELSE NULL END) AS 'VELOCIDAD INFUSION',
	ISNULL(FECINIINF, FECINIINFM) AS 'FECHA INICIO'
FROM 
	HCINFLIQD  with(noLock) 
WHERE 
	IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio AND IDETIPHIS<>'CODIGOAZU'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de mezclas y líquidos registrados en la historia clínica de un paciente hospitalizado. Dado el código del paciente, el número de ingreso y el número de folio, consulta la tabla de infusiones y líquidos (HCINFLIQD) para devolver el detalle de cada mezcla o líquido indicado: descripción de la mezcla, instrucciones de administración, indicaciones de aplicación, cantidad, duración de la infusión y una leyenda que indica si el registro es nuevo (N) o fue modificado (M). Se usa para imprimir o visualizar las órdenes de infusión/mezclas dentro de la historia clínica del paciente durante su hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera las mezclas y líquidos prescritos a un paciente en un folio e ingreso específicos, indicando si la orden es nueva o modificada respecto al folio inicial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y folio deben existir en HCINFLIQD; Se excluyen registros con IDETIPHIS=''CODIGOAZU''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna registros tipificados como ''CODIGOAZU''; Diferencia entre tratamiento nuevo (N), modificado (M) o sin marca según folio inicial y bandera de modificación; Sustituye nulos en indicaciones y duración por textos por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Folio de historia clínica; Mezclas y líquidos; Administración de mezclas y líquidos; Indicaciones de aplicación; Tratamiento modificado; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCINFLIQD: Devuelve mezclas y líquidos cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros e IDETIPHIS<>''CODIGOAZU''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FOLIOINIC = NUMEFOLIO → Marca LEYENDA=''N'' (nueva, folio inicial igual al actual) else Si TRATMODIF=''1'' marca LEYENDA=''M'' (modificada); en otro caso LEYENDA queda en blanco; si DURACIINFM IS NULL → Devuelve DURACION=''No aplica'' else Devuelve el valor de DURACIINFM; si INDAPLMED IS NULL → Devuelve INDICACIONES DE APLICACION=''--No Refiere--'' else Devuelve el valor real de INDAPLMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidos';
-- GO

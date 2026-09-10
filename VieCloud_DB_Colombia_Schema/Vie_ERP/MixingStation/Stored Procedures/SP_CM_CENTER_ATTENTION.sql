-- =============================================
-- Author:		HECTOR RODRIGUEZ
-- Create date: 25-09-2019
-- Description:	Consulta centros de atencion de una central de mezcla
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_CM_CENTER_ATTENTION]
	@IdMixingStation	int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT DISTINCT
	CodeCenterAttention = RTRIM(CMCA.CodeCenterAttention)
	, NameCenterAttention = RTRIM(CA.NOMCENATE)
	, Ubicacion = CASE WHEN C.Code IS NULL THEN CA.DEPMUNCOD ELSE C.Code + ' - ' + C.Name END
	FROM MixingStation.CMCenterAttention CMCA WITH(NOLOCK)
	INNER JOIN dbo.ADCENATEN CA WITH(NOLOCK)
	ON CA.CODCENATE = CMCA.CodeCenterAttention
	LEFT OUTER JOIN Common.City C WITH(NOLOCK)
	ON C.Code = CA.DEPMUNCOD
	WHERE CMCA.IdMixingStation = @IdMixingStation

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los centros de atención (sedes, clínicas u hospitales) asociados a una central de mezcla específica, identificada por su ID. Combina la tabla de asignaciones entre centrales de mezcla y centros de atención con el catálogo maestro de centros de atención (ADCENATEN) para obtener el nombre de cada sede, y lo enriquece con el catálogo de municipios o ciudades para mostrar la ubicación geográfica legible. Se utiliza para saber qué centros de atención abastece o atiende una central de mezcla determinada dentro del sistema de farmacia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_CM_CENTER_ATTENTION';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_CM_CENTER_ATTENTION';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los centros de atención asociados a una central de mezcla, mostrando su código, nombre y ubicación resuelta contra el catálogo de ciudades cuando aplica.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CM_CENTER_ATTENTION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en MixingStation.CMCenterAttention para la central de mezcla solicitada para obtener resultados.; Los centros referenciados en CMCenterAttention deben existir en dbo.ADCENATEN (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CM_CENTER_ATTENTION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan centros de atención asociados a la central de mezcla indicada (vínculo obligatorio en CMCenterAttention vía INNER JOIN con ADCENATEN).; Los códigos y nombres se devuelven sin espacios finales (RTRIM).; El resultado es DISTINCT: no hay duplicados de centro de atención.; La relación con la ciudad es opcional (LEFT JOIN): un centro sin ciudad catalogada igual aparece en el resultado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CM_CENTER_ATTENTION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'central de mezcla; centro de atención; ubicación geográfica; municipio/ciudad', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CM_CENTER_ATTENTION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.CMCenterAttention: Retorna conjunto con CodeCenterAttention, NameCenterAttention y Ubicacion para los centros vinculados a la central de mezcla recibida (CMCA.IdMixingStation = @IdMixingStation).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CM_CENTER_ATTENTION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.Code IS NULL (no hay coincidencia en el catálogo de ciudades para DEPMUNCOD) → Devuelve la ubicación como el código DEPMUNCOD del centro de atención else Devuelve la ubicación concatenando ''Code - Name'' de la ciudad', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CM_CENTER_ATTENTION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CMCenterAttention; dbo.ADCENATEN; Common.City', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CM_CENTER_ATTENTION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CM_CENTER_ATTENTION';
-- GO

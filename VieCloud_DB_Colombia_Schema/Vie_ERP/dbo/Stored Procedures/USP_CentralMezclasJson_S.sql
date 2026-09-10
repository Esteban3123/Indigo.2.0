
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CentralMezclasJson_S]
	-- Add the parameters for the stored procedure here
	--@idUsuario_IN INT,
	@json_OUT NVARCHAR(MAX) OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE	@mensaje_OUT NVARCHAR(MAX), @error_OUT NVARCHAR(MAX), @codigo_OUT NVARCHAR(2);
	SET @codigo_OUT = '00';
	SET @mensaje_OUT = 'OK';
	--DECLARE @codigoInscripcion NVARCHAR(255);

	--SET @codigoInscripcion = CONCAT('%',@idDocumento_IN,'%');

	BEGIN TRY

	SET @json_OUT =(
		SELECT @codigo_OUT				AS 'respuesta.codigo',
			   @mensaje_OUT				AS 'respuesta.mensaje',
		(SELECT COUNT(cpd.CampaignNumber) as Repetido,     --DISTINCT
				CONCAT('Campaña ' ,  cpd.CampaignNumber) AS 'Item',
				 rmsd.PackageId AS 'Paquete',
				rmsd.PackagePersonalizedId 'PaquetePersonalizado'
				FROM MixingStation.CampaignDetail AS cpd
				INNER JOIN MixingStation.UnitDoseType AS udt ON cpd.UnitDoseTypeId = udt.Id
				INNER JOIN MixingStation.RequestMixingStationDetail AS rmsd ON cpd.Id = rmsd.CampaignDetailId
				INNER JOIN MixingStation.Package AS pck ON cpd.Id = rmsd.CampaignDetailId
				INNER JOIN MixingStation.PackagePersonalized AS p ON cpd.Id = rmsd.CampaignDetailId

				GROUP BY  cpd.CampaignNumber, rmsd.CampaignDetailId, rmsd.PackageId, rmsd.PackagePersonalizedId
				ORDER BY cpd.CampaignNumber ASC
		FOR JSON PATH, INCLUDE_NULL_VALUES) AS 'mensaje'
		FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER)

	  END TRY
	BEGIN CATCH  

				-------------- set de la respuesta del procedimiento ------------------------------------
				SET @codigo_OUT = '01';
				SET @mensaje_OUT = (SELECT CONCAT('ERROR_NUMBER ',ERROR_NUMBER(), ' ERROR_MESSAGE ',ERROR_MESSAGE(),' ERROR_LINE ', ERROR_LINE() ));
				SET @error_OUT = 'ERROR'

				SET @json_OUT =(
						SELECT @codigo_OUT				AS 'respuesta.codigo',
							   @error_OUT				AS 'respuesta.mensaje',
						(SELECT @mensaje_OUT AS 'mensaje.error'
						FOR JSON PATH, INCLUDE_NULL_VALUES) AS 'mensaje'
						FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER)
	END CATCH;
END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que consulta la estación de mezclas farmacéuticas y retorna un JSON con el conteo de apariciones por número de campaña, agrupando los detalles de solicitud junto con su paquete estándar y paquete personalizado asociado. Sirve como endpoint de consulta general (sin filtros de usuario o fecha) para listar campañas activas con sus preparaciones correspondientes. El resultado se envuelve en una estructura de respuesta con código y mensaje de estado, incluyendo manejo de errores en formato JSON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve en formato JSON el listado agrupado de campañas de la estación de mezclas con sus paquetes y paquetes personalizados asociados, encapsulando la respuesta con código y mensaje de éxito o error.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir relaciones consistentes entre CampaignDetail, RequestMixingStationDetail, Package y PackagePersonalized mediante CampaignDetailId para que aparezcan filas en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La respuesta siempre se entrega como JSON con estructura {respuesta.codigo, respuesta.mensaje, mensaje}.; El código ''00'' indica éxito y ''01'' indica error controlado.; Los resultados se ordenan ascendentemente por número de campaña.; Cada fila representa una agrupación única por CampaignNumber + CampaignDetailId + PackageId + PackagePersonalizedId, contando repeticiones en ''Repetido''.; Nunca se modifican datos; el procedimiento es de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de mezclas; Estación de mezclas (farmacia); Tipo de dosis unitaria; Paquete farmacéutico; Paquete personalizado (mezcla magistral); Solicitud a estación de mezclas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando la consulta se ejecuta sin error, retorna JSON con respuesta.codigo=''00'', respuesta.mensaje=''OK'' y un arreglo de campañas agrupadas por CampaignNumber, CampaignDetailId, PackageId y PackagePersonalizedId.; [RETURN_RESULT] N/A: Cuando se captura una excepción en TRY/CATCH, retorna JSON con respuesta.codigo=''01'', mensaje=''ERROR'' y detalle con ERROR_NUMBER, ERROR_MESSAGE y ERROR_LINE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ejecución exitosa de la consulta principal (TRY) → Construye JSON de éxito con el listado de campañas y sus paquetes. else En CATCH, construye JSON de error con la información de la excepción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.UnitDoseType; MixingStation.RequestMixingStationDetail; MixingStation.Package; MixingStation.PackagePersonalized', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CentralMezclasJson_S';
-- GO

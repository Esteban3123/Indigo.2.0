
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
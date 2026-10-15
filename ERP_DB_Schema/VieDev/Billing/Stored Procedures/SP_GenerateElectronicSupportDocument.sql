
-- ===============================================================================================================
-- Author:	Giovanny Plazas Lozano	
-- Create date: 2022/07/28
-- Description:	Procedimiento para Generar Documentos Soporte, apartir de CxP
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GenerateElectronicSupportDocument]
	@AccountPayableIds As xml,
	@IdAutorization as int = null
	------------------------------------------------------
AS
SET
  ANSI_NULLS,
  QUOTED_IDENTIFIER,
  CONCAT_NULL_YIELDS_NULL,
  ANSI_WARNINGS,
  ANSI_PADDING
ON;
BEGIN
	SET NOCOUNT ON

	

	DECLARE @MessageAuxTablet  Table(	Id int identity(1,1),
										IdDS INT,
										Code varchar(20),
										Message varchar(max))
/*----------------------------------------------------*/
BEGIN TRY

	IF NOT EXISTS (SELECT 1
					FROM @AccountPayableIds.nodes('/Data') t(x)
					WHERE t.x.value('CodeUser[1]','Varchar(20)') IS NOT NULL)
					BEGIN
						INSERT INTO @MessageAuxTablet VALUES(NULL,'999','No hay Datos para generar')

						SELECT * from @MessageAuxTablet
						RETURN
					END

	IF EXISTS( SELECT 1
				FROM @AccountPayableIds.nodes('/Data/AccountPayableIds') t(x))
				BEGIN 
					EXEC Billing.SP_GenerateElectronicSupportDocumentAC @AccountPayableIds, @IdAutorization
					RETURN
				END
	ELSE IF EXISTS( SELECT 1
				FROM @AccountPayableIds.nodes('/Data/VoucherTransactionIds') t(x))
				BEGIN 
					EXEC Billing.SP_GenerateElectronicSupportDocumentVT @AccountPayableIds, @IdAutorization
					RETURN
				END
	ELSE
				BEGIN
					INSERT INTO @MessageAuxTablet VALUES(NULL,'999','La estructura del XML es erronea')
				END

	
	/*se retorna la tabla de control*/
	SELECT *
	from @MessageAuxTablet
	RETURN
END TRY
BEGIN CATCH
	SELECT 0 as Id ,NULL as IdDS,'999' as Code,CONCAT('Error generando el documento Soporte', ERROR_MESSAGE(),' - Linea: ',ERROR_LINE()) AS [Message]
END CATCH
END
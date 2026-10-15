



-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-04-07
-- Description:	Procedimiento que se encarga de copiar y pegar o la importacion de archivo para los detalles de banco
-- =====================================================
CREATE PROCEDURE [Payroll].[SP_SetBankDetail]
    @BankDetailXml AS XML	
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @BankConciliationConcepts TABLE
	(
		Code varchar(20),		
		ExtractCode varchar(60),
		Detail varchar(60)
	)

	--Tabla temporal de los detalles para el return
	DECLARE @Detail TABLE
	(
		Id Int,
		BankId INT,
		BankConciliationConceptsId INT,
		Code varchar(20),
		Name varchar(100),
		ExtractCode varchar(60),
		Detail varchar(60)
	
	)

	BEGIN TRY
		--Se obtiene los detalles que vienen en el xml
			INSERT INTO @BankConciliationConcepts			
				SELECT	t.x.value('Code[1]','varchar(20)'),
						t.x.value('ExtractCode[1]','varchar(60)'),
						t.x.value('Detail[1]','varchar(60)')
				FROM @BankDetailXml.nodes('/BankDetail') t(x)

			----------------------------------------------------------------------------------------------------------------------------------------------------------------
						
			-- se inserta en la tabla temporal las relaciones y los detalles 
			INSERT INTO @Detail
			(
				[BankConciliationConceptsId],[Code],[Name],[ExtractCode],[Detail]
			)
			SELECT 
				bcc.Id, bcc.Code, bcc.Name, bc.ExtractCode, bc.Detail
			FROM Treasury.BankConciliationConcepts bcc WITH (NOLOCK) 
			JOIN @BankConciliationConcepts bc ON bc.Code = bcc.Code
			
			----------------------------------------------------------------------------------------------------------------------------------------------------------------

			IF (SELECT COUNT(BankConciliationConceptsId) FROM @Detail) = 0
			BEGIN
				SELECT 'Error, no se encuentra código del concepto de conciliación bancaria'
				RETURN
			END
						
			-- Retornar los datos
			SELECT * FROM @Detail		
			RETURN		
	END TRY
	BEGIN CATCH
		  SELECT '0' ,0 , 0, 2, ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message
	END CATCH
END
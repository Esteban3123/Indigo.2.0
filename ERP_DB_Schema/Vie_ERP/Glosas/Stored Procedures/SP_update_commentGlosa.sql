-- =============================================
-- Author:		
-- Create date: 12-08-2014
-- Description:	Script de actualizacion de cuenats 1302 - 1303
-- =============================================
CREATE PROCEDURE [Glosas].[SP_update_commentGlosa]

AS
BEGIN

	BEGIN TRY

	begin transaction 


	DECLARE @JustificationGlosa AS VARCHAR(max)
	DECLARE @JustificationReiteration AS VARCHAR(max)
	DECLARE @id AS INT 

		declare c_Depende cursor for
		SELECT Id,JustificationGlosa,JustificationReiteration FROM GLOSAS.GlosaMovementGlosa WHERE JustificationGlosa IS NOT NULL --OR JustificationReiteration IS NOT null
						
						SELECT Id,JustificationGlosa,JustificationGlosaText FROM GLOSAS.GlosaMovementGlosa WHERE JustificationGlosa IS NOT NULL
						
				open c_Depende
					fetch next from c_Depende into @id,@JustificationGlosa, @JustificationReiteration
					while @@FETCH_STATUS = 0 begin	
							
							
							DECLARE @texthtml AS VARCHAR(mAX) = @JustificationGlosa
							SET @texthtml =(SELECT dbo.udf_StripHTML3(@texthtml))
							SET @texthtml = (SELECT  RTRIM(LTRIM(dbo.udf_StripHTML2(@texthtml)) )  )
							SET @texthtml =  REPLACE(REPLACE(REPLACE(@texthtml, CHAR(9), ''), CHAR(10), ''), CHAR(13), '') 
							SET @texthtml =  RTRIM(LTRIM(@texthtml))

								
							UPDATE GLOSAS.GlosaMovementGlosa SET JustificationGlosatext = @texthtml WHERE id = @id
					
					fetch next from c_Depende INTO @id,@JustificationGlosa, @JustificationReiteration 
					END -- fin del while de C_Depende	
				close  c_Depende
				deallocate  c_Depende




	Commit Transaction
		--ROLLBACK transaction
	END TRY
	BEGIN CATCH
	rollback transaction
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END
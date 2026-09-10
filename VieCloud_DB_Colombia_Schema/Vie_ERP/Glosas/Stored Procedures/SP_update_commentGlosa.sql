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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de mantenimiento que limpia y normaliza el texto de las justificaciones de glosa almacenadas en los movimientos de glosa. Recorre todos los registros de GlosaMovementGlosa que tienen justificación de glosa en formato HTML y elimina las etiquetas HTML, espacios en blanco, tabulaciones y saltos de línea usando las funciones udf_StripHTML3 y udf_StripHTML2, guardando el resultado como texto plano en el campo JustificationGlosaText. Fue creado como un proceso de saneamiento puntual para las cuentas 1302-1303, y garantiza que las justificaciones de glosa queden legibles sin marcado HTML para su consulta, reporte o auditoría en el ciclo de glosas médicas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_update_commentGlosa';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_update_commentGlosa';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Normaliza la justificación de glosa convirtiendo su contenido HTML a texto plano (sin etiquetas, sin tabuladores ni saltos de línea, recortado) y lo persiste en la columna de texto correspondiente.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_commentGlosa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir las funciones dbo.udf_StripHTML2 y dbo.udf_StripHTML3.; La tabla GLOSAS.GlosaMovementGlosa debe contener registros con JustificationGlosa para que se actualice algo.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_commentGlosa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan filas cuya justificación de glosa no es NULL.; El texto resultante queda sin etiquetas HTML, sin tabuladores/saltos de línea (CHAR(9), CHAR(10), CHAR(13)) y recortado de espacios en extremos.; Toda la operación se realiza dentro de una transacción; ante cualquier error se hace rollback y se devuelven código y mensaje del error.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_commentGlosa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Justificación de glosa; Movimiento de glosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_commentGlosa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] GLOSAS.GlosaMovementGlosa: Para cada fila con JustificationGlosa NOT NULL, se calcula el texto plano (StripHTML3 + StripHTML2 + remoción de CHAR(9)/CHAR(10)/CHAR(13) + TRIM) y se escribe en JustificationGlosatext.; [RETURN_RESULT] (resultset de error): En caso de excepción, hace rollback y retorna un resultset con ERROR_NUMBER() y ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_commentGlosa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.udf_StripHTML3; dbo.udf_StripHTML2', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_commentGlosa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GLOSAS.GlosaMovementGlosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_commentGlosa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_commentGlosa';
-- GO

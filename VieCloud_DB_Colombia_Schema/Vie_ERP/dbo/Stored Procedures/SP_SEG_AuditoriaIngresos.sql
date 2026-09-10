

CREATE PROCEDURE [dbo].[SP_SEG_AuditoriaIngresos]
(
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT FECREGCRE AS FechaCrea,RTRIM(CODUSUCRE) + ' - ' + RTRIM(B.NOMUSUARI) AS Crea, 
FECREGMOD AS FechaModifica,RTRIM(CODUSUMOD) + ' - ' + RTRIM(C.NOMUSUARI) AS Modifica, 
FECREGANU AS FechaAnula,RTRIM(CODUSUANU) + ' - ' + RTRIM(D.NOMUSUARI) AS Anula
FROM dbo.ADINGRESO A INNER JOIN dbo.SEGusuaru B ON A.CODUSUCRE=B.CODUSUARI
LEFT OUTER JOIN dbo.SEGusuaru C ON A.CODUSUMOD=C.CODUSUARI
LEFT OUTER JOIN dbo.SEGusuaru D ON A.CODUSUANU=D.CODUSUARI
WHERE NUMINGRES = @Ingreso
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la auditoría de un ingreso o admisión específico: muestra quién creó el registro, quién lo modificó y quién lo anuló, junto con las fechas correspondientes de cada acción. Recibe como parámetro el número de ingreso y cruza la tabla de ingresos (ADINGRESO) con la tabla de usuarios del sistema (SEGusuaru) para mostrar el código y el nombre completo del usuario responsable de cada operación. Se usa para rastrear y auditar el ciclo de vida de un episodio de atención (urgencia, hospitalización, consulta), identificando responsabilidades ante cambios o anulaciones de ingresos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_AuditoriaIngresos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_AuditoriaIngresos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta la trazabilidad de auditoría (creación, modificación y anulación) de un ingreso/admisión, mostrando fechas y usuarios responsables de cada acción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El número de ingreso debe corresponder a un registro existente en la tabla de ingresos.; El usuario creador del ingreso debe existir en el catálogo de usuarios de seguridad (de lo contrario no se retorna fila por el INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El usuario de creación es obligatorio (INNER JOIN), por lo que todo ingreso debe tener un creador registrado existente en la tabla de usuarios.; Los usuarios de modificación y anulación son opcionales (LEFT JOIN); se muestran solo si existen.; El identificador del usuario se concatena con su nombre en formato ''CODIGO - NOMBRE'' para fines de auditoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Auditoría de usuarios; Usuario de creación; Usuario de modificación; Usuario de anulación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Cuando NUMINGRES coincide con el ingreso solicitado, retorna fechas y usuarios (código + nombre) de creación, modificación y anulación del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaIngresos';
-- GO

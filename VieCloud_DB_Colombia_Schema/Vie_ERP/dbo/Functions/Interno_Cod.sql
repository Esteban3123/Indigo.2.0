

CREATE FUNCTION [dbo].[Interno_Cod] (
@NFOLIO as char(10),
@COD AS varchar(25)

)
RETURNS varchar (100)
AS
BEGIN

declare @CODIGO varchar(100)
declare @NOMBRE varchar(100)

/*SET @grupo= (
SELECT EQUI.CODPROSAL from dbo.HCQXEQUIP AS EQUI WHERE ((EQUI.NUMEFOLIO=@NFOLIO) AND (EQUI.IPCODPACI=@COD)AND (EQUI.CODPROSAL LIKE 'MI%' )))
*/
SELECT @CODIGO=EQUI.CODPROSAL,@NOMBRE=INP.NOMMEDICO from dbo.HCQXEQUIP AS EQUI INNER JOIN dbo.INPROFSAL AS INP ON EQUI.CODPROSAL=INP.CODPROSAL
WHERE ((EQUI.NUMEFOLIO=@NFOLIO) AND (EQUI.IPCODPACI=@COD)AND (EQUI.CODPROSAL LIKE 'MI%' ))
RETURN (@CODIGO+@NOMBRE)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que, dado un número de folio quirúrgico y el código o cédula del paciente, devuelve el código y nombre del profesional de salud interno (cirujano o integrante del equipo quirúrgico cuyo código comienza con ''MI'') asignado a ese procedimiento. Combina el registro de equipos quirúrgicos (HCQXEQUIP) con el maestro de profesionales de la salud (INPROFSAL) para identificar al médico interno participante en la cirugía. Se usa principalmente para obtener el nombre del profesional interno en reportes quirúrgicos, historia clínica de cirugías e impresión de documentos del acto quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Interno_Cod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Interno_Cod';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la concatenación del código y nombre del profesional de salud tipo "Médico Internista" (prefijo MI) asociado a un equipo quirúrgico de un paciente en un folio determinado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Interno_Cod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCQXEQUIP que coincida con el folio y el código de paciente dados; El profesional asociado debe tener un código que comience con ''MI''; Debe existir el profesional en INPROFSAL para hacer el JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Interno_Cod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera profesionales con código que comienza con ''MI''; Requiere coincidencia exacta de folio y código de paciente en el equipo quirúrgico; Retorna información combinada (código+nombre) sin separador entre ambos valores', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Interno_Cod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Folio de atención; Equipo quirúrgico; Profesional de salud; Médico internista', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Interno_Cod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna la concatenación de CODPROSAL + NOMMEDICO cuando hay coincidencia con folio, paciente y código que inicie con ''MI''; si no hay match, retorna NULL por la concatenación con variables nulas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Interno_Cod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EQUI.CODPROSAL LIKE ''MI%'' → Filtra solo profesionales cuyo código inicie con ''MI'' (médicos internistas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Interno_Cod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXEQUIP; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Interno_Cod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Interno_Cod';
GO

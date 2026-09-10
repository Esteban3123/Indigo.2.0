

create FUNCTION [dbo].[Quirurgicos] (@Ingreso as char(10))
RETURNS nvarchar (1000)
AS
BEGIN

declare @Quirurgicos varchar (max)
 SELECT @Quirurgicos =  COALESCE(rtrim(@Quirurgicos) + ' ;*' + rtrim(b.DESSERIPS), rtrim(b.DESSERIPS))
FROM  .HCORDPROQ as a INNER JOIN
     .INCUPSIPS AS b on b.CODSERIPS = a.CODSERIPS
	 where @Ingreso=NUMINGRES and ESTSERIPS NOT IN ('3','4') AND UFUCODIGO='N01'
RETURN @Quirurgicos

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un número de ingreso hospitalario, devuelve la lista concatenada de nombres de procedimientos quirúrgicos activos ordenados para ese paciente durante su internación. Consulta las órdenes de procedimientos de la historia clínica (HCORDPROQ) filtrando únicamente las pertenecientes a la unidad funcional de quirófano (N01) y excluyendo las que están anuladas o en estado cancelado (estados 3 y 4); luego cruza con el catálogo de servicios CUPS/IPS (INCUPSIPS) para obtener el nombre descriptivo de cada procedimiento. El resultado es un texto separado por punto y coma que resume los procedimientos quirúrgicos vigentes de un ingreso, utilizado típicamente para mostrar en pantalla o en reportes clínicos qué cirugías o intervenciones tiene ordenadas el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Quirurgicos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Quirurgicos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve una cadena concatenada con los nombres de los procedimientos quirúrgicos vigentes asociados a un ingreso, separados por '' ;*''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Quirurgicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el ingreso en HCORDPROQ con órdenes de procedimientos; Los procedimientos deben tener correspondencia en el catálogo INCUPSIPS por CODSERIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Quirurgicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se concatenan procedimientos cuyo estado NO sea ''3'' ni ''4''; Solo se consideran procedimientos de la unidad funcional UFUCODIGO=''N01''; El separador entre descripciones es '' ;*''; Para el primer elemento no se antepone separador (uso de COALESCE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Quirurgicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'procedimiento quirúrgico; ingreso; catálogo CUPS/IPS; unidad funcional quirúrgica; estado del servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Quirurgicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna nvarchar(1000) concatenando DESSERIPS de procedimientos del ingreso filtrados por ESTSERIPS NOT IN (''3'',''4'') y UFUCODIGO=''N01''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Quirurgicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS IN (''3'',''4'') → Excluye el procedimiento del resultado (estados anulado/finalizado descartados) else Incluye el procedimiento concatenándolo a la cadena; si UFUCODIGO = ''N01'' → Solo considera procedimientos de la unidad funcional ''N01'' (quirúrgica) else No los incluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Quirurgicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDPROQ; INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Quirurgicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Quirurgicos';
GO

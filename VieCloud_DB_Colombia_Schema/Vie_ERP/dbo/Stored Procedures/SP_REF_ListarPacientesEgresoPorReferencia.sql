-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,27-10-2018,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarPacientesEgresoPorReferencia]
(
  @CentroAtencion as varchar(Max)
)

AS
BEGIN
  SET NOCOUNT ON;
  
 declare @Sql as nvarchar(Max)

  Set @Sql = ' SELECT DISTINCT A.NUMINGRES,S.CODTIPPAC,Case S.CODTIPPAC when  1 then  ''Maternas'' when 2 then ''Menores de 5 Años'' when 3 then ''Adultos Mayores''  when 4 then ''Discapacitados'' when 5 then ''Poblacion General'' end as ''TipoPoblacion'',FECSOLICIT,G.IPCODPACI,RTRIM(I.IPNOMCOMP) as IPNOMCOMP,I.IPFECNACI,RTRIM(E.CODENTIDA) +''-''+RTRIM(E.NOMENTIDA) AS ''Entdidad'',RTRIM(UFUEGRHOS) +''-''+RTRIM(UFUDESCRI) AS ''UnidadFuncional'',S.FECREGCRE,S.FECHEGRESO,Rtrim(Z.CODCENATE) as ''Codigo Centro Atencion'',Rtrim(z.NOMCENATE) as ''Nombre Centro Atencion''
				FROM dbo.HCREFCONP  AS G  
						INNER JOIN dbo.INPACIENT AS I ON I.IPCODPACI = G.IPCODPACI 
						INNER JOIN dbo.ADINGRESO  AS S ON  S.NUMINGRES  = G.NUMINGRES 
						INNER JOIN dbo.CHREGEGRE  AS A ON A.NUMINGRES  = G.NUMINGRES  
                        INNER JOIN dbo.ADCENATEN  AS Z ON  Z.CODCENATE = g.CODCENATE  
						LEFT JOIN dbo.INENTIDAD as E ON E.CODENTIDA = S.CODENTIDA 
						INNER JOIN dbo.INUNIFUNC as R ON R.UFUCODIGO = UFUEGRHOS 
  Where  convert(varchar(500),g.CODCENATE) IN (' + @CentroAtencion + ') and  ESTPACEGR = 4 and G.ESTADO = 5 AND VISADO is null '
  
  exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes egresados que tienen una solicitud de remisión o referencia pendiente de visar, filtrando por uno o más centros de atención indicados como parámetro. Combina datos del paciente (cédula, nombre, fecha de nacimiento), del ingreso hospitalario (tipo de población, entidad aseguradora, unidad funcional de egreso, fechas de registro y egreso) y del registro de egreso, mostrando únicamente aquellos cuyo estado de egreso es 4 y cuya referencia tiene estado 5 sin visado. Utiliza SQL dinámico para permitir el filtro por múltiples centros de atención a la vez. Sirve al módulo de referencias y contrarreferencias para el seguimiento y control de pacientes dados de alta que aún requieren gestión de remisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los pacientes con solicitud de remisión vigente (no visada) que ya cuentan con egreso hospitalario registrado, filtrados por uno o varios centros de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe llegar como una lista de valores ya formateada e inyectable dentro de un IN (...) (se concatena directamente al SQL dinámico).; Deben existir maestros consistentes de pacientes, ingresos, egresos, centros de atención y unidades funcionales para que los INNER JOIN devuelvan datos.; El usuario ejecutor debe tener permisos para ejecutar SQL dinámico vía sp_executesql sobre las tablas consultadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven pacientes con egreso registrado (existe fila en CHREGEGRE para el ingreso).; Solo se incluyen registros con estado de paciente egresado igual a 4 (ESTPACEGR=4).; Solo se consideran remisiones con estado = 5 en HCREFCONP.; Solo se consideran remisiones aún no visadas (VISADO IS NULL).; El listado se filtra a los centros de atención indicados en el parámetro de entrada.; Los resultados son únicos por la combinación seleccionada (uso de SELECT DISTINCT).; La entidad puede no existir (LEFT JOIN con INENTIDAD), el resto de relaciones son obligatorias (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; remisión/referencia; egreso hospitalario; ingreso/admisión; centro de atención; unidad funcional de egreso; entidad/aseguradora; tipo de población (maternas, menores de 5, adultos mayores, discapacitados, población general); visado de remisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando CODCENATE de la remisión está en la lista recibida y ESTPACEGR=4 y ESTADO de la remisión=5 y VISADO IS NULL, retorna ingreso, tipo de población (mapeado a texto), fecha de solicitud, datos del paciente, entidad, unidad funcional de egreso, fechas de registro/egreso y centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODTIPPAC = 1 → clasifica como ''Maternas''; si CODTIPPAC = 2 → clasifica como ''Menores de 5 Años''; si CODTIPPAC = 3 → clasifica como ''Adultos Mayores''; si CODTIPPAC = 4 → clasifica como ''Discapacitados''; si CODTIPPAC = 5 → clasifica como ''Poblacion General''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFCONP; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHREGEGRE; dbo.ADCENATEN; dbo.INENTIDAD; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesEgresoPorReferencia';
-- GO

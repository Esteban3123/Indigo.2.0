
CREATE PROCEDURE [dbo].[SP_AD_ListarPacientesExtramuralesAutoServicio]
(
@CentroAtencion as varchar(200),
@UnidadFuncional as varchar(200),
@FechaInicial as varchar(200),
@FechaFinal as varchar(200),
@Entidad as varchar(200),
@EmpresaVie as varchar(20)
)

AS
BEGIN
	
	SET NOCOUNT ON;

declare @Sql as nvarchar(max)

set @Sql = 'SELECT distinct  '''' as Alerta, RTRIM(A.IPCODPACI) AS Identificacion,Estado, RTRIM(IPNOMCOMP) as NombrePaciente, IPFECNACI AS ''Fecha Nacimiento'', CAST('''' AS CHAR(50)) AS Edad, A.NUMINGRES AS Ingreso, RTRIM(D.DESESPECI) AS Especialidad, RTRIM(E.Name) AS Entidad, rtrim(a.UFUCODIGO) as ''Unidad Funcional'' 
From dbo.ADAUTOSER A 
INNER JOIN dbo.ADINGRESO B ON B.NUMINGRES = A.NUMINGRES 
INNER JOIN dbo.INPACIENT C ON C.IPCODPACI = A.IPCODPACI 
LEFT OUTER JOIN ' + @EmpresaVie + '.Contract.healthadministrator E on E.Id = B.GENCONENTITY 
inner join dbo.INESPECIA D ON D.CODESPECI=B.CODESPTRA 
WHERE A.CODCENATE = ' + @CentroAtencion + ' AND A.UFUCODIGO IN ( '+ @UnidadFuncional +' ) AND SOLEXTRAM = 1 AND A.SERSUSCEP = 1 AND PROESTADO IN (1,2,3,5) AND FECREGIST BETWEEN ' + @FechaInicial + ' AND ' + @FechaFinal + ' AND E.Code IN (' + @Entidad + ') '

exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes extramurales registrados en el módulo de autoservicio de admisiones, filtrando por centro de atención, unidad funcional, rango de fechas y entidad (aseguradora o pagador). Combina datos de la tabla de autoservicio (ADAUTOSER), el ingreso hospitalario (ADINGRESO), la información del paciente (INPACIENT) y la especialidad de traslado (INESPECIA), mostrando cédula, nombre, fecha de nacimiento, número de ingreso, especialidad, entidad y unidad funcional. Se usa para consultar pacientes con solicitud extramuros activa (SOLEXTRAM=1) en estados de proceso vigentes, típicamente en pantallas o reportes de gestión de atención domiciliaria o traslados externos. Construye SQL dinámico en tiempo de ejecución combinando los parámetros recibidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con solicitudes de servicios extramurales susceptibles de autoservicio, filtrando por centro de atención, unidad funcional, rango de fechas, entidad y estados de proceso vigentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El nombre de base de datos pasado para la administradora de salud debe existir y contener el esquema Contract.healthadministrator.; Los parámetros de filtros se concatenan directamente al SQL dinámico (deben venir formateados con comillas y comas si corresponde).; El centro de atención, unidad funcional, fechas y entidad deben venir en formato compatible con la cláusula SQL construida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen solicitudes marcadas como extramurales (SOLEXTRAM=1).; Solo se incluyen servicios susceptibles de autoservicio (SERSUSCEP=1).; Solo se consideran solicitudes con estado de proceso 1, 2, 3 o 5 (excluye otros estados como anulados/finalizados fuera de ese conjunto).; El filtro por entidad se aplica sobre el código (E.Code) de la administradora de salud en la base externa indicada.; El join con la administradora de salud es LEFT OUTER pero el WHERE sobre E.Code lo convierte efectivamente en INNER.; Los campos Alerta y Edad se devuelven como cadenas vacías (placeholders calculados aguas arriba).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Servicio extramural; Autoservicio; Especialidad médica; Entidad/Administradora de salud; Centro de atención; Unidad funcional; Estado de proceso de solicitud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADAUTOSER: Devuelve registros distinct con datos del paciente, ingreso, especialidad y entidad cuando SOLEXTRAM=1, SERSUSCEP=1, PROESTADO IN (1,2,3,5) y FECREGIST está entre las fechas indicadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADAUTOSER; dbo.ADINGRESO; dbo.INPACIENT; dbo.INESPECIA; Contract.healthadministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarPacientesExtramuralesAutoServicio';
-- GO

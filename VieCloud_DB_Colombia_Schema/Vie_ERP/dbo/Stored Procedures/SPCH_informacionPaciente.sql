
CREATE PROCEDURE [dbo].[SPCH_informacionPaciente]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

 SELECT
 d.NUMINGRES,
 d.CODDIAPRI,
 d.FECDIAGNO,
--Consulta que tenga un diagnostico inicial (d - INDIAGNOS)
case WHEN D.NOMDIAGNO IS NULL THEN 'No Tiene Diagnostico Principal' else RTRIM(d.NOMDIAGNO) end as DaignostcoPrincipal,
--Nombre Persona (p - INPACIENT)
RTRIM(p.IPNOMCOMP) AS  NombreCompleto, 
-- Edad (b - INPACIENT)
p.IPFECNACI as Edad,
-- Grupo Sanguineo (b - INPACIENT)
CASE WHEN p.IPGRUPSAN IS NULL THEN 'No Tiene Grupo Sanguineo' else p.IPGRUPSAN end  as GrupoSanguineo,
-- Datos Personales (p - INPACIENT)
p.IPCODPACI as Identificacion,
-- Direccion (p - INPACIENT)
p.IPDIRECCI as Direccion,
-- Telefono (p - INPACIENT)
p.IPTELMOVI as Telefono,
-- Genero (p - INPACIENT)
case p.IPSEXOPAC when 1 then 'Masculino' when 2 then 'Femenino' end as Sexo,
-- Fecha Nacimiento (b - INPACIENT)
convert(varchar, p.IPFECNACI, 105) as FechaNacimiento
FROM INPACIENT p
left join (SELECT [NUMINGRES] ,[IPCODPACI] ,[NOMDIAGNO]  ,[CODDIAPRI], FECDIAGNO FROM [INDIAGNOH] rel inner join [INDIAGNOS] diag on rel.CODDIAGNO = diag.CODDIAGNO) as d on d.IPCODPACI = p.IPCODPACI
WHERE p.IPCODPACI= @Paciente
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera la ficha completa de un paciente a partir de su cédula o código de identificación y número de ingreso. Retorna datos personales del paciente (nombre completo, fecha de nacimiento, sexo, grupo sanguíneo, dirección y teléfono celular) obtenidos de la tabla maestra de pacientes, junto con el diagnóstico principal registrado en su historia clínica, incluyendo el código CIE-10, nombre del diagnóstico y fecha en que fue registrado. Combina la información demográfica de INPACIENT con los diagnósticos de INDIAGNOH y su descripción en el catálogo INDIAGNOS, mostrando un mensaje descriptivo cuando el paciente no tiene diagnóstico principal o grupo sanguíneo registrado. Se usa típicamente en pantallas de atención clínica, urgencias u hospitalización para consultar de un vistazo la información esencial del paciente y su diagnóstico principal en un ingreso determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_informacionPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_informacionPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los datos demográficos básicos del paciente junto con su diagnóstico principal asociado, devolviendo etiquetas por defecto cuando faltan diagnóstico o grupo sanguíneo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_informacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente en INPACIENT con el identificador suministrado para obtener filas; La relación INDIAGNOH-INDIAGNOS debe estar consistente vía CODDIAGNO para resolver el diagnóstico principal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_informacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Filtra siempre por el identificador del paciente recibido; El cruce con diagnósticos es opcional (LEFT JOIN): un paciente sin diagnóstico igual aparece en el resultado; Las fechas de nacimiento se entregan formateadas con estilo 105 (dd-mm-yyyy); Solo se consideran sexos válidos los códigos 1 y 2', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_informacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Diagnóstico principal; Grupo sanguíneo; Género; Fecha de nacimiento; Identificación del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_informacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando IPCODPACI coincide con el paciente solicitado, retorna sus datos personales y, si existe, su diagnóstico (LEFT JOIN con INDIAGNOH/INDIAGNOS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_informacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOMDIAGNO IS NULL → Devuelve la etiqueta ''No Tiene Diagnostico Principal'' como diagnóstico principal else Devuelve el nombre del diagnóstico (RTRIM); si IPGRUPSAN IS NULL → Devuelve ''No Tiene Grupo Sanguineo'' else Devuelve el grupo sanguíneo del paciente; si IPSEXOPAC = 1 → Devuelve ''Masculino'' else Si IPSEXOPAC = 2 devuelve ''Femenino''; otro valor queda nulo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_informacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INDIAGNOH; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_informacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_informacionPaciente';
-- GO

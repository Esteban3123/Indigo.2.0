

-- Ordenes Asignadas sin gestionar
CREATE VIEW [Integrations].[viewCIMAHospital_Softland_PatientInformation]
AS

select
P.IPCODPACI as Identificacion,
'Normal' as TipoPaciente,
rtrim(ltrim(p.IPPRINOMB)) + ' ' + rtrim(ltrim(p.IPSEGNOMB)) as NombrePaciente,
rtrim(ltrim(p.IPPRIAPEL)) as PrimerApellido,
rtrim(ltrim(p.IPSEGAPEL)) as SegundoApellido,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = P.IPTIPODOC and MasterType = 'TipoDocumento' ) as TipoIdentificacion,
p.IPSEXOPAC,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = P.IPSEXOPAC  and MasterType = 'GeneroPaciente' ) as Genero,
format(p.IPFECNACI,'dd/MM/yyyy') as FechaNacimiento,
Pa.StandardCode as CodigoNacionalidad,
case p.IPESTADOC when 1 then 'Soltero' when 2 then 'Casado' when 3 then 'Viudo' when 4 then 'Union libre' when 5 then 'Divorciado' END EstadoCivil,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = P.CREDCODIGO and MasterType = 'Religion' ) as Religion,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = P.IDICODIGO  and MasterType = 'Idioma' ) as Idioma,
pa.Name as NombrePais,
d.NOMDEPART,
u.UBINOMBRE,
m.MUNNOMBRE,
co.Nombre as NombreComuna,
Pa.ID Idpaciente,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = pa.Code  and MasterType = 'Pais' ) as CodigoPais,
SUBSTRING(d.depcodigo,2, 1) as DivisionGeografica1,
SUBSTRING(m.MUNCODIGO,2,2)as DivisionGeografica2,
RIGHT('0' + Ltrim(Rtrim(co.Codigo)),2) as DivisionGeografica3,
SUBSTRING(u.UBICODIGO,4,2)as DivisionGeografica4,
ISNULL(IPTELEFON,ISNULL(IPTELMOVI,'999999')) as Telefono,
ISNULL(iif(p.CORELEPAC = '','Integrations@gmail.com',p.CORELEPAC) ,'Integrations@gmail.com') as Email
from INPACIENT p inner join
dbo.INUBICACI u on u.AUUBICACI = p.AUUBICACI inner join
dbo.INMUNICIP m on M.DEPMUNCOD = u.DEPMUNCOD inner join
dbo.INDEPARTA d on d.DEPCODIGO = m.DEPCODIGO inner join
common.country pa on d.idpais = pa.id left join
dbo.ADCOMUNAS co on co.Id = u.IDcomuna
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de integración que expone la información demográfica completa de cada paciente del sistema Indigo en el formato requerido por el sistema legado CIMA Hospital (Softland). Consolida datos de la tabla maestra de pacientes (nombre completo, cédula o documento de identidad, fecha de nacimiento, sexo, estado civil, religión, idioma, teléfono y correo electrónico) con su ubicación geográfica (país, departamento, municipio, barrio/sector y comuna), recorriendo los catálogos de ubicación, municipios, departamentos y países. Traduce los códigos internos de Indigo (tipo de documento, género, país, religión, idioma) a los códigos equivalentes del sistema CIMA Hospital mediante la tabla de homologación CIMAHospital_HomologationMaster, y descompone la jerarquía geográfica en cuatro niveles de división territorial (DivisionGeografica1 a 4) que el sistema receptor requiere. Se usa como fuente de sincronización o exportación de maestro de pacientes hacia CIMA Hospital / Softland durante procesos de integración.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'viewCIMAHospital_Softland_PatientInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'viewCIMAHospital_Softland_PatientInformation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone información demográfica y de ubicación de pacientes homologada hacia los códigos del sistema legado CIMA/Softland para integración externa.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_PatientInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener una ubicación válida asociada (AUUBICACI) que enlace con municipio, departamento y país.; Las tablas de homologación deben contener mapeos para TipoDocumento, GeneroPaciente, Religion, Idioma y Pais para obtener los códigos legados.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_PatientInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TipoPaciente siempre se reporta como ''Normal''.; Nombres y apellidos se entregan sin espacios sobrantes (LTRIM/RTRIM).; FechaNacimiento se formatea siempre como ''dd/MM/yyyy''.; Teléfono nunca se entrega NULL: si no hay fijo ni móvil se devuelve ''999999''.; Email nunca se entrega NULL ni vacío: se sustituye por ''Integrations@gmail.com''.; Las divisiones geográficas se derivan posicionalmente desde códigos: depcodigo[2:1], MUNCODIGO[2:2], UBICODIGO[4:2] y Codigo de comuna con padding a 2 dígitos.; La comuna es opcional (LEFT JOIN); el resto de la jerarquía geográfica es obligatoria.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_PatientInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Identificación; Tipo de documento; Género; Estado civil; Religión; Idioma; Nacionalidad; País; Departamento; Municipio; Comuna; Ubicación geográfica; Datos de contacto (teléfono, email); Homologación de códigos entre sistemas', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_PatientInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] INPACIENT: Devuelve un registro por cada paciente que tenga ubicación, municipio, departamento y país relacionados (INNER JOIN); pacientes sin estos vínculos quedan excluidos.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_PatientInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPESTADOC = 1..5 → Traduce el código de estado civil a etiqueta textual: 1=Soltero, 2=Casado, 3=Viudo, 4=Union libre, 5=Divorciado else NULL (cualquier otro valor no se traduce); si IPTELEFON es NULL → Usa IPTELMOVI como teléfono; si también es NULL, asigna ''999999'' como valor por defecto; si CORELEPAC es vacío o NULL → Asigna ''Integrations@gmail.com'' como correo electrónico por defecto', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_PatientInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; common.country; dbo.ADCOMUNAS; Integrations.CIMAHospital_HomologationMaster', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_PatientInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_PatientInformation';
GO

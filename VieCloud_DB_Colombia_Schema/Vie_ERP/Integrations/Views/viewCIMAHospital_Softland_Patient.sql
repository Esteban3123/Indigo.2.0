

-- Ordenes Asignadas sin gestionar
CREATE VIEW [Integrations].[viewCIMAHospital_Softland_Patient]
AS

select
Sync.id,
Sync.Type,
case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
Sync.TransactionDate,
Sync.State,
'Normal' as TipoPaciente,
rtrim(ltrim(p.IPPRINOMB)) + ' ' + rtrim(ltrim(p.IPSEGNOMB)) as NombrePaciente,
rtrim(ltrim(p.IPPRIAPEL)) as PrimerApellido,
rtrim(ltrim(p.IPSEGAPEL)) as SegundoApellido,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = P.IPTIPODOC and MasterType = 'TipoDocumento' ) as TipoIdentificacion,
[Integrations].[GetIdentificationSoftland](P.IPTIPODOC,P.IPCODPACI) as Identificacion,
P.IPTIPODOC,
P.IPCODPACI as IdentificacionReal,
p.IPSEXOPAC ,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = P.IPSEXOPAC and MasterType = 'GeneroPaciente' ) as Genero,
format(p.IPFECNACI,'dd/MM/yyyy') as FechaNacimiento,
---Pa.StandardCode as CodigoNacionalidad,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = Pa.Code and MasterType = 'Pais' ) as CodigoNacionalidad,
--'CRI' as CodigoNacionalidad,
case p.IPESTADOC when 1 then 'Soltero' when 2 then 'Casado' when 3 then 'Viudo' when 4 then 'Union libre' when 5 then 'Divorciado' END EstadoCivil,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = ISNULl(P.CREDCODIGO,'008') and MasterType = 'Religion' ) as Religion,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = ISNULL(P.IDICODIGO,'137')  and MasterType = 'Idioma' ) as Idioma,
pa.Name as NombrePais,
d.NOMDEPART,
m.MUNNOMBRE,
co.Nombre as NombreComuna,
u.UBINOMBRE,
Pa.ID Idpaciente,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = Pa.Code and MasterType = 'Pais' ) as CodigoPais,
SUBSTRING(d.depcodigo,2, 1) as DivisionGeografica1,
SUBSTRING(m.MUNCODIGO,2,2)as DivisionGeografica2,
--RIGHT('0' + Ltrim(Rtrim(co.Codigo)),2) as DivisionGeografica3,
SUBSTRING(UBICODIGO,4,2) as DivisionGeografica3,
SUBSTRING(u.UBICODIGO,6,2)as DivisionGeografica4,
ISNULL(iif(IPTELEFON = '','99999999',IPTELEFON),ISNULL(IPTELMOVI,'99999999')) as Telefono,
ISNULL(iif(p.CORELEPAC = '','Integrations@gmail.com',p.CORELEPAC) ,'Integrations@gmail.com') as Email
from INPACIENT p inner join
dbo.INUBICACI u on u.AUUBICACI = p.AUUBICACI inner join
dbo.INMUNICIP m on M.DEPMUNCOD = u.DEPMUNCOD inner join
dbo.INDEPARTA d on d.DEPCODIGO = m.DEPCODIGO inner join
common.country pa on d.idpais = pa.id left join
dbo.ADCOMUNAS co on co.Id = u.IDcomuna inner join
Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = p.ID  
where sync.State = 0 AND sync.Type = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de integración que expone los datos de pacientes pendientes de sincronización (estado 0, tipo 2) desde el sistema Indigo hacia el ERP Softland a través del puente CIMA Hospital. Consolida información personal del paciente —nombre completo, apellidos, tipo y número de identificación (cédula, documento), sexo, género, fecha de nacimiento, estado civil, religión, idioma, teléfono y correo electrónico— con su ubicación geográfica completa (país, departamento, municipio, comuna, barrio/ubicación). Aplica la tabla de homologación CIMAHospital_HomologationMaster para traducir los códigos internos de Indigo (tipo de documento, género, país, religión, idioma) a los códigos equivalentes que reconoce Softland y CIMA Hospital. Existe para alimentar el proceso de sincronización de maestro de pacientes entre ambos sistemas, permitiendo crear, actualizar o eliminar registros de pacientes en el ERP externo con los datos correctamente mapeados y formateados.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'viewCIMAHospital_Softland_Patient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'viewCIMAHospital_Softland_Patient';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los pacientes pendientes de sincronizar hacia Softland, homologando catálogos (documento, género, país, religión, idioma) y armando datos demográficos y de contacto con valores por defecto.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en CIMAHospital_Softland_Synch con State=0 y Type=2 asociado al ID del paciente.; El paciente debe tener ubicación, municipio, departamento y país relacionados vía AUUBICACI / DEPMUNCOD / DEPCODIGO / idpais.; Los códigos del paciente (tipo documento, sexo, país, religión, idioma) deben estar mapeados en CIMAHospital_HomologationMaster bajo el MasterType correspondiente para obtener el LegacyCode.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo paciente devuelto siempre tiene teléfono no nulo (default ''99999999'') y email no nulo (default ''Integrations@gmail.com'').; TipoPaciente siempre se reporta como ''Normal''.; Religión e idioma nunca quedan sin código fuente: se sustituyen por ''008'' y ''137'' respectivamente cuando faltan.; Los códigos de catálogo (documento, género, país, religión, idioma) se entregan ya homologados al sistema legado vía CIMAHospital_HomologationMaster.; La fecha de nacimiento se entrega siempre con formato ''dd/MM/yyyy''.; La división geográfica se obtiene siempre por substring fijo de los códigos de departamento, municipio y ubicación (posiciones 2/2,2/4,2/6,2).', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Sincronización con ERP Softland; Homologación de catálogos; Tipo de documento; Género; Estado civil; Religión; Idioma; Nacionalidad / País; División geográfica (departamento, municipio, comuna, ubicación); Datos de contacto (teléfono, email)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Integrations.viewCIMAHospital_Softland_Patient: Solo retorna pacientes cuando sync.State = 0 AND sync.Type = 2 (pendientes de sincronización tipo paciente).', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Sync.Action = 1 / 2 / 3 → Etiqueta la acción como ''Insert'', ''update'' o ''delete'' respectivamente. else NULL (sin etiqueta); si p.IPESTADOC en 1..5 → Traduce a ''Soltero'', ''Casado'', ''Viudo'', ''Union libre'' o ''Divorciado''. else NULL; si P.CREDCODIGO IS NULL → Usa ''008'' como código de religión por defecto para homologar. else Usa el CREDCODIGO del paciente.; si P.IDICODIGO IS NULL → Usa ''137'' como código de idioma por defecto para homologar. else Usa el IDICODIGO del paciente.; si IPTELEFON vacío o NULL → Usa IPTELMOVI; si también es NULL, usa ''99999999'' como teléfono por defecto. else Usa IPTELEFON.; si CORELEPAC vacío o NULL → Usa ''Integrations@gmail.com'' como email por defecto. else Usa el correo del paciente.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Integrations.GetIdentificationSoftland', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; common.country; dbo.ADCOMUNAS; Integrations.CIMAHospital_Softland_Synch; Integrations.CIMAHospital_HomologationMaster', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Patient';
GO






-- Ordenes Asignadas sin gestionar
CREATE VIEW [Integrations].[viewCIMAHospital_Softland_Admission]
AS

select
Sync.id,
Sync.Type,
case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
Sync.TransactionDate,
Sync.State,
I.NUMINGRES as NumeroIngreso,
I.IFECHAING as FechaIngreso,
'Admitido' as EstadoPaciente,
'Externo' as TipoVisita,
'Urgencias' as TipoPaciente,
'Urgencias' as NivelAtencion,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = I.UFUCODIGO  and MasterType = 'UnidadFuncional' ) as Ubicacion,
'ND' as Profesional,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = P.IPTIPODOC and MasterType = 'TipoDocumento' ) as TipoIdentificacion,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = I.GENCONENTITY and MasterType = 'Aseguradoras' ) as Aseguradora,
--P.IPCODPACI as IdentificacionPaciente,
[Integrations].[GetIdentificationSoftland](P.IPTIPODOC,P.IPCODPACI) as IdentificacionPaciente,
rtrim(ltrim(p.IPNOMCOMP)) as NombrePaciente,
P.IPTIPODOC,
P.IPCODPACI as IdentificacionReal
from ADINGRESO I inner join
INPACIENT p on I.IPCODPACI = p.IPCODPACI inner join 
Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = I.NUMINGRES  
where sync.State = 0 AND sync.Type = 1 -- 1 : ingresos
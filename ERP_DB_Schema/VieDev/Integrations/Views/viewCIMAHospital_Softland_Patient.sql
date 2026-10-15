









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
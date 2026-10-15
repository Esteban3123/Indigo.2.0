




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
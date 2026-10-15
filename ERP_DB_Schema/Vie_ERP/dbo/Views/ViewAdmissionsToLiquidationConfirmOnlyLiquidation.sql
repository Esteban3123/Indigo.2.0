

CREATE VIEW [dbo].[ViewAdmissionsToLiquidationConfirmOnlyLiquidation]
  
AS

-- Vista campos necesarios para la liquidación

SELECT DISTINCT
ING.NUMINGRES AS AdmissionCode,
PAT.IPCODPACI AS PatientCode,
PAT.IPNOMCOMP AS PatientName,
ING.TIPOINGRE AS AdmissionType,
ING.IFECHAING AS AdmissionDate,
ING.CODICAMHO AS BedStay,
ING.ILIQUIDAC AS LiquidationType,
ING.IPRNOMBRE AS ResponsibleName,
ING.IESTADOIN AS Status,
'- Ingresos' AS StatusName,
null TratamientoEspecial
, tpp.Id As ThirdPartyPatientId
FROM dbo.ADINGRESO AS ING with(nolock)
INNER JOIN dbo.INPACIENT AS PAT with(nolock) ON ING.IPCODPACI = PAT.IPCODPACI
--INNER JOIN dbo.ADNIVELES AS NIV with(nolock) ON PAT.NIVCODIGO = NIV.NIVCODIGO
--INNER JOIN dbo.ADCENATEN AS CEN with(nolock) ON ING.CODCENATE = CEN.CODCENATE
--INNER JOIN dbo.INUNIFUNC AS UFU with(nolock) ON ING.UFUCODIGO = UFU.UFUCODIGO
inner join Billing.Invoice bi with(nolock) on bi.AdmissionNumber = ING.NUMINGRES
left join Common.ThirdParty tpp with(nolock) on tpp.Nit = ltrim(rtrim(pat.IPCODPACI))
where ING.IESTADOIN = 'F'
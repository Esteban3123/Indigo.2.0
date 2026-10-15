
CREATE VIEW [dbo].[ViewDashBoardPharmacyDevolutionReport]
AS
select d.CODCONCEC Row,
RTRIM(LTRIM(d.IPCODPACI)) PatientCode, RTRIM(LTRIM(pa.IPNOMCOMP)) PatientName,
RTRIM(LTRIM(c.NUMINGRES)) AdmissionNumber,
RTRIM(LTRIM(c.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitCodeName,
cama.NUMCAMHOS BedNumber,
c.FECHDEVOL DevolutionDate,
RTRIM(LTRIM(d.CODPRODUC)) + ' - ' + RTRIM(LTRIM(pro.DESPRODUC)) ProductCodeName,
d.CANDEVOLV DevolutionQuantity, d.CANPENDIE PendingQuantity,
RTRIM(LTRIM(c.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalCodeName, RTRIM(LTRIM(prof.TARJETAPR)) ProfessionalCard
from dbo.HCDEVMEDD d
inner join dbo.HCDEVMEDC c on c.CODCONCEC = d.CODCONCEC
inner join dbo.IHLISTPRO pro on pro.CODPRODUC = d.CODPRODUC
inner join dbo.IHBODEGAS as bo on bo.CODBODEGA = c.CODBODEGA
inner join dbo.INPACIENT AS pa on pa.IPCODPACI  = c.IPCODPACI
inner join dbo.INPROFSAL AS prof on prof.CODPROSAL = c.CODPROSAL 
inner join dbo.INUNIFUNC AS fu on fu.UFUCODIGO  = c.UFUCODIGO	
inner join dbo.ADCENATEN cent on cent.CODCENATE = c.CODCENATE 
inner join dbo.ADINGRESO as ing on ing.NUMINGRES = c.NUMINGRES 
left join dbo.CHCAMASHO cama on cama.CODICAMAS = ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '')
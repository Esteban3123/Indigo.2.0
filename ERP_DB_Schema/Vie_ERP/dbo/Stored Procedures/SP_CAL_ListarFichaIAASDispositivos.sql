-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,10-07-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas de IAAS Dispositivos>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CAL_ListarFichaIAASDispositivos]
(
  @IdFicha as Int
)
AS
BEGIN
  SET NOCOUNT ON;

Select Case B.TIPOIDENTIFICA when 1 then 'X' END AS 'Cédula',CASE B.TIPOIDENTIFICA when 2 then 'X' END AS 'Extranjera',CASE B.TIPOIDENTIFICA when 3 then 'X' END AS 'Tarjeta identidad',CASE B.TIPOIDENTIFICA when 4 then 'X' END AS 'Registro Civil',CASE B.TIPOIDENTIFICA when 5 then 'X' END AS 'Pasaporte',CASE B.TIPOIDENTIFICA when 6 then 'X' END AS 'Adulto sin ID',CASE B.TIPOIDENTIFICA when 7 then 'X' END AS 'Menor sin ID',CASE B.TIPOIDENTIFICA when 8 then 'X' END AS 'NU',
       RTRIM(B.NOMBRECOMP) AS 'Nombre Completo',RTRIM(B.IPCODPACI) AS 'Numero Identificacion',
	   Case A.TIPOUCI WHEN 1 then 'X' end as 'UCI-A ',Case A.TIPOUCI WHEN 2 then 'X' end as 'UCI-P',Case A.TIPOUCI WHEN 3 then 'X' end as 'UCI-N',
	   convert(varchar(10),A.FECHAINGRESOUCI,103) as 'Fecha de Ingreso UCI', 
	   case A.REINGRESO when 1 then 'X' end as 'Si Reingreso',case A.REINGRESO when 0 then 'X' end as 'No Reingreso',
	   case A.PACIENTEREMITIDO when 1 then 'X' end as 'Si Remitido',case A.PACIENTEREMITIDO when 0 then 'X' end as 'No Remitido',
	   case A.CASOIADEXTRAHOSPITA when 1 then 'X' end as 'Si Caso IAD',case A.CASOIADEXTRAHOSPITA when 0 then 'X' end as 'No Caso IAD',RTRIM(A.NOMINSTITUCIONIAD) As 'Nombre Institucion',
	   case A.PESONACER when 1 then 'X' end as '750',case A.PESONACER when 2 then 'X' end as '751',case A.PESONACER when 3 then 'X' end as '1001',case A.PESONACER when 4 then 'X' end as '1501',case A.PESONACER when 5 then 'X' end as '2501',
	   Case A.TIPOIAD when 1 then 'X' end as 'NAV',Case A.TIPOIAD when 2 then 'X' end as 'ISTU-AC',Case A.TIPOIAD when 3 then 'X' end as 'ITS-AC',
	   case A.CRITERIONAV when 1 then 'X' end as 'NEU 1',case A.CRITERIONAV when 2 then 'X' end as 'NEU 2',case A.CRITERIONAV when 3 then 'X' end as 'NEU 3',
	   case A.CRITERIOITS when 1 then 'X' end as 'Criterio 1',case A.CRITERIOITS when 2 then 'X' end as 'Criterio 2',case A.CRITERIOITS when 3 then 'X' end as 'Criterio 3',case A.CRITERIOITS when 4 then 'X' end as 'Criterio 4',
	   case A.CRITERIOITSUAC when 1 then 'X' end as 'ITSUAC 1',case A.CRITERIOITSUAC when 2 then 'X' end as 'ITSUAC 2',case A.CRITERIOITSUAC when 3 then 'X' end as 'ITSUAC 3',case A.CRITERIOITSUAC when 4 then 'X' end as 'ITSUAC 4',
	   convert(varchar(10),A.FECHADIAGNOSTI,103) as 'Fecha Diagnostico', 
	   case A.IADPOLIMICROBIANA when 1 then 'X' end as 'Si polimicrobina',case A.IADPOLIMICROBIANA when 0 then 'X' end as 'No polimicrobina',
	   case A.VENTILADORMECANICO when 1 then 'X' end as 'Si Ventilador Mecanico',case A.VENTILADORMECANICO when 0 then 'X' end as 'No Ventilador Mecanico',
	   convert(varchar(10),A.FECHAINSER1,103) as 'Fecha Insercion 1',
	   convert(varchar(10),A.FECHARETIRO1,103) as 'Fecha retiro 1',
	   case A.CATETERCENTRAL when 1 then 'X' end as 'Si Cateter Central',case A.CATETERCENTRAL when 0 then 'X' end as 'No Cateter Central',
	   convert(varchar(10),A.FECHAINSER2,103) as 'Fecha Insercion 2',
	   convert(varchar(10),A.FECHARETIRO2,103) as 'Fecha retiro 2',
	   case A.CATETERURINARIO when 1 then 'X' end as 'Si Cateter Urinario',case A.CATETERURINARIO when 0 then 'X' end as 'No Cateter Urinario',
	   convert(varchar(10),A.FECHAINSER3,103) as 'Fecha Insercion 3',
	   convert(varchar(10),A.FECHARETIRO3,103) as 'Fecha retiro 3',
	   Case A.CANCER when 1 then 'X' end as 'Cancer',
	   Case A.CORTICOTERAPIA when 1 then 'X' end as 'Corticoterapia',
	   Case A.DESNUTRICION when 1 then 'X' end as 'Desnutrición',
	   Case A.DIABETES when 1 then 'X' end as 'Diabetes',
	   Case A.DIALISIS when 1 then 'X' end as 'Diálisis',
	   Case A.EDADEXTREMA when 1 then 'X' end as 'Edad extrema',
	   Case A.ENFERMEDADRENAL when 1 then 'X' end as 'Enfermedad renal',
	   Case A.EPOC when 1 then 'X' end as 'EPOC',
	   Case A.INMUNOSUPRESION when 1 then 'X' end as 'Inmunosupresión',
	   Case A.PARALISIS when 1 then 'X' end as 'Parálisis',
	   Case A.VIH when 1 then 'X' end as 'VIH-SIDA',
	   Case A.INFEPREVIA when 1 then 'X' end as 'Infección previa',
	   Case A.QUIMIOTERAPIA when 1 then 'X' end as 'Quimioterapia',
	   Case A.TRAUMATISMO when 1 then 'X' end as 'Traumatismo',
	   Case A.OBESIDAD when 1 then 'X' end as 'Obesidad',
	   Case A.PREMATUREZ when 1 then 'X' end as 'Prematurez',
	   Case A.NINGUNO when 1 then 'X' end as 'Ninguno',
	   Case A.OTRO when 1 then 'X' end as 'Otro',
	   Rtrim(A.CUALOTRO) As 'Cual Otro',
	   convert(varchar(10), FECHATOMAMUESTRA1,103) as 'Fecha Toma 1',
	   convert(varchar(10),FECHATOMAMUESTRA2,103) as 'Fecha Toma 2',
	   convert(varchar(10),FECHATOMAMUESTRA3,103) as 'Fecha Toma 3',
	   RTRIM(CODMUESTRA1) as 'Codigo Muestra 1',
	   RTRIM(CODMUESTRA2) as 'Codigo Muestra 2',
	   RTRIM(CODMUESTRA3) as 'Codigo Muestra 3',
	   RTRIM(CODPRUEBA1) as 'Codigo Prueba 1',
	   RTRIM(CODPRUEBA2) as 'Codigo Prueba 2',
	   RTRIM(CODPRUEBA3) as 'Codigo Prueba 3',
	   RTRIM(MICROORGANISMO1) as 'MicroOrganismos 1',
	   RTRIM(MICROORGANISMO2) as 'MicroOrganismos 2',
	   RTRIM(MICROORGANISMO3) as 'MicroOrganismos 3'
from [dbo].[CALIAASDISPOSITIVOS] A 
Inner Join dbo.CALREPORTE B on A.IDCALREPORTE = B.ID
where A.IDCALREPORTE = @IdFicha

END
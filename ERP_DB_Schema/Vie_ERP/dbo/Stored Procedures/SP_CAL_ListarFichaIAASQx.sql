-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,24-07-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas de IAAS Qx>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CAL_ListarFichaIAASQx]
(
  @IdFicha as Int
)
AS
BEGIN
  SET NOCOUNT ON;

Select Case B.TIPOIDENTIFICA when 1 then 'X' END AS 'Cédula',CASE B.TIPOIDENTIFICA when 2 then 'X' END AS 'Extranjera',CASE B.TIPOIDENTIFICA when 3 then 'X' END AS 'Tarjeta identidad',CASE B.TIPOIDENTIFICA when 4 then 'X' END AS 'Registro Civil',CASE B.TIPOIDENTIFICA when 5 then 'X' END AS 'Pasaporte',CASE B.TIPOIDENTIFICA when 6 then 'X' END AS 'Adulto sin ID',CASE B.TIPOIDENTIFICA when 7 then 'X' END AS 'Menor sin ID',CASE B.TIPOIDENTIFICA when 8 then 'X' END AS 'NU',
       RTRIM(B.NOMBRECOMP) AS 'Nombre Completo',RTRIM(B.IPCODPACI) AS 'Numero Identificacion',
	   Case A.COMPLEJIDAD WHEN 1 then 'X' end as 'Baja',Case A.COMPLEJIDAD WHEN 2 then 'X' end as 'Media',Case A.COMPLEJIDAD WHEN 3 then 'X' end as 'Alta',
	   case A.SERVICIOPROCEDIMIENTO when 1 then 'X' end as 'Programado ambulatorio',case A.SERVICIOPROCEDIMIENTO when 2 then 'X' end as 'Urgencias',case A.SERVICIOPROCEDIMIENTO when 3 then 'X' end as 'Programado hospitalizado',
	   case A.PROCEDIMIENTOMEDICO when 1 then 'X' end as 'Cesárea',case A.PROCEDIMIENTOMEDICO when 2 then 'X' end as 'Herniorrafia',case A.PROCEDIMIENTOMEDICO when 3 then 'X' end as 'Parto',case A.PROCEDIMIENTOMEDICO when 4 then 'X' end as 'Revascularización',case A.PROCEDIMIENTOMEDICO when 5 then 'X' end as 'Colecistectomía',case A.PROCEDIMIENTOMEDICO when 6 then 'X' end as 'Otro Procedimiento medico',
	   case A.DIABETES WHEN 1 THEN 'X' END AS 'Diabetes',
	   case A.INMUNOSUPRESION WHEN 1 THEN 'X' END AS 'Inmunosupresión',
	   case A.OBESIDAD WHEN 1 THEN 'X' END AS 'Obesidad',
	   case A.DESNUTRICION WHEN 1 THEN 'X' END AS 'Desnutrición',
	   case A.PRECLAMPSIA WHEN 1 THEN 'X' END AS 'Preeclampsia',
	   case A.ANEMIA WHEN 1 THEN 'X' END AS 'Anemia',
	   case A.CLASIFICACIONASA WHEN 1 THEN 'X' END AS 'ASA 1',
	   case A.CLASIFICACIONASA WHEN 2 THEN 'X' END AS 'ASA 2',
	   case A.CLASIFICACIONASA WHEN 3 THEN 'X' END AS 'ASA 3',
	   case A.CLASIFICACIONASA WHEN 4 THEN 'X' END AS 'ASA 4',
	   case A.CLASIFICACIONASA WHEN 5 THEN 'X' END AS 'ASA 5',
	   case A.TIPOHERIDA WHEN 1 THEN 'X' END AS 'Limpia',
	   case A.TIPOHERIDA WHEN 2 THEN 'X' END AS 'Limpia contaminada',
	   case A.TIPOHERIDA WHEN 3 THEN 'X' END AS 'Herida contaminada ',
	   case A.TIPOHERIDA WHEN 4 THEN 'X' END AS 'Herida sucia',
	   Rtrim(A.DURACIONPROCEDIMIENTO) AS 'Duracion',
	   case A.SUPERFICIEPRIMARIA WHEN 1 THEN 'X' END AS 'Superficial primaria',
	   case A.SUPERFICIESECUNDAR WHEN 1 THEN 'X' END AS 'Superficial secundaria ',
	   case A.PROFUNDAPRIMARIA WHEN 1 THEN 'X' END AS 'Profunda primaria',
	   case A.PROFUNDASECUNDARIA WHEN 1 THEN 'X' END AS 'Profunda secundaria',
	   case A.ORGANOESPACIO WHEN 1 THEN 'X' END AS 'Organo espacio',
       case A.PROFIAXISANTIBIOTICA when 1 then 'X' end as 'Si Profilaxis',case A.PROFIAXISANTIBIOTICA when 0 then 'X' end as 'No Profilaxis',
	   Rtrim(A.CUAL) AS 'Cual Profilaxis',
	   case A.TIEMPOANTIBIOTICO WHEN 1 THEN 'X' END AS 'Antes',
	   case A.TIEMPOANTIBIOTICO WHEN 2 THEN 'X' END AS 'Durante',
	   case A.TIEMPOANTIBIOTICO WHEN 3 THEN 'X' END AS 'Después',
	   case A.TIEMPOANTIBIOTICO WHEN 4 THEN 'X' END AS 'Ninguna',
	   case A.REQUIRIONUEVAINTERVEN when 1 then 'X' end as 'Si requierio intervencion',case A.REQUIRIONUEVAINTERVEN when 0 then 'X' end as 'No requierio intervencion',
	   convert(varchar(10), FECHATOMAMUESTRA1,103) as 'Fecha Toma 1',
	   convert(varchar(10),FECHATOMAMUESTRA2,103) as 'Fecha Toma 2',
	   convert(varchar(10),FECHATOMAMUESTRA3,103) as 'Fecha Toma 3',
	   RTRIM(A.CODIGOMUESTRA1) as 'Codigo Muestra 1',
	   RTRIM(A.CODIGOMUESTRA2) as 'Codigo Muestra 2',
	   RTRIM(A.CODIGOMUESTRA3) as 'Codigo Muestra 3',
	   RTRIM(A.CODIGOPRUEBA1) as 'Codigo Prueba 1',
	   RTRIM(A.CODIGOPRUEBA2) as 'Codigo Prueba 2',
	   RTRIM(A.CODIGOPRUEBA3) as 'Codigo Prueba 3',
	   RTRIM(A.MICROORGANISMOAISLADO1) as 'MicroOrganismos 1',
	   RTRIM(A.MICROORGANISMOAISLADO2) as 'MicroOrganismos 2',
	   RTRIM(A.MICROORGANISMOAISLADO3) as 'MicroOrganismos 3'
from [dbo].[CALIAASQX] A 
Inner Join dbo.CALREPORTE B on A.IDCALREPORTE = B.ID
where A.IDCALREPORTE = @IdFicha

END
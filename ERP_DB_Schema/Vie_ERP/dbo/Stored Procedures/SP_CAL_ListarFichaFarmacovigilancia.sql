-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,09-07-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas de tecnovigilancia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CAL_ListarFichaFarmacovigilancia]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;

  declare @FechaActual as date = [Common].[GETDATE]()		
select 
CASE C.TIPOIDENTIFICA when 1 then 'X' end as 'CedulaCiudadania', case C.TIPOIDENTIFICA when 2 then 'X' end as 'Extranjeria', case C.TIPOIDENTIFICA when 3 then 'X' end as 'Tarjetaidentidad',case C.TIPOIDENTIFICA when 4 then 'X' end as 'RegistroCivil',case C.TIPOIDENTIFICA when 5 then 'X' end as 'Pasaporte',case C.TIPOIDENTIFICA when 6 then 'X' end as 'AdultoSinIdentificacion',case C.TIPOIDENTIFICA when 7 then 'X' end as 'MenorSinIdentificacion',case C.TIPOIDENTIFICA when 8 then 'X' end as 'NUIP'
,Case SEXO when 1 then 'X' end as 'Masculino', Case SEXO when 2 then 'X' end as 'Femenino',Case SEXO when 2 then 'X' end as 'S/I'
,[dbo].[EDAD] (C.FECNACIMIENTO,@FechaActual) As 'Edad'
,c.IPCODPACI as 'IdentificacionPaciente'
,C.NOMBRECOMP as 'NombrePaciente'
,convert(varchar(20),FECHANACIMIENTO,103)  as FECHANACIMIENTO 
,Rtrim(E.nomdepart) +' , '+ Rtrim(P.MUNNOMBRE)  as 'Departamento Municipio reportante'
,NOMBREINSTITUCION 
,CODIGOPNF 
,NOMBREREPORTANTE 
,Rtrim(z.desactivi) as 'Profesion reportante'
,CORREOREPORTANTE 
,PESO
,TALLA 
,rtrim(D.CODDIAGNO) +' - '+ rtrim(D.NOMDIAGNO) as Diagnostico
,TITULARREGISTRO
,NOMBRECOMERCIAL
,REGISTROSANITARIO
,LOTE
,convert(varchar(20),FECHAINICIOEVENTO,103)  as FECHAINICIOEVENTO 
,A.DESCRIPCION 
,CASE DESRECUPSINSECUELAS WHEN '1' THEN 'X' END AS 'DesEnlace_RecuperadoSINsecuelas'
,CASE DESRECUPCONSECUELAS WHEN '1' THEN 'X' END AS 'DesEnlace_RecuperadoCONsecuelas'
,CASE DESRECUPRESOLVIENDO WHEN '1' THEN 'X' END AS 'DesEnlace_RecuperadoResolviendo'
,CASE DESNORECUPERADO WHEN '1' THEN 'X' END AS 'DesEnlace_NOrecuperado'
,CASE DESFATAL WHEN '1' THEN 'X' END AS 'DesEnlace_Fatal'
,CASE DESDESCONOCIDO WHEN '1' THEN 'X' END AS 'DesEnlace_Desconocido'
,CASE SERIPRODUJO WHEN '1' THEN 'X' END AS 'Serieded_Produjo'
,CASE SERIADNOMALIA WHEN '1' THEN 'X' END AS 'Serieded_Anomalia'
,CASE AMENAZAVIDA WHEN '1' THEN 'X' END AS 'Serieded_Amenazavida'
,CASE SERIAMENAZAMUERTE WHEN '1' THEN 'X' END AS 'Serieded_AmenzaMuerte'
,CASE SERIPRODUJODISCAPAC WHEN '1' THEN 'X' END AS 'Serieded_Dsicapacidad'
,CASE EVENTODESPUES WHEN '1' THEN 'X' END AS 'Pregunta1_SI',CASE EVENTODESPUES WHEN '2' THEN 'X' END AS 'Pregunta1_NO',CASE EVENTODESPUES WHEN '3' THEN 'X' END AS 'Pregunta1_NoSabe'
,CASE OTROSFACTORES WHEN '1' THEN 'X' END AS 'Pregunta2_SI',CASE OTROSFACTORES WHEN '2' THEN 'X' END AS 'Pregunta2_NO',CASE OTROSFACTORES WHEN '3' THEN 'X' END AS 'Pregunta2_NoSabe'
,CASE EVENTODESAPARE WHEN '1' THEN 'X' END AS 'Pregunta3_SI',CASE EVENTODESAPARE WHEN '2' THEN 'X' END AS 'Pregunta3_NO',CASE EVENTODESAPARE WHEN '3' THEN 'X' END AS 'Pregunta3_NoSabe'
,CASE PACIENTEMISMAREACC WHEN '1' THEN 'X' END AS 'Pregunta4_SI',CASE PACIENTEMISMAREACC WHEN '2' THEN 'X' END AS 'Pregunta4_NO',CASE PACIENTEMISMAREACC WHEN '3' THEN 'X' END AS 'Pregunta4_NoSabe'
,CASE AMPLIARINFORMAC WHEN '1' THEN 'X' END AS 'Pregunta5_SI',CASE AMPLIARINFORMAC WHEN '2' THEN 'X' END AS 'Pregunta5_NO',CASE AMPLIARINFORMAC WHEN '3' THEN 'X' END AS 'Pregunta5_NoSabe'
,Ctc.CODIGO + ' - ' + Ctc.DESCRIPCION AS 'EventoAdverso',
convert(varchar(10),A.FECHAMUERTE,103) as 'Fecha muerte',
(select  LEFT(IPPRINOMB,1) +' '+ LEFT(IPSEGNOMB,1) +' '+ LEFT(IPPRIAPEL,1) +' '+ LEFT(IPSEGAPEL,1)  from INPACIENT WHERE IPCODPACI = C.IPCODPACI ) as 'Iniciales'
from [dbo].[CALFARMACOVIGILANCIA]  A
	Inner Join dbo.CALREPORTE C on A.IDCALREPORTE = C.ID
	Inner Join dbo.INUBICACI B ON A.ORIGENREPORTE = B.AUUBICACI
	Inner Join dbo.INMUNICIP P on B.DEPMUNCOD = P.DEPMUNCOD
	Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
	left join dbo.ADACTIVID z on A.PROFESIONREPORTANTE  = z.codactivi
	inner join dbo.INDIAGNOS D on D.CODDIAGNO = A.CODDIAGNO
	inner join dbo.CALTIPOCLASE Ctc on ctc.Id = C.IDTIPO
where A.idcalreporte = @IdFicha 
	
--select * from [dbo].[CALFARMACOVIGILANCIA]
--select * from [dbo].[CALREPORTE] where id =32
--select * from [dbo].[CALFARMAMEDICAMEN] 
--select * from [dbo].[CALTIPOCLASE]


	
END
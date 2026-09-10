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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera y presenta el detalle completo de una ficha de farmacovigilancia identificada por su ID, consolidando en un único resultado toda la información necesaria para imprimir o visualizar el reporte oficial de un evento adverso o reacción a medicamentos. Integra los datos del paciente (tipo de identificación, cédula, nombre, sexo, edad, fecha de nacimiento e iniciales), la ubicación geográfica del reporte (municipio y departamento), los datos del reportante (institución, nombre, profesión, correo), las características del medicamento involucrado (nombre comercial, registro sanitario, lote, titular del registro), el diagnóstico CIE-10 asociado, la descripción del evento adverso con su categoría según el catálogo de tipos de calidad, el desenlace clínico del paciente (recuperado sin secuelas, con secuelas, fatal, desconocido, entre otros), la gravedad o seriedad del evento (anomalía congénita, amenaza de muerte, discapacidad, etc.) y las respuestas a las preguntas de causalidad (reexposición, factores concurrentes, desaparición al suspender el medicamento). Se usa principalmente para generar la ficha oficial de farmacovigilancia que las instituciones de salud deben diligenciar y reportar ante las autoridades sanitarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos consolidados de una ficha de farmacovigilancia (paciente, reportante, medicamento, evento adverso, desenlace, seriedad y preguntas de causalidad) en el formato de impresión INVIMA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en CALFARMACOVIGILANCIA cuyo IDCALREPORTE coincida con el identificador recibido; El reporte debe tener relaciones válidas con ubicación (INUBICACI), municipio (INMUNICIP), departamento (INDEPARTA), diagnóstico (INDIAGNOS) y tipo/clase de calidad (CALTIPOCLASE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna información de un único reporte de farmacovigilancia (filtrado por identificador de ficha); La edad se calcula contra la fecha actual del sistema obtenida vía Common.GETDATE(); Las iniciales del paciente se construyen con la primera letra de cada componente del nombre/apellidos; El departamento y municipio reportante se arman concatenando el origen del reporte con su jerarquía geográfica; El diagnóstico se presenta como ''código - nombre'' y el evento adverso como ''código - descripción'' del tipo/clase; La profesión del reportante es opcional (LEFT JOIN); el resto de relaciones son obligatorias (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Farmacovigilancia; Tecnovigilancia; Evento adverso; Reacción adversa a medicamento; Reporte de paciente; Diagnóstico CIE; Desenlace clínico; Seriedad del evento; Causalidad; Identificación del paciente; Profesión del reportante; Ubicación geográfica (departamento/municipio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.idcalreporte = @IdFicha, retorna una fila con los datos del paciente, reportante, ubicación, diagnóstico, medicamento, evento adverso y marcas ''X'' para tipo de identificación, sexo, desenlace, seriedad y preguntas de causalidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOIDENTIFICA del reporte (1..8) → Marca con ''X'' la casilla del tipo de documento correspondiente (CC, CE, TI, RC, Pasaporte, Adulto sin id, Menor sin id, NUIP); si SEXO = 1 / 2 → Marca ''X'' en Masculino o Femenino (la casilla S/I también se marca cuando SEXO=2); si Campos de desenlace, seriedad y preguntas de causalidad = ''1''/''2''/''3'' → Se marca ''X'' en la casilla correspondiente del formato (recuperado, fatal, amenaza vida, SI/NO/No sabe, etc.)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.EDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALFARMACOVIGILANCIA; dbo.CALREPORTE; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.ADACTIVID; dbo.INDIAGNOS; dbo.CALTIPOCLASE; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilancia';
-- GO

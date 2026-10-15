
CREATE PROCEDURE [dbo].[SPCH_PacientesEscalas]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;


	SELECT TOP 1 '001' AS Codigo , 'Escala Bieri' AS Escala
	from HCESCABIE
	where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	UNION ALL
	select TOP 1 '002' as Codigo, 'Escala Apache' as Escala 
	from HCESCAPAC
	where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	UNION ALL
	SELECT TOP 1 '003' AS Codigo , 'Escala DownTon' as Escala
	from HCESCDOWN
	where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	UNION ALL
	SELECT TOP 1 '004' AS Codigo , 'Escala NorTon' as Escala
	from HCESCNTON
	where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	UNION ALL
	SELECT TOP 1 '005' AS Codigo, 'Escala RASS' as Escala
	FROM HCESCRASS
	 where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	 SELECT TOP 1 '006' AS Codigo, 'Escala VAS' as Escala
	FROM HCESCRASS
	 where IPCODPACI=@Paciente and NUMINGRES=@Ingreso

--SELECT case   when dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI)>=1 then 'El Paciente Presenta Riesgo de Caidas, Puntaje '  + RTRIM(cast( dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as char)) end as   PUNTAJEDOWN,
--case  dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI)  when 0 then 'Sedacion : El paciente esta alerta, tranquilo' when 1 then 'Sedacion : El paciente esta inquieto o agitado' WHEN 2 THEN  'Sedacion : El paciente esta inquieto o agitado' WHEN 3 THEN  'Sedacion : El paciente esta inquieto o agitado' WHEN 4  THEN  'Sedacion : El paciente esta inquieto o agitado' WHEN -1 THEN 'Sedacion : El paciente se despierta y  abre los ojos, manteniendo contacto visual durante mas de 10 segundos' WHEN -2 THEN 'Sedacion : El paciente se despierta y  abre los ojos, manteniendo contacto visual durante menos de 10 segundos' WHEN -3 THEN 'Sedacion : El paciente se mueve a la llamada pero sin abrir los ojos' WHEN -4 THEN 'Sedacion : El paciente se mueve ante el estimulo fisico' WHEN -5 THEN 'Sedacion : El paciente no se mueve ante ningun estimulo' END AS   EscalaRASS
--FROM  dbo.ADINGRESO J
--WHERE J.IPCODPACI=@Paciente AND j.NUMINGRES = @Ingreso 
end
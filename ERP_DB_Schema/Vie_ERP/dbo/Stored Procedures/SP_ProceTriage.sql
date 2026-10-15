
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProceTriage] 
	-- Add the parameters for the stored procedure here
	@fecha_ini date,
	@fecha_fin date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT        
T.TRIANUMER AS 'CONSECUTIVO DE REGISTRO(NUMEO DE TRIAGE)',
'TIPO DE IDENTIFICACION' = CASE INP.IPTIPODOC
WHEN '1' THEN 'Cédula de Ciudadanía'
WHEN '2' THEN 'Cédula de Extranjería'
WHEN '3' THEN 'Tarjeta de Identidad'
WHEN '4' THEN 'Registro Civil'
WHEN '5' THEN 'Pasporte'
WHEN '6' THEN 'Adulto Sin Identificación'
ELSE 'Menor Sin Identificación'
END,
T.IPCODPACI AS 'NUMERO DE IDENTIFICACION',
CAST(INP.IPFECNACI AS date) AS 'FECHA DE NACIMINETO',
'SEXO PACIENTE'= CASE INP.IPSEXOPAC
         WHEN '1' THEN 'HOMBRE'
		 WHEN '2' THEN 'MUJER'
         ELSE 'INDEFINIDO'
		 end,
INP.IPPRIAPEL AS 'PRIMER APELLIDO',
INP.IPSEGAPEL as 'SEGUNDO APELLIDO',
INP.IPPRINOMB as 'PRIMER NOMBRE',
INP.IPSEGNOMB as 'SEGUNDO NOMBRE',
T.CODENTIDA AS 'CODIGO DE LA EAPB',
INTE.NOMENTIDA AS 'NOMBRE DE LA EAPB',
CAST(T.TRIAFECHA AS DATE) AS 'FECHA DE CLASIFICACION TRIAGE',
CAST(T.TRIAFECHA AS time) AS 'HORA DE CLASIFICACION TRIAGE',
CAST(T.TRIFECCON AS DATE) AS 'FECHA DE ATENCION EN CONSULTA URGENCIAS',
CAST(T.TRIFECCON AS time) AS 'HORA DE ATENCION EN CONSULTA URGENCIAS'
                       
FROM            ADTRIAGEU AS T INNER JOIN
                         INPROFSAL AS P ON T.CODPROSAL = P.CODPROSAL LEFT OUTER JOIN
                         ADCONTURG AS C ON T.CODCONCEC = C.CODCONCEC INNER JOIN
                         INUNIFUNC AS UF ON C.UFUCODIGO = UF.UFUCODIGO INNER JOIN
                         INPACIENT AS INP ON T.IPCODPACI = INP.IPCODPACI INNER JOIN
						 INENTIDAD AS INTE ON INTE.CODENTIDA = T.CODENTIDA
WHERE        (T.TRIAFECHA BETWEEN @fecha_ini AND @fecha_fin) 
END
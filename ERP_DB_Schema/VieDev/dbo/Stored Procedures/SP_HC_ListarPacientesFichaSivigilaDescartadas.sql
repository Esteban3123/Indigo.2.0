-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,22-05-2018,>
-- Description:	<Description,Sp que me lista los pacientes que tienen ficha del Sivigila descartadas, esto para el Dashboard de Epidemiologia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesFichaSivigilaDescartadas]
(
  @CentroAtencion as varchar(10),
  @FechaInicial datetime,
  @FechaFinal datetime
)

AS
BEGIN
  SET NOCOUNT ON;

select C.ID,FECHACREACION As 'Fecha notificacion',C.FECHADESCARTE As 'Fecha Descartada',Rtrim(Z.NOMBRE) AS 'Motivo descarte', (C.IPCODPACI) As 'Identificacion', Rtrim(B.IPNOMCOMP) As 'Nombre Paciente',cast(datediff(dd,B.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int) As 'Edad',
Rtrim(E.NOMCENATE) As 'Centro Atencion',Rtrim(F.UFUDESCRI) As 'Unidad Funcional',Rtrim(C.NOMBEVENTO) As 'Nombre Evento', Rtrim(C.CODEVENTO) As 'Codigo Evento', CASE RTRIM(CODEVENTO) WHEN '356_D' THEN '356' WHEN '875_D' THEN '875'  WHEN '903_D' THEN '903' ELSE RTRIM(CODEVENTO) END AS CodigoVisible , 
CASE CLASIFICACIONCASO WHEN '1' THEN 'Sospechoso' WHEN '2' THEN 'Probable' WHEN '3' THEN 'Conf laboratorio'  WHEN '4' THEN 'Conf clinica' WHEN '5' THEN 'Conf epidemiológico' END AS 'Clasificacion',
Rtrim(G.NOMMEDICO) As 'Nombre Medico',Rtrim(C.CODCENATE) as 'Codigo CA',Rtrim(C.UFUCODIGO) As 'Codigo UF',Rtrim(C.NUMINGRES) As 'Ingreso',Rtrim(C.CODDIAGNO) As 'Codigo Diagnostico'
From HCFICHANOTIFICACION C
  Inner Join INPACIENT B ON C.IPCODPACI = B.IPCODPACI  
  Inner Join ADINGRESO D ON C.NUMINGRES = D.NUMINGRES
  Inner Join ADCENATEN E ON C.CODCENATE = E.CODCENATE
  Inner Join INUNIFUNC F ON C.UFUCODIGO = F.UFUCODIGO
  Inner Join INPROFSAL G ON C.CODUSUARIO = G.CODPROSAL
  Inner Join HCDESCARTEFICHA Z ON C.MOTIVODESCAR  = Z.ID where C.ESTADO = 3 AND FECHADESCARTE BETWEEN @FechaInicial AND @FechaFinal 

END
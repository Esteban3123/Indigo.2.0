-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,28-10-2019,>
-- Description:	<Description,Sp que me lista los pacientes de historias clinicas dinamicas>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesHistoricoHCDinamicas]
(
  @CentroAtencion as varchar(MAX),
  @IdModeloHC as varchar,
  @FechaInicial as Date,
  @FechaFinal as Date
)

AS
BEGIN
  SET NOCOUNT ON;

select IDMODELOHC, RTRIM(A.IPCODPACI) AS 'CODIGO PACIENTE',RTRIM(B.IPNOMCOMP) AS 'NOMBRE PACIENTE',A.NUMINGRES AS 'INGRESO',Rtrim(D.NOMENTIDA) AS 'NOMBRE ENTIDAD INGRESO',A.NUMEFOLIO AS 'Folio',
	   A.FECHISPAC AS 'Fecha Historia',E.NOMCENATE as 'Centro Atencion', F.UFUDESCRI AS 'Unidad Funcional', G.NOMDIAGNO as 'Diagnostico Principal', B.IPDIRECCI AS 'Direccion Paciente',IPFECNACI as 'Fecha Nacimiento',
	   C.IFECHAING AS 'Fecha Ingreso',h.NOMMEDICO as 'Nombre Medico'
from HCHISPACA A 
	INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI  
	INNER JOIN ADINGRESO C ON C.NUMINGRES = A.NUMINGRES   
	INNER JOIN INENTIDAD D ON D.CODENTIDA = C.CODENTIDA 
	INNER JOIN ADCENATEN E ON E.CODCENATE = A.CODCENATE  
	INNER JOIN INUNIFUNC F ON F.UFUCODIGO = A.UFUCODIGO 
	INNER JOIN INDIAGNOS G ON G.CODDIAGNO = A.CODDIAGNO 
	INNER JOIN INPROFSAL h ON H.CODPROSAL = A.CODPROSAL
WHERE ( A.IDMODELOHC = @IdModeloHC and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion))  AND A.FECHISPAC between @FechaInicial and @FechaFinal) 


END
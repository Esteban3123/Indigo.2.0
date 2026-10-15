
-- =============================================
-- Author:		Juan David Patiño Cabrera
-- Create date: 23-10-2017
-- Description:	Sp que me cargara las Salas Agendadas, adicionalemte se cargara los pacientes que estas para la sala.
-- =============================================

CREATE PROCEDURE [dbo].[SPHC_ListarSalasAgendadasCirugia]
(
@FechaFiltro datetime 
)
AS
BEGIN
	SET NOCOUNT ON;

/*Select Rtrim(DESCRIPSAL) As 'Nombre Sala',CONVERT(VARCHAR(5), FECHORAIN,108) AS 'Hora', Rtrim(A.IPCODPACI) As IPCODPACI,Rtrim(IPNOMCOMP) As 'Nombre Paciente',Rtrim(E.CODSERIPS) +'-'+ Rtrim(DESSERIPS) As 'Procedimiento','Programada' As 'Prioridad', Rtrim(NOMMEDICO) As 'Profesional',Rtrim(G.DESESPECI) As 'Especialidad', ' ' As 'Unidad Funcional', ' ' As NUMINGRES, '' as AUTO from [dbo].AGEPROGQX A
		INNER JOIN AGENSALAC B ON A.AGENSALAC = B.CODCONCEC 
		INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI
		INNER JOIN INCUPSIPS  E ON A.CODSERIPS = E.CODSERIPS
		INNER JOIN INPROFSAL   F ON A.CODPROSAL  = F.CODPROSAL
		INNER JOIN  INESPECIA G ON A.CODESPECI = G.CODESPECI   WHERE CONVERT(VARCHAR(20),FECHORAIN,103) = @FechaFiltro 
UNION ALL*/
   Select Rtrim(DESCRIPSAL) As 'Nombre Sala',CONVERT(VARCHAR(5), FECHORAIN,108) AS 'Hora', Rtrim(A.IPCODPACI) As IPCODPACI,Rtrim(IPNOMCOMP) As 'Nombre Paciente',Rtrim(E.CODSERIPS) +'-'+ Rtrim(DESSERIPS) As 'Procedimiento',CASE PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' else 'Porgramada'  END AS 'Prioridad', Rtrim(NOMMEDICO) As 'Profesional',Rtrim(G.DESESPECI) As 'Especialidad', Rtrim(J.UFUDESCRI) AS 'Unidad Funcional', I.NUMINGRES  As NUMINGRES,AUTO from [dbo].AGEPROGQX A
		INNER JOIN AGENSALAC B ON A.AGENSALAC = B.CODCONCEC 
		INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI
		INNER JOIN INCUPSIPS  E ON A.CODSERIPS = E.CODSERIPS
		INNER JOIN INPROFSAL   F ON A.CODPROSAL  = F.CODPROSAL
		INNER JOIN INESPECIA G ON A.CODESPECI = G.CODESPECI   
		LEFT JOIN HCORDPROQ H ON A.AUTOHCORDPROQ = H.AUTO
		LEFT JOIN ADINGRESO  I ON H.NUMINGRES  = I.NUMINGRES
		LEFT JOIN INUNIFUNC J ON I.UFUCODIGO = J.UFUCODIGO  	WHERE CONVERT(VARCHAR(20),FECHORAIN,103) = @FechaFiltro 
END
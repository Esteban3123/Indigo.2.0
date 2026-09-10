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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial de historias clínicas dinámicas registradas para pacientes, filtrando por centro de atención, modelo de historia clínica y rango de fechas. Para cada registro devuelve: código y nombre del paciente, número de ingreso, entidad aseguradora o pagadora, número de folio, fecha de la historia, centro de atención, unidad funcional (servicio o sala), diagnóstico principal (CIE-10), dirección y fecha de nacimiento del paciente, fecha de ingreso y nombre del médico tratante. Integra información de historias clínicas (HCHISPACA), datos del paciente (INPACIENT), admisiones (ADINGRESO), entidades pagadoras (INENTIDAD), sedes (ADCENATEN), unidades funcionales (INUNIFUNC), diagnósticos (INDIAGNOS) y profesionales de la salud (INPROFSAL). Se usa principalmente para reportería y auditoría de historias clínicas dinámicas por sede y período.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con sus historias clínicas dinámicas registradas para un modelo de HC, filtrando por uno o varios centros de atención y un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La lista de centros de atención debe venir en un formato que dbo.splitstring pueda parsear.; Debe existir el modelo de historia clínica indicado y centros de atención válidos para que el resultado no sea vacío.; El rango de fechas debe ser coherente (FechaInicial ≤ FechaFinal) para obtener resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna historias clínicas dinámicas que tengan paciente, ingreso, entidad, centro de atención, unidad funcional, diagnóstico y profesional asociados (al usar INNER JOIN se excluyen registros con relaciones incompletas).; Filtra exclusivamente por el modelo de historia clínica indicado.; Restringe el resultado a los centros de atención contenidos en la lista delimitada recibida.; Solo incluye historias cuya fecha esté dentro del rango (inclusivo) entre fecha inicial y fecha final.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica dinámica; Modelo de historia clínica; Ingreso; Entidad de ingreso (asegurador/pagador); Centro de atención; Unidad funcional; Diagnóstico principal; Profesional médico; Folio de historia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Cuando la historia coincide con el modelo de HC, el centro de atención está dentro de la lista recibida y la fecha está entre las fechas indicadas, retorna datos del paciente, ingreso, entidad, centro, unidad funcional, diagnóstico y médico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INDIAGNOS; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesHistoricoHCDinamicas';
-- GO

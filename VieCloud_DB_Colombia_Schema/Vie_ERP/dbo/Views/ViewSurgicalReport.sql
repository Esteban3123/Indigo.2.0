

CREATE VIEW [dbo].[ViewSurgicalReport]
AS
select IDETIPHIS,NUMEFOLIO,IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODDIAPRE,CODDIAPOS,a.CODPROSAL,b.CODIGONIT,b.CODESPEC1 as CODESPEC,CODSERIPS,TIPOANEST,FECHORINI,FECHORFIN,TIPHERCIR,SALACIRUG,URGECIRUG,CLASIFASA,PROFIANTI,PROTEIMPL,CXCADERAS,CXRODILLA,LAPAROTOM,FRACTABIE,CLAFRACAB,DESVIAABO,DESHALLOP,DESPROCED,DESCOMPIC,CONTEOMAT,DESCOMPRE,DESCGASAS,PREPATOLO,NUMCANMUE,OBSMUESTR,MATERADIC,[AUTO],GENSERVICEORDER
, a.CODPROSAL as MedicoRealizo
, FECHORINI as FechaRealizacion
, CASE WHEN a.CODPROSAL IS NULL THEN 0 ELSE 1 END as Realizo
from dbo.HCQXINFOR a 
inner join dbo.INPROFSAL b on a.CODPROSAL = b.CODPROSAL 

union all 

select a.IDETIPHIS,a.NUMEFOLIO,a.IPCODPACI,INGMH.NUMINGRES,a.CODCENATE,a.UFUCODIGO,CODDIAPRE,CODDIAPOS,a.CODPROSAL,b.CODIGONIT,b.CODESPEC1 as CODESPEC,CODSERIPS,TIPOANEST,FECHORINI,FECHORFIN,TIPHERCIR,SALACIRUG,URGECIRUG,CLASIFASA,PROFIANTI,PROTEIMPL,CXCADERAS,CXRODILLA,LAPAROTOM,FRACTABIE,CLAFRACAB,DESVIAABO,DESHALLOP,DESPROCED,DESCOMPIC,CONTEOMAT,DESCOMPRE,DESCGASAS,PREPATOLO,NUMCANMUE,OBSMUESTR,MATERADIC,[AUTO],GENSERVICEORDER
, a.CODPROSAL as MedicoRealizo
, FECHORINI as FechaRealizacion
, CASE WHEN a.CODPROSAL IS NULL THEN 0 ELSE 1 END as Realizo
from dbo.HCQXINFOR a
inner join dbo.INPROFSAL b on a.CODPROSAL = b.CODPROSAL 
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del informe quirúrgico que consolida los datos clínicos y operativos de cada cirugía realizada, combinando el informe quirúrgico (HCQXINFOR) con la información del cirujano o profesional responsable (INPROFSAL). Integra dos fuentes: cirugías de pacientes con ingreso directo y cirugías asociadas a ingresos de recién nacidos, permitiendo trazabilidad completa en ambos escenarios. Para cada acto quirúrgico expone: identificación y número de ingreso del paciente, centro de atención, unidad funcional, diagnósticos preoperatorio y posoperatorio (CIE-10), código del procedimiento (CUPS), tipo de anestesia, sala de cirugía, horario de inicio y fin, tipo de herida, clasificación ASA, profilaxis antibiótica, uso de prótesis o implantes, hallazgos, descripción del procedimiento, complicaciones, conteo de materiales, muestras de patología y datos del cirujano (código, NIT y especialidad). Sirve como fuente principal para reportes quirúrgicos, auditoría clínica, RIPS de procedimientos quirúrgicos y seguimiento de indicadores de sala de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewSurgicalReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewSurgicalReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los informes quirúrgicos asociándolos al profesional que los realizó, incluyendo tanto los ingresos directos del paciente como los ingresos de recién nacidos vinculados al ingreso de la madre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El informe quirúrgico debe tener un profesional (CODPROSAL) válido existente en el maestro de profesionales de la salud para ser visible en la vista (INNER JOIN).; Para la rama de recién nacidos, debe existir vínculo madre-hijo en HCINGRESORECNAC, registro clínico en HCRECINAC y un ingreso administrativo en ADINGRESO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen informes quirúrgicos cuyo profesional ejecutante existe en el maestro de profesionales (INPROFSAL).; La especialidad reportada corresponde siempre a la especialidad principal (CODESPEC1) del profesional.; El médico que realizó (MedicoRealizo) y la fecha de realización (FechaRealizacion) se derivan del profesional y la fecha de inicio del informe quirúrgico.; La rama de recién nacidos reemplaza el NUMINGRES del informe por el NUMINGRESHIJO obtenido del vínculo madre-hijo.; El uso de UNION ALL permite que un mismo informe pueda aparecer duplicado si cumple ambos criterios (ingreso propio y vínculo de recién nacido).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Informe quirúrgico; Profesional de la salud; Especialidad médica; Procedimiento quirúrgico; Ingreso/Admisión; Recién nacido; Vínculo madre-hijo; Tipo de anestesia; Sala de cirugía; Clasificación ASA; Profilaxis antibiótica; Prótesis/implantes; Diagnóstico pre y postoperatorio; Muestras patológicas; Cirugía de urgencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewSurgicalReport: Devuelve el conjunto de informes quirúrgicos unidos al profesional ejecutante y, vía UNION ALL, también los informes asociados a ingresos de recién nacidos relacionados con el ingreso de la madre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si a.CODPROSAL IS NULL → El campo Realizo se marca como 0 (no realizado) else El campo Realizo se marca como 1 (realizado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INPROFSAL; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalReport';
GO

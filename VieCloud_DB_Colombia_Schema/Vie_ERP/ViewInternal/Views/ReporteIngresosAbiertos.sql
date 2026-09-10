

CREATE view [ViewInternal].[ReporteIngresosAbiertos]
as 
(
SELECT        ADINGRESO.IPCODPACI AS [DOC PACIENTE], INPACIENT.IPNOMCOMP AS [NOMBRE PACIENTE], ADINGRESO.NUMINGRES AS INGRESO, 
                         CASE ADINGRESO.IESTADOIN WHEN ' ' THEN 'No Facturado' WHEN 'F' THEN 'Facturado' WHEN 'A' THEN 'Anulado' when 'P' then 'Facturado Parcial' when 'C' then 'Cerrado'
                          END AS [ESTADO INGRESO], ADINGRESO.IFECHAING AS [FECHA INGRESO PACIENTE], ADINGRESO.CODUSUCRE AS [COD USUARIO CREA], 
                         SEGusuaru_2.NOMUSUARI AS [USUARIO CREA], 
                         CASE WHEN ADINGRESO.UFUCODIGO = '01' THEN 'URGENCIAS CONSULTA Y PROCEDIMIENTOS' WHEN ADINGRESO.UFUCODIGO = '031' THEN 'URGENCIAS OBSERVACION PEDIATRICA'
                          WHEN ADINGRESO.UFUCODIGO = '032' THEN 'URGENCIAS OBSERVACION HOMBRES 1' WHEN ADINGRESO.UFUCODIGO = '033' THEN 'URGENCIAS OBSERVACION HOMBRES 2'
                          WHEN ADINGRESO.UFUCODIGO = '034' THEN 'URGENCIAS OBSERVACION MUJERES 1' WHEN ADINGRESO.UFUCODIGO = '035' THEN 'URGENCIAS OBSERVACION MUJERES 2'
                          WHEN ADINGRESO.UFUCODIGO = '036' THEN 'URGENCIAS OBSERVACION PASILLO' WHEN ADINGRESO.UFUCODIGO = '037' THEN 'URGENCIAS OBSERVACION REANIMACION'
                          WHEN ADINGRESO.UFUCODIGO = '12' THEN 'URGENCIAS GINECO OBSTETRICIA' WHEN ADINGRESO.UFUCODIGO = '122' THEN 'HOSPITALIZACION GINECOOBSTETRICA'
                          WHEN ADINGRESO.UFUCODIGO = '123' THEN 'OBSTETRICIA PREQUIRURGICA' WHEN ADINGRESO.UFUCODIGO = '13' THEN 'HOSPITALIZACION MEDICINA INTERNA'
                          WHEN ADINGRESO.UFUCODIGO = '14' THEN 'UNIDAD DE CUIDADOS INTERMEDIOS' WHEN ADINGRESO.UFUCODIGO = '141' THEN 'UCI ADULTOS' WHEN ADINGRESO.UFUCODIGO
                          = '15' THEN 'HOSPITALIZACION MEDICOQUIRURGICA' WHEN ADINGRESO.UFUCODIGO = '17' THEN 'HOSPITALIZACION PEDIATRIA' WHEN ADINGRESO.UFUCODIGO
                          = '18' THEN 'HOSPITALIZACION RECIEN NACIDO' WHEN ADINGRESO.UFUCODIGO = '191' THEN 'RECUPERACION QUIROFANO' WHEN ADINGRESO.UFUCODIGO = '192'
                          THEN 'RECUPERACION OBSTETRICA' WHEN ADINGRESO.UFUCODIGO = '193' THEN 'QUIROFANO PREQUIRURGICAS' WHEN ADINGRESO.UFUCODIGO = '20' THEN 'TRABAJO DE PARTOS'
                          WHEN ADINGRESO.UFUCODIGO = '48' THEN 'HOSPITALIZACION PENSION' WHEN ADINGRESO.UFUCODIGO = '04' THEN 'CONSULTA EXTERNA' WHEN ADINGRESO.UFUCODIGO
                          = '05' THEN 'CONSULTA ESPECIALIZADA' WHEN ADINGRESO.UFUCODIGO = '06' THEN 'C EXT ORTOPEDIA' WHEN ADINGRESO.UFUCODIGO = '07' THEN 'C EXT PEDIATRIA'
                          WHEN ADINGRESO.UFUCODIGO = '08' THEN 'C EXT MEDICIANA INTERNA' WHEN ADINGRESO.UFUCODIGO = '09' THEN 'C EXT CIRUGIA GRAL' WHEN ADINGRESO.UFUCODIGO
                          = '10' THEN 'C EXT UROLOGIA' WHEN ADINGRESO.UFUCODIGO = '11' THEN 'C EXT OFTALMOLOGIA' WHEN ADINGRESO.UFUCODIGO = '121' THEN 'CONSULTA EXTERNA GINECO OBSTETRICIA'
                          WHEN ADINGRESO.UFUCODIGO = '29' THEN 'C EXT NEUROLOGIA' WHEN ADINGRESO.UFUCODIGO = '51' THEN 'C EXT GASTROENTEROLOGIA' WHEN ADINGRESO.UFUCODIGO
                          = '52' THEN 'C EXT CARDIOLOGIA' WHEN ADINGRESO.UFUCODIGO = '62' THEN 'C EXT OTORRINOLARINGOLOGIA' WHEN ADINGRESO.UFUCODIGO = '19' THEN 'QUIROFANO'
                          END AS [UNIDAD FUNCIONAL], ADINGRESO.FECREGCRE AS [FEC CREACION INGRESO], ADINGRESO.CODUSUMOD AS [COD USUARIO MODIFICA], 
                         SEGusuaru_1.NOMUSUARI AS [USUARIO MODIFICA], ADINGRESO.FECREGMOD AS [FECHA MODIFICACION], ADINGRESO.CODUSUANU AS [COD USUARIO ANULA], 
                         SEGusuaru.NOMUSUARI AS [USUARIO ANULA], ADINGRESO.FECREGANU AS [FECHA ANULACION], ADINGRESO.IJUSTIFIC AS [JUSTIFICACION ANULACION], 
                         CASE WHEN ADINGRESO.IINGREPOR = '1' THEN 'URGENCIAS' WHEN ADINGRESO.IINGREPOR = '2' THEN 'CONSULTA EXTERNA' WHEN ADINGRESO.IINGREPOR = '3'
                          THEN 'NACIDO HOSPITAL' WHEN ADINGRESO.IINGREPOR = '4' THEN 'REMITIDO' WHEN ADINGRESO.IINGREPOR = '5' THEN 'HOSPITALIZACION URGENCIAS' END AS
                          [INGRESA POR], ADINGRESO.IOBSERVAC AS Observaciones, INENTIDAD.NOMENTIDA, BS.CreationDate AS Fecha_BS, bs.CreationUser AS Cod_usuario_BS,
						  (select FECEGRESO from dbo.CHREGEGRE WHERE NUMINGRES = dbo.ADINGRESO.NUMINGRES) as FechaEgreso
FROM            dbo.ADINGRESO INNER JOIN
                         dbo.INPACIENT ON ADINGRESO.IPCODPACI = INPACIENT.IPCODPACI INNER JOIN
                         dbo.INENTIDAD ON ADINGRESO.CODENTIDA = INENTIDAD.CODENTIDA LEFT OUTER JOIN
                         dbo.SEGusuaru ON ADINGRESO.CODUSUANU = SEGusuaru.CODUSUARI LEFT OUTER JOIN
                         dbo.SEGusuaru AS SEGusuaru_2 ON ADINGRESO.CODUSUCRE = SEGusuaru_2.CODUSUARI LEFT OUTER JOIN
                         dbo.SEGusuaru AS SEGusuaru_1 ON ADINGRESO.CODUSUMOD = SEGusuaru_1.CODUSUARI LEFT OUTER JOIN
						Billing.SlipOut AS BS ON ADINGRESO.NUMINGRES =BS.AdmissionNumber
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a auditoría y seguimiento administrativo de ingresos hospitalarios. Consolida por cada admisión los datos del paciente, su estado de facturación (No Facturado, Facturado, Parcial, Anulado, Cerrado), la unidad funcional de atención, la entidad aseguradora, los usuarios que crearon/modificaron/anularon el registro, el comprobante de salida de facturación y la fecha de egreso hospitalario cuando existe.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los ingresos de pacientes con su estado, unidad funcional, datos de creación/modificación/anulación, comprobante de salida de facturación y fecha de egreso, para seguimiento de ingresos abiertos.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso debe tener un paciente válido en INPACIENT (INNER JOIN por IPCODPACI).; Cada ingreso debe tener una entidad pagadora válida en INENTIDAD (INNER JOIN por CODENTIDA).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos que tengan paciente y entidad existentes en sus maestros.; La fecha de egreso se obtiene del primer/único registro de CHREGEGRE cuyo NUMINGRES coincide con el ingreso (subconsulta escalar).; Los datos de usuarios (creación, modificación, anulación) y el comprobante de salida son opcionales (LEFT JOIN), por lo que pueden venir nulos.; El catálogo de unidades funcionales y de motivos de ingreso está hardcodeado en la vista, no se consulta una tabla de parámetros.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/admisión de paciente; Estado de facturación del ingreso (No facturado, Facturado, Facturado parcial, Anulado, Cerrado); Unidad funcional (urgencias, hospitalización, UCI, consulta externa, quirófano, trabajo de partos); Vía de ingreso (urgencias, consulta externa, nacido en hospital, remitido); Anulación de ingreso con justificación; Egreso del paciente; Entidad pagadora; Comprobante de salida de facturación (Slip Out)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.ReporteIngresosAbiertos: Devuelve un registro por cada ingreso de ADINGRESO con paciente y entidad asociados, enriquecido con usuarios de creación/modificación/anulación, comprobante de salida (Billing.SlipOut) y fecha de egreso de CHREGEGRE.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IESTADOIN = '' '' → Estado del ingreso se reporta como ''No Facturado''; si IESTADOIN = ''F'' → Estado ''Facturado''; si IESTADOIN = ''A'' → Estado ''Anulado''; si IESTADOIN = ''P'' → Estado ''Facturado Parcial''; si IESTADOIN = ''C'' → Estado ''Cerrado''; si IINGREPOR = ''1'' → Ingresa por ''URGENCIAS''; si IINGREPOR = ''2'' → Ingresa por ''CONSULTA EXTERNA''; si IINGREPOR = ''3'' → Ingresa por ''NACIDO HOSPITAL''; si IINGREPOR = ''4'' → Ingresa por ''REMITIDO''; si IINGREPOR = ''5'' → Ingresa por ''HOSPITALIZACION URGENCIAS''; si UFUCODIGO coincide con catálogo embebido (01, 031..037, 04..20, 29, 48, 51, 52, 62, etc.) → Se traduce a la descripción de unidad funcional correspondiente (urgencias, hospitalizaciones, consultas externas, quirófano, UCI, etc.) else Unidad funcional queda NULL si el código no está en el catálogo', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; dbo.SEGusuaru; Billing.SlipOut; dbo.CHREGEGRE', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteIngresosAbiertos';
GO

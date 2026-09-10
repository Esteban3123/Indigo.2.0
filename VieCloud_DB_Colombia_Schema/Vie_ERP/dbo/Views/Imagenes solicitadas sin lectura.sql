

CREATE VIEW [dbo].[Imagenes solicitadas sin lectura]
AS
SELECT        A.IPCODPACI AS [DOC PACIENTE], D.IPNOMCOMP AS [NOMBRE PACIENTE], dbo.DestinoFolio(A.IPCODPACI) AS FolioActual, 
                         A.FECRECEXA AS [FECHA TOMA SERVICIO], dbo.DiferenciaDias(A.FECRECEXA) AS DiasTranscurridos, A.CODSERIPS AS [COD SERVICIO], 
                         B.DESSERIPS AS [DESCRIPCION SERVICIO], A.CANSERIPS AS [CANTIDAD SOLICITADA], C.DESSUBIPS AS [TIPO EXAMEN], A.NUMINGRES AS INGRESO, 
                         A.CODPROSAL AS [PROF SOLICITO], A.FECORDMED AS [FECHA ORDEN], I.NOMUSUARI AS [USUARIO TOMA EXAMEN], G.UFUDESCRI AS [UF. Actual], 
                         H.DESCCAMAS AS [Cama Actual]
FROM            dbo.HCORDIMAG AS A INNER JOIN
                         dbo.ADINGRESO AS F ON A.NUMINGRES = F.NUMINGRES INNER JOIN
                         dbo.INCUPSIPS AS B ON A.CODSERIPS = B.CODSERIPS INNER JOIN
                         dbo.INCUPSSUB AS C ON B.CODGRUSUB = C.CODGRUSUB INNER JOIN
                         dbo.INPACIENT AS D ON A.IPCODPACI = D.IPCODPACI INNER JOIN
                         dbo.HCHISPACA AS E ON A.IPCODPACI = E.IPCODPACI AND A.NUMEFOLIO = E.NUMEFOLIO INNER JOIN
                         dbo.INUNIFUNC AS G ON F.UFUACTPAC = G.UFUCODIGO INNER JOIN
                         dbo.CHCAMASHO AS H ON F.CODCAMACT = H.CODICAMAS INNER JOIN
                         dbo.SEGusuaru AS I ON A.USURECEXA = I.CODUSUARI
WHERE        (F.TIPOINGRE = '2') AND (A.SERTRANSC = 0) AND (A.FECORDMED BETWEEN '10/09/2012 00:00:00' AND '30/12/2012 23:59:59')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Listado de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias, entre otras) que fueron solicitadas y tomadas pero que aún no tienen lectura o interpretación registrada, correspondientes a pacientes hospitalizados (ingreso tipo hospitalización). Integra las órdenes de imágenes (HCORDIMAG) con el ingreso del paciente (ADINGRESO), el catálogo de servicios CUPS/IPS (INCUPSIPS), el subgrupo del examen (INCUPSSUB), los datos del paciente (INPACIENT), la historia clínica (HCHISPACA), la unidad funcional y cama actual del paciente (INUNIFUNC, CHCAMASHO) y el usuario que realizó la toma del examen (SEGusuaru). Permite identificar estudios de imagen pendientes de lectura médica, mostrando el paciente, su ubicación actual en el hospital, los días transcurridos desde la toma del examen, el profesional que los solicitó y el usuario que los ejecutó, siendo útil para gestión de calidad asistencial, seguimiento de resultados y auditoría clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Imagenes solicitadas sin lectura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Imagenes solicitadas sin lectura';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas solicitadas a pacientes hospitalizados que aún no han sido transcritas/leídas, en un rango específico de fechas, con datos de paciente, ubicación y servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Imagenes solicitadas sin lectura';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen ingresos de tipo hospitalización (TIPOINGRE=''2'') con órdenes de imagen registradas.; Las órdenes de imagen tienen folio de historia clínica asociado existente en HCHISPACA.; El paciente tiene cama y unidad funcional actual asignadas en el ingreso.; El usuario que recibió/tomó el examen existe en el catálogo de seguridad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Imagenes solicitadas sin lectura';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con bandera SERTRANSC=0 (sin transcripción/lectura).; Solo se consideran ingresos de tipo ''2'' (hospitalización).; El rango de FECORDMED está fijo (10/09/2012 a 30/12/2012), por lo que la vista no es dinámica respecto a la fecha actual.; Cada fila representa una orden de imagen vinculada a un folio de historia clínica existente (INNER JOIN con HCHISPACA).; Se calcula el folio destino actual del paciente y los días transcurridos desde la recepción del examen mediante funciones escalares.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Imagenes solicitadas sin lectura';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas; Pacientes hospitalizados; Folio de historia clínica; Ingreso hospitalario; Unidad funcional; Cama hospitalaria; Servicios CUPS/IPS; Profesional solicitante; Transcripción/lectura de exámenes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Imagenes solicitadas sin lectura';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDIMAG: Devuelve órdenes de imagen donde SERTRANSC=0 (servicio no transcrito/sin lectura), TIPOINGRE=''2'' (hospitalización) y FECORDMED entre 10/09/2012 y 30/12/2012.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Imagenes solicitadas sin lectura';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DestinoFolio; dbo.DiferenciaDias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Imagenes solicitadas sin lectura';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.ADINGRESO; dbo.INCUPSIPS; dbo.INCUPSSUB; dbo.INPACIENT; dbo.HCHISPACA; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Imagenes solicitadas sin lectura';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Imagenes solicitadas sin lectura';
GO


CREATE VIEW [dbo].[Estado_citas_medicas_pacientes]
AS
SELECT     C.CODAUTONU AS [N° CITA], C.IPCODPACI AS IDENTIFICACION, PA.IPNOMCOMP AS PACIENTE, PA.IPTELMOVI AS [CELULAR PACINTE], 
                      PA.IPTELEFON AS [FIJO PACINTE], dbo.INENTIDAD.NOMENTIDA AS ENTIDAD, P.NOMMEDICO AS MEDICO, E.DESESPECI AS ESPECIALIDAD, 
                      C.FECHORAIN AS [FECHA DE CITA], CO.DESCRICON AS CONSULTORIO, A.DESACTMED AS ACTIVIDAD, 
                      CASE WHEN C.CODTIPSOL = '0' THEN 'Presencial' WHEN C.CODTIPSOL = '1' THEN 'Telefónica' END AS SOLICITUD, 
                      CASE WHEN C.CODTIPCIT = '0' THEN 'Primera Vez' WHEN C.CODTIPCIT = '1' THEN 'Control' WHEN C.CODTIPCIT = '2' THEN 'Pos Operatorio' END AS TIPO, 
                      CASE WHEN C.CODESTCIT = '0' THEN 'Asignada' WHEN C.CODESTCIT = '1' THEN 'Cumplida' WHEN C.CODESTCIT = '2' THEN 'Incumplida' WHEN C.CODESTCIT = '3' THEN
                       'Preasignada' END AS ESTADO, CASE WHEN C.CITAEXTRA = '0' THEN 'Normal' WHEN C.CITAEXTRA = '1' THEN 'Cita Extra' END AS [CITA EXTRA], 
                      US.NOMUSUARI AS USUARIO, C.FECREGSIS AS [FECHA REGISTRO], DATEDIFF(y, C.FECREGSIS, C.FECHORAIN) AS [Días Transcurridos], 1 AS Cont, 
                      YEAR(C.FECREGSIS) AS AÑO, MONTH(C.FECREGSIS) AS MES, DAY(C.FECREGSIS) AS DIA
FROM         dbo.AGASICITA AS C INNER JOIN
                      dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI INNER JOIN
                      dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI INNER JOIN
                      dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL INNER JOIN
                      dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON INNER JOIN
                      dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED INNER JOIN
                      dbo.SEGusuaru AS US ON US.CODUSUARI = C.CODUSUASI INNER JOIN
                      dbo.INENTIDAD ON PA.CODENTIDA = dbo.INENTIDAD.CODENTIDA
WHERE     (C.FECHORAIN BETWEEN '01/01/2013 00:00:00' AND '31/12/2013 23:59:59')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el estado de las citas médicas de pacientes para el año 2013, integrando en una sola consulta los datos del paciente (cédula, nombre, teléfonos), la entidad aseguradora o pagadora (EPS, ARS), el médico tratante, la especialidad, el consultorio, la actividad médica programada y el usuario que registró la cita. Traduce los códigos internos de estado (Asignada, Cumplida, Incumplida, Preasignada), tipo de cita (Primera Vez, Control, Pos Operatorio), modalidad de solicitud (Presencial, Telefónica) y si es cita extra, en etiquetas legibles para reportería. Incluye la fecha de la cita, la fecha de registro, los días transcurridos entre ambas y desgloses por año, mes y día para facilitar análisis de agendamiento, cumplimiento y productividad médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Estado_citas_medicas_pacientes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Estado_citas_medicas_pacientes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolidar y traducir a etiquetas legibles las citas médicas del año 2013 con datos del paciente, médico, especialidad, consultorio, entidad, usuario asignador y estado, para reportes y análisis de agendamiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Estado_citas_medicas_pacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de catálogo (especialidad, profesional, consultorio, actividad médica, usuario, entidad) deben tener registros coherentes con las claves usadas en la cita; de lo contrario la cita no aparece.; El paciente debe tener una entidad (CODENTIDA) válida en INENTIDAD para que la cita sea visible.; El rango de fechas está fijo al año 2013, por lo que la vista solo es útil para ese período.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Estado_citas_medicas_pacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen citas cuya fecha/hora de inicio (FECHORAIN) esté dentro del año 2013.; Solo se incluyen citas que tengan correspondencia válida (INNER JOIN) en especialidad, paciente, profesional, consultorio, actividad médica, usuario asignador y entidad del paciente; cualquier cita huérfana queda excluida.; Cada fila representa exactamente una cita y aporta el contador fijo Cont=1 para totalizaciones.; Los códigos de tipo de solicitud, tipo de cita, estado y cita extra fuera de los valores definidos se devuelven como NULL en los campos descriptivos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Estado_citas_medicas_pacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Paciente; Profesional de la salud / Médico; Especialidad; Consultorio; Actividad médica; Entidad (aseguradora/pagador); Tipo de solicitud (Presencial/Telefónica); Tipo de cita (Primera Vez/Control/Pos Operatorio); Estado de la cita (Asignada/Cumplida/Incumplida/Preasignada); Cita extra; Usuario que asigna', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Estado_citas_medicas_pacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve únicamente las citas con FECHORAIN entre 2013-01-01 00:00:00 y 2013-12-31 23:59:59, descodificando tipo de solicitud, tipo de cita, estado y bandera de cita extra a texto, e incluye días transcurridos entre registro y cita más desglose de año/mes/día del registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Estado_citas_medicas_pacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODTIPSOL = ''0'' → Solicitud clasificada como ''Presencial'' else Si CODTIPSOL = ''1'' se clasifica como ''Telefónica''; si CODTIPCIT = ''0'' → Tipo de cita ''Primera Vez'' else ''1'' = ''Control''; ''2'' = ''Pos Operatorio''; si CODESTCIT = ''0'' → Estado ''Asignada'' else ''1'' = ''Cumplida''; ''2'' = ''Incumplida''; ''3'' = ''Preasignada''; si CITAEXTRA = ''0'' → Marca ''Normal'' else ''1'' = ''Cita Extra''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Estado_citas_medicas_pacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INESPECIA; dbo.INPACIENT; dbo.INPROFSAL; dbo.AGCONSULT; dbo.AGACTIMED; dbo.SEGusuaru; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Estado_citas_medicas_pacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Estado_citas_medicas_pacientes';
GO

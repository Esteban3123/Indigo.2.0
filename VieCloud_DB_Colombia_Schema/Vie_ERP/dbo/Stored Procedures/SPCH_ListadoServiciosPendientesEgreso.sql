

CREATE PROCEDURE [dbo].[SPCH_ListadoServiciosPendientesEgreso]
(
@Paciente Varchar(25),
@Ingreso  Char(10),
@CentroAtencion  Char(10),
@UnidadFuncional  Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT A.FECORDMED AS 'FechaSolicitud', RTRIM(A.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) AS Servicio,
CASE ESTSERIPS WHEN '2' THEN 'Pendiente de Resultado' WHEN '3' THEN 'Pendiente de Interpretacion' ELSE 'Remitido' END AS Estado,'Laboratorios' AS Tipo
FROM dbo.HCORDLABO A 
INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND ESTSERIPS IN ('1','2','3','5') AND A.CODSERIPS NOT IN ( SELECT CODSERIPS FROM dbo.HCEGRESINRESULT WHERE UFUCODIGO = @UnidadFuncional AND CODCENATE = @CentroAtencion  )
UNION
SELECT A.FECORDMED AS 'FechaSolicitud',RTRIM(A.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) AS Servicio,
CASE ESTSERIPS WHEN '2' THEN 'Estudio Realizado' WHEN '3' THEN 'Pendiente de Interpretacion' ELSE 'Remitido' END AS Estado,'Imagenes DX' AS Tipo
FROM dbo.HCORDIMAG A 
INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND ESTSERIPS IN ('1','2','3','5') AND A.CODSERIPS NOT IN ( SELECT CODSERIPS FROM dbo.HCEGRESINRESULT WHERE UFUCODIGO = @UnidadFuncional AND CODCENATE = @CentroAtencion  )
/*
UNION
SELECT A.FECORDMED AS 'FechaSolicitud',RTRIM(A.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) AS Servicio,
CASE ESTSERIPS WHEN '2' THEN 'Pendiente de Resultado' WHEN '3' THEN 'Pendiente de Interpretacion' ELSE 'Remitido' END AS Estado,'Patologias' AS Tipo
FROM dbo.HCORDPATO A INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND ESTSERIPS IN ('2','3','5')
*/
UNION
SELECT A.FECORDMED AS 'FechaSolicitud',RTRIM(A.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) AS Servicio,
'Pendiente Evolucion' AS Estado,'Interconsultas' AS Tipo
FROM dbo.HCORDINTE A 
INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND ESTSERIPS IN ('1','2')
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los servicios clínicos pendientes de resolución para un paciente y un ingreso hospitalario específico, antes de proceder con el egreso. Consolida en un único resultado tres tipos de órdenes médicas activas: exámenes de laboratorio (pendientes de resultado o interpretación), imágenes diagnósticas (radiología, ecografías, tomografías, etc.) e interconsultas pendientes de evolución. Excluye los servicios de laboratorio e imágenes que ya fueron marcados como resueltos en la tabla de resultados de egreso para la unidad funcional y centro de atención indicados. Se usa en el proceso de egreso hospitalario para verificar que no queden órdenes médicas sin completar antes de dar de alta al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios clínicos (laboratorios, imágenes diagnósticas e interconsultas) solicitados durante un ingreso que aún están pendientes y deben resolverse antes del egreso del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir y tener órdenes registradas en HCORDLABO, HCORDIMAG o HCORDINTE.; Los códigos de servicio deben estar parametrizados en INCUPSIPS para poder obtener su descripción.; Se requiere identificar el centro de atención y la unidad funcional para excluir servicios marcados como no requeridos al egreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran laboratorios e imágenes con ESTSERIPS en (''1'',''2'',''3'',''5''); las interconsultas solo con ESTSERIPS en (''1'',''2'').; Las órdenes de patología (HCORDPATO) están comentadas y no se incluyen en el resultado.; Los servicios registrados en HCEGRESINRESULT para la unidad funcional y centro de atención dados se consideran resueltos/excluidos para el egreso.; El listado siempre se filtra por la combinación paciente + ingreso.; Los códigos de servicio se devuelven sin espacios en blanco a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Egreso; Órdenes de laboratorio; Órdenes de imágenes diagnósticas; Interconsultas; Servicios CUPS/IPS; Estado de servicio (pendiente de resultado, pendiente de interpretación, remitido, pendiente evolución, estudio realizado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto unificado (UNION) con FechaSolicitud, Código, Servicio, Estado y Tipo para laboratorios, imágenes DX e interconsultas pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo Laboratorios: ESTSERIPS = ''2'' → Estado = ''Pendiente de Resultado'' else Si ''3'' → ''Pendiente de Interpretacion''; otro valor → ''Remitido''; si Tipo Imágenes DX: ESTSERIPS = ''2'' → Estado = ''Estudio Realizado'' else Si ''3'' → ''Pendiente de Interpretacion''; otro valor → ''Remitido''; si Tipo Interconsultas (HCORDINTE) → Estado fijo ''Pendiente Evolucion'' sin importar ESTSERIPS; si Servicio existe en HCEGRESINRESULT para la unidad funcional y centro de atención dados → Se excluye del listado de laboratorios e imágenes else Se incluye si su ESTSERIPS está en (''1'',''2'',''3'',''5'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.HCORDIMAG; dbo.HCORDINTE; dbo.INCUPSIPS; dbo.HCEGRESINRESULT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListadoServiciosPendientesEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListadoServiciosPendientesEgreso';
-- GO

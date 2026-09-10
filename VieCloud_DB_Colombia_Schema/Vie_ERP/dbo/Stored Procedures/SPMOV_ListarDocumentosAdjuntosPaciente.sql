CREATE  Procedure [dbo].[SPMOV_ListarDocumentosAdjuntosPaciente]
(
@Paciente Varchar(25)
)
AS
Select  RTRIM(A.NOMARCADJ) as 'NombreDocumento',
		RTRIM(A.ARCHEXTEN) as Extension,
		Case TIPODOCUM
			when 2 then 'Laboratorio'
			when 3 then 'Imagenologia'
			when 4 then 'Patologias'
			when 5 then 'Decreto_3047'
			when 6 then 'Plantillas_Documentos'
			when 7 then 'Consentimiento_Informado'
			when 8 then 'Resultados_Examenes_Sitio'
			when 9 then 'Lectura_Imagenes'
			when 10 then 'Solicitudes'
			when 11 then 'FacturasDeVenta'
			else 'Otro'
		end as TipoDocumento,
		RTRIM(A.NUMINGRES) as Ingreso,
		A.FECPROCES as 'Fecha',
		dbo.ObtenerFechaFormateada(FECPROCES) as 'FechaFormateada',
		url = 'http://ruta/' + RTRIM(NOMARCADJ)
 from HCDOCUMAD as A
 where A.IPCODPACI = @Paciente
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los documentos adjuntos vinculados a un paciente en su historia clínica, consultando la tabla de documentos adjuntos por admisión (HCDOCUMAD) a partir de la cédula o código del paciente. Para cada archivo devuelve el nombre del documento, la extensión, el número de ingreso, la fecha de carga formateada mediante la función ObtenerFechaFormateada y la URL de acceso al archivo. Clasifica cada documento según su tipo de negocio: resultados de laboratorio, imágenes diagnósticas, patologías, consentimientos informados, facturas de venta, solicitudes, entre otros, facilitando la visualización del historial documental del paciente desde cualquier ingreso u hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los documentos adjuntos asociados a un paciente, traduciendo el tipo de documento a una etiqueta legible y entregando la URL de acceso al archivo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un identificador de paciente válido para filtrar la tabla de documentos adjuntos.; Debe existir la función escalar de formateo de fecha utilizada en la proyección.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna documentos asociados al paciente recibido (filtra por IPCODPACI).; El nombre del documento y la extensión se devuelven sin espacios en blanco a la derecha (RTRIM).; La URL del documento siempre se construye con el prefijo fijo ''http://ruta/'' concatenado al nombre del archivo adjunto.; El tipo de documento se traduce siempre a una etiqueta textual; valores no contemplados se etiquetan como ''Otro''.; La fecha se entrega en dos formatos: cruda y formateada vía función de utilidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Documentos adjuntos clínicos; Ingreso del paciente; Laboratorio; Imagenología; Patología; Consentimiento informado; Decreto 3047; Facturas de venta; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCDOCUMAD: Cuando IPCODPACI coincide con el paciente solicitado, devuelve un resultset con nombre, extensión, tipo traducido, ingreso, fecha, fecha formateada y URL construida del documento adjunto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPODOCUM = 2 → Clasifica como ''Laboratorio''; si TIPODOCUM = 3 → Clasifica como ''Imagenologia''; si TIPODOCUM = 4 → Clasifica como ''Patologias''; si TIPODOCUM = 5 → Clasifica como ''Decreto_3047''; si TIPODOCUM = 6 → Clasifica como ''Plantillas_Documentos''; si TIPODOCUM = 7 → Clasifica como ''Consentimiento_Informado''; si TIPODOCUM = 8 → Clasifica como ''Resultados_Examenes_Sitio''; si TIPODOCUM = 9 → Clasifica como ''Lectura_Imagenes''; si TIPODOCUM = 10 → Clasifica como ''Solicitudes''; si TIPODOCUM = 11 → Clasifica como ''FacturasDeVenta'' else Cualquier otro valor de TIPODOCUM se clasifica como ''Otro''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ObtenerFechaFormateada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDOCUMAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDocumentosAdjuntosPaciente';
-- GO

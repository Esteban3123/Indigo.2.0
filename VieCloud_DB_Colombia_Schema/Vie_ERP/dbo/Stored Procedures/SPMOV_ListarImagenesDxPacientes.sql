
CREATE PROCEDURE [dbo].[SPMOV_ListarImagenesDxPacientes]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  A.IPCODPACI
	,A.NUMINGRES,
				RTRIM(E.DESSERIPS) AS 'Servicio',
				RTRIM(A.CODSERIPS) AS Codigo,
				RTRIM(A.FECORDMED) AS 'Fecha Solicitud',
				dbo.ObtenerFechaFormateada(A.FECORDMED) as 'FechaFormateada',
				A.ESTSERIPS AS IdEstado,
				CASE A.ESTSERIPS
				WHEN '1' THEN 'Solicitado'
				WHEN '2' THEN 'Estudio Realizado'
				WHEN '3' THEN 'Imagen Procesada'
				WHEN '4' THEN 'Estudio Interpretado'
				WHEN '5' THEN 'Remitido'
				WHEN '6' THEN 'Anulado'
				WHEN '7' THEN 'Extramural'
				END AS Estado,
				RTRIM(B.NOMMEDICO) AS 'Medico', 
				CASE A.PRISERIPS 
					WHEN '1' THEN 'Urgente' 
					WHEN '2' THEN 'Rutina' 
				END AS 'Tipo Examen', 
				RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS Unidad,
				RTRIM(A.CANSERIPS) AS Cantidad, 
				RTRIM(A.OBSSERIPS) AS Observacion,
				RTRIM(MANEXTPRO) AS Externo,
				RTRIM(N.DESESPECI) AS Especialidad,
				/*RTRIM(K.NOMSERPAC) as 'Url',
				RTRIM(HKE.url_imagen) as 'UrlImagen'*/
				RTRIM(K.NOMSERPACEXT) as 'Url',
				LTRIM(RTRIM(K.NOMSERPACEXT)) + '' +Ltrim(RTRIM(HKE.url_imagen)) as 'UrlImagen'
	FROM HCORDIMAG A 
			INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
			INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
			INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
			INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS
			INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
			LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
			Inner join HCPARPACS K on K.CODCENATE = A.CODCENATE
			left outer join HKLECTURA HKE ON HKE.auto_imagen_his = A.AUTO
	WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias, entre otros) solicitadas para un paciente en un ingreso específico. Para cada orden muestra el servicio CUPS, la fecha de solicitud, el estado del estudio (solicitado, realizado, procesado, interpretado, anulado, etc.), la prioridad (urgente o rutina), el médico solicitante, la unidad funcional y sede donde se ordenó, la especialidad, las observaciones y la URL para visualizar la imagen en el visor diagnóstico (PACS/HIS). Combina información de órdenes de imágenes (HCORDIMAG), el catálogo de profesionales (INPROFSAL), los centros de atención (ADCENATEN), las unidades funcionales (INUNIFUNC), el catálogo de servicios CUPS (INCUPSIPS), la historia clínica del folio asociado (HCHISPACA), las especialidades médicas (INESPECIA), los parámetros de conectividad con el visor de imágenes por sede (HCPARPACS) y el registro de lecturas de imágenes (HKLECTURA) para construir el enlace directo al estudio. Se usa principalmente en la visualización del historial de imágenes diagnósticas del paciente durante o después de una atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas de un paciente en un ingreso específico, con datos del estudio, médico, unidad funcional, especialidad y enlaces a las imágenes en el visor PACS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir en HCORDIMAG; El centro de atención de la orden debe estar parametrizado en HCPARPACS para obtener la URL del visor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estudio siempre se asocia a un profesional de salud, centro de atención, unidad funcional y servicio CUPS (joins INNER obligatorios); La especialidad se obtiene del folio histórico de la atención (HCHISPACA) y puede ser nula (LEFT JOIN); La URL de la imagen se compone concatenando la URL base del PACS del centro de atención (HCPARPACS.NOMSERPACEXT) con la URL específica de la lectura (HKLECTURA.url_imagen); Una orden puede no tener lectura asociada en HKLECTURA (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Orden de imagen diagnóstica; Servicio CUPS; Estado del estudio; Prioridad del examen (Urgente/Rutina); Médico solicitante; Unidad funcional; Centro de atención; Especialidad; PACS / Visor de imágenes; Estudio extramural', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDIMAG: Devuelve las órdenes de imagen diagnóstica filtradas por paciente e ingreso (WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS de la orden de imagen → Traduce el código de estado a etiqueta: 1=Solicitado, 2=Estudio Realizado, 3=Imagen Procesada, 4=Estudio Interpretado, 5=Remitido, 6=Anulado, 7=Extramural; si PRISERIPS de la orden → Traduce prioridad: 1=Urgente, 2=Rutina', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ObtenerFechaFormateada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; dbo.HCPARPACS; dbo.HKLECTURA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarImagenesDxPacientes';
-- GO

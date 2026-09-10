CREATE PROCEDURE [dbo].[SPREP_ADM_ListarRadicacionCirugia]
(
@NumeroRadicacion varchar(20)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT A.ID as ID,A.NUMRADICACION as 'NumeroRadicacion' ,A.FECHAREGISTRO  as 'FechaRadicado', A.IPCODPACI, S.CODSERIPS, 
B.IPCODPACI as Codigo,RTRIM(IPNOMCOMP) as Nombre,IPFECNACI AS 'Fecha Nacimiento','' as Edad ,IPDIRECCI AS Direccion,IPTELEFON AS Telefono,
CASE IPSEXOPAC WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS Genero,

dbo.TipoDocumentoNombreCompleto(B.IPTIPODOC) AS TipoIdentificacion,
dbo.TipoPaciente(B.IPTIPOPAC) AS TipoUsuario,
dbo.TipoAfiliado(B.IPTIPOAFI) AS 'TIPO AFILIADO',
dbo.RangoAfiliacion(B.NIVCODIGO) AS 'NIVEL AFILIACION',

--CASE when b.IPTIPODOC = 1 then 'Cédula de Ciudadanía' 
--	when IPTIPODOC = 2 then 'Cedula Extranjera' 
--	when IPTIPODOC = 3 then 'Tarjeta Identidad' 
--	when IPTIPODOC = 4 then 'Registro Civil' 
--	when IPTIPODOC = 5 then 'Pasaporte' 
--	when IPTIPODOC = 6 THEN 'Adulto sin Identificación (solo para el Régimen Subsidiado)' 
--	When IPTIPODOC = 7 then 'Menor sin Identificación (solo para el Régimen Subsidiado)'
--	when IPTIPODOC= 9 then 'Carnet Diplomático' END AS TipoIdentificacion,

--CASE when IPTIPOPAC = 0 then 'Contributivo' 
--	when IPTIPOPAC = 1 then 'Subsidiado'  
--	when IPTIPOPAC = 2 then 'Vinculado'
--	when IPTIPOPAC = 3 then 'Particular' 
--	when IPTIPOPAC = 5 then 'Desplazado Reg. Contributivo'  
--	when IPTIPOPAC = 6 then 'Desplazado Reg. Subsidiado' 
--	when IPTIPOPAC = 7 then 'Desplazado no Asegurado' END AS TipoUsuario,

	rtrim(c.CODCENATE) + ' - ' + c.NOMCENATE as 'CentroAtencion',
	rtrim(D.CODPROSAL) + ' - ' + D.NOMMEDICO as 'Profesional',
	rtrim(s.CODSERIPS) + ' - ' + s.DESSERIPS as 'Servicio',
	 ENT.Code + ' - ' + ENT.Name as 'Entidad',
	 G.Code + ' - ' + G.Name as 'GrupoAtencion',
CASE A.PRIORIDAD WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal'   END AS 'PRIORIDAD',
	F.CODESPECI +' - ' + F.DESESPECI as 'Especialidad',
	CASE A.ESTADO WHEN '1' THEN 'Radicado' WHEN '2' THEN 'Confirmado' WHEN '3' THEN 'Anulado'   END AS 'ESTADO'
FROM ADRADICACIONQX A with(nolock) INNER JOIN 
INPACIENT B with(nolock) ON B.IPCODPACI = A.IPCODPACI INNER JOIN 
INPROFSAL D with(nolock) ON D.CODPROSAL = A.CODPROSAL INNER JOIN 
INESPECIA F with(nolock) ON F.CODESPECI = A.CODESPECI INNER JOIN  
INCUPSIPS S with(nolock) ON S.CODSERIPS = A.QXPRINCIPAL INNER JOIN
ADCENATEN c with(nolock) ON C.CODCENATE = A.CODCENATE INNER JOIN
[Contract].[HealthAdministrator] ENT with(nolock) on ENT.ID = A.GENCONENTITY INNER JOIN
[Contract].[CareGroup] G with(nolock) on G.ID = A.GENCAREGROUP 
WHERE A.NUMRADICACION = @NumeroRadicacion 
 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el detalle completo de una radicación de cirugía a partir de su número de radicado. Integra datos del paciente (nombre, fecha de nacimiento, dirección, teléfono, género, tipo de documento, tipo de usuario y nivel de afiliación), el centro de atención, el profesional de la salud solicitante, la especialidad médica, el procedimiento quirúrgico principal (código CUPS), la entidad pagadora (EPS/aseguradora) y el grupo de atención del contrato. Devuelve además el estado de la radicación (Radicado, Confirmado o Anulado) y la prioridad asignada (Emergencia, Urgencia o Normal), siendo el punto de entrada para visualizar o imprimir el comprobante de radicación de una cirugía programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el detalle de una radicación de cirugía identificada por su número, retornando datos del paciente, profesional, especialidad, procedimiento, centro, entidad administradora, grupo de atención, prioridad y estado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una radicación de cirugía con el número de radicación suministrado.; La radicación debe tener referencias válidas y existentes a paciente, profesional, especialidad, procedimiento principal, centro de atención, administradora de salud y grupo de atención (de lo contrario el INNER JOIN excluye el registro).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo retorna radicaciones que tengan paciente, profesional, especialidad, procedimiento principal (CUPS), centro de atención, entidad administradora y grupo de atención asociados (INNER JOIN obligatorios).; El procedimiento quirúrgico principal de la radicación debe existir como servicio CUPS válido.; La entidad pagadora y el grupo de atención referenciados deben existir en los catálogos contractuales.; Género, prioridad y estado se entregan traducidos a etiquetas legibles; valores fuera del dominio definido se devuelven como NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de cirugía; Paciente; Profesional de salud; Especialidad; Servicio CUPS; Centro de atención; Administradora de salud (entidad/pagador); Grupo de atención; Tipo de identificación; Tipo de afiliado; Nivel de afiliación; Prioridad quirúrgica (Emergencia/Urgencia/Normal); Estado de radicación (Radicado/Confirmado/Anulado); Procedimiento quirúrgico principal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando NUMRADICACION coincide con el parámetro y existen todas las relaciones requeridas, retorna una fila con la información consolidada de la radicación de cirugía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = 1 → Género se reporta como ''Masculino'' else Si IPSEXOPAC = 2 → ''Femenino''; si PRIORIDAD = ''1'' → Prioridad ''Emergencia'' else ''2'' → ''Urgencia''; ''3'' → ''Normal''; si ESTADO = ''1'' → Estado ''Radicado'' else ''2'' → ''Confirmado''; ''3'' → ''Anulado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoDocumentoNombreCompleto; dbo.TipoPaciente; dbo.TipoAfiliado; dbo.RangoAfiliacion', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADRADICACIONQX; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INCUPSIPS; dbo.ADCENATEN; Contract.HealthAdministrator; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarRadicacionCirugia';
-- GO

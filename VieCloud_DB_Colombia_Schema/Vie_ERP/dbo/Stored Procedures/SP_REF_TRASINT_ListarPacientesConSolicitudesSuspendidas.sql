-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,17-10-2019,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas]
(
  @CentroAtencion as varchar(MAX)
)

AS
BEGIN
  SET NOCOUNT ON;
  
  declare @Sql as nvarchar(MAX)

  Set @Sql = ' Select A.ID,I.CODTIPPAC as ''Grupo Poblacion'',Case I.CODTIPPAC when 1 then ''Maternas'' when 2 then ''Menor de 5 Años'' when 3 then ''Adulto Mayor'' when 4 then ''Discapacitado'' when 5 then ''Población General'' end As ''Tipo Poblacion'', a.FECHAREGISTRO as ''Fecha Solicitud'', Rtrim(A.IPCODPACI) As ''Paciente'',Rtrim(inpa.IPNOMCOMP) as ''Nombre Paciente'', Rtrim(A.NUMINGRES) as ''Ingreso'', Rtrim(B.NOMCENATE) as ''CA Origen'',  
		Rtrim(NOMENTIDA) As ''Nombre Entidad'',IPFECNACI
		From [dbo].[HCSOLTRASLADOSINT] a 
			--INNER JOIN HCTRASLADOS  x with(nolock) ON A.ID = x.IDHCSOLTRASLADOSINT
			INNER JOIN INPACIENT  inpa with(nolock) ON inpa.IPCODPACI = A.IPCODPACI
			INNER JOIN ADINGRESO I with(nolock) ON  A.NUMINGRES  = I.NUMINGRES 
			INNER JOIN INENTIDAD inen with(nolock) ON inen.CODENTIDA =  I.CODENTIDA  
			INNER JOIN ADCENATEN B  with(nolock)ON  A.CODCENATEOR = B.CODCENATE 
		WHERE  convert(varchar(MAX),A.CODCENATEOR) IN (' + @CentroAtencion + ') and A.ESTADO IN (5) ' --- 5 - Cancelado

		 
--  PRINT @SQL
  exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con solicitudes de traslado interno suspendidas o canceladas (estado 5) para uno o varios centros de atención indicados como parámetro. Consolida información del paciente (cédula, nombre, fecha de nacimiento, grupo y tipo de población), del ingreso hospitalario, del centro de atención de origen y de la entidad aseguradora. Se usa en el módulo de referencia y traslado interno para hacer seguimiento de remisiones que fueron canceladas y que requieren revisión o gestión por parte del personal asistencial o administrativo. Construye SQL dinámico para filtrar por múltiples centros de atención en una sola llamada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con solicitudes de traslado interno en estado cancelado, filtrando por uno o varios centros de atención de origen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe venir como lista válida para concatenarse en cláusula IN (SQL dinámico).; Cada solicitud debe tener paciente, ingreso, entidad y centro de atención asociados (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen solicitudes en estado Cancelado (ESTADO=5).; El resultado se restringe siempre al centro de atención de origen (CODCENATEOR).; Solo se incluyen pacientes con ingreso administrativo y entidad aseguradora vigentes (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de traslado interno; Paciente; Ingreso; Centro de atención de origen; Entidad (aseguradora); Grupo poblacional (Maternas, Menor de 5 Años, Adulto Mayor, Discapacitado, Población General); Solicitud cancelada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCSOLTRASLADOSINT: Devuelve solicitudes con ESTADO = 5 (Cancelado) y CODCENATEOR dentro de la lista de centros de atención recibida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODTIPPAC = 1 → Etiqueta el grupo poblacional como ''Maternas''; si CODTIPPAC = 2 → Etiqueta el grupo poblacional como ''Menor de 5 Años''; si CODTIPPAC = 3 → Etiqueta el grupo poblacional como ''Adulto Mayor''; si CODTIPPAC = 4 → Etiqueta el grupo poblacional como ''Discapacitado''; si CODTIPPAC = 5 → Etiqueta el grupo poblacional como ''Población General''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCSOLTRASLADOSINT; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas';
-- GO



CREATE PROCEDURE [dbo].[SPREP_AGE_CabeceraHojaGastoQX]
(
@IdHojaGastoQX varchar(20)
)
AS
BEGIN

	SET NOCOUNT ON;
	
SELECT A.ID as ID,A.CONSECUTIVO as 'ConsecutivoHoja' ,A.IDAGEPROGQX as 'IdprogramacionQXPrincipal', A.FECHAREGISTRO  as 'FechaHoja', A.IPCODPACI,A.NUMINGRES as 'Ingreso', S.CODSERIPS, 
RTRIM(IPNOMCOMP) as Nombre,IPFECNACI AS 'Fecha Nacimiento','' as Edad ,IPDIRECCI AS Direccion,IPTELEFON AS Telefono,
CASE IPSEXOPAC WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS Sexo, [dbo].[TypeGenderIdentity](B.IdGenderIdentity) AS IdentidadGenero,
CASE when IPTIPODOC = 1 then 'Cédula de Ciudadanía' 
	when IPTIPODOC = 2 then 'Cedula Extranjera' 
	when IPTIPODOC = 3 then 'Tarjeta Identidad' 
	when IPTIPODOC = 4 then 'Registro Civil' 
	when IPTIPODOC = 5 then 'Pasaporte' 
	when IPTIPODOC = 6 THEN 'Adulto sin Identificación (solo para el Régimen Subsidiado)' 
	When IPTIPODOC = 7 then 'Menor sin Identificación (solo para el Régimen Subsidiado)'
	when IPTIPODOC= 9 then 'Carnet Diplomático' END AS TipoIdentificacion,
CASE when IPTIPOPAC = 0 then 'Contributivo' 
	when IPTIPOPAC = 1 then 'Subsidiado'  
	when IPTIPOPAC = 2 then 'Vinculado'
	when IPTIPOPAC = 3 then 'Particular' 
	when IPTIPOPAC = 5 then 'Desplazado Reg. Contributivo'  
	when IPTIPOPAC = 6 then 'Desplazado Reg. Subsidiado' 
	when IPTIPOPAC = 7 then 'Desplazado no Asegurado' END AS TipoUsuario,
	rtrim(c.CODCENATE) + ' - ' + c.NOMCENATE as 'CentroAtencion',
	rtrim(U.UFUCODIGO) + ' - ' + U.UFUDESCRI as 'UnidadFuncional',
	rtrim(D.CODPROSAL) + ' - ' + D.NOMMEDICO as 'Profesional',
	rtrim(s.CODSERIPS) + ' - ' + s.DESSERIPS as 'Servicio',	
	rtrim(F.CODESPECI) +' - ' + F.DESESPECI as 'Especialidad',
	rtrim(sal.CODIGSALA) +' - ' + sal.DESCRIPSAL as 'Sala',
	CASE A.ESTADO WHEN 1 THEN 'Hoja Sin confirmar' WHEN 2 THEN 'En espera aceptacion devolutivo' WHEN 3 THEN 'Devolucion parcial aceptada' WHEN 4 THEN 'Hoja Confirmada'   END AS 'ESTADO'
FROM HCHOJAGASTOQX A with(nolock) INNER JOIN 
AGEPROGQX P with(nolock) ON P.CODAUTONU = A.IDAGEPROGQX INNER JOIN 
INPACIENT B with(nolock) ON B.IPCODPACI = P.IPCODPACI INNER JOIN 
INPROFSAL D with(nolock) ON D.CODPROSAL = P.CODPROSAL INNER JOIN 
INESPECIA F with(nolock) ON F.CODESPECI = P.CODESPECI INNER JOIN  
INCUPSIPS S with(nolock) ON S.CODSERIPS = P.CODSERIPS INNER JOIN
ADCENATEN c with(nolock) ON C.CODCENATE = P.CODCENATE INNER JOIN 
AGENSALAC Sal with(nolock) ON sal.CODCONCEC = P.AGENSALAC INNER JOIN
INUNIFUNC U with(nolock) ON U.UFUCODIGO = sal.UFUCODIGO  
WHERE A.ID = @IdHojaGastoQX
 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la cabecera completa de una Hoja de Gasto Quirúrgico a partir de su identificador, consolidando en un solo resultado todos los datos necesarios para encabezar el documento de quirófano. Integra información del paciente (nombre, cédula, fecha de nacimiento, sexo, tipo de documento, tipo de usuario/régimen), la programación quirúrgica asociada (procedimiento CUPS/IPS, especialidad, profesional de la salud, sala de cirugía, unidad funcional y centro de atención), y los datos propios de la hoja (consecutivo, fecha de registro, número de ingreso y estado de confirmación). Se usa para imprimir o visualizar el encabezado de la hoja de gastos en el proceso de gestión de salas de cirugía y registro de insumos quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de cabecera de una hoja de gasto quirúrgica, incluyendo información del paciente, profesional, servicio, sala y estado, para su visualización/reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una hoja de gasto quirúrgica con el identificador suministrado; La hoja debe tener una programación quirúrgica asociada con paciente, profesional, especialidad, servicio CUPS, centro de atención y sala válidos; La sala debe estar vinculada a una unidad funcional existente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información si la hoja de gasto QX tiene integridad referencial completa con programación QX, paciente, profesional, especialidad, servicio CUPS, centro de atención, sala y unidad funcional (todos INNER JOIN); La consulta usa NOLOCK en todas las tablas, por lo que puede leer datos no confirmados; El estado de la hoja se interpreta en cuatro valores fijos del ciclo de vida: sin confirmar, en espera de aceptación de devolutivo, devolución parcial aceptada y confirmada; La identidad de género se obtiene mediante la función dbo.TypeGenderIdentity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hoja de gasto quirúrgico; Programación quirúrgica; Paciente; Profesional de salud; Especialidad; Servicio CUPS; Centro de atención; Sala quirúrgica; Unidad funcional; Tipo de identificación; Régimen/tipo de usuario (Contributivo, Subsidiado, Vinculado, Particular, Desplazado); Identidad de género; Estado de hoja (sin confirmar, en espera devolutivo, devolución parcial, confirmada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHOJAGASTOQX: Cuando A.ID coincide con el identificador suministrado, retorna una fila con la cabecera de la hoja de gasto quirúrgica enriquecida con datos del paciente, profesional, servicio, especialidad, centro, sala y unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = 1 / 2 → Traduce a ''Masculino'' o ''Femenino''; si IPTIPODOC en 1..7,9 → Traduce el código a etiqueta de tipo de identificación (CC, CE, TI, RC, Pasaporte, Adulto/Menor sin ID, Carnet Diplomático); si IPTIPOPAC en 0..3,5..7 → Traduce a régimen/tipo de usuario (Contributivo, Subsidiado, Vinculado, Particular, Desplazado Contributivo/Subsidiado/No Asegurado); si A.ESTADO en 1..4 → Traduce a ''Hoja Sin confirmar'', ''En espera aceptacion devolutivo'', ''Devolucion parcial aceptada'' o ''Hoja Confirmada''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TypeGenderIdentity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAGASTOQX; dbo.AGEPROGQX; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INCUPSIPS; dbo.ADCENATEN; dbo.AGENSALAC; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_CabeceraHojaGastoQX';
-- GO

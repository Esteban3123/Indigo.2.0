CREATE PROCEDURE [dbo].[SP_PAD_ListarDatosGeneralesPAD]
(
  @IDPHDCONTROL as Int
)
AS
BEGIN
  SET NOCOUNT ON;
  
Select a.ID,IDPADCONTROL,
	RTRIM(RESUMENHC) As 'Resumen HC',RTRIM(PERTINENCIA) AS 'Pertinencia PHD',
	HORAALDIA AS 'Hora al dia',DIASALMES AS 'Dias al mes',
	CASE CAMBIOPOSICION  WHEN 1  THEN 'X' END AS 'Cambio de posición',
	CASE SUMINISTROALIMENT    WHEN 1  THEN 'X' END AS 'Sumnistro de alimentación',
	CASE ASEOBANOPACIENTE    WHEN 1  THEN 'X' END AS 'Aseo y baño del paciente',
	CASE LUBRICACIONPIEL    WHEN 1  THEN 'X' END AS 'Lubricación de la piel',
	CASE ASISTENCIAMOVILIZACION    WHEN 1  THEN 'X' END AS 'Asistencia para movilización',
	CASE ADMINISTRACIONENDOVENOSO    WHEN 1  THEN 'X' END AS 'Administración Medic Endovenosos',
	CASE ALIMENTACIONPARENTAL    WHEN 1  THEN 'X' END AS 'Administrar alimentación parental',
	CASE SONDAVESICAL   WHEN 1  THEN 'X' END AS 'Manejo de sonda vesical',
	CASE VIGILANCIAHERIDAS   WHEN 1  THEN 'X' END AS 'Cuidados y vigilancia de heridas',
	CASE CATETERESPERIFERICOS   WHEN 1  THEN 'X' END AS 'Manejo de cateteres perifericos y cambio',
	RTRIM(OBSERVACIONENFERMERIA) as 'Observacion Enfermeria',
	CASE VISITAMEDICA_ACTUALIZACIONFORMULA  WHEN 1  THEN 'X' END AS 'Actualizacion de formulas',
	VISITAMEDICA_DIASPORMES as 'Visita medica por mes',
	RTRIM(VISITAMEDICA_OBSERVACIONES) AS 'Visita Medica Observacion',
	---
	HERIDAS_DIASPORMES as 'Heridas Mes',
	HERIDAS_FRECUENCIA AS 'Heridas Frecuencia',
	HERIDAS_MOTIVO AS 'Heridas Motivo',
	HERIDAS_OBSERVACIONES AS 'Heridas Observacion',
	---
	OXILISTROS as 'Oxigeno Litros',
	OXIHORASDIA AS 'Oxigeno dia',
	OXIDIASALMES AS 'Oxigeno Mes',
	CASE OXIBALADOMICILIARIA  WHEN 1  THEN 'X' END AS 'Oxigeno Bala Domiciliaria',
	CASE OXIBALAPORTATIL WHEN 1  THEN 'X' END AS 'Oxigeno Bala portatil',
	Rtrim(B.DESVIAADM)AS 'Oxigeno Via administracion',
	OXIOBSERVACIONES AS 'Oxigeno Observacion',
	---
	CASE AMBTIPOAMBULANCIA   WHEN 1  THEN 'X' END AS 'Ambulancia Basica',
	CASE AMBTIPOAMBULANCIA   WHEN 2  THEN 'X' END AS 'Ambulancia Medicada',
	RTRIM(AMBTELEFONO) AS 'Ambulancia Telefono',
	Rtrim(AMBNOMBRERESPONSABLE) AS 'Ambulancia Nombre responsable',
	RTRIM(AMBOBSERVACIONES) AS 'Ambulancia Observaciones',
	---
	RTRIM(OBSERVACIONFORMULAMED) AS 'Formula Medica observacion',
	CASE z.TIPOATENCION WHEN 1 THEN 'X' END AS 'Atencion Domiciliaria',
	CASE z.TIPOATENCION WHEN 2 THEN 'X' END AS 'Hospitalizacion Casa'

from [dbo].[PADORDEN] AS A  with(nolock)
INNER JOIN PADCONTROL z on A.IDPADCONTROL = z.ID 
LEFT JOIN HCPARCONO B with(nolock) ON A.OXICODVIAADM = B.CODVIAADM   
WHERE IDPADCONTROL =  @IDPHDCONTROL

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera todos los datos generales de una orden de atención domiciliaria (PAD/PHD) dado el identificador del control de episodio domiciliario. Consolida en una sola consulta las indicaciones de enfermería (cambio de posición, aseo, lubricación de piel, movilización, sonda vesical, cuidado de heridas, catéteres, alimentación parenteral, medicación endovenosa), los parámetros de oxigenoterapia domiciliaria con su vía de administración, las condiciones de traslado en ambulancia y la programación de visitas médicas, tomando los datos de la orden (PADORDEN), el encabezado del episodio domiciliario (PADCONTROL) y la descripción de la vía de administración de oxígeno (HCPARCONO). Se utiliza para imprimir o visualizar el resumen completo de la fórmula/orden médica del programa de atención domiciliaria de un paciente, distinguiendo si el tipo de atención es domiciliaria simple o hospitalización en casa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de la orden de atención domiciliaria (PAD) asociada a un control específico, formateando indicadores booleanos como ''X'' y describiendo cuidados de enfermería, oxigenoterapia, ambulancia y tipo de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en PADORDEN cuyo IDPADCONTROL coincida con el control solicitado; Debe existir el control PAD relacionado en PADCONTROL para resolver el tipo de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los campos booleanos (1/0) se presentan al usuario como ''X'' o NULL, nunca como número crudo; El tipo de ambulancia y el tipo de atención son mutuamente excluyentes (solo se marca una columna a la vez); Se usa NOLOCK en PADORDEN y HCPARCONO permitiendo lecturas sucias; La vía de administración de oxígeno es opcional (LEFT JOIN), por lo que la orden se devuelve aun sin código de vía válido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención domiciliaria (PAD); Hospitalización en casa; Cuidados de enfermería; Visita médica domiciliaria; Manejo de heridas; Oxigenoterapia domiciliaria; Ambulancia básica/medicalizada; Sonda vesical; Catéteres periféricos; Alimentación parenteral; Fórmula médica; Resumen de historia clínica; Pertinencia PHD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.PADORDEN: Cuando IDPADCONTROL coincide con el parámetro, retorna las indicaciones generales de la orden domiciliaria con su control y vía de administración de oxígeno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cada bandera de cuidado (CAMBIOPOSICION, SUMINISTROALIMENT, ASEOBANOPACIENTE, LUBRICACIONPIEL, ASISTENCIAMOVILIZACION, ADMINISTRACIONENDOVENOSO, ALIMENTACIONPARENTAL, SONDAVESICAL, VIGILANCIAHERIDAS, CATETERESPERIFERICOS) = 1 → Se muestra ''X'' en la columna correspondiente else Se muestra NULL; si VISITAMEDICA_ACTUALIZACIONFORMULA = 1 → Marca con ''X'' la actualización de fórmulas médicas else NULL; si OXIBALADOMICILIARIA = 1 / OXIBALAPORTATIL = 1 → Marca con ''X'' el tipo de bala de oxígeno correspondiente else NULL; si AMBTIPOAMBULANCIA = 1 → Clasifica como ''Ambulancia Básica'' else Si AMBTIPOAMBULANCIA = 2 se clasifica como ''Ambulancia Medicada''; si z.TIPOATENCION = 1 → Marca ''Atención Domiciliaria'' else Si TIPOATENCION = 2 marca ''Hospitalización Casa''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.PADORDEN; dbo.PADCONTROL; dbo.HCPARCONO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_PAD_ListarDatosGeneralesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_PAD_ListarDatosGeneralesPAD';
-- GO

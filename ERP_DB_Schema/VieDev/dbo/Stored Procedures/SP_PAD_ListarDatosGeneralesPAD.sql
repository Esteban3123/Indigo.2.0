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
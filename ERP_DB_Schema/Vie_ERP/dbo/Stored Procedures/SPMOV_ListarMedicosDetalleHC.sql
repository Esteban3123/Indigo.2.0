
CREATE Procedure [dbo].[SPMOV_ListarMedicosDetalleHC]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
	SELECT  DISTINCT
		RTRIM(A.CODPROSAL) as CodigoMedico,
		RTRIM(C.DESESPECI) as Especialidad,
		RTRIM(B.NOMMEDICO) as NombreCompleto,
		A.FECHISPAC as Fecha,
		dbo.ObtenerFechaFormateada(A.FECHISPAC) as FechaFormateada,
		RTRIM(A.DATOBJETI) as Analisis,
		RTRIM(A.NUMEFOLIO) as Folio,
		CASE 
			WHEN A.IDMODELOHC IS NULL THEN 
				(CASE A.TIPHISPAC
					WHEN 'I' THEN 'Historia Clinica Ingreso'
					WHEN 'N' THEN 'Nota Evolución'
					WHEN 'E' THEN 'Evolución'
					WHEN 'O' THEN 'Otros modelos de apoyo'
					WHEN 'PT' THEN 'Partograma'
					WHEN 'NF' THEN 'Nota Farmaceutica'
					WHEN 'V' THEN 'Valoración de Seguimiento'
					WHEN 'F' THEN 'Consulta Preanestesia'	
					WHEN 'T' THEN 'Historia clinica de Control'
					WHEN 'S' THEN 'Servicio de apoyo'
					WHEN 'P' THEN 'Atención partos'
					WHEN 'B' THEN 'Recien Nacido'
					WHEN 'JM' THEN 'Junta Médica - Nota Evolución'
					ELSE 'Otro'
				END)
			WHEN A.IDMODELOHC IS NOT NULL THEN M.DESCRIPCION
		End as Tipo
	FROM 
		HCHISPACA AS A with(nolock)
		Inner Join INPROFSAL B with(nolock) on B.CODPROSAL = A.CODPROSAL
		Inner Join INESPECIA C with(nolock) on C.CODESPECI = A.CODESPTRA
		LEFT JOIN PRMODELOHC M with(nolock) ON M.ID = A.IDMODELOHC
	where 
		A.IPCODPACI	= @Paciente AND A.NUMINGRES = @Ingreso
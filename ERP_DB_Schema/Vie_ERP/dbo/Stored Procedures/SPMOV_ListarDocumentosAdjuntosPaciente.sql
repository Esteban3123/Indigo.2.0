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
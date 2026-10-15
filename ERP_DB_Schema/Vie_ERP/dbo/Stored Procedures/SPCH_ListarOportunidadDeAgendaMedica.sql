
CREATE PROCEDURE [dbo].[SPCH_ListarOportunidadDeAgendaMedica]
(
	  @CentroAtencion as char(10),
	  @FechoraInicial as date ,
	  @FechoraFianal as date

	 /*
		 @CentroAtencion as char(10) = '11011',
		 @FechoraInicial as date = '01/06/2024 00:00:00',
		 @FechoraFianal as date = '30/06/2024 23:59:59',
	 */

)
AS
BEGIN
	SET NOCOUNT ON;

WITH Tmp_ConsultaEspecialidad AS (
		
		select distinct agagemedc.CODAUTONU, agagemedc.CODPROSAL, agagemedc.CODESPECI, agagemedc.FECHORAIN,agagemedc.FECHORAFI, codcenate, codigocon,TIPAGEMED
			,MinutosCitasAsignadadas  = (
												Select isnull(SUM(DATEDIFF(MINUTE,FECHORAIN, FECHORAFI )),0) 
												FROM dbo.AGASICITA  With (nolock) 
												WHERE IDAGENDA  = agagemedc.CODAUTONU AND CODESTCIT NOT IN(4,5) AND CODCENATE=@CentroAtencion  
									   ) 
			,MinutosAgenda = (
									DATEDIFF(MINUTE,agagemedc.FECHORAIN, agagemedc.FECHORAFI)
							 )
			,ROW_NUMBER() OVER (PARTITION BY CODESPECI ORDER BY FECHORAIN asc) AS rn
			from dbo.AGAGEMEDC as agagemedc  WITH (nolock)
			WHERE   agagemedc.TIPAGEMED = 0 
				AND agagemedc.CODCENATE = @CentroAtencion 
				AND agagemedc.FECHORAIN  >= @FechoraInicial  
				AND agagemedc.FECHORAIN <= @FechoraFianal 
				AND (Select isnull(SUM(DATEDIFF(MINUTE,FECHORAIN, FECHORAFI )),0) FROM dbo.AGASICITA  With (nolock) 
												WHERE IDAGENDA  = agagemedc.CODAUTONU AND CODESTCIT NOT IN(4,5) AND CODCENATE=@CentroAtencion)  < (DATEDIFF(MINUTE,agagemedc.FECHORAIN, agagemedc.FECHORAFI))
)
--select CODAUTONU,CODPROSAL,CODESPECI, FECHORAIN, FECHORAFI,MinutosCitasAsignadadas,MinutosAgenda from Tmp_ConsultaEspecialidad  where rn = 1  
--select * from Tmp_ConsultaEspecialidad

		SELECT cte.codautonu AS codautonuAgenda,
			   concat(cte.CODESPECI ,' - ',ESPECIALIDAD.DESESPECI) as 'Especialidad Agenda',
			   fechorain as 'Fecha Hora inicial Agenda',
			   fechorafi as 'Fecha Hora Final Agenda',
			   Rtrim(cte.codprosal) AS 'Profesional Agenda',
			   Rtrim(PROFESIONALSALUD.nommedico)     AS 'Nombre Profesional',
			   cte.codcenate as 'Centro Atención',
			   concat(cte.codigocon , ' - ', UBICACION.DESCRICON) AS Consultorio,
			   cte.MinutosCitasAsignadadas, 
			   cte.MinutosAgenda,
			   CitasAgenda = (
				SELECT   CONCAT('Cita el: ', format(FECHORAIN,'dd/MM/yyyy HH:mm'), ' hasta las: ', format(FECHORAFI,'dd/MM/yyyy HH:mm'), ' / Paciente: ', IPCODPACI, '  ******  ' ) 
				FROM AGASICITA
				WHERE IDAGENDA = cte.CODAUTONU  AND CODESTCIT NOT IN(4,5) AND CODCENATE=@CentroAtencion  
				FOR XML PATH ('')   )					  		
		FROM  Tmp_ConsultaEspecialidad cte   WITH (nolock)
			   INNER JOIN dbo.adcenaten CENTROATENCION WITH (nolock)		 ON cte.codcenate = CENTROATENCION.codcenate
			   LEFT OUTER JOIN dbo.agconsult UBICACION WITH (nolock)		 ON cte.codigocon = UBICACION.codigocon AND UBICACION.codcenate = @CentroAtencion
			   LEFT OUTER JOIN dbo.inprofsal PROFESIONALSALUD WITH (nolock)	 ON cte.codprosal = PROFESIONALSALUD.codprosal
			   LEFT OUTER JOIN dbo.inespecia ESPECIALIDAD WITH (nolock)		 ON cte.codespeci = ESPECIALIDAD.codespeci
		WHERE   
		--		rn = 1  
				cte.TIPAGEMED = 0 
				AND cte.codcenate = @CentroAtencion 
				AND fechorain >= @FechoraInicial  
				AND fechorain <= @FechoraFianal 
	  order by cte.CODESPECI ASC, cte.FECHORAIN asc, cte.FECHORAFI asc
end
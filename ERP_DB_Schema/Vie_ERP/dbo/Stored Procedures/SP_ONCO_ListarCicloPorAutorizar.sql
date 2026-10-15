

CREATE PROCEDURE [dbo].[SP_ONCO_ListarCicloPorAutorizar]
(
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
			TMP.IdEsquema AS 'Id Esquema', Rtrim(S.Description) AS 'Nombre Esquema', Rtrim(A.CICLO) as 'Ciclo',Rtrim(A.SEMANA) as 'Semana',Rtrim(A.DIA) As 'Dia',A.ESTADOCICLO as 'Estado Ciclo',A.CICLO As 'Ciclo Actual',B.CICLOS AS 'Total Ciclos',
								  CAST(NULL AS INT) AS 'Autorizar',A.CICLO + 1 As 'Ciclo Autorizar', TMP.idUltimociclo 
			FROM (
						select distinct O.ID as IdEsquema, 
							  (SELECT TOP 1 X.ID   From [EHR].[HCQUICICLOS] x WHERE x.IDHCQUIORDENC =O.ID   AND x.CICLO = O.CICLOACTUAL Order by SEMANA desc, DIA desc  , CICLO DESC  ) AS idUltimociclo,
							  (SELECT TOP 1 X.ESTADOCICLO  From [EHR].[HCQUICICLOS] x WHERE x.IDHCQUIORDENC =O.ID  AND x.CICLO = O.CICLOACTUAL  Order by SEMANA desc, DIA desc  , CICLO DESC  ) AS EstadoUltimoCiclo,
							  (SELECT TOP 1 X.CICLO From [EHR].[HCQUICICLOS] x WHERE x.IDHCQUIORDENC =O.ID   AND x.CICLO = O.CICLOACTUAL  Order by SEMANA desc, DIA desc  , CICLO DESC  ) AS NumeroUltimoCiclo   									       
						from EHR.[HCQUIORDENC] O 
			 ) as TMP  	 INNER JOIN [EHR].[HCQUICICLOS] A with(nolock) on tmp.idUltimociclo = A.ID
						  INNER JOIN [EHR].[HCQUIORDENC] B with(nolock) on A.IDHCQUIORDENC  = B.ID 
						  INNER JOIN [EHR].OncologicalSchemes S with(nolock) on S.Id = B.IDOncologicalSchemes  
			  WHERE TMP.EstadoUltimoCiclo  = 2 and B.ESTADO IN (1,2,3) And b.IPCODPACI = @Paciente  And  A.CICLO <  B.CICLOS
			
					
	
	/*
	select * from (
			Select Top 1 A.IDHCQUIORDENC AS 'Id Esquema', Rtrim(C.Description) AS 'Nombre Esquema', Rtrim(A.CICLO) as 'Ciclo',Rtrim(A.SEMANA) as 'Semana',Rtrim(A.DIA) As 'Dia',A.ESTADOCICLO as 'Estado Ciclo',A.CICLO As 'Ciclo Actual',B.CICLOS AS 'Total Ciclos',
						  CAST(NULL AS INT) AS 'Autorizar',A.CICLO + 1 As 'Ciclo Autorizar'
				   From [EHR].[HCQUICICLOS] A 
					INNER JOIN [EHR].[HCQUIORDENC] B on A.IDHCQUIORDENC = b.ID 
					INNER JOIN [EHR].OncologicalSchemes C on C.Id = b.IDOncologicalSchemes  
			where B.ESTADO IN (1,2,3) And b.IPCODPACI = @Paciente  And  A.CICLO <  B.CICLOS
			Order by SEMANA desc, DIA desc  , CICLO DESC
		) as TMP where TMP.[Estado Ciclo] = 2*/
end
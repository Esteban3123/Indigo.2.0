
-- =============================================
-- Author:		<Author,Juan David Patiño Cabrera,Name>
-- Create date: <Create Date,11-12-2017,>
-- Description:	<Description,Listar los servicios extramurales en el dashboard de Autorizaciones,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AD_ListarServiciosAutorizadosExtramural]
	
(
@UnidadFuncional as varchar(200),
@Paciente as varchar(25),
@Ingreso as varchar(200)
)

AS
BEGIN
	
  Select cast(0 as bit) As Seleccione,A.CODCONCEC , RTRIM(A.CODSERIPS) AS 'Codigo',RTRIM(B.DESSERIPS) AS Descripcion,A.CANSERIPS AS Cantidad, CAST(0 AS INT) AS 'Cantidad Autorizada', (CAST(A.NUMEFOLIO AS INT)) AS Folio,CAST(1 AS BIT) AS PROSERIPS,'1' AS MEDIOSERV, CAST(0 AS BIT) AS 'NO POS',RTRIM(A.UFUCODIGO) AS UFUCODIGO,RTRIM(C.UFUDESCRI) AS UFUDESCRI, '' AS 'Justificacion Clinica',  RTRIM(ING.CODENTIDA) AS Entidad,    Case MEDIOSERV WHEN 1 then 'Servicio' ELSE 'Medicamento' END  AS tipoServicio,   JUSANULA as JustificacionAnula,   C.UFUTIPUNI,   ING.ICAUSAING, '' AS Archivo, A.SERSUSCEP, TIPOSERIPS
	,A.NUMINGRES,A.IPCODPACI   
	,CASE A.TIPOSERIPS   
			when 1 then 
				( select 
					case ESTSERIPS 
						when 1 then 'Solicitado'
						when 2 then 'Muestra recolectada'
						when 3 then 'Resultado entregado'
						when 4 then 'Exámen interpretado'
						when 5 then 'Remitido'
						when 6 then 'Anulado'
						when 7 then 'Extramural'
						when 8 then 'Muestra recolectada parcialmente'
					end as Estado
				from HCORDLABO  LAB
					INNER JOIN ADCONFSER S ON S.CODSERIPS = LAB.CODSERIPS 
					where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND LAB.CODSERIPS = A.CODSERIPS AND MANEXTPRO = 1)  
	        when 2 then 
				( select 
					case ESTSERIPS 
						when 1 then 'Solicitado'
						when 2 then 'Muestra recolectada'
						when 3 then 'Resultado entregado'
						when 4 then 'Exámen interpretado'
						when 5 then 'Remitido'
						when 6 then 'Anulado'
						when 7 then 'Extramural'
						when 8 then 'Muestra recolectada parcialmente'
					end as Estado
				   from HCORDPATO  PAT 
				   INNER JOIN ADCONFSER S ON S.CODSERIPS = PAT.CODSERIPS 
				   where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND PAT.CODSERIPS = A.CODSERIPS AND MANEXTPRO = 1)  
			when 3 then 
				( select 
					case ESTSERIPS 
						when 1 then 'Solicitado'
						when 2 then 'Estudio realizado'
						when 3 then 'Imagen procesada'
						when 4 then 'Estudio interpretado'
						when 5 then 'Remitido'
						when 6 then 'Anulado'
						when 7 then 'Extramural'
					end as Estado
				  from HCORDIMAG IMG
				   where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND IMG.CODSERIPS = A.CODSERIPS AND MANEXTPRO = 1) 
			when 4 then 
				( select 
					case ESTSERIPS 
						when 1 then 'Ordenado'
						when 2 then 'Completado'
						when 3 then 'Interpretado'
						when 4 then 'Sin interfaz'
						when 5 then 'Anulado'
					end as Estado
				 from HCORDPRON NoQX     
				 INNER JOIN ADCONFSER S ON S.CODSERIPS = NoQX.CODSERIPS 
				 where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND NoQX.CODSERIPS = A.CODSERIPS AND MANEXTPRO = 1)  
			when 5 then
				 ( select 
					case ESTSERIPS 
						when 1 then 'Solicitado'
						when 2 then 'Sala programada'
						when 3 then 'Cancelado'
						when 4 then 'Realizado'
					end as Estado
				  from HCORDPROQ QX
				  INNER JOIN ADCONFSER S ON S.CODSERIPS = QX.CODSERIPS
				  where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND QX.CODSERIPS = A.CODSERIPS AND MANEXTPRO = 1)
				    
		when 6 then 'Informe qx' 
		END as 'ESTADO'
		,CASE A.TIPOSERIPS   
			when 1 then 
				( select OBSSERIPS AS Observacion
				from HCORDLABO    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
	        when 2 then 
				( select OBSSERIPS AS Observacion
				   from HCORDPATO    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
			when 3 then 
				( select OBSSERIPS AS Observacion
				  from HCORDIMAG    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
			when 4 then 
				( select OBSSERIPS AS Observacion
				 from HCORDPRON    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
			when 5 then
				 ( select OBSSERIPS AS Observacion
				  from HCORDPROQ    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
		END as 'Observacion', CASE A.SOLINFOQX WHEN 1 THEN 'Si' ELSE 'No' END AS InformeQx
	from dbo.ADAUTOSER A  
	INNER JOIN INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS  
	INNER JOIN INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO  
	INNER JOIN ADINGRESO ING ON A.NUMINGRES=ING.NUMINGRES  
	INNER JOIN ADCONFSER S ON S.CODSERIPS = A.CODSERIPS AND SUSCEPTIB = 1   
	WHERE A.UFUCODIGO = @UnidadFuncional AND  A.IPCODPACI = @Paciente AND A.NUMINGRES=@Ingreso AND A.PROESTADO IN ('1') AND A.SOLEXTRAM = 1
	
	UNION ALL

	SELECT cast(0 as bit) As 'Seleccione', CAST(QD.ID as numeric) as 'CODCONCEC', RTRIM(QD.CODPRODUC) AS 'Codigo', RTRIM(H.DESPRODUC) AS 'Descripcion', QD.CANTSOLICIT AS 'Cantidad', CAST(0 AS INT) AS 'Cantidad Autorizada',
	(CAST(Q.NUMEFOLIO AS INT)) AS 'Folio', CAST(1 AS BIT) AS 'PROSERIPS', '1' AS 'MEDIOSERV', B.SERIPSPOS AS 'NO POS', RTRIM(Q.UFUCODIGO) AS 'UFUCODIGO', RTRIM(C.UFUDESCRI) AS 'UFUDESCRI', '' AS 'Justificacion Clinica',
	RTRIM(ING.CODENTIDA) AS 'Entidad', 'Material para Osteosíntesis'  AS 'tipoServicio', Q.OBSANULADO as 'JustificacionAnula', C.UFUTIPUNI, ING.ICAUSAING, '' AS Archivo, '' AS 'SERSUSCEP', '' AS 'TIPOSERIPS', Q.NUMINGRES, Q.IPCODPACI, 
	CASE QD.MATESTADO WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'Orden de compra' END AS 'ESTADO', Q.OBSSERIPS AS 'bservacion', '' AS InformeQx
	FROM HCORDPROQD QD
				INNER JOIN dbo.HCORDPROQ AS Q ON Q.AUTO = QD.AUTOPROCED  
				INNER JOIN dbo.IHLISTPRO AS H ON H.CODPRODUC = QD.CODPRODUC
				INNER JOIN INCUPSIPS B ON Q.CODSERIPS = B.CODSERIPS  
				INNER JOIN INUNIFUNC C ON Q.UFUCODIGO=C.UFUCODIGO  
				INNER JOIN ADINGRESO ING ON Q.NUMINGRES=ING.NUMINGRES
				LEFT join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  Q.IDDESCRIPCIONRELACIONADA  = CupsDescriptions.id 
				LEFT join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId      
	WHERE ING.UFUACTPAC = @UnidadFuncional AND  Q.IPCODPACI = @Paciente AND Q.NUMINGRES= @Ingreso AND QD.MATESTADO IN ('2') AND Q.MANEXTPRO = 1

END
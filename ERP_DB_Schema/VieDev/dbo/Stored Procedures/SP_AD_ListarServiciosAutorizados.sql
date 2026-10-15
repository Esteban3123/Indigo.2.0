CREATE PROCEDURE [dbo].[SP_AD_ListarServiciosAutorizados]
(
@UnidadFuncional as varchar(200),
@Paciente as varchar(25),
@Ingreso as varchar(200),
@Extramural as bit
)

AS

IF (@Extramural = 0) BEGIN ---- lista los servicios intrahospitalarios
		Select cast(0 as int) as TipoAutorizacion,A.IDDESCRIPCIONRELACIONADA, Descriptions.Name as DescripcionRelacionada,B.SERIPSPOS AS 'NO POS', x.NUMEFOLIO,X.IDETIPHIS, cast(0 as bit) As Seleccione, CAST(A.CODCONCEC as numeric) as CODCONCEC, RTRIM(A.CODSERIPS) AS 'Codigo',RTRIM(B.DESSERIPS) AS Descripcion,A.CANSERIPS AS Cantidad, CAST(0 AS INT) AS 'Cantidad Autorizada', (CAST(A.NUMEFOLIO AS INT)) AS Folio,CAST(1 AS BIT) AS PROSERIPS,'1' AS MEDIOSERV, CAST(0 AS BIT) AS 'NO POS',RTRIM(A.UFUCODIGO) AS UFUCODIGO,RTRIM(C.UFUDESCRI) AS UFUDESCRI, '' AS 'Justificacion Clinica',  RTRIM(ING.CODENTIDA) AS Entidad,    Case MEDIOSERV WHEN 1 then 'Servicios' ELSE 'Medicamento' END  AS tipoServicio,   JUSANULA as JustificacionAnula,   C.UFUTIPUNI,   ING.ICAUSAING, '' AS Archivo, A.SERSUSCEP, TIPOSERIPS
		,A.NUMINGRES,A.IPCODPACI    
		,CASE A.TIPOSERIPS   
		when 1 then 
				( select   TOP 1
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
				from HCORDLABO    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
		when 2 then 
				( select   TOP 1
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
				   from HCORDPATO    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
		when 3 then 
				( select   TOP 1
				case ESTSERIPS 
				when 1 then 'Solicitado'
				when 2 then 'Estudio realizado'
				when 3 then 'Imagen procesada'
				when 4 then 'Estudio interpretado'
				when 5 then 'Remitido'
				when 6 then 'Anulado'
				when 7 then 'Extramural'
				end as Estado
				  from HCORDIMAG    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
		when 4 then 
				( select   TOP 1
				case ESTSERIPS 
				when 1 then 'Ordenado'
				when 2 then 'Completado'
				when 3 then 'Interpretado'
				when 4 then 'Sin interfaz'
				when 5 then 'Anulado'
				end as Estado
				from HCORDPRON    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
		when 5 then
				( select   TOP 1
				case ESTSERIPS 
				when 1 then 'Solicitado'
				when 2 then 'Sala programada'
				when 3 then 'Cancelado'
				when 4 then 'Realizado'
				end as Estado
				  from HCORDPROQ    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS )  
		when 6 then 'Informe qx' 
		END as 'ESTADO'
		,CASE A.TIPOSERIPS
		when 1 then 
			( select top 1 OBSSERIPS AS Observacion
			from HCORDLABO    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS AND OBSSERIPS IS NOT NULL) 
		when 2 then 
			( select top 1 OBSSERIPS AS Observacion
			   from HCORDPATO    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS AND OBSSERIPS IS NOT NULL)  
		when 3 then 
			( select top 1 OBSSERIPS AS Observacion
			  from HCORDIMAG    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS AND OBSSERIPS IS NOT NULL)  
		when 4 then 
			( select top 1 OBSSERIPS AS Observacion
			from HCORDPRON    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS AND OBSSERIPS IS NOT NULL)  
		when 5 then
			( select top 1 OBSSERIPS AS Observacion
			  from HCORDPROQ    where NUMINGRES = A.NUMINGRES  AND IPCODPACI = A.IPCODPACI   AND NUMEFOLIO = A.NUMEFOLIO AND CODSERIPS = A.CODSERIPS AND OBSSERIPS IS NOT NULL)  
		END as 'Observacion', CASE A.SOLINFOQX WHEN 1 THEN 'Si' ELSE 'No' END AS InformeQx
		, CPC.Contracted AS 'Contratado', CPC.Quoted AS 'Cotizar'
		from ADAUTOSER A  
			INNER JOIN HCHISPACA X ON X.IPCODPACI = A.IPCODPACI AND   X.NUMEFOLIO = A.NUMEFOLIO AND X.NUMINGRES = A.NUMINGRES 
			INNER JOIN INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS  
			INNER JOIN INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO  
			INNER JOIN ADINGRESO ING ON A.NUMINGRES=ING.NUMINGRES
			LEFT join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  A.IDDESCRIPCIONRELACIONADA  = CupsDescriptions.id 
			LEFT join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId   
			INNER JOIN Contract.CareGroup CG ON CG.Id = ING.GENCAREGROUP
			INNER JOIN Contract.CUPSEntity CE ON CE.Code = A.CODSERIPS 
			LEFT JOIN Contract.ProcedureCups CPC ON CPC.ProceduresTemplateId = CG.ProcedureTemplateId AND CPC.CupsId = CE.Id AND ISNULL(CPC.CUPSEntityContractDescriptionId,0) = ISNULL(CupsDescriptions.id,0) AND ISNULL(CPC.ContractDescriptionId,0) = ISNULL(Descriptions.Id,0)
		WHERE --A.UFUCODIGO = @UnidadFuncional AND  
		A.IPCODPACI = @Paciente AND A.NUMINGRES=@Ingreso AND A.PROESTADO IN ('1')  AND A.SOLEXTRAM = 0
UNION ALL
		SELECT cast(1 as int) as TipoAutorizacion, '' as IDDESCRIPCIONRELACIONADA, '' As DescripcionRelacionada,B.NOPOSPROD AS 'NO POS', X.NUMEFOLIO,X.IDETIPHIS ,cast(0 as bit) As Seleccione, A.ID AS CODCONCEC, RTRIM(A.CODPRODUC) AS 'Codigo',RTRIM(B.DESPRODUC) AS Descripcion,A.CANPEDPRO AS Cantidad, CAST(0 AS INT) AS 'Cantidad Autorizada', (CAST(A.NUMEFOLIO AS INT)) AS Folio,CAST(1 AS BIT) AS PROSERIPS,'2' AS MEDIOSERV, CAST(1 AS BIT) AS 'NO POS',RTRIM(A.UFUCODIGO) AS UFUCODIGO,RTRIM(C.UFUDESCRI) AS UFUDESCRI, '' AS 'Justificacion Clinica',  RTRIM(ING.CODENTIDA) AS Entidad, 
		'Medicamentos NO PBS' AS 'tipoServicio', NULL as JustificacionAnula,   C.UFUTIPUNI,   ING.ICAUSAING, '' AS Archivo, '' AS SERSUSCEP, '' AS TIPOSERIPS, '' AS NUMINGRES, '' AS  IPCODPACI, 
		case PREESTADO 
		when 1 then 'Solicitado sin autorización'
		when 2 then 'Ciclo Completado'
		when 3 then 'Tratamiento descontinuado'
		when 4 then 'Tratamiento suspendido'
		when 5 then 'Plan de manejo externo'
		when 6 then 'Solicitado sin existencia'
		when 7 then 'Terminado por salida' 
		end AS ESTADO, '' AS Observacion, '' AS InformeQx, null as 'Contratado', null AS 'Cotizar'
		FROM HCPRESCRA A 
				INNER JOIN HCHISPACA X ON X.IPCODPACI = A.IPCODPACI AND   X.NUMEFOLIO = A.NUMEFOLIO AND X.NUMINGRES = A.NUMINGRES 
				INNER JOIN IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC
				INNER JOIN INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO
				INNER JOIN ADINGRESO ING ON A.NUMINGRES=ING.NUMINGRES   
		WHERE --A.UFUCODIGO = @UnidadFuncional AND  
		A.IPCODPACI = @Paciente AND A.NUMINGRES=@Ingreso 
		AND NOT EXISTS(Select IdHCPRESCRA from dbo.MedicationAuthorization o where o.IdHCPRESCRA = A.ID )
		AND (NOPOSPROD=1  OR EXISTS (SELECT top 1 IPCODPACI FROM HCJUNOPOM Z WHERE Z.IPCODPACI = @Paciente --AND Z.UFUCODIGO = @UnidadFuncional 
		AND Z.NUMINGRES=@Ingreso AND Z.CODPRODUC = A.CODPRODUC AND Z.NUMEFOLIO = A.NUMEFOLIO))

UNION ALL

	SELECT cast(0 as int) as TipoAutorizacion, Q.IDDESCRIPCIONRELACIONADA, Descriptions.Name As 'DescripcionRelacionada', B.SERIPSPOS AS 'NO POS', Q.NUMEFOLIO, Q.IDETIPHIS, cast(0 as bit) As 'Seleccione', CAST(QD.ID as numeric) as 'CODCONCEC', 
	RTRIM(QD.CODPRODUC) AS 'Codigo', RTRIM(H.DESPRODUC) AS 'Descripcion', QD.CANTSOLICIT AS 'Cantidad', CAST(0 AS INT) AS 'Cantidad Autorizada', (CAST(Q.NUMEFOLIO AS INT)) AS 'Folio', CAST(1 AS BIT) AS 'PROSERIPS',
	'1' AS 'MEDIOSERV', CAST(0 AS BIT) AS 'NO POS', RTRIM(Q.UFUCODIGO) AS 'UFUCODIGO', RTRIM(C.UFUDESCRI) AS 'UFUDESCRI', '' AS 'Justificacion Clinica', RTRIM(ING.CODENTIDA) AS 'Entidad',  
	'Material para Osteosíntesis'  AS 'tipoServicio', Q.OBSANULADO as 'JustificacionAnula', C.UFUTIPUNI, ING.ICAUSAING, '' AS Archivo, '' AS 'SERSUSCEP', 
	'' AS 'TIPOSERIPS', Q.NUMINGRES, Q.IPCODPACI, CASE QD.MATESTADO WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'Orden de compra' END AS 'ESTADO', Q.OBSSERIPS AS Observacion, '' AS InformeQx
	,CPC.Contracted AS 'Contratado', CPC.Quoted AS 'Cotizar'
	from HCORDPROQD QD
				INNER JOIN dbo.HCORDPROQ AS Q ON Q.AUTO = QD.AUTOPROCED  
				INNER JOIN dbo.IHLISTPRO AS H ON H.CODPRODUC = QD.CODPRODUC
				INNER JOIN INCUPSIPS B ON Q.CODSERIPS = B.CODSERIPS  
				INNER JOIN INUNIFUNC C ON Q.UFUCODIGO=C.UFUCODIGO  
				INNER JOIN ADINGRESO ING ON Q.NUMINGRES=ING.NUMINGRES
				LEFT join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  Q.IDDESCRIPCIONRELACIONADA  = CupsDescriptions.id 
				LEFT join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId 
				INNER JOIN Contract.CareGroup CG ON CG.Id = ING.GENCAREGROUP
				INNER JOIN Contract.CUPSEntity CE ON CE.Code = Q.CODSERIPS
				LEFT JOIN Contract.ProcedureCups CPC ON CPC.ProceduresTemplateId = CG.ProcedureTemplateId AND CPC.CupsId = CE.Id AND ISNULL(CPC.CUPSEntityContractDescriptionId,0) = ISNULL(CupsDescriptions.id,0) AND ISNULL(CPC.ContractDescriptionId,0) = ISNULL(Descriptions.Id,0)
	WHERE --ING.UFUACTPAC = @UnidadFuncional AND  
	Q.IPCODPACI = @Paciente AND Q.NUMINGRES= @Ingreso AND QD.MATESTADO IN ('2') AND Q.MANEXTPRO = 0

END
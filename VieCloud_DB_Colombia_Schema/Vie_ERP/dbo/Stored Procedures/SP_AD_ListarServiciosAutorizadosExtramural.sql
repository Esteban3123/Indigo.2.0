
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los servicios autorizados de tipo extramural (realizados fuera de la institución) asociados a un paciente, ingreso y unidad funcional específicos, para mostrarlos en el tablero de autorizaciones. Combina información de autorizaciones de servicios (ADAUTOSER), el catálogo de servicios CUPS/IPS (INCUPSIPS), las unidades funcionales (INUNIFUNC), el ingreso del paciente (ADINGRESO) y la configuración de servicios susceptibles (ADCONFSER), incluyendo órdenes de laboratorio, patología, imágenes diagnósticas, procedimientos no quirúrgicos, procedimientos quirúrgicos y materiales de osteosíntesis. Para cada servicio devuelve su código CUPS, descripción, cantidad solicitada, folio de autorización, estado actual de la orden (solicitado, realizado, anulado, extramural, entre otros), observaciones clínicas, entidad de salud, tipo de servicio y datos del paciente e ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista en el dashboard de autorizaciones los servicios y materiales de osteosíntesis solicitados como extramurales para un paciente, ingreso y unidad funcional, mostrando su estado actual según el tipo de orden clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un ingreso activo identificado por NUMINGRES y IPCODPACI.; Los servicios deben estar asociados a una unidad funcional válida (INUNIFUNC).; Para la primera consulta: el servicio debe estar marcado como susceptible (ADCONFSER.SUSCEPTIB = 1), en estado de proceso ''1'' y solicitado como extramural (SOLEXTRAM = 1).; Para la segunda consulta: la orden quirúrgica debe estar marcada como manejo extramural de productos (MANEXTPRO = 1) y el material en estado 2 (Orden de compra).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen servicios marcados como extramurales (SOLEXTRAM=1 o MANEXTPRO=1).; Los servicios listados son únicamente los configurados como susceptibles de autorización (SUSCEPTIB=1).; Solo se muestran servicios del paciente, ingreso y unidad funcional indicados.; El estado mostrado depende del tipo de servicio (TIPOSERIPS), garantizando que cada tipo se consulta en su tabla de orden correspondiente.; Los materiales solo se incluyen cuando el procedimiento quirúrgico tiene manejo extramural y el material está en estado ''Orden de compra''.; Los registros en estado de proceso distinto a ''1'' no se listan en la primera consulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios; Servicios extramurales; Paciente; Ingreso hospitalario; Unidad funcional; Órdenes de laboratorio; Órdenes de patología; Órdenes de imágenes diagnósticas; Procedimientos no quirúrgicos; Procedimientos quirúrgicos; Material de osteosíntesis; CUPS; Justificación clínica; Justificación de anulación; Informe quirúrgico; Servicios POS/NO POS; Entidad responsable', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.PROESTADO=''1'' y A.SOLEXTRAM=1 y SUSCEPTIB=1, se devuelven los servicios autorizados extramurales del paciente/ingreso/unidad funcional.; [RETURN_RESULT] resultset: Cuando QD.MATESTADO=2 (Orden de compra) y Q.MANEXTPRO=1, se devuelven los materiales de osteosíntesis asociados a órdenes quirúrgicas extramurales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOSERIPS = 1 → Se busca el estado en HCORDLABO (laboratorio) traduciendo ESTSERIPS a etiquetas como Solicitado, Muestra recolectada, Resultado entregado, etc.; si TIPOSERIPS = 2 → Se busca el estado en HCORDPATO (patología) traduciendo ESTSERIPS a etiquetas equivalentes a las de laboratorio.; si TIPOSERIPS = 3 → Se busca el estado en HCORDIMAG (imágenes) con etiquetas Solicitado, Estudio realizado, Imagen procesada, Estudio interpretado, Remitido, Anulado, Extramural.; si TIPOSERIPS = 4 → Se busca el estado en HCORDPRON (procedimientos no quirúrgicos) con etiquetas Ordenado, Completado, Interpretado, Sin interfaz, Anulado.; si TIPOSERIPS = 5 → Se busca el estado en HCORDPROQ (procedimientos quirúrgicos) con etiquetas Solicitado, Sala programada, Cancelado, Realizado.; si TIPOSERIPS = 6 → Se asigna el estado fijo ''Informe qx''.; si MEDIOSERV = 1 → El tipo se etiqueta como ''Servicio''. else Se etiqueta como ''Medicamento''.; si SOLINFOQX = 1 → InformeQx = ''Si''. else InformeQx = ''No''.; si QD.MATESTADO = 1 → Estado ''Solicitado'' para material de osteosíntesis.; si QD.MATESTADO = 2 → Estado ''Orden de compra'' para material de osteosíntesis.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADAUTOSER; dbo.INCUPSIPS; dbo.INUNIFUNC; dbo.ADINGRESO; dbo.ADCONFSER; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDIMAG; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORDPROQD; dbo.IHLISTPRO; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarServiciosAutorizadosExtramural';
-- GO

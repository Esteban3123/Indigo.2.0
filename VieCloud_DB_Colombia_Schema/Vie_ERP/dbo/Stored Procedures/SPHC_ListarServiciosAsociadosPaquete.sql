
CREATE PROCEDURE [dbo].[SPHC_ListarServiciosAsociadosPaquete]
(
@CodigoPaquete varchar(3)
)
AS
BEGIN
	
	SET NOCOUNT ON;
	
 SELECT * FROM (   
					select distinct Convert(BIT,0) As 'Seleccion', Convert(BIT,0) As 'ManejoExtramural', B.IDHCPAQORDENESC, a.CODIGO as CodigoPaquete, a.NOMBRE as NombrePaquete , 
					c.TIPSERIPS as TIPOSERVICIO, 
								  case C.TIPSERIPS 
										when 1 then 'Laboratorios' 
										when 2 then 'Patologias' 
										when 3 then 'Imágenes diagnósticas' 
										when 4 then 'Procedimientos no quirúrgicos'  
										--when 5 then 'Procedimientos quirúrgicos' 
										--when 6 then 'Interconsultas' 
										--when 7 then 'Ninguno' 
										--when 8 then 'Consulta externa' 
										--when 9 then 'Hemocomponentes' 				
							end as 'NombreTipoServicio',
					RTRIM(B.CODIGOSERVICIO) CODSERIPS, 
					RTRIM(C.DESSERIPS) DESSERIPS,
					RTRIM(F.Code) + ' - ' + RTRIM(F.Name) as 'DescripcionRelacionada',  B.IDDESCRIPCIONRELACIONADA, 
					B.OrderQuantity as Cantidad,
					case c.TIPSERIPS 
						when 1 then Common.GETDATE() --Laboratorios
						when 3 then Common.GETDATE() --Imágenes diagnósticas
						else NULL
					end as FechaSugerida,
					CAST(0 AS INT) as Specimen, '' as EspecimeNombre,
					CAST(0 AS INT) as Technique, '' as TecnicaNombre,
					CAST(0 AS INT) as CollectionMedium, '' MedioRecoleccionNombre,
					CAST(0 AS INT) As Lateralidad,
					'' LateralidadNombre,
					'2' as Prioridad,
					CAST (0 AS bit) AS ExamenSitio,
					c.TIPSERIPS,c.SERIPSDASH,
					c.RequiresLaterality AS 'RequiereLateralidad',
					isnull(TIPSERTER,0) AS Terapia,
					iif ((SELECT COUNT(*) FROM HCPLANDOC planC INNER JOIN HCPLANDOCCUPS planD ON planC.CODCONSEC = planD.IDHCPLANDOC WHERE planC.TIPDOCUME = 0 AND planD.CODSERIPS = B.CODIGOSERVICIO) >= 1, 2, 1) AS ConsentimientoInformado,
					C.SERREASIT AS ServicioRealizaSitio,
					B.ClinicalDataRelevant AS DatosClinicosRelevantes,
					CAST (0 AS bit) AS RequiereQuirofano,
					IPSSERIAD AS ServicioSeriado
					from  HCPAQORDENESC A
							inner join HCPAQORDENESD B on B.IDHCPAQORDENESC = A.ID 
							---inner join HCPAQORDENPRONOQXD D on D.IDHCPAQORDENESC = A.ID 
							inner join  INCUPSIPS C on B.CODIGOSERVICIO = C.CODSERIPS  and C.TIPSERIPS  IN (1,2,3,4) AND C.SIPSESTADO = 1
							Left Join  contract.CUPSEntityContractDescriptions E with(nolock) ON  E.Id = B.IDDESCRIPCIONRELACIONADA 
							Left Join  Contract.ContractDescriptions F with(nolock) ON F.Id = E.ContractDescriptionId 
					where a.CODIGO = @CodigoPaquete
UNION ALL
						select distinct
							Convert(BIT,0) As 'Seleccion',
							Convert(BIT,0) As 'ManejoExtramural',
							B.IDHCPAQORDENESC,
							a.CODIGO as CodigoPaquete,
							a.NOMBRE as NombrePaquete ,
							100 as TIPOSERVICIO, 
							'Insumos' as 'NombreTipoServicio',
							RTRIM(B.CODIGOSERVICIO) CODSERIPS, 
							RTRIM(C.DESPRODUC) DESSERIPS,
							NULL as 'DescripcionRelacionada', 
							NULL AS IDDESCRIPCIONRELACIONADA, 
							B.OrderQuantity as Cantidad,
							NULL as FechaSugerida,
							NULL as Specimen, 
							NULL as EspecimeNombre,
							NULL as Technique, 
							NULL as TecnicaNombre,
							NULL as CollectionMedium, 
							NULL MedioRecoleccionNombre,
							CAST(0 AS INT) As Lateralidad,
							'' LateralidadNombre,
							'2' as Prioridad,
							CAST (0 AS bit) AS ExamenSitio,
							100 AS TIPSERIPS,
							100 AS SERIPSDASH,
							NULL AS 'RequiereLateralidad',
							NULL AS Terapia,
							1 AS ConsentimientoInformado,
							0 AS ServicioRealizaSitio,
							'' AS DatosClinicosRelevantes,
							CAST (0 AS bit) AS RequiereQuirofano,
							0 AS ServicioSeriado
						from  HCPAQORDENESC A
								inner join HCPAQORDENESD B on B.IDHCPAQORDENESC = A.ID 
								--inner join HCPAQORDENPRONOQXD D on D.IDHCPAQORDENESC = A.ID 
								inner join  IHLISTPRO C on B.CODIGOSERVICIO = C.CODPRODUC AND  TIPPRODUC in ('2')
						where a.CODIGO = @CodigoPaquete
) xpc order by xpc.TIPOSERVICIO asc

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los servicios e insumos asociados a un paquete de órdenes de enfermería (historia clínica), dado el código del paquete. Combina dos conjuntos de datos: por un lado, los servicios clínicos (laboratorios, patologías, imágenes diagnósticas y procedimientos no quirúrgicos) obtenidos del catálogo CUPS/IPS con su descripción, tipo, cantidad, fecha sugerida, lateralidad, consentimiento informado y datos clínicos relevantes; por otro lado, los insumos o productos farmacéuticos asociados al mismo paquete. Para cada servicio también resuelve la descripción contractual vinculada (concepto de facturación del contrato) y devuelve todos los atributos necesarios para que la interfaz clínica pueda presentar y gestionar la orden de un paquete de atención al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios CUPS y los insumos farmacéuticos asociados a un paquete de atención, enriquecidos con descripción contractual y atributos clínicos necesarios para generar la orden del paquete.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un paquete en HCPAQORDENESC cuyo CODIGO coincida con el parámetro recibido; Los servicios del paquete deben estar registrados en HCPAQORDENESD vinculados al encabezado del paquete; Para CUPS: el código debe existir en INCUPSIPS, estar activo (SIPSESTADO=1) y ser de tipo 1,2,3 o 4; Para insumos: el código debe existir en IHLISTPRO con TIPPRODUC=''2''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen servicios CUPS activos (SIPSESTADO=1) y de tipos 1,2,3,4; Solo se incluyen insumos con TIPPRODUC=''2''; Insumos se etiquetan siempre con TIPOSERVICIO=100 y NombreTipoServicio=''Insumos''; Los campos Seleccion y ManejoExtramural se inicializan siempre en 0 (false); Prioridad por defecto = ''2'' para todas las filas; FechaSugerida solo se propone para laboratorios e imágenes diagnósticas; El resultado se ordena ascendentemente por TIPOSERVICIO; El consentimiento informado se determina por presencia de plantillas documentales tipo 0 asociadas al CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paquete de atención; Servicios CUPS (laboratorios, patologías, imágenes diagnósticas, procedimientos no quirúrgicos); Insumos / productos farmacéuticos; Consentimiento informado; Lateralidad; Descripción contractual (concepto de facturación); Terapia; Servicio seriado; Servicio que se realiza en sitio; Prioridad de orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la unión de servicios CUPS (INCUPSIPS con TIPSERIPS IN (1,2,3,4) y SIPSESTADO=1) e insumos (IHLISTPRO con TIPPRODUC=''2'') del paquete cuyo CODIGO = @CodigoPaquete, ordenado por TIPOSERVICIO ascendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPSERIPS del servicio (1=Laboratorios, 2=Patologías, 3=Imágenes diagnósticas, 4=Procedimientos no quirúrgicos) → Asigna nombre legible del tipo de servicio en ''NombreTipoServicio''; si TIPSERIPS = 1 (Laboratorio) o 3 (Imagen diagnóstica) → FechaSugerida = fecha actual (Common.GETDATE()) else FechaSugerida = NULL; si Existe al menos un registro en HCPLANDOC (TIPDOCUME=0) ligado a HCPLANDOCCUPS con el CODSERIPS del servicio → ConsentimientoInformado = 2 (requiere consentimiento) else ConsentimientoInformado = 1; si Origen de la fila: servicios CUPS vs insumos farmacéuticos → Para CUPS filtra INCUPSIPS con TIPSERIPS IN (1,2,3,4) y SIPSESTADO=1; para insumos filtra IHLISTPRO con TIPPRODUC=''2'' y marca TIPOSERVICIO=100 ''Insumos''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPAQORDENESC; dbo.HCPAQORDENESD; dbo.INCUPSIPS; contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.IHLISTPRO; dbo.HCPLANDOC; dbo.HCPLANDOCCUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosAsociadosPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosAsociadosPaquete';
-- GO

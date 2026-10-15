
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
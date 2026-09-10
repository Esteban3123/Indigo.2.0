
CREATE PROCEDURE [dbo].[SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica]
(
@Idsourcetable as Integer,
@Paciente Varchar(25),
@Ingreso  Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

if @Idsourcetable = 0 begin
	print '@Idsourcetable = 0'
			SELECT cast(0 as bit) as Seleccion, FAR.id, FAR.idsourcetable, FAR.SourceTable, FC.codconcec AS 'IDHCFARMEPC', FC.fechaorde AS 'FechaOrden',
				   LIS.coddcimed, Rtrim(FAR.codproduc) AS 'Codigo',
				   Rtrim(LIS.codproduc) + ' - ' + Rtrim(LIS.desproduc) AS 'Medicamento',
				   FAR.dosisprod, CONVERT(INT, FAR.sendto) AS 'SENDTO',
				   FAR.sendto
				   AS 'RoutingFrom',
				   CASE WHEN FAR.sendto <> 0 THEN 1 ELSE 0 END AS 'Validador',
				   (SELECT DISTINCT unitdosetypeid
					FROM   medicalhistory.pharmadose
					WHERE  codesusceptiblemixingstation = FAR.codesusceptiblemixingstation) AS
				   'IdTypeDose', NULl as IdMixingStation,
				   CONVERT(VARCHAR(50),FAR.codesusceptiblemixingstation) AS 'CodeSusceptibleMixingStation',
				   FAR.codunimed AS 'CODUNIMED',
				   (SELECT TOP 1 Rtrim(description)
					FROM   medicalhistory.routinglog
					WHERE  idhcfarmepd = FAR.id
					ORDER  BY creationdate DESC)
				   AS 'Observaciones',
				   CONVERT(BIT, CASE FAR.codproduc
								  WHEN (SELECT top 1 maindrugcode
										FROM
								  medicalhistory.productsusceptiblemixingstation
										WHERE  idorigin = FAR.idsourcetable and Origin = FAR.SourceTable)  THEN 1
								  ELSE 0
								END)
				   AS 'Principal',
				   CASE SourceTable
						WHEN 'HCINFLIQA' THEN (
							select RTRIM(MEZLIQPAC) FROM HCINFLIQA WHERE CONSECUTI = FAR.IdSourceTable
						)
						WHEN 'HCNUTPAREC' THEN (
							select RTRIM(C.NAME) FROM HCNUTPAREC A INNER JOIN HCPARNUTC C ON A.IDHCPARNUTC = C.ID WHERE A.ID = FAR.IdSourceTable
						)
						WHEN 'HCPRESCRA' THEN 'Prescripción individual de medicamentos'
						WHEN 'HCORDMEDICAM' THEN NULL
					END AS NombreProducto,
					FAR.CANPEDPRO AS Cantidad, FAR.FRECUENCI, FAR.UNIFRECUE, FAR.DURACIDOS, CASE FAR.DURACIDOS WHEN 'Fija' THEN FAR.VALDURFIJ ELSE 0 END AS 'Duracion', CASE FAR.DURACIDOS WHEN 'Fija' THEN FAR.UNIDURFIJ ELSE 0 END AS 'UnidadMedidaDuracion',
					CASE LIS.TIPFORMED WHEN 1 THEN LIS.PESTOTMED WHEN 2 THEN LIS.VOLTOTMED WHEN 4 THEN 1 ELSE LIS.PESTOTMED END AS ConcentracionMed, LIS.CALCANAUT AS 'Calcula', LIS.TIEESTMED AS 'TiempoVida',
					CASE WHEN (
						SELECT COUNT(*) 
						FROM HCINFLIQD A 
						INNER JOIN HCFARMEPD B ON A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES AND A.NUMEFOLIO = B.NUMEFOLIO AND A.CODPRODUC = B.CODPRODUC AND A.CANPASDIL = B.DOSISPROD 
						WHERE A.IPCODPACI = FAR.IPCODPACI 
						AND A.NUMINGRES = FAR.NUMINGRES
						AND A.NUMEFOLIO = FAR.NUMEFOLIO
						AND A.CODPRODUC = FAR.CODPRODUC
						AND A.CANPASDIL = FAR.DOSISPROD
				    ) > 0 THEN 1
				    ELSE 0
					END AS 'Vehicle',
					FAR.IDETIPHIS AS 'IDETIPHIS'
			FROM   HCFARMEPD FAR
				   INNER JOIN HCFARMEPC FC WITH(nolock) ON FC.codconcec = FAR.codconcec
				   INNER JOIN IHLISTPRO LIS WITH(nolock) ON LIS.codproduc = FAR.codproduc
			WHERE FAR.IPCODPACI = @Paciente AND FAR.NUMINGRES = @Ingreso AND FAR.VIEPROCESSED = 0 AND FAR.PROESTADO = 1
END 
ELSE IF @Idsourcetable <> 0 begin 
	print '@Idsourcetable > 0'
		SELECT cast(0 as bit) as Seleccion, FAR.id, FAR.idsourcetable, FAR.SourceTable, FC.codconcec AS 'IDHCFARMEPC', FC.fechaorde AS 'FechaOrden',
				   LIS.coddcimed, Rtrim(FAR.codproduc) AS 'Codigo',
				   Rtrim(LIS.codproduc) + ' - ' + Rtrim(LIS.desproduc) AS 'Medicamento',
				   FAR.dosisprod, CONVERT(INT, FAR.sendto) AS 'SENDTO',
				   FAR.sendto
				   AS 'RoutingFrom',
				   CASE WHEN FAR.sendto <> 0 THEN 1 ELSE 0 END AS 'Validador',
				   (SELECT DISTINCT unitdosetypeid
					FROM   medicalhistory.pharmadose
					WHERE  codesusceptiblemixingstation = FAR.codesusceptiblemixingstation) AS
				   'IdTypeDose', NULl as IdMixingStation,
				   CONVERT(VARCHAR(50),FAR.codesusceptiblemixingstation) AS 'CodeSusceptibleMixingStation',
				   FAR.codunimed AS 'CODUNIMED',
				   (SELECT TOP 1 Rtrim(description)
					FROM   medicalhistory.routinglog
					WHERE  idhcfarmepd = FAR.id
					ORDER  BY creationdate DESC)
				   AS 'Observaciones',
				   CONVERT(BIT, CASE FAR.codproduc
								  WHEN (SELECT top 1 maindrugcode
										FROM
								  medicalhistory.productsusceptiblemixingstation
										WHERE  idorigin = FAR.idsourcetable and Origin = FAR.SourceTable)  THEN 1
								  ELSE 0
								END)
				   AS 'Principal',
				   CASE SourceTable
						WHEN 'HCINFLIQA' THEN (
							select RTRIM(MEZLIQPAC) FROM HCINFLIQA WHERE CONSECUTI = FAR.IdSourceTable
						)
						WHEN 'HCNUTPAREC' THEN (
							select RTRIM(C.NAME) FROM HCNUTPAREC A INNER JOIN HCPARNUTC C ON A.IDHCPARNUTC = C.ID WHERE A.ID = FAR.IdSourceTable
						)
						WHEN 'HCPRESCRA' THEN 'Prescripción individual de medicamentos'
						WHEN 'HCORDMEDICAM' THEN NULL
					END AS NombreProducto,
					FAR.CANPEDPRO AS Cantidad, FAR.FRECUENCI, FAR.UNIFRECUE, FAR.DURACIDOS, CASE FAR.DURACIDOS WHEN 'Fija' THEN FAR.VALDURFIJ ELSE 0 END AS 'Duracion', CASE FAR.DURACIDOS WHEN 'Fija' THEN FAR.UNIDURFIJ ELSE 0 END AS 'UnidadMedidaDuracion',
					CASE LIS.TIPFORMED WHEN 1 THEN LIS.PESTOTMED WHEN 2 THEN LIS.VOLTOTMED WHEN 4 THEN 1 ELSE LIS.PESTOTMED END AS ConcentracionMed, LIS.CALCANAUT AS 'Calcula', LIS.TIEESTMED AS 'TiempoVida',
					CASE WHEN (
						SELECT COUNT(*) 
						FROM HCINFLIQD A 
						INNER JOIN HCFARMEPD B ON A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES AND A.NUMEFOLIO = B.NUMEFOLIO AND A.CODPRODUC = B.CODPRODUC AND A.CANPASDIL = B.DOSISPROD 
						WHERE A.IPCODPACI = FAR.IPCODPACI 
						AND A.NUMINGRES = FAR.NUMINGRES
						AND A.NUMEFOLIO = FAR.NUMEFOLIO
						AND A.CODPRODUC = FAR.CODPRODUC
						AND A.CANPASDIL = FAR.DOSISPROD
				    ) > 0 THEN 1
				    ELSE 0
					END AS 'Vehicle',
					FAR.IDETIPHIS AS 'IDETIPHIS'
			FROM   HCFARMEPD FAR
				   INNER JOIN HCFARMEPC FC WITH(nolock) ON FC.codconcec = FAR.codconcec
				   INNER JOIN IHLISTPRO LIS WITH(nolock) ON LIS.codproduc = FAR.codproduc
			WHERE FAR.IPCODPACI = @Paciente AND FAR.NUMINGRES = @Ingreso AND FAR.VIEPROCESSED = 0 AND FAR.PROESTADO = 1 and far.IdSourceTable = @Idsourcetable
   end
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle de medicamentos pendientes de gestión logística farmacéutica para un paciente e ingreso específicos, combinando las órdenes de despacho (HCFARMEPD/HCFARMEPC) con el catálogo de productos (IHLISTPRO) para obtener nombre, código, dosis, frecuencia, duración, concentración y unidad de medida de cada ítem. Enriquece cada línea con información de enrutamiento y observaciones del historial de traslados (RoutingLog), el tipo de dosis unitaria (PharmaDose), si el producto es susceptible de mezcla en estación de preparación (ProductSusceptibleMixingStation) y si actúa como vehículo diluyente. Según el parámetro IdSourceTable, filtra medicamentos de toda la orden o de una fuente específica (mezcla líquida, nutrición parenteral, prescripción individual u orden médica), soportando así la preparación y validación de pedidos en la cadena logística de farmacia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el detalle de medicamentos prescritos a un paciente/ingreso para gestión logística farmacéutica, enriqueciendo con datos de origen (mezclas, nutrición parenteral, prescripción) y marcando principal/vehículo/ruteo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe el paciente e ingreso indicados con registros en HCFARMEPD; Los registros a listar deben tener VIEPROCESSED = 0 (no procesados en vista); Los registros a listar deben tener PROESTADO = 1 (estado activo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen registros activos (PROESTADO=1) y no procesados (VIEPROCESSED=0); Seleccion siempre se devuelve como bit 0 por defecto; La observación corresponde al último registro de routinglog por creationdate DESC; El NombreProducto depende del tipo de origen (SourceTable) del detalle; Un medicamento es ''Principal'' solo si coincide con el maindrugcode de su origen en productsusceptiblemixingstation', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Prescripción de medicamentos; Mezcla de líquidos; Nutrición parenteral; Dosis farmacéutica; Estación de mezclas (mixing station); Ruteo farmacéutico; Vehículo de mezcla; Concentración de medicamento; Duración de tratamiento; Frecuencia de administración', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @Idsourcetable = 0 retorna todos los detalles de medicamentos del paciente/ingreso con VIEPROCESSED=0 y PROESTADO=1; [RETURN_RESULT] resultset: Cuando @Idsourcetable <> 0 retorna solo los detalles cuyo IdSourceTable coincide con el parámetro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Idsourcetable = 0 → Lista todos los medicamentos del paciente/ingreso sin filtrar por origen else Filtra adicionalmente por FAR.IdSourceTable = @Idsourcetable; si FAR.sendto <> 0 → Marca Validador = 1 (tiene ruteo asignado) else Validador = 0; si FAR.codproduc coincide con maindrugcode de productsusceptiblemixingstation para ese origen → Marca Principal = 1 else Principal = 0; si SourceTable = ''HCINFLIQA'' → NombreProducto = MEZLIQPAC de HCINFLIQA (mezcla de líquidos); si SourceTable = ''HCNUTPAREC'' → NombreProducto = nombre de parámetro de nutrición parenteral (HCPARNUTC.NAME); si SourceTable = ''HCPRESCRA'' → NombreProducto = ''Prescripción individual de medicamentos''; si SourceTable = ''HCORDMEDICAM'' → NombreProducto = NULL; si FAR.DURACIDOS = ''Fija'' → Duracion = VALDURFIJ y UnidadMedidaDuracion = UNIDURFIJ else Duracion = 0 y UnidadMedidaDuracion = 0; si LIS.TIPFORMED = 1 → ConcentracionMed = PESTOTMED (peso total); si LIS.TIPFORMED = 2 → ConcentracionMed = VOLTOTMED (volumen total); si LIS.TIPFORMED = 4 → ConcentracionMed = 1 else ConcentracionMed = PESTOTMED; si Existe coincidencia en HCINFLIQD con HCFARMEPD por paciente/ingreso/folio/producto/dosis → Vehicle = 1 (es vehículo de mezcla) else Vehicle = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.IHLISTPRO; medicalhistory.pharmadose; medicalhistory.routinglog; medicalhistory.productsusceptiblemixingstation; dbo.HCINFLIQA; dbo.HCNUTPAREC; dbo.HCPARNUTC; dbo.HCINFLIQD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentoDetalleGestionLogisticaFarmaceutica';
-- GO

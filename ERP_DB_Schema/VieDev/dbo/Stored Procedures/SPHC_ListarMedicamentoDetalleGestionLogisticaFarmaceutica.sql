
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
					END AS 'Vehicle'
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
					END AS 'Vehicle'
			FROM   HCFARMEPD FAR
				   INNER JOIN HCFARMEPC FC WITH(nolock) ON FC.codconcec = FAR.codconcec
				   INNER JOIN IHLISTPRO LIS WITH(nolock) ON LIS.codproduc = FAR.codproduc
			WHERE FAR.IPCODPACI = @Paciente AND FAR.NUMINGRES = @Ingreso AND FAR.VIEPROCESSED = 0 AND FAR.PROESTADO = 1 and far.IdSourceTable = @Idsourcetable
   end
end
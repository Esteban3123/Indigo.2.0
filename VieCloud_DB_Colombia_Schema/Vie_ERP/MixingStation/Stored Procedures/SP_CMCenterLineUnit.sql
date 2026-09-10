
-- =============================================
-- Author:		HECTOR RODRIGUEZ
-- Create date: 28-01-2020
-- Description:	Consulta unidades funcionales de centros de atencion de una central de mezcla
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_CMCenterLineUnit]
	@Xml xml
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Tabla en donde se almacenan los centros de atencion y las líneas de producción
	declare @TableCMCenterLine table(CodeCenterAttention CHAR(10), ProductionLineId int, StatusCL BIT)
	declare @MixingStationId int

	--Fecha actual y hora que se asigna por defecto para el campo CMCLU.FirstDeliveryTime si este no existe
	declare @currentlydate date = CURRENT_TIMESTAMP 
	declare @initialtime time = '07:00:00.000'

	select
	@MixingStationId = t.x.value('MixingStationId[1]', 'int')
	from @Xml.nodes('/Parametros') t(x)

	--Se obtienen la relación centro de atención - línea de producción
	insert into @TableCMCenterLine
	select 
		t.x.value('Center[1]','CHAR(10)') as CodeCenterAttention,
		t.x.value('Line[1]','int') as ProductionLineId,
		t.x.value('StatusCL[1]','bit') as StatusCL
	from @Xml.nodes('/Parametros/CL') t(x)

    SELECT DISTINCT
	CodeCenterAttention = CMCA.CodeCenterAttention
	,CMCA.ProductionLineId
	, NameProductionLine = PL.Code + ' - ' + PL.Name
	, CodeFunctionalUnit = UF.UFUCODIGO
	, NameFunctionalUnit = RTRIM(UF.UFUDESCRI)
	, CMCenterLineUnitId = ISNULL(CMCLU.Id,0)
	,IIF(CMCLU.EveryTimeDeliverId is NULL, CAST(4 as tinyint) , CMCLU.EveryTimeDeliverId) EveryTimeDeliverId  
	--, CMCLU.EveryTimeDeliverId 
	,IIF(CMCLU.FirstDeliveryTime is NULL, convert(datetime, @currentlydate) + convert(datetime, @initialtime),CMCLU.FirstDeliveryTime) FirstDeliveryTime
	--, CMCLU.FirstDeliveryTime 
	, CMCLU.SecondDeliveryTime
	, CMCLU.ThirdDeliveryTime
	, CMCLU.FourthDeliveryTime
	, StatusCLU = ISNULL(CMCLU.StatusCLU,CMCA.StatusCL)
	, CMCA.StatusCL
	, EveryTimeDeliverName = CASE CMCLU.EveryTimeDeliverId WHEN 1 THEN '6 horas' WHEN 2 THEN '8 horas' WHEN 3 THEN '12 horas' WHEN 4 THEN '24 horas' ELSE '24 horas' END
	FROM 
	@TableCMCenterLine CMCA
	INNER JOIN MixingStation.ProductionLine PL WITH(NOLOCK)
	ON PL.Id = CMCA.ProductionLineId
	INNER JOIN .INCENUNFU CAUF WITH(NOLOCK)
	ON CAUF.CODCENATE = CMCA.CodeCenterAttention
	INNER JOIN .INUNIFUNC UF WITH(NOLOCK)
	ON UF.UFUCODIGO = CAUF.UFUCODIGO
	AND UF.UFUTIPUNI IN (2,5,6,7,8,9,10,11,13,17,23)
	LEFT OUTER JOIN MixingStation.CMCenterLineUnit CMCLU WITH(NOLOCK)
	ON CMCLU.MixingStationId = @MixingStationId
	AND CMCLU.CodeCenterAttention = CAUF.CODCENATE
	AND CMCLU.ProductionLineId = PL.Id
	AND CMCLU.CodeFunctionalUnit = UF.UFUCODIGO

	--IFF(CMCLU.EveryTimeDeliverId = NULL, CMCLU.EveryTimeDeliverId = '4' , CMCLU.EveryTimeDeliverId) 
	--UF.UFUTIPUNI
	--Hospitalización Codigo 2
	--Unidad de Cuidados Intensivos Adulto Código 5
	--Unidad de Cuidados Intermedios Adulto Código- 6 -
	--Unidad de Cuidados Intensivos Pediátrico Código  7 
	--Unidad de Cuidados Intermedios Pediátrico Código- 8 
	--Unidad de Cuidados Intensivos Neonatal Código- 9 
	--Unidad de Cuidados Intermedios Neonatal Código 10 
	--Unidad de Cuidados Básicos Neonatal Código 11 
	--Gineco-Obstetricia 23.

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las unidades funcionales (salas de hospitalización, UCI, intermedios, neonatología, ginecología, entre otras) asociadas a los centros de atención y líneas de producción de una central de mezclas farmacéuticas. Recibe como entrada un XML con el identificador de la central de mezclas y la relación centro de atención / línea de producción, cruza esa información con el catálogo maestro de unidades funcionales (INUNIFUNC) y la tabla de relación centro-unidad (INCENUNFU), filtrando solo los tipos de unidad clínica relevantes para dispensación. Retorna, por cada unidad funcional elegible, los horarios de entrega configurados (primera, segunda y tercera entrega), la frecuencia de despacho (cada 6, 8, 12 o 24 horas) y el estado activo/inactivo, tomando valores predeterminados cuando la unidad aún no tiene configuración registrada en CMCenterLineUnit.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_CMCenterLineUnit';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_CMCenterLineUnit';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para una central de mezcla y un conjunto de pares centro-de-atención/línea-de-producción, las unidades funcionales asociadas junto con la configuración de horarios de entrega aplicando valores por defecto cuando no existe configuración previa.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CMCenterLineUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /Parametros con MixingStationId y un conjunto de nodos /Parametros/CL con Center, Line y StatusCL.; Cada Center recibido debe existir en INCENUNFU y la línea en MixingStation.ProductionLine para que el INNER JOIN retorne resultados.; Las unidades funcionales relacionadas deben tener UFUTIPUNI dentro de los tipos clínicos permitidos (2,5,6,7,8,9,10,11,13,17,23).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CMCenterLineUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran unidades funcionales cuyo tipo (UFUTIPUNI) corresponde a hospitalización, UCI/UCIN adulto, pediátrico y neonatal, cuidados básicos neonatales y gineco-obstetricia (códigos 2,5,6,7,8,9,10,11,13,17,23).; La frecuencia de entrega por defecto siempre es 24 horas (EveryTimeDeliverId=4).; El primer horario de entrega por defecto siempre es a las 07:00 del día actual.; Si no existe configuración previa en CMCenterLineUnit, CMCenterLineUnitId se devuelve como 0.; El emparejamiento con CMCenterLineUnit se restringe siempre al MixingStationId recibido.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CMCenterLineUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Central de mezcla; Centro de atención; Línea de producción; Unidad funcional; Hospitalización; Unidad de Cuidados Intensivos (adulto/pediátrico/neonatal); Unidad de Cuidados Intermedios (adulto/pediátrico/neonatal); Cuidados Básicos Neonatales; Gineco-Obstetricia; Frecuencia y horarios de entrega de medicamentos', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CMCenterLineUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Retorna un conjunto distinct con centro de atención, línea de producción, unidad funcional y configuración (CMCenterLineUnitId, horarios y frecuencia de entrega) usando defaults cuando no existe registro en MixingStation.CMCenterLineUnit.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CMCenterLineUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CMCLU.EveryTimeDeliverId IS NULL → Asigna por defecto el valor 4 (equivalente a ''24 horas'') como frecuencia de entrega. else Conserva el valor configurado en CMCenterLineUnit.; si CMCLU.FirstDeliveryTime IS NULL → Asigna como primer horario la fecha actual a las 07:00:00. else Conserva FirstDeliveryTime registrado.; si CMCLU.StatusCLU IS NULL → Toma el StatusCL provisto en el XML para el par centro-línea. else Conserva el StatusCLU registrado.; si CMCLU.EveryTimeDeliverId IN (1,2,3,4) o distinto → Traduce a etiqueta legible: 1=''6 horas'', 2=''8 horas'', 3=''12 horas'', 4=''24 horas'', cualquier otro valor o NULL se rotula como ''24 horas''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CMCenterLineUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.ProductionLine; INCENUNFU; INUNIFUNC; MixingStation.CMCenterLineUnit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CMCenterLineUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CMCenterLineUnit';
-- GO

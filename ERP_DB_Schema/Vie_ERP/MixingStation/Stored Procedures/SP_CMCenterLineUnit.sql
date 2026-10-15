
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
	INNER JOIN MixingStation.ProductionLine PL
	ON PL.Id = CMCA.ProductionLineId
	INNER JOIN .INCENUNFU CAUF
	ON CAUF.CODCENATE = CMCA.CodeCenterAttention
	INNER JOIN .INUNIFUNC UF
	ON UF.UFUCODIGO = CAUF.UFUCODIGO
	AND UF.UFUTIPUNI IN (2,5,6,7,8,9,10,11,13,17,23)
	LEFT OUTER JOIN MixingStation.CMCenterLineUnit CMCLU
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
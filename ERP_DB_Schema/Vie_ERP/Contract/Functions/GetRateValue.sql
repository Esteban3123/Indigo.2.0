CREATE Function [Contract].[GetRateValue]
(
	@CupsEntityId Int,
	@CareGroupId Int,
	@FunctionalUnitId Int,
	@SpecialtyId Char(3),
	@ServiceDate DateTime,
	@InitialService Tinyint,
	@RiasId int = 0,
	@ContractDescriptionId int = 0,
	@IPSServiceId INT
)
Returns @DefinitionRateDetail Table
(
	StateResult Bit,
	MessageResult Varchar(Max),
	ConditionType Tinyint,	
	LogicalOperator Tinyint,
	ConditionType2 Tinyint,

	DefinitionRateDetailId Int,
	LiquidateionType Tinyint Null,
	ManualType Tinyint Null,
	RateManualValidityId Int Null,
	RateManualId Int Null,
	AllowValueChange Bit,
	RateVariation Decimal(5, 2) Null,
	SalesValue Decimal(18, 2) Null,
	SalesValueWithSurcharge Decimal(18, 2) Null,

	DefinitionRateDetailConditionId Int Null,	
	LiquidateionTypeAux Tinyint Null,
	ManualTypeAux Tinyint Null,
	RateManualValidityIdAux Int Null,
	RateManualIdAux Int Null,
	RateVariationAux Decimal(5, 2) Null,
	SalesValueAux Decimal(18, 2) Null,
	SalesValueWithSurchargeAux Decimal(18, 2) Null
) 
As
Begin
	
	Declare @Id Int, @NProcedureCups Int, @ProcedureTemplateId Int
		, @CareGroupCode Varchar(20), @CareGroupName Varchar(100)

	Select @Id = cg.Id
		, @CareGroupCode = cg.Code
		, @CareGroupName = cg.[Name]
		, @ProcedureTemplateId = cg.ProcedureTemplateId
	From [Contract].CareGroup cg With(Nolock)
	Where Id = @CareGroupId
	
	Select @NProcedureCups = Count(1) 
	From [Contract].ProcedureCups With(Nolock)
	Where ProceduresTemplateId = @ProcedureTemplateId And CupsId = @CupsEntityId and (ContractDescriptionId is null or ContractDescriptionId = @ContractDescriptionId )

	If @NProcedureCups > 0 Begin
		
		Insert Into @DefinitionRateDetail
		Select StateResult, MessageResult, ConditionType, LogicalOperator, ConditionType2
			, DefinitionRateDetailId, LiquidateionType, ManualType, RateManualValidityId, RateManualId, AllowValueChange, RateVariation, SalesValue, SalesValueWithSurcharge
			, DefinitionRateDetailConditionId, LiquidateionTypeAux, ManualTypeAux, RateManualValidityIdAux, RateManualIdAux, RateVariationAux, SalesValueAux, SalesValueWithSurchargeAux
		From [Contract].[GetRateValueCondition](@CupsEntityId, @CareGroupId, @FunctionalUnitId, @SpecialtyId, @ServiceDate, @InitialService, @RiasId, @ContractDescriptionId, @IPSServiceId)
		
	End
	Else Begin
		Declare @CodeCups Varchar(20), @NameCups Varchar(300)
		Select @CodeCups = Code, @NameCups = [Description] From [Contract].CUPSEntity With(Nolock)
		Where Id = @CupsEntityId
		Insert Into @DefinitionRateDetail
		Values (0, 'El CUPS ' + @CodeCups + ' - ' + @NameCups + ' no esta cubierto en el grupo de atencion ' + @CareGroupCode + ' - ' + @CareGroupName
			,0,0,0,0,Null,Null,Null,0,Null,Null,Null,Null,Null,Null,Null,Null,Null,NULL, NULL, NULL)
	End	
	Return

End
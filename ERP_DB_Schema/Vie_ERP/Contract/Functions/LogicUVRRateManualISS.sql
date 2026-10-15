/*
<LogicUVRRateManualISS>
<RateManualId></RateManualId>
<IPSServiceParentId></IPSServiceParentId>
<IPSServiceChildId></IPSServiceChildId>
<serviceAmount></serviceAmount>
</LogicUVRRateManualISS>
*/

CREATE Function [Contract].[LogicUVRRateManualISS]
(
	@ParametersXml Xml

)
Returns @ServiceOrderDetail Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	CostValueItem Decimal(20,2),
	RateManualDetailSurgicalId INT
) 
As
Begin
	DECLARE @_serviceClassTable as Table (	ServiceClass tinyint,
											ServiceClassName varchar(20))
	DECLARE @_rateManualId INT,
			@_iPSServiceParentId INT,
			@_iPSServiceChildId As INT,
			@_serviceAmount As NUMERIC(20,2),
			@_costValueItem  As NUMERIC(20,2)=0,
			@_serviceManualType as tinyint,
			@_serviceClass AS TINYINT,
			@_uVRNumber	as INT,
			@_const_UVRNUMBER as INT = 450,
			@_applyChangeScore BIT =0,
			@_score decimal(20,2),
			@_rateManualDetailSurgicalId INT =NULL

	-- se almacenan los datos del input en la tabla variable
	SELECT
	@_rateManualId = t.x.value('RateManualId[1]', 'int') ,
	@_iPSServiceParentId = t.x.value('IPSServiceParentId[1]', 'INT'),
	@_iPSServiceChildId = t.x.value('IPSServiceChildId[1]', 'INT'),
	@_serviceAmount = t.x.value('ServiceAmount[1]', 'NUMERIC(20,2)')
	from @ParametersXml.nodes('/LogicUVRRateManualISS') t(x);
	

	IF COALESCE( @_rateManualId,0)=0 OR COALESCE( @_iPSServiceParentId,0)=0 OR COALESCE( @_iPSServiceChildId,0)=0
	BEGIN
		INSERT INTO  @ServiceOrderDetail VALUES(0,'faltan parámetros en la función de obtención de valores ISS',0,NULL)
		 RETURN
	END

	SELECT top 1 @_serviceManualType = ServiceManual,
				 @_uVRNumber = UVRNumber
	from Contract.IPSService WITH(NOLOCK) where Id= @_iPSServiceParentId
	
	SELECT top 1 @_serviceClass =ServiceClass,
				 @_applyChangeScore = ApplyChangeScore,
				 @_score = IIF(ApplyChangeScore=1 AND @_uVRNumber > @_const_UVRNUMBER ,NewScore,Score)
	from Contract.IPSService WITH(NOLOCK) 
	where Id= @_iPSServiceChildId
	
	If @_serviceManualType >= 3
	BEGIN
		INSERT INTO  @ServiceOrderDetail VALUES(0,'El tipo de manul no es ISS',0,NULL)
		 RETURN
    End 

	INSERT into @_serviceClassTable VALUES(2,'Surgeon'),(3,'Anesthesiologist'),(4,'Anesthesiologist'),(5,'RightRoom')

	IF EXISTS(	SELECT 1 
				FROM @_serviceClassTable
				WHERE ServiceClass IN (2,3,4) AND ServiceClass =@_serviceClass ) OR (@_serviceClass = 5 AND  @_uVRNumber > @_const_UVRNUMBER AND @_applyChangeScore=1 )
	BEGIN
	   
		SET   @_costValueItem = @_uVRNumber * @_score * @_serviceAmount		
		INSERT INTO  @ServiceOrderDetail VALUES(1,'Valor generado exitosamente',@_costValueItem,NULL)
		RETURN
	END
	ELSE
	BEGIN
			Select Top 1 @_rateManualDetailSurgicalId = rms.Id, @_costValueItem = ( rms.SalesValue * @_serviceAmount)
			From [Contract].RateManualDetailSurgical rms With(Nolock)
			Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
			Where RateManualId = @_rateManualId And IPSServiceId = @_iPSServiceChildId And @_uVRNumber >= uvr.InitialUVR And @_uVRNumber <= uvr.EndUVR

			IF COALESCE(@_rateManualDetailSurgicalId,0,NULL)=0
			BEGIN
				INSERT INTO  @ServiceOrderDetail VALUES(0,'El servicio IPS QX no esta parámetrizado dentro del manual de tarifas',0,NULL)
				RETURN
			END
	END

	INSERT INTO  @ServiceOrderDetail VALUES(1,'Valor generado exitosamente',@_costValueItem,@_rateManualDetailSurgicalId)	
	RETURN
End
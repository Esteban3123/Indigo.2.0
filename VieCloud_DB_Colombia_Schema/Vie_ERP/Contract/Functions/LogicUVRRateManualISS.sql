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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tarificación quirúrgica que calcula el valor económico de un servicio IPS de tipo quirúrgico según el manual de tarifas ISS (manual tipo 1 o 2). Recibe por XML el identificador del manual de tarifas, el servicio padre (que aporta el número UVR y el tipo de manual), el servicio hijo (que aporta la clase de servicio, el puntaje y si aplica cambio de puntaje) y la cantidad del servicio a liquidar. Para cirujanos, anestesiólogos y sala de operaciones con UVR superior al umbral de 450, calcula el valor multiplicando UVR × puntaje × cantidad; para los demás casos, busca en la tabla de detalle de tarifas quirúrgicas el rango UVR que corresponde al servicio hijo y multiplica el valor de venta por la cantidad. Devuelve el valor calculado (costo del ítem), el identificador del detalle de tarifa quirúrgica utilizado y un indicador de éxito o mensaje de error, siendo utilizada principalmente en la liquidación y facturación de procedimientos quirúrgicos contratados bajo manual ISS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'LogicUVRRateManualISS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'LogicUVRRateManualISS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el valor (costo) de un ítem de servicio quirúrgico bajo manual tarifario ISS, aplicando UVR y score, o consultando el detalle de tarifa quirúrgica según el rango UVR.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'LogicUVRRateManualISS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben suministrarse RateManualId, IPSServiceParentId e IPSServiceChildId distintos de cero/no nulos.; El servicio padre debe existir en Contract.IPSService con un ServiceManual definido.; El servicio padre debe tener ServiceManual menor que 3 (es decir, manual tipo ISS).; El servicio hijo debe existir en Contract.IPSService con ServiceClass, ApplyChangeScore, Score/NewScore.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'LogicUVRRateManualISS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El umbral de UVR para aplicar cambio de score es la constante 450.; Solo se procesan servicios cuyo ServiceManual sea menor que 3 (manual ISS).; Los ServiceClass 2,3,4 corresponden a Cirujano/Anestesiólogo y siempre calculan por fórmula UVR*Score*Cantidad.; El ServiceClass 5 (RightRoom) solo entra en la fórmula UVR*Score*Cantidad si UVR>450 y ApplyChangeScore=1; en caso contrario va al detalle quirúrgico.; Cuando el cálculo se hace por fórmula UVR, RateManualDetailSurgicalId se devuelve NULL.; Cuando el cálculo se hace por detalle quirúrgico, se usa SalesValue del registro cuyo rango UVR contiene al UVRNumber del servicio padre.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'LogicUVRRateManualISS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Manual tarifario ISS; UVR (Unidad de Valor Relativo); Score / NewScore; Servicio IPS padre e hijo; Cirujano; Anestesiólogo; RightRoom (derechos de sala); Tarifa quirúrgica; Valor de venta (SalesValue); Liquidación de procedimientos quirúrgicos', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'LogicUVRRateManualISS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @ServiceOrderDetail: Si falta alguno de los parámetros (RateManualId, IPSServiceParentId o IPSServiceChildId en 0/null) se retorna StatusResult=0 con mensaje ''faltan parámetros en la función de obtención de valores ISS''.; [RETURN_RESULT] @ServiceOrderDetail: Si el ServiceManual del servicio padre es >= 3 se retorna StatusResult=0 con mensaje ''El tipo de manul no es ISS''.; [RETURN_RESULT] @ServiceOrderDetail: Si ServiceClass del hijo está en (2,3,4) Cirujano/Anestesiólogo, o ServiceClass=5 (RightRoom) con UVRNumber>450 y ApplyChangeScore=1, se calcula CostValueItem = UVRNumber * Score * ServiceAmount y se retorna StatusResult=1 con mensaje ''Valor generado exitosamente'' y RateManualDetailSurgicalId NULL.; [RETURN_RESULT] @ServiceOrderDetail: En los demás casos se busca en RateManualDetailSurgical (joineado con UVRRange) el detalle cuyo RateManualId e IPSServiceId coincidan y cuyo UVRNumber esté entre InitialUVR y EndUVR; si se encuentra, CostValueItem = SalesValue * ServiceAmount y se retorna StatusResult=1 con el RateManualDetailSurgicalId.; [RETURN_RESULT] @ServiceOrderDetail: Si no se encuentra detalle quirúrgico parametrizado se retorna StatusResult=0 con mensaje ''El servicio IPS QX no esta parámetrizado dentro del manual de tarifas''.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'LogicUVRRateManualISS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Falta alguno de los parámetros clave (RateManualId, IPSServiceParentId, IPSServiceChildId) en cero o nulo → Retornar error de parámetros faltantes y terminar else Continuar con la búsqueda del servicio padre/hijo; si ServiceManual del servicio padre >= 3 → Retornar error indicando que el tipo de manual no es ISS y terminar else Continuar con el cálculo según ServiceClass; si ApplyChangeScore=1 AND UVRNumber > 450 → Usar NewScore como score del cálculo else Usar Score base; si ServiceClass del hijo en (2,3,4) o (ServiceClass=5 AND UVRNumber>450 AND ApplyChangeScore=1) → Calcular costo como UVRNumber * Score * ServiceAmount else Buscar valor en RateManualDetailSurgical según rango UVR; si No se encuentra registro en RateManualDetailSurgical para el rango UVR → Retornar error ''El servicio IPS QX no esta parámetrizado dentro del manual de tarifas'' else Retornar costo calculado con el id del detalle quirúrgico', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'LogicUVRRateManualISS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.IPSService; Contract.RateManualDetailSurgical; Contract.UVRRange', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'LogicUVRRateManualISS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'LogicUVRRateManualISS';
GO

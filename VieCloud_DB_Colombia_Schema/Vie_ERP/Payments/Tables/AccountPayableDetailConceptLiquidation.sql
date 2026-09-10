CREATE TABLE [Payments].[AccountPayableDetailConceptLiquidation] (
    [Id]                                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableDetailConceptId]        INT             NOT NULL,
    [TotalIncome]                          DECIMAL (18)    NOT NULL,
    [PensionFundContribution]              DECIMAL (18)    NOT NULL,
    [PensionFundContributionReal]          DECIMAL (18)    NOT NULL,
    [VoluntaryPensionFundContribution]     DECIMAL (18)    NOT NULL,
    [VoluntaryPensionFundContributionReal] DECIMAL (18)    NOT NULL,
    [SolidarityPensionFund]                DECIMAL (18)    NOT NULL,
    [SolidarityPensionFundReal]            DECIMAL (18)    NOT NULL,
    [ContributionAccountAFC]               DECIMAL (18)    NOT NULL,
    [ContributionAccountAFCReal]           DECIMAL (18)    NOT NULL,
    [TotalIncomeExempt]                    DECIMAL (18)    NOT NULL,
    [TotalIncomeExemptReal]                DECIMAL (18)    NOT NULL,
    [PaymentCompulsoryHealth]              DECIMAL (18)    NOT NULL,
    [PaymentCompulsoryHealthReal]          DECIMAL (18)    NOT NULL,
    [PaymentPrepaidMedical]                DECIMAL (18)    NOT NULL,
    [PaymentPrepaidMedicalReal]            DECIMAL (18)    NOT NULL,
    [PaymentForDependent]                  DECIMAL (18)    NOT NULL,
    [PaymentForDependentReal]              DECIMAL (18)    NOT NULL,
    [HousingLoanInterest]                  DECIMAL (18)    NOT NULL,
    [HousingLoanInterestReal]              DECIMAL (18)    NOT NULL,
    [OccupationalRiskContribution]         DECIMAL (18)    NOT NULL,
    [OccupationalRiskContributionReal]     DECIMAL (18)    NOT NULL,
    [TotalDeduction]                       DECIMAL (18)    NOT NULL,
    [TotalDeductionReal]                   DECIMAL (18)    NOT NULL,
    [SubTotal]                             DECIMAL (18)    NOT NULL,
    [ExemptIncome]                         DECIMAL (18)    NOT NULL,
    [TaxableBase]                          DECIMAL (18)    NOT NULL,
    [RetentionValue383]                    DECIMAL (18)    NOT NULL,
    [RetentionValue384]                    DECIMAL (18)    NOT NULL,
    [ApplyRetention]                       DECIMAL (18)    NOT NULL,
    [MaxDeductionsAndRentExents]           DECIMAL (18)    NULL,
    [PensionByIndividualSavingsRegime]     DECIMAL (18)    CONSTRAINT [DF_AccountPayableDetailConceptLiquidation_PensionByIndividualSavingsRegime] DEFAULT ((0)) NOT NULL,
    [PensionByIndividualSavingsRegimeReal] DECIMAL (18)    CONSTRAINT [DF_AccountPayableDetailConceptLiquidation_PensionByIndividualSavingsRegimeReal] DEFAULT ((0)) NOT NULL,
    [UVT]                                  DECIMAL (18)    NULL,
    [SMLV]                                 DECIMAL (18)    NULL,
    [PreviousDeductionsForWithholdings]    DECIMAL (18)    NULL,
    [AccumulatedIncome]                    DECIMAL (18, 2) CONSTRAINT [DF__AccountPa__Accum__191DC3CA] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AccountPayableDetailConceptLiquidation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableDetailConceptLiquidation_AccountPayableDetailConcept] FOREIGN KEY ([AccountPayableDetailConceptId]) REFERENCES [Payments].[AccountPayableDetailConcept] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingresos acumulados del período, DECIMAL(18,2), suma progresiva de ingresos para cálculo de retención en la fuente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'AccumulatedIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingresos acumulados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'AccumulatedIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'AccumulatedIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deducciones previas acumuladas para retenciones, DECIMAL(18), afecta el cálculo de retención en la fuente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PreviousDeductionsForWithholdings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Acumula las deducciones previas de las retenciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PreviousDeductionsForWithholdings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PreviousDeductionsForWithholdings';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario Mínimo Legal Vigente (SMLV) utilizado en cálculos de pensión y topes, DECIMAL(18), referencia para solidaridad pensional y aportes obligatorios', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SMLV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del SMLV con la que se realizaron los cálculos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SMLV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SMLV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de Valor Tributario (UVT) vigente en los cálculos de deducciones y rentas exentas, DECIMAL(18), parámetro regulatorio para límites de deducción', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'UVT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la UVT con la que se realizaron los cálculos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'UVT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'UVT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado (Real) de cotizaciones voluntarias al régimen de ahorro individual con solidaridad, DECIMAL(18), sin límite ni condición aplicada', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionByIndividualSavingsRegimeReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Cotizaciones voluntarias al régimen de ahorro individual con solidaridad del sistema general de pensiones, Este valor es el mismo valor ingresado por el usuario ya que no tiene ninguna condicion o tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionByIndividualSavingsRegimeReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionByIndividualSavingsRegimeReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor digitado por usuario para cotizaciones voluntarias al régimen de ahorro individual con solidaridad, DECIMAL(18), antes de validación de topes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionByIndividualSavingsRegime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado por el usuario por el concepto de Cotizaciones voluntarias al régimen de ahorro individual con solidaridad del sistema general de pensiones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionByIndividualSavingsRegime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionByIndividualSavingsRegime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite máximo permitido para rentas exentas y deducciones (40% del ingreso, máx 5.040 UVT), DECIMAL(18), NULL si no aplica, tope regulatorio', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'MaxDeductionsAndRentExents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor máximo que podrá restarse por todas las rentas exentas y las deducciones, siempre que no excedan el cuarenta (40%) del resultado del inciso anterior, que en todo caso no puede exceder cinco mil cuarenta (5.040) UVT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'MaxDeductionsAndRentExents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'MaxDeductionsAndRentExents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de retención final a aplicar: mayor entre retención 383 y 384, DECIMAL(18), descuento por impuesto a la renta', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ApplyRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de la retencion que se va aplicar la cual va ser la mayor entre la 383 y la 384', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ApplyRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ApplyRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de retención según concepto 384 (impuesto a la renta), DECIMAL(18), alternativa de cálculo de retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'RetentionValue384';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor de retencion para el concepto 384', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'RetentionValue384';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'RetentionValue384';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de retención según concepto 383 (impuesto complementario), DECIMAL(18), alternativa de cálculo de retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'RetentionValue383';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de la retencion del concepto 383', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'RetentionValue383';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'RetentionValue383';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base gravable para impuesto a la renta: Subtotal - ExemptIncome, DECIMAL(18), fundamento del cálculo de retención en la fuente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TaxableBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor de la base gravable el cual es el Subtotal - ExemptIncome', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TaxableBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TaxableBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de renta exenta según topes (240 UVT o 25% del ingreso neto), DECIMAL(18), no sujeto a retención en la fuente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ExemptIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que esta exento de renta el cual depende de dos topes    Tope1 = 240 UVT  Tope2 = 25% del TotalIncome - PensionFundContributionReal - SolidaityPensionFundReal - PaymentCompulsoryHealth - PensionByIndividualSavingsRegimeReal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ExemptIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ExemptIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal neto: TotalIncome - TotalIncomeExemptReal - TotalDeductionReal, DECIMAL(18), paso intermedio hacia base gravable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SubTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del subtotal el cual es = TotalIncome - TotalIncomeExemptReal - TotalDeductionReal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SubTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SubTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total deducciones Real: suma de salud, medicina prepagada, dependientes, vivienda, riesgo laboral (valores calculados), DECIMAL(18), deducciones validadas con topes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeductionReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de deducciones Real el cual es la sumatoria de los campos (PaymentCompulsoryHealthReal + PaymentPrepaidMedicalReal + PaymentForDependentReal + HousingLoanInterestReal + OccupationalRiskContributionReal)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeductionReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeductionReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total deducciones ingresadas: suma de salud, medicina prepagada, dependientes, vivienda, riesgo laboral (valores usuario), DECIMAL(18), antes de validar topes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de deducciones el cual es la sumatoria de los campos (PaymentCompulsoryHealth + PaymentPrepaidMedical + PaymentForDependent + HousingLoanInterest + OccupationalRiskContribution)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeduction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de aportes a riesgos profesionales sin tope, DECIMAL(18), costo laboral de afiliación a ARL', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRiskContributionReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Aportes a riesgos profesionales. Este valor es igual al ingresado por el usuario ya que no tiene ninguna condicion o tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRiskContributionReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRiskContributionReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para aportes a riesgos profesionales, DECIMAL(18), aporte a Aseguradora de Riesgos Laborales', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRiskContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ingresado  por el usuario por el concepto de Aportes a riesgos profesionales', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRiskContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRiskContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de intereses por préstamo de vivienda (límite 100 UVT), DECIMAL(18), deducción por crédito hipotecario', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'HousingLoanInterestReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Intereses por prestamo de vivienda. Este valor puede ser diferente al ingresado por el usuario ya que depende de un tope:    Tope = 100 UVT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'HousingLoanInterestReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'HousingLoanInterestReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para intereses por préstamo de vivienda, DECIMAL(18), antes de aplicar tope de 100 UVT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'HousingLoanInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado  por el usuario por el concepto de Intereses por prestamo de vivienda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'HousingLoanInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'HousingLoanInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de pagos por dependientes (10% ingresos o 32 UVT, el menor), DECIMAL(18), deducción familiar limitada', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentForDependentReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de pagos por dependientes. Este valor puede ser diferente al ingresado por el usuario ya que este depende de un tope:    Tope1 = El 10% del total de los ingresos  Tope2 = 32 UVT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentForDependentReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentForDependentReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para pagos por dependientes, DECIMAL(18), antes de validar tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentForDependent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado por el usuario por el concepto de pagos por dependientes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentForDependent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentForDependent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de medicina prepagada/adicional (límite 16 UVT), DECIMAL(18), deducción por aseguramiento complementario', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentPrepaidMedicalReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Pagos por medicina prepagada, planes adicionales de salud y pagos por seguros de salud. Este valor puede ser diferente al ingresado por el usuario ya que depende de un tope:    El valor de los pagos por medicina prepagada no debe superarar los16 UVT  Tope = (16*UVT)  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentPrepaidMedicalReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentPrepaidMedicalReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para medicina prepagada, planes adicionales, seguros salud, DECIMAL(18), antes de aplicar tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentPrepaidMedical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado por el usuario por el concepto de Pagos por medicina prepagada, planes adicionales de salud y pagos por seguros de salud', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentPrepaidMedical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentPrepaidMedical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de salud obligatoria (fórmula: TotalIncome × 0.4 × 0.125), DECIMAL(18), aporte al sistema de salud sin tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentCompulsoryHealthReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Pagos De Salud Obligatoria. Este valor es igual al ingresado por el usuario ya que no tiene ninguna condicion o tope.    La formula para calcular por defecto el aporte a pension es = (TotalIncome * 0.4 * 0.125)  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentCompulsoryHealthReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentCompulsoryHealthReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para salud obligatoria (EPS), DECIMAL(18), costo de afiliación al régimen contributivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentCompulsoryHealth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado por el usuario por el concepto de Pagos De Salud Obligatoria', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentCompulsoryHealth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentCompulsoryHealth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total rentas exentas calculadas (límite 30% ingreso o 3.800 UVT anuales/12 mensual), DECIMAL(18), ingresos no tributarios validados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncomeExemptReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de rentas exentas calculada. Este valor puede ser diferente al valor normal ya que depende de un tope:    "30% del Ingreso tributario del año y hasta  3.800 UVT anuales"  la mas baja de estas dos condiciones sera el tope del total exento de renta    Tope1 = (TotalIncome * 0.30)  Tope2 = (Valor UVT * 3800 / 12)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncomeExemptReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncomeExemptReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total rentas exentas ingresadas: suma de pensión obligatoria, voluntaria, solidaridad, AFC, DECIMAL(18), antes de aplicar topes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncomeExempt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de rentas exentas el cual es la sumatoria de los campos (PensionFundContribution + VoluntaryPensionFundContribution + SolidarityPensionFund + ContributionAccountAFC)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncomeExempt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncomeExempt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de aportes a cuentas AFC sin tope, DECIMAL(18), aporte a cuenta de ahorro permanente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ContributionAccountAFCReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Aportes con Destino a cuentas AFC. Este valor es igual al ingresado por el usuario ya que no tiene ninguna condicion o tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ContributionAccountAFCReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ContributionAccountAFCReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para aportes con destino a cuentas AFC, DECIMAL(18), ahorro complementario obligatorio', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ContributionAccountAFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado por el usuario por el concepto de Aportes con Destino a cuentas AFC', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ContributionAccountAFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ContributionAccountAFC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de Fondo de Solidaridad Pensional (límite 1% de 25 SMLV), DECIMAL(18), aporte solidario del sistema pensional', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SolidarityPensionFundReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Fondo de Solidaridad Pensional. Este valor puede ser diferente al ingresado por el usuario ya que depende de un tope:    El valor del Fondo de Solidaridad Pensional no debe ser superior al 1% de 25 SMLV  Tope = (25*SMLV*0.01)    La formula para calcular por defecto el valor del fondo de solidaridad pensional es = (TotalIncome * 0.4 * 0.01)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SolidarityPensionFundReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SolidarityPensionFundReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para Fondo de Solidaridad Pensional, DECIMAL(18), antes de aplicar tope regulatorio', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SolidarityPensionFund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado por el usuario por el concepto de Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SolidarityPensionFund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SolidarityPensionFund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de aportes voluntarios a fondos de pensiones sin tope, DECIMAL(18), aporte adicional a pensión complementaria', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundContributionReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Aportes Voluntarios a Fondos de Pensiones. Este valor es igual al ingresado por el usuario ya que no tiene ninguna condicion o tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundContributionReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundContributionReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para aportes voluntarios a fondos de pensiones, DECIMAL(18), cotización extra pensional', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado por el usuario por el concepto de Aportes Voluntarios a Fondos de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado de aportes obligatorios a pensiones (límite 16% de 25 SMLV), DECIMAL(18), contribución obligatoria a AFP/SPP', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionFundContributionReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado del concepto de Aportes Obligatorios a Fondos de Pensiones. Este valor puede ser diferente al ingresado por el usuario ya que depende de un tope:    El valor de los aportes obligatorios en pension no deben ser superiores al 16% de 25 SMLV  Tope = (25*SMLV*0.16)      La formula para calcular por defecto el aporte a pension es = (TotalIncome * 0.4 * 0.16)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionFundContributionReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionFundContributionReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ingresado por usuario para aportes obligatorios a fondos de pensiones, DECIMAL(18), antes de aplicar tope regulatorio', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionFundContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor digitado por el usuario por el concepto de Aportes Obligatorios a Fondos de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionFundContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionFundContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de ingresos brutos del profesional/proveedor antes de deducciones, DECIMAL(18), base para cálculo de cotizaciones y retenciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Ingresos del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del concepto en cuenta por pagar, INT, referencia a AccountPayableDetailConcept', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'AccountPayableDetailConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'AccountPayableDetailConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'AccountPayableDetailConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de liquidación de cuenta por pagar, INT IDENTITY, clave primaria clusterizada', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidación de retención en la fuente por concepto de cuentas por pagar a terceros (honorarios, servicios). Guarda los valores calculados de ingresos, deducciones permitidas (salud, pensión, AFC, dependientes, intereses de vivienda), riesgos laborales, base gravable, UVT, SMLV y el valor de retención aplicado según los artículos 383 y 384 del Estatuto Tributario.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation';

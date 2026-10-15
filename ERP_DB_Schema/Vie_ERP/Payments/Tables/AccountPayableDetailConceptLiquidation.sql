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
ALTER TABLE [Payments].[AccountPayableDetailConceptLiquidation] NOCHECK CONSTRAINT [FK_AccountPayableDetailConceptLiquidation_AccountPayableDetailConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingresos acumulados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'AccumulatedIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumula las deducciones previas de las retenciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PreviousDeductionsForWithholdings';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del SMLV con la que se realizaron los cálculos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SMLV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la UVT con la que se realizaron los cálculos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'UVT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Cotizaciones voluntarias al régimen de ahorro individual con solidaridad del sistema general de pensiones, Este valor es el mismo valor ingresado por el usuario ya que no tiene ninguna condicion o tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionByIndividualSavingsRegimeReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado por el usuario por el concepto de Cotizaciones voluntarias al régimen de ahorro individual con solidaridad del sistema general de pensiones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionByIndividualSavingsRegime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor máximo que podrá restarse por todas las rentas exentas y las deducciones, siempre que no excedan el cuarenta (40%) del resultado del inciso anterior, que en todo caso no puede exceder cinco mil cuarenta (5.040) UVT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'MaxDeductionsAndRentExents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la retencion que se va aplicar la cual va ser la mayor entre la 383 y la 384', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ApplyRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor de retencion para el concepto 384', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'RetentionValue384';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la retencion del concepto 383', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'RetentionValue383';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor de la base gravable el cual es el Subtotal - ExemptIncome', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TaxableBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que esta exento de renta el cual depende de dos topes    Tope1 = 240 UVT  Tope2 = 25% del TotalIncome - PensionFundContributionReal - SolidaityPensionFundReal - PaymentCompulsoryHealth - PensionByIndividualSavingsRegimeReal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ExemptIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del subtotal el cual es = TotalIncome - TotalIncomeExemptReal - TotalDeductionReal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SubTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de deducciones Real el cual es la sumatoria de los campos (PaymentCompulsoryHealthReal + PaymentPrepaidMedicalReal + PaymentForDependentReal + HousingLoanInterestReal + OccupationalRiskContributionReal)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeductionReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total de deducciones el cual es la sumatoria de los campos (PaymentCompulsoryHealth + PaymentPrepaidMedical + PaymentForDependent + HousingLoanInterest + OccupationalRiskContribution)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeduction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Aportes a riesgos profesionales. Este valor es igual al ingresado por el usuario ya que no tiene ninguna condicion o tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRiskContributionReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ingresado  por el usuario por el concepto de Aportes a riesgos profesionales', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRiskContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Intereses por prestamo de vivienda. Este valor puede ser diferente al ingresado por el usuario ya que depende de un tope:    Tope = 100 UVT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'HousingLoanInterestReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado  por el usuario por el concepto de Intereses por prestamo de vivienda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'HousingLoanInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de pagos por dependientes. Este valor puede ser diferente al ingresado por el usuario ya que este depende de un tope:    Tope1 = El 10% del total de los ingresos  Tope2 = 32 UVT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentForDependentReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado por el usuario por el concepto de pagos por dependientes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentForDependent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Pagos por medicina prepagada, planes adicionales de salud y pagos por seguros de salud. Este valor puede ser diferente al ingresado por el usuario ya que depende de un tope:    El valor de los pagos por medicina prepagada no debe superarar los16 UVT  Tope = (16*UVT)  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentPrepaidMedicalReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado por el usuario por el concepto de Pagos por medicina prepagada, planes adicionales de salud y pagos por seguros de salud', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentPrepaidMedical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Pagos De Salud Obligatoria. Este valor es igual al ingresado por el usuario ya que no tiene ninguna condicion o tope.    La formula para calcular por defecto el aporte a pension es = (TotalIncome * 0.4 * 0.125)  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentCompulsoryHealthReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado por el usuario por el concepto de Pagos De Salud Obligatoria', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PaymentCompulsoryHealth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total de rentas exentas calculada. Este valor puede ser diferente al valor normal ya que depende de un tope:    "30% del Ingreso tributario del año y hasta  3.800 UVT anuales"  la mas baja de estas dos condiciones sera el tope del total exento de renta    Tope1 = (TotalIncome * 0.30)  Tope2 = (Valor UVT * 3800 / 12)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncomeExemptReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de rentas exentas el cual es la sumatoria de los campos (PensionFundContribution + VoluntaryPensionFundContribution + SolidarityPensionFund + ContributionAccountAFC)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncomeExempt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Aportes con Destino a cuentas AFC. Este valor es igual al ingresado por el usuario ya que no tiene ninguna condicion o tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ContributionAccountAFCReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado por el usuario por el concepto de Aportes con Destino a cuentas AFC', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'ContributionAccountAFC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Fondo de Solidaridad Pensional. Este valor puede ser diferente al ingresado por el usuario ya que depende de un tope:    El valor del Fondo de Solidaridad Pensional no debe ser superior al 1% de 25 SMLV  Tope = (25*SMLV*0.01)    La formula para calcular por defecto el valor del fondo de solidaridad pensional es = (TotalIncome * 0.4 * 0.01)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SolidarityPensionFundReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado por el usuario por el concepto de Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'SolidarityPensionFund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Aportes Voluntarios a Fondos de Pensiones. Este valor es igual al ingresado por el usuario ya que no tiene ninguna condicion o tope', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundContributionReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado por el usuario por el concepto de Aportes Voluntarios a Fondos de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado del concepto de Aportes Obligatorios a Fondos de Pensiones. Este valor puede ser diferente al ingresado por el usuario ya que depende de un tope:    El valor de los aportes obligatorios en pension no deben ser superiores al 16% de 25 SMLV  Tope = (25*SMLV*0.16)      La formula para calcular por defecto el aporte a pension es = (TotalIncome * 0.4 * 0.16)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionFundContributionReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor digitado por el usuario por el concepto de Aportes Obligatorios a Fondos de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'PensionFundContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Ingresos del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'TotalIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'AccountPayableDetailConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidation', @level2type = N'COLUMN', @level2name = N'Id';


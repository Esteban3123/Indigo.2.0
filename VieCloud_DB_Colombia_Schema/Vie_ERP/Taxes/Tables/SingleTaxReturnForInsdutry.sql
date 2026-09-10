CREATE TABLE [Taxes].[SingleTaxReturnForInsdutry] (
    [Id]                                   BIGINT         IDENTITY (20162000000001, 1) NOT FOR REPLICATION NOT NULL,
    [TaxablePeriod]                        INT            NOT NULL,
    [TypeStatement]                        TINYINT        NOT NULL,
    [NumberStatement]                      NVARCHAR (50)  NULL,
    [NameOrBusinessName]                   NVARCHAR (200) NOT NULL,
    [IdentificationType]                   TINYINT        NOT NULL,
    [IdentificationNumber]                 NVARCHAR (20)  NOT NULL,
    [PhoneNumber]                          NVARCHAR (20)  NULL,
    [NotificationAddress]                  NVARCHAR (100) NOT NULL,
    [PrincipalName]                        NVARCHAR (200) NULL,
    [NumEstablishmentsNeiva]               NUMERIC (18)   NULL,
    [NumEstablishmentsOtherCity]           NUMERIC (18)   NULL,
    [TotalTaxableOrdinaryAndExtraordinary] NUMERIC (18)   NULL,
    [TaxesOutsideCity]                     NUMERIC (18)   NULL,
    [TotalGrossIncomeCity]                 NUMERIC (18)   NULL,
    [ReturnsAndDiscounts]                  NUMERIC (18)   NULL,
    [DeductionsAndExemptions]              NUMERIC (18)   NULL,
    [TaxesNetIncome]                       NUMERIC (18)   NULL,
    [AnnualTaxOnIndustry]                  NUMERIC (18)   NULL,
    [AnnualTaxNotices]                     NUMERIC (18)   NULL,
    [AdditionalFinancialSector]            NUMERIC (18)   NULL,
    [TotalTaxesPaid]                       NUMERIC (18)   NULL,
    [FireDepartmentSurtax]                 NUMERIC (18)   NULL,
    [TiebackTax]                           NUMERIC (18)   NULL,
    [Advancetax]                           NUMERIC (18)   NULL,
    [AdvancePreviousYear]                  NUMERIC (18)   NULL,
    [PositiveBalance]                      NUMERIC (18)   NULL,
    [Sanctions]                            NUMERIC (18)   NULL,
    [TotalBalanceCharger]                  NUMERIC (18)   NULL,
    [TotalCreditBalance]                   NUMERIC (18)   NULL,
    [PaymentValidation]                    TINYINT        NULL,
    [AmountToBePaid]                       NUMERIC (18)   NULL,
    [AmountSanctions]                      NUMERIC (18)   NULL,
    [InterestOnArrears]                    NUMERIC (18)   NULL,
    [TotalToPay]                           NUMERIC (18)   NULL,
    [DeclarantName]                        NVARCHAR (150) NULL,
    [DeclarationBy]                        TINYINT        NULL,
    [IdentificationTypeProfessional]       TINYINT        NULL,
    [IdentificationNumberProfessional]     NVARCHAR (20)  NULL,
    [ProfessionalName]                     NVARCHAR (50)  NULL,
    [ProfessionalCardNumber]               NVARCHAR (20)  NULL,
    [Type]                                 TINYINT        CONSTRAINT [DF_SingleTaxReturnForInsdutry_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                         NCHAR (20)     NULL,
    [CreationDate]                         DATE           NULL,
    CONSTRAINT [PK_SingleTaxReturnForInsdutry] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro de declaración tributaria (DATE). Permite auditar cuándo se ingresó la declaración al sistema.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (NCHAR(20)). Identificador del operador o sistema que ingresó la declaración inicial.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de estado de declaración: 0=Borrador (sin confirmar), 1=Confirmada (enviada). Define si la declaración está lista para gestión tributaria.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de declaración: 0-Borrador, 1-Confimada', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tarjeta/cédula profesional del contador o revisor fiscal (NVARCHAR(20)). Credencial de ejercicio profesional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ProfessionalCardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de tarjeta profesional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ProfessionalCardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ProfessionalCardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del profesional contador o revisor fiscal que asesoró la declaración (NVARCHAR(50)).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ProfessionalName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre profesional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ProfessionalName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ProfessionalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cédula, NIT o TI del profesional asesor (NVARCHAR(20)). Identificación PII del contador/revisor.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationNumberProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de identificación profesional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationNumberProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationNumberProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento del profesional: 1=C.C. (Cédula Ciudadanía), 2=NIT (Número Identificación Tributaria), 3=T.I. (Tarjeta Identidad).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationTypeProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de indentificacion del profesional que ayudo a la declaracion:  1 - C.C.  2 - NIT.  3 - T.I.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationTypeProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationTypeProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de profesional que asistió: 1=Contador Público, 2=Revisor Fiscal. Indica quién validó la declaración.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeclarationBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de profesional que ayudo a la declaracion:  1 - Contador,  2 - Revisor Fiscal', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeclarationBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeclarationBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del declarante/responsable (NVARCHAR(150)). Quien legalmente firma la declaración.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeclarantName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del declarante.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeclarantName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeclarantName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total a pagar: impuestos + sanciones + intereses (NUMERIC(18)). Cifra final de obligación tributaria.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalToPay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de impuestos a pagar.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalToPay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalToPay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intereses por mora/retraso en pago (NUMERIC(18)). Costo por incumplimiento de plazo tributario.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'InterestOnArrears';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intereses de demora.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'InterestOnArrears';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'InterestOnArrears';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de sanciones tributarias impuestas (NUMERIC(18)). Castigos por infracciones tributarias.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AmountSanctions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Monto de las sanciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AmountSanctions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AmountSanctions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de impuesto a pagar según declaración (NUMERIC(18)). Base antes de intereses y sanciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AmountToBePaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Monto de importe a pagar.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AmountToBePaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AmountToBePaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación de pago: 1=Declaración con pago comprobado, 0=Sin pago. Indica si hay evidencia de cancelación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PaymentValidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Validacion de Pago:  1 - Declaracion con pago  0 - Declaracion sin pago', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PaymentValidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PaymentValidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo a favor del declarante (NUMERIC(18)). Monto que puede compensarse o solicitar devolución.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalCreditBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo total del crédito.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalCreditBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalCreditBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo a cargo/deuda del declarante (NUMERIC(18)). Obligación pendiente de pago.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalBalanceCharger';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargador de saldo total.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalBalanceCharger';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalBalanceCharger';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de sanciones tributarias (NUMERIC(18)). Multas por incumplimiento de normas tributarias.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Sanctions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sanciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Sanctions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Sanctions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo positivo/favorable acumulado (NUMERIC(18)). Diferencia cuando hay exceso de pagos o retenciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PositiveBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el saldo positivo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PositiveBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PositiveBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anticipo tributario del año anterior (NUMERIC(18)). Pago anticipado que se puede descontar.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AdvancePreviousYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el anticipo del año anterior.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AdvancePreviousYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AdvancePreviousYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anticipo de impuestos pagado en período actual (NUMERIC(18)). Abono anticipado sobre obligación tributaria.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Advancetax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el anticipo de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Advancetax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Advancetax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto de amarre/conexión (NUMERIC(18)). Gravamen especial por operaciones vinculadas.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TiebackTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Impuesto de amarre.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TiebackTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TiebackTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recargo para bomberos/cuerpo de bomberos (NUMERIC(18)). Contribución adicional para servicios de emergencia.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'FireDepartmentSurtax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recargo del personal de bomberos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'FireDepartmentSurtax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'FireDepartmentSurtax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de impuestos pagados/cancelados (NUMERIC(18)). Suma de todos los pagos tributarios efectuados.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalTaxesPaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de impuestos pagados.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalTaxesPaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalTaxesPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte/contribución adicional sector financiero (NUMERIC(18)). Gravamen especial para entidades financieras.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AdditionalFinancialSector';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el sector financiero adicional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AdditionalFinancialSector';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AdditionalFinancialSector';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Avisos/notificaciones fiscales anuales emitidas (NUMERIC(18)). Comunicaciones de obligaciones tributarias.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AnnualTaxNotices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Avisos fiscales anuales.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AnnualTaxNotices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AnnualTaxNotices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto anual sobre la industria/comercio (NUMERIC(18)). Gravamen principal de actividad económica.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AnnualTaxOnIndustry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Impuesto anual sobre la industria.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AnnualTaxOnIndustry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AnnualTaxOnIndustry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto sobre renta neta (NUMERIC(18)). Gravamen después de deducciones y exenciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxesNetIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Utilidad neta de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxesNetIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxesNetIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deducciones e exenciones aplicadas (NUMERIC(18)). Descuentos legales sobre base imponible.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeductionsAndExemptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena deducciones y exenciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeductionsAndExemptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeductionsAndExemptions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devoluciones y descuentos comerciales (NUMERIC(18)). Ajustes por mercancía devuelta o rebajas.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ReturnsAndDiscounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Devoluciones y descuentos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ReturnsAndDiscounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ReturnsAndDiscounts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingresos brutos totales en la ciudad (NUMERIC(18)). Renta sin descontar gastos ni deducciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalGrossIncomeCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingresos brutos totales de la ciudad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalGrossIncomeCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalGrossIncomeCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuestos generados fuera de la ciudad (NUMERIC(18)). Gravamen por actividad en otros municipios.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxesOutsideCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Impuestos fuera de la ciudad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxesOutsideCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxesOutsideCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base imponible ordinaria y extraordinaria (NUMERIC(18)). Total sujeto a imposición fiscal.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalTaxableOrdinaryAndExtraordinary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total imponible ordinario y extraordinario.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalTaxableOrdinaryAndExtraordinary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalTaxableOrdinaryAndExtraordinary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de establecimientos en otros municipios (NUMERIC(18)). Sucursales o puntos de operación foráneos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumEstablishmentsOtherCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de establecimientos registrados en otros municipios', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumEstablishmentsOtherCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumEstablishmentsOtherCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de establecimientos registrados en Neiva (NUMERIC(18)). Sucursales en jurisdicción de Neiva.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumEstablishmentsNeiva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de establecimientos registrados en Neiva', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumEstablishmentsNeiva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumEstablishmentsNeiva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del establecimiento principal o matriz (NVARCHAR(200)). Sede principal del negocio.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PrincipalName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre principal.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PrincipalName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PrincipalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección para notificaciones tributarias (NVARCHAR(100)). Domicilio donde se envían comunicaciones oficiales.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NotificationAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección de notificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NotificationAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NotificationAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico de contacto (NVARCHAR(20)). Teléfono para comunicaciones tributarias.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número telefónico.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cédula, NIT o TI del declarante (NVARCHAR(20)). Identificación PII del contribuyente.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de identificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento: 1=C.C., 2=NIT, 3=T.I. Clasifica el documento de identidad del declarante.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de documento de identidad del declarante:  1 - C.C.  2 - NIT  3 - T.I.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social de la empresa (NVARCHAR(200)). Identidad legal del contribuyente.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o razón social.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la declaración (NVARCHAR(50)). Requerido si TypeStatement es 2-7 (correcciones/clausura).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumberStatement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la declaración, es requerido si el tipo de la declaracion (TypeStatement) es:  2 - 3 - 4 - 5 - 6 - 7', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumberStatement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumberStatement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de declaración: 1=Inicial, 2=Corrección Voluntaria, 3=Corrección Especial, 4=Corrección Emplazamiento, 5=Corrección Liquidación Revisión, 6=Corrección Aritmética, 7=Clausura. Clasifica la naturaleza del trámite.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TypeStatement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de declaración:  1 - Declaración Inicial,   2 - Corrección Voluntaria,   3 - Corrección Req. especial,   4 - Corrección Emplaza/to,   5 - Corrección Liq. Revisión,   6 - Corrección Aritmética,   7- Clausura', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TypeStatement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TypeStatement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período gravable/fiscal de la declaración (INT). Año o período al cual corresponde el impuesto.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxablePeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el periodo gravable.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxablePeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxablePeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (BIGINT IDENTITY). Clave primaria de la declaración tributaria (PK).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Declaraciones únicas del impuesto de industria y comercio (ICA) presentadas por contribuyentes o empresas ante el municipio. Registra la información tributaria, ingresos gravables, deducciones, liquidación de impuestos, anticipos, sanciones e intereses de mora correspondientes a cada período fiscal.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry';

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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tarjeta profesional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ProfessionalCardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre profesional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ProfessionalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación profesional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationNumberProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de indentificacion del profesional que ayudo a la declaracion:  1 - C.C.  2 - NIT.  3 - T.I.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationTypeProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de profesional que ayudo a la declaracion:  1 - Contador,  2 - Revisor Fiscal', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeclarationBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del declarante.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeclarantName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de impuestos a pagar.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalToPay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intereses de demora.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'InterestOnArrears';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de las sanciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AmountSanctions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de importe a pagar.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AmountToBePaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Validacion de Pago:  1 - Declaracion con pago  0 - Declaracion sin pago', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PaymentValidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo total del crédito.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalCreditBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargador de saldo total.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalBalanceCharger';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sanciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Sanctions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el saldo positivo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PositiveBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el anticipo del año anterior.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AdvancePreviousYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el anticipo de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Advancetax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto de amarre.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TiebackTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recargo del personal de bomberos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'FireDepartmentSurtax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de impuestos pagados.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalTaxesPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el sector financiero adicional.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AdditionalFinancialSector';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Avisos fiscales anuales.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AnnualTaxNotices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto anual sobre la industria.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'AnnualTaxOnIndustry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Utilidad neta de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxesNetIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena deducciones y exenciones.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'DeductionsAndExemptions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devoluciones y descuentos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'ReturnsAndDiscounts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingresos brutos totales de la ciudad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalGrossIncomeCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuestos fuera de la ciudad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxesOutsideCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total imponible ordinario y extraordinario.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TotalTaxableOrdinaryAndExtraordinary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre principal.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PrincipalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de notificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NotificationAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de documento de identidad del declarante:  1 - C.C.  2 - NIT  3 - T.I.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la declaración, es requerido si el tipo de la declaracion (TypeStatement) es:  2 - 3 - 4 - 5 - 6 - 7', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'NumberStatement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de declaración:  1 - Declaración Inicial,   2 - Corrección Voluntaria,   3 - Corrección Req. especial,   4 - Corrección Emplaza/to,   5 - Corrección Liq. Revisión,   6 - Corrección Aritmética,   7- Clausura', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TypeStatement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el periodo gravable.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'TaxablePeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutry', @level2type = N'COLUMN', @level2name = N'Id';


CREATE TABLE [Inventory].[OtherWithholdingDeduction] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20) NOT NULL,
    [Type]                    TINYINT      NOT NULL,
    [AccountPayableConceptId] INT          NULL,
    [RoundType]               INT          NULL,
    [DeductionValue]          NUMERIC (18) NULL,
    [Status]                  BIT          NOT NULL,
    [CreationUser]            VARCHAR (20) NOT NULL,
    [CreationDate]            DATETIME     NOT NULL,
    [ModificationUser]        VARCHAR (20) NULL,
    [ModificationDate]        DATETIME     NULL,
    [TimeStamp]               ROWVERSION   NOT NULL,
    CONSTRAINT [PK_OtherWithholdingDeduction__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OtherWithholdingDeduction_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id])
);


GO
ALTER TABLE [Inventory].[OtherWithholdingDeduction] NOCHECK CONSTRAINT [FK_OtherWithholdingDeduction_AccountPayableConcepts];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OtherWithholdingDeduction__Code]
    ON [Inventory].[OtherWithholdingDeduction]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la deduccion, esta se muestra si el tipo es deduccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'DeductionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este se muestra solo si es de tipo retencion  Especifica como se va redondear los valores   10 -- Decima  100 -- Centecima  1000 -- Milesima    La formula que se usa es Round(valor / redondeo) * redondeo  Round(2005/ 10) * 10', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'RoundType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del concepto por pagar    Si el tipo es Retencion el concepto de la cuenta por pagar que seleccionen debe ser de tipo especifico y debe Manejar Retencion    Si el tipo es de Deduccion el concepto de la cuenta por pagar debe de ser de tipo Especifico y NO Debe Manejar Retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del registro  1 - Retención  2 - Deducción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la deduccion o retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la retencion o deduccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Id';


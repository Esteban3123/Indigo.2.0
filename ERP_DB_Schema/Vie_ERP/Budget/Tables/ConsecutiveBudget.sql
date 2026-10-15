CREATE TABLE [Budget].[ConsecutiveBudget] (
    [Id]                INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ValidityId]        INT     NOT NULL,
    [FormId]            INT     NOT NULL,
    [NumberConsecutive] INT     NOT NULL,
    [ProcessType]       TINYINT NULL,
    [Used]              BIT     CONSTRAINT [DF_ConsecutiveBudget_Used] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ConsecutiveBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsecutiveBudget_Validity] FOREIGN KEY ([ValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[ConsecutiveBudget] NOCHECK CONSTRAINT [FK_ConsecutiveBudget_Validity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si el consecutivo se encuentra en uso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'Used';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de proceso: 1->ingreso,2-> gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'ProcessType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'NumberConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del formulario en el que se encuentra bloqueado el registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'FormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de validez', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'ValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'Id';


CREATE TABLE [Inventory].[AdjustmentConceptUser] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [AdjustmentConceptId] INT          NOT NULL,
    [UserId]              INT          NOT NULL,
    [UserCode]            VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_AdjustmentConceptUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdjustmentConceptUser_AdjustmentConcept] FOREIGN KEY ([AdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id])
);


GO
ALTER TABLE [Inventory].[AdjustmentConceptUser] NOCHECK CONSTRAINT [FK_AdjustmentConceptUser_AdjustmentConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de usuarios por almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'Id';


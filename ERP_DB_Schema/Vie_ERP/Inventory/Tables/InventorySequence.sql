CREATE TABLE [Inventory].[InventorySequence] (
    [Id]                    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]                VARCHAR (5) NOT NULL,
    [IsManual]              BIT         CONSTRAINT [DF_InventorySequence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]                 VARCHAR (2) CONSTRAINT [DF_InventorySequence_Scope] DEFAULT ('O') NOT NULL,
    [Sequential]            BIT         CONSTRAINT [DF_InventorySequence_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]                  TINYINT     CONSTRAINT [DF_InventorySequence_Rate] DEFAULT ((3)) NOT NULL,
    [IdSequence]            INT         NULL,
    [RequireValidateLength] BIT         NULL,
    [MinimumLength]         TINYINT     NULL,
    [MaximumLength]         TINYINT     NULL,
    CONSTRAINT [PK_InventorySequence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventorySequence_Sequense] FOREIGN KEY ([IdSequence]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Inventory].[InventorySequence] NOCHECK CONSTRAINT [FK_InventorySequence_Sequense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad máxima de caracteres', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'MaximumLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad mínima de caracteres', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'MinimumLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si o no requiere validar la cantidad de caracteres', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'RequireValidateLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base usada cuando el ambito de la secuencia es de tipo caja o bancos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'IdSequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa de secuencias a generar por cada petición cuando la secuencia NO es continua', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si la secuencia es continua y se garantiza un consecutivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'Sequential';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ambito de la secuencia. O-Organización, OU-Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si es automatica o manual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del frontal alq ue aplica la secuencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequence', @level2type = N'COLUMN', @level2name = N'Id';


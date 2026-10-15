CREATE TABLE [Inventory].[ReasonCancellationOfDevolution] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [HCDEVMEDCId]  INT           NOT NULL,
    [HCMOANULBId]  CHAR (4)      NOT NULL,
    [Description]  VARCHAR (500) NOT NULL,
    [CreationUser] VARCHAR (20)  NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [HCDEVMEDDId]  INT           NULL,
    CONSTRAINT [PK_ReasonCancellationOfDevolution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReasonCancellationOfDevolution_HCDEVMEDC] FOREIGN KEY ([HCDEVMEDCId]) REFERENCES [dbo].[HCDEVMEDC] ([CODCONCEC]),
    CONSTRAINT [FK_ReasonCancellationOfDevolution_HCDEVMEDD] FOREIGN KEY ([HCDEVMEDDId]) REFERENCES [dbo].[HCDEVMEDD] ([Id]),
    CONSTRAINT [FK_ReasonCancellationOfDevolution_HCMOANULB] FOREIGN KEY ([HCMOANULBId]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU])
);


GO
ALTER TABLE [Inventory].[ReasonCancellationOfDevolution] NOCHECK CONSTRAINT [FK_ReasonCancellationOfDevolution_HCDEVMEDC];


GO
ALTER TABLE [Inventory].[ReasonCancellationOfDevolution] NOCHECK CONSTRAINT [FK_ReasonCancellationOfDevolution_HCDEVMEDD];


GO
ALTER TABLE [Inventory].[ReasonCancellationOfDevolution] NOCHECK CONSTRAINT [FK_ReasonCancellationOfDevolution_HCMOANULB];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de farmacia a anular ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCDEVMEDDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la anulacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del motivo de general ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCMOANULBId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la solicitud de la tabla [HCFARMEPC]', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCDEVMEDCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'Id';


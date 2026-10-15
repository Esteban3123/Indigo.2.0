CREATE TABLE [Inventory].[ReasonCancellationOfRequest] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [HCFARMEPCId]  NUMERIC (18)  NOT NULL,
    [HCMOANULBId]  CHAR (4)      NOT NULL,
    [Description]  VARCHAR (500) NOT NULL,
    [CreationUser] VARCHAR (20)  NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [HCFARMEPDId]  INT           NULL,
    CONSTRAINT [PK_ReasonCancellationOfRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPC] FOREIGN KEY ([HCFARMEPCId]) REFERENCES [dbo].[HCFARMEPC] ([CODCONCEC]),
    CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPD] FOREIGN KEY ([HCFARMEPDId]) REFERENCES [dbo].[HCFARMEPD] ([ID]),
    CONSTRAINT [FK_ReasonCancellationOfRequest_HCMOANULB] FOREIGN KEY ([HCMOANULBId]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU])
);


GO
ALTER TABLE [Inventory].[ReasonCancellationOfRequest] NOCHECK CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPC];


GO
ALTER TABLE [Inventory].[ReasonCancellationOfRequest] NOCHECK CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPD];


GO
ALTER TABLE [Inventory].[ReasonCancellationOfRequest] NOCHECK CONSTRAINT [FK_ReasonCancellationOfRequest_HCMOANULB];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de farmacia a anular ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCFARMEPDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la anulacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del motivo de general ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCMOANULBId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la solicitud de la tabla [HCFARMEPC]', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCFARMEPCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'Id';


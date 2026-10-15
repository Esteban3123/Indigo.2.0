CREATE TABLE [dbo].[HCPAQRIASD] (
    [ID]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC] INT NOT NULL,
    [IDRIAS]          INT NOT NULL,
    CONSTRAINT [PK_HCPAQRIASD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAQRIASD_HCPAQORDENESC] FOREIGN KEY ([IDHCPAQORDENESC]) REFERENCES [dbo].[HCPAQORDENESC] ([ID]),
    CONSTRAINT [FK_HCPAQRIASD_PRMODELOHC] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCPAQRIASD] NOCHECK CONSTRAINT [FK_HCPAQRIASD_HCPAQORDENESC];


GO
ALTER TABLE [dbo].[HCPAQRIASD] NOCHECK CONSTRAINT [FK_HCPAQRIASD_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de las RIAS o rutas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera de las ordenes de paquetes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'ID';


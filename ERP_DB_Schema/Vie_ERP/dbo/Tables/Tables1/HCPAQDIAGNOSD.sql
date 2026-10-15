CREATE TABLE [dbo].[HCPAQDIAGNOSD] (
    [ID]              INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC] INT      NOT NULL,
    [CODDIAGNO]       CHAR (4) NOT NULL,
    CONSTRAINT [PK_HCPAQDIAGNOSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAQDIAGNOSD_HCPAQORDENESC] FOREIGN KEY ([IDHCPAQORDENESC]) REFERENCES [dbo].[HCPAQORDENESC] ([ID]),
    CONSTRAINT [FK_HCPAQDIAGNOSD_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCPAQDIAGNOSD] NOCHECK CONSTRAINT [FK_HCPAQDIAGNOSD_HCPAQORDENESC];


GO
ALTER TABLE [dbo].[HCPAQDIAGNOSD] NOCHECK CONSTRAINT [FK_HCPAQDIAGNOSD_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de cabecera de los paquetes de ordenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'ID';


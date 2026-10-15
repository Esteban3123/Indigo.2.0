CREATE TABLE [dbo].[HCPAQRIESGOSD] (
    [ID]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC] INT NOT NULL,
    [IDPRHCEXPRES]    INT NOT NULL,
    CONSTRAINT [PK_HCPAQRIESGOSD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de expresion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'IDPRHCEXPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera de la tabla de paquetes de ordenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'ID';

